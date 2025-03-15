using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Vital.Business.Managers;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.Update.Managers;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmBackupRestore : DevExpress.XtraEditors.XtraForm
    {
        #region Constants

        private const string RestoreDialogFilter = "Backup files (*.bak)|*.bak";

        #endregion

        #region Fields

        private SqlConfigManager _sqlConfigManager;
        private ApplicationUpdateManager _updateManager;
        private SettingsManager _settingsManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public frmBackupRestore()
        {
            InitializeComponent();
            _updateManager = new ApplicationUpdateManager();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Determine if the backup method is automatic or not
        /// </summary>
        private bool IsAutomatic
        {
            get
            {
                return radioGroupBackupMethod.EditValue !=  null && (bool)radioGroupBackupMethod.EditValue == true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public void PerformSpecificIntializationSteps()
        {
            UiHelperClass.ShowWaitingPanel("Loading ...");

            _sqlConfigManager = new SqlConfigManager();
            _settingsManager = new SettingsManager();

            SetBinding();
            
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Binds the fields
        /// </summary>
        public void SetBinding()
        {
            radioGroupBackupMethod.EditValue = UiHelperClass.GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutomaticBackup);
            checkEditAutoRemoveOldBackups.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutoRemoveOldBackups);
            checkEditShowBackupReminder.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.ShowBackupReminder);
            trackBarControlBackuPeriod.Value = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.BackupPeriod, CachableDataEnum.BackupAndRestore).ToString());
            trackBarControlRecentBackupsCount.Value = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.RecentBackupFilesCount, CachableDataEnum.BackupAndRestore).ToString());
            simpleLabelItemLastBackupDate.Text = DateTime.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.LastBackupDate, CachableDataEnum.BackupAndRestore).ToString()).ToShortDateString();
            buttonEditBackupDestination.Text = UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();
            buttonEditBackupSecondaryDestination.Text = UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultSecondaryBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();
            buttonEditRestore.Text = UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();
            hyperLinkEditBackupsFolder.EditValue = UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();
            hyperLinkEditBackupsSecondaryFolder.EditValue = UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultSecondaryBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();
        }

        /// <summary>
        /// Saves current backup/restore settings
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            var isSuccessful = true;

            UiHelperClass.ShowWaitingPanel("Saving ...");

            var secondaryBackupPath = buttonEditBackupSecondaryDestination.Text;

            if (!string.IsNullOrEmpty(secondaryBackupPath) && Directory.Exists(secondaryBackupPath))
            {
                UiHelperClass.SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.DefaultSecondaryBackupRestoreLocation, secondaryBackupPath, _settingsManager);
            }
            
            radioGroupBackupMethod.DoValidate();
            checkEditAutoRemoveOldBackups.DoValidate();
            checkEditShowBackupReminder.DoValidate();
            trackBarControlBackuPeriod.DoValidate();
            trackBarControlRecentBackupsCount.DoValidate();

            isSuccessful = UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.BackupAndRestore,
                                                      SettingKeys.AutomaticBackup,
                                                      (bool)radioGroupBackupMethod.EditValue,
                                                      _settingsManager);

            if (isSuccessful)
            {
                isSuccessful = UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutoRemoveOldBackups, checkEditAutoRemoveOldBackups.Checked, _settingsManager);

                if (isSuccessful)
                {
                    isSuccessful = UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.BackupAndRestore, SettingKeys.ShowBackupReminder, checkEditShowBackupReminder.Checked, _settingsManager);

                    if (isSuccessful)
                    {
                        isSuccessful = UiHelperClass.SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.BackupPeriod, trackBarControlBackuPeriod.Value, _settingsManager);

                        if (isSuccessful)
                        {
                            isSuccessful = UiHelperClass.SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.RecentBackupFilesCount, trackBarControlRecentBackupsCount.Value, _settingsManager);
                        }
                    }
                }
            }

            if (!isSuccessful)
            {
                UiHelperClass.ShowError("Backup/Restore Save Error","An error occurred while saving backup/restore settings, please check with the technical team.");
            }

            UiHelperClass.HideSplash();

            return isSuccessful;
        }

        #region Backup

        /// <summary>
        /// Sets the backup location
        /// </summary>
        private void SetBackupLocation()
        {
            var dialog = new FolderBrowserDialog();

            if (buttonEditBackupDestination.Text != string.Empty)
            {
                dialog.SelectedPath = buttonEditBackupDestination.Text;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.SelectedPath != string.Empty)
                {
                    buttonEditBackupDestination.Text = dialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Sets the backup Secondary location
        /// </summary>
        private void SetBackupSecondaryLocation()
        {
            var dialog = new FolderBrowserDialog();

            if (buttonEditBackupSecondaryDestination.Text != string.Empty)
            {
                dialog.SelectedPath = buttonEditBackupSecondaryDestination.Text;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.SelectedPath != string.Empty)
                {
                    while (dialog.SelectedPath != string.Empty && UiHelperClass.IsInvalidBackupPath(dialog.SelectedPath))
                    {
                        UiHelperClass.ShowInformation("Please select a local destination in your computer and not a removable drive or network location.");
                        if (dialog.ShowDialog() != DialogResult.OK)
                        {
                            dialog.SelectedPath = buttonEditBackupSecondaryDestination.Text;
                            break;
                        }
                    }

                    if (dialog.SelectedPath != string.Empty)
                    {
                        buttonEditBackupSecondaryDestination.Text = dialog.SelectedPath;
                    }
                }
            }
        }

        #endregion

        #region Restore

        /// <summary>
        /// Sets the restore location
        /// </summary>
        private void SetRestoreLocation()
        {
            var dialog = new OpenFileDialog { Filter = RestoreDialogFilter, FilterIndex = 2, RestoreDirectory = true };
            var path = buttonEditRestore.Text;

            if (path != string.Empty && Directory.Exists(path))
            {
                var fileAttributes = File.GetAttributes(path);

                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    dialog.InitialDirectory = path;
                }
                else
                {
                    var directoryFullPath = Path.GetDirectoryName(path);
                    dialog.InitialDirectory = Path.GetFileName(directoryFullPath);
                }
            }
      
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName != string.Empty)
                {
                    buttonEditRestore.Text = dialog.FileName;
                }
            }
        }

        #endregion

        #endregion

        #region Handlers

        #region Backup

        /// <summary>
        /// Sets the backup destination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditBackupDestination_Properties_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                SetBackupLocation();
            }
        }

        /// <summary>
        /// Sets the secondary backup location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditBackupSecondaryDestination_Properties_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                SetBackupSecondaryLocation();
            }
        }

        /// <summary>
        /// Performs the backup process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonBackup_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (UiHelperClass.Backup(buttonEditBackupDestination.Text, true, SettingKeys.DefaultBackupRestoreLocation).IsSucceed)
                {
                    SetBinding();
                }
                else if (UiHelperClass.Backup(buttonEditBackupSecondaryDestination.Text, true, SettingKeys.DefaultSecondaryBackupRestoreLocation).IsSucceed)
                {
                    SetBinding();
                }
            }
        }

        #endregion

        #region Restore

        /// <summary>
        /// Performs the restoring logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRestore_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (UiHelperClass.Restore(buttonEditRestore.Text).IsSucceed)
                {
                    SetBinding();
                }
            }
        }

        /// <summary>
        /// Handles the logic of restoring DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditRestore_Properties_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                SetRestoreLocation();
            }
        }

        #endregion

        #region Others

        /// <summary>
        /// Performs the loading logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            PerformSpecificIntializationSteps();
        }

        /// <summary>
        /// Handles form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBackupRestore_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Handles selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupBackupMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checkEditShowBackupReminder.Enabled = !IsAutomatic;
            //checkEditAutoRemoveOldBackups.Enabled = IsAutomatic;
            //trackBarControlBackuPeriod.Enabled = IsAutomatic;
            //trackBarControlRecentBackupsCount.Enabled = IsAutomatic && checkEditAutoRemoveOldBackups.Checked;
        }

        #endregion

        #endregion
    }
}