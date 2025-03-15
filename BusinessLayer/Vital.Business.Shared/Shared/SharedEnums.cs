using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Exceptions;

namespace Vital.Business.Shared.Shared
{
    #region  Enums

    public enum CustomFonts
    {
        [DescriptionAttribute("Undefined")] Undefined,
        [DescriptionAttribute("Open Sans Semibold")] OpenSansSemibold,
        [DescriptionAttribute("Open Sans Extrabold")] OpenSansExtrabold,
        [DescriptionAttribute("Open Sans Light")] OpenSansLight
    }

    public enum ScanCheck
    {
        Undefined,
        Childes,
        AddAllChildesAsResults
    }

    public enum AutoItemScanState
    {
        Pending,
        InProgress,
        Finished,
        None
    }

    public enum AutoResultCheck
    {
        NoResult,
        HasResult,
        ResultNotNeeded,
        None
    }

    public enum AutoResultAddResult
    {
        None,
        ResultNotNeeded,
        ResultAdded,
        HasResultAlready
    }

    public enum ScanCheckState
    {
        Pending,
        Checked,
        NotNeeded,
        None
    }

    public enum MultiLevelScanAction
    {
        Undefined,
        Split,      //Peform Split Operation
        Switch,     //Perform Switch Operation
        MoveNext,   //Moves to the next item in 1x1 mode
        MarkResult, //Make the highlighted item as a result
        Skip        //Skip current scanning operation
    }

    public enum FocusedRowChangeMethod
    {
        Undefined,
        Mouse,
        Keyboard
    }

    public enum ScanningAnnouncement
    {
        Undefined,
        ScanningStatus,
        CollectingScanningData,
        InitializeScanningFlags,
        ValidateHardware,
        StartingScanning,
        StartedStageScanning
    }

    public enum ScanBookmarkType
    {
        Undefined,
        Stage,
        StageItem,
        ScanningRounds,
        DosageOptions,
        MultiLevelScanning
    }

    public enum ScanBookmarkSource
    {
        Undefined,
        Automation,
        User
    }

    public enum CSAState
    {
        Undefined,
        Detecting,
        Disconnected,
        Connected,
        Failure
    }

    public enum PrototypeState
    {
        Undefined,
        Detecting,
        Disconnected,
        Connected,
        Failure
    }

    /// <summary>
    /// Memeory only status for the automated test
    /// </summary>
    public enum AutoTestStatus
    {
        Undefined,
        Pending,
        InProgress,
        Paused,
        Ended
    }

    /// <summary>
    /// The keys to the different stage tabs in AutoTest screen, this is needed because multiple stages can have the same UI
    /// </summary>
    public enum StageTabKey
    {
        Undefined,
        PreliminarySummary,
        Testing,
        Dosage,
        Results
    }

    /// <summary>
    /// This is the key to each individual stage, each stage needs to have its own unique key even if they share the same UI
    /// </summary>
    public enum StageKey
    {
        Undefined,
        Preliminary,
        MajorIssues,
        Testing,
        Dosage,
        Summary,
        Results
    }

    /// <summary>
    /// The type of the AutoItem
    /// </summary>
    public enum AutoItemType
    {
        Undefined,
        AutoItemTypePoint,
        AutoItemTypeGland,
        AutoItemTypeOrgan,
        AutoItemTypeProduct,
        AutoItemTypeProductCategory,
        AutoItemTypeGeneral,
    }

    public enum AutoItemStructureType
    {
        Undefined,
        AutoItemStructureTypeSingleItem,
        AutoItemStructureTypeCategory,
        AutoItemStructureTypeCustom
    }

    public enum AutoItemStatus
    {
        Undefined,
        AutoItemStatusActive,
        AutoItemStatusDeactivated,
        AutoItemStatusHidden,
        AutoItemStatusDiscontinued
    }

    public enum ChildsOrderType
    {
        Undefined,
        ChildsOrderTypeNone,
        ChildsOrderTypeAlphabatic,
        ChildsOrderTypeByOrder
    }

    /// <summary>
    /// Determines how the sub items are scanned
    /// </summary>
    public enum ChildsScanningType
    {
        Undefined,
        ChildsScanningTypeNone,
        ChildsScanningTypeSequential,
        ChildsScanningTypeElimination
    }

    /// <summary>
    /// Determines how the scanning of an item should be performed and if it is normal or if a match is required
    /// </summary>
    public enum AutoItemScanningMethod
    {
        Undefined,
        AutoItemScanningMethodNormal,
        AutoItemScanningMethodMatchRequired
    }

    public enum SortByTypesEnum
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// Type of filter operation to apply
    /// </summary>
    public enum FilterType
    {
        Equal,
        NotEqual,
        StringContains,
        StringEqual,
        StringEqualOrContains
    }

    /// <summary>
    /// Enum that lists the types in the data management
    /// </summary>
    public enum DataManagementTypes
    {
        Points,
        Substances,
        Potencies,
        MyAdditions,
        Products
    }

    public enum ProductInfoEnum
    {
        None,
        ProductsOnBackorder,
        DiscontinuedProducts
    }

    public enum FileToDownloadCheck
    {
        None,
        UpdatesAvailable,
        NoUpdateAvailable,
        UpdateCheckErrorOccured
    }

    /// <summary>
    /// Enum for results of checking if there are script updates available and also for execuritng them
    /// </summary>
    public enum ScriptUpdateExecuteCheck
    {
        None,
        UpdatesAvailable,
        NoUpdateAvailable,
        UpdateCheckErrorOccured,
        ScriptFailed,
        ScriptMissing,
        ScriptSuccess
    }

    /// <summary>
    /// Enum for different types of files that Vital downloads
    /// </summary>
    public enum FilesDownloadType
    {
        ReleaseScripts,
        DataScripts,
        Database,
        ProductsOnBackorder,
        DiscontinuedProducts,
        Images
    }

    public enum DBCreateResult
    {
        None,
        NoFilesToDownload,
        DownloadErrorOccurred,
        ErrorOccurred,
        Success,
        DBCreateError
    }

    public enum DBCreationProcessState
    {
        None,
        DBCreationSuccessful,
        DBFileMissing,
        DBCreationFailed,
        DBInfoMissing,
        ErrorOccured,
        DBIncompatibleVersion
    }

    /// <summary>
    /// An enum for the result of a multiple files download
    /// </summary>
    public enum FilesDownloadResult
    {
        None,
        Sucess,
        DownloadError,
        FileAccessError,
        NoFilesToDownload,
        NoInternetConnection,
        Cancelled,
        ErrorOccurred
    }

    /// <summary>
    /// Enum for the states of the update scripts download
    /// </summary>
    public enum FilesDownloadState
    {
        Downloaded,
        Failed,
        DownloadFilesMissing,
        DownloadFolderMissing,
        Unkown,
        ErrorDuringCheck
    }

    public enum DBScriptProcessState
    {
        ScriptSuccessful,
        ScriptMissing,
        ScriptFailed,
        VersionInfoMissing,
        ErrorOccured,
        ScriptIncompatibleVersion
    }

    /// <summary>
    /// Enum for view options of multiple images
    /// </summary>
    public enum MultipleImageOptions
    {
        [DescriptionAttribute("None")]None,
        [DescriptionAttribute("Single Image")] SingleImage,
        [DescriptionAttribute("Multiple Images")] MultipleImages,
        [DescriptionAttribute("Parent Image")] ParentImage,
        [DescriptionAttribute("Issue Image")] IssueImage
    }

    /// <summary>
    /// Enum for different cases of image selection
    /// </summary>
    public enum MultiImageSelectionCases
    {
        None,
        SingleWithImage,
        SingleNoImage,
        MultipleNone,
        MultipleSingleImage,
        MultipleAllSame,
        MultipleMix
    }

    /// <summary>
    /// Enum for different modes of multiple images
    /// </summary>
    public enum MultipleImageViewOptions
    {
        Single,
        MultipleGallery,
        MultipleNavigator,
        Default,
        KeepCurrentImage
    }

    /// <summary>
    /// Enum for MultipleImageModes
    /// </summary>
    public enum MultipleImageModes
    {
        Gallery,
        Navigator
    }

    /// <summary>
    /// Enum for the type of selection provided to the item detail window
    /// </summary>
    public enum NavGridSelection
    {
        UserSelect,
        SystemHighlight
    }

    /// <summary>
    /// Enum for the default of view of the multiple images option
    /// </summary>
    public enum MultipleImageDefaultView
    {
        SelectionGallery,
        SelectionNavigation
    }

    /// <summary>
    /// Enum that lists the app info keys
    /// </summary>
    public enum AppInfoKeys
    {
        Version,
        DBVersion,
        AppBranch,
        ProductsEntryCapacity,
        CheckForDongle,
        ReleaseNotes,
        ShippingTimeZoneId,
        ShippingTimeZoneDisplayName,
        ShippingOpenAt,
        ShippingCloseAt,
        ShippingWeekEndDays,
        ShippingTargetEmail,
        FeedbackTargetEmail,
        ShippingSenderEmail,
        ShippingSenderPass,
        FeedbackSenderEmail,
        FeedbackSenderPass,
        ExceptionSenderEmail,
        ExceptionSenderPass,
        ProductsOnBackorder,
        ProductsOnBackorderLastModified,
        DiscontinuedProducts,
        DiscontinuedProductsLastModified,
        ShowPhysiciansHRAd,
        TechnicianName,
        TechnicianClinicName,
        TechnicianClinicWebsite,
        TechnicianAddress,
        TechnicianCity,
        TechnicianState,
        TechnicianZip,
        TechnicianPhone,
        TechnicianEmail,
        VitalKey,
        VitalKeyDate,
        VitalKeyDataSentToServer
    }
    
    /// <summary>
    /// Enum that lists the types of loading the test issues from the DB
    /// </summary>
    public enum TestIssuesLoadingType
    {
        NormalIssuesOnly,//Gets only normal issues and filters the main issue
        MainIssueOnly,   //Gets on the Main issue for the test without the normal issues
        Both             //Gets all the issues regardless main or not
    }

    /// <summary>
    /// Enum for Item Keys
    /// </summary>
    public enum ItemKeys
    {
        TestMainIssue
    }

    /// <summary>
    /// Enum for commands used to communicate with active Splash Screen
    /// </summary>
    public enum SplashScreenCommand
    {
        SetText
    }

    /// <summary>
    /// Enum for the status of checking DB existance
    /// </summary>
    public enum DBCheckStatusEnum
    {
        ExistsAndConnected,
        DatabaseNotFound,
        ConnectionError,
        FilesAccessError
    }

    /// <summary>
    /// Enum for SQL DB Tasks
    /// </summary>
    public enum SQLTask
    {
        Create,
        Update,
        Backup,
        Verify,
        Restore
    }

    /// <summary>
    /// List of actions for imprinting
    /// </summary>
    public enum ImprintingAction
    {
        Imprint,
        RemoveFromImprintList,
        MarkNotImprintable
    }

    /// <summary>
    /// Drag actions for tree
    /// </summary>
    public enum TreeDragAction
    {
        Move,
        DragUp,
        DragDown,
        None,
        Root
    }

    /// <summary>
    /// The DongleState Enum.
    /// </summary>
    public enum DongleState
    {
        LeaseExpired,
        NonLeasedDongle,
        SysDateSetBack,
        NoLeaseDate,
        LeaseDateBad,
        LastSysDateCorrupt,
        LessThanTenDays,
        LessThanFiveDays,
        Default,
        NotCompitable
    }

    /// <summary>
    /// This enum will be used to handle the automatic navigation in the item testing form
    /// </summary>
    public enum TestStage
    {
        Eds,
        EdsAutoPlay,
        ItemTesting,
        MajorIssuesCount,
        SetMajorIssues,
        AddMajorIssue,
        MajorIssueTest,        
        VitalForce,
        FourFactors,
        Ratios,
        Top10Causes,
        ProductSchedule,
        EdsBalancing
    }

    /// <summary>
    /// Lookups Types Enum.
    /// </summary>
    public enum LookupTypes
    {
        Gender,
        TestType,
        TestState,
        ItemType,
        ListType,
        TargetType,
        HistoryType,
        SettingGroup,
        YesNo,
        OnOff,        
        ListPoints,
        PointsNaming,
        BroadcastMethodologies,
        RelationType,
        ItemGender,
        ValueTypes,
        ApplicableType,
        LeftRight,
        SpotCheckResultType,
        SourceType,
        EvalPeriodType,
        ServiceType,
        AdjustmentApply,
        ShippingMethod,
        VFSSecondarySection,
        VFSSourceItemValueType,
        VFSSourceItemSection,
        VFSSourceItemGroup,
        VFSSourceItemGridGroup,
        VFSSourceItemChangeType,
        ItemState,
        MultipleImageOptions,
        MultipleImageModes,
        AutoItemType,
        AutoItemStructureType,
        AutoItemStatus,
        ChildsOrderType,
        ChildsScanningType,
        AutoItemScanningMethod
    }


    /// <summary>
    /// VFS Secondary items sections
    /// </summary>
    public enum VFSSecondarySection
    {
        PrimaryIssues,
        SecondaryIssues,
        ThyroidIssues,
        MercuryIssues,
        None //NOT IN DB
    }

    /// <summary>
    /// Four Factors Enum
    /// </summary>
    public enum FourFactors
    {
        [DescriptionAttribute("LY-1-2 (CMP)")]LY,
        [DescriptionAttribute("NE-1b  (CMP)")]NE,
        [DescriptionAttribute("CI-8d  (CMP)")]CI,
        [DescriptionAttribute("OR-1b  (CMP)")]OR
    }

    /// <summary>
    /// Ratios Enum
    /// </summary>
    public enum Ratios
    {
        [DescriptionAttribute("10/4")] R1,
        [DescriptionAttribute("12/3")] R2,
        [DescriptionAttribute("14/2")] R3,
        [DescriptionAttribute("16/1")] R4,
        [DescriptionAttribute("18/0")] R5
    }

    /// <summary>
    /// Enum for VFSSourceItemGroup
    /// </summary>
    public enum VFSSourceItemGroup
    {
        [DescriptionAttribute("Overall")]Overall,
        [DescriptionAttribute("Major Systems")]MajorSystems,
        [DescriptionAttribute("Organs and Glands 1")]OrganAndGlandsOne,
        [DescriptionAttribute("Organs and Glands 2")]OrganAndGlandsTwo,
        [DescriptionAttribute("Age")]Age,
        [DescriptionAttribute("Services1")]Services1,
        [DescriptionAttribute("Services2")]Services2,
        [DescriptionAttribute("pH")]pH,
        [DescriptionAttribute("Hormones")]Hormones,
        [DescriptionAttribute("None")]None
    }

    /// <summary>
    /// Enum for VFSSourceItemGridGroup
    /// </summary>
    public enum VFSSourceItemGridGroup
    {
        [DescriptionAttribute("Overall")]Overall,
        [DescriptionAttribute("Major Systems")]MajorSystems,
        [DescriptionAttribute("Organs and Glands")]OrganAndGlandsOne,
        [DescriptionAttribute("Nervous System")]NervousSystem,
        [DescriptionAttribute("Age")]Age,
        [DescriptionAttribute("Services1")]Services1,
        [DescriptionAttribute("Services2")]Services2,
        [DescriptionAttribute("pH")]pH,
        [DescriptionAttribute("Hormones")]Hormones,
        [DescriptionAttribute("None")]None
    }

    /// <summary>
    /// Enum for VFS change type
    /// </summary>
    public enum VFSSourceItemChangeType
    {
        Normal,
        Elevation,
        Decrease
    }

    /// <summary>
    /// Enum for VFS value types
    /// </summary>
    public enum VFSSourceItemValueType
    {
        Integer,
        Decimal,
        Percentage,
        String,
        Lookup,
        None //NOT IN DB
    }

    /// <summary>
    /// Four Factor State
    /// </summary>
    public enum FourFactorState
    {
        Balanced,
        UnBalanced,
        Clear
    }

    /// <summary>
    /// Adjustment apply Types Enum.
    /// </summary>
    public enum AdjustmentApplyEnum
    {
        Products,
        Services
    }

    /// <summary>
    /// Service Types Enum.
    /// </summary>
    public enum ServiceType
    {
        SystemService,
        UserService,
        OnFlyService
    }

    /// <summary>
    /// Shipping method Enum.
    /// </summary>
    public enum ShippingMethod
    {
        Unspecified,
        USPS,
        [DescriptionAttribute("UPS Ground")] UPSGround,
        [DescriptionAttribute("UPS 3 Day Select")] UPS3DaySelect,
        [DescriptionAttribute("UPS 2nd Day Air")] UPS2ndDayAir,
        [DescriptionAttribute("UPS 2nd Day Air A.M.")] UPS2ndDayAirAM,
        [DescriptionAttribute("UPS Next Day Air Saver")] UPSNextDayAirSaver,
        [DescriptionAttribute("UPS Next Day Air")] UPSNextDayAir,
        [DescriptionAttribute("UPS Next Day Air Early A.M.")] UPSNextDayAirEarlyAM,
    }

    /// <summary>
    /// Service Keys
    /// </summary>
    public enum ServiceKeys
    {
        DefaultTestingFee,
        DefaultRevalTestingFee
    }

    public enum SettingGroups
    {
        ReportOptions,
        TechnicianInfo,
        ShippingOrders,
        BackupAndRestore,
        HardwareProfileSettings
    }

    /// <summary>
    /// Enum for the source types.
    /// </summary>
    public enum SourceTypeEnum
    {
        SystemItem,
        UserNewItem,
        UserMigratedItem
    }

    /// <summary>
    /// Enum for different item states
    /// </summary>
    public enum ItemStateEnum
    {
        Active,
        SystemHidden,
        UserHidden
    }

    /// <summary>
    /// Enum for revaluation period Types.
    /// </summary>
    public enum EvalPeriodTypeEnum
    {
        Weeks,
        Months
    }

    /// <summary>
    /// The item gender options
    /// </summary>
    public enum ItemGender
    {
        ItemGenderNone,
        ItemGenderMale,
        ItemGenderFemale,
        ItemGenderBoth
    }

    /// <summary>
    /// The Gender Enum.
    /// </summary>
    public enum GenderEnum
    {
        Male,
        Female
    }

    /// <summary>
    /// The ApplicableType enum.
    /// </summary>
    public enum ApplicableTypesEnum
    {
        Item,
        ItemRelation,
        ItemAndItemRelation,
        Product
    }

    /// <summary>
    /// Test State Enum.
    /// </summary>
    public enum TestStateEnum
    {
        InProgress,
        NotStarted,
        Done,
        [DescriptionAttribute("Done/Shipped")] DoneShipped
    }

    public enum RelationTypeEnum
    {
        None
    }

    /// <summary>
    /// Items Types Enum.
    /// </summary>
    public enum ItemTypeEnum
    {
        Point,
        Substance,
        Potency,
        Factor,
        Product,
        Bacteria,
        Ingredient,
        None
    }

    /// <summary>
    /// List Types Enum.
    /// </summary>
    public enum ListTypeEnum
    {
        UserList,
        SystemList,
        None,
    }

    /// <summary>
    /// Settings Types Enum.
    /// </summary>
    public enum ValueTypes
    {
        Numerical,
        Text,
        List,
        Memo
    }

    /// <summary>
    /// Targets Types Enum.
    /// </summary>
    public enum TargetType
    {
        MyPointsList,
        MySubstancesList,
        MyPotenciesList,
        MySubstancesAdditionsList,
        TestProtocolsList,
        SystemPointsList,
        SystemSubstancesList,
        SystemPotenciesList,
        GoToPropertyList,
        FourFactors,
        Ratios,
        MyProductsList,
        IVSheet,
        CapsolT,
        Minerals,
        Dmps,
        EnergyFrequencies,
        FourCauses,
        None
    }

    public enum SpotCheckResultType
    {
        IVSheet,
        CapsolT,
        Minerals,
        Dmps
    }

    public enum OnOffEnum
    {
        On,
        Off
    }

    /// <summary>
    /// Settings Keys Enum.
    /// </summary>
    public enum SettingKeys
    {
        CommunicationsPort,
        PointsFull,
        PointsShort,
        Potency,
        Display,
        PrintSaveTankDescriptiveText,
        ListPoints,
        Frequency,
        Duration,
        ShowDormantItems,
        HawverProtocolDefault,
        FullOrShort,
        BiofeedbackDevice,
        TimeBeforeLockingReading,
        ReadingRiseTime,
        DefaultDropLock,
        DefaultDoEventsLoop,
        PointsNaming,
        EdsNextPointNavigationTime,
        FilterBalancedPoints,
        ItemTestingNavigationTime,
        DefaultTestProtocol,
        NewSubstanceWaiting,
        NewSubstanceWaitingTime,
        BroadcastMethodology,
        BroadcastingStatus,
        NumberOfVitalForceItemsInGroup,        
        DefaultTestingTax,
        FontSize,
        MeterPosition,
        SkinName,
        ClearSelectionOnNoReading,
        ReportDisclaimer,
        AutoOpenProductDosages,
        FourFactorsFullName,
        AutoCloseDialogs,
        AutoCloseDialogsTimeout,
        ReportPrintEdsReadings,
        ReportPrintTestResults,
        ReportPrintDescriptiveText,
        ReportUseFullPointName,
        ReportShowMeridian,
        ReportShowDateWithoutTime,
        ReportHidePatientName,
        ReportShowTestNotes,        
        ReportHideLogo,
        ShowHints,
        ReportGroupDilutions,
        TechnicianName,
        TechnicianAddress,
        TechnicianCity,
        TechnicianState,
        TechnicianZip,
        TechnicianPhone,
        ShowTechnicianInfo,
        DefaultRevalPeriod,        
        ClinicInfo,
        ShowPatientSearchAutomatically,
        AutoEnableEdit,
        DefaultAdjustmentValue,
        DefaultAdjustmentPercent,
        DefaultAdjustmentType,
        DefaultAdjustmentApplying,
        RemindBeforeShippingClose,
        RemindWhenShippingClose,
        RemindToOrderBeforeClosingTest,
        RemindBeforeShippingCloseHours,
        ShippingBarPosition,
        SendShipmentToClient,
        ShowShippingBar,
        DefaultAlertDuration,
        ShowAddressInfo,
        AutomaticBackup,
        BackupPeriod,
        LastBackupDate,
        LastBackupNotificationDate,
        ShowBackupReminder,
        DefaultBackupRestoreLocation,
        AutoRemoveOldBackups,
        RecentBackupFilesCount,
        OffSystemBackupCounter,
        HideAdjustmentFields,
        TechnicianEmail,
        ShippingMethod,
        ImprintingTimerEnabled,
        ImprintingTime,
        ImprintingSoundEnabled,
        DefaultSecondaryBackupRestoreLocation,
        MinimumReadingtoRegister,
        CsaDisconnectedTimeout,
        ReadingStabilityTimeout,
        ReadingStabilityRange,
        UseAutoZoom,
        GalleryDefaultZoomLevel,
        ProductInfoUpdateNotificationEnabled,
        KeepOneByOneModeOn,
        FrequencyTestItemsPath,
        ShowScheduleReportDefaultInstructions,
        EnableAutomationWhenOpeningExistingTest,
        ShowScreenClosingConfirmation,
        AutoTestDeviceComPort,
        AutomatedTestScreenEnabled
    }

    public enum ConfigKeys
    {
        HardwareCheckDelay,
        StageAutoItemPostDelay,
        MultiLevelStageItemDelay,
        ProductDosageDelay,
        CSAGeneralCommandDelay,
        ReadingStabilityDelay,
        MutliLevelStageItemPostDelay,
        UseStableReadingsOnly,
        EnableDebuggingLongHWDisconnectTimeout,
        DebuggingLongHWDisconnectTimeout,
        EnableAutomationPostFailureAutoResume,
        MutliLevelScanningEliminationRounds,
        UseHumanAnatomyView,
        PerformPressureCheck,
        PerformMoistureCheck,
        PerformHingeCheck,
        UseSimulatedReadings
    }

    public enum ShippingTarget
    {
        ShipToClient,
        ShipToMe
    }


    /// <summary>
    /// Points Naming Enum.
    /// </summary>
    public enum PointsNaming
    {
        FullName,
        ShortName
    }

    /// <summary>
    /// Tests Types Enum.
    /// </summary>
    public enum TestType
    {
        Full,
        Short
    }

    /// <summary>
    /// Lists Points Enum.
    /// </summary>
    public enum ListPoints
    {
        Both,
        Left,
        Right
    }

    /// <summary>
    /// Error codes for ConfigurationException
    /// </summary>
    public enum ConfigurationExceptionErrorCode
    {
        GeneralConfigurationError = 0,
        SettingNotFound,
        SettingValueIsInvalid,
        ConnectionStringSettingIsInvalid,
        ConnectionStringSettingHasInvalidDatabaseName,
        ConnectionStringTimeout
    }; 

    /// <summary>
    /// The Broadcasting types.
    /// </summary>
    public enum BroadcastMethodologies
    {
        VitalIngredientHashCode,
        VitalMessage,
        VitalDegreeOfAffect,
        VitalPointHashCode,
        BlankString,
        VitalMessageIngredients
    }

    /// <summary>
    /// The Properties enum.
    /// </summary>
    public enum PropertiesEnum
    {
        InsertOnNo,
        HasVitalForce,
        HasFourFactors,
        HasRatios,
        DoNotEnergize,
        ProductSize,
        SizeUnit,
        ProductQuantity,
        ProductContains,
        SuggestedUsage,
        ProductSupports,
        FoodsFoundIn,
        GoTo,
        ChildItemsOrder,
        GoToCustomDilutions,
        IsImprintable,
        DontFilter,
        GotoTop10Causes,
        DefaultDuration,
        DefaultWhenArising,
        DefaultBreakfast,
        DefaultBetweenMealsLate,
        DefaultLunch,
        DefaultBetweenMealsEarly,
        DefaultDinner,
        DefaultBeforeSleep,
        DefaultNoPerBottle,        
        DefaultPrice,
        DefaultNotes,
        MineralMGML,
        DosageMinimum,
        DosageMaximum,
        ItemNotes,
        ItemState,
        UseParentImageOnSplitSwitch
    }

    /// <summary>
    /// The Yes No enum.
    /// </summary>
    public enum YesNoEnum
    {
        Yes,
        No
    }

    /// <summary>
    /// The Left Right enum.
    /// </summary>
    public enum LeftRightEnum
    {
        Left,
        Right
    }

    public enum SingleValueTypeEnum
    {
        Settings,
        Item,
        AppInfo
    }

    public enum ActionType
    {
        DrillDown,
        Back,
        FullSave,
        AddToTestResults
    }

    public enum NavigationGridType
    {
        Editable,
        ReadOnly
    }

    public enum DisabledControlsTypes
    {
        SimpleButton,
        TextEditor
    }

    public enum GuageOperation
    {
        IncrementValue,
        DecrementValue
    }

    public enum PaymentMethodsEnum
    {
        Cash,
        Cheque,
        CreditCard
    }

    public enum ApplicationBranches
    {
        DEV,
        PROD,
        TEST,
        UAT,
        UNKOWN
    }

    public enum HwProfileKeyEnum
    {
        OldHwProfile,
        NewHwProfile
    }

    #endregion

    #region Helper Classes

    /// <summary>
    /// Enum Name Resolver contains the methods that returns the name of the enum key.
    /// </summary>
    public class EnumNameResolver
    {
        /// <summary>
        /// Gets the description attribute of the enum if any
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumNameOrDescription<T>(T value)
        {
            var descriptionAttribute = Enum.GetName(typeof(T), value);
            
            try
            {
                var enumType = typeof(T);
                if (Enum.IsDefined(enumType, value))
                {
                    var memInfo = enumType.GetMember(descriptionAttribute);

                    if (memInfo.Count() != 0)
                    {
                        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                        if (attributes.Count() != 0)
                        {
                            descriptionAttribute = ((DescriptionAttribute)attributes[0]).Description;
                        } 
                    }
                }
            }
            catch
            {

            }

            return descriptionAttribute;
        }

        /// <summary>
        /// Gets enum value using the description attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }
        /// <summary>
        /// Resolve a value of an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="isMultiWords"></param>
        /// <returns></returns>
        public static string Resolve<T>(T key, bool isMultiWords = false)
        {
            return isMultiWords ? ResolveMultiWord(key) : GetEnumNameOrDescription(key);
        }
        
        /// <summary>
        /// Resolving the name of the setting key with multi word.
        /// </summary>
        /// <param name="multiWordKey"></param>
        /// <returns></returns>
        private static string ResolveMultiWord<T>(T multiWordKey)
        {
            return AddSpacesToSentence(GetEnumNameOrDescription(multiWordKey));
        }

        /// <summary>
        /// Replace the c\ital later with a white space.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var newText = new StringBuilder();

            newText.Append(text[0]);

            for (var i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');

                newText.Append(text[i]);
            }

            return newText.ToString();
        }

        /// <summary>
        /// Gets the lookup value as Enum(T)"/>
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="lookupValue"></param>
        /// <returns></returns>
        public static T LookupAsEnum<T>(string lookupValue)
        {
            if (typeof(T).BaseType != typeof(Enum))
                throw new VitalLogicalException(typeof (T).FullName + "should be an System.Enum or primitive enum.");

            return (T)Enum.Parse(typeof(T), lookupValue.Replace(" ",string.Empty));
        }

        /// <summary>
        /// Gets the lookup value as Enum(T)"/>
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="lookup"></param>
        /// <returns></returns>
        public static T LookupAsEnum<T>(Lookup lookup)
        {
            return lookup == null || (lookup.Id == 0 && string.IsNullOrEmpty(lookup.Key)) ? LookupAsEnum<T>("Undefined") : LookupAsEnum<T>(lookup.Key);
        }

        /// <summary>
        /// Gets the string value as Enum(T)"/>
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T StringAsEnum<T>(string value)
        {
            if (typeof(T).BaseType != typeof(Enum))
                throw new VitalLogicalException(typeof(T).FullName + "should be an System.Enum or primitive enum.");

            return (T)Enum.Parse(typeof(T), value.Replace(" ", string.Empty));
        }

        /// <summary>
        /// Gets the string value as Enum(T)"/>
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T StringAsEnumWithUndefined<T>(string value) where T : struct
        {
            try
            {
                return StringAsEnum<T>(value);
            }
            catch (Exception)
            {
                return StringAsEnum<T>("Undefined");
            }
        }
    }

    #endregion
}