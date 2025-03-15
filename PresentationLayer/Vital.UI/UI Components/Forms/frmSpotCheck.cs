using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTab;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.SpotChecks;
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
    public partial class frmSpotCheck : VitalBaseForm
    {
        #region PrivateMembers

        #region GeneralPrivateMemebers

        private BarButtonItem _barButtonItemPausePlay;

        private SpotCheckAutoTestMode _currentAutoTestMode;
        private SpotCheckAutoTestMode _lastMtesSpotCheckAutoTestMode;
        private SpotCheckAutoTestMode _lastIngSpotCheckAutoTestMode;

        private SpotCheckManager _spotCheckManager;
        private LookupsManager _lookupsManager;
        private ItemsManager _itemsManager;
        private SettingsManager _settingsManager;

        private bool _isCsaUnitConnected;
        private object _connectionLooker;
        private int _broadcastingStage;
        private int _autoTestNextReadingTimeOut;

        private Lookup _ivSheetLookup;
        private Lookup _capsolTLookup;
        private Lookup _dmpsLookup;
        private Lookup _mineralsLookup;

        private BindingList<SpotCheckResult> _ivSheetSpotCheckResults;
        private BindingList<SpotCheckResult> _capsolTSpotCheckResults;
        private BindingList<SpotCheckResult> _dmpsSpotCheckResults;
        private BindingList<SpotCheckResult> _mineralsSpotCheckResults;
        private BindingList<Item> _ivSheetItems;
        private BindingList<Item> _mineralsItems;
        private BindingList<Item> _capsolTItems;
        private BindingList<Item> _dmpsItems;
        private List<SpotCheckResult> _deletedSpotCheckResults;

        private int _leftLookupId;
        private int _rightLookupId;
        private int _yesLookupId;

        private int _lastReadingValue;

        private bool _isEditable;

        private TestPlayStateEnum _currentTestPlayState;
        private SpotCheckReadingPlayType _currentSpotCheckReadingPlayType;
        private XtraUserControlSpotCheckResults _cureentSpotCheckResultsUserControl;
        private Lookup _currentResultType;

        #endregion

        #region NavigationVariables.

        private int _part = 1;
        private int _partHilight = 1;

        private bool _isFirstSwitch = true;
        private bool _isTopListFirst = true;
        private BindingList<Item> _topItems;
        private BindingList<Item> _bottomItems;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the spot check object
        /// </summary>
        public SpotCheck SpotCheckObject { get; set; }

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
        /// Gets if split and switch mode & starred valid for selected tab page.
        /// </summary>
        private bool IsSplitSwitchAndStarredModeValid
        {
            get
            {
                return xtraTabControlSpotCheck.SelectedTabPage != xtraTabPageDmps && xtraTabControlSpotCheck.SelectedTabPage != xtraTabPageCapsolT;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="spotCheck"></param>
        public frmSpotCheck(SpotCheck spotCheck)
        {
            InitializeComponent();
            SpotCheckObject = spotCheck;
            CustomeInitializeComponent();
        }

        #endregion

        #region Methods

        #region InitializeAndBinding

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

            AddBarButtonItem("barButtonItemSettings", "Settings", true, Resources.SettingsSmall, 7,
                             7, true,
                             new BarShortcut((Keys.Alt | Keys.S)), true,
                             "Settings",
                             "Click this button to access the application settings.\r\n",
                             "You can use Alt+S to access the application settings.", true);

            AddBarButtonItem("barButtonItemHelp", "Help", true, Resources.HelpSmall, 8,
                             8, true,
                             null, true,
                             "Help",
                             "Click this button to access the application help.\r\n",
                             string.Empty, true);

            AddBarButtonItem("barButtonItemHotKeys", "Shortcuts", true, Resources.keyIcon, 9,
                             9, false,
                             null, true,
                             "Shortcuts",
                             "Click this button to access the application shortcuts.\r\n",
                             string.Empty, true);

            AddBarButtonItem("barButtonItemFeedback", "Feedback", true, Resources.feedback_small, 10,
                             10, true,
                             null, true,
                             "Feedback",
                             "Report an issue or make a suggestion.\r\n",
                             string.Empty, true);

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

            memoEditCapsolTNotes.Properties.ReadOnly = isReadOnly;
            memoEditMtesNotes.Properties.ReadOnly = isReadOnly;
            memoEditIngredientsNotes.Properties.ReadOnly = isReadOnly;


            textEditMondayNotes.Properties.ReadOnly = isReadOnly;
            textEditTusedayNotes.Properties.ReadOnly = isReadOnly;
            textEditWednesdayNotes.Properties.ReadOnly = isReadOnly;

            checkEditMineralsIvPush.Properties.ReadOnly = isReadOnly;
            checkEditMineralsOne.Properties.ReadOnly = isReadOnly;
            checkEditMineralsThree.Properties.ReadOnly = isReadOnly;

            spinEditNoOfBags.Properties.ReadOnly = isReadOnly;
            spinEditNoPerWeeks.Properties.ReadOnly = isReadOnly;

            spinEditMineralsDextroseCC.Properties.ReadOnly = isReadOnly;
            spinEditMineralsDextroseCCPriority.Properties.ReadOnly = isReadOnly;
            spinEditMineralsNormalSalineCC.Properties.ReadOnly = isReadOnly;
            spinEditMineralsNormalSalineCCPriority.Properties.ReadOnly = isReadOnly;
            spinEditMineralsSterlieWaterCc.Properties.ReadOnly = isReadOnly;
            spinEditMineralsSterlieWaterCCPriority.Properties.ReadOnly = isReadOnly;

            spinEditMineralsIVPerMin.Properties.ReadOnly = isReadOnly;
            spinEditMineralsPerWeek.Properties.ReadOnly = isReadOnly;
            spinEditMineralsEDTA.Properties.ReadOnly = isReadOnly;

            xtraUserControlSpotCheckResultsMtes.SetEditMode(isReadOnly);
            xtraUserControlSpotCheckResultsDmps.SetEditMode(isReadOnly);
            xtraUserControlSpotCheckResultsCapsolT.SetEditMode(isReadOnly);
            xtraUserControlSpotCheckResultsIngredients.SetEditMode(isReadOnly);

            gridViewItems.OptionsBehavior.ReadOnly = isReadOnly;
            simpleButtonReload.Enabled = !isReadOnly;

            simpleButtonSetDetails.Enabled = !isReadOnly;

            xtraUserControlReadingGauge.Clear();

            SetModeButtonsSatatus();
            SetPrintAndDetailsButtonStatus();

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
        /// Set print and details buttons status based on the count of results.
        /// </summary>
        private void SetPrintAndDetailsButtonStatus()
        {
            if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageMtes)
            {
                simpleButtonPrintMTE.Enabled = simpleButtonPrintPreviewMTE.Enabled = _ivSheetSpotCheckResults.Any();
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageCapsolT)
            {
                simpleButtonPrintCapsolT.Enabled = simpleButtonPrintPreviewCapsolT.Enabled = _capsolTSpotCheckResults.Any();
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients)
            {
                simpleButtonPrintMinerals.Enabled = simpleButtonPrintPreviewMinerals.Enabled = simpleButtonSetDetails.Enabled = _mineralsSpotCheckResults.Any();
                simpleButtonSetDetails.Enabled &= _isEditable;
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageDmps)
            {
                simpleButtonPrintDMPS.Enabled = simpleButtonPrintPreviewDMPS.Enabled = _dmpsSpotCheckResults.Any();
            }
        }

        /// <summary>
        /// Performs some steps to initialize the form.
        /// </summary>
        public override void PerformSpecificIntializationSteps()
        {
            _connectionLooker = new object();

            _spotCheckManager = new SpotCheckManager();
            _lookupsManager = new LookupsManager();
            _itemsManager = new ItemsManager();
            _settingsManager = new SettingsManager();

            _deletedSpotCheckResults = new List<SpotCheckResult>();

            _currentTestPlayState = TestPlayStateEnum.Paused;
            _currentAutoTestMode = SpotCheckAutoTestMode.SplitAndSwitch;
            _lastIngSpotCheckAutoTestMode = SpotCheckAutoTestMode.Starred;
            _lastMtesSpotCheckAutoTestMode = SpotCheckAutoTestMode.Starred;

            _topItems = new BindingList<Item>();
            _bottomItems = new BindingList<Item>();

            var isNew = SpotCheckObject.Id == 0;

            if (isNew)
            {
                SpotCheckObject.MineralsThree = true;
                SpotCheckObject.MineralsNormalSalineCc = 500;
            }

            InitSpotCheckObject(isNew);
            SetFormStatus(isNew);
            FillLookUps();
            SetDefaultMeterPositionSettings();
            SetDefaultFornts();
            SetupAutoTestTimerAndProgressBar();
            SetFormTitle(string.Format(StaticKeys.SpotCheckTitile, SpotCheckObject.Patient.FirstName, SpotCheckObject.Patient.LastName));
            
            if(isNew)
                SetAutoPlayTestMode(true);
        }


        /// <summary>
        /// Set the controls font.
        /// </summary>
        private void SetFonts(float fontSize)
        {
            xtraUserControlSpotCheckResultsMtes.GridFont = UiHelperClass.GetFontWithSize(fontSize);
            xtraUserControlSpotCheckResultsDmps.GridFont = UiHelperClass.GetFontWithSize(fontSize);
            xtraUserControlSpotCheckResultsCapsolT.GridFont = UiHelperClass.GetFontWithSize(fontSize);
            xtraUserControlSpotCheckResultsIngredients.GridFont = UiHelperClass.GetFontWithSize(fontSize);
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            SpotCheckObject.PropertyChanged += SpotCheckObject_PropertyChanged;

            _ivSheetSpotCheckResults.RaiseListChangedEvents = true;
            _ivSheetSpotCheckResults.ListChanged += subListSpotCheckResults_ListChanged;

            _mineralsSpotCheckResults.RaiseListChangedEvents = true;
            _mineralsSpotCheckResults.ListChanged += subListSpotCheckResults_ListChanged;

            _capsolTSpotCheckResults.RaiseListChangedEvents = true;
            _capsolTSpotCheckResults.ListChanged += subListSpotCheckResults_ListChanged;

            _dmpsSpotCheckResults.RaiseListChangedEvents = true;
            _dmpsSpotCheckResults.ListChanged += subListSpotCheckResults_ListChanged;

        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            SpotCheckObject.PropertyChanged -= SpotCheckObject_PropertyChanged;

            _ivSheetSpotCheckResults.RaiseListChangedEvents = false;
            _ivSheetSpotCheckResults.ListChanged -= subListSpotCheckResults_ListChanged;

            _mineralsSpotCheckResults.RaiseListChangedEvents = false;
            _mineralsSpotCheckResults.ListChanged -= subListSpotCheckResults_ListChanged;

            _capsolTSpotCheckResults.RaiseListChangedEvents = false;
            _capsolTSpotCheckResults.ListChanged -= subListSpotCheckResults_ListChanged;

            _dmpsSpotCheckResults.RaiseListChangedEvents = false;
            _dmpsSpotCheckResults.ListChanged -= subListSpotCheckResults_ListChanged;
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            FillSpotCheckResultLists();
            ProcessSelectedTabPageChanged();

            UiHelperClass.BindControl(textEditDate, SpotCheckObject, () => SpotCheckObject.CreationDateTime);
            UiHelperClass.BindControl(textEditName, SpotCheckObject, () => SpotCheckObject.Name);

            UiHelperClass.BindControl(memoEditMtesNotes, SpotCheckObject, () => SpotCheckObject.Notes);
            UiHelperClass.BindControl(memoEditCapsolTNotes, SpotCheckObject, () => SpotCheckObject.CapsoleTnotes);
            UiHelperClass.BindControl(memoEditIngredientsNotes, SpotCheckObject, () => SpotCheckObject.IngredientsNotes);

            UiHelperClass.BindControl(textEditMondayNotes, SpotCheckObject, () => SpotCheckObject.MondayNotes);
            UiHelperClass.BindControl(textEditTusedayNotes, SpotCheckObject, () => SpotCheckObject.TuesdayNotes);
            UiHelperClass.BindControl(textEditWednesdayNotes, SpotCheckObject, () => SpotCheckObject.WednesdayNotes);

            UiHelperClass.BindControl(checkEditMineralsIvPush, SpotCheckObject, () => SpotCheckObject.MineralsIvPush);
            UiHelperClass.BindControl(checkEditMineralsOne, SpotCheckObject, () => SpotCheckObject.MineralsOne);
            UiHelperClass.BindControl(checkEditMineralsThree, SpotCheckObject, () => SpotCheckObject.MineralsThree);

            UiHelperClass.BindControl(spinEditNoOfBags, SpotCheckObject, () => SpotCheckObject.IngredientsNumberOfBags);
            UiHelperClass.BindControl(spinEditNoPerWeeks, SpotCheckObject, () => SpotCheckObject.IngredientsNumberPerWeek);

            UiHelperClass.BindControl(spinEditMineralsDextroseCC, SpotCheckObject, () => SpotCheckObject.MineralsDextroseCc);
            UiHelperClass.BindControl(spinEditMineralsDextroseCCPriority, SpotCheckObject, () => SpotCheckObject.MineralsDextroseCcpriority);
            UiHelperClass.BindControl(spinEditMineralsNormalSalineCC, SpotCheckObject, () => SpotCheckObject.MineralsNormalSalineCc);
            UiHelperClass.BindControl(spinEditMineralsNormalSalineCCPriority, SpotCheckObject, () => SpotCheckObject.MineralsNormalSalineCcpriority);
            UiHelperClass.BindControl(spinEditMineralsSterlieWaterCc, SpotCheckObject, () => SpotCheckObject.MineralsSterlieWaterCc);
            UiHelperClass.BindControl(spinEditMineralsSterlieWaterCCPriority, SpotCheckObject, () => SpotCheckObject.MineralsSterlieWaterCcpriority);

            UiHelperClass.BindControl(spinEditMineralsIVPerMin, SpotCheckObject, () => SpotCheckObject.MineralsIvperMin);
            UiHelperClass.BindControl(spinEditMineralsPerWeek, SpotCheckObject, () => SpotCheckObject.MineralsPerWeek);
            UiHelperClass.BindControl(spinEditMineralsEDTA, SpotCheckObject, () => SpotCheckObject.MineralsEdta);

            xtraUserControlPrintingOptionsMain.UpdateOptions();

            xtraUserControlSpotCheckResultsMtes.SpotCheckResults = _ivSheetSpotCheckResults;
            xtraUserControlSpotCheckResultsIngredients.SpotCheckResults = _mineralsSpotCheckResults;
            xtraUserControlSpotCheckResultsCapsolT.SpotCheckResults = _capsolTSpotCheckResults;
            xtraUserControlSpotCheckResultsDmps.SpotCheckResults = _dmpsSpotCheckResults;

            _deletedSpotCheckResults.Clear();

        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            textEditName.DataBindings.Clear();

            memoEditCapsolTNotes.DataBindings.Clear();
            memoEditMtesNotes.DataBindings.Clear();
            memoEditIngredientsNotes.DataBindings.Clear();


            textEditMondayNotes.DataBindings.Clear();
            textEditTusedayNotes.DataBindings.Clear();
            textEditWednesdayNotes.DataBindings.Clear();

            checkEditMineralsIvPush.DataBindings.Clear();
            checkEditMineralsOne.DataBindings.Clear();
            checkEditMineralsThree.DataBindings.Clear();

            spinEditNoOfBags.DataBindings.Clear();
            spinEditNoPerWeeks.DataBindings.Clear();

            spinEditMineralsDextroseCC.DataBindings.Clear();
            spinEditMineralsDextroseCCPriority.DataBindings.Clear();
            spinEditMineralsNormalSalineCC.DataBindings.Clear();
            spinEditMineralsNormalSalineCCPriority.DataBindings.Clear();
            spinEditMineralsSterlieWaterCc.DataBindings.Clear();
            spinEditMineralsSterlieWaterCCPriority.DataBindings.Clear();

            spinEditMineralsIVPerMin.DataBindings.Clear();
            spinEditMineralsPerWeek.DataBindings.Clear();
            spinEditMineralsEDTA.DataBindings.Clear();

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
            dxErrorProviderMain.DataSource = SpotCheckObject;
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public override void AfterLoadAction()
        {
            IsLoaded = true;

            if (FormStatus != FormStatusEnum.New)
            {
                var enableAutomationWhenOpeningExistingTest = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.EnableAutomationWhenOpeningExistingTest);

                if (enableAutomationWhenOpeningExistingTest && _currentTestPlayState == TestPlayStateEnum.Paused)
                {
                    SetAutoPlayTestMode(true);
                }
            }

            OpenCsaConnection();
        }

        /// <summary>
        /// Init the test object.
        /// </summary>
        /// <param name="isNew">Is new test.</param>
        private void InitSpotCheckObject(bool isNew)
        {
            if (isNew)
            {
                SpotCheckObject.CreationDateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Fills the lookups ids.
        /// </summary>
        public override void FillLookUps()
        {
            _ivSheetLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.IVSheet));

            _capsolTLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.CapsolT));

            _mineralsLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.Minerals));

            _dmpsLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SpotCheckResultType, SpotCheckResultType.Dmps));

            var leftLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Left));

            var rightLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Right));

            var yesLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes)).FirstOrDefault();

            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;
            _leftLookupId = leftLookup != null ? leftLookup.Id : 0;
            _rightLookupId = rightLookup != null ? rightLookup.Id : 0;
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
        private void SetDefaultFornts()
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

        #region GeneralLogic

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
                CollectSpotCheckResults();

            if (SpotCheckObject.ObjectState == DomainEntityState.Unchanged ||
                SpotCheckObject.ObjectState == DomainEntityState.Deleted) return true;

            return SaveOrSaveAndClose(false);
        }

        #endregion

        #region ItemsGridLogic

        /// <summary>
        /// Update the items grid based on selected tab page.
        /// </summary>
        private void UpdateItemsGridBasedOnSelectedTab()
        {
            if (SpotCheckObject == null)
                return;

            if (SpotCheckObject.SpotCheckResults == null)
                SpotCheckObject.SpotCheckResults = new BindingList<SpotCheckResult>();

            if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageMtes)
            {
                SetItemsGridDatasource(TargetType.IVSheet, ref _ivSheetItems, _ivSheetSpotCheckResults);
                _cureentSpotCheckResultsUserControl = xtraUserControlSpotCheckResultsMtes;
                _currentResultType = _ivSheetLookup;
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageCapsolT)
            {
                SetItemsGridDatasource(TargetType.CapsolT, ref _capsolTItems, _capsolTSpotCheckResults);
                _cureentSpotCheckResultsUserControl = xtraUserControlSpotCheckResultsCapsolT;
                _currentResultType = _capsolTLookup;
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients)
            {
                SetItemsGridDatasource(TargetType.Minerals, ref _mineralsItems, _mineralsSpotCheckResults);
                _cureentSpotCheckResultsUserControl = xtraUserControlSpotCheckResultsIngredients;
                _currentResultType = _mineralsLookup;
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageDmps)
            {
                SetItemsGridDatasource(TargetType.Dmps, ref _dmpsItems, _dmpsSpotCheckResults);
                _cureentSpotCheckResultsUserControl = xtraUserControlSpotCheckResultsDmps;
                _currentResultType = _dmpsLookup;
            }

            xtraTabPageItems.Text = string.Format(StaticKeys.ItemsListTitile, xtraTabControlSpotCheck.SelectedTabPage.Text);
            xtraTabPageItems.Image = xtraTabControlSpotCheck.SelectedTabPage.Image;

            SelectRowBasedOnTestMode();
            SetModeButtonsSatatus();
            Broadcast();
        }

        /// <summary>
        /// Select a row based on the current test mode.
        /// </summary>
        private void SelectRowBasedOnTestMode()
        {
            switch (_currentAutoTestMode)
            {
                case SpotCheckAutoTestMode.Starred:
                    ClearStarredCheckFlags();
                    MoveNextStarredItem();
                    break;
                case SpotCheckAutoTestMode.OneByOne:
                    SelectFirstItem();
                    break;
            }
        }

        /// <summary>
        /// Update columns visibility for items grid. 
        /// </summary>
        private void UpdateItemsGridColumnVisiblity()
        {
            //Setting for VisibleIndex is to keep the order of the columns after show or hide.
            gridColumnStarred.VisibleIndex = 0;
            gridColumnStarred.Visible = xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients || xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageMtes;
            gridColumnMgPerMl.VisibleIndex = 2;
            gridColumnMgPerMl.Visible = xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients;
            gridColumnDosageRange.VisibleIndex = 3;
            gridColumnDosageRange.Visible = xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients;
            gridColumnNotes.VisibleIndex = 4;
            gridColumnNotes.Visible = xtraTabControlSpotCheck.SelectedTabPage != xtraTabPageIngredients;
        }

        /// <summary>
        /// Process selected row change on items grid.
        /// </summary>
        private void ProcessSelectedTabPageChanged()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingItems);

            StopAutoTestTimer();
            xtraUserControlReadingGauge.Clear();
            ClearTestingFlags();
            UpdateItemsGridColumnVisiblity();
            UpdateItemsGridBasedOnSelectedTab();
            UpdateModeButtonsBasedOnSelectedTab();
            SetPrintAndDetailsButtonStatus();

            if (_currentTestPlayState == TestPlayStateEnum.Playing)
                AutoTestEngine();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Sets the items grid dataSource depends on the passed parameters.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="itemsListToSet">Items list to fill.</param>
        /// <param name="spotCheckResults">Spot check results to avoid the matched items.</param>
        private void SetItemsGridDatasource(TargetType targetType, ref BindingList<Item> itemsListToSet, BindingList<SpotCheckResult> spotCheckResults)
        {
            if (itemsListToSet == null)
            {
                var targetLookup =
                    UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, targetType));

                if (targetLookup == null || targetLookup.Id == 0)
                    return;

                itemsListToSet = _itemsManager.GetItems(new ItemsFilter { TargetTypeLookupId = targetLookup.Id })
                    .OrderBy(i =>
                    {
                        var firstOrDefault =
                            i.ItemTargets.FirstOrDefault(t => t.TargetTypeLookup.Id == targetLookup.Id);
                        return firstOrDefault != null ? firstOrDefault.Order : 0;
                    })
                    .ToBindingList();
            }

            gridControlItems.DataSource = itemsListToSet.Where(i => spotCheckResults == null || !spotCheckResults.Any(r => r.Item != null && r.Item.Id == i.Id)).ToBindingList();

            ClearSelection();
        }

        /// <summary>
        /// Update the mode buttons based on selected tab..
        /// </summary>
        private void UpdateModeButtonsBasedOnSelectedTab()
        {
            if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageMtes)
            {
                layoutControlItemOneByOneMode.Visibility = LayoutVisibility.Always;
                layoutControlItemStarredMode.Visibility = LayoutVisibility.Always;
                layoutControlItemSplit.Visibility = LayoutVisibility.Always;
                layoutControlItemSwitch.Visibility = LayoutVisibility.Always;
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients)
            {
                layoutControlItemOneByOneMode.Visibility = LayoutVisibility.Always;
                layoutControlItemStarredMode.Visibility = LayoutVisibility.Always;
                layoutControlItemSplit.Visibility = LayoutVisibility.Always;
                layoutControlItemSwitch.Visibility = LayoutVisibility.Always;
            }
            else if (!IsSplitSwitchAndStarredModeValid)
            {
                layoutControlItemOneByOneMode.Visibility = LayoutVisibility.Always;
                layoutControlItemStarredMode.Visibility = LayoutVisibility.Never;
                layoutControlItemSplit.Visibility = LayoutVisibility.Never;
                layoutControlItemSwitch.Visibility = LayoutVisibility.Never;
            }

        }

        /// <summary>
        /// Sets the source of the grid.
        /// </summary>
        public void InitGridItems(BindingList<Item> items)
        {
            gridControlItems.DataSource = items;

            gridViewItems.RefreshData();

            SetModeButtonsSatatus();

            ClearSelection();
        }

        /// <summary>
        /// Gets the selected items.
        /// </summary>
        /// <returns>The items as binding List.</returns>
        public BindingList<Item> GetSelectedItems()
        {
            var selectedItems = new BindingList<Item>();

            if (gridViewItems.SelectedRowsCount > 0)
            {
                var selectedRowsIndex = gridViewItems.GetSelectedRows();

                foreach (var t in selectedRowsIndex)
                {
                    var item = gridViewItems.GetRow(t) as Item;

                    if (item == null) continue;

                    selectedItems.Add(item);
                }
            }

            return selectedItems;
        }

        /// <summary>
        /// Gets item for broadcast.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItemsToBroadcast()
        {
            if(gridViewItems.SelectedRowsCount > 0)
            {
                return GetSelectedItems().ToList();
            }

            return GetSystemSelectionItems().ToList();
        }

        /// <summary>
        /// Gets the system selection items.
        /// </summary>
        /// <returns></returns>
        private BindingList<Item> GetSystemSelectionItems()
        {
            var currentGridDataSource = (BindingList<Item>)gridViewItems.DataSource;

            var sytemSelectionItems = new BindingList<Item>();

            var middleIndex = gridViewItems.DataRowCount / 2;

            double value = (float)(gridViewItems.DataRowCount / 2.0);

            var isOddList = (value - Math.Floor(value)) == 0 ? false : true;

            if (isOddList)
            {
                if (_partHilight == 1)
                {
                    for (var i = 0; i < middleIndex; i++)
                    {
                        sytemSelectionItems.Add(currentGridDataSource[i]);
                    }
                }
                else
                {
                    for (var i = 0; i <= middleIndex; i++)
                    {
                        sytemSelectionItems.Add(currentGridDataSource[i]);
                    }
                }
            }
            else
            {
                for (var i = 0; i < middleIndex; i++)
                {
                    sytemSelectionItems.Add(currentGridDataSource[i]);
                }
            }

            return sytemSelectionItems;
        }

        /// <summary>
        /// Select first item in the grid.
        /// </summary>
        private void SelectFirstItem()
        {
            gridViewItems.ClearSelection();
            gridViewItems.MoveFirst();
            gridControlItems.ForceInitialize();
            gridControlItems.Refresh();
            gridViewItems.Focus();
        }

        /// <summary>
        /// Splits the items in the grid
        /// </summary>
        private void SplitItems()
        {
            InitGridItems(GetSystemSelectionItems());

            _partHilight = 1;
            _isFirstSwitch = true;

            gridViewItems.RefreshData();

            if (gridViewItems.RowCount == 1)
                SelectFirstItem();

            ProcessSelectedRowChanged();
        }

        /// <summary>
        /// Highlight logic for the items in the grid.
        /// </summary>
        private void SwitchSelection()
        {

            /*TODO: This checking is for determining if the navigation grid in the Items mode or in the major issues mode,
             *if the currentTestResult does equals null, then the navigation grid is in the issues mode, what should be done
             * here is declaring an enum or a flag to indicate the mode of the navigation grid.*/

            SwitchingLogic();

            _part = (_part == 1 ? _part + 1 : _part - 1);
            _partHilight = (_partHilight == 1 ? _partHilight + 1 : _partHilight - 1);

            gridViewItems.RefreshData();
            gridViewItems.MakeRowVisible(0);

            ClearSelection();
            ProcessSelectedRowChanged();
            gridViewItems.Focus();
        }

        /// <summary>
        /// Contains the logic of switching or highlight the items in the grid.
        /// </summary>
        private void SwitchingLogic()
        {
            var items = (BindingList<Item>)gridViewItems.DataSource;

            if (_isFirstSwitch)
            {
                _isFirstSwitch = false;
                _isTopListFirst = true;
                _topItems.Clear();
                _bottomItems.Clear();

                //Split the items into top (should contain the first part of the list) and bottom (should contain the remaining items) lists.
                for (var i = 0; i < items.Count; i++)
                {
                    //Put the first part in the Top Items list and the other in the Bottom Items list.
                    if (i >= 0 && i < (gridViewItems.DataRowCount / 2))
                    {
                        _topItems.Add(items[i]);
                    }
                    else
                    {
                        _bottomItems.Add(items[i]);
                    }
                }

            }

            //Clearing the data source items.
            items.Clear();

            if (_isTopListFirst)
            {
                //Switching operation, putting the bottom items first, then the top items.
                foreach (var bottomItem in _bottomItems)
                {
                    items.Add(bottomItem);
                }

                foreach (var topItem in _topItems)
                {
                    items.Add(topItem);
                }
            }
            else
            {
                //Switching operation, putting the bottom items first, then the top items.
                foreach (var bottomItem in _topItems)
                {
                    items.Add(bottomItem);
                }

                foreach (var topItem in _bottomItems)
                {
                    items.Add(topItem);
                }
            }

            //invert the isTopList flag
            _isTopListFirst = !_isTopListFirst;

            //Set the new data source
            InitGridItems(items);

        }

        /// <summary>
        /// Clean the user selection.
        /// </summary>
        private void ClearSelection()
        {
            gridViewItems.ClearSelection();
            gridControlItems.Update();
            gridControlItems.ForceInitialize();
            gridControlItems.Refresh();
            gridViewItems.Focus();
            SetModeButtonsSatatus();
        }

        /// <summary>
        /// Reload the items.
        /// </summary>
        private void ReloadItems()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingItems);
            ClearTestingFlags();
            UpdateItemsGridBasedOnSelectedTab();
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Add selected item as spot check result.
        /// </summary>
        private void AddToResults()
        {
            if (_deletedSpotCheckResults == null || _currentResultType == null)
                return;

            BindingList<Item> selectedItems = null;

            if(gridViewItems.SelectedRowsCount > 0)
            {
                selectedItems = GetSelectedItems();
            }
            else if(IsSplitSwitchAndStarredModeValid && _currentAutoTestMode == SpotCheckAutoTestMode.SplitAndSwitch)
            {
                var systemSelection = GetSystemSelectionItems();

                if (systemSelection.Count > 0)
                     selectedItems = systemSelection;
            }

            if (selectedItems == null)
                return;

            foreach (var selectedItem in selectedItems)
            {
                var result = new SpotCheckResult { Item = selectedItem, ResultType = _currentResultType, SpotCheck = SpotCheckObject, Notes = selectedItem.Notes };

                ShowAutoTestDialog(result, _currentTestPlayState == TestPlayStateEnum.Paused);

                var items = gridControlItems.DataSource as BindingList<Item>;

                if (items == null)
                    return;

                items.Remove(selectedItem);

                if (_currentAutoTestMode == SpotCheckAutoTestMode.SplitAndSwitch)
                    ClearTestingFlags();

                _cureentSpotCheckResultsUserControl.AddResult(result);

                gridViewItems.SelectRow(gridViewItems.FocusedRowHandle);
            }

            if (gridViewItems.RowCount == 0)
            {
                ReloadItems();
            }
            else if (gridViewItems.RowCount == 1)
            {
                SelectFirstItem();
            }

            // Update the testing mode after insert, ex. if starred mode is one and all starred items had been inserted, so starred mode should be off and move to split and switch mode.
            if(_currentAutoTestMode == SpotCheckAutoTestMode.Starred)
                MoveNextStarredItem();

            SetModeButtonsSatatus();
            SetPrintAndDetailsButtonStatus();
        }


        /// <summary>
        /// Move next starred item if exists.
        /// </summary>
        private void MoveNextStarredItem()
        {
            var items = (BindingList<Item>)gridViewItems.DataSource;

            if (items == null)
                return;

            var firstNotChekedItem = items.FirstOrDefault(i => i.IsStarred && !i.IsChecked);

            if (firstNotChekedItem == null)
            {
                ProcessStarredModeDone();
                return;
            }

            var index = items.IndexOf(firstNotChekedItem);

            gridControlItems.ForceInitialize();
            gridViewItems.ClearSelection();
            gridViewItems.SelectRow(index);
            gridViewItems.FocusedRowHandle = index;
            gridControlItems.Refresh();
            gridViewItems.Focus();

        }

        /// <summary>
        /// Process on starred mode is finished.
        /// </summary>
        private void ProcessStarredModeDone()
        {
            SetAutoTestMode(SpotCheckAutoTestMode.SplitAndSwitch, true);
        }

        /// <summary>
        /// Move next item.
        /// </summary>
        private void MoveNextItem()
        {
            if (gridViewItems.IsLastRow)
            {
                SelectFirstItem();
            }
            else
            {
                gridViewItems.MoveNext();
                gridControlItems.ForceInitialize();
                gridControlItems.Refresh();
                gridViewItems.Focus();
            }


        }

        #endregion

        #region CRUDLogic

        /// <summary>
        /// Posts the values in the controls that are not yet committed to the dataSource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public override void PostValues()
        {
            textEditName.DoValidate();

            memoEditCapsolTNotes.DoValidate();
            memoEditMtesNotes.DoValidate();
            memoEditIngredientsNotes.DoValidate();


            textEditMondayNotes.DoValidate();
            textEditTusedayNotes.DoValidate();
            textEditWednesdayNotes.DoValidate();

            checkEditMineralsIvPush.DoValidate();
            checkEditMineralsOne.DoValidate();
            checkEditMineralsThree.DoValidate();

            spinEditNoOfBags.DoValidate();
            spinEditNoPerWeeks.DoValidate();

            spinEditMineralsDextroseCC.DoValidate();
            spinEditMineralsDextroseCCPriority.DoValidate();
            spinEditMineralsNormalSalineCC.DoValidate();
            spinEditMineralsNormalSalineCCPriority.DoValidate();
            spinEditMineralsSterlieWaterCc.DoValidate();
            spinEditMineralsSterlieWaterCCPriority.DoValidate();

            spinEditMineralsIVPerMin.DoValidate();
            spinEditMineralsPerWeek.DoValidate();
            spinEditMineralsEDTA.DoValidate();

            xtraUserControlSpotCheckResultsMtes.PostValues();
            xtraUserControlSpotCheckResultsIngredients.PostValues();
            xtraUserControlSpotCheckResultsCapsolT.PostValues();
            xtraUserControlSpotCheckResultsDmps.PostValues();
        }

        /// <summary>
        /// Uses the Tests manager to save the spot check.        
        /// </summary>
        public override bool Save(bool isClosing)
        {
            try
            {
                PostValues();

                CollectSpotCheckResults();

                if (!SpotCheckObject.Validate())
                    return false;

                SpotCheckObject.TestId = SpotCheckObject.TestId > 0 ? SpotCheckObject.TestId : null;

                return _spotCheckManager.SaveSpotCheck(SpotCheckObject).IsSucceed;
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
            _deletedSpotCheckResults.Clear();
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

                var result = _spotCheckManager.DeleteSpotCheck(SpotCheckObject);

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
        /// Delete passed spot check result.
        /// </summary>
        /// <param name="result"></param>
        private void DeleteSpotCheckResult(SpotCheckResult result)
        {
            if (result != null && result.Id != 0)
                _deletedSpotCheckResults.Add(result);

            UpdateCurrentItemsWithDeletedResult(result);
            SetModeButtonsSatatus();
            SetPrintAndDetailsButtonStatus();
        }

        /// <summary>
        /// Update the items grid with deleted spot check item.
        /// </summary>
        private void UpdateCurrentItemsWithDeletedResult(SpotCheckResult result)
        {
            if(result == null || result.Item == null)
                return;

            var currentItems = gridViewItems.DataSource as BindingList<Item>;
            var needToSelectAll = gridViewItems.SelectedRowsCount == gridViewItems.RowCount;

            if(currentItems == null)
                return;

            result.Item.IsChecked = false;

            currentItems.Add(result.Item);

            if (_currentAutoTestMode == SpotCheckAutoTestMode.SplitAndSwitch)
                ClearTestingFlags();

            if(needToSelectAll)
                gridViewItems.SelectAll();
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            try
            {
                IsLoaded = false;

                SpotCheckObject = _spotCheckManager.GetSpotCheckById(new SingleItemFilter { ItemId = SpotCheckObject.Id });

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

        #region SpotCheckResultsLogic

        /// <summary>
        /// Fill the spot check result lists depends on the result type.
        /// </summary>
        private void FillSpotCheckResultLists()
        {
            if (SpotCheckObject == null || _dmpsLookup == null || _ivSheetLookup == null || _capsolTLookup == null || _mineralsLookup == null)
                return;

            if (SpotCheckObject.SpotCheckResults == null)
                SpotCheckObject.SpotCheckResults = new BindingList<SpotCheckResult>();

            _ivSheetSpotCheckResults =
                SpotCheckObject.SpotCheckResults.Where(r => r.ResultType != null && r.ResultType.Id == _ivSheetLookup.Id)
                    .ToBindingList();

            _mineralsSpotCheckResults =
                SpotCheckObject.SpotCheckResults.Where(r => r.ResultType != null && r.ResultType.Id == _mineralsLookup.Id)
                    .ToBindingList();

            _capsolTSpotCheckResults =
                SpotCheckObject.SpotCheckResults.Where(r => r.ResultType != null && r.ResultType.Id == _capsolTLookup.Id)
                    .ToBindingList();

            _dmpsSpotCheckResults = SpotCheckObject.SpotCheckResults.Where(r => r.ResultType != null && r.ResultType.Id == _dmpsLookup.Id)
                    .ToBindingList();
        }

        /// <summary>
        /// Collect the spot check results list to the main spot check results list inside the spot check object. 
        /// </summary>
        private void CollectSpotCheckResults()
        {
            var collectedSpotCheckResult = new List<SpotCheckResult>();

            collectedSpotCheckResult.AddRange(_ivSheetSpotCheckResults);
            collectedSpotCheckResult.AddRange(_mineralsSpotCheckResults);
            collectedSpotCheckResult.AddRange(_capsolTSpotCheckResults);
            collectedSpotCheckResult.AddRange(_dmpsSpotCheckResults);

            collectedSpotCheckResult.AddRange(_deletedSpotCheckResults);

            SpotCheckObject.SpotCheckResults = collectedSpotCheckResult.ToBindingList();
        }

        #endregion

        #region AutoTestingLogic

        /// <summary>
        /// Clear all the testing flags.
        /// </summary>
        private void ClearTestingFlags()
        {
            _isFirstSwitch = true;
            _isTopListFirst = true;
            _part = 1;
            _partHilight = 1;
            _topItems.Clear();
            _bottomItems.Clear();
            //SetOneByOneMode(false);
            //SetStarredMode(false);

        }

        /// <summary>
        /// Start the auto play test timer.
        /// </summary>
        private void StartAutoTestTimer()
        {
            _autoTestNextReadingTimeOut = 0;

            timerAutoTest.Tick -= timerAutoTest_Tick;

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
        /// Set the button status based on the grid items.
        /// </summary>
        private void SetModeButtonsSatatus()
        {
            var isSplitswitchMode = _currentAutoTestMode == SpotCheckAutoTestMode.SplitAndSwitch;

            simpleButtonSplit.Enabled = _isEditable && gridViewItems.RowCount > 1 && isSplitswitchMode;

            simpleButtonSwitch.Enabled = _isEditable && gridViewItems.RowCount > 1 && isSplitswitchMode;

            simpleButtonClearSelection.Enabled = _isEditable && isSplitswitchMode && gridViewItems.SelectedRowsCount > 0 && gridViewItems.RowCount != 1;

            simpleButtonAddToResults.Enabled = _isEditable && gridViewItems.SelectedRowsCount > 0;

            checkButtonOneByOneMode.Enabled = _isEditable && gridViewItems.RowCount > 0 && (isSplitswitchMode || _currentAutoTestMode == SpotCheckAutoTestMode.OneByOne);

            checkButtonStarredMode.Enabled = _isEditable && gridViewItems.RowCount > 0 && CheckStarredItemsExist() &&(isSplitswitchMode || _currentAutoTestMode == SpotCheckAutoTestMode.Starred);

            gridViewItems.Focus();
        }

        /// <summary>
        /// Check if there are starred items exist.
        /// </summary>
        /// <returns></returns>
        private bool CheckStarredItemsExist()
        {
            var items = gridViewItems.DataSource as BindingList<Item>;

            if (items == null)
                return false;

            return items.Any(i => i.IsStarred);
        }

        /// <summary>
        /// Toggle the auto testing mode.
        /// </summary>
        private void ToggleAutoTestMode()
        {
            SetAutoPlayTestMode(_currentTestPlayState == TestPlayStateEnum.Paused);
        }

        /// <summary>
        /// Set one by one mode on or off.
        /// </summary>
        /// <param name="isOn"></param>
        private void SetOneByOneMode(bool isOn)
        {
            //this code to force repainting for the grid, to avoid the row style issue.
            checkButtonOneByOneMode.Focus();
            gridControlItems.DataSource = gridControlItems.DataSource as BindingList<Item>;
            gridControlItems.RefreshDataSource();

            if (isOn)
            {
                _currentAutoTestMode = SpotCheckAutoTestMode.OneByOne;
                checkButtonOneByOneMode.Checked = true;
                checkButtonOneByOneMode.Image = Resources.OneByOneOn;
                SelectFirstItem();
            }
            else
            {
                _currentAutoTestMode = SpotCheckAutoTestMode.SplitAndSwitch;
                checkButtonOneByOneMode.Checked = false;
                checkButtonOneByOneMode.Image = Resources.OneByOneOff;
                ClearSelection();
            }

            SetModeButtonsSatatus();
        }

        /// <summary>
        /// Set starred mode on or off.
        /// </summary>
        /// <param name="isOn"></param>
        private void SetStarredMode(bool isOn)
        {
            if (isOn)
            {
                _currentAutoTestMode = SpotCheckAutoTestMode.Starred;
                checkButtonStarredMode.Checked = true;
                checkButtonStarredMode.Image = Resources.StarsMode;
                ReloadItems();
                MoveNextStarredItem();
            }
            else
            {
                _currentAutoTestMode = SpotCheckAutoTestMode.SplitAndSwitch;
                checkButtonStarredMode.Checked = false;
                checkButtonStarredMode.Image = Resources.StarsModeOff;
                ClearStarredCheckFlags();
                ReloadItems();
                ClearSelection();
            }

            SetModeButtonsSatatus();
        }

        /// <summary>
        /// Clear isChecked flag for the items list - make all items not checked.
        /// </summary>
        private void ClearStarredCheckFlags()
        {
            var items = (BindingList<Item>) gridViewItems.DataSource;

            if (items == null)
                return;

            foreach (var item in items.Where(item => item.IsChecked))
            {
                item.IsChecked = false;
            }
        }

        /// <summary>
        /// Sets the auto test mode. 
        /// </summary>
        /// <param name="isPlaying">is the auto test playing.</param>
        private void SetAutoPlayTestMode(bool isPlaying)
        {
            _barButtonItemPausePlay.Glyph = isPlaying ? Resources.Test_Pause : Resources.Test_Play;
            _currentTestPlayState = isPlaying ? TestPlayStateEnum.Playing : TestPlayStateEnum.Paused;
            _barButtonItemPausePlay.Caption = isPlaying ? CommonResources.CommonResources.Pause : CommonResources.CommonResources.Play;

            
            if (isPlaying)
            {
                AutoTestEngine();
            }
            else
            {
                SetAutoTestMode(SpotCheckAutoTestMode.SplitAndSwitch, false);
            }

        }

        /// <summary>
        /// Sets auto test mode on or off.
        /// </summary>
        /// <param name="autoTestMode"></param>
        /// <param name="keepForCurrentMode"></param>
        private void SetAutoTestMode(SpotCheckAutoTestMode autoTestMode, bool keepForCurrentMode)
        {
            switch (autoTestMode)
            {
                case SpotCheckAutoTestMode.SplitAndSwitch:
                    checkButtonOneByOneMode.Checked = false; SetOneByOneMode(false);
                    checkButtonStarredMode.Checked = false;
                    if (_currentSpotCheckReadingPlayType == SpotCheckReadingPlayType.Mtes)
                        gridViewItems.SelectAll();
                    break;
                case SpotCheckAutoTestMode.Starred:
                    checkButtonOneByOneMode.Checked = false;
                    checkButtonStarredMode.Checked = true;
                    break;
                case SpotCheckAutoTestMode.OneByOne:
                    checkButtonStarredMode.Checked = false;
                    checkButtonOneByOneMode.Checked = true;
                    break;
            }

            if (!keepForCurrentMode)
                return;

            switch (_currentSpotCheckReadingPlayType)
            {
                case SpotCheckReadingPlayType.Mtes:
                    _lastMtesSpotCheckAutoTestMode = autoTestMode;
                    break;
                case SpotCheckReadingPlayType.Ingredients:
                    _lastIngSpotCheckAutoTestMode = autoTestMode;
                    break;
            }

        }

        /// <summary>
        /// The auto testing engine.
        /// </summary>
        private void AutoTestEngine()
        {
            if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageMtes)
            {
                _currentSpotCheckReadingPlayType = SpotCheckReadingPlayType.Mtes;
                SetAutoTestMode(_lastMtesSpotCheckAutoTestMode, true);
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients)
            {
                _currentSpotCheckReadingPlayType = SpotCheckReadingPlayType.Ingredients;
                SetAutoTestMode(_lastIngSpotCheckAutoTestMode, true);
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageCapsolT)
            {
                _currentSpotCheckReadingPlayType = SpotCheckReadingPlayType.CapsolT;
                SetAutoTestMode(SpotCheckAutoTestMode.OneByOne, true);
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageDmps)
            {
                _currentSpotCheckReadingPlayType = SpotCheckReadingPlayType.Dmps;
                SetAutoTestMode(SpotCheckAutoTestMode.OneByOne, true);
            }

        }

        /// <summary>
        /// Do the auto test action, the Engine.
        /// </summary>
        /// <param name="justChecking">The just checking flag, to tell the automation to execute the action or not.</param>
        /// <returns></returns>
        private string AutoTestActionEngine(bool justChecking)
        {
            var actionName = CrossLayersSharedLogic.IsAcceptableReading(_lastReadingValue) ? AutoTestYesActionEngine(justChecking) : AutoTestNoActionEngine(justChecking);

            return actionName;
        }

        /// <summary>
        /// Auto test action on yes reading.
        /// </summary>
        /// <param name="justChecking">Is just for checking - no action will apply.</param>
        /// <returns></returns>
        private string AutoTestYesActionEngine(bool justChecking)
        {
            if (gridViewItems.RowCount == 0)
            {
                return StaticKeys.NoneAction;
            }

            var actionName = StaticKeys.NoneAction;

            switch (_currentAutoTestMode)
            {
                case SpotCheckAutoTestMode.Starred:

                    if (!justChecking)
                    {
                        AddToResults();
                        MoveNextStarredItem();
                    }

                    actionName = StaticKeys.InsertingAction;

                    break;
                case SpotCheckAutoTestMode.OneByOne:

                    if (!justChecking)
                        AddToResults();

                    actionName = StaticKeys.InsertingAction;

                    break;
                case SpotCheckAutoTestMode.SplitAndSwitch:
                    if (gridViewItems.RowCount == gridViewItems.SelectedRowsCount && gridViewItems.RowCount > 1)
                    {
                        if (!justChecking)
                        {
                            ClearSelection();
                        }

                        actionName = StaticKeys.ClearingAction;
                    }
                    else if ((gridViewItems.RowCount > 0 && gridViewItems.SelectedRowsCount > 0 ) || (IsSplitSwitchAndStarredModeValid && GetSystemSelectionItems().Count == 1))
                    {
                        if (!justChecking)
                        {
                            var needToReload = IsSplitSwitchAndStarredModeValid && GetSystemSelectionItems().Count == 1;

                            AddToResults();
                            ClearSelection();

                            if (needToReload)
                            {
                                ReloadItems();
                                gridViewItems.SelectAll();
                            }
                        }

                        actionName = StaticKeys.InsertingAction;
                    }
                    else if (IsSplitSwitchAndStarredModeValid && GetSystemSelectionItems().Count > 1)
                    {
                        if (!justChecking)
                            SplitItems();

                        actionName = StaticKeys.SplittingAction;
                    }

                    break;
            }

            return actionName;
        }

        /// <summary>
        /// Auto test actions on no reading.
        /// </summary>
        /// <param name="justChecking">Is just for checking - no action will apply.</param>
        /// <returns></returns>
        private string AutoTestNoActionEngine(bool justChecking)
        {
            if (gridViewItems.RowCount == 0)
            {
                return StaticKeys.NoneAction;
            }

            var actionName = StaticKeys.NoneAction;

            switch (_currentAutoTestMode)
            {
                case SpotCheckAutoTestMode.Starred:

                    if (!justChecking)
                        MoveNextStarredItem();

                    actionName = StaticKeys.MovingAction;

                    break;
                case SpotCheckAutoTestMode.OneByOne:

                    if (!justChecking)
                        MoveNextItem();

                    actionName = StaticKeys.MovingAction;

                    break;
                case SpotCheckAutoTestMode.SplitAndSwitch:

                    if (gridViewItems.RowCount > 1 && gridViewItems.SelectedRowsCount > 0)
                    {
                        var clearOnNo = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.ClearSelectionOnNoReading));

                        if (clearOnNo != null && Convert.ToInt32(clearOnNo.Value) == _yesLookupId)
                        {
                            actionName = StaticKeys.ClearingAction;

                            if (!justChecking)
                                ClearSelection();
                        }
                        else
                        {
                            actionName = StaticKeys.NoneAction;
                        }
                        
                    }
                    else if (IsSplitSwitchAndStarredModeValid)
                    {
                        if (!justChecking)
                            SwitchSelection();

                        actionName = StaticKeys.SwitchingAction;
                    }
                    break;
            }


            return actionName;
        }

        /// <summary>
        /// Gets next auto test action name based on reading.
        /// </summary>
        /// <returns></returns>
        public string GetAutoTestActionName()
        {
            return AutoTestActionEngine(true);
        }

        /// <summary>
        /// Do next action based on reading.
        /// </summary>
        /// <returns></returns>
        public string DoAutoTestAction()
        {
            return AutoTestActionEngine(false);
        }

        private void BeforeShowAutoTestDialog()
        {
            StopReading();
            CsaEmdUnitManager.Instance.DisposeConnection(
                                           Csa_Instance_Released,
                                           _csaManager_ReadingDone,
                                          _csaManager_MeterValueChanged);
        }

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
        private void ShowAutoTestDialog(SpotCheckResult spotCheckResult, bool isUserNavigation)
        {
            BeforeShowAutoTestDialog();

            ShowHideOverlay(true);
            var dialogResult = new XtraFormSpotCheckDialog(spotCheckResult, isUserNavigation).ShowDialog();
            ShowHideOverlay(false);

            if (dialogResult == DialogResult.Cancel && _currentTestPlayState == TestPlayStateEnum.Playing)
                SetAutoPlayTestMode(false);

            AfterShowAutoTestDialog();
        }

        private void ShowAutoTestDialog(SpotCheck spotCheck, bool isUserNavigation)
        {
            BeforeShowAutoTestDialog();

            ShowHideOverlay(true);
            var dialogResult = new XtraFormSpotCheckDialog(spotCheck, isUserNavigation).ShowDialog();
            ShowHideOverlay(false);

            if (dialogResult == DialogResult.Cancel && _currentTestPlayState == TestPlayStateEnum.Playing)
                SetAutoPlayTestMode(false);

            AfterShowAutoTestDialog();
        }

        /// <summary>
        /// Process selected row changed.
        /// </summary>
        private void ProcessSelectedRowChanged()
        {
            SetModeButtonsSatatus();
            Broadcast();
        }

        /// <summary>
        /// Mark the item as checked in starred mode.
        /// </summary>
        private void MarkItemAsChecked()
        {
            if (_currentAutoTestMode == SpotCheckAutoTestMode.Starred)
            {
                var currentItem = gridViewItems.GetFocusedRow() as Item;

                if (currentItem != null)
                    currentItem.IsChecked = true;

                gridViewItems.UpdateCurrentRow();
            }
        }

        /// <summary>
        /// Show auto test dialog for details.
        /// </summary>
        private void ShowSetDetailsAutoTestDialog()
        {
            if (_cureentSpotCheckResultsUserControl == null)
                return;

            if(_currentTestPlayState == TestPlayStateEnum.Paused)
                SetAutoPlayTestMode(true);

            _cureentSpotCheckResultsUserControl.SelectAll();

            if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients)
            {
                layoutControlGroupBagsPerWeek.Expanded = true;
                layoutControlGroupIngreadientsDetails.Expanded = true;
                spinEditNoOfBags.Focus();
                //this update for the button mode and for the group it self is required to avoid the group button issue.
                //ex : if group expanded the button sometime keep it self like this (<) which in opposite form what is should be (>)
                layoutControlGroupBagsPerWeek.ExpandButtonMode = ExpandButtonMode.Inverted;
                layoutControlGroupBagsPerWeek.Update();
                layoutControlGroupIngreadientsDetails.ExpandButtonMode = ExpandButtonMode.Inverted;
                layoutControlGroupIngreadientsDetails.Update();
            }
            else if (xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageCapsolT)
            {
                layoutControlGroupProgram.Expanded = true;
                textEditMondayNotes.Focus();
                //this update for the button mode and for the group it self is required to avoid the group button issue.
                //ex : if group expanded the button sometime keep it self like this (<) which in opposite form what is should be (>)
                layoutControlGroupProgram.ExpandButtonMode = ExpandButtonMode.Inverted;
                layoutControlGroupProgram.Update();
            }

            ShowAutoTestDialog(SpotCheckObject, true);
        }

        #endregion

        #region Printing

        /// <summary>
        /// Prints the MTE report
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintMTE(bool isPreview)
        {
            if (_isEditable && !SaveAction())
                return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.PreparingReportMessage);

            var report = new XtraReportMTE()
            {
                PatientName = { Value = SpotCheckObject.Patient.FirstName + " " + SpotCheckObject.Patient.LastName },
                bindingSourcePatient = { DataSource = SpotCheckObject.Patient },
                bindingSourceMTE = { DataSource = _ivSheetSpotCheckResults },
                bindingSourceSpotCheck = { DataSource = SpotCheckObject },
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

        /// <summary>
        /// Prints the CAPSOLT report
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintCapsolT(bool isPreview)
        {
            if (_isEditable && !SaveAction()) 
                return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.PreparingReportMessage);

            var report = new XtraReportCapsolT()
            {
                PatientName = { Value = SpotCheckObject.Patient.FirstName + " " + SpotCheckObject.Patient.LastName },
                bindingSourcePatient = { DataSource = SpotCheckObject.Patient },
                bindingSourceCapsolT = { DataSource = _capsolTSpotCheckResults },
                bindingSourceSpotCheck = { DataSource = SpotCheckObject },
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

        /// <summary>
        /// Prints the Minerals report
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintMinerals(bool isPreview)
        {
            if (_isEditable && !SaveAction())
                return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.PreparingReportMessage);

            var report = new XtraReportMinerals()
            {
                PatientName = { Value = SpotCheckObject.Patient.FirstName + " " + SpotCheckObject.Patient.LastName },
                bindingSourcePatient = { DataSource = SpotCheckObject.Patient },
                bindingSourceMinerals = { DataSource = _mineralsSpotCheckResults },
                bindingSourceSpotCheck = { DataSource = SpotCheckObject },
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

        /// <summary>
        /// Prints the DMPS report
        /// </summary>
        /// <param name="isPreview"></param>
        private void PrintDMPS(bool isPreview)
        {
            if (_isEditable && !SaveAction())
                return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.PreparingReportMessage);

            var report = new XtraReportDMPS()
            {
                PatientName = { Value = SpotCheckObject.Patient.FirstName + " " + SpotCheckObject.Patient.LastName },
                bindingSourcePatient = { DataSource = SpotCheckObject.Patient },
                bindingSourceDMPS = { DataSource = _dmpsSpotCheckResults },
                bindingSourceSpotCheck = { DataSource = SpotCheckObject },
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

            var actionName = GetAutoTestActionName();

            SetReadingStatusBarMode(testBarState, actionName, secondsToWait);
        }

        /// <summary>
        /// Setup the Auto test timer and progress bar.
        /// </summary>
        private void SetupAutoTestTimerAndProgressBar()
        {
            timerAutoTest.Interval = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime < 500
                                         ? CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime
                                         : 500;
        }

        #endregion

        #region Handlers

        #region GeneralHandlers

        /// <summary>
        /// Handle the propriety changed event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        private void SpotCheckObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(SpotCheckObject_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                switch (SpotCheckObject.ObjectState)
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

                var nextActionName = GetAutoTestActionName();

                SetReadingStatusBarMode(TestBarStateEnum.WaitBeforTakeAction, nextActionName, secondToWait);

                if (_autoTestNextReadingTimeOut >= timeOut)
                {
                    timerAutoTest.Tick -= timerAutoTest_Tick;
                    timerAutoTest.Enabled = false;
                    _autoTestNextReadingTimeOut = 0;
                    AutoTestActionEngine(false);
                    SetReadingIndicators(TestBarStateEnum.Ready, 0);
                    xtraUserControlReadingGauge.Clear();
                }
            }

        }

        /// <summary>
        /// Handel the key down on the form.
        /// </summary>
        private void frmSpotCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(frmSpotCheck_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.KeyCode == Keys.F6)
                {
                    if (IsSplitSwitchAndStarredModeValid && simpleButtonSwitch.Enabled)
                    {
                        SwitchSelection();
                    }
                }
                else if (e.KeyCode == Keys.F7)
                {
                    if (simpleButtonReload.Enabled)
                    {
                        ReloadItems();
                    }
                }
                else if (e.KeyCode == Keys.F5 && IsSplitSwitchAndStarredModeValid)
                {
                    if (simpleButtonSplit.Enabled)
                    {
                        SplitItems();
                    }
                }
                else if (e.KeyCode == Keys.F4 && !_cureentSpotCheckResultsUserControl.IsFocusedView)
                {
                    gridViewItems.SelectAll();
                }
                else if (e.KeyCode == Keys.Insert || (e.Control && e.KeyCode == Keys.I) || e.KeyCode == Keys.Oemtilde)
                {
                    if (simpleButtonAddToResults.Enabled)
                    {
                        gridColumnNotes.OptionsColumn.AllowEdit = false;
                        AddToResults();
                        gridColumnNotes.OptionsColumn.AllowEdit = true;
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (simpleButtonClearSelection.Enabled && !_cureentSpotCheckResultsUserControl.IsFocusedView)
                    {
                        ClearSelection();
                    }
                }
                else if (e.KeyCode == Keys.Home)
                {
                    gridViewItems.Focus();
                    gridViewItems.MoveFirst();
                    gridViewItems.MakeRowVisible(gridViewItems.FocusedRowHandle);
                }
                else if (e.KeyCode == Keys.End)
                {
                    gridViewItems.Focus();
                    gridViewItems.MoveLastVisible();
                }
                else if (e.KeyCode == Keys.F12)
                {
                    if (checkButtonOneByOneMode.Enabled)
                        checkButtonOneByOneMode.Checked = !checkButtonOneByOneMode.Checked;
                }
                else if (e.KeyCode == Keys.F11)
                {
                    if (checkButtonStarredMode.Enabled && IsSplitSwitchAndStarredModeValid)
                        checkButtonStarredMode.Checked = !checkButtonStarredMode.Checked;
                }
                else if (e.KeyCode == Keys.F3)
                {
                   if(xtraTabControlSpotCheck.SelectedTabPage == xtraTabPageIngredients && simpleButtonSetDetails.Enabled)
                       ShowSetDetailsAutoTestDialog();
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

                    xtraUserControlReadingGauge.Clear();

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

                _lastReadingValue = reading;

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

                StopAutoTestTimer();

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

                if (_currentTestPlayState == TestPlayStateEnum.Playing)
                {
                    MarkItemAsChecked();
                    StartAutoTestTimer();
                }

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

        #endregion

        #region UiHandlers

        /// <summary>
        /// Prevent the showing of the PopupMenu for the dock panel, (this PopupMenu contains close option that we need to disable)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockManagerMain_PopupMenuShowing(object sender, DevExpress.XtraBars.Docking.PopupMenuShowingEventArgs e)
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
        private void subListSpotCheckResults_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(subListSpotCheckResults_ListChanged), sender, e);
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
        /// Handel SelectedPageChanged for the tabs.
        /// </summary>
        private void xtraTabControlSpotCheck_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangedEventHandler(xtraTabControlSpotCheck_SelectedPageChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ProcessSelectedTabPageChanged();
            }
        }

        /// <summary>
        /// Handel ShowAutoTestDialog request.
        /// </summary>
        private void xtraUserControlSpotCheckResults_ShowAutoTestDialog(SpotCheckResult result)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlSpotCheckResults.ShowAutoTestDialogEventHandler(xtraUserControlSpotCheckResults_ShowAutoTestDialog), result);
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

        /// <summary>
        /// Handel CellChanging for results.
        /// </summary>
        private void xtraUserControlSpotCheckResults_CellChanging()
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlSpotCheckResults.CellchangingEventHandler(xtraUserControlSpotCheckResults_CellChanging));
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
        /// Handel the deleting for spot check result.
        /// </summary>
        private void xtraUserControlSpotCheckResults_SpotCheckReseltDeleted(SpotCheckResult result)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlSpotCheckResults.SpotCheckReseltDeletedEventHandler(xtraUserControlSpotCheckResults_SpotCheckReseltDeleted));
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                DeleteSpotCheckResult(result);
            }
        }

        /// <summary>
        /// Handel for the broadcast request from the results grids.
        /// </summary>
        /// <param name="results"></param>
        private void xtraUserControlSpotCheckResults_BroadcastRequest(List<SpotCheckResult> results)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlSpotCheckResults.BroadcastRequestEventHandler(xtraUserControlSpotCheckResults_BroadcastRequest), results);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                Broadcast();
            }
        }

        /// <summary>
        /// Handle the handle the items grid check buttons status changed.
        /// </summary>
        private void ItemsGridCheckButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(ItemsGridCheckButtons_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == checkButtonOneByOneMode)
                {
                    SetOneByOneMode(checkButtonOneByOneMode.Checked);
                }
                else if (sender == checkButtonStarredMode)
                {
                    SetStarredMode(checkButtonStarredMode.Checked);
                }
            }
        }

        /// <summary>
        /// Handle the clicking on grid items buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsGridButtons_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(ItemsGridButtons_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == simpleButtonAddToResults)
                {
                    AddToResults();
                }
                else if (sender == simpleButtonClearSelection)
                {
                    ClearSelection();
                }
                else if (sender == simpleButtonReload)
                {
                    ReloadItems();
                }
                else if (sender == simpleButtonSplit)
                {
                    SplitItems();
                }
                else if (sender == simpleButtonSwitch)
                {
                    SwitchSelection();
                }
            }
        }

        /// <summary>
        /// Handles the row style event.The coloring or hilighting logic of the items.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewItemsNav_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowStyleEventHandler(gridViewItemsNav_RowStyle), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_currentAutoTestMode != SpotCheckAutoTestMode.SplitAndSwitch || !IsSplitSwitchAndStarredModeValid)
                    return;

                double value = (float)(gridViewItems.DataRowCount / 2.0);

                var isOddList = (value - Math.Floor(value)) == 0 ? false : true;

                if (isOddList)
                {
                    if (_partHilight == 1)
                    {
                        if (e.RowHandle >= 0 && (e.RowHandle < (gridViewItems.DataRowCount / 2)))
                        {
                            e.Appearance.BackColor = Color.FromArgb(240, 252, 0);
                            e.Appearance.BackColor2 = Color.AliceBlue;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (e.RowHandle >= 0 && (e.RowHandle <= (gridViewItems.DataRowCount / 2)))
                        {
                            e.Appearance.BackColor = Color.FromArgb(240, 252, 0);
                            e.Appearance.BackColor2 = Color.AliceBlue;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                }
                else
                {
                    if (e.RowHandle >= 0 && (e.RowHandle < (gridViewItems.DataRowCount / 2)))
                    {
                        e.Appearance.BackColor = Color.FromArgb(240, 252, 0);
                        e.Appearance.BackColor2 = Color.AliceBlue;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }

                if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Handel the selection changed for the items grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SelectionChangedEventHandler(gridViewItems_SelectionChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ProcessSelectedRowChanged();
            }
        }

        /// <summary>
        /// Handle the cick on set details button.
        /// </summary>
        private void simpleButtonSetDetails_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonSetDetails_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowSetDetailsAutoTestDialog();
            }
        }

        #endregion

        #region Printing

        /// <summary>
        /// Print the MTE report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintMTE_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintMTE_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintMTE(false);
            }
        }

        /// <summary>
        /// Print preview MTE report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintPreviewMTE_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintPreviewMTE_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintMTE(true);
            }
        }

        /// <summary>
        /// Print Minerals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintMinerals_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintMinerals_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintMinerals(false);
            }
        }

        /// <summary>
        /// Print preview minerals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintPreviewMinerals_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintPreviewMinerals_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintMinerals(true);
            }
        }

        /// <summary>
        /// Print CapsolT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintCapsolT_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintCapsolT_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintCapsolT(false);
            }
        }

        /// <summary>
        /// Print preview CapsolT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintPreviewCapsolT_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintPreviewCapsolT_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintCapsolT(true);
            }
        }

        /// <summary>
        /// Print the DMPS report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintDMPS_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintDMPS_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintDMPS(false);
            }
        }

        /// <summary>
        /// Print preview the DMPS report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintPreviewDMPS_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintPreviewDMPS_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PrintDMPS(true);
            }
        }

        #endregion

        #endregion
    }
}

