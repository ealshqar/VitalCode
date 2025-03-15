namespace Vital.UI.Enums
{
    /// <summary>
    /// Enum that indicates the version compatability state between code & DB.
    /// </summary>
    public enum VersionCheck
    {
        Failed = 0, 
        Equal, 
        DatabaseIsNewer,
        DatabaseIsOlder,
        DatabaseNotFound,
        ConnectionError,
        FilesAccessError
    }

    /// <summary>
    /// Enum for checking DB & Code Compatibility
    /// </summary>
    public enum BranchCompatibilityCheck
    {
        BranchIncompatible,
        BranchCompatible,
        NoBranchProperty
    }

    /// <summary>
    /// Enum that indicates the from status.
    /// </summary>
    public enum FormStatusEnum
    {
        Locked,
        UnLocked,
        Disabled,
        Editable,
        New,
        Modified,
        Unchanged
    }

    /// <summary>
    /// Enum that indicates the auto play readings status.
    /// </summary>
    public enum TestPlayStateEnum
    {
        Playing,
        Paused
    }

    /// <summary>
    /// Enum for auto test mode in spot check form.
    /// </summary>
    public enum SpotCheckAutoTestMode
    {
        Starred,
        OneByOne,
        SplitAndSwitch
    }

    /// <summary>
    /// Enum for auto test mode in FrequencyTest form.
    /// </summary>
    public enum FrequencyTestAutoTestMode
    {
        Starred,
        OneByOne,
        SplitAndSwitch
    }

    /// <summary>
    /// Enum for spot check dialog type.
    /// </summary>
    public enum SpotCheckDialogType
    {
        SpotCheckResult,
        SpotCheck
    }

    /// <summary>
    /// Enum that indicates the bottom progress bar status.
    /// </summary>
    public enum TestBarStateEnum
    {
        TakeReading,
        Reading,
        Ready,
        WaitMoving,
        WaitBeforTakeAction,
        WaitingToRelease,
        HideStatus,
        Yes,
        No,
        HideYesNo
    }

    /// <summary>
    /// Reading requester type
    /// </summary>
    internal enum ReadingRequesterTypeEnum
    {
        Eds,
        ItemTestingIssueNav,
        Others
    }

    /// <summary>
    /// The status of the connection indicator.
    /// </summary>
    internal enum ConnectionIndicatorStatusEnum
    {
        Connected,
        Disconnected,
        Broadcasting,
        NotBroadcasting,
        Imprinting,
        Searching,
        SearchingPort
    }

    /// <summary>
    /// Enums for reading type.
    /// </summary>
    public enum ReadingPlayTypes
    {
        Eds,
        ItemTesting,
        TestSchedule,
        None
    }

    /// <summary>
    /// Enum for auto test type for the spot check.
    /// </summary>
    public enum SpotCheckReadingPlayType
    {
        Mtes,
        Ingredients,
        CapsolT,
        Dmps
    }

    /// <summary>
    /// Indicator Component Status enum
    /// </summary>
    public enum IndicatorComponentStatus
    {
        Red = 1,
        Ornage = 2,
        Green = 3
    }

    /// <summary>
    /// Enum for About form types.
    /// </summary>
    public enum AboutFormType
    {
        About,
        ReleaseNotes
    }

    /// <summary>
    /// Enum for Alert Keys.
    /// </summary>
    public enum AlertKey
    {
        ShippingClosesAfter,
        ShippingCloses
    }

}
