using System.Deployment.Application;

namespace Vital.Update.Enums
{
    /// <summary>
    /// The update types enum.
    /// </summary>
    public enum UpdateType
    {
        Required,
        Optional,
        None
    }

    /// <summary>
    /// ApplicationUpdateProgressState enum.
    /// </summary>
    public enum ApplicationUpdateProgressState
    {
        DownloadingApplicationFiles,
        DownloadingApplicationInformation,
        DownloadingDeploymentInformation,
    }

    /// <summary>
    /// The update action types.
    /// </summary>
    internal enum UpdateActionType
    {
        CheckForUpdate,
        CheckForUpdateThenUpdate,
        CancelCheckForUpdate,
        CancelUpdate
    }

    #region EnumHelpers 
    
    public static class EnumHelpers
    {
        /// <summary>
        /// Map DeploymentProgressState enum to ApplicationUpdateProgressState enum.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static ApplicationUpdateProgressState Map(DeploymentProgressState src)
        {
            var mapResult = new ApplicationUpdateProgressState();

            switch (src)
            {
                case DeploymentProgressState.DownloadingDeploymentInformation:
                    mapResult = ApplicationUpdateProgressState.DownloadingDeploymentInformation;
                    break;
                case DeploymentProgressState.DownloadingApplicationInformation:
                    mapResult =  ApplicationUpdateProgressState.DownloadingApplicationInformation;
                    break;
                case DeploymentProgressState.DownloadingApplicationFiles:
                    mapResult = ApplicationUpdateProgressState.DownloadingApplicationFiles;
                    break;
            }

            return mapResult;
        }

    }

    #endregion
}
