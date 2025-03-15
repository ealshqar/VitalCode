
namespace Vital.Business.Shared.Shared
{
    /// <summary>
    /// This class contains a group of static values that the business layer may use.
    /// </summary>
    public class StaticKeys
    {
        #region Database

        public const string DataBaseName = "VitalExpert";
        public const string TempDataBaseName = "VitalExpertTemp";
        public const string MasterDataBase = "Master";
        public const string MasterConnectionStringKey = "MasterConnectionString";
        public const string MainConnectionStringKey = "Vital.UI.Properties.Settings.Setting";
        public const string DataBaseExtension = ".mdf";
        public const string VITALSCRIPTSUCCESS = "VITALSCRIPTSUCCESS";
        public const string VITALSCRIPTFAIL = "VITALSCRIPTFAIL";
        public const string SqlExpressServiceName = "MSSQL$SQLEXPRESS";

        #endregion
      
        #region Dongle

        public const string DongleGeneralError = "An error occurred during dongle check";

        public const string DongleResetTitle = "Dongle Reset";
        public const string DongleResetSucceeded = "Done Reset Succeeded";
        public const string DongleResetError = "Dongle Reset Error";
        public const string DongleResetErrorCouldNotVerifySuccess = "Couldn't verify the dongle reset success, please check with our staff.";
        public const string DongleResetErrorIncorrectCodes = "Couldn't reset the dongle, please make sure the codes are correct.";
        public const string ErrorDuringDongleReset = "Couldn't reset the dongle, please check with our staff.";
        public const string CouldNotLoadDongleCodes = "Couldn't load dongle reset codes, please make sure the dongle is attached.";
        
        public const string DongleExpiredMessage = "Your dongle has expired on @ExpiryDate@, you need to reset your dongle to continue using Vital.";
        public const string DongleWillExpireMessage = "Your dongle will expire on @ExpiryDate@. Follow the steps below to reset now.";
        public const string DongleExpiryDateString = "@ExpiryDate@";

        #endregion

        #region Security

        public const int CSAPVSecurityAddress = 10;
        public const int CSAPVSecurityNumber = 11;

        public const int VitalSecurityAddress = 11;
        public const int VitalSecurityNumber = 12;

        #endregion

        #region General Constants

        public const string ApplicationName = "Vital Expert";
        public const string AppName = "Vital App";
        public const string DefaultSkinRestName = " (Default)";
        public const string ApplicationAlreadyRunning = "Vital Expert already running.";
        public const string ValidationMessageBlankField = "This field is required.";
        public const string ValidationMessageBlankFieldOptinal = "This field is required. \nPress Esc to cancel.";
        public const string ValidationMessageValueLength = "The length of this value exceeds the correct length.";
        public const string ValidationMessageAtLeastOneItem = "At least one item should be added.";
        public const string ValidationMessageDuplicatedItem = "This item is duplicated.";
        public const string ValidationMessageDate = "Please provide a correct date.";
        public const string AttemptToModifyADeletedObjectErrorMessage = "Attempt to modify a deleted object";
        public const string ValidationMessageMinimumLength = "Entered text is less than the value needed.";
        public const string ValidationMessageGeneral = "Please make sure all fields are valid.";
        public const string ValidationMessageTitle = "Validation Error";


        public const string ClaerHandlersError = "An error occurred during closing the tab.";
        public const string DeletedInfoUnableToEdit = "This tab currently is not editable, the opened item was deleted by another user and it is not usable.";
        public const string EditInfoInfo = "This tab currently is not editable, another related tab is being edited now, please finish changes on that tab first to enable this one.";
        public const string ItemHasNoValueError = "Item has no value.";
        public const string ItemIsNotSaved = "The item you selected is not saved yet, please save it first in order to load its information.";
        public const string RefreshConfirmQuestion = "This tab information will be loaded from the database, all your changes will be lost, are you sure?";

        public const string PressEnterToCloseForm = "Press [Enter] to continue after finding the vital force.";
        public const string FormWillColseAfterMessage = "Closing after <color=red>{0}</color> second(s), Press [Enter] for close now.";
        public const string ImageErrorOccurred = "An Error occurred while setting image.";
        public const string LikeFormatForString = "%{0}%";

        #endregion

        #region Form: frmGeneralTest

        public const string ChangesWillBeCanceledMessage = "Changes will be canceled, Are you sure?";
        public const string DeleteConfirmQuestion = "The current record will be deleted, are you sure?";
        public const string DeleteMultipleConfirmQuestion = "The current record(s) will be deleted, are you sure?";
        public const string DeleteImprintableProductConfirmQuestion = "The product ({0}) and its related data will be deleted, are you sure you want to continue ?";
        public const string DeletedInfo = "The opened item was deleted.";
        public const string ExceptionDatabaseErrorMessage = "Database Error: \n";
        public const string ExceptionLogicalErrorMessage = "Error: \n";
        public const string ClearingCurrentPointSetReadings = "Current point set readings will be cleared, Are you sure ?";
        public const string ClearingAllEDSReadings = "All EDS readings will be cleared, Are you sure ?";
        public const string AutoTestDone = "EDS reading done.";
        public const string BalancingDone = "Balancing is done.";
        public const string AutoTestDoneQuestion = "EDS reading done, would you like to start with item testing?";
        public const string NewTest = "Test {0}";
        public const string NewSpotCheck = "Check {0}";
        public const string NewAutoTest = "Automated Test {0}";
        public const string NewVFS = "VFS {0}";
        public const string PointName = "Full Point Name";
        public const string ShortPointName = "Short Point Name";
        public const string CreateMajorIssues = "The current test will be saved, are you sure?";
        public const string SettingsError = "Could not save the setting value.";
        public const string SpotCheckTitile = "Spot Check - {0} {1}";
        public const string VFSTitle = "Vital Force Sheet - {0} {1}";
        public const int EvaluationPeriodDefault = 4;
        public const string AddToImprintingList = "Adding to imprint list ...";
        public const string RemoveFromImprintingList = "Removing from imprint list ...";
        public const string UpdateImprintingState = "Update imprinting state ...";
        public const string ImprintableItemsProperty = "ImprintableItems";
        public const string HwProfileTitle = " Hardware Profile - {0}";
        public const string KeepOneByOneConfirm = "One By One testing mode will remain active anytime you manually use it, are you sure?" + "\n" + "When this option is checked, Vital will keep the 'One By One' testing mode active if it was activated purposely by the user." + "\n" +"This option will cause Vital to force testing using 'One By One' testing mode in all categories and lists, please activate this option only if you would like to keep the 'One By One' testing mode active all time during testing.";

        #endregion

        #region Form: ribbonFrmMain

        public const string GroupIsNUllError = "The navigation group has no value.";
        public const string ItemHasNoActionMsg = "The item you clicked has no action specified yet.";
        public const string MainFormIsNUllError = "Failed to load the main form of the application.";
        public const string RecentProjectsFileName = "RecentProjects.config";
        public const string TabsUnsavedError = "Some opened tabs are unsaved, please save them before closing.";
        public const string XmlNavigatorIsNUllError = "Failed to load recent projects.";
        public const string ErrorDatabaseUpdate = "An Error occurred while updating the Database.";
        public const string ErrorDatabaseTitle = "Database Error.";
        public const string ErrorDatabaseCompatibility = "Your database is not compatible with the application and has a newer version.";
        public const string ErrorDatabaseCreation = "An Error occurred while creating the Database.";
        public const string ErrorVersionCannotBeIdentified = "Database version information is missing, version can't be identified.";
        public const string ErrorDatabaseVersion = "Database Version Error.";
        public const string ErrorAccessingDatabase = "An Error occurred while accessing database.";
        public const string ExportPatientsToCsvFileName = "Patients {0}";
        public const string NotExportableColumn = "NotExportable";
        public const string SearchHintNoTabs = "Start searching or hit F12 to show client search.";
        public const string SearchHintWithTabs = "Hit F12 to show client search.";
        public const string ErrorConnectingToDatabase = "Couldn't connect to database.";
        public const string ErrorDatabaseFileAccess = "Couldn't access database files.";
        public const string ErrorAppBranchIncompatibile = "Application branch is incompatible.";
        public const string ErrorAppBranchMissing = "Application branch Info is Missing.";

        #endregion

        #region Form: TestProtocolsStuff

        public const string StepAlreadyExists = "Protocol step already exists.";

        public const string ProtocolHasRelatedTests =
            "Protocol can not be deleted,\none or more tests are using this protocol.";

        public const string ProtocolIsDefault =
            "Protocol can not be deleted,\nthis Protocol has been set as a default test protocol.";

        public const string ProtocolInUseForStepsEditing =
            "Protocol steps can not be edited, \nthis protocol is in use.";

        public const string ProtocolInUseMessageTitle =
           "Protocol is in use";

        public const string NewTestProtocol = "New Test Protocol";
        public const string ProtocolSteps = "Protocol Steps";
        public const string ProtocolStepsLocked = "Protocol Steps ( Locked )";


        #endregion

        #region Form: frmItemManagement

        public const string ItemTargetAlreadyExists = "Target already exists.";
        public const string CannotDeleteItemTitle = "Item cannot be deleted.";
        public const string CannotDeleteItem = "This item cannot be deleted because of related data.";
        public const string CannotSaveItemTitle = "Can't save item";
        public const string CannotSaveItem = "This item can't be saved because the target type 'MyPointsList' was removed or changed.";
        public const string TimerOn = "On";
        public const string TimerDefaultTimeWaiting = "10";
        public const string NewItem = "New Item";
        public const string ProductType = "Product";
        public const string SystemItemEdit = "This is a system item that can't be edited or deleted.";
        public const string Root = "Root";
        public const string ItemsHasRelatedDataDeletionConfirmationQuestion = "This item has related data, are you sure you want to continue ?";
        public const string WaitTimerMessgae = "Please wait until the timer is done.";
        public const string WaitTimerMessgaeTitile = "Adding item in progress ...";
        public const string GeneratingMessage = "Generating ...";
        
        #endregion

        #region Form: SelectingStartingItems

        public const string WarningMessageUnSelectingItems = "Some items are selected and they will be unchecked, Are you sure you want to continue ?";

        #endregion

        #region Form : DataManagment

        public const string SaveBeforChangeSelectedGroupType = "You have some unsaved changed, would you like to save them first?";
        public const string LoadingData = "Loading Data ...";
        public const string NotValidItemError = "There are not valid items.";
        public const string NewProductName = "New Product";
        public const string NewProductGroup = "New Product Group";
        public const string SeatsAvailableText = "You have <color=red>{0}</color> seats available.";
        public const string NoSeatsAvailableText = "<color=red>You have no more seats available.</color>";
        public const string NoSeatsAvailableMessage = "You have no more seats available.";

        #endregion

        #region Form: Settings

        public const string AutoDetectedDisconnected = " Disconnected (Auto Detection)";
        public const string ManualDetectedDisconnected = "Disconnected (COM Port {0})";

        public const string AutoDetectedConnected = "Connected (Auto Detected COM Port {0})";
        public const string ManualDetectedConnected = "Connected (COM Port {0})";

        public const string SearchingPortDescription = "COM Port {0} ...";

        public const string AutoDetectionText = "Auto Detection";

        public const string ResetToDefultConfirmMessage = "Settings will be reset to defaults, Are you sure?";
        public const string ShippingConfirmation = "Shipping Order will be sent to shipping department, Are you sure?";
        public const string SavingImage = "Saving Image";
        public const string AppImagesLogo = "Logo";
        public const string ErrorMessageCouldNotUseLogo = "An error occurred while vital is trying to use the logo";

        #endregion

        #region Form: SpotCheck

        public const string MtesAutomationDone = "MTEs automation is done.";
        public const string CapsolTAutomationDone = "Capsol T automation is done.";
        public const string DmpsAutomationDone = "DMPS automation is done.";
        public const string MineralsAutomationDone = "Minerals automation is done.";
        public const string PreparingReportMessage = "Preparing Report ...";
        public const string ItemsListTitile = "{0} List";
        
        #endregion

        #region Form: About

        public const string AboutFormTitle = "About Vital";
        public const string ReleaseNotesFormTitle = "Release Notes";

        #endregion

        #region FormProsuctDosages

        public const string ProsuctDosagesGroupText = "{0} - Product Dosages";
        public const string SetAsDefaultsDosagesCheckboxText = "Set as defaults dosages for {0}.";

        #endregion

        #region Form: ItemDescription

        public const string ItemDescriptionTitle = "{0} Description";

        #endregion

        #region Program

        public const string ConfigurationError = "Configuration error, check with your administrator.";
        public const string SettingError = "Failed to access Vital Database, please check with your administrator.";

        #endregion

        #region Exceptions

        public const string GeneralConfigurationErrorMessage = "An Error occurred while trying to read configuration setting \"{0}\"";
        public const string ConfigSettingNotFoundErrorMessage = "Configuration setting \"{0}\" is not found";
        public const string ConfigSettingValueIsInvalidErrorMessage = "Configuration setting \"{0}\" has an invalid value of \"{1}\"";
        public const string ConnectionStringSettingIsInvalidErrorMessage = "Connection string setting \"(0)\" is invalid";
        public const string ConnectionStringSettingHasInvalidDatabaseNameErrorMessage = "Connection string setting \"(0)\" has invalid database name";
        public const string ConnectionStringTimeoutSqlErrorMessage = "An error has occurred while establishing a connection to the server using connection string \"{0}\"";
        public const string ErrorCodePropertyName = "ErrorCode";
        public const string SettingNamePropertyName = "SettingName";
        public const string SettingValuePropertyName = "SettingValue";
        public const string ObjectHasNoValueError = "The item to be opened has no value.";
        public const string ExceptionReportTitle = "Exception Report";

        public const string ExceptionDetailsFormat = "Class Name:{0}\n\nMethod Name:{1}\n\nLine Number:{2}\n\nColumn Number:{3}\n\nException Message:{4}\n\nInner Message:{5}\n\nStack Trace:{6}\n\n";
        public const string ExceptionImageContentId = "ExceptionImage";
        public const string ExceptionEmailHTMLMediaType = "text/html";
        public const string ExceptionEmailHTMLStructure = "{0}\n\n<img src=cid:{1}>";
        public const int ExceptionEmailSubjectLength = 76;
        #endregion

        #region UI

        public const string ErrorMessageTitle = "Error";
        public const string InformationMessageTitle = "Information";
        public const string WarningMessageTitle = "Warning";
        public const string QuestionMessageTitle = "Confirmation";
        public const string Products = "Products";
        public const string Bacteria = "Bacteria";
        public const string Potency = "Potency";
        public const string EnergyFrequencies = "Energy Frequencies";

        public const string ClosingScreenConfirmation = "Are you sure you want to close this screen?";
        public const string ClosingScreenConfirmationYesNoCancel = "You have unsaved changes. Would you like to save before closing?";
        public const string VitalExitConfirmation = "Are you sure you want to exit Vital?";

        #endregion

        #region Imprinting

        public const string StartImprinting = "Start Imprinting";
        public const string StopImprinting = "Stop Imprinting";

        #endregion

        #region Patients

        public const string DeletePatientHasTestsOrSpotChecksQuestion = "Current patient tests/spot checks/frequency tests will be deleted, would you like to continue?";
        public const string DeletePatientConfirmQuestion = "The current record will be deleted, are you sure?";
        public const string DeleteRecordConfirmQuestion = "The current record will be deleted, are you sure?";

        #endregion

        #region Items

        public const string ItemInUseMessageTitle = "Item cannot be deleted, because of related data.";

        #endregion

        #region Binding

        public const string EditValuePropertyname = "EditValue";
        public const string SelectedIndexPropertyname = "SelectedIndex";
        public const string DatasourcePropertyname = "DataSource";
        public const string EditvaluePropertyname = "EditValue";
        public const string SelectedindexPropertyname = "SelectedIndex";
        public const string TextPropertyname = "Text";
        public const string TextRtfPropertyName = "RtfText";
        public const string ImagePropertyname = "Image";
        public const string CheckedPropertyname = "Checked";
        
        #endregion

        #region Properties

        public const string PropertyAlreadyExists = "Property already exists. \nPress Esc to cancel.";

        #endregion

        #region CommonHardcodedStrings

        #region Area Specific Items

        /// This region contains items that are in DB but are important for filtration in UI and we 
        /// need them by their names
        public const string VitalForce = "Vital Force";
        public const string TopTen = "Top 10";
        public const string FourFactors = "Four Factors";
        public const string CustomDilutions = "Custom Dilutions";
        public const string Generic = "SystemGenericList";
        public const string GenericListTitle = "Generic List";
        public const string FourCauses = "SystemFourCausesList";
        public const string FourCausesListTitle = "Circulation Causes";
        #endregion

        #region Grids

        public const string UnboundValueFieldName = "UnboundValue";
        public const string IsBalancedFieldName = "gridColumnIsBalanced";
        public const string PropertyId = "Property.Id";

        #endregion

        #region External Paths

        public static string ReleaseUpdatesFolderPath = "ReleaseUpdates";
        public static string DataScriptsFolderPath = "DataScripts";
        public static string DatabaseFolderPath = "Database";
        public static string ProductsOnBackorderFolderPath = "ProductsOnBackorder";
        public static string DiscontinuedProductsFolderPath = "DiscontinuedProducts";

        #endregion

        #region Application Paths

        public const string ImagesFolderName = "Images";

        #endregion


        #region Frequency Testing

        public const string EnergyFrequencyGroupsParentItemKey = "EnergeyFrequencyGroup_TrueRifeFrequenciesParent";
        public const string RootFrequencyItemKey = "EnergeyFrequencyGroup_TrueRifeFrequencies";
        public const string NewFrequencyTest = "Frequency Test {0}";
        public const string FrequencyTestTitle = "Frequency Test - {0} {1}";
        public const string AutoTestNoTitle = "Auto Test";
        public const string AutoTestTitle = "Auto Test - {0}";
        public const string FrequenciesFolderName = "TrueRife Frequencies";
        public const string FrequencyFileExtensionFilter = "*.frq";
        public const string FrequencyInfoFileExtensionFilter = "*.trl";
        public const string EnergyFrequencyGeneralItemKey = "EnergeyFrequency_";
        public const string EnergyFrequencyGeneralParentItemKey = "EnergeyFrequencyGroup_";
        public const string IncorrectFrequencyFolder = "No frequency files were found in the update folder, please try selecting a different folder";
        public static string DefaultFrequencyTestResultAnswer = "1";

        #endregion

        #region General 

        public const string NextIssueText = "Next Issue";

        #endregion

        #endregion

        #region CSA Connection & Readings

        public const int MeterMinAcceptableReading = 46;
        public const int MeterMaxAcceptableReading = 54;

        public const string CsaDisconnectedText = "Disconnected";
        public const string CsaConnectedText = " Connected ";
        public const string CsaBrodcastingText = "Broadcasting";
        public const string CsaBrodcast = "Broadcast";
        public const string CsaImprintingText =  " Imprinting";
        public const string CsaSearchingText = " Searching";
        public const string CsaPortText = "COM Port {0}       ";
        public const string CsaRetryText = "Retry       ";
        public const string CsaNotConnected = "Please make sure the meter is connected.";
        
        public const string Reading = "Reading...";
        public const string Ready = "Ready";
        public const string Waiting = "Waiting...";

        public const string Release = "Release";
        public const string ReleaseLong = "Waiting release...";

        public const string Moving = "Moving...";
        public const string MovingLong = "Reading after {0} seconds...";

        public const string TakeActionWithin = "{0} after {1} seconds";

        public const string TakeReading = "Waiting reading ...";

        public const string ViewDetailsAction = "Details";
        public const string InsertingAction = "Insert";
        public const string ClearingAction = "Clear";
        public const string NoneAction = "None";
        public const string SplittingAction = "Split";
        public const string MovingAction = "Move";
        public const string SwitchingAction = "Switch";

        public const string Yes = "Yes";
        public const string No = "No";
        public const string HideYesNo = "---";

        #endregion

        #region Loading Messages

        #region Backup & Restore

        public const string DatabaseBackup = "Database Backup to default location ...";
        public const string DatabaseBackupSecondary = "Database Backup to Secondary Location ...";
        public const string DatabaseBackupSuccessful = "Database Backup Successful";
        public const string DatabaseBackupMessageTitle = "Backup";
        public const string DatabaseBackupErrorOccured = "An Error Occurred";
        public const string DatabaseBackupDestinationNotWritable = "Default Backup destination doesn't allow access permission.";
        public const string DatabaseBackupSecondaryDestinationNotWritable = "Backup Secondary destination  doesn't allow access permission.";
        public const string DatabaseBackupDestinationDoesNotExist = "Default Backup destination doesn't exist, system will try to backup to secondary location.";
        public const string DatabaseBackupSecondaryDestinationDoesNotExist = "Backup Secondary destination doesn't exist.";
        public const string DatabaseBackupIsNotSpecified = "Default Backup destination was not being specified, system will try to backup to secondary location.";
        public const string DatabaseSecondaryBackupIsNotSpecified = "Backup Secondary destination was not being specified.";
        public const string DatabaseVerifyingBackup = "Verifying Backup ...";
        public const string DatabaseRestoringDatabase = "Restoring Database ...";
        public const string DatabaseRestoreSuccessful = "Database Restore Successful.";
        public const string DatabaseCodeIsOld = "Application code is older than database, please update the application.";
        public const string DatabaseRestoreMessageTitle = "Restore";
        public const string DatabaseInvalidVersion = "Invalid Database Version.";
        public const string DatabaseInvalidExtension = "The specified file has an invalid extension.";
        public const string DatabaseFileDoesNotExist = "Restore file doesn't exist.";
        public const string DatabaseRestoreIsNotSpecified = "Restore file was not being specified.";
        public const string ApplicationBranchCompatabilityError = "This backup file cannot be restored because the application version is incompatible with the database version, please check with your administrator.";
        public const string DatabaseBackupPermissionError = "There is no permission to access selected backup location.";
        public const string DatabaseRestoreUpdateFilesDownloadError = "The database restore stopped because there was an error while downloading the required update files from server, please make sure you have online internet connection or check with administrator.";

        #endregion

        #region General Test

        public const string InitilizingUserInterface = "Initializing User Interface ...";
        public const string LoadingCustomComponents = "Loading Custom Components ...";

        #endregion

        #region Settings

        public const string ResutToDefault = "Resetting to default";

        #endregion

        #region RibbonMain Form

        public const string InitializationApplication = "Initializing Application ...";
        public const string LoadingPatients = "Loading Patients ...";
        public const string CachingCommonData = "Caching Common Data ...";
        public const string CachingProducts = "Caching Products ...";
        public const string LoadingSettings = "Loading Settings ...";
        public const string LoadingHwProfiles = "Loading Hardware Profiles ...";
        public const string InitializingHwProfilesFirstUse = "Initializing Hardware Profiles For First Use...";
        public const string LoadingServices = "Loading Services ...";
        public const string LoadingNotes = "Loading Notes ...";
        public const string LoadingItems = "Loading Items ...";
        public const string LoadingClinicInfo = "Loading Clinic Info ...";
        public const string LoadingShippingCount = "Loading Shipping Orders Info ...";
        public const string ClosingMessage = "Closing ...";
        public const string LoadingProductInfoProfiles = "Loading Product Information ...";

        #endregion

        #region GeneralLoadingMessage

        public const string SavingMessgae = "Saving ...";
        public const string OpeningMessgae = "Opening ...";
        public const string DataInitializationMessgae = "Data Initialization ...";
        public const string BindingInformationMessgae = "Binding Information ...";
        public const string FinalizingMessage = "Finalizing ...";
        public const string PostingMessage = "Posting Data ...";
        public const string SynchronizingMessage = "Synchronizing Data ...";
        public const string DeletingMessage = "Deleting ...";
        public const string ClearBindingMessage = "Clear Binding ...";
        public const string ClearHandlersMessage = "Clear Handlers ...";
        public const string SetupPropertiesMessage = "Setup Properties ...";
        public const string SetupHandlersMessage = "Setup Handlers ...";
        public const string ProcessingMessage = "Processing ...";

        #endregion

        #endregion

        #region Navigation Grid

        public const string VisitedItemTypeLookup = "Lookup";
        public const string VisitedItemTypeProtocol = "Protocol";
        public const string VisitedItemTypeSearch = "Search";
        public const string VisitedItemTypeItem = "Item";
        public const string VertabreList = "Vertebrae";
        public const string InsertOnNoNote = "Is the highlighted item(s) in balance?";
        public const string NumberOfItems = "{0} Item{1}";
        public const string NumberOfSelectedItems = " ({0} Selected)";

        #endregion

        #region Files Download

        public static string DownloadResultString = "Success";
        public static string DownloadErrorOccured = "A download error occurred";

        public static string UpdateScriptsUrlConfigKey = "UpdateScriptsURL";
        public static string UpdateScriptsDownloadLog = "UpdateScriptsDownloadLog.txt";
        public static string UpdateScriptsDownloadResult = "UpdateScriptsDownloadResult.txt";
        public static string UpdateScriptsFileName = "Update Scripts.txt";

        public static string DataScriptsUrlConfigKey = "DataScriptsURL";
        public static string DataScriptsDownloadLog = "DataScriptsDownloadLog.txt";
        public static string DataScriptsDownloadResult = "DataScriptsDownloadResult.txt";
        public static string DataScriptsFileName = "Data Scripts.txt";

        public static string DatabaseUrlConfigKey = "DatabaseURL";
        public static string DatabaseDownloadLog = "DatabaseDownloadLog.txt";
        public static string DatabaseDownloadResult = "DatabaseDownloadResult.txt";
        public static string DatabaseFileName = "Database.txt";

        public static string ProductsOnBackorderUrlConfigKey = "ProductsOnBackorderURL";
        public static string ProductsOnBackorderDownloadLog = "ProductsOnBackorderDownloadLog.txt";
        public static string ProductsOnBackorderDownloadResult = "ProductsOnBackorderDownloadResult.txt";
        public static string ProductsOnBackorderFileName = "ProductsOnBackorderInfo.txt";

        public static string DiscontinuedProductsUrlConfigKey = "DiscontinuedProductsURL";
        public static string DiscontinuedProductsDownloadLog = "DiscontinuedProductsDownloadLog.txt";
        public static string DiscontinuedProductsDownloadResult = "DiscontinuedProductsDownloadResult.txt";
        public static string DiscontinuedProductsFileName = "DiscontinuedProductsInfo.txt";

        public static string ImagesUrlConfigKey = "ImagesURL";
        public static string ImagesDownloadLog = "ImagesDownloadLog.txt";
        public static string ImagesDownloadResult = "ImagesDownloadResult.txt";
        public static string ImagesFileName = "Images.txt";

        public static char DataScriptsFileNameSeparator = ',';

        public static string FileAccessError = "File Access Error";
        public static string DownloadingFile = "Downloading File";
        public static string DownloadCancelled = "Download Cancelled";
        public static string DownloadCompleted = "Download Completed";
        public static string NoFilesToDownload = "No Files to download.";
        public static string NoInternetConnection = "There is no internet connection.";
        public static string FileDownloadError = "An error occurred during update files download";
        public static string CouldNotCreateDownloadLocation = "Could not create files download location";

        #endregion

        #region Services

        public static string VitalApiBaseUrlConfigKey = "VitalApiBaseUrl";
        public static string UpdateUserInfoApiRouteConfigKey = "UpdateUserInfoApiRoute";

        #endregion

        #region Data Updates

        public static string CheckWithSupportForDataUpdates = "Vital will continue to work however there seems to be an issue in applying data updates to your system, please check with support team for further investigation.";
        public static string DataUpdateErrorOccured = "Data update Occurred";
        public static string DataScriptFailed = "Data Script Failed.";
        public static string DataScriptIncompatible = "Data Script has an incompatible version";
        public static string DataScriptMissing = "Data Script Missing";

        #endregion

        #region Application Update

        public static string UpdateConfirmationQuestion = "An Update is available, would you like to install it now?";
        public static string VersionIsUptoDate = "This version is up to date.";
        public static string UpdateErrorOccured = "An Error Occurred";
        public static string ServerNotAccessible = "Update server is not accessible.";
        public static string CheckingForUpdates = "Checking for Updates ...";
        public static string ApplicationRestart = "Update is Done, the Application Will Restart Now.";
        public static string UpdateScriptFailed = "Update Script Failed.";
        public static string UpdateScriptIncompatible = "Update Script has an incompatible version";
        public static string UpdateScriptMissing = "Update Script Missing";

        #endregion

        #region Administrative Access

        public static string VitalExecutionResult = "VitalExecutionResult.txt";
        public static string VitalExecutionLog = "VitalExecutionLog.txt";
        public static string VitalNeedsPermission = "Vital needs your permission to perform an operation that requires your approval, would you like to confirm operation?";
        public static string VitalHasNoServicePermission = "Vital couldn't have permission to start the Sql service, please start Vital again and confirm permission request when needed.";
        public static string VitalServiceCheckError = "An error occurred while trying to check the SQL service, please check with support team";
        public static string VitalCodeExecuterError = "An error occurred while trying to compile the code logic passed to execute, please check with support team";
        public static string FolderLocationPlaceHolder = "%DESTINATION%";
        public static string ConnectionStringPlaceHolder = "%CONNECTIONSTRING%";
        public static string ValuePlaceHolder = "%VALUE%";
        public static string PerformingVitalSystemOperation = "Performing Vital System Operation ...";

        #endregion

        #region ExportToCsvExtensions

        public const string CsvFileFilter = "Excel File (*.csv)|*.csv";
        public const string CsvSaveDialogTitle = "Export";
        public const string CsvFileExtintion = ".csv";
        public const string ExportToCsvErrorTitle = "An Error Occurs While Exporting";
        public const string ExportingToCsvWaitText = "Exporting";
        public const string ExportToCsvErrorText = "The selected file ({0}) currently in use, please close it and try again. ";

        #endregion

        #region DateTimeFormats

        public const string DateFormatMmmDdYyy = "MMM dd yyyy";
        public const string TimeAmText = "AM";
        public const string TimePmText = "PM";

        #endregion

        #region Mail Client

        public const string SMTPAddress = "smtp.office365.com";
        public const int PortNumber = 587;
        
        //public const string MailClientSmtpHost = "smtp.gmail.com";
        //public const int MailClientPort = 587;

        public const string MailClientMessageSent = "Your feedback was sent successfully.";
        public const string MailClientSendingMail = "Sending feedback...";
        public const string MailClientValidationMessageTitle = "Validation";
        public const string MailClientNameValidationMessage = "Enter your name please.";
        
        public const string MailClientFirstLine = "This feedback message was sent to you by";
        public const string MailClientNoSubject = "(No Subject)";
        public const string MailClientNoMessage = "(No Message)";
        public const string DashSeperatorLong = "---------------------------------------------------------";
        public const string DashSeperatorShort = "----------------------------";

        #endregion

        #region Spot Checks

        public static string DefaultSpotCheckAnswer = "1";

        #endregion

        #region Reports

        public const string UpdatingPrintingSetting = "Updating Settings ...";
        public const string ApplingChange = "Appling Change ...";

        #endregion

        #region Hints

        public const string HintTextPrefix = "            ";

        public const string HintTitleEdit = "Click here to start editing.";
        public const string HintTextEdit = "Use (Ctrl + E) to start editing.";

        public const string HintTitleReEvaluation = "Click here to start Re-Evaluation.";
        public const string HintTextReEvaluation = "Or use (Ctrl + R).";


        #endregion

        #region MeterCounterDialog
       
        public const string CounterTipMajorIssues = "Press [Enter] to continue after finding the major issues.";
        public const string CounterQuestionMajorIssues = "Is there only 1 issue to be addressed?";
        public const string CounterTitleMajorIssues = "Number of major issues";

        public const string CounterTipRevaluationInWeeks = "Press [Enter] to continue after finding Re-Evaluation value.";
        public const string CounterQuestionRevaluationIn = "Re-Evaluation in {0}?";
        public const string CounterTitleMajorRevaluationInWeeks = "Re-Evaluate";

        #endregion

        #region GridFilterTexts

        public const string HiddenItemsFilterText = "[Hidden] = 'False'";
        public const string DeletedScheduleFilterText = "[IsDeleted] = 'False'";

        #endregion

        #region Shipping 

        public const string ShippingActionIn = "Shipping {0} in </br> {1} {2}";
        public const string ShippingActionWeekend = "Shipping will open next {0}.";
        public const string ShippingActionOpen = "Opens";
        public const string ShippingActionClose = "Closes";
        public const string ShippingClosesAlert = "Shipping closed.";
        public const string ShippingClosesAfterAlert = "Shipping will close in {0} hour{1}.";
        public const string ShippingAlertTitle = "Shipping";

        #endregion

        #region Product Information

        public const string ProductInfoUpdateAvialableTitle = "Product Info Update";
        public const string ProductInfoUpdateAvialableMessage = "This message is to let you know that product information have been updated, please check it out at the product information screen.";
        
        #endregion

        #region DatetimeExtintions

        public const string HoursPeriodName = "Hour{0}";
        public const string MinutesPeriodName = "Minute{0}";
        public const string SecondsPeriodName = "Second{0}";
        public const string YearsPeriodName = "Year{0}";

        #endregion

        #region VFS

        public static string ActualAge = "Actual age";

        #endregion

        #region Encryption and Decryption

        public static string EncryptionKey = "62DAA5F6E8EDBDBC3657";
        public static string EncryptionError = "An error occurred during data encryption, please check with support team.";
        public static string DecryptionError = "An error occurred during data decryption, please check with support team.";

        #endregion

        #region CefSharp & BioDigital 3D Model

        public static string CefSharpResouresInstalledFlagFileName = "CefSharpResouresInstalled"; 
        public static string CefSharpResourcesUrlConfigKey = "CefSharpResourcesUrl";
        public static string CefSharpResourcesLoaclFileNameConfigKey = "CefSharpResourcesLoaclFileName";
        public static string BioDigitalBaseModelUrlConfigKey = "BioDigitalBaseModelUrl";

        public static string BioDigitalFullBodyMaleModelBaseObjIdConfigKey = "BioDigitalFullBodyMaleModelBaseObjId";
        public static string BioDigitalFullBodyMaleModelIdConfigKey = "BioDigitalFullBodyMaleModelId";

        public static string BioDigitalFullBodyFemaleModelBaseObjIdConfigKey = "BioDigitalFullBodyFemaleModelBaseObjId";
        public static string BioDigitalFullBodyFemaleModelIdConfigKey = "BioDigitalFullBodyFemaleModelId";

        public static string Viewer3DModelInitializingMsg = "Initializing ...";
        public static string Viewer3DModelDownloadingResourcesMsg = "Downloading Resources ...";
        public static string Viewer3DModelInstallingResourcesMsg = "Installing ...";
        public static string Viewer3DModelPreparingModelMsg = "Preparing the 3D Model ...";
        public static string Viewer3DModelNoInternetMsg = "Unable to load the 3D model. Please check your internet connection and try again.";
        public static string Viewer3DModelLoadingModelFailedMsg = "Failed to load the 3D Model due to unexpected error.";


        #endregion
    }
}
