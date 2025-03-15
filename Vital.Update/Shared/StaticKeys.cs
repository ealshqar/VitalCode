namespace Vital.Update.Shared
{
    public static class StaticKeys
    {
        #region CheckForUpdatesMessages

        public const string DownloadError = "The new version of Vital cannot be downloaded at this time. \n\nPlease check your network connection, or try again later.";

        public const string InvalidDeployment = "Cannot check for a new version of Vital. The deployment is corrupt. Please redeploy Vital and try again.";

        public const string InvalidOperation = "Vital cannot be updated. It is likely not a updateable version.";

        #endregion

        #region UpdateResult Messages

        public const string UpdateAvailable = "An update is available.";

        public const string UpdateUnavailable = "Update is unavailable.";

        public const string RequestedUpdateVersionUnavailable = "Requested update version is unavailable";

        public const string RequiredUpdateAvailable = "Vital has detected a mandatory update from your current " + "version to version {0}.";

        public const string InstallAndRestart = "Vital will now install the update and restart.";

        public const string Upgraded = "The application has been upgraded, and will now restart.";

        #endregion

        #region CustomExceptionsMessgaes

        public const string ReatartEventHavNoHandler = "The RestartEvent Should be handled.";

        #endregion
    }
}
