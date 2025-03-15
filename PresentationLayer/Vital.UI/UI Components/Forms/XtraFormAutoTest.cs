using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Data.Filtering;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms.BaseForms;
using Vital.UI.UI_Components.Reports;
using Vital.UI.UI_Components.User_Controls.BioDigital3DModel;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using KeyEventHandler = System.Windows.Forms.KeyEventHandler;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using MouseEventHandler = System.Windows.Forms.MouseEventHandler;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormAutoTest : DevExpress.XtraEditors.XtraForm
    {
        #region Private Members

        #region UI

        /// <summary>
        /// Delegate used to set the caption of the start/stop edit button without blocking current thread
        /// </summary>
        private delegate void StartStopButtonUpdateDelegate();

        /// <summary>
        /// This flag is used to prevent calling the stage UI selection UI multiple times by the same event because the logic itself will updated selected rows causing the same event
        /// to fire before the method itself finishes processing, this doesn't prevent other event types from firing.
        /// </summary>
        private bool _isUpdatingUserStageSelection;

        /// <summary>
        /// This is a very important flag that we use to determine what action the user used to change the focused row in the stages grid because knowing the method used to change
        /// the selected row affects the row we focus when the focus is moving between the detailed views where the exact stage item is not selected by the user and has to be selected
        /// autoamtically by code, using this flag we can correctly determine whether to select the first or the last row and maintain consistency in behavior
        /// </summary>
        private FocusedRowChangeMethod _lastStageRowChangeMethod;

        /// <summary>
        /// Collection Of RepositoryItemLookupEdits for ProductForm in dosage
        /// </summary>
        private IDictionary<int, RepositoryItemLookUpEdit> productFormLookupEdits;

        /// <summary>
        /// Collection Of RepositoryItemLookupEdits for ProductSize in dosage
        /// </summary>
        private IDictionary<int, RepositoryItemLookUpEdit> productSizeLookupEdits;

        private int _hardwareCheckDelay;
        private int _stageAutoItemPostDelay;
        private int _multiLevelStageItemDelay;
        private int _productDosageDelay;
        private int _cSAGeneralCommandDelay;
        private int _readingStabilityDelay;
        private int _mutliLevelStageItemPostDelay;

        /// <summary>
        /// A flag that is used to indicate that a data operation is active by enabling and disabling it each time a manager is called to load/save data
        /// This is a very important flag that prevents threading exceptions caused when aborting the scanning related threads while LLBLGen is doing database
        /// related operations and the thread that calls it gets aborted. The flag prevents the thread from aborting until LLBLGen is done with its logic.
        /// </summary>
        private bool _dataOperationActive;

        /// <summary>
        /// Determines if the 3D anatomy view is enabled or disabled
        /// </summary>
        private bool _useHumanAnatomyView;

        private XtraUserControlBioDigital3DModel _bioDigital3DModel;
        private List<string> _models;

        private Dictionary<string, string> _testingPoints;

        #endregion

        #region Process

        /// <summary>
        /// Main scanning thread
        /// </summary>
        private Thread _scanningThread;

        /// <summary>
        /// Thread used for simulated readings
        /// </summary>
        private Thread _readingThread;

        /// <summary>
        /// Thread to execute hardware validation command separately so it can shut down scanning thread if needed.
        /// </summary>
        private Thread _hardwareValidationThread;

        private Thread _errorThread;

        private Random _randomReadingGenerator;

        #endregion

        #region Data

        private AutoTest _autoTest;
        private AutoTestSourceManager _autoTestSourceManager;
        private AutoTestDestinationManager _autoTestDestinationManager;

        private int _sessionSturctureId;

        #endregion

        #region Hardware

        private AutoCSACommand _lastSentCommand;
        private bool _lastCommandInProgress;
        private AutoCSAResponse _lastCommandResponse;
        private bool _hardwareValidationPerformed;

        /// <summary>
        /// Option to determine if the test should auto resume testing if it was paused because of a failure and then it senses the failure was fixed
        /// </summary>
        private bool _enableAutomationPostFailureAutoResume;

        /// <summary>
        /// Indicates if the testing was paused based on a CSA failure notification and not a response to previously sent check command
        /// </summary>
        private bool _pausedByCSAFailureNotification;

        /// <summary>
        /// The error message of the last CSA failure notification
        /// </summary>
        private string _lastCSANotificationFailure;

        /// <summary>
        /// Flags to determine if the system should perform the different CSA checks based on config key values
        /// </summary>
        private bool _performPressureCheck;
        private bool _performMoistureCheck;
        private bool _performHingeCheck;

        /// <summary>
        /// These are different lock objects used to prevent parts of logic that are heavily executed using threads that could happen repeatidly and
        /// from multiple sources from causing multi-threading exceptions.
        /// </summary>
        private object _responseLock;
        private object _startStopLock;
        private object _hardwareValidationThreadLock;
        private object _scanningThreadLock;

        #endregion

        #endregion

        #region Public Properties

        #region UI

        #region General

        /// <summary>
        /// Indicates if the AutoTest is new
        /// </summary>
        public bool IsNew
        {
            get
            {
                return AutoTest == null || AutoTest.IsNew;
            }
        }

        /// <summary>
        /// Get or set the IsFormLoaded.
        /// </summary>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Get or set IsClosing value.
        /// </summary>
        public bool IsClosing { get; set; }

        /// <summary>
        /// The start/stop button edit
        /// </summary>
        public EditorButton StartStopButton
        {
            get
            {
                return repositoryItemButtonEditStartStop.Buttons[0];
            }
        }

        /// <summary>
        /// The tab group related to the current stage
        /// </summary>
        private LayoutGroup CurrentStageTabGroup
        {
            get
            {
                return AutoTest.StageBookmark.IsEmpty ? null:(LayoutGroup)tabbedControlGroupMain.TabPages.ConvertToTypedList().FirstOrDefault(page => (string)page.Tag == AutoTest.StageBookmark.UIKey);
            }
        }

        /// <summary>
        /// Returns the Testing Tab Results TreeList Control
        /// </summary>
        public TreeList TestingTreeList
        {
            get
            {
                return xtraUserControlAutoTestResultsTesting.TreeList;
            }
        }

        /// <summary>
        /// Returns the Results Tab Results TreeList Control
        /// </summary>
        public TreeList AllResultsTreeList
        {
            get
            {
                return xtraUserControlAutoTestResultsAllResults.TreeList;
            }
        }

        /// <summary>
        /// Return TestingPointIconWidth
        /// </summary>
        private int TestingPointIconWidth
        {
            get
            {
                return Resources.TestingPoint.Size.Width;
            }
        }

        /// <summary>
        /// Icon offset amount
        /// </summary>
        private int TestingPointIconWidthOffset
        {
            get
            {
                return TestingPointIconWidth / 2;
            }
        }

        #endregion

        #endregion

        #region Data

        /// <summary>
        /// Current AutoTest
        /// </summary>
        public AutoTest AutoTest
        {
            get
            {
                return _autoTest;
            }
            set
            {
                _autoTest = value;
            }
        }

        /// <summary>
        /// List of stage revisions ordered
        /// </summary>
        public List<AutoProtocolStageRevision> StageRevisions
        {
            get
            {
                return AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions.OrderBy(sr => sr.Order).ToList();
            }
        }

        #endregion

        #region Hardware

        /// <summary>
        /// Indicates if at least one hardware check is activated
        /// </summary>
        public bool HardwareCheckRequired
        {
            get
            {
                return _performHingeCheck || _performMoistureCheck || _performPressureCheck;
            }
        }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormAutoTest(AutoTest autoTest)
        {
            AutoTest = autoTest;

            //Hide the form during initialization
            Opacity = 0;
            
            InitializeComponent();
            CustomInitializeComponent();
        }

        #endregion

        #region Logic

        #region UI

        #region General

        /// <summary>
        /// Perform UI related initialization actions
        /// </summary>
        private void PerformUiInitializations()
        {
            //Initialize the lock objects used to prevent multi-threading exceptions
            _responseLock = new object();
            _startStopLock = new object();
            _hardwareValidationThreadLock = new object();
            _scanningThreadLock = new object();

            //Link TreeList Controls & Register TreeList events
            TestingTreeList.ColumnsImageList = imageListMain;//Set image list for columns
            TestingTreeList.ContextMenuStrip = contextMenuStripTestingResult;
            TestingTreeList.MenuManager = barManagerMain;
            TestingTreeList.BeforeFocusNode += treeListTestingResults_BeforeFocusNode;
            TestingTreeList.FocusedNodeChanged += treeListTestingResults_FocusedNodeChanged;
            TestingTreeList.GotFocus += treeListTestingResults_GotFocus;
            TestingTreeList.KeyDown += treeListTestingResults_KeyDown;
            TestingTreeList.MouseDown += treeListTestingResults_MouseDown;

            AllResultsTreeList.ColumnsImageList = imageListMain;//Set image list for columns
            AllResultsTreeList.ContextMenuStrip = contextMenuStripResult;
            AllResultsTreeList.MenuManager = barManagerMain;
            AllResultsTreeList.BeforeFocusNode += treeListTestingResults_BeforeFocusNode;
            AllResultsTreeList.FocusedNodeChanged += treeListTestingResults_FocusedNodeChanged;
            AllResultsTreeList.GotFocus += treeListTestingResults_GotFocus;
            AllResultsTreeList.KeyDown += treeListTestingResults_KeyDown;
            AllResultsTreeList.MouseDown += treeListTestingResults_MouseDown;

            //Set initial hand image without a testing point
            pictureEditImage.PerformSafely(() => pictureEditHand.Image = Resources.HandOutline);

            //Hide the tabbed group tab headers in code because we need them visible by default to help us work with the designer during development
            tabbedControlGroupMain.ShowTabHeader = DefaultBoolean.False;

            //The lines below make sure that the gridviews keep their style/appearence of the current focused row even if the grid itself loses focus
            gridViewStages.OptionsSelection.EnableAppearanceHideSelection = false;
            
            //This line is a very special case because the stage items are rows that should be highlighted when they are scanned or manually selected but
            //should be cleared otherwise, this line makes sure we keep their highlight in case the grid is not focused. However to clear their highlight
            //if they are not supposed to be highlighted (Say after the scan finished, we use proeprty "EnableAppearanceFocusedRow" to handle this. This
            //is all happening because of a limitation with gridview where rows cannot be really unselected, we have to play with appearence to make them
            //look unselected.
            gridViewStageAutoItems.OptionsSelection.EnableAppearanceHideSelection = false;

            var currentSkin = GridSkins.GetSkin(TestingTreeList.LookAndFeel);
            //Force showing tree lines even when using skins
            currentSkin.Properties[GridSkins.OptShowTreeLine] = true;
            var highlightColor = CommonSkins.GetSkin(TestingTreeList.LookAndFeel.ActiveLookAndFeel).Colors.GetColor("Highlight");
            TestingTreeList.Appearance.HideSelectionRow.BackColor = highlightColor;//Equivilant to "gridViewTestingResults.OptionsSelection.EnableAppearanceHideSelection = false;"
            AllResultsTreeList.Appearance.HideSelectionRow.BackColor = highlightColor;

            gridViewPreliminarySummary.OptionsSelection.EnableAppearanceHideSelection = false;
            gridViewTestingItems.OptionsSelection.EnableAppearanceHideSelection = false;
            
            gridViewlDosage.OptionsSelection.EnableAppearanceHideSelection = false;

            //IMPORTANT:
            //Lines below are used to allow grids to filter records that are deleted, this is the first time we do this and its because we changed the way
            //we remove records, previously we used to remove the row from its original collection and move it to a deleted rows collection and call save
            //logic on it separately however here we keep the record in its original collection and just mark it as deleted, the grid is designed to filter
            //deleted row so the record will dissapear and the default saving of the test will take care of removing the record from collection and from DB
            //during saving.
            TestingTreeList.ActiveFilterCriteria = new BinaryOperator(ExpressionHelper.GetPropertyName(() => AutoTest.IsDeletedMemory), false.ToString());
            AllResultsTreeList.ActiveFilterCriteria = new BinaryOperator(ExpressionHelper.GetPropertyName(() => AutoTest.IsDeletedMemory), false.ToString());

            //Initialize collection of product form LookupEdits, these are the lookup edits for each product in dosage grid because each product
            //has its own forms and so we can't use one dropdown (LookupEdit) for all rows.
            productFormLookupEdits = new Dictionary<int, RepositoryItemLookUpEdit>();
            productSizeLookupEdits = new Dictionary<int, RepositoryItemLookUpEdit>();

            //Register mouse wheel event to allow capturing mouse wheel movement to close active editor in treelist control
            MouseWheel += OnMouseWheel;
        }

        /// <summary>
        /// Sets the title
        /// </summary>
        public void SetTitle()
        {
            Text = string.IsNullOrEmpty(AutoTest.Name) ? StaticKeys.AutoTestNoTitle : string.Format(StaticKeys.AutoTestTitle, AutoTest.Name);
        }

        /// <summary>
        /// Sets the access rules/permissions on fields, controls and any UI componenets based on the current test status
        /// </summary>
        private void SetAccessRules()
        {
            //Enable/Disable CRUD Buttons Based On Test Status
            EnableDisableCrudButtons();

            //Enable/Disable fields based on current status
            textEditTitle.PerformSafely(() => textEditTitle.Properties.ReadOnly = AutoTest.CurrentTestStatus == AutoTestStatus.InProgress);
            memoEditNotes.PerformSafely(() => memoEditNotes.Properties.ReadOnly = AutoTest.CurrentTestStatus == AutoTestStatus.InProgress);
            memoEditDescription.PerformSafely(() => memoEditDescription.Properties.ReadOnly = AutoTest.CurrentTestStatus == AutoTestStatus.InProgress);
            gridControlDosage.PerformSafely(() => gridViewlDosage.OptionsBehavior.ReadOnly = AutoTest.CurrentTestStatus != AutoTestStatus.Ended);
            barManagerMain.Form.PerformSafely(() => barEditItemStartStop.Enabled = AutoTest.CurrentTestStatus != AutoTestStatus.Ended);
            gridControlDosage.PerformSafely(() => gridColumnDosageOption.OptionsColumn.ReadOnly = AutoTest.CurrentTestStatus != AutoTestStatus.Ended);

            xtraUserControlAutoTestResultsTesting.PerformSafely(() => xtraUserControlAutoTestResultsTesting.TreeListColumnNotes.OptionsColumn.ReadOnly = AutoTest.CurrentTestStatus != AutoTestStatus.Ended);
            xtraUserControlAutoTestResultsAllResults.PerformSafely(() => xtraUserControlAutoTestResultsAllResults.TreeListColumnNotes.OptionsColumn.ReadOnly = AutoTest.CurrentTestStatus != AutoTestStatus.Ended);

            simpleButtonReconnectHW.PerformSafely(() => simpleButtonReconnectHW.Enabled = AutoTest.CurrentTestStatus != AutoTestStatus.Ended && 
                                                                                          AutoTest.CurrentTestStatus != AutoTestStatus.InProgress &&
                                                                                          !AutoTest.UseSimulatedReadings &&
                                                                                          AutoTest.AHardwareIsDisconnected);

            //Enable/Disable the stage related event handlers to avoid issues/duplicated actions during scanning
            EnableDisableStageRelatedHandlers();

            //Enable/Disable the highlight on the TestingItems grid
            EnableDisableTestingItemsHighlight();
        }

        /// <summary>
        /// Enable/Disable CRUD Buttons Based On Test Status
        /// </summary>
        private void EnableDisableCrudButtons()
        {
            barLargeButtonItemSave.Enabled = HasChanges() && AutoTest.CurrentTestStatus == AutoTestStatus.Ended;
            barLargeButtonItemDelete.Enabled = (AutoTest.CurrentTestStatus == AutoTestStatus.Paused || 
                                           AutoTest.CurrentTestStatus == AutoTestStatus.Ended) && !AutoTest.IsNew;
            barLargeButtonItemPrintPreview.Enabled = AutoTest.CurrentTestStatus == AutoTestStatus.Ended;
            barLargeButtonItemPrint.Enabled = AutoTest.CurrentTestStatus == AutoTestStatus.Ended;
        }

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        private void CustomInitializeComponent()
        {
            //Determine if the 3D human anatomy UI is enabled or disabled
            _useHumanAnatomyView = ConfigurationManager.AppSettings[ConfigKeys.UseHumanAnatomyView.ToString()].ToBoolean();

            //Perform 3D human anatomy related logic if the feature is enabled
            if (_useHumanAnatomyView)
            {
                //Set the 3D BigDigital model using the pre-initialized model instance based on the client's gender
                _bioDigital3DModel = AutoTest.Patient.GenderEnum == GenderEnum.Male ? UiHelperClass.BioDigitalModelMale : UiHelperClass.BioDigitalModelFemale;

                //Set the placeholder image based on gender
                xtraUserControlBioDigital3DModelViewerMain.PerformSafely(
                    () =>
                        xtraUserControlBioDigital3DModelViewerMain.PlaceholderImage =
                            AutoTest.Patient.GenderEnum == GenderEnum.Male
                                ? Resources.Male3dModelViewerPlaceholder
                                : Resources.Female3dModelViewerPlaceholder);

                //Open the model in the model viewer
                xtraUserControlBioDigital3DModelViewerMain.PerformSafely(() => xtraUserControlBioDigital3DModelViewerMain.Open(_bioDigital3DModel));
            }
            else
            {
                //If the 3D human anatomy feature is disabled then disable its UI 
                layoutControlImage.PerformSafely(() => layoutControlGroup3D.Visibility = LayoutVisibility.Never);
                layoutControlImage.PerformSafely(() => tabbedControlGroupImage.ShowTabHeader = DefaultBoolean.False);
            }

            //Set Form Icon
            Icon = Icon.FromHandle(Resources.AutoTest_16.GetHicon());
            ShowIcon = true;

            SetCustomFonts();

            _performPressureCheck = ConfigurationManager.AppSettings[ConfigKeys.PerformPressureCheck.ToString()].ToBoolean();
            _performMoistureCheck = ConfigurationManager.AppSettings[ConfigKeys.PerformMoistureCheck.ToString()].ToBoolean();
            _performHingeCheck = ConfigurationManager.AppSettings[ConfigKeys.PerformHingeCheck.ToString()].ToBoolean();
            _hardwareCheckDelay = ConfigurationManager.AppSettings[ConfigKeys.HardwareCheckDelay.ToString()].ToInteger();
            _stageAutoItemPostDelay = ConfigurationManager.AppSettings[ConfigKeys.StageAutoItemPostDelay.ToString()].ToInteger();
            _multiLevelStageItemDelay = ConfigurationManager.AppSettings[ConfigKeys.MultiLevelStageItemDelay.ToString()].ToInteger();
            _productDosageDelay = ConfigurationManager.AppSettings[ConfigKeys.ProductDosageDelay.ToString()].ToInteger();
            _cSAGeneralCommandDelay = ConfigurationManager.AppSettings[ConfigKeys.CSAGeneralCommandDelay.ToString()].ToInteger();
            _readingStabilityDelay = ConfigurationManager.AppSettings[ConfigKeys.ReadingStabilityDelay.ToString()].ToInteger();
            _mutliLevelStageItemPostDelay = ConfigurationManager.AppSettings[ConfigKeys.MutliLevelStageItemPostDelay.ToString()].ToInteger();
            _enableAutomationPostFailureAutoResume = ConfigurationManager.AppSettings[ConfigKeys.EnableAutomationPostFailureAutoResume.ToString()].ToBoolean();

            //Enable animation in the readings meter, this can only be enabled using code
            arcScaleComponentMeter.EnableAnimation = true;
            arcScaleComponentMeter.EasingMode = EasingMode.EaseInOut;
            arcScaleComponentMeter.EasingFunction = new BounceEase();

            //IMPORTANT:
            //The logic below is required to set the images of columns in grids, in the current devexpress version
            //columns can only use images included within ImageList, and below we first add images to the image list
            //dynamically and then reference them by Key, it is a bit complicated but it is better than referencing
            //by index which could easily change, we couldn't find an easier way to make the code cleaner at this point.
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Readings_16), Resources.Readings_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Meter_16), Resources.Meter_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Results_16), Resources.Results_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.ScanList_16), Resources.ScanList_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.ProductForms_16), Resources.ProductForms_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.ProductSize_16), Resources.ProductSize_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Calendar_16), Resources.Calendar_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Description_16), Resources.Description_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.DosageOption_16), Resources.DosageOption_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Price_16), Resources.Price_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.Quantity_16), Resources.Quantity_16);
            imageListMain.Images.Add(ExpressionHelper.GetNameOf(() => Resources.products), Resources.products);
            
            //Set the image index of each column by finding the index of the key that matches the image resource name.
            gridColumnPoint.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Readings_16));
            gridColumnPreliminary.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Meter_16));
            gridColumnSummary.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Meter_16));
            xtraUserControlAutoTestResultsTesting.TreeListColumnName.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Results_16));
            xtraUserControlAutoTestResultsTesting.TreeListColumnNotes.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Description_16));
            xtraUserControlAutoTestResultsAllResults.TreeListColumnName.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Results_16));
            xtraUserControlAutoTestResultsAllResults.TreeListColumnNotes.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Description_16));
            gridColumnScanItem.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.ScanList_16));
            gridColumnForm.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.ProductForms_16));
            gridColumnSize.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.ProductSize_16));
            gridColumnDuration.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Calendar_16));
            gridColumnDosageComments.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Description_16));
            gridColumnSuggestedUsage.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Description_16));
            gridColumnSchedule.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Description_16));
            gridColumnDosageOption.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.DosageOption_16));
            gridColumnPrice.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Price_16));
            gridColumnQuantity.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.Quantity_16));
            gridColumnProduct.ImageIndex = imageListMain.Images.Keys.IndexOf(ExpressionHelper.GetNameOf(() => Resources.products));
        }

        /// <summary>
        /// Handle setting custom fonts included with Vital without installation on user computer
        /// </summary>
        private void SetCustomFonts()
        {
            //IMPORTANT:
            //General:
            //We used new types of fonts in Vital that are not normally installed on the user computer, these fonts will of course not show
            //up on the computer correctly unless we handle it, there are two ways to fix this:
            //1- Install the font on the computer. (Tool Complicated).
            //2- Add ability to show fonts without installation by adding fonts as resources and including them in something called PrivateFontCollection.
            //and then using these fonts programmatically by setting them to the fonts of the controls.
            //We used the second approach and added the fonts to a PrivateFontCollection. This approach can only be used in code so we can't use these 
            //fonts while in designer, because of this, the fonts needed to be installed on the devs computer so we can design correctly but in code we 
            //still needed to set the font manually to each control even though the font itself didn't change but it will be coming from different source, 
            //in designer it is coming from computer, in Vital it will be coming from the PrivateFontCollection.

            //Detail:
            //Below we are setting the font manually on each control that uses a custom font, we tried setting it dynmaically but it didn't work simply
            //because at runtime the original font will be replaced with something else and by the time we reach here we would have lost any trace to the
            //font we used during design of the screen and we have to manually determine the font for each control as we are doing below.

            //TODO: We may change this whole thing to install fonts on the user computer, this would make things way easier but it is complex since it requires permissions
            
            StartStopButton.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            barAndDockingControllerMain.AppearancesDocking.PanelCaption.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            barAndDockingControllerMain.AppearancesDocking.PanelCaptionActive.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            
            barProgress.BarAppearance.Normal.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            barMain.BarAppearance.Normal.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            
            contextMenuStripTestingResult.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            contextMenuStripProducts.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            contextMenuStripResult.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            contextMenuStripTestingItems.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            
            dockPanelDescription.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            dockPanelImage.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            dockPanelNotes.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            dockPanelStages.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            dockPanelStages.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);

            gridViewlDosage.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            gridViewlDosage.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewlDosage.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewPreliminarySummary.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            gridViewPreliminarySummary.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewPreliminarySummary.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewStageAutoItems.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            gridViewStageAutoItems.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewStages.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            gridViewStages.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewTestingItems.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            gridViewTestingItems.Appearance.GroupPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewTestingItems.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            gridViewTestingItems.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            
            TestingTreeList.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            TestingTreeList.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            TestingTreeList.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            
            AllResultsTreeList.Appearance.FocusedRow.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            AllResultsTreeList.Appearance.HeaderPanel.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            AllResultsTreeList.Appearance.Row.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            
            layoutControlGroupMain.AppearanceGroup.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            layoutControlGroupMain.AppearanceItemCaption.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            layoutControlItemCSAState.AppearanceItemCaption.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            //layoutControlItemPrototypeState.AppearanceItemCaption.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            
            memoEditItemDescription.Properties.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 11F);
            memoEditNotes.Properties.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 10F);
            
            repositoryItemLookUpEditForm.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemLookUpEditForm.AppearanceDropDown.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemLookUpEditForm.AppearanceDropDownHeader.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemProgressBarTestProgress.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            repositoryItemMarqueeProgressBarError.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F);
            repositoryItemSpinEditPrice.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemSpinEditQuantity.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemTextEditCurrentTestStage.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            
            tabbedControlGroupMain.AppearanceTabPage.Header.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            tabbedControlGroupImage.AppearanceTabPage.Header.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            tabbedControlGroupImage.AppearanceTabPage.HeaderActive.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            tabbedControlGroupImage.AppearanceTabPage.HeaderDisabled.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            tabbedControlGroupImage.AppearanceTabPage.HeaderHotTracked.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            
            textEditBreadCrumbs.Properties.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            textEditCreationDate.Properties.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            textEditTitle.Properties.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);

            //Set font type and size for RepositoryItemLookUpEdit
            SetRepositoryLookupEditFont(repositoryItemLookUpEditDosageOptions);
        }

        /// <summary>
        /// Set font type and size for RepositoryItemLookUpEdit
        /// </summary>
        /// <param name="repositoryItemLookUpEdit"></param>
        private void SetRepositoryLookupEditFont(RepositoryItemLookUpEdit repositoryItemLookUpEdit)
        {
            repositoryItemLookUpEdit.Appearance.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemLookUpEdit.AppearanceDropDown.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemLookUpEdit.AppearanceDropDownHeader.Font = UiHelperClass.GetCustomFont(CustomFonts.OpenSansLight, 12F);
            repositoryItemLookUpEdit.Appearance.Options.UseFont = true;
            repositoryItemLookUpEdit.AppearanceDropDown.Options.UseFont = true;
            repositoryItemLookUpEdit.AppearanceDropDownHeader.Options.UseFont = true;
        }

        /// <summary>
        /// Adds a line of text to the memoEditItemDescription control
        /// </summary>
        /// <param name="text"></param>
        private void AddTemporaryDescriptionLine(string text)
        {
            return;
            memoEditItemDescription.PerformSafely(() => memoEditItemDescription.Text += text + Environment.NewLine);
            memoEditItemDescription.PerformSafely(() => memoEditItemDescription.SelectionStart = Int32.MaxValue);
            memoEditItemDescription.PerformSafely(() => memoEditItemDescription.ScrollToCaret());
        }

        /// <summary>
        /// Adds a line of text to the Notes field
        /// </summary>
        /// <param name="text"></param>
        private void AddTemporaryNotesLine(string text)
        {
            return;
            AutoTest.Notes += text + Environment.NewLine;
            memoEditNotes.PerformSafely(() => memoEditNotes.Refresh());
            memoEditNotes.PerformSafely(() => memoEditNotes.DoValidate());
            memoEditNotes.PerformSafely(() => memoEditNotes.SelectionStart = Int32.MaxValue);
            memoEditNotes.PerformSafely(() => memoEditNotes.ScrollToCaret());
        }

        /// <summary>
        /// Custom draw form to allow using custom font for the title
        /// </summary>
        /// <returns></returns>
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new MyFormPainter(this, LookAndFeel, UiHelperClass.GetCustomFont(CustomFonts.OpenSansSemibold, 12F), this);
        }

        /// <summary>
        /// Draw testing point icon based on specified location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public void DrawTestingPoint(int x, int y)
        {
            var image = Resources.HandOutline;
            var graphics = Graphics.FromImage(image);

            x = x - TestingPointIconWidthOffset;
            y = y - TestingPointIconWidthOffset;

            graphics.CompositingMode = CompositingMode.SourceOver;
            Resources.TestingPoint.MakeTransparent();
            graphics.DrawImage(Resources.TestingPoint, new Point(x, y));

            pictureEditImage.PerformSafely(() => pictureEditHand.Image = image);
            pictureEditImage.PerformSafely(() => pictureEditHand.Refresh());
        }

        #endregion

        #region Scanning

        /// <summary>
        /// Set the caption of the start/stop button based on current TestStatus
        /// </summary>
        private void SetStartStopButtonCaption()
        {
            //The logic here is used to set the caption of the start/stop edit button, we are using this logic and not the cross thread
            //extention "PerformSafely" because the control we need to set the caption for "StartStopButton" is not a control and so it doesn't
            //have "Invoke" method, also even if we used this extension on a related control like the commented line below "barManagerMain.Form.PerformSafely"
            //we would still get a little hang and so the solution is to first call this logic in a separate thread and second use "barManagerMain.Form"
            //to call "BeginInvoke" which allows us to set the caption safely. We also could use barManagerMain.Form.PerformSafely as mentioned below
            //but we kept BeginInvoke to avoid confusion that barManagerMain.Form.PerformSafely alone would solve the issue without the thread.
            //Source: https://supportcenter.devexpress.com/ticket/details/t595702/devexpress-xtrabars-barstaticitem-does-not-support-begininvoke-method
            if (barManagerMain.Form.InvokeRequired)
            {
                var buttonDelegate = new StartStopButtonUpdateDelegate(SetStartStopButtonCaption);
                barManagerMain.Form.Invoke(buttonDelegate, new object[] {});
            }
            else
            {
                //Change caption and icon based on state
                var newCaption = StartStopButton.Caption;
                var buttonIcon = Resources.Play_Colored_32;

                switch (AutoTest.CurrentTestStatus)
                {
                    case AutoTestStatus.Pending:
                        newCaption = "Start";
                        break;
                    case AutoTestStatus.InProgress:
                        newCaption = "Pause";
                        buttonIcon = Resources.Pause_Colored_32;
                        break;
                    case AutoTestStatus.Paused:
                        newCaption = "Resume";
                        break;
                    case AutoTestStatus.Ended:
                        newCaption = "Finished";
                        buttonIcon = Resources.Stop_Colored_32;
                        break;
                }

                barManagerMain.Form.PerformSafely(() => barEditItemProgress.BeginUpdate());
                //Set the button caption and icon
                barManagerMain.Form.BeginInvoke(new MethodInvoker(() => { StartStopButton.Caption = newCaption; }));
                barManagerMain.Form.BeginInvoke(new MethodInvoker(() => { StartStopButton.Image = buttonIcon; }));

                barManagerMain.Form.PerformSafely(() => barEditItemProgress.EndUpdate());
            }
        }

        /// <summary>
        /// Shows an announcement about scanning based on current state
        /// </summary>
        private void ShowScanningAnnouncement(ScanningAnnouncement announcementType)
        {
            var announcement = string.Empty;

            if (announcementType == ScanningAnnouncement.ScanningStatus)
            {
                switch (AutoTest.CurrentTestStatus)
                {
                    case AutoTestStatus.Pending:
                        UiHelperClass.HideSplash();
                        break;
                    case AutoTestStatus.InProgress:
                        announcement = AutoTest.LastTestStatus == AutoTestStatus.Pending ? "Start Scanning" : "Resume Scanning";
                        break;
                    case AutoTestStatus.Paused:
                        announcement = "Scanning Paused";
                        break;
                    case AutoTestStatus.Ended:
                        announcement = "Scanning Ended";
                        break;
                }
            }
            else
            {
                //All announcements below should only be shown during scanning, this is important to check becasue some of the logic that calls
                //the announcements gets called during scanning and non scanning logic and so instead of adding a check everywhere it is called
                //we universally check for the status here.
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    switch (announcementType)
                    {
                        case ScanningAnnouncement.CollectingScanningData:
                            announcement = "Collecting Scanning Data";
                            break;
                        case ScanningAnnouncement.InitializeScanningFlags:
                            announcement = "Initialize Scanning Flags";
                            break;
                        case ScanningAnnouncement.ValidateHardware:
                            announcement = "Validating Hardware";
                            break;
                        case ScanningAnnouncement.StartingScanning:
                            announcement = "Starting Scanning";
                            break;
                        case ScanningAnnouncement.StartedStageScanning:
                            announcement = "Scanning " + AutoTest.StageBookmark.Name;
                            break;
                    }
                }
            }

            //If the announcement text is not set then don't show it
            if (!string.IsNullOrEmpty(announcement))
            {
                UiHelperClass.ShowWaitingPanel(announcement + " ...");    
            }
        }

        /// <summary>
        /// Enable/Disable the stage related event handlers to avoid issues/duplicated actions during scanning
        /// </summary>
        private void EnableDisableStageRelatedHandlers()
        {
            //The logic below will link/unlink grids and gridviews to their handlers depending on status to prevent un-necessary firing of events
            //during scanning because the scanning logic will actually perform all actions manually without dependency on UI including UI updates like
            //setting focused rows and focused views and choosing the tab to show, this all happens without dependency on the handlers so we can minimize
            //process dependency on UI and keep those handlers strictly for user related actions only.
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
            {
                gridViewStages.FocusedRowChanged -= gridViewStages_FocusedRowChanged;
                gridViewStageAutoItems.FocusedRowChanged -= gridViewStages_FocusedRowChanged;
                gridControlStages.FocusedViewChanged -= gridControlStages_FocusedViewChanged;

            }
            else
            {
                //Unregister the events just in case they were registered before to avoid the issue of events running twice, this happened before when the even was already registered in designer code
                //and it was registered here again and so the same event fired twice, we kept the registration in designer because removing it caused issues.
                gridViewStages.FocusedRowChanged -= gridViewStages_FocusedRowChanged;
                gridViewStageAutoItems.FocusedRowChanged -= gridViewStages_FocusedRowChanged;
                gridControlStages.FocusedViewChanged -= gridControlStages_FocusedViewChanged;

                gridViewStages.FocusedRowChanged += gridViewStages_FocusedRowChanged;
                gridViewStageAutoItems.FocusedRowChanged += gridViewStages_FocusedRowChanged;
                gridControlStages.FocusedViewChanged += gridControlStages_FocusedViewChanged;
            }
        }

        /// <summary>
        /// Enables or disables the highlight on the stage items
        /// </summary>
        /// <param name="isEnabled"></param>
        private void EnableDisableStageItemsHighlight(bool isEnabled)
        {
            //We use the line below to enable/clear the highlight effect on the stage auto items because gridview cannot unselect all rows 
            //after at least one got selected and so we have to play with appearance to clear/enable the highligh to avoid having the last 
            //stage auto item of the previously scanned stage being highlighted (The stage item we mean) in the stage panel and to allow 
            //highlighting new stage items while scanning them
            gridControlStages.PerformSafely(() => gridViewStageAutoItems.OptionsSelection.EnableAppearanceFocusedRow = isEnabled);
        }

        /// <summary>
        /// Enables or disables the highlight on the testing items grid
        /// </summary>
        private void EnableDisableTestingItemsHighlight()
        {
            //We use the line below to enable/clear the highlight effect on the testing items because gridview cannot unselect all rows 
            //after at least one got selected and so we have to play with appearance to clear/enable the highligh to avoid having a testing 
            //item being highlighted in the testing grid during scanning

            //We show the focused row style in the TestingItems grid based on the scanning mode, if the current StageAutoItem uses Split/Switch then we always show the highlight except when the scanning
            //is in progress, however if the scanning mode is 1x1 then we always show the focused row highlight
            var isSequantialScanning = !AutoTest.StageItemBookmark.IsEmpty && AutoTest.CurrentTestingPathParent.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeSequential;
            var showRowFocusHighlight = isSequantialScanning || AutoTest.CurrentTestStatus != AutoTestStatus.InProgress;

            gridControlTestingItems.PerformSafely(() => gridViewTestingItems.OptionsSelection.EnableAppearanceFocusedRow = showRowFocusHighlight);
            gridControlTestingItems.PerformSafely(() => gridControlTestingItems.Refresh());
            //gridControlTestingItems.PerformSafely(() => gridControlTestingItems.FocusedView = gridViewTestingItems);
            //gridControlTestingItems.PerformSafely(() => gridControlTestingItems.Focus());
            //gridControlTestingItems.PerformSafely(() => ActiveControl = gridControlTestingItems);
        }

        /// <summary>
        /// Performs any UI actions that should be done after scanning is done.
        /// </summary>
        private void PerformPostScanningUIActions()
        {
            EnableDisableStageItemsHighlight(true);
            
            _lastStageRowChangeMethod = FocusedRowChangeMethod.Undefined;
        }

        /// <summary>
        /// Sets the meter reading value
        /// </summary>
        /// <param name="reading"></param>
        /// <param name="isRealReading"></param>
        private void SetMeterReadingValue(int reading, bool isRealReading)
        {
            //Update the meter reading value
            gaugeControlMeterNumber.PerformSafely(() => digitalGaugeMeterNumber.Text = reading.ToString());

            //Logic below is needed to allow animation to show properly on the gauge control, this is required because the guage control has an issue
            //when it gets updated quickly during a thread where the animation doesn't have a chance to show the change, so using the logic below
            //forces it to update by disabling animation, update the meter value by a small margin and then enable it again and set the actual correct
            //reading value
            gaugeControlMeter.PerformSafely(() => arcScaleComponentMeter.EnableAnimation = false);
            gaugeControlMeter.PerformSafely(() => arcScaleComponentMeter.Value = arcScaleComponentMeter.Value - 2);
            gaugeControlMeter.PerformSafely(() => arcScaleComponentMeter.EnableAnimation = true);
            gaugeControlMeter.PerformSafely(() => arcScaleComponentMeter.EasingMode = EasingMode.EaseInOut);
            gaugeControlMeter.PerformSafely(() => arcScaleComponentMeter.EasingFunction = new BounceEase());
            gaugeControlMeter.PerformSafely(() => arcScaleComponentMeter.Value = reading);

            AddTemporaryDescriptionLine(isRealReading ? "Real Reading" : "Simulated Reading" + ": " + reading);
        }

        #endregion

        #region Stages

        /// <summary>
        /// Analyze the current selection in stages panel and call logic to update tabs selection accordingly
        /// </summary>
        private void UpdateStageSelection(GridView selectedView, bool isViewChangeEvent, int? previousRowHandle = null)
        {
            //IMPORTANT:
            //In this method it is important to pass the selected view because it is the actual view that fired a certain event and it is the actual instance of that
            //view in case it was a detailed view. This is very important becasuse sometimes we need to apply a change on the instance specifically because applying it
            //on the view (gridViewStages or gridViewStageAutoItems) alone won't work.

            //Ignore the logic if the status is in progress since automation should handle this on its own.
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress) return;

            //This flag is used to the prevent the execution of this logic multiple times during the same event handling (Method will still be called by other events)
            _isUpdatingUserStageSelection = true;

            if (selectedView != null)
            {
                //This is used nnregister the events from the grid to prevent them from firing when setting focused row handles or focused views however it didn't work
                //and the events keep firing but we keep it here for reference
                //gridViewStages.FocusedRowChanged -= gridViewStages_FocusedRowChanged;
                //gridViewStageAutoItems.FocusedRowChanged -= gridViewStages_FocusedRowChanged;
                //gridControlStages.FocusedViewChanged -= gridControlStages_FocusedViewChanged;

                //Set the focused stage either manually based on user selection or automatically based on the parent of the current stage item
                AutoTest.SetStageBookmarkIndex(selectedView.IsDetailView ? selectedView.SourceRowHandle : selectedView.FocusedRowHandle);

                //Set the stage title in description area
                SetCurrentStageImageAndDescription();

                if (selectedView.IsDetailView)
                {
                    //If the selected view is the stage items view, then set the selected row handle in the stages view to auto select the parent stage of
                    //stage item that the user selected manually
                    gridControlStages.PerformSafely(() => gridViewStages.FocusedRowHandle = AutoTest.StageBookmark.IndexValue);
                }
                
                //Set the focused stage item either manually based on user selection or automatically based on the selected stage
                //If the selected view is the stage item view then just use the current FocusedRowHandle, not if the focused view is the stage view then things are more
                //complex and to determine the right row to focus we need to understand our situation and which row we are coming from, to handle this we are using the "previousRowHandle"
                //If previousRowHandle isn't available (Meanining this method wasn't called by FocusedRowChanged event) or if the current focused stage is after the previous focused stage then
                //we set the focused stage item to the first one (Index = 0), if however that is not the case then we focus the stage auto item. This whole logic is very important when navigating
                //the stages panel using the keyboard and not the mouse, knowing the previous focused stage order will help us determine if we should focus the first or the last stage item which
                //would maintain consistency in the navigation of the items in the stages panel up and down as if they are in one single list. If we don't do this, the selection will always jump
                //to the first stage item even if we came from a stage that is after the current one.
                AutoTest.SetStageItemBookmarkIndex(selectedView.IsDetailView ? selectedView.FocusedRowHandle : 
                                                                              !previousRowHandle.HasValue || 
                                                                              !AutoTest.StageBookmark.IsMultiLevel ||
                                                                              _lastStageRowChangeMethod == FocusedRowChangeMethod.Mouse ||
                                                                              selectedView.FocusedRowHandle > previousRowHandle? 0 :
                                                                              AutoTest.StageItemBookmarkLimit - 1);

                //Selects the stage tab and performs stage related UI updates
                UpdateStageUI();

                //IMPORTANT:
                //If the current bookmark is multi-level then we initialize the StepBoomark to make sure user manual navigation can work properly
                //even for manual user selection
                if (AutoTest.StageBookmark.IsMultiLevel)
                {
                    InitializeStageItemScanBookmarks();    
                }

                //Set the stage item data based on current stage and stage item
                BindStageItemData(AutoTest.CurrentTestingPathParent);

                //If the user manually selected the stages view, then we perform logic below to automatically focus one of its stage items (First or Last based on case).
                if (AutoTest.StageBookmark.IsMultiLevel && !selectedView.IsDetailView)
                {
                    var relationIndex = gridViewStages.GetRelationIndex(gridViewStages.FocusedRowHandle, gridViewStageAutoItems.LevelName);

                    //Here we are loading the detailed view by focused row handle of the stages view because we can't just use "gridViewStageAutoItems" since it represents the general
                    //detail view and not the exact instance that represents the detail view we want under a certain stage.
                    //A note regarding highlighting the current stage item, we can use the even "RowStyle" to keep the row highlighted regardless if the grid is focused or not, but then
                    //we would have to write logic to turn it on and off based based on status and checks because if we don't then the stage item row would be focused forever. We thought
                    //this is complexity is not needed and so we are handling this by setting the focused view which is working find and is not complicated.
                    var currentDetailView = (GridView)selectedView.GetDetailView(gridViewStages.FocusedRowHandle, relationIndex);//Notice here we are loading the exact instance of the view that we need to modify

                    //We apply the logic below only is this event if not a view change event, this is important because if we don't do this we will get a problem where a user navigating
                    //the stages panel by keyboard will get stuck inside the stage auto items and won't be able to go up outside of it to the next stage, the user can go down and can select
                    //using the mouse but not using the keyboard becasuse the logic will keep setting the stage items view as the focused view, however using the check below "isViewChangeEvent" this won't happen.
                    if (currentDetailView != null && !isViewChangeEvent)
                    {
                        gridControlStages.PerformSafely(() => gridControlStages.FocusedView = currentDetailView);//Here if we set the FocusedView to gridViewStageAutoItems then it won't work, we need the exact instance
                        gridControlStages.PerformSafely(() => currentDetailView.FocusedRowHandle = AutoTest.StageItemBookmark.IndexValue);
                        gridControlStages.PerformSafely(() => gridControlStages.Refresh());    
                    }
                    else
                    {
                        //Always if there is no detail view then make sure to set focus to the stages view to make sure the highligh effect appears without problems.
                        gridControlStages.PerformSafely(() => gridControlStages.FocusedView = gridViewStages);
                    }
                }
                else
                {
                    //If the user selected a stage that doesn't have stage items in the stages panel then we perform logic to automatically select the first stage item
                    //in the right side of the screen
                    if (!AutoTest.StageBookmark.IsMultiLevel)
                    {
                        HighlightCurrentStageItem();
                    }
                }
                
                //If there is a current stage item then we update the image/description area with its details
                if (AutoTest.CurrentTestingPathParent != null)
                {
                    //Sets the image and description of the current item
                    UpdateImageAndDescription();
                }

                //Performs stage item related UI updates
                UpdateStageItemUI();
            }

            //This is used re-register the events to the grid to allow them to fire when changing focused row handles or focused views however it didn't work
            //and the events keep firing anyway event when we unregsiter them but we keep logic it here for reference
            //gridViewStages.FocusedRowChanged += gridViewStages_FocusedRowChanged;
            //gridViewStageAutoItems.FocusedRowChanged += gridViewStages_FocusedRowChanged;
            //gridControlStages.FocusedViewChanged += gridControlStages_FocusedViewChanged;

            //Reset flag used to the prevent the execution of this logic multiple times
            _isUpdatingUserStageSelection = false;
        }

        /// <summary>
        /// Selects the row of the current stage in the stages panel
        /// </summary>
        private void HighlightCurrentStage()
        {
            //WE DON'T NEED TO ACTIVATE THE GRID ANYMORE TO KEEP ITS STYLE WHEN NOT FOCUSE BUT KEEP CODE HERE FOR REFERNCE
            //Activate the stages panel to make sure the selection of the row is visible
            //For some reason only setting the ActiveControl property worked to set the focus to the stages grid
            //gridControlStages.PerformSafely(() => ActiveControl = gridControlStages);

            //Make sure to focus the stages view to make sure the highlighting appears correctly
            gridControlStages.PerformSafely(() => gridControlStages.FocusedView = gridViewStages);

            //Set the stage title in description area
            SetCurrentStageImageAndDescription();

            //We use the line below to cler the highlight effect on the stage auto items because gridview cannot unselect all rows after at least one
            //got selected and so we have to play with appearance to clear the highligh to avoid having the last stage auto item of the previously
            //scanned stage being highlighted (The stage item we mean) in the stage panel.
            EnableDisableStageItemsHighlight(false);

            //Select the row of the current stage
            gridControlStages.PerformSafely(() => gridViewStages.FocusedRowHandle = AutoTest.StageBookmark.IndexValue);
        }

        /// <summary>
        /// Select the testing tab based on selected stage
        /// </summary>
        private void UpdateStageUI()
        {
            //At minimum a stage should be available for selection to work
            if (AutoTest.StageBookmark.IsEmpty)
            {
                return;
            }

            //Find matching tab group based on Tag and UIKey matching and select it
            var selectedTabGroup = (LayoutGroup)tabbedControlGroupMain.TabPages.ConvertToTypedList().FirstOrDefault(page => (string)page.Tag == AutoTest.StageBookmark.UIKey);

            //If no matching tab group found then ignore the logic
            if (selectedTabGroup == null)
            {
                return;
            }
            
            //Set the selected tab group
            layoutControlMain.PerformSafely(() => tabbedControlGroupMain.SelectedTabPage = selectedTabGroup);

            //If the current stage doesn't have multiple levels then the stage doesn't have items in the stages panel and so we should use
            //the stage name as it is in the current stage label at the bottom left of the screen
            if (!AutoTest.StageBookmark.IsMultiLevel)
            {
                //Set the stage tab title, this is not necessary but we keep it here just in case
                //layoutControlMain.PerformSafely(() => selectedTabGroup.Text = AutoTest.CurrentScanTitle);

                //Set current stage text at the scanning status label at the bottom left side of the screen
                barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));
            }

            //Performs stage specific UI activation logic
            PerformStageSpecificUIActivation();
        }

        /// <summary>
        /// Performs stage specific UI activation logic
        /// </summary>
        private void PerformStageSpecificUIActivation()
        {
            //At minimum a stage should be available for selection to work
            if (AutoTest.StageBookmark.IsEmpty)
            {
                return;
            }

            switch (AutoTest.StageTabKeyEnum)
            {
                case StageTabKey.PreliminarySummary:
                    //If the current stage is summary then we show the "Summary" column in the readings gridview
                    gridControlPreliminarySummary.PerformSafely(() => gridColumnSummary.Visible = AutoTest.StageKeyEnum == StageKey.Summary);
                    break;
            }
        }

        #endregion

        #region Stage Item

        /// <summary>
        /// Selects the row of the current stage item in its UI location
        /// </summary>
        private void HighlightCurrentStageItem()
        {
            //Make sure a stage and stage item are available for selection to work
            if (AutoTest.StageBookmark.IsEmpty || AutoTest.StageItemBookmark.IsEmpty)
            {
                return;
            }

            //Update selected row in UI based on current stage and stage item
            switch (AutoTest.StageTabKeyEnum)
            {
                case StageTabKey.PreliminarySummary:
                    gridControlPreliminarySummary.PerformSafely(() => gridViewPreliminarySummary.FocusedRowHandle = AutoTest.StageItemBookmark.IndexValue);
                    //gridControlPreliminarySummary.PerformSafely(() => ActiveControl = gridControlPreliminarySummary);
                    break;
                case StageTabKey.Testing:
                    //We use the line below to enable the highlight effect on the stage auto items because selection appearence on stage items in stage panel
                    //get reset when starting each stage because gridview cannot unselect all rows after at least one got selected and so we have to play with 
                    //appearance to clear/enable the highligh to avoid having the last stage auto item of the previously scanned stage being highlighted (The stage item we mean) 
                    //in the stage panel and to allow highlighting new stage items while scanning them
                    EnableDisableStageItemsHighlight(true);
                    
                    var relationIndex = gridViewStages.GetRelationIndex(gridViewStages.FocusedRowHandle, gridViewStageAutoItems.LevelName);

                    //Here we are loading the detailed view by focused row handle of the stages view because we can't just use "gridViewStageAutoItems" since it represents the general
                    //detail view and not the exact instance that represents the detail view we want under a certain stage.
                    //A note regarding highlighting the current stage item, we can use the even "RowStyle" to keep the row highlighted regardless if the grid is focused or not, but then
                    //we would have to write logic to turn it on and off based based on status and checks because if we don't then the stage item row would be focused forever. We thought
                    //this is complexity is not needed and so we are handling this by setting the focused view which is working find and is not complicated.
                    var currentDetailView = (GridView)gridViewStages.GetDetailView(gridViewStages.FocusedRowHandle, relationIndex);
                    gridControlStages.PerformSafely(() => gridControlStages.FocusedView = currentDetailView);
                    gridControlStages.PerformSafely(() => currentDetailView.FocusedRowHandle = AutoTest.StageItemBookmark.IndexValue);
                    gridControlStages.PerformSafely(() => gridControlStages.Refresh());
                    //gridControlStages.PerformSafely(() => ActiveControl = gridControlStages);
                    break;
                case StageTabKey.Dosage:
                    gridControlDosage.PerformSafely(() => gridViewlDosage.FocusedRowHandle = AutoTest.StageItemBookmark.IndexValue);
                    //gridControlDosage.PerformSafely(() => ActiveControl = gridControlDosage);
                    break;
                case StageTabKey.Results:
                    
                    //gridControlTestingResults.PerformSafely(() => gridViewTestingResults.FocusedRowHandle = AutoTest.StageItemBookmark.IndexValue);
                    //gridControlTestingResults.PerformSafely(() => ActiveControl = gridControlTestingResults);
                    break;
            }

            if (AutoTest.CurrentRootStageAutoItem != null)
            {
                //Sets the image and description of the current item
                UpdateImageAndDescription();
            }
        }

        /// <summary>
        /// Performs stage item related UI updates
        /// </summary>
        private void UpdateStageItemUI()
        {
            //Set the selected tab group name
            if (AutoTest.CurrentTestingPathParent != null && CurrentStageTabGroup != null)
            {
                //Set the stage tab title, this is not necessary but we keep it here just in case
                //layoutControlMain.PerformSafely(() => CurrentStageTabGroup.Text = AutoTest.CurrentScanTitle);

                //Set current stage text
                barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));

                if (AutoTest.StageTabKeyEnum == StageTabKey.Testing)
                {
                    //Set the current breadcrums text
                    UpdateBreadcrumb();

                    //Call the logic below only if there are items to scan in the first place
                    if (AutoTest.CurrentTestingPathParent.HasScanItems)
                    {
                        //Enable or disable testing items highlight, this is important to call here because the scanning type
                        //can change from one item to another so we always have to update the highlight
                        EnableDisableTestingItemsHighlight();    
                    }
                }
            }
        }

        /// <summary>
        /// Set the current breadcrums text
        /// </summary>
        private void UpdateBreadcrumb()
        {
            //Set the current breadcrums text
            textEditBreadCrumbs.PerformSafely(() => textEditBreadCrumbs.Text = AutoTest.GetCurrentScanInfo(true));

            //Scroll to the end to make sure the last part of text is always visible
            textEditBreadCrumbs.PerformSafely(() => textEditBreadCrumbs.SelectionStart = Int32.MaxValue);
            textEditBreadCrumbs.PerformSafely(() => textEditBreadCrumbs.ScrollToCaret());
        }

        #endregion

        #endregion

        #region Process

        #region General

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        private void SetBinding()
        {
            UiHelperClass.BindControl(textEditCreationDate, AutoTest, () => AutoTest.TestDate);
            UiHelperClass.BindControl(textEditTitle, AutoTest, () => AutoTest.Name);
            UiHelperClass.BindControl(memoEditDescription, AutoTest, () => AutoTest.Description);
            UiHelperClass.BindControl(memoEditNotes, AutoTest, () => AutoTest.Notes);
            BindStagesPanel();
            BindPreliminarySummary();
            BindTestingResults();

            //Dosage data should actually be done after the other tests and not before however we only bind it like this when the test is already done
            //and opened from the DB, there is also a check inside the method to  validate that the test has a Dosage stage
            if (AutoTest.CurrentTestStatus == AutoTestStatus.Ended)
            {
                BindDosage();
            }
            
            BindResults();
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        private void ClearBinding()
        {
            textEditCreationDate.DataBindings.Clear();
            textEditTitle.DataBindings.Clear();
            memoEditDescription.DataBindings.Clear();
            memoEditNotes.DataBindings.Clear();
            gridControlStages.DataBindings.Clear();
            gridControlPreliminarySummary.DataBindings.Clear();
            TestingTreeList.DataBindings.Clear();
            AllResultsTreeList.DataBindings.Clear();
            gridControlDosage.DataBindings.Clear();
        }

        /// <summary>
        /// Rebinds the object.
        /// </summary>
        private void Rebind()
        {
            ClearBinding();
            ClearHandlers();
            //SetProperties();
            UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);
            SetBinding();
            UiHelperClass.ShowWaitingPanel(StaticKeys.FinalizingMessage);
            SetupMainErrorProvider();
            UpdateErrorProvider();
            ShowHideErrorIcons();
            SetupHandlers();
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        private void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = AutoTest;
        }

        /// <summary>
        /// Updates the binding of error provider
        /// </summary>
        public void UpdateErrorProvider()
        {
            //This is needed because of a crash in designer since the datasoruce is not defined yet
            if (dxErrorProviderMain.DataSource != null)
            {
                dxErrorProviderMain.ClearErrors();
                dxErrorProviderMain.UpdateBinding();
            }
        }

        /// <summary>
        /// Validate the current form.
        /// </summary>
        public void ValidateForm()
        {
            ShowHideErrorIcons();
        }

        /// <summary>
        /// Show or Hide the errors upper control they not supported the DxErrorProvider.
        /// </summary>
        private void ShowHideErrorIcons()
        {

        }

        #endregion

        #region Binding
        
        #region Stages

        /// <summary>
        /// Binds Stages Panel
        /// </summary>
        private void BindStagesPanel()
        {
            UiHelperClass.BindControl(gridControlStages, AutoTest.AutoProtocolRevision,() => AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions);

            //Automatically expand all stages detail views
            UiHelperClass.ExpandAllRows(gridViewStages);
        }

        #endregion

        #region Preliminary/Summary

        /// <summary>
        /// Binds PreliminarySummary
        /// </summary>
        private void BindPreliminarySummary()
        {
            if (AutoTest.TestHasStage(StageKey.Preliminary))
            {
                //Bind the preliminary/summary grid
                UiHelperClass.BindControl(gridControlPreliminarySummary, AutoTest, () => AutoTest.Readings);
            }

            GenerateSummaryStageNavigationData();
        }

        #endregion

        #region Testing Results

        /// <summary>
        /// Binds Testing Results
        /// </summary>
        private void BindTestingResults()
        {
            //We are setting datasource instead of using Binding because with binding the grid is not refreshing
            //to reflect changes in the data source.
            TestingTreeList.PerformSafely(() => TestingTreeList.DataSource = AutoTest.TestingResults);
            
            UiHelperClass.RefreshTreeListData(TestingTreeList);

            TestingTreeList.PerformSafely(() => TestingTreeList.ExpandAll());
        }

        #endregion

        #region All Results

        /// <summary>
        /// Binds All AutoTestResults
        /// </summary>
        private void BindResults()
        {
            AllResultsTreeList.PerformSafely(() => AllResultsTreeList.DataSource = AutoTest.TestingResults);

            UiHelperClass.RefreshTreeListData(AllResultsTreeList);

            //If the test is ended then genereate the navigation data for the results stage
            if (AutoTest.CurrentTestStatus == AutoTestStatus.Ended)
            {
                GenerateResultsStageNavigationData();
            }

            AllResultsTreeList.PerformSafely(() => AllResultsTreeList.ExpandAll());
        }

        #endregion

        #region Dosage

        /// <summary>
        /// Binds Dosage
        /// </summary>
        private void BindDosage()
        {
            if (!AutoTest.TestHasStage(StageKey.Dosage)) return;

            gridControlDosage.PerformSafely(() => gridControlDosage.DataSource = AutoTest.Products);
            UiHelperClass.RefreshGridData(gridControlDosage);

            GenerateDosageStageNavigationData();
        }

        #endregion

        #endregion

        #region Scanning

        #region General

        /// <summary>
        /// Performs the logic of starting or stopping the scanning based on button click
        /// </summary>
        private void ToggleScanningState()
        {
            //Use the lock mechanism to prevent multi-threading exceptions
            lock (_startStopLock)
            {
                //Before doing extra steps validate that hardware is valid for scanning, this should only be done
                //if the current status is Pending or Paused, in such case and if hardware is not valid then we show an
                //error and stop, this way user receives a message and scanning doesn't proceed.
                //This is useful in stopping the scanning activites asap without having to wait for logic to proceed further
                //and only stop after the usual recurring hardware validation logic.
                if ((AutoTest.CurrentTestStatus == AutoTestStatus.Pending ||
                     AutoTest.CurrentTestStatus == AutoTestStatus.Paused) &&
                    !AutoTest.HardwareValidForScanning)
                {
                    UiHelperClass.ShowInformation("Please make sure that CSA is connected and checked.", "Hardware Connection Issue");

                    return;
                }

                //Reset the status of the hardware validation process
                _hardwareValidationPerformed = false;

                //Updates current/previous status based on current status checks
                ToggleTestStatus();

                //Shows announcement about the current new scanning state
                ShowScanningAnnouncement(ScanningAnnouncement.ScanningStatus);

                //Call logic to set button caption in thread to avoid blocking current thread
                var captionThread = new Thread(SetStartStopButtonCaption) { IsBackground = true };
                captionThread.Start();

                //Enable/Disable fields based on the current status
                SetAccessRules();

                //If the current status is in progress, then start scanning thread, otherwise stop it.
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    StartScanningThread();
                }
                else
                {
                    StopScanning();
                }
            }
        }

        /// <summary>
        /// Updates current/previous status based on current status checks
        /// </summary>
        private void ToggleTestStatus()
        {
            //Store the last test status before updating the current status.
            AutoTest.LastTestStatus = AutoTest.CurrentTestStatus;

            //Switch current scanning status based on previous status
            switch (AutoTest.CurrentTestStatus)
            {
                case AutoTestStatus.Pending:
                    AutoTest.CurrentTestStatus = AutoTestStatus.InProgress;
                    break;
                case AutoTestStatus.InProgress:
                    AutoTest.CurrentTestStatus = AutoTestStatus.Paused;
                    break;
                case AutoTestStatus.Paused:
                    AutoTest.CurrentTestStatus = AutoTestStatus.InProgress;
                    break;
                case AutoTestStatus.Ended:
                    AutoTest.CurrentTestStatus = AutoTestStatus.Ended;
                    break;
            }
        }

        /// <summary>
        /// Initializes and starts the scanning thread
        /// </summary>
        private void StartScanningThread()
        {
            //Use the lock mechanism to prevent multi-threading exceptions
            lock (_scanningThreadLock)
            {
                //Before scanning, initialize the scanning thread
                InitializeScanningThread();

                //Start scanning thread after initialization
                _scanningThread.Start();

                //In case of simulated readings, start the simulated readings thread too
                if (AutoTest.UseSimulatedReadings)
                {
                    _readingThread.Start();
                }
            }
        }

        /// <summary>
        /// Initializes the scanning thread
        /// </summary>
        private void InitializeScanningThread()
        {
            //Initialize scanning thread each time this is called
            _scanningThread = new Thread(StartOrResumeScanning) { IsBackground = true };
            
            //In case of simulated readings, initialize all simulated readings related variables
            if (AutoTest.UseSimulatedReadings)
            {
                //Initialize reandom reading generator
                _randomReadingGenerator = new Random();

                //Initialize the simulated readings thread
                _readingThread = new Thread(SetSimulatedReading) { IsBackground = true };

                //Set the ApartmentState to STA to avoid error "The calling thread must be STA, because many UI components require this" 
                //which started happening after we added the option to explicitly set reading to Yes or No based on a keyboard key being
                //pressed
                _readingThread.SetApartmentState(ApartmentState.STA);
            }
        }

        /// <summary>
        /// Performs the scanning action sequence
        /// </summary>
        private void StartOrResumeScanning()
        {
            //Hide any previous waiting panel
            UiHelperClass.HideSplash();

            //Do not proceed if the status is incorrect
            if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
            {
                StopScanning();
                return;
            }

            //2- Initializes status flags and any testing variables
            InitializeScanningFlags();

            //3- Validates hardware connection
            ValidateHardware();

            ShowScanningAnnouncement(ScanningAnnouncement.StartingScanning);

            //4- Move to the next stage
            MoveToNextStage();
        }

        /// <summary>
        /// Collects data to scan and calculate total items count and progress limit
        /// </summary>
        private void CountScanningItems()
        {
            ShowScanningAnnouncement(ScanningAnnouncement.CollectingScanningData);

            //Calculate the number of items to scan
            AutoTest.ScanItemsCount = 
                AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions.Count(stage => stage.AutoTestStage.StageKeyEnum != StageKey.Results) + // Count stages except for results
                AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions
                    .Where(stage => stage.AutoTestStage.StageKeyEnum != StageKey.Results)
                        .Select(protocolStageRevision=> protocolStageRevision.AutoProtocolStage)
                            .SelectMany(protocolStage => protocolStage.StageAutoItems).Count();// Count all stage items from all stages

            //If the test is ended then set the scanned items count to the number of items to scan, this will set the progress to the max value.
            if (AutoTest.CurrentTestStatus == AutoTestStatus.Ended)
            {
                AutoTest.ScannedItemsCount = AutoTest.ScanItemsCount;    
            }

            //Update the progress maximum based on the number of items to scan
            barManagerMain.Form.PerformSafely(() => repositoryItemProgressBarTestProgress.Maximum = AutoTest.ScanItemsCount);

            //Set the intial value of the progress bar, this is important in the case of existing saved tests where the value should be set to %100
            barManagerMain.Form.PerformSafely(() => barEditItemProgress.EditValue = AutoTest.ScannedItemsCount);
        }

        /// <summary>
        /// Initializes status flags and any testing variables
        /// </summary>
        private void InitializeScanningFlags()
        {
            ShowScanningAnnouncement(ScanningAnnouncement.InitializeScanningFlags);
            //The logic here should set any initial flags when a scan is started the first time, right now there is no logic here because all the flags are inside
            //the AutoTest entity where they are initialized when the entity is created so nothing is needed here but we will keep this method in case we got the properties
            //out of the AutoTest entity into a separate object and wanted to initialize it
        }

        /// <summary>
        /// Stops the scanning thread
        /// </summary>
        private void StopScanning()
        {
            //Hide any previous waiting panel
            UiHelperClass.HideSplash();

            PerformPostScanningUIActions();

            //Validate that scanning thread is alive before aborting
            if (_scanningThread.IsAlive)
            {
                //VERY IMPORTANT:
                /*
                 * During testing we found that aborting the testing thread while loading/saving data by LLBLGen caused threading exceptions, online we found that
                 * using Thread.Abort() isn't adviced at all but we have no choice but to keep it now due to lack of time and its a major change.
                 * The solution was to prevent LLBLGen from conflicting with the abort action and since we can't predict when an abort could happen we created
                 * the flag below to record when LLBLGen is performing an operation and during that time we activate the flag and below we check it to prevent
                 * aborting the thread until the flag gets reset. This gives LLBLGen the chance to finish and keeps the thread waiting until it can be safely
                 * aborted
                 */
                while (_dataOperationActive)
                {
                    Thread.Sleep(_readingStabilityDelay);
                }

                //In case of simulated readings, abort the simulated readings thread too
                if (AutoTest.UseSimulatedReadings && _readingThread != null)
                {
                    _readingThread.Abort();
                }

                //Validate thread is initialized first
                if (_hardwareValidationThread != null)
                {
                    _hardwareValidationThread.Abort();    
                }

                //Validate thread is initialized first
                if (_scanningThread != null)
                {
                    _scanningThread.Abort();
                }
                
                //--------------------------------------------------------------------
                //IMPORTANT: Notice that after this line nothing else will be exectued so make sure to finish anything before this method call
                //--------------------------------------------------------------------
            }
        }

        /// <summary>
        /// Finish Scanning and move to ended state
        /// </summary>
        private void EndScanning()
        {
            SendCommandToCSA(AutoCSACommand.Reset);
            SendCommandToCSA(AutoCSACommand.StopAutomation);

            //Set the status to ended, we do this here because the logic has no way of toggling the InProgress status to ended unless we explicitly set it.
            AutoTest.CurrentTestStatus = AutoTestStatus.Ended;

            //Call AutoSave at the end of scanning to make sure there are no pending changes for saving
            AutoSave();

            ToggleScanningState();
        }

        /// <summary>
        /// Validates hardware connection
        /// </summary>
        private void ValidateHardware()
        {
            ShowScanningAnnouncement(ScanningAnnouncement.ValidateHardware);

            //Determine if progressbar should show progress or if it should indicate an active CSA error/failure
            ShowHideErrorIndication(false);

            //Reset the CSA notification failure message
            _lastCSANotificationFailure = string.Empty;

            SendCommandToCSA(AutoCSACommand.Reset);

            SendCommandToCSA(AutoCSACommand.ActivateAutomationMode);

            //Perform check only if it is activated
            if (_performHingeCheck)
            {
                SendCommandToCSA(AutoCSACommand.HingeCheck);    
            }

            //Perform check only if it is activated
            if (_performMoistureCheck)
            {
                SendCommandToCSA(AutoCSACommand.MoistureCheck);    
            }

            //Perform check only if it is activated
            if (_performPressureCheck)
            {
                SendCommandToCSA(AutoCSACommand.PressureCheck);    
            }

            SendCommandToCSA(AutoCSACommand.StartAutomation);

            //Reset the CSA failure notification flags
            _pausedByCSAFailureNotification = false;
            
            //Confirm hardware validation is performed which would also allow Vital to start allowing CSA failure notifications to be recieved and handled
            //which shouldn't be allowed before the hardware validation is fully performed to prevent issues when the test starts with CSA already failing
            //one of the checks.
            _hardwareValidationPerformed = true;

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Performs initialization for scanning related parameters
        /// </summary>
        private void InitializeScanningParameters()
        {
            //Collects data to scan and calculate total items count and progress limit
            CountScanningItems();
        }

        /// <summary>
        /// Updates the progress count and also updates related user interface
        /// </summary>
        /// <param name="isStageProgress"></param>
        private void UpdateScanningProgress(bool isStageProgress)
        {
            //We only update progress if it wasn't recorded already, this is very important to avoid
            //counting progress multiple times when the test is paused and resumed because the logic passes through
            //the same methods again, the index helps us continue from where we stopped but the progress will be counted
            //again unless we use this flag to prevent it.
            if (isStageProgress && AutoTest.CurrentStage.ProgressRecorded ||
               !isStageProgress && AutoTest.CurrentRootStageAutoItem.ProgressRecorded) return;

            //Make sure to skip counting progress for the Results stage since it doesn't involve scanning
            if (AutoTest.StageKeyEnum == StageKey.Results) return;

            //If the current stage is dosage and the progress is by item then return because we are not supposed to count progress for dosage items
            //Since they are generated during the scanning and so we cannot predict how many items to scan and adjusting the scan total during the scan
            //based on the items to dosage is not good.
            if (!isStageProgress && AutoTest.CurrentStage.StageKeyEnum == StageKey.Dosage) return;

            AutoTest.ScannedItemsCount += 1;
            barManagerMain.Form.PerformSafely(() => barEditItemProgress.EditValue = AutoTest.ScannedItemsCount);

            //Determine what item to mark as ProgressRecorded based on whether it is a stage or stage item progress.
            if (isStageProgress)
            {
                AutoTest.CurrentStage.ProgressRecorded = true;
            }
            else
            {
                AutoTest.CurrentRootStageAutoItem.ProgressRecorded = true;
            }
        }

        #endregion

        #region Stage Scanning

        #region General
        
        /// <summary>
        /// Performs logic that moves the stage bookmark and calls stage scanning logic
        /// </summary>
        private void MoveToNextStage()
        {
            //Move the stage bookmark to the next stage or resume the last stage if it was paused
            AutoTest.MoveStageBookmarkNext();

            //Start/Resume current stage scanning
            StartStageScanning();
        }

        /// <summary>
        /// Starts scanning activities on the current stage
        /// </summary>
        private void StartStageScanning()
        {
            //Show announcement about current stage
            ShowScanningAnnouncement(ScanningAnnouncement.StartedStageScanning);

            //Highlight the current stage in the stages panel
            HighlightCurrentStage();

            //Generate navigation data for destination only stages
            GenerateDestinationOnlyNavigationData();

            //Selects the stage tab and performs stage related UI updates
            UpdateStageUI();

            //AddTemporaryNotesLine(AutoTest.CurrentStage.Name);
            
            //Thread.Sleep(1000);

            //Hide Announcement
            UiHelperClass.HideSplash();

            //In all stages we move to the next stage auto item except for the results case where no further scanning/testing should happen
            if (AutoTest.StageKeyEnum != StageKey.Results)
            {
                if (AutoTest.CurrentStageHasItems)
                {
                    //Move to the next stage auto item
                    MoveToNextStageAutoItem();
                }
            }
            else
            {
                //In the case of the results stage we only call the logic to highlight the first item in the results but we don't do any scanning
                HighlightCurrentStageItem();

                //In the case of results stage we just end the scanning.
                EndScanning();
            }

            //Update progress
            //Updating progress was moved here to make sure we update progress of stage only after all of its items are scanned and not
            //before, this gives a more realistic progress to the user specially in cases of pause/resume
            UpdateScanningProgress(true);

            //After finishing all stage items scanning we move to the next stage unless the current stage is the last, we shouldn't reach this code in the last stage (Results) beause
            //it is designed to end the scanning before reahing this code anyway.
            if (!AutoTest.StageBookmark.IsLast)
            {
                //Perform automatic saving of the AutoTest after each stage
                AutoSave();

                //Move to next stage
                MoveToNextStage();
            }
        }

        /// <summary>
        /// Sets the description and image of current stage
        /// </summary>
        private void SetCurrentStageImageAndDescription()
        {
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress) return;

            //If the stage is the results then just clear the image and description since there is nothing to show
            if (AutoTest.StageKeyEnum == StageKey.Results)
            {
                ClearImageAndDescription();
                return;
            }

            //Set the stage title in description area
            UpdateImageAndDescription(AutoTest.CurrentStage.Name, AutoTest.CurrentStage.Description,null, null,null);
        }

        #endregion

        #region Specific

        /// <summary>
        /// Check d
        /// </summary>
        private void GenerateDestinationOnlyNavigationData()
        {
            if (AutoTest.StageBookmark.IsEmpty)
            {
                return;
            }

            //In the case of dosage stage, we generate StageAutoItems in runtime before the stage starts to allow navigating it using the same navigation logic used
            //in all other stages, this is required because the Dosage stage is actually dependent on data that gets generated during the scanning and not before and so it doesn't
            //actually have any StageAutoItems, the fastest soltuion to make it work without complicating the code much is to generate StageAutoItems for it in runtime
            //instead of having to edit the code to work with this special case in a very different way than the rest. This logic also applies to the results except that in results
            //there is really no scanning, we just need to switch to the stage and show it, we might face this issue in future lists but this solution seems like a quick one we
            //can replicate.
            if (AutoTest.StageKeyEnum == StageKey.Dosage)
            {
                //Here we bind dosage data too because in the case of dosage the data shouldn't be there until the scan is performed and a list of products is generated
                BindDosage();
            }
            else if (AutoTest.StageKeyEnum == StageKey.Results)
            {
                GenerateResultsStageNavigationData();
            }
        }

        /// <summary>
        /// Generates navigation data for results stage
        /// </summary>
        private void GenerateResultsStageNavigationData()
        {
            //Generate navigation data for results stage since it is a destination only stage
            var resultsStage = AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions.FirstOrDefault(protocolStageRevision => protocolStageRevision.AutoTestStage.StageKeyEnum == StageKey.Results);

            if (resultsStage != null)
            {
                resultsStage.AutoProtocolStage.StageAutoItems = AutoTest.TestingResults.ToList().Select(
                    result => new StageAutoItem
                    {
                        AutoProtocolStage = resultsStage.AutoProtocolStage,
                        AutoItem = result.AutoItem,
                        /* Notice below that we are trying to get the TestingPoint from StageAutoItem if it wasn't Null and that is because the testing point inside
                         * StageAutoitem is the one that should be used for testing however the StageAutoItem isn't always available because we only set it in memory
                         * during scanning however when the test is loaded from DB we don't link the StageAutoItem to the result again and there is no relation between
                         * them in DB. Based on this we check the StageAutoItem and if it exists then we use its testing point. This logic should be ok for now because
                         * the StageAutoItem isn't available only when the test is ended and when the test is ended we don't allow any scanning activities.
                         * 
                         * Also specifically in case of results there is no scanning activities at all so the testing point isn't actually used.
                         */
                        TestingPoint = result.StageAutoItem != null ? result.StageAutoItem.TestingPoint : result.AutoItem.TestingPoint,
                        StageAutoItems = new BindingList<StageAutoItem>(),
                        ScanningMethod = UiHelperClass.GetSingleLookupFromCache(LookupTypes.AutoItemScanningMethod, AutoItemScanningMethod.AutoItemScanningMethodNormal),
                        ChildsOrderType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsOrderType, ChildsOrderType.ChildsOrderTypeByOrder),
                        ChildsScanningType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsScanningType, ChildsScanningType.ChildsScanningTypeNone),
                    }).ToBindingList();
            }
        }

        /// <summary>
        /// Generates navigation data for summary stage
        /// </summary>
        private void GenerateSummaryStageNavigationData()
        {
            if (AutoTest.TestHasStage(StageKey.Summary))
            {
                //Generate navigation data for summary stage since it is a destination only stage, we can do this before the scanning starts because the items of this stage are the same
                //as the preliminary items and are not supposed to change
                var summaryStage = AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions.FirstOrDefault(protocolStageRevision => protocolStageRevision.AutoTestStage.StageKeyEnum == StageKey.Summary);

                if (summaryStage != null)
                {
                    summaryStage.AutoProtocolStage.StageAutoItems = AutoTest.Readings.ToList().Select(
                        reading => new StageAutoItem
                        {
                            AutoProtocolStage = summaryStage.AutoProtocolStage,
                            AutoItem = reading.AutoItem,
                            /* Notice below that we are trying to get the TestingPoint from StageAutoItem if it wasn't Null and that is because the testing point inside
                             * StageAutoitem is the one that should be used for testing however the StageAutoItem isn't always available because we only set it in memory
                             * during scanning however when the test is loaded from DB we don't link the StageAutoItem to the result again and there is no relation between
                             * them in DB. Based on this we check the StageAutoItem and if it exists then we use its testing point. This logic should be ok for now because
                             * the StageAutoItem isn't available only when the test is ended and when the test is ended we don't allow any scanning activities.
                             */
                            TestingPoint = reading.StageAutoItem != null ? reading.StageAutoItem.TestingPoint : reading.AutoItem.TestingPoint,
                            StageAutoItems = new BindingList<StageAutoItem>(),
                            ScanningMethod = UiHelperClass.GetSingleLookupFromCache(LookupTypes.AutoItemScanningMethod, AutoItemScanningMethod.AutoItemScanningMethodNormal),
                            ChildsOrderType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsOrderType, ChildsOrderType.ChildsOrderTypeByOrder),
                            ChildsScanningType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsScanningType, ChildsScanningType.ChildsScanningTypeNone),
                        }).ToBindingList();
                }
            }
        }

        /// <summary>
        /// Generates navigation data for dosage stage for the case when test is already ended and loaded from database
        /// </summary>
        private void GenerateDosageStageNavigationData()
        {
            if (!AutoTest.TestHasStage(StageKey.Dosage)) return;

            if (AutoTest.CurrentTestStatus == AutoTestStatus.Ended)
            {
                //Generate navigation data for dosage stage since it is a destination only stage, 
                var dosageStage = AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions.FirstOrDefault(protocolStageRevision => protocolStageRevision.AutoTestStage.StageKeyEnum == StageKey.Dosage);

                if (dosageStage != null)
                {
                    dosageStage.AutoProtocolStage.StageAutoItems = AutoTest.Products.ToList().Select(
                    product => new StageAutoItem
                    {
                        AutoProtocolStage = dosageStage.AutoProtocolStage,
                        AutoItem = product.AutoTestResult.AutoItem,
                        //IMPORTANT: Notice below that we are using the TestingPoint inside the AutoItem and not from StageAutoItem
                        /* This can be problematic because the testing point inside the AutoItem might be different from the one set inside the StageAutoItem but
                         * in the current context the result has no access to its related StageAutoItem and so we can't get that information. Within the current context
                         * this may be ok because the data loaded here is loaded when the test has ended and so there is no scanning anyway and so the testing point
                         * doesn't matter. But this will cause an issue if we allow any scanning activities on the items for a test that has ended because the testing point
                         * may not be the same.
                         */
                        /* Notice below that we are trying to get the TestingPoint from StageAutoItem if it wasn't Null and that is because the testing point inside
                        * StageAutoitem is the one that should be used for testing however the StageAutoItem isn't always available because we only set it in memory
                        * during scanning however when the test is loaded from DB we don't link the StageAutoItem to the result again and there is no relation between
                        * them in DB. Based on this we check the StageAutoItem and if it exists then we use its testing point. This logic should be ok for now because
                        * the StageAutoItem isn't available only when the test is ended and when the test is ended we don't allow any scanning activities.
                        */
                        TestingPoint = product.AutoTestResult.StageAutoItem != null ? product.AutoTestResult.StageAutoItem.TestingPoint : product.AutoTestResult.AutoItem.TestingPoint,
                        StageAutoItems = new BindingList<StageAutoItem>(),
                        ScanningMethod = UiHelperClass.GetSingleLookupFromCache(LookupTypes.AutoItemScanningMethod, AutoItemScanningMethod.AutoItemScanningMethodNormal),
                        ChildsOrderType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsOrderType, ChildsOrderType.ChildsOrderTypeByOrder),
                        ChildsScanningType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsScanningType, ChildsScanningType.ChildsScanningTypeNone),
                        DosageOptions = new BindingList<DosageOption>()//Skip loading dosage option because the test is ended and we don't need to load them
                        //DosageOptions = _autoTestDestinationManager.GetDosageOptions(new DosageOptionsFilter { ProductsId = product.Id })
                    }).ToBindingList();
                }
            }
        }

        #endregion

        #endregion

        #region Stage Item Scanning

        #region General
        
        /// <summary>
        /// Performs logic that moves the stage item bookmark and calls stage item scanning logic
        /// </summary>
        private void MoveToNextStageAutoItem()
        {
            //Move the stage item bookmark to the next stage item or resume the last stage item if it was paused
            AutoTest.MoveStageItemBookmarkNext();

            //Start/Resume current stage item scanning
            StartStageAutoItemScanning();
        }

        /// <summary>
        /// Starts scanning activities on the current stage item
        /// </summary>
        private void StartStageAutoItemScanning()
        {
            UiHelperClass.HideSplash();

            //if the scanning reached this level then make sure to set previous status to InProgress
            AutoTest.LastTestStatus = AutoTestStatus.InProgress;

            //Performs the common initialization actions required for multi-level StageAutoItems whether on root or deeper levels
            PerformStageAutoItemInitialScanningSteps();

            //Stage Item Scanning Logic
            //-------------------------------------------------------------------
            //Performs stage item specific scanning activities
            PerformStageItemSpecificScanning();
            //-------------------------------------------------------------------

            //Update progress
            UpdateScanningProgress(false);

            Thread.Sleep(_stageAutoItemPostDelay);

            if (!AutoTest.StageItemBookmark.IsLast)
            {
                MoveToNextStageAutoItem();
            }
        }

        /// <summary>
        /// Performs the common initialization actions required for multi-level StageAutoItems whether on root or deeper levels
        /// </summary>
        private void PerformStageAutoItemInitialScanningSteps()
        {
            //Initializes the ScanBookmarks collection based on the stage, this action is performed once on each StageAutoItem for multi-level stage items
            InitializeStageItemScanBookmarks();

            //Make sure childes are loaded before binding stage item data
            if (AutoTest.CurrentTestingPathParent.ChildesLoaded)
            {
                //Set the stage item data based on current stage and stage item
                BindStageItemData(AutoTest.CurrentTestingPathParent);
            }

            //Highlight the current stage item in its UI
            HighlightCurrentStageItem();

            //Performs stage item related UI updates
            UpdateStageItemUI();

            //Select stage auto item in its list
            AddTemporaryNotesLine(AutoTest.CurrentTestingPathParent.AutoItem.Name);

            //Send command to CSA to set scanning point based on current item
            SendCommandToCSA(AutoCSACommand.SetAutomationProbe);
        }

        /// <summary>
        /// Set the stage item data based on current stage and stage item
        /// </summary>
        private void BindStageItemData(StageAutoItem currentStageAutoItem)
        {
            //Perform logic only on multilevel stages
            if (AutoTest.StageItemBookmark.IsMultiLevel)
            {
                //Show announcement about current stage item scanning if the current stage item is multilevel, meaning that it is shown in the stage panel.
                //We only do this for stages items that shows in stages panel to avoid showing it for each item that is not shown there like preliminary for example.
                //ShowScanningAnnouncement(ScanningAnnouncement.StartedStageScanning);

                switch (AutoTest.StageTabKeyEnum)
                {
                    case StageTabKey.Testing:
                        //Set the AutoTest reference inside the current testing path parent to allow for testing items filtration based on results
                        currentStageAutoItem.SetAutoTest(AutoTest);

                        //The method below was called to solve cross threading issue but it turned out that it wasn't necessary
                        //if (currentStageAutoItem.ScanItems != null)
                        //{
                        //    //The logic below was used to temporary set the testing grid data temporary replica of the scanning items while
                        //    //we update the real ScanItems list in the background but we eventually didn't use it because the problem was caused
                        //    //because the list we wanted to update had a reference to an object we were deleting and so this caused an issue
                        //    var tempScanItems = new BindingList<StageAutoItem>(currentStageAutoItem.ScanItems);

                        //    _isUpdatingUserStageSelection = true;

                        //    gridControlTestingItems.PerformSafely(() => gridControlTestingItems.DataSource = tempScanItems);
                        //    UiHelperClass.RefreshGridData(gridControlTestingItems);

                        //    _isUpdatingUserStageSelection = false;
                        //}

                        //SetScanItems before scanning, this will only update the items if the ScanItems list wasn't set before
                        gridControlTestingItems.PerformSafely(() => currentStageAutoItem.SetUpdateScanItems());

                        //Clears and rebinds the stage item data
                        RefreshStageItemDataBinding(currentStageAutoItem);
                        
                        break;
                }
            }
        }

        /// <summary>
        /// Initializes the ScanBookmarks collection based on the stage, this action is performed once on each StageAutoItem for multi-steps stage items
        /// </summary>
        private void InitializeStageItemScanBookmarks()
        {
            //Perform logic only on stages that have multiple steps within
            //Notice here ware depending on the StageTabKey and not the StageKey because the StageTabKey is more generic
            //and can cover more stages without having to make changes to support each of them manually, this can help us in
            //future to avoid adding logic for each stage that we add specially if it handles things the same way.
            if (AutoTest.StageTabKeyEnum == StageTabKey.Testing || AutoTest.StageTabKeyEnum == StageTabKey.Dosage)
            {
                AutoTest.CurrentTestingPathParent.InitializeScanBookmarks(AutoTest.StageTabKeyEnum);
            }
        }

        /// <summary>
        /// Clears and rebinds the stage item data
        /// </summary>
        private void RefreshStageItemDataBinding(StageAutoItem currentStageAutoItem)
        {
            //IMPORTANT:
            //We set the testing items in grid only if there are items to test or if the scanning is paused even if there are no items to scan,
            //this is important for the case when the user manually adds all the testing items as results, the outcome at the end should be an empty list.
            if (currentStageAutoItem.HasScanItems || AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
            {
                //Check this to prevent events firing during setting the grid datasource
                _isUpdatingUserStageSelection = true;

                //If the test is not running and we need to refresh the list then we temporarily disabled the focused row appearence to prevent
                //the effect of jumping row focus when adding results which happens because we set the datasource and then set the FocusedRowHandle
                //we avoid this by disabling UI updates on the grid temporarily until all the datasource and row handle actions are done and then we enable
                //it again
                if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
                {
                    gridControlTestingItems.PerformSafely(() => gridViewTestingItems.BeginDataUpdate());
                }

                //We are setting datasource instead of using Binding because with binding the grid is not refreshing
                //to reflect changes in the data source.
                gridControlTestingItems.PerformSafely(() => gridControlTestingItems.DataSource = currentStageAutoItem.ScanItems);
                
                //We only need to refresh the grid in the case of eliminiation, in sequential mode the datasource refresh is not needed
                if (currentStageAutoItem.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeElimination)
                {
                    UiHelperClass.RefreshGridData(gridControlTestingItems);    
                }

                //Enable the UI updates on the grid again
                if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
                {
                    gridControlTestingItems.PerformSafely(() => gridViewTestingItems.EndDataUpdate());
                }

                //VERY IMPORTANT:
                /*
                 * The line below is important to make sure the grid restore back its active state after we update it, during testing and
                 * in cases of constant and repeating pause/resume caused by CSA failures and validations the grid UI used to fail and become
                 * a gray unresponsive area and it turns out it needed the line below to look back normal again, the same line is called later
                 * in the process but it seems that because of timing and threads and state of the test this logic may not be executed and so it
                 * is important to call it here to make sure the grid doesn't remain unrefreshed.
                 */
                gridControlTestingItems.PerformSafely(() => gridControlTestingItems.Refresh());

                //IMPORTANT:
                //After setting the grid datasource we restore the FocusedRowHandle based on the last index value that we had
                //this is important when adding a result or when deleting a result and restoring a testing item where the grid
                //gets ites datasource set again and the focused row handle jumps to the first row and we use the logic below to 
                //restore it back to where it was.
                //We only do this if the test is paused or if the scanning type is sequential
                if ((AutoTest.CurrentTestStatus != AutoTestStatus.InProgress ||
                    currentStageAutoItem.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeSequential))
                {
                    //Get the bookmark, notice that this could be an Automation or User bookmark based on case.
                    var currentStepBookmark = currentStageAutoItem.GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning);

                    if (currentStepBookmark != null && !currentStepBookmark.IsEmpty)
                    {
                        var focusedRowIndex = currentStepBookmark.IndexValue;

                        //Only set the FocusedRowHandle if the index is valid
                        if (focusedRowIndex >= 0)
                        {
                            //Highlight the current testing item in its UI
                            gridControlTestingItems.PerformSafely(() => gridViewTestingItems.FocusedRowHandle = focusedRowIndex);
                        }
                    }
                }

                _isUpdatingUserStageSelection = false;
            }
        }

        #endregion

        #region Specific

        /// <summary>
        /// Performs stage item specific scanning activities
        /// </summary>
        private void PerformStageItemSpecificScanning()
        {
            //Checks the status of the CSA and updates status flags
            //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
            //if hardware faliure was found
            StartHardwareValidationThread();

            //Validate that the CSA connceted and valid
            if (AutoTest.CurrentCsaState == CSAState.Connected)
            {
                switch (AutoTest.StageKeyEnum)
                {
                    case StageKey.Preliminary:
                    case StageKey.Summary:
                        ScanPreliminarySummaryItem();
                        break;
                    case StageKey.MajorIssues:
                    case StageKey.Testing:
                        ScanMutliLevelStageItem(false);
                        break;
                    case StageKey.Dosage:
                        ScanProductDosage();
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region Stage Specific Scanning

        #region Perliminary/Summary
        
        /// <summary>
        /// Performs specific scanning activities for Preliminary/Summary stage item
        /// </summary>
        private void ScanPreliminarySummaryItem()
        {
            //In case of simulated readings mode use simulated readings instead of real readings
            if (AutoTest.UseSimulatedReadings)
            {
                //Here we are checking if the logic should use stablilized readings only, if that is the case, then the logic will keep holding the current
                //thread until a stable reading is rached, otherwise we just use the current reading as it is.
                if (AutoTest.UseStableReadingsOnly)
                {
                    //Here we reset the simulated reading flags to allow the simulated reading generator to start a reading session
                    ResetSimulatedReadingFlags();

                    //Stay in loop until the reading is marked as stabilized
                    while (!AutoTest.IsReadingStable)
                    {
                        //We are calling the perform safely logic on the grid because devexpress gave us a threading exception
                        //when updating the reading value and we could suppress it however using the approach below the exception
                        //didn't happen and so we are assuming the call below is safe to avoid threading exceptions.
                        if (AutoTest.StageKeyEnum == StageKey.Preliminary)
                        {
                            gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.PreliminaryReading = AutoTest.CurrentReadingValue);
                        }
                        else
                        {
                            gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.SummaryReading = AutoTest.CurrentReadingValue);
                        }

                        Thread.Sleep(_readingStabilityDelay);
                    }

                    //AddTemporaryDescriptionLine("Locked Reading: " + AutoTest.CurrentReadingValue + Environment.NewLine);
                }
                else
                {
                    //We are calling the perform safely logic on the grid because devexpress gave us a threading exception
                    //when updating the reading value and we could suppress it however using the approach below the exception
                    //didn't happen and so we are assuming the call below is safe to avoid threading exceptions.
                    if (AutoTest.StageKeyEnum == StageKey.Preliminary)
                    {
                        gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.PreliminaryReading = AutoTest.CurrentReadingValue);
                    }
                    else
                    {
                        gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.SummaryReading = AutoTest.CurrentReadingValue);
                    }
                }
            }
            else
            {
                //Send command to prototype to start reading
                AutoCsaEmdUnitManagerPhase2.Instance.StartReading();

                //Here we are checking if the logic should use stablilized readings only, if that is the case, then the logic will keep holding the current
                //thread until a stable reading is rached, otherwise we just use the current reading as it is.
                if (AutoTest.UseStableReadingsOnly)
                {
                    //Thread.Sleep(10);

                    AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);
                    
                    //Stay in loop until the reading is marked as stabilized
                    while (!AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized)
                    {
                        //We are calling the perform safely logic on the grid because devexpress gave us a threading exception
                        //when updating the reading value and we could suppress it however using the approach below the exception
                        //didn't happen and so we are assuming the call below is safe to avoid threading exceptions.
                        if (AutoTest.StageKeyEnum == StageKey.Preliminary)
                        {
                            gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.PreliminaryReading = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value);
                        }
                        else
                        {
                            gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.SummaryReading = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value);
                        }

                        AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);
                        
                        //AutoCsaEmdUnitManagerPhase2.Instance.StopReading();

                        Thread.Sleep(_readingStabilityDelay);

                        //AutoCsaEmdUnitManagerPhase2.Instance.StartReading();
                    }
                }

                //After reading is stabilized make sure to record the last reading value
                if (AutoTest.StageKeyEnum == StageKey.Preliminary)
                {
                    gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.PreliminaryReading = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value);
                }
                else
                {
                    gridControlPreliminarySummary.PerformSafely(() => AutoTest.CurrentTestResult.SummaryReading = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value);
                }
                //AutoCsaEmdUnitManagerPhase2.Instance.StopReading();
            }            
        }

        #endregion

        #region Major Issues/Testing

        /// <summary>
        /// Performs specific scanning activities for multi-level stage item like Major Issues or Testing
        /// </summary>
        private void ScanMutliLevelStageItem(bool isDrillDown, int currentReadingValue = 0)
        {
            //Make sure the StageAutoItem initialization logic below is only called if we are testing a non root item because in the case of root
            //StageAutoItem this logic should already be called and this method gets called in both cases but by different locations and so we 
            //add this check here to make sure the logic is called only when it is needed
            if (!AutoTest.CurrentRootStageAutoItem.IsTestingOnRoot)
            {
                PerformStageAutoItemInitialScanningSteps();
            }

            //Move the bookmark to the next index or resume the last index if it was paused
            AutoTest.MoveStepBookmarkNext(ScanBookmarkType.ScanningRounds);

            //Get the current step ScanBookmark based on type
            var scanBookmark = AutoTest.GetStepScanBookmark(ScanBookmarkType.ScanningRounds);

            AddTemporaryNotesLine(AutoTest.CurrentTestingPathParent.AutoItem.Name + " - Scanning Round - " + (scanBookmark.Index + 1));
            
            //Set current scan round title
            barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));

            //Set the current breadcrums text
            UpdateBreadcrumb();

            //Call the result adding logic for the current item, the logic inside will only add result if the item doesn't have one already
            //This is important in the case of pause/resume (With bad timing) to make sure the result is added before any further logic
            //Below we avoid adding result if we are testing on root because otherwise the root StageAutoItem will always get inserted as result anyway
            if (!AutoTest.CurrentRootStageAutoItem.IsTestingOnRoot)
            {
                AddItemAsResult(AutoTest.CurrentTestingPathParent, currentReadingValue);    
            }

            //Perform checks on different scanning actions for the current item
            if (AutoTest.CurrentTestingPathParent.DirectAccessScanChecks != null &&
                AutoTest.CurrentTestingPathParent.DirectAccessScanChecks.Any())
            {
                foreach (var scanCheck in AutoTest.CurrentTestingPathParent.DirectAccessScanChecks)
                {
                    PerformMultiLevelStageAutoItemCheck(AutoTest.CurrentTestingPathParent, false, scanCheck.Key, currentReadingValue);
                }    
            }

            PerformMultiLevelStageAutoItemCheck(AutoTest.CurrentTestingPathParent, false, ScanCheck.AddAllChildesAsResults.ToString(), currentReadingValue);
            PerformMultiLevelStageAutoItemCheck(AutoTest.CurrentTestingPathParent, false, ScanCheck.Childes.ToString(), currentReadingValue);

            //Perform the detailed scanning activities of the current selected stage item
            //PerformMultiLevelStageItemSpecificScanning();

            Thread.Sleep(_mutliLevelStageItemPostDelay);

            if (scanBookmark.IsLast)
            {
                //TODO: Check if we still need the logic to collapse parents when drilling back
                //var currentParentNode = AutoTest.CurrentRootStageAutoItem == null ||
                //    AutoTest.CurrentRootStageAutoItem.AutoTestResult == null? null : treeListTestingResults.FindNodeByKeyID(AutoTest.CurrentRootStageAutoItem.AutoTestResult.StructureId);
                
                //if (currentParentNode != null)
                //{
                //    treeListTestingResults.PerformSafely(() => currentParentNode.Expanded = false);
                //}

                //IMPORTANT: Identification of Direct Access Items
                //The flag below is important to use in the case of scanning an item that is accessed with the direct access method to make them stand out from other items to help
                //handle an important and problematic case when pausing test and resuming. In normal case the system just resumes from the last parent in list which is fine and the parent
                //is removed from scanning path when it finishes scanning and testing resumes normally however in the case of items accessed with direct access approach the system needs to
                //check the state of the DirectAccessScanCheck itself and see if it finished and this status gets set by calling FinishCheck after performing the action for that direct access
                //item, however if the test was paused during the scanning of the direct access item then the system will not get the chance to finish the check and at that point it becomes
                //difficult to finish the check when resuming because we have lost the context of where were we before and so the only way to restore that context is to help identify such items
                //and then add the handling for such case wherever its needed.
                //Below we temporarily store the current item key and check if it was a direct access item, then after drill back, we finish the check related to the direct access item inside its
                //parent.
                //TODO: VERY IMPORTANT NOTE: This logic will work only for direct access scanning that is based on a StageAutoItem, if moving forward we add such scanning activites that are not based on
                //items then they won't work this way because there is no item to refer to to use its key and so we need to handle the key extraction and handling in a different way.
                var currentTestingPathParentKey = AutoTest.CurrentTestingPathParent.AutoItem.Key;
                var currentTestingPathParentIsDirectAcessScanCheck = AutoTest.CurrentTestingPathParent.IsDirectAcessScanCheck;

                //Remove the last parent from the testing path which would cause the previous item in the path to become the current parent
                AutoTest.CurrentRootStageAutoItem.DrillBack();
                
                //Now that we got back the parent, if we find that the last item that we scanned was a DirectAccessItem then we make sure to finish the check for it, this is needed
                //only when the test is paused and resumed during the scanning of the direct access item, in normal scanning case the scanning check for this item should be marked as finished already
                if (currentTestingPathParentIsDirectAcessScanCheck)
                {
                    AutoTest.CurrentTestingPathParent.FinishCheck(currentTestingPathParentKey);
                }

                //Set current scan round title
                barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));

                //Set the current breadcrums text
                UpdateBreadcrumb();

                //IMPORTANT: Handling Resume After Pause With Multiple Drill Down Operations Pending To Finish
                /* Basically we let every LastStageAutoItem call scanning again on the previous one.
                 * We have a special case which is when we return to the root, the number of items in TestingPath will always be larger than 1 so in this case 
                 * we continue scanning if the first root item didn't finish scanning yet.
                 * BE CAREFULL: SINCE THIS IS NOT APPLIYING RECURSION (THE CHILD DIDN'T RETURN NATURALLY TO PARENT EXECUTION PATH BUT INSTEAD CALLED THE PARENT 
                 * SCAN LOGIC MANUALLY) AS THE NORMAL CASE THEN SOME LOGIC MIGHT BE LOST IF IT DEPENDS ON RECURSION BETWEEN PARENT & CHILD.
                 */
                if (!isDrillDown && (!AutoTest.CurrentRootStageAutoItem.IsTestingOnRoot || !AutoTest.CurrentRootStageAutoItem.IsScanningFinished))
                {
                    ScanMutliLevelStageItem(false);
                }
            }
            else
            {
                //Checks the status of the CSA and updates status flags
                //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
                //if hardware faliure was found
                StartHardwareValidationThread();

                //If the current stage item isn't last, then call logic again.
                ScanMutliLevelStageItem(isDrillDown);
            }
        }

        /// <summary>
        /// Perform specific scanning for multi-level stage item
        /// </summary>
        private void PerformMultiLevelStageItemSpecificScanning(bool isDrillDown, string scanCheckKey)
        {
            /*TODO:
             * In the scanning of a multi-level stage item we have a downside in terms of coding and it will be good to find a way around it.
             * The problem is that the two scanning modes that we have (Elimination & Sequential) are updating the ScanBookmark index in different ways
             * and in different locations.
             * 1- Sequential: The sequential mode follows an approach similar to Stages, StageItems, ScanningRounds & DosageOptions because all of these
             * are sequential by nature, basicaly there are 2 steps that each of these follow, first the MoveToNextIndex and second they check if the index IsLast
             * at the end of the method and based on it determine if the method should be called again, basically the index is updated by a call from the form.
             * 2- Elimination: The elimination mode however handles things a bit different, while it is also using the ScanBookmark (Which is a good thing to keep things unified)
             * but it updates the index differently and from a different location, in case of elimination, the index is updated from inside the StageAutoItem and not from the form.
             * It gets updated within method "PerformActionByReading" based on case and also gets reset when the ScanItems collection gets reset and so we can see that the index
             * gets updated in a different way and different location when compared to the behavior in sequential mode and it is better to unfiy the approach so we don't have
             * difficulty understanding or updating it in future
             */

            //Checks the status of the CSA and updates status flags
            //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
            //if hardware faliure was found
            StartHardwareValidationThread();

            var currentReadingValue = 0;
            var isCurrentReadingYes = false;

            //If the scanning type is sequential then perform sequential scanning realted activities
            if (AutoTest.CurrentTestingPathParent.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeSequential)
            {
                //Moves the index of the bookmark
                AutoTest.MoveStepBookmarkNext(ScanBookmarkType.MultiLevelScanning);

                //IMPORTANT: Set the focused row only if there are items to test, this is important to avoid highlighting issues
                /*
                 * We faced an issue where after drilling down abd drilling back on multiple levels the highlighting of the testing items in 1x1 mode failed and never
                 * worked afterwards unless we pause the test and manually select a row, we found that if we adjusted the code in event "gridViewTestingItems_FocusedRowChanged"
                 * to always keep the row colored in orange then this problem would not happen, this lead us to believe that for some reason the focused row handle might have been
                 * set permenanetly to a value less than 0 which kept the style as while row and not orange when it is focused, we tried to avoid this problem by eliminating the case
                 * where the focused row handle could be set to such value and we prevented setting it unless the there are items to scan and this solved the issue.
                 */
                if (AutoTest.CurrentTestingPathParent != null && AutoTest.CurrentTestingPathParent.HasScanItems)
                {
                    var currentStepBookmark = AutoTest.GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning);
                    var focusedRowIndex = currentStepBookmark.IndexValue;

                    //Make sure row is focused only if index is higher than 0
                    if (focusedRowIndex >= 0)
                    {
                        //Make sure that events are ignored because they FocusedRowChanged event will fire again when setting the FocusedRowHandle
                        _isUpdatingUserStageSelection = true;

                        //Highlight the current testing item in its UI
                        gridControlTestingItems.PerformSafely(() => gridViewTestingItems.FocusedRowHandle = focusedRowIndex);
                        gridControlTestingItems.PerformSafely(() => gridControlTestingItems.Refresh());

                        _isUpdatingUserStageSelection = false;

                        //Sets the image and description of the current item
                        var currentAutoItem = AutoTest.GetCurrentScanItem(ScanBookmarkType.MultiLevelScanning).AutoItem;
                        UpdateImageAndDescription(currentAutoItem.Name, currentAutoItem.VisualDescription,currentAutoItem.TestingPoint.Key, currentAutoItem.Image, GetPatientGenderBasedBioDigitalID(currentAutoItem));
                    }
                }
            }

            //Set current scan round title
            barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));

            //Set the current breadcrums text
            UpdateBreadcrumb();

            //In case of simulated readings mode use simulated readings instead of real readings
            if (AutoTest.UseSimulatedReadings)
            {
                Thread.Sleep(_readingStabilityDelay);

                //Here we are checking if the logic should use stablilized readings only, if that is the case, then the logic will keep holding the current
                //thread until a stable reading is rached, otherwise we just use the current reading as it is.
                if (AutoTest.UseStableReadingsOnly)
                {
                    //Here we reset the simulated reading flags to allow the simulated reading generator to start a reading session
                    ResetSimulatedReadingFlags();

                    //Stay in loop until the reading is marked as stabilized
                    while (!AutoTest.IsReadingStable)
                    {
                        Thread.Sleep(_readingStabilityDelay);
                    }
                }

                //Record the value in a local variable because it might change
                currentReadingValue = AutoTest.CurrentReadingValue;
                isCurrentReadingYes = AutoTest.IsCurrentReadingYes;
            }
            else
            {
                //Send command to prototype to start reading
                AutoCsaEmdUnitManagerPhase2.Instance.StartReading();

                //Thread.Sleep(10);

                //Here we are checking if the logic should use stablilized readings only, if that is the case, then the logic will keep holding the current
                //thread until a stable reading is rached, otherwise we just use the current reading as it is.
                if (AutoTest.UseStableReadingsOnly)
                {
                    AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);
                    
                    //Stay in loop until the reading is marked as stabilized
                    while (!AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized)
                    {
                        AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);
                        
                        //AutoCsaEmdUnitManagerPhase2.Instance.StopReading();

                        Thread.Sleep(_readingStabilityDelay);

                        //AutoCsaEmdUnitManagerPhase2.Instance.StartReading();
                    }
                }
                //Record the value in a local variable because it might change
                currentReadingValue = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value;
                isCurrentReadingYes = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.IsBalanced;

                AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);
                
                //AutoCsaEmdUnitManagerPhase2.Instance.StopReading();
            }

            //Temporary Testing Logic
            //if (AutoTest.StageKeyEnum == StageKey.GlandsAndOrgans && AutoTest.CurrentRootStageAutoItem.AutoItem.Name == "Glands")
            //{
            //    if (AutoTest.CurrentRootStageAutoItem.IsTestingOnRoot)
            //    {
            //        var currentResultStageAutoItem = AutoTest.CurrentTestingPathParent.GetCurrentScanItem(ScanBookmarkType.MultiLevelScanning);

            //        isCurrentReadingYes = currentResultStageAutoItem.AutoItem.Name == "Pancreas";
            //    }
            //    else
            //    {
            //        if (AutoTest.CurrentTestingPathParent.AutoItem.Name == "Pancreas")
            //        {
            //            var currentResultStageAutoItem = AutoTest.CurrentTestingPathParent.GetCurrentScanItem(ScanBookmarkType.MultiLevelScanning);

            //            if (AutoTest.CurrentTestingPathParent.ScanItems.Any(item => item.AutoItem.Name == "Metatox" || item.AutoItem.Name == "Sycosis"))
            //            {
            //                isCurrentReadingYes = currentResultStageAutoItem.AutoItem.Name == "Metatox" || currentResultStageAutoItem.AutoItem.Name == "Sycosis";    
            //            }
                        
            //        }
            //        else if (AutoTest.CurrentTestingPathParent.AutoItem.Name == "Metatox" || 
            //                 AutoTest.CurrentTestingPathParent.AutoItem.Name == "Sycosis" ||
            //                 AutoTest.CurrentRootStageAutoItem.TestingPathItems.Any(item => item.AutoItem.Name == "Metatox" || item.AutoItem.Name == "Sycosis"))
            //        {
            //            isCurrentReadingYes = true;
            //        }
            //    }
            //}

            //Perform multi-level stage item scanning action based on the reading value whether it is Yes or No
            var currentScanAction = AutoTest.CurrentTestingPathParent.PerformActionByReading(isCurrentReadingYes, AutoTest.LastTestStatus);

            //Refresh the grid directly after performing the action inside the StageAutoItem because the data shown in UI might have been adjusted
            RefreshStageItemDataBinding(AutoTest.CurrentTestingPathParent);

            AddTemporaryNotesLine(AutoTest.CurrentTestingPathParent.AutoItem.Name + " - Perform Action - " + currentScanAction);

            Thread.Sleep(_multiLevelStageItemDelay);

            //Based on the action taken in the datasource, determine what the UI should do
            switch (currentScanAction)
            {
                case MultiLevelScanAction.Split:
                case MultiLevelScanAction.Switch:
                    //Call same method again because we still need to split or switch based on reading value
                    PerformMultiLevelStageItemSpecificScanning(isDrillDown,scanCheckKey);
                    break;
                case MultiLevelScanAction.MoveNext:
                    PerformMultiLevelStageItemMoveNext(isDrillDown, scanCheckKey);
                    break;
                case MultiLevelScanAction.Skip:
                    //Do Nothing. Only Refresh binding of stage item data because during the skip action the data gets reset
                    break;
                case MultiLevelScanAction.MarkResult:
                    //Because we found a test result then we move the ScanningRound index to the last index to avoid doing further
                    //scanning on the current StageAutoItem
                    //AutoTest.MoveScanBookmarkToLast(ScanBookmarkType.ScanningRounds);

                    //Get the AutoItem from the ScanItem.
                    //Check the scanning type to determine how to get the AutoItem, it should be the first one for Split/Switch case however for the
                    //sequential scanning case we get the AutoItem based on idex
                    var currentResultStageAutoItem = AutoTest.CurrentTestingPathParent.GetCurrentScanItem(ScanBookmarkType.MultiLevelScanning);

                    AddTemporaryNotesLine(AutoTest.CurrentTestingPathParent.AutoItem.Name + " - Found Result : " + currentResultStageAutoItem.AutoItem.Name);

                    //Perform match actions
                    PerformMatchActions(AutoTest.CurrentTestingPathParent, currentResultStageAutoItem, scanCheckKey, 0);

                    //RefreshStageItemDataBinding(AutoTest.CurrentTestingPathParent);

                    //If scanning is finished then we mark the current scan check as finished
                    if (AutoTest.CurrentTestingPathParent.IsScanningFinished)
                    {
                        AutoTest.CurrentTestingPathParent.FinishCheck(scanCheckKey);
                    }
                    else
                    {
                        //This is a very important handling of the scanning behavior after finding a match in the case of sequential scanning type where multiple matches are needed. Previously the system
                        //allowed for only one match within a sequential scanning round and that is wrong because this behavior of one match per scanning round is valid only for Elimination scanning type
                        //because once an item is matched the rest of the items will be filtered already and so there is no value of using the same scanning round and so we end it and move to the next one, in
                        //this case it makes sense to have one match per scanning round and so to have multiple matches we need multiple scanning rounds, however in the case of sequential scanning, when a match is
                        //found, the remaining of the list is still there and so there is no need to end the scanning round and start again if we need multiple matches, we can just continue scanning until the end
                        //of the list is reached. The code below in cooperation with the special handling of the IsScanningFinished flag (In the case of Sequential Scanning) which is used previously makes sure 
                        //that after a match is found that we don't just end scanning and move forward and we don't skip over the next item in list but continue scanning from that next item. This happens because
                        //below using the check we directly call the method PerformMultiLevelStageItemMoveNext and not PerformMultiLevelStageAutoItemCheck, this is important because the method PerformMultiLevelStageItemMoveNext
                        //doesn't call inside it the method BindStageItemData (Called in PerformMultiLevelStageAutoItemCheck) which causes the index to move to next item (Since it calls currentStageAutoItem.SetUpdateScanItems()) which used to cause the system to skip an item
                        //in this special case but it doesn't since we call PerformMultiLevelStageItemMoveNext and not PerformMultiLevelStageAutoItemCheck
                        if (AutoTest.CurrentTestingPathParent.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeSequential)
                        {
                            PerformMultiLevelStageItemMoveNext(isDrillDown, scanCheckKey);
                        }
                        else
                        {
                            PerformMultiLevelStageAutoItemCheck(AutoTest.CurrentTestingPathParent, isDrillDown, scanCheckKey);    
                        }
                    }

                    //If we reach the end of the current scanning round then we need to reset the index so the next scanning round doesn't get affected, if there
                    //is no next scanning round then based on this the index of the scan items will just go to the first index.
                    //AutoTest.ResetStepBookmarkMarker(ScanBookmarkType.MultiLevelScanning);

                    break;
            }
        }

        /// <summary>
        /// Performs the action of moving to next
        /// </summary>
        /// <param name="isDrillDown"></param>
        /// <param name="scanCheckKey"></param>
        private void PerformMultiLevelStageItemMoveNext(bool isDrillDown, string scanCheckKey)
        {
            if (!AutoTest.GetStepScanBookmark(ScanBookmarkType.MultiLevelScanning).IsLast)
            {
                PerformMultiLevelStageItemSpecificScanning(isDrillDown, scanCheckKey);
            }
            else
            {
                //If we reach the end of the current scanning round while scanning with 1x1 mode then we need to reset the index so the next scanning round doesn't get affected, if there
                //is no next scanning round then based on this the index of the scan items will just go to the first index.
                AutoTest.ResetStepBookmarkMarker(ScanBookmarkType.MultiLevelScanning);
            }
        }

        /// <summary>
        /// Adds an auto item as a result regardless if it was added with automation or manually
        /// </summary>
        /// <param name="currentResultStageAutoItem"></param>
        /// <param name="currentReadingValue"></param>
        /// <param name="skipUIRefresh"></param>
        /// <param name="parentResult"></param>
        public AutoResultAddResult AddItemAsResult(StageAutoItem currentResultStageAutoItem, int currentReadingValue, bool skipUIRefresh = false, AutoTestResult parentResult = null)
        {
            //IMPORTANT:
            //We initially wanted to comment this code because there shouldn't be a need for it however an issue happened where a matched
            //item was added twice right after it matched because there is a logic in ScanMultiLevelStageAutoItem that double checks if an item
            //needs to add a result and it depends on the flag HasResult anyway so we decided to keep the check here after all so any place where
            //this is called doesn't cause an error.
            //Validate that the item doesn't already have a result added
            if (currentResultStageAutoItem.HasResult)
            {
                return AutoResultAddResult.HasResultAlready;
            }

            //If the AutoItem is not supposed to add result on match then we return
            if (!currentResultStageAutoItem.AddResultOnMatch)
            {
                return AutoResultAddResult.ResultNotNeeded;
            }

            //IMPORTANT: Duplicates should be ok because the same AutoItem can be added under multiple parents
            //IMPORTANT: We may need more complex logic to prevent duplicates under the same parent
            //Check if the item was added as a result before to avoid adding it as a result multiple times
            //if (AutoTest.TestingResults.Any(result => result.AutoItem != null && 
            //                                          result.AutoItem.Id == currentResultStageAutoItem.AutoItem.Id))
            //{
            //    return false;
            //}

            //Create the test result
            var newAutoTestResult = new AutoTestResult
            {
                StructureId = _sessionSturctureId + 1,

                AutoTest = AutoTest,
                AutoTestResultParent = parentResult ?? (currentResultStageAutoItem.IsRoot ||
                                                        AutoTest.CurrentTestingPathParentWithResult == null ||
                                                        !AutoTest.CurrentTestingPathParentWithResult.HasResult ? null :
                                                        AutoTest.CurrentTestingPathParentWithResult.AutoTestResult),
                AutoItem = currentResultStageAutoItem.AutoItem,
                AutoProtocolStageRevision = AutoTest.CurrentStageRevision,
                PreliminaryReading = currentReadingValue,
                IsAddedManually = currentReadingValue == 0,
                StageAutoItem = currentResultStageAutoItem,//Set the StageAutoItem with the result to link them with each other
                AutoTestResultProducts = new BindingList<AutoTestResultProduct>(),
            };

            _sessionSturctureId += 1;

            //Set the result inside the StageAutoItem to link them with each other
            currentResultStageAutoItem.AutoTestResult = newAutoTestResult;

            //If the result is for a product then generate its AutoTestResultProduct
            if (currentResultStageAutoItem.AutoItem.TypeEnum == AutoItemType.AutoItemTypeProduct)
            {
                //Get the product from the current AutoItem, there should be one product in list
                var product = currentResultStageAutoItem.AutoItem.Products.FirstOrDefault();

                //Create a product test result and add it to collection.
                newAutoTestResult.AutoTestResultProducts.Add(new AutoTestResultProduct
                {
                    AutoTestResult = newAutoTestResult,
                    Price = product == null ? 0 : product.Price,
                    Quantity = 1,
                    IsChecked = true
                });

                //Also generate the dosage stage related data
                //Check if there is a dosage stage
                var dosageStage = AutoTest.GetStageRevisionByKey(StageKey.Dosage);

                //Proceed only if there is a dosage stage and if the product is found
                if (product != null && dosageStage != null)
                {
                    //Indicate DB operation is active to prevent mutli-threading exceptions
                    _dataOperationActive = true;
                    //Generate the Dosage stage StageAutoItem which is used for navigation during scanning
                    dosageStage.AutoProtocolStage.StageAutoItems.Add(
                    new StageAutoItem
                    {
                        AutoProtocolStage = dosageStage.AutoProtocolStage,
                        AutoItem = currentResultStageAutoItem.AutoItem,
                        TestingPoint = currentResultStageAutoItem.TestingPoint,
                        StageAutoItems = new BindingList<StageAutoItem>(),
                        ScanningMethod = UiHelperClass.GetSingleLookupFromCache(LookupTypes.AutoItemScanningMethod, AutoItemScanningMethod.AutoItemScanningMethodNormal),
                        ChildsOrderType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsOrderType, ChildsOrderType.ChildsOrderTypeByOrder),
                        ChildsScanningType = UiHelperClass.GetSingleLookupFromCache(LookupTypes.ChildsScanningType, ChildsScanningType.ChildsScanningTypeNone),
                        DosageOptions = _autoTestDestinationManager.GetDosageOptions(new DosageOptionsFilter { ProductsId = product.Id })//Load the dosage options of all the product forms of a product using ProductId
                    });
                    _dataOperationActive = false;
                }
            }

            //Add the test result to all test results collection and also to TestingResults collection
            //This will make sure the product show up in test results grid and all in Results stage
            TestingTreeList.PerformSafely(() => AutoTest.AutoTestResults.Add(newAutoTestResult));
            TestingTreeList.PerformSafely(() => AutoTest.TestingResults.Add(newAutoTestResult));

            //Add option to skip refreshing which is usefull when adding multiple results at once
            if (!skipUIRefresh)
            {
                TestingTreeList.PerformSafely(() => TestingTreeList.Refresh());
                TestingTreeList.PerformSafely(() => TestingTreeList.ExpandAll());
            }

            return AutoResultAddResult.ResultAdded;
        }

        #endregion

        #region Dosage

        /// <summary>
        /// Performs specific scanning activities for product dosage
        /// </summary>
        private void ScanProductDosage()
        {
            //Avoid scanning if there are no dosage options in the first place
            if (AutoTest.CurrentRootStageAutoItem.DosageOptions.Any())
            {
                //Checks the status of the CSA and updates status flags
                //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
                //if hardware faliure was found
                StartHardwareValidationThread();

                //Move the bookmark to the next index or resume the last index if it was paused
                AutoTest.MoveStepBookmarkNext(ScanBookmarkType.DosageOptions);

                //Get the current step ScanBookmark based on type
                var scanBookmark = AutoTest.GetStepScanBookmark(ScanBookmarkType.DosageOptions);

                AddTemporaryNotesLine(AutoTest.CurrentRootStageAutoItem.AutoItem.Name + " - Dosage Option - " + (scanBookmark.Index + 1));

                //Set current scan round title
                //barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.CurrentScanTitle);
                barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));

                //Set the current breadcrums text
                UpdateBreadcrumb();

                var currentReadingValue = 0;
                var isCurrentReadingYes = false;

                //In case of simulated readings mode use simulated readings instead of real readings
                if (AutoTest.UseSimulatedReadings)
                {
                    //Here we are checking if the logic should use stablilized readings only, if that is the case, then the logic will keep holding the current
                    //thread until a stable reading is rached, otherwise we just use the current reading as it is.
                    if (AutoTest.UseStableReadingsOnly)
                    {
                        //Here we reset the simulated reading flags to allow the simulated reading generator to start a reading session
                        ResetSimulatedReadingFlags();

                        //Stay in loop until the reading is marked as stabilized
                        while (!AutoTest.IsReadingStable)
                        {
                            Thread.Sleep(_readingStabilityDelay);
                        }
                    }

                    //Record the value in a local variable because it might change
                    currentReadingValue = AutoTest.CurrentReadingValue;
                    isCurrentReadingYes = AutoTest.IsCurrentReadingYes;
                }
                else
                {
                    //Send command to prototype to start reading
                    AutoCsaEmdUnitManagerPhase2.Instance.StartReading();
                    
                    //Here we are checking if the logic should use stablilized readings only, if that is the case, then the logic will keep holding the current
                    //thread until a stable reading is rached, otherwise we just use the current reading as it is.
                    if (AutoTest.UseStableReadingsOnly)
                    {
                        //Stay in loop until the reading is marked as stabilized
                        while (!AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized)
                        {
                            AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);

                            //AutoCsaEmdUnitManagerPhase2.Instance.StopReading();

                            Thread.Sleep(_readingStabilityDelay);

                            //AutoCsaEmdUnitManagerPhase2.Instance.StartReading();
                        }
                    }

                    //Record the value in a local variable because it might change
                    currentReadingValue = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value;
                    isCurrentReadingYes = AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.IsBalanced;

                    AddTemporaryNotesLine("Reading : " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Value + " - Is Stable: " + AutoCsaEmdUnitManagerPhase2.Instance.CurrentReading.Stabilized);

                    //AutoCsaEmdUnitManagerPhase2.Instance.StopReading();
                }

                //Get the current product result based on the current index, notice here we get the index of the StageItemBookmark
                var currentProduct = AutoTest.Products[AutoTest.StageItemBookmark.IndexValue];

                //If we received a Yes reading then perform the logic to set the dosage details
                if (isCurrentReadingYes)
                {
                    //Get the current dosage option based on index, notice here was are using the index of the ItemSubItemBookmark
                    var currentDosageOption = AutoTest.CurrentRootStageAutoItem.DosageOptions[scanBookmark.IndexValue];

                    AddTemporaryNotesLine(AutoTest.CurrentRootStageAutoItem.AutoItem.Name + " - Matched Dosage Match - " + currentDosageOption.Name);

                    SetProductDosageDetails(currentProduct, currentDosageOption);

                    //Because we found the dosage option then we move the index of the dosage options to the last,
                    //this will end dosage scanning on the current product and allows us to move to next product
                    //without having to cycle all possible other dosage options which is not necessary
                    AutoTest.MoveScanBookmarkToLast(ScanBookmarkType.DosageOptions);
                }

                //Set current scan round title
                //barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.CurrentScanTitle);
                barManagerMain.Form.PerformSafely(() => barEditItemCurrentTestStage.EditValue = AutoTest.GetCurrentScanInfo(false));

                Thread.Sleep(_productDosageDelay);

                if (!scanBookmark.IsLast)
                {
                    //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
                    //if hardware faliure was found
                    StartHardwareValidationThread();
                    ScanProductDosage();
                }
                else
                {
                    //If the product was fully scanned and not dosage option matched then set the dosage details based on the first dosage option
                    if (currentProduct != null && currentProduct.ProductForm == null)
                    {
                        var firstDosageOption = AutoTest.CurrentRootStageAutoItem.DosageOptions[0];

                        if (firstDosageOption != null)
                        {
                            SetProductDosageDetails(currentProduct, firstDosageOption);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set dosage details for product
        /// </summary>
        /// <param name="currentProduct"></param>
        /// <param name="currentDosageOption"></param>
        private void SetProductDosageDetails(AutoTestResultProduct currentProduct, DosageOption currentDosageOption)
        {
            //Set the dosage details, we are using the PerformSafely action to avoid threading issues
            gridControlDosage.PerformSafely(() => currentProduct.ProductForm = currentDosageOption.ProductForm);
            gridControlDosage.PerformSafely(() => currentProduct.ProductSize = currentDosageOption.ProductForm.ProductSizes.FirstOrDefault());
            gridControlDosage.PerformSafely(() => currentProduct.SuggestedUsage = currentDosageOption.SuggestedUsage);
            gridControlDosage.PerformSafely(() => currentProduct.Schedule = currentDosageOption.UsageSchedule);

            //Bind the dosage grid to reflect the changes done to the data source
            BindDosage();
        }

        #endregion

        #endregion

        #region Multi-Level Scanning

        /// <summary>
        /// Checks a StageAutoItem for all different scanning operations and calls the right logic with the right parameters based on case
        /// </summary>
        /// <param name="parentStageAutoItem"></param>
        /// <param name="isDrillDown"></param>
        /// <param name="scanCheckKey"></param>
        /// <param name="currentReadingValue"></param>
        private void PerformMultiLevelStageAutoItemCheck(StageAutoItem parentStageAutoItem, bool isDrillDown, string scanCheckKey, int currentReadingValue = 0)
        {
            //Get the scan check key value as enum
            var scanCheck = EnumNameResolver.StringAsEnumWithUndefined<ScanCheck>(scanCheckKey);

            //If the special scanning action is checked already then don't proceed
            if (parentStageAutoItem.CanSkipCheck(scanCheckKey))
            {
                return;
            }

            if (scanCheck == ScanCheck.Undefined)
            {
                //If not checking childes, then handle the special scanning check here
                
                //TODO: Investigate adding special check items to cache and load them from cache instead of DB
                //IMPORTANT:
                //Notice below that we are loading the special check StageAutoItem from the database each time and while this doesn't sound like a very good approach in terms of performance
                //it has a major importance on the correctness of the adding results in this case because each time we test this case we get the StageAutoItem fresh from the database and this
                //means that this StageAutoItem has no result in it and so it gets added as a result without any issues or concerns about having a result already even though that we are using
                //the same exact StageAutoItemId all the time but each time we are interacting with a different object even though all the instances we are interacting with have the same exact
                //details but luckily they don't have the same exact shared reference so when adding them as a result it works without issues, now when deleting a result for a special case check
                //there shouldn't be an issue either because the linked StageAutoItem inside the result doesn't actually exist anymore anywhere else other than this result and so this shouldn't
                //conflict with anything else. As you can see if we stop loading the record from database we will start getting a shared reference to a cached version of the entity and this will
                //start causing issues unless we clone the entity completely each time we get a copy.
                //Load the special scanning check data based on case and call "PerformMatchActions" to start scanning on it.
                _dataOperationActive = true;//Indicate DB operation is active to prevent mutli-threading exceptions
                var specialCheckStageAutoItem = _autoTestSourceManager.GetStageAutoItemByAutoItemKey(new AutoItemsFilter { Key = scanCheckKey, LoadingType = LoadingTypeEnum.All });
                _dataOperationActive = false;
                //IMPORTANT: Identification of Direct Access Items
                //The flag below is important to set in the case of scanning an item that is accessed with the direct access method to make them stand out from other items to help
                //handle an important and problematic case when pausing test and resuming. In normal case the system just resumes from the last parent in list which is fine and the parent
                //is removed from scanning path when it finishes scanning and testing resumes normally however in the case of items accessed with direct access approach the system needs to
                //check the state of the DirectAccessScanCheck itself and see if it finished and this status gets set by calling FinishCheck after performing the action for that direct access
                //item, however if the test was paused during the scanning of the direct access item then the system will not get the chance to finish the check and at that point it becomes
                //difficult to finish the check when resuming because we have lost the context of where were we before and so the only way to restore that context is to help identify such items
                //and then add the handling for such case wherever its needed.
                specialCheckStageAutoItem.IsDirectAcessScanCheck = true;

                //Perform match actions on the special scanning check
                PerformMatchActions(parentStageAutoItem, specialCheckStageAutoItem, scanCheckKey,0);

                //Mark the special scanning check as finished. Now if the test was paused before reaching this point and the direct access item wasn't marked as checked
                //then we rely on the flag IsDirectAcessScanCheck to make sure the scanning is marked finished before drilling back to the parent item.
                parentStageAutoItem.FinishCheck(scanCheckKey);
            }
            else if (scanCheck == ScanCheck.AddAllChildesAsResults)
            {
                //Update ScanItems to filter and exclude any items previously added
                parentStageAutoItem.SetUpdateScanItems();

                var lastItem = parentStageAutoItem.ScanItems.LastOrDefault();

                //Add all childes as results
                foreach (var childStageAutoItem in parentStageAutoItem.ScanItems)
                {
                    AddItemAsResult(childStageAutoItem, currentReadingValue, !childStageAutoItem.Equals(lastItem));//Refresh the results only on the last result
                }
                
                //Mark the special scanning check as finished
                parentStageAutoItem.FinishCheck(scanCheckKey);
            }
            else if (scanCheck == ScanCheck.Childes)
            {
                //Set the ScanItems of the current parent, apply filtrations on them and set the data in the testing grid
                BindStageItemData(parentStageAutoItem);

                UpdateStageItemUI();

                //Start multi-level scanning activites on the childes
                PerformMultiLevelStageItemSpecificScanning(isDrillDown, scanCheckKey);
            }
        }

        /// <summary>
        /// Perform set of actions required when finding a match
        /// </summary>
        /// <param name="parentStageAutoItem"></param>
        /// <param name="matchedStageAutoItem"></param>
        /// <param name="scanCheckKey"></param>
        /// <param name="currentReadingValue"></param>
        private void PerformMatchActions(StageAutoItem parentStageAutoItem, StageAutoItem matchedStageAutoItem, string scanCheckKey, int currentReadingValue)
        {
            //Get the scan check key value as enum
            var scanCheck = EnumNameResolver.StringAsEnumWithUndefined<ScanCheck>(scanCheckKey);

            //If a match is found and we are testing on root level and the root StaegAutoItem doesn't have a result then generate one for it
            if (!AutoTest.CurrentRootStageAutoItem.HasResult && AutoTest.CurrentRootStageAutoItem.IsTestingOnRoot)
            {
                AddItemAsResult(AutoTest.CurrentRootStageAutoItem, 0);
            }

            //Handle loading StageAutoItem childes if not loaded already
            PreLoadStageAutoItemChildes(matchedStageAutoItem);

            //If the matched StageAutoItem has further scanning then add it to the testing path, this is the first step of drilling down to this item.
            if (matchedStageAutoItem.HasDrillDownChecks)
            {
                //Add the StageAutoItem to the end of the testing path
                AutoTest.CurrentRootStageAutoItem.DrillDown(matchedStageAutoItem);

                //Update breadcrumb to reflect changes about drilldown
                UpdateBreadcrumb();
            }

            var autoResultAddResult = AutoResultAddResult.ResultNotNeeded;

            //If the matched item has AddResultOnMatch checked then we call logic to add result for it
            if (matchedStageAutoItem.AddResultOnMatch)
            {
                autoResultAddResult = AddItemAsResult(matchedStageAutoItem, currentReadingValue);
            }

            var increaseMatchFirst = false;

            if (matchedStageAutoItem.HasDrillDownChecks)
            {
                switch (autoResultAddResult)
                {
                    case AutoResultAddResult.ResultAdded:
                        increaseMatchFirst = true;
                        //+1
                        break;
                    case AutoResultAddResult.ResultNotNeeded:
                        increaseMatchFirst = false;
                        //Delay
                        break;
                    case AutoResultAddResult.HasResultAlready:
                        increaseMatchFirst = false;
                        //Delay
                        break;
                }
            }
            else
            {
                switch (autoResultAddResult)
                {
                    case AutoResultAddResult.ResultAdded:
                        increaseMatchFirst = true;
                        //+1
                        break;
                    case AutoResultAddResult.ResultNotNeeded:
                        increaseMatchFirst = false;
                        //Delay
                        break;
                    case AutoResultAddResult.HasResultAlready:
                        increaseMatchFirst = false;
                        //Delay (SHOULD NOT HAPPEN)
                        break;
                }
            }

            //Because we are checking the childes and since we found a match then increase the found matches number at the parent
            if (scanCheck == ScanCheck.Childes && increaseMatchFirst)
            {
                parentStageAutoItem.FoundMatchesNumber += 1;
            }

            //If the matched StageAutoItem has further scanning then this is where we start on performing the drilldown processing
            if (matchedStageAutoItem.HasDrillDownChecks)
            {
                var notFirstTimeResult = autoResultAddResult == AutoResultAddResult.HasResultAlready ||
                                         autoResultAddResult == AutoResultAddResult.ResultNotNeeded;

                if (notFirstTimeResult && scanCheck == ScanCheck.Childes && matchedStageAutoItem.CheckChildes)
                {
                    matchedStageAutoItem.ChildesCheck = ScanCheckState.Pending;

                    if (!matchedStageAutoItem.IsMatchesNumberPushedBack)
                    {
                        if (matchedStageAutoItem.MatchesNumberAchieved)
                        {
                            matchedStageAutoItem.FoundMatchesNumber = matchedStageAutoItem.FoundMatchesNumber - 1;
                            matchedStageAutoItem.IsMatchesNumberPushedBack = true;
                        }
                    }

                    matchedStageAutoItem.ResetBookmarkMarker(ScanBookmarkType.ScanningRounds);
                    matchedStageAutoItem.ResetBookmarkMarker(ScanBookmarkType.MultiLevelScanning);
                }

                ScanMutliLevelStageItem(true, currentReadingValue);
            }

            matchedStageAutoItem.IsMatchesNumberPushedBack = false;

            //Because we are checking the childes and since we found a match then increase the found matches number at the parent
            if (!increaseMatchFirst && scanCheck == ScanCheck.Childes)
            {
                parentStageAutoItem.FoundMatchesNumber += 1;
            }

            //At this point the drill down on this item should be done.

            //Update the ScanItems in the parent to make sure we exclude the current match and any other possible matches that we may have found during a drill down and also
            //update the scanning index to account for execluded items.
            //Notice that below we could call the logic and pass "parentStageAutoItem" instead however the TestingPathParent should be the current parent at this point even
            //if we drilled down the current result because the current result should have removed itself after it finished scanning
            BindStageItemData(AutoTest.CurrentTestingPathParent);

            UpdateStageItemUI();

            //If we are scanning the childes and if we can end scanning by matches number then we move the ScanningRound index to the last index to avoid doing further scanning on the current StageAutoItem
            //and so finishing scanning activites on the current parent
            if (scanCheck == ScanCheck.Childes && parentStageAutoItem.CanEndScanningByMatchesNumber)
            {
                parentStageAutoItem.MoveScanBookmarkToLast(ScanBookmarkType.ScanningRounds);
            }
        }

        /// <summary>
        /// Handle loading StageAutoItem childes if not loaded already
        /// </summary>
        /// <param name="stageAutoItem"></param>
        private void PreLoadStageAutoItemChildes(StageAutoItem stageAutoItem)
        {
            //IMPORTANT: Handling loading of StageAutoItem childes
            /*
             * Multiple methods use a StageAutoItem entity to preform scanning activities and the logic in many of these methods depend on the the existance of childes
             * in that StageAutoItem, now the issue is that the StageAutoItem entity being processed doesn't always come from the same place and so it is not always loaded
             * in the same way, sometimes it is the Root StageAutoItem, sometimes it is a match that we just found and sometimes it is a special scan list that we are supposed to
             * test. The difference in calling places comes with a challenge about the childes of a StageAutoItem, in the case of Root they come preloaded, in other cases they are
             * not and so we need to make sure that they are checked and loaded in all cases and this is where the challenge exists, its because the current StageAutoItem can be generated
             * in multiple places and for different reasons and we want to avoid adding logic to each one to preload the childes but at the same time we don't want to put this logic in
             * one place and risk missing some cases. The problem too is that the method StageAutoItem.SetUpdateScanItems can cause a problem if it was called before loading the childes
             * because it can initialize the collection to an empty collection and this causes rest of the logic to think that the childes were loaded and that there are no childes for
             * the current StageAutoItem we are handling.
             * 
             * To solve this, we reviewed the code and tried to find the first use of the childes in different versions of the StageAutoItems and where are they coming from and the conclusion was
             * that regardless of the source, most items we care about are starting to use their childes in method "PerformMatchActions" and this means we can preload the childes in the
             * begining of this method to make sure our testing data is correct. So this leaves us with two cases:
             * 1- Root: Case where childes are already loaded.
             * 2- Found Match or Special Scan(VF,4Factors ...): The childes are loaded in method PerformMatchActions before any logic.
             */
            if (!stageAutoItem.ChildesLoaded)
            {
                //TODO: Here we can add a check for special scan cases and load them from cache instead of database
                _dataOperationActive = true;//Indicate DB operation is active to prevent mutli-threading exceptions
                stageAutoItem.StageAutoItems = _autoTestSourceManager.GetStageAutoItems(new StageAutoItemsFilter { StageAutoItemsParentId = stageAutoItem.Id });
                stageAutoItem.ChildesLoaded = true;
                _dataOperationActive = false;
                
            }
        }

        #endregion

        #region Simulated Readings

        /// <summary>
        /// Resets the current simulated reading session flags
        /// </summary>
        private void ResetSimulatedReadingFlags()
        {
            //Reset simulated readings flags only in case of simulated readings mode
            if (!AutoTest.UseSimulatedReadings) return;

            AutoTest.SessionInProgress = false;
            AutoTest.IsReadingStable = false;
            AutoTest.IsStabilizing = false;
            AutoTest.StabilizationCounter = 0;

            //Reset current reading value
            AutoTest.CurrentReadingValue = 0;
        }

        /// <summary>
        /// Generates simulated reading values
        /// </summary>
        private void SetSimulatedReading()
        {
            //The while loop here has no exit but it will be ended by stopping the caller thread when scanning is done
            while (true)
            {
                if (AutoTest.UseStableReadingsOnly)
                {
                    if (!AutoTest.SessionInProgress)
                    {
                        ResetSimulatedReadingFlags();
                        //Start New Reading Session
                        AutoTest.SessionInProgress = true;

                        var readingRangeNumber = 0;

                        //IMPORTANT:
                        //Hoteys below allow overriding the random reading with a Yes (Using Left Shift) or No (Using Left Control)
                        //As long as the hotkey is pressed the reading should generally be yes or no based on key. Useful for testing.
                        if (Keyboard.IsKeyDown(Key.LeftShift))
                        {
                            readingRangeNumber = 2;
                        }
                        else if (Keyboard.IsKeyDown(Key.LeftCtrl))
                        {
                            readingRangeNumber = 1;
                        }
                        else
                        {
                            if (AutoTest.StageKeyEnum == StageKey.Summary && AutoTest.SimulateBalancedSummary)
                            {
                                readingRangeNumber = 2;
                            }
                            else if (AutoTest.CurrentSessionMin == 0)
                            {
                                readingRangeNumber = _randomReadingGenerator.Next(1, 4);
                            }
                            else if (AutoTest.CurrentSessionMin == 5)
                            {
                                readingRangeNumber = _randomReadingGenerator.Next(2, 4);
                            }
                            else if (AutoTest.CurrentSessionMin == StaticKeys.MeterMinAcceptableReading)
                            {
                                readingRangeNumber = _randomReadingGenerator.Next(0, 2) == 1 ? 1 : 3;
                            }
                            else if (AutoTest.CurrentSessionMin == StaticKeys.MeterMaxAcceptableReading + 1)
                            {
                                readingRangeNumber = _randomReadingGenerator.Next(1, 3);
                            }
                        }
                        
                        if (readingRangeNumber == 1)
                        {
                            AutoTest.CurrentSessionMin = 5;
                            AutoTest.CurrentSessionMax = StaticKeys.MeterMinAcceptableReading;
                        }
                        else if (readingRangeNumber == 2)
                        {
                            AutoTest.CurrentSessionMin = StaticKeys.MeterMinAcceptableReading;
                            AutoTest.CurrentSessionMax = StaticKeys.MeterMaxAcceptableReading + 1;
                        }
                        else if (readingRangeNumber == 3)
                        {
                            AutoTest.CurrentSessionMin = 55;
                            AutoTest.CurrentSessionMax = StaticKeys.MeterMaxAcceptableReading + 1;
                        }

                        AutoTest.CurrentSessionTarget = _randomReadingGenerator.Next(AutoTest.CurrentSessionMin, AutoTest.CurrentSessionMax);

                        AddTemporaryDescriptionLine("Selected Range: " + AutoTest.CurrentSessionMin + " - " + AutoTest.CurrentSessionMax + " : " + AutoTest.CurrentSessionTarget);
                    }

                    var maxIncrement = (AutoTest.CurrentSessionTarget - AutoTest.CurrentReadingValue) / 5;
                    var minIncrement = ((AutoTest.CurrentSessionTarget - AutoTest.CurrentReadingValue) / 10) + 1;

                    maxIncrement = maxIncrement > 1 ? maxIncrement : 1;
                    minIncrement = minIncrement > 1 ? minIncrement : 1;

                    //This check is to prevent cases where the MaxIncrement comes to be less than MinIncrement for some reason
                    if (maxIncrement < minIncrement)
                    {
                        maxIncrement = minIncrement;
                    }

                    //If the reading within the selected range, then start stabilizing it
                    if (AutoTest.CurrentSessionTarget - AutoTest.CurrentReadingValue <= 2)
                    {
                        if (!AutoTest.IsStabilizing)
                        {
                            AutoTest.IsStabilizing = _randomReadingGenerator.Next(0, 2) == 1;
                        }

                        if (AutoTest.IsStabilizing)
                        {
                            if (AutoTest.StabilizationCounter <= 15)
                            {
                                var increment = _randomReadingGenerator.Next(1, 3);

                                AutoTest.CurrentReadingValue += _randomReadingGenerator.Next(0, 2) == 1 ? increment : increment * -1;
                                AutoTest.StabilizationCounter += 1;

                                if (AutoTest.StabilizationCounter >= 5)
                                {
                                    AutoTest.IsReadingStable = true;
                                }
                            }
                            else
                            {
                                ResetSimulatedReadingFlags();
                            }
                        }
                        else
                        {
                            AutoTest.CurrentReadingValue += _randomReadingGenerator.Next(1, _randomReadingGenerator.Next(minIncrement, maxIncrement));
                        }
                    }
                    else if (AutoTest.CurrentSessionTarget > AutoTest.CurrentReadingValue)
                    {
                        AutoTest.CurrentReadingValue += _randomReadingGenerator.Next(1, _randomReadingGenerator.Next(minIncrement, maxIncrement));
                    }

                    if (AutoTest.CurrentReadingValue > AutoTest.CurrentSessionTarget)
                    {
                        //Reset current reading value
                        AutoTest.IsReadingStable = true;
                    }
                }
                else
                {
                    AutoTest.CurrentReadingValue = _randomReadingGenerator.Next(0, 101);
                }

                //Set meter reading value
                SetMeterReadingValue(AutoTest.CurrentReadingValue,false);

                Thread.Sleep(_readingStabilityDelay);

                if (!_readingThread.IsAlive)
                {
                    break;
                }
            }

            //We previously used recursion to keep readings coming but it caused a stack overflow and so we replaced it with a while loop, this is ok because this logic is temporary
            //and will eventually be removed
            //SetSimulatedReading();
        }

        #endregion

        #endregion

        #region Image & Description

        /// <summary>
        /// Updates the image and description UI componenets based on current selected item
        /// </summary>
        private void UpdateImageAndDescription()
        {
            //If the current stage item is not empty then use it to set image and description
            if (AutoTest.CurrentTestingPathParent != null)
            {
                UpdateImageAndDescription(AutoTest.CurrentTestingPathParent.AutoItem.Name,
                                          AutoTest.CurrentTestingPathParent.AutoItem.VisualDescription,
                                          AutoTest.CurrentRootStageAutoItem.TestingPoint.Key,
                                          AutoTest.CurrentTestingPathParent.AutoItem.Image,
                                          GetPatientGenderBasedBioDigitalID(AutoTest.CurrentTestingPathParent.AutoItem));
            }
        }

        /// <summary>
        /// Updates the image and description UI componenets based on current selected item
        /// </summary>
        private void UpdateImageAndDescription(string name,string description, string testingPointKey, Business.Shared.DomainObjects.Images.Image image, string modeIdentifier, bool isClearning = false)
        {
            //Set description
            memoEditItemDescription.PerformSafely(() => memoEditItemDescription.Text = description);

            //We use this variable to make sure the picture is set to null in case the image was null
            //or if the image path wasn't found on disk.
            Image imageData = null;

            if (image != null)
            {
                //Validate that image path exists
                var imagePath = UiHelperClass.ImagesFolderPath + image.Path;

                if (File.Exists(imagePath))
                {
                    //Load the image data
                    imageData = UiHelperClass.LoadImgeWithoutLocking(imagePath);
                }
            }

            barManagerMain.Form.PerformSafely(() => dockPanelImage.Text = name);
            barManagerMain.Form.PerformSafely(() => dockPanelDescription.Text = name);

            //If there is a testing point then set the testing point location on the hand image
            if (!string.IsNullOrEmpty(testingPointKey) && _testingPoints.ContainsKey(testingPointKey))
            {
                //Extract the testing point location on image based on key
                var testingPoint = _testingPoints[testingPointKey];
                var point = testingPoint.Split(',');
                var locationX = int.Parse(point[0]);
                var locationY = int.Parse(point[1]);

                DrawTestingPoint(locationX, locationY);    
            }

            //Set the image in the pictureEdit
            pictureEditImage.PerformSafely(() => pictureEditImage.EditValue = imageData);

            if (!isClearning)
            {
                //Select the image or 3D tab page based on availability of the model identifier
                //Only select the model ID if the 3D viewer is ready and if the item has a model identifier and if the model is fully loaded
                if (_useHumanAnatomyView && //Perform logic only if human anatomy option is enabled
                    xtraUserControlBioDigital3DModelViewerMain.Ready && 
                    _bioDigital3DModel.Status == XtraUserControlBioDigital3DModel.BioDigital3DModelStatus.Ready &&
                    !string.IsNullOrEmpty(modeIdentifier) && _bioDigital3DModel != null)
                {
                    layoutControlImage.PerformSafely(() => tabbedControlGroupImage.SelectedTabPage = layoutControlGroup3D); 
                    xtraUserControlBioDigital3DModelViewerMain.PerformSafely(() => xtraUserControlBioDigital3DModelViewerMain.SelectObject(modeIdentifier));
                }
                else
                {
                    layoutControlImage.PerformSafely(() => tabbedControlGroupImage.SelectedTabPage = layoutControlGroupImage);
                }
            }
        }

        /// <summary>
        /// Sets the image and description based on the selection in the passed view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recordType"></param>
        private void UpdateImageAndDescriptionByView(Object sender, Type recordType)
        {
            //Only perform the action of updating description and image if the current test is not
            //in progress to prevent automation from doing actions twice (Since it already calls logic to set
            //item details)
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress) return;

            var view = (GridView)sender;

            //Make sure the focused row handle belongs to an actual data row.
            if (UiHelperClass.IsClickInRow(view))
            {
                AutoItem currentAutoItem = null;

                //Determine how to extract the AutoItem from the GridView based on the data in the gridview
                if (recordType == typeof (AutoTestResult))
                {
                    currentAutoItem = ((AutoTestResult) view.GetFocusedRow()).AutoItem;
                }
                if (recordType == typeof(StageAutoItem))
                {
                    currentAutoItem = ((StageAutoItem)view.GetFocusedRow()).AutoItem;
                }
                else if (recordType == typeof(AutoTestResultProduct))
                {
                    currentAutoItem = ((AutoTestResultProduct)view.GetFocusedRow()).AutoTestResult.AutoItem;
                }
               
                if (currentAutoItem != null)
                {
                    //Sets the image and description of the current item
                    UpdateImageAndDescription(currentAutoItem.Name, currentAutoItem.VisualDescription,currentAutoItem.TestingPoint.Key, currentAutoItem.Image, GetPatientGenderBasedBioDigitalID(currentAutoItem));
                }
            }
            else
            {
                //If there was no record selected then clear the content to avoid keeping
                //an image/description stuck from last selection, this is important in the case of 
                //deleting records when there are no more records in list, if we don't clear the image/description
                //then they will remain from the last selection even though the record itself isn't there anymore
                ClearImageAndDescription();
            }
        }

        /// <summary>
        /// Sets the image and description based on the selection in the tree list
        /// </summary>
        /// <param name="treeList"></param>
        private void UpdateImageAndDescriptionByTreeList(TreeList treeList)
        {
            //Only perform the action of updating description and image if the current test is not
            //in progress to prevent automation from doing actions twice (Since it already calls logic to set
            //item details)
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress) return;

            //Make sure the focused row handle belongs to an actual data row.
            if (UiHelperClass.IsClickInRow(treeList) && treeList.FocusedNode != null)
            {
                var currentAutoItem = ((AutoTestResult)treeList.GetDataRecordByNode(treeList.FocusedNode)).AutoItem;

                if (currentAutoItem != null)
                {
                    //Sets the image and description of the current item
                    UpdateImageAndDescription(currentAutoItem.Name, currentAutoItem.VisualDescription,currentAutoItem.TestingPoint.Key, currentAutoItem.Image, GetPatientGenderBasedBioDigitalID(currentAutoItem));
                }
            }
            else
            {
                //If there was no record selected then clear the content to avoid keeping
                //an image/description stuck from last selection, this is important in the case of 
                //deleting records when there are no more records in list, if we don't clear the image/description
                //then they will remain from the last selection even though the record itself isn't there anymore
                ClearImageAndDescription();
            }
        }

        /// <summary>
        /// Clears content in the image and description areas
        /// </summary>
        private void ClearImageAndDescription()
        {
            UpdateImageAndDescription(string.Empty,string.Empty, null,null,null,true);
        }

        /// <summary>
        /// Checks if an item has an image
        /// </summary>
        /// <param name="autoItem"></param>
        /// <returns></returns>
        private bool AutoItemHasImage(AutoItem autoItem)
        {
            if (autoItem == null || autoItem.Image == null) return false;

            return File.Exists(UiHelperClass.ImagesFolderPath + autoItem.Image.Path);
        }
        #endregion

        #endregion

        #region Data

        /// <summary>
        /// Init the AutoTest
        /// </summary>
        private void InitializeData()
        {
            //Initialize testing points locations
            _testingPoints = new Dictionary<string, string>
            {
                {"Lymph", "30,355"},
                {"Nerve", "283,168"},
                {"Circulation", "359,177"},
                {"MasterOrgan", "435,237"},
                {"Endocrine", "474,259"}
            };

            _autoTestSourceManager = new AutoTestSourceManager();
            _autoTestDestinationManager = new AutoTestDestinationManager();
            
            if (IsNew)
            {
                //AutoTest = new AutoTest
                //{
                //    UseStableReadingsOnly = true,
                //    Patient = new Patient {FirstName = "Test", LastName = "Patient"},
                //    Name = "Test 1",
                //    TestDate = DateTime.Now,
                //    AutoTestResults = new BindingList<AutoTestResult>()
                //};

                _autoTest.UseStableReadingsOnly = ConfigurationManager.AppSettings[ConfigKeys.UseStableReadingsOnly.ToString()].ToBoolean();
                _autoTest.MutliLevelScanningEliminationRounds = ConfigurationManager.AppSettings[ConfigKeys.MutliLevelScanningEliminationRounds.ToString()].ToInteger();
                _autoTest.UseSimulatedReadings = ConfigurationManager.AppSettings[ConfigKeys.UseSimulatedReadings.ToString()].ToBoolean();
                _autoTest.SimulateBalancedSummary = true;
                _autoTest.TestDate = DateTime.Now;
                _autoTest.AutoProtocolRevision = GetAutoProtocolRevisionForNewTest();
                _autoTest.AutoTestResults = InitializeAutoTestResults();
                //GenerateDummyTest();
            }

            SetSessionStructureId();

            //IMPORTANT: Set the status of the test
            AutoTest.SetTestStatus();
        }

        /// <summary>
        /// Gets the optimal auto protocol revision for the new test.
        /// </summary>
        /// <returns></returns>
        private AutoProtocolRevision GetAutoProtocolRevisionForNewTest()
        {
            //Indicate DB operation is active to prevent mutli-threading exceptions
            _dataOperationActive = true;
            
            // 1. Get the default protocol from the DB. We need to change this when we ask the user to select the protocol in future.
            var defaultProtocol = _autoTestSourceManager.GetAutoProtocols(new AutoProtocolsFilter { IsDefaultProtocol = true }).FirstOrDefault();

            if (defaultProtocol == null)
                return null;

            // 2. Get the last protocol revision that belong to the selected protocol.
            var latestProtocolRevision =
                _autoTestSourceManager.GetAutoProtocolRevisions(new AutoProtocolRevisionsFilter
                {
                    AutoProtocolsId = defaultProtocol.Id
                }).OrderBy(r => r.CreationDateTime).LastOrDefault();

            _dataOperationActive = false;

            return latestProtocolRevision;
        }

        /// <summary>
        /// Creates the auto test results collection for new auto test and validates if the results for preliminary/summary stages should be generated.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoTestResult> InitializeAutoTestResults()
        {
            //Check if the current test hsa a perliminary stage so we can generate its results
            if (AutoTest.TestHasStage(StageKey.Preliminary))
            {
                var preliminaryStageRevisions = AutoTest.AutoProtocolRevision.AutoProtocolStageRevisions.Where(r => r.AutoTestStage.StageKeyEnum == StageKey.Preliminary).ToList();

                // 2. Generate an AutoTestResult for each autoTestItem inside the preliminary stage, we use this to bind both of the preliminary and summary stages UI.
                var preliminarySummaryResults = new List<AutoTestResult>();

                preliminaryStageRevisions.ForEach(stageRevision => preliminarySummaryResults.AddRange(stageRevision.AutoProtocolStage.StageAutoItems.Select(si => new AutoTestResult
                {
                    AutoItem = si.AutoItem,
                    StageAutoItem = si,//IMPORTANT: The StageAutoItem here is used to access the testing point later
                    AutoProtocolStageRevision = stageRevision,
                    AutoTestResultProducts = new BindingList<AutoTestResultProduct>()
                })));


                return new BindingList<AutoTestResult>(preliminarySummaryResults);
            }

            //If there is no preliminary stage then just return empty collection.
            return new BindingList<AutoTestResult>();
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        private void SetupHandlers()
        {
            AutoTest.PropertyChanged += AutoTest_PropertyChanged;
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        private void ClearHandlers()
        {
            AutoTest.PropertyChanged -= AutoTest_PropertyChanged;
        }

        /// <summary>
        /// Delete result record
        /// </summary>
        /// <param name="selectedResult"></param>
        /// <param name="refreshUI"></param>
        private void DeleteResultRecord(AutoTestResult selectedResult, bool refreshUI = true)
        {
            if (selectedResult != null)
            {
                //Find the node matching the result before it gets deleted
                var resultNode = refreshUI ? TestingTreeList.FindNodeByKeyID(selectedResult.StructureId) : null;

                //If UI should be refreshed then move the selection off the current node since it will be deleted
                if (refreshUI)
                {
                    //If you can move next then do so, otherwise move to previous node.
                    if (resultNode.NextVisibleNode != null)
                    {
                        TestingTreeList.PerformSafely(() => TestingTreeList.MoveNextVisible());
                    }
                    else
                    {
                        TestingTreeList.PerformSafely(() => TestingTreeList.MovePrevVisible());
                    }
                }

                selectedResult.ObjectState = DomainEntityState.Deleted;//Set status as deleted so record is removed during saving.
                selectedResult.IsDeleted = true;//IMPORTANT: This is not really needed but we use it to notify test about the change and set the test status to Modified

                //IMPORTANT: We set the result back to null or otherwise the connected StageAutoItem will still think it has a result    
                //We only do this if the selected result has a StageAutoItem because when results are loaded from DB then they have no linked StageAutoItem
                if (selectedResult.StageAutoItem != null)
                {
                    selectedResult.StageAutoItem.AutoTestResult = null;
                }

                //If the result is for a product then we delete the Product record from the AutoTest.Products collection, notice that we keep
                //the product result in the selectedResult.AutoTestResultProducts and this is because we want the save logic to remove it when saving.
                if (selectedResult.AutoItem.TypeEnum == AutoItemType.AutoItemTypeProduct && selectedResult.AutoTestResultProducts.Any())
                {
                    //Remove product from products list
                    AutoTest.Products.Remove(selectedResult.AutoTestResultProducts.FirstOrDefault());
                }

                var childResults = AutoTest.AutoTestResults.Where(result => result.StructureParentId == selectedResult.StructureId).ToList();

                //Delete any child results too
                //Loop the childes with for instead of foreach to avoid list enumeration issue when list is modified
                for (int i = 0; i < childResults.Count(); i++)
                {
                    var childResult = childResults[i];
                    DeleteResultRecord(childResult, false);
                }

                if (selectedResult.Id == 0)
                {
                    TestingTreeList.PerformSafely(() => AutoTest.AutoTestResults.Remove(selectedResult));
                    TestingTreeList.PerformSafely(() => AutoTest.TestingResults.Remove(selectedResult));
                }

                if (refreshUI)
                {
                    //Refresh dosage grid
                    UiHelperClass.RefreshGridData(gridControlDosage);

                    //Refresh testing results treelist
                    UiHelperClass.RefreshTreeListData(TestingTreeList);
                    
                    //Refresh all results grid
                    UiHelperClass.RefreshTreeListData(AllResultsTreeList);

                    //if (nodeToFocus != null && nodeToFocus.Visible)
                    //{
                    //    TestingTreeList.SetFocusedNode(nodeToFocus);
                    //}

                    //IMPORTANT: THIS IS NEEDED TO MAKE SURE ANY ITEM WE DELETE SHOWS UP BACK IN TESTING LIST AND DOESN'T REMAIN FILTERED OUT
                    BindStageItemData(AutoTest.CurrentTestingPathParent);
                }
            }
        }

        /// <summary>
        /// Delete Result
        /// </summary>
        private void DeleteTestingResult()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected result and any descendant results will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var selectedResult = TestingTreeList.GetDataRecordByNode(TestingTreeList.FocusedNode) as AutoTestResult;

                    if (selectedResult != null)
                    {
                        DeleteResultRecord(selectedResult);

                        //Update image and description based on selection
                        UpdateImageAndDescriptionByTreeList(TestingTreeList);    
                    }
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Delete Result
        /// </summary>
        private void DeleteResult()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected result will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var selectedResult = AllResultsTreeList.GetDataRecordByNode(AllResultsTreeList.FocusedNode) as AutoTestResult;

                    DeleteResultRecord(selectedResult);

                    //Update image and description based on selection
                    UpdateImageAndDescriptionByTreeList(AllResultsTreeList);    
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        private void DeleteProduct()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected product and its result will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var selectedProduct = gridViewlDosage.GetFocusedRow() as AutoTestResultProduct;

                    if (selectedProduct != null && selectedProduct.Id > 0)
                    {
                        var selectedResult = selectedProduct.AutoTestResult;

                        DeleteResultRecord(selectedResult);

                        //Update image and description based on selection
                        UpdateImageAndDescriptionByView(gridViewlDosage, typeof(AutoTestResultProduct));
                    }
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Marks the user selected testing item as a test result
        /// </summary>
        private void MarkTestingItemAsResult(StageAutoItem currentTestingItem, AutoTestResult parentResult)
        {
            if (currentTestingItem != null && currentTestingItem.AutoItem != null)
            {
                //If a match is found and we are testing on root level and the root StaegAutoItem doesn't have a result then generate one for it
                //if (!AutoTest.CurrentRootStageAutoItem.HasResult && AutoTest.CurrentRootStageAutoItem.IsTestingOnRoot)
                //{
                //    AddItemAsResult(AutoTest.CurrentRootStageAutoItem, 0);
                //}

                bool addResult;

                if (parentResult != null)
                {
                    addResult = !AutoTest.AutoTestResults.Any(result => result.StructureParentId != null &&
                                                                        result.StructureParentId == parentResult.StructureId &&
                                                                        result.AutoItem.Id == currentTestingItem.AutoItem.Id);
                }
                else
                {
                    addResult = AutoTest.AutoTestResults.Where(result => result.StructureParentId == null)
                                                        .All(result => result.AutoItem.Id != currentTestingItem.AutoItem.Id);
                }

                if (addResult)
                {
                    AddItemAsResult(currentTestingItem, 0, false, parentResult);
                }

                if (addResult)
                {
                    //If the item was successfully added as a result then focus the last added result
                    //TestingTreeList.MoveLast();
                    BindStageItemData(AutoTest.CurrentTestingPathParent);
                    
                    //If the added item is a product then update the dosage list
                    if (currentTestingItem.AutoItem.TypeEnum == AutoItemType.AutoItemTypeProduct)
                    {
                        //Override the product list again to make sure the added product shows up in dosage screen, this action isn't needed during scanning
                        //because during scanning the product added isn't shown in the grid yet because the dosage stage tab isn't selected however in this case the user
                        //can switch to dosage screen right away and without the logic below the newly added product won't show up.
                        AutoTest.UpdateProducts();

                        //Rebind the dosage grid to show the updated datasource
                        BindDosage();
                    }
                }

                //Focus the added/found result
                var existingResult = AutoTest.TestingResults.FirstOrDefault(result => result.AutoItem.Id == currentTestingItem.AutoItem.Id);

                if (existingResult != null)
                {
                    if (!addResult)
                    {
                        //If the result wasn't added then we need to restore it since it may have been removed then added again
                        //Restore the object state to make sure it is not deleted
                        existingResult.ObjectState = DomainEntityState.Modified;
                        existingResult.IsDeleted = false;

                        //Linke the restored result with its StageAutoItem if any
                        if (existingResult.StageAutoItem != null)
                        {
                            existingResult.StageAutoItem.AutoTestResult = existingResult;
                        }

                        //if the result is a product then add the product again to the list
                        if (existingResult.AutoItem.TypeEnum == AutoItemType.AutoItemTypeProduct && existingResult.AutoTestResultProducts.Any())
                        {
                            AutoTest.Products.Add(existingResult.AutoTestResultProducts.FirstOrDefault());
                        }
                    }

                    var resultNode = TestingTreeList.FindNodeByKeyID(existingResult.StructureId);

                    if (resultNode != null && resultNode.Visible)
                    {
                        TestingTreeList.SetFocusedNode(resultNode);
                    }
                }
            }
        }

        /// <summary>
        /// Logic to add a test result by search
        /// </summary>
        private void AddResultBySearch(bool isInAllResultsStage)
        {
            var xtraFormAddAutoResult = new XtraFormAddAutoResult();

            xtraFormAddAutoResult.ShowDialog();

            if (xtraFormAddAutoResult.DialogResult == DialogResult.Yes)
            {
                //Identify the selected result by loading it from the correct user control instance
                var selectedResult =  isInAllResultsStage? (!xtraUserControlAutoTestResultsAllResults.LastClickInRow ? null : AllResultsTreeList.GetDataRecordByNode(AllResultsTreeList.FocusedNode) as AutoTestResult) :
                                                           (!xtraUserControlAutoTestResultsTesting.LastClickInRow ? null : TestingTreeList.GetDataRecordByNode(TestingTreeList.FocusedNode) as AutoTestResult);

                //Add all checked items in case there are multiple
                foreach (var checkedAutoItem in xtraFormAddAutoResult.CheckedItems)
                {
                    //Create a temporary StageAutoItem since the result addition requires a StageAutoItem
                    var temporaryStageAutoItem = new StageAutoItem
                    {
                        AutoItem = checkedAutoItem,
                        TestingPoint = checkedAutoItem.TestingPoint
                    };

                    MarkTestingItemAsResult(temporaryStageAutoItem, selectedResult);
                }
            }
        }

        /// <summary>
        /// Sets the structure Id for the current session
        /// </summary>
        private void SetSessionStructureId()
        {
            if (IsNew)
            {
                _sessionSturctureId = 0;
            }
            else
            {
                if (AutoTest.AutoTestResults != null && AutoTest.AutoTestResults.Any())
                {
                    _sessionSturctureId = AutoTest.AutoTestResults.Max(result => result.Id);
                }

            }
        }

        /// <summary>
        /// Compares the gender of the client against the gender of the item to determine the returned value for BioDigitalID
        /// </summary>
        /// <param name="autoItem"></param>
        /// <returns></returns>
        private string GetPatientGenderBasedBioDigitalID(AutoItem autoItem)
        {
            return autoItem.GenderEnum == ItemGender.ItemGenderBoth || 
                    (autoItem.GenderEnum == ItemGender.ItemGenderMale &&  AutoTest.Patient.GenderEnum == GenderEnum.Male) ||
                    (autoItem.GenderEnum == ItemGender.ItemGenderFemale && AutoTest.Patient.GenderEnum == GenderEnum.Female)
                            ? autoItem.ModelIdentifier
                            : null;
        }

        private void GenerateDummyTest()
        {
            //var pointLookup = new Lookup { Value = "Point", Key = "AutoItemTypePoint" };
            //var glandLookup = new Lookup { Value = "Gland", Key = "AutoItemTypeGland" };
            //var organLookup = new Lookup { Value = "Organ", Key = "AutoItemTypeOrgan" };
            //var productLookup = new Lookup { Value = "Product", Key = "AutoItemTypeProduct" };
            ////var productCategoryLookup = new Lookup { Value = "Product Category", Key = "ProductCategory" };

            //var caps = new ProductForm { Form = "Caps" };
            //var tabs = new ProductForm { Form = "Tabs" };
            //var liquid = new ProductForm { Form = "Liquid" };
            //var pills = new ProductForm { Form = "Pills" };

            //var s20 = new ProductSize { Size = "20" };
            //var s10 = new ProductSize { Size = "10" };
            //var s95 = new ProductSize { Size = "95 ML" };
            //var s40 = new ProductSize { Size = "40" };
            //var s80 = new ProductSize { Size = "80" };

            //var rand = new Random();

            //AutoTest = new AutoTest
            //{
            //    Patient = new Patient { FirstName = "Test", LastName = "Patient" },
            //    Name = "Test 1",
            //    TestDate = DateTime.Now,
            //    AutoTestResults = new BindingList<AutoTestResult>
            //    {
            //        //Points
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Lymph",Type = pointLookup}, PreliminaryReading = 0, SummaryReading = 0, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Nerve",Type = pointLookup}, PreliminaryReading = 0, SummaryReading = 0, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Circulation",Type = pointLookup}, PreliminaryReading = 0, SummaryReading = 0, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Master Organ",Type = pointLookup}, PreliminaryReading = 0, SummaryReading = 0, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Endocrine",Type = pointLookup}, PreliminaryReading = 0, SummaryReading = 0, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},

            //        //Issues
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Gall Bladder",Type = organLookup}, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Hypothalamus",Type = glandLookup}, AutoTestResultProducts = new BindingList<AutoTestResultProduct>()},

            //        //Products
            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Infection B",Type = productLookup},
            //                             AutoTestResultProducts = new BindingList<AutoTestResultProduct> { new AutoTestResultProduct
            //                             {
            //                                Quantity = 1,Price = rand.Next(10,200),IsChecked = true,
            //                                ProductForm = caps,ProductSize = s20,
            //                                Duration = "3X Weekly",Schedule = "Before Breakfast",SuggestedUsage = "As prescribed"}}},

            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Tea Tree Essential Oil",Type = productLookup},
            //                             AutoTestResultProducts = new BindingList<AutoTestResultProduct> { new AutoTestResultProduct
            //                             {
            //                                Quantity = 1,Price = rand.Next(10,200),IsChecked = true,
            //                                ProductForm = tabs,ProductSize = s10,
            //                                Duration = "2X Weekly",Schedule = "After Breakfast",SuggestedUsage = "As prescribed"}}},

            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Adrena-Plus",Type = productLookup},
            //                             AutoTestResultProducts = new BindingList<AutoTestResultProduct> { new AutoTestResultProduct
            //                             {
            //                                Quantity = 1,Price = rand.Next(10,200),IsChecked = true,
            //                                ProductForm = liquid,ProductSize = s95,
            //                                Duration = "3X Daily",Schedule = "Before Lunch",SuggestedUsage = "As prescribed"}}},

            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Red Chestnut",Type = productLookup},
            //                             AutoTestResultProducts = new BindingList<AutoTestResultProduct> { new AutoTestResultProduct
            //                             {
            //                                Quantity = 1,Price = rand.Next(10,200),IsChecked = true,
            //                                ProductForm = pills,ProductSize = s40,
            //                                Duration = "8X Weekly",Schedule = "Before Dinner",SuggestedUsage = "As prescribed"}}},

            //        new AutoTestResult { AutoItem = new AutoItem {Name = "Immu Plus",Type = productLookup},
            //                             AutoTestResultProducts = new BindingList<AutoTestResultProduct> { new AutoTestResultProduct
            //                             {
            //                                Quantity = 1,Price = rand.Next(10,200),IsChecked = true,
            //                                ProductForm = caps,ProductSize = s80,
            //                                Duration = "3X Weekly",Schedule = "Between Meals Early",SuggestedUsage = "As prescribed"}}}
                    

            //    },
            //    AutoProtocolRevision = new AutoProtocolRevision
            //    {
            //        Name = "Basic Protocol",
            //        AutoProtocolStageRevisions = new BindingList<AutoProtocolStageRevision>
            //        {
            //            new AutoProtocolStageRevision
            //            {
            //                Order = 1,
            //                AutoTestStage = new AutoTestStage {Name = "Preliminary", Key = "Preliminary", StageTabKey = "PreliminarySummary", IsMultiLevel = false, IsDestinationOnly = false},
            //                AutoProtocolStage = new AutoProtocolStage
            //                {
            //                    StageAutoItems = new BindingList<StageAutoItem>
            //                    {
            //                        new StageAutoItem {Order = 1, AutoItem = new AutoItem {Name = "Lymph"}},
            //                        new StageAutoItem {Order = 2, AutoItem = new AutoItem {Name = "Nerve"}},
            //                        new StageAutoItem {Order = 3, AutoItem = new AutoItem {Name = "Circulation"}},
            //                        new StageAutoItem {Order = 4, AutoItem = new AutoItem {Name = "Master Organ"}},
            //                        new StageAutoItem {Order = 5, AutoItem = new AutoItem {Name = "Endocrine"}},
            //                    }
            //                }
            //            },
            //            new AutoProtocolStageRevision
            //            {
            //                Order = 2,
            //                AutoTestStage = new AutoTestStage {Name = "Glands & Organs", Key = "GlandsAndOrgans", StageTabKey = "Testing", IsMultiLevel = true, IsDestinationOnly = false},
            //                AutoProtocolStage = new AutoProtocolStage
            //                {
            //                    StageAutoItems = new BindingList<StageAutoItem>
            //                    {
            //                        new StageAutoItem
            //                        {
            //                            Order = 1, AutoItem = new AutoItem {Name = "Organs"},
            //                            ScansNumber = 4,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "Appendix"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "Brain"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Colon"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Gall Bladder"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Heart"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "Kidneys"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Liver"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "Lungs"}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Pancreas"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "Skin"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Small Intestine"}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "Spleen"}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Stomach"}},
            //                                new StageAutoItem { Order = 14, AutoItem = new AutoItem {Name = "Urinary Bladder"}}
            //                            }
            //                        },
            //                        new StageAutoItem
            //                        {
            //                            Order = 2, AutoItem = new AutoItem {Name = "Glands"},
            //                            ScansNumber = 2,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "Adrenal"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "Gonad"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Hypothalamus"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Ovaries"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Pancreas"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "Parathyroid"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Pineal"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "Pituitary"}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Prostate"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "Testes"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Thymus"}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "Thyroid"}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Uterus"}}
            //                            }
            //                        }
            //                    }
            //                }
            //            },
            //            new AutoProtocolStageRevision
            //            {
            //                Order = 3,
            //                AutoTestStage = new AutoTestStage {Name = "Testing", Key = "Testing", StageTabKey = "Testing", IsMultiLevel = true, IsDestinationOnly = false},
            //                AutoProtocolStage = new AutoProtocolStage
            //                {
            //                    StageAutoItems = new BindingList<StageAutoItem>
            //                    {
            //                        new StageAutoItem 
            //                        {
            //                            Order = 1, AutoItem = new AutoItem {Name = "Detox"},
            //                            ScansNumber = 5,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "Candidiasis"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "Carcinosii Miasm"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Chemtox"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Cosmetox"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Femtox"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "Free Radicals"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Geotox"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "H-S Formula"}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Infection"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "Infection B"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Infection E.B."}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "Infection E.G.A."}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Infection V"}},
            //                                new StageAutoItem { Order = 14, AutoItem = new AutoItem {Name = "Allertox I"}},
            //                                new StageAutoItem { Order = 15, AutoItem = new AutoItem {Name = "Allertox II"}},
            //                                new StageAutoItem { Order = 16, AutoItem = new AutoItem {Name = "Amalgatox"}},
            //                                new StageAutoItem { Order = 17, AutoItem = new AutoItem {Name = "Anti-Stress"}},
            //                                new StageAutoItem { Order = 18, AutoItem = new AutoItem {Name = "ASP"}},
            //                                new StageAutoItem { Order = 19, AutoItem = new AutoItem {Name = "Aspartox"}},
            //                                new StageAutoItem { Order = 20, AutoItem = new AutoItem {Name = "Lyme Disease"}},
            //                                new StageAutoItem { Order = 21, AutoItem = new AutoItem {Name = "Metatox"}},
            //                                new StageAutoItem { Order = 22, AutoItem = new AutoItem {Name = "Nicotox"}},
            //                                new StageAutoItem { Order = 23, AutoItem = new AutoItem {Name = "Pestox"}},
            //                                new StageAutoItem { Order = 24, AutoItem = new AutoItem {Name = "Psorinum"}},
            //                                new StageAutoItem { Order = 25, AutoItem = new AutoItem {Name = "Radiation"}},
            //                                new StageAutoItem { Order = 26, AutoItem = new AutoItem {Name = "Silicone"}},
            //                                new StageAutoItem { Order = 27, AutoItem = new AutoItem {Name = "Sycosis"}},
            //                                new StageAutoItem { Order = 28, AutoItem = new AutoItem {Name = "Syphilinum/Sycosis"}},
            //                                new StageAutoItem { Order = 29, AutoItem = new AutoItem {Name = "Syphilinum"}},
            //                                new StageAutoItem { Order = 30, AutoItem = new AutoItem {Name = "Vaccination"}},
            //                                new StageAutoItem { Order = 31, AutoItem = new AutoItem {Name = "Vermatox"}},
            //                                new StageAutoItem { Order = 32, AutoItem = new AutoItem {Name = "H-S Formula II"}},
            //                                new StageAutoItem { Order = 33, AutoItem = new AutoItem {Name = "Infection B II"}},
            //                                new StageAutoItem { Order = 34, AutoItem = new AutoItem {Name = "Infection II"}},
            //                                new StageAutoItem { Order = 35, AutoItem = new AutoItem {Name = "Infection V II"}},
            //                                new StageAutoItem { Order = 36, AutoItem = new AutoItem {Name = "Infection E.B. II"}},
            //                                new StageAutoItem { Order = 37, AutoItem = new AutoItem {Name = "Infection E.G.A. II"}},
            //                                new StageAutoItem { Order = 38, AutoItem = new AutoItem {Name = "IMMU-FLU"}},
            //                                new StageAutoItem { Order = 39, AutoItem = new AutoItem {Name = "Vermatox II"}},
            //                                new StageAutoItem { Order = 40, AutoItem = new AutoItem {Name = "ADD/ADHD"}},
            //                                new StageAutoItem { Order = 41, AutoItem = new AutoItem {Name = "Infection E.G.A. III"}},
            //                                new StageAutoItem { Order = 42, AutoItem = new AutoItem {Name = "Para-Trav"}},
            //                                new StageAutoItem { Order = 43, AutoItem = new AutoItem {Name = "ASP II"}},
            //                                new StageAutoItem { Order = 44, AutoItem = new AutoItem {Name = "Alrgy/Sin"}},
            //                                new StageAutoItem { Order = 45, AutoItem = new AutoItem {Name = "Vermatox III"}},
            //                                new StageAutoItem { Order = 46, AutoItem = new AutoItem {Name = "Mold X"}}
            //                            }
            //                        },
            //                        new StageAutoItem
            //                        {
            //                            Order = 2, AutoItem = new AutoItem {Name = "Herbs"},
            //                            ScansNumber = 1,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "Burdock-plus"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "Cat`s Claw"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Chestnut-plus"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Chrysanthemum Complex"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Cohosh-plus"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "Dandi-plus"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Echinacea Complex"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "Echinacea-plus"}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Equista-plus"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "Ginger-plus I"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Ginkgo Complex"}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "Ginseng-plus"}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Gota-plus"}},
            //                                new StageAutoItem { Order = 14, AutoItem = new AutoItem {Name = "Hawthorne-plus"}},
            //                                new StageAutoItem { Order = 15, AutoItem = new AutoItem {Name = "Hydrangea-plus"}},
            //                                new StageAutoItem { Order = 16, AutoItem = new AutoItem {Name = "Juni-plus"}},
            //                                new StageAutoItem { Order = 17, AutoItem = new AutoItem {Name = "Kelp Complex"}},
            //                                new StageAutoItem { Order = 18, AutoItem = new AutoItem {Name = "Lapacho-plus"}},
            //                                new StageAutoItem { Order = 19, AutoItem = new AutoItem {Name = "Licro-plus"}},
            //                                new StageAutoItem { Order = 20, AutoItem = new AutoItem {Name = "Apizelen-plus"}},
            //                                new StageAutoItem { Order = 21, AutoItem = new AutoItem {Name = "Arnica-plus"}},
            //                                new StageAutoItem { Order = 22, AutoItem = new AutoItem {Name = "Black Radish Complex"}},
            //                                new StageAutoItem { Order = 23, AutoItem = new AutoItem {Name = "Lymph/Mammary"}},
            //                                new StageAutoItem { Order = 24, AutoItem = new AutoItem {Name = "Lymphatic Drainer"}},
            //                                new StageAutoItem { Order = 25, AutoItem = new AutoItem {Name = "Lymphatic Immune"}},
            //                                new StageAutoItem { Order = 26, AutoItem = new AutoItem {Name = "Lymphatic Vascular Draine"}},
            //                                new StageAutoItem { Order = 27, AutoItem = new AutoItem {Name = "Mullein-plus"}},
            //                                new StageAutoItem { Order = 28, AutoItem = new AutoItem {Name = "Myrrh-plus"}},
            //                                new StageAutoItem { Order = 29, AutoItem = new AutoItem {Name = "Nettle-plus"}},
            //                                new StageAutoItem { Order = 30, AutoItem = new AutoItem {Name = "Otalga-plus"}},
            //                                new StageAutoItem { Order = 31, AutoItem = new AutoItem {Name = "Plantago Complex"}},
            //                                new StageAutoItem { Order = 32, AutoItem = new AutoItem {Name = "Scrofulara-plus"}},
            //                                new StageAutoItem { Order = 33, AutoItem = new AutoItem {Name = "Solidago-plus"}},
            //                                new StageAutoItem { Order = 34, AutoItem = new AutoItem {Name = "St. John`s-plus"}},
            //                                new StageAutoItem { Order = 35, AutoItem = new AutoItem {Name = "Trifolo-plus"}},
            //                                new StageAutoItem { Order = 36, AutoItem = new AutoItem {Name = "Una De Gato cat's Claw"}},
            //                                new StageAutoItem { Order = 37, AutoItem = new AutoItem {Name = "Universal Complex"}},
            //                                new StageAutoItem { Order = 38, AutoItem = new AutoItem {Name = "Valeri-plus"}},
            //                                new StageAutoItem { Order = 39, AutoItem = new AutoItem {Name = "Verma-plus"}},
            //                                new StageAutoItem { Order = 40, AutoItem = new AutoItem {Name = "Yarrow-plus"}},
            //                                new StageAutoItem { Order = 41, AutoItem = new AutoItem {Name = "KLS-Plus"}},
            //                                new StageAutoItem { Order = 42, AutoItem = new AutoItem {Name = "Gravi-Plus"}},
            //                                new StageAutoItem { Order = 43, AutoItem = new AutoItem {Name = "Female Formula"}},
            //                                new StageAutoItem { Order = 44, AutoItem = new AutoItem {Name = "Female Balance"}},
            //                                new StageAutoItem { Order = 45, AutoItem = new AutoItem {Name = "K/B-Plus"}},
            //                                new StageAutoItem { Order = 46, AutoItem = new AutoItem {Name = "Heart Formula"}},
            //                                new StageAutoItem { Order = 47, AutoItem = new AutoItem {Name = "L/GB-AP"}},
            //                                new StageAutoItem { Order = 48, AutoItem = new AutoItem {Name = "Lobelia"}},
            //                                new StageAutoItem { Order = 49, AutoItem = new AutoItem {Name = "Male  Formula"}},
            //                                new StageAutoItem { Order = 50, AutoItem = new AutoItem {Name = "Nerve Formula"}},
            //                                new StageAutoItem { Order = 51, AutoItem = new AutoItem {Name = "Brain Formula"}},
            //                                new StageAutoItem { Order = 52, AutoItem = new AutoItem {Name = "Eyebright Plus"}},
            //                                new StageAutoItem { Order = 53, AutoItem = new AutoItem {Name = "Immu Boost"}},
            //                                new StageAutoItem { Order = 54, AutoItem = new AutoItem {Name = "Drainage Plus"}},
            //                                new StageAutoItem { Order = 55, AutoItem = new AutoItem {Name = "Herbal and Mineral Com"}},
            //                                new StageAutoItem { Order = 56, AutoItem = new AutoItem {Name = "Cough Syrup"}},
            //                                new StageAutoItem { Order = 57, AutoItem = new AutoItem {Name = "Palmetto Plus"}},
            //                                new StageAutoItem { Order = 58, AutoItem = new AutoItem {Name = "Red Raspberry"}},
            //                                new StageAutoItem { Order = 59, AutoItem = new AutoItem {Name = "Sutherlandia"}},
            //                                new StageAutoItem { Order = 60, AutoItem = new AutoItem {Name = "Tea Tree Essential Oil"}},
            //                                new StageAutoItem { Order = 61, AutoItem = new AutoItem {Name = "Ginger-Plus II"}},
            //                                new StageAutoItem { Order = 62, AutoItem = new AutoItem {Name = "Arco Plus"}},
            //                                new StageAutoItem { Order = 63, AutoItem = new AutoItem {Name = "E Plus"}},
            //                                new StageAutoItem { Order = 64, AutoItem = new AutoItem {Name = "Metaba Plus"}},
            //                                new StageAutoItem { Order = 65, AutoItem = new AutoItem {Name = "R Plus"}},
            //                                new StageAutoItem { Order = 66, AutoItem = new AutoItem {Name = "Vita-Green"}},
            //                                new StageAutoItem { Order = 67, AutoItem = new AutoItem {Name = "KLS Enviro"}},
            //                                new StageAutoItem { Order = 68, AutoItem = new AutoItem {Name = "SEC Greens"}},
            //                                new StageAutoItem { Order = 69, AutoItem = new AutoItem {Name = "Immu C capsules"}},
            //                                new StageAutoItem { Order = 70, AutoItem = new AutoItem {Name = "Immu C 4oz"}},
            //                                new StageAutoItem { Order = 71, AutoItem = new AutoItem {Name = "Chaga"}},
            //                                new StageAutoItem { Order = 72, AutoItem = new AutoItem {Name = "Camu Plus"}},
            //                                new StageAutoItem { Order = 73, AutoItem = new AutoItem {Name = "Clear Mind"}},
            //                                new StageAutoItem { Order = 74, AutoItem = new AutoItem {Name = "Her Formula"}},
            //                                new StageAutoItem { Order = 75, AutoItem = new AutoItem {Name = "His Formula"}},
            //                                new StageAutoItem { Order = 76, AutoItem = new AutoItem {Name = "Kava Kalm"}},
            //                                new StageAutoItem { Order = 77, AutoItem = new AutoItem {Name = "MB Builder"}}
            //                            }
            //                        },
            //                        new StageAutoItem
            //                        {
            //                            Order = 3, AutoItem = new AutoItem {Name = "Endocrine"},
            //                            ScansNumber = 1,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "CFS Adrenal"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "CFS Thyroid"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Chrysanthemum Complex"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Cohosh-plus"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Dia-Plus"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "E-Mang-Plus"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Endo-Glan-F"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "Endo-Glan-M"}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Endo-glan-plus"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "Estrogen"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Exhaustion"}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "NT-1"}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Ginseng-plus"}},
            //                                new StageAutoItem { Order = 14, AutoItem = new AutoItem {Name = "Juni-plus"}},
            //                                new StageAutoItem { Order = 15, AutoItem = new AutoItem {Name = "Kelp Complex"}},
            //                                new StageAutoItem { Order = 16, AutoItem = new AutoItem {Name = "Kelp-Plus"}},
            //                                new StageAutoItem { Order = 17, AutoItem = new AutoItem {Name = "L-Glutamine Capsule"}},
            //                                new StageAutoItem { Order = 18, AutoItem = new AutoItem {Name = "Licro-plus"}},
            //                                new StageAutoItem { Order = 19, AutoItem = new AutoItem {Name = "Adrena-Plus"}},
            //                                new StageAutoItem { Order = 20, AutoItem = new AutoItem {Name = "DMPS IV"}},
            //                                new StageAutoItem { Order = 21, AutoItem = new AutoItem {Name = "AQUAZON"}},
            //                                new StageAutoItem { Order = 22, AutoItem = new AutoItem {Name = "L-Tyrosine"}},
            //                                new StageAutoItem { Order = 23, AutoItem = new AutoItem {Name = "Lunazon"}},
            //                                new StageAutoItem { Order = 24, AutoItem = new AutoItem {Name = "Mg/K Aspartate"}},
            //                                new StageAutoItem { Order = 25, AutoItem = new AutoItem {Name = "Norepinephrine"}},
            //                                new StageAutoItem { Order = 26, AutoItem = new AutoItem {Name = "Ovary-Uterus-Plus"}},
            //                                new StageAutoItem { Order = 27, AutoItem = new AutoItem {Name = "Pitui-Plus"}},
            //                                new StageAutoItem { Order = 28, AutoItem = new AutoItem {Name = "Progesterone"}},
            //                                new StageAutoItem { Order = 29, AutoItem = new AutoItem {Name = "Prosta-Plus"}},
            //                                new StageAutoItem { Order = 30, AutoItem = new AutoItem {Name = "DHEA 25mg"}},
            //                                new StageAutoItem { Order = 31, AutoItem = new AutoItem {Name = "Thymo-Plus"}},
            //                                new StageAutoItem { Order = 32, AutoItem = new AutoItem {Name = "Warrior"}},
            //                                new StageAutoItem { Order = 33, AutoItem = new AutoItem {Name = "Yarrow-plus"}},
            //                                new StageAutoItem { Order = 34, AutoItem = new AutoItem {Name = "Thyroid Support"}},
            //                                new StageAutoItem { Order = 35, AutoItem = new AutoItem {Name = "Female Formula"}},
            //                                new StageAutoItem { Order = 36, AutoItem = new AutoItem {Name = "Male  Formula"}},
            //                                new StageAutoItem { Order = 37, AutoItem = new AutoItem {Name = "Sumacazon Liquizon"}},
            //                                new StageAutoItem { Order = 38, AutoItem = new AutoItem {Name = "Thyro-Plus"}},
            //                                new StageAutoItem { Order = 39, AutoItem = new AutoItem {Name = "Iodoral"}},
            //                                new StageAutoItem { Order = 40, AutoItem = new AutoItem {Name = "MHS"}},
            //                                new StageAutoItem { Order = 41, AutoItem = new AutoItem {Name = "NT-2"}},
            //                                new StageAutoItem { Order = 42, AutoItem = new AutoItem {Name = "FHS"}},
            //                                new StageAutoItem { Order = 43, AutoItem = new AutoItem {Name = "Herbal and Mineral Com"}},
            //                                new StageAutoItem { Order = 44, AutoItem = new AutoItem {Name = "Diadren-Forte"}},
            //                                new StageAutoItem { Order = 45, AutoItem = new AutoItem {Name = "Alpha-Mino"}},
            //                                new StageAutoItem { Order = 46, AutoItem = new AutoItem {Name = "Zamu"}},
            //                                new StageAutoItem { Order = 47, AutoItem = new AutoItem {Name = "Dia-Zyme"}},
            //                                new StageAutoItem { Order = 48, AutoItem = new AutoItem {Name = "CFS-A Plus"}},
            //                                new StageAutoItem { Order = 49, AutoItem = new AutoItem {Name = "Trans D Tropin"}},
            //                                new StageAutoItem { Order = 50, AutoItem = new AutoItem {Name = "Ultra D"}},
            //                                new StageAutoItem { Order = 51, AutoItem = new AutoItem {Name = "DHEA 5mg"}},
            //                                new StageAutoItem { Order = 52, AutoItem = new AutoItem {Name = "CG Complex"}},
            //                                new StageAutoItem { Order = 53, AutoItem = new AutoItem {Name = "Para Thy"}},
            //                                new StageAutoItem { Order = 54, AutoItem = new AutoItem {Name = "SLP"}},
            //                                new StageAutoItem { Order = 55, AutoItem = new AutoItem {Name = "Prosta Complete"}},
            //                                new StageAutoItem { Order = 56, AutoItem = new AutoItem {Name = "C-1000"}},
            //                                new StageAutoItem { Order = 57, AutoItem = new AutoItem {Name = "Alpha Flavin"}},
            //                                new StageAutoItem { Order = 58, AutoItem = new AutoItem {Name = "Ovary Uterus Plus"}},
            //                                new StageAutoItem { Order = 59, AutoItem = new AutoItem {Name = "Calm Naturally"}},
            //                                new StageAutoItem { Order = 60, AutoItem = new AutoItem {Name = "IMMU C"}},
            //                                new StageAutoItem { Order = 61, AutoItem = new AutoItem {Name = "Neuro Balance"}},
            //                                new StageAutoItem { Order = 62, AutoItem = new AutoItem {Name = "Prosta Plus Complete"}},
            //                                new StageAutoItem { Order = 63, AutoItem = new AutoItem {Name = "Multi Methyl B"}},
            //                                new StageAutoItem { Order = 64, AutoItem = new AutoItem {Name = "His Formula"}},
            //                                new StageAutoItem { Order = 65, AutoItem = new AutoItem {Name = "Mold X"}},
            //                                new StageAutoItem { Order = 66, AutoItem = new AutoItem {Name = "Camu Plus"}},
            //                                new StageAutoItem { Order = 67, AutoItem = new AutoItem {Name = "Clear Mind"}},
            //                                new StageAutoItem { Order = 68, AutoItem = new AutoItem {Name = "Her Formula"}},
            //                                new StageAutoItem { Order = 69, AutoItem = new AutoItem {Name = "Kava Kalm"}},
            //                                new StageAutoItem { Order = 70, AutoItem = new AutoItem {Name = "MB Builder"}},
            //                                new StageAutoItem { Order = 71, AutoItem = new AutoItem {Name = "Chaga"}},
            //                                new StageAutoItem { Order = 72, AutoItem = new AutoItem {Name = "AMYGDALIN (B17)"}},
            //                                new StageAutoItem { Order = 73, AutoItem = new AutoItem {Name = "SE-CBD"}},
            //                                new StageAutoItem { Order = 74, AutoItem = new AutoItem {Name = "STONEBREAKER PLUS"}},
            //                                new StageAutoItem { Order = 75, AutoItem = new AutoItem {Name = "Female Balance"}},
            //                                new StageAutoItem { Order = 76, AutoItem = new AutoItem {Name = "Kava Kalm"}},
            //                                new StageAutoItem { Order = 77, AutoItem = new AutoItem {Name = "MB Builder"}}
            //                            }
            //                        },
            //                        new StageAutoItem
            //                        {
            //                            Order = 4, AutoItem = new AutoItem {Name = "Back Flower"},
            //                            ScansNumber = 1,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "Centaury"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "Cerato"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Cherry Plum"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Chestnut Bud"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Chicory"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "Clematis"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Crab Apple"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "Elm"}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Gentian"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "Gorse"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Heather"}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "Holly"}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Honeysuckle"}},
            //                                new StageAutoItem { Order = 14, AutoItem = new AutoItem {Name = "Hornbeam"}},
            //                                new StageAutoItem { Order = 15, AutoItem = new AutoItem {Name = "Impatiens"}},
            //                                new StageAutoItem { Order = 16, AutoItem = new AutoItem {Name = "Larch"}},
            //                                new StageAutoItem { Order = 17, AutoItem = new AutoItem {Name = "Agrimony"}},
            //                                new StageAutoItem { Order = 18, AutoItem = new AutoItem {Name = "Aspen"}},
            //                                new StageAutoItem { Order = 19, AutoItem = new AutoItem {Name = "Beech"}},
            //                                new StageAutoItem { Order = 20, AutoItem = new AutoItem {Name = "Mimulus"}},
            //                                new StageAutoItem { Order = 21, AutoItem = new AutoItem {Name = "Mustard"}},
            //                                new StageAutoItem { Order = 22, AutoItem = new AutoItem {Name = "Oak"}},
            //                                new StageAutoItem { Order = 23, AutoItem = new AutoItem {Name = "Olive"}},
            //                                new StageAutoItem { Order = 24, AutoItem = new AutoItem {Name = "Pine"}},
            //                                new StageAutoItem { Order = 25, AutoItem = new AutoItem {Name = "Red Chestnut"}},
            //                                new StageAutoItem { Order = 26, AutoItem = new AutoItem {Name = "Rescue Remedy"}},
            //                                new StageAutoItem { Order = 27, AutoItem = new AutoItem {Name = "Rock Rose"}},
            //                                new StageAutoItem { Order = 28, AutoItem = new AutoItem {Name = "Rock Water"}},
            //                                new StageAutoItem { Order = 29, AutoItem = new AutoItem {Name = "Scleranthus"}},
            //                                new StageAutoItem { Order = 30, AutoItem = new AutoItem {Name = "Star Of Bethlehem"}},
            //                                new StageAutoItem { Order = 31, AutoItem = new AutoItem {Name = "Sweet Chestnut"}},
            //                                new StageAutoItem { Order = 32, AutoItem = new AutoItem {Name = "Vervain"}},
            //                                new StageAutoItem { Order = 33, AutoItem = new AutoItem {Name = "Vine"}},
            //                                new StageAutoItem { Order = 34, AutoItem = new AutoItem {Name = "Walnut"}},
            //                                new StageAutoItem { Order = 35, AutoItem = new AutoItem {Name = "Water Violet"}},
            //                                new StageAutoItem { Order = 36, AutoItem = new AutoItem {Name = "White Chestnut"}},
            //                                new StageAutoItem { Order = 37, AutoItem = new AutoItem {Name = "Wild Oat"}},
            //                                new StageAutoItem { Order = 38, AutoItem = new AutoItem {Name = "Wild Rose"}},
            //                                new StageAutoItem { Order = 39, AutoItem = new AutoItem {Name = "Willow"}}
            //                            }
            //                        },
            //                        new StageAutoItem
            //                        {
            //                            Order = 5, AutoItem = new AutoItem {Name = "Vitamins & Minerals"},
            //                            ScansNumber = 1,
            //                            StageAutoItems = new BindingList<StageAutoItem>
            //                            {
            //                                new StageAutoItem { Order = 1, AutoItem = new AutoItem {Name = "Br-Sp-Plus"}},
            //                                new StageAutoItem { Order = 2, AutoItem = new AutoItem {Name = "Cal-Lac-Plus"}},
            //                                new StageAutoItem { Order = 3, AutoItem = new AutoItem {Name = "Cape Aloe"}},
            //                                new StageAutoItem { Order = 4, AutoItem = new AutoItem {Name = "Cardi-Plus"}},
            //                                new StageAutoItem { Order = 5, AutoItem = new AutoItem {Name = "Chola Plus"}},
            //                                new StageAutoItem { Order = 6, AutoItem = new AutoItem {Name = "Chromium Picolinate"}},
            //                                new StageAutoItem { Order = 7, AutoItem = new AutoItem {Name = "Circu-Plus"}},
            //                                new StageAutoItem { Order = 8, AutoItem = new AutoItem {Name = "Detox-H.M."}},
            //                                new StageAutoItem { Order = 9, AutoItem = new AutoItem {Name = "Dia-Plus"}},
            //                                new StageAutoItem { Order = 10, AutoItem = new AutoItem {Name = "E-Mang-Plus"}},
            //                                new StageAutoItem { Order = 11, AutoItem = new AutoItem {Name = "Endo-Glan-F"}},
            //                                new StageAutoItem { Order = 12, AutoItem = new AutoItem {Name = "Endo-Glan-M"}},
            //                                new StageAutoItem { Order = 13, AutoItem = new AutoItem {Name = "Endo-glan-plus"}},
            //                                new StageAutoItem { Order = 14, AutoItem = new AutoItem {Name = "Evening Primrose-se"}},
            //                                new StageAutoItem { Order = 15, AutoItem = new AutoItem {Name = "Ferro-B Plus C"}},
            //                                new StageAutoItem { Order = 16, AutoItem = new AutoItem {Name = "Fiber Balance"}},
            //                                new StageAutoItem { Order = 17, AutoItem = new AutoItem {Name = "Alpha-Lax"}},
            //                                new StageAutoItem { Order = 18, AutoItem = new AutoItem {Name = "Alpha-Ortho-Phos"}},
            //                                new StageAutoItem { Order = 19, AutoItem = new AutoItem {Name = "Alpha-Oxzyme"}},
            //                                new StageAutoItem { Order = 20, AutoItem = new AutoItem {Name = "Fiber Life"}},
            //                                new StageAutoItem { Order = 21, AutoItem = new AutoItem {Name = "Flax Seed Oil"}},
            //                                new StageAutoItem { Order = 22, AutoItem = new AutoItem {Name = "Hepachol"}},
            //                                new StageAutoItem { Order = 23, AutoItem = new AutoItem {Name = "Hista-Plus"}},
            //                                new StageAutoItem { Order = 24, AutoItem = new AutoItem {Name = "Immu-Plus"}},
            //                                new StageAutoItem { Order = 25, AutoItem = new AutoItem {Name = "Kelp-Plus"}},
            //                                new StageAutoItem { Order = 26, AutoItem = new AutoItem {Name = "Liga-Plus"}},
            //                                new StageAutoItem { Order = 27, AutoItem = new AutoItem {Name = "Adrena-Plus"}},
            //                                new StageAutoItem { Order = 28, AutoItem = new AutoItem {Name = "Alpha E W/Spleen"}},
            //                                new StageAutoItem { Order = 29, AutoItem = new AutoItem {Name = "Alpha-B"}},
            //                                new StageAutoItem { Order = 30, AutoItem = new AutoItem {Name = "Alpha-Flavin"}},
            //                                new StageAutoItem { Order = 31, AutoItem = new AutoItem {Name = "Alpha-Gest"}},
            //                                new StageAutoItem { Order = 32, AutoItem = new AutoItem {Name = "Alpha-Gest II"}},
            //                                new StageAutoItem { Order = 33, AutoItem = new AutoItem {Name = "Alpha-Gest III"}},
            //                                new StageAutoItem { Order = 34, AutoItem = new AutoItem {Name = "Alpha-Green (Dup)"}},
            //                                new StageAutoItem { Order = 35, AutoItem = new AutoItem {Name = "Alpha-Green II"}},
            //                                new StageAutoItem { Order = 36, AutoItem = new AutoItem {Name = "Alpha-Statin"}},
            //                                new StageAutoItem { Order = 37, AutoItem = new AutoItem {Name = "Alpha-Zyme"}},
            //                                new StageAutoItem { Order = 38, AutoItem = new AutoItem {Name = "Alpha-Zyme II"}},
            //                                new StageAutoItem { Order = 39, AutoItem = new AutoItem {Name = "Alpha-Zyme V"}},
            //                                new StageAutoItem { Order = 40, AutoItem = new AutoItem {Name = "Vitamin Complete"}},
            //                                new StageAutoItem { Order = 41, AutoItem = new AutoItem {Name = "Beta-Plus"}},
            //                                new StageAutoItem { Order = 42, AutoItem = new AutoItem {Name = "Black Currant Oil"}},
            //                                new StageAutoItem { Order = 43, AutoItem = new AutoItem {Name = "Borage Oil"}},
            //                                new StageAutoItem { Order = 44, AutoItem = new AutoItem {Name = "LSK-Plus"}},
            //                                new StageAutoItem { Order = 45, AutoItem = new AutoItem {Name = "Melatonin"}},
            //                                new StageAutoItem { Order = 46, AutoItem = new AutoItem {Name = "Mg/K Aspartate"}},
            //                                new StageAutoItem { Order = 47, AutoItem = new AutoItem {Name = "Ovary-Uterus-Plus"}},
            //                                new StageAutoItem { Order = 48, AutoItem = new AutoItem {Name = "Phosphatidyl-Serine"}},
            //                                new StageAutoItem { Order = 49, AutoItem = new AutoItem {Name = "Pitui-Plus"}},
            //                                new StageAutoItem { Order = 50, AutoItem = new AutoItem {Name = "Pneumo-Plus"}},
            //                                new StageAutoItem { Order = 51, AutoItem = new AutoItem {Name = "Prosta-Plus"}},
            //                                new StageAutoItem { Order = 52, AutoItem = new AutoItem {Name = "Pycnogenol"}},
            //                                new StageAutoItem { Order = 53, AutoItem = new AutoItem {Name = "Rena-Plus"}},
            //                                new StageAutoItem { Order = 54, AutoItem = new AutoItem {Name = "RNA/DNA"}},
            //                                new StageAutoItem { Order = 55, AutoItem = new AutoItem {Name = "Shark Cartilage-se"}},
            //                                new StageAutoItem { Order = 56, AutoItem = new AutoItem {Name = "Soybean Lecithin"}},
            //                                new StageAutoItem { Order = 57, AutoItem = new AutoItem {Name = "DHEA 25mg"}},
            //                                new StageAutoItem { Order = 58, AutoItem = new AutoItem {Name = "Thymo-Plus"}},
            //                                new StageAutoItem { Order = 59, AutoItem = new AutoItem {Name = "Thyro-Plus (DUP)"}},
            //                                new StageAutoItem { Order = 60, AutoItem = new AutoItem {Name = "Una De Gato"}},
            //                                new StageAutoItem { Order = 61, AutoItem = new AutoItem {Name = "Veno-Plus"}},
            //                                new StageAutoItem { Order = 62, AutoItem = new AutoItem {Name = "Vitamin C (chew)-se"}},
            //                                new StageAutoItem { Order = 63, AutoItem = new AutoItem {Name = "Thyroid Support"}},
            //                                new StageAutoItem { Order = 64, AutoItem = new AutoItem {Name = "Intestinal Formula"}},
            //                                new StageAutoItem { Order = 65, AutoItem = new AutoItem {Name = "Omega Complete"}},
            //                                new StageAutoItem { Order = 66, AutoItem = new AutoItem {Name = "Thyro-Plus"}},
            //                                new StageAutoItem { Order = 67, AutoItem = new AutoItem {Name = "Vitamin D3 & K2"}},
            //                                new StageAutoItem { Order = 68, AutoItem = new AutoItem {Name = "Meta Boost"}},
            //                                new StageAutoItem { Order = 69, AutoItem = new AutoItem {Name = "O2 Support"}},
            //                                new StageAutoItem { Order = 70, AutoItem = new AutoItem {Name = "Horny Goat Weed"}},
            //                                new StageAutoItem { Order = 71, AutoItem = new AutoItem {Name = "Diadren-Forte"}},
            //                                new StageAutoItem { Order = 72, AutoItem = new AutoItem {Name = "Alpha-Mino"}},
            //                                new StageAutoItem { Order = 73, AutoItem = new AutoItem {Name = "Zamu"}},
            //                                new StageAutoItem { Order = 74, AutoItem = new AutoItem {Name = "Alpha-Zyme III"}},
            //                                new StageAutoItem { Order = 75, AutoItem = new AutoItem {Name = "Alpha-Dophilus"}},
            //                                new StageAutoItem { Order = 76, AutoItem = new AutoItem {Name = "Dia-Zyme"}},
            //                                new StageAutoItem { Order = 77, AutoItem = new AutoItem {Name = "Camu Gold"}}
            //                            }
            //                        },
            //                    }
            //                }
            //            },
            //            new AutoProtocolStageRevision
            //            {
            //                Order = 4,
            //                AutoTestStage = new AutoTestStage {Name = "Dosage", Key = "Dosage", StageTabKey = "Dosage", IsMultiLevel = false, IsDestinationOnly = true},
            //                AutoProtocolStage = new AutoProtocolStage
            //                {
            //                    StageAutoItems = new BindingList<StageAutoItem>()
            //                }
            //            },
            //            new AutoProtocolStageRevision
            //            {
            //                Order = 5,
            //                AutoTestStage = new AutoTestStage {Name = "Summary", Key = "Summary", StageTabKey = "PreliminarySummary", IsMultiLevel = false, IsDestinationOnly = true},
            //                AutoProtocolStage = new AutoProtocolStage
            //                {
            //                    StageAutoItems = new BindingList<StageAutoItem>()
            //                }
            //            },
            //            new AutoProtocolStageRevision
            //            {
            //                Order = 6,
            //                AutoTestStage = new AutoTestStage {Name = "Results", Key = "Results", StageTabKey = "Results", IsMultiLevel = false, IsDestinationOnly = true},
            //                AutoProtocolStage = new AutoProtocolStage
            //                {
            //                    StageAutoItems = new BindingList<StageAutoItem>()
            //                }
            //            }
            //        }
            //    }
            //};

            //var productResults = AutoTest.AutoTestResults.Where(result => result.AutoItem.Type.Value == productLookup.Value);

            ////Set references of results inside their products
            //foreach (var productResult in productResults)
            //{
            //    foreach (var product in productResult.AutoTestResultProducts)
            //    {
            //        product.AutoTestResult = productResult;
            //    }
            //}
        }

        #endregion

        #region Save related actions

        /// <summary>
        /// Process Save & Save and Close Actions.
        /// </summary>
        public bool SaveOrSaveAndClose(bool isClosing)
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);

            BeforeSaveAction();

            var isSuccessful = Save(isClosing);

            if (isSuccessful)
            {
                AfterSaveAction();
                //FormStatus = FormStatusEnum.Unchanged;

                if (isClosing)
                {
                    Close();
                }
            }

            UiHelperClass.HideSplash();

            return isSuccessful;
        }

        /// <summary>
        /// Performs the logic of save action
        /// </summary>
        private bool SaveAction()
        {
            return SaveOrSaveAndClose(false);
        }

        /// <summary>
        /// Performs the logic of save and close action
        /// </summary>
        private void SaveAndCloseAction()
        {
            SaveOrSaveAndClose(true);
        }

        /// <summary>
        /// Perform thread safe call to saving logic
        /// </summary>
        private void AutoSave()
        {
            gridControlStages.PerformSafely(() => SaveAction());
        }

        /// <summary>
        /// Uses the Tests manager to save the test        
        /// </summary>
        private bool Save(bool isClosing)
        {
            try
            {
                PostValues();

                if (!AutoTest.Validate())
                    return false;

                //Indicate DB operation is active to prevent mutli-threading exceptions
                _dataOperationActive = true;

                var result = _autoTestDestinationManager.SaveWithDetails(AutoTest).IsSucceed;

                _dataOperationActive = false;

                return result;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }
        }

        /// <summary>
        /// Performs the logic of delete action
        /// </summary>
        private void DeleteAction()
        {
            if (!CanDelete()) return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            BeforeDeleteAction();

            if (Delete())
            {
                AfterDeleteAction();
                //FormStatus = FormStatusEnum.Disabled;
            }

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Delete the current test object.
        /// </summary>
        /// <returns></returns>
        private bool Delete()
        {
            try
            {
                PostValues();

                Revert();

                //Indicate DB operation is active to prevent mutli-threading exceptions
                _dataOperationActive = true;

                var result = _autoTestDestinationManager.Delete(AutoTest);

                _dataOperationActive = false;

                if (result.IsSucceed)
                {
                    Close();
                }

                return result.IsSucceed;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }

        /// <summary>
        /// Confirm the delete operation.
        /// </summary>
        /// <returns></returns>
        private bool CanDelete()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.DeleteConfirmQuestion);

            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        private bool HasChanges()
        {
            return AutoTest.CurrentTestStatus != AutoTestStatus.Pending &&
                   (AutoTest.ObjectState == DomainEntityState.New || AutoTest.ObjectState == DomainEntityState.Modified);
        }

        /// <summary>
        /// Can the form close. With user notification.
        /// </summary>
        /// <returns></returns>
        private bool CanClose()
        {
            if (!HasChanges()) return true;

            //Confirm closing
            var showScreenClosingConfirmation = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ShowScreenClosingConfirmation);
            var saveChanges = true;

            if (showScreenClosingConfirmation)
            {
                var userResponse = UiHelperClass.ShowYesNoCancelMessageBox(StaticKeys.ClosingScreenConfirmationYesNoCancel);

                if (userResponse == DialogResult.Cancel)
                {
                    return false;
                }

                saveChanges = userResponse == DialogResult.Yes;
            }
            
            return !saveChanges || SaveOrSaveAndClose(true);
        }

        /// <summary>
        /// Posts the values in the controls that are not yet comitted to the datasource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        private void PostValues()
        {
            textEditTitle.DoValidate();
            memoEditNotes.DoValidate();
            memoEditDescription.DoValidate();

            if (AutoTest.TestHasStage(StageKey.Dosage))
            {
                gridViewlDosage.PostEditor();
                gridViewlDosage.ValidateEditor();
            }
        }

        /// <summary>
        /// Check if the object can revert or not.
        /// </summary>
        /// <returns></returns>
        private bool CanRevert()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage);

            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        private bool Revert()
        {
            try
            {
                IsLoaded = false;

                //Indicate DB operation is active to prevent mutli-threading exceptions
                _dataOperationActive = true;

                AutoTest = _autoTestDestinationManager.GetAutoTestById(new AutoTestsFilter { AutoTestId = AutoTest.Id, LoadingType = LoadingTypeEnum.All });

                _dataOperationActive = false;
                

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
        /// Do some operations after deleting.
        /// </summary>
        private void AfterDeleteAction()
        {

        }

        /// <summary>
        /// Do some operation before deleting.
        /// </summary>
        private void BeforeDeleteAction()
        {

        }

        /// <summary>
        /// Do some operations after saving.
        /// </summary>
        private void AfterSaveAction()
        {
            //Enable/Disable CRUD Buttons Based On Test Status
            EnableDisableCrudButtons();

            //Set the structure Id after saving because at this point it should depend on the ID's of the saved results not
            //a temporary ID in UI
            SetSessionStructureId();

            //We bind the results again because at this point the StructureId in results should be using real ID's and not ID's from database
            BindTestingResults();
            BindResults();
        }

        /// <summary>
        /// Do some operation before saving.
        /// </summary>
        private void BeforeSaveAction()
        {
            PostValues();
            ValidateForm();
        }

        /// <summary>
        /// Do some operations after reverting.
        /// </summary>
        private void AfterRevertAction()
        {

        }

        /// <summary>
        /// Do some operation before reverting.
        /// </summary>
        private void BeforeRevertAction()
        {

        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        private void AfterLoadAction()
        {
            //Show the form after loading is done
            Opacity = 100;

            if (AutoTest.ObjectState == DomainEntityState.New)
            {
                //SaveAction();
            }
        }

        /// <summary>
        /// Customize for a new Items of the bar manager, or to do extra items;
        /// </summary>
        /// <param name="itemName">The Clicked item tag.</param>
        private void CustomeBarManagerClickHandling(string itemName)
        {

        }

        #endregion

        #region Reporting

        /// <summary>
        /// Generates the AutoTest report
        /// </summary>
        private void Print(bool isPreview)
        {
            PostValues();

            var report = new XtraReportAutoTest
            {
                PatientName = { Value = AutoTest.Patient.FullName  },
                bindingSourcePatient = { DataSource = AutoTest.Patient },
                bindingSourceAutoTest = { DataSource = AutoTest },
                HidePatientName = { Value = false },
                HideLogo = { Value = false },
                ClinicAndPatientName = { Value = UiHelperClass.TechnicianClinicName + " - " + AutoTest.Patient.FullName },
            };

            //Set the default report file name in case it was saved as pdf
            report.ExportOptions.PrintPreview.DefaultFileName = "Automated Test - " + AutoTest.Patient.FullName + " - " + AutoTest.CreationDateTime.ToString(@"MM-dd-yyyy");
            
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

        #region Hardware

        #region Connection

        /// <summary>
        /// Setup hardware related handlers
        /// </summary>
        private void SetupHardwareHandlers(bool handlersEnabled)
        {
            //In case of simulated readings mode setup all hardware flags to be connected without issues
            //and call logic to update connection status related based on used status, notice that we don't
            //connect real hardware events for CSA because it won't be needed.
            if (AutoTest.UseSimulatedReadings)
            {
                AutoTest.CurrentCsaState = CSAState.Connected;
                AutoTest.LastCsaState = CSAState.Connected;
                
                UpdateCsaConnectionStatus(AutoTest.CurrentCsaState);
            }
            else
            {
                //Because this logic can be called from multiple places in different scenarios then use the flag
                //to determined if handlers are supposed to be enabled or disabled.
                if (handlersEnabled)
                {
                    ConnectCSAHandlers();
                }
                else
                {
                    DisconnectCSAHandlers();
                }
            }
        }

        /// <summary>
        /// Performs hardware related initialization actions
        /// </summary>
        private void PerformHardwareInitialization()
        {
            PerformCsaInitialization();
        }

        /// <summary>
        /// Initializes the Hardware Validation thread
        /// </summary>
        private void StartHardwareValidationThread()
        {
            //Only perform real hardware validation logic if the simulated reading mode is not enabled.
            if (AutoTest.UseSimulatedReadings) return;

            //Use the lock mechanism to prevent multi-threading exceptions
            lock (_hardwareValidationThreadLock)
            {
                //Only start the hardware validation thread if it is null or not already alive
                if (_hardwareValidationThread == null || !_hardwareValidationThread.IsAlive)
                {
                    //Set the hardware validation thread
                    _hardwareValidationThread = new Thread(ValidateCSAAndUpdateState) { IsBackground = false };

                    //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
                    //if hardware faliure was foundab
                    _hardwareValidationThread.Start();
                }    
            }
        }
        
        /// <summary>
        /// Performs CSA related initialization steps
        /// </summary>
        private void PerformCsaInitialization()
        {
            //Only perform real hardware initializatin logic if the simulated reading mode is not enabled.
            if (AutoTest.UseSimulatedReadings) return;

            //IMPORTANT:
            //Currently the auto detection logic is flawed because it connects to a com-port as long as it is receiving data from it, the logic doesn't
            //check the data itself and assumes that the device being checked (Previously CSA) is simply connected, this logic can cause serious problems
            //now because we have 2 devices and not just one. To avoid this problem (Temporarily) we are doing downing the following steps:
            //1- We will setup the Prototype to use a fixed com-port and not rely on AutoDetect.
            //2- We will keep the CSA to AutoDetect.
            //3- We will intentionally call the prototype initialization before the csa initialization to make sure that the com-port used by the
            //prototype is locked and blocked from being accessed during the CSA AutoDetect logic.

            //CALL THIS FIRST UNTIL ABOVE IS RESOLVE: Use Fixed Setting For COM-PORT

            UpdateCsaConnectionStatus(CSAState.Detecting);

            //Set the default reading mode
            AutoCsaEmdUnitManagerPhase2.Instance.SetReadingMode(AutoCSAReadingMode.StableReading);

            //Activate Prototype connection
            //AutoCsaEmdUnitManagerPhase2.Instance.OpenPrototypeConnection();
            AutoCsaEmdUnitManagerPhase2.Instance.OpenCSAConnection();
            //AutoCsaEmdUnitManagerPhase2.Instance.OpenPrototypeConnection(new SerialPortConnectionFilter(HardwareType.Prototype) { ComPortNumber = 4 });

            //CALL THIS SECOND UNTIL ABOVE IS RESOLVE: You CAN USE AUTODETECT

            //Activate CSA connection and specifying related events.
            AutoCsaEmdUnitManagerPhase2.Instance.ActivateCSAConnection(CsaMeterValueChanged);
        }

        /// <summary>
        /// Connects CSA handlers
        /// </summary>
        private void ConnectCSAHandlers()
        {
            ////CSA Handlers
            //AutoCsaEmdUnitManagerPhase2.Instance.CSADetecting += CsaDetectingEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.CSAConnected += CsaConnectedEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.CSADisconnected += CsaDisconnectedEvent;

            ////Prototype Handlers
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDetecting += PrototypeDetectingEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypeConnected += PrototypeConnectedEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDisconnected += PrototypeDisconnectedEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypResponseReceived += PrototypResponseReceived;
            //CSA Handlers
            AutoCsaEmdUnitManagerPhase2.Instance.Detecting += DetectingEvent;
            AutoCsaEmdUnitManagerPhase2.Instance.Connected += ConnectedEvent;
            AutoCsaEmdUnitManagerPhase2.Instance.Disconnected += DisconnectedEvent;
            AutoCsaEmdUnitManagerPhase2.Instance.ResponseReceived += ResponseReceived;
        }

        /// <summary>
        /// Disconnects CSA handlers
        /// </summary>
        private void DisconnectCSAHandlers()
        {
            //Close the CSA connection
            AutoCsaEmdUnitManagerPhase2.Instance.CancelCSAAutoDetection();

            ////CSA Handlers
            //AutoCsaEmdUnitManagerPhase2.Instance.CSADetecting -= CsaDetectingEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.CSAConnected -= CsaConnectedEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.CSADisconnected -= CsaDisconnectedEvent;
            //CSA Handlers
            AutoCsaEmdUnitManagerPhase2.Instance.Detecting -= DetectingEvent;
            AutoCsaEmdUnitManagerPhase2.Instance.Connected -= ConnectedEvent;
            AutoCsaEmdUnitManagerPhase2.Instance.Disconnected -= DisconnectedEvent;
            AutoCsaEmdUnitManagerPhase2.Instance.ResponseReceived -= ResponseReceived;

            //Dispose CSA connection after removing handlers
            AutoCsaEmdUnitManagerPhase2.Instance.DisposeCSAConnection(CsaMeterValueChanged);

            //Prototype Handlers
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDetecting -= PrototypeDetectingEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypeConnected -= PrototypeConnectedEvent;
            //AutoCsaEmdUnitManagerPhase2.Instance.PrototypeDisconnected -= PrototypeDisconnectedEvent;
            
            //Close prototype connections
            //AutoCsaEmdUnitManagerPhase2.Instance.ClosePrototypeConnection();
            AutoCsaEmdUnitManagerPhase2.Instance.CloseCSAConnection();
        }

        /// <summary>
        /// Updates the CSA connection status flag
        /// </summary>
        /// <param name="newState"></param>
        private void UpdateCsaConnectionStatus(CSAState newState)
        {
            //Update last and current state falgs
            AutoTest.LastCsaState = AutoTest.CurrentCsaState;
            AutoTest.CurrentCsaState = newState;

            //Set connection state text in the gauge control
            gaugeControlCSA.PerformSafely(() => stateIndicatorComponentCSA.SetStateByName(AutoTest.CurrentCsaState.ToString()));

            //If the test is in progress and the state is connected, then stop scanning.
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress && newState == CSAState.Disconnected)
            {
                //Call stop scanning logic to make sure we stop scanning as quick as possible
                //before performing other validation actions
                StopScanning();

                //Validation logic called in thread to avoid cutting it off in case the scanning thread was stopped
                //if hardware faliure was found
                StartHardwareValidationThread();
            }

            AddTemporaryNotesLine("CSA " + newState);
            AddTemporaryDescriptionLine("CSA " + newState);
        }

        #endregion

        #region Scanning

        /// <summary>
        /// Send commands to CSA based on the specified command parameter
        /// </summary>
        /// <param name="command"></param>
        private void SendCommandToCSA(AutoCSACommand command)
        {
            //Set the last sent command
            _lastSentCommand = command;
            _lastCommandInProgress = true;
            
            switch (command)
            {
                case AutoCSACommand.Reset:

                    UiHelperClass.ShowWaitingPanel("Clearing Hardware State ...");

                    //AutoCsaEmdUnitManagerPhase2.Instance.ResetPrototype();
                    AutoCsaEmdUnitManagerPhase2.Instance.PerformReset();

                    break;
                case AutoCSACommand.ActivateManualMode:
                    AutoCsaEmdUnitManagerPhase2.Instance.ActivateManualMode();
                    break;
                case AutoCSACommand.ActivateAutomationMode:
                    AutoCsaEmdUnitManagerPhase2.Instance.ActivateAutomationMode();
                    break;
                case AutoCSACommand.ActivateImprinting:
                    AutoCsaEmdUnitManagerPhase2.Instance.ActivateImprintingMode();
                    break;
                case AutoCSACommand.ActivateTopPlate:
                    AutoCsaEmdUnitManagerPhase2.Instance.ActivateTopPlate();
                    break;
                case AutoCSACommand.MoistureCheck:

                    UiHelperClass.ShowWaitingPanel("Checking Moisture Level ...");
                    AutoCsaEmdUnitManagerPhase2.Instance.PerformMoistureCehck();

                    break;
                case AutoCSACommand.PressureCheck:

                    UiHelperClass.ShowWaitingPanel("Checking Pressure Level ...");

                    AutoCsaEmdUnitManagerPhase2.Instance.PerformPressureCheck();

                    break;
                case AutoCSACommand.HingeCheck:

                    UiHelperClass.ShowWaitingPanel("Checking Hinge ...");

                    AutoCsaEmdUnitManagerPhase2.Instance.PerformHingeCheck();

                    break;
                case AutoCSACommand.StartAutomation:

                    AutoCsaEmdUnitManagerPhase2.Instance.StartAutomation();

                    break;
                case AutoCSACommand.StopAutomation:
                    AutoCsaEmdUnitManagerPhase2.Instance.StopAutomation();
                    break;
                case AutoCSACommand.SetAutomationProbe:
                    //Get current testing point and its name. Notice that we are using the version inside StageAutoItem
                    var currentTestingPoint = AutoTest.CurrentTestingPathParent.TestingPoint;
                    //var point = AutoTestingPoint.ToArray().FirstOrDefault(p => p.Name.Equals(currentTestingPoint.Name));

                    //Digital Gauge control doesn't allow aligning testing point name in the middle and so we need to do so
                    //manually by adding spaces to the right side of the point name in case the name was shorter than the maximum
                    //number of characters we can show in the gauge.
                    var testingPointVisualName = currentTestingPoint.Name.Length < digitalGaugeTestingPoint.DigitCount
                                                ? currentTestingPoint.Name + new string(' ', (digitalGaugeTestingPoint.DigitCount - currentTestingPoint.Name.Length) / 2)
                                                : currentTestingPoint.Name;

                    //Set the text of the digital gauge to the testing point visual name
                    gaugeControlTestingPoint.PerformSafely(() => digitalGaugeTestingPoint.Text = testingPointVisualName);

                    //We only call actual CSA logic in case the simulated readings mode is disabled
                    if (!AutoTest.UseSimulatedReadings)
                    {
                        //Send the actual command to set the testing point in the CSA
                        //TODO: SelectTestingProbe(AutoTest.CurrentTestingPathParent.AutoItem.TestingPoint.HWIdentifier, AutoTest.CurrentTestingPathParent.AutoItem.Frequency);
                        AutoCsaEmdUnitManagerPhase2.Instance.SetPoint(currentTestingPoint);
                    }

                    AddTemporaryNotesLine("Setting Reading Point : " + currentTestingPoint.Name);
                    break;
            }

            //Perform general delay for any of the commands above after it is called
            Thread.Sleep(_cSAGeneralCommandDelay);

            //If the commands needs a response then check for response
            if (!AutoTest.UseSimulatedReadings && IsResponseWaitingCommand(command))
            {
                //Keep waiting until a response is received
                while (_lastCommandInProgress)
                {
                    Thread.Sleep(_readingStabilityDelay);
                }

                //Handle commands that received incompatible responses
                //Exclude the case where the command is ActivateAutomationMode or ActivateImprinting but the response is among valid or invalid states because these may
                //come across alot
                if ((!IsFailureResponse(_lastCommandResponse, false) &&
                     !IsValidCSAResponse(_lastCommandResponse) &&
                    ((command == AutoCSACommand.ActivateAutomationMode && _lastCommandResponse != AutoCSAResponse.IdleAutomationMode) ||
                    (command == AutoCSACommand.ActivateImprinting     && _lastCommandResponse != AutoCSAResponse.ImprintingModeActivated))) ||

                    //Perform checks only if they are activated in config file
                    (_performPressureCheck && command == AutoCSACommand.PressureCheck && !(_lastCommandResponse == AutoCSAResponse.ValidPressure || _lastCommandResponse == AutoCSAResponse.InvalidPressure)) ||
                    (_performMoistureCheck && command == AutoCSACommand.MoistureCheck && !(_lastCommandResponse == AutoCSAResponse.ValidMoisture || _lastCommandResponse == AutoCSAResponse.InvalidMoisture)) ||
                    (_performHingeCheck && command == AutoCSACommand.HingeCheck && !(_lastCommandResponse == AutoCSAResponse.ValidHinge || _lastCommandResponse == AutoCSAResponse.InvalidHinge)))
                {
                    //HandleInvalidResponse(_lastCommandResponse,true);
                    HandleInvalidResponse(_lastCommandResponse);//We are skipping the invalid CSA response because the user can do nothing about it and instead show a friendly error to generally check CSA
                }

                //Handle hardware checks invalid states
                if ((_performPressureCheck && command == AutoCSACommand.PressureCheck && _lastCommandResponse == AutoCSAResponse.InvalidPressure) ||
                    (_performMoistureCheck && command == AutoCSACommand.MoistureCheck && _lastCommandResponse == AutoCSAResponse.InvalidMoisture) ||
                    (_performHingeCheck && command == AutoCSACommand.HingeCheck && _lastCommandResponse == AutoCSAResponse.InvalidHinge))
                {
                    HandleInvalidResponse(_lastCommandResponse);
                }
            }
        }

        /// <summary>
        /// Checks the status of the CSA and updates status flags
        /// </summary>
        private void ValidateCSAAndUpdateState()
        {
            AddTemporaryNotesLine("Validating CSA ...");

            //If any of the validation flags isn't valid then proceed with invalid case actions
            if (!AutoTest.HardwareValidForScanning)
            {
                //Since hardware validation faild, if the test is in progress then pause it right away.
                //We call this as first action to pause scanning as quick as possible.
                //Notice that this won't prevent further logic after this point from executing (Which is what we want)
                //and this is because this logic called in a thread separate from the scanning thread so when the scanning
                //thread is stopped, the validation logic here will continue until its done.
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    //Change the scanning state to paused
                    ToggleScanningState();
                }

                //If is the CSA is invalid
                if (AutoTest.CurrentCsaState != CSAState.Connected)
                {
                    //Set the reason.
                    AutoTest.CurrentCsaReason = "Please make sure the CSA is connected.";

                    //Show error to use about connection state
                    UiHelperClass.ShowInformation(AutoTest.CurrentCsaReason, "CSA Disconnected");
                }
                else
                {
                    //Set the reason.
                    //AutoTest.CurrentPrototypeReason = "Please make sure the Prototype is connected.";

                    //Show error to use about connection state
                    //UiHelperClass.ShowInformation(AutoTest.CurrentPrototypeReason, "Prototype Disconnected");
                }
            }
            else
            {
                AddTemporaryNotesLine("CSA Valid!");
            }
        }

        /// <summary>
        /// Handles invalid responses from hardware and performs the appropriate behavior
        /// </summary>
        /// <param name="response"></param>
        /// <param name="invalidCSAResponse"></param>
        private void HandleInvalidResponse(AutoCSAResponse response, bool invalidCSAResponse = false)
        {
            if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress) return;

            var error = string.Empty;

            if (invalidCSAResponse)
            {
                error = "Invalid CSA Response";
            }
            else
            {
                switch (response)
                {
                    case AutoCSAResponse.InvalidPressure:
                        error = "Please check pressure.";
                        break;
                    case AutoCSAResponse.InvalidMoisture:
                        error = "Please check moisture.";
                        break;
                    case AutoCSAResponse.InvalidHinge:
                        error = "Please check hinge.";
                        break;
                    case AutoCSAResponse.ManualProbesDisconnected:
                        error = "Please check probes.";
                        break;
                    default:
                        error = "Please check CSA.";
                        break;
                }
            }

            //Set the CSA failure error
            _lastCSANotificationFailure = error;

            //Show the error indicator UI
            ShowHideErrorIndication(true);

            //VERY IMPORTANT: WITHOUT THIS WE WILL GET ERRORS IN READINGS
            AutoCsaEmdUnitManagerPhase2.Instance.StopReading();

            //STOP SCANNING
            ToggleScanningState();
        }

        /// <summary>
        /// Switches the progress bar area between progress and error indication
        /// </summary>
        /// <param name="showError"></param>
        private void ShowHideErrorIndication(bool showError)
        {
            barManagerMain.Form.PerformSafely(() => barEditItemProgress.BeginUpdate());

            //Switch between two controls to determine if we want progress or CSA error indication
            if (showError)
            {
                barManagerMain.Form.PerformSafely(() => barEditItemProgress.Edit =repositoryItemMarqueeProgressBarError);
            }
            else
            {
                barManagerMain.Form.PerformSafely(() => barEditItemProgress.Edit = repositoryItemProgressBarTestProgress);
            }
            

            barManagerMain.Form.PerformSafely(() => barEditItemProgress.EndUpdate());
        }

        /// <summary>
        /// Indicates if the command should wait for a response
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool IsResponseWaitingCommand(AutoCSACommand command)
        {
            return command == AutoCSACommand.ActivateAutomationMode ||
                   command == AutoCSACommand.PressureCheck ||
                   command == AutoCSACommand.MoistureCheck ||
                   command == AutoCSACommand.HingeCheck ||
                   command == AutoCSACommand.ActivateImprinting;
        }

        /// <summary>
        /// Indicates if the response is a failure response
        /// </summary>
        /// <param name="response"></param>
        /// <param name="useActivatedChecksOnly"></param>
        /// <returns></returns>
        private bool IsFailureResponse(AutoCSAResponse response, bool useActivatedChecksOnly)
        {
            //We use the useActivatedChecksOnly flag to return failure only for checks that are activated in config file
            return (response == AutoCSAResponse.InvalidPressure && (!useActivatedChecksOnly || _performPressureCheck)) ||
                   (response == AutoCSAResponse.InvalidMoisture && (!useActivatedChecksOnly || _performMoistureCheck)) ||
                   (response == AutoCSAResponse.InvalidHinge && (!useActivatedChecksOnly || _performHingeCheck)) ||
                    response == AutoCSAResponse.ManualProbesDisconnected;
        }

        /// <summary>
        /// Indicates if the response is a failure response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private bool IsValidCSAResponse(AutoCSAResponse response)
        {
            return response == AutoCSAResponse.ValidPressure ||
                    response == AutoCSAResponse.ValidMoisture ||
                    response == AutoCSAResponse.ValidHinge;
        }

        #endregion

        #endregion

        #endregion

        #region Handlers

        #region UI

        #region Layout

        /// <summary>
        /// Handle dock panel expanding event to dock it automatically when panel is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockPanel_Expanding(object sender, DockPanelCancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new DockPanelCancelEventHandler(dockPanel_Expanding), sender, e);
                }
                catch { }
            }
            else
            {
                try
                {
                    //This logic allows us to automatically dock a folded (Hidden) panel when it is clicked (Expanded) this helps the user
                    //avoid having to do multiple clicks to dock/undock a panel
                    e.Panel.PerformSafely(() => e.Panel.Visibility = DockVisibility.Visible);
                }
                catch { }
            }
        }

        /// <summary>
        /// Handle dock panel visibility change to hide immediately when it is folded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockPanel_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new VisibilityChangedEventHandler(dockPanel_VisibilityChanged), sender, e);
                }
                catch { }
            }
            else
            {
                try
                {
                    //Here we are explicitly hiding the dock panel right away when clicking the fold button, by default the
                    //dock panel is designed to stay open while the mouse is above it and there is actually an option to disable this
                    //without using this event however when that is enabled this causes the other event "DockPanel_Expanding" to stop firing
                    //correctly for some reason and we need that to dock the panel when it is opened to avoid extra clicks.
                    if (e.Visibility == DockVisibility.AutoHide)
                    {
                        e.Panel.PerformSafely(() => e.Panel.HideImmediately());
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Handle Test Info Group Double Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutControlGroupTestInfo_DoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new EventHandler(layoutControlGroupTestInfo_DoubleClick), sender, e);
                }
                catch
                {
                }
            }
            else
            {
                layoutControlGroupTestInfo.Expanded = !layoutControlGroupTestInfo.Expanded;
            }
        }

        #endregion

        #region Preliminary/Summary
        
        /// <summary>
        /// Handles the custom draw cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlPreliminarySummary_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new RowCellCustomDrawEventHandler(gridControlPreliminarySummary_CustomDrawCell), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;
                
                //Avoid running any UI logic if the selected tab is not the right one
                if (tabbedControlGroupMain.SelectedTabPage != layoutControlGroupPreliminarySummary) return;
                
                if (e.Column.Name == gridColumnPreliminary.Name ||
                    e.Column.Name == gridColumnSummary.Name)
                {
                    int value = Convert.ToInt16(e.CellValue);

                    var gridCellInfo = e.Cell as GridCellInfo;
                    if (gridCellInfo != null)
                    {
                        var viewInfo = gridCellInfo.ViewInfo as ProgressBarViewInfo;

                        if (viewInfo != null)
                        {
                            viewInfo.ProgressInfo.EndColor = UiHelperClass.GetRangeColor(value);
                            viewInfo.ProgressInfo.StartColor = UiHelperClass.GetRangeColor(value);
                        }
                    }
                }
            }
        }

        #endregion

        #region Stages
        
        /// <summary>
        /// Handle drawing cells for stages grid views to allow showing icons with the text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStages_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new RowCellCustomDrawEventHandler(gridViewStages_CustomDrawCell), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;

                if (e.Column.Name == gridColumnStage.Name || e.Column.Name == gridColumnStageAutoItem.Name)
                {
                    var image = e.Column.Name == gridColumnStage.Name ? Resources.StageIcon16 : Resources.MinusWhiteSmall;

                    var iconYIndent = (e.Bounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, e.Bounds.X + 1, e.Bounds.Y + iconYIndent, image.Width, image.Height);

                    // always indent the text even if no icon for this cell - so all columns are aligned
                    var bounds = e.Bounds;

                    bounds.Width -= 23;
                    bounds.X += 23;

                    // TODO: this doesn't handle long text values getting truncated - should show '…' at the end
                    e.Appearance.DrawString(e.Cache, e.DisplayText, bounds);
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Handle row style on Stages view to make sure that the current stage remains highlighted when scanning its stage items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStages_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new RowStyleEventHandler(gridViewStages_RowStyle), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;

                //The logic here makes sure that a stage row remains highlighted during the scanning of the stage items that
                //are shown in the stages panel, this is required because the stage items are not highlighted unless "gridViewStageAutoItems" is set
                //as focused view, in such case "gridViewStages" will lose its focus and highlight and we use the logic below to keep it highlighted.
                //Notice that we use this event only with "gridViewStages" and not with "gridViewStageAutoItems" (even though we could) and this is to avoid keeping
                //the a stage item in "gridViewStageAutoItems" focused forever. We want the stage item to be focused during scaning or when manually selected by the user but not forever.
                //The logic below will always keep the row style as if it is focused and in the stage items that is not always the case and so if we use the logic below on it, then we would
                //need to tell it when to turn off and when to turn on and it is just easier to not use it and rely on the gridview focus only.
                var currentView = (GridView)sender;
                if ((e.State & GridRowCellState.Focused) == GridRowCellState.Focused)
                {
                    e.Appearance.Assign(currentView.GetViewInfo().PaintAppearance.GetAppearance("FocusedRow"));
                }
            }
        }

        /// <summary>
        /// Handle gridview getting focus to show details of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStageAutoItems_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewStageAutoItems_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                if (UiHelperClass.IsClickInRow((GridView)sender) && AutoTest.CurrentTestStatus != AutoTestStatus.InProgress)
                {
                    if (AutoTest.CurrentRootStageAutoItem != null)
                    {
                        //Sets the image and description
                        UpdateImageAndDescription(AutoTest.CurrentRootStageAutoItem.AutoItem.Name, 
                                                  AutoTest.CurrentRootStageAutoItem.AutoItem.VisualDescription,
                                                  AutoTest.CurrentRootStageAutoItem.TestingPoint.Key,
                                                  AutoTest.CurrentRootStageAutoItem.AutoItem.Image,
                                                  GetPatientGenderBasedBioDigitalID(AutoTest.CurrentRootStageAutoItem.AutoItem));
                    }
                }
            }
        }

        #endregion

        #region Testing Items

        /// <summary>
        /// Handle cell drawing in testing items grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewTestingItems_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new RowCellCustomDrawEventHandler(gridViewTestingItems_CustomDrawCell), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;

                //Handle drawing a folder icon in a cell if the StageAutoItem has childes
                if (e.Column.Name == gridColumnScanItem.Name && gridViewTestingItems.IsValidRowHandle(e.RowHandle))
                {
                    var currentStageAutoItem = (StageAutoItem)gridViewTestingItems.GetRow(e.RowHandle);

                    if (currentStageAutoItem.HasChildes)
                    {
                        var image = Resources.Folder_16;

                        //Logic to draw the folder icon at the right side of the cell
                        int xPos = (((e.Bounds.Location.X + e.Bounds.Width) - image.Width) - 2);
                        int yPos = (e.Bounds.Location.Y + 1);

                        var imagePoint = new Point(xPos, yPos);

                        e.Graphics.DrawImage(image, imagePoint);
                    }
                }
            }
        }

        #endregion

        #region Multi-Level Scanning

        /// <summary>
        /// Handle row style for TestingItems grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewTestingItems_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowStyleEventHandler(gridViewTestingItems_RowStyle), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Avoid running any UI logic if the selected tab is not the right one
                if (AutoTest.StageTabKeyEnum != StageTabKey.Testing || AutoTest.StageItemBookmark.IsEmpty) return;

                //Only show yellow highlight on rows when the test is in progress or paused and only if the scanning type is Elimination, otherwise hide the higlight
                if ((AutoTest.CurrentTestStatus == AutoTestStatus.InProgress ||
                     AutoTest.CurrentTestStatus == AutoTestStatus.Paused) &&
                     AutoTest.CurrentTestingPathParent.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeElimination &&
                    e.RowHandle >= 0 && e.RowHandle < AutoTest.CurrentTestingPathParent.FirstHalfCount)
                {
                    e.Appearance.BackColor = Color.FromArgb(240, 252, 0);
                    e.Appearance.BackColor2 = Color.AliceBlue;
                    e.Appearance.ForeColor = Color.Black;
                }
                else if (e.RowHandle == GridControl.AutoFilterRowHandle)
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Handle focused row change event on the TestingItems gridview to update its style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewTestingItems_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewTestingItems_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;
                
                //Sets the image and description
                UpdateImageAndDescriptionByView(sender, typeof (StageAutoItem));
                
                //Avoid running any UI logic if the selected tab is not the right one
                if (AutoTest.StageTabKeyEnum != StageTabKey.Testing) return;

                //Important:
                //If the test is not progress and the click is in row then we set the StepBookmark index based on the user selection, this is important
                //to rememeber the user last position so we can restore it in the case of add or delete result which causes the grid to set its datasource
                //and lose its focused row handle.
                if (!_isUpdatingUserStageSelection &&
                    AutoTest.CurrentTestStatus != AutoTestStatus.InProgress && 
                    UiHelperClass.IsClickInRow(gridViewTestingItems))
                {
                    AutoTest.CurrentTestingPathParent.SetBookmarkIndex(e.FocusedRowHandle, ScanBookmarkType.MultiLevelScanning);
                }

                if (e.FocusedRowHandle < 0)
                {
                    gridViewTestingItems.PaintAppearance.FocusedRow.BackColor = Color.White;
                    gridViewTestingItems.PaintAppearance.FocusedRow.BackColor2 = Color.White;
                }//Only show the orange highlight if the test is not in progress or if the scanning method is 1x1
                else if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress || 
                         AutoTest.CurrentTestingPathParent.ChildsScanningTypeEnum == ChildsScanningType.ChildsScanningTypeSequential)
                {
                    gridViewTestingItems.PaintAppearance.FocusedRow.BackColor = Color.Orange;
                    gridViewTestingItems.PaintAppearance.FocusedRow.BackColor2 = Color.FloralWhite;
                }
            }
        }

        /// <summary>
        /// Handle gridview getting focus to show details of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewTestingItems_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewTestingItems_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByView(sender, typeof(StageAutoItem));
            }
        }

        #endregion

        #region General

        /// <summary>
        /// Handle title bar Item Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManagerMain_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(barManagerMain_ItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Item == barLargeButtonItemSave)
                {
                    AutoSave();
                }
                else if (e.Item == barLargeButtonItemDelete)
                {
                    DeleteAction();
                }
                else if (e.Item == barLargeButtonItemPrintPreview)
                {
                    Print(true);
                }
                else if (e.Item == barLargeButtonItemPrint)
                {
                    Print(false);
                }
            }
        }

        /// <summary>
        /// Handle clicks on grids to determine if user is allowed to perform action based on current test status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new MouseEventHandler(GridView_MouseDown), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;

                var view = sender as GridView;

                //If the current status is in progress then mark the MouseDown event as handled to prevent changing the focused row
                //on any grid that is linked to this event
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    if (view != null)
                    {
                        ((DXMouseEventArgs)e).Handled = true;
                    }
                }

                if (view == gridViewStages || view == gridViewStageAutoItems)
                {
                    _lastStageRowChangeMethod = FocusedRowChangeMethod.Mouse;
                }
            }
        }

        /// <summary>
        /// Handle keys on grids to determine if user is allowed to perform action based on current test status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new KeyEventHandler(GridView_KeyDown), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;

                var view = sender as GridView;

                //If the current status is in progress then mark the keydown event as handled to prevent changing the focused row
                //on any grid that is linked to this event
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    if (view != null)
                    {
                        e.Handled = true;
                    }
                }

                if (view == gridViewStages || view == gridViewStageAutoItems)
                {
                    _lastStageRowChangeMethod = FocusedRowChangeMethod.Keyboard;
                }
            }
        }

        /// <summary>
        /// Handles context menu opnining
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStrip_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    e.Cancel = true;
                }

                if (sender == contextMenuStripTestingResult)
                {
                    e.Cancel = AutoTest.CurrentTestStatus != AutoTestStatus.Ended;
                    toolStripMenuItemDeleteTestingResult.Enabled = UiHelperClass.IsClickInRow(TestingTreeList) && AutoTest.CurrentTestStatus == AutoTestStatus.Ended;
                }
                else if (sender == contextMenuStripProducts)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewlDosage) || AutoTest.CurrentTestStatus != AutoTestStatus.Ended;
                    toolStripMenuItemDeleteProduct.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewlDosage) && AutoTest.CurrentTestStatus == AutoTestStatus.Ended;
                }
                else if (sender == contextMenuStripResult)
                {
                    e.Cancel = AutoTest.CurrentTestStatus != AutoTestStatus.Ended;
                    toolStripMenuItemDeleteResult.Enabled = UiHelperClass.IsClickInRow(AllResultsTreeList) && AutoTest.CurrentTestStatus == AutoTestStatus.Ended;
                }
                else if (sender == contextMenuStripTestingItems)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewTestingItems) || AutoTest.CurrentTestStatus != AutoTestStatus.Ended;
                    toolStripMenuItemMarkAsResult.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewTestingItems);
                }
            }
        }

        /// <summary>
        /// Handles context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStrip_ItemClicked), sender, e);
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

                if (sender == contextMenuStripTestingResult && e.ClickedItem == toolStripMenuItemDeleteTestingResult)
                {
                    DeleteTestingResult();
                }
                else if (sender == contextMenuStripProducts && e.ClickedItem == toolStripMenuItemDeleteProduct)
                {
                    DeleteProduct();
                }
                else if (sender == contextMenuStripResult && e.ClickedItem == toolStripMenuItemDeleteResult)
                {
                    DeleteResult();
                }
                else if ((sender == contextMenuStripResult && e.ClickedItem == toolStripMenuItemAddResult) ||
                         (sender == contextMenuStripTestingResult && e.ClickedItem == toolStripMenuItemAddTestingResult))
                {
                    AddResultBySearch(e.ClickedItem == toolStripMenuItemAddResult);
                }
                else if (sender == contextMenuStripTestingItems && e.ClickedItem == toolStripMenuItemMarkAsResult)
                {
                    //Get current selected stage auto item
                    var currentTestingItem = gridViewTestingItems.GetFocusedRow() as StageAutoItem;

                    MarkTestingItemAsResult(currentTestingItem, null);
                }
            }
        }

        /// <summary>
        /// Handle progress bar custom display text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemProgressBarTestProgress_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new CustomDisplayTextEventHandler(repositoryItemProgressBarTestProgress_CustomDisplayText), sender, e);
                }
                catch { }
            }
            else
            {
                //Set the custom text content of the progress bar as the current progress percetnage and current scan path or info
                barManagerMain.Form.PerformSafely(() => e.DisplayText = "   " + AutoTest.ScanProgressPercentage + " %" + " : " + AutoTest.GetCurrentScanInfo(true));         
            }
        }

        /// <summary>
        /// Handle setting the error text inside the repositoryItemMarqueeProgressBarError control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemMarqueeProgressBarError_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new CustomDisplayTextEventHandler(repositoryItemMarqueeProgressBarError_CustomDisplayText), sender, e);
                }
                catch { }
            }
            else
            {
                //Handle showing the failure/error text
                barManagerMain.Form.PerformSafely(() => e.DisplayText = "   " + _lastCSANotificationFailure);
            }
        }

        /// <summary>
        /// Dispose the 3D model viewer control on closing to allow reusing it later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormAutoTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_useHumanAnatomyView)
            {
                // IMPORTANT: Dispose the model control before close the from (or the viewer) to avoid conflicts when add it to another viewer.
                xtraUserControlBioDigital3DModelViewerMain.PerformSafely(() => xtraUserControlBioDigital3DModelViewerMain.DisposeControl());
            }
        }

        /// <summary>
        /// Handle mouse wheel event to close active editor to allow scrolling to work without issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void OnMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            TestingTreeList.CloseEditor();
            AllResultsTreeList.CloseEditor();
        }

        #endregion

        #endregion

        #region Process

        #region General

        /// <summary>
        /// Handle form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormAutoTest_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new EventHandler(XtraFormAutoTest_Load), sender, e);
                }
                catch { }
            }
            else
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.DataInitializationMessgae);

                //Data initialization
                InitializeData();

                //UI Initialization
                PerformUiInitializations();

                //Set title
                SetTitle();

                UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);

                //Binding logic for grids and controls
                Rebind();

                //Update access rules of the controls
                SetAccessRules();

                //Set the first stage as the first selected stage
                UpdateStageSelection(gridViewStages,false);

                //Initializae scanning related parameters, this should be called after binding to make sure all needed data is available
                InitializeScanningParameters();

                //Here we setup hardware handlers and we call the logic specifically after the AutoTest has been initialized
                //because we need the AutoTest properties to be set for the hardware handling logic to work properly
                SetupHardwareHandlers(true);

                //Perform hardware intitalization like activating connection, setting reading mode and other HW details
                PerformHardwareInitialization();

                ////SetProperties();
                //SetBinding();
                //UiHelperClass.ShowWaitingPanel(StaticKeys.FinalizingMessage);
                ////SetupMainErrorProvider();
                ////UpdateErrorProvider();
                //SetupHandlers();
                
                //SetAccessRules();
                UiHelperClass.HideSplash();
                AfterLoadAction();
            }

            //GenerateDummyData();
            
            
            //Mark the screen as loaded to enable events firing
            IsLoaded = true;
        }

        /// <summary>
        /// Handle form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormAutoTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new FormClosingEventHandler(XtraFormAutoTest_FormClosing), sender, e);
                }
                catch { }
            }
            else
            {
                switch (AutoTest.CurrentTestStatus)
                {
                    case AutoTestStatus.Pending:
                        e.Cancel = false;
                        IsClosing = true;
                        break;
                    case AutoTestStatus.InProgress:
                        e.Cancel = true;
                        break;
                    case AutoTestStatus.Paused:
                    case AutoTestStatus.Ended:
                        PostValues();

                        e.Cancel = !CanClose();

                        if (e.Cancel)
                        {
                            IsClosing = false;
                            return;
                        }

                        IsClosing = true;
                        break;
                }

                if (!e.Cancel)
                {
                    SetupHardwareHandlers(false);
                }
            }
        }

        #endregion

        #region Scanning
        
        /// <summary>
        /// Handle start/pause button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItemStartStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new ItemClickEventHandler(barEditItemStartStop_ItemClick), sender, e);
                }
                catch { }
            }
            else
            {
                ToggleScanningState();
            }
        }

        #endregion

        #region Stages

        /// <summary>
        /// Handle selecting stage/stage auto item based on selected row within a certain view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStages_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new FocusedRowChangedEventHandler(gridViewStages_FocusedRowChanged), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded || _isUpdatingUserStageSelection) return;

                //Calling this method within this event makes sure we update stage based on row selection changes within the same view
                //This guarantess that when selecting a different row in the current view that the tabs are updates, this however doesn't recognize
                //changing the selected view, for example selecting the Row# 1 in two detail views and jumping between them, this even't won't fire.
                //That's why we need the FocusedViewChanged event

                UpdateStageSelection(sender as GridView, false,e.PrevFocusedRowHandle);
            }
        }

        /// <summary>
        /// Handle selecting stage/stage auto item based on selected view within the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlStages_FocusedViewChanged(object sender, ViewFocusEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new ViewFocusEventHandler(gridControlStages_FocusedViewChanged), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded || _isUpdatingUserStageSelection) return;

                //IMPORTANT:
                //The check below was added to avoid unnecessary calling of the FocusedRowChanged event caused by calling the
                //method "UpdateStageSelection" below during the switching from root StageAutoItem to a Stage which caused an unnecessary
                //loading of data for the current selected stage which isn't required since we are moving to a different stage and this
                //eventually caused delay specially when switching between stages manually. The check below was added to mainly fix a performance
                //issue that we noticed when switching from a Stage/StageAutoItem to the Dosage stage, we found that during the switch the system
                //quickly showed the data of the first item in the currently selected stage before switching to the dosage list and this is unwanted.
                //It turned out that the FocusedViewChanged fired first and called the logic below which in turn set the focused row in StageAutoItems
                //to the first row which trigerred the event FocusedRowChanged which called a refresh on the list while still on the previous Stage, all
                //this happens before switching to Dosage which causes a delay. We figured this event should fire only when switching from Stage to
                //StageAutoItem and not the other way around.
                if(e.PreviousView.Name == gridViewStageAutoItems.Name && e.View.Name == gridViewStages.Name) return;

                //Calling this method within this event is important because here we recognize switching the selected view, this important when jumping
                //between multiple detail views under different parents, the FocusedRowHandle event won't fire in such case if in both views we have the
                //same row selected but relying in this event here would help us recognize that the selection has changed and handle it.

                UpdateStageSelection(e.View as GridView,true);
            }

        }

        /// <summary>
        /// Handle gridview getting focus to show details of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewStages_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewStages_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the description and image of current stage
                SetCurrentStageImageAndDescription();
            }
        }

        #endregion

        #endregion

        #region Data

        /// <summary>
        /// Handle the propriety changed event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        private void AutoTest_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new PropertyChangedEventHandler(AutoTest_PropertyChanged), sender, e);
                }
                catch { }
            }
            else
            {
                //switch (AutoTest.ObjectState)
                //{
                //    case DomainEntityState.Modified:
                //        FormStatus = FormStatusEnum.Modified;
                //        break;
                //    case DomainEntityState.New:
                //        FormStatus = FormStatusEnum.Modified;
                //        break;
                //    case DomainEntityState.Unchanged:
                //        FormStatus = FormStatusEnum.Unchanged;
                //        break;
                //}

                //Update the form title based on title field changes
                if (e.PropertyName == ExpressionHelper.GetPropertyName(() => AutoTest.Name))
                {
                    SetTitle();
                }

                //Enable/Disable CRUD Buttons Based On Test Status
                EnableDisableCrudButtons();
            }
        }

        /// <summary>
        /// Handle assigning a filtered LookupEdit for Form/Size with only allowed options for each row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewlDosage_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new CustomRowCellEditEventHandler(gridViewlDosage_CustomRowCellEdit), sender, e);
                }
                catch { }
            }
            else
            {
                //IMPORTANT:
                /* Goal: Provide two dropdowns one for form and one for size with dynamic datasources based on the row so a product can only use
                 * its own forms and sizes while allowing selection by user in the grid and filteration of sizes based on selected form.
                 * 
                 * Problems:
                 * 1- We failed when using the GridView ShownEditor event to set data source because it only works when dropdown is clicked, leaving cells empty all time.
                 * 2- We failed when using CustomRowCellEditing event because the LookupEdit is only used during editing so when cell is left user will see the ID instead of the value.
                 * 3- IMPORTANT: Field In the LookupEdit Column Uses Property That Has Shared Reference With LookupEdit Datasource:
                 *    We had a major issue with committing changes to the datasource when dropdown value is selected because the actual ProductForm or ProductSize property in the current
                 *    AutoTestResultProduct had its value set to an object that is also part of the datasource of the LookupEdit meaning that the ProductForm object for example was both used
                 *    inside the LookupEdit datasource (ProductForms collection) and it was also used as the value of the ProductForm property inside the AutoTestResultProduct.
                 *    
                 *    This shared reference meant that any changes in selection in the LookupEdit were reflected on the ProductForm property in AutoTestResultProduct but also itself existed inside the
                 *    LookupEdit datasource, meaning that the selections in LookupEdit also affected its datasource itself because of the shared references of the data, this was happeneing
                 *    because all the data we use either in AutoTestResultProduct or in the LookupEdit are eventually coming from the same place which is LLBLGen loading data from the DB and
                 *    creating shared references because it avoids loading the data multiple times spearately and so it loads the ProductForm once and uses it as the value of the ProductForm
                 *    property inside the AutoTestResultProduct while also including the same object within the list of ProductForms inside AutoTestResultProduct.AutoTestResult.AutoItem.Product
                 *    
                 *    This problem was causing the UI to appear as if the selection didn't happen, user selects a value but the selection remains the same, what was actually happening was that the
                 *    ID inside the linked property was changed but the name or value remained the same, so for example if we choose a product form, the control will set the new ID selected inside
                 *    the linked ProductForm property of the current row to the new ID but will keep the Form property the same so the user won't know that the selected actually changed but it only
                 *    changed in the product because the value the user sees on screen is still the same.
                 * 
                 * 4- When changing the selected of the LookupEdit it wasn't committed to the datasource even though we see the new selection on the screen.
                 * 
                 * Solutions:
                 * 1- Seeing Id Instead of Value When Cell Is UnSelected: We used the event CustomRowCellEdit because it creates a RepositoryItem and keeps it in cell. We also kept all LookupEdits in collection to avoid creating new ones each time.
                 * 2- Uncommitted Changes to Datasource: We called UiHelperClass.GridViewPostValues(gridViewlDosage) to save user selection instantly after it is made.
                 * 3- Shared Reference Problem:
                 *    1- We thought about using UnBound column like we did in the Settings screen but this in fact avoids the issue instead of solving it leaving it possible to fall in it in future so we avoided it.
                 *    2- The solution we picked was to avoid the Shared Reference all together and instead of using the ProductForm.Id or ProductSize.Id as Field property in the column we instead created two numeric
                 *    properties just for this purpose inside AutoTestResultProduct and updated their values in two ways:
                 *      1- When the ProductForm or ProductSize is set, we set those ID's based on their values, this is important to make sure the ID's are set when object is loaded from DB.
                 *      2- When the user makes a selection in the grid, we update the ID's based on the user selected and then update ProductForm and ProductSize accordingly.
                 *    
                 *    This meant that the LookupEdit wasn't linked to the property in anyway and was just linked to an ID and our logic will always override the property based on the matching object found inside
                 *    the collection which is found by the ID, this is important to make sure that when a selection is made by the user that it is also reflected on the object which might be used in any kind of
                 *    processing or logic, this is important while in memory, it is also important because when saving, the AutoTest will save the ProductForm or ProductSize property values automatically, otherwise
                 *    we would have to adjust the mapping from BO to Entity to map from the new UI only properties we created but updating ProductForm and ProductSize on the fly is a cleaner and better solution.
                */


                //Validate that the RowHandle belongs to a data row
                if (e.RowHandle < 0) return;

                //Get the current gridview
                var currentView = sender as GridView;

                //Validate the column if ProductForm column
                if (currentView != null && e.Column == gridColumnForm)
                {
                    //Get current focused product result
                    var productResult = (AutoTestResultProduct)currentView.GetRow(e.RowHandle);

                    //Get the Product
                    var product = productResult.Product;

                    //Validate product exists
                    if (product != null && product.ProductForms.Any())
                    {
                        //If the LookupEdits collection already contains a LookupEdit for current product result then just use it instead of creating a new one.
                        //Previously we used the RowHandle instead of the ID but the ID is better because it is linked to the ProductResult we are editing and not to the grid
                        //this is important because if we add new rows or remove product results or change order for example then the row handle would be related to a different
                        //row and the lookup edit may become a mess, even though that we set its datasource each time but relying on ID is cleaner and better.
                        if (productFormLookupEdits.ContainsKey(productResult.Id))
                        {
                            //Extract the lookupedit using ID
                            var lookupEdit = productFormLookupEdits[productResult.Id];

                            //Update the LookupEdit datasource just in case that is needed in future.
                            lookupEdit.DataSource = product.ProductForms;

                            //Set the editor as the LookupEdit
                            e.RepositoryItem = lookupEdit;
                        }
                        else
                        {
                            //If the ID isn't found that we need to create a new LookupEdit
                            var firstProductForm = product.ProductForms[0];

                            //Get the "Form" property as string, this is better than using a hardcoded string
                            var formProperty = ExpressionHelper.GetPropertyName(() => firstProductForm.Form);

                            //Create the lookupedit
                            var productFormLookupEdit = new RepositoryItemLookUpEdit
                            {
                                Name = "ProductFormLookupEdit_" + productResult.Id,
                                DisplayMember = formProperty,
                                ValueMember = ExpressionHelper.GetPropertyName(() => firstProductForm.Id),
                                DataSource = product.ProductForms,
                                AutoHeight = false,
                                NullText = "",
                            };

                            //Set font type and size for RepositoryItemLookUpEdit
                            SetRepositoryLookupEditFont(productFormLookupEdit);
                            
                            //Add one column inside the LookupEdit, otherwise all ProductForm properties will show up
                            productFormLookupEdit.Columns.AddRange(new[] { new LookUpColumnInfo(formProperty, formProperty) });

                            //Link the LookupEdit with EditValueChanged event to catch user selections and handle them
                            productFormLookupEdit.EditValueChanged += ProductFormLookupEditOnEditValueChanged;

                            //Add the created LookupEdit to the collection so we can extract it when needed
                            productFormLookupEdits.Add(productResult.Id, productFormLookupEdit);

                            //Set the editor as the LookupEdit
                            e.RepositoryItem = productFormLookupEdit;
                        }
                    }
                }
                //Validate the column is ProductSize column
                else if (currentView != null && e.Column == gridColumnSize)
                {
                    //Get current focused product result
                    var productResult = (AutoTestResultProduct)currentView.GetRow(e.RowHandle);

                    //Get the Product
                    var product = productResult.Product;

                    //Get the selected ProductForm
                    var selectedProductForm = productResult.ProductForm;

                    //Validate product exists and also ProductForm exists
                    if (product != null && productResult.ProductForm != null && selectedProductForm.ProductSizes.Any())
                    {
                        //If the LookupEdits collection already contains a LookupEdit for current product result then just use it instead of creating a new one.
                        //Previously we used the RowHandle instead of the ID but the ID is better because it is linked to the ProductResult we are editing and not to the grid
                        //this is important because if we add new rows or remove product results or change order for example then the row handle would be related to a different
                        //row and the lookup edit may become a mess, even though that we set its datasource each time but relying on ID is cleaner and better.
                        if (productSizeLookupEdits.ContainsKey(productResult.Id))
                        {
                            //Extract the lookupedit using ID
                            var lookupEdit = productSizeLookupEdits[productResult.Id];

                            //Update the LookupEdit datasource, this is important because the LookupEdit should only show the sizes based on the selected Form.
                            lookupEdit.DataSource = selectedProductForm.ProductSizes;

                            //Set the editor as the LookupEdit
                            e.RepositoryItem = lookupEdit;
                        }
                        else
                        {
                            //If the ID isn't found that we need to create a new LookupEdit

                            var firstProductSize = selectedProductForm.ProductSizes.FirstOrDefault();

                            //Get the "Size" property as string, this is better than using a hardcoded string
                            var sizeProperty = ExpressionHelper.GetPropertyName(() => firstProductSize.Size);

                            //Create the lookupedit
                            var productSizeLookupEdit = new RepositoryItemLookUpEdit
                            {
                                Name = "ProductSizeLookupEdit_" + productResult.Id,
                                DisplayMember = sizeProperty,
                                ValueMember = ExpressionHelper.GetPropertyName(() => firstProductSize.Id),
                                DataSource = selectedProductForm.ProductSizes,
                                AutoHeight = false,
                                NullText = "",
                            };

                            //Set font type and size for RepositoryItemLookUpEdit
                            SetRepositoryLookupEditFont(productSizeLookupEdit);

                            //Add one column inside the LookupEdit, otherwise all ProductSize properties will show up
                            productSizeLookupEdit.Columns.AddRange(new[] { new LookUpColumnInfo(sizeProperty, sizeProperty) });

                            //Link the LookupEdit with EditValueChanged event to catch user selections and handle them
                            productSizeLookupEdit.EditValueChanged += ProductSizeLookupEditOnEditValueChanged;

                            //Add the created LookupEdit to the collection so we can extract it when needed
                            productSizeLookupEdits.Add(productResult.Id, productSizeLookupEdit);

                            //Set the editor as the LookupEdit
                            e.RepositoryItem = productSizeLookupEdit;
                        }
                    }
                }
                else if (currentView != null && e.Column == gridColumnDosageOption && !currentView.IsFilterRow(e.RowHandle))
                {
                    //Here we assign the dosage dropdown LookUpEdit to the cell in run-time to avoid showing the dropdown
                    //button for the filter row.
                    if (e.RepositoryItem is RepositoryItemTextEdit)
                    {
                        e.RepositoryItem = repositoryItemLookUpEditDosageOptions;    
                    }
                }
            }
        }

        /// <summary>
        /// Handle showing the editor in the dosage grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewlDosage_ShownEditor(object sender, EventArgs e)
        {
            //Ignore the logic if the status is in progress
            if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress) return;

            var view = (GridView)sender;

            if (view.FocusedColumn == gridColumnDosageOption && view.ActiveEditor is LookUpEdit)
            {
                var edit = (LookUpEdit)view.ActiveEditor;

                var currentProduct = (AutoTestResultProduct)view.GetFocusedRow();

                if (currentProduct == null || currentProduct.ProductForm == null)
                {
                    return;
                }

                //Set the dosage options list based on current selected form
                edit.Properties.DataSource = currentProduct.ProductForm.DosageOptions;
            }
        }

        /// <summary>
        /// Handle dosage option drodown selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void repositoryItemLookUpEditDosageOptions_EditValueChanged(object sender, EventArgs eventArgs)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new EventHandler(repositoryItemLookUpEditDosageOptions_EditValueChanged), sender, eventArgs);
                }
                catch { }
            }
            else
            {
                //Validate the focused row handle belongs to a row
                if (!UiHelperClass.IsClickInRow(gridViewlDosage)) return;

                var lookupEdit = (LookUpEdit)sender;

                //Get current focused product result
                var productResult = (AutoTestResultProduct)gridViewlDosage.GetRow(gridViewlDosage.FocusedRowHandle);

                if (productResult == null)
                {
                    return;
                }

                //Identify selected dosage option by Id
                var selectedDosageOption =
                    ((BindingList<DosageOption>)lookupEdit.Properties.DataSource).FirstOrDefault(
                        dosageOption => dosageOption.Id == (int)lookupEdit.EditValue);

                if (selectedDosageOption != null)
                {
                    //IMPORTANT: Before any further logic make sure to commit the LookupEdit change to the datasource
                    UiHelperClass.GridViewPostValues(gridViewlDosage);

                    productResult.SuggestedUsage = selectedDosageOption.SuggestedUsage;
                    productResult.Schedule = selectedDosageOption.UsageSchedule;

                    //Refresh the grid to make sure size changes get reflected in UI
                    UiHelperClass.RefreshGridData(gridControlDosage);
                }
            }
        }

        /// <summary>
        /// Handle dosage option column button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemLookUpEditDosageOptions_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //Validate the focused row handle belongs to a row
            if (!UiHelperClass.IsClickInRow(gridViewlDosage)) return;

            var lookupEdit = (LookUpEdit)sender;

            //Get current focused product result
            var productResult = (AutoTestResultProduct)gridViewlDosage.GetRow(gridViewlDosage.FocusedRowHandle);

            if (productResult == null)
            {
                UiHelperClass.ShowInformation("Please make sure a record is selected.");
                return;
            }

            if (productResult.ProductForm == null)
            {
                UiHelperClass.ShowInformation("Please make sure a form is selected.");
                return;
            }

            var dosageOptions = (BindingList<DosageOption>)lookupEdit.Properties.DataSource;

            if (dosageOptions == null || !dosageOptions.Any())
            {
                UiHelperClass.ShowInformation("No dosage options are available.");
                return;
            }
        }

        /// <summary>
        /// Handle Changing Selected Form in Dosage Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ProductFormLookupEditOnEditValueChanged(object sender, EventArgs eventArgs)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new EventHandler(ProductFormLookupEditOnEditValueChanged), sender, eventArgs);
                }
                catch { }
            }
            else
            {
                //The logic in this method is implemented for two reasons:
                //1- Set the ProductForm property based on the seleted ProductFormId to make sure ProductForm object is updated and eventually saved in DB.
                //2- Set the ProductSizeId & ProductSize based on the selected ProductForm, this is important to make sure data is consistent.

                //Validate the focused row handle belongs to a row
                if (!UiHelperClass.IsClickInRow(gridViewlDosage)) return;

                //Get current focused product result
                var productResult = (AutoTestResultProduct)gridViewlDosage.GetRow(gridViewlDosage.FocusedRowHandle);

                //Get the Product
                var product = productResult.Product;

                //Validate product exists
                if (product != null)
                {
                    //IMPORTANT: Before any further logic make sure to commit the LookupEdit change to the datasource
                    UiHelperClass.GridViewPostValues(gridViewlDosage);

                    //Set the ProductForm based on the matching form by ID
                    productResult.ProductForm = product.ProductForms.FirstOrDefault(pf => pf.Id == productResult.ProductFormId);

                    //If the ProductForm is found then assign the size to the first sizes found under the product form
                    if (productResult.ProductForm != null && productResult.ProductForm.ProductSizes.Any())
                    {
                        productResult.ProductSizeId = productResult.ProductForm.ProductSizes.FirstOrDefault().Id;
                        productResult.ProductSize = productResult.ProductForm.ProductSizes.FirstOrDefault(pf => pf.Id == productResult.ProductSizeId);

                        //Set schedule & suggested usage
                        if (string.IsNullOrEmpty(productResult.Schedule))
                        {
                            productResult.Schedule = productResult.ProductForm.UsageSchedule;    
                        }

                        if (string.IsNullOrEmpty(productResult.SuggestedUsage))
                        {
                            productResult.SuggestedUsage = productResult.ProductForm.SuggestedUsage;
                        }
                    }

                    //Refresh the grid to make sure size changes get reflected in UI
                    UiHelperClass.RefreshGridData(gridControlDosage);
                }
            }
        }

        /// <summary>
        /// Handle Changing Selected Size in Dosage Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ProductSizeLookupEditOnEditValueChanged(object sender, EventArgs eventArgs)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new EventHandler(ProductFormLookupEditOnEditValueChanged), sender, eventArgs);
                }
                catch { }
            }
            else
            {
                //Set the ProductSize property based on the seleted ProductSizeId to make sure ProductSize object is updated and eventually saved in DB.

                //Validate the focused row handle belongs to a row
                if (!UiHelperClass.IsClickInRow(gridViewlDosage)) return;

                //Get current focused product result
                var productResult = (AutoTestResultProduct)gridViewlDosage.GetRow(gridViewlDosage.FocusedRowHandle);

                //Get the Product
                var product = productResult.Product;

                //Validate product exists
                if (product != null)
                {
                    //IMPORTANT: Before any further logic make sure to commit the LookupEdit change to the datasource
                    UiHelperClass.GridViewPostValues(gridViewlDosage);

                    //Set the ProductSize based on the matching size by ID
                    productResult.ProductSize = productResult.ProductForm.ProductSizes.FirstOrDefault(pf => pf.Id == productResult.ProductSizeId);
                }
            }
        }

        /// <summary>
        /// Handle treelist getting focus to show details of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListTestingResults_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(treeListTestingResults_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByTreeList(sender as TreeList);
            }
        }

        /// <summary>
        /// Handle focused item changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListTestingResults_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedNodeChangedEventHandler(treeListTestingResults_FocusedNodeChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByTreeList(sender as TreeList);
            }
        }

        /// <summary>
        /// Handle mouse down on treelist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListTestingResults_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new MouseEventHandler(treeListTestingResults_MouseDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                var treeList = sender as TreeList;

                //If the current status is in progress then mark the MouseDown event as handled to prevent changing the focused row
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    if (treeList != null)
                    {
                        //DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                        //((DXMouseEventArgs)e).Handled = true;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (treeList != null)
                    {
                        var info = treeList.CalcHitInfo(e.Location);

                        if (info.Node != null)
                        {
                            treeList.FocusedNode = info.Node;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle tree list key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListTestingResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new KeyEventHandler(treeListTestingResults_KeyDown), sender, e);
                }
                catch { }
            }
            else
            {
                if (!IsLoaded) return;

                var treeList = sender as TreeList;

                //If the current status is in progress then mark the keydown event as handled to prevent changing the focused row
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    if (treeList != null)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handle treelist node focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListTestingResults_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new BeforeFocusNodeEventHandler(treeListTestingResults_BeforeFocusNode), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                var treeList = sender as TreeList;

                //If the current status is in progress then mark the MouseDown event as handled to prevent changing the focused row
                if (AutoTest.CurrentTestStatus == AutoTestStatus.InProgress)
                {
                    if (treeList != null)
                    {
                        e.CanFocus = false;
                    }
                }
            }
        }

        /// <summary>
        /// Handle setting image/description of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewPreliminarySummary_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewPreliminarySummary_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByView(sender, typeof(AutoTestResult));
            }
        }

        /// <summary>
        /// Handle gridview getting focus to show details of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewPreliminarySummary_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewPreliminarySummary_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByView(sender, typeof(AutoTestResult));
            }
        }

        /// <summary>
        /// Handle setting image/description of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewlDosage_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewlDosage_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByView(sender, typeof(AutoTestResultProduct));
            }
        }

        /// <summary>
        /// Handle gridview getting focus to show details of selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewlDosage_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewlDosage_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsLoaded) return;

                //Sets the image and description
                UpdateImageAndDescriptionByView(sender, typeof(AutoTestResultProduct));
            }
        }

        #endregion

        #region Hardware

        #region General

        /// <summary>
        /// Handle clicking hardware reconnect button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonReconnectHW_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new EventHandler(simpleButtonReconnectHW_Click), sender, e);
                }
                catch { }
            }
            else
            {
                SetupHardwareHandlers(false);

                //Here we setup hardware handlers and we call the logic specifically after the AutoTest has been initialized
                //because we need the AutoTest properties to be set for the hardware handling logic to work properly
                SetupHardwareHandlers(true);

                //Perform hardware intitalization like activating connection, setting reading mode and other HW details
                PerformHardwareInitialization();
            }
        }

        #endregion

        #region Connection

        /// <summary>
        /// Handle CSA Detecting event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        private void DetectingEvent(object sender, int comPortNumber)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnDetecting(DetectingEvent), sender, comPortNumber);
                }
                catch { }
            }
            else
            {
                //Detecting
                UpdateCsaConnectionStatus(CSAState.Detecting);
            }
        }

        /// <summary>
        /// Handle CSA Connected event
        /// </summary>
        /// <param name="sender"></param>
        private void ConnectedEvent(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnConnected(ConnectedEvent), sender);
                }
                catch { }
            }
            else
            {
                //Connected
                UpdateCsaConnectionStatus(CSAState.Connected);
            }
        }

        /// <summary>
        /// Handle CSA Disconnected event
        /// </summary>
        /// <param name="sender"></param>
        private void DisconnectedEvent(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnDisconnected(DisconnectedEvent), sender);
                }
                catch { }
            }
            else
            {
                //Disconnected
                UpdateCsaConnectionStatus(CSAState.Disconnected);
            }
        }

        /// <summary>
        /// Handle prototype detecting event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="comPortNumber"></param>
        private void PrototypeDetectingEvent(object sender, int comPortNumber)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnDetecting(PrototypeDetectingEvent), sender, comPortNumber);
                }
                catch { }
            }
            else
            {
                //Detecting
                UpdateCsaConnectionStatus(CSAState.Detecting);
            }
        }

        /// <summary>
        /// Handle prototype connected event
        /// </summary>
        /// <param name="sender"></param>
        private void PrototypeConnectedEvent(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnConnected(PrototypeConnectedEvent), sender);
                }
                catch { }
            }
            else
            {
                //Connected
                UpdateCsaConnectionStatus(CSAState.Connected);
            }
        }

        /// <summary>
        /// Handle prototype disconnected event
        /// </summary>
        /// <param name="sender"></param>
        private void PrototypeDisconnectedEvent(object sender)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnDisconnected(PrototypeDisconnectedEvent), sender);
                }
                catch { }
            }
            else
            {
                //Disconnected
                UpdateCsaConnectionStatus(CSAState.Disconnected);
            }
        }

        /// <summary>
        /// Handle prototype response received event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="response"></param>
        /// <param name="originData"></param>
        private void ResponseReceived(object sender, AutoCSAResponse response, string originData)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.OnResponseReceived(ResponseReceived), sender, response, originData);
                }
                catch { }
            }
            else
            {
                //Use the lock mechanism to prevent multi-threading exceptions
                lock (_responseLock)
                {
                    //Reset the command status and set the response
                    _lastCommandInProgress = false;
                    _lastCommandResponse = response;

                    //If AutomationPostFailureAutoResume is enabled and in case the test was previously paused because of a CSA failure notification then
                    //if the flags are valid again then resume the test automatically
                    if (_enableAutomationPostFailureAutoResume &&   //Check AutoResume is enabled
                        HardwareCheckRequired &&                    //Check that at least one hardware check is enabled, otherwise we shouldn't auto resume
                        _pausedByCSAFailureNotification &&          //Make sure we are resuming after CSA failure notification and after CSA responds to a command we previously sent
                        AutoTest.CurrentTestStatus == AutoTestStatus.Paused && //To resume, the test has to be paused
                        //Validate that a check is activated and valid, if all activated checks are valid then we auto resume
                        (!_performHingeCheck || (AutoCsaEmdUnitManagerPhase2.Instance.HasValidHinge.HasValue && AutoCsaEmdUnitManagerPhase2.Instance.HasValidHinge.Value)) &&
                        (!_performMoistureCheck || (AutoCsaEmdUnitManagerPhase2.Instance.HasValidMoisture.HasValue && AutoCsaEmdUnitManagerPhase2.Instance.HasValidMoisture.Value)) &&
                        (!_performPressureCheck || (AutoCsaEmdUnitManagerPhase2.Instance.HasValidPressure.HasValue && AutoCsaEmdUnitManagerPhase2.Instance.HasValidPressure.Value)))
                    {
                        //Instantly hide the error indication, we will still re-run hardware validation but we call the logic below to speed up UI update
                        ShowHideErrorIndication(false);

                        ToggleScanningState();

                        return;
                    }

                    //If the event fired on its own then make sure we properly handle the response we recieve
                    //Here we make sure that we don't perform the logic below unless the current command is not a comand that waits for a response, this way we make
                    //sure the logic below is only performed when we receive the response as a notification from the CSA and not as a response to a command that we sent
                    //This is because commands that we send which wait for responses should handle the responses inside the method SendCommandToCSA and not here.
                    if (!IsResponseWaitingCommand(_lastSentCommand))
                    {
                        //Prevent the event from firing if the test isn't started and also if the hardware validation logic wasn't conducted already, this is to avoid
                        //failure errors that would happen in case the CSA was already sending failure responses while the test is trying to start, in this case the system
                        //should first be given the chance to send the reset command and the commands to activate automation and basically start the process from scratch
                        //and on clean basis.
                        if (AutoTest.CurrentTestStatus != AutoTestStatus.InProgress || !_hardwareValidationPerformed) return;

                        //Check if this is a failure response and make sure the checks are activated in config first
                        if (IsFailureResponse(_lastCommandResponse,true))
                        {
                            //In case of CSA failure notification we set the flag below so we can auto resume once the issue is fixed
                            _pausedByCSAFailureNotification = true;

                            //Handle the failure notification
                            HandleInvalidResponse(_lastCommandResponse);
                        }
                    }
                }
            }
        }

        #endregion

        #region Readings

        /// <summary>
        /// Handel the reading value change event
        /// </summary>
        private void CsaMeterValueChanged(object sender, int reading, int min, int max)
        {
            if (InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return; Invoke(new AutoCsaEmdUnitManagerPhase2.MeterValueChangedHandle(CsaMeterValueChanged), sender, reading, min, max);
                }
                catch { }
            }
            else
            {
                //Set meter reading value
                SetMeterReadingValue(reading, true);
            }
        }

        #endregion

        #endregion

        #endregion
    }  
}