using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Shared
{
    public class VitalLogicScripts
    {

        #region Assemblies

        private const string System = "System";
        private const string SystemDLL = "System";

        private const string ServiceProcess = "System.ServiceProcess";
        private const string ServiceProcessDLL = "System.ServiceProcess";

        private const string AccessControl = "System.Security.AccessControl";
        private const string AccessControlDLL = "System";

        private const string Principal = "System.Security.Principal";
        private const string PrincipalDLL = "System";

        private const string Linq = "System.Linq";
        private const string LinqDLL = "System.Core";

        private const string SqlClient = "System.Data.SqlClient";
        private const string SqlClientDLL = "System.Data";

        private const string SqlServerSmo = "Microsoft.SqlServer.Management.Smo";
        private const string SqlServerSmoDLL = "Microsoft.SqlServer.Smo";
        private const string SqlServerSdkDLL = "Microsoft.SqlServer.Management.Sdk.Sfc";
        private const string SqlServerConnectionInfoDLL = "Microsoft.SqlServer.ConnectionInfo";

        #endregion

        #region SQL Server Service Startup

        public static List<string> StartSqlServiceAssemblies = new List<string> { System, ServiceProcess };
        public static List<string> StartSqlServiceAssembliesDLLs = new List<string> { SystemDLL, ServiceProcessDLL };
        public static string StartSqlServiceMethod = "CheckForService";
        public static string StartSqlServiceScript =
            @"private static void CheckForService(out string executionResult, out string executionLog)
                {
                    var sc = new ServiceController("""+ StaticKeys.SqlExpressServiceName + @""");

                    switch (sc.Status)
                    {
                        case ServiceControllerStatus.Paused:
                            sc.Continue();
                            break;
                        case ServiceControllerStatus.Stopped:
                            sc.Start();
                            break;
                        case ServiceControllerStatus.PausePending:
                            sc.WaitForStatus(ServiceControllerStatus.Paused);
                            sc.Continue();
                            break;
                        case ServiceControllerStatus.StopPending:
                            sc.WaitForStatus(ServiceControllerStatus.Stopped);
                            sc.Start();
                            break;
                        case ServiceControllerStatus.ContinuePending:
                        case ServiceControllerStatus.StartPending:
                            break;
                    }
                    try
                    {
                        sc.WaitForStatus(ServiceControllerStatus.Running);
                        executionResult = """+ StaticKeys.VITALSCRIPTSUCCESS + @""";
                        executionLog = string.Empty;
                    }
                    catch (Exception exception)
                    {
                        executionResult = """ + StaticKeys.VITALSCRIPTFAIL + @""";
                        executionLog = exception.Message + Environment.NewLine +  exception.InnerException;
                    }
                }";

        #endregion

        #region Grant Access Permissions

        public static List<string> GrantSQLUserPermissionAssemblies = new List<string> { System, AccessControl, Linq, SqlClient, SqlServerSmo };
        public static List<string> GrantSQLUserPermissionAssembliesDLLs = new List<string> { SystemDLL, AccessControlDLL, LinqDLL, SqlClientDLL, SqlServerSmoDLL, SqlServerSdkDLL, SqlServerConnectionInfoDLL };
        public static string GrantSQLUserPermissionMethod = "GrantSQLUserPermission";
        public static string GrantSQLUserPermissionScript =
            @"private static void GrantSQLUserPermission(out string executionResult, out string executionLog)
            {
                try
                {
                    var sqlCon = new SqlConnection
                    {
                        ConnectionString = @""" + StaticKeys.ConnectionStringPlaceHolder + @"""
                    };

                    var builder = new SqlConnectionStringBuilder
                    {
                        ConnectionString = @""" + StaticKeys.ConnectionStringPlaceHolder + @"""
                    };
                    var serverName = builder.DataSource;
                    var server = new Server(serverName);
                    var backupDirectory = server.MasterDBPath;
                    var sqlDomainGroup = server.SqlDomainGroup;

                    //Get the directory info for current backup source
                    var myDirectoryInfo = new DirectoryInfo(@""" + StaticKeys.FolderLocationPlaceHolder + @""");
                    var myDirectorySecurity = myDirectoryInfo.GetAccessControl();

                    //Get the permission for the sql login that the system will use to excute the sql task
                    //_sqlDomainGroup it should be like this: [Machine Name]\SQLServerMSSQLUser$[Machine Name]$[SQL INSTANCE Name (MSSQLSERVER OR SQLEXPRESS)]
                    //Also make sure this signature is used to create the access rule sinc, If it's not inherited in the good way, 
                    //you'll see your permissions only in 'special permission' and not 'FULL Control'
                    var sqlPermissionRule = new FileSystemAccessRule(sqlDomainGroup, FileSystemRights.FullControl,
                                                                                    InheritanceFlags.ContainerInherit |//IMPORTANT: USE FOR FULL CONTROL
                                                                                    InheritanceFlags.ObjectInherit,    //IMPORTANT: USE FOR FULL CONTROL
                                                                                    PropagationFlags.None,             //IMPORTANT: USE FOR FULL CONTROL
                                                                                    AccessControlType.Allow);


                    //Check if the rule exists originally
                    var hasPermission = myDirectorySecurity.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier))
                        .Cast<AuthorizationRule>().Any(existingRule =>
                            existingRule.IdentityReference.Value == sqlPermissionRule.IdentityReference.Value);

                    //If the rule doesn't exist then add it to avoid permission issues
                    if (!hasPermission)
                    {
                        myDirectorySecurity.AddAccessRule(sqlPermissionRule);
                        myDirectoryInfo.SetAccessControl(myDirectorySecurity);
                    }
                    executionResult = """ + StaticKeys.VITALSCRIPTSUCCESS + @""";
                    executionLog = hasPermission.ToString();

                }
                catch (Exception exception)
                {
                    executionResult = """ + StaticKeys.VITALSCRIPTFAIL + @""";
                    executionLog = exception.Message + Environment.NewLine +  exception.InnerException;
                }
            }";

        #endregion

        #region Restore Folder Permission

        public static List<string> RestoreFolderPermissionAssemblies = new List<string> { System, AccessControl, Linq, SqlClient, SqlServerSmo };
        public static List<string> RestoreFolderPermissionAssembliesDLLs = new List<string> { SystemDLL, AccessControlDLL, LinqDLL, SqlClientDLL, SqlServerSmoDLL, SqlServerSdkDLL, SqlServerConnectionInfoDLL };
        public static string RestoreFolderPermissionMethod = "RestoreFolderPermission";
        public static string RestoreFolderPermissionScript =
            @"private static void RestoreFolderPermission(out string executionResult, out string executionLog)
            {
                try
                {
                    //Remove the permission for the rule if it wasn't there originally
                    var hasPermission = " + StaticKeys.ValuePlaceHolder + @";
                    if (!hasPermission)
                    {
                        var sqlCon = new SqlConnection
                        {
                            ConnectionString = @""" + StaticKeys.ConnectionStringPlaceHolder + @"""
                        };

                        var builder = new SqlConnectionStringBuilder
                        {
                            ConnectionString = @""" + StaticKeys.ConnectionStringPlaceHolder + @"""
                        };
                        var serverName = builder.DataSource;
                        var server = new Server(serverName);
                        var backupDirectory = server.MasterDBPath;
                        var sqlDomainGroup = server.SqlDomainGroup;

                        //Get the permission for the sql login that the system will use to excute the sql task
                        //_sqlDomainGroup it should be like this: [Machine Name]\SQLServerMSSQLUser$[Machine Name]$[SQL INSTANCE Name (MSSQLSERVER OR SQLEXPRESS)]
                        //Also make sure this signature is used to create the access rule sinc, If it's not inherited in the good way, 
                        //you'll see your permissions only in 'special permission' and not 'FULL Control'
                        var sqlPermissionRule = new FileSystemAccessRule(sqlDomainGroup, FileSystemRights.FullControl,
                                                                                        InheritanceFlags.ContainerInherit |//IMPORTANT: USE FOR FULL CONTROL
                                                                                        InheritanceFlags.ObjectInherit,    //IMPORTANT: USE FOR FULL CONTROL
                                                                                        PropagationFlags.None,             //IMPORTANT: USE FOR FULL CONTROL
                                                                                        AccessControlType.Allow);

                        //Get the directory info for current backup source
                        var myDirectoryInfo = new DirectoryInfo(@""" + StaticKeys.FolderLocationPlaceHolder + @""");
                        var myDirectorySecurity = myDirectoryInfo.GetAccessControl();

                        myDirectorySecurity.RemoveAccessRule(sqlPermissionRule);
                        myDirectoryInfo.SetAccessControl(myDirectorySecurity);
                    }

                    executionResult = """ + StaticKeys.VITALSCRIPTSUCCESS + @""";
                    executionLog = string.Empty;
                }
                catch (Exception exception)
                {
                    executionResult = """ + StaticKeys.VITALSCRIPTFAIL + @""";
                    executionLog = exception.Message + Environment.NewLine +  exception.InnerException;
                }
            }";


        #endregion

        #region Create Directory

        public static List<string> CreateDirectoryAssemblies = new List<string> { System, AccessControl, Principal};
        public static List<string> CreateDirectoryAssembliesDLLs = new List<string> { SystemDLL, AccessControlDLL, PrincipalDLL};
        public static string CreateDirectoryMethod = "CreateDirectory";
        public static string CreateDirectoryScript =
            @"private static void CreateDirectory(out string executionResult, out string executionLog)
            {
                try
                {
                    DirectoryInfo directoryInfo;
                    DirectorySecurity directorySecurity;
                    AccessRule rule;
                    var securityIdentifier = new SecurityIdentifier (WellKnownSidType.BuiltinUsersSid, null);
                    var directoryPath = @""" + StaticKeys.FolderLocationPlaceHolder + @""";
                    if (!Directory.Exists(directoryPath))
                    {
                        directoryInfo = Directory.CreateDirectory(directoryPath);
                        bool modified;
                        directorySecurity = directoryInfo.GetAccessControl();
                        rule = new FileSystemAccessRule(
                                securityIdentifier,
                                FileSystemRights.Write |
                                FileSystemRights.ReadAndExecute |
                                FileSystemRights.Modify," + StaticKeys.ValuePlaceHolder + @"
                                AccessControlType.Allow);
                        directorySecurity.ModifyAccessRule(AccessControlModification.Add, rule, out modified);
                        directoryInfo.SetAccessControl(directorySecurity);
                    }
                    
                    executionResult = """ + StaticKeys.VITALSCRIPTSUCCESS + @""";
                    executionLog = string.Empty;
                }
                catch (Exception exception)
                {
                    executionResult = """ + StaticKeys.VITALSCRIPTFAIL + @""";
                    executionLog = exception.Message + Environment.NewLine +  exception.InnerException;
                }
            }";


        #endregion
    }
}
