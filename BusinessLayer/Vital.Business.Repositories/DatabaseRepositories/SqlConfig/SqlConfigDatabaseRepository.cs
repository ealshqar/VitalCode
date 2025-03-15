using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Vital.Business.Repositories.Properties;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.SqlConfig
{
    public class SqlConfigDatabaseRepository : BaseRepository,ISqlConfigRepository
    {
        #region Fields
        
        private readonly SqlConnection _sqlCon;
        private readonly SqlConnectionStringBuilder _builder;
        private readonly string _serverName;
        private readonly string _sqlDomainGroup;
        private readonly string _backupDirectory;
        private readonly FileSystemAccessRule sqlPermissionRule;

        #endregion

        #region Constructor

        /// <summary>
        /// SqlConfigDatabaseRepository Constructor.
        /// </summary>
        public SqlConfigDatabaseRepository()
        {
            _sqlCon = new SqlConnection
                          {
                              ConnectionString = ConfigurationManager.ConnectionStrings[StaticKeys.MainConnectionStringKey].ConnectionString
                          };
            
            _builder = new SqlConnectionStringBuilder
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[StaticKeys.MainConnectionStringKey].ConnectionString
            };
            _serverName = _builder.DataSource;
            var server = new Server(_serverName);
            _backupDirectory = server.MasterDBPath;
            _sqlDomainGroup = server.SqlDomainGroup;

            //Get the permission for the sql login that the system will use to excute the sql task
            //_sqlDomainGroup it should be like this: [Machine Name]\SQLServerMSSQLUser$[Machine Name]$[SQL INSTANCE Name (MSSQLSERVER OR SQLEXPRESS)]
            //Also make sure this signature is used to create the access rule sinc, If it's not inherited in the good way, 
            //you'll see your permissions only in 'special permission' and not 'FULL Control'
            sqlPermissionRule = new FileSystemAccessRule(_sqlDomainGroup, FileSystemRights.FullControl,
                                                         InheritanceFlags.ContainerInherit |//IMPORTANT: USE FOR FULL CONTROL
                                                         InheritanceFlags.ObjectInherit,    //IMPORTANT: USE FOR FULL CONTROL
                                                         PropagationFlags.None,             //IMPORTANT: USE FOR FULL CONTROL
                                                         AccessControlType.Allow);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs a sql DB task
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void CreateDatabase(string filePath)
        {
            var sourceDirectory = Path.GetDirectoryName(Application.StartupPath + @"\");
            bool userCancelledPermission;

            var hasPermission = GrantSQLUserPermission(sourceDirectory, out userCancelledPermission);
            try
            {
                var script = Resources.DBCreate;
                script = script.Replace("@FILEPATH@", filePath);
                script = script.Replace("@DATAPATH@", _backupDirectory + @"\");
                RunScript(script, StaticKeys.DataBaseName, true);
            }
            catch (VitalDatabaseException exception)
            {
                throw new VitalDatabaseException(exception);
            }
            finally
            {
                if (!userCancelledPermission)
                {
                    RestoreFolderPermission(sourceDirectory, hasPermission);
                }
            }
        }

        /// <summary>
        /// Runs an update script on DB
        /// </summary>
        /// <param name="updateScript"></param>
        public bool RunUpdateScript(string updateScript)
        {
            return RunScript(updateScript, StaticKeys.DataBaseName, true).IsSucceed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string PreparePathForSQLSyntax(string path)
        {
            return path.Replace("'", "''");
        }

        /// <summary>
        /// Calls logic for the backup
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="branch"></param>
        /// <param name="vitalVersion"></param>
        /// <param name="dbVersion"></param>
        public ProcessResult Backup(string destination, string branch, string vitalVersion, string dbVersion)
        {
            var result = ProcessResult.Succeed;

            bool hasPermission = false;
            bool userCancelledPermission = false;

            try
            {
                hasPermission = GrantSQLUserPermission(destination, out userCancelledPermission);
            }
            catch (UnauthorizedAccessException exception)
            {
                result.IsSucceed = false;
                result.Message = exception.Message;
            }
            
            try
            {
                //Perform the backup process
                var script = Resources.DBBackup;
                script = script.Replace("@PATH", PreparePathForSQLSyntax(destination));
                script = script.Replace("@BRANCH", branch);
                script = script.Replace("@VITALVERSION", vitalVersion);
                script = script.Replace("@DBVERSION", dbVersion);

                result =  RunScript(script, StaticKeys.DataBaseName, false);
            }
            catch (VitalDatabaseException exception)
            {
                result.IsSucceed = false;
                result.Message = exception.Message;
            }
            finally
            {
                if (!userCancelledPermission)
                {
                    RestoreFolderPermission(destination, hasPermission);
                }
            }

            return result;
        }

        /// <summary>
        /// Calls logic for the verification of a backup for restore
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strDBInfo"></param>
        public DatabaseVerificationObject VerifyBackup(string source, ref string strDBInfo)
        {
            var sourceDirectory = Path.GetDirectoryName(source);
            bool hasPermission = false;
            bool userCancelledPermission;

            try
            {
                hasPermission = GrantSQLUserPermission(sourceDirectory, out userCancelledPermission);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new VitalDatabaseException(StaticKeys.DatabaseBackupPermissionError, exception);
            }
            
            var verifyScript = Resources.DBVerify;
            verifyScript = verifyScript.Replace("@PATH", PreparePathForSQLSyntax(source));
            verifyScript = verifyScript.Replace("@DATAPATH@", _backupDirectory + @"\");

            if (RunScript(verifyScript, StaticKeys.TempDataBaseName,true).IsSucceed)
            {
                try
                {
                    //THIS IS VERY IMPORTANT FOR THE RESTORE PROCESS, IT WILL CLEAR CONNECTIONS AFTER RESTORE SO WE CAN USE DB AGAIN
                    SqlConnection.ClearAllPools();
                    _sqlCon.Open();

                    try
                    {
                        var databaseVerificationObject = new DatabaseVerificationObject();

                        //Verify that the Database exists
                        var sqlCmd = new SqlCommand("SELECT [AppInfo_Value] FROM [VitalExpertTemp].[dbo].[AppInfo] WHERE [AppInfo_Property]='Version'", _sqlCon);
                        string strVersion = sqlCmd.ExecuteScalar().ToString();

                        sqlCmd = new SqlCommand("SELECT COUNT(*) FROM [VitalExpertTemp].[dbo].[Patients]", _sqlCon);
                        string strPatientsCount = sqlCmd.ExecuteScalar().ToString();

                        //get the app branch - database
                        sqlCmd = new SqlCommand("SELECT [AppInfo_Value] FROM [VitalExpertTemp].[dbo].[AppInfo] WHERE [AppInfo_Property]='AppBranch'", _sqlCon);
                        var scalarValue = sqlCmd.ExecuteScalar();
                        string appBrnach = scalarValue != null ? scalarValue.ToString() : ApplicationBranches.UNKOWN.ToString();

                        //get the app branch - app.config
                        var appBranchConfigValue = ConfigurationManager.AppSettings["AppBranch"];

                        sqlCmd = new SqlCommand("SELECT TOP(5) Patient_FirstName + ' ' + Patient_LastName AS Name,  CONVERT(VARCHAR(MAX), [Patient_CreationDateTime], 101) AS Date FROM [VitalExpertTemp].[dbo].[Patients] ORDER BY [Patient_Id] DESC", _sqlCon);
                        var dataAdapter = new SqlDataAdapter(sqlCmd);
                        var lastPatients = new DataTable();
                        dataAdapter.Fill(lastPatients);
                        dataAdapter.Dispose();

                        var infoString = string.Empty;

                        infoString += "Application Version : " + strVersion + "\n" +
                                      "Patients Count        : " + strPatientsCount + "\n\n";

                        if (lastPatients.Rows.Count > 0)
                        {
                            infoString += "Date, Patient Name:" + "\n" +
                                          "---------------------------"+"\n";

                            infoString = lastPatients.Rows.Cast<DataRow>().Aggregate(infoString, (current, patientRow) => current + (patientRow["Date"] + ", " + patientRow["Name"] + "\n"));
                        }

                        strDBInfo = infoString;

                        _sqlCon.Close();

                        var dropScript = Resources.DBDrop;
                        RunScript(dropScript, StaticKeys.TempDataBaseName, false);

                        
                        databaseVerificationObject.IsApplicationBranchCompatible =  string.IsNullOrEmpty(appBrnach) ||
                                                                                    string.IsNullOrEmpty(appBranchConfigValue) ||
                                                                                    (appBrnach == appBranchConfigValue);
                        databaseVerificationObject.Version = strVersion;
                        databaseVerificationObject.ApplicationBranch = GetApplicationBranch(appBrnach);

                        return databaseVerificationObject;
                    }
                    catch (SqlException exception)
                    {
                        throw new VitalDatabaseException(exception);
                    }                    
                }
                catch (SqlException exception)
                {
                    throw new VitalDatabaseException(exception);
                }
                finally
                {
                    if (!userCancelledPermission)
                    {
                        RestoreFolderPermission(sourceDirectory, hasPermission);
                    }
                }
            }
            return new DatabaseVerificationObject() { Version = string.Empty };
        }

        /// <summary>
        /// Gets the application enum.
        /// </summary>
        /// <param name="appBrnach">The app brnach key.</param>
        /// <returns></returns>
        private ApplicationBranches GetApplicationBranch(string appBrnach)
        {
            if (string.IsNullOrEmpty(appBrnach)) return ApplicationBranches.UNKOWN;

            var branch = ApplicationBranches.UNKOWN;

            try
            {
                branch = EnumNameResolver.StringAsEnum<ApplicationBranches>(appBrnach);
            }
            catch (Exception)
            {
                
            }

            return branch;
        }

        /// <summary>
        /// Calls logic for the restore
        /// </summary>
        /// <param name="source"></param>
        public void Restore(string source)
        {
            var sourceDirectory = Path.GetDirectoryName(source);
            bool hasPermission = false;
            bool userCancelledPermission;
            try
            {
                hasPermission = GrantSQLUserPermission(sourceDirectory, out userCancelledPermission);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new VitalDatabaseException(StaticKeys.DatabaseBackupPermissionError, exception);
            }

            try
            {
                var script = string.Format(Resources.DBRestore, Application.StartupPath + @"\");
                script = script.Replace("@PATH", PreparePathForSQLSyntax(source));
                script = script.Replace("@DATAPATH@", _backupDirectory + @"\");
                RunScript(script, StaticKeys.DataBaseName, true);
            }
            catch (VitalDatabaseException exception)
            {
                throw new VitalDatabaseException(exception);
            }
            finally
            {
                if (!userCancelledPermission)
                {
                    RestoreFolderPermission(sourceDirectory, hasPermission);
                }
            }            
        }

        /// <summary>
        /// Run a script using ADO.Net to apply custom commands to the DB
        /// </summary>        
        /// <param name="script"></param>
        /// <param name="dbName"> </param>
        /// <param name="checkDBExists"> </param>
        /// <returns></returns>
        private ProcessResult RunScript(string script, string dbName, bool checkDBExists)
        {
            var result = ProcessResult.Succeed;

            try
            {
                string scriptFileName = Path.GetRandomFileName();
                string outputFileName = Path.GetRandomFileName();
                string scriptFilePath = Application.StartupPath + @"\" + scriptFileName;
                string outputFilePath = Application.StartupPath + @"\" + outputFileName;

                //Surround script with try catch to catch scrip errors
                var tryCatchScript = "BEGIN TRY" + Environment.NewLine +
                                     script + Environment.NewLine +
                                     "PRINT '" + StaticKeys.VITALSCRIPTSUCCESS + "'" + Environment.NewLine +
                                     "END TRY" + Environment.NewLine +
                                     "BEGIN CATCH" + Environment.NewLine +
                                     "PRINT ERROR_MESSAGE()" + Environment.NewLine +
                                     "END CATCH";


                File.WriteAllText(scriptFilePath, tryCatchScript);

                string commandLineString = "-S \"" + _serverName + "\" -i \"" + scriptFilePath + "\" -E -o \"" + outputFilePath + "\"";

                var info = new ProcessStartInfo("SQLCMD", commandLineString)
                               {
                                   WindowStyle = ProcessWindowStyle.Hidden,
                                   RedirectStandardOutput = true,
                                   RedirectStandardError = true,
                                   UseShellExecute = false,
                                   CreateNoWindow = true
                               };                
                var dbProcess = Process.Start(info);
                File.AppendAllText(outputFilePath, dbProcess.StandardOutput.ReadToEnd());
                dbProcess.StandardOutput.ReadToEnd();
                dbProcess.WaitForExit();
                dbProcess.StandardOutput.ReadToEnd();
                
                if (dbProcess.ExitCode > 0)
                {
                    dbProcess.Close();
                    result.IsSucceed = false;
                    return result;
                }

                dbProcess.Close();

                var outputText = File.ReadAllText(outputFilePath);

                File.Delete(scriptFilePath);
                File.Delete(outputFilePath);

                if (!outputText.Contains(StaticKeys.VITALSCRIPTSUCCESS))
                {
                    //var reader = new StringReader(outputText);
                    //reader.ReadLine();
                    ////Get the second line where the error is
                    //string error = reader.ReadLine();
                    //throw new VitalDatabaseException(outputText);
                    result.IsSucceed = false;
                    result.Message = outputText;
                    return result;
                }

                if (checkDBExists)
                {
                    while (true)
                    {
                        if (CheckDbExists(dbName).CheckStatus == DBCheckStatusEnum.ExistsAndConnected)
                            break;
                    }
                }
                return result;
            }
            catch (Exception exception)
            {
                result.IsSucceed = false;
                result.Message = exception.Message;
            }

            return result;
        }

        /// <summary>
        /// This function checks if the DB exists or not
        /// </summary>
        /// <returns>True if the DB exists</returns>
        public DBCheckState CheckDbExists(string dbName)
        {
            var masterSQLCon = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[StaticKeys.MasterConnectionStringKey].ConnectionString
            };

            try
            {
                //THIS IS VERY IMPORTANT FOR THE RESTORE PROCESS, IT WILL CLEAR CONNECTIONS AFTER RESTORE SO WE CAN USE DB AGAIN
                SqlConnection.ClearAllPools();

                //Check connection to master DB
                masterSQLCon.Open();

                //Connection to master succeeded

                //Verify that the Database exists
                var sqlCmdCheckVitalDB = new SqlCommand("SELECT COUNT(*) FROM [MASTER].[SYS].[DATABASES] WHERE NAME='" + dbName + "'", masterSQLCon);
                var strResult = sqlCmdCheckVitalDB.ExecuteScalar().ToString();
                masterSQLCon.Close();

                if (strResult != "0")
                {
                    //Vital DB Exists
                    try
                    {
                        SqlConnection.ClearAllPools();
                        //Check connection with Vital DB
                        masterSQLCon.Open();
                        //Vital DB Connected
                        return new DBCheckState() { CheckStatus = DBCheckStatusEnum.ExistsAndConnected };
                    }
                    catch (SqlException exception)
                    {
                        //Vital DB Exists but no connection
                        return new DBCheckState() { CheckStatus = DBCheckStatusEnum.ConnectionError, CheckException = exception };
                    }
                }
                else
                {
                    //Vital DB Doesn't Exist based on query test
                    //Check if Vital DB Files exist
                    try
                    {
                        if (File.Exists(_backupDirectory + @"\" + StaticKeys.DataBaseName + StaticKeys.DataBaseExtension))
                        {
                            //The DB wasn't found by query but DB files exists in system.
                            return new DBCheckState() { CheckStatus = DBCheckStatusEnum.ConnectionError };
                        }
                        else
                        {
                            return new DBCheckState() { CheckStatus = DBCheckStatusEnum.DatabaseNotFound };
                        }
                    }
                    catch (Exception exception)
                    {
                        //Couldn't access the DB files
                        return new DBCheckState() { CheckStatus = DBCheckStatusEnum.FilesAccessError, CheckException = exception };
                    }

                }
            }
            catch (SqlException exception)
            {
                //Connection to Master failed
                return new DBCheckState() { CheckStatus = DBCheckStatusEnum.ConnectionError, CheckException = exception };
            }
        }

        /// <summary>
        /// Gives permission for SQL Server login user for current instance to access and modify files in certain folder
        /// </summary>
        /// <param name="destination"></param>
        private bool GrantSQLUserPermission(string destination, out bool userCancelledPermission)
        {
            try
            {
                var grantSQLUserPermissionScript =
                    VitalLogicScripts.GrantSQLUserPermissionScript.Replace(StaticKeys.ConnectionStringPlaceHolder,
                    ConfigurationManager.ConnectionStrings[StaticKeys.MainConnectionStringKey].ConnectionString);

                grantSQLUserPermissionScript = grantSQLUserPermissionScript.Replace(StaticKeys.FolderLocationPlaceHolder, destination);

                var result = VitalLogicExecuter.ExecuteMethod(VitalLogicScripts.GrantSQLUserPermissionMethod,
                                                                grantSQLUserPermissionScript,
                                                                VitalLogicScripts.GrantSQLUserPermissionAssemblies,
                                                                VitalLogicScripts.GrantSQLUserPermissionAssembliesDLLs,
                                                                out userCancelledPermission);

                return result.IsSucceed && bool.Parse(result.Message);
            }
            catch (VitalDatabaseException exception)
            {
                throw new VitalDatabaseException(exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new UnauthorizedAccessException(StaticKeys.DatabaseBackupPermissionError,exception);
            }
        }

        /// <summary>
        /// Restores certain folder permission
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="hasPermission"> </param>
        private void RestoreFolderPermission(string destination, bool hasPermission)
        {
            try
            {
                bool userCancelledPermission;

                var restoreFolderPermissionScript =
                    VitalLogicScripts.RestoreFolderPermissionScript.Replace(StaticKeys.ConnectionStringPlaceHolder,
                    ConfigurationManager.ConnectionStrings[StaticKeys.MainConnectionStringKey].ConnectionString);

                restoreFolderPermissionScript = restoreFolderPermissionScript.Replace(StaticKeys.FolderLocationPlaceHolder, destination);
                restoreFolderPermissionScript = restoreFolderPermissionScript.Replace(StaticKeys.ValuePlaceHolder, hasPermission.ToString().ToLower());

                var result = VitalLogicExecuter.ExecuteMethod(VitalLogicScripts.RestoreFolderPermissionMethod,
                                                                restoreFolderPermissionScript,
                                                                VitalLogicScripts.RestoreFolderPermissionAssemblies,
                                                                VitalLogicScripts.RestoreFolderPermissionAssembliesDLLs,
                                                                out userCancelledPermission);
            }
            catch (VitalDatabaseException exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion 
    }
}
