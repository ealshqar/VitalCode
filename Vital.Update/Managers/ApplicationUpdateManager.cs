using System;
using System.ComponentModel;
using System.Deployment.Application;
using System.Windows.Forms;
using Vital.Update.Enums;
using Vital.Update.EventArgs;
using Vital.Update.Exceptions;
using Vital.Update.Shared;

namespace Vital.Update.Managers
{
    public class ApplicationUpdateManager
    {
        #region Events

        /// <summary>
        /// Event for ApplicationUpdateProgressChanged.
        /// </summary>
        public event AppUpdateProgressChangedHandler ApplicationUpdateProgressChanged;

        /// <summary>
        /// Event for ApplicationUpdateCompleted.
        /// </summary>
        public event AppUpdateCompletedHandler ApplicationUpdateCompleted;

        /// <summary>
        /// Event for ApplicationCheckUpdateProgressChanged.
        /// </summary>
        public event AppUpdateCheckProgressChangedHandler ApplicationUpdateCheckProgressChanged;

        /// <summary>
        /// Event for ApplicationUpdateCheckCompleted.
        /// </summary>
        public event AppUpdateCheckCompletedHandler ApplicationUpdateCheckCompleted;

        /// <summary>
        /// Event for RestartRequrest.
        /// </summary>
        public event RestartRequrestHandler RestartRequrest;

        #endregion

        #region Event Invokers

        /// <summary>
        /// Invoke ApplicationUpdateProgressChanged event;
        /// </summary>
        private void OnApplicationUpdateProgressChanged(AppActionProgressChangedEventArgs e)
        {
            if (ApplicationUpdateProgressChanged != null)
                ApplicationUpdateProgressChanged(this, e);
        }

        /// <summary>
        /// Invoke ApplicationUpdateCompleted event;
        /// </summary>
        private void OnApplicationUpdateCompleted(AppUpdateCompletedEventArgs e)
        {
            if (ApplicationUpdateCompleted != null)
                ApplicationUpdateCompleted(this, e);
        }

        /// <summary>
        /// Invoke ApplicationUpdateCheckProgressChanged event;
        /// </summary>
        public void OnApplicationUpdateCheckProgressChanged(AppActionProgressChangedEventArgs e)
        {
            if (ApplicationUpdateCheckProgressChanged != null)
                ApplicationUpdateCheckProgressChanged(this, e);
        }

        /// <summary>
        /// Invoke ApplicationUpdateCheckCompleted event;
        /// </summary>
        public void OnApplicationUpdateCheckCompleted(AppUpdateCheckCompletedEventArgs e)
        {
            if (ApplicationUpdateCheckCompleted != null)
                ApplicationUpdateCheckCompleted(this, e);
        }

        /// <summary>
        /// Invoke RestartRequrest event;
        /// </summary>
        private void OnRestartRequrest()
        {
            if (RestartRequrest != null)
            {
                
                RestartRequrest(this);
            }
            else
            {
                throw new RestartEventNotHandledException();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the version of the application either from click once or the assembly.
        /// </summary>
        public Version CurrentPublishVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion : new Version(Application.ProductVersion);
            }
        }

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks for update.
        /// </summary>
        public ProcessResult CheckForUpdate()
        {
            return DoApplicationUpdateAction(UpdateActionType.CheckForUpdate);
        }

        /// <summary>
        /// Update the Application with passed version.
        /// </summary>
        /// <returns></returns>
        public ProcessResult Update()
        {
            return DoApplicationUpdateAction(UpdateActionType.CheckForUpdateThenUpdate);
        }

        /// <summary>
        /// Cancel the checking for update operation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CancelCheckForUpdate()
        {
            return DoApplicationUpdateAction(UpdateActionType.CancelCheckForUpdate);
        }

        /// <summary>
        /// Cancel the update Operation.
        /// </summary>
        /// <returns></returns>
        public ProcessResult CancelUpdate()
        {
            return DoApplicationUpdateAction(UpdateActionType.CancelUpdate);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the update progress handlers.
        /// </summary>
        /// <param name="applicationDeployment">The ApplicationDeployment.</param>
        private void SetUpdateHandlers(ApplicationDeployment applicationDeployment)
        {
            applicationDeployment.UpdateCompleted += ApplicationDeploymentUpdateCompleted;
            applicationDeployment.UpdateProgressChanged += ApplicationDeploymentUpdateProgressChanged;
        }

        /// <summary>
        /// Sets the update check progress handlers.
        /// </summary>
        /// <param name="applicationDeployment">The ApplicationDeployment.</param>
        /// <param name="doUpdate">Don't fire the events out side the manager, used for doing the update after checking completed.</param>
        private void SetUpdateCheckHandlers(ApplicationDeployment applicationDeployment, bool doUpdate)
        {
            if (doUpdate)
            {
                applicationDeployment.CheckForUpdateCompleted += ApplicationDeploymentCheckForUpdateThenUpdateCompleted;
                //We need to deal with the CheckForUpdateProgressChanged as changed for update,
                //because we need to handle it as a Update Progress changed not as checking for Progress changed.
                applicationDeployment.CheckForUpdateProgressChanged += ApplicationDeploymentUpdateProgressChanged;
            }
            else
            {
                applicationDeployment.CheckForUpdateProgressChanged += ApplicationDeploymentCheckForUpdateProgressChanged;
                applicationDeployment.CheckForUpdateCompleted += ApplicationDeploymentCheckForUpdateCompleted;
            }
        }

        /// <summary>
        /// Remove the update progress handlers.
        /// </summary>
        /// <param name="applicationDeployment">The ApplicationDeployment.</param>
        private void RemoveUpdateHandlers(ApplicationDeployment applicationDeployment)
        {
            applicationDeployment.UpdateCompleted -= ApplicationDeploymentUpdateCompleted;
            applicationDeployment.UpdateProgressChanged -= ApplicationDeploymentUpdateProgressChanged;
        }

        /// <summary>
        /// Remove the update check progress handlers.
        /// </summary>
        /// <param name="applicationDeployment">The ApplicationDeployment.</param>
        private void RemoveUpdateCheckHandlers(ApplicationDeployment applicationDeployment)
        {
            applicationDeployment.CheckForUpdateCompleted -= ApplicationDeploymentCheckForUpdateThenUpdateCompleted;

            /*We need to deal with the CheckForUpdateProgressChanged as changed for update,
            because we need to handle it as a Update Progress changed not as checking for Progress changed.*/

            applicationDeployment.CheckForUpdateProgressChanged -= ApplicationDeploymentUpdateProgressChanged;
            applicationDeployment.CheckForUpdateProgressChanged -= ApplicationDeploymentCheckForUpdateProgressChanged;
            applicationDeployment.CheckForUpdateCompleted -= ApplicationDeploymentCheckForUpdateCompleted;
        }

        /// <summary>
        /// Gets the optimal message for the passed exception.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetCustomExceptionMessage(Exception exception)
        {
            if (exception == null)
                return string.Empty;

            if (exception is DeploymentDownloadException)
            {
                return StaticKeys.DownloadError;
            }

            if (exception is InvalidDeploymentException)
            {
                return StaticKeys.DownloadError;
            }

            if (exception is InvalidOperationException)
            {
                return StaticKeys.DownloadError;
            }

            return StaticKeys.DownloadError;
        }

        /// <summary>
        /// Starts the update for the application.
        /// </summary>
        /// <returns></returns>
        private ProcessResult StartUpdate()
        {
            var processResult = new ProcessResult { IsSucceed = false };

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    var applicationDeployment = ApplicationDeployment.CurrentDeployment;

                    SetUpdateHandlers(applicationDeployment);

                    applicationDeployment.UpdateAsync();

                    processResult.IsSucceed = true;
                }
                catch (Exception exception)
                {
                    processResult.Exception = exception;
                }
            }
            else
            {
                processResult.Message = StaticKeys.InvalidOperation;
            }

            return processResult;
        }

        /// <summary>
        /// Do an application update action.
        /// </summary>
        /// <param name="actionType">The update action.</param>
        /// <returns></returns>
        private ProcessResult DoApplicationUpdateAction(UpdateActionType actionType)
        {
            var processResult = new ProcessResult { IsSucceed = false };

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var applicationDeployment = ApplicationDeployment.CurrentDeployment;

                try
                {
                    switch (actionType)
                    {
                        case UpdateActionType.CheckForUpdate:
                        case UpdateActionType.CheckForUpdateThenUpdate:
                            RemoveUpdateCheckHandlers(applicationDeployment);
                            SetUpdateCheckHandlers(applicationDeployment, actionType == UpdateActionType.CheckForUpdateThenUpdate);
                            applicationDeployment.CheckForUpdateAsync();
                            break;
                        case UpdateActionType.CancelCheckForUpdate:
                            applicationDeployment.CheckForUpdateAsyncCancel();
                            break;
                        case UpdateActionType.CancelUpdate:
                            applicationDeployment.UpdateAsyncCancel();
                            break;
                    }

                    processResult.IsSucceed = true;
                }
                catch (Exception exception)
                {
                    processResult.Exception = exception;
                    processResult.Message = exception.Message;
                }

            }
            else
            {
                processResult.Message = StaticKeys.InvalidOperation;
            }

            return processResult;
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Handel the UpdateProgressChanged event for the ApplicationDeployment.
        /// </summary>
        private void ApplicationDeploymentUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            OnApplicationUpdateProgressChanged(new AppActionProgressChangedEventArgs
            {
                BytesCompleted = e.BytesCompleted,
                BytesTotal = e.BytesTotal,
                ProgressPercentage = e.ProgressPercentage,
                State = EnumHelpers.Map(e.State)
            });

        }

        /// <summary>
        /// Handel the UpdateCompleted event for the ApplicationDeployment.
        /// </summary>
        private void ApplicationDeploymentUpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var isSuccessful = !e.Cancelled && e.Error == null;

            OnApplicationUpdateCompleted(new AppUpdateCompletedEventArgs
            {
                IsSucceed = isSuccessful,
                Cancelled = e.Cancelled,
                Exception = e.Error,
                Message = GetCustomExceptionMessage(e.Error)
            });

            //Remove the handlers to be ready for the next call.
            var applicationDeployment = sender as ApplicationDeployment;
            if (applicationDeployment != null)
                RemoveUpdateHandlers(applicationDeployment);

            if (!isSuccessful)
                return;

            OnRestartRequrest();
        }


        /// <summary>
        /// Handel the update check Completed event. 
        /// </summary>
        private void ApplicationDeploymentCheckForUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            try
            {
                OnApplicationUpdateCheckCompleted(new AppUpdateCheckCompletedEventArgs
                {
                    IsSucceed = e.Error != null,
                    Exception = e.Error,
                    UpdateAvailable = e.UpdateAvailable,
                    UpdateType = e.UpdateAvailable ? (e.IsUpdateRequired ? UpdateType.Required : UpdateType.Optional) : UpdateType.None,
                    AvailableVersion = e.UpdateAvailable ? e.AvailableVersion : null,
                });
            }
            catch (Exception exception)
            {
                OnApplicationUpdateCheckCompleted(new AppUpdateCheckCompletedEventArgs
                {
                    IsSucceed = false,
                    Exception = exception,
                    UpdateAvailable = false,
                    UpdateType = UpdateType.None,
                    AvailableVersion = null,
                });
            }

            //Remove the handlers to be ready for the next call.
            var applicationDeployment = sender as ApplicationDeployment;
            if (applicationDeployment != null)
                RemoveUpdateCheckHandlers(applicationDeployment);
        }

        /// <summary>
        /// Handel the update check progress changed event. 
        /// </summary>
        private void ApplicationDeploymentCheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            OnApplicationUpdateCheckProgressChanged(new AppActionProgressChangedEventArgs
            {
                BytesCompleted = e.BytesCompleted,
                BytesTotal = e.BytesTotal,
                ProgressPercentage = e.ProgressPercentage,
                State = EnumHelpers.Map(e.State)
            });
        }

        /// <summary>
        /// Handel the update check Completed event with silent mode. 
        /// </summary>
        private void ApplicationDeploymentCheckForUpdateThenUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            var appUpdateCompletedEventArgs = new AppUpdateCompletedEventArgs();

            if (e.Error != null)
            {
                appUpdateCompletedEventArgs.IsSucceed = false;
                appUpdateCompletedEventArgs.Message = GetCustomExceptionMessage(e.Error);
                OnApplicationUpdateCompleted(appUpdateCompletedEventArgs);
            }
            else if (e.Cancelled)
            {
                appUpdateCompletedEventArgs.IsSucceed = false;
                appUpdateCompletedEventArgs.Cancelled = true;
                appUpdateCompletedEventArgs.Message = "Update Cancelled";
                OnApplicationUpdateCompleted(appUpdateCompletedEventArgs);
            }
            else if (e.UpdateAvailable)
            {
                //Start the actual update.
                var result = StartUpdate();

                //Every thing regarding the update is start so just return and don't fire any event.
                if (!result.IsSucceed)
                {
                    appUpdateCompletedEventArgs.IsSucceed = result.IsSucceed;
                    appUpdateCompletedEventArgs.Message = result.Message;
                    //Update not start, so fire an update completed event that include the error.                     
                    OnApplicationUpdateCompleted(appUpdateCompletedEventArgs);
                }
            }
            else
            {
                appUpdateCompletedEventArgs.IsSucceed = false;
                appUpdateCompletedEventArgs.Message = StaticKeys.UpdateUnavailable;
            }
            //Remove the handlers to be ready for the next call.
            var applicationDeployment = sender as ApplicationDeployment;
            if (applicationDeployment != null)
                RemoveUpdateCheckHandlers(applicationDeployment);
        }

        #endregion
    }

    #region Events Delegates

    /// <summary>
    /// Delegate for handling the ApplicationUpdateProgressChanged event.
    /// </summary>
    public delegate void AppUpdateProgressChangedHandler(object sender, AppActionProgressChangedEventArgs e);

    /// <summary>
    /// Delegate for handling the ApplicationUpdateCompleted event.
    /// </summary>
    public delegate void AppUpdateCompletedHandler(object sender, AppUpdateCompletedEventArgs e);

    /// <summary>
    /// Delegate for handling the ApplCheckUpdateProgressChanged event.
    /// </summary>
    public delegate void AppUpdateCheckProgressChangedHandler(object sender, AppActionProgressChangedEventArgs e);

    /// <summary>
    /// Delegate for handling the AppCheckUpdateCompleted event.
    /// </summary>
    public delegate void AppUpdateCheckCompletedHandler(object sender, AppUpdateCheckCompletedEventArgs e);

    /// <summary>
    /// Delegate for handling the RestartRequrest event.
    /// </summary>
    public delegate void RestartRequrestHandler(object sender);

    #endregion
}
