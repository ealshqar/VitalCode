using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraBars;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;
using Vital.UI.UI_Components.UI_Classes;
using Vital.UI.UI_Components.User_Controls.Modules;
using Vital.Business.Shared.DomainObjects.Services;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmSettings : VitalBaseForm
    {
        #region Fields

        private SettingsManager _settingsManager;
        private ServicesManager _servicesManager;
        private TestsManager _testManager;
        private AppImagesManager _appImagesManager;
        private bool _isListChanged;
        private ConnectionIndicatorStatusEnum _connectionIndicatorStatus;
        private const string DialogFilter = "Image files (*.jpg,*.png,*.gif,*.tiff,*.bmp,*.jpe,*.jpeg) |*.jpg;*.png;*.gif;*.tiff;*.bmp;*.jpe;*.jpeg";
        private BarButtonItem _barButtonItemResetDefaults;
        private XtraUserControlVitalRichEdit _xtraUserControlVitalRichEditClinicInfo;        
        private bool _rtfChanged;
        private bool _servicesChanged;
        private bool _hwProfilesChanged;
        private Lookup _serviceSystemType;
        readonly List<Service> _deletedServices;

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public frmSettings()
        {
            InitializeComponent();
            CustomeInitializeComponent();
            _deletedServices = new List<Service>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the current settings.
        /// </summary>
        public BindingList<Setting> SettingsList { get; private set; }

        /// <summary>
        /// Collection of services
        /// </summary>
        private BindingList<Service> ServicesList { get; set; }

        /// <summary>
        /// Gets the current focused service
        /// </summary>
        private Service FocusedService
        {
            get
            {
                return (gridViewServices.IsDataRow(gridViewServices.FocusedRowHandle))?
                        gridViewServices.GetFocusedRow() as Service: null;
            }
        }

        /// <summary>
        /// Returns true if the focused service is a system service
        /// </summary>
        private bool IsSystemService
        {
            get
            {
                return FocusedService != null && FocusedService.TypeLookup.Id == _serviceSystemType.Id;
            }
        }

        /// <summary>
        /// Collection of services that are in use
        /// </summary>
        public BindingList<Service> ServicesInUse { get; set; }

        #endregion

        #region Methods

        #region Initialization & Binding & Helpers

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public override sealed void PerformSpecificIntializationSteps()
        {
            _settingsManager = new SettingsManager();
            _servicesManager = new ServicesManager();
            _testManager = new TestsManager();
            _appImagesManager = new AppImagesManager();

            FormStatus = FormStatusEnum.Disabled;

            UiHelperClass.SetReflectionBaseViewProperties(gridViewSettings);
            FillLookUps();
        }

        /// <summary>
        /// Fill the lookup controls with the collections of objects from the cache
        /// </summary>
        public override void FillLookUps()
        {
            try
            {
                UiHelperClass.FillLookup(repositoryItemLookUpEditServiceType, 
                    UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ServiceType))
                    .Where(st=>st.Value != EnumNameResolver.Resolve(ServiceType.OnFlyService,true)));
                _serviceSystemType = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ServiceType,
                        ServiceType.SystemService, false, true));
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        public override sealed void CustomeInitializeComponent()
        {
            _barButtonItemResetDefaults = AddBarButtonItem("barButtonResetDefault", "Reset to Default", true,
                                                           Resources.reset, 6,
                                                           6, true,
                                                           new BarShortcut((Keys.Control | Keys.R)), true,
                                                           "Reset to Default",
                                                           "Click this button to reset the default settings.\r\n",
                                                           "You can use Ctrl+R to Reset default settings.", true);
        }

        /// <summary>
        /// Set the connection indicators based on passed state.
        /// </summary>
        /// <param name="connectionState"></param>
        private void SetConnectionIndicators(ConnectionIndicatorStatusEnum connectionState)
        {
            _connectionIndicatorStatus = connectionState;

            switch (connectionState)
            {
                case ConnectionIndicatorStatusEnum.Disconnected:
                    stateIndicatorComponentConnectionState.SetState(IndicatorComponentStatus.Red);
                    layoutControlItemSearching.Visibility = LayoutVisibility.Never;
                    labelControlConnectionInfo.Text = CsaEmdUnitManager.Instance.AutoComPortDetection
                                                          ? StaticKeys.AutoDetectedDisconnected
                                                          : string.Format(StaticKeys.ManualDetectedDisconnected,
                                                                          CsaEmdUnitManager.Instance.ComPortNumber);
                    layoutControlItemConnectionInfo.Visibility = LayoutVisibility.Always;
                    layoutControlGroupRefresh.Visibility = CsaEmdUnitManager.Instance.AutoComPortDetection ? LayoutVisibility.Always : LayoutVisibility.Never;
                    pictureEditSerialPort.Enabled = false;
                    break;
                case ConnectionIndicatorStatusEnum.Connected:
                    stateIndicatorComponentConnectionState.SetState(IndicatorComponentStatus.Green);
                    layoutControlItemSearching.Visibility = LayoutVisibility.Never;
                    labelControlConnectionInfo.Text = string.Format(CsaEmdUnitManager.Instance.AutoComPortDetection
                                                                        ? StaticKeys.AutoDetectedConnected
                                                                        : StaticKeys.ManualDetectedConnected,
                                                                    CsaEmdUnitManager.Instance.ComPortNumber);
                    layoutControlItemConnectionInfo.Visibility = LayoutVisibility.Always;
                    layoutControlGroupRefresh.Visibility = LayoutVisibility.Never;
                    pictureEditSerialPort.Enabled = true;
                    break;
                case ConnectionIndicatorStatusEnum.Searching:
                    case ConnectionIndicatorStatusEnum.SearchingPort:
                    stateIndicatorComponentConnectionState.SetState(IndicatorComponentStatus.Ornage);
                    if (connectionState == ConnectionIndicatorStatusEnum.SearchingPort)
                    {
                        progressPanelRefreshConnection.Description = string.Format(StaticKeys.SearchingPortDescription, CsaEmdUnitManager.Instance.ComPortNumber);
                        progressPanelRefreshConnection.ShowDescription = true;
                    }
                    else
                    {
                        progressPanelRefreshConnection.ShowDescription = false;
                    }
                    layoutControlItemSearching.Visibility = LayoutVisibility.Always;
                    layoutControlItemConnectionInfo.Visibility = LayoutVisibility.Never;
                    layoutControlGroupRefresh.Visibility = LayoutVisibility.Never;
                    pictureEditSerialPort.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            SettingsList.RaiseListChangedEvents = true;
            SettingsList.ListChanged += SettingsList_ListChanged;
        }
        
        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            if(SettingsList != null)
                SettingsList.ListChanged -= SettingsList_ListChanged;

            if (ServicesList != null)
                ServicesList.ListChanged -= ServicesList_ListChanged;
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            BindSettings();
            BindReportsSettings();
            BindServices();
            SetConnectionIndicators(ConnectionIndicatorStatusEnum.Searching);

            CsaEmdUnitManager.Instance.RefreshPortNumberSettings();

            _isListChanged = false;
            _servicesChanged = false;
            _hwProfilesChanged = false;
        }

        /// <summary>
        /// Binds the settings grid.
        /// </summary>
        private void BindSettings()
        {
            SettingsList = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings));
            gridControlSettings.DataBindings.Clear();
            gridControlSettings.DataSource = SettingsList;
            UiHelperClass.BindControl(gridControlSettings, this, () => SettingsList);
        }

        /// <summary>
        /// Binds the services
        /// </summary>
        private void BindServices()
        {
            _servicesChanged = false;
            ServicesList = ((BindingList<Service>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Services));            
            gridControlServices.DataBindings.Clear();
            gridControlServices.DataSource = ServicesList;
            gridControlServices.Refresh();
            gridControlServices.RefreshDataSource();
            ServicesList.ListChanged += ServicesList_ListChanged;

            dxErrorProviderServices.DataSource = ServicesList;
            dxErrorProviderServices.ClearErrors();
        }

        /// <summary>
        /// Handles changing the services list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServicesList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(SettingsList_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                FormStatus = FormStatusEnum.Modified;

                _servicesChanged = true;
            }
        }

        /// <summary>
        /// Binds the reports settings.
        /// </summary>
        private void BindReportsSettings()
        {
            //report logo.
            pictureEditLogo.Image = (Image)CacheHelper.SetOrGetCachableData(CachableDataEnum.Logo);
            if (_xtraUserControlVitalRichEditClinicInfo != null)
            {
                BindClinicInfoControl();
            }
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            gridControlSettings.DataBindings.Clear();
            gridControlSettings.DataSource = null;
            ClearSettingsInCache();

            gridControlServices.DataBindings.Clear();
            gridControlServices.DataSource = null;
            CachingManager.RemoveFromCache(CachableDataEnum.Services.ToString());
            _deletedServices.Clear();
        }

        /// <summary>
        /// Clears settings in cache
        /// </summary>
        private void ClearSettingsInCache()
        {
            CachingManager.RemoveFromCache(CachableDataEnum.Settings.ToString());
            CachingManager.RemoveFromCache(CachableDataEnum.VisibleSettings.ToString());
            CachingManager.RemoveFromCache(CachableDataEnum.ShippingOrderSettings.ToString());
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public override void SetEditMode(bool isReadOnly) 
        {
            gridViewSettings.OptionsBehavior.ReadOnly = isReadOnly;
            gridViewServices.OptionsBehavior.ReadOnly = isReadOnly;
            _barButtonItemResetDefaults.Enabled = !isReadOnly;
            simpleButtonUseLogo.Enabled = !isReadOnly;
            //xtraUserControlHwProfiles.SetEditMode(isReadOnly);

            ClinicInfoEditMode(isReadOnly);
        }

        /// <summary>
        /// Customize for a new Items of the bar manager, or to do extra items;
        /// </summary>
        /// <param name="itemName">The Clicked item tag.</param>
        public override void CustomeBarManagerClickHandling(string itemName)
        {
            if (string.IsNullOrEmpty(itemName)) return;

            if (itemName.Equals("barButtonResetDefault"))
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ResetToDefultConfirmMessage) == DialogResult.Yes)
                    ResetDefaultSettings();
            }
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public override void AfterLoadAction()
        {
            OpenCsaConnection();
        }        

        #endregion        

        #region Save related actions

        /// <summary>
        /// Uses the Tests manager to save the test        
        /// </summary>
        public override bool Save(bool isColsing)
        {
            try
            {
                GridViewPostValues(gridViewSettings);
                GridViewPostValues(gridViewServices);

                foreach (var service in ServicesList)
                {
                    service.Validate();
                }

                ClinicInfoPostValues();
                dxErrorProviderServices.UpdateBinding();
                
                //Update service keys and validate services
                if (_servicesChanged)
                {
                    foreach (var service in ServicesList.Where(service => service.TypeLookup.Id != _serviceSystemType.Id))
                    {
                        service.Key = service.Name == null ? service.Key : service.Name.Replace(" ", "");
                    }    
                }
                
                if (!ServicesList.All(s => s.IsValid))
                {
                    UiHelperClass.ShowError("Invalid Information","Please validate all service information are entered correctly.");
                    return false;
                }
                
                UpdateServiceListWithDeletedRows();

                var result = _settingsManager.Save(SettingsList);
                
                if (_rtfChanged)
                {
                    var settingsList = CacheHelper.SetOrGetCachableData(CachableDataEnum.PrintingSettings) as BindingList<Setting>;

                    var setting = settingsList == null ? null : settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(SettingKeys.ClinicInfo)));
                    if (setting != null)
                    {
                        setting.Value = _xtraUserControlVitalRichEditClinicInfo.NotesRtf;
                        _settingsManager.Save(setting);
                    }
                    CachingManager.RemoveFromCache(CachableDataEnum.PrintingSettings.ToString());
                    _rtfChanged = false;
                }

                if (_servicesChanged)
                {
                    _servicesManager.Save(ServicesList);
                    BindServices();
                }

                //result.IsSucceed = xtraUserControlHwProfiles.Save();

                CachingManager.RemoveFromCache(CachableDataEnum.VisibleSettings.ToString());
                CachingManager.RemoveFromCache(CachableDataEnum.SingleValues.ToString());
                CachingManager.RemoveFromCache(CachableDataEnum.Services.ToString());
                //CachingManager.RemoveFromCache(CachableDataEnum.HwProfiles.ToString());

                if (result.IsSucceed)
                {
                    _deletedServices.Clear();
                }

                return result.IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty,exception);

                return false;
            }
        }

        /// <summary>
        /// Actions After save.
        /// </summary>
        public override void AfterSaveAction()
        {
            //This calling for refresh the HW setting to get it ready in the next use.
            CsaEmdUnitManager.Instance.RefreshSettings();
            CsaEmdUnitManager.Instance.RefreshConnection();
            //AutoCsaEmdUnitManager.Instance.RefreshSettings();
            //AutoCsaEmdUnitManager.Instance.RefreshCSAConnection();
            AutoCsaEmdUnitManagerPhase2.Instance.RefreshSettings();
            AutoCsaEmdUnitManagerPhase2.Instance.RefreshCSAConnection();

            _isListChanged = false;
            _servicesChanged = false;
            _hwProfilesChanged = false;
        }

        /// <summary>
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        public override bool HasChanges()
        {
            return _isListChanged || _servicesChanged || _hwProfilesChanged;
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            ClearHandlers();
            ClearBinding();

            SetProperties();
            //xtraUserControlHwProfiles.Revert();
            SetBinding();
            SetupMainErrorProvider();
            UpdateErrorProvider();
            SetupHandllers();

            RedetectHwChanges();            
            return true;
        } 
      
        /// <summary>
        /// Reset the default settings.
        /// </summary>
        /// <returns></returns>
        public void ResetDefaultSettings()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.ResutToDefault);

            gridViewSettings.CancelUpdateCurrentRow();
            gridViewSettings.CloseEditor();

            foreach (var setting in SettingsList)
            {
                //Execlude Tech info from restore default settings logic
                if (!setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianName)) &&
                    !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianAddress)) &&
                    !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianCity)) &&
                    !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianEmail)) &&
                    !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianPhone)) &&
                    !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianState)) &&
                    !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.TechnicianZip)))
                {
                    setting.Value = setting.DefaultValue;
                }
            }

            //Disabled the code to reset the services to their defaults since it might be better to keep them as they are
            //var testingFeeService =
            //    ServicesList.FirstOrDefault(
            //        s => s.Key == ServiceKeys.DefaultTestingFee.ToString() && s.TypeLookup.Id == _serviceSystemType.Id);

            //if (testingFeeService != null)
            //{
            //    testingFeeService.Name = testingFeeService.DefaultName;
            //    testingFeeService.Description = testingFeeService.DefaultDescription;
            //    testingFeeService.Comments = testingFeeService.DefaultComments;
            //    testingFeeService.IsDefault = testingFeeService.DefaultIsDefault;
            //    testingFeeService.Price = testingFeeService.DefaultPrice;
            //}

            //var evaluationFeeService =
            //    ServicesList.FirstOrDefault(
            //        s => s.Key == ServiceKeys.DefaultRevalTestingFee.ToString() && s.TypeLookup.Id == _serviceSystemType.Id);

            //if (evaluationFeeService != null)
            //{
            //    evaluationFeeService.Name = evaluationFeeService.DefaultName;
            //    evaluationFeeService.Description = evaluationFeeService.DefaultDescription;
            //    evaluationFeeService.Comments = evaluationFeeService.DefaultComments;
            //    evaluationFeeService.IsDefault = evaluationFeeService.DefaultIsDefault;
            //    evaluationFeeService.Price = evaluationFeeService.DefaultPrice;
            //}

            //gridViewServices.RefreshData();
            gridViewSettings.RefreshData();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Updates form status and sets flag
        /// </summary>
        private void NotifyServiceChanged()
        {
            //This line makes sure the Save buttons becomes enabled instantly when user edits a cell since devexpress
            //grid by default posts values to the underlying datasoure when leaving cell and changing focus.
            if (FormStatus != FormStatusEnum.Modified && gridViewServices.FocusedRowHandle >= 0)
            {
                FormStatus = FormStatusEnum.Modified;
                _servicesChanged = true;
            }
        }

        #endregion

        #region Hardware Logic

        /// <summary>
        /// Open the Connection with the HW.
        /// </summary>
        private void OpenCsaConnection()
        {
            ResetHardwareHandlers();
            CsaEmdUnitManager.Instance.OpenConnection();
        }

        /// <summary>
        /// Reset the HW handlers.
        /// </summary>
        private void ResetHardwareHandlers()
        {
            RemoveHardwareHandlers();
            AddHardwareHandlers();
        }

        /// <summary>
        /// Add Handlers for HW connection.
        /// </summary>
        private void AddHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.Connected += Instance_Connected;

            CsaEmdUnitManager.Instance.Disconnected += Instance_Disconnected;

            CsaEmdUnitManager.Instance.Detecting += Instance_Detecting;
        }

        /// <summary>
        /// Remove Handlers from HW connection.
        /// </summary>
        private void RemoveHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.Connected -= Instance_Connected;

            CsaEmdUnitManager.Instance.Disconnected -= Instance_Disconnected;

            CsaEmdUnitManager.Instance.Detecting -= Instance_Detecting;
        }

        /// <summary>
        /// close the Connection with the HW.
        /// </summary>
        private void CloseCsaConnection()
        {
            try
            {
                RemoveHardwareHandlers();
                CsaEmdUnitManager.Instance.CloseConnection();
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Redetect hardware changes.
        /// </summary>
        private void RedetectHwChanges()
        {
            try
            {
                SetConnectionIndicators(ConnectionIndicatorStatusEnum.Searching);
                CsaEmdUnitManager.Instance.RefreshConnection();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Com Port changed actions.
        /// </summary>
        /// <param name="portNumber"></param>
        private void ComPortChangedActions(int portNumber)
        {
            SetConnectionIndicators(ConnectionIndicatorStatusEnum.Searching);

            CsaEmdUnitManager.Instance.RefreshConnection(new SerailPortNumberFilter
            {
                ComPortNumber = portNumber,
                AutoComPortDetection = portNumber == 0
            });

            gridViewSettings.PostEditor();
            gridViewSettings.CloseEditor();
        }

        #endregion

        #region Clinic Info

        /// <summary>
        /// Performs any specific actions needed for the tab before the saving
        /// </summary>
        public void ClinicInfoPostValues()
        {
            if (_xtraUserControlVitalRichEditClinicInfo != null)
                _xtraUserControlVitalRichEditClinicInfo.PostValue();
        }

        /// <summary>
        /// Performs custom set edit mode action
        /// </summary>
        /// <param name="isReadOnly"></param>
        public void ClinicInfoEditMode(bool isReadOnly)
        {
            if (_xtraUserControlVitalRichEditClinicInfo != null)
                _xtraUserControlVitalRichEditClinicInfo.ReadOnly = isReadOnly;
        }

        /// <summary>
        /// Handles creation of the clinic info rich edit control
        /// </summary>
        private void InitializeClinicInfoControl()
        {
            if (_xtraUserControlVitalRichEditClinicInfo == null)
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingClinicInfo);

                _xtraUserControlVitalRichEditClinicInfo = new XtraUserControlVitalRichEdit()
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = !IsInEditMode
                };
                layoutControl2.Controls.Add(_xtraUserControlVitalRichEditClinicInfo);

                var layoutControlItemClinicInfo = new LayoutControlItem();

                ((System.ComponentModel.ISupportInitialize)(layoutControlItemClinicInfo)).BeginInit();

                layoutControlItemClinicInfo.Control = _xtraUserControlVitalRichEditClinicInfo;
                layoutControlItemClinicInfo.CustomizationFormText = "Clinic Info";
                layoutControlItemClinicInfo.Location = new System.Drawing.Point(0, 0);
                layoutControlItemClinicInfo.Name = "layoutControlItemClinicInfo";
                layoutControlItemClinicInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                layoutControlItemClinicInfo.TextVisible = false;
                layoutControlItemClinicInfo.Size = new System.Drawing.Size(673, 288);
                layoutControlItemClinicInfo.Text = "Clinic Info";
                layoutControlItemClinicInfo.TextSize = new System.Drawing.Size(93, 13);

                layoutControlGroupClinicInfo.Items.AddRange(new BaseLayoutItem[] { layoutControlItemClinicInfo });

                ((System.ComponentModel.ISupportInitialize)(layoutControlItemClinicInfo)).EndInit();

                _xtraUserControlVitalRichEditClinicInfo.Size = new System.Drawing.Size(577, 288);

                BindClinicInfoControl();

                ClinicInfoEditMode(!IsInEditMode);

                UiHelperClass.HideSplash();
            }
            else
            {
                BindClinicInfoControl();
            }
        }

        /// <summary>
        /// Sets the value of the rich edit control
        /// </summary>
        private void BindClinicInfoControl()
        {
            if (_xtraUserControlVitalRichEditClinicInfo != null)
            {
                _rtfChanged = false;
                var settingsList = CacheHelper.SetOrGetCachableData(CachableDataEnum.PrintingSettings) as BindingList<Setting>;

                var setting = settingsList == null ? null : settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(SettingKeys.ClinicInfo)));

                _xtraUserControlVitalRichEditClinicInfo.Control.ModifiedChanged -= richEditControlNotes_ModifiedChanged;

                _xtraUserControlVitalRichEditClinicInfo.Control.Modified = false;

                _xtraUserControlVitalRichEditClinicInfo.Control.RtfText = setting == null ? null : setting.Value.ToString();

                _xtraUserControlVitalRichEditClinicInfo.Control.ModifiedChanged += richEditControlNotes_ModifiedChanged;
            }
        }

        #endregion

        #region Service

        /// <summary>
        /// Appends the marked as deleted object to the service list.
        /// </summary>
        private void UpdateServiceListWithDeletedRows()
        {
            foreach (var history in _deletedServices)
            {
                ServicesList.Add(history);
            }
        }
        
        /// <summary>
        /// Delete patient Service
        /// </summary>
        private void DeleteService()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected service will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var focusedRow = gridViewServices.GetFocusedRow() as Service;

                    if (focusedRow != null && focusedRow.Id > 0)
                    {
                        if (FocusedService != null && 
                            (_testManager.GetTestServices(new TestServicesFilter() 
                            { ServiceId = FocusedService.Id }).Count > 0) ||
                            (ServicesInUse != null && ServicesInUse.FirstOrDefault(s => s.Id == FocusedService.Id) != null))
                        {
                            UiHelperClass.ShowError("Service In Use","Selected service can't be deleted since it has been used in tests.");
                            return;
                        }

                        //mark the object as deleted.
                        focusedRow.ObjectState = DomainEntityState.Deleted;
                        //add the deleted objects to a temporary list.
                        _deletedServices.Add(focusedRow);
                    }

                    //delete the row 
                    gridViewServices.DeleteRow(gridViewServices.FocusedRowHandle);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #region HwProfiles

        /// <summary>
        /// Handles the hw profiles user control changes.
        /// </summary>
        /// <param name="sender"></param>
        private void xtraUserControlHwProfiles_HwProfilesChanged(object sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlHwProfiles.OnHwProfilesChanged(xtraUserControlHwProfiles_HwProfilesChanged), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //_hwProfilesChanged = true;
                //FormStatus = FormStatusEnum.Modified;
            }
        }

        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the event of changing the list of settings.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SettingsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(SettingsList_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                FormStatus = FormStatusEnum.Modified;

                _isListChanged = true;
            }
        }

        /// <summary>
        /// Handles the item changing of a cell value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSettingsCellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CellValueChangedEventHandler(gridViewSettingsCellValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //This line makes sure the Save buttons becomes enabled instantly when user edits a cell since devexpress
                //grid by default posts values to the underlying datasoure when leaving cell and changing focus.
                if (FormStatus != FormStatusEnum.Modified && gridViewSettings.FocusedRowHandle >= 0)
                {
                    FormStatus = FormStatusEnum.Modified;
                    _isListChanged = true;
                }


                var setting = gridViewSettings.GetRow(e.RowHandle) as Setting;

                if (setting == null || !setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.CommunicationsPort))) return;

                ComPortChangedActions((int)e.Value);

                setting.Value = e.Value;
            }
        }

        /// <summary>
        /// Handel the form closing event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(frmSettings_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                e.Cancel = !CanClose();

                if (e.Cancel)
                    return;

                CloseCsaConnection();
                ClearSettingsInCache();
                CsaEmdUnitManager.Instance.RefreshPortNumberSettings();
            }
        }

        /// <summary>
        /// Handel clicking on RefreshConnection button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRefreshConnection_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonRefreshConnection_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                RedetectHwChanges();
            }
        }

        /// <summary>
        /// Handel the showing for the editor to prevent com change while searching.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewSettings_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(gridViewSettings_ShowingEditor), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var setting = gridViewSettings.GetFocusedRow() as Setting;

                if (setting == null)
                    return;

                if (setting.Key.Equals(EnumNameResolver.Resolve(SettingKeys.CommunicationsPort)) && (_connectionIndicatorStatus == ConnectionIndicatorStatusEnum.Searching || _connectionIndicatorStatus == ConnectionIndicatorStatusEnum.SearchingPort))
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Handel the show event of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSettings_Shown(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(frmSettings_Shown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //Hide the splash screen. have a look on frmSettings_Load event handler method.
                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Handel on load event of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSettings_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(frmSettings_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //We show the splash screen here until the Reflection finished setting of the setting value in the grid.
                UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingSettings);
            }
        }

        /// <summary>
        /// Handles the click on the logo button edit .
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonEditLogo_Click(object sender, EventArgs e)
        {

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(buttonEditLogo_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (IsInEditMode)
                {
                    var dialog = new OpenFileDialog { Filter = DialogFilter, FilterIndex = 2, RestoreDirectory = true };

                    if (buttonEditLogo.Text != string.Empty)
                    {
                        var directoryFullPath = Path.GetDirectoryName(buttonEditLogo.Text);
                        dialog.InitialDirectory = Path.GetFileName(directoryFullPath);
                    }

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.FileName != string.Empty)
                        {
                            buttonEditLogo.Text = dialog.FileName;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the click on use logo button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonUseLogo_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonUseLogo_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var choosenFilePath = buttonEditLogo.Text;

                if (choosenFilePath != string.Empty)
                {
                    if (File.Exists(choosenFilePath))
                    {
                        if (Path.GetExtension(choosenFilePath).IsInImageFormats())
                        {
                            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingImage);

                            try
                            {
                                var appImage = _appImagesManager.GetAppImageByProperty(new AppImageFilter
                                                                                           {
                                                                                               Property = StaticKeys.AppImagesLogo
                                                                                           });

                                appImage.Value = VitalHelper.ToBytesArray(Image.FromFile(choosenFilePath));

                                if (_appImagesManager.SaveAppImage(appImage).IsSucceed)
                                {
                                    pictureEditLogo.Image = Image.FromFile(choosenFilePath);
                                }
                                else
                                {
                                    UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.ErrorMessageCouldNotUseLogo);
                                }
                                CachingManager.RemoveFromCache(CachableDataEnum.Logo.ToString());
                            }
                            catch (Exception exception)
                            {
                                UiHelperClass.HideSplash();
                                UiHelperClass.ShowError(StaticKeys.DatabaseBackupErrorOccured, exception);
                            }
                            finally
                            {
                                UiHelperClass.HideSplash();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handel the first change on the richEditControlNote text (Rtf Text ..).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richEditControlNotes_ModifiedChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(richEditControlNotes_ModifiedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                FormStatus = FormStatusEnum.Modified;
                _rtfChanged = true;
            }
        }

        /// <summary>
        /// Handles changing the selected tab in the report settings area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabbedControlGroupReportInfo_SelectedPageChanging(object sender, DevExpress.XtraLayout.LayoutTabPageChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new LayoutTabPageChangingEventHandler(tabbedControlGroupReportInfo_SelectedPageChanging), sender, e);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Page == layoutControlGroupClinicInfo)
                {
                    InitializeClinicInfoControl();
                }
            }
        }

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(ContextMenuStripOpening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == contextMenuStripServices)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewServices);
                    toolStripMenuItemDeleteService.Enabled = 
                        UiHelperClass.IsClickInRowByMouse(gridViewServices) && IsInEditMode && !IsSystemService;
                }
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(ContextMenuStripItemClicked), sender, e);
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

                    if (sender == contextMenuStripServices)
                    {
                        if (e.ClickedItem == toolStripMenuItemDeleteService)
                        {
                            DeleteService();
                        }
                    }
                }
            }
        }

        #region Service
        /// <summary>
        /// Handles the i
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewServiceInitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new InitNewRowEventHandler(GridViewServiceInitNewRow), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var newServiceRow = gridViewServices.GetFocusedRow() as Service;
                var userLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ServiceType,
                        ServiceType.UserService, false, true));
                
                if (newServiceRow != null)
                {
                    newServiceRow.TypeLookup = new Lookup() { Id = userLookup.Id };
                    newServiceRow.Key = EnumNameResolver.Resolve(ServiceType.UserService, true);
                }
            }
        }

        /// <summary>
        /// Handles cell value changes in services grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewServices_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CellValueChangedEventHandler(gridViewSettingsCellValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                NotifyServiceChanged();
            }
        }

        /// <summary>
        /// Handles value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckEditServiceIsDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(repositoryItemCheckEditServiceIsDefault_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                NotifyServiceChanged();
            }
        }

        /// <summary>
        /// Handles value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemSpinEditServicePrice_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ChangingEventHandler(repositoryItemSpinEditServicePrice_EditValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                NotifyServiceChanged();
            }
        }      

        #endregion

        #endregion

        #region HW_Handlers

        /// <summary>
        /// Handel disconnected event.
        /// </summary>
        private void Instance_Disconnected(object sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnDisconnected(Instance_Disconnected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (IsDisposed) return;
                SetConnectionIndicators(ConnectionIndicatorStatusEnum.Disconnected);
            }
        }

        /// <summary>
        /// Handel connected event.
        /// </summary>
        private void Instance_Connected(object sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnConnected(Instance_Connected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }

            }
            else
            {
                if (IsDisposed) return;
                SetConnectionIndicators(ConnectionIndicatorStatusEnum.Connected);
            }
        }

        /// <summary>
        /// Handel searching event.
        /// </summary>
        private void Instance_Detecting(object sender, int comPortNumber)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnDetecting(Instance_Detecting), sender, comPortNumber);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }

            }
            else
            {
                if (IsDisposed) return;
                SetConnectionIndicators(ConnectionIndicatorStatusEnum.SearchingPort);
            }
        }

        #endregion                         
    }
}
