using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.UI_Classes;
using Padding = System.Windows.Forms.Padding;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlIssue : XtraUserControl
    {
        #region Fields

        private TestsManager _issueTestManager;
        private LookupsManager _lookupsManager;
        private PropertiesManager _propertiesManager;
        private ItemsManager _issueItemsManager;
        private readonly TestProtocolsManager _testProtocolsManager;
        private TestPlayStateEnum _currentTestPlayState;
        private SettingsManager _settingsManager;
        private bool _isEditable;
        private int _yesLookupId;
        private int _noLookupId;
        private int _lookupProductId;
        private int _noneListTypeLookupId;
        private readonly List<VisitedItem> _tempVisitedItems;
        private bool _requestedManually;
        private TestHelper _testHelper;
        private bool _isMainIssue;
        
        #endregion

        #region Events

        /// <summary>
        /// Handel the reading request of the control.
        /// </summary>
        public event OnReadingRequest ReadingRequest;

        /// <summary>
        /// Handel the canceling for reading request from the control.
        /// </summary>
        public event OnCancelReadingRequest CancelReadingRequest;

        /// <summary>
        /// Handel the selected Item changed.
        /// </summary>
        public event OnSelectedItemChanged SelectedItemChanged;

        /// <summary>
        /// Handel the Activate Main Connection Request.
        /// </summary>
        public event OnActivateConnectionRequest ActivateConnectionRequest;

        /// <summary>
        /// Handel the Addition of a test result
        /// </summary>
        public event OnUpdateOnTestResults UpdateOnTestResults;

        /// <summary>
        /// Handle the request to refresh image and details
        /// </summary>
        public event OnRefreshDetailsAndImageIssue RefreshDetailsAndImageIssue;

        /// <summary>
        /// Handle the request to update the image control ignore state
        /// </summary>
        public event OnSetImageIgnoreState SetImageIgnoreState;

        /// <summary>
        /// Handel the opening of a dialog with meter
        /// </summary>
        public event OnMeterDialogOpen MeterDialogOpen;

        /// <summary>
        /// Balancing request event
        /// </summary>
        public event OnBalancingRequest BalancingRequest;

        #endregion

        #region Events Invokers

        /// <summary>
        /// Invokes the reading request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="itemsToBroadcast">The items to broadcast.</param>
        public void InvokeReadingRequest(XtraUserControlIssue sender, List<Item> itemsToBroadcast)
        {
            if(ReadingRequest == null)
                return;

            ReadingRequest(sender, itemsToBroadcast);
        }

        /// <summary>
        /// Invokes the cancel reading request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void InvokeCancelReadingRequest(XtraUserControlIssue sender)
        {
            if(CancelReadingRequest == null)
                return;

            CancelReadingRequest(sender);
        }

        /// <summary>
        /// Invokes the selected item changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="item">The item.</param>
        /// <param name="reading">The reading.</param>
        public void InvokeSelectedItemChanged(XtraUserControlIssue sender,bool forTestResults, Item item, int reading)
        {
            if(SelectedItemChanged == null)
                return;

            SelectedItemChanged(sender,forTestResults, item, reading);
        }

        /// <summary>
        /// Invokes the activate connection request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void InvokeActivateConnectionRequest(XtraUserControlIssue sender)
        {
            if(sender == null)
                return;

            ActivateConnectionRequest(sender);
        }

        /// <summary>
        /// Invokes the update on test results.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void InvokeUpdateOnTestResults(XtraUserControlIssue sender)
        {
            if (UpdateOnTestResults == null)
                return;

            UpdateOnTestResults(sender);
        }

        /// <summary>
        /// Invokes the meter dialog opening
        /// </summary>
        /// <param name="showOverlay">The sender.</param>
        public void InvokeMeterDialogOpen(bool showOverlay)
        {
            MeterDialogOpen(showOverlay);
        }

        /// <summary>
        /// Invokes the balancing request
        /// </summary>
        public void InvokeBalancingRequest()
        {
            if (BalancingRequest == null)
                return;

            BalancingRequest();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the protocol step.
        /// </summary>
        public BindingList<ProtocolStep> ProtocolSteps { get; set; }

        /// <summary>
        /// Gets or sets the protocols list.
        /// </summary>
        public BindingList<TestProtocol> Protocols { get; set; }

        /// <summary>
        /// Gets or sets the test issue.
        /// </summary>
        public TestIssue CurrentTestIssue { get; set; }

        /// <summary>
        /// Gets a ref on UserControlItemsNavGrid.
        /// </summary>
        public XtraUserControlItemsNavGrid ControlItemsNavGrid
        {
            get { return xtraUserControlItemsNavGridIssueItems; }
        }

        /// <summary>
        /// This is a pointer to the XtraTabPage containing the user control
        /// </summary>
        public XtraTabPage ParentTab
        {
            get { return Parent as XtraTabPage; }
            set { Parent = value; }
        }

        /// <summary>
        /// This will set and get the icon of the tab
        /// </summary>
        public Image TabTypeImage { get; set; }

        /// <summary>
        /// Gets or sets the current test.
        /// </summary>
        public Test CurrentTest { get; set; }

        /// <summary>
        /// Sets and gets the text of the tab
        /// </summary>
        public string TabText
        {
            get { return (ParentTab == null) ? string.Empty : ParentTab.Text; }
            set { ParentTab.Text = value; }
        }

        /// <summary>
        /// Gets or sets the IssueTestManager value.
        /// </summary>
        public TestsManager IssueTestManager
        {
            get
            {
                return _issueTestManager ?? (_issueTestManager = new TestsManager());
            }
            set
            {
                _issueTestManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the IssueLookupsManager value.
        /// </summary>
        public LookupsManager IssueLookupsManager
        {
            get
            {
                return _lookupsManager ?? (_lookupsManager = new LookupsManager());
            }
            set
            {
                _lookupsManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the IssueItemsManager value.
        /// </summary>
        public ItemsManager IssueItemsManager
        {
            get
            {
                return _issueItemsManager ?? (_issueItemsManager = new ItemsManager());
            }
            set
            {
                _issueItemsManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the Properties Manager value.
        /// </summary>
        public PropertiesManager IssuePropertiesManager
        {
            get
            {
                return _propertiesManager ?? (_propertiesManager = new PropertiesManager());
            }
            set
            {
                _propertiesManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the Test Play State
        /// </summary>
        public TestPlayStateEnum TestPlayState
        {
            get
            {
                return _currentTestPlayState;
            }
            set
            {
                _currentTestPlayState = value;
            }
        }

        /// <summary>
        /// Gets IsIssueItemsBinded value.
        /// </summary>
        public bool IsIssueItemsBinded { get; private set; }

        /// <summary>
        /// Test Logic helprer
        /// </summary>
        public TestHelper IssueTestLogicHelper
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

        /// <summary>
        /// Gets the next test result temp ID
        /// </summary>
        private int NextResultId
        {
            get
            {
                return CurrentTestIssue != null &&
                       CurrentTestIssue.TestResults != null &&
                       CurrentTestIssue.TestResults.Count != 0 ? CurrentTestIssue.TestResults.Max(tr => tr.Id) + 1 : 1 ;
            }
        }

        /// <summary>
        /// Deteremines if the current user control is used for Main Issue
        /// </summary>
        public bool IsMainIssue
        {
            get
            {
                return _isMainIssue;
            }
            set
            {
                _isMainIssue = value;
            }
        }

        /// <summary>
        /// Returns list of selected test result items
        /// </summary>
        /// <returns></returns>
        public BindingList<Item> GetSelectedTestResultItems()
        {
            var testResultHandlers = gridViewTestResults.GetSelectedRows();

            var testResults = testResultHandlers.Select(h => gridViewTestResults.GetRow(h) as TestResult).ToBindingList();
            
            var items = new BindingList<Item>();

            foreach (var testResult in testResults)
            {
                items.Add(testResult.Item);
            }

            return items;
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// The Constructor.
        /// </summary>
        public XtraUserControlIssue()
        {
            InitializeComponent();
            xtraUserControlItemsNavGridIssueItems.AddToTestResults += xtraUserControlItemsNavGridIssueItems_AddToTestResults;
            xtraUserControlItemsNavGridIssueItems.RefreshDetailsAndImageNavGrid += xtraUserControlItemsNavGridIssueItems_RefreshDetailsAndImageNavGrid;
            xtraUserControlItemsNavGridIssueItems.SetImageIgnoreState += xtraUserControlItemsNavGridIssueItems_SetImageIgnoreState;
            TestPlayState = TestPlayStateEnum.Paused;

            _testProtocolsManager = new TestProtocolsManager();
            _tempVisitedItems = new List<VisitedItem>();
            
            var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (!designMode)
                FillLocalLookupIds();
        }

        #endregion

        #region Methods

        #region Initialization & Binding

        /// <summary>
        /// Perform the initialization of the test issue
        /// </summary>
        public void PerformSpecificIntializationSteps(bool isNew)
        {
            ControlItemsNavGrid.IsMainIssue = IsMainIssue;

            TabTypeImage = Resources.Issue_16;
            CurrentTestIssue = CurrentTestIssue ?? new TestIssue();
            _settingsManager = new SettingsManager();

            if (IsMainIssue)
            {
                layoutControlGroupIssue.Text = "Items Investigation";
                layoutControlGroupIssueResults.Text = "Results";
            }

            layoutControlItemBalance.Visibility = IsMainIssue ? LayoutVisibility.Always : LayoutVisibility.Never;
            emptySpaceItemBalancing.Visibility = IsMainIssue ? LayoutVisibility.Always : LayoutVisibility.Never;

            InitializeFonts();
        }

        /// <summary>
        /// Initializes the font settings.
        /// </summary>
        private void InitializeFonts()
        {
            var fontSize =
                   _settingsManager.GetSetting(new SettingsFilter() { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });

            gridViewTestResults.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
            gridViewTestResultFactors.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
        }

        /// <summary>
        /// Does the binding of the fields in the issueK
        /// </summary>
        public void SetBinding()
        {
            if (CurrentTestIssue != null)
            {
                CurrentTestIssue.TestResults.RaiseListChangedEvents = false;

                if (!IsMainIssue)
                {
                    ParentTab.Text = CurrentTestIssue.IssueNameAndNumber;
                    UiHelperClass.BindControl(ParentTab, CurrentTestIssue, () => CurrentTestIssue.IssueNameAndNumber);   
                }
                
                UiHelperClass.BindControl(gridControlTestResults, CurrentTestIssue, () => CurrentTestIssue.TestResults);

                RefreshResultsFilter();

                xtraUserControlItemsNavGridIssueItems.TestResults = CurrentTestIssue.TestResults;
                xtraUserControlItemsNavGridIssueItems.CurrentIssue = CurrentTestIssue;

            
                //Add the temp test result for unbalance for factor.
                var tempResults = new BindingList<TestResult>();

                //Store the results to be modified in temp collection to prevent results collection
                //modification during loop which causes an exception
                foreach (var testResult in CurrentTestIssue.TestResults)
                {                    
                    if (testResult.TestResultFactors != null && testResult.TestResultFactors.Count > 0)
                    {
                        tempResults.Add(testResult);
                    }
                }
                
                gridViewTestResults.MoveFirst();
            }

            if (CurrentTestIssue != null)
                CurrentTestIssue.TestResults.RaiseListChangedEvents = true;

            ProcessSelectedTestResultChanged();

            if (!IsMainIssue)
            {
                ParentTab.Refresh();
                ParentTab.Update(); 
            }
        }

        /// <summary>
        /// Bind the issue items.
        /// </summary>
        public void BindIssueItems()
        {
            if (CurrentTestIssue == null || IsIssueItemsBinded) 
                return;

            var currentTestResult = GetCurrentTestResult(CurrentTestIssue.TestResults);

            var items = new BindingList<Item>();

            if (CurrentTestIssue.TestResults.Count > 0)
                if (currentTestResult != null)
                {
                    if (currentTestResult.Item != null)
                        items =
                            IssueItemsManager.GetItemChildren(new SingleItemFilter()
                                                                  {ItemId = currentTestResult.Item.Id});
                    else if (currentTestResult.StepType != null)
                    {
                        if (currentTestResult.StepType.Value == EnumNameResolver.Resolve(ItemTypeEnum.Product))
                        {
                            items = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Products);
                        }
                        else
                        {
                            items = IssueItemsManager.GetItems(new ItemsFilter()
                            {
                                TypeLookupId =
                                    currentTestResult.StepType != null
                                        ? currentTestResult.StepType.Id
                                        : 0
                            });
                        }
                    }
                }
                else
                {
                    var firstTestResult = CurrentTestIssue.TestResults[0];

                    if (firstTestResult.Item != null)
                    {
                        items = IssueItemsManager.GetItemChildren(new SingleItemFilter() { ItemId = firstTestResult.Item.Id });
                    }
                    else if (firstTestResult.StepType != null)
                    {
                        if (firstTestResult.StepType.Value == EnumNameResolver.Resolve(ItemTypeEnum.Product))
                        {
                            items = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Products);
                        }
                        else
                        {
                            items = IssueItemsManager.GetItems(new ItemsFilter()
                            {
                                TypeLookupId =
                                    firstTestResult.StepType != null
                                        ? firstTestResult.StepType.Id
                                        : 0
                            });
                        }
                    }
                }
            else
                items = IssueItemsManager.GetItemChildren(new SingleItemFilter() {ItemId = CurrentTestIssue.Item.Id});

            xtraUserControlItemsNavGridIssueItems.InitGridItems(items, true, false, false);

            IsIssueItemsBinded = true;
        }

        /// <summary>
        /// Refreshes the results on filtering.
        /// </summary>
        private void RefreshResultsFilter()
        {
            var strIsSelected = "[" + ExpressionHelper.GetPropertyName(() => new TestResult().IsSelected) + "]";
            var strReading = "[" + ExpressionHelper.GetPropertyName(() => new TestResultFactor().Reading) + "]";
            gridViewTestResultFactors.ActiveFilterString = strReading + " <> 0 AND (" + strReading + " <= " +
                                                           StaticKeys.MeterMinAcceptableReading + " OR " +
                                                           strReading + " >= " +
                                                           StaticKeys.MeterMaxAcceptableReading + ")";

            gridViewTestResults.ActiveFilterString = strIsSelected + " = true";
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public void ClearBinding()
        {
            if (!IsMainIssue)
            {
                ParentTab.DataBindings.Clear();
            }
            
            gridControlTestResults.DataBindings.Clear();
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public void ClearHandlers()
        {
            
        }

        /// <summary>
        /// Process Selected Test Result Changed.
        /// </summary>
        public void ProcessSelectedTestResultChanged()
        {
            CheckButtonsForEnable(false);

            if (gridViewTestResults.SelectedRowsCount == 1)
            {
                var testResult = gridViewTestResults.GetRow(gridViewTestResults.GetSelectedRows().FirstOrDefault()) as TestResult;

                if (testResult == null) 
                    return;

                InvokeSelectedItemChanged(this,true, testResult.Item, 0);

            }
            else
            {
                InvokeSelectedItemChanged(this,true, null, 0);
            }

        }

        /// <summary>
        /// Fill the lookup controls with the collections of objects from the cache
        /// </summary>
        private void FillLookUps()
        {
            if (CurrentTestIssue.Test == null || Protocols == null) return;

            gridLookUpEditNextProtocolStep.Properties.DataSource = Protocols;

            gridLookUpEditNextProtocolStep.EditValue = CurrentTestIssue.Test.TestProtocol != null
                                                           ? CurrentTestIssue.Test.TestProtocol.Id
                                                           : (Protocols.FirstOrDefault() != null ? Protocols.FirstOrDefault().Id : 0);
        }

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            var yesLookup = IssueLookupsManager.GetLookups(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes)).FirstOrDefault();
            var noLookup = IssueLookupsManager.GetLookups(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.No)).FirstOrDefault();
            var lookupProduct = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product)).FirstOrDefault();
            var noneListTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.None)).FirstOrDefault();

            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;
            _noLookupId = noLookup != null ? noLookup.Id : 0;
            _lookupProductId = lookupProduct != null ? lookupProduct.Id : 0;
            _noneListTypeLookupId = noneListTypeLookup != null ? noneListTypeLookup.Id : 0;

        }

        /// <summary>
        /// Set the edit mode of the user control
        /// </summary>
        /// <param name="isReadOnly">The is read only flag.</param>
        /// <param name="isChangedByUser">Indicate that the changed happend by a user click on edit button and not initialization</param>
        public void SetEditMode(bool isReadOnly, bool isChangedByUser)
        {
            _isEditable = !isReadOnly;            
            gridViewTestResults.OptionsBehavior.ReadOnly = true;
            gridViewTestResults.OptionsBehavior.Editable = false;
            gridViewTestResultFactors.OptionsBehavior.ReadOnly = true;
            gridViewTestResultFactors.OptionsBehavior.Editable = false;
            xtraUserControlItemsNavGridIssueItems.SetEditMode(isReadOnly);
            gridLookUpEditNextProtocolStep.Properties.ReadOnly = isReadOnly;
            gridLookUpEditNextProtocolStep.Properties.Buttons[1].Enabled = !isReadOnly;
            CheckButtonsForEnable(isReadOnly);
        }

        /// <summary>
        /// Create an XtraTabPage and put the user control in it
        /// </summary>
        private XtraTabPage CreateTab()
        {
            //Creates a tab instance
            var parentTab = new XtraTabPage { PageVisible = true };
            //Page should be set to visible or it won't appear by default
            //Set the dock to fill so it will fill all the available area
            if (!IsMainIssue)
            {
                Dock = DockStyle.Fill;
                //Add the current entity user control in it
                parentTab.Controls.Add(this);
            }
            
            //Add the tab to the tab pages and give it focus
            return parentTab;
        }

        /// <summary>
        /// Sets the icon for the tab, this also includes error icon if there are any errors
        /// </summary>
        public void SetTabIcon()
        {
            if (!IsMainIssue)
            {
                //Set the icon depending on the tab type and if there are any errors then add error icon
                ParentTab.Image = (CurrentTestIssue.IsValid) ? MergeImages(false) : MergeImages(true);
                ParentTab.Tooltip = (CurrentTestIssue.IsValid) ? null : CurrentTestIssue.ErrorSummary;
            }
        }

        /// <summary>
        /// Call common actions when creating new issue or when opening an existing one
        /// </summary>
        private XtraTabPage StartCommonInitializationActions(bool isNew, TestIssue testIssue)
        {
            //Create a tab and put the current entity user control in the tab page created
            var currentTab = CreateTab();
            InitializeTabObject(isNew, testIssue);
            FillLookUps();
            SetBinding();

            return currentTab;
        }


        /// <summary>
        /// This will initialize the tab object by setting its subclass type and tying it to an event handler
        /// that will monitor property changes in the business object
        /// </summary>
        /// <param name="isNew"></param>
        /// <param name="testIssue"></param>
        private void InitializeTabObject(bool isNew, TestIssue testIssue)
        {
            //Create the business object
            PerformSpecificIntializationSteps(isNew);
            //Set the data source for the Error Providor control and initialize it
            dxErrorProviderMain.DataSource = testIssue;
            dxErrorProviderMain.ClearErrors();
            testIssue.PropertyChanged += TestIssue_PropertyChanged;
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
            CurrentTestIssue.Validate();
            dxErrorProviderMain.UpdateBinding();
            SetTabIcon();
            ShowHideErrorIcons();
            return CurrentTestIssue.IsValid;
        }

        /// <summary>
        /// Shows or hides the error icons in the tabs for the whole user control and detail controls
        /// </summary>
        public void ShowHideErrorIcons()
        {
            SetTabIcon();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Opens the tab of the test issue.
        /// </summary>
        /// <param name="openedTestIssue">The opend issue.</param>
        /// <param name="isReadOnly">The is read only flag.</param>
        /// <returns></returns>
        public XtraTabPage Open(TestIssue openedTestIssue, bool  isReadOnly)
        {
            XtraTabPage tabPage = null;

            //If the object is null, then it will be a special type tab
            if (openedTestIssue != null)
            {
                //Set the initialized object of the tab to the passed object
                CurrentTestIssue = openedTestIssue;

                //Call some methods that are common when opening a new tab or an existing object
                tabPage = StartCommonInitializationActions(false, openedTestIssue);
                SetEditMode(isReadOnly, false);
                SetTabIcon();
                tabPage.Padding = new Padding(0);
            }
            return tabPage;
        }

        /// <summary>
        /// Confirm the Unselect / Delete for a test result.
        /// </summary>
        /// <returns></returns>
        private static bool CanUnselectTestResult()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.DeleteMultipleConfirmQuestion);

            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Unselects TestResult.
        /// </summary>
        /// <param name="testResult">The test result.</param>
        public void UnselectTestResult(TestResult testResult, bool hasMultipleItems)
        {
            if (testResult == null) return;

            testResult.IsSelected = false;
            testResult.VitalForce = null;
            testResult.RatioItem = null;

            ApplyImprintActionSingleResult(testResult, ImprintingAction.RemoveFromImprintList, !hasMultipleItems);

            if (testResult.Item != null)
            {
                xtraUserControlItemsNavGridIssueItems.UpdateAfterDeletion(testResult.Item);

                //Delete corresponding schedule line for deleted item in case it was a product and a schedule line
                //got added for it.
                if (IsProduct(testResult) &&
                    CurrentTest.TestSchedule != null && 
                    CurrentTest.TestSchedule.ScheduleLines != null)
                {
                    var productScheduleLine =
                        CurrentTest.TestSchedule.ScheduleLines.FirstOrDefault(s => s.Item.Id == testResult.Item.Id);

                    if (productScheduleLine != null)
                    {
                        CurrentTest.TestSchedule.ScheduleLines.Remove(productScheduleLine);
                    }
                }
            }

            //Rest the four factors if exist.
            if (testResult.TestResultFactors != null)
            {
                foreach (var factor in testResult.TestResultFactors)
                {
                    factor.Reading = 0;
                }
            }

            var factorsGroupTestResult =
                CurrentTestIssue.TestResults.FirstOrDefault(
                    tr => tr.SelectedParentResultId == testResult.Id && tr.ObjectState == DomainEntityState.Temp);

            if (factorsGroupTestResult != null)
            {
                for (var i = 0; i < CurrentTestIssue.TestResults.Count; i++)
                {
                    var testResultToCheck = CurrentTestIssue.TestResults[i];

                    if (testResultToCheck.Id == factorsGroupTestResult.Id || 
                        (testResultToCheck.SelectedParentResultId == factorsGroupTestResult.Id &&
                        testResultToCheck.ObjectState == DomainEntityState.Temp))
                    {
                        CurrentTestIssue.TestResults.RemoveAt(i);
                        i--;
                    }
                }
            }

            var children =
                CurrentTestIssue.TestResults.Where(tr => tr.SelectedParentResultId == testResult.Id);

            foreach (var child in children)
            {
                child.SelectedParent = testResult.SelectedParent;
            }

            RefreshResultsGrid();

            InvokeUpdateOnTestResults(this);

            var currentTestResult = ControlItemsNavGrid.GetCurrentTestResult();

            //after deleting the test result, the item should appear again in the items grid.
            if(testResult.Item != null && currentTestResult != null && testResult.Parent != null && testResult.Parent.Id == currentTestResult.Id )
            {
                /*Step back to keep the hilighting on the current step, since the setItems which is called after
                 * the AddItemAfterDeletion, will increment the current step by (1)*/
                if(ControlItemsNavGrid.CurrentStep > 1)
                    ControlItemsNavGrid.CurrentStep -= 1; 
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
                || CurrentTestIssue == null
                || CurrentTestIssue.TestResults == null)
                return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
            TestResult lastTestResultAdded = null;

            SetImageIgnoreState(true);

            foreach (var selectedItem in items)
            {
                if (selectedItem == null) continue;               

                var testResultToAdd = new TestResult()
                {
                    TestIssue = CurrentTestIssue,
                    DateTime = DateTime.Now,

                    Item = selectedItem,
                    IsSelected = true,
                    TestResultFactors = new BindingList<TestResultFactor>(),
                    Parent =
                        CurrentTestIssue.TestResults.FirstOrDefault(tr => tr.IsCurrent) ??
                        CurrentTestIssue.TestResults.FirstOrDefault(),
                    Id =
                        CurrentTestIssue.TestResults.Count > 0
                            ? NextResultId
                            : 1,
                    TempImprintingId = CurrentTest.GetNextTestResultId()
                };

                //Check for VF Duplication.
                testResultToAdd.VitalForce = TestsManager.GetVitalForceForDuplicatedTestResult(CurrentTestIssue.Test, testResultToAdd);

                CheckTestResultRelationShips(testResultToAdd);

                testResultToAdd.SetUserAndDates();

                CurrentTestIssue.TestResults.Add(testResultToAdd);

                RefreshResultsGrid();

                InvokeUpdateOnTestResults(this);

                gridViewTestResults.FocusedRowHandle =
                        gridViewTestResults.ViewRowHandleToDataSourceIndex(
                            CurrentTestIssue.TestResults.IndexOf(testResultToAdd));

                if (selectedItem.Properties.HasProperty(PropertiesEnum.IsImprintable, _yesLookupId.ToString()))
                {
                    ApplyImprintActionSingleResult(testResultToAdd, ImprintingAction.Imprint, items.Count == 1);
                }

                PerformAutomatedResultActions(testResultToAdd,selectedItem);
                lastTestResultAdded = testResultToAdd;
            }

            if (ControlItemsNavGrid.CurrentStep == 1 && ControlItemsNavGrid.IsStepAutomated)
                ControlItemsNavGrid.ForceStepIncrement = true;

            ControlItemsNavGrid.RefreshItems(true, true);

            //Adding the items of the automatic step [GoTo] as a visited items.
            AddGotoItemsToVisitedList();

            SetImageIgnoreState(false);

            //Show the image of the added test result so technician can see it after it gets added
            if (lastTestResultAdded != null)
            {
                InvokeSelectedItemChanged(this, true, lastTestResultAdded.Item, 0);    
            }
            
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Adds the items in a temp list to the visited items.
        /// </summary>
        private void AddGotoItemsToVisitedList()
        {
            foreach (var visitedItem in _tempVisitedItems)
            {
                ControlItemsNavGrid.VisitedItems.Add(visitedItem);    
            }
            
            _tempVisitedItems.Clear();
        }

        /// <summary>
        /// Performs the issue auto actions.
        /// </summary>
        public void PerformIssueAutoActions()
        {
            if (TestPlayState == TestPlayStateEnum.Playing)
            {
                if (CurrentTestIssue.TestResults.Count == 1)
                {
                    var firstResult = CurrentTestIssue.TestResults[0];
                    var currentItem = firstResult.Item;
                    PerformAutomatedResultActions(firstResult, currentItem);                    
                }
            }
        }

        /// <summary>
        /// Perform actions dependent on properties like VF, 4F and Top 10
        /// </summary>
        /// <param name="currentResult">The current test result.</param>
        /// <param name="currentItem">The current item.</param>
        public void PerformAutomatedResultActions(TestResult currentResult,Item currentItem)
        {
            UiHelperClass.HideSplash();

            if (TestPlayState == TestPlayStateEnum.Playing)
            {
                if (currentResult.VitalForce == null &&  currentItem.Properties.HasProperty(PropertiesEnum.HasVitalForce, _yesLookupId.ToString()))
                {
                    SetVitalForce(false);
                }

                if (currentResult.RatioItem == null && currentItem.Properties.HasProperty(PropertiesEnum.HasRatios, _yesLookupId.ToString()))
                {
                    SetRatios();
                }

                if (currentResult.TestResultFactors.Count == 0 && currentItem.Properties.HasProperty(PropertiesEnum.HasFourFactors, _yesLookupId.ToString()))
                {
                    SetFourFactor();
                }

                var hasGotoProperty = currentItem.Properties.HasProperty(PropertiesEnum.GoTo);

                if (!ProductSeatsHelper.ItemHasChilds(currentItem, true) && hasGotoProperty)
                {
                    var goToPropertyValue =int.Parse(currentItem.Properties.GetPropertyValue(PropertiesEnum.GoTo).ToString());
                    OpenGoToPropertyItem(goToPropertyValue);
                }

                if (!ProductSeatsHelper.ItemHasChilds(currentItem, true) && currentItem.Properties.HasProperty(PropertiesEnum.GoToCustomDilutions, _yesLookupId.ToString()))
                {
                    OpenCustomDilution();
                }

                if (IsProduct(currentResult))
                {
                    //Check if the product schedule will be skipped before opening dosage dialog
                    if (CsaEmdUnitManager.Instance.AutoOpenProductDosages && !SkipScheduleForProduct(currentResult))
                    {
                        SetProductDosages();
                    }
                }
            }
        }

        /// <summary>
        /// Return true if a product shouldn't have a schedule line based on a condition
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public bool SkipScheduleForProduct(TestResult testResult)
        {
            //In this method we handle a case where a product added as a test result is not supposed to have a schedule line added for it since its parent (direct or indirect)
            //is also a product and was added as a test result and was added to schedule, in this case, the child product should be added to imprint list but not to product
            //schedule.
            
            //Below we are handling 3 levels of parents since parent product might not be the direct parent of current product, we think 3 levels should be enough to handle most
            //cases.

            var parent1 = testResult == null ? null : testResult.Parent;
            var parent2 = parent1 == null ? null : parent1.Parent;
            var parent3 = parent2 == null ? null : parent2.Parent;

            var item1 = parent1 == null ? null : parent1.Item;
            var item2 = parent2 == null ? null : parent2.Item;
            var item3 = parent3 == null ? null : parent3.Item;

            var skip1 = IsProduct(item1) && GetTestResultProductScheduleLine(parent1) != null;
            var skip2 = IsProduct(item2) && GetTestResultProductScheduleLine(parent2) != null;
            var skip3 = IsProduct(item3) && GetTestResultProductScheduleLine(parent3) != null;

            return skip1 || skip2 || skip3;
        }

        /// <summary>
        /// Check the passed test result is product.
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public bool IsProduct(TestResult testResult)
        {
            return testResult != null && IsProduct(testResult.Item);
        }

        /// <summary>
        /// Check the passed item is product.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsProduct(Item item)
        {
            return item != null && item.TypeLookup != null &&
                   item.TypeLookup.Id == _lookupProductId && item.ListTypeLookup != null && item.ListTypeLookup.Id == _noneListTypeLookupId;
        }

        private ScheduleLine GetTestResultProductScheduleLine(TestResult testResult)
        {
            return testResult == null || testResult.Item == null ? null: GetItemProductScheduleLine(testResult.Item);
        }

        private ScheduleLine GetItemProductScheduleLine(Item item)
        {
            return item == null
                ? null
                : CurrentTestIssue.Test.TestSchedule.ScheduleLines.FirstOrDefault(sl => sl.Item != null && sl.Item.Id == item.Id);
        }

        /// <summary>
        /// Set the Product Dosages for the passed product.
        /// </summary>
        private void SetProductDosages()
        {
            if (!gridViewTestResults.IsDataRow(gridViewTestResults.FocusedRowHandle)) return;
            InvokeMeterDialogOpen(true);
            var selectedTestResult = gridViewTestResults.GetFocusedRow() as TestResult;

            if (selectedTestResult == null ||!IsProduct(selectedTestResult))
                return;

            var productScheduleLine = GetTestResultProductScheduleLine(selectedTestResult);

            if(productScheduleLine == null)
            {
                productScheduleLine = new ScheduleLine { Item = selectedTestResult.Item, TestSchedule = CurrentTestIssue.Test.TestSchedule };
                TestsManager.SetScheduleLineDefaultValues(productScheduleLine);
                CurrentTestIssue.Test.TestSchedule.ScheduleLines.Add(productScheduleLine);
            }
                                            
            xtraUserControlItemsNavGridIssueItems.CancelReading();

            var productDosagesDilaog = new XtraFormProductDosages { ScheduleLine = productScheduleLine };

            productDosagesDilaog.ShowDialog();

            InvokeActivateConnectionRequest(this);

            xtraUserControlItemsNavGridIssueItems.StartReading();
            InvokeMeterDialogOpen(false);
        }

        /// <summary>
        /// Update the tab test issue and other stuff during undo process in parent form
        /// </summary>
        /// <param name="openedTestIssue">The opened issue.</param>
        /// <param name="isEditable">The is editable.</param>
        public void Update(TestIssue openedTestIssue, bool isEditable)
        {
            if (openedTestIssue != null)
            {
                _isEditable = isEditable;
                CurrentTestIssue = openedTestIssue;
                InitializeTabObject(false, CurrentTestIssue);
                ClearBinding();
                SetBinding();
                SetEditMode(!_isEditable, false);
                SetTabIcon();
                RefreshResultsGrid();
            }
        }

        /// <summary>
        /// Initialize a new tab for the business object
        /// </summary>
        public XtraTabPage New(TestIssue testIssue)
        {
            var tabPage = StartCommonInitializationActions(true, testIssue);
            SetTabIcon();
            dxErrorProviderMain.ClearErrors();
            return tabPage;
        }

        #endregion

        #region General Common Functionalities

        /// <summary>
        /// Merge the images of error icon and the icon for the tab.
        /// </summary>
        /// <param name="isErrorIncluded">Determine if the error icon should be included or not.</param>
        /// <returns></returns>
        public Image MergeImages(bool isErrorIncluded)
        {
            Image tempImage = Resources.TabEmptyIcon;

            //Put the image in a graphics object for editing
            Graphics graphics = Graphics.FromImage(tempImage);

            if (TabTypeImage != null)
            {
                graphics.DrawImage(GetThumbnailImage(TabTypeImage), 1, 1, 16, 16);
                graphics.DrawImage(GetThumbnailImage(TabTypeImage), 1, 1, 16, 16);
            }

            if (isErrorIncluded)
            {
                graphics.DrawImage(Resources.Error, 18, 1, 16, 16);
                graphics.Dispose();
                return tempImage;
            }

            graphics.Dispose();

            return GetThumbnailImage(TabTypeImage);
        }

        /// <summary>
        /// Return the passed image but with a smaller size.
        /// </summary>
        /// <param name="largeImage">The large image.</param>
        /// <returns></returns>
        public Image GetThumbnailImage(Image largeImage)
        {
            return largeImage.GetThumbnailImage(16, 16, ThumbnailTargetMethod, new IntPtr());
        }

        /// <summary>
        /// This method is used only as a parameter for the GetThumbnailImageAbort method of Image type
        /// </summary>
        /// <returns></returns>
        public bool ThumbnailTargetMethod()
        {
            return false;
        }

        #endregion

        #region Logic

        #region Test Results Handling

        /// <summary>
        /// Gets the result that is marked is current.
        /// </summary>
        /// <returns>The current test result.</returns>
        private static TestResult GetCurrentTestResult(BindingList<TestResult> testResult)
        {
            return testResult.FirstOrDefault(tr => tr.IsCurrent);
        }

        /// <summary>
        /// Set the vital force for the current selected test result
        /// </summary>
        private void SetVitalForce(bool forceToOpen)
        {
            if (!gridViewTestResults.IsDataRow(gridViewTestResults.FocusedRowHandle)) return;
            InvokeMeterDialogOpen(true);
            var selectedTestResult = gridViewTestResults.GetFocusedRow() as TestResult;

            if (selectedTestResult == null) return;

            var existsVitalForce = TestsManager.GetVitalForceForDuplicatedTestResult(CurrentTestIssue.Test, selectedTestResult);

            if (existsVitalForce == null || forceToOpen)
            {
                xtraUserControlItemsNavGridIssueItems.CancelReading();

                var frmVitalForce = new XtraFormVitalForce
                {
                    VitalForce = selectedTestResult.VitalForce,
                    ItemName = selectedTestResult.Item.Name,
                    TestPlayState = TestPlayState
                };

                var result = frmVitalForce.ShowDialog();

                if (result == DialogResult.OK)
                {
                    selectedTestResult.VitalForce = frmVitalForce.VitalForce;

                    RefreshResultsGrid();

                    TestsManager.SetVitalForceForDuplicatedTestResults(CurrentTestIssue.Test, selectedTestResult, selectedTestResult.VitalForce);

                    InvokeUpdateOnTestResults(this);
                }

                InvokeActivateConnectionRequest(this);

                xtraUserControlItemsNavGridIssueItems.StartReading();
            }
            else
            {
                selectedTestResult.VitalForce = existsVitalForce;
            }
            InvokeMeterDialogOpen(false);
        }

        /// <summary>
        /// Set the ratios for the current selected test result
        /// </summary>
        private void SetRatios()
        {
            if (!gridViewTestResults.IsDataRow(gridViewTestResults.FocusedRowHandle)) return;
            InvokeMeterDialogOpen(true);
            var selectedTestResult = gridViewTestResults.GetFocusedRow() as TestResult;

            if (selectedTestResult == null) return;

            xtraUserControlItemsNavGridIssueItems.CancelReading();

            var frmRatios = new XtraFormRatios
            {
                RatioItem = selectedTestResult.RatioItem,
                ItemName = selectedTestResult.Item.Name,
                TestPlayState = TestPlayState
            };

            var result = frmRatios.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedTestResult.RatioItem = frmRatios.RatioItem;

                RefreshResultsGrid();

                InvokeUpdateOnTestResults(this);
            }

            InvokeActivateConnectionRequest(this);

            xtraUserControlItemsNavGridIssueItems.StartReading();

            InvokeMeterDialogOpen(false);
        }

        /// <summary>
        /// Set the four factors for certain test result
        /// </summary>
        private void SetFourFactor()
        {
            if (!gridViewTestResults.IsDataRow(gridViewTestResults.FocusedRowHandle)) return;
            InvokeMeterDialogOpen(true);
            var selectedTestResult = gridViewTestResults.GetFocusedRow() as TestResult;

            if (selectedTestResult == null) return;

            xtraUserControlItemsNavGridIssueItems.CancelReading();

            var frmFourFactors = new XtraFormFourFactors
            {
                ResultFactors = selectedTestResult.TestResultFactors,
                ItemName = selectedTestResult.Item.Name,
                TestResult = selectedTestResult,
                TestPlayState = TestPlayState
            };

            frmFourFactors.ShowDialog();

            InvokeActivateConnectionRequest(this);

            xtraUserControlItemsNavGridIssueItems.StartReading();

            selectedTestResult.TestResultFactors = frmFourFactors.ResultFactors;

            RefreshResultsGrid();

            InvokeUpdateOnTestResults(this);
            InvokeMeterDialogOpen(false);
        }

        /// <summary>
        /// Refresh the grid data source.
        /// </summary>
        private void RefreshResultsGrid()
        {
            gridViewTestResults.RefreshData();
            gridControlTestResults.RefreshDataSource();
            gridViewTestResults.RefreshRow(gridViewTestResults.FocusedRowHandle);
            gridControlTestResults.Focus();
            gridViewTestResults.Focus();
        }

        /// <summary>
        /// Check for the parents and children relation ships.
        /// </summary>
        /// <param name="testResult">The test result.</param>
        private void CheckTestResultRelationShips(TestResult testResult)
        {
            CheckParentRelationSheps(testResult);
            CheckChildrenRelationShips(testResult, testResult);
        }

        /// <summary>
        /// Check for parents relation ships.
        /// </summary>
        /// <param name="testResult">The test result to check.</param>
        public void CheckParentRelationSheps(TestResult testResult)
        {
            var parentToCheck = testResult.Parent;

            while (parentToCheck != null)
            {
                if (parentToCheck.IsSelected)
                {
                    testResult.SelectedParent = parentToCheck;
                    break;
                }

                parentToCheck = parentToCheck.Parent;
            }
        }

        /// <summary>
        /// Check for children relation ships.
        /// </summary>
        /// <param name="testResult">The test result to check.</param>
        /// <param name="parentToAdd">The parent to add.</param>
        private void CheckChildrenRelationShips(TestResult testResult, TestResult parentToAdd)
        {
            var children =
                CurrentTestIssue.TestResults.Where(
                    tr => tr.Parent != null && tr.Parent.Id == testResult.Id);

            foreach (var child in children)
            {
                CheckChildrenRelationShips(child, parentToAdd);

                if (child.SelectedParent != null && child.Parent != null && child.Parent.Item != null && child.SelectedParent.Item.Id == child.Parent.Item.Id) return;

                child.SelectedParent = parentToAdd;
            }
        }

        /// <summary>
        /// Updates the test result datasource.
        /// </summary>
        public void UpdateTestResult()
        {
            CurrentTestIssue.TestResults = xtraUserControlItemsNavGridIssueItems.TestResults;
        }

        /// <summary>
        ///  Opens the bacteria list.
        /// </summary>
        private void OpenBacteria()
        {
            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

            ControlItemsNavGrid.CheckOneByOneModeForDisable();

            var lookupBacteria = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Bacteria)).FirstOrDefault();

            var items = IssueItemsManager.GetItems(new ItemsFilter()
            {
                TypeLookupId = lookupBacteria.Id,
            });

            var parent = xtraUserControlItemsNavGridIssueItems.GetCurrentTestResult();

            //Any test result should has a value for the id to let the 'mark as test result' function work fine.
            var testStep = new TestResult()
            {
                IsCurrent = true,
                IsSelected = false,
                Item = null,
                Parent = parent,
                DateTime = DateTime.Now,
                StepType = lookupBacteria,
                TestIssue = this.CurrentTestIssue,
                Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                TempImprintingId = CurrentTest.GetNextTestResultId()
            };


            if (parent != null)
                parent.IsCurrent = false;

            xtraUserControlItemsNavGridIssueItems.TestResults.Add(testStep);

            xtraUserControlItemsNavGridIssueItems.BackClicks += 1;

            xtraUserControlItemsNavGridIssueItems.InitGridItems(items, true, false, false);

            xtraUserControlItemsNavGridIssueItems.SetCurrentNodeLabel(StaticKeys.Bacteria);

            //adding the visited item from type lookup.
            ControlItemsNavGrid.VisitedItems.Add(new VisitedItem() { ItemId = lookupBacteria.Id, Type = StaticKeys.VisitedItemTypeLookup });

            xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
            xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
            xtraUserControlItemsNavGridIssueItems.PartHilight = 1;
            xtraUserControlItemsNavGridIssueItems.ResetHideShowItemsFeature();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Opens the products list.
        /// </summary>
        public void OpenProducts()
        {
            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

            ControlItemsNavGrid.CheckOneByOneModeForDisable();

            var lookupProduct = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product)).FirstOrDefault();

            var items = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Products);

            var parent = xtraUserControlItemsNavGridIssueItems.GetCurrentTestResult();

            //Any test result should has a value for the id to let the 'mark as test result' function work fine.
            var testStep = new TestResult()
            {
                IsCurrent = true,
                IsSelected = false,
                Item = null,
                Parent = parent,
                DateTime = DateTime.Now,
                StepType = lookupProduct,
                TestIssue = this.CurrentTestIssue,
                Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                TempImprintingId = CurrentTest.GetNextTestResultId()
            };


            if (parent != null)
                parent.IsCurrent = false;

            xtraUserControlItemsNavGridIssueItems.TestResults.Add(testStep);

            xtraUserControlItemsNavGridIssueItems.BackClicks += 1;
            xtraUserControlItemsNavGridIssueItems.InitGridItems(items, true, false, false);
            xtraUserControlItemsNavGridIssueItems.SetCurrentNodeLabel(StaticKeys.Products);

            //adding the visited item from the type lookup.
            ControlItemsNavGrid.VisitedItems.Add(new VisitedItem() { ItemId = lookupProduct.Id, Type = StaticKeys.VisitedItemTypeLookup });

            xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
            xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
            xtraUserControlItemsNavGridIssueItems.PartHilight = 1;
            xtraUserControlItemsNavGridIssueItems.ResetHideShowItemsFeature();

            ControlItemsNavGrid.Focus();
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Opens the custom dilutions list.
        /// </summary>
        private void OpenCustomDilution()
        {
            var currentTestResult = ControlItemsNavGrid.GetCurrentTestResult();

            if (currentTestResult != null && currentTestResult.Item != null && currentTestResult.Item.Name == StaticKeys.CustomDilutions) return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

            /*to avoid the applying of this function [SelectNextRowBaseOnUserOldSelection()] 
             *at automatic navigation to custom dilutions case since its only applicable when
             *the grid has the same data source.
             */
            ControlItemsNavGrid.NextRowToSelectBaseOnUserSelection = -1;

            var customDilutionItem = _issueItemsManager.GetItems(new ItemsFilter { Name = StaticKeys.CustomDilutions }).FirstOrDefault();

            if (customDilutionItem != null)
            {
                var parent = ControlItemsNavGrid.GetCurrentTestResult();

                //Any test result should has a value for the id to let the 'mark as test result' function work fine.
                var testStep = new TestResult()
                {
                    IsCurrent = true,
                    IsSelected = false,
                    Item = customDilutionItem,
                    Parent = parent,
                    DateTime = DateTime.Now,
                    StepType = null,
                    TestIssue = this.CurrentTestIssue,
                    Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                    TempImprintingId = CurrentTest.GetNextTestResultId()
                };

                if (parent != null)
                    parent.IsCurrent = false;

                ControlItemsNavGrid.TestResults.Add(testStep);

                ControlItemsNavGrid.BackClicks += 1;

                var customDilutionItems = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.CustomDilutions);

                ControlItemsNavGrid.SetItems(customDilutionItems, true, false, false);

                ControlItemsNavGrid.SetCurrentNodeLabel(customDilutionItem.Name);

                //adding the visited item from type item.
                ControlItemsNavGrid.VisitedItems.Add(new VisitedItem() { ItemId = customDilutionItem.Id, Type = StaticKeys.VisitedItemTypeItem });
            }

            xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
            xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
            xtraUserControlItemsNavGridIssueItems.PartHilight = 1;
            xtraUserControlItemsNavGridIssueItems.ResetHideShowItemsFeature();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Open the item for the go to property
        /// </summary>
        private void OpenGoToPropertyItem(int itemId)
        {
            var currentTestResult = ControlItemsNavGrid.GetCurrentTestResult();

            if (currentTestResult != null &&
               currentTestResult.Item != null &&
               currentTestResult.Item.Name == StaticKeys.TopTen)
                return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

            ControlItemsNavGrid.CheckOneByOneModeForDisable();

            /*[Oday: Double Check]to avoid the applying of this function [SelectNextRowBaseOnUserOldSelection()] 
            *at automatic navigation to the top ten causes, since its only applicable when 
            *the grid has the same data source.
            */
            ControlItemsNavGrid.NextRowToSelectBaseOnUserSelection = -1;

            var goToItem = _issueItemsManager.GetItemById(new SingleItemFilter() { ItemId = itemId });

            if (goToItem != null)
            {
                var parent = ControlItemsNavGrid.GetCurrentTestResult();

                //Any test result should has a value for the id to let the 'mark as test result' function work fine.
                var testStep = new TestResult()
                {
                    IsCurrent = true,
                    IsSelected = false,
                    Item = goToItem,
                    Parent = parent,
                    DateTime = DateTime.Now,
                    StepType = null,
                    TestIssue = this.CurrentTestIssue,
                    Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                    TempImprintingId = CurrentTest.GetNextTestResultId()
                };

                if (parent != null)
                    parent.IsCurrent = false;

                ControlItemsNavGrid.TestResults.Add(testStep);

                ControlItemsNavGrid.BackClicks += 1;

                var goToItemChilds = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Top10);

                ControlItemsNavGrid.InitGridItems(goToItemChilds, true, false, false);

                ControlItemsNavGrid.SetCurrentNodeLabel(goToItem.Name);

                /*adding the visited item from type item to a temp list. later on, it will go to the original visited items list.
                 using a temporary list here is a important to arrange the feature of when to select or not select all the items.
                 */
                if (_requestedManually)
                    ControlItemsNavGrid.VisitedItems.Add(new VisitedItem() { ItemId = goToItem.Id, Type = StaticKeys.VisitedItemTypeItem });
                else
                    _tempVisitedItems.Add(new VisitedItem() { ItemId = goToItem.Id, Type = StaticKeys.VisitedItemTypeItem });

            }

            xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
            xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
            xtraUserControlItemsNavGridIssueItems.PartHilight = 1;
            xtraUserControlItemsNavGridIssueItems.ResetHideShowItemsFeature();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Open the top ten causes list.
        /// </summary>
        private void OpenCauses()
        {
            var topTenItem = _issueItemsManager.GetItems(new ItemsFilter { Name = StaticKeys.TopTen }).FirstOrDefault();

            if (topTenItem != null)
            {
                ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

                _requestedManually = true;

                OpenGoToPropertyItem(topTenItem.Id);

                _requestedManually = false;
            }
        }

        /// <summary>
        /// Open the generic list
        /// </summary>
        private void OpenGeneric()
        {
            var currentTestResult = ControlItemsNavGrid.GetCurrentTestResult();

            if (currentTestResult != null && currentTestResult.Item != null && currentTestResult.Item.Name == StaticKeys.Generic) return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

            ControlItemsNavGrid.CheckOneByOneModeForDisable();
            /*to avoid the applying of this function [SelectNextRowBaseOnUserOldSelection()] 
             *at automatic navigation to custom dilutions case since its only applicable when
             *the grid has the same data source.
             */
            ControlItemsNavGrid.NextRowToSelectBaseOnUserSelection = -1;

            var genericItem = _issueItemsManager.GetItems(new ItemsFilter { Name = StaticKeys.Generic }).FirstOrDefault();

            if (genericItem != null)
            {
                var parent = ControlItemsNavGrid.GetCurrentTestResult();

                //Any test result should has a value for the id to let the 'mark as test result' function work fine.
                var testStep = new TestResult()
                {
                    IsCurrent = true,
                    IsSelected = false,
                    Item = genericItem,
                    Parent = parent,
                    DateTime = DateTime.Now,
                    StepType = null,
                    TestIssue = this.CurrentTestIssue,
                    Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                    TempImprintingId = CurrentTest.GetNextTestResultId()
                };

                if (parent != null)
                    parent.IsCurrent = false;

                ControlItemsNavGrid.TestResults.Add(testStep);

                ControlItemsNavGrid.BackClicks += 1;

                var genericItems = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.GenericList);

                ControlItemsNavGrid.SetItems(genericItems, true, false, false);

                ControlItemsNavGrid.SetCurrentNodeLabel(StaticKeys.GenericListTitle);

                //adding the visited item from type item.
                ControlItemsNavGrid.VisitedItems.Add(new VisitedItem() { ItemId = genericItem.Id, Type = StaticKeys.VisitedItemTypeItem });
            }

            xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
            xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
            xtraUserControlItemsNavGridIssueItems.PartHilight = 1;
            xtraUserControlItemsNavGridIssueItems.ResetHideShowItemsFeature();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Open the Four Causes list
        /// </summary>
        private void OpenFourCauses()
        {
            var currentTestResult = ControlItemsNavGrid.GetCurrentTestResult();

            if (currentTestResult != null && currentTestResult.Item != null && currentTestResult.Item.Name == StaticKeys.FourCauses) return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

            ControlItemsNavGrid.CheckOneByOneModeForDisable();
            /*to avoid the applying of this function [SelectNextRowBaseOnUserOldSelection()] 
             *at automatic navigation to custom dilutions case since its only applicable when
             *the grid has the same data source.
             */
            ControlItemsNavGrid.NextRowToSelectBaseOnUserSelection = -1;

            var fourCausesItem = _issueItemsManager.GetItems(new ItemsFilter { Name = StaticKeys.FourCauses }).FirstOrDefault();

            if (fourCausesItem != null)
            {
                var parent = ControlItemsNavGrid.GetCurrentTestResult();

                //Any test result should has a value for the id to let the 'mark as test result' function work fine.
                var testStep = new TestResult()
                {
                    IsCurrent = true,
                    IsSelected = false,
                    Item = fourCausesItem,
                    Parent = parent,
                    DateTime = DateTime.Now,
                    StepType = null,
                    TestIssue = CurrentTestIssue,
                    Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                    TempImprintingId = CurrentTest.GetNextTestResultId()
                };

                if (parent != null)
                    parent.IsCurrent = false;

                ControlItemsNavGrid.TestResults.Add(testStep);

                ControlItemsNavGrid.BackClicks += 1;

                var fourCausesItems = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.FourCauses);

                ControlItemsNavGrid.SetItems(fourCausesItems, true, false, false);

                ControlItemsNavGrid.SetCurrentNodeLabel(StaticKeys.FourCausesListTitle);

                //adding the visited item from type item.
                ControlItemsNavGrid.VisitedItems.Add(new VisitedItem() { ItemId = fourCausesItem.Id, Type = StaticKeys.VisitedItemTypeItem });
            }

            xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
            xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
            xtraUserControlItemsNavGridIssueItems.PartHilight = 1;
            xtraUserControlItemsNavGridIssueItems.ResetHideShowItemsFeature();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Delete passed test results.
        /// </summary>
        /// <param name="testresults">Test results to delete.</param>
        private void DeleteTestResults(IEnumerable<TestResult> testresults)
        {
            var hasMultipleItems = testresults.Count() > 1;

            if (hasMultipleItems)
            {
                SetImageIgnoreState(true);
            }

            if (!CanUnselectTestResult())
            {
                SetImageIgnoreState(false);
                return;
            }

            UiHelperClass.ShowWaitingPanel(StaticKeys.DeletingMessage);

            if (testresults == null)
                return;

            foreach (var testresult in testresults)
            {
                if (testresult == null || testresult.ObjectState == DomainEntityState.Temp)
                    continue;

                UnselectTestResult(testresult, hasMultipleItems);
            }

            if (hasMultipleItems)
            {
                SetImageIgnoreState(false);
            }

            if (gridViewTestResults.FocusedRowHandle >= 0 && gridViewTestResults.SelectedRowsCount == 0)
            {
                gridViewTestResults.SelectRow(gridViewTestResults.FocusedRowHandle);
            }
            xtraUserControlItemsNavGridIssueItems.UpdateTopAndBottomItemsLists();

            UiHelperClass.HideSplash();
        }

        #endregion

        #region Imprinting

        /// <summary>
        /// Calls logic for imprinting action
        /// </summary>
        private void ImprintAction()
        {
            var testResultHandlers = gridViewTestResults.GetSelectedRows();

            var testResults = testResultHandlers.Select(h => gridViewTestResults.GetRow(h) as TestResult).ToList();

            ApplyImprintAction(testResults, ImprintingAction.Imprint);
        }

        /// <summary>
        /// Apply imprinting action to single test result
        /// </summary>
        /// <param name="testResult"></param>
        /// <param name="action"></param>
        /// <param name="showWaitingPanel"></param>
        /// <param name="refresh"></param>
        private void ApplyImprintActionSingleResult(TestResult testResult, ImprintingAction action, bool showWaitingPanel = false, bool refresh = false)
        {
            IssueTestLogicHelper.ApplyImprintActionSingleResult(CurrentTest, testResult, action, showWaitingPanel);
            if (refresh)
            {
                gridViewTestResults.RefreshData();
            }
        }

        /// <summary>
        /// Applies an imprinting related action on one or multiple test results.
        /// </summary>
        /// <param name="testresults"></param>
        /// <param name="action"></param>
        private void ApplyImprintAction(IEnumerable<TestResult> testresults, ImprintingAction action)
        {
            IssueTestLogicHelper.ApplyImprintAction(CurrentTest,testresults, action);
            gridViewTestResults.RefreshData();
        }

        #endregion

        #endregion

        #endregion

        #region Handlers

        #region XtraUserControlIssue Handlers

        /// <summary>
        /// Notify the current tab that a property has changed in its business object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestIssue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(TestIssue_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowHideErrorIcons();
            }
        }

        /// <summary>
        /// Handles the actions that occurs when loading the user control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraUserControlIssue_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraUserControlIssue_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                RefreshResultsGrid();

                //WE CALL THIS LINE TO SET THE CONTAINERCONTROL FOR dxErrorProvider after the user control load
                //since the user control will be created at this time and error provider will be initialized
                //properly and validation will work, on there other hand to achieve the desired effect,
                //in the designer code the ContainerControl property was being set to null to make sure the
                //error provider isn't being initialized too early which causes validation errors not to show up.
                dxErrorProviderMain.ContainerControl = this;
            }
        }

        /// <summary>
        /// Edit the status of the buttons depend on the selected test result and the edit mode.
        /// </summary>
        /// <param name="forceToLock">The force to lock flag.</param>
        private void CheckButtonsForEnable(bool forceToLock)
        {
            var buttonEnabled = false;
            var buttonEnabledProductDosages = false;
            var buttonEnabledFourFactors = false;
            var buttonEnabledRatios = false;

            if (!forceToLock)
            {
                if(gridViewTestResults.IsDataRow(gridViewTestResults.FocusedRowHandle))
                {
                    var selectedTestResult = gridViewTestResults.GetFocusedRow() as TestResult;

                    if (selectedTestResult != null)
                    {
                        var isProduct = IsProduct(selectedTestResult);
                        buttonEnabled = _isEditable;
                        buttonEnabledProductDosages = buttonEnabled && isProduct;
                        buttonEnabledFourFactors = buttonEnabled && !isProduct;
                        //buttonEnabledRatios = selectedTestResult.Item.Properties.HasProperty(PropertiesEnum.HasRatios, _yesLookupId.ToString());
                    }
                }                
            }
            simpleButtonCauses.Enabled = _isEditable;
            simpleButtonGeneric.Enabled = _isEditable;
            simpleButtonProducts.Enabled = _isEditable;
            simpleButtonCustomeDilution.Enabled = _isEditable;

            simpleButtonImprint.Enabled = buttonEnabled;
            simpleButtonImprint.Enabled = buttonEnabled;
            simpleButtonSetVitalForce.Enabled = buttonEnabled;

            simpleButtonFourCauses.Enabled = _isEditable;
            simpleButtonRatios.Enabled = _isEditable;
            simpleButtonFourFactors.Enabled = buttonEnabledFourFactors;
            simpleButtonProductDosages.Enabled = buttonEnabledProductDosages;
        }

        /// <summary>
        /// Gets the test results to broadcast it to the device.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetTestResultsToBroadcast()
        {
            try
            {
                var testResultItems = CurrentTestIssue.TestResults.Where(tr => tr.Item != null && tr.IsSelected).Select(tr => tr.Item);
                return testResultItems.ToList();
            }
            catch
            {
                return new List<Item>();
            }
        }        

        /// <summary>
        /// handles the click on the context menu for a tree.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
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
                if (sender == null) return;

                ((ContextMenuStrip)sender).Hide();

                if (sender == contextMenuStripDeleteTestResult)
                {
                    var testResultHandlers = gridViewTestResults.GetSelectedRows();

                    var testResults = testResultHandlers.Select(h => gridViewTestResults.GetRow(h) as TestResult).ToList();

                    if (e.ClickedItem == toolStripMenuItemDelete)
                    {
                        DeleteTestResults(testResults);
                    }
                    else if (e.ClickedItem == toolStripMenuItemImprint)
                    {
                        ApplyImprintAction(testResults, ImprintingAction.Imprint);
                    }
                    else if (e.ClickedItem == toolStripMenuItemMarkNotImprintable)
                    {
                        ApplyImprintAction(testResults, ImprintingAction.MarkNotImprintable);
                    }
                    else if (e.ClickedItem == toolStripMenuItemRemoveFromImprintList)
                    {
                        ApplyImprintAction(testResults, ImprintingAction.RemoveFromImprintList);
                    }
                }
            }
        }

        /// <summary>
        /// Show the vital force dialog for the current node
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonSetVitalForce_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonSetVitalForce_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetVitalForce(true);
            }
        }

        /// <summary>
        /// Handle ratios button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonRatios_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonRatios_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetRatios();
            }
        }

        /// <summary>
        /// Shows the four factor dialog of the current node
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonFourFactors_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonFourFactors_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetFourFactor();
            }
        }

        /// <summary>
        /// Handles the click on products button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonProducts_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonProducts_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenProducts();
            }
        }

        /// <summary>
        /// Handles the click on custom dilutions button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonCustomeDilution_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonCustomeDilution_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenCustomDilution();
            }
        }

        /// <summary>
        /// Handles the click on the Bacteria button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonBacteria_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonBacteria_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenBacteria();
            }
        }

        /// <summary>
        /// Handles the click on the ProductDosages button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonProductDosages_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonProductDosages_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetProductDosages();
            }
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        public void Issue_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(Issue_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Alt && e.KeyCode == Keys.M)
                {
                    if (simpleButtonProducts.Enabled)
                    {
                        OpenProducts();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.D)
                {
                    if (simpleButtonCustomeDilution.Enabled)
                    {
                        OpenCustomDilution();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.V)
                {
                    if (simpleButtonSetVitalForce.Enabled)
                    {
                        SetVitalForce(true);
                    }
                }
                if (e.Alt && e.KeyCode == Keys.D4)
                {
                    if (simpleButtonFourCauses.Enabled)
                    {
                        OpenFourCauses();
                    }
                }
                else if (e.Shift && e.KeyCode == Keys.R)
                {
                    if (simpleButtonRatios.Enabled)
                    {
                        SetRatios();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.R)
                {
                    if (simpleButtonImprint.Enabled)
                    {
                        ImprintAction();
                    }
                }
                else if (e.KeyCode == Keys.F10)
                {
                    if (simpleButtonCauses.Enabled)
                    {
                        e.Handled = true;//This is to fix problem in pressing F10 where hot keys on grid stops reponding after
                        OpenCauses();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.F)
                {
                    if (simpleButtonFourFactors.Enabled)
                    {
                        SetFourFactor();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.G)
                {
                    if (simpleButtonProductDosages.Enabled)
                    {
                        SetProductDosages();
                    }
                }
                else if (e.Alt && e.KeyCode == Keys.L)
                {
                    if (simpleButtonGeneric.Enabled)
                    {
                        OpenGeneric();
                    }
                }
                else
                {
                    ControlItemsNavGrid.NavGrid_KeyDown(sender, e);
                }
            }
        }

        /// <summary>
        /// Handles opening the generic list items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonGeneric_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonGeneric_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenGeneric();
            }            
        }

        /// <summary>
        /// Imprints current selected test results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonImprint_Click(object sender, EventArgs e)
        {
            ImprintAction();
        }

        /// <summary>
        /// Handle request for balancing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonProductsTestingBalance_Click(object sender, EventArgs e)
        {
            InvokeBalancingRequest();
        }

        /// <summary>
        /// Handle FourCauses button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonFourCauses_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonProducts_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenFourCauses();
            }
        }

        #endregion

        #region xtraUserControlItemsNavGrid Handlers

        /// <summary>
        /// Handel the CancelReadingRequest event of the xtraUserControlItemsNavGrid and forward it.
        /// </summary>
        private void xtraUserControlItemsNavGridIssueItems_CancelReadingRequest(XtraUserControlItemsNavGrid sender)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    Invoke(
                        new XtraUserControlItemsNavGrid.OnCancelReadingRequest(
                            xtraUserControlItemsNavGridIssueItems_CancelReadingRequest), sender);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                InvokeCancelReadingRequest(this);
            }
        }

        /// <summary>
        /// Handel the ReadingRequest event of the xtraUserControlItemsNavGrid and forward it.
        /// </summary>
        private void xtraUserControlItemsNavGrid1_ReadingRequest(XtraUserControlItemsNavGrid sender, List<Item> itemsToBroadcast)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlItemsNavGrid.OnReadingRequest(xtraUserControlItemsNavGrid1_ReadingRequest), sender, itemsToBroadcast);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (CsaEmdUnitManager.Instance.IsBroadcastingOn)
                {
                    itemsToBroadcast.AddRange(GetTestResultsToBroadcast());
                }

                InvokeReadingRequest(this, itemsToBroadcast);
            }
        }

        /// <summary>
        /// Handel the SelectedItemChanged event of the xtraUserControlItemsNavGrid and forward it.
        /// </summary>
        private void xtraUserControlItemsNavGridIssueItems_SelectedItemChanged(XtraUserControlItemsNavGrid sender, Item item, int reading)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new XtraUserControlItemsNavGrid.OnSelectedItemChanged(xtraUserControlItemsNavGridIssueItems_SelectedItemChanged), sender, item, reading);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                InvokeSelectedItemChanged(this, false, item, reading);
            }
        }

        /// <summary>
        /// Handel the Load event of the xtraUserControlItemsNavGrid and Clear the selection.
        /// </summary>
        private void xtraUserControlItemsNavGridIssueItems_Load(object sender, EventArgs e)
        {
            //xtraUserControlItemsNavGridIssueItems.ClearSelection();
        }

        /// <summary>
        /// Handel the adding a test result.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="items">The items.</param>
        List<Item> xtraUserControlItemsNavGridIssueItems_AddToTestResults(XtraUserControlItemsNavGrid sender, BindingList<Item> items)
        {
            AddToTestResults(items);
            return null;
        }

        /// <summary>
        /// Handle the request to refresh image and detail
        /// </summary>
        /// <param name="sender"></param>
        private void xtraUserControlItemsNavGridIssueItems_RefreshDetailsAndImageNavGrid(XtraUserControlItemsNavGrid sender)
        {
            if (RefreshDetailsAndImageIssue == null)
                return;

            RefreshDetailsAndImageIssue(sender);
        }

        /// <summary>
        /// Handle the request to set the image control image ignore state
        /// </summary>
        /// <param name="ignoreState"></param>
        private void xtraUserControlItemsNavGridIssueItems_SetImageIgnoreState(bool ignoreState)
        {
            if (SetImageIgnoreState == null)
                return;

            SetImageIgnoreState(ignoreState);
        }

        /// <summary>
        /// Handles next protocol button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void gridLookUpEditNextProtocolStep_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ButtonPressedEventHandler(gridLookUpEditNextProtocolStep_Properties_ButtonClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Button.Kind != ButtonPredefines.Glyph || ((int)gridLookUpEditNextProtocolStep.EditValue <= 0))
                    return;

                UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

                ControlItemsNavGrid.UpdateCurrentTestResultChildIndex();

                ControlItemsNavGrid.CheckOneByOneModeForDisable();

                int protocolId;

                int.TryParse(gridLookUpEditNextProtocolStep.EditValue.ToString(), out protocolId);

                var protocol = _testProtocolsManager.GetTestProtocolById(new SingleItemFilter() { ItemId = protocolId });

                var items = xtraUserControlItemsNavGridIssueItems.GetProtocolItems(protocol);

                var parent = xtraUserControlItemsNavGridIssueItems.GetCurrentTestResult();

                //Any test result should has a value for the id to let the 'mark as test result' function work fine.
                var testStep = new TestResult()
                {
                    IsCurrent = true,
                    IsSelected = false,
                    Item = null,
                    Parent = parent,
                    DateTime = DateTime.Now,
                    StepType = null,
                    TestIssue = this.CurrentTestIssue,
                    TestProtocol = protocol,
                    Id = NextResultId, //The Id property should be filled with a value to work properly with the tree.
                    TempImprintingId = CurrentTest.GetNextTestResultId()
                };

                if (parent != null)
                    parent.IsCurrent = false;

                xtraUserControlItemsNavGridIssueItems.TestResults.Add(testStep);

                xtraUserControlItemsNavGridIssueItems.BackClicks += 1;

                xtraUserControlItemsNavGridIssueItems.InitGridItems(items, true, false, false);

                //adding the visited item of type protocol.
                xtraUserControlItemsNavGridIssueItems.VisitedItems.Add(new VisitedItem() { ItemId = protocol.Id, Type = StaticKeys.VisitedItemTypeProtocol });

                xtraUserControlItemsNavGridIssueItems.SetCurrentNodeLabel(protocol.Name);

                xtraUserControlItemsNavGridIssueItems.IsFirstSwitch = false;
                xtraUserControlItemsNavGridIssueItems.CurrentNodeId = 0;
                xtraUserControlItemsNavGridIssueItems.PartHilight = 1;

                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Handles the click on the causes button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonCauses_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonCauses_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                OpenCauses();
            }
        }

        /// <summary>
        /// Handles the hiding of the expand button when not needed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewTestResults_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowCellCustomDrawEventHandler(gridViewTestResults_CustomDrawCell), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.HandleViewDetailExpandButton(sender as GridView, e);

                if (gridViewTestResults.IsDataRow(e.RowHandle) && e.Column == gridColumnResultsItem)
                {
                    var currentResult = gridViewTestResults.GetRow(e.RowHandle) as TestResult;

                    if (currentResult != null && currentResult.IsImprinted)
                    {
                        e.Graphics.DrawImage(Resources.Imprint16x16, e.Bounds.Right - Resources.Imprint16x16.Width - 3, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the opening event for the context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuStripDeleteTestResult_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripDeleteTestResult_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                e.Cancel = gridViewTestResults.CalcHitInfo(gridViewTestResults.GridControl.PointToClient(Cursor.Position)).InGroupPanel ||
                                   gridViewTestResults.CalcHitInfo(gridViewTestResults.GridControl.PointToClient(Cursor.Position)).InFilterPanel ||
                                   gridViewTestResults.CalcHitInfo(gridViewTestResults.GridControl.PointToClient(Cursor.Position)).InColumnPanel ||
                                   gridViewTestResults.CalcHitInfo(gridViewTestResults.GridControl.PointToClient(Cursor.Position)).InGroupColumn;
                var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewTestResults);

                if (_isEditable)
                {
                    var enabled = isEnabled && ((!gridViewTestResults.IsRowSelected(0) && !IsMainIssue) || IsMainIssue) ;
                    toolStripMenuItemDelete.Enabled = enabled;
                    toolStripMenuItemImprint.Enabled = isEnabled;
                    toolStripMenuItemMarkNotImprintable.Enabled = isEnabled;
                    toolStripMenuItemRemoveFromImprintList.Enabled = isEnabled;
                }
                else
                {
                    toolStripMenuItemDelete.Enabled = false;
                    toolStripMenuItemImprint.Enabled = false;
                    toolStripMenuItemMarkNotImprintable.Enabled = false;
                    toolStripMenuItemRemoveFromImprintList.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the focused row changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewTestResultFactors_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewTestResultFactors_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ProcessSelectedTestResultChanged();
            }
        }

        /// <summary>
        /// Handles the selected rows changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewTestResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SelectionChangedEventHandler(gridViewTestResult_SelectionChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ProcessSelectedTestResultChanged();
            }
        }

        /// <summary>
        /// Handle the focus on the test results grid.
        /// </summary>
        private void gridViewTestResultFactors_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewTestResultFactors_GotFocus), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ProcessSelectedTestResultChanged();
            }
        }

        #endregion

        #endregion

        #region Event Handler Delegates

        /// <summary>
        /// Delegate for ReadingOrder handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="itemsToBroadcast">The items to be broadcasted..</param>
        public delegate void OnReadingRequest(XtraUserControlIssue sender, List<Item> itemsToBroadcast);

        /// <summary>
        /// Delegate for CancelReadingOrder handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void OnCancelReadingRequest(XtraUserControlIssue sender);

        /// <summary>
        /// Delegate for the removal of a major issue.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void OnRemoveMajorIssue(XtraUserControlIssue sender, Item item);

        /// <summary>
        /// Delegate for SelectedItemChanged handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="item">The item.</param>
        /// <param name="reading">The reading.</param>
        public delegate void OnSelectedItemChanged(XtraUserControlIssue sender, bool forTestResults, Item item, int reading);

        /// <summary>
        /// Delegate for OnBroadcastRequest handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="items">The items.</param>
        public delegate void OnBroadcastRequest(XtraUserControlIssue sender, List<Item> items);

        /// <summary>
        /// Delegate for OnActivateConnectionRequest handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void OnActivateConnectionRequest(XtraUserControlIssue sender);

        /// <summary>
        /// Delegate for OnUpdateOnTestResults handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void OnUpdateOnTestResults(XtraUserControlIssue sender);

        /// <summary>
        /// Delegate for OnRefreshDetailsAndImageIssue handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnRefreshDetailsAndImageIssue(XtraUserControlItemsNavGrid sender);

        /// <summary>
        /// Delegate for the OnSetImageIgnoreState handler method
        /// </summary>
        /// <param name="ignoreState"></param>
        public delegate void OnSetImageIgnoreState(bool ignoreState);

        /// <summary>
        /// Delegate for OnMeterDialogOpen handler method.
        /// </summary>
        /// <param name="hidePanel">The sender.</param>
        public delegate void OnMeterDialogOpen(bool showOverlay);

        /// <summary>
        /// Delegate for OnBalancingRequest handler method
        /// </summary>
        public delegate void OnBalancingRequest();

        #endregion
    }
}
