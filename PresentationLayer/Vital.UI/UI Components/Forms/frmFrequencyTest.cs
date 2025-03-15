using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraReports.UI;
using Vital.Business.Managers;
using Vital.Business.Shared;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.Tests;
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
    public partial class frmFrequencyTest : VitalBaseForm
    {
        #region PrivateMembers

        private BarButtonItem _barButtonItemPausePlay;
        private FrequencyTestsManager _frequencyTestsManager;
        private ItemsManager _itemsManager;
        private SettingsManager _settingsManager;

        private bool _isCsaUnitConnected;
        private object _connectionLooker;
        private int _broadcastingStage;
        private int _autoTestNextReadingTimeOut;

        private BindingList<FrequencyTestResult> _frequencyTestResultsResults;
        private List<FrequencyTestResult> _deletedFrequencyTestResults;

        private int _leftLookupId;
        private int _rightLookupId;
        private bool _isEditable;

        private TestPlayStateEnum _currentTestPlayState;
        private List<VisitedItem> _tempVisitedItems;
        private bool _isWaitingCsaRealsedToTakeNewReading;
        private int _lastItemTestingReadingValue;
        private bool _isInReadingRunningMode;
        private XtraFormFrequenciesUpdate _xtraFormFrequenciesUpdate;
        private List<string> _frequencyTRLFiles;
        private string _defaultFrequencyItemsPath;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the FrequencyTest object
        /// </summary>
        public FrequencyTest FrequencyTestObject { get; set; }

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

        /// <summary>
        /// Gets the next test result temp ID
        /// </summary>
        private int NextResultId
        {
            get
            {
                return xtraUserControlItemsNavGridFrequencyTest.TestResults != null &&
                       xtraUserControlItemsNavGridFrequencyTest.TestResults.Count != 0 ? xtraUserControlItemsNavGridFrequencyTest.TestResults.Max(tr => tr.Id) + 1 : 1;
            }
        }

        /// <summary>
        /// Gets the next result ID
        /// </summary>
        /// <returns></returns>
        public int GetNextTestResultId()
        {
            return xtraUserControlItemsNavGridFrequencyTest.TestResults.Count == 0 ? 1 : xtraUserControlItemsNavGridFrequencyTest.TestResults.Max(r => r.TempImprintingId) + 1;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="frequencyTest"></param>
        public frmFrequencyTest(FrequencyTest frequencyTest)
        {
            InitializeComponent();
            FrequencyTestObject = frequencyTest;
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

            _barButtonItemPausePlay = AddBarButtonItem("barButtonItemPausePlay", "Play", false, Resources.Test_Play, 0,
                                                       0, false,
                                                       new BarShortcut((Keys.Control | Keys.P)), true,
                                                       "Play/Pause",
                                                       "Click this button to start automatic navigation between points.\r\n",
                                                       "You can use Ctrl+P to toggle this button.", true);

            var position = 7;

            AddBarButtonItem("barButtonItemSettings", "Settings", true, Resources.SettingsSmall, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.S)), true,
                             "Settings",
                             "Click this button to access the application settings.\r\n",
                             "You can use Alt+S to access the application settings.", true);
            position += 1;

            AddBarButtonItem("barButtonItemHelp", "Help", true, Resources.HelpSmall, position,
                             position, true,
                             null, true,
                             "Help",
                             "Click this button to access the application help.\r\n",
                             string.Empty, true);
            position += 1;
            AddBarButtonItem("barButtonItemHotKeys", "Shortcuts", true, Resources.keyIcon, position,
                             position, false,
                             null, true,
                             "Shortcuts",
                             "Click this button to access the application shortcuts.\r\n",
                             string.Empty, true);
            position += 1;
            AddBarButtonItem("barButtonItemFeedback", "Feedback", true, Resources.feedback_small, position,
                             position, true,
                             null, true,
                             "Feedback",
                             "Report an issue or make a suggestion.\r\n",
                             string.Empty, true);
            position += 1;
            AddBarButtonItem("barButtonItemPrint", "Print", true, Resources.Print, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.P)), true,
                             "Print",
                             "Click this button to print current frequency test according the selected settings in printing area.\r\n",
                             "You can use Alt+P to print current test.", true);
           
            position += 1;
            AddBarButtonItem("barButtonItemPrintPreview", "Print Preview", true, Resources.Test_Tab, position,
                             position, false,
                             new BarShortcut((Keys.Alt | Keys.Control | Keys.P)), true,
                             "Print Preview",
                             "Click this button to preview current frequency test report.\r\n",
                             "You can use Alt + Ctrl + P to preview current test report.", true);
            position += 1;
            AddBarButtonItem("barButtonItemUpdateFrequencies", "Update Frequencies", true, Resources.refresh, position,
                             position, true,
                             null, true,
                             "Update Frequencies",
                             "Click this button to check for frequency updates in the default folder and perform them.\r\n",
                             string.Empty, true);
            position += 1;
            AddBarButtonItem("barButtonItemUpdateFrequenciesFromFolder", "Update Frequencies From Folder", true, Resources.Notes_16, position,
                            position, true,
                            null, true,
                            "Update Frequencies From Folder",
                            "Click this button to select a folder to check for frequency updates and perform them.\r\n",
                            string.Empty, true);
            position += 1;

            xtraUserControlItemDetailsMain.EditDescriptionButtonHidden = true;
            xtraUserControlItemsNavGridFrequencyTest.SetCurrentNodeLabel(StaticKeys.EnergyFrequencies);
            xtraUserControlItemsNavGridFrequencyTest.AddToTestResults += xtraUserControlItemsNavGridFrequencyTest_AddToTestResults;
            xtraUserControlItemsNavGridFrequencyTest.ReadingRequest += xtraUserControlItemsNavGridFrequencyTest_ReadingRequest;
            xtraUserControlItemsNavGridFrequencyTest.CancelReadingRequest += xtraUserControlItemsNavGridFrequencyTest_CancelReadingRequest;
            xtraUserControlItemsNavGridFrequencyTest.RefreshDetailsAndImageNavGrid += xtraUserControlItemsNavGridFrequencyTest_RefreshDetailsAndImageNavGrid;
            xtraUserControlItemsNavGridFrequencyTest.SetImageIgnoreState += xtraUserControlItemsNavGridFrequencyTest_SetImageIgnoreState;
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public override void SetEditMode(bool isReadOnly)
        {
            _isEditable = !isReadOnly;

            _barButtonItemPausePlay.Enabled = !isReadOnly;

            textEditName.Properties.ReadOnly = isReadOnly;
            memoEditNotes.Properties.ReadOnly = isReadOnly;
            xtraUserControlItemsNavGridFrequencyTest.SetEditMode(isReadOnly);
            xtraUserControlFrequencyTestResults.SetEditMode(isReadOnly);

            xtraUserControlItemDetailsMain.ClearImageAndDetails();
            xtraUserControlReadingGauge.Clear();

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
            _currentTestPlayState = TestPlayStateEnum.Paused;
            _frequencyTRLFiles = new List<string>();

            _frequencyTestsManager = new FrequencyTestsManager();
            _itemsManager = new ItemsManager();
            _settingsManager = new SettingsManager();

            _deletedFrequencyTestResults = new List<FrequencyTestResult>();
            _tempVisitedItems = new List<VisitedItem>();
            xtraUserControlItemsNavGridFrequencyTest.TestResults = new BindingList<TestResult>();
            xtraUserControlItemsNavGridFrequencyTest.CurrentIssue = new TestIssue();
            
            var isNew = FrequencyTestObject.Id == 0;
            InitFrequencyTestObject(isNew);
            SetFormStatus(isNew);
            FillLookUps();
            SetDefaultMeterPositionSettings();
            SetDefaultFornts();
            SetupAutoTestTimerAndProgressBar();
            SetFormTitle(string.Format(StaticKeys.FrequencyTestTitle, FrequencyTestObject.Patient.FirstName, FrequencyTestObject.Patient.LastName));
            SetImageAreaSettings();

            if (isNew)
            {
                ToggleAutoTestMode();
            }
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            FillFrequencyTestResultLists();
            StopAutoTestTimer();

            xtraUserControlItemDetailsMain.ClearImageAndDetails();
            xtraUserControlReadingGauge.Clear();

            UiHelperClass.BindControl(textEditDate, FrequencyTestObject, () => FrequencyTestObject.CreationDateTime);
            UiHelperClass.BindControl(textEditName, FrequencyTestObject, () => FrequencyTestObject.Name);
            UiHelperClass.BindControl(memoEditNotes, FrequencyTestObject, () => FrequencyTestObject.Notes);

            BindNavGrid();
            GenerateTestResults();

            xtraUserControlPrintingOptionsMain.UpdateOptions();

            xtraUserControlFrequencyTestResults.FrequencyTestResults = _frequencyTestResultsResults;

            _deletedFrequencyTestResults.Clear();

            _defaultFrequencyItemsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), StaticKeys.FrequenciesFolderName);
            _frequencyTRLFiles = new List<string>();

            if (Directory.Exists(_defaultFrequencyItemsPath))
            {
                _frequencyTRLFiles = Directory.GetFiles(_defaultFrequencyItemsPath, StaticKeys.FrequencyInfoFileExtensionFilter, SearchOption.AllDirectories).ToList();
            }
            
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            textEditName.DataBindings.Clear();
            memoEditNotes.DataBindings.Clear();
        }

        /// <summary>
        /// Setting some properties.
        /// </summary>
        public override void SetProperties()
        {
            textEditName.Properties.MaxLength = 200;
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = FrequencyTestObject;
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public override void AfterLoadAction()
        {
            IsLoaded = true;
            OpenCsaConnection();

            if (FormStatus != FormStatusEnum.New)
            {
                var enableAutomationWhenOpeningExistingTest = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.EnableAutomationWhenOpeningExistingTest);

                if (enableAutomationWhenOpeningExistingTest && _currentTestPlayState == TestPlayStateEnum.Paused)
                {
                    StartAutoPlayTestMode();
                }
            }
        }

        /// <summary>
        /// Fills the lookups ids.
        /// </summary>
        public override void FillLookUps()
        {
            var leftLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Left));
            var rightLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Right));

            _leftLookupId = leftLookup != null ? leftLookup.Id : 0;
            _rightLookupId = rightLookup != null ? rightLookup.Id : 0;

            xtraUserControlItemDetailsMain.FillLookups();
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
                ToggleAutoTestMode();
            }
            else if (itemName.Equals("barButtonItemEdit"))
            {
                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
            else if (itemName.Equals("barButtonItemDisable"))
            {
                ControlItemDetailesAndReadingsDependOnUserAction(true);
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
            else if (itemName.Equals("barButtonItemUpdateFrequencies"))
            {
                CheckForFrequenciesUpdates(true);
            }
            else if (itemName.Equals("barButtonItemUpdateFrequenciesFromFolder"))
            {
                CheckForFrequenciesUpdates(false);
            }
            else if (itemName.Equals("barButtonItemPrint"))
            {
                Print(false);
            }
            else if (itemName.Equals("barButtonItemPrintPreview"))
            {
                Print(true);
            }
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            FrequencyTestObject.PropertyChanged += FrequencyTestObject_PropertyChanged;

            _frequencyTestResultsResults.RaiseListChangedEvents = true;
            _frequencyTestResultsResults.ListChanged += subListFrequencyTestResults_ListChanged;
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            FrequencyTestObject.PropertyChanged -= FrequencyTestObject_PropertyChanged;

            _frequencyTestResultsResults.RaiseListChangedEvents = false;
            _frequencyTestResultsResults.ListChanged -= subListFrequencyTestResults_ListChanged;
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

            if (FormStatus == FormStatusEnum.Modified)
                CollectFrequencyTestResults();

            if (FrequencyTestObject.ObjectState == DomainEntityState.Unchanged ||
                FrequencyTestObject.ObjectState == DomainEntityState.Deleted) return true;

            return SaveOrSaveAndClose(false);
        }

        /// <summary>
        /// Init the test object.
        /// </summary>
        /// <param name="isNew">Is new test.</param>
        private void InitFrequencyTestObject(bool isNew)
        {
            if (isNew)
            {
                FrequencyTestObject.CreationDateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Binds the nav grid with items
        /// </summary>
        private void BindNavGrid()
        {
            var parent = xtraUserControlItemsNavGridFrequencyTest.GetCurrentTestResult();

            var frequencyGroupsParent = _itemsManager.GetItems(new ItemsFilter { Key = StaticKeys.EnergyFrequencyGroupsParentItemKey }).FirstOrDefault();

            var itemsListToSet = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.FrequencyList);

            var testStep = new TestResult
            {
                IsCurrent = true,
                IsSelected = false,
                Item = frequencyGroupsParent,
                Parent = parent,
                DateTime = DateTime.Now,
                StepType = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Substance)),
                TestIssue = xtraUserControlItemsNavGridFrequencyTest.CurrentIssue,
                Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                TempImprintingId = GetNextTestResultId()
            };

            if (parent != null)
                parent.IsCurrent = false;

            xtraUserControlItemsNavGridFrequencyTest.TestResults.Add(testStep);
            //xtraUserControlItemsNavGridFrequencyTest.BackClicks += 1;
            xtraUserControlItemsNavGridFrequencyTest.InitGridItems(itemsListToSet, true, false, false);
            xtraUserControlItemsNavGridFrequencyTest.SetCurrentNodeLabel(StaticKeys.EnergyFrequencies);

            xtraUserControlItemsNavGridFrequencyTest.VisitedItems.Add(new VisitedItem { ItemId = frequencyGroupsParent.Id, Type = StaticKeys.VisitedItemTypeItem });

            xtraUserControlItemsNavGridFrequencyTest.IsFirstSwitch = false;
            xtraUserControlItemsNavGridFrequencyTest.CurrentNodeId = 0;
            xtraUserControlItemsNavGridFrequencyTest.PartHilight = 1;
            xtraUserControlItemsNavGridFrequencyTest.ResetHideShowItemsFeature();

            SetDetails(xtraUserControlItemsNavGridFrequencyTest.GetSelectedItems(),
                        xtraUserControlItemsNavGridFrequencyTest.TopItems,
                        xtraUserControlItemsNavGridFrequencyTest.BottomItems,
                        xtraUserControlItemsNavGridFrequencyTest.IsTopListFirst,
                        xtraUserControlItemsNavGridFrequencyTest.CurrentTestResultItem,
                        xtraUserControlItemsNavGridFrequencyTest.CurrentIssueItem);
        }

        /// <summary>
        /// Clears NavGrid and binds it again
        /// </summary>
        private void RebindNavGrid()
        {
            xtraUserControlItemsNavGridFrequencyTest.TestResults.Clear();
            BindNavGrid();
            GenerateTestResults();
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
        private void SetDefaultFornts()
        {
            var fontSizeSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.FontSize));

            float fontSizeSettingValue;

            if (fontSizeSetting != null && float.TryParse(fontSizeSetting.Value.ToString(), out fontSizeSettingValue))
            {
                SetFonts(fontSizeSettingValue);
            }

        }

        /// <summary>
        /// Set the controls font.
        /// </summary>
        private void SetFonts(float fontSize)
        {
            xtraUserControlFrequencyTestResults.GridFont = UiHelperClass.GetFontWithSize(fontSize);
        }

        /// <summary>
        /// Gets settings for image area from DB and sets them in control
        /// </summary>
        private void SetImageAreaSettings()
        {
            UiHelperClass.ShowWaitingPanel("Load Image Settings ...");
            xtraUserControlItemDetailsMain.UseAutoZoom = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.UseAutoZoom);
            xtraUserControlItemDetailsMain.ZoomLevel = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.GalleryDefaultZoomLevel, CachableDataEnum.Settings).ToString());
            UiHelperClass.HideSplash();
        }

        private void UpdateImageAreaSettings()
        {
            UiHelperClass.ShowWaitingPanel("Updating image settings ...");
            UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.UseAutoZoom, xtraUserControlItemDetailsMain.UseAutoZoom, _settingsManager);

            var galleryZoomLevelSetting = UiHelperClass.GetSettingFromCache(SettingKeys.GalleryDefaultZoomLevel, CachableDataEnum.Settings);
            if (galleryZoomLevelSetting != null)
            {
                galleryZoomLevelSetting.Value = xtraUserControlItemDetailsMain.ZoomLevel;
                _settingsManager.Save(galleryZoomLevelSetting);
            }
            UiHelperClass.HideSplash();
        }

        #endregion

        #region Hardware Logic
        
        #region Hardware Initialization

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
                        ControlItemDetailesAndReadingsDependOnUserAction(false);
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
                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
        }

        #endregion

        #region Hardware Handlers Setup

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

        #endregion

        #region Indicators

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

            var actionName = xtraUserControlItemsNavGridFrequencyTest.GetNextActionName();
            xtraUserControlItemsNavGridFrequencyTest.SetReadingStatusBarMode(testBarState, actionName, secondsToWait);
            SetReadingStatusBarMode(testBarState, actionName, secondsToWait);
        }

        #endregion

        #region Reading Logic

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading()
        {
            if (!IsInEditMode)
                return;

            CsaEmdUnitManager.Instance.Clear();

            ResetHardwareHandlers();

            CsaEmdUnitManager.Instance.StartReading();
        }

        /// <summary>
        /// Stop the reading.
        /// </summary>
        private void StopReading()
        {
            RemoveHardwareHandlers();

            CsaEmdUnitManager.Instance.StopReading();

            SetReadingMode(false);
        }

        /// <summary>
        /// Stops All the running reading.
        /// </summary>
        private void StopRunningReading()
        {
            if (!_isInReadingRunningMode) return;

            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                StopAutoPlayTestMode();
            }
            else
            {
                StopReading();
            }

            if (FormStatus != FormStatusEnum.New)
                FormStatus = FormStatusEnum.Modified;
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
        /// Update the broadcasting image.
        /// </summary>
        /// <param name="stage"></param>
        private void UpdateBroadcastingStageImage(int stage)
        {
            switch (stage)
            {
                case 0:
                    pictureEditBroadcasting.Image = Resources.BC0;
                    break;
                case 1:
                    pictureEditBroadcasting.Image = Resources.BC1;
                    break;
                case 2:
                    pictureEditBroadcasting.Image = Resources.BC2;
                    break;
                case 3:
                    pictureEditBroadcasting.Image = Resources.BC3;
                    break;
            }
        }
        
        /// <summary>
        /// Show the focused item image, description and reading depends on the selected [ ACTIVE ] tab in the form.
        /// </summary>
        private void ControlItemDetailesAndReadingsDependOnUserAction(bool forceStopReading)
        {
            //Stop all running reading, this case when user changed the selected tab while reading is on.
            //What was happening is "the focus area that reading running on is changed, so its status and flags will not changed, and so the new focus area status and flags will changed".
            StopRunningReading();
            StopAutoTestTimer();

            xtraUserControlItemDetailsMain.ClearImageAndDetails();
            UpdateReading(null, 0);

            //var selectedItems = xtraUserControlItemsNavGridFrequencyTest.GetSelectedItems();

            if (forceStopReading)
            {
                xtraUserControlItemsNavGridFrequencyTest.CancelReading();
            }
            else if (IsInEditMode)
            {
                xtraUserControlItemsNavGridFrequencyTest.StartReading();

                xtraUserControlItemsNavGridFrequencyTest.SetAutoTestPlayMode(_currentTestPlayState == TestPlayStateEnum.Playing);
            }

            SetDetails(xtraUserControlItemsNavGridFrequencyTest.GetSelectedItems(),
                        xtraUserControlItemsNavGridFrequencyTest.TopItems,
                        xtraUserControlItemsNavGridFrequencyTest.BottomItems,
                        xtraUserControlItemsNavGridFrequencyTest.IsTopListFirst,
                        xtraUserControlItemsNavGridFrequencyTest.CurrentTestResultItem,
                        xtraUserControlItemsNavGridFrequencyTest.CurrentIssueItem);
        }

        /// <summary>
        /// Updates the reading value for the gauges.
        /// </summary>
        /// <param name="locationLookup">The location lookup.</param>
        /// <param name="readingValue">The reading value.</param>
        private void UpdateReading(Lookup locationLookup, float readingValue)
        {
            if (IsClosing)
                return;

            xtraUserControlReadingGauge.ReadingValue = readingValue;
            xtraUserControlReadingGauge.LocationLookup = locationLookup;
        }

        /// <summary>
        /// Enable or disable the save functionality, while reading is running.
        /// </summary>
        /// <param name="isReadingRunning">Is reading running.</param>
        private void SetReadingMode(bool isReadingRunning)
        {
            if (_isInReadingRunningMode == isReadingRunning)
                return;

            _isInReadingRunningMode = isReadingRunning;

            SetReadingIndicators(isReadingRunning ? TestBarStateEnum.Reading : _currentTestPlayState == TestPlayStateEnum.Playing
                                           ? TestBarStateEnum.TakeReading
                                           : TestBarStateEnum.Ready, 0);
        }

        /// <summary>
        /// Clears the meter value, and move to next reading row.
        /// </summary>
        private void AfterReadingDone()
        {
            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                xtraUserControlItemsNavGridFrequencyTest.DoAutoTestAction();
                SetReadingIndicators(TestBarStateEnum.Ready, 0);
            }
            SetReadingIndicators(_currentTestPlayState == TestPlayStateEnum.Playing ? TestBarStateEnum.TakeReading : TestBarStateEnum.Ready, 0);
            SetReadingMode(false);
        }

        #endregion

        #region Auto Testing Logic

        /// <summary>
        /// Setup the Auto test timer and progress bar.
        /// </summary>
        private void SetupAutoTestTimerAndProgressBar()
        {
            timerAutoTest.Interval = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime < 500
                                         ? CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime
                                         : 500;
        }

        /// <summary>
        /// Start the auto play test timer.
        /// </summary>
        private void StartAutoTestTimer()
        {
            _autoTestNextReadingTimeOut = 0;

            timerAutoTest.Tick -= timerAutoTest_Tick;

            xtraUserControlItemsNavGridFrequencyTest.SetAutoTestPlayMode(true);

            SetReadingIndicators(TestBarStateEnum.WaitBeforTakeAction,
                                     CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime / 1000f);

            timerAutoTest.Tick += timerAutoTest_Tick;

            timerAutoTest.Enabled = true;
        }

        /// <summary>
        /// Stop the auto test timer.
        /// </summary>
        private void StopAutoTestTimer()
        {
            if (!timerAutoTest.Enabled)
                return;

            timerAutoTest.Tick -= timerAutoTest_Tick;

            timerAutoTest.Enabled = false;

            SetReadingIndicators(TestBarStateEnum.Ready, 0);

            SetReadingIndicators(TestBarStateEnum.Ready, 0);
        }

        /// <summary>
        /// Toggle the auto testing mode.
        /// </summary>
        private void ToggleAutoTestMode()
        {
            //SetAutoPlayTestMode(_currentTestPlayState == TestPlayStateEnum.Paused);

            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                StopAutoPlayTestMode();
                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
            else
            {
                StartAutoPlayTestMode();
            }
        }

        /// <summary>
        /// Starts the auto play.
        /// </summary>
        private void StartAutoPlayTestMode()
        {
            _barButtonItemPausePlay.Glyph = Resources.Test_Pause;
            _currentTestPlayState = TestPlayStateEnum.Playing;

            _barButtonItemPausePlay.Caption = CommonResources.CommonResources.Pause;

            xtraUserControlItemsNavGridFrequencyTest.SetAutoTestPlayMode(true);
        }

        /// <summary>
        /// Stops the auto play.
        /// </summary>
        private void StopAutoPlayTestMode()
        {
            _currentTestPlayState = TestPlayStateEnum.Paused;
            StopReading();
            _barButtonItemPausePlay.Glyph = Resources.Test_Play;
            _barButtonItemPausePlay.Caption = CommonResources.CommonResources.Play;
            StopAutoTestTimer();
        }

        #endregion

        #region Frequency Test Result Values

        /// <summary>
        /// BeforeShowAutoTestDialog
        /// </summary>
        private void BeforeShowAutoTestDialog()
        {
            StopReading();
            CsaEmdUnitManager.Instance.DisposeConnection(
                                           Csa_Instance_Released,
                                           _csaManager_ReadingDone,
                                          _csaManager_MeterValueChanged);
        }

        /// <summary>
        /// AfterShowAutoTestDialog
        /// </summary>
        private void AfterShowAutoTestDialog()
        {
            CsaEmdUnitManager.Instance.ActivateConnection(
                                           Csa_Instance_Released,
                                           _csaManager_ReadingDone,
                                          _csaManager_MeterValueChanged);

            StartReading();
        }

        /// <summary>
        /// Open the auto testing dialog for the passed test result.
        /// </summary>
        private void ShowAutoTestDialog(FrequencyTestResult frequencyTestResult, bool isUserNavigation)
        {
            BeforeShowAutoTestDialog();

            ShowHideOverlay(true);

            var dialogResult = new XtraFormFrequencyResultDialog(frequencyTestResult, isUserNavigation).ShowDialog();

            ShowHideOverlay(false);

            if (dialogResult == DialogResult.Cancel && _currentTestPlayState == TestPlayStateEnum.Playing)
            {
                ToggleAutoTestMode();
            }

            AfterShowAutoTestDialog();
        }

        #endregion

        #region Hardware Handlers

        #region Timer

        /// <summary>
        /// Auto test interval event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerAutoTest_Tick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(timerAutoTest_Tick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _autoTestNextReadingTimeOut += timerAutoTest.Interval;

                var remindingTime = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime -
                                    _autoTestNextReadingTimeOut;

                if (remindingTime > 0 && remindingTime < 1000)
                {
                    timerAutoTest.Interval = remindingTime;
                }

                var timeOut = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime;

                var secondToWait = (timeOut - _autoTestNextReadingTimeOut) / 1000f;

                SetReadingIndicators(TestBarStateEnum.WaitMoving, secondToWait);

                var nextActionName = xtraUserControlItemsNavGridFrequencyTest.GetNextActionName();
                xtraUserControlItemsNavGridFrequencyTest.SetReadingStatusBarMode(TestBarStateEnum.WaitBeforTakeAction, nextActionName, secondToWait);

                SetReadingStatusBarMode(TestBarStateEnum.WaitBeforTakeAction, nextActionName, secondToWait);

                if (_autoTestNextReadingTimeOut >= timeOut)
                {
                    timerAutoTest.Tick -= timerAutoTest_Tick;
                    timerAutoTest.Enabled = false;
                    _autoTestNextReadingTimeOut = 0;
                    AfterReadingDone();
                }
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

        #region CSA Manager
        
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
                    
                    xtraUserControlItemDetailsMain.ClearImageAndDetails();
                    xtraUserControlReadingGauge.Clear();

                }
            }
        }

        /// <summary>
        /// Handel the CSA released  event.
        /// </summary>
        /// <param name="sender">The sender as CSA manager.</param>
        private void Csa_Instance_Released(object sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
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
                lock (_connectionLooker)
                {
                    if (!_isWaitingCsaRealsedToTakeNewReading || !CsaEmdUnitManager.Instance.HasReading) return;

                    xtraUserControlItemsNavGridFrequencyTest.ResponseReadingValue = _lastItemTestingReadingValue;
                    if (_currentTestPlayState == TestPlayStateEnum.Playing)
                    {
                        StartAutoTestTimer();
                    }
                    else
                    {
                        AfterReadingDone();
                    }

                    xtraUserControlItemsNavGridFrequencyTest.StartReading();

                    _isWaitingCsaRealsedToTakeNewReading = false;
                }
            }
        }

        /// <summary>
        /// Handel the meter value changed.
        /// </summary>
        private void _csaManager_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_MeterValueChanged(sender, reading, min, max);

            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ReadingDone), sender,
                           reading, min, max, fall, rise);

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
                    if (!CsaEmdUnitManager.Instance.HasReading) return;

                    _isWaitingCsaRealsedToTakeNewReading = true;

                    if (_currentTestPlayState == TestPlayStateEnum.Paused)
                        SetReadingIndicators(TestBarStateEnum.Ready, 0);

                    StopReading();

                    SetReadingMode(false);

                    _lastItemTestingReadingValue = reading;

                    SetReadingIndicators(TestBarStateEnum.WaitingToRelease, 0);
                }

            }
        }

        /// <summary>
        /// Handel the reading done event.
        /// </summary>
        private void _csaManager_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (IsHandleCreated && InvokeRequired)
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
                lock (_connectionLooker)
                {
                    if (!CsaEmdUnitManager.Instance.HasReading || !IsInEditMode) return;

                    SetReadingMode(true);

                    UpdateReading(null, reading);

                    if (timerAutoTest.Enabled)
                    {
                        StopAutoTestTimer();
                    }
                    SetReadingIndicators(TestBarStateEnum.Reading, 0);
                }
            }

        }

        /// <summary>
        /// Instance_Detecting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
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

        #region NavGrid

        /// <summary>
        /// Handel the adding a test result.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="items">The items.</param>
        private List<Item> xtraUserControlItemsNavGridFrequencyTest_AddToTestResults(XtraUserControlItemsNavGrid sender, BindingList<Item> items)
        {
            AddToTestResults(items);
            return null;
        }

        /// <summary>
        /// Handle Reading Request Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="itemsToBroadcast"></param>
        private void xtraUserControlItemsNavGridFrequencyTest_ReadingRequest(XtraUserControlItemsNavGrid sender, List<Item> itemsToBroadcast)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlItemsNavGrid.OnReadingRequest(xtraUserControlItemsNavGridFrequencyTest_ReadingRequest), sender, itemsToBroadcast);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                StopReading();
                StartReading();
            }
        }

        /// <summary>
        /// Handle Reading Request Cancel Event
        /// </summary>
        /// <param name="sender"></param>
        private void xtraUserControlItemsNavGridFrequencyTest_CancelReadingRequest(XtraUserControlItemsNavGrid sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnCancelReadingRequest(
                            xtraUserControlItemsNavGridFrequencyTest_CancelReadingRequest), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                StopReading();
                StartReading();
            }
        }

        /// <summary>
        /// Handle the request to refresh image and detail
        /// </summary>
        /// <param name="sender"></param>
        private void xtraUserControlItemsNavGridFrequencyTest_RefreshDetailsAndImageNavGrid(XtraUserControlItemsNavGrid sender)
        {
            //if (RefreshDetailsAndImageIssue == null)
            //    return;

            //RefreshDetailsAndImageIssue(sender);
        }

        /// <summary>
        /// Handle the request to set the image control image ignore state
        /// </summary>
        /// <param name="ignoreState"></param>
        private void xtraUserControlItemsNavGridFrequencyTest_SetImageIgnoreState(bool ignoreState)
        {
            //if (SetImageIgnoreState == null)
            //    return;

            //SetImageIgnoreState(ignoreState);
        }

        /// <summary>
        /// Handle the selectitemchanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        /// <param name="reading"></param>
        private void xtraUserControlItemsNavGridFrequencyTest_SelectedItemChanged(XtraUserControlItemsNavGrid sender, Item item, int reading)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnSelectedItemChanged(
                            xtraUserControlItemsNavGridFrequencyTest_SelectedItemChanged), sender, item, reading);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!sender.IsSwitching || (sender.ImageRefreshNeeded))
                {
                    SetDetails(sender.GetSelectedItems(),
                                    sender.TopItems,
                                    sender.BottomItems,
                                    sender.IsTopListFirst,
                                    sender.CurrentTestResultItem,
                                    sender.CurrentIssue == null ? null : sender.CurrentIssueItem);
                    sender.ImageRefreshNeeded = false;
                }

                UpdateReading(null, reading);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Logic

        #region Frequency Updates

        /// <summary>
        /// Show the folder selection dialog for frequency update
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ShowFolderSelectDialog(string path)
        {
            var newPath = string.Empty;

            var dialog = new FolderBrowserDialog { SelectedPath = path };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var directoryName = new DirectoryInfo(dialog.SelectedPath).Name.Trim();

                if (dialog.SelectedPath != string.Empty)
                {
                    if (directoryName == StaticKeys.FrequenciesFolderName)
                    {
                        newPath = dialog.SelectedPath;    
                    }
                    else if(UiHelperClass.ShowConfirmQuestion("The foot folder for frequencies should be selected to perform an update, would you like to select a different folder?") == DialogResult.Yes)
                    {
                        newPath = ShowFolderSelectDialog(path);
                    }
                }
            }

            return newPath;
        }

        /// <summary>
        /// Perform checks for frequency update and performs the update
        /// </summary>
        private void CheckForFrequenciesUpdates(bool useDefaultFolder)
        {
            if (UiHelperClass.ShowConfirmQuestion("Checking for frequency updates might take some time. Are you sure?") == DialogResult.Yes)
            {
                var frequencyTestItemsPathSetting = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FrequencyTestItemsPath) });
                frequencyTestItemsPathSetting.Value.ToString();

                var frequencyItemsPath = string.Empty;
                var folderExists = false;
                var showFolderSelectionMsg = false;

                if (string.IsNullOrEmpty(frequencyItemsPath))
                {
                    frequencyItemsPath = _defaultFrequencyItemsPath;
                }

                if (useDefaultFolder)
                {
                    if (Directory.Exists(frequencyItemsPath))
                    {
                        folderExists = true;
                    }
                    else
                    {
                        if (UiHelperClass.ShowConfirmQuestion("The frequencies folder couldn't be found, would you like to select a different folder?") == DialogResult.Yes)
                        {
                            showFolderSelectionMsg = true;//Ask the user for target folder
                        }
                    }
                }
                else
                {
                    showFolderSelectionMsg = true;//Ask the user for target folder
                }

                //Ask the user for target folder
                if (showFolderSelectionMsg)
                {
                    var newPath = ShowFolderSelectDialog(frequencyItemsPath);

                    if (newPath != string.Empty)
                    {
                        frequencyItemsPath = newPath;
                        folderExists = true;
                    }
                }

                var directoryName = new DirectoryInfo(frequencyItemsPath).Name.Trim();

                if (folderExists && directoryName == StaticKeys.FrequenciesFolderName)
                {
                    _xtraFormFrequenciesUpdate = new XtraFormFrequenciesUpdate { StartPath = frequencyItemsPath };
                    _xtraFormFrequenciesUpdate.ShowDialog();

                    if (showFolderSelectionMsg)
                    {
                        frequencyTestItemsPathSetting.Value = frequencyItemsPath;
                        _settingsManager.Save(frequencyTestItemsPathSetting);
                    }

                    var updateDetails = string.Empty;
                    updateDetails += "Total files & folders count =" + _xtraFormFrequenciesUpdate.CurrentUpdateFoldersFilesCount + Environment.NewLine;
                    if (!_xtraFormFrequenciesUpdate.AddedItems.Any() && !_xtraFormFrequenciesUpdate.UpdatedItems.Any())
                    {
                        updateDetails += Environment.NewLine;
                        updateDetails += "No updates were found, the frequency items in Vital are up to date.";
                    }
                    else
                    {
                        if (_xtraFormFrequenciesUpdate.AddedItems.Any())
                        {
                            updateDetails += Environment.NewLine;
                            updateDetails += Environment.NewLine;
                            updateDetails += _xtraFormFrequenciesUpdate.AddedItems.Count + " items were added:" + Environment.NewLine;
                            updateDetails += Environment.NewLine;
                            updateDetails += string.Join(Environment.NewLine, _xtraFormFrequenciesUpdate.AddedItems);
                        }

                        if (_xtraFormFrequenciesUpdate.UpdatedItems.Any())
                        {
                            updateDetails += Environment.NewLine;
                            updateDetails += "-----------------------------------------------------";
                            updateDetails += Environment.NewLine;
                            updateDetails += _xtraFormFrequenciesUpdate.UpdatedItems.Count + " items were updated:" + Environment.NewLine;
                            updateDetails += Environment.NewLine;
                            updateDetails += string.Join(Environment.NewLine, _xtraFormFrequenciesUpdate.UpdatedItems);
                        }
                    }

                    UiHelperClass.ShowdDescriptionMessage("Frequency Update Results", "Frequency Update Completed", updateDetails);
                    ItemsCacheHelper.ClearCache();//Clear cache so when items are loaded again the list will include new added items
                    RebindNavGrid();
                }
                
            }
            
        }

        #endregion

        #region Results Logic

        /// <summary>
        /// Generates a test result from frequency test result
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private TestResult GenerateTestResult(Item item)
        {
            var testResultToAdd = new TestResult
            {
                TestIssue = xtraUserControlItemsNavGridFrequencyTest.CurrentIssue,
                DateTime = DateTime.Now,
                Item = item,
                IsSelected = true,
                TestResultFactors = new BindingList<TestResultFactor>(),
                Parent = xtraUserControlItemsNavGridFrequencyTest.TestResults.FirstOrDefault(tr => tr.IsCurrent) ??
                         xtraUserControlItemsNavGridFrequencyTest.TestResults.FirstOrDefault(),
                Id = xtraUserControlItemsNavGridFrequencyTest.TestResults.Count > 0 ? NextResultId : 1,
                TempImprintingId = GetNextTestResultId()
            };

            testResultToAdd.SetUserAndDates();

            return testResultToAdd;
        }

        /// <summary>
        /// Generates test results from frequency test results to be used in NavGrid
        /// </summary>
        private void GenerateTestResults()
        {
            foreach (var result in FrequencyTestObject.FrequencyTestResults)
            {
                var testResultToAdd = GenerateTestResult(result.Item);
                xtraUserControlItemsNavGridFrequencyTest.TestResults.Add(testResultToAdd);
            }
        }

        /// <summary>
        /// Add Passed items to the test results.
        /// </summary>
        /// <param name="items">The items.</param>
        private void AddToTestResults(BindingList<Item> items)
        {
            if (!_isEditable
                || items.Count == 0
                || _deletedFrequencyTestResults == null)
                return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            foreach (var selectedItem in items)
            {
                if (selectedItem == null) continue;

                var testResultToAdd = GenerateTestResult(selectedItem);
                xtraUserControlItemsNavGridFrequencyTest.TestResults.Add(testResultToAdd);

                var result = new FrequencyTestResult { Item = selectedItem, FrequencyTest = FrequencyTestObject, Notes = selectedItem.Notes };

                var itemHasChilds = ProductSeatsHelper.ItemHasChilds(selectedItem, false);

                //Based on request we added a check to prevent showing the dosage dialog for items that has childs and we only show it for sub
                //items where childs exist, the user can still open the dosage dialog when they need it though.
                if (!itemHasChilds)
                {
                    ShowAutoTestDialog(result, _currentTestPlayState == TestPlayStateEnum.Paused);    
                }

                xtraUserControlFrequencyTestResults.AddResult(result);

                xtraUserControlFrequencyTestResults.RefreshResultsGrid();

                //InvokeUpdateOnTestResults(this);
                xtraUserControlFrequencyTestResults.View.FocusedRowHandle =
                        xtraUserControlFrequencyTestResults.View.ViewRowHandleToDataSourceIndex(
                            xtraUserControlItemsNavGridFrequencyTest.TestResults.IndexOf(testResultToAdd));

                //PerformAutomatedResultActions(testResultToAdd, selectedItem);
            }

            if (xtraUserControlItemsNavGridFrequencyTest.CurrentStep == 1 && xtraUserControlItemsNavGridFrequencyTest.IsStepAutomated)
                xtraUserControlItemsNavGridFrequencyTest.ForceStepIncrement = true;

            xtraUserControlItemsNavGridFrequencyTest.RefreshItems(true, true);

            //Adding the items of the automatic step [GoTo] as a visited items.
            AddGotoItemsToVisitedList();

            //Show the image of the added test result so technician can see it after it gets added
            //if (lastTestResultAdded != null)
            //{
            //    InvokeSelectedItemChanged(this, true, lastTestResultAdded.Item, 0);
            //}

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Adds the items in a temp list to the visited items.
        /// </summary>
        private void AddGotoItemsToVisitedList()
        {
            foreach (var visitedItem in _tempVisitedItems)
            {
                xtraUserControlItemsNavGridFrequencyTest.VisitedItems.Add(visitedItem);
            }

            _tempVisitedItems.Clear();
        }

        /// <summary>
        /// Delete passed frquency test result.
        /// </summary>
        /// <param name="result"></param>
        private void DeleteFrequencyTestResult(FrequencyTestResult result)
        {
            if (result != null && result.Id != 0)
                _deletedFrequencyTestResults.Add(result);

            var testResult = xtraUserControlItemsNavGridFrequencyTest.TestResults.FirstOrDefault(tr => tr.Item.Id == result.Item.Id);
            testResult.IsSelected = false;

            xtraUserControlItemsNavGridFrequencyTest.UpdateAfterDeletion(testResult.Item);

            var children = xtraUserControlItemsNavGridFrequencyTest.TestResults.Where(tr => tr.SelectedParentResultId == testResult.Id);

            foreach (var child in children)
            {
                child.SelectedParent = testResult.SelectedParent;
            }

            xtraUserControlFrequencyTestResults.RefreshResultsGrid();

            //InvokeUpdateOnTestResults(this);

            var currentTestResult = xtraUserControlItemsNavGridFrequencyTest.GetCurrentTestResult();

            //after deleting the test result, the item should appear again in the items grid.
            if (testResult.Item != null && currentTestResult != null && testResult.Parent != null && testResult.Parent.Id == currentTestResult.Id)
            {
                /*Step back to keep the hilighting on the current step, since the setItems which is called after
                 * the AddItemAfterDeletion, will increment the current step by (1)*/
                if (xtraUserControlItemsNavGridFrequencyTest.CurrentStep > 1)
                    xtraUserControlItemsNavGridFrequencyTest.CurrentStep -= 1;
            }
            xtraUserControlItemsNavGridFrequencyTest.UpdateTopAndBottomItemsLists();
        }

        /// <summary>
        /// Fill the frquency test result lists depends on the result type.
        /// </summary>
        private void FillFrequencyTestResultLists()
        {
            if (FrequencyTestObject.FrequencyTestResults == null)
                FrequencyTestObject.FrequencyTestResults = new BindingList<FrequencyTestResult>();

            _frequencyTestResultsResults = FrequencyTestObject.FrequencyTestResults.ToBindingList();
        }

        /// <summary>
        /// Collect the frquency test results list to the main frquency test results list inside the frquency test object. 
        /// </summary>
        private void CollectFrequencyTestResults()
        {
            var collectedFrequencyTestResult = new List<FrequencyTestResult>();

            collectedFrequencyTestResult.AddRange(_frequencyTestResultsResults);
            collectedFrequencyTestResult.AddRange(_deletedFrequencyTestResults);

            FrequencyTestObject.FrequencyTestResults = collectedFrequencyTestResult.ToBindingList();
        }

        #endregion

        #region Details Area

        /// <summary>
        /// Binds the images and its description.
        /// </summary>
        private void SetDetails(Item item)
        {
            //_currentSelectedItem = item;
            SetItemTempImage(item);
            xtraUserControlItemDetailsMain.SetDetails(item);
        }

        /// <summary>
        /// Binds the images and its description.
        /// </summary>
        private void SetDetails(BindingList<Item> selectedItems,
                                BindingList<Item> topItems,
                                BindingList<Item> bottomItems,
                                bool topItemsHightlighted,
                                Item parentItem,
                                Item issueItem,
                                bool clearOnNull = false)
        {
            SetItemsTempImages(selectedItems);
            SetItemsTempImages(topItems);
            SetItemsTempImages(bottomItems);
            SetItemsTempImages(selectedItems);
            SetItemTempImage(parentItem);
            SetItemTempImage(issueItem);
            xtraUserControlItemDetailsMain.SetDetails(selectedItems, topItems, bottomItems, topItemsHightlighted, parentItem, issueItem);
        }

        /// <summary>
        /// Set temp images for items
        /// </summary>
        /// <param name="items"></param>
        private void SetItemsTempImages(BindingList<Item> items)
        {
            foreach (var item in items)
            {
                SetItemTempImage(item);
            }
        }

        /// <summary>
        /// Set temporary image for item
        /// </summary>
        /// <param name="item"></param>
        private void SetItemTempImage(Item item)
        {
            if (item != null && item.ItemDetail == null)
            {
                var file = _frequencyTRLFiles.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == item.Name);

                if (file != null)
                {
                    var info = new FileInfo(file);
                    var imageFile = info.Directory + @"\" + item.Name + ".trl";
                    var image = File.Exists(imageFile) ? File.ReadLines(imageFile).First().Replace("image=", "") : string.Empty;
                    var imagePath = Path.Combine(_defaultFrequencyItemsPath, "images", image);

                    item.ItemDetail = new ItemDetails() { Image = new Image() { Path = imagePath } };
                }
            }
        }

        #endregion

        #region CRUD Logic

        /// <summary>
        /// Posts the values in the controls that are not yet committed to the dataSource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public override void PostValues()
        {
            textEditName.DoValidate();
            memoEditNotes.DoValidate();
            xtraUserControlFrequencyTestResults.PostValues();
        }

        /// <summary>
        /// Uses the Tests manager to save the frquency test.        
        /// </summary>
        public override bool Save(bool isClosing)
        {
            try
            {
                SetImageAreaSettings();

                PostValues();

                CollectFrequencyTestResults();

                if (!FrequencyTestObject.Validate())
                    return false;

                return _frequencyTestsManager.SaveFrequencyTest(FrequencyTestObject).IsSucceed;
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
            ControlItemDetailesAndReadingsDependOnUserAction(false);
            _deletedFrequencyTestResults.Clear();
        }

        /// <summary>
        /// Perform after revert action.
        /// </summary>
        public override void AfterRevertAction()
        {
            ControlItemDetailesAndReadingsDependOnUserAction(false);
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

                var result = _frequencyTestsManager.DeleteFrequencyTest(FrequencyTestObject);

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

                FrequencyTestObject = _frequencyTestsManager.GetFrequencyTestById(new SingleItemFilter { ItemId = FrequencyTestObject.Id });

                Rebind();

                IsLoaded = true;

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

        #endregion

        #region Printing

        /// <summary>
        /// Prints the report
        /// </summary>
        /// <param name="isPreview"></param>
        private void Print(bool isPreview)
        {
            if (_isEditable && !SaveAction())
                return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.PreparingReportMessage);

            var report = new XtraReportFrequencyTest()
            {
                PatientName = { Value = FrequencyTestObject.Patient.FirstName + " " + FrequencyTestObject.Patient.LastName },
                bindingSourcePatient = { DataSource = FrequencyTestObject.Patient },
                bindingSourceFrequencyTest = { DataSource = FrequencyTestObject },
                HidePatientName = { Value = xtraUserControlPrintingOptionsMain.HidePatientName },
                HideLogo = { Value = xtraUserControlPrintingOptionsMain.HideLogo },
            };

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

        #region Handlers

        #region General Handlers

        /// <summary>
        /// Handle the propriety changed event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        private void FrequencyTestObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(FrequencyTestObject_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                switch (FrequencyTestObject.ObjectState)
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
        private void frm_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// Handel the key down on the form.
        /// </summary>
        private void frmFrequencyTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(frmFrequencyTest_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //if (e.KeyCode == Keys.F6)
                //{
                //    if (IsSplitSwitchAndStarredModeValid && simpleButtonSwitch.Enabled)
                //    {
                //        SwitchSelection();
                //    }
                //}
                //else if (e.KeyCode == Keys.F7)
                //{
                //    if (simpleButtonReload.Enabled)
                //    {
                //        ReloadItems();
                //    }
                //}
                //else if (e.KeyCode == Keys.F5 && IsSplitSwitchAndStarredModeValid)
                //{
                //    if (simpleButtonSplit.Enabled)
                //    {
                //        SplitItems();
                //    }
                //}
                //else if (e.KeyCode == Keys.F4 && !xtraUserControlFrequencyTestResults.IsFocusedView)
                //{
                //    gridViewItems.SelectAll();
                //}
                //else if (e.KeyCode == Keys.Insert || (e.Control && e.KeyCode == Keys.I) || e.KeyCode == Keys.Oemtilde)
                //{
                //    if (simpleButtonAddToResults.Enabled)
                //    {
                //        gridColumnNotes.OptionsColumn.AllowEdit = false;
                //        AddToResults();
                //        gridColumnNotes.OptionsColumn.AllowEdit = true;
                //    }
                //}
                //else if (e.KeyCode == Keys.F8)
                //{
                //    if (simpleButtonClearSelection.Enabled && !xtraUserControlFrequencyTestResults.IsFocusedView)
                //    {
                //        ClearSelection();
                //    }
                //}
                //else if (e.KeyCode == Keys.Home)
                //{
                //    gridViewItems.Focus();
                //    gridViewItems.MoveFirst();
                //    gridViewItems.MakeRowVisible(gridViewItems.FocusedRowHandle);
                //}
                //else if (e.KeyCode == Keys.End)
                //{
                //    gridViewItems.Focus();
                //    gridViewItems.MoveLastVisible();
                //}
                //else if (e.KeyCode == Keys.F12)
                //{
                //    if (checkButtonOneByOneMode.Enabled)
                //        checkButtonOneByOneMode.Checked = !checkButtonOneByOneMode.Checked;
                //}
                //else if (e.KeyCode == Keys.F11)
                //{
                //    if (checkButtonStarredMode.Enabled && IsSplitSwitchAndStarredModeValid)
                //        checkButtonStarredMode.Checked = !checkButtonStarredMode.Checked;
                //}
                //else if (e.KeyCode == Keys.F3)
                //{
                //   if(xtraTabControlFrequencyTest.SelectedTabPage == xtraTabPageIngredients && simpleButtonSetDetails.Enabled)
                //       ShowSetDetailsAutoTestDialog();
                //}
                xtraUserControlItemsNavGridFrequencyTest.NavGrid_KeyDown(sender, e);
            }
        }

        #endregion

        #region UiHandlers

        /// <summary>
        /// Prevent the showing of the PopupMenu for the dock panel, (this PopupMenu contains close option that we need to disable)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockManagerMain_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraBars.Docking.PopupMenuShowingEventHandler(dockManagerMain_PopupMenuShowing),
                        sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

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
        /// Handle the list change to update the form status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subListFrequencyTestResults_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(subListFrequencyTestResults_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OnCellChanging();
            }
        }

        /// <summary>
        /// Handel CellChanging for results.
        /// </summary>
        private void xtraUserControlFrequencyTestResults_CellChanging()
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlFrequencyTestResults.CellchangingEventHandler(xtraUserControlFrequencyTestResults_CellChanging));
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OnCellChanging();
            }
        }

        /// <summary>
        /// Handel the deleting for frquency test result.
        /// </summary>
        private void xtraUserControlFrequencyTestResults_FrequencyTestReseltDeleted(FrequencyTestResult result)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlFrequencyTestResults.FrequencyTestReseltDeletedEventHandler(xtraUserControlFrequencyTestResults_FrequencyTestReseltDeleted));
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                DeleteFrequencyTestResult(result);
            }
        }

        /// <summary>
        /// Handel for the broadcast request from the results grids.
        /// </summary>
        /// <param name="results"></param>
        private void xtraUserControlFrequencyTestResults_BroadcastRequest(List<FrequencyTestResult> results)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlFrequencyTestResults.BroadcastRequestEventHandler(xtraUserControlFrequencyTestResults_BroadcastRequest), results);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //Broadcast();
            }
        }

        /// <summary>
        /// Handle selected item changed event
        /// </summary>
        /// <param name="item"></param>
        private void xtraUserControlFrequencyTestResults_SelectedItemChanged(Item item)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlFrequencyTestResults.OnSelectedItemChanged(xtraUserControlFrequencyTestResults_SelectedItemChanged), item);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetDetails(item);
            }
        }

        /// <summary>
        /// Handel ShowAutoTestDialog request.
        /// </summary>
        private void xtraUserControlFrequencyTestResults_ShowAutoTestDialog(FrequencyTestResult result)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlFrequencyTestResults.ShowAutoTestDialogEventHandler(xtraUserControlFrequencyTestResults_ShowAutoTestDialog), result);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowAutoTestDialog(result, true);
            }
        }

        #endregion

        #endregion
    }
}