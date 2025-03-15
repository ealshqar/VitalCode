using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using System.Windows.Forms;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlHwProfiles : DevExpress.XtraEditors.XtraUserControl
    {
        #region PrivateMemebers

        private SettingsManager _settingsManager;
        private readonly List<HwProfile> _deletedHwProfiles;
        private bool _isHwProfilesChanged;
        private bool _isEditable;

        #endregion

        #region EventsHandlersDelegates

        /// <summary>
        /// Delegate for On HwProfiles Change handle method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnHwProfilesChanged(object sender);

        #endregion

        #region EventsAndInvokers

        /// <summary>
        /// Event on hw profiles changed.
        /// </summary>
        public event OnHwProfilesChanged HwProfilesChanged;

        /// <summary>
        /// Invoke HwProfilesChanged event.
        /// </summary>
        protected virtual void InvokeHwProfilesChanged()
        {
            if (HwProfilesChanged != null) 
                HwProfilesChanged(this);
        }

        #endregion

        #region PublicProperties

        /// <summary>
        /// Collection of hw profiles.
        /// </summary>
        public BindingList<HwProfile> HwProfiles { get; set; }

        #endregion

        #region Constructors

        public XtraUserControlHwProfiles()
        {
            InitializeComponent();
            _deletedHwProfiles = new List<HwProfile>();
            PerformSpecificIntializationSteps();
        }

        #endregion
        
        #region Methods

        #region Initialization & Binding

        /// <summary>
        /// Perform the initialization of the Hw Profile user control.
        /// </summary>
        public void PerformSpecificIntializationSteps()
        {
            _settingsManager = new SettingsManager();
        }

        /// <summary>
        /// Does the binding of the fields in the issueK
        /// </summary>
        public void SetBinding()
        {
            try
            {
                ClearBinding();
                _isHwProfilesChanged = false;
                HwProfiles = ((BindingList<HwProfile>)CacheHelper.SetOrGetCachableData(CachableDataEnum.HwProfiles));
                gridControlHwProfiles.DataBindings.Clear();
                gridControlHwProfiles.DataSource = HwProfiles;
                gridControlHwProfiles.Refresh();
                gridControlHwProfiles.RefreshDataSource();
                HwProfiles.ListChanged += hwProfiles_ListChanged;

                dxErrorProviderHwProfiles.DataSource = HwProfiles;
                dxErrorProviderHwProfiles.ClearErrors();

                _isHwProfilesChanged = false;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

            }
           
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public void ClearBinding()
        {
            gridControlHwProfiles.DataBindings.Clear();
            gridControlHwProfiles.DataSource = null;
            CachingManager.RemoveFromCache(CachableDataEnum.HwProfiles.ToString());
            _deletedHwProfiles.Clear();
        }

        /// <summary>
        /// Set the edit mode of the user control
        /// </summary>
        /// <param name="isReadOnly">The is read only flag.</param>
        public void SetEditMode(bool isReadOnly)
        {
            _isEditable = !isReadOnly;
            layoutViewHwProfiles.OptionsBehavior.ReadOnly = isReadOnly;
            simpleButtonNewHwProfile.Enabled = _isEditable;
        }

        /// <summary>
        /// Check and perform Enabled changes for ToolStripMenuItemHwProfile items.
        /// </summary>
        private void CheckToolStripMenuItemHwProfileEnabled()
        {
            var focusedHwProfile = layoutViewHwProfiles.GetFocusedRow() as HwProfile;

            if (focusedHwProfile == null)
                return;

            var clickInCard = UiHelperClass.IsClickInRowByMouse(layoutViewHwProfiles) && _isEditable;
            toolStripMenuItemDeleteHwProfile.Enabled = clickInCard && !focusedHwProfile.IsSystemProfile && !focusedHwProfile.IsDefault;
            openToolStripMenuItemOpenHwProfile.Enabled = clickInCard;
            toolStripMenuItemNewHWProfile.Enabled = _isEditable;

            if (!clickInCard)
            {
                toolStripMenuItemUseAsDefaultHwProfile.Enabled = false;
                toolStripMenuItemUseAsDefaultHwProfile.Image = Resources.CheckboxNo;
            }
            else if (focusedHwProfile.IsDefault)
            {
                toolStripMenuItemUseAsDefaultHwProfile.Enabled = false;
                toolStripMenuItemUseAsDefaultHwProfile.Image = Resources.CheckboxYes;
            }
            else
            {
                toolStripMenuItemUseAsDefaultHwProfile.Enabled = true;
                toolStripMenuItemUseAsDefaultHwProfile.Image = Resources.CheckboxNo;
            }

        }

        #endregion

        #region Validation

        /// <summary>
        /// Calls a sequence to validate the tab and show or hide error icons in addition to 
        /// error summary
        /// </summary>
        /// <returns></returns>
        public bool ValidateUserControl()
        {
            return true;
        }

        #endregion

        #region DataActionsMethods 

        public bool Save()
        {
            try
            {
                if (!_isHwProfilesChanged)
                    return true;

                dxErrorProviderHwProfiles.UpdateBinding();

                if (!HwProfiles.All(s => s.IsValid))
                {
                    UiHelperClass.ShowError("Invalid Information", "Please validate all Hardware Profile information are entered correctly.");
                    return false;
                }

                UpdateHwProfilesWithDeletedRows(); 

                var result = _settingsManager.Save(HwProfiles);
                
                SetBinding();

                if(result.IsSucceed)
                    _deletedHwProfiles.Clear();

                CachingManager.RemoveFromCache(CachableDataEnum.HwProfiles.ToString());

                return result.IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }
        }

        public void Revert()
        {
            ClearBinding();
            SetBinding();
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Add new HW Profile row.
        /// </summary>
        private void AddNewHwProfile()
        {
            var defaultMinReadingStng = UiHelperClass.GetSetting(CachableDataEnum.HwProfileSettings, SettingKeys.MinimumReadingtoRegister);
            var defaultStabilityRangeStng = UiHelperClass.GetSetting(CachableDataEnum.HwProfileSettings, SettingKeys.ReadingStabilityRange);
            var defaultStabilityTimeoutStng = UiHelperClass.GetSetting(CachableDataEnum.HwProfileSettings, SettingKeys.ReadingStabilityTimeout);
            var defaultCsaDisconnectedTimeoutStng = UiHelperClass.GetSetting(CachableDataEnum.HwProfileSettings, SettingKeys.CsaDisconnectedTimeout);

            var newHWProfile = new HwProfile
            {
                MinReading = defaultMinReadingStng == null ? 0 : int.Parse(defaultMinReadingStng.Value.ToString()),
                StabilityRange = defaultStabilityRangeStng == null ? 0 : int.Parse(defaultStabilityRangeStng.Value.ToString()),
                StabilityTimeout = defaultStabilityTimeoutStng == null ? 0 : int.Parse(defaultStabilityTimeoutStng.Value.ToString()),
                DisconnectedTimeout = defaultCsaDisconnectedTimeoutStng == null ? 0 : int.Parse(defaultCsaDisconnectedTimeoutStng.Value.ToString()),
            };

            newHWProfile.SetDefaultsInitialValues();

            var frm = new XtraFormHwProfile {IsNewHwProfile = true, HwProfile =  newHWProfile };

            var result = frm.ShowDialog();

            if(result == DialogResult.Cancel)
                return;

            HwProfiles.Add(frm.HwProfile);

            if(frm.HwProfile.IsDefault)
                SetAsDefault(frm.HwProfile);

        }

        private void SetFocusedHwProfileAsDefault()
        {
            var focusedHwProfile = layoutViewHwProfiles.GetFocusedRow() as HwProfile;

            if (focusedHwProfile == null)
                return;

            if (focusedHwProfile.IsDefault)
            {
                layoutViewHwProfiles.CloseEditor();
                return;
            }
            
            SetAsDefault(focusedHwProfile);

            layoutViewHwProfiles.CloseEditor();
        }

        private void DeleteFocusedHwProfile()
        {
            var focusedHwProfile = layoutViewHwProfiles.GetFocusedRow() as HwProfile;

            if (focusedHwProfile == null || focusedHwProfile.IsSystemProfile)
                return;

            focusedHwProfile.ObjectState = DomainEntityState.Deleted;
            _deletedHwProfiles.Add(focusedHwProfile);

            layoutViewHwProfiles.DeleteRow(layoutViewHwProfiles.FocusedRowHandle);
        }

        private void OpenFocusedHwProfile()
        {
            if(!_isEditable)
                return;

            var focusedHwProfile = layoutViewHwProfiles.GetFocusedRow() as HwProfile;

            if (focusedHwProfile == null)
                return;

            var frm = new XtraFormHwProfile();
            frm.HwProfile = focusedHwProfile;
            frm.ShowDialog();

            if (frm.HwProfile.IsDefault && frm.HwProfile.IsChanged)
                SetAsDefault(frm.HwProfile);

        }

        private void UpdateHwProfilesWithDeletedRows()
        {
            foreach (var deletedHwProfile in _deletedHwProfiles)
            {
                HwProfiles.Add(deletedHwProfile);
            }
        }

        private void SetAsDefault(HwProfile hwProfile)
        {
            //Set all hw profile as not default.
            HwProfiles.ToList().ForEach(p => p.IsDefault = false);

            //Set the focused hw profile as default.
            hwProfile.IsDefault = true;
        }

        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Handles changing the Hw Profiles list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hwProfiles_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(hwProfiles_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                InvokeHwProfilesChanged();
                _isHwProfilesChanged = true;
            }
        }

        /// <summary>
        /// Handles CellValueChanging to update the Hw Profile status.
        /// </summary>
        private void layoutViewHwProfiles_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CellValueChangedEventHandler(layoutViewHwProfiles_CellValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var view = sender as LayoutView;

                if (view == null)
                    return;

                if (view.FocusedColumn == gridColumnIsDefault)
                    SetFocusedHwProfileAsDefault();
            }
        }

        /// <summary>
        /// Handles simpleButtonNewHwProfile click to open file dialog to select the hw profile image.
        /// </summary>
        private void simpleButtonNewHwProfile_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonNewHwProfile_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                AddNewHwProfile();
            }
        }

        /// <summary>
        /// Handles Load action to setup the user control.
        /// </summary>
        private void XtraUserControlHwProfilesLoad(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraUserControlHwProfilesLoad), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                    return;

                SetBinding();
            }
        }

        /// <summary>
        /// Handles Load action to setup the user control.
        /// </summary>
        private void layoutViewHwProfiles_DoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(layoutViewHwProfiles_DoubleClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var view = sender as LayoutView;

                if (view == null)
                    return;

                var pt = view.GridControl.PointToClient(MousePosition);

                var info = view.CalcHitInfo(pt);

                if (!info.InCard)
                    return;

                OpenFocusedHwProfile();
            }
        }

        /// <summary>
        /// Handles Opening for contextMenuStripHwProfiles to control its actions.
        /// </summary>
        private void contextMenuStripHwProfiles_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripHwProfiles_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == contextMenuStripHwProfiles)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(layoutViewHwProfiles);
                    CheckToolStripMenuItemHwProfileEnabled();
                }
            }
        }

        /// <summary>
        /// Handles ItemClicked for contextMenuStripHwProfiles to perform its actions.
        /// </summary>
        private void contextMenuStripHwProfiles_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStripHwProfiles_ItemClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender != null)
                {
                    ((ContextMenuStrip)sender).Hide();

                    if (sender == contextMenuStripHwProfiles)
                    {
                        if (e.ClickedItem == openToolStripMenuItemOpenHwProfile)
                        {
                            OpenFocusedHwProfile();
                        }
                        else if (e.ClickedItem == toolStripMenuItemNewHWProfile)
                        {
                            AddNewHwProfile();
                        }
                        else if (e.ClickedItem == toolStripMenuItemDeleteHwProfile)
                        {
                            DeleteFocusedHwProfile();
                        }
                        else if (e.ClickedItem == toolStripMenuItemUseAsDefaultHwProfile)
                        {
                            SetFocusedHwProfileAsDefault();
                        }
                    }
                }
            }
        }

        #endregion

    }
}
