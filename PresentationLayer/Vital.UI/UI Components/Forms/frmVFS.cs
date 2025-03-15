using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;
using Vital.UI.UI_Components.Forms.BaseForms;
using Vital.UI.UI_Components.Reports;
using Vital.UI.UI_Components.UI_Classes;
using Vital.UI.UI_Components.User_Controls.Modules;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmVFS : VitalBaseForm
    {
        #region Fields

        private VitalForceSheetManager _vfsManager;
        private LookupsManager _lookupsManager;
        private SettingsManager _settingsManager;
        private TestsManager _testsManager;

        private Test _test;

        private bool _isCsaUnitConnected;
        private object _connectionLooker;
        private int _broadcastingStage;

        private BindingList<Lookup> _groupLookups; 

        private int _leftLookupId;
        private int _rightLookupId;
        private bool _isEditable;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the VFS object
        /// </summary>
        public VFS VFSObject { get; set; }

        /// <summary>
        /// Indicate the Broadcasting stage image.
        /// </summary>
        private int BroadcastingStage
        {
            get { return _broadcastingStage; }
            set
            {
                if (_broadcastingStage == value) return;

                _broadcastingStage = value > 3 ? 1 : value;

                UpdateBroadcastingStageImage(_broadcastingStage);
            }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vfs"></param>
        public frmVFS(VFS vfs)
        {
            InitializeComponent();
            VFSObject = vfs;
            CustomeInitializeComponent();
        }

        #endregion

        #region Methods

        #region Initialize And Binding

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        public override sealed void CustomeInitializeComponent()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingCustomComponents);

            var startingIndex = 6;

            AddBarButtonItem("barButtonItemPrint", "Print", true, Resources.Print, startingIndex,
                             startingIndex, true,
                             new BarShortcut((Keys.Alt | Keys.P)), true,
                             "Print",
                             "Click this button to print current test.\r\n",
                             "You can use Alt+P to print current test.", true);

            startingIndex += 1;

            AddBarButtonItem("barButtonItemPrintPreview", "Print Preview", true, Resources.Test_Tab, startingIndex,
                             startingIndex, false,
                             new BarShortcut((Keys.Alt | Keys.Control | Keys.P)), true,
                             "Print Preview",
                             "Click this button to preview current test report.\r\n",
                             "You can use Alt+P to preview current test report.", true);

            startingIndex += 1;

            AddBarButtonItem("barButtonItemSettings", "Settings", true, Resources.SettingsSmall, startingIndex,
                             startingIndex, true,
                             new BarShortcut((Keys.Alt | Keys.S)), true,
                             "Settings",
                             "Click this button to access the application settings.\r\n",
                             "You can use Alt+S to access the application settings.", true);

            startingIndex += 1;

            AddBarButtonItem("barButtonItemHelp", "Help", true, Resources.HelpSmall, startingIndex,
                             startingIndex, true,
                             null, true,
                             "Help",
                             "Click this button to access the application help.\r\n",
                             string.Empty, true);
            startingIndex += 1;
            AddBarButtonItem("barButtonItemHotKeys", "Shortcuts", true, Resources.keyIcon, startingIndex,
                             startingIndex, false,
                             null, true,
                             "Shortcuts",
                             "Click this button to access the application shortcuts.\r\n",
                             string.Empty, true);
            startingIndex += 1;
            AddBarButtonItem("barButtonItemFeedback", "Feedback", true, Resources.feedback_small, startingIndex,
                             startingIndex, true,
                             null, true,
                             "Feedback",
                             "Report an issue or make a suggestion.\r\n",
                             string.Empty, true);

            //Add Images and Combobox Items for handling four factor images, we are doing it like this because the ImageList control
            //doesn't allow adding images from Project Resources and we don't want to store images the in the resource file of the form itself
            //Using the code below, we are adding images from project resources and then creating combobox items based on them.
            imageListFourFactors.Images.Add("CircleBalanced", Resources.CircleBalanced);
            imageListFourFactors.Images.Add("CircleUnbalanced", Resources.CircleUnbalanced);
            imageListFourFactors.Images.Add("CircleClear", Resources.CircleClear);
            
            repositoryItemImageComboBoxFourFactors.Items.AddRange(new ImageComboBoxItem[] {
            new ImageComboBoxItem("Balanced", FourFactorState.Balanced, 0),
            new ImageComboBoxItem("Unbalanced", FourFactorState.UnBalanced, 1),
            new ImageComboBoxItem("Clear", FourFactorState.Clear, 2)});

            xtraUserControlVFSItemsMain.CustomeInitializeComponent();
            xtraUserControlPrimaryIssue.CustomeInitializeComponent();
            xtraUserControlSecondaryIssues.CustomeInitializeComponent();
            xtraUserControlThyroidIssues.CustomeInitializeComponent();
            xtraUserControlMercuryIssues.CustomeInitializeComponent();
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public override void SetEditMode(bool isReadOnly)
        {
            _isEditable = !isReadOnly;

            textEditClientName.Properties.ReadOnly = true;
            textEditDate.Properties.ReadOnly = true;
            gridViewMajorIssues.OptionsBehavior.ReadOnly = true;
            gridViewMajorIssues.OptionsBehavior.Editable = false;

            textEditName.Properties.ReadOnly = isReadOnly;
            memoEditNotes.Properties.ReadOnly = isReadOnly;
            memoEditEmotionalIssues.Properties.ReadOnly = isReadOnly;
            spinEditMercuryNumberOfIssues.Properties.ReadOnly = isReadOnly;
            spinEditThyroidNumberOfIssues.Properties.ReadOnly = isReadOnly;

            xtraUserControlVFSItemsMain.SetEditMode(isReadOnly);
            xtraUserControlPrimaryIssue.SetEditMode(isReadOnly);
            xtraUserControlSecondaryIssues.SetEditMode(isReadOnly);
            xtraUserControlThyroidIssues.SetEditMode(isReadOnly);
            xtraUserControlMercuryIssues.SetEditMode(isReadOnly);

            if (isReadOnly)
            {
                StopReading();
            }
            else
            {
                StartReading();
            }
        }

        /// <summary>
        /// Performs some steps to initialize the form.
        /// </summary>
        public override void PerformSpecificIntializationSteps()
        {
            _connectionLooker = new object();

            _vfsManager = new VitalForceSheetManager();
            _lookupsManager = new LookupsManager();
            _settingsManager = new SettingsManager();
            _testsManager = new TestsManager();

            var isNew = VFSObject.Id == 0;

            if (isNew)
            {
               InitializeVFSItems();
               InitializeVFSSecondaryItems();
                //Initialize VFS Values
            }

            SetFormStatus(isNew);
            FillLookUps();
            SetDefaultMeterPositionSettings();
            SetDefaultFonts();
            //SetupAutoTestTimerAndProgressBar();
            SetFormTitle(string.Format(StaticKeys.VFSTitle, VFSObject.Patient.FirstName, VFSObject.Patient.LastName));          
        }

        /// <summary>
        /// Initializes the VFS Items based on the VFS Items Source List
        /// </summary>
        private void InitializeVFSItems()
        {
            try
            {
                UiHelperClass.ShowWaitingPanel("VFS Items Initialization ...");
                VFSObject.VfsItems = new BindingList<VFSItem>();
                var vfsItemsSourceList = ((BindingList<VFSItemSource>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VFSItemsSource));

                UiHelperClass.ShowWaitingPanel("Loading Previous VFS Data ...");
                var previousVFS =
                    _vfsManager.GetVFSs(new VFSsFilter() {PatientId = VFSObject.Patient.Id, LoadingType = LoadingTypeEnum.None})
                        .LastOrDefault();
                
                BindingList<VFSItem> previousVFSItems = null;

                UiHelperClass.ShowWaitingPanel("Setup VFS Items ...");
                if (previousVFS != null)
                {
                    previousVFSItems = _vfsManager.GetVFSById(new SingleItemFilter() { ItemId = previousVFS.Id }).VfsItems;
                }

                foreach (var vfsItemSource in vfsItemsSourceList)
                {
                    var vfsItem = new VFSItem
                    {
                        VFS = VFSObject,
                        Item = new Item() { Id = vfsItemSource.Item.Id,
                                            Name = vfsItemSource.Item.Name, 
                                            FullName = vfsItemSource.Item.FullName,
                                            GenderLookup = vfsItemSource.Item.GenderLookup},
                        VFSItemSource = vfsItemSource,
                        GroupLookup = vfsItemSource.GroupLookup,
                        GridGroupLookup = vfsItemSource.GridGroupLookup,
                        SectionLookup = vfsItemSource.SectionLookup
                    };

                    SetVFSItemPreviousValues(vfsItem, previousVFSItems);

                    VFSObject.VfsItems.Add(vfsItem);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Initialize VFS Secondary Items
        /// </summary>
        private void InitializeVFSSecondaryItems()
        {
            try
            {
                UiHelperClass.ShowWaitingPanel("VFS Secondary Items Initialization ...");
                VFSObject.VfsSecondaryItems = new BindingList<VFSSecondaryItem>();
                var vfsSecondaryItemsSourceList = ((BindingList<VFSSecondaryItemSource>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VFSSecondaryItemsSource));

                UiHelperClass.ShowWaitingPanel("Setup VFS Secondary Items ...");

                foreach (var vfsSecondaryItemSource in vfsSecondaryItemsSourceList)
                {
                    var item = new Item();
                    item.Id = vfsSecondaryItemSource.Item.Id;
                    item.Name = vfsSecondaryItemSource.Item.Name;
                    item.FullName = vfsSecondaryItemSource.Item.FullName;
                    item.GenderLookup = vfsSecondaryItemSource.Item.GenderLookup;

                    var vfsSecondaryItem = new VFSSecondaryItem();
                    vfsSecondaryItem.VFS = VFSObject;
                    vfsSecondaryItem.Item = item;
                    vfsSecondaryItem.SectionLookup = vfsSecondaryItemSource.SectionLookup;

                    var siblings = VFSObject.VfsSecondaryItems.Where(s => s.SectionLookup != null &&  s.SectionLookup.Id == vfsSecondaryItemSource.SectionLookup.Id);

                    if (!siblings.Any())
                    {
                        vfsSecondaryItem.Order = 1;
                    }
                    else
                    {
                        vfsSecondaryItem.Order = siblings.Where(s => s.SectionLookup.Id == vfsSecondaryItemSource.SectionLookup.Id).Max(i => i.Order) + 1;
                    }

                    VFSObject.VfsSecondaryItems.Add(vfsSecondaryItem);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Sets previous values of a VFS item based on previous VFS items
        /// </summary>
        /// <param name="vfsItem"></param>
        /// <param name="previousVFSItems"></param>
        private void SetVFSItemPreviousValues(VFSItem vfsItem, BindingList<VFSItem> previousVFSItems)
        {
            if (previousVFSItems == null) return;

            var previousVFSItem = previousVFSItems.FirstOrDefault(v => v.Item.Id == vfsItem.Item.Id &&
                                                                       v.SectionLookup.Id == vfsItem.SectionLookup.Id &&
                                                                       v.GroupLookup.Id == vfsItem.GroupLookup.Id &&
                                                                       v.IsOnFlyItem == vfsItem.IsOnFlyItem);

            if (previousVFSItem == null) return;

            //Only set previous value if the current item setup requires the previous value to be set
            if (vfsItem.VFSItemSource.HasPreviousV1)
            {
                vfsItem.PreviousV1 = previousVFSItem.CurrentV1;
            }
            if (vfsItem.VFSItemSource.HasPreviousV2)
            {
                vfsItem.PreviousV2 = previousVFSItem.CurrentV2;
            }
        }

        /// <summary>
        /// Set the controls font.
        /// </summary>
        private void SetFonts(float fontSize)
        {
           //Set fonts for controls
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            VFSObject.PropertyChanged += VFS_PropertyChanged;
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            VFSObject.PropertyChanged -= VFS_PropertyChanged;
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            FillVFSLists();

            UiHelperClass.BindControl(textEditClientName, VFSObject, () => VFSObject.Patient, () => VFSObject.Patient.FullName);
            UiHelperClass.BindControl(textEditName, VFSObject, () => VFSObject.Name);
            UiHelperClass.BindControl(textEditDate, VFSObject, () => VFSObject.DateTime);
            UiHelperClass.BindControl(memoEditNotes, VFSObject, () => VFSObject.Notes);
            UiHelperClass.BindControl(memoEditEmotionalIssues, VFSObject, () => VFSObject.EmotionalIssues);
            UiHelperClass.BindControl(spinEditMercuryNumberOfIssues, VFSObject, () => VFSObject.MercuryNumOfIssues);
            UiHelperClass.BindControl(spinEditThyroidNumberOfIssues, VFSObject, () => VFSObject.ThyroidNumOfIssues);

            xtraUserControlVFSItemsMain.VFSObject = VFSObject;
            xtraUserControlPrimaryIssue.VFSObject = VFSObject;
            xtraUserControlSecondaryIssues.VFSObject = VFSObject;
            xtraUserControlThyroidIssues.VFSObject = VFSObject;
            xtraUserControlMercuryIssues.VFSObject = VFSObject;
            
            xtraUserControlVFSItemsMain.SetBinding();
            xtraUserControlPrimaryIssue.SetBinding();
            xtraUserControlSecondaryIssues.SetBinding();
            xtraUserControlThyroidIssues.SetBinding();
            xtraUserControlMercuryIssues.SetBinding();

            xtraUserControlPrintingOptionsMain.UpdateOptions();

            ShowHideMajorIssues(false);
            if (VFSObject.TestId != null)
            {
                UiHelperClass.ShowWaitingPanel("Loading Major Issues ...");

                _test = _testsManager.GetTestAndMajorIssuesAndTestResultsById(new SingleItemFilter() { ItemId = VFSObject.TestId.Value });

                if (_test != null)
                {
                    VFSObject.Test = _test;
                    ShowHideMajorIssues(true);
                    gridControlMajorIssues.DataSource = _test.MajorIssues;
                }
                
                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            textEditClientName.DataBindings.Clear();
            textEditDate.DataBindings.Clear();
            memoEditNotes.DataBindings.Clear();
            textEditName.DataBindings.Clear();
            memoEditEmotionalIssues.DataBindings.Clear();
            spinEditMercuryNumberOfIssues.DataBindings.Clear();
            spinEditThyroidNumberOfIssues.DataBindings.Clear();
            xtraUserControlVFSItemsMain.ClearBinding();
            xtraUserControlPrimaryIssue.ClearBinding();
            xtraUserControlSecondaryIssues.ClearBinding();
            xtraUserControlThyroidIssues.ClearBinding();
            xtraUserControlMercuryIssues.ClearBinding();
        }

        /// <summary>
        /// Setting some properties.
        /// </summary>
        public override void SetProperties()
        {
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = VFSObject;
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public override void AfterLoadAction()
        {
            IsLoaded = true;
            OpenCsaConnection();
            xtraUserControlVFSItemsMain.Focus();
            xtraUserControlVFSItemsMain.FocusFirstRow();
            Opacity = 100;
        }

        /// <summary>
        /// Fills the lookups ids.
        /// </summary>
        public override void FillLookUps()
        {
            var leftLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Left));

            var rightLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Right));

            _leftLookupId = leftLookup != null ? leftLookup.Id : 0;
            _rightLookupId = rightLookup != null ? rightLookup.Id : 0;

            _groupLookups = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.VFSSourceItemGroup));

            xtraUserControlVFSItemsMain.FillLookup();
            xtraUserControlPrimaryIssue.FillLookup();
            xtraUserControlSecondaryIssues.FillLookup();
            xtraUserControlThyroidIssues.FillLookup();
            xtraUserControlMercuryIssues.FillLookup();
        }

        /// <summary>
        /// Customize the crud bar actions.
        /// </summary>
        /// <param name="itemName"></param>
        public override void CustomeBarManagerClickHandling(string itemName)
        {
            if (string.IsNullOrEmpty(itemName)) return;

            if (itemName.Equals("barButtonItemPausePlay"))
            {
                //ToggleAutoTestMode();
            }
            else if (itemName.Equals("barButtonItemPrint"))
            {
                Print(false);
            }
            else if (itemName.Equals("barButtonItemPrintPreview"))
            {
                Print(true);
            }
            else if (itemName.Equals("barButtonItemSettings"))
            {
                CloseCsaConnection();
                ShowHideOverlay(true);
                new frmSettings().ShowDialog();
                ShowHideOverlay(false);
                OpenCsaConnection();
                StartReading();
            }
            else if (itemName.Equals("barButtonItemHelp"))
            {
                if (!File.Exists(Application.StartupPath + @"\" + "Help.chm"))
                {
                    UiHelperClass.ShowError("The Help file is missing.", "Help File Error");
                }

                Help.ShowHelp(this, Application.StartupPath + @"\" + "Help.chm");
            }
            else if (itemName.Equals("barButtonItemHotKeys"))
            {
                if (!File.Exists(Application.StartupPath + @"\" + "Shortcuts.chm"))
                {
                    UiHelperClass.ShowError("The Shortcuts file is missing.", "Shortcuts File Error");
                }

                Help.ShowHelp(this, Application.StartupPath + @"\" + "Shortcuts.chm");
            }
            else if (itemName.Equals("barButtonItemFeedback"))
            {
                UiHelperClass.ShowWaitingPanel("Opening Feedback Form ...", true);

                var feedbackForm = new XtraFormMailClient();

                UiHelperClass.HideSplash();
                ShowHideOverlay(true);
                feedbackForm.ShowDialog();
                ShowHideOverlay(false);
            }
        }

        /// <summary>
        /// Sets position of the meter to default one.
        /// </summary>
        private void SetDefaultMeterPositionSettings()
        {
            var meterPosition = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.MeterPosition));

            int meterPositionValue;

            if (meterPosition != null && int.TryParse(meterPosition.Value.ToString(), out meterPositionValue))
            {
                dockPanelMeter.Dock = meterPositionValue == _leftLookupId ? DockingStyle.Left : DockingStyle.Right;
            }
        }

        /// <summary>
        /// Sets default fonts.
        /// </summary>
        private void SetDefaultFonts()
        {
            var fontSizeSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.FontSize));

            float fontSizeSettingValue;

            if (fontSizeSetting != null && float.TryParse(fontSizeSetting.Value.ToString(), out fontSizeSettingValue))
            {
                SetFonts(fontSizeSettingValue);
            }
        }

        #endregion

        #region Hardware Init

        /// <summary>
        /// Open the connection with the hardware.
        /// </summary>
        /// <returns></returns>
        private void OpenCsaConnection()
        {
            try
            {
                lock (_connectionLooker)
                {
                    SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.Searching);
                    SetupCommonCsaConnectionHandlers();

                    var isPortOpend = CsaEmdUnitManager.Instance.OpenConnection().IsSucceed;

                    if (isPortOpend)
                    {
                        timerExtraConnectionChecker.Enabled = true;
                    }
                }

            }
            catch
            {

            }
        }

        /// <summary>
        /// Close the connection with the CSA
        /// </summary>
        private void CloseCsaConnection()
        {
            timerExtraConnectionChecker.Enabled = false;
            timerBroadcastingIndecator.Enabled = false;
            CsaEmdUnitManager.Instance.CancelAutoDetection();
            RemoveCommonCsaConnectionHandlers();
            CsaEmdUnitManager.Instance.CloseConnection();
        }

        /// <summary>
        /// Redetect(refresh) Hw connection.
        /// </summary>
        private void RedetectCsatConnection()
        {
            lock (_connectionLooker)
            {
                CloseCsaConnection();
                CsaEmdUnitManager.Instance.RefreshConnection();
                SetupCommonCsaConnectionHandlers();
            }
        }

        /// <summary>
        /// Set up the common handlers for the CSA connection to be used.
        /// </summary>
        private void SetupCommonCsaConnectionHandlers()
        {
            CsaEmdUnitManager.Instance.Connected += CsaManager_Connected;
            CsaEmdUnitManager.Instance.Disconnected += CsaManager_Disconnected;
            CsaEmdUnitManager.Instance.Released += Csa_Instance_Released;
            CsaEmdUnitManager.Instance.Detecting += Instance_Detecting;
        }

        /// <summary>
        /// Remove the common handlers for the CSA connection.
        /// </summary>
        private void RemoveCommonCsaConnectionHandlers()
        {
            CsaEmdUnitManager.Instance.Connected -= CsaManager_Connected;
            CsaEmdUnitManager.Instance.Disconnected -= CsaManager_Disconnected;
            CsaEmdUnitManager.Instance.Released -= Csa_Instance_Released;
            CsaEmdUnitManager.Instance.Detecting -= Instance_Detecting;
        }

        /// <summary>
        /// Set the connections indicator status.
        /// </summary>
        private void SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum indicatorStatusEnum)
        {
            var isAutoPortDetection = CsaEmdUnitManager.Instance.AutoComPortDetection;
            switch (indicatorStatusEnum)
            {
                case ConnectionIndicatorStatusEnum.Connected:
                    _isCsaUnitConnected = true;
                    stateIndicatorComponentConnectionStatus.SetState(IndicatorComponentStatus.Green);
                    labelControlConnectionStatus.Text = StaticKeys.CsaConnectedText;
                    simpleButtonConnectionPort.Text = string.Format(StaticKeys.CsaPortText,
                                                                    CsaEmdUnitManager.Instance.ComPortNumber);
                    simpleButtonConnectionPort.Image = Resources.port16x16;
                    simpleButtonConnectionPort.Enabled = false;
                    break;
                case ConnectionIndicatorStatusEnum.Disconnected:
                    _isCsaUnitConnected = false;
                    stateIndicatorComponentConnectionStatus.SetState(IndicatorComponentStatus.Red);
                    labelControlConnectionStatus.Text = StaticKeys.CsaDisconnectedText;
                    simpleButtonConnectionPort.Text = isAutoPortDetection
                                                          ? StaticKeys.CsaRetryText
                                                          : string.Format(StaticKeys.CsaPortText,
                                                                          CsaEmdUnitManager.Instance.ComPortNumber);
                    simpleButtonConnectionPort.Image = isAutoPortDetection
                                                           ? Resources.Patient_Refresh
                                                           : Resources.port16x16;
                    simpleButtonConnectionPort.Enabled = isAutoPortDetection;
                    break;
                case ConnectionIndicatorStatusEnum.Broadcasting:
                    labelControlBroadcastingIndecator.Text = StaticKeys.CsaBrodcastingText;
                    break;
                case ConnectionIndicatorStatusEnum.NotBroadcasting:
                    labelControlBroadcastingIndecator.Text = StaticKeys.CsaBrodcast;
                    break;
                case ConnectionIndicatorStatusEnum.Imprinting:
                    labelControlBroadcastingIndecator.Text = StaticKeys.CsaImprintingText;
                    break;
                case ConnectionIndicatorStatusEnum.Searching:
                case ConnectionIndicatorStatusEnum.SearchingPort:
                    simpleButtonConnectionPort.Enabled = false;
                    stateIndicatorComponentConnectionStatus.SetState(IndicatorComponentStatus.Ornage);
                    _isCsaUnitConnected = false;
                    labelControlConnectionStatus.Text = StaticKeys.CsaSearchingText;
                    simpleButtonConnectionPort.Image = Resources.port16x16;

                    simpleButtonConnectionPort.Text = indicatorStatusEnum == ConnectionIndicatorStatusEnum.SearchingPort
                                                          ? string.Format(StaticKeys.SearchingPortDescription,
                                                                          CsaEmdUnitManager.Instance.ComPortNumber)
                                                          : StaticKeys.CsaSearchingText;

                    break;
            }

        }

        #endregion

        #region Logic

        #region General Logic

        /// <summary>
        /// Update the broadcasting image.
        /// </summary>
        /// <param name="stage"></param>
        private void UpdateBroadcastingStageImage(int stage)
        {
            //switch (stage)
            //{
            //    case 0:
            //        pictureEditBroadcasting.Image = Resources.BC0;
            //        break;
            //    case 1:
            //        pictureEditBroadcasting.Image = Resources.BC1;
            //        break;
            //    case 2:
            //        pictureEditBroadcasting.Image = Resources.BC2;
            //        break;
            //    case 3:
            //        pictureEditBroadcasting.Image = Resources.BC3;
            //        break;
            //}
        }
        
        /// <summary>
        /// Can the form close. With user notification.
        /// </summary>
        /// <returns></returns>
        public override bool CanClose()
        {
            //Confirm closing
            var showScreenClosingConfirmation = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ShowScreenClosingConfirmation);

            if (showScreenClosingConfirmation)
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ClosingScreenConfirmation) != DialogResult.Yes)
                {
                    return false;
                }
            }

            if (VFSObject.ObjectState == DomainEntityState.Unchanged ||
                VFSObject.ObjectState == DomainEntityState.Deleted) return true;

            return SaveOrSaveAndClose(false);
        }

        #endregion

        #region CRUD Logic

        /// <summary>
        /// Posts the values in the controls that are not yet committed to the dataSource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public override void PostValues()
        {
            memoEditNotes.DoValidate();
            textEditName.DoValidate();
            xtraUserControlVFSItemsMain.PostValues();
            memoEditEmotionalIssues.DoValidate();
            spinEditMercuryNumberOfIssues.DoValidate();
            spinEditThyroidNumberOfIssues.DoValidate();
            xtraUserControlPrimaryIssue.PostValues();
            xtraUserControlSecondaryIssues.PostValues();
            xtraUserControlThyroidIssues.PostValues();
            xtraUserControlMercuryIssues.PostValues();
        }

        /// <summary>
        /// Uses the Tests manager to save the spot check.        
        /// </summary>
        public override bool Save(bool isClosing)
        {
            try
            {
                PostValues();

                UpdateListsWithDeletedRows();

                if (!VFSObject.Validate())
                    return false;

                VFSObject.TestId = VFSObject.TestId > 0 ? VFSObject.TestId : null;

                return _vfsManager.SaveVFS(VFSObject).IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }

        }

        /// <summary>
        /// Do some operations after saving.
        /// </summary>
        public override void AfterSaveAction()
        {
            xtraUserControlVFSItemsMain.DeletedVFSItems.Clear();

            xtraUserControlPrimaryIssue.ClearBinding();
            xtraUserControlSecondaryIssues.ClearBinding();
            xtraUserControlThyroidIssues.ClearBinding();
            xtraUserControlMercuryIssues.ClearBinding();
            xtraUserControlPrimaryIssue.SetBinding();
            xtraUserControlSecondaryIssues.SetBinding();
            xtraUserControlThyroidIssues.SetBinding();
            xtraUserControlMercuryIssues.SetBinding();
        }

        /// <summary>
        /// Delete the current test object.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            try
            {
                PostValues();

                Revert();

                var result = _vfsManager.DeleteVFS(VFSObject);

                if (result.IsSucceed)
                    Close();

                return result.IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }

        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            try
            {
                IsLoaded = false;

                VFSObject = _vfsManager.GetVFSById(new SingleItemFilter { ItemId = VFSObject.Id });

                Rebind();

                IsLoaded = true;
                xtraUserControlVFSItemsMain.DeletedVFSItems.Clear();
                xtraUserControlPrimaryIssue.DeletedVFSSecondaryItems.Clear();
                xtraUserControlSecondaryIssues.DeletedVFSSecondaryItems.Clear();
                xtraUserControlThyroidIssues.DeletedVFSSecondaryItems.Clear();
                xtraUserControlMercuryIssues.DeletedVFSSecondaryItems.Clear();

                return true;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }
        }

        /// <summary>
        /// Process the cell value changing to update the form status.
        /// </summary>
        private void OnCellChanging()
        {
            if (FormStatus != FormStatusEnum.New && FormStatus != FormStatusEnum.Modified)
                FormStatus = FormStatusEnum.Modified;
        }

        /// <summary>
        /// Add the deleted objects to the actual list again.
        /// </summary>
        private void UpdateListsWithDeletedRows()
        {
            foreach (var item in xtraUserControlVFSItemsMain.DeletedVFSItems)
            {
                VFSObject.VfsItems.Add(item);
            }

            VFSObject.VfsSecondaryItems.Clear();
            UpdateSectoinSecondaryItemsForSave(xtraUserControlPrimaryIssue);
            UpdateSectoinSecondaryItemsForSave(xtraUserControlSecondaryIssues);
            UpdateSectoinSecondaryItemsForSave(xtraUserControlThyroidIssues);
            UpdateSectoinSecondaryItemsForSave(xtraUserControlMercuryIssues);

            xtraUserControlVFSItemsMain.DeletedVFSItems.Clear();
        }

        /// <summary>
        /// Updates the master list with items from the 4 sections
        /// </summary>
        /// <param name="section"></param>
        private void UpdateSectoinSecondaryItemsForSave(XtraUserControlVFSSecondaryItems section)
        {
            foreach (var item in section.VFSSecondaryItems)
            {
                VFSObject.VfsSecondaryItems.Add(item);
            }

            foreach (var item in section.DeletedVFSSecondaryItems)
            {
                VFSObject.VfsSecondaryItems.Add(item);
            }
            section.DeletedVFSSecondaryItems.Clear();
        }

        #endregion

        #region VFS Logic

        /// <summary>
        /// Show or hides major issues list
        /// </summary>
        /// <param name="isVisible"></param>
        private void ShowHideMajorIssues(bool isVisible)
        {
            splitterItemVFSItemsTab.Visibility = isVisible ? LayoutVisibility.Always:LayoutVisibility.Never ;
            layoutControlGroupMajorIssues.Visibility = isVisible ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlGroupVFSItems.ExpandButtonVisible = isVisible;
        }

        /// <summary>
        /// Fill the spot check result lists depends on the result type.
        /// </summary>
        private void FillVFSLists()
        {
            if (VFSObject == null)
                return;

            if (VFSObject.VfsSecondaryItems == null)
                VFSObject.VfsSecondaryItems = new BindingList<VFSSecondaryItem>();
        }

        /// <summary>
        /// Gets item for broadcast.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItemsToBroadcast()
        {
            //ToDo: this list should come from VFS Item user control.
            return new List<Item>();
        }

        #endregion

        #region Prining

        /// <summary>
        /// Adds empty lines to group for report layout consistency purposes
        /// </summary>
        /// <param name="sectionGroups"></param>
        private void AddEmptyLinesToGroups(BindingList<VFSReportGroup> sectionGroups, bool includeHormones)
        {
            if (sectionGroups == null) return;

            var sectionMax = 0;
            var hormonesCount = 0;
            var countpHColumn = 0;

            if (includeHormones)
            {
                
                hormonesCount =
                    VFSObject.VfsItems.Count(
                        i =>
                            EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) ==
                            VFSSourceItemGroup.Hormones) + 1;//We add one to handle Hormones header in report

                countpHColumn = sectionGroups.FirstOrDefault(i =>
                            EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) ==
                            VFSSourceItemGroup.pH).VFSItems.Count + hormonesCount;

                sectionMax = sectionGroups.Where(i =>
                            EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) !=
                            VFSSourceItemGroup.pH).Max(g => g.VFSItems.Count);

                if (countpHColumn > sectionMax)
                {
                    sectionMax = countpHColumn;
                }
            }
            else
            {
                sectionMax = sectionGroups.Max(g => g.VFSItems.Count);
            }
            
            foreach (var groupLookup in sectionGroups)
            {
                var seccionFirst = groupLookup.VFSItems.FirstOrDefault();

                if (seccionFirst == null || EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(groupLookup.GroupLookup.Value) == VFSSourceItemGroup.pH) continue;

                var count = sectionMax - groupLookup.VFSItems.Count;

                if (includeHormones &&
                    EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(groupLookup.GroupLookup.Value) == VFSSourceItemGroup.Hormones)
                {
                    count = sectionMax - countpHColumn;
                }

                if (groupLookup.VFSItems.Count >= sectionMax) continue;

                for (var i = 0; i < count; i++)
                {
                    groupLookup.VFSItems.Add(new VFSItem() { Item = new Item(), GroupLookup = seccionFirst.GroupLookup });
                }
            }
        }

        private void HandleHormonesSectionItems(BindingList<VFSReportGroup> section3Groups, VFSReportGroup hormonGroup, BindingList<VFSItem> section4Items)
        {
            var hormonesCount = 0;
            var countpHColumn = 0;
            var sectionMax = 0;

            hormonesCount =
            VFSObject.VfsItems.Count(
                i =>
                    EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) ==
                    VFSSourceItemGroup.Hormones) + 1;//We add one to handle Hormones header in report

            countpHColumn = section3Groups.FirstOrDefault(i =>
                    EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) ==
                    VFSSourceItemGroup.pH).VFSItems.Count + hormonesCount;

            sectionMax = section3Groups.Where(i =>
                    EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) !=
                    VFSSourceItemGroup.pH).Max(g => g.VFSItems.Count);

            var count = sectionMax - countpHColumn;

            for (var i = 0; i < count; i++)
            {
                section4Items.Add(new VFSItem() { Item = new Item(), GroupLookup = hormonGroup.GroupLookup });
            }
        }

        /// <summary>
        /// Logic for printing
        /// </summary>
        /// <param name="isPreview"></param>
        private void Print(bool isPreview)
        {
            if (_isEditable && !SaveAction())
                return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.PreparingReportMessage);

            var section1Groups = new BindingList<VFSReportGroup>();
            var section2 = new BindingList<VFSItem>();
            var section3Groups = new BindingList<VFSReportGroup>();
            VFSReportGroup hormonGroup = null;
            var section4Items = new BindingList<VFSItem>();

            //Fill lists based on Group - VFSItem structure needed for report layout
            foreach (var groupLookup in _groupLookups)
            {
                var groupEnum = EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(groupLookup.Value);

                if (groupEnum != VFSSourceItemGroup.None)
                {
                    var reportGroup = new VFSReportGroup();
                    var lookup = groupLookup;

                    reportGroup.GroupLookup = lookup;

                    reportGroup.VFSItems = VFSObject.VfsItems.Where(v => v.GroupLookup.Id == lookup.Id).ToBindingList();

                    switch (groupEnum)
                    {
                        case VFSSourceItemGroup.OrganAndGlandsTwo:
                        case VFSSourceItemGroup.Overall:
                        case VFSSourceItemGroup.MajorSystems:
                        case VFSSourceItemGroup.OrganAndGlandsOne:
                            {
                                section1Groups.Add(reportGroup);
                            }
                            break;
                        case VFSSourceItemGroup.Age:
                            section2.Add(new VFSItem()
                            {
                                GroupLookup = groupLookup,
                                Item = new Item() { Name = "Actual Age" },
                                CurrentV1 = VFSObject.Patient.DateOfBirth.HasValue ? (DateTime.Now.Year - VFSObject.Patient.DateOfBirth.Value.Year).ToString("N0") : string.Empty
                            });

                            foreach (var vfsItem in VFSObject.VfsItems.Where(i =>
                                        EnumNameResolver.GetValueFromDescription<VFSSourceItemGroup>(i.GroupLookup.Value) == VFSSourceItemGroup.Age))
                            {
                                section2.Add(vfsItem);
                            }
                            break;
                        case VFSSourceItemGroup.Services1:
                        case VFSSourceItemGroup.Services2:
                        case VFSSourceItemGroup.pH:
                            {
                                section3Groups.Add(reportGroup);
                            }
                            break;
                        case VFSSourceItemGroup.Hormones:
                            section4Items = reportGroup.VFSItems;
                            hormonGroup = reportGroup;
                            break;
                    }   
                }
            }

            AddEmptyLinesToGroups(section1Groups,false);
            AddEmptyLinesToGroups(section3Groups,true);
            HandleHormonesSectionItems(section3Groups, hormonGroup, section4Items);
            
            var report = new XtraReportVFS()
            {
                PatientName = { Value = VFSObject.Patient.FullName },
                bindingSourcePatient = { DataSource = VFSObject.Patient },
                bindingSourceVFS = { DataSource = VFSObject },
                bindingSourceGroupsSection1 = { DataSource = section1Groups },
                bindingSourceSection2 = { DataSource = section2 },
                bindingSourceGroupsSection3 = { DataSource = section3Groups },
                bindingSourceSection4 = { DataSource = section4Items },
                HidePatientName = { Value = xtraUserControlPrintingOptionsMain.HidePatientName },
                HideLogo = { Value = xtraUserControlPrintingOptionsMain.HideLogo },
                ShowAddressInfo = { Value = xtraUserControlPrintingOptionsMain.ShowAddressInfo },
                ElevationChangeType = { Value = xtraUserControlVFSItemsMain.ElevationChangeTypeId },
                NormalChangeType = { Value = xtraUserControlVFSItemsMain.NormalChangeTypeId },
                DecreaseChangeType = { Value = xtraUserControlVFSItemsMain.DecreaseChangeTypeId },
            };

            if (_test != null)
            {
                report.bindingSourceTest.DataSource = _test;
            }

            UiHelperClass.SetReportLogo(report.xrSubreportHeader);

            if (isPreview)
            {
                var reportViewer = new XtraFormReportViewer();
                reportViewer.SetReport(report);
                UiHelperClass.HideSplash();
                reportViewer.ShowDialog();
            }
            else
            {
                UiHelperClass.HideSplash();
                report.PrintDialog();
            }
        }

        #endregion
        #endregion

        #endregion

        #region HW_Logic

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading()
        {
            if (!IsInEditMode)
                return;

            CsaEmdUnitManager.Instance.Clear();

            ResetHardwareHandlers();

            Broadcast();
            
            CsaEmdUnitManager.Instance.StartReading();
        }

        /// <summary>
        /// Broadcasting actions.
        /// </summary>
        private void Broadcast()
        {
            var itemsToBroadcast = GetItemsToBroadcast();

            if (CsaEmdUnitManager.Instance.IsBroadcastingOn && itemsToBroadcast != null && itemsToBroadcast.Count > 0)
            {
                CsaEmdUnitManager.Instance.Broadcast(itemsToBroadcast);
                OnBroadcastingActions();
            }
            else
            {
                OnBroadcastDoneActions();
            }

        }

        /// <summary>
        /// Handel the broadcasting action.
        /// </summary>
        private void OnBroadcastingActions()
        {

            if (!IsInEditMode || !IsLoaded || !CsaEmdUnitManager.Instance.IsCsaEmdUnitConnected)
                return;

            BroadcastingStage = 1;

            SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.Broadcasting);

            lock (timerBroadcastingIndecator)
            {
                timerBroadcastingIndecator.Enabled = true;
            }
        }

        /// <summary>
        /// Handel the broadcastDone action.
        /// </summary>
        private void OnBroadcastDoneActions()
        {
            if(!IsLoaded)
                return;

            SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.NotBroadcasting);

            BroadcastingStage = 0;
        }

        /// <summary>
        /// Reset the hardware handlers.
        /// </summary>
        private void ResetHardwareHandlers()
        {
            RemoveHardwareHandlers();
            AddHardwareHandlers();
        }

        /// <summary>
        /// Remove the hardware handlers.
        /// </summary>
        private void RemoveHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_ReadingDone;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone += _csaManager_ReadingDone;
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            CsaEmdUnitManager.Instance.StopReading();
            RemoveHardwareHandlers();
        }

        /// <summary>
        /// Set the status test status bar.
        /// </summary>
        public void SetReadingStatusBarMode(TestBarStateEnum state, string actionName, float secondsToWait)
        {
            if (IsClosing)
                return;

            xtraUserControlReadingGauge.SetReadingStatusBarMode(state, actionName, secondsToWait);
        }

        /// <summary>
        /// Sets the reading indicator.
        /// </summary>
        public void SetReadingIndicators(TestBarStateEnum testBarState, float secondsToWait)
        {
            if (IsClosing)
                return;

            SetReadingStatusBarMode(testBarState, string.Empty, secondsToWait);
        }


        #endregion

        #region Handlers

        #region GeneralHandlers

        /// <summary>
        /// Handles form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVFS_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handle the propriety changed event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        private void VFS_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(VFS_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                switch (VFSObject.ObjectState)
                {
                    case DomainEntityState.Modified:
                        FormStatus = FormStatusEnum.Modified;
                        break;
                    case DomainEntityState.Unchanged:
                        FormStatus = FormStatusEnum.Unchanged;
                        break;
                }
            }
        }

        /// <summary>
        /// Handle Close event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        public void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(frm_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PostValues();

                e.Cancel = !CanClose();

                if (e.Cancel)
                {
                    IsClosing = false;
                    return;
                }

                CloseCsaConnection();

                //saving the meter position
                if (dockPanelMeter.Dock == DockingStyle.Right || dockPanelMeter.Dock == DockingStyle.Left)
                {
                    var meterPosition =
                        _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.MeterPosition) });
                    int oldMeterPositionValue;

                    int.TryParse(meterPosition.Value.ToString(), out oldMeterPositionValue);
                    int newMeterPositionValue = dockPanelMeter.Dock == DockingStyle.Left
                                                    ? _leftLookupId
                                                    : _rightLookupId;

                    if (oldMeterPositionValue != newMeterPositionValue)
                    {
                        try
                        {
                            meterPosition.Value = newMeterPositionValue;
                            _settingsManager.Save(meterPosition);
                            CachingManager.RemoveFromCache(CachableDataEnum.VisibleSettings.ToString());
                        }
                        catch (Exception)
                        {
                            UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.SettingError);
                        }
                    }
                }

            }
        }

        #endregion

        #region Hardware Handlers

        #region General Hardware Handler

        /// <summary>
        /// Handel the Hw when being Connected.
        /// </summary>
        /// <param name="sender"></param>
        private void CsaManager_Connected(object sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnConnected(CsaManager_Connected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                lock (_connectionLooker)
                {
                    if (IsClosing)
                        return;

                    SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.Connected);
                }
            }
        }

        /// <summary>
        /// Handel the Hw when being Disconnected.
        /// </summary>
        /// <param name="sender"></param>
        private void CsaManager_Disconnected(object sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnDisconnected(CsaManager_Disconnected), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                lock (_connectionLooker)
                {
                    if (IsClosing)
                        return;

                    SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.Disconnected);

                    OnBroadcastDoneActions();

                    //xtraUserControlReadingGauge.Clear();

                    Broadcast();
                }
            }
        }

        private void Instance_Detecting(object sender, int comPortNumber)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
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
                lock (_connectionLooker)
                {
                    if (IsClosing)
                        return;

                    SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.SearchingPort);
                }
            }
        }

        #endregion

        #region HW_Events

        /// <summary>
        /// Handel the click on the redetect a connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRedetect_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonRedetect_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                RedetectCsatConnection();
            }
        }

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            var csaManager = sender as CsaEmdUnitManager;

            if (csaManager != null && csaManager.IsReadingOn == false) return;

            if (InvokeRequired)
            {
                Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender, reading, min, max, fall, rise);
            }
            else
            {
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                StopReading();

                SetReadingIndicators(TestBarStateEnum.WaitingToRelease, 0);
            }
        }

        /// <summary>
        /// Handel the reading done event.
        /// </summary>
        void _csaManager_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_MeterValueChanged), sender,
                           reading, min, max);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlReadingGauge.ReadingValue = reading;
                SetReadingStatusBarMode(TestBarStateEnum.Reading, string.Empty, 0);
            }

        }

        /// <summary>
        /// Handel the CSA released  event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        void Csa_Instance_Released(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReleased(Csa_Instance_Released), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //This check for prevent the released [@UI level only] action get executing twice.
                if (!CsaEmdUnitManager.Instance.HasReading) return;

                SetReadingStatusBarMode(TestBarStateEnum.Ready, string.Empty, 0);

                StartReading();

            }
        }

        /// <summary>
        /// Handel the extra time tick for checking the connection.
        /// </summary>
        private void timerExtraConnectionChecker_Tick(object sender, EventArgs e)
        {
            try
            {

                if (IsHandleCreated && InvokeRequired)
                {
                    try
                    {
                        if (IsDisposed) return;
                        Invoke(new EventHandler(timerExtraConnectionChecker_Tick), sender, e);
                    }
                    catch
                    {
                        // Nothing to do, form had been disposed.
                    }
                }
                else
                {
                    //This checking for connection status is done here because some times the connected event raised before the Indecator complonent is created, also in some cases when CPU beening busy.
                    if (!_isCsaUnitConnected && CsaEmdUnitManager.Instance.IsCsaEmdUnitConnected)
                    {
                        lock (_connectionLooker)
                        {
                            SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.Connected);
                        }
                    }

                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// Handel the timer tick for the broadcasting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBroadcastingIndecator_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsHandleCreated && InvokeRequired)
                {
                    // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                    try
                    {
                        if (IsDisposed) return;
                        Invoke(new EventHandler(timerBroadcastingIndecator_Tick), sender, e);
                    }
                    catch
                    {
                        // Nothing to do, form had been disposed.
                    }
                }
                else
                {
                    lock (timerBroadcastingIndecator)
                    {
                        if (BroadcastingStage == 3)
                        {
                            BroadcastingStage = 1;

                            if (!CsaEmdUnitManager.Instance.IsBroadcasting)
                            {
                                timerBroadcastingIndecator.Enabled = false;
                                OnBroadcastDoneActions();
                            }

                            return;
                        }

                        BroadcastingStage++;
                    }

                }
            }
            catch
            {

            }
        }

        #endregion        

        /// <summary>
        /// Handle general key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVFS_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    tabbedControlGroupVFSTabs.SelectedTabPage = layoutControlGroupVFSItemsTab;
                    xtraUserControlVFSItemsMain.Focus();
                    xtraUserControlVFSItemsMain.FocusFirstRow();
                    break;
                case Keys.F2:
                    if (dockPanelClientIssues.Visibility == DockVisibility.Hidden)
                    {
                        dockPanelClientIssues.Visibility = DockVisibility.Visible;
                    }
                    xtraUserControlPrimaryIssue.Focus();
                    xtraUserControlPrimaryIssue.FocusFirstRow();
                    break;
                case Keys.F3:
                    if (dockPanelClientIssues.Visibility == DockVisibility.Hidden)
                    {
                        dockPanelClientIssues.Visibility = DockVisibility.Visible;
                    }
                    xtraUserControlSecondaryIssues.Focus();
                    xtraUserControlSecondaryIssues.FocusFirstRow();
                    break;
                case Keys.F4:
                    tabbedControlGroupVFSTabs.SelectedTabPage = layoutControlGroupThyroidAndMercury;
                    xtraUserControlThyroidIssues.Focus();
                    xtraUserControlThyroidIssues.FocusFirstRow();
                    break;
                case Keys.F5:
                    tabbedControlGroupVFSTabs.SelectedTabPage = layoutControlGroupThyroidAndMercury;
                    xtraUserControlMercuryIssues.Focus();
                    xtraUserControlMercuryIssues.FocusFirstRow();
                    break;
            }
        }

        #endregion

        #endregion
    }
}