using System;
using System.Windows.Forms;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.Update.Enums;
using Vital.Update.Managers;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private ApplicationUpdateManager _updateManager;

        #endregion

        #region Properties

        public Version NewVersion { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormUpdate()
        {
            InitializeComponent();
            _updateManager = new ApplicationUpdateManager();
        }

        #endregion

        #region Methods

        public void StartUpdate()
        {
            _updateManager.Update();
        }

        /// <summary>
        /// Update showReleaseNotes setting to show the Release notes dialog on start up the application after update.
        /// </summary>
        private void UpdateShowReleaseNotesSetting()
        {
            Properties.Settings.Default.ShowReleaseNotes = true;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles checking for updates before starting update process
        /// </summary>        
        private void CheckForUpdates()
        {
            try
            {
                var result = _updateManager.CheckForUpdate();
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(StaticKeys.UpdateErrorOccured, exception);
                Close();
            }
        }

        /// <summary>
        /// Handles the form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormUpdate_Load(object sender, EventArgs e)
        {
            simpleButtonCancel.Enabled = false;
            _updateManager.ApplicationUpdateCompleted += _updateManager_ApplicationUpdateCompleted;
            _updateManager.ApplicationUpdateProgressChanged += _updateManager_ApplicationUpdateProgressChanged;
            _updateManager.RestartRequrest += _updateManager_RestartRequrest;
            StartUpdate();
        }

        /// <summary>
        /// Handles the restart request event
        /// </summary>
        /// <param name="sender"></param>
        void _updateManager_RestartRequrest(object sender)
        {
            UpdateShowReleaseNotesSetting();
            UiHelperClass.ShowInformation(StaticKeys.ApplicationRestart);
            UibllInteraction.Instance.MainForm.ClosingByUpdate = true;
            Close();
        }

        /// <summary>
        /// Handles the application update progress change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _updateManager_ApplicationUpdateProgressChanged(object sender, Update.EventArgs.AppActionProgressChangedEventArgs e)
        {
            simpleButtonCancel.Enabled = true;

            if (e.ProgressPercentage == 100)
                simpleButtonCancel.Enabled = false;

            progressBarControlUpdate.EditValue = e.ProgressPercentage;
            var info = string.Empty;

            switch (e.State)
            {
                case ApplicationUpdateProgressState.DownloadingApplicationFiles:
                    info = "Downloading Application Files ...";
                    break;
                case ApplicationUpdateProgressState.DownloadingApplicationInformation:
                    info = "Downloading Application Information ...";
                    break;
                case ApplicationUpdateProgressState.DownloadingDeploymentInformation:
                    info = "Downloading Deployment Information ...";
                    break;
            }

            simpleLabelItemInfo.Text = info + " " + (e.BytesCompleted / 1048576f).ToString() + " / " + (e.BytesTotal / 1048576f).ToString() + " MB.";
        }

        /// <summary>
        /// Handles the completion of the update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _updateManager_ApplicationUpdateCompleted(object sender, Update.EventArgs.AppUpdateCompletedEventArgs e)
        {
            if (!e.IsSucceed && !e.Cancelled)
            {
                UiHelperClass.ShowError(StaticKeys.UpdateErrorOccured, e.Message);
                Close();
            }
        }

        /// <summary>
        /// Handles the cancel of the update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            _updateManager.CancelUpdate();
            Close();
        }        

        private void XtraFormUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateManager.ApplicationUpdateCompleted -= _updateManager_ApplicationUpdateCompleted;
            _updateManager.ApplicationUpdateProgressChanged -= _updateManager_ApplicationUpdateProgressChanged;
            _updateManager.RestartRequrest -= _updateManager_RestartRequrest;
        }

        #endregion
    }
}