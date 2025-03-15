using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGauges.Core.Resources;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Microsoft.VisualBasic.Devices;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Memory_Shell_Objects;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;
using Vital.UI.UI_Components.Reports;
using Vital.UI.UI_Components.User_Controls.Modules;
using System.Drawing;
using Vital.UI.UI_Components.UI_Classes;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using CellValueChangedEventHandler = DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmGeneralTest : VitalBaseForm
    {
        #region Fields

        #region Managers

        private ItemsManager _itemManager;
        private TestsManager _testsManager;
        private LookupsManager _lookupsManager;
        private ReadingsManager _readingsManager;
        private SettingsManager _settingsManager;
        private TestProtocolsManager _testProtocolsManager;
        private SpotCheckManager _spotCheckManager;
        private ShippingOrdersManager _shippingOrdersManager;
        private VitalForceSheetManager _vfsManager;

        #endregion

        #region Dinamic Components

        private BarButtonItem _barButtonItemPausePlay;
        private XtraUserControlVitalRichEdit _xtraUserControlVitalRichEditNotes;
        
        #endregion

        #region Status Indecators and objects

        private Reading _currentReading;
        private TestPlayStateEnum _currentTestPlayState;
        private TestStage _currentAutomaticStage;
        private XtraUserControlItemsNavGrid _currentControlItemsNavGridRequestor;
        private XtraUserControlItemsNavGrid _currentFoucsControlItemsNavGrid;
        private ReadingPlayTypes _currentAutoPlayTestType = ReadingPlayTypes.Eds;

        #endregion

        #region Flags

        private int _currentLocationLookupId;
        private int _autoTestNextReadingTimeOut;
        private int _lastItemTestingReadingValue;
        private bool _showPointFullName;
        private bool _isInReadingRunningMode;
        private bool _isRebinding;
        private bool _edsIsDone;
        private bool _isInBalancingMode;
        private bool _isBalancingDone;
        private bool _isAddingEdsProduct;
        private bool _allowSummaryUpdate;

        //Should the Add Major issue button enabled when the form become editable, this case happens when there are one or more item selected in the items grid.
        private bool _shouldTheAddMajorIssueEnabledOnEdit;
        private bool _isWaitingCsaRealsedToTakeNewReading;

        private bool _isCsaUnitConnected;

        private object _connectionLooker;
        private bool _servicesChanged;
        private bool _shippingOrdersChanged;
        private bool _patientDetailsChanged;
        private bool _testImprintableItemsChanged;
        private bool _imprintingTreeExpanded;
        private bool _treeMouseDown;
        private TreeDragAction _currentDragAction;
        private int _lowerNodeY;
        private bool _isImprinting;
        private int _imprintingCounter;
        private int _imprintingLightCounter;
        private decimal _imprintingPagingCounter;
        private bool _newSavedTest;
        private bool _ignoreAutoTestDoneMessage;//Used to ignore AutoTestDone message when automatically activating automation when opening an existing test

        #endregion

        #region Variables

        private Lookup _bothListPointLookup;
        private Lookup _leftListPointLookup;
        private Lookup _rightListPointLookup;
        private int _itemTypePointId;
        private int _prevNonFifty = -1;
        private int _broadcastingStage;
        private GenderEnum _gender;
        private static int _productTypeLookupId;
        private static int _potencyTypeLookupId;
        private static int _nonelistTypeLookupId;
        private static int _yesLookupId;
        private int _leftLookupId;
        private int _rightLookupId;
        private Item _currentSelectedItem;
        private List<Item> _updatedDescriptionItems; 
        private SpotCheck _currentSpotCheck;
        private VFS _currentVfs;
        private Lookup _serviceSystemType;        

        // save the status of item details group to revert the group for its old status after it hided automatically.
        private bool _shouldDetailsGroupExpended;
        private TestHelper _testHelper;
        private FormStatusEnum _imprintingFormStatus;
        private SoundPlayer _imprintingSoundPlayer;
        private BindingList<ReadingPointSet> _readingPointSets;
 
        #endregion

        #region Deleted objects containers

        private BindingList<TestIssue> _deletedTestIssues;
        private BindingList<ScheduleLine> _deletedScheduleLines;
        private BindingList<TestService> _deletedServices;
        private BindingList<ShippingOrder> _deletedOrders;

        #endregion

        #endregion

        #region Properties

        #region Public Properties

        /// <summary>
        /// Returns the current test.
        /// </summary>
        public Test TestObject { get; private set; }

        /// <summary>
        /// Returns the current test reading.
        /// </summary>
        public Reading CurrentReading
        {
            get { return _currentReading; }
            set
            {
                _currentReading = value;
                UpdateImageAndGaugesForReading();
            }
        }

        /// <summary>
        /// Flag to indicate that patient profile info changed using shipping orders area
        /// </summary>
        public bool PatientDetailsChanged
        {
            get
            {
                return _patientDetailsChanged;
            }
            set
            {
                _patientDetailsChanged = value;
            }
        }

        /// <summary>
        /// Test Logic helprer
        /// </summary>
        public TestHelper TestLogicHelper
        {
            get
            {
                return _testHelper;
            }
            set
            {
                _testHelper = value;
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Returns true if the EDS are all done
        /// </summary>
        private bool AreEdsReadingsDone
        {
            get
            {
                //EDS is done when test is saved or all points are tested or if the user decides to leave the tab before
                //points testing is done.
                return TestObject.ObjectState != DomainEntityState.New ||
                       FilteredReadings.Count(r => r.Value == 0) == 0 ||
                       _edsIsDone;
            }
        }

        /// <summary>
        /// Returns true if the EDS readings are all empty
        /// </summary>
        private bool EdsReadingsNotStarted
        {
            get { return FilteredReadings.Count(r => r.Value != 0) == 0; }
        }

        /// <summary>
        /// Gets the test issues items.
        /// </summary>
        private List<Item> TestIssuesItems
        {
            get
            {
                if (TestObject.TestIssues != null)
                {
                    return TestObject.TestIssues.Select(i => i.Item).ToList();
                }

                return null;
            }
        }

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
        /// Imprinting stage.
        /// </summary>
        private int ImprintingStage { get; set; }

        /// <summary>
        /// Is the application imprinting now.
        /// </summary>
        private bool IsImprinting { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Sets The current reading value on the gauges.
        /// </summary>
        public float ReadingValue
        {
            set { xtraUserControlReadingGauge.ReadingValue = value; }
        }

        /// <summary>
        /// Sets the current reading location on the gauges.
        /// </summary>
        public Lookup LocationLookup
        {
            set { xtraUserControlReadingGauge.LocationLookup = value; }
        }

        /// <summary>
        /// Gets or sets the gender enum.
        /// </summary>
        public GenderEnum Gender
        {
            get { return xtraUserControlItemDetails.Gender; }
            set { xtraUserControlItemDetails.Gender = value; }
        }

        /// <summary>
        /// The yes and no label indicator will be shown or not.
        /// </summary>
        public bool ShowYesNoLabel
        {
            get { return xtraUserControlReadingGauge.ShowYesNoLabel; }
            set { xtraUserControlReadingGauge.ShowYesNoLabel = value; }
        }

        /// <summary>
        /// Gets the current focused test service
        /// </summary>
        private TestService FocusedService
        {
            get
            {
                return (gridViewServices.IsDataRow(gridViewServices.FocusedRowHandle)) ?
                        gridViewServices.GetFocusedRow() as TestService : null;
            }
        }

        /// <summary>
        /// Gets the current focused shipping order
        /// </summary>
        private ShippingOrder FocusedShippingOrder
        {
            get
            {
                return (gridViewShippingOrders.IsDataRow(gridViewShippingOrders.FocusedRowHandle)) ?
                        gridViewShippingOrders.GetFocusedRow() as ShippingOrder : null;
            }
        }

        /// <summary>
        /// Gets if the focused shipping order is sent or not
        /// </summary>
        private bool OrderSent
        {
            get
            {
                return FocusedShippingOrder == null ? false : FocusedShippingOrder.Sent;
            }
        }

        /// <summary>
        /// Gets if teh test has offline orders
        /// </summary>
        private bool HasOfflineOrders
        {
            get
            {
                return TestObject.ShippingOrders.Any(so => !so.Sent) && TestObject.ShippingOrders.Any(); 
            }
        }

        /// <summary>
        /// Returns true if the focused test service is a system service
        /// </summary>
        private bool IsSystemService
        {
            get
            {
                return FocusedService != null && FocusedService.TypeLookup.Id == _serviceSystemType.Id;
            }
        }

        /// <summary>
        /// Gets the imprinting time int value
        /// </summary>
        private int ImprintingTime
        {
            get
            {
                return (int) spinEditImprentingTime.Value;
            }
        }

        /// <summary>
        /// Readings filtered by PointSet and Direction
        /// </summary>
        private List<Reading> FilteredReadings
        {
            get
            {
                return _bothListPointLookup == null? TestObject.Readings.ToList():
                    _currentLocationLookupId == _bothListPointLookup.Id ?
                        TestObject.Readings.Where(r => r.PointSetItemId == TestObject.Item.Id).ToList():
                        TestObject.Readings.Where(r => r.PointSetItemId == TestObject.Item.Id && r.ListPointLookupId == _currentLocationLookupId).ToList();
            }
        }

        #endregion

        #endregion

        #region Constructors

        public frmGeneralTest(Test test)
        {
            Check.Argument.IsNotNull(() => test);
            Check.Argument.IsNotNull(() => test.Patient);

            _settingsManager = new SettingsManager();

            IsLoaded = false;

            UiHelperClass.ShowWaitingPanel(StaticKeys.InitilizingUserInterface);
            InitializeComponent();
            TestObject = test;

            _currentSpotCheck = new SpotCheck();

            CustomeInitializeComponent();
        }

        #endregion

        #region Methods

        #region Initialization & Binding & Helpers

        #region Vital Base Form Ui Overrided Methods

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public override void PerformSpecificIntializationSteps()
        {
            _testsManager = new TestsManager();
            _lookupsManager = new LookupsManager();
            _readingsManager = new ReadingsManager();
            _lookupsManager = new LookupsManager();
            _itemManager = new ItemsManager();
            _spotCheckManager = new SpotCheckManager();
            _shippingOrdersManager = new ShippingOrdersManager();
            _vfsManager = new VitalForceSheetManager();
            _currentTestPlayState = TestPlayStateEnum.Paused;
            _settingsManager = new SettingsManager();
            _testProtocolsManager = new TestProtocolsManager();
            _currentAutomaticStage = TestStage.Eds;
            _deletedTestIssues = new BindingList<TestIssue>();
            _deletedServices = new BindingList<TestService>();
            TestObject.DeletedTestImprintableItems = new BindingList<TestImprintableItem>();
            _deletedOrders = new BindingList<ShippingOrder>();
            _deletedScheduleLines = new BindingList<ScheduleLine>();
            _updatedDescriptionItems = new List<Item>();
            _readingPointSets = new BindingList<ReadingPointSet>();
            _testHelper = new TestHelper();

            _connectionLooker = new object();

            _allowSummaryUpdate = true;
            _shouldDetailsGroupExpended = true;

            var isNew = TestObject.Id == 0;

            var fontSizeSetting =
                _settingsManager.GetSetting(new SettingsFilter {Key = EnumNameResolver.Resolve(SettingKeys.FontSize)});

            var fontSize = float.Parse(fontSizeSetting.Value.ToString());

            //This lookup is important to be filled here since it is being used during initialization
            _serviceSystemType =
                    UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ServiceType,
                        ServiceType.SystemService, false, true));

            SetFonts(fontSize);

            SetupAutoTestTimerAndProgressBar();

            InitTestObject(isNew);

            SetDefaultPointsNameSetting();

            InitLayoutControls();

            SetFormStatus(isNew);

            SetFormTitle(TestObject.Patient.FirstName + " " + TestObject.Patient.LastName);

            SetDefaultTestScheduleSettings();

            InitNavGridMajorIssues();

            var itemTypeLookup = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Point));
            var firstOrDefault = itemTypeLookup.FirstOrDefault();
            if (firstOrDefault != null) _itemTypePointId = firstOrDefault.Id;

            FillLookUps();

            SetDefaultReadingsLocationSetting();

            var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (!designMode)
            {
                FillLocalLookupIds();
            }
            
            SetDefaultMeterPositionSettings();
            SetDefaultImprintableItems();
            SetDefaultScheduleSettings();
            SetImageAreaSettings();
            _imprintingSoundPlayer =  new SoundPlayer { Stream = Properties.Resources.ImprintingSound };

        }

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        public override sealed void CustomeInitializeComponent()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingCustomComponents);

            var position = 7;
            _barButtonItemPausePlay = AddBarButtonItem("barButtonItemPausePlay", "Play", false, Resources.Test_Play, 0,
                                                       0, false,
                                                       new BarShortcut((Keys.Control | Keys.P)), true,
                                                       "Play/Pause",
                                                       "Click this button to start automatic navigation between points.\r\n",
                                                       "You can use Ctrl+P to toggle this button.", true);

            AddBarButtonItem("barButtonItemPrint", "Print", true, Resources.Print, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.P)), true,
                             "Print",
                             "Click this button to print current test according the selected settings in printing area.\r\n",
                             "You can use Alt+P to print current test.", true);
            position += 1;
            AddBarButtonItem("barButtonItemPrintShort", "Print Short", true, Resources.Print, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.Shift | Keys.P)), true,
                             "Print",
                             "Click this button to print current test short with no description.\r\n",
                             "You can use Alt+Shift+P to print current test.", true);
            position += 1;
            AddBarButtonItem("barButtonItemPrintLong", "Print Long", true, Resources.Print, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.Shift | Keys.Control | Keys.P)), true,
                             "Print",
                             "Click this button to print current test long with description.\r\n",
                             "You can use Alt+Shift+Control+P to print current test.", true);
            position += 1;
            AddBarButtonItem("barButtonItemPrintPreview", "Print Preview", true, Resources.Test_Tab, position,
                             position, false,
                             new BarShortcut((Keys.Alt | Keys.Control | Keys.P)), true,
                             "Print Preview",
                             "Click this button to preview current test report.\r\n",
                             "You can use Alt + Ctrl + P to preview current test report.", true);

            position += 1;
            AddBarButtonItem("barButtonProductInformation", "Product Information", true, Resources.product21, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.Shift | Keys.I)), true,
                             "Product Information",
                             "Click this button to access the latest product information.\r\n",
                             "You can use Alt+Shift+I to access the latest product information.", true);
            
            position += 1;
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
            AddBarButtonItem("barButtonItemSpotCheck", "Spot Check", true, Resources.SpotCheck, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.K)), true,
                             "Spot Check",
                             "Click this button to start spot check for the current test.\r\n",
                             "You can use Alt+K to start a spot check.", true);
            position += 1;
            AddBarButtonItem("barButtonItemVFS", "Vital Force Sheet", true, Resources.VFS, position,
                             position, true,
                             new BarShortcut((Keys.Alt | Keys.Shift | Keys.V)), true,
                             "Vital Force Sheet",
                             "Click this button to start vital force sheet for the current test.\r\n",
                             "You can use Alt + Shift + V to start a vital force sheet.", true);
            position += 1;
            radioGroupEdsMode.SelectedIndex = 0;

            imageListImprinting.Images.Add("NonImprinted", Resources.ImprintingClear);
            imageListImprinting.Images.Add("Imprinted", Resources.Imprint16x16);
            _currentDragAction = TreeDragAction.None;
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.SetupHandlersMessage);
            TestObject.PropertyChanged += TestObject_PropertyChanged;
            TestObject.TestSchedule.PropertyChanged += TestSchedule_PropertyChanged;
            UiHelperClass.HideSplash();
        }
        
        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.ClearHandlersMessage);

            TestObject.PropertyChanged -= TestObject_PropertyChanged;
            TestObject.TestIssues.ListChanged -= TestIssues_ListChanged;
            TestObject.TestServices.ListChanged -= TestServices_ListChanged;
            TestObject.ShippingOrders.ListChanged -= ShippingOrders_ListChanged;
            ClearHandlersTestIssues();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);
            Gender = _gender;
            UiHelperClass.BindControl(textEditTestName, TestObject, () => TestObject.Name);
            UiHelperClass.BindControl(textEditDescription, TestObject, () => TestObject.Description);
            UiHelperClass.BindControl(textEditCreatedOn, TestObject, () => TestObject.DateTime);
            UiHelperClass.BindControl(lookUpEditType, TestObject, () => TestObject.TypeLookup,
                                      () => TestObject.TypeLookup.Id);
            
            BindTestState();
            UiHelperClass.BindControl(lookUpEditReadingLocation, TestObject, () => TestObject.ListPointLookup,
                                      () => TestObject.ListPointLookup.Id);
            UiHelperClass.BindControl(gridLookUpEditItemGroup, TestObject, () => TestObject.Item,
                                      () => TestObject.Item.Id);
            UiHelperClass.BindControl(lookUpEditTestProtocol, TestObject, () => TestObject.TestProtocol,
                                      () => TestObject.TestProtocol.Id);
            UiHelperClass.BindControl(textEditNumberOfIssues, TestObject, () => TestObject.NumberOfIssues);

            UiHelperClass.BindControl(checkEditShippingOrderSent, TestObject,
                                          () => TestObject.IsOrderSent);
            
            BindVitalRichEditControl();
            xtraUserControlPrintingOptionsMain.UpdateOptions();

            UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);
            if (TestObject.TestSchedule != null)
            {                
                UiHelperClass.BindControl(spinEditReevaluationDate, TestObject.TestSchedule,
                                          () => TestObject.TestSchedule.ReevalInWeeks);
                UiHelperClass.BindControl(spinEditTax, TestObject.TestSchedule, () => TestObject.TestSchedule.Tax);
                UiHelperClass.BindControl(spinEditAdjustment, TestObject.TestSchedule, () => TestObject.TestSchedule.Discount);
                SetAdjustmentTypeImage(TestObject.TestSchedule.DiscountAsPercentage);

                UiHelperClass.BindControl(checkEditIsCreditCard, TestObject.TestSchedule,
                                          () => TestObject.TestSchedule.IsCreditCard);
                UiHelperClass.BindControl(checkEditIsCash, TestObject.TestSchedule, () => TestObject.TestSchedule.IsCash);
                UiHelperClass.BindControl(checkEditIsCheck, TestObject.TestSchedule,
                                          () => TestObject.TestSchedule.IsCheck);
                UiHelperClass.BindControl(textEditCheckNumber, TestObject.TestSchedule,
                                          () => TestObject.TestSchedule.CheckNumber);

                UpdateCheckNumberVisibility();

                UiHelperClass.BindControl(memoEditSpecialInstructions, TestObject.TestSchedule,
                                          () => TestObject.TestSchedule.SpecialInstructions);
                UiHelperClass.BindControl(lookUpEditReEvaluationType, TestObject.TestSchedule, () => TestObject.TestSchedule.EvalPeriodType, () => TestObject.TestSchedule.EvalPeriodType.Id);

                UiHelperClass.BindControl(lookUpEditAdjustmentApply, 
                    TestObject.TestSchedule, 
                    () => TestObject.TestSchedule.DiscountApply,
                    () => TestObject.TestSchedule.DiscountApply.Id);
                
                BindScheduleLines();
            }

            SetResultsViewDataSource();

            SetupReadings(TestObject.Item.Id);
            BindMajorIssues();
            BindServices();
            BindShippingOrders();
            SetBindingTestIssues();
            BindTestImprintableItems();

            CurrentReading = gridViewItems.GetFocusedRow() as Reading;
            

            gridLookUpEditItemGroup.Update();
            lookUpEditTestProtocol.Update();
            SetNextIssueNumberText();
            ShowHideAdjustmentFields();
            SetCountDownString(ImprintingTime, false);

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Binds test state lookup
        /// </summary>
        private void BindTestState()
        {
            lookUpEditStatus.DataBindings.Clear();
            UiHelperClass.BindControl(lookUpEditStatus, TestObject, () => TestObject.StateLookup,
                                      () => TestObject.StateLookup.Id);
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public override void AfterLoadAction()
        {
            OpenCsaConnection();

            if (TestObject.ObjectState == DomainEntityState.New)
            {
                SaveAction();
                _newSavedTest = true;
            }
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = TestObject;
            dxErrorProviderState.DataSource = TestObject;
            dxErrorProviderType.DataSource = TestObject;

            dxErrorProviderState.ClearErrors();
            dxErrorProviderType.ClearErrors();

            dxErrorProviderState.UpdateBinding();
            dxErrorProviderType.UpdateBinding();
        }

        /// <summary>
        /// Setting some properties.
        /// </summary>
        public override void SetProperties()
        {
            textEditTestName.Properties.MaxLength = 50;
            textEditDescription.Properties.MaxLength = 50;
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.ClearHandlersMessage);
            textEditTestName.DataBindings.Clear();
            textEditDescription.DataBindings.Clear();
            textEditTestName.DataBindings.Clear();
            textEditCreatedOn.DataBindings.Clear();
            lookUpEditType.DataBindings.Clear();
            lookUpEditReEvaluationType.DataBindings.Clear();
            lookUpEditAdjustmentApply.DataBindings.Clear();
            lookUpEditStatus.DataBindings.Clear();
            lookUpEditReadingLocation.DataBindings.Clear();
            checkEditShippingOrderSent.DataBindings.Clear();
            lookUpEditTestProtocol.DataBindings.Clear();
            gridLookUpEditItemGroup.DataBindings.Clear();
            gridLookUpEditItemGroupView.ClearSelection();
            textEditNumberOfIssues.DataBindings.Clear();
            ClearBindingTestIssues();

            //In case : undo on new test, reset the edit value of the point group combo box to nothing.
            if (TestObject.ObjectState == DomainEntityState.New)
                gridLookUpEditItemGroup.EditValue = null;

            //Test Schedule Related Bindings
            spinEditReevaluationDate.DataBindings.Clear();
            spinEditTax.DataBindings.Clear();
            spinEditAdjustment.DataBindings.Clear();
            textEditCheckNumber.DataBindings.Clear();
            checkEditIsCash.DataBindings.Clear();
            checkEditIsCheck.DataBindings.Clear();
            checkEditIsCreditCard.DataBindings.Clear();
            memoEditSpecialInstructions.DataBindings.Clear();
            treeListImprintableItems.DataBindings.Clear();
            _testImprintableItemsChanged = false;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Light rebind for specific objects.
        /// </summary>
        public void RebindLight()
        {
            BindPointReadings();
            BindMajorIssues();
            BindServices();
            ClearBindingTestIssues();
            SetBindingTestIssues();
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public override void SetEditMode(bool isReadOnly)
        {
            textEditTestName.Properties.ReadOnly = isReadOnly;
            textEditDescription.Properties.ReadOnly = isReadOnly;
            lookUpEditType.Properties.ReadOnly = isReadOnly;
            lookUpEditReEvaluationType.Properties.ReadOnly = isReadOnly;
            lookUpEditAdjustmentApply.Properties.ReadOnly = isReadOnly;
            lookUpEditStatus.Properties.ReadOnly = isReadOnly;
            lookUpEditReadingLocation.Properties.ReadOnly = isReadOnly;
            gridLookUpEditItemGroup.Enabled = !isReadOnly;
            simpleButtonSetMajorIssuesNumber.Enabled = !isReadOnly;
            simpleButtonSetMajorIssuesNumber.Enabled = !isReadOnly;
            xtraUserControlItemsNavGridMajorIssues.SetEditMode(isReadOnly);
            lookUpEditTestProtocol.Properties.ReadOnly = isReadOnly || TestObject.TestIssues.Count > 0;
            simpleButtonShowItems.Enabled = !isReadOnly && TestObject.TestIssues.Count > 0;
            simpleButtonBalance.Enabled = !isReadOnly;
            radioGroupEdsMode.Enabled = !isReadOnly;
            simpleButtonAddProducts.Enabled = !isReadOnly;
            simpleButtonSendShippingOrder.Enabled = !isReadOnly && TestObject.ObjectState != DomainEntityState.New;
            
            if (_xtraUserControlVitalRichEditNotes != null)
                _xtraUserControlVitalRichEditNotes.ReadOnly = isReadOnly;

            if (isReadOnly)
            {
                CsaEmdUnitManager.Instance.FlushBroadcastBuffer();
            }

            simpleButtonAddMajorIssue.Enabled = _shouldTheAddMajorIssueEnabledOnEdit && !isReadOnly;

            if (isReadOnly)
                SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.HideStatus, 0);

            if (!_isInReadingRunningMode && isReadOnly && _currentTestPlayState == TestPlayStateEnum.Playing)
                StopAutoPlayTestMode();

            SetAutoPlayAllowMode(TestObject.Readings != null && TestObject.Readings.Count > 0);

            SetClearEdsMode();
            xtraUserControlIssueMain.SetEditMode(isReadOnly,true);
            SetTestIssuesEditMode(isReadOnly);

            //Test Schedule related fields
            textEditCheckNumber.Properties.ReadOnly = isReadOnly;
            spinEditReevaluationDate.Properties.ReadOnly = isReadOnly;
            spinEditReevaluationDate.Properties.Buttons[1].Enabled = !isReadOnly;
            spinEditTax.Properties.ReadOnly = isReadOnly;
            spinEditAdjustment.Properties.ReadOnly = isReadOnly;
            checkEditShippingOrderSent.Properties.ReadOnly = true;
            checkEditIsCash.Properties.ReadOnly = isReadOnly;
            checkEditIsCheck.Properties.ReadOnly = isReadOnly;
            checkEditIsCreditCard.Properties.ReadOnly = isReadOnly;
            memoEditSpecialInstructions.Properties.ReadOnly = isReadOnly;
            gridViewScheduleLines.OptionsBehavior.ReadOnly = isReadOnly;
            gridViewInvoicing.OptionsBehavior.ReadOnly = isReadOnly;
            gridViewTestResults.OptionsBehavior.ReadOnly = true;
            gridViewTestResultFactors.OptionsBehavior.ReadOnly = true;

            gridViewServices.OptionsBehavior.ReadOnly = isReadOnly;
            gridViewShippingOrders.OptionsBehavior.ReadOnly = true;

            lookUpEditServices.Properties.ReadOnly = isReadOnly;
            simpleButtonAddService.Enabled = !isReadOnly;

            CustomBarItemEditMode("barButtonItemSpotCheck", TestObject.ObjectState == DomainEntityState.New);
            CustomBarItemEditMode("barButtonItemVFS", TestObject.ObjectState == DomainEntityState.New);
            checkEditShowDefaultScheduleInstructions.Properties.ReadOnly = isReadOnly;

            SetImprintingEditMode(isReadOnly);
        }

        /// <summary>
        /// Sets edit mode for imprinting controls
        /// </summary>
        /// <param name="isReadOnly"></param>
        private void SetImprintingEditMode(bool isReadOnly)
        {
            //Imprinting button enabled if form is enabled
            simpleButtonToggleImprinting.Enabled = !isReadOnly || _isImprinting;
            simpleButtonImprintCollapseAll.Enabled = !_isImprinting;
            simpleButtonImprintExpandAll.Enabled = !_isImprinting;

            var readOnlyImprinting = isReadOnly || _isImprinting;

            treeListImprintableItems.OptionsBehavior.Editable = !readOnlyImprinting;
            treeListImprintableItems.OptionsBehavior.DragNodes = !readOnlyImprinting;
            simpleButtonAddImprintableItem.Enabled = !readOnlyImprinting;
            simpleButtonImprintCheckAll.Enabled = !readOnlyImprinting;
            simpleButtonImprintUncheckAll.Enabled = !readOnlyImprinting;
            EnableDisableImprintingOrderButtons();
            checkEditUseImprintingSound.Properties.ReadOnly = readOnlyImprinting;
            checkEditUseTimer.Properties.ReadOnly = readOnlyImprinting;
            spinEditImprentingTime.Properties.ReadOnly = readOnlyImprinting;
        }

        /// <summary> 
        /// Fill the lookup controls with the collections of objects from the cache
        /// </summary>
        public override void FillLookUps()
        {
            try
            {
                gridLookUpEditItemGroup.Properties.DataSource =
                    CacheHelper.SetOrGetCachableData(CachableDataEnum.ItemsGroup);
                lookUpEditTestProtocol.Properties.DataSource =
                    CacheHelper.SetOrGetCachableData(CachableDataEnum.TestProtocols);
                lookUpEditType.Properties.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.TestTypes);
                lookUpEditStatus.Properties.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.TestStates);
                lookUpEditReadingLocation.Properties.DataSource =
                    CacheHelper.SetOrGetCachableData(CachableDataEnum.PointsList);
                lookUpEditReEvaluationType.Properties.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.EvalPeriodTypes);
                lookUpEditAdjustmentApply.Properties.DataSource = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.AdjustmentApply));
                UiHelperClass.FillLookup(repositoryItemLookUpEditServiceType,
                    UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ServiceType)));
                UiHelperClass.FillLookup(repositoryItemLookUpEditReadingDirection,
                    UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ListPoints)));
                xtraUserControlItemDetails.FillLookups();

                FillServicesLookups();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Fill the services lookup edit.
        /// This code moved to another fill lookup method, because services list need to be refresh after change settings screen. 
        /// </summary>
        private void FillServicesLookups()
        {
            try
            {
                lookUpEditServices.Properties.DataSource =
                    CacheHelper.SetOrGetCachableData(CachableDataEnum.Services);
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Sets the Issue Image and desc'.
        /// </summary>
        private void SetUiItemDetailsMajorIssues()
        {
            var testIssue = gridViewMajorIssues.GetRow(gridViewMajorIssues.FocusedRowHandle) as TestIssue;

            if (testIssue == null || testIssue.Item == null) return;

            var selectedItems = new BindingList<Item> {testIssue.Item};

            SetDetails(selectedItems,
                            null,
                            null,
                            false,
                            null,
                            null);

            UpdateReading(null, 0);
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
                ToggleTestPlayState();
            }
            else if (itemName.Equals("barButtonItemEdit"))
            {
                //To avoid the selected row inside reading grid.
                ResetFocus();
                //
                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
            else if (itemName.Equals("barButtonItemDisable"))
            {
                ControlItemDetailesAndReadingsDependOnUserAction(true);
            }
            else if (itemName.Equals("barButtonItemPrint"))
            {
                Print(false,xtraUserControlPrintingOptionsMain.ShowDescription);
            }
            else if (itemName.Equals("barButtonItemPrintShort"))
            {
                Print(false,false);
            }
            else if (itemName.Equals("barButtonItemPrintLong"))
            {
                Print(false,true);
            }
            else if (itemName.Equals("barButtonItemPrintPreview"))
            {
                Print(true, xtraUserControlPrintingOptionsMain.ShowDescription);
            }
            else if (itemName.Equals("barButtonProductInformation"))
            {
                var frmProductsInfo = new frmProductsInfo();
                frmProductsInfo.ShowDialog();
            }
            else if (itemName.Equals("barButtonItemSettings"))
            {
                CloseCsaConnection();
                UpdateImprintingSettings();
                ShowHideOverlay(true);
                var frmSettings = new frmSettings();

                //Get the services that are in use 
                var servicesInUse = new BindingList<Service>();

                //Add services that are in the use
                foreach (var service in TestObject.TestServices.Where(ts => ts.Service != null))
                {
                    servicesInUse.Add(service.Service);
                }

                //Add services that are in the deletion list
                foreach (var service in _deletedServices.Where(ts => ts.Service != null))
                {
                    servicesInUse.Add(service.Service);
                }

                frmSettings.ServicesInUse = servicesInUse;
                frmSettings.ShowDialog();
                ShowHideOverlay(false);
                OpenCsaConnection();
                FillServicesLookups();
                ShowHideAdjustmentFields();
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
            else if (itemName.Equals("barButtonItemSpotCheck"))
            {
                if (TestObject.ObjectState != DomainEntityState.New)
                    _currentSpotCheck = GetRelatedSpotCheck();
                OpenSpotCheck(_currentSpotCheck == null);
            }
            else if (itemName.Equals("barButtonItemVFS"))
            {
                if (TestObject.ObjectState != DomainEntityState.New)
                    _currentVfs = GetRelatedVFS();
                OpenVfs(_currentVfs == null);
            }
        }

        /// <summary>
        /// Open a spot check.
        /// </summary>
        private void OpenSpotCheck(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Spot Check ..." : "Loading Spot Check ...");

                BeforeReadingDialogOpened();

                var currentSpotCheck = isNew
                                           ? new SpotCheck
                                           {
                                               Patient = TestObject.Patient,
                                               Name =
                                                   string.Format(StaticKeys.NewSpotCheck,
                                                                 TestObject.Patient.FirstName),
                                               TestId = TestObject.Id
                                           }
                                           : _currentSpotCheck;


                var spotCheckFrm = new frmSpotCheck(currentSpotCheck);

                spotCheckFrm.ShowDialog();

                AfterReadingDialogClosed();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        private SpotCheck GetRelatedSpotCheck()
        {
            return _spotCheckManager.GetSpotChecks(new SpotChecksFilter()
            {
                TestId = TestObject.Id

            }).FirstOrDefault();
        }

        /// <summary>
        /// Open a VFS.
        /// </summary>
        private void OpenVfs(bool isNew)
        {
            try
            {
                var openVFS = true;

                if (TestObject.ObjectState == DomainEntityState.Modified)
                {
                    openVFS = SaveAction();
                }

                if (openVFS)
                {
                    UiHelperClass.ShowWaitingPanel(isNew ? "New Vital Force Sheet ..." : "Loading Vital Force Sheet ...");

                    BeforeReadingDialogOpened();

                    var currentVfs = isNew
                                               ? new VFS
                                               {
                                                   Patient = TestObject.Patient,
                                                   Name = string.Format(StaticKeys.NewVFS, TestObject.Patient.FirstName),
                                                   TestId = TestObject.Id,
                                                   DateTime = DateTime.Now
                                               }
                                               : _currentVfs;


                    var vfsFrm = new frmVFS(currentVfs);

                    vfsFrm.ShowDialog();

                    AfterReadingDialogClosed();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        private VFS GetRelatedVFS()
        {
            return _vfsManager.GetVFSs(new VFSsFilter()
            {
                TestId = TestObject.Id

            }).FirstOrDefault();
        }

        #endregion

        #region UI Inti

        /// <summary>
        /// Set Default TestSchedule Settings.
        /// </summary>
        private void SetDefaultTestScheduleSettings()
        {
            if (TestObject.Id != 0 || TestObject.TestSchedule == null)
                return;

            var settingsList =
                CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings) as BindingList<Setting>;

            if (settingsList == null)
                return;

            var defaultTax = settingsList.FirstOrDefault(
                s => s.Key.Equals(EnumNameResolver.Resolve(SettingKeys.DefaultTestingTax)));

            TestObject.TestSchedule.Tax = Convert.ToDecimal(defaultTax == null ? 0 : defaultTax.Value);            
        }

        /// <summary>
        /// Init the NavGridMajorIssues control.
        /// </summary>
        private void InitNavGridMajorIssues()
        {
            SetNavGridMajorIssuesProtocol();
            xtraUserControlItemsNavGridMajorIssues.InsertedItems = TestIssuesItems;
        }

        /// <summary>
        /// Sets the protocol for the major issues nav grid
        /// </summary>
        private void SetNavGridMajorIssuesProtocol()
        {
            xtraUserControlItemsNavGridMajorIssues.TestProtocol = TestObject.TestProtocol;
        }

        /// <summary>
        /// Init the test object.
        /// </summary>
        /// <param name="isNew">Is new test.</param>
        private void InitTestObject(bool isNew)
        {
            if (isNew)
            {
                TestObject.StateLookup = new Lookup();

                TestObject.Readings = new BindingList<Reading>();
                TestObject.TestIssues = new BindingList<TestIssue>();
                TestObject.TestServices = new BindingList<TestService>();
                TestObject.ShippingOrders = new BindingList<ShippingOrder>();
                TestObject.TestImprintableItems = new BindingList<TestImprintableItem>();

                TestObject.DateTime = DateTime.Now;
                TestObject.EvalPeriodChecked = false;
                TestObject.IsOrderSent = false;

                AddTestMainIssue();

                int adjustmentApplyingDefaultId = 0;
                    int.TryParse(UiHelperClass.GetSettingValueFromCache(
                    SettingKeys.DefaultAdjustmentApplying, CachableDataEnum.VisibleSettings).ToString(),
                    out adjustmentApplyingDefaultId);

                var tempAdjustmentApplyLookup =
                    UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.AdjustmentApply, false))
                            .FirstOrDefault(l => l.Id == adjustmentApplyingDefaultId);

                Lookup defaultAdjustmentApplyingLookup = null;

                defaultAdjustmentApplyingLookup = UiHelperClass.GetSingleLookupFromCache(
                    LookupsFilter.As(LookupTypes.AdjustmentApply,
                        EnumNameResolver.LookupAsEnum<AdjustmentApplyEnum>(tempAdjustmentApplyLookup.Value), false, false));

                TestObject.TestSchedule = new TestSchedule
                                              {
                                                  ScheduleLines = new BindingList<ScheduleLine>(),
                                                  ReevalInWeeks = StaticKeys.EvaluationPeriodDefault,
                                                  DiscountApply = defaultAdjustmentApplyingLookup
                                              };

                //It is important to call adjustment logic after initializing the schedule line to make sure it is
                //not null
                var adjustmentAsPercentSetting = UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultAdjustmentType,
                    CachableDataEnum.VisibleSettings);

                bool adjustmentAsPercent = true;
                if (adjustmentAsPercentSetting != null)
                {
                    adjustmentAsPercent = adjustmentAsPercentSetting.ToString() == UiHelperClass.GetYesLookupId().ToString();
                }

                SetAdjustmentTypeAndValue(adjustmentAsPercent);

                TestObject.StateLookup =
                    UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TestState, TestStateEnum.InProgress, false,
                                                                true));

                var defaultServices =
                    ((BindingList<Service>) CacheHelper.SetOrGetCachableData(CachableDataEnum.Services)).
                        Where(s =>
                            (s.Key == (TestObject.Patient.Tests.Any(t => t.ObjectState != DomainEntityState.New)
                                ? ServiceKeys.DefaultRevalTestingFee.ToString()
                                : ServiceKeys.DefaultTestingFee.ToString()) &&
                                s.IsDefault &&
                                s.TypeLookup.Id == _serviceSystemType.Id) ||
                             s.IsDefault &&
                             s.TypeLookup.Id != _serviceSystemType.Id);

                foreach (var defaultService in defaultServices)
                {
                    AddService(defaultService);
                }
              
                SetDefaultReevaluationPeriodType();
                SetDefaultPointsGroupSetting();
                SetDefaultTestProtocolSetting();
            }
            else
            {
                if (TestObject.TestSchedule == null)
                {
                    TestObject.TestSchedule = new TestSchedule();
                    TestObject.TestSchedule.ReevalInWeeks = StaticKeys.EvaluationPeriodDefault;

                    if (TestObject.TestSchedule.ScheduleLines == null)
                    {
                        TestObject.TestSchedule.ScheduleLines = new BindingList<ScheduleLine>();
                    }
                }

                _isBalancingDone = FilteredReadings.All(r => r.ValueBalanced == 50);
            }
        }

        /// <summary>
        /// Set the controls font.
        /// </summary>
        private void SetFonts(float fontSize)
        {
            gridViewItems.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewAllEDS.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewMajorIssues.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewScheduleLines.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewInvoicing.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewTestResultFactors.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewTestResults.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewServices.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            gridViewShippingOrders.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
            treeListImprintableItems.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
        }

        /// <summary>
        /// Init layout controls.
        /// </summary>
        private void InitLayoutControls()
        {
            UiHelperClass.SetLayoutControlProperties(layoutControlInfo);
            UiHelperClass.SetLayoutControlProperties(layoutControlEDS);
            UiHelperClass.SetLayoutControlProperties(layoutControlTestItems);
            UiHelperClass.SetTabControlProperties(xtraTabControlIssues);
            UiHelperClass.SetTabControlProperties(xtraTabControlItemTesting);
            UiHelperClass.SetTabControlProperties(xtraTabControlTestSchedule);
            UiHelperClass.SetViewProperties(gridViewItems);
            UiHelperClass.SetViewProperties(gridViewAllEDS);
            UiHelperClass.SetViewProperties(gridViewMajorIssues);
            UiHelperClass.SetViewProperties(gridViewScheduleLines);
            UiHelperClass.SetViewProperties(gridViewInvoicing);
        }

        #endregion

        #region Timers Settings and Methods

        /// <summary>
        /// Setup the Auto test timer and progress bar.
        /// </summary>
        private void SetupAutoTestTimerAndProgressBar()
        {
            timerAutoTest.Interval = CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime < 1000
                                         ? CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime
                                         : 1000;

            progressBarControlAutoTestStatus.Properties.Maximum = CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime;

            progressBarControlAutoTestStatus.Properties.Minimum = 0;
        }

        /// <summary>
        /// Start the auto play test timer.
        /// </summary>
        private void StartAutoTestTimer()
        {
            _autoTestNextReadingTimeOut = 0;

            timerAutoTest.Tick -= timerAutoTest_Tick;

            if (_currentAutoPlayTestType == ReadingPlayTypes.Eds)
            {
                SetReadingIndicators(_currentAutoPlayTestType, TestBarStateEnum.WaitMoving,
                                     CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime/1000f);
                progressBarControlAutoTestStatus.EditValue = 0;
            }
            else
            {
                if (_currentControlItemsNavGridRequestor != null)
                {
                    _currentControlItemsNavGridRequestor.SetAutoTestPlayMode(true);

                    SetReadingIndicators(_currentAutoPlayTestType, TestBarStateEnum.WaitBeforTakeAction,
                                     CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime / 1000f);
                }                
            }

            timerAutoTest.Tick += timerAutoTest_Tick;

            timerAutoTest.Enabled = true;
        }

        /// <summary>
        /// Stop the auto test timer.
        /// </summary>
        private void StopAutoTestTimer()
        {
            timerAutoTest.Tick -= timerAutoTest_Tick;

            timerAutoTest.Enabled = false;

            SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.Ready, 0);

            SetReadingIndicators(ReadingPlayTypes.ItemTesting, TestBarStateEnum.Ready, 0);
        }

        #endregion

        #region Metodes and Ui Controlers

        /// <summary>
        /// Set the status test status bar.
        /// </summary>
        private void SetReadingStatusBarMode(TestBarStateEnum state, float secondToWait)
        {
            if (IsClosing)
                return;

            switch (state)
            {
                case TestBarStateEnum.TakeReading:
                    labelControlAutoTestStatus.Text = StaticKeys.TakeReading;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemAutoTestProgressBar.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.Ready:
                    labelControlAutoTestStatus.Text = StaticKeys.Ready;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemAutoTestProgressBar.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.Reading:
                    labelControlAutoTestStatus.Text = StaticKeys.Reading;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemAutoTestProgressBar.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Always;
                    break;
                case TestBarStateEnum.WaitMoving:
                    progressBarControlAutoTestStatus.EditValue = CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime -
                                                                 (secondToWait*1000);
                    labelControlAutoTestStatus.Text = string.Format(StaticKeys.MovingLong, secondToWait);
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemAutoTestProgressBar.Visibility = LayoutVisibility.Always;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.HideStatus:
                    layoutControlItemTestStatus.ContentVisible = false;
                    layoutControlItemAutoTestProgressBar.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.WaitingToRelease:
                    labelControlAutoTestStatus.Text = StaticKeys.ReleaseLong;
                    layoutControlItemTestStatus.ContentVisible = true;
                    layoutControlItemAutoTestProgressBar.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;

            }

        }

        /// <summary>
        /// Set the status of the auto play test button.
        /// </summary>
        /// <param name="isAllowed">Is the auto play allowed.</param>
        private void SetAutoPlayAllowMode(bool isAllowed)
        {
            _barButtonItemPausePlay.Enabled = isAllowed && IsInEditMode;
        }

        /// <summary>
        /// Check if the item is updated for the description.
        /// </summary>
        /// <param name="item"></param>
        private void CheckForDescriptionChange(Item item)
        {
            if(item == null)
                return;

            var updatedItem = _updatedDescriptionItems.FirstOrDefault(i => i.Id == item.Id);

            if(updatedItem == null)
                return;

            item.Description = updatedItem.Description;
        }

        /// <summary>
        /// Binds the images and its description.
        /// </summary>
        private void SetDetails(Item item)
        {
            _currentSelectedItem = item;

            xtraUserControlItemDetails.SetDetails(item);
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
            _currentSelectedItem = selectedItems.Any() ? selectedItems[0] : null;
            xtraUserControlItemDetails.SetDetails(selectedItems, topItems, bottomItems, topItemsHightlighted, parentItem, issueItem);
        }

        /// <summary>
        /// Clear the user control of xtraUserControlItemReadingDetails.
        /// </summary>
        private void ClearItemReadingAndDetails()
        {
            xtraUserControlItemDetails.ClearImageAndDetails();

            UpdateReading(null, 0);
        }

        /// <summary>
        /// Expand or collapse ItemDetailsGroup depends on selected tab.
        /// </summary>
        private void UpdateItemDetailsGroupExpandedStatus()
        {
            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
            {
                layoutControlInfo.GroupExpandChanged -= layoutControlInfo_GroupExpandChanged;
                layoutControlGroupReadingAndImage.Expanded = false;
                layoutControlInfo.GroupExpandChanged += layoutControlInfo_GroupExpandChanged;
            }
            else
            {
                layoutControlGroupReadingAndImage.Expanded = _shouldDetailsGroupExpended;
            }

            //this update for the button mode and for the group it self is required to avoid the group button issue.
            //ex : if group expanded the button sometime keep it self like this (<) which in opposite form what is should be (>)
            layoutControlGroupReadingAndImage.ExpandButtonMode = ExpandButtonMode.Inverted;
            layoutControlGroupReadingAndImage.Update();

        }
        
        /// <summary>
        /// Gets the selected XtraUserControlIssue, depends on the tab that shown to the user.
        /// </summary>
        /// <returns></returns>
        private XtraUserControlIssue GetSelectedIssueUserConrol()
        {
            return xtraTabControlIssues != null && xtraTabControlIssues.SelectedTabPage != null &&
                   xtraTabControlIssues.SelectedTabPage.Controls.Count > 0
                       ? xtraTabControlIssues.SelectedTabPage.Controls[0] as XtraUserControlIssue
                       : null;
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
            //

            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
            {
                ShowYesNoLabel = false;

                UpdateImageAndGaugesForReading();

                SetAutoPlayAllowMode(TestObject.Readings != null && TestObject.Readings.Count > 0);

                StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
                StopReading(ReadingRequesterTypeEnum.Others);

                if (forceStopReading)
                {
                    StopReading(ReadingRequesterTypeEnum.Eds);
                }
                else if (IsInEditMode)
                {
                    StartReading(ReadingRequesterTypeEnum.Eds, GetItemsToBroadcast());

                    if (_currentTestPlayState == TestPlayStateEnum.Playing)
                        CheckThenMoveNextReading();
                }

                _currentAutoPlayTestType = ReadingPlayTypes.Eds;
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule || xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageNotes)
            {
                if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
                    _edsIsDone = true;

                ShowYesNoLabel = true;

                StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
                StopReading(ReadingRequesterTypeEnum.Eds);
                StopReading(ReadingRequesterTypeEnum.Others);

                ClearItemReadingAndDetails();

                if (forceStopReading)
                {
                    StopReading(ReadingRequesterTypeEnum.Others);
                }
                else if (IsInEditMode)
                {
                    StartReading(ReadingRequesterTypeEnum.Others, null);
                }
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageProductsTesting)
            {
                _edsIsDone = true;

                StopReading(ReadingRequesterTypeEnum.Eds);
                StopReading(ReadingRequesterTypeEnum.Others);

                ShowYesNoLabel = true;

                _currentFoucsControlItemsNavGrid = xtraUserControlIssueMain.ControlItemsNavGrid;
                ClearItemReadingAndDetails();

                var selectedItems = _currentFoucsControlItemsNavGrid.GetSelectedItems();

                if (forceStopReading)
                {
                    _currentFoucsControlItemsNavGrid.CancelReading();
                }
                else if (IsInEditMode)
                {
                    _currentFoucsControlItemsNavGrid.StartReading();

                    _currentFoucsControlItemsNavGrid.SetAutoTestPlayMode(_currentTestPlayState == TestPlayStateEnum.Playing);
                }

                SetDetails(selectedItems,
                                _currentFoucsControlItemsNavGrid.TopItems,
                                _currentFoucsControlItemsNavGrid.BottomItems,
                                _currentFoucsControlItemsNavGrid.IsTopListFirst,
                                _currentFoucsControlItemsNavGrid.CurrentTestResultItem,
                                _currentFoucsControlItemsNavGrid.CurrentIssueItem);

                //if (_currentFoucsControlItemsNavGrid.ItemsWithImagesExist())
                //{
                    
                //}
                //else
                //{
                //    SetDefaultUiDetails();
                //}

                _currentAutoPlayTestType = ReadingPlayTypes.ItemTesting;
            }
            else
            {
                _edsIsDone = true;

                StopReading(ReadingRequesterTypeEnum.Eds);
                StopReading(ReadingRequesterTypeEnum.Others);

                if (xtraTabControlItemTestingTabs.SelectedTabPage != xtraTabPageItemTesting)
                {
                    ClearItemReadingAndDetails();
                    _currentAutoPlayTestType = ReadingPlayTypes.None;
                    return;
                }

                ShowYesNoLabel = true;

                //This logic is to show the item image and des' if selected tab is item testing.
                if (xtraTabControlItemTesting.SelectedTabPage == null) return;

                if (xtraTabControlItemTesting.SelectedTabPage == xtraTabPageItems)
                {
                    if (xtraUserControlItemsNavGridMajorIssues == null) return;

                    _currentFoucsControlItemsNavGrid = xtraUserControlItemsNavGridMajorIssues;

                    ClearItemReadingAndDetails();

                    var selectedItems = _currentFoucsControlItemsNavGrid.GetSelectedItems();

                    if (forceStopReading)
                    {
                        _currentFoucsControlItemsNavGrid.CancelReading();
                    }
                    else if (IsInEditMode)
                    {
                        _currentFoucsControlItemsNavGrid.StartReading();

                        _currentFoucsControlItemsNavGrid.SetAutoTestPlayMode(_currentTestPlayState ==
                                                                             TestPlayStateEnum.Playing);
                    }

                    SetDetails(selectedItems,
                                       _currentFoucsControlItemsNavGrid.TopItems,
                                       _currentFoucsControlItemsNavGrid.BottomItems,
                                       _currentFoucsControlItemsNavGrid.IsTopListFirst,
                                       _currentFoucsControlItemsNavGrid.CurrentTestResultItem,
                                       _currentFoucsControlItemsNavGrid.CurrentIssueItem);

                    //if (_currentFoucsControlItemsNavGrid.ItemsWithImagesExist())
                    //{
                       
                    //}
                    //else
                    //{
                    //    SetDefaultUiDetails();
                    //}
                }
                else
                {
                    ControlItemDetailesAndReadingsDependOnUserActionForIssue(forceStopReading);
                }

                _currentAutoPlayTestType = ReadingPlayTypes.ItemTesting;
            }
        }

        /// <summary>
        /// Show the focused item image, description and reading depends on the selected [ ACTIVE ] issue tab.
        /// </summary>
        private void ControlItemDetailesAndReadingsDependOnUserActionForIssue(bool forceStopReading)
        {
            var issueUserControl = GetSelectedIssueUserConrol();

            if (issueUserControl == null)
                return;

            _currentFoucsControlItemsNavGrid = issueUserControl.ControlItemsNavGrid;

            if (forceStopReading)
            {
                _currentFoucsControlItemsNavGrid.CancelReading();
            }
            else if (IsInEditMode)
            {
                _currentFoucsControlItemsNavGrid.StartReading();
                _currentFoucsControlItemsNavGrid.SetAutoTestPlayMode(_currentTestPlayState == TestPlayStateEnum.Playing);
            }

            if (issueUserControl.IsIssueItemsBinded)
            {
                var selectedItems = issueUserControl.ControlItemsNavGrid.GetSelectedItems();

                SetDetails(selectedItems,
                                    _currentFoucsControlItemsNavGrid.TopItems,
                                    _currentFoucsControlItemsNavGrid.BottomItems,
                                    _currentFoucsControlItemsNavGrid.IsTopListFirst,
                                    _currentFoucsControlItemsNavGrid.CurrentTestResultItem,
                                    _currentFoucsControlItemsNavGrid.CurrentIssueItem);

                //if (issueUserControl.ControlItemsNavGrid.ItemsWithImagesExist())
                //{
                    
                //}
                //else
                //{
                //    SetDefaultUiDetails();
                //}
            }
        }

        /// <summary>
        /// Reset the focus to the first field.
        /// </summary>
        private void ResetFocus()
        {
            textEditTestName.Focus();
        }

        /// <summary>
        /// Set the mode of the clear EDS reading Button.
        /// </summary>
        private void SetClearEdsMode()
        {
            if (TestObject.Readings != null && TestObject.Readings.Count > 0 && IsInEditMode && !_isInReadingRunningMode)
            {
                simpleButtonClearEds.Enabled = FilteredReadings.FirstOrDefault(r => r.Value != 0) != null;
                simpleButtonClearAllEDS.Enabled = TestObject.Readings.FirstOrDefault(r => r.Value != 0) != null;
            }
            else
            {
                simpleButtonClearEds.Enabled = false;
                simpleButtonClearAllEDS.Enabled = false;
            }
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

            //ToDo By Zurba: Ask Oday about this change.
            if (TestObject.ObjectState != DomainEntityState.New && xtraTabControlItemTestingTabs.SelectedTabPage != xtraTabPagePatientSchedule)
                TestObject.ObjectState = DomainEntityState.Modified;

            simpleButtonClearEds.Enabled = FilteredReadings.Any(c => c.Value > 0);
            simpleButtonClearAllEDS.Enabled = TestObject.Readings.Any(c => c.Value > 0);

            SetReadingIndicators(ReadingPlayTypes.Eds,
                                 isReadingRunning
                                     ? TestBarStateEnum.Reading
                                     : _currentTestPlayState == TestPlayStateEnum.Playing
                                           ? TestBarStateEnum.TakeReading
                                           : TestBarStateEnum.Ready, 0);
        }

        #endregion

        #region Default Settings Init

        /// <summary>
        /// Sets the default test protocol form the setting.
        /// </summary>
        private void SetDefaultTestProtocolSetting()
        {
            var defaultTestProtocol =
                _settingsManager.GetSetting(new SettingsFilter
                                                {Key = EnumNameResolver.Resolve(SettingKeys.DefaultTestProtocol)});

            var foundTestProtocol = new TestProtocol();

            if (defaultTestProtocol != null)
            {
                foundTestProtocol =
                    _testProtocolsManager.GetTestProtocolById(new SingleItemFilter
                                                                  {
                                                                      ItemId =
                                                                          Int32.Parse(
                                                                              defaultTestProtocol.Value.ToString())
                                                                  });
            }

            TestObject.TestProtocol = foundTestProtocol;
        }

        /// <summary>
        /// Set the default of how points names are being shown, Full or Short based on settings
        /// </summary>
        private void SetDefaultPointsNameSetting()
        {
            var pointsFullName =
                _settingsManager.GetSetting(new SettingsFilter
                                                {Key = EnumNameResolver.Resolve(SettingKeys.PointsNaming)});

            var pointsNamingLookups =
                UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.PointsNaming));

            if (pointsFullName == null || pointsNamingLookups == null) return;

            var lookup = pointsNamingLookups.FirstOrDefault(h => h.Id == Int32.Parse(pointsFullName.Value.ToString()));

            if (lookup != null)
            {
                _showPointFullName =
                    lookup.Value.Replace(" ", "").Equals(Enum.GetName(typeof (PointsNaming), PointsNaming.FullName));
            }
        }

        /// <summary>
        /// Sets position of the meter to default one.
        /// </summary>
        private void SetDefaultMeterPositionSettings()
        {
            var meterPosition =
                _settingsManager.GetSetting(new SettingsFilter
                                                {Key = EnumNameResolver.Resolve(SettingKeys.MeterPosition)});

            int meterPositionValue;

            if (meterPosition != null && int.TryParse(meterPosition.Value.ToString(), out meterPositionValue))
            {
                dockPanelMeter.Dock = meterPositionValue == _leftLookupId ? DockingStyle.Left : DockingStyle.Right;
            }
        }

        /// <summary>
        /// Set default value(lookup) for ReevaluationPeriodType in the patient schedule. 
        /// </summary>
        private void SetDefaultReevaluationPeriodType()
        {
            var settingsList =
                CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings) as BindingList<Setting>;

            if (settingsList == null) 
                return;

            var setting =
                settingsList.FirstOrDefault(
                    s => s.Key.Equals(EnumNameResolver.Resolve(SettingKeys.DefaultRevalPeriod)));

            if (setting == null) 
                return;

            int settingValue;

            if (!int.TryParse(setting.Value.ToString(), out settingValue)) 
                return;

            TestObject.TestSchedule.EvalPeriodType = _lookupsManager.GetLookupById(new SingleItemFilter { ItemId = settingValue });
        }

        /// <summary>
        /// Set the default points group when creating a new test
        /// </summary>        
        private void SetDefaultPointsGroupSetting()
        {
            /*This is to fix the issue of crashing when saving test when initializing item and saving without selection
            so the ID will be 0 and FK error occurs or when selecting a value but without initializing and have the selection
            not saved*/

            var defaultTypeList =
                _settingsManager.GetSetting(new SettingsFilter {Key = EnumNameResolver.Resolve(SettingKeys.FullOrShort)});

            TestObject.TypeLookup = defaultTypeList != null
                                        ? new Lookup {Id = Int32.Parse(defaultTypeList.Value.ToString())}
                                        : new Lookup();

            var typeLookup =
                UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.TestType));

            if (defaultTypeList != null && typeLookup != null)
            {
                int defaultType;

                Int32.TryParse(defaultTypeList.Value.ToString(), out defaultType);

                var lookupValue = typeLookup.FirstOrDefault(h => h.Id == defaultType);

                if (lookupValue != null)
                {
                    TestObject.TypeLookup.Value = lookupValue.Value;

                    var defaultItemList =
                        _settingsManager.GetSetting(new SettingsFilter
                                                        {
                                                            Key =
                                                                EnumNameResolver.Resolve(lookupValue.Value ==
                                                                                         Enum.GetName(
                                                                                             typeof (TestType),
                                                                                             TestType.Full)
                                                                                             ? SettingKeys.PointsFull
                                                                                             : SettingKeys.PointsShort)
                                                        });

                    if (defaultItemList == null) return;

                    int defaultItemListValue;

                    Int32.TryParse(defaultItemList.Value.ToString(), out defaultItemListValue);

                    TestObject.Item = new Item {Id = defaultItemListValue};
                }
            }

            if (TestObject.Item == null)
            {
                TestObject.Item = new Item {Id = 1};
            }
        }

        /// <summary>
        /// Set the Readings Direction rather that its default or not.
        /// </summary>        
        private void SetDefaultReadingsLocationSetting()
        {
            var listPointsLookups = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ListPoints));

            //Set the lookups as fields.
            _bothListPointLookup =
                listPointsLookups.FirstOrDefault(l => l.Value == EnumNameResolver.Resolve(ListPoints.Both));

            _leftListPointLookup =
                listPointsLookups.FirstOrDefault(l => l.Value == EnumNameResolver.Resolve(ListPoints.Left));

            _rightListPointLookup =
                listPointsLookups.FirstOrDefault(l => l.Value == EnumNameResolver.Resolve(ListPoints.Right));

            if (TestObject.ListPointLookup.Id != 0)
            {
                _currentLocationLookupId = TestObject.ListPointLookup.Id;
                return;
            }

            var defaultTypeList =
                _settingsManager.GetSetting(new SettingsFilter {Key = EnumNameResolver.Resolve(SettingKeys.ListPoints)});

            if (defaultTypeList != null && listPointsLookups != null)
            {
                var lookup = listPointsLookups.FirstOrDefault(h => h.Id == Int32.Parse(defaultTypeList.Value.ToString()));

                if (lookup != null)
                    _currentLocationLookupId = lookup.Id;
            }

            TestObject.ListPointLookup = defaultTypeList != null
                                             ? new Lookup {Id = Int32.Parse(defaultTypeList.Value.ToString())}
                                             : new Lookup();
        }

        /// <summary>
        /// Gets settings for image area from DB and sets them in control
        /// </summary>
        private void SetImageAreaSettings()
        {
            UiHelperClass.ShowWaitingPanel("Load Image Settings ...");
            xtraUserControlItemDetails.UseAutoZoom = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.UseAutoZoom);
            xtraUserControlItemDetails.ZoomLevel = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.GalleryDefaultZoomLevel, CachableDataEnum.Settings).ToString());
            UiHelperClass.HideSplash();
        }

        private void UpdateImageAreaSettings()
        {
            UiHelperClass.ShowWaitingPanel("Updating image settings ...");
            UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.UseAutoZoom, xtraUserControlItemDetails.UseAutoZoom, _settingsManager);

            var galleryZoomLevelSetting = UiHelperClass.GetSettingFromCache(SettingKeys.GalleryDefaultZoomLevel, CachableDataEnum.Settings);
            if (galleryZoomLevelSetting != null)
            {
                galleryZoomLevelSetting.Value = xtraUserControlItemDetails.ZoomLevel;
                _settingsManager.Save(galleryZoomLevelSetting);
            }
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Get settings for imprintable items
        /// </summary>
        private void SetDefaultImprintableItems()
        {
            UiHelperClass.ShowWaitingPanel("Load Imprinting Settings ...");
            checkEditUseTimer.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ImprintingTimerEnabled);
            checkEditUseImprintingSound.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ImprintingSoundEnabled);
            spinEditImprentingTime.Value = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.ImprintingTime,CachableDataEnum.Settings).ToString());
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Get settings for schedule settings
        /// </summary>
        private void SetDefaultScheduleSettings()
        {
            UiHelperClass.ShowWaitingPanel("Load Schedule Settings ...");
            checkEditShowDefaultScheduleInstructions.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ShowScheduleReportDefaultInstructions);
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Updates imprintable item settings in DB
        /// </summary>
        private void UpdateImprintingSettings()
        {
            UiHelperClass.ShowWaitingPanel("Updating imprinting settings ...");
            UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.ImprintingTimerEnabled, checkEditUseTimer.Checked, _settingsManager);
            UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.ImprintingSoundEnabled, checkEditUseImprintingSound.Checked, _settingsManager);
            var imprintingTimeSetting = UiHelperClass.GetSettingFromCache(SettingKeys.ImprintingTime, CachableDataEnum.Settings);
            if (imprintingTimeSetting != null)
            {
                imprintingTimeSetting.Value = ImprintingTime;
                _settingsManager.Save(imprintingTimeSetting);
            }
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Updates schedule settings in DB
        /// </summary>
        private void UpdateScheduleSettings()
        {
            UiHelperClass.ShowWaitingPanel("Updating Schedule settings ...");
            UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.ShowScheduleReportDefaultInstructions, checkEditShowDefaultScheduleInstructions.Checked, _settingsManager);
            UiHelperClass.HideSplash();
        }

        #endregion

        #endregion

        #region CRUD Actions

        /// <summary>
        /// Uses the Tests manager to save the test        
        /// </summary>
        public override bool Save(bool isClosing)
        {
            try
            {
                return SaveLogic(isClosing);

            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }

        }

        /// <summary>
        /// Save Logic
        /// </summary>
        private bool SaveLogic(bool isClosing)
        {
            UpdateImprintingSettings();
            UpdateScheduleSettings();
            UpdateImageAreaSettings();

            if (!TestObject.Validate())
                return false;

            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);

            //Update service keys and validate services
            if (_servicesChanged)
            {
                foreach (var service in TestObject.TestServices.Where(service => service.TypeLookup.Id != _serviceSystemType.Id))
                {
                    service.Key = service.Name == null ? service.Key : service.Name.Replace(" ", "");
                    service.Validate();
                }
            }

            var result = _testsManager.SaveTest(TestObject);

            //if (result.IsSucceed)
            //{                
            //    //TestObject.TestIssues = _testsManager.GetTestIssues(new TestIssuesFilter { TestId = TestObject.Id });

            //    //TestObject.TestSchedule.ScheduleLines =
            //    //    _testSchedulesManager.GetScheduleLines(new ScheduleLinesFilter { TestScheduleId = TestObject.TestSchedule.Id });
            //}

            if (result.IsSucceed && TestObject.Readings != null)
            {
                /*Assigning the Test object for each reading. Setting the id as 0 to indicate that these 
                     are new readings since all the existed readings are deleted when saving (updating) the test.*/
                foreach (var reading in TestObject.Readings)
                {
                    reading.Test = TestObject;
                    reading.Id = 0;
                }

                _readingsManager.DeleteReadings(new ReadingsFilter {TestId = TestObject.Id});

                var readingResult = _readingsManager.SaveReadings(TestObject.Readings.ToList());

                if (!isClosing)
                {
                    RebindLight();
                    BindVitalRichEditControl();
                }

                return result.IsSucceed && readingResult.IsSucceed;
            }

            _newSavedTest = false;

            UiHelperClass.HideSplash();

            return result.IsSucceed;
        }

        /// <summary>
        /// Performs after save actions.
        /// </summary>
        public override void AfterSaveAction()
        {
            ControlItemDetailesAndReadingsDependOnUserAction(false);
            _servicesChanged = false;
            _shippingOrdersChanged = false;
            _testImprintableItemsChanged = false;

            //Bind list again because it gets loaded from DB during save and we need to hook the events again
            BindTestImprintableItems();
        }

        /// <summary>
        /// Perform after revert action.
        /// </summary>
        public override void AfterRevertAction()
        {
            ControlItemDetailesAndReadingsDependOnUserAction(false);
            ProcessSelectedItemTestingTabPageChenaged();
        }

        /// <summary>
        /// Save the navigation.
        /// </summary>
        private void SaveForNavigation()
        {
            if (TestObject.IsChanged)
            {
                SaveAction();
            }
        }

        /// <summary>
        /// Delete the current test object.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.PostingMessage);
                PostValues();

                UiHelperClass.ShowWaitingPanel(StaticKeys.SynchronizingMessage);
                Revert();

                UiHelperClass.ShowWaitingPanel(StaticKeys.DeletingMessage);
                var result = _testsManager.DeleteTest(TestObject);

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
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        public override bool HasChanges()
        {
            return TestObject.ObjectState != DomainEntityState.Unchanged &&
                   TestObject.ObjectState != DomainEntityState.Deleted;
        }

        /// <summary>
        /// Posts the values in the controls that are not yet committed to the dataSource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public override void PostValues()
        {
            textEditDescription.DoValidate();
            textEditTestName.DoValidate();
            lookUpEditType.DoValidate();

            lookUpEditStatus.DoValidate();
            lookUpEditReadingLocation.DoValidate();
            gridLookUpEditItemGroup.DoValidate();
            lookUpEditTestProtocol.DoValidate();

            gridViewItems.PostEditor();
            gridViewItems.ValidateEditor();
            gridViewItems.UpdateCurrentRow();
            
            spinEditReevaluationDate.DoValidate();
            spinEditTax.DoValidate();
            spinEditAdjustment.DoValidate();
            memoEditSpecialInstructions.DoValidate();

            gridViewScheduleLines.PostEditor();
            gridViewInvoicing.PostEditor();
            gridViewScheduleLines.ValidateEditor();
            gridViewInvoicing.ValidateEditor();
            
            lookUpEditType.PostLookupValues(TestObject.TypeLookup);
            lookUpEditStatus.PostLookupValues(TestObject.StateLookup);
            lookUpEditReEvaluationType.PostLookupValues(TestObject.TestSchedule.EvalPeriodType);
            lookUpEditAdjustmentApply.PostLookupValues(TestObject.TestSchedule.DiscountApply);
            UiHelperClass.GridViewPostValues(gridViewServices);
            treeListImprintableItems.PostEditor();
            
            if (_xtraUserControlVitalRichEditNotes != null)
                _xtraUserControlVitalRichEditNotes.PostValue();
        }

        /// <summary>
        /// Check if the object can revert or not, User Confirmation.
        /// </summary>
        /// <returns></returns>
        public override bool CanRevert()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage);

            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            try
            {
                IsLoaded = false;

                UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData);

                TestObject = _testsManager.GetTestById(new SingleItemFilter {ItemId = TestObject.Id});

                UiHelperClass.ShowWaitingPanel(StaticKeys.ProcessingMessage);

                Rebind();

                TestIssuesChangedActions();

                //Clearing the deletedTestIsssues list since we revert everything to the original data - the removing of this step will cause duplication issue
                _deletedTestIssues.Clear();
                _deletedScheduleLines.Clear();
                _deletedServices.Clear();
                TestObject.DeletedTestImprintableItems.Clear();
                _deletedOrders.Clear();

                IsLoaded = true;
                UiHelperClass.HideSplash();
                return true;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }
        }

        /// <summary>
        /// Rebinds the object.
        /// </summary>
        public override void Rebind()
        {
            _isRebinding = true;

            ClearBinding();
            ClearHandlers();
            SetupHandllers();
            SetProperties();
            SetBinding();
            SetupMainErrorProvider();
            UpdateErrorProvider();
            ControlItemDetailesAndReadingsDependOnUserAction(true);

            _isRebinding = false;
        }

        /// <summary>
        /// Before Save Actions.
        /// </summary>
        public override void BeforeSaveAction()
        {
            PostValues();
            StopRunningReading();
            UpdateListsWithDeletedRows();
            UpdateTestIssueTestResult();
        }

        /// <summary>
        /// Before Revert Actions.
        /// </summary>
        public override void BeforeRevertAction()
        {
            StopRunningReading();
        }

        /// <summary>
        /// Before Delete Actions.
        /// </summary>
        public override void BeforeDeleteAction()
        {
            StopRunningReading();
        }

        #endregion

        #region Hardware

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
                        ControlItemDetailesAndReadingsDependOnUserAction(false);
                    }
                }

            }
            catch (VitalHardwareException)
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

        #region Hardware Actions and Methods

        #region General Hardware Actions and Methods

        /// <summary>
        /// Start the reading based on the selected tab.
        /// </summary>
        private void StartReadingBasedOnSelectedTab()
        {
            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
            {
                StartReading(ReadingRequesterTypeEnum.Eds, null);
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting)
            {
                if (_currentFoucsControlItemsNavGrid != null)
                    _currentFoucsControlItemsNavGrid.StartReading();
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
            {
                StartReading(ReadingRequesterTypeEnum.Others, null);
            }
        }

        /// <summary>
        /// Stop the reading based on the selected tab.
        /// </summary>
        private void StopReadingBasedOnSelectedTab()
        {
            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
            {
                StopReading(ReadingRequesterTypeEnum.Eds);
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting)
            {
                StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
            {
                StopReading(ReadingRequesterTypeEnum.Others);
            }
        }

        /// <summary>
        /// Activate the connection based on the selected tab.
        /// </summary>
        private void ActivateConnectionBasedOnSelectedTab()
        {
            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
            {
                CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_Eds_ReadingDone,
                                                          _csaManager_Eds_MeterValueChanged);
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting)
            {
                CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ItemTesting_ReadingDone,
                                                          _csaManager_ItemTesting_MeterValueChanged);
            }
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
            {
                CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_Others_ReadingDone,
                                                          _csaManager_Others_MeterValueChanged);
            }
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
                StopReading(ReadingRequesterTypeEnum.Eds);
                StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
            }

            if (FormStatus != FormStatusEnum.New)
                FormStatus = FormStatusEnum.Modified;
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
        /// Clears the meter value, and move to next reading row.
        /// </summary>
        private void AfterReadingDone()
        {
            //Indicate if the object need to save, to avoid the set FormStatus = FormStatusEnum.Modified; in case the balancing is done.
            bool isAllReadingDone = false;

            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                if (_currentAutoPlayTestType == ReadingPlayTypes.Eds)
                {
                    isAllReadingDone = MoveNextReadingRow();
                    if (isAllReadingDone)
                        SaveForNavigation();
                }
                else if (_currentControlItemsNavGridRequestor != null)
                {
                    _currentControlItemsNavGridRequestor.DoAutoTestAction();
                    SetReadingIndicators(ReadingPlayTypes.ItemTesting, TestBarStateEnum.Ready, 0);
                }
            }

            SetReadingMode(false);

            if (!isAllReadingDone)
                FormStatus = FormStatusEnum.Modified;
            
        }

        /// <summary>
        /// Start the reading.
        /// </summary>
        private void StartReading(ReadingRequesterTypeEnum requesterTypeEnum, List<Item> itemsToBroadcast)
        {
            if (!IsInEditMode || !IsLoaded || _isRebinding)
                return;

            if ((requesterTypeEnum == ReadingRequesterTypeEnum.Eds && !gridViewItems.IsFocusedView))
                return;

            if (_currentTestPlayState == TestPlayStateEnum.Paused ||
                requesterTypeEnum == ReadingRequesterTypeEnum.Others)
                SetReadingIndicators(
                    requesterTypeEnum == ReadingRequesterTypeEnum.Eds
                        ? ReadingPlayTypes.Eds
                        : ReadingPlayTypes.ItemTesting, TestBarStateEnum.Ready, 0);

            CsaEmdUnitManager.Instance.Clear();

            ResetHardwareHandlers(requesterTypeEnum);

            if (CsaEmdUnitManager.Instance.IsBroadcastingOn && itemsToBroadcast != null && itemsToBroadcast.Count > 0)
            {
                CsaEmdUnitManager.Instance.Broadcast(itemsToBroadcast);
                OnBroadcastingActions();
            }
            else
            {
                OnBroadcastDoneActions();
            }

            CsaEmdUnitManager.Instance.StartReading();
        }

        /// <summary>
        /// Reactivate current connection.
        /// </summary>
        private void ReactivateConnection()
        {
            StopRunningReading();
            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ItemTesting_ReadingDone,
                                                          _csaManager_ItemTesting_MeterValueChanged);
        }

        /// <summary>
        /// Force to stop the reading.
        /// </summary>
        private void StopReading(ReadingRequesterTypeEnum requesterTypeEnum)
        {
            RemoveHardwareHandlers();

            switch (requesterTypeEnum)
            {
                case ReadingRequesterTypeEnum.Eds:
                    StopEdsReading();
                    break;

                case ReadingRequesterTypeEnum.ItemTestingIssueNav:
                    StopReading();
                    break;

                case ReadingRequesterTypeEnum.Others:
                    StopReading();
                    break;
            }

            SetReadingMode(false);
        }

        /// <summary>
        /// Stops the reading.
        /// </summary>
        private void StopReading()
        {
            CsaEmdUnitManager.Instance.StopReading();
        }

        /// <summary>
        /// Reset the hardware handlers.
        /// </summary>
        private void ResetHardwareHandlers(ReadingRequesterTypeEnum requesterTypeEnum)
        {
            RemoveHardwareHandlers();
            AddHardwareHandlers(requesterTypeEnum);
        }

        /// <summary>
        /// Remove the hardware handlers.
        /// </summary>
        private void RemoveHardwareHandlers()
        {
            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_Eds_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_Eds_ReadingDone;

            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_ItemTesting_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_ItemTesting_ReadingDone;

            CsaEmdUnitManager.Instance.MeterValueChanged -= _csaManager_Others_MeterValueChanged;
            CsaEmdUnitManager.Instance.ReadingDone -= _csaManager_Others_ReadingDone;
        }

        /// <summary>
        /// Add the hardware handlers.
        /// </summary>
        private void AddHardwareHandlers(ReadingRequesterTypeEnum requesterTypeEnum)
        {
            switch (requesterTypeEnum)
            {
                case ReadingRequesterTypeEnum.Eds:
                    CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_Eds_MeterValueChanged;
                    CsaEmdUnitManager.Instance.ReadingDone += _csaManager_Eds_ReadingDone;
                    break;

                case ReadingRequesterTypeEnum.ItemTestingIssueNav:
                    CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_ItemTesting_MeterValueChanged;
                    CsaEmdUnitManager.Instance.ReadingDone += _csaManager_ItemTesting_ReadingDone;
                    break;

                case ReadingRequesterTypeEnum.Others:
                    CsaEmdUnitManager.Instance.MeterValueChanged += _csaManager_Others_MeterValueChanged;
                    CsaEmdUnitManager.Instance.ReadingDone += _csaManager_Others_ReadingDone;
                    break;
            }
        }

        /// <summary>
        /// Handel the broadcasting action.
        /// </summary>
        private void OnBroadcastingActions()
        {

            if (!IsInEditMode || !IsLoaded || _isRebinding || !CsaEmdUnitManager.Instance.IsCsaEmdUnitConnected)
                return;

            BroadcastingStage = 1;

            SetConnectionIndicatorStatus(IsImprinting
                                             ? ConnectionIndicatorStatusEnum.Imprinting
                                             : ConnectionIndicatorStatusEnum.Broadcasting);

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
            if (!IsLoaded || _isRebinding)
                return;

            SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.NotBroadcasting);

            
            BroadcastingStage = 0;
        }

        /// <summary>
        /// Actions Before Reading dialog opend
        /// Unlink handlers for current form.
        /// </summary>
        private void BeforeReadingDialogOpened()
        {
            StopReading();

            CsaEmdUnitManager.Instance.DisposeConnection(
                                               Csa_Instance_Released,
                                               _csaManager_ItemTesting_ReadingDone,
                                              _csaManager_ItemTesting_MeterValueChanged);
        }

        /// <summary>
        /// Actions After Reading dialog closed
        /// Re-Link handlers for current form.
        /// </summary>
        private void AfterReadingDialogClosed()
        {
            xtraUserControlPrintingOptionsMain.UpdateOptions();

            CsaEmdUnitManager.Instance.ActivateConnection(
                                           Csa_Instance_Released,
                                           _csaManager_ItemTesting_ReadingDone,
                                          _csaManager_ItemTesting_MeterValueChanged);

            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
                StartReading(ReadingRequesterTypeEnum.Eds, new List<Item>());
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
                StartReading(ReadingRequesterTypeEnum.Others, new List<Item>());
            else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting)
                StartReading(ReadingRequesterTypeEnum.ItemTestingIssueNav, new List<Item>());
            else
                StopReading();

            UiHelperClass.HideSplash();
        }

        #endregion

        #region EDS Hardware Actions and Methods

        /// <summary>
        /// Stops the Esd Reading.
        /// </summary>
        private void StopEdsReading()
        {
            CsaEmdUnitManager.Instance.StopReading();

            ResetCurrentReadingValue();

            SetReadingMode(false);
        }

        #endregion

        #region Item Testing Hardware Actions and Methods

        //Keep it for future changes.

        #endregion

        #endregion

        #endregion

        #region Logic

        #region Auto Test Logic

        /// <summary>
        /// Toggle the Auto test play status depends on the current status.
        /// </summary>
        private void ToggleTestPlayState()
        {
            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                StopAutoPlayTestMode();
                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
            else
            {
                StartAutoPlayTestMode();
                //This will help the user by resetting the state of the testing current stage
                //so when the play button is clicked again, it will jump to where it should go automatically
                if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
                {
                    _currentAutomaticStage = TestStage.Eds;
                }

                CheckForAutomation();
            }
        }

        /// <summary>
        /// Starts the auto play.
        /// </summary>
        private void StartAutoPlayTestMode()
        {
            _barButtonItemPausePlay.Glyph = Resources.Test_Pause;
            _currentTestPlayState = TestPlayStateEnum.Playing;

            UpdateIssuesPlayState();

            _barButtonItemPausePlay.Caption = CommonResources.CommonResources.Pause;

            if (_currentAutoPlayTestType == ReadingPlayTypes.Eds)
            {
                ResetCurrentReadingValue();
                gridViewItems.Focus();
                MoveNextReadingRow();
            }
            else
            {
                if (_currentControlItemsNavGridRequestor != null)
                {
                    _currentControlItemsNavGridRequestor.SetAutoTestPlayMode(true);
                }
            }
        }

        /// <summary>
        /// Stops the auto play.
        /// </summary>
        private void StopAutoPlayTestMode()
        {
            _currentTestPlayState = TestPlayStateEnum.Paused;
            UpdateIssuesPlayState();
            StopReading(ReadingRequesterTypeEnum.Eds);
            StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
            _barButtonItemPausePlay.Glyph = Resources.Test_Play;
            _barButtonItemPausePlay.Caption = CommonResources.CommonResources.Play;
            StopAutoTestTimer();
        }

        /// <summary>
        /// Check if an automatic action can be done based on current condition
        /// </summary>
        private void CheckForAutomation()
        {
            //EDS tab is activated if it is not done based on property AreEDSReadingsDone or if it is
            //not started and user didn't yet leave the tab
            if (((!_edsIsDone && EdsReadingsNotStarted) || !AreEdsReadingsDone))
            {
                AutomaticAction(TestStage.EdsAutoPlay);
            }
            else if (AreEdsReadingsDone)
            {
                if (_currentAutomaticStage == TestStage.Eds ||
                    _currentAutomaticStage == TestStage.EdsAutoPlay ||
                    _currentAutomaticStage == TestStage.EdsBalancing)
                {
                    if (TestObject.NumberOfIssues == 0)
                    {
                        AutomaticAction(TestStage.MajorIssuesCount);
                    }
                    else
                    {
                        if (_currentTestPlayState == TestPlayStateEnum.Playing)
                        {
                            _currentAutomaticStage = TestStage.MajorIssuesCount;
                            CheckForAutomation();
                        }
                    }
                }
                else if (_currentAutomaticStage == TestStage.MajorIssuesCount)
                {
                    if (TestObject.NumberOfIssues != 0 && TestObject.TestIssues.Count == 0)
                    {
                        AutomaticAction(TestStage.SetMajorIssues);
                    }
                    else
                    {
                        if (_currentTestPlayState == TestPlayStateEnum.Playing)
                        {
                            _currentAutomaticStage = TestStage.SetMajorIssues;
                            CheckForAutomation();
                        }
                    }
                }
                else if (_currentAutomaticStage == TestStage.SetMajorIssues)
                {
                    if (TestObject.TestIssues.Count != 0)
                    {
                        AutomaticAction(TestStage.MajorIssueTest);
                    }
                }
                else if (_currentAutomaticStage == TestStage.MajorIssueTest)
                {
                    if (TestObject.TestIssues.Count != 0)
                    {
                        PerfromLastIssueAutoActions();
                    }
                }
            }
        }

        /// <summary>
        /// Makes the form editable automatically in case of locked record that is not new
        /// </summary>
        private void MakeFormEditableAutomatically()
        {
            if (TestObject.ObjectState != DomainEntityState.New && !IsInEditMode)
            {
                FormStatus = FormStatusEnum.Editable;
            }
        }

        /// <summary>
        /// This method will take visual action and maybe some behavior based on the action required
        /// </summary>
        /// <param name="stage"></param>
        private void AutomaticAction(TestStage stage)
        {
            _currentAutomaticStage = stage;

            if (stage == TestStage.Eds ||
                stage == TestStage.EdsAutoPlay ||
                stage == TestStage.EdsBalancing)
            {
                xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageEDS;
            }
            else
            {
                xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageItemTesting;
                if (stage == TestStage.MajorIssueTest)
                {
                    xtraTabControlItemTesting.SelectedTabPage = xtraTabPageIssues;
                }
            }

            switch (stage)
            {
                case TestStage.Eds:
                    break;
                case TestStage.EdsAutoPlay:
                    MakeFormEditableAutomatically();
                    if (_currentTestPlayState == TestPlayStateEnum.Paused)
                    {
                        ToggleTestPlayState();
                    }
                    break;
                case TestStage.ItemTesting:
                    break;
                case TestStage.MajorIssuesCount:
                    MakeFormEditableAutomatically();
                    OpenMajorIssuesDialog();
                    if (_currentFoucsControlItemsNavGrid != null)
                        _currentFoucsControlItemsNavGrid.Focus();
                    break;
                case TestStage.SetMajorIssues:
                    gridViewMajorIssues.Focus();
                    if (_currentTestPlayState == TestPlayStateEnum.Paused)
                    {
                        ToggleTestPlayState();
                    }
                    break;
                case TestStage.AddMajorIssue:
                    if (CreateMajorIssues())
                    {
                        //THE FOLLOWING TWO STEPS ARE VERY IMPORTANT:
                        //We make them like this to make sure the vital force dialog doesn't show up after canceling it
                        //when it shows for a major issue, this order of steps makes sure that the dialog shows up once for user.
                        //ToggleTestPlayState();
                        AutomaticAction(TestStage.MajorIssueTest);
                    }
                    break;
                case TestStage.MajorIssueTest:
                    MakeFormEditableAutomatically();
                    ControlItemDetailesAndReadingsDependOnUserAction(false);
                    if (_currentTestPlayState == TestPlayStateEnum.Paused)
                    {
                        ToggleTestPlayState();
                    }
                    xtraTabPageItemTesting.Focus();
                    xtraTabPageIssues.Focus();
                    FocusLastIssue();
                    PerfromLastIssueAutoActions();
                    break;
                case TestStage.VitalForce:
                    break;
                case TestStage.FourFactors:
                    break;
                case TestStage.Top10Causes:
                    break;
                case TestStage.EdsBalancing:
                    break;
            }
        }

        #endregion

        #region ESD Logic

        /// <summary>
        /// Binds the readings on the readings grid.
        /// </summary>
        private void BindPointReadings()
        {
            gridControlItems.DataBindings.Clear();
            gridControlAllEDS.DataBindings.Clear();
            
            SetupPointDisplay();
            UpdateEDSOverview();

            UiHelperClass.BindControl(gridControlItems, TestObject, () => TestObject.Readings);
            UiHelperClass.BindControl(gridControlAllEDS, TestObject, () => TestObject.ReadingPointSets);

            var enableTheAutoTest = TestObject.Readings != null && TestObject.Readings.Count > 0;

            if (enableTheAutoTest)
            {
                UpdateImageAndGaugesForReading();
            }

            SetAutoPlayAllowMode(enableTheAutoTest);
        }

        /// <summary>
        /// Update the EDS overview
        /// </summary>
        public void UpdateEDSOverview()
        {
            TestHelper.GenerateReadingPointSets(TestObject);
        }

        /// <summary>
        /// Checks reading by PointSet Item Id and location lookup Id
        /// </summary>
        /// <param name="currentItemGroupId"></param>
        /// <param name="locationLookupId"></param>
        public void CheckAndAddReadings(int currentItemGroupId, int locationLookupId)
        {
            if (TestObject.Readings.Any(r => r.PointSetItemId == currentItemGroupId && r.ListPointLookupId == locationLookupId)) return;

            UiHelperClass.ShowWaitingPanel("Loading Points ...");

            var loadedReadings = _itemManager.GetItemsAsReadings(new SingleItemFilter { ItemId = currentItemGroupId });

            foreach (var readingClone in loadedReadings.Select(reading => reading.Clone()))
            {
                readingClone.ListPointLookupId = locationLookupId;

                TestObject.Readings.Add(readingClone);
            }

            UiHelperClass.HideSplash();
        }
        
        /// <summary>
        /// Setups or reset the readings.
        /// </summary>
        /// <param name="currentItemGroupId">The current item group Id.</param>
        public void SetupReadings(int currentItemGroupId, bool callEDSTestingUpdateLogic = false)
        {
            gridViewItems.ActiveFilter.Clear();
            
            if (_currentLocationLookupId == _bothListPointLookup.Id)
            {
                //Filter only based on PointSetItemId
                gridViewItems.ActiveFilterString = "[PointSetItemId] = " + currentItemGroupId;
                CheckAndAddReadings(currentItemGroupId, _leftListPointLookup.Id);
                CheckAndAddReadings(currentItemGroupId, _rightListPointLookup.Id);
            }
            else
            {
                //Filter by PointSetItemId and LocationLookupId
                gridViewItems.ActiveFilterString = "[PointSetItemId] = " + currentItemGroupId + " AND [ListPointLookupId] = " + _currentLocationLookupId;
                CheckAndAddReadings(currentItemGroupId, _currentLocationLookupId);
            }
            
            BindPointReadings();
            AfterPointGroupChanged();
            gridViewItems.RefreshData();
            SetClearEdsMode();

            if (callEDSTestingUpdateLogic)
            {
                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
        }

        /// <summary>
        /// Set up the Display of the point name.
        /// </summary>
        public void SetupPointDisplay()
        {
            var tempItem = new Item();

            gridColumnItemName.FieldName = "Item." + (_showPointFullName
                                                          ? ExpressionHelper.GetPropertyName(() => tempItem.FullName)
                                                          : ExpressionHelper.GetPropertyName(() => tempItem.Name));
            gridColumnItemName.Caption = _showPointFullName ? StaticKeys.PointName : StaticKeys.ShortPointName;
        }

        /// <summary>
        /// Stops the reading and reset the current reading value if needs.
        /// </summary>
        private void BeforePointGroupChange()
        {
            if (CsaEmdUnitManager.Instance != null && CsaEmdUnitManager.Instance.IsReadingOn)
                StopReading(ReadingRequesterTypeEnum.Eds);

            ResetCurrentReadingValue();
        }

        /// <summary>
        /// Starts the reading after the group change.
        /// </summary>
        private void AfterPointGroupChanged()
        {
            gridViewItems.Focus();
            FocusedReadingRowChanged(true);
            StartReading(ReadingRequesterTypeEnum.Eds, GetItemsToBroadcast());
        }

        /// <summary>
        /// Helper for EDS reading focused row Changed.
        /// </summary>
        /// <param name="isUserAction">Is the change done by user or by code. By code: when the auto test move to the next code.</param>
        private void FocusedReadingRowChanged(bool isUserAction)
        {
            //Check for the first calling when set the default points group.
            if (!IsLoaded)
            {
                ResetFocus();
                return;
            }

            if (IsInEditMode || _isInReadingRunningMode)
            {
                if (isUserAction && _currentTestPlayState == TestPlayStateEnum.Playing)
                {
                    ResetCurrentReadingValue();
                }

                if (CurrentReading != null)
                {
                    if (_isInReadingRunningMode)
                    {
                        ResetCurrentReadingValue();
                        StopReading(ReadingRequesterTypeEnum.Eds);
                        _isWaitingCsaRealsedToTakeNewReading = true;
                    }
                    else if (!_isWaitingCsaRealsedToTakeNewReading)
                    {
                        StopReading(ReadingRequesterTypeEnum.Eds);
                        StartReading(ReadingRequesterTypeEnum.Eds, GetItemsToBroadcast());
                    }

                }
            }

            CurrentReading = gridViewItems.GetFocusedRow() as Reading;

        }

        /// <summary>
        /// Moves to the next reading row by logic.
        /// </summary>
        /// <returns>Is all reading done.</returns>
        private bool MoveNextReadingRow()
        {
            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                 return CheckThenMoveNextReading();
            }

            gridViewItems.MoveNext();
            SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.Ready, 0);
            return false;
        }

        /// <summary>
        /// Searchs the reading list inside the test for a reading to get the original reference copy by searching for it using a reading that came from the filtered list, this is used to prevent
        /// issues if the system tries to update the reading record by reference or if it tries to find its index in its parent list where the index should be extracted from the non filtered list as we are
        /// using it as the source of the grid and not using the filtered list for that
        /// </summary>
        /// <param name="filteredReadingItem"></param>
        /// <returns></returns>
        private Reading GetMatchingReadingInNonFilteredList(Reading filteredReadingItem)
        {
            return filteredReadingItem ==null? null:  TestObject.Readings.FirstOrDefault(r => r.Item.Id == filteredReadingItem.Item.Id && 
                                                    r.Id == filteredReadingItem.Id && 
                                                    r.ListPointLookupId == filteredReadingItem.ListPointLookupId);
        }

        /// <summary>
        /// Check the readings and move to the next reading if that is available.
        /// </summary>
        /// <returns>Is all reading done.</returns>
        private bool CheckThenMoveNextReading()
        {
            //VERY IMPORTANT: NOTICE BELOW THAT WE ARE SEARCHINNG FOR THESE TWO READING RECORDS USING THE FILTERED READING LIST AND NOT THE
            //ORIGINAL READING LIST ... THIS IS NEEDED TO ALLOW THE LOGIC TO PROVIDE READING RECORD INDEX IN THE GRID TO WORK PROPERLY SINCE THE GRID DOESN'T
            //USE THE INDEX OF THE ORIGINAL LIST BUT USES THE INDEX OF THE FILTERED ONE EVEN THOUGH THAT IT USES THE ORIGINAL ONE AS A DATASOURCE
            //WE ARE ALLOWED TO USE THESE TWO READING OBJECTIS IN THE LOGIC BELOW BECUASE THEIR VALUES ARE USE FOR READY ONLY PURPOSES AND WE DON'T SET
            //THEIR VALUES IN ANY WAY .. IN THAT CASE .. THE ORIGINAL READING OBJECT NEEDS TO BE EXTRACTED FROM THE ORIGINAL LIST TO BE USED.
            //------------------------------------------------------------------------------------------------------------------------------------
            var firstReadingToStart = FilteredReadings.FirstOrDefault(r => r.Value == 0);
            var firstBalancedReadingToStart = FilteredReadings.FirstOrDefault(r => r.ValueBalanced == 0 && r.Value > 0);
            //------------------------------------------------------------------------------------------------------------------------------------

            var nonFiftyReading = new Reading();

            if (firstReadingToStart == null && !_isInBalancingMode)
            {
                SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.Ready, 0);

                bool isEdsDone = false;

                if (TestObject.ObjectState != DomainEntityState.Unchanged)
                {
                    if (_currentAutomaticStage == TestStage.Eds ||
                        _currentAutomaticStage == TestStage.EdsAutoPlay ||
                        _currentAutomaticStage == TestStage.EdsBalancing &&
                        _currentTestPlayState == TestPlayStateEnum.Playing)
                    {
                        isEdsDone = true;
                        StopReading(ReadingRequesterTypeEnum.Eds);

                        if (!_ignoreAutoTestDoneMessage)
                        {
                            UiHelperClass.ShowInformation(StaticKeys.AutoTestDone);    
                        }
                    }

                    //Check if the automation will take the user to the item testing tab
                    //and maybe initially to the major issues count screen.
                    CheckForAutomation();
                }

                return isEdsDone;
            }

            gridViewItems.FocusedRowChanged -= gridViewItems_FocusedRowChanged;

            int index;

            //this variable is to allow overwriting the ValueBalanced readings.
            bool overwrite = false;

            //getting the index of the first applicable reading.
            if (firstReadingToStart != null && !_isInBalancingMode)
                index = FilteredReadings.IndexOf(firstReadingToStart);
            else
            {
                //the balanced readings are ready.
                if (firstBalancedReadingToStart == null)
                {
                    var remainingUnbalacedReadings = FilteredReadings.Any(r => r.ValueBalanced != 50);

                    if (!remainingUnbalacedReadings && !_isBalancingDone)
                    {
                        _isBalancingDone = true;

                        StopReading(ReadingRequesterTypeEnum.Eds);
                        UiHelperClass.ShowInformation(StaticKeys.BalancingDone);
                        StartReadingBasedOnSelectedTab();

                        SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.Ready, 0);
                        return true;
                    }

                    var remainingUnbalancedFromRead = FilteredReadings.Any(r => r.ValueBalanced != 50 && r.Value != 0);

                    if (firstReadingToStart != null)
                    {
                        if (!remainingUnbalancedFromRead)
                        {
                            SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.Ready, 0);
                            return false;
                        }
                    }

                    /*balancing mode, but with non-zero values (non fifty readings balancing stage).
                     *gets the suitable reading  (in terms of automation)
                     *set the overwrite flag to true so as to allow taking the new value.
                     */
                    nonFiftyReading = GetNextNonFiftyReading();
                    overwrite = true;
                }

                /*
                 * overwrite = true => getting the suitable index of the non fifty reading
                 * overwrite = false => getting the index of the first applicable balanced reading.
                 */
                index = FilteredReadings.IndexOf(!overwrite ? firstBalancedReadingToStart : nonFiftyReading);
            }

            gridViewItems.SelectRow(index);

            gridViewItems.FocusedRowHandle = index;

            FocusedReadingRowChanged(false);

            gridViewItems.FocusedRowChanged += gridViewItems_FocusedRowChanged;

            SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.TakeReading, 0);

            gridViewItems.Focus();

            return false;

        }

        /// <summary>
        /// Gets the next or suitable non fifty reading.
        /// </summary>
        /// <returns>The reading</returns>
        private Reading GetNextNonFiftyReading()
        {
            //getting all the unbalanced readings.
            var allNonFiftyReadings = FilteredReadings.Where(c => c.ValueBalanced != 50 && c.Value != 0).ToList();

            /*when the reading is 50 an item of the non fifty readings will be eliminated so keep the prevNonFiftyIndex as it is
             * when the reading is non fifty, the items of the non fifty readings will stay as they are, so increase the prevNonFiftyIndex
             * by 1 to point to the next unbalanced reading.
             */
            _prevNonFifty = CurrentReading.ValueBalanced == 50 ? _prevNonFifty : _prevNonFifty + 1;

            //finding the suitable next unbalanced reading to balance it.
            for (var i = 0; i < allNonFiftyReadings.Count(); i++)
            {
                if (i >= _prevNonFifty)
                {
                    _prevNonFifty = i;
                    return GetMatchingReadingInNonFilteredList(allNonFiftyReadings[i]);
                }
            }

            /*case: all the readings were checked and the application needs to start over the navigation if there
             *still any remaining non fifty values. */
            if (FilteredReadings.Any(c => c.ValueBalanced != 50 && c.Value != 0))
            {
                _prevNonFifty = -1;
                return GetNextNonFiftyReading();
            }

            return null;
        }

        /// <summary>
        /// Reset the reading when the reading from the HW not done yet, refresh the UI.
        /// </summary>
        private void ResetCurrentReadingValue()
        {
            if(IsClosing)
                return;

            if (CsaEmdUnitManager.Instance.IsReadingOn && _isInReadingRunningMode && CurrentReading != null)
            {
                //reset the balanced value.
                if (_isInBalancingMode)
                {
                    CurrentReading.ValueBalanced = 0;
                }
                    //reset the other values
                else
                {
                    CurrentReading.Value = 0;
                    CurrentReading.Min = 0;
                    CurrentReading.Max = 0;
                    CurrentReading.Fall = 0;
                    CurrentReading.Rise = 0;
                }

                if (FormStatus != FormStatusEnum.New)
                    FormStatus = FormStatusEnum.Modified;
            }

            UpdateImageAndGaugesForReading();
        }

        /// <summary>
        /// Updates both image and gauges for reading
        /// </summary>
        private void UpdateImageAndGaugesForReading()
        {
            if (CurrentReading == null || CurrentReading.Item == null || IsClosing) return;

            SetDetails(CurrentReading.Item);
            SetEdsGuageValues();
        }

        /// <summary>
        /// Set the Gauge Value from current reading.
        /// </summary>
        private void SetEdsGuageValues()
        {
            if (CurrentReading == null)
                return;

            var listPointlookup = CurrentReading.ListPointLookupId == _leftListPointLookup.Id? _leftListPointLookup: _rightListPointLookup;

            UpdateReading(listPointlookup, _isInBalancingMode ? CurrentReading.ValueBalanced : CurrentReading.Value);
        }

        /// <summary>
        /// Clear the current readings.
        /// </summary>
        private void ClearCurrentPointSetEdsReadings()
        {
            if (TestObject.Readings == null || TestObject.Readings.Count <= 0) return;

            if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ClearingCurrentPointSetReadings) == DialogResult.Yes)
            {
                if (TestObject.Item != null)
                {
                    TestObject.Readings.Where(r => r.PointSetItemId == TestObject.Item.Id).ToList().ForEach(r => r.Min = r.Max = r.Rise = r.Value = r.ValueBalanced = r.Fall = 0);

                    UpdateEDSOverview();

                    AfterPointGroupChanged();

                    UpdateImageAndGaugesForReading();

                    SetClearEdsMode();
                }
            }
        }

        /// <summary>
        /// Clear all EDS readings.
        /// </summary>
        private void ClearAllEdsReadings()
        {
            if (TestObject.Readings == null || TestObject.Readings.Count <= 0) return;

            if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ClearingAllEDSReadings) == DialogResult.Yes)
            {
                TestObject.Readings.ToList().ForEach(r => r.Min = r.Max = r.Rise = r.Value = r.ValueBalanced = r.Fall = 0);

                UpdateEDSOverview();

                AfterPointGroupChanged();

                UpdateImageAndGaugesForReading();

                SetClearEdsMode();
            }
        }

        /// <summary>
        /// Check if the eds reading is done and show the message.
        /// </summary>
        /// <param name="requesterTypeEnum"></param>
        private void CheckForAutoTestPlay(ReadingRequesterTypeEnum requesterTypeEnum)
        {
            if (_currentTestPlayState == TestPlayStateEnum.Playing)
            {
                StartAutoTestTimer();
            }
            else
            {
                AfterReadingDone();

                //Show Message after EDS readings are done even without the play mode and also
                //check for automation options.

                if (requesterTypeEnum == ReadingRequesterTypeEnum.Eds &&
                    AreEdsReadingsDone &&
                    _currentTestPlayState == TestPlayStateEnum.Playing)
                {
                    StopReading(ReadingRequesterTypeEnum.Eds);

                    if (!_ignoreAutoTestDoneMessage)
                    {
                        UiHelperClass.ShowInformation(StaticKeys.AutoTestDone);
                    }

                    CheckForAutomation();
                }
            }
        }

        /// <summary>
        /// Returns the selected EDS's reading item as a binding list to be able to broadcast it.
        /// </summary>
        /// <returns></returns>
        private List<Item> GetItemsToBroadcast()
        {
            //Part 1 - Get the item of readings.
            var itemList = new List<Item>();

            if (!IsImprinting && !CsaEmdUnitManager.Instance.IsBroadcastingOn)
                return itemList;

            var reading =
                gridViewItems.GetRow(gridViewItems.FocusedRowHandle >= 0 ? gridViewItems.FocusedRowHandle : 0) as
                Reading;

            if (reading != null)
            {
                itemList.Add(reading.Item);
            }

            //Part 2 - Get the products for the issues.
            if (TestObject.TestIssues != null)
            {
                foreach (var issue in TestObject.TestIssues)
                {
                    if (issue.TestResults == null) continue;

                    var products =
                        issue.TestResults.Where(
                            tr =>
                            tr.Item != null && tr.Item.TypeLookup != null &&
                            EnumNameResolver.LookupAsEnum<ItemTypeEnum>(tr.Item.TypeLookup.Value) ==
                            ItemTypeEnum.Product).Select(tri => tri.Item).ToList();

                    if (products.Count == 0) continue;

                    itemList.AddRange(products);
                }
            }

            if (TestObject.TestMainIssue != null)
            {
                var products =
                        TestObject.TestMainIssue.TestResults.Where(
                            tr =>
                            tr.Item != null && tr.Item.TypeLookup != null &&
                            EnumNameResolver.LookupAsEnum<ItemTypeEnum>(tr.Item.TypeLookup.Value) ==
                            ItemTypeEnum.Product).Select(tri => tri.Item).ToList();

                itemList.AddRange(products);
            }

            return itemList;
        }

        #endregion

        #region Item Testing Logic

        /// <summary>
        /// Creates the test Main Issue
        /// </summary>
        private void AddTestMainIssue()
        {
            if (TestObject.TestMainIssue != null) return;

            UiHelperClass.ShowWaitingPanel("Initializing Products Testing ...");

            TestObject.TestMainIssue = new TestIssue();

            var mainTestResult = new TestResult
            {
                Item = null,
                Parent = null,
                TestIssue = TestObject.TestMainIssue,
                IsSelected = false,
                IsCurrent = true,
                StepType = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product)),
                DateTime = DateTime.Now,
                TestResultFactors = new BindingList<TestResultFactor>()
            };

            TestObject.TestMainIssue.Item = _itemManager.GetItemByKey(ItemKeys.TestMainIssue);

            TestObject.TestMainIssue.Name = TestObject.TestMainIssue.Item.Name;
            TestObject.TestMainIssue.TestResults = new BindingList<TestResult> { mainTestResult };
            TestObject.TestMainIssue.Test = TestObject;
            TestObject.TestMainIssue.IsMainIssue = true;

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Opens the major issues dialog to take number of issues
        /// </summary>
        private void OpenMajorIssuesDialog()
        {
            var frmNumberOfIssues = new XtraFormMeterCounterDialog
                                        {
                                            Count =
                                                (TestObject.NumberOfIssues == 0
                                                     ? 1
                                                     : TestObject.NumberOfIssues),
                                            Tip = StaticKeys.CounterTipMajorIssues,
                                            QuestionText = StaticKeys.CounterQuestionMajorIssues,
                                            Title = StaticKeys.CounterTitleMajorIssues,
                                            TestPlayState = _currentTestPlayState
                                        };

            ShowHideOverlay(true);
            var result = ShowMeterBaseDialog(frmNumberOfIssues);
            ShowHideOverlay(false);
            if (result != DialogResult.OK) return;

            TestObject.NumberOfIssues = frmNumberOfIssues.Count;
            textEditNumberOfIssues.Text = frmNumberOfIssues.Count.ToString();

            if (_currentFoucsControlItemsNavGrid != null)
                _currentFoucsControlItemsNavGrid.Focus();

            CheckForAutomation();
        }

        /// <summary>
        /// Show meter base dialog.
        /// </summary>
        /// <param name="meterBaseDialog"></param>
        /// <returns></returns>
        private DialogResult ShowMeterBaseDialog(Form meterBaseDialog)
        {
            //Steps of use the connection in other area [form].
            //1- Stop the reading.
            if (_currentFoucsControlItemsNavGrid != null)
                _currentFoucsControlItemsNavGrid.CancelReading();

            //2- Show the form.
            var result = meterBaseDialog.ShowDialog();

            //3- After the frmNumberOfIssues deactivate the event handlers on the connection, we should reactivate them including the Csa_Instance_Released.
            CsaEmdUnitManager.Instance.ActivateConnection(Csa_Instance_Released, _csaManager_ItemTesting_ReadingDone,
                                                          _csaManager_ItemTesting_MeterValueChanged);

            //4- Start the reading again.
            if (_currentFoucsControlItemsNavGrid != null)
                _currentFoucsControlItemsNavGrid.StartReading();

            return result;
        }

        /// <summary>
        /// Show reEvaluation dialog.
        /// </summary>
        private void ShowReEvaluationnDialog()
        {
            if(TestObject.TestSchedule == null)
                return;

            StopReadingBasedOnSelectedTab();

            spinEditReevaluationDate.DoValidate();

            var frmReevalution = new XtraFormMeterCounterDialog
            {
                Count = TestObject.TestSchedule.ReevalInWeeks,
                Tip = StaticKeys.CounterTipRevaluationInWeeks,
                QuestionText = string.Format(StaticKeys.CounterQuestionRevaluationIn, lookUpEditReEvaluationType.Text),
                Title = StaticKeys.CounterTitleMajorRevaluationInWeeks,
                TestPlayState = _currentTestPlayState
            };
            ShowHideOverlay(true);
            var result = ShowMeterBaseDialog(frmReevalution);
            ShowHideOverlay(false);
            if (result != DialogResult.OK)
                return;

            TestObject.TestSchedule.ReevalInWeeks = frmReevalution.Count;
            TestObject.EvalPeriodChecked = true;

            StartReadingBasedOnSelectedTab();
        }

        /// <summary>
        /// Delete a major issue
        /// </summary>
        /// <param name="rowHandler">The row handler.</param>
        /// <returns></returns>
        public bool DeleteMajorIssue(int rowHandler)
        {
            var currentRow = gridViewMajorIssues.GetRow(rowHandler) as TestIssue;

            if (currentRow == null) return false;

            if (currentRow.ObjectState != DomainEntityState.New)
            {
                currentRow.ObjectState = DomainEntityState.Deleted;

                _deletedTestIssues.Add(currentRow);

                var issueTabPage = GetIssueTab(currentRow, true);

                if (issueTabPage != null)
                {
                    xtraTabControlIssues.TabPages.Remove(issueTabPage);
                }

                gridViewMajorIssues.DeleteRow(gridViewMajorIssues.FocusedRowHandle);
            }
            else
            {
                TestObject.TestIssues.Remove(currentRow);
            }

            var itemToBeDeleted =
                xtraUserControlItemsNavGridMajorIssues.InsertedItems.FirstOrDefault(c => c.Id == currentRow.Item.Id);

            if (itemToBeDeleted != null)
                xtraUserControlItemsNavGridMajorIssues.InsertedItems.Remove(itemToBeDeleted);

            SetIssuesNumbers();

            xtraTabPageIssues.PageVisible =
                TestObject.TestIssues.Count(issue => issue.ObjectState != DomainEntityState.New) > 0;
            SetResultsViewDataSource();

            _testHelper.ApplyImprintAction(TestObject, currentRow.TestResults, ImprintingAction.RemoveFromImprintList);
            
            return true;
        }

        /// <summary>
        /// Sets/Updates the issues numbering
        /// </summary>
        private void SetIssuesNumbers()
        {
            var count = 1;

            foreach (var issue in TestObject.TestIssues)
            {
                if (issue.ObjectState == DomainEntityState.Deleted)
                    continue;

                issue.IssueNumber = count;
                issue.IssueNameAndNumber = string.Format("{0} - {1} ", issue.IssueNumber, issue.Name);
                count += 1;
            }
        }

        /// <summary>
        /// Add the deleted objects to the actual list again.
        /// </summary>
        private void UpdateListsWithDeletedRows()
        {
            foreach (var item in _deletedTestIssues)
            {
                TestObject.TestIssues.Add(item);
            }

            foreach (var scheduleLine in _deletedScheduleLines)
            {
                TestObject.TestSchedule.ScheduleLines.Add(scheduleLine);
            }

            foreach (var service in _deletedServices)
            {
                TestObject.TestServices.Add(service);
            }

            foreach (var order in _deletedOrders)
            {
                TestObject.ShippingOrders.Add(order);
            }

            _deletedServices.Clear();
            _deletedTestIssues.Clear();
            _deletedScheduleLines.Clear();
            _deletedOrders.Clear();
        }

        /// <summary>
        /// Add the passed item to the issues and create then move this issue.
        /// </summary>
        /// <param name="itemToAdd">Item to add as major issue.</param>
        /// <returns>Current list of items that added as issues.</returns>
        private List<Item> AddToMajorIssues(Item itemToAdd)
        {
            if (AddToMajorIssuesList(itemToAdd))
            {
                if (_currentTestPlayState == TestPlayStateEnum.Playing)
                {
                    AutomaticAction(TestStage.AddMajorIssue);
                }
                else
                {
                    if (CreateMajorIssues())
                    {
                        xtraTabControlItemTesting.SelectedTabPage = xtraTabPageIssues;
                        xtraTabPageItemTesting.Focus();
                        xtraTabPageIssues.Focus();
                        FocusLastIssue();
                    }
                }
            }
            else
            {
                OpenMajorIssue(itemToAdd);
            }

            return TestIssuesItems;
        }

        /// <summary>
        /// Open major issue by its item
        /// </summary>
        /// <param name="itemToAdd"></param>
        private void OpenMajorIssue(Item itemToOpen)
        {
            if (itemToOpen != null)
            {
                xtraTabControlItemTesting.SelectedTabPage = xtraTabPageIssues;
                xtraTabPageItemTesting.Focus();
                xtraTabPageIssues.Focus();
                FocusIssueTab(itemToOpen);
            }
        }

        /// <summary>
        /// Add the selected item to the test issues list.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>true if new issue added.</returns>
        private bool AddToMajorIssuesList(Item item)
        {
            if (item == null || TestObject.TestIssues.FirstOrDefault(testIssue => testIssue.Item.Id == item.Id) != null)
                return false;

            var newTestIssue = new TestIssue();

            var initialTestResult = new TestResult
                                        {
                                            Item = item,
                                            Parent = null,
                                            TestIssue = newTestIssue,
                                            IsSelected = true,
                                            DateTime = DateTime.Now,
                                            TestResultFactors = new BindingList<TestResultFactor>()
                                        };

            //Sets Vital force for initial test result if it exists.
            initialTestResult.VitalForce = TestsManager.GetVitalForceForDuplicatedTestResult(TestObject,
                                                                                             initialTestResult);

            newTestIssue.Item = item;
            var result = xtraUserControlItemsNavGridMajorIssues.GetCurrentTestResult();
            var parentName = result == null
                                 ? xtraUserControlItemsNavGridMajorIssues.TestProtocol.Name
                                 : result.Item.Name;

            newTestIssue.Name = parentName + " - " + item.Name;
            newTestIssue.TestResults = new BindingList<TestResult> {initialTestResult};
            newTestIssue.Test = TestObject;
            newTestIssue.IsMainIssue = false;

            TestObject.TestIssues.Add(newTestIssue);
            SetResultsViewDataSource();

            return true;
        }

        /// <summary>
        /// Create issues from the list of issues.
        /// </summary>
        private bool CreateMajorIssues()
        {
            ControlItemDetailesAndReadingsDependOnUserAction(true);

            xtraTabControlItemTesting.SelectedPageChanged -= xtraTabControlItemTesting_SelectedPageChanged;

            var isSaved = SaveAction();

            xtraTabControlItemTesting.SelectedPageChanged += xtraTabControlItemTesting_SelectedPageChanged;

            ControlItemDetailesAndReadingsDependOnUserAction(false);
            SetResultsViewDataSource();

            //Test not saved, remove the last added issue.
            if (!isSaved)
            {
                UiHelperClass.ShowError(StaticKeys.ValidationMessageTitle, StaticKeys.ValidationMessageGeneral);
                DeleteMajorIssue(gridViewMajorIssues.RowCount - 1);
            }
            else
            {
                //Handle adding the test result inside the test issue to the imprintable items in case it was imprintable
                var currentIssue = gridViewMajorIssues.GetRow(gridViewMajorIssues.RowCount - 1) as TestIssue;

                if (currentIssue == null) return true;

                var currentResult = currentIssue.TestResults.FirstOrDefault();

                if (currentResult == null) return true;

                if (currentResult.Item == null) return true;

                if (currentResult.Item.Properties.HasProperty(PropertiesEnum.IsImprintable, _yesLookupId.ToString()))
                {
                    _testHelper.ApplyImprintActionSingleResult(TestObject, currentResult, ImprintingAction.Imprint);
                }                
            }

            return isSaved;
        }

        /// <summary>
        /// Update the issue tabs play status.
        /// </summary>
        private void UpdateIssuesPlayState()
        {
            foreach (var testIssue in TestObject.TestIssues)
            {
                var issueTabPage = GetIssueTab(testIssue, false);

                if (issueTabPage == null)
                    continue;

                var inspectedEntity = issueTabPage.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity != null)
                {
                    inspectedEntity.TestPlayState = _currentTestPlayState;
                }
            }
            xtraUserControlIssueMain.TestPlayState = _currentTestPlayState;
        }

        /// <summary>
        /// Show the items tab
        /// </summary>
        private void ShowItems()
        {
            xtraTabControlItemTesting.SelectedTabPage = xtraTabPageItems;
            xtraUserControlItemsNavGridMajorIssues.ResetStartingItems();
        }

        /// <summary>
        /// Set next issue number for the next issue button text.
        /// </summary>
        private void SetNextIssueNumberText()
        {
            simpleButtonShowItems.Text = StaticKeys.NextIssueText + "(" + (TestObject.TestIssues.Count + 1) + ")";
        }

        #endregion

        #region Test Issues Tab Actions

        /// <summary>
        /// Binds the test issues
        /// </summary>
        private void SetBindingTestIssues()
        {
            xtraTabPageIssues.PageVisible =
                TestObject.TestIssues.Count(issue => issue.ObjectState != DomainEntityState.New) > 0;

            var protocols = _testProtocolsManager.GetTestProtocols(new TestProtocolsFilter());

            SetBindingTestMainIssue();

            SetIssuesNumbers();

            foreach (var testIssue in TestObject.TestIssues)
            {
                var issue = testIssue;

                if (issue != null)
                {
                    Open(ref issue, ref protocols);
                }
            }

            FocusLastIssue();

            //Update the text object in existing opened Issue tabls
            if (xtraTabControlIssues.TabPages.Count > 0)
            {
                foreach (XtraTabPage page in xtraTabControlIssues.TabPages)
                {
                    var issueControl = page.Controls[0] as XtraUserControlIssue;

                    if (issueControl != null)
                    {
                        issueControl.CurrentTest = TestObject;
                    }
                }   
            }
        }

        /// <summary>
        /// Sets focus to the last issue
        /// </summary>
        private void FocusLastIssue()
        {
            if (xtraTabControlIssues.TabPages.Count > 0)
            {
                xtraTabControlIssues.SelectedTabPage =
                    xtraTabControlIssues.TabPages[xtraTabControlIssues.TabPages.Count - 1];
                var issueControl =
                    xtraTabControlIssues.TabPages[xtraTabControlIssues.TabPages.Count - 1].Controls[0] as
                    XtraUserControlIssue;

                if (issueControl != null)
                {
                    xtraTabControlIssues.TabPages[xtraTabControlIssues.TabPages.Count - 1].Focus();
                    issueControl.Focus();
                    issueControl.ControlItemsNavGrid.Focus();
                }
            }
        }

        /// <summary>
        /// Sets focus to a certain issue
        /// </summary>
        private void FocusIssueTab(Item issueItem)
        {
            if (xtraTabControlIssues.TabPages.Count > 0)
            {
                foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
                {
                    var testIssue = tab.Controls[0] as XtraUserControlIssue;

                    if (testIssue == null) continue;

                    if (testIssue.CurrentTestIssue.Item.Id == issueItem.Id)
                    {
                        xtraTabControlIssues.SelectedTabPage = tab;
                        xtraTabControlIssues.TabPages[xtraTabControlIssues.TabPages.Count - 1].Focus();
                        testIssue.Focus();
                        testIssue.ControlItemsNavGrid.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Performs automatic actions for the last issue
        /// </summary>
        private void PerfromLastIssueAutoActions()
        {
            if (xtraTabControlIssues.TabPages.Count <= 0)
                return;

            var issueControl =
                xtraTabControlIssues.TabPages[xtraTabControlIssues.TabPages.Count - 1].Controls[0] as
                XtraUserControlIssue;

            if (issueControl != null)
            {
                issueControl.PerformIssueAutoActions();
            }
        }

        /// <summary>
        /// Binds the major issues grid
        /// </summary>
        private void BindMajorIssues()
        {
            gridControlMajorIssues.DataBindings.Clear();
            TestObject.TestIssues.ListChanged += TestIssues_ListChanged;
            UiHelperClass.BindControl(gridControlMajorIssues, TestObject, () => TestObject.TestIssues);
        }

        /// <summary>
        /// Set the edit mode for each test issue tab
        /// </summary>
        /// <param name="isReadOnly"></param>
        private void SetTestIssuesEditMode(bool isReadOnly)
        {
            foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
            {
                var inspectedEntity = tab.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity == null)
                    continue;

                if (inspectedEntity.CurrentTestIssue != null)
                {
                    inspectedEntity.SetEditMode(isReadOnly, true);
                }
            }
        }

        /// <summary>
        /// sets binding of test issue tabs
        /// </summary>
        private void UpdateTestIssueTestResult()
        {
            foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
            {
                var testIssue = tab.Controls[0] as XtraUserControlIssue;

                if (testIssue == null) continue;

                if (testIssue.CurrentTestIssue != null)
                {
                    testIssue.UpdateTestResult();
                }
            }
        }

        /// <summary>
        /// Clears binding of test issue tabs
        /// </summary>
        private void ClearBindingTestIssues()
        {
            foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
            {
                var inspectedEntity = tab.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity == null) continue;

                if (inspectedEntity.CurrentTestIssue != null)
                {
                    inspectedEntity.ClearBinding();
                }
            }
        }

        /// <summary>
        /// Clears handlers of test issues
        /// </summary>
        private void ClearHandlersTestIssues()
        {
            foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
            {
                var inspectedEntity = tab.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity == null) continue;

                if (inspectedEntity.CurrentTestIssue != null)
                {
                    inspectedEntity.ClearHandlers();
                }
            }
        }

        /// <summary>
        /// Binds the Test Main issue user control and sets its settings
        /// </summary>
        private void SetBindingTestMainIssue()
        {
            var protocols = _testProtocolsManager.GetTestProtocols(new TestProtocolsFilter());

            xtraUserControlIssueMain.IssueItemsManager = _itemManager;
            xtraUserControlIssueMain.IssueLookupsManager = _lookupsManager;
            xtraUserControlIssueMain.IssueTestManager = _testsManager;
            xtraUserControlIssueMain.Protocols = protocols;
            xtraUserControlIssueMain.TestPlayState = _currentTestPlayState;
            xtraUserControlIssueMain.CurrentTest = TestObject;
            xtraUserControlIssueMain.IssueTestLogicHelper = TestLogicHelper;
            xtraUserControlIssueMain.ControlItemsNavGrid.TestProtocol = TestObject.TestProtocol;
            SetTestIssueTabHandlers(xtraUserControlIssueMain);

            xtraUserControlIssueMain.Open(TestObject.TestMainIssue, !IsInEditMode);
            xtraUserControlIssueMain.BindIssueItems();
        }

        /// <summary>
        /// Opens a test issue in a tab
        /// </summary>
        /// <param name="openedTestIssue"></param>
        /// <param name="protocols"></param>
        /// <returns></returns>
        public XtraUserControlIssue Open(ref TestIssue openedTestIssue, ref BindingList<TestProtocol> protocols)
        {
            //Check if the tab was opened before, and activate it in that case
            if (openedTestIssue != null)
            {
                if (!IsIssueExisting(openedTestIssue))
                {
                    //Get the correct tab type using the object type
                    var openedTab = new XtraUserControlIssue
                                        {
                                            IssueItemsManager = _itemManager,
                                            IssueLookupsManager = _lookupsManager,
                                            IssueTestManager = _testsManager,
                                            Protocols = protocols,
                                            TestPlayState = _currentTestPlayState,
                                            CurrentTest = TestObject,
                                            IssueTestLogicHelper = TestLogicHelper
                                        };

                    SetTestIssueTabHandlers(openedTab);

                    var tabPage = openedTab.Open(openedTestIssue, !IsInEditMode);
                    xtraTabControlIssues.TabPages.Add(tabPage);

                    return openedTab;
                }

                return new XtraUserControlIssue
                           {
                               CurrentTest = TestObject,
                               IssueTestLogicHelper = TestLogicHelper
                           };
            }
            return null;
        }
        
        /// <summary>
        /// Bind the items for selected issue control, this method to fix the performance issue the comes from loading the items of the issue.
        /// </summary>
        private void BindSelectedIssueTabItems()
        {
            var selectedIssueControl = GetSelectedIssueUserConrol();

            if (selectedIssueControl != null && !selectedIssueControl.IsIssueItemsBinded)
            {
                UiHelperClass.ShowWaitingPanel("Loading issue items ...");

                selectedIssueControl.BindIssueItems();
                SetDetails(selectedIssueControl.ControlItemsNavGrid.GetSelectedItems(),
                                selectedIssueControl.ControlItemsNavGrid.TopItems,
                                selectedIssueControl.ControlItemsNavGrid.BottomItems,
                                selectedIssueControl.ControlItemsNavGrid.IsTopListFirst,
                                selectedIssueControl.ControlItemsNavGrid.CurrentTestResultItem,
                                selectedIssueControl.ControlItemsNavGrid.CurrentIssueItem);

                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Set test issue tab handlers.
        /// </summary>
        /// <param name="tabIssue">The tab issue to set it handlers.</param>
        private void SetTestIssueTabHandlers(XtraUserControlIssue tabIssue)
        {
            if (tabIssue == null) return;

            tabIssue.ReadingRequest += xtraUserControlIssue_ReadingRequest;
            tabIssue.CancelReadingRequest += xtraUserControlIssue_CancelReadingRequest;
            tabIssue.SelectedItemChanged += xtraUserControlIssue_SelectedItemChanged;
            tabIssue.ActivateConnectionRequest += tabIssue_ActivateConnectionRequest;
            tabIssue.UpdateOnTestResults += tabIssue_UpdateTestResult;
            tabIssue.RefreshDetailsAndImageIssue += tabIssue_RefreshDetailsAndImageIssue;
            tabIssue.SetImageIgnoreState += tabIssue_SetImageIgnoreState;
            tabIssue.MeterDialogOpen += tabIssue_MeterDialogOpen;
            tabIssue.BalancingRequest += tabIssue_BalancingRequest;
        }

        /// <summary>
        /// Create a new tab for a specified object type
        /// </summary>
        public XtraUserControlIssue New(TestIssue testIssue)
        {
            var newTab = new XtraUserControlIssue();
            var tabPage = newTab.New(testIssue);
            xtraTabControlIssues.TabPages.Add(tabPage);
            xtraTabControlIssues.SelectedTabPage = tabPage;

            return newTab;
        }

        /// <summary>
        /// Check if a tab is opened or not, and if it is opened, only activate the tab and don't create a new one
        /// </summary>
        /// <param name="openedTestIssue"></param>
        /// <returns></returns>     
        public bool IsIssueExisting(TestIssue openedTestIssue)
        {
            var tabsObjectDictionary = new Dictionary<TestIssue, XtraTabPage>();

            var tabsIdDictionary = new Dictionary<int, XtraTabPage>();

            foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
            {
                var inspectedEntity = tab.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity == null || inspectedEntity.CurrentTestIssue == null)
                    continue;

                tabsObjectDictionary.Add(inspectedEntity.CurrentTestIssue, tab);

                if (openedTestIssue != null && inspectedEntity.CurrentTestIssue.GetType() == openedTestIssue.GetType())
                {
                    tabsIdDictionary.Add(Int32.Parse(inspectedEntity.CurrentTestIssue.Id.ToString()), tab);
                }
            }

            XtraTabPage pageToActivate = null;

            if (openedTestIssue != null)
            {
                tabsObjectDictionary.TryGetValue(openedTestIssue, out pageToActivate);
            }

            if (pageToActivate == null)
            {
                if (openedTestIssue != null)
                {
                    tabsIdDictionary.TryGetValue(Int32.Parse(openedTestIssue.Id.ToString()), out pageToActivate);
                }
            }

            if (pageToActivate != null)
            {
                var inspectedEntity = pageToActivate.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity != null)
                {
                    inspectedEntity.Update(openedTestIssue, IsInEditMode);
                }
            }

            return (pageToActivate != null);
        }

        /// <summary>
        /// Gets the tab page for a certain issue
        /// </summary>
        public XtraTabPage GetIssueTab(TestIssue openedTestIssue, bool update)
        {
            var tabsObjectDictionary = new Dictionary<TestIssue, XtraTabPage>();

            var tabsIdDictionary = new Dictionary<int, XtraTabPage>();

            foreach (XtraTabPage tab in xtraTabControlIssues.TabPages)
            {
                var inspectedEntity = tab.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity == null || inspectedEntity.CurrentTestIssue == null) continue;

                tabsObjectDictionary.Add(inspectedEntity.CurrentTestIssue, tab);

                if (openedTestIssue != null && inspectedEntity.CurrentTestIssue.GetType() == openedTestIssue.GetType())
                {
                    tabsIdDictionary.Add(Int32.Parse(inspectedEntity.CurrentTestIssue.Id.ToString()), tab);
                }
            }

            XtraTabPage pageToActivate = null;

            if (openedTestIssue != null)
            {
                tabsObjectDictionary.TryGetValue(openedTestIssue, out pageToActivate);
            }

            if (pageToActivate == null)
            {
                if (openedTestIssue != null)
                {
                    tabsIdDictionary.TryGetValue(Int32.Parse(openedTestIssue.Id.ToString()), out pageToActivate);
                }
            }

            if (pageToActivate != null && update)
            {
                var inspectedEntity = pageToActivate.Controls[0] as XtraUserControlIssue;

                if (inspectedEntity != null)
                {
                    inspectedEntity.Update(openedTestIssue, IsInEditMode);
                }
            }

            return pageToActivate;
        }

        /// <summary>
        /// The logic to move to the last issue and fill the products list
        /// </summary>
        private void AddProducts()
        {
            _isAddingEdsProduct = true;
            xtraUserControlIssueMain.OpenProducts();
            xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageProductsTesting;

            _isAddingEdsProduct = false;
        }

        #endregion

        #region Invoicing

        /// <summary>
        /// Sets the Adjustment value and type based on input type
        /// </summary>
        /// <param name="isPercent"></param>
        private void SetAdjustmentTypeAndValue(bool isPercent)
        {
            _allowSummaryUpdate = false;
            if (TestObject == null || TestObject.TestSchedule == null) return;

            var defaultAdjustment = isPercent
                ? UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultAdjustmentPercent,
                    CachableDataEnum.VisibleSettings)
                : UiHelperClass.GetSettingValueFromCache(SettingKeys.DefaultAdjustmentValue,
                    CachableDataEnum.VisibleSettings);
            SetAdjustmentTypeImage(isPercent);
            TestObject.TestSchedule.Discount = defaultAdjustment == null ? 0 : decimal.Parse(defaultAdjustment.ToString());
            _allowSummaryUpdate = true;
            TestObject.TestSchedule.DiscountAsPercentage = isPercent;            
        }

        /// <summary>
        /// Sets the Adjustment spin edit image
        /// </summary>
        /// <param name="isPercent"></param>
        private void SetAdjustmentTypeImage(bool isPercent)
        {
            spinEditAdjustment.Properties.Buttons[1].Image = isPercent ? Resources.Money : Resources.Percent;
            spinEditAdjustment.Properties.Mask.EditMask = isPercent ? "p" : "c";
            spinEditAdjustment.Properties.MaxValue = isPercent ? 1 : 9999;
            spinEditAdjustment.Properties.MinValue = isPercent ? -1 : -9999;
        }

        /// <summary>
        /// Shows or hides check number
        /// </summary>
        private void UpdateCheckNumberVisibility()
        {
            layoutControlItemCheckNumber.Visibility = checkEditIsCheck.Checked
                                                              ? LayoutVisibility.Always
                                                              : LayoutVisibility.Never;
        }

        /// <summary>
        /// Refreshes the invoing grid summary
        /// </summary>
        private void UpdateTotalSummary()
        {
            try
            {               
                textEditProductsSubtotal.EditValue= TestObject.ProductsSubtotal;
                textEditProductsAdjustmentAmount.EditValue = TestObject.ProductsAdjustmentValue;
                textEditProductswithAdjustment.EditValue = TestObject.ProductsWithAdjustment;
                textEditProductsTaxAmount.EditValue = TestObject.ProductsWithTax;
                textEditProductsTotal.EditValue = TestObject.ProductsWithAdjustmentAndTax;
                textEditServicesSubtotal.EditValue = TestObject.ServicesSubtotal;
                textEditServicesAdjustmentAmount.EditValue = TestObject.ServicesAdjustmentValue;
                textEditServiceswithAdjustment.EditValue = TestObject.ServicesWithAdjustment;
                textEditServicesTotal.EditValue = TestObject.ServicesWithAdjustment;
                textEditTotal.EditValue = TestObject.TestTotal;                
            }
            catch (Exception e)
            {
                
            }
        }

        /// <summary>
        /// Shows or hides Adjustment fields
        /// </summary>
        private void ShowHideAdjustmentFields()
        {
            var hideAdjustmentFields = UiHelperClass.GetSettingCheckValue(CachableDataEnum.VisibleSettings, SettingKeys.HideAdjustmentFields);
            var visbility = hideAdjustmentFields ? LayoutVisibility.Never : LayoutVisibility.Always;

            emptySpaceItemAdjustment.Visibility = hideAdjustmentFields ? LayoutVisibility.Always : LayoutVisibility.Never;
            layoutControlItemAdjustment.Visibility = visbility;
            layoutControlItemAdjustmentApply.Visibility = visbility;
            layoutControlItemProductsAdjustmentAmount.Visibility = visbility;
            layoutControlItemServicesAdjustmentAmount.Visibility = visbility;
            layoutControlItemProductswithAdjustment.Visibility = visbility;
            layoutControlItemServiceswithAdjustment.Visibility = visbility;
        }

        #endregion
        
        #region Test Schedule

        /// <summary>
        /// Binds the schedule lines grid.
        /// </summary>
        private void BindScheduleLines()
        {
            gridControlScheduleLines.DataBindings.Clear();

            UiHelperClass.BindControl(gridControlScheduleLines, TestObject.TestSchedule,
                                      () => TestObject.TestSchedule.ScheduleLines);

            gridControlInvoicing.DataBindings.Clear();

            UiHelperClass.BindControl(gridControlInvoicing, TestObject.TestSchedule,
                                      () => TestObject.TestSchedule.ScheduleLines);

            UpdateScheduleGridFilters(checkEditShowDeletedScheduleLines.Checked);
        }

        /// <summary>
        /// Bind the VitalRichEditControl.
        /// </summary>
        private void BindVitalRichEditControl()
        {
            if (_xtraUserControlVitalRichEditNotes != null)
                _xtraUserControlVitalRichEditNotes.Bind(TestObject, () => TestObject.Notes);
        }

        /// <summary>
        /// Refresh the PatientScuedule lines.
        /// </summary>
        public void RefreshPatientScuedule()
        {
            UiHelperClass.ShowWaitingPanel("Refreshing Schedule ...");

            var products = GetTestScheduleProducts();

            foreach (var product in products)
            {
                var scheduleLine = new ScheduleLine
                {
                    Item = product,
                    TestSchedule = TestObject.TestSchedule
                };

                TestsManager.SetScheduleLineDefaultValues(scheduleLine);

                TestObject.TestSchedule.ScheduleLines.Add(scheduleLine);
            }

            BindScheduleLines();

            if (xtraTabControlTestSchedule.SelectedTabPage == xtraTabPageInvoice)
            {
                UpdateTotalSummary();
            }

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Update the filter of deleted schedule lines on the schedule grid.
        /// </summary>
        /// <param name="showDeleted"></param>
        private void UpdateScheduleGridFilters(bool showDeleted)
        {
            gridViewScheduleLines.OptionsView.EnableAppearanceEvenRow = !showDeleted;
            gridViewInvoicing.OptionsView.EnableAppearanceEvenRow = !showDeleted;

            if (showDeleted)
            {
                gridViewScheduleLines.ActiveFilter.Clear();
                //Enable the row style handler to styling the deleted rows with deferent style.
                gridViewScheduleLines.RowStyle -= gridViewScheduleLines_RowStyle;
                gridViewScheduleLines.RowStyle += gridViewScheduleLines_RowStyle;

                gridViewInvoicing.ActiveFilter.Clear();
                //Enable the row style handler to styling the deleted rows with deferent style.
                gridViewInvoicing.RowStyle -= gridViewScheduleLines_RowStyle;
                gridViewInvoicing.RowStyle += gridViewScheduleLines_RowStyle;
            }
            else
            {
                gridViewScheduleLines.ActiveFilterString = StaticKeys.DeletedScheduleFilterText;
                //Disable the row style handler to styling the deleted rows with deferent style.
                gridViewScheduleLines.RowStyle -= gridViewScheduleLines_RowStyle;

                gridViewInvoicing.ActiveFilterString = StaticKeys.DeletedScheduleFilterText;
                //Disable the row style handler to styling the deleted rows with deferent style.
                gridViewInvoicing.RowStyle -= gridViewScheduleLines_RowStyle;
            }
        }

        /// <summary>
        /// Delete a schedule line.
        /// </summary>
        /// <param name="selectedItem">The row selected Items.</param>
        /// <returns></returns>
        public bool SetScheduleLineDeletedStatus(ScheduleLine selectedItem, bool isDeleted)
        {
            if (selectedItem == null) return false;

            selectedItem.IsDeleted = isDeleted;
            UpdateTotalSummary();
            return true;
        }

        /// <summary>
        /// Open the passed Schedule Line.
        /// </summary>
        /// <param name="rowHandler"></param>
        private void OpenScheduleLine(int rowHandler)
        {
            var currentRow = gridViewScheduleLines.GetRow(rowHandler) as ScheduleLine;

            if (currentRow == null) return;

            StopReading(ReadingRequesterTypeEnum.Others);

            var productDosagesDilaog = new XtraFormProductDosages {ScheduleLine = currentRow};
            ShowHideOverlay(true);
            productDosagesDilaog.ShowDialog();
            ShowHideOverlay(false);
            ReactivateConnection();

            StartReading(ReadingRequesterTypeEnum.Others, null);
            UpdateTotalSummary();
        }

        /// <summary>
        /// Shows the test schedule report
        /// </summary>
        private void ShowTestScheduleReport()
        {
            StopReading(ReadingRequesterTypeEnum.Others);

            UpdateScheduleSettings();

            UiHelperClass.PrintPatientScheduleReport(TestObject, _lookupsManager, _settingsManager, xtraUserControlPrintingOptionsMain);

            StartReading(ReadingRequesterTypeEnum.Others, null);
        }

        /// <summary>
        /// Shows the product invoice report
        /// </summary>
        private void ShowProductInvoiceReport()
        {
            StopReading(ReadingRequesterTypeEnum.Others);

            UiHelperClass.PrintClientInvoice(TestObject, xtraUserControlPrintingOptionsMain);

            StartReading(ReadingRequesterTypeEnum.Others, null);
        }

        /// <summary>
        /// Gets a unique list of products of the test schedule.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Item> GetTestScheduleProducts()
        {
            try
            {
                var products = new BindingList<Item>();

                if (_productTypeLookupId == 0 || _nonelistTypeLookupId == 0) return products;

                var issuesProducts = TestObject.TestIssues.SelectMany(r => r.TestResults).Where(rr =>
                                                                                  rr.ObjectState !=
                                                                                  DomainEntityState.Temp &&
                                                                                  rr.IsSelected &&
                                                                                  rr.Item != null &&
                                                                                  rr.Item.TypeLookup != null &&
                                                                                  rr.Item.ListTypeLookup != null &&
                                                                                  IsNotInScheduleLines(rr) &&
                                                                                  rr.Item.TypeLookup.Id ==
                                                                                  _productTypeLookupId &&
                                                                                  rr.Item.ListTypeLookup.Id ==
                                                                                  _nonelistTypeLookupId).Select(
                                                                                      rrr => rrr.Item).DistinctBy(
                                                                                          prod => prod.Id).ToList();
                var testMainIssueProducts = TestObject.TestMainIssue.TestResults.Where(rr =>
                                                                                  rr.ObjectState !=
                                                                                  DomainEntityState.Temp &&
                                                                                  rr.IsSelected &&
                                                                                  rr.Item != null &&
                                                                                  rr.Item.TypeLookup != null &&
                                                                                  rr.Item.ListTypeLookup != null &&
                                                                                  IsNotInScheduleLines(rr) &&
                                                                                  rr.Item.TypeLookup.Id ==
                                                                                  _productTypeLookupId &&
                                                                                  rr.Item.ListTypeLookup.Id ==
                                                                                  _nonelistTypeLookupId).Select(
                                                                                      rrr => rrr.Item).DistinctBy(
                                                                                          prod => prod.Id).ToList();
                return issuesProducts.Concat(testMainIssueProducts);
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return new BindingList<Item>();
            }

        }

        /// <summary>
        /// Checks if the product is already in the schedule line.
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        private bool IsNotInScheduleLines(TestResult testResult)
        {
            try
            {
                var issueTab = GetIssueTab(testResult.TestIssue, false);
                var xtraUserControlIssue = issueTab == null? null: issueTab.Controls[0] as XtraUserControlIssue;

                //Check if product schedule should be skipped before including it in the products to be refereshed.
                var skipProduct = xtraUserControlIssue != null && xtraUserControlIssue.SkipScheduleForProduct(testResult);

                return testResult.Item != null &&
                       TestObject.TestSchedule.ScheduleLines.All(
                           scheduleLine => scheduleLine.Item == null || scheduleLine.Item.Id != testResult.Item.Id) && !skipProduct;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }
        }

        /// <summary>
        /// Enables the balancing mode and sets the needed properties.
        /// </summary>
        private void EnableBalancingMode()
        {
            MapBalancedReadings();

            radioGroupEdsMode.SelectedIndex = 1;

            _isInBalancingMode = true;

            StartBalancingWithStartIndex();
        }

        /// <summary>
        /// Focuses on the correct index based on the balancing process situation.
        /// </summary>
        private void StartBalancingWithStartIndex()
        {
            var firstBalancedReadingToStart = FilteredReadings.FirstOrDefault(r => r.ValueBalanced == 0 && r.Value != 0) ?? GetNextNonFiftyReading();

            var index = FilteredReadings.IndexOf(firstBalancedReadingToStart);

            xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageEDS;

            gridViewItems.MakeRowVisible(index);

            gridViewItems.SelectRow(index);

            gridViewItems.FocusedRowHandle = index;

            gridViewItems.Focus();
        }

        /// <summary>
        /// Maps the balance readings.
        /// </summary>
        private void MapBalancedReadings()
        {
            foreach (var reading in FilteredReadings)
            {
                if (reading.Value == 50)
                    reading.ValueBalanced = reading.Value;
            }
        }

        /// <summary>
        /// Update current test status to done.
        /// </summary>
        private void UpdateTestStatusToDone()
        {
            lookUpEditStatus.EditValue = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TestState, TestStateEnum.Done, false, true)).Id;
        }

        #endregion

        #region Imprinting

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            var maleLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.Gender, GenderEnum.Male));

            _gender = maleLookup != null && maleLookup.Id == TestObject.Patient.GenderLookup.Id
                          ? GenderEnum.Male
                          : GenderEnum.Female;

            var productTypeLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product));
            var potencyTypeLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Potency));
            var nonelistTypeLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.None));
            var yesLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));

            _productTypeLookupId = productTypeLookup != null ? productTypeLookup.Id : 0;
            _potencyTypeLookupId = potencyTypeLookup != null ? potencyTypeLookup.Id : 0;
            _nonelistTypeLookupId = nonelistTypeLookup != null ? nonelistTypeLookup.Id : 0;
            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;

            var leftLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Left));

            var rightLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Right));

            _leftLookupId = leftLookup != null ? leftLookup.Id : 0;
            _rightLookupId = rightLookup != null ? rightLookup.Id : 0;

        }

        #endregion

        #region Test Results

        /// <summary>
        /// Sets the dataSource for the test results grid
        /// </summary>
        private void SetResultsViewDataSource()
        {
            gridControlTestResults.DataSource = null;
            gridControlTestResults.DataSource = GetAllTestResults();

            var strReading = "[" + ExpressionHelper.GetPropertyName(() => new TestResultFactor().Reading) + "]";
            gridViewTestResultFactors.ActiveFilterString = "(" + strReading + " <= " +
                                                           StaticKeys.MeterMinAcceptableReading +
                                                           " OR " +
                                                           strReading + " >= " +
                                                           StaticKeys.MeterMaxAcceptableReading +
                                                           ") AND " +
                                                           strReading + " <> 0";
            gridViewTestResults.ExpandAllGroups();
        }

        /// <summary>
        /// Gets all the test results in the current test.
        /// </summary>
        /// <returns></returns>
        private List<TestResult> GetAllTestResults()
        {
            return TestObject.TestIssues.SelectMany(r => r.TestResults).Where(rr => rr.IsSelected &&
                                                                                    rr.Item != null &&
                                                                                    rr.Item.TypeLookup != null &&
                                                                                    rr.Item.TypeLookup.Id !=
                                                                                    _itemTypePointId).ToList();
        }

        #endregion

        #region Service

        /// <summary>
        /// Binds the services
        /// </summary>
        private void BindServices()
        {
            gridControlServices.DataBindings.Clear();
            TestObject.TestServices.ListChanged += TestServices_ListChanged;
            UiHelperClass.BindControl(gridControlServices, TestObject, () => TestObject.TestServices);
            _servicesChanged = false;
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
                    var focusedRow = gridViewServices.GetFocusedRow() as TestService;

                    if (focusedRow != null && focusedRow.Id > 0)
                    {                    
                        //mark the object as deleted.
                        focusedRow.ObjectState = DomainEntityState.Deleted;
                        //add the deleted objects to a temporary list.
                        _deletedServices.Add(focusedRow);
                    }

                    //delete the row 
                    gridViewServices.DeleteRow(gridViewServices.FocusedRowHandle);

                    //ATTENTION: THE UpdateTotalSummary SEEMS TO CRASH WHEN GRID IS NOT BOUNDED YET, EVEN IF SETBINDING IS CALLED, IT WON'T BIND UNTIL THE GRID IS VISIBLE...MAKE SURE SUMMARY UPDATE OCCURS ONLY WHEN FORM IS ACTIVE...MAKE SURE SUMMARY UPDATE OCCURS ONLY WHEN FORM IS ACTIVE
                    if (IsLoaded)
                    {
                        gridViewServices.UpdateTotalSummary();
                    }
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Adds a service to the test services
        /// </summary>
        /// <param name="servicetoAdd"></param>
        private void AddService(Service servicetoAdd)
        {
            var newTestService = new TestService
            {
                Key = servicetoAdd.Key,
                Name = servicetoAdd.Name,
                Description = servicetoAdd.Description,
                Comments = servicetoAdd.Comments,
                Price = servicetoAdd.Price,
                Service = servicetoAdd,
                TypeLookup = servicetoAdd.TypeLookup,
                Test = TestObject
            };
            TestObject.TestServices.Add(newTestService);

            //ATTENTION: THE UpdateTotalSummary SEEMS TO CRASH WHEN GRID IS NOT BOUNDED YET, EVEN IF SETBINDING IS CALLED, IT WON'T BIND UNTIL THE GRID IS VISIBLE...MAKE SURE SUMMARY UPDATE OCCURS ONLY WHEN FORM IS ACTIVE
            if (IsLoaded)
            {
                gridViewServices.UpdateTotalSummary();
            }
        }

        #endregion

        #region Shipping Orders

        /// <summary>
        /// Sends a shipping order email
        /// </summary>
        private void SendShippingOrder()
        {
            if (UiHelperClass.ShowConfirmQuestion("Are you sure you would like to confirm and send the shipping order?") == DialogResult.Yes)
            {
                if (UiHelperClass.SendShipmentOrder(
                    FocusedShippingOrder, 
                    TestObject, 
                    true, 
                    true, 
                    xtraUserControlPrintingOptionsMain,true).IsSucceed)
                {
                    _shippingOrdersManager.Save(FocusedShippingOrder);
                    UpdateShippingOrders();
                }
            }
        }
        
        /// <summary>
        /// Open a Shipping Order.
        /// </summary>
        private void OpenShippingOrder(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Shipping Order ..." : "Loading Shipping Order ...");

                int lookupId = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.ShippingMethod, CachableDataEnum.VisibleSettings).ToString());
                var shippingMethod = _lookupsManager.GetLookupById(new SingleItemFilter() { ItemId = lookupId });

                var currentShippingOrder = isNew
                    ? new ShippingOrder
                      {
                          Number = TestObject.Patient.Id.ToString() + "-" +
                                   TestObject.Id.ToString() + "-0",
                          Sent = false,
                          ShippingMethod = shippingMethod,
                          SendToClient = UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings, SettingKeys.SendShipmentToClient),
                          PatientFirstName = TestObject.Patient.FirstName,
                          PatientLastName = TestObject.Patient.LastName,
                          PatientAddress1 = TestObject.Patient.Address1,
                          PatientAddress2 = TestObject.Patient.Address2,
                          PatientCity = TestObject.Patient.City,
                          PatientState = TestObject.Patient.State,
                          PatientZip = TestObject.Patient.Zip,
                          PatientHomePhone = TestObject.Patient.HomePhone,
                          PatientWorkPhone = TestObject.Patient.WorkPhone,
                          PatientCellPhone = TestObject.Patient.CellPhone,
                          PatientFax = TestObject.Patient.Fax,
                          PatientEmail = TestObject.Patient.Email,
                          TechnicianName = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianName),
                          TechnicianAddress = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianAddress),
                          TechnicianState = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianState),
                          TechnicianZipCode = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianZip),
                          TechnicianCity = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianCity),
                          TechnicianPhone = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianPhone),
                          Comments = string.Empty,
                          Test = TestObject
                      }
                    : _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter()
                                                                {
                                                                    ItemId = FocusedShippingOrder.Id
                                                                });

                if (isNew)
                {
                    var orderItems = new BindingList<OrderItem>();
                    foreach (var sl in TestObject.TestSchedule.ScheduleLines.Where(sl => !sl.IsDeleted && (sl.Item.ItemSourceLookup == null || (sl.Item.ItemSourceLookup != null && sl.Item.ItemSourceLookup.Id == UiHelperClass.GetSystemItemSourceLookupId()))))
                    {
                        orderItems.Add(new OrderItem() { ShippingOrder = currentShippingOrder, Include = true, Item = sl.Item, Quantity = int.Parse(sl.NoOfBottle) });
                    }

                    currentShippingOrder.OrderItems = orderItems;
                }

                var shippingOrderForm = new frmShippingOrder();
                shippingOrderForm.ShippingOrderObject = currentShippingOrder;
                shippingOrderForm.TestObject = TestObject;
                shippingOrderForm.PrintingOptions = xtraUserControlPrintingOptionsMain;

                UiHelperClass.HideSplash();

                shippingOrderForm.ShowDialog();
                if (shippingOrderForm.PatientDetailsChanged)
                {
                    _patientDetailsChanged = true;
                }

                UpdateShippingOrders();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Reloads shipping orders from DB and update their binding
        /// </summary>
        private void UpdateShippingOrders()
        {
            TestObject.ShippingOrders =
                       _shippingOrdersManager.GetShippingOrders(new ShippingOrdersFilter() { TestId = TestObject.Id });

            BindShippingOrders();
            TestObject.IsOrderSent = !(TestObject.ShippingOrders.Any(so => !so.Sent)) && TestObject.ShippingOrders.Any();
            TestObject.StateLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TestState, TestObject.IsOrderSent ? TestStateEnum.DoneShipped : TestStateEnum.Done, false, false));
            BindTestState();
        }

        /// <summary>
        /// Binds the shipping orders
        /// </summary>
        private void BindShippingOrders()
        {
            gridControlShippingOrders.DataBindings.Clear();
            TestObject.ShippingOrders.ListChanged += ShippingOrders_ListChanged;
            UiHelperClass.BindControl(gridControlShippingOrders, TestObject, () => TestObject.ShippingOrders);
            _shippingOrdersChanged = false;
        }

        /// <summary>
        /// Delete shipping order
        /// </summary>
        private void DeleteShippingOrder()
        {
            try
            {
                var shippingOrder = FocusedShippingOrder;

                if (shippingOrder.Sent)
                {
                    UiHelperClass.ShowInformation("Selected Shipping Order has been sent and can't be deleted.","Delete Shipping Order");
                    return;
                }
                
                if (UiHelperClass.ShowConfirmQuestion("The selected order will be deleted, are you sure?") == DialogResult.Yes)
                {
                    _shippingOrdersManager.Delete(shippingOrder);
                    UpdateShippingOrders();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #region Imprintable Items

        #region CRUD

        /// <summary>
        /// Binds the test imprintable items tree
        /// </summary>
        private void BindTestImprintableItems()
        {
            //treeListImprintableItems.DataSource = null;
            //treeListImprintableItems.ResetBindings();
            //treeListImprintableItems.DataBindings.Clear();
            TestObject.TestImprintableItems.ListChanged += TestImprintableItems_ListChanged;
            UiHelperClass.BindControl(treeListImprintableItems, TestObject, () => TestObject.TestImprintableItems);
            //treeListImprintableItems.DataSource = TestObject.TestImprintableItems;
            _testImprintableItemsChanged = false;
            treeListImprintableItems.ForceInitialize();
            treeListImprintableItems.ExpandAll();
        }

        /// <summary>
        /// Add imprintable Item to list
        /// </summary>
        private void AddImprintableItem()
        {
            var frmSelectItem = new XtraFormSelectItem();

            if (frmSelectItem.ShowDialog() == DialogResult.Yes && frmSelectItem.SelectedItem != null)
            {
                TestObject.AddNewImprintableItem(new Item() { Id = frmSelectItem.SelectedItem.Id, Name = frmSelectItem.SelectedItem.Name }, null);
            }
        }

        /// <summary>
        /// Delete Imprintable Item
        /// </summary>
        private void DeleteImprintableItems(bool markAsNotImprintable)
        {
            if (treeListImprintableItems.Selection.Count == 0) return;

            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected items will be deleted, are you sure?") == DialogResult.Yes)
                {
                    treeListImprintableItems.LockReloadNodes();

                    var tempDeletedItems = new BindingList<TestImprintableItem>();

                    foreach (TreeListNode node in treeListImprintableItems.Selection)
                    {
                        var imprintableItem = treeListImprintableItems.GetDataRecordByNode(node) as TestImprintableItem;

                        if (imprintableItem != null)
                        {
                            tempDeletedItems.Add(imprintableItem);
                        }
                    }

                    foreach (var tempImprintableItem in tempDeletedItems)
                    {
                        TestObject.DeleteImprintableItem(tempImprintableItem);

                        if (markAsNotImprintable)
                        {
                            TestLogicHelper.UpdateItemImprintableProperty(tempImprintableItem.Item, false);
                        }
                    }
                    treeListImprintableItems.RefreshDataSource();

                    treeListImprintableItems.UnlockReloadNodes();
                    treeListImprintableItems.ExpandAll();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #region UI

        /// <summary>
        /// Gets if sub item options are enabled for current selection
        /// </summary>
        /// <returns></returns>
        private bool SubItemsOptionsEnabled()
        {
            if (treeListImprintableItems.Selection.Count != 1) return false;

            var imprintableItem = treeListImprintableItems.GetDataRecordByNode(treeListImprintableItems.FocusedNode) as TestImprintableItem;

            var enableSubItemsOptions = TestObject.SubItemsOptionsEnabled(imprintableItem);

            return enableSubItemsOptions && !_isImprinting;
        }

        /// <summary>
        /// Enables and Disables imprinting order buttons
        /// </summary>
        /// <returns></returns>
        private void EnableDisableImprintingOrderButtons()
        {
            var enabled = treeListImprintableItems.Selection.Count == 1 && TestObject.TestImprintableItems.Any();

            var testImprintableItem = treeListImprintableItems.GetDataRecordByNode(treeListImprintableItems.FocusedNode) as TestImprintableItem;

            enabled = enabled && testImprintableItem != null && IsInEditMode && !_isImprinting;

            if (enabled)
            {
                simpleButtonImprintMoveUp.Enabled = !TestObject.IsFirstItem(testImprintableItem);
                simpleButtonImprintMoveDown.Enabled = !TestObject.IsLastItem(testImprintableItem);
            }
            else
            {
                simpleButtonImprintMoveUp.Enabled = false;
                simpleButtonImprintMoveDown.Enabled = false;
            }
        }

        /// <summary>
        /// Update the imprinting stage image.
        /// </summary>
        /// <param name="stage"></param>
        private void UpdateImprintingStageImage(int stage)
        {
            switch (stage)
            {
                case 0:
                    simpleButtonToggleImprinting.Image = Resources.Imp0;
                    break;
                case 1:
                    simpleButtonToggleImprinting.Image = Resources.Imp1;
                    break;
                case 2:
                    simpleButtonToggleImprinting.Image = Resources.Imp2;
                    break;
                case 3:
                    simpleButtonToggleImprinting.Image = Resources.Imp3;
                    break;
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Unimprint selected items
        /// </summary>
        private void UnimprintItems()
        {
            try
            {
                treeListImprintableItems.LockReloadNodes();

                foreach (TreeListNode node in treeListImprintableItems.Selection)
                {
                    var imprintableItem = treeListImprintableItems.GetDataRecordByNode(node) as TestImprintableItem;

                    if (imprintableItem != null)
                    {
                        imprintableItem.IsImprinted = false;
                    }
                }

                treeListImprintableItems.RefreshDataSource();
                treeListImprintableItems.UnlockReloadNodes();
                treeListImprintableItems.ExpandAll();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Check and Uncheck sub Items
        /// </summary>
        private void UpdateSubItemsCheck(bool isChecked)
        {
            try
            {
                treeListImprintableItems.LockReloadNodes();

                foreach (TreeListNode node in treeListImprintableItems.Selection)
                {
                    var imprintableItem = treeListImprintableItems.GetDataRecordByNode(node) as TestImprintableItem;
                    TestObject.UpdateSubItemsCheck(imprintableItem, isChecked);
                }

                treeListImprintableItems.RefreshDataSource();
                treeListImprintableItems.UnlockReloadNodes();
                treeListImprintableItems.ExpandAll();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Move imprintable items to root
        /// </summary>
        private void MoveImprintableItemsToRoot()
        {
            try
            {
                treeListImprintableItems.LockReloadNodes();

                foreach (TreeListNode node in treeListImprintableItems.Selection)
                {
                    var imprintableItem = treeListImprintableItems.GetDataRecordByNode(node) as TestImprintableItem;
                    TestObject.MoveItemToRoot(imprintableItem);
                }

                treeListImprintableItems.RefreshDataSource();
                treeListImprintableItems.UnlockReloadNodes();
                treeListImprintableItems.ExpandAll();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Updates imprintable items check state
        /// </summary>
        /// <param name="isChecked"></param>
        private void UpdateImprintableItemsCheck(bool isChecked)
        {
            foreach (var testImprintableItem in TestObject.TestImprintableItems)
            {
                testImprintableItem.IsChecked = isChecked;
            }
        }

        /// <summary>
        /// Switch item order
        /// </summary>
        /// <param name="isMovingUp"></param>
        private void SwitchOrder(bool isMovingUp)
        {
            var currentNode = treeListImprintableItems.FocusedNode;
            var currentItem = treeListImprintableItems.GetDataRecordByNode(treeListImprintableItems.FocusedNode) as TestImprintableItem;
            TestObject.SwitchOrder(currentItem, isMovingUp);
            treeListImprintableItems.SetFocusedNode(currentNode);
        }

        #endregion

        #region Imprinting

        /// <summary>
        /// Imprinting Action.
        /// </summary>
        private void ImprintItem(TestImprintableItem imprintableItem)
        {
            //if (!CsaEmdUnitManager.Instance.IsCsaEmdUnitConnected)
            //    return;

            imprintableItem.IsImprinted = true;
            imprintableItem.IsChecked = false;
            CsaEmdUnitManager.Instance.Broadcast(new List<Item> { imprintableItem.Item }, true);
        }

        /// <summary>
        /// Imprints multiple items at once
        /// </summary>
        /// <param name="imprintableItems"></param>
        private void ImprintMultipleItems(List<TestImprintableItem> imprintableItems)
        {
            //if (!CsaEmdUnitManager.Instance.IsCsaEmdUnitConnected)
            //    return;

            foreach (var testImprintableItem in imprintableItems)
            {
                ImprintItem(testImprintableItem);
            }
        }

        /// <summary>
        /// Starts imprinting
        /// </summary>
        private void StartImprinting()
        {
            //START IMPRINTING
            if (CsaEmdUnitManager.Instance.IsCsaEmdUnitConnected)
            {
                if (TestObject.TestImprintableItems.Any(t => t.IsChecked))
                {
                    tabbedControlGroupImprinting.SelectedTabPage = layoutControlGroupImprintableItems;
                    UiHelperClass.ShowWaitingPanel("Building Imprintable Items List ...");

                    TestObject.UpdateImprintableItemsIndex();

                    UiHelperClass.HideSplash();

                    simpleButtonToggleImprinting.Text = StaticKeys.StopImprinting;
                    marqueeProgressBarControlImprinting.Properties.Stopped = false;
                    
                    simpleLabelItemImprintingInfo.Visibility = LayoutVisibility.Always;

                    _imprintingCounter = ImprintingTime;
                    SetCountDownString(_imprintingCounter, true);

                    treeListImprintableItems.Selection.Clear();

                    foreach (var imprintableItem in TestObject.TestImprintableItems)
                    {
                        if (imprintableItem != null && imprintableItem.IsChecked && !imprintableItem.IsImprinted)
                        {
                            var node = treeListImprintableItems.FindNodeByFieldValue(
                                ExpressionHelper.GetPropertyName(() => imprintableItem.TempId), imprintableItem.TempId);
                            if (node != null)
                            {
                                node.Selected = true;
                                treeListImprintableItems.Selection.Add(node);
                            }
                        }
                    }
                    treeListImprintableItems.Refresh();
                    treeListImprintableItems.ExpandAll();
                    _isImprinting = true;

                    _imprintingFormStatus = FormStatus;

                    FormStatus = FormStatusEnum.Locked;
                    SetAllToolbarItemsEditState(true);

                    timerImprinting.Start();
                    if (checkEditUseTimer.Checked)
                    {
                        var itemsCount = TestObject.TestImprintableItems.Count(t => !t.IsImprinted && t.IsChecked);
                        _imprintingPagingCounter = Math.Ceiling(itemsCount / ((decimal)ImprintingTime));
                        timerImprintingCountDown.Start();
                    }
                    else
                    {
                        ImprintMultipleItems(TestObject.TestImprintableItems.Where(t => !t.IsImprinted && t.IsChecked).ToList());
                        treeListImprintableItems.Refresh();
                    }
                }
                else
                {
                    UiHelperClass.ShowInformation("No Items are checked for imprinting.");
                }
            }
            else
            {
                UiHelperClass.ShowInformation(StaticKeys.CsaNotConnected, StaticKeys.CsaImprintingText);   
            }
        }

        /// <summary>
        /// Stops imprinting
        /// </summary>
        private void StopImprinting()
        {
            timerImprinting.Stop();
            timerImprintingCountDown.Stop();
            simpleButtonToggleImprinting.Text = StaticKeys.StartImprinting;
            marqueeProgressBarControlImprinting.Properties.Stopped = true;
            simpleLabelItemImprintingInfo.Visibility = LayoutVisibility.Never;
            stateIndicatorComponent1.StateIndex = 0;

            _imprintingCounter = ImprintingTime;
            SetCountDownString(_imprintingCounter, false);

            ImprintingStage = 0;

            UpdateBroadcastingStageImage(0);
            UpdateImprintingStageImage(0);
            simpleButtonToggleImprinting.Image = Resources.Imp3;
            SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.NotBroadcasting);

            _isImprinting = false;

            SetAllToolbarItemsEditState(false);
            FormStatus = FormStatusEnum.UnLocked;
            FormStatus = FormStatusEnum.Modified;
            treeListImprintableItems.Selection.Clear();
            _imprintingCounter = 0;

            if (checkEditUseTimer.Checked)
            {
                UiHelperClass.ShowInformation("Imprinting is done.");
            }
        }

        /// <summary>
        /// Sets the imprinting counter string
        /// </summary>
        /// <param name="secondsCount"></param>
        /// <param name="isStarting"></param>
        private void SetCountDownString(int secondsCount, bool isStarting)
        {

            if (isStarting)
            {
                if (checkEditUseTimer.Checked)
                {
                    digitalGaugeImprintingTime.DigitCount = 4;
                    var seconds = secondsCount % 60;
                    var minutes = secondsCount / 60;
                    digitalGaugeImprintingTime.Text = minutes.ToString("00") + ":" + seconds.ToString("00");
                }
                else
                {
                    digitalGaugeImprintingTime.DigitCount = 8;
                    digitalGaugeImprintingTime.Text = " Manual ";
                }
            }
            else
            {
                if (checkEditUseTimer.Checked)
                {
                    digitalGaugeImprintingTime.DigitCount = 4;
                    var seconds = secondsCount % 60;
                    var minutes = secondsCount / 60;
                    digitalGaugeImprintingTime.Text = minutes.ToString("00") + ":" + seconds.ToString("00");
                }
                else
                {
                    digitalGaugeImprintingTime.DigitCount = 8;
                    digitalGaugeImprintingTime.Text = "On Hold";
                }
            }
        }

        /// <summary>
        /// Logic for imprinting one or multiple items on timer tick
        /// </summary>
        private void TimerImprint()
        {
            //var itemsCount = TestObject.TestImprintableItems.Count(t => !t.IsImprinted && t.IsChecked);

            ////If there are no more items to imprint, then stop even if the time is not done.
            //if (itemsCount == 0)
            //{
            //    StopImprinting();
            //}

            //Imprint the items in segments to make sure that all marked items gets imprinted within the available time
            for (var i = 0; i < _imprintingPagingCounter; i++)
            {
                var nextImprintableItem = TestObject.TestImprintableItems.Where(t => !t.IsImprinted && t.IsChecked).OrderBy(t => t.ImprintingIndex).FirstOrDefault();

                if (nextImprintableItem != null)
                {
                    ImprintItem(nextImprintableItem);
                    var node = treeListImprintableItems.FindNodeByFieldValue(ExpressionHelper.GetPropertyName(() => nextImprintableItem.TempId), nextImprintableItem.TempId);

                    //Unselect node after imprint to make sure the highlighting works
                    if (node != null)
                    {
                        node.Selected = false;
                        treeListImprintableItems.Selection.Remove(node);
                    }
                }
            }

            _imprintingCounter -= 1;

            //Show the timing string only if the value is bigger than or equal to zero to prevent showing -1
            if (_imprintingCounter >= 0)
            {
                SetCountDownString(_imprintingCounter, true);
            }

            if (_imprintingCounter == 0)
            {
                StopImprinting();
            }
        }

        /// <summary>
        /// Updates state indicators during imprinting to reflect that in UI
        /// </summary>
        private void TimerUpdateStateIndicators()
        {
            ImprintingStage = ImprintingStage == 3 ? 0 : ImprintingStage + 1;

            if (_imprintingLightCounter == 1)
            {
                _imprintingLightCounter = 0;

                stateIndicatorComponent1.StateIndex = stateIndicatorComponent1.StateIndex == 0 ? 3 : 0;
            }
            else
            {
                _imprintingLightCounter += 1;
            }

            SetConnectionIndicatorStatus(ConnectionIndicatorStatusEnum.Imprinting);

            UpdateBroadcastingStageImage(ImprintingStage);
            UpdateImprintingStageImage(ImprintingStage);
        }

        /// <summary>
        /// Stops or starts imprinting
        /// </summary>
        private void ToggleImprintinState()
        {
            //STOP IMPRINTING
            if (_isImprinting)
            {
                StopImprinting();
            }
            else
            {
                StartImprinting();
            }
        }

        #endregion
   
        #endregion

        #region UI Helpers

        #endregion

        #region Printing

        /// <summary>
        /// Logic for printing
        /// </summary>
        /// <param name="isPreview"></param>
        /// <param name="printShort">Option to indicate if report should be printed short or long</param>
        private void Print(bool isPreview, bool showDescription)
        {
            if (!UiHelperClass.HasPrintableInfo(xtraUserControlPrintingOptionsMain)) return;

            PostValues();

            var selectedTests = new List<Test>();
            UiHelperClass.ShowSplash(typeof (WaitFormLoadingData));
            selectedTests.Add(TestObject);

            UiHelperClass.HideSplash();
            xtraUserControlPrintingOptionsMain.ShowDescriptionTemporaryOption = showDescription;

            UiHelperClass.PrintTestReport(isPreview, _settingsManager, TestObject.Patient, selectedTests, _productTypeLookupId,
                                          xtraUserControlPrintingOptionsMain, _nonelistTypeLookupId, _yesLookupId, _potencyTypeLookupId);
            xtraUserControlPrintingOptionsMain.ShowDescriptionTemporaryOption = false;
        }

        /// <summary>
        /// Logic for printing the imprinting report
        /// </summary>
        /// <param name="testObject">The test object,</param>        
        /// <param name="printingOptions">The printing options.</param>
        public void ShowImprintingReport()
        {
            UiHelperClass.ShowWaitingPanel("Preparing Report");

            var report = new XtraReportImprinting
            {
                PatientName = { Value = TestObject.Patient.FirstName + " " + TestObject.Patient.LastName },
                bindingSourcePatient = { DataSource = TestObject.Patient },
                bindingSourceTest = { DataSource = TestObject },
                HidePatientName = { Value = xtraUserControlPrintingOptionsMain.HidePatientName },
                HideLogo = { Value = xtraUserControlPrintingOptionsMain.HideLogo },
                ShowAddressInfo = { Value = xtraUserControlPrintingOptionsMain.ShowAddressInfo },
            };

            var wcn = new WinControlContainer();

            var treeListImprintingPrint = new TreeList();
            treeListImprintingPrint.Columns.AddRange(new[] { treeListColumnItemName, treeListColumnImprintingOrder });

            if (
                TestObject.TestImprintableItems.Any(ti => ti.IsImprinted && !string.IsNullOrEmpty(ti.Comments)))
            {
                treeListImprintingPrint.Columns.AddRange(new[] {treeListColumnImpritableComments });
            }

            treeListImprintingPrint.ParentFieldName = "ParentId";
            treeListImprintingPrint.KeyFieldName = "TempId";
            treeListImprintingPrint.Name = "treeListImprintingPrint";

            treeListImprintingPrint.OptionsPrint.AutoWidth = false;
            treeListImprintingPrint.OptionsPrint.AutoRowHeight = true;
            treeListImprintingPrint.OptionsPrint.PrintPageHeader = false;
            treeListImprintingPrint.OptionsPrint.PrintReportFooter = false;
            treeListImprintingPrint.OptionsPrint.UsePrintStyles = true;
            treeListImprintingPrint.OptionsPrint.PrintAllNodes = true;

            treeListImprintingPrint.OptionsBehavior.PopulateServiceColumns = true;
            treeListImprintingPrint.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeListImprintingPrint.OptionsSelection.UseIndicatorForSelection = true;
            treeListImprintingPrint.OptionsView.EnableAppearanceEvenRow = true;
            treeListImprintingPrint.OptionsView.EnableAppearanceOddRow = true;
            treeListImprintingPrint.OptionsView.ShowFocusedFrame = false;

            treeListImprintingPrint.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEditImprintableItems });
            treeListImprintingPrint.RootValue = null;
            treeListImprintingPrint.SelectImageList = imageListImprinting;
            treeListImprintingPrint.Size = new Size(770, 457);
            
            treeListImprintingPrint.DataSource = TestObject.TestImprintableItems.Where(ti=>ti.IsImprinted).ToBindingList();
            treeListImprintingPrint.LookAndFeel.SetDefaultStyle();
            treeListImprintingPrint.ForceInitialize();

            wcn.WinControl = treeListImprintingPrint;

            report.Bands[BandKind.Detail].Controls.Add(wcn);
            wcn.Location = new Point(10, 0);
            wcn.Size = new Size(790, 457);

            UiHelperClass.SetReportLogo(report.xrSubreportHeader);

            UiHelperClass.HideSplash();
            UiHelperClass.ShowReport(report);
        }

        /// <summary>
        /// Print Notes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintNotes_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintNotes_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!UiHelperClass.HasPrintableInfo(xtraUserControlPrintingOptionsMain)) return;

                PostValues();

                var selectedTests = new List<Test>();
                UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
                selectedTests.Add(TestObject);

                UiHelperClass.HideSplash();
                UiHelperClass.PrintTestNotesReport(true, TestObject.Patient, selectedTests, xtraUserControlPrintingOptionsMain);
            }
        }

        #endregion

        #region Reading Controls Methods

        /// <summary>
        /// Updates the reading value for the gauges.
        /// </summary>
        /// <param name="locationLookup">The location lookup.</param>
        /// <param name="readingValue">The reading value.</param>
        public void UpdateReading(Lookup locationLookup, float readingValue)
        {
            if(IsClosing)
                return;

            xtraUserControlReadingGauge.ReadingValue = readingValue;
            xtraUserControlReadingGauge.LocationLookup = locationLookup;
        }

        /// <summary>
        /// Clear the binding.
        /// </summary>
        public void ClearReadingControls()
        {
            xtraUserControlItemDetails.ClearImageAndDetails();
            xtraUserControlReadingGauge.Clear();
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
        private void TestObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(TestObject_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //e.PropertyName == ExpressionHelper.GetMemberName(() => TestObject.TestSchedule.ReevalInWeeks)
                if (e.PropertyName == ExpressionHelper.GetPropertyName(() => TestObject.ShippingOrders))
                {
                    return;
                }

                if (e.PropertyName == StaticKeys.ImprintableItemsProperty && _isImprinting)
                {
                    return;
                }

                switch (TestObject.ObjectState)
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
        /// Handle the propriety changed event for TestSchedule.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        void TestSchedule_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(TestSchedule_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (ExpressionHelper.GetPropertyName(() => TestObject.TestSchedule.ReevalInWeeks).Equals(e.PropertyName))
                    TestObject.EvalPeriodChecked = true;

                if (_allowSummaryUpdate && 
                   (ExpressionHelper.GetPropertyName(() => TestObject.TestSchedule.Discount).Equals(e.PropertyName)             ||
                    ExpressionHelper.GetPropertyName(() => TestObject.TestSchedule.DiscountAsPercentage).Equals(e.PropertyName) ||
                    ExpressionHelper.GetPropertyName(() => TestObject.TestSchedule.DiscountApply,() => TestObject.TestSchedule.DiscountApply.Id).Equals(e.PropertyName) ||
                    ExpressionHelper.GetPropertyName(() => TestObject.TestSchedule.Tax).Equals(e.PropertyName))                 ||
                    ExpressionHelper.GetPropertyName(() => TestObject.TestSchedule.ScheduleLines).Equals(e.PropertyName))
                {
                    UpdateTotalSummary();
                }
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

            if (!HasChanges()) return true;

            return SaveOrSaveAndClose(false);
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
                if (_isImprinting)
                {
                    e.Cancel = true;
                    IsClosing = false;
                    return;
                }

                var canClose = CanClose();

                var checkOrders = false;
                if (canClose && 
                    TestObject.ObjectState != DomainEntityState.Deleted && 
                    UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings,SettingKeys.RemindToOrderBeforeClosingTest) && !TestObject.IsOrderSent)
                {
                    if (
                        UiHelperClass.ShowConfirmQuestion(
                            "You are closing the test without sending a shipment order, are you sure?") !=
                        DialogResult.Yes)
                    {
                        checkOrders = true;
                    }
                }

                e.Cancel = !canClose || checkOrders;

                if (e.Cancel)
                {
                    IsClosing = false;
                    return;
                }
                UpdateImprintingSettings();
                CloseCsaConnection();

                //saving the meter position
                if (dockPanelMeter.Dock == DockingStyle.Right || dockPanelMeter.Dock == DockingStyle.Left)
                {
                    var meterPosition =
                        _settingsManager.GetSetting(new SettingsFilter
                                                        {Key = EnumNameResolver.Resolve(SettingKeys.MeterPosition)});
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
        /// Handles the form loaded event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGeneralTest_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(frmGeneralTest_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                IsLoaded = true;
                UiHelperClass.HideSplash();
                //We set the initial opacity of the form as 0 so it shows up invisible while ax
                //and while the splash screen is showing up, then when all loading is done, the splash screen
                //is closed and the form opacity is set to 100 to make it completely visible.
                Opacity = 100;

                if (FormStatus == FormStatusEnum.New || _newSavedTest)
                {
                    CheckForAutomation();
                }
                else
                {
                    var enableAutomationWhenOpeningExistingTest = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.EnableAutomationWhenOpeningExistingTest);

                    if (enableAutomationWhenOpeningExistingTest && _currentTestPlayState == TestPlayStateEnum.Paused)
                    {
                        _ignoreAutoTestDoneMessage = true;

                        StartAutoPlayTestMode();

                        _ignoreAutoTestDoneMessage = false;
                    }
                }
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
        /// Handel edit description request.
        /// </summary>
        /// <param name="sender"></param>
        private void xtraUserControlItemDetails_EditDescription(XtraUserControlItemDetails sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlItemDetails.EditDescriptionEventHandler(xtraUserControlItemDetails_EditDescription), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.OpeningMessgae);

                var itemDescriptionDialog = new XtraFormItemDescription(_currentSelectedItem);

                StopReadingBasedOnSelectedTab();
                ShowHideOverlay(true);
                var result = itemDescriptionDialog.ShowDialog();
                ShowHideOverlay(false);
                if (result == DialogResult.OK)
                {
                    var existsItem = _updatedDescriptionItems.FirstOrDefault(i => i.Id == _currentSelectedItem.Id);

                    if (existsItem != null)
                        _updatedDescriptionItems.Remove(existsItem);

                    _updatedDescriptionItems.Add(_currentSelectedItem);
                }
                    
                ActivateConnectionBasedOnSelectedTab();

                StartReadingBasedOnSelectedTab();

                SetDetails(_currentSelectedItem);

                UiHelperClass.HideSplash();
            }
        }
        
        /// <summary>
        /// Handel key down event on the form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void frmGeneralTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(frmGeneralTest_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isImprinting) return;

                if (e.KeyCode == Keys.P && e.Control && IsInEditMode)
                {
                    ToggleTestPlayState();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageEDS;
                }
                else if (e.KeyCode == Keys.I && e.Alt)
                {
                    xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageItemTesting;
                }
                else if (e.KeyCode == Keys.N && e.Alt)
                {
                    xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageImprinting;
                }
                else if (e.KeyCode == Keys.T && e.Alt)
                {
                    xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPagePatientSchedule;
                }
                else if (e.KeyCode == Keys.O && e.Alt)
                {
                    xtraTabControlItemTestingTabs.SelectedTabPage = xtraTabPageNotes;
                }
                else if (e.KeyCode == Keys.B && e.Alt)
                {
                    EnableBalancingMode();
                }
                else if (e.KeyCode == Keys.Y && e.Alt)
                {
                    radioGroupEdsMode.SelectedIndex = radioGroupEdsMode.SelectedIndex == 0 ? 1 : 0;
                }
                else if (e.KeyCode == Keys.C && e.Alt &&
                         xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting && IsInEditMode)
                {
                    OpenMajorIssuesDialog();
                }
                else if (e.KeyCode == Keys.F3 && xtraTabControlItemTesting.SelectedTabPage == xtraTabPageIssues)
                {
                    ShowItems();
                }
                else if ((e.KeyCode == Keys.Insert || (e.Control && e.KeyCode == Keys.I) || e.KeyCode == Keys.Oemtilde) &&
                         xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageImprinting)
                {
                    AddImprintableItem();
                }
                else if (e.KeyCode == Keys.A && e.Alt && IsInEditMode &&
                         xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
                {
                    AddProducts();
                }
                else if (e.KeyCode == Keys.R && e.Control &&
                     xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule && IsInEditMode)
                {
                    ShowReEvaluationnDialog();
                }
                else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting &&
                         xtraTabControlItemTesting.SelectedTabPage == xtraTabPageItems)
                {
                    xtraUserControlItemsNavGridMajorIssues.NavGrid_KeyDown(sender, e);
                }
                else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageItemTesting &&
                         xtraTabControlItemTesting.SelectedTabPage == xtraTabPageIssues &&
                         TestObject.TestIssues.Count != 0)
                {
                    if (e.KeyCode == Keys.PageUp)
                    {
                        if (xtraTabControlIssues.SelectedTabPageIndex < xtraTabControlIssues.TabPages.Count)
                        {
                            xtraTabControlIssues.SelectedTabPageIndex =
                                xtraTabControlIssues.SelectedTabPageIndex + 1;
                        }
                    }
                    else if (e.KeyCode == Keys.PageDown)
                    {
                        if (xtraTabControlIssues.SelectedTabPageIndex > 0)
                        {
                            xtraTabControlIssues.SelectedTabPageIndex =
                                xtraTabControlIssues.SelectedTabPageIndex - 1;
                        }
                    }
                    else
                    {
                        var currentIssueControl = GetSelectedIssueUserConrol();
                        if (currentIssueControl != null)
                        {
                            currentIssueControl.Issue_KeyDown(sender, e);
                        }
                    }

                }
                else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageProductsTesting)
                {
                    xtraUserControlIssueMain.Issue_KeyDown(sender, e);
                }
            }
        }

        /// <summary>
        /// Prevent the showing of the PopupMenu for the dock panel, (this PopupMenu contains close option that we need to disable)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockManagerMain_PopupMenuShowing(object sender,
                                                      DevExpress.XtraBars.Docking.PopupMenuShowingEventArgs e)
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

        #endregion

        #region EDS Events Handler

        /// <summary>
        /// Handel the edit value changing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridLookUpEditItemGroup_EditValueChanging(object sender,ChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraEditors.Controls.ChangingEventHandler(
                            gridLookUpEditItemGroup_EditValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded && TestObject.ObjectState != DomainEntityState.New) return;

                BeforePointGroupChange();

                var currentItemGroup = ((BindingList<Item>) gridLookUpEditItemGroup.Properties.DataSource).FirstOrDefault(i => i.Id == (int) e.NewValue);

                if (currentItemGroup != null)
                {
                    if (TestObject.Readings != null && TestObject.Readings.Count > 0)
                    {
                        gridLookUpEditItemGroup.EditValueChanging -= gridLookUpEditItemGroup_EditValueChanging;
                        gridLookUpEditItemGroup.EditValue = e.NewValue;
                        gridLookUpEditItemGroup.EditValueChanging += gridLookUpEditItemGroup_EditValueChanging;

                        SetupReadings(currentItemGroup.Id, true);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the change of focused reading row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_FocusedRowChanged(object sender,DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewItems_FocusedRowChanged),
                        sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded)
                    return;

                FocusedReadingRowChanged(true);
            }
        }

        /// <summary>
        /// Handles the custom draw cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_CustomDrawCell(object sender,DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gridViewItems_CustomDrawCell),
                        sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Column.Name == gridlColumnValue.Name || e.Column.Name == gridColumnValueBalanced.Name ||
                    e.Column.Name == gridlColumnValue2.Name || e.Column.Name == gridColumnValueBalanced2.Name)
                {
                    int value = Convert.ToInt16(e.CellValue);

                    var gridCellInfo = e.Cell as GridCellInfo;
                    if (gridCellInfo != null)
                    {
                        var vi = gridCellInfo.ViewInfo as ProgressBarViewInfo;

                        if (vi != null)
                        {
                            vi.ProgressInfo.EndColor = GetRangeColor(value);
                            vi.ProgressInfo.StartColor = GetRangeColor(value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the clicking on the reading row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_RowClick(object sender, RowClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowClickEventHandler(gridViewItems_RowClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                FocusedReadingRowChanged(true);
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
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
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

                int timeOut;
                float secondToWait;
                TestBarStateEnum status;

                if (_currentAutoPlayTestType == ReadingPlayTypes.ItemTesting)
                {
                    timeOut = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime;

                    secondToWait = (timeOut - _autoTestNextReadingTimeOut)/1000f;

                    status = TestBarStateEnum.WaitBeforTakeAction;

                }
                else
                {
                    timeOut = CsaEmdUnitManager.Instance.EdsAutoPlayWaitingTime;

                    secondToWait = (timeOut - _autoTestNextReadingTimeOut)/1000f;

                    status = TestBarStateEnum.WaitMoving;
                }

                SetReadingIndicators(_currentAutoPlayTestType, status, secondToWait);

                if (_currentAutoPlayTestType == ReadingPlayTypes.Eds)
                {
                    SetReadingStatusBarMode(TestBarStateEnum.WaitMoving, secondToWait);
                    SetReadingStatusBarMode(TestBarStateEnum.WaitMoving, string.Empty, secondToWait);
                }
                else
                {
                    if (_currentControlItemsNavGridRequestor != null)
                    {
                        var nextActionName = _currentControlItemsNavGridRequestor.GetNextActionName();
                        _currentControlItemsNavGridRequestor.SetReadingStatusBarMode(
                            TestBarStateEnum.WaitBeforTakeAction,
                            nextActionName, secondToWait);
                        SetReadingStatusBarMode(TestBarStateEnum.WaitBeforTakeAction, nextActionName, secondToWait);
                    }
                }

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
        /// Sets the reading indicator.
        /// </summary>
        public void SetReadingIndicators(ReadingPlayTypes readingPlayType, TestBarStateEnum testBarState, float secondsToWait)
        {
            if(IsClosing) 
                return;

            if (readingPlayType == ReadingPlayTypes.Eds || readingPlayType == ReadingPlayTypes.TestSchedule)
            {

                SetReadingStatusBarMode(testBarState, secondsToWait);
                SetReadingStatusBarMode(testBarState, string.Empty, secondsToWait);
            }
            else
            {
                if (_currentControlItemsNavGridRequestor == null) return;
                var nextActionName = testBarState == TestBarStateEnum.WaitBeforTakeAction
                                         ? _currentControlItemsNavGridRequestor.GetNextActionName()
                                         : string.Empty;

                _currentControlItemsNavGridRequestor.SetReadingStatusBarMode(testBarState, nextActionName, secondsToWait);
                SetReadingStatusBarMode(testBarState, nextActionName, secondsToWait);
            }
        }

        /// <summary>
        /// Handel when the lose the Focus on the eds points grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_LostFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewItems_LostFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ResetCurrentReadingValue();
            }
        }

        /// <summary>
        /// Handel the clear eds reading button click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void simpleButtonClearEds_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonClearEds_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ClearCurrentPointSetEdsReadings();
            }
        }

        /// <summary>
        /// Handle logic to clear all eds readings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClearAllEDS_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonClearAllEDS_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ClearAllEdsReadings();
            }
        }

        /// <summary>
        /// Handel the Hover over the reading row to show the tool tip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolTipControllerPointsGrid_GetActiveObjectInfo(object sender,ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new ToolTipControllerGetActiveObjectInfoEventHandler(
                            toolTipControllerPointsGrid_GetActiveObjectInfo), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.SelectedControl != gridControlItems) return;

                ToolTipControlInfo info = null;
                //Get the view at the current mouse position
                var view = gridControlItems.GetViewAt(e.ControlMousePosition) as GridView;

                if (view == null) return;

                //Get the view's element information that resides at the current position
                var hitInfo = view.CalcHitInfo(e.ControlMousePosition);

                //Display a hint for row indicator cells
                if (hitInfo.HitTest == GridHitTest.RowCell)
                {
                    //An object that uniquely identifies a row indicator cell
                    var o = hitInfo.HitTest.ToString() + hitInfo.RowHandle;
                    var focusedReading = gridViewItems.GetRow(hitInfo.RowHandle) as Reading;

                    if (focusedReading != null && focusedReading.Item != null)
                    {
                        info = new ToolTipControlInfo(o, focusedReading.Item.Name + "\n" + focusedReading.Item.FullName);
                    }

                }

                //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
                if (info != null)
                    e.Info = info;
            }
        }

        /// <summary>
        /// Handles the reading point location changing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditReadingLocation_EditValueChanging(object sender,ChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return; Invoke(new ChangingEventHandler(lookUpEditReadingLocation_EditValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (IsLoaded && TestObject.Readings != null)
                {
                    _currentLocationLookupId = (int)e.NewValue;
                    SetupReadings((int)gridLookUpEditItemGroup.EditValue, true);
                }
            }
        }

        /// <summary>
        /// Handles the focus on the reading grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewItems_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                StartReading(ReadingRequesterTypeEnum.Eds, null);
                UpdateImageAndGaugesForReading();
            }
        }

        #endregion

        #region Item Testing Handlers

        /// <summary>
        /// Handle the enable/disable of create major issues button based on the number of major issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestIssues_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(TestIssues_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                TestIssuesChangedActions();
                SetNextIssueNumberText();
            }
        }

        /// <summary>
        /// Do the test issues list changed event actions, this extract method for make a code useable incase we have to do this actions manually. 
        /// </summary>
        private void TestIssuesChangedActions()
        {
            lookUpEditTestProtocol.Properties.ReadOnly = TestObject.TestIssues.Count > 0;
        }

        /// <summary>
        /// This event will set the datasource of the items nav grid based on test protocol selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditTestProtocol_EditValueChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(lookUpEditTestProtocol_EditValueChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (lookUpEditTestProtocol.EditValue == null || lookUpEditTestProtocol.EditValue == DBNull.Value)
                    return;

                var protocolItems = _testProtocolsManager.GetProtocolItems(new ProtocolItemsFilter
                                                                               {
                                                                                   TestProtocolId =
                                                                                       (int)
                                                                                       lookUpEditTestProtocol.EditValue
                                                                               });

                var protocols = lookUpEditTestProtocol.Properties.DataSource as BindingList<TestProtocol>;

                if (protocols != null)
                    TestObject.TestProtocol =
                        protocols.FirstOrDefault(protocol => protocol.Id == (int) lookUpEditTestProtocol.EditValue);

                xtraUserControlItemsNavGridMajorIssues.TestResults.Clear();
                xtraUserControlItemsNavGridMajorIssues.IsFirstFetch = true;
                xtraUserControlItemsNavGridMajorIssues.BackClicks = 0;
                SetNavGridMajorIssuesProtocol();
                xtraUserControlItemsNavGridMajorIssues.InitGridItems(
                    new BindingList<Item>(protocolItems.Select(protocolItem => protocolItem.Item).ToList()), true, false,
                    false);
            }
        }

        /// <summary>
        /// Handle the change of the issues tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControlIssues_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangedEventHandler(xtraTabControlIssues_SelectedPageChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isRebinding) return;

                if (xtraTabControlItemTesting.SelectedTabPage == xtraTabPageIssues)
                {
                    BindSelectedIssueTabItems();
                }

                ClearReadingControls();

                //Logic below is to fix an issue with the following Steps:
                //- Add two major issues
                //- Save and close
                //- Open test and right click second issue in the list and delete it
                //- Without changing focus or anything ... make a 50 reading .. a crash will happen
                //- Exception happened because Vital was trying to do testing using the tab of the first issue where it is not loaded yet
                //- Fix below is by disabling the logic called to update UI if the issues tab is not selected
                if (xtraTabControlItemTesting.SelectedTabPage == xtraTabPageIssues)
                {
                    ControlItemDetailesAndReadingsDependOnUserActionForIssue(false);
                }
            }
        }

        /// <summary>
        /// Handle the change of the item testing tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControlItemTesting_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangedEventHandler(xtraTabControlItemTesting_SelectedPageChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isRebinding) return;

                if (!_isAddingEdsProduct && xtraTabControlItemTesting.SelectedTabPage == xtraTabPageIssues)
                {
                    BindSelectedIssueTabItems();
                }

                ClearReadingControls();

                ControlItemDetailesAndReadingsDependOnUserAction(false);
            }
        }

        /// <summary>
        /// Handel the tab page changing to bind the notes.
        /// </summary>
        private void xtraTabControlItemTestingTabs_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangingEventHandler(xtraTabControlItemTestingTabs_SelectedPageChanging), sender,
                           e);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isImprinting)
                {
                    e.Cancel = true;
                    xtraTabControlItemTestingTabs.SelectedTabPage = e.PrevPage;
                }

                if (e.Page == xtraTabPageNotes && _xtraUserControlVitalRichEditNotes == null)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingNotes);

                    _xtraUserControlVitalRichEditNotes = new XtraUserControlVitalRichEdit()
                                                             {
                                                                 Dock = DockStyle.Fill,
                                                                 ReadOnly = !IsInEditMode
                                                             };

                    panelControlNotes.Controls.Add(_xtraUserControlVitalRichEditNotes);

                    BindVitalRichEditControl();

                    UiHelperClass.HideSplash();
                }
            }
        }

        /// <summary>
        /// This event will add item selected in the items nav grid as a major issue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelectMajorIssue_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonSelectMajorIssue_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var item = xtraUserControlItemsNavGridMajorIssues.GetSelectedItems().FirstOrDefault();

                AddToMajorIssues(item);
            }
        }

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
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
                if (sender != contextMenuStripMajorIssues)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewMajorIssues);

                    var isEnabled = UiHelperClass.IsClickInRow(gridViewMajorIssues);

                    contextMenuStripMajorIssues.Enabled = isEnabled && IsInEditMode;
                }
                
                if (sender == contextMenuStripServices)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewServices);
                    toolStripMenuItemDeleteService.Enabled =
                        UiHelperClass.IsClickInRowByMouse(gridViewServices) && IsInEditMode;
                }

                if (sender == contextMenuStripShippingOrders)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewShippingOrders);

                    toolStripMenuItemDeleteOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders) && IsInEditMode && !OrderSent;
                    toolStripMenuItemNewOrder.Enabled = TestObject.ObjectState != DomainEntityState.New && IsInEditMode && !HasOfflineOrders;
                    toolStripMenuItemOpenOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders);
                    toolStripMenuItemSendOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders) &&
                                                         IsInEditMode && !OrderSent;

                }
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripMajorIssuesClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(ContextMenuStripMajorIssuesClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                ((ContextMenuStrip) sender).Hide();

                if (sender == contextMenuStripMajorIssues)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteMajorIssue)
                    {
                        DeleteMajorIssue(gridViewMajorIssues.FocusedRowHandle);
                    }
                }

                if (sender == contextMenuStripServices)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteService)
                    {
                        DeleteService();
                    }
                }

                if (sender == contextMenuStripShippingOrders)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteOrder)
                    {
                        DeleteShippingOrder();
                    }
                    else if (e.ClickedItem == toolStripMenuItemOpenOrder)
                    {
                        OpenShippingOrder(false);
                    }
                    else if (e.ClickedItem == toolStripMenuItemNewOrder)
                    {
                        OpenShippingOrder(true);
                    }
                    else if (e.ClickedItem == toolStripMenuItemSendOrder)
                    {
                        SendShippingOrder();
                    }
                }
            }
        }

        /// <summary>
        /// Show the number of issues counting dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSetMajorIssuesNumber_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonSetMajorIssuesNumber_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenMajorIssuesDialog();
            }
        }
        
        /// <summary>
        /// Show the items tab in case it is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonShowItems_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonShowItems_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowItems();
            }
        }

        /// <summary>
        /// Handle the request to refresh image and detail
        /// </summary>
        /// <param name="sender"></param>
        private void xtraUserControlItemsNavGridMajorIssues_RefreshDetailsAndImageNavGrid(XtraUserControlItemsNavGrid sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueC`hanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnRefreshDetailsAndImageNavGrid(xtraUserControlItemsNavGridMajorIssues_RefreshDetailsAndImageNavGrid),
                        sender);

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
            }
        }

        /// <summary>
        /// Handel the ReadingRequest requests from the xtraUserControlItemsNavGridMajorIssues.
        /// </summary>
        private void xtraUserControlItemsNavGridMajorIssues_ReadingRequest(XtraUserControlItemsNavGrid sender,
                                                                           List<Item> itemsToBroadcast)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnReadingRequest(
                            xtraUserControlItemsNavGridMajorIssues_ReadingRequest), sender, itemsToBroadcast);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
                StartReading(ReadingRequesterTypeEnum.ItemTestingIssueNav, itemsToBroadcast.ToList());

                _currentControlItemsNavGridRequestor = sender;
            }

        }
        /// <summary>
        /// Re Activate the CSA connection after using from another from.
        /// </summary>
        /// <param name="sender"></param>
        private void tabIssue_ActivateConnectionRequest(XtraUserControlIssue sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueC`hanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnActivateConnectionRequest(tabIssue_ActivateConnectionRequest),
                           sender);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ReactivateConnection();
            }
        }

        /// <summary>
        /// Notifies the form when updating test results for an issue
        /// </summary>
        /// <param name="sender"></param>
        private void tabIssue_UpdateTestResult(XtraUserControlIssue sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueC`hanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnUpdateOnTestResults(tabIssue_UpdateTestResult), sender);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetResultsViewDataSource();
            }
        }

        /// <summary>
        /// Handle setting the image area ignore state
        /// </summary>
        /// <param name="ignoreState"></param>
        private void tabIssue_SetImageIgnoreState(bool ignoreState)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueC`hanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnSetImageIgnoreState(tabIssue_SetImageIgnoreState), ignoreState);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlItemDetails.IgnoreImageRefresh = ignoreState;
            }
        }

        /// <summary>
        /// Handle details and image area refresh
        /// </summary>
        /// <param name="sender"></param>
        private void tabIssue_RefreshDetailsAndImageIssue(XtraUserControlItemsNavGrid sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueC`hanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new XtraUserControlIssue.OnRefreshDetailsAndImageIssue(tabIssue_RefreshDetailsAndImageIssue),
                        sender);

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

                //    if (sender.ItemsWithImagesExist())
                //    {

                //    }
                //    else
                //    {
                //        SetDefaultUiDetails();
                //    }
                //}
            }
        }

        /// <summary>
        /// Notifies the form when opening a dialog with a meter for an issue
        /// </summary>
        /// <param name="hidePanel"></param>
        private void tabIssue_MeterDialogOpen(bool showOverlay)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueC`hanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnMeterDialogOpen(tabIssue_MeterDialogOpen), showOverlay);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowHideOverlay(showOverlay);
            }
        }

        /// <summary>
        /// Handel the SelectedItemChanged requests from the xtraUserControlItemsNavGridMajorIssues.
        /// </summary>
        private void xtraUserControlItemsNavGridMajorIssues_SelectedItemChanged(XtraUserControlItemsNavGrid sender,
                                                                                Item item, int reading)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnSelectedItemChanged(
                            xtraUserControlItemsNavGridMajorIssues_SelectedItemChanged), sender, item, reading);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _shouldTheAddMajorIssueEnabledOnEdit = sender.SelectedRowCount == 1;
                simpleButtonAddMajorIssue.Enabled = _shouldTheAddMajorIssueEnabledOnEdit && IsInEditMode;

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

        /// <summary>
        /// Handel the CancelReadingRequest requests from the xtraUserControlItemsNavGridMajorIssues.
        /// </summary>
        private void xtraUserControlItemsNavGridMajorIssues_CancelReadingRequest(XtraUserControlItemsNavGrid sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnCancelReadingRequest(
                            xtraUserControlItemsNavGridMajorIssues_CancelReadingRequest), sender);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null || _currentControlItemsNavGridRequestor == null) 
                    return;

                StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);
            }

        }

        /// <summary>
        /// Handel the adding for the test result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="itemsToAdd"></param>
        /// <returns>The inserted items.</returns>
        private List<Item> xtraUserControlItemsNavGridMajorIssues_AddToTestResults(XtraUserControlItemsNavGrid sender,
                                                                                   BindingList<Item> itemsToAdd)
        {
            if (itemsToAdd.Count > 1)
                return TestIssuesItems;

            return AddToMajorIssues(itemsToAdd.FirstOrDefault());
        }
        /// <summary>
        /// Handel the FocusedRowChanged event on the gridViewMajorIssues to change the image, desc' & reading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewMajorIssues_FocusedRowChanged(object sender,
                                                           DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(
                            gridViewMajorIssues_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetUiItemDetailsMajorIssues();
            }
        }
        /// <summary>
        /// Handel the focus event on the gridViewMajorIssues to change the image, desc' & reading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewMajorIssues_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewMajorIssues_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetUiItemDetailsMajorIssues();
            }
        }

        /// <summary>
        /// Handel the SelectedItemChanged that coming from xtraUserControlItemsNavGridMajorIssues.
        /// </summary>
        private void xtraUserControlIssue_SelectedItemChanged(XtraUserControlIssue sender, bool forTestResults, Item item, int reading)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnSelectedItemChanged(xtraUserControlIssue_SelectedItemChanged),
                           sender, item, reading);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (forTestResults)
                {
                    _shouldTheAddMajorIssueEnabledOnEdit = sender.ControlItemsNavGrid.SelectedRowCount == 1;
                    simpleButtonAddMajorIssue.Enabled = _shouldTheAddMajorIssueEnabledOnEdit && IsInEditMode;

                    SetDetails(sender.GetSelectedTestResultItems(),
                                    null,
                                    null,
                                    false,
                                    null,
                                    sender.ControlItemsNavGrid.CurrentIssue == null ? null : sender.ControlItemsNavGrid.CurrentIssueItem);

                    UpdateReading(null, reading);
                }
                else
                {
                    xtraUserControlItemsNavGridMajorIssues_SelectedItemChanged(sender.ControlItemsNavGrid, item, reading);
                }
            }
        }

        /// <summary>
        /// Handel the CancelReadingRequest that coming from xtraUserControlItemsNavGridMajorIssues.
        /// </summary>
        private void xtraUserControlIssue_CancelReadingRequest(XtraUserControlIssue sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnCancelReadingRequest(xtraUserControlIssue_CancelReadingRequest),
                           sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlItemsNavGridMajorIssues_CancelReadingRequest(sender.ControlItemsNavGrid);
            }
        }

        /// <summary>
        /// Handel the SelectedItemChanged that coming from xtraUserControlItemsNavGridMajorIssues.
        /// </summary>
        private void xtraUserControlIssue_ReadingRequest(XtraUserControlIssue sender, List<Item> itemsToBroadcast)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlIssue.OnReadingRequest(xtraUserControlIssue_ReadingRequest), sender,
                           itemsToBroadcast);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                xtraUserControlItemsNavGridMajorIssues_ReadingRequest(sender.ControlItemsNavGrid, itemsToBroadcast);
            }
        }

        /// <summary>
        /// Handles event of loading products screen in the last issue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAddProducts_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonAddProducts_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                AddProducts();
            }
        }

        /// <summary>
        /// handles balancing request
        /// </summary>
        private void tabIssue_BalancingRequest()
        {
            EnableBalancingMode();
        }
        
        /// <summary>
        /// Handle double click to open issue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewMajorIssues_DoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewMajorIssues_DoubleClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == gridViewMajorIssues && UiHelperClass.IsClickInRowByMouse(gridViewMajorIssues))
                {
                    var testIssue = gridViewMajorIssues.GetRow(gridViewMajorIssues.FocusedRowHandle) as TestIssue;

                    if (testIssue != null)
                    {
                        OpenMajorIssue(testIssue.Item);
                    }
                }
            }
        }
        #endregion

        #region Test Schedules Handlers

        /// <summary>
        /// Handles the click on the schedule lines context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        private void contextMenuStripScheduleLines_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStripScheduleLines_ItemClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                ((ContextMenuStrip) sender).Hide();

                var view = xtraTabControlTestSchedule.SelectedTabPage == xtraTabPageTestSchedule
                    ? gridViewScheduleLines
                    : gridViewInvoicing;

                var selectedItems = new List<ScheduleLine>();

                foreach (var row in view.GetSelectedRows())
                {
                    var schedule = view.GetRow(row) as ScheduleLine;
                    if (schedule == null) continue;
                    selectedItems.Add(schedule);
                }
 
                if (e.ClickedItem == toolStripMenuItemDeleteScheduleLine)
                {
                    foreach (var row in selectedItems)
                    {
                        SetScheduleLineDeletedStatus(row, true);
                    }
                }
                else if (e.ClickedItem == openToolStripMenuItemOpenScheduleLine)
                {
                    OpenScheduleLine(view.FocusedRowHandle);
                }
                else if(e.ClickedItem == toolStripMenuItemRestore)
                {
                    foreach (var row in selectedItems)
                    {
                        SetScheduleLineDeletedStatus(row, false);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the opening of the schedule lines context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuStripScheduleLines_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripScheduleLines_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                if (sender != contextMenuStripScheduleLines) return;
                var view = xtraTabControlTestSchedule.SelectedTabPage == xtraTabPageTestSchedule
                    ? gridViewScheduleLines
                    : gridViewInvoicing;

                e.Cancel = UiHelperClass.CancelClickAction(view);

                var isEnabled = UiHelperClass.IsClickInRow(view) && IsInEditMode;

                contextMenuStripScheduleLines.Enabled = isEnabled;
                openToolStripMenuItemOpenScheduleLine.Enabled = isEnabled && view.SelectedRowsCount == 1;

                var numberOfDeletedSchedule = 0;

                foreach (var row in view.GetSelectedRows())
                {
                    var schedule = gridViewScheduleLines.GetRow(row) as ScheduleLine;

                    if (schedule == null)
                        continue;

                    if (schedule.IsDeleted)
                        numberOfDeletedSchedule++;
                }


                toolStripMenuItemRestore.Visible = numberOfDeletedSchedule == gridViewScheduleLines.SelectedRowsCount;
                toolStripMenuItemDeleteScheduleLine.Visible = openToolStripMenuItemOpenScheduleLine.Visible = numberOfDeletedSchedule != gridViewScheduleLines.SelectedRowsCount;
                toolStripMenuItemDeleteScheduleLine.Enabled = numberOfDeletedSchedule == 0;

            }
        }

        /// <summary>
        /// Handel the focus on the re-evaluation spin edit to show the hint.
        /// </summary>
        private void spinEditReevaluationDate_Enter(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(spinEditReevaluationDate_Enter), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if(!IsInEditMode)
                    return;

                ShowHint(spinEditReevaluationDate, StaticKeys.HintTitleReEvaluation, StaticKeys.HintTextReEvaluation,
                         ToolTipLocation.RightCenter, true, 5000);
            }
        }

        /// <summary>
        /// Handel the clicking on reevaluate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinEditReevaluationDate_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ButtonPressedEventHandler(spinEditReevaluationDate_ButtonClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Button.Tag.Equals("reevaluate"))
                {
                    ShowReEvaluationnDialog();
                }
            }
        }

        /// <summary>
        /// Handle the showing for the editor to prevent editing on the deleted schedule lines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewScheduleLines_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(gridViewScheduleLines_ShowingEditor), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var view = sender as GridView;

                var schedule = view.GetFocusedRow() as ScheduleLine;

                if (schedule == null)
                    return;

                if (schedule.IsDeleted)
                    e.Cancel = true; 

            }
        }

        /// <summary>
        /// Handel check button of filter deleted schedule.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkButtonShowDeletedSchedules_CheckedChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkButtonShowDeletedSchedules_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UpdateScheduleGridFilters(checkEditShowDeletedScheduleLines.Checked);
            }
        }

        /// <summary>
        /// Handel row styling event to change the style of deleted schedule.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewScheduleLines_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowStyleEventHandler(gridViewScheduleLines_RowStyle), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                var view = sender as GridView;
                var currentRow = view.GetRow(e.RowHandle);

                if(currentRow == null)
                    return;

                var schedule = currentRow as ScheduleLine;

                if (schedule == null)
                    return;

                if (!schedule.IsDeleted)
                    return;

                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.BackColor2 = Color.WhiteSmoke;
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size + 1, FontStyle.Strikeout);

            }
        }
        
        /// <summary>
        /// Handles printing of test schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintPatientSchedule_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintPatientSchedule_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (TestObject.TestSchedule != null && !TestObject.EvalPeriodChecked)
                {
                    if (UiHelperClass.ShowConfirmQuestion(
                        "Would you like to fill the field 'Re-Evaluation In Weeks' before printing") 
                        == DialogResult.Yes)
                    {
                        ShowReEvaluationnDialog();
                    }
                }
                UpdateTestStatusToDone();
                SaveForNavigation();
                ShowTestScheduleReport();
            }
        }

        /// <summary>
        /// Handles the click on the balance mode.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonBalance_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonBalance_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                EnableBalancingMode();
            }
        }

        /// <summary>
        /// Handles the change of the selected index.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void radioGroupEdsMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(radioGroupEdsMode_SelectedIndexChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _isInBalancingMode = radioGroupEdsMode.SelectedIndex == 1;

                if (_isInBalancingMode)
                    StartBalancingWithStartIndex();
                    
            }
        }

        /// <summary>
        /// Handel check payment selection.
        /// </summary>
        private void checkEditIsCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkEditIsCheck_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                checkEditIsCheck.CheckStateChanged -= checkEditIsCheck_CheckedChanged;

                if (!checkEditIsCheck.Checked && TestObject.TestSchedule != null)
                {
                    TestObject.TestSchedule.IsCheck = false;
                    TestObject.TestSchedule.CheckNumber = string.Empty;
                }

                UpdateCheckNumberVisibility();

                checkEditIsCheck.CheckStateChanged += checkEditIsCheck_CheckedChanged;
            }
        }

        #endregion
        
        #region Test Result Handlers

        /// <summary>
        /// Enables or disables the Auto Play button when changing focused tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControlItemTestingTabs_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangedEventHandler(xtraTabControlItemTestingTabs_SelectedPageChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ProcessSelectedItemTestingTabPageChenaged();
            }

        }

        /// <summary>
        /// Process selected tab change actions on the ItemTesting tabs.
        /// </summary>
        private void ProcessSelectedItemTestingTabPageChenaged()
        {
            if (_isRebinding) return;

            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule)
            {
                RefreshPatientScuedule();
            }

            if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageImprinting)
            {
                BindTestImprintableItems();
                if (!_imprintingTreeExpanded)
                {
                    _imprintingTreeExpanded = true;
                    treeListImprintableItems.ForceInitialize();
                    treeListImprintableItems.ExpandAll();
                }

                treeListImprintableItems.RefreshDataSource();
                treeListImprintableItems.Refresh();
            }

            UpdateItemDetailsGroupExpandedStatus();
            ControlItemDetailesAndReadingsDependOnUserAction(false);
        }

        /// <summary>
        /// handle the showing for the item details group to keep revert the group for its old status after it hided automatically.
        /// </summary>
        private void layoutControlInfo_GroupExpandChanged(object sender, LayoutGroupEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new LayoutGroupEventHandler(layoutControlInfo_GroupExpandChanged), sender, e);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Group == layoutControlGroupReadingAndImage)
                {
                    _shouldDetailsGroupExpended = e.Group.Expanded;
                }
            }
        }

        /// <summary>
        /// Handles the hiding of the expand button when not needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewTestResults_CustomDrawCell(object sender,
                                                        DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(
                            gridViewTestResults_CustomDrawCell), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.HandleViewDetailExpandButton(sender as GridView, e);
            }
        }

        #endregion

        #region Test Services Handlers

        /// <summary>
        /// Handles the initialization of a new test service row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewServices_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new InitNewRowEventHandler(gridViewServices_InitNewRow), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var newServiceRow = gridViewServices.GetFocusedRow() as TestService;
                var onFlyServiceLookup = UiHelperClass.GetSingleLookupFromCache(new LookupsFilter { Type = Enum.GetName(typeof(LookupTypes), LookupTypes.ServiceType), Value = "On Fly Service" });

                if (newServiceRow != null)
                {
                    newServiceRow.TypeLookup = new Lookup() { Id = onFlyServiceLookup.Id };
                    newServiceRow.Key = EnumNameResolver.Resolve(ServiceType.OnFlyService, true);
                    newServiceRow.Test = TestObject;
                }
            }
        }

        /// <summary>
        /// Handles clicking add service button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAddService_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonAddService_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (lookUpEditServices.EditValue == null)
                {
                    UiHelperClass.ShowInformation("Please select a service to add", "No Service Selected");
                    return;
                }

                var serviceToAdd = lookUpEditServices.GetSelectedDataRow() as Service;

                if (serviceToAdd != null && TestObject.TestServices.Any(ts => ts.Service != null && ts.Service.Id == serviceToAdd.Id))
                {
                    UiHelperClass.ShowInformation("The selected service is already added.", "Service Exists");
                    return;
                }

                AddService(serviceToAdd);
            }            
        }

        /// <summary>
        /// Handles test services list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestServices_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(TestServices_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _servicesChanged = true;             
            }            
        }

        /// <summary>
        /// Handles services cell value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewServices_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CellValueChangedEventHandler(gridViewServices_CellValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Column == gridColumnServicePrice)
                {
                    decimal newValue = 0;
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out newValue);

                    if (newValue < 0 || newValue > 9999)
                    {
                        return;
                    }

                    var view = (GridView)sender;
                    view.SetFocusedRowCellValue(e.Column, e.Value);
                    view.PostEditor();
                    view.UpdateCurrentRow();

                    //ATTENTION: THE UpdateTotalSummary SEEMS TO CRASH WHEN GRID IS NOT BOUNDED YET, EVEN IF SETBINDING IS CALLED, IT WON'T BIND UNTIL THE GRID IS VISIBLE...MAKE SURE SUMMARY UPDATE OCCURS ONLY WHEN FORM IS ACTIVE
                    view.UpdateTotalSummary();
                }
            }
        }

        /// <summary>
        /// Handles custom summary calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewServices_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CustomSummaryEventHandler(gridViewServices_CustomSummaryCalculate), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    var item = e.Item as GridColumnSummaryItem;

                    if (item == null || item.Tag == null) return;

                    switch (item.Tag.ToString())
                    {
                        case "ServicesSubtotal":
                            e.TotalValue = TestObject.ServicesSubtotal;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handle cases or larger numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemSpinEditServicePrice_EditValueChanging(object sender, ChangingEventArgs e)
        {
            decimal newValue = 0;
            decimal.TryParse(e.NewValue == null ? "0" : e.NewValue.ToString(), out newValue);

            if (newValue < 0 || newValue > 9999)
            {
                e.Cancel = true;
                return;
            }
        }

        #endregion

        #region Shipping Orders Handlers

        /// <summary>
        /// Handles test shipping orders list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingOrders_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(ShippingOrders_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _shippingOrdersChanged = true;
            }
        }

        /// <summary>
        /// Handles shipping record double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewShippingOrders_DoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewShippingOrders_DoubleClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == gridViewShippingOrders && UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders))
                {
                    OpenShippingOrder(false);
                }
            }
        }

        /// <summary>
        /// Handles coloring of rows that are sent and rows that are not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewShippingOrders_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            UiHelperClass.HandleOrderColor(sender, e);
        }

        #endregion

        #region Invoicing

        /// <summary>
        /// Handles custom summary calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewInvoicing_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CustomSummaryEventHandler(gridViewInvoicing_CustomSummaryCalculate), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    var item = e.Item as GridColumnSummaryItem;
                    
                    if (item == null || item.Tag == null) return;                   

                    switch (item.Tag.ToString())
                    {
                        case "ProductsSubtotal":
                            e.TotalValue = TestObject.ProductsSubtotal;
                            break;                        
                    }
                }
            }            
        }

        /// <summary>
        /// Handles clicking the Adjustment spin edit type button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinEditAdjustment_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ButtonPressedEventHandler(spinEditAdjustment_ButtonClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetAdjustmentTypeAndValue(!TestObject.TestSchedule.DiscountAsPercentage);
            }            
        }

        /// <summary>
        /// Handles upading invoicing calcualtion based on tab selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControlTestSchedule_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangedEventHandler(xtraTabControlTestSchedule_SelectedPageChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Page == xtraTabPageInvoice)
                {
                    ShowDeletedProductsLayoutControlItem.Visibility = LayoutVisibility.Always;
                    UpdateTotalSummary();
                }

                ShowDeletedProductsLayoutControlItem.Visibility = e.Page == xtraTabPageTestSchedule ||
                                                                  e.Page == xtraTabPageInvoice
                                                                      ? LayoutVisibility.Always
                                                                      : LayoutVisibility.Never;
            }
        }       

        /// <summary>
        /// Handles cell value changing and posting value directly to the datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewInvoicing_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CellValueChangedEventHandler(gridViewInvoicing_CellValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Column == gridColumnUnitPrice || e.Column == gridColumnQuantity)
                {
                    decimal newValue = 0;
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out newValue);

                    if (newValue < 0 || newValue > 9999)
                    {
                        return;
                    }

                    var view = (GridView)sender;
                    view.SetFocusedRowCellValue(e.Column, e.Value);
                    view.PostEditor();
                    view.UpdateCurrentRow();
                    UpdateTotalSummary();

                    //ATTENTION: THE UpdateTotalSummary SEEMS TO CRASH WHEN GRID IS NOT BOUNDED YET, EVEN IF SETBINDING IS CALLED, IT WON'T BIND UNTIL THE GRID IS VISIBLE...MAKE SURE SUMMARY UPDATE OCCURS ONLY WHEN FORM IS ACTIVE
                    gridViewInvoicing.UpdateTotalSummary();
                }
            }
        }

        /// <summary>
        /// Handles changes in the Adjustment spin edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinEditAdjustment_EditValueChanging(object sender, ChangingEventArgs e)
        {
            decimal oldValue = 0;
            decimal newValue = 0;
            decimal.TryParse(e.OldValue == null ? "0" : e.OldValue.ToString(), out oldValue);
            decimal.TryParse(e.NewValue == null ? "0" : e.NewValue.ToString(), out newValue);

            if ((TestObject.TestSchedule.DiscountAsPercentage && (newValue < -100 || newValue > 100)) ||
                (!TestObject.TestSchedule.DiscountAsPercentage && (newValue < -9999 || newValue > 9999)))
            {
                e.Cancel = true;
                return;
            }

            spinEditAdjustment.DoValidate();
            TestObject.TestSchedule.Discount = newValue;
            UpdateTotalSummary();
        }

        /// <summary>
        /// Handles changes in the tax spin edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinEditTax_EditValueChanging(object sender, ChangingEventArgs e)
        {
            decimal oldValue = 0;
            decimal newValue = 0;
            decimal.TryParse(e.OldValue == null ? "0" : e.OldValue.ToString(), out oldValue);
            decimal.TryParse(e.NewValue == null ? "0" : e.NewValue.ToString(), out newValue);

            if (newValue < 0 || newValue > 100)
            {
                e.Cancel = true;
                return;
            }

            spinEditTax.DoValidate();
            TestObject.TestSchedule.Tax = newValue;
            UpdateTotalSummary();
        }

        /// <summary>
        /// Handles changing the Adjustment applying dropdown value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditAdjustmentApply_EditValueChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(lookUpEditAdjustmentApply_EditValueChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                lookUpEditAdjustmentApply.PostLookupValues(TestObject.TestSchedule.DiscountApply);
            }
        }

        /// <summary>
        /// Handle cases or larger numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemSpinEditQuantity_EditValueChanging(object sender, ChangingEventArgs e)
        {
            decimal newValue = 0;
            decimal.TryParse(e.NewValue == null ? "0" : e.NewValue.ToString(), out newValue);

            if (newValue < 0 || newValue > 9999)
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// Handle cases or larger numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemSpinEditUnitPrice_EditValueChanging(object sender, ChangingEventArgs e)
        {
            decimal newValue = 0;
            decimal.TryParse(e.NewValue == null ? "0" : e.NewValue.ToString(), out newValue);

            if (newValue < 0 || newValue > 9999)
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// Handles sending a shipping order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSendShippingOrder_Click(object sender, EventArgs e)
        {
            if (TestObject.ShippingOrders.Any(so => !so.Sent))
            {
                UiHelperClass.ShowInformation("Only one offline order can exist in a test, please modify existing order.","Offline Orders Exists");
                return;
            }
        
            OpenShippingOrder(true);
        }

        #endregion        

        #region Test Imprintable Items

        #region General

        /// <summary>
        /// Handles test imprintable items list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestImprintableItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(TestImprintableItems_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _testImprintableItemsChanged = true;

                if (_isImprinting)
                {
                    return;
                }

                simpleButtonImprintCheckAll.Enabled = IsInEditMode && TestObject.TestImprintableItems.Any();
                simpleButtonImprintUncheckAll.Enabled = IsInEditMode && TestObject.TestImprintableItems.Any();
                EnableDisableImprintingOrderButtons();
            }
        }

        /// <summary>
        /// Handles changing selected imprinting tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabbedControlGroupImprinting_SelectedPageChanging(object sender, DevExpress.XtraLayout.LayoutTabPageChangingEventArgs e)
        {
            if (_isImprinting)
            {
                e.Cancel = true;
                tabbedControlGroupImprinting.SelectedTabPage = e.PrevPage;
            }
        }

        #endregion

        #region Context Menu

        /// <summary>
        /// Handle imprinting context menu opening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripImprinting_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripImprinting_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                if (sender != contextMenuStripImprinting) return;

                var clickInCell =
                    treeListImprintableItems.CalcHitInfo(treeListImprintableItems.PointToClient(Cursor.Position))
                        .HitInfoType == HitInfoType.Cell;

                var isEnabled = clickInCell && IsInEditMode && !_isImprinting;

                toolStripMenuItemAddImprintableItem.Enabled = IsInEditMode && !_isImprinting;
                toolStripMenuItemImprint.Enabled = IsInEditMode && !_isImprinting;

                toolStripMenuItemDeleteImprintableItem.Enabled = isEnabled;
                toolStripMenuItemUnimprint.Enabled = isEnabled;
                toolStripMenuItemDeleteAndNotImprintableItem.Enabled = isEnabled;

                var enableSubItemsOptions = SubItemsOptionsEnabled();
                var enableMoveToRoot = false;

                foreach (TreeListNode node in treeListImprintableItems.Selection)
                {
                    var imprintableItem = treeListImprintableItems.GetDataRecordByNode(node) as TestImprintableItem;
                    if (imprintableItem != null && !imprintableItem.IsRootItem())
                    {
                        enableMoveToRoot = true;
                    }
                }

                toolStripMenuItemCheckSubItems.Enabled = isEnabled && enableSubItemsOptions;
                toolStripMenuItemUnCheckSubItems.Enabled = isEnabled && enableSubItemsOptions;
                toolStripMenuItemMoveToRoot.Enabled = isEnabled && enableMoveToRoot;
            }
        }

        /// <summary>
        /// Handle imprinting context menu item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripImprinting_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStripImprinting_ItemClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                ((ContextMenuStrip)sender).Hide();

                if (e.ClickedItem == toolStripMenuItemDeleteImprintableItem)
                {
                    DeleteImprintableItems(false);
                }
                else if (e.ClickedItem == toolStripMenuItemDeleteAndNotImprintableItem)
                {
                    DeleteImprintableItems(true);
                }
                else if (e.ClickedItem == toolStripMenuItemAddImprintableItem)
                {
                    AddImprintableItem();
                }
                else if (e.ClickedItem == toolStripMenuItemImprint)
                {
                    ToggleImprintinState();
                }
                else if (e.ClickedItem == toolStripMenuItemUnimprint)
                {
                    UnimprintItems();
                }
                else if (e.ClickedItem == toolStripMenuItemCheckSubItems)
                {
                    UpdateSubItemsCheck(true);
                }
                else if (e.ClickedItem == toolStripMenuItemUnCheckSubItems)
                {
                    UpdateSubItemsCheck(false);
                }
                else if (e.ClickedItem == toolStripMenuItemMoveToRoot)
                {
                    MoveImprintableItemsToRoot();
                }
            }
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Handle adding a new imprintable item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAddImprintableItem_Click(object sender, EventArgs e)
        {
            AddImprintableItem();
        }

        /// <summary>
        /// Expand all nodes in tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprintExpandAll_Click(object sender, EventArgs e)
        {
            treeListImprintableItems.ExpandAll();
        }

        /// <summary>
        /// Collapse all nodes in tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprintCollapseAll_Click(object sender, EventArgs e)
        {
            treeListImprintableItems.CollapseAll();
        }

        /// <summary>
        /// Check all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprintCheckAll_Click(object sender, EventArgs e)
        {
            UpdateImprintableItemsCheck(true);
        }

        /// <summary>
        /// Uncheck all items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprintUncheckAll_Click(object sender, EventArgs e)
        {
            UpdateImprintableItemsCheck(false);
        }

        /// <summary>
        /// Moves item up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprintMoveUp_Click(object sender, EventArgs e)
        {
            SwitchOrder(true);
        }

        /// <summary>
        /// Moves item down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprintMoveDown_Click(object sender, EventArgs e)
        {
            SwitchOrder(false);
        }

        #endregion

        #region Tree

        /// <summary>
        /// Apply Drop action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_DragDrop(object sender, DragEventArgs e)
        {
            if (!IsInEditMode)
            {
                _currentDragAction = TreeDragAction.None;
                e.Effect = DragDropEffects.None;
                return;
            }
            var point = treeListImprintableItems.PointToClient(new Point(e.X, e.Y));

            var targetNode = treeListImprintableItems.CalcHitInfo(point).Node;
            var targetItem = treeListImprintableItems.GetDataRecordByNode(targetNode) as TestImprintableItem;

            var dragNodes = treeListImprintableItems.Selection.Cast<TreeListNode>().ToList();

            var dragItems = _currentDragAction == TreeDragAction.DragDown? 
                            treeListImprintableItems.Selection.Cast<TreeListNode>().ToList().
                            Select(dragNode => treeListImprintableItems.GetDataRecordByNode(dragNode) as TestImprintableItem).
                            OrderByDescending(t=>t.Order).ToList() :
                            treeListImprintableItems.Selection.Cast<TreeListNode>().ToList().
                            Select(dragNode => treeListImprintableItems.GetDataRecordByNode(dragNode) as TestImprintableItem).
                            OrderBy(t=>t.Order).ToList();

            UiHelperClass.ShowWaitingPanel("Applying drop action ...");

            foreach (TestImprintableItem dragItem in dragItems)
            {
                switch (_currentDragAction)
                {
                    case TreeDragAction.Root:
                        if (dragItem != null)
                        {
                            if (!dragItem.IsRootItem())
                            {
                                var oldParent = dragItem.Parent;
                                dragItem.Order = TestObject.ImprintingRootLastOrder + 1;
                                dragItem.Parent = null;
                                TestObject.ResetParentSubItemsOrder(oldParent);
                                TestObject.ResetRootOrder();
                            }
                        }
                        break;
                    case TreeDragAction.Move:

                        if (dragItem != null && targetItem != null)
                        {
                            if (targetItem.IsRootItem() || targetItem.ParentId != dragItem.TempId)
                            {
                                if (dragItem.IsRootItem())
                                {
                                    dragItem.Order = TestObject.GetParentLastOrder(targetItem) + 1;
                                    dragItem.Parent = targetItem;
                                    TestObject.ResetParentSubItemsOrder(dragItem.Parent);
                                    TestObject.ResetRootOrder();
                                }
                                else
                                {
                                    var oldParent = dragItem.Parent;
                                    dragItem.Order = TestObject.GetParentLastOrder(targetItem) + 1;
                                    dragItem.Parent = targetItem;
                                    TestObject.ResetParentSubItemsOrder(oldParent);
                                    TestObject.ResetRootOrder();
                                }

                                TestObject.ResetParentSubItemsOrder(targetItem);
                            }
                        }

                        break;
                    case TreeDragAction.DragUp:
                        TestObject.MoveItemRelativeToOtherItem(dragItem,targetItem,false);
                        treeListImprintableItems.RefreshDataSource();
                        treeListImprintableItems.Refresh();
                        break;
                    case TreeDragAction.DragDown:
                        TestObject.MoveItemRelativeToOtherItem(dragItem, targetItem, true);
                        treeListImprintableItems.RefreshDataSource();
                        treeListImprintableItems.Refresh();
                        break;
                    case TreeDragAction.None:
                        break;
                }
            }

            UiHelperClass.HideSplash();

            e.Effect = DragDropEffects.None;
            treeListImprintableItems.RefreshDataSource();
            treeListImprintableItems.Refresh();
        }

        /// <summary>
        /// Handle dragging behavior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_DragOver(object sender, DragEventArgs e)
        {
            if (!IsInEditMode)
            {
                _currentDragAction = TreeDragAction.None;
                e.Effect = DragDropEffects.None;
                return;
            }
            var p = treeListImprintableItems.PointToClient(MousePosition);
            var targetNode = treeListImprintableItems.CalcHitInfo(p).Node;
            DragDropEffects effect;

            if (ModifierKeys == Keys.Alt)
            {
                if (treeListImprintableItems.Selection.Contains(targetNode))
                {
                    _currentDragAction = TreeDragAction.None;
                    effect = DragDropEffects.None;
                }
                else
                {
                    _currentDragAction = p.Y < _lowerNodeY ? TreeDragAction.DragUp : TreeDragAction.DragDown;
                    effect = DragDropEffects.Move;
                }
            }
            else if ((treeListImprintableItems.CalcHitInfo(p).HitInfoType == HitInfoType.Cell ||
                      treeListImprintableItems.CalcHitInfo(p).HitInfoType == HitInfoType.Row) &&
                     !treeListImprintableItems.Selection.Contains(targetNode))
            {
                _currentDragAction = TreeDragAction.Move;
                effect = DragDropEffects.Link;
            }
            else if (treeListImprintableItems.CalcHitInfo(p).HitInfoType == HitInfoType.Empty ||
                     treeListImprintableItems.CalcHitInfo(p).HitInfoType == HitInfoType.None)
            {
                _currentDragAction = TreeDragAction.Root;
                effect = DragDropEffects.Copy;
            }
            else
            {
                _currentDragAction = TreeDragAction.None;
                effect = DragDropEffects.None;
            }
            e.Effect = effect;
            if (_treeMouseDown)
            {
                treeListImprintableItems_CalcNodeDragImageIndex(
                treeListImprintableItems,
                    new CalcNodeDragImageIndexEventArgs
                                        (treeListImprintableItems.FocusedNode,
                                        (int)_currentDragAction,
                                        treeListImprintableItems.CalcHitInfo(p).MousePoint,
                            new DragEventArgs(new DataObject(treeListImprintableItems.FocusedNode), 0,
                                                             treeListImprintableItems.CalcHitInfo(p).MousePoint.X,
                                                             treeListImprintableItems.CalcHitInfo(p).MousePoint.Y,
                                                             effect,
                                                             effect)));
            }
        }

        /// <summary>
        /// Change drag icon in the tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_CalcNodeDragImageIndex(object sender, CalcNodeDragImageIndexEventArgs e)
        {
            e.ImageIndex = (int)_currentDragAction;
            treeListImprintableItems.Refresh(); //VERY IMPORTANT OR THE ICONS WON'T SHOW UP
        }

        /// <summary>
        /// Handle mouse down event for tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isImprinting) return;
            var tree = sender as TreeList;
            var hitInfo = tree.CalcHitInfo(e.Location);

            if (e.Button == MouseButtons.Right &&
                (hitInfo.HitInfoType == HitInfoType.Cell || hitInfo.HitInfoType == HitInfoType.Row))
            {
                if ((tree.Selection.Count == 0 || tree.Selection.Count == 1) || (tree.Selection.Count > 1 && !tree.Selection.Contains(hitInfo.Node)))
                {
                    tree.Selection.Clear();
                    treeListImprintableItems.FocusedNode = hitInfo.Node;
                    hitInfo.Node.Selected = true;
                }
            }

            _treeMouseDown = true;
            Point p = treeListImprintableItems.PointToClient(MousePosition);
            _lowerNodeY = treeListImprintableItems.CalcHitInfo(p).Bounds.Y;
        }

        /// <summary>
        /// Clears the mouse down flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isImprinting) return;
            //THIS IS THE OLD CODE WHERE DRAG UP AND DOWN ICONS SHOWED UP CORRECTLY BUT WITH THE PROBLEM OF DRAGGING MULTISELECTED ITEMS
            //var tree = sender as TreeList;
            //var hitInfo = tree.CalcHitInfo(e.Location);

            //if (e.Button == MouseButtons.Right && hitInfo.HitInfoType == HitInfoType.Cell)
            //{
            //    if ((tree.Selection.Count == 0 || tree.Selection.Count == 1) || (tree.Selection.Count > 1 && !tree.Selection.Contains(hitInfo.Node)))
            //    {
            //        tree.Selection.Clear();
            //        treeListImprintableItems.FocusedNode = hitInfo.Node;
            //        hitInfo.Node.Selected = true;
            //    }
            //}
            //else if (e.Button == MouseButtons.Left && ModifierKeys != Keys.Shift)
            //{
            //    if (tree.Selection.Count > 1 && tree.Selection.Contains(hitInfo.Node))
            //    {
            //        tree.Selection.Clear();
            //        treeListImprintableItems.FocusedNode = hitInfo.Node;
            //        hitInfo.Node.Selected = true;
            //    }
            //}
            //else if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Shift)
            //{
            //    Point p = treeListImprintableItems.PointToClient(MousePosition);
            //    _lowerNodeY = treeListImprintableItems.CalcHitInfo(p).Bounds.Y;
            //}
            //_treeMouseDown = false;


            _treeMouseDown = false;
        }

        /// <summary>
        /// Handle mouse click on tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isImprinting) return;
            var tree = sender as TreeList;
            var hitInfo = tree.CalcHitInfo(e.Location);

            if (e.Button == MouseButtons.Left && ModifierKeys != Keys.Shift && ModifierKeys != Keys.Control)
            {
                if (tree.Selection.Count > 1 && tree.Selection.Contains(hitInfo.Node))
                {
                    tree.Selection.Clear();
                    treeListImprintableItems.FocusedNode = hitInfo.Node;
                    hitInfo.Node.Selected = true;
                }
            }
        }

        /// <summary>
        /// Handle tree focus node changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (_isImprinting) return;
            EnableDisableImprintingOrderButtons();
        }

        /// <summary>
        /// Handle buttons enable disable based on selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_SelectionChanged(object sender, EventArgs e)
        {
            if (_isImprinting) return;
            EnableDisableImprintingOrderButtons();
        }

        /// <summary>
        /// Handles tree key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isImprinting) return;

            var allowedColumns = treeListImprintableItems.FocusedColumn == treeListColumnImprintCheck ||
                                 treeListImprintableItems.FocusedColumn == treeListColumnIsImprinted ||
                                 treeListImprintableItems.FocusedColumn == treeListColumnItemName;

            if (IsInEditMode && allowedColumns && e.KeyCode == Keys.Space)
            {
                UiHelperClass.ShowWaitingPanel("Updating checking state ...");
                var nodesList = treeListImprintableItems.Selection.Cast<TreeListNode>().ToList();

                foreach (TreeListNode node in nodesList)
                {
                    var imprintableItem = treeListImprintableItems.GetDataRecordByNode(node) as TestImprintableItem;

                    if (imprintableItem != null)
                    {
                        imprintableItem.IsChecked = !imprintableItem.IsChecked;
                    }
                }
                UiHelperClass.HideSplash();
            }
            else if (IsInEditMode && allowedColumns && e.KeyCode == Keys.Delete)
            {
                DeleteImprintableItems(false);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (IsInEditMode && allowedColumns && simpleButtonImprintMoveUp.Enabled)
                {
                    SwitchOrder(true);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (IsInEditMode && allowedColumns && simpleButtonImprintMoveDown.Enabled)
                {
                    SwitchOrder(false);
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handle tree nodes coloring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            var imprintableItem = treeListImprintableItems.GetDataRecordByNode(e.Node) as TestImprintableItem;

            if (imprintableItem != null && imprintableItem.IsImprinted)
            {
                if (!e.Node.Selected)
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                }
                else
                {
                    e.Appearance.BackColor = Color.Green;
                }
            }
        }

        /// <summary>
        /// Handle is imprinted custom text in imprinting tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemTextEditIsImprinted_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (e.Value == null) return;
            e.DisplayText = (bool)e.Value ? "Yes" : "No";
        }

        /// <summary>
        /// Notify form and object when changing the checked property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListImprintableItems_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            var imprintableItem = treeListImprintableItems.GetDataRecordByNode(e.Node) as TestImprintableItem;

            if (imprintableItem == null) return;

            if (e.Column == treeListColumnImprintCheck)
            {
                imprintableItem.IsChecked = !imprintableItem.IsChecked;
                imprintableItem.NotifyPropertyChanged(ExpressionHelper.GetPropertyName(() => imprintableItem.IsChecked));
            }

        }

        #endregion

        #region Imprinting

        /// <summary>
        /// Starts or stops imprinting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonToggleImprinting_Click(object sender, EventArgs e)
        {
            ToggleImprintinState();
        }

        /// <summary>
        /// Handles the tick event of the imprinting timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void timerImprinting_Tick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(timerImprinting_Tick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (checkEditUseImprintingSound.Checked)
                {
                    _imprintingSoundPlayer.Play();
                }
                
                TimerUpdateStateIndicators();
            }
        }

        /// <summary>
        /// Handle logic for time based imprinting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerImprintingCountDown_Tick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(timerImprintingCountDown_Tick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                TimerImprint();
            }
        }

        /// <summary>
        /// Handle printing the imprinting data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintImprinting_Click(object sender, EventArgs e)
        {
            ShowImprintingReport();
        }
        #endregion

        #endregion

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

                    if (!IsImprinting)
                        //This Call for exit from the current broadcasting.
                        OnBroadcastDoneActions();
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

                    if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageEDS)
                    {
                        CheckForAutoTestPlay(ReadingRequesterTypeEnum.Eds);
                        StartReading(ReadingRequesterTypeEnum.Eds, GetItemsToBroadcast());
                    }
                    else if (xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPagePatientSchedule || xtraTabControlItemTestingTabs.SelectedTabPage == xtraTabPageNotes)
                    {
                        StartReading(ReadingRequesterTypeEnum.Others, null);
                    }
                    else if (_currentFoucsControlItemsNavGrid != null)
                    {
                        _currentFoucsControlItemsNavGrid.ResponseReadingValue = _lastItemTestingReadingValue;
                        CheckForAutoTestPlay(ReadingRequesterTypeEnum.ItemTestingIssueNav);
                        _currentFoucsControlItemsNavGrid.StartReading();
                    }

                    _isWaitingCsaRealsedToTakeNewReading = false;
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
                    // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
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
                                IsImprinting = false;
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

        #region EDS Hardware Handlers

        /// <summary>
        /// Handel the hardware reading done event.
        /// </summary>
        private void _csaManager_Eds_ReadingDone(object sender, int reading, int min, int max, int fall, int rise)
        {
            _csaManager_Eds_MeterValueChanged(sender, reading, min, max);

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_Eds_ReadingDone), sender, reading, min, max,
                           fall, rise);

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

                    CsaEmdUnitManager.Instance.StopReading();

                    StopReading(ReadingRequesterTypeEnum.Eds);

                    _isWaitingCsaRealsedToTakeNewReading = true;

                    CurrentReading.Rise = rise;
                    CurrentReading.Fall = fall;

                    SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.WaitingToRelease, 0);
                }
            }
        }

        /// <summary>
        /// Handel the hardware meter value changed.
        /// </summary>
        private void _csaManager_Eds_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_Eds_MeterValueChanged), sender,
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
                    var focusedReading = gridViewItems.GetFocusedRow() as Reading;

                    if (focusedReading != null && (CurrentReading != null && CurrentReading.Id == focusedReading.Id))
                    {

                        if (IsHandleCreated && InvokeRequired)
                        {
                            // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                            try
                            {
                                if (IsDisposed) return;
                                Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_Eds_MeterValueChanged),
                                       sender,
                                       reading, min, max);
                            }
                            catch
                            {
                                // Nothing to do, form had been disposed.
                            }
                        }
                        else
                        {
                            if (!CsaEmdUnitManager.Instance.HasReading || !IsInEditMode) return;

                            if (_isInBalancingMode)
                            {
                                CurrentReading.ValueBalanced = reading;

                            }
                            else
                            {
                                CurrentReading.Value = reading;

                                CurrentReading.ValueBalanced = reading == 50 ? reading : 0;
                            }

                            CurrentReading.Max = max;
                            CurrentReading.Min = min;
                            CurrentReading.Fall = 0;
                            CurrentReading.Rise = 0;

                            if (timerAutoTest.Enabled)
                            {
                                StopAutoTestTimer();
                            }

                            SetReadingIndicators(ReadingPlayTypes.Eds, TestBarStateEnum.Reading, 0);
                            SetReadingMode(true);
                            UpdateImageAndGaugesForReading();
                        }

                    }
                }
            }

        }

        #endregion
        
        #region Item Testing Hardware Handlers

        /// <summary>
        /// Handel the hardware reading done event.
        /// </summary>
        private void _csaManager_ItemTesting_ReadingDone(object sender, int reading, int min, int max, int fall,
                                                         int rise)
        {
            _csaManager_ItemTesting_MeterValueChanged(sender, reading, min, max);

            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_ItemTesting_ReadingDone), sender,
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
                    if (_currentControlItemsNavGridRequestor == null || !CsaEmdUnitManager.Instance.HasReading) return;

                    _isWaitingCsaRealsedToTakeNewReading = true;

                    if (_currentTestPlayState == TestPlayStateEnum.Paused)
                        SetReadingIndicators(ReadingPlayTypes.ItemTesting, TestBarStateEnum.Ready, 0);

                    StopReading(ReadingRequesterTypeEnum.ItemTestingIssueNav);

                    SetReadingMode(false);

                    _lastItemTestingReadingValue = reading;

                    SetReadingIndicators(ReadingPlayTypes.ItemTesting, TestBarStateEnum.WaitingToRelease, 0);
                }

            }

        }

        /// <summary>
        /// Handel the hardware reading done event.
        /// </summary>
        private void _csaManager_ItemTesting_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_ItemTesting_MeterValueChanged),
                           sender,
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

                    if (_currentControlItemsNavGridRequestor != null)
                        SetReadingIndicators(ReadingPlayTypes.ItemTesting, TestBarStateEnum.Reading, 0);
                }
            }

        }

        #endregion

        #region Test Schedule Hardware Handlers

        /// <summary>
        /// Handel the hardware reading done event.
        /// </summary>
        private void _csaManager_Others_ReadingDone(object sender, int reading, int min, int max, int fall,
                                                          int rise)
        {
            _csaManager_Others_MeterValueChanged(sender, reading, min, max);

            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.OnReadingDone(_csaManager_Others_ReadingDone), sender,
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

                    StopReading(ReadingRequesterTypeEnum.Others);

                    SetReadingMode(false);

                    SetReadingIndicators(ReadingPlayTypes.TestSchedule, TestBarStateEnum.WaitingToRelease, 0);
                }

            }

        }

        /// <summary>
        /// Handel the click on simpleButtonPrintInvoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonPrintInvoice_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonPrintInvoice_Click), sender, e);

                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }

            }
            else
            {
                UpdateTestStatusToDone();
                SaveForNavigation();
                ShowProductInvoiceReport();
            }
        }

        /// <summary>
        /// Handel the hardware reading done event.
        /// </summary>
        private void _csaManager_Others_MeterValueChanged(object sender, int reading, int min, int max)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CsaEmdUnitManager.MeterValueChangedHandle(_csaManager_Others_MeterValueChanged),
                           sender,
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

                    SetReadingIndicators(ReadingPlayTypes.TestSchedule, TestBarStateEnum.Reading, 0);
                }
            }

        }

        #endregion

        private void repositoryItemTextEditImprintableItemComments_EditValueChanging(object sender, ChangingEventArgs e)
        {
            TestObject.SetModifiedState(StaticKeys.ImprintableItemsProperty);
            TestObject.NotifyPropertyChanged(StaticKeys.ImprintableItemsProperty);
        }
        
        #endregion
    }
}
