using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Windows.Forms;
using DevExpress.XtraSplashForm;
using Microsoft.VisualBasic.ApplicationServices;
using Vital.Business.Managers;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;
using Vital.Update.Managers;

namespace Vital.UI
{
    public class VitalInstanceController : WindowsFormsApplicationBase
    {
        #region Variables

        private static AppInfoManager _appInfoManager;
        private static SqlConfigManager _sqlConfigManager;
        private static ApplicationUpdateManager _updateManager;
        #endregion

        #region Constructors

        public VitalInstanceController()
        {
            // Set whether the application is single instance
            IsSingleInstance = true;
            StartupNextInstance += StartupNextVitalInstance;
            _updateManager = new ApplicationUpdateManager();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Overwrite OnCreateMainForm; 
        /// </summary>
        protected override void OnCreateMainForm()
        {
            try
            {
                var readyToRun = false;

                try
                {
                    if (IsSQLServiceRunning())
                    {
                        UiHelperClass.ShowWaitingPanel(StaticKeys.ProcessingMessage);
                        readyToRun = CheckDbExists().CheckStatus == DBCheckStatusEnum.ExistsAndConnected ? CheckDongle() && IsVersionCompatible() : IsVersionCompatible() && CheckDongle();
                        UiHelperClass.HideSplash();
                    }
                }
                catch (Exception exception)
                {
                    UiHelperClass.GetTechAndVitalAppInfo();
                    UiHelperClass.SendExceptionReport(exception,true, true);
                    UiHelperClass.HideSplash();

                    MainForm = new XtraFormRestartRequired();
                }

                try
                {
                    //An exception was happening at an internal level and the system is not catching it and its an SSL/TLS exception.
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                }
                catch (Exception)
                {
                }

                if (readyToRun)
                {
                    try
                    {
                        //Check image files and download them if confirmed, we call this logic here very early in the process to allow
                        //us to download the images when Vital is installed without having to wait for the user to register first.
                        UiHelperClass.CheckImagesAndUpdate();
                    }
                    catch (Exception exception)
                    {
                        UiHelperClass.GetTechAndVitalAppInfo();
                        UiHelperClass.SendExceptionReport(exception);
                        UiHelperClass.HideSplash();
                    }
                }

                if (readyToRun)
                {
                    var frmDisclaimer = new XtraFormDisclaimer();

                    if (frmDisclaimer.ShowDialog() == DialogResult.OK)
                    {
                        //IMPORTANT: CATCH ALL UNHANDLED EXCEPTIONS ON APPLICATION LEVEL BUT ONLY IF NOT IN DEBUGGING MODE
                        //====================================================================================
                        if (!Debugger.IsAttached)
                        {
                            Application.ThreadException += Application_ThreadException;
                            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;    
                        }
                        //====================================================================================

                        MainForm = new ribbonFrmMain();
                    }
                    else
                    {
                        //to speed up the delay that happens when the disclaimer reject.
                        Process.GetCurrentProcess().Kill();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception exception)
            {
                UiHelperClass.GetTechAndVitalAppInfo();
                UiHelperClass.SendExceptionReport(exception,true,true);
                UiHelperClass.HideSplash();
            }

        }

        #endregion

        #region Prerequisites

        /// <summary>
        /// Executes logic to start the sql service
        /// </summary>
        /// <param name="userCancelledPermission"></param>
        /// <returns></returns>
        public static ProcessResult ExecuteStartSQLServiceMethod(out bool userCancelledPermission)
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.PerformingVitalSystemOperation, true);

            var result = VitalLogicExecuter.ExecuteMethod(VitalLogicScripts.StartSqlServiceMethod,
                                                    VitalLogicScripts.StartSqlServiceScript,
                                                    VitalLogicScripts.StartSqlServiceAssemblies,
                                                    VitalLogicScripts.StartSqlServiceAssembliesDLLs,
                                                    out userCancelledPermission);
            UiHelperClass.HideSplash();

            return result;
        }

        /// <summary>
        /// Checking the Prerequisites for the application.
        /// </summary>
        /// <returns></returns>
        private bool IsSQLServiceRunning()
        {
            UiHelperClass.ShowWaitingPanel("Checking Prerequisites ...");

            var checkDB = false;

            try
            {
                var serviceCheck = new ServiceController(StaticKeys.SqlExpressServiceName);

                if (serviceCheck.Status == ServiceControllerStatus.Running)
                {
                    checkDB = true;
                }
                else
                {
                    bool userCancelledPermission;

                    var result = ExecuteStartSQLServiceMethod(out userCancelledPermission);
                    
                    if (result.IsSucceed)
                    {
                        checkDB = true;
                    }
                    else if (!UiHelperClass.IsRunAsAdministrator() && userCancelledPermission)
                    {
                        if (UiHelperClass.ShowConfirmQuestion(StaticKeys.VitalNeedsPermission) == DialogResult.Yes)
                        {
                            result = ExecuteStartSQLServiceMethod(out userCancelledPermission);
                            if (result.IsSucceed)
                            {
                                checkDB = true;
                            }
                            else if (userCancelledPermission)
                            {
                                UiHelperClass.ShowInformation(StaticKeys.VitalHasNoServicePermission);
                            }
                            else
                            {
                                UiHelperClass.ShowInformation(StaticKeys.VitalServiceCheckError);
                                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle,result.Message);
                            }
                        }
                        else
                        {
                            UiHelperClass.ShowInformation(StaticKeys.VitalHasNoServicePermission);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowInformation(StaticKeys.VitalServiceCheckError);
                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, exception);
            }

            UiHelperClass.HideSplash();

            return checkDB;
        }

        #endregion

        #region Code and Database Compatability

        /// <summary>
        /// Checking if the database exists.
        /// </summary>
        /// <returns></returns>
        public DBCheckState CheckDbExists()
        {
            _sqlConfigManager = _sqlConfigManager ?? new SqlConfigManager();

            return _sqlConfigManager.CheckDbExists(StaticKeys.DataBaseName);
        }

        /// <summary>
        /// Returns an enum value specifing the compatability state between code and DB.
        /// </summary>
        /// <returns></returns>
        public VersionCheck CheckVersion()
        {
            _appInfoManager = new AppInfoManager();
            _sqlConfigManager = new SqlConfigManager();

            var dbCheckState = CheckDbExists();

            switch (dbCheckState.CheckStatus)
            {
                //Continue Normally, the system will create the DB
                case DBCheckStatusEnum.DatabaseNotFound:
                    return VersionCheck.DatabaseNotFound;

                //Show an error message
                case DBCheckStatusEnum.ConnectionError:

                    if (dbCheckState.HasException)
                    {
                        MailHelper.SendExcecptionMailIfOnline(UiHelperClass.GetVitalEmail().ExceptionSenderEmail, StaticKeys.AppName, "Database Connection Error",dbCheckState.Message);
                    }

                    UiHelperClass.ShowError(StaticKeys.ErrorConnectingToDatabase,
                                            "The system couldn't connect to the database, please send the error details below to the support team." +
                                            "Please try in a few moments, if the problem persists, please contact the support team.", dbCheckState.CheckException);

                    return VersionCheck.ConnectionError;

                //Show an error message
                case DBCheckStatusEnum.FilesAccessError:

                    if (dbCheckState.HasException)
                    {
                        MailHelper.SendExcecptionMailIfOnline(UiHelperClass.GetVitalEmail().ExceptionSenderEmail, StaticKeys.AppName, "Database File Access Error", dbCheckState.Message);
                    }

                    UiHelperClass.ShowError(StaticKeys.ErrorDatabaseFileAccess,
                                            "The system couldn't access the database files, please send the error details below to the support team." +
                                            "Please try in a few moments, if the problem persists, please contact the support team.", dbCheckState.CheckException);

                    return VersionCheck.FilesAccessError;
            }

            //Get Version information from application
            var appCurrentVersion = _updateManager.CurrentPublishVersion;
            Version dbCurrentVersion;

            try
            {
                var versionAppInfo = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.Version) });
                if (versionAppInfo != null)
                {
                    dbCurrentVersion = new Version(versionAppInfo.Value);
                }
                else
                {
                    UiHelperClass.ShowError(StaticKeys.ErrorVersionCannotBeIdentified, string.Empty);
                    return VersionCheck.Failed;
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return VersionCheck.Failed;
            }

            if (dbCurrentVersion == appCurrentVersion)
                return VersionCheck.Equal;

            if (dbCurrentVersion > appCurrentVersion)
                return VersionCheck.DatabaseIsNewer;

            if (dbCurrentVersion < appCurrentVersion)
                return VersionCheck.DatabaseIsOlder;
            
            UiHelperClass.ShowError(StaticKeys.ErrorAccessingDatabase, string.Empty);

            return VersionCheck.Failed;
        }

        /// <summary>
        /// Gets Branch Compatibility state
        /// </summary>
        /// <returns></returns>
        private BranchCompatibilityCheck GetBranchCompatibility()
        {
            //get the key of the application branch.
            var appInfoProperty = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = "AppBranch" });
            var appBranchConfigValue = ConfigurationManager.AppSettings["AppBranch"];

            if (appInfoProperty == null || string.IsNullOrEmpty(appInfoProperty.Value) || string.IsNullOrEmpty(appBranchConfigValue))
            {
                return BranchCompatibilityCheck.NoBranchProperty;
            }
            
            return appBranchConfigValue != appInfoProperty.Value ? BranchCompatibilityCheck.BranchIncompatible : BranchCompatibilityCheck.BranchCompatible;
        }

        /// <summary>
        /// Checks branch Compatibility and returns true only if branch is compatible
        /// </summary>
        /// <returns></returns>
        private bool CheckBranchCompatibility()
        {
            var compatibilityStatus = GetBranchCompatibility();

            switch (compatibilityStatus)
            {
                case BranchCompatibilityCheck.BranchIncompatible:
                    UiHelperClass.HideSplash();
                    UiHelperClass.ShowError(StaticKeys.ErrorAppBranchIncompatibile,
                        "The application branch is incompatible with the database, please check with your administrator.");
                    return false;
                case BranchCompatibilityCheck.NoBranchProperty:
                    UiHelperClass.HideSplash();
                    UiHelperClass.ShowError(StaticKeys.ErrorAppBranchMissing,
                        "The application branch info is missing, please check with your administrator.");
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Check version compatability
        /// </summary>
        /// <returns></returns>
        private bool IsVersionCompatible()
        {
            UiHelperClass.ShowWaitingPanel("Checking Database Version ...");

            var isVersionCompatible = true;

            _sqlConfigManager = new SqlConfigManager();
            _appInfoManager = new AppInfoManager();

            switch (CheckVersion())
            {
                case VersionCheck.ConnectionError:
                    {
                        isVersionCompatible = false;
                        break;
                    }
                case VersionCheck.FilesAccessError:
                    {
                        isVersionCompatible = false;
                        break;
                    }
                case VersionCheck.Equal:
                    {
                        if (!CheckBranchCompatibility())
                        {
                            isVersionCompatible = false;
                        }
                        else
                        {
                            //if the database is compatible, check for data script updates and execute them if there are any
                            //This will backup database if updates are found
                            var dataScriptsResult = UiHelperClass.CheckDataScriptsAndUpdate(true,true);

                            if (dataScriptsResult != ScriptUpdateExecuteCheck.NoUpdateAvailable && dataScriptsResult != ScriptUpdateExecuteCheck.ScriptSuccess)
                            {
                                switch (dataScriptsResult)
                                {
                                    case ScriptUpdateExecuteCheck.ScriptFailed:
                                        UiHelperClass.ShowError(StaticKeys.DataScriptFailed, StaticKeys.CheckWithSupportForDataUpdates);
                                        break;
                                    case ScriptUpdateExecuteCheck.ScriptMissing:
                                        UiHelperClass.ShowError(StaticKeys.DataScriptMissing, StaticKeys.CheckWithSupportForDataUpdates);
                                        break;
                                    case ScriptUpdateExecuteCheck.UpdateCheckErrorOccured:
                                        UiHelperClass.ShowError(StaticKeys.DataUpdateErrorOccured, StaticKeys.CheckWithSupportForDataUpdates);
                                        break;
                                }
                            }
                        }
                        break;
                    }
                case VersionCheck.Failed:
                    {
                        isVersionCompatible = false;
                        break;
                    }
                case VersionCheck.DatabaseIsOlder:
                    {
                        isVersionCompatible = UpgradeDatabase();

                        if (isVersionCompatible && !CheckBranchCompatibility())
                        {
                            isVersionCompatible = false;
                        }

                        break;
                    }
                case VersionCheck.DatabaseIsNewer:
                    {
                        isVersionCompatible = false;

                        UiHelperClass.ShowError(StaticKeys.ErrorDatabaseCompatibility, string.Empty);

                        break;
                    }
                case VersionCheck.DatabaseNotFound:
                    {
                        UiHelperClass.ShowWaitingPanel("No Database Found ...");

                        //Run the upgrade script
                        isVersionCompatible = false;

                        try
                        {
                            //if the database is not found, check for databasse file on server and download and restore
                            var databaseResult = UiHelperClass.CheckDatabaseFileAndRestore(true);
                            if (databaseResult == DBCreateResult.Success)
                            {
                                isVersionCompatible = true;    
                            }
                        }
                        catch (VitalDatabaseException exception)
                        {
                            UiHelperClass.ShowError(StaticKeys.ErrorDatabaseCreation, exception);
                        }

                        break;
                    }
                default:
                    {
                        UiHelperClass.ShowWaitingPanel("Database Compatible ...");
                        isVersionCompatible = false;
                        break;
                    }
            }

            UiHelperClass.HideSplash();
            return isVersionCompatible;
        }

        /// <summary>
        /// Applies update scripts to DB
        /// </summary>
        /// <returns></returns>
        public static bool UpgradeDatabase()
        {
            UiHelperClass.ShowWaitingPanel("Older Database Found ...");
            
            var isVersionCompatible = false;

            try
            {
                //Check for data scripts and execute them
                var dataScriptsResult = UiHelperClass.CheckDataScriptsAndUpdate(false,true);

                if (dataScriptsResult == ScriptUpdateExecuteCheck.NoUpdateAvailable || dataScriptsResult == ScriptUpdateExecuteCheck.ScriptSuccess)
                {
                    if (UiHelperClass.ReleaseScriptsDownload.CheckIfDownloadedOrDownloadNow(false))
                    {
                        var dbUpdateProcessState = UiHelperClass.ApplyDownloadedUpdateScripts();

                        if (dbUpdateProcessState == DBScriptProcessState.ScriptSuccessful)
                        {
                            isVersionCompatible = true;
                        }
                    }
                }
                
            }
            catch (VitalDatabaseException exception)
            {
                UiHelperClass.ShowError(StaticKeys.ErrorDatabaseUpdate, exception);
            }

            return isVersionCompatible;
        }

        #endregion

        #region Dongle

        /// <summary>
        /// Check the existance of a Key-lok dongle
        /// </summary>
        private static bool CheckDongle()
        {
             return true;

            UiHelperClass.ShowWaitingPanel("Checking Dongle ...");

            var shouldContinue = false;

            try
            {
                UiHelperClass.ReadDongleInfo();

                if (!UiHelperClass.CheckForDongle)
                {
                    return true;
                }

                if (UiHelperClass.DonglePresent)
                {
                    switch (UiHelperClass.DongleState)
                    {
                        case DongleState.LeaseExpired:

                            //Set the expiry date to yesterday to prevent issues caused by very old expiry date. More details about this are inside the method.
                            UiHelperClass.SetDongleExpiryDateToYesterday();

                            var xtraFormDongleReset = new XtraFormDongleReset(true);
                            var result = xtraFormDongleReset.ShowDialog();

                            shouldContinue = result == DialogResult.OK;
                            
                            break;
                        case DongleState.LessThanTenDays:
                        case DongleState.LessThanFiveDays:

                            shouldContinue = true;
                            break;
                        case DongleState.Default:
                            UiHelperClass.ShowDongleCheckMessage();
                            shouldContinue = true;
                            break;
                        default:
                            UiHelperClass.ShowDongleCheckMessage();
                            break;
                    }
                }
                else
                {
                    UiHelperClass.ShowDongleCheckMessage();
                }
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(SecurityManager.KeyLokStateTitle, exception);
                shouldContinue = false;
            }

            UiHelperClass.HideSplash();

            return shouldContinue;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handel the second instance run from the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartupNextVitalInstance(object sender, StartupNextInstanceEventArgs e)
        {
            var currentForm = Application.OpenForms.Cast<Form>().FirstOrDefault(form => form.Modal) ?? MainForm;

            if (currentForm is SplashFormBase)
            {
                currentForm = null;
            }

            UiHelperClass.ShowInformation(StaticKeys.ApplicationAlreadyRunning, currentForm);

            if (MainForm != null)
            {
                MainForm.WindowState = FormWindowState.Maximized;
                MainForm.Show();
            }
        }

        /// <summary>
        /// CurrentDomain_UnhandledException
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            var exception = (Exception) e.ExceptionObject;

            try
            {
                UiHelperClass.SendExceptionReport(exception, false, true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Application_ThreadException
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                UiHelperClass.SendExceptionReport(e.Exception, false, true);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}