using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Settings;
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

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlItemsNavGrid : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        #region Managers

        private readonly ItemsManager _itemsManager;
        private readonly LookupsManager _lookupsManager;
        private readonly TestProtocolsManager _testProtocolsManager;
        private readonly SettingsManager _settingsManager;

        #endregion

        #region Navigation variables.

        private int _part = 1;
        private int _partHilight = 1;
        private int _backClicks;
        private bool _isWaitingToTakeAction;
        private bool _isOneByOneTest;
        private bool _oneByOneActivedByUser;
        private bool _currentHasChilds;
        private bool _isFirstFetch = true;
        private bool _isSwitched;
        private bool _isSplitted;
        private bool _showHiddenItems;
        private bool _dontDrillDownInFirstInsert;

        private bool _isMainIssue;

        // Needed variables for the highlight/switching feature.
        private bool _isFirstSwitch = true;
        private bool _isTopListFirst = true;
        private readonly BindingList<Item> _topItems;
        private readonly BindingList<Item> _bottomItems;
        private bool _imageRefreshNeeded;
        private bool _isSwitching;
        private bool _searchCriteriaActive;
        private string _lastSearchCriteria;
        private bool _itemSearchEnabled;

        /// <summary>
        /// This member is to avoid the the problem of default focus item, when drill down in the first item.
        /// </summary>
        private int _lastDoubleClickedRowHandler;

        #endregion

        #region CRUD & Readings Variable

        private bool _isEditable = true;
        private bool _isCurrentReadingOn;
        private int _lastReadingValue;
        private int _nextRowToSelectBaseOnUserSelection;
        private int _nextRowToSelectBaseOnUserSelectionTemp;
        private int _yesLookupId;
        private int _lastItemRemovedCount;

        #endregion
        
        #endregion

        #region Events

        /// <summary>
        /// Handle the reading request of the control.
        /// </summary>
        public event OnReadingRequest ReadingRequest;

        /// <summary>
        /// Handle the canceling for reading request from the control.
        /// </summary>
        public event OnCancelReadingRequest CancelReadingRequest;

        /// <summary>
        /// Handle the selected Item changed.
        /// </summary>
        public event OnSelectedItemChanged SelectedItemChanged;

        /// <summary>
        /// Handle the add to test result.
        /// </summary>
        public event OnAddToTestResults AddToTestResults;

        /// <summary>
        /// Handle the request to refresh image and details
        /// </summary>
        public event OnRefreshDetailsAndImageNavGrid RefreshDetailsAndImageNavGrid;

        /// <summary>
        /// Handle the request to update the image control ignore state
        /// </summary>
        public event OnSetImageIgnoreState SetImageIgnoreState;

        #endregion

        #region Events Invokers

        /// <summary>
        /// Invoke Add To Test Results event.
        /// </summary>
        public List<Item> InvokeAddToTestResults(XtraUserControlItemsNavGrid sender, BindingList<Item> items)
        {
            if (AddToTestResults == null)
                return new List<Item>();

            _nextRowToSelectBaseOnUserSelectionTemp = _nextRowToSelectBaseOnUserSelection;

            _lastItemRemovedCount = _showHiddenItems
                                        ? 0
                                        : gridViewItems.GetSelectedRows().Where(
                                            sr => sr < _nextRowToSelectBaseOnUserSelection).Count();

            return AddToTestResults(sender, items);
        }

        /// <summary>
        /// Invoke ReadingRequest event.
        /// </summary>
        private void InvokeReadingRequest(XtraUserControlItemsNavGrid sender, List<Item> itemsToBroadcast)
        {
            if(ReadingRequest == null)
                return;

            ReadingRequest(sender, itemsToBroadcast);
        }

        /// <summary>
        /// Invoke CancelReadingRequest event.
        /// </summary>
        private void InvokeCancelReadingRequest(XtraUserControlItemsNavGrid sender)
        {
            if(CancelReadingRequest == null)
                return;

            CancelReadingRequest(sender);
        }

        /// <summary>
        /// Invoke SelectedItemChanged event.
        /// </summary>
        private void InvokeSelectedItemChanged(XtraUserControlItemsNavGrid sender, Item item, int reading)
        {
            if (SelectedItemChanged == null)
                return;

            SelectedItemChanged(sender, item, reading);
        }

        /// <summary>
        /// Invoke RefreshDetailsAndImage event.
        /// </summary>
        private void InvokeRefreshDetailsAndImageNavGrid(XtraUserControlItemsNavGrid sender)
        {
            if (RefreshDetailsAndImageNavGrid == null)
                return;

            RefreshDetailsAndImageNavGrid(sender);
        }

        /// <summary>
        /// Invoke SetImageIgnoreState event
        /// </summary>
        /// <param name="imageIgnoreState"></param>
        private void InvokeSetImageIgnoreState(bool imageIgnoreState)
        {
            if (SetImageIgnoreState ==null)
                return;

            SetImageIgnoreState(imageIgnoreState);
        }
        #endregion

        #region Event Handler Delegates

        /// <summary>
        /// Delegate for ReadingOrder handler method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="itemsToBroadcast"></param>
        public delegate void OnReadingRequest(XtraUserControlItemsNavGrid sender, List<Item> itemsToBroadcast);

        /// <summary>
        /// Delegate for CancelReadingOrder handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnCancelReadingRequest(XtraUserControlItemsNavGrid sender);

        /// <summary>
        /// Delegate for SelectedItemChanged handler method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        /// <param name="reading"></param>
        public delegate void OnSelectedItemChanged(XtraUserControlItemsNavGrid sender, Item item, int reading);

        /// <summary>
        /// Delegate for AddToTestResults handler method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="items">Items To add.</param>
        public delegate List<Item> OnAddToTestResults(XtraUserControlItemsNavGrid sender, BindingList<Item> items);

        /// <summary>
        /// Delegate for OnRefreshDetailsAndImageNavGrid handler method.
        /// </summary>
        /// <param name="sender"></param>
        public delegate void OnRefreshDetailsAndImageNavGrid(XtraUserControlItemsNavGrid sender);

        /// <summary>
        /// Delegate for the OnSetImageIgnoreState handler method
        /// </summary>
        /// <param name="ignoreState"></param>
        public delegate void OnSetImageIgnoreState(bool ignoreState);
        #endregion        

        #region Properties

        #region Hilighting / Switching properties

        /// <summary>
        /// Gets or sets the Highlighted part number.
        /// </summary>
        public int PartHilight
        {
            get { return _partHilight; }
            set { _partHilight = value; }
        }

        /// <summary>
        /// Gets the Top Items.
        /// </summary>
        public BindingList<Item> TopItems
        {
            get { return _topItems; }
        }

        /// <summary>
        /// Gets the Bottom Items.
        /// </summary>
        public BindingList<Item> BottomItems
        {
            get { return _bottomItems; }
        }

        /// <summary>
        /// Gets or sets the IsFirstSwitch flag.
        /// </summary>
        public bool IsFirstSwitch
        {
            get
            {
                return _isFirstSwitch;
            } 
            set
            {
                _isFirstSwitch = value;   
            }
        }

        /// <summary>
        /// Gets or sets the IsTopListFirst flag.
        /// </summary>
        public bool IsTopListFirst
        {
            get
            {
                return _isTopListFirst;
            }
            set
            {
                _isTopListFirst = value;
            }
        }

        /// <summary>
        /// Gets or sets the current node id.
        /// </summary>
        public int CurrentNodeId 
        { 
            get; 
            set; 
        }

        #endregion

        /// <summary>
        /// Gets or sets the visited items.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<VisitedItem> VisitedItems { get; set; }

        /// <summary>
        /// The dataSource for the items that will be initially shown in the grid
        /// </summary>
        public BindingList<Item> StartingItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TestProtocol TestProtocol { get; set; }

        /// <summary>
        /// Gets the current items of the grid.
        /// </summary>
        public BindingList<Item> GridItems
        {
            get
            {
                return (BindingList<Item>)gridControlVitalForce.DataSource;
            }
        }

        /// <summary>
        /// This list is currently used to get items that are not hidden
        /// </summary>
        public BindingList<Item> GridItemsFiltered
        {
            get
            {
                return GridItems.Where(item => !item.Hidden).ToBindingList();
            }
        }

            /// <summary>
        /// Gets or sets the test results list.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<TestResult> TestResults
        {
            set;
            get;
        }

        /// <summary>
        /// Gets item from current test result
        /// </summary>
        public Item CurrentTestResultItem
        {
            get
            {
                return GetCurrentTestResult() == null ? null : GetCurrentTestResult().Item;
            }
        }

        /// <summary>
        /// Gets the Issue item.
        /// </summary>
        public Item CurrentIssueItem
        {
            get
            {
                return CurrentIssue == null ? null : CurrentIssue.Item;
            }
        }

        /// <summary>
        /// Gets or Sets the Issue.
        /// </summary>
        public TestIssue CurrentIssue
        {
            get; set;
        }

        /// <summary>
        /// Gets the Is Waiting Reading Response, to check if there is some items in waiting for reading response. 
        /// </summary>
        public bool IsWaitingReadingResponse
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets or sets the Is First Fetch.
        /// </summary>
        public bool IsFirstFetch
        {
            get
            {
                return _isFirstFetch;
            }
            set
            {
                _isFirstFetch = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the Back Clicks.
        /// </summary>
        public int BackClicks
        {
            get
            {
                return _backClicks;
            }
            set
            {
                _backClicks = value;
            }
        }

        /// <summary>
        /// This Property is use to handle the out side response on the reading request. 
        /// </summary>
        public int ResponseReadingValue
        {
            set
            {
                ProcessResponsedReading(value);
            }
            
        }

        /// <summary>
       /// Gets the selected rows count.
       /// </summary>
        public int SelectedRowCount
        {
            get
            {
                return gridViewItems == null ? 0 : gridViewItems.SelectedRowsCount;
            }
        }

        public bool IsStepAutomated
        {
            get
            {
                return  gridViewItems.DataSource != null && ((BindingList<Item>) gridViewItems.DataSource).Any(c => c.Step > 0);
            }    
        }

        /// <summary>
        /// Set the Visibility of ShowAddToTestResultButton;
        /// </summary>
        public LayoutVisibility ShowAddToTestResultButton
        {
            get { return layoutControlItemAddAsTestResult.Visibility; }
            set { layoutControlItemAddAsTestResult.Visibility = value; }
        }

        /// <summary>
        /// Gets the Auto navigation mode.
        /// </summary>
        public bool IsAutoNavigationOn
        {
            get; 
            private set;
        }

        /// <summary>
        /// Prevent the drilling down while auto testing. 
        /// </summary>
        public bool DontDrillDownInFirstInsert
        {
            get { return _dontDrillDownInFirstInsert; }
            set
            {
                _dontDrillDownInFirstInsert = value;
                layoutControlItemHideShowItems.Visibility = _dontDrillDownInFirstInsert ? LayoutVisibility.Never : LayoutVisibility.Always;   
            }
        }

        /// <summary>
        /// Gets or sets the insertedItems.
        /// </summary>
        public List<Item> InsertedItems
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current step.
        /// </summary>
        public int CurrentStep
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the force step increment.
        /// </summary>
        public bool ForceStepIncrement
        {
            get; set;
        }

        public bool IsOneByOneTest
        {
            set { _isOneByOneTest = value; }
            get { return _isOneByOneTest; }
        }

        public  int NextRowToSelectBaseOnUserSelection
        {
            set { _nextRowToSelectBaseOnUserSelection = value; }
            get { return _nextRowToSelectBaseOnUserSelection; }
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
        /// A flag used to prevent duplicate refresh of images
        /// </summary>
        public bool ImageRefreshNeeded
        {
            get
            {
                return _imageRefreshNeeded;
            }
            set
            {
                _imageRefreshNeeded = value;
            }
        }

        public bool IsSwitching
        {
            get
            {
                return _isSwitching;
            }
            set
            {
                _isSwitching = value;
            }
        }

        /// <summary>
        /// Indicates if item search is enabled or disabled
        /// </summary>
        public bool ItemSearchEnabled
        {
            get
            {
                return _itemSearchEnabled;
            }
            set
            {
                _itemSearchEnabled = value;
                SetSearchControlsVisibility();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// The Constructor.
        /// </summary>
        public XtraUserControlItemsNavGrid()
        {
            InitializeComponent();           

            var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (designMode) return;

            _itemsManager = new ItemsManager();
            _testProtocolsManager = new TestProtocolsManager();
            _lookupsManager = new LookupsManager();
            _settingsManager = new SettingsManager();

            TestResults = new BindingList<TestResult>();
            VisitedItems = new List<VisitedItem>();

            _topItems = new BindingList<Item>();
            _bottomItems = new BindingList<Item>();

            IsAutoNavigationOn = true;
            IsFirstSwitch = true;
            IsTopListFirst = true;

            CurrentStep = 1;

            FillLocalLookupIds();

            SetupFont();

            //Get the value of the keep one by one option from settings
            checkEditKeepOneByOne.Checked = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.KeepOneByOneModeOn);
        }

        #endregion        

        #region General Methods

        /// <summary>
        /// Performs the needed actions of the edit mode.
        /// </summary>
        /// <param name="isReadOnly">The read only flag.</param>        
        public void SetEditMode(bool isReadOnly)
        {
            var filterText = gridViewItems.GetFilterDisplayText(gridViewItems.ActiveFilter);
            var viewNotFiltered = filterText.Equals(StaticKeys.HiddenItemsFilterText) || string.IsNullOrEmpty(filterText);

            _isEditable = !isReadOnly;

            simpleButtonViewDetails.Enabled = _isEditable && gridViewItems.SelectedRowsCount == 1 && _currentHasChilds;

            simpleButtonSplit.Enabled = GridItems != null && gridViewItems.RowCount > 1 && (_isEditable && !_isOneByOneTest && viewNotFiltered);

            simpleButtonSwitch.Enabled = GridItems != null && gridViewItems.RowCount > 1 && (_isEditable && !_isOneByOneTest && viewNotFiltered);
            
            simpleButtonClearUserSelection.Enabled = _isEditable && !_isOneByOneTest && 
                                                    gridViewItems.SelectedRowsCount > 0 && 
                                                    gridViewItems.RowCount != 1;

            simpleButtonAddToTestResults.Enabled = _isEditable && gridViewItems.SelectedRowsCount > 0;

            checkButtonOneByOne.Enabled = _isEditable && viewNotFiltered && gridViewItems.RowCount > 0;
            checkEditKeepOneByOne.Enabled = _isEditable;

            checkButtonHideShowItems.Enabled = _isEditable && CurrentIssue != null && !DontDrillDownInFirstInsert;

            var currentTestResult = GetCurrentTestResult();
           
            /*
             case : if the user control in the issues case (not items case), and there is only one test result, 
             then this step needs to stay since it represents the issue item (the initial step).
             */
            simpleButtonBack.Enabled = (currentTestResult != null) && (currentTestResult.Parent != null || CurrentIssue == null || (_backClicks != 0 && _backClicks >= 0)) && (CurrentIssue != null || _backClicks != 0) && _isEditable;

            simpleButtonReloadItems.Enabled = _isEditable;

            SetFindButtonEnableState(textEditFind.Text);
        }

        private void SetEmptyItemMode()
        {
            
        }

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            /*This code will cause an issue in the designer mode of the general test form. we need to check if the form is loaded
            or not before executing this */
            var yesLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes)).FirstOrDefault();

            _yesLookupId = yesLookup != null ? yesLookup.Id : 0;
        }

        /// <summary>
        /// Setup the font for the grid.
        /// </summary>
        private void SetupFont()
        {
            var fontSize = _settingsManager.GetSetting(new SettingsFilter() { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });
            gridViewItems.Appearance.Row.Font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));
        }

        /// <summary>
        /// Sets the reading mode.
        /// </summary>
        /// <param name="isReadingOn">is in read only mode.</param>
        public void SetReadingMode(bool isReadingOn)
        {
            _isCurrentReadingOn = isReadingOn;

            SetEditMode(!_isEditable);
        }               

        #endregion

        #region Logic

        #region Items Logic
        
        /// <summary>
        /// Toggle (Hide/Show) inserted - Test results - items.
        /// </summary>
        private void ToggleHideShowItems()
        {
            _showHiddenItems = !_showHiddenItems;

            checkButtonHideShowItems.Image = _showHiddenItems ? Resources.eyeShow : Resources.eyeHide;

            RefreshItems(true, IsStepAutomated);
        }

        private void ResetItemGridActiveFilter()
        {
            if(_showHiddenItems)
            {
                gridViewItems.ActiveFilter.Clear();
            }
            else
            {
                gridViewItems.ActiveFilterString = StaticKeys.HiddenItemsFilterText;
            }
        }
        
        /// <summary>
        /// Sets the item of the grid
        /// </summary>
        public void SetItems(BindingList<Item> items, bool selectAll, bool keepUserSelection, bool keepCurrentStep, bool updateTopAndBottom = true,bool isBackToParentAction = false)
        {
            //Reset here to avoid user filtering.
            ResetItemGridActiveFilter();

            gridViewItems.FocusedRowHandle = 0;

            gridViewItems.SelectionChanged -= gridViewItemsNav_SelectionChanged;
            gridViewItems.FocusedRowChanged -= gridViewItemsNav_FocusedRowChanged;

            if (!keepUserSelection && !keepCurrentStep)
                ClearNextUserSelectionMembers();

            var currentTestResult = GetCurrentTestResult();

            ShowCurrentListNotes(currentTestResult);

            /*This code is responsible for filtering the items grid.*/
            /*skip the root item in the tree.The condition [c.Parents != null] is 
            there to skip the temp node that is normally added for the Four Factors.*/

            var testResultItems = TestResults.Where(p => p.IsSelected &&
                                                         p.Item != null &&
                                                         p.Item.Properties.HasProperty(PropertiesEnum.DontFilter,
                                                             _yesLookupId.ToString()) == false).Select(c => c.Item);

            if (!IsMainIssue)
            {
                testResultItems = testResultItems.Skip(1);
            }
            
            //get the items that only has no child items.
            //testResultItems = testResultItems.Where(c => !c.HasChildren);
            //No need for this code, we need to filter item form the nav grid whether it has children or not 
            
            //get the filtered list by eliminating the test result items from the current grid source.
            //BindingList<Item> filteredList;

            //Reset the hidden property for all items in the list before applying the new hidden filter on them based on the current
            //test results, to make sure hidden items from before are visible now if the need to be visible.
            items.DoAction(i => i.Hidden = false);
          
            if (currentTestResult == null || currentTestResult.Item == null || currentTestResult.Item.TypeLookup == null || currentTestResult.Item.TypeLookup.Value != StaticKeys.Potency)
            {
                foreach (var item in testResultItems.Select(testResult => items.Where(i => i.Id == testResult.Id)).SelectMany(x => x))
                {
                    item.Hidden = true;
                }               
            }

            gridControlVitalForce.DataSource = items;
            gridViewItems.RefreshData();

            ResetItemGridActiveFilter();

            //check if its not the items grid and if this list has steps to select.
            if (CurrentIssue != null && items.Any(c => c.Step > 0))
            {
                updateTopAndBottom = false;
                /*some cases other than the normal cases need to increment the current step, 
                that's why the ForceStepIncrement flag is used here */
                if (ForceStepIncrement)
                {
                    CurrentStep += 1;
                    ForceStepIncrement = false;
                }
                //incrementing the step if the current step between the min-max step values.
                else if ((CurrentStep > 1 && CurrentStep < GetMaxStepNumber()) && !keepCurrentStep)
                {
                    CurrentStep += 1;
                }
                //setting the step back to the first step.
                else if (!ForceStepIncrement && !keepCurrentStep)
                {
                    CurrentStep = 1;
                }

                //selecting the steps.
                HilightSteps(items);
                
                if(!_isSwitched && !_isSplitted)
                //enabling the one by one (custom steps) navigation.
                checkButtonOneByOne.Checked = true;

                /*[Oday] This call is a workaround solution to solve an issue which's is like the following : Go to Top 10 -> Miasms -> Add one of the Products
                 *-> Enable one by one mode -> Add one of the bacteria's inside the Bacteria nosodes -> it will go automatically to custom dilutions
                 *-> it will select one of the items and highlight the list in a strange way even though this list has a custom steps.*/
                SetOneByOneTestMode(_isOneByOneTest);

            }
            else if (_isOneByOneTest)
            {
                //Select the first item in one by one mode when one by one mode is active
                //or when it is not active and the action is back to parent action
                if (KeepOneByOneMode() || isBackToParentAction && !KeepOneByOneMode())
                {
                    updateTopAndBottom = false;
                    SelectFirstItem();
                }
            }
            else if (selectAll && items.Count > 0)
            {
                //Set the one by one toggle stated based on the setting and user explicit click state
                checkButtonOneByOne.Checked = KeepOneByOneMode();
                SelectAllIfApplicable(currentTestResult);
                gridViewItems.RefreshData();
                gridViewItems.MakeRowVisible(0); 
                gridViewItems.FocusedRowHandle = 0;
            }

            //To add the issue itself (the root item in the tree) as a visited item.
            if (TestResults.Count == 1)
            {
                if (currentTestResult != null && currentTestResult.Item != null)
                    VisitedItems.Add(new VisitedItem() { ItemId = currentTestResult.Item.Id, Type = StaticKeys.VisitedItemTypeItem });
            }

            RestartReadingRequest();
            gridViewItems.SelectionChanged += gridViewItemsNav_SelectionChanged;
            gridViewItems.FocusedRowChanged += gridViewItemsNav_FocusedRowChanged;

            _isSwitched = false;
            _isSplitted = false;

            if (updateTopAndBottom)
            {
                UpdateTopAndBottomItemsLists();
            }
            
            InvokeRefreshDetailsAndImageNavGrid(this);
        }

        /// <summary>
        /// Shows if there are notes related to this list.
        /// </summary>
        /// <param name="currentTestResult">The current test result.</param>
        private void ShowCurrentListNotes(TestResult currentTestResult)
        {
            if(currentTestResult != null && currentTestResult.Item != null)
            {
                var note = VitalHelper.CheckForNotes(currentTestResult.Item.Name);

                simpleLabelNote.Text = note;
                simpleLabelNote.Visibility = !string.IsNullOrEmpty(note) ? LayoutVisibility.Always : LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// Selects all the items in the navigation grid when applicable.
        /// </summary>
        /// <param name="currentTestResult">The current test result.</param>
        private void SelectAllIfApplicable(TestResult currentTestResult)
        {
            if (currentTestResult == null) return;

            if (currentTestResult.Item != null && CheckIfVisited(new VisitedItem() { ItemId = currentTestResult.Item.Id , Type = StaticKeys.VisitedItemTypeItem } ))
                gridViewItems.SelectAll();

            else if (currentTestResult.TestProtocol != null && CheckIfVisited(new VisitedItem() { ItemId = currentTestResult.TestProtocol.Id, Type = StaticKeys.VisitedItemTypeProtocol }))
                gridViewItems.SelectAll();
                
            else if(currentTestResult.StepType != null && CheckIfVisited(new VisitedItem() { ItemId = currentTestResult.StepType.Id , Type = StaticKeys.VisitedItemTypeLookup } ))
                gridViewItems.SelectAll();
        }

        /// <summary>
        /// Update grid list items after passed item removed from the test results tree.
        /// </summary>
        /// <returns></returns>
        public void UpdateAfterDeletion(Item item)
        {
            var items = (BindingList<Item>)gridViewItems.DataSource;

            var itemTestResults = items.Where(i => i.Id == item.Id);

            var needToSelectAll = gridViewItems.SelectedRowsCount == gridViewItems.RowCount;

            var currentSelectedItem = gridViewItems.GetRow(gridViewItems.FocusedRowHandle) as Item;

            foreach (var itemTestResult in itemTestResults)
            {
                itemTestResult.Hidden = false;
                gridViewItems.RefreshData();

                if (currentSelectedItem == null)
                    return;

                if (itemTestResult.Step != 0 && itemTestResult.Step == CurrentStep && currentSelectedItem.Step == CurrentStep)
                {
                    gridViewItems.SelectRow(gridViewItems.GetRowHandle(items.IndexOf(itemTestResult)));
                }

                gridViewItems.FocusedRowHandle = gridViewItems.GetRowHandle(items.IndexOf(currentSelectedItem));

            }

            if (needToSelectAll)
            {
                gridViewItems.SelectAll();
            }    
        }

        /// <summary>
        /// Removes selected items from the grid
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="selectedItems">The selected items.</param>
        public void RemoveItems(BindingList<Item> items, BindingList<Item> selectedItems)
        {
            items = new BindingList<Item>(items.Where(x => selectedItems.ToList().Count(y => y.Id == x.Id) == 0).ToList());

            gridControlVitalForce.DataSource = items;
        }

        /// <summary>
        /// Sets the source of the grid.
        /// </summary>
        public void InitGridItems(BindingList<Item> items, bool selectAll, bool keepUserSelection, bool keepCurrentStep, bool updateTopAndBottom = true)
        {
            //The first fetch of data will set the node label as root and saves the starting items.
            if (_isFirstFetch)
            {
                StartingItems = items;
                _isFirstFetch = false;
            }

            var currentNodeLabel = CurrentIssue == null ? TestProtocol.Name : CurrentIssue.Item == null ?
                                                                                  CurrentIssue.Name : string.IsNullOrEmpty(CurrentIssue.Item.Key) ?
                                                                                                      CurrentIssue.Name : CurrentIssue.Item.Key == ItemKeys.TestMainIssue.ToString() ?
                                                                                                                          StaticKeys.Products : CurrentIssue.Name;

            SetCurrentNodeLabel(currentNodeLabel);

            SetItems(items, selectAll, keepUserSelection, keepCurrentStep, updateTopAndBottom);

            SetEditMode(!_isEditable);

            gridViewItems.RefreshData();

            //This call to force the system to throw the event of selected row changed.
            ProcessSelectedRowsChanged();
        }

        /// <summary>
        /// Gets the items for broadcasting.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItemsToBroadcast()
        {
            var currentGridItems = gridControlVitalForce.DataSource as BindingList<Item>;

            if (currentGridItems == null) 
                return new List<Item>();

            var selectedItem = GetSelectedItems();

            if (selectedItem == null) 
                return new List<Item>();

            if (selectedItem.Count <= 0)
            {
                var items = GetSystemSelectionItems().ToList();
                return items;
            }
            else
            {
                var items = selectedItem.ToList();
                return items;
            }
        }

        /// <summary>
        /// Handles the changes int the data source
        /// </summary>
        private void HandleDatasourceChange()
        {
            if (gridViewItems.RowCount > 1)
            {
                gridViewItems.FocusedRowHandle = _lastDoubleClickedRowHandler == 1 ? 0 : 1;
                ClearSelection();
            }
            else
            {
                gridViewItems.SelectRow(0);
            }
        }

        /// <summary>
        /// Process the selected rows changed.
        /// </summary>
        private void ProcessSelectedRowsChanged()
        {
            gridControlVitalForce.ForceInitialize();

            if (gridViewItems.SelectedRowsCount == 1)
            {
                var item = gridViewItems.GetRow(gridViewItems.GetSelectedRows().FirstOrDefault()) as Item;

                if (item == null) return;
                _currentHasChilds = ProductSeatsHelper.ItemHasChilds(item, true);
                InvokeSelectedItemChanged(this, item, 0);
                _lastReadingValue = 0;
            }
            else
            {
                _currentHasChilds = false;
                InvokeSelectedItemChanged(this, null, _lastReadingValue);
            }

            SetNumberOfItemsIndicator();
            
            SetEditMode(!_isEditable);

            RestartReadingRequest();
        }

        /// <summary>
        /// Set number of items indicator text.
        /// </summary>
        private void SetNumberOfItemsIndicator()
        {
            simpleLabelItemNumItems.Text = string.Format(StaticKeys.NumberOfItems, gridViewItems.RowCount,
                gridViewItems.RowCount > 1 ? "s" : string.Empty);

            if(gridViewItems.SelectedRowsCount > 0)
                simpleLabelItemNumItems.Text += string.Format(StaticKeys.NumberOfSelectedItems, gridViewItems.SelectedRowsCount);
        }

        /// <summary>
        /// Return selected items or top items to show their images
        /// </summary>
        /// <returns></returns>
        public BindingList<Item> GetItemsToShowImagesFor()
        {
            if (GetSelectedItems().Any())
            {
                return GetSelectedItems();
            }
            if (IsTopListFirst)
            {
                if (TopItems.Any())
                {
                    return TopItems;
                }
            }
            else
            {
                if (BottomItems.Any())
                {
                    return BottomItems;
                }
            }
            
            return GridItems ?? GetSelectedItems();
        }

        public bool ItemsWithImagesExist()
        {
            var itemsWithImage = GetItemsToShowImagesFor();
            return itemsWithImage != null && itemsWithImage.Any();
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
        /// Gets the system selection items.
        /// </summary>
        /// <returns></returns>
        private BindingList<Item> GetSystemSelectionItems()
        {
            var currentGridDataSource = (BindingList<Item>)gridViewItems.DataSource;

            if (!_showHiddenItems)//*Fix
                currentGridDataSource = currentGridDataSource.Where(i => !i.Hidden).ToBindingList();

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
                        var selectionItem = currentGridDataSource.ElementAtOrDefault(i);

                        if (selectionItem != null)
                        {
                            sytemSelectionItems.Add(selectionItem);
                        }
                    }
                }
                else
                {
                    for (var i = 0; i <= middleIndex; i++)
                    {
                        var selectionItem = currentGridDataSource.ElementAtOrDefault(i);

                        if (selectionItem != null)
                        {
                            sytemSelectionItems.Add(selectionItem);
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < middleIndex; i++)
                {
                    var selectionItem = currentGridDataSource.ElementAtOrDefault(i);

                    if (selectionItem != null)
                    {
                        sytemSelectionItems.Add(selectionItem);    
                    }
                }
            }

            return sytemSelectionItems;
        }

        /// <summary>
        /// Sets the current node name in the node label.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        public void SetCurrentNodeLabel(string nodeName)
        {
            if (!string.IsNullOrEmpty(nodeName))
            {
                simpleLabelCurrentNode.Text = nodeName;    
            }
        }

        /// <summary>
        /// Gets the protocol items fro the passed protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns></returns>
        public BindingList<Item> GetProtocolItems(TestProtocol protocol)
        {
            var items = new BindingList<Item>();

            foreach (var protocolItem in protocol.ProtocolItems)
            {
                if (protocolItem.Item != null)
                    items.Add(protocolItem.Item);
            }

            return items;
        }

        /// <summary>
        /// Clear any filters in the NavGrid.
        /// </summary>
        private void ClearNavGridViewFilters()
        {
            gridViewItems.ClearColumnsFilter();
            if (!_showHiddenItems)//*Fix
                gridViewItems.ActiveFilterString = StaticKeys.HiddenItemsFilterText;
        }

        /// <summary>
        /// Check if parent has custom steps. 
        /// </summary>
        /// <returns></returns>
        private bool CheckIfParentHasCustomSteps()
        {
            var currentTestResult = GetCurrentTestResult();

            //gets the parent of the current test result (the parent of the new step).
            var parent = currentTestResult == null ? null : currentTestResult.Parent;

            if (parent != null && parent.Item != null)
            {
                var parentItems = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = parent.Item.Id });

                return parentItems.Any(c => c.Step > 0);
            }

            return false;
        }

        /// <summary>
        /// Reset the grid item to the Starting Items.
        /// </summary>
        public void ResetStartingItems()
        {
            if (StartingItems == null)
                return;

            _backClicks = 0;

            TestResults.Clear();
            SetItems(StartingItems, true, false, IsStepAutomated);
            
            SetEditMode(!_isEditable);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateTopAndBottomItemsLists()
        {
            TopItems.Clear();
            BottomItems.Clear();

            //Get items for top and bottom based on filtered items or not according to user selection.
            var gridItems = _showHiddenItems ? GridItems : GridItemsFiltered;

            for (var i = 0; i < gridItems.Count; i++)
            {
                //Put the first part in the Top Items list and the other in the Bottom Items list.
                if (i >= 0 && i < (gridItems.Count / 2))
                {
                    TopItems.Add(gridItems[i]);
                }
                else
                {
                    BottomItems.Add(gridItems[i]);
                }
            }

            IsTopListFirst = true;
        }

        /// <summary>
        /// Is the passed item has Insert on no property.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsInserOnNoItem(Item item)
        {
            return item.Properties != null && item.Properties.HasProperty(PropertiesEnum.InsertOnNo, "48");
        }

        /// <summary>
        /// Gets the items that have insert on no property from the passed items.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private BindingList<Item> GetInsertOnNoItems(BindingList<Item> items)
        {
            return items.Where(
                i => i.Properties != null && i.Properties.HasProperty(PropertiesEnum.InsertOnNo, _yesLookupId.ToString())).
                ToBindingList();
        }

        /// <summary>
        /// Gets the items that have the insert in yes property for the passed items.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private BindingList<Item> GetInsertOnYesItems(BindingList<Item> items)
        {
            return items.Where(
                i => i.Properties == null || !i.Properties.HasProperty(PropertiesEnum.InsertOnNo, _yesLookupId.ToString())).
                ToBindingList();
        }

        /// <summary>
        /// Gets the maximum step value in the current list.
        /// </summary>
        /// <returns></returns>
        private int GetMaxStepNumber()
        {
            return GridItems.Max(c => c.Step);
        }

        #endregion

        #region Find Logic

        /// <summary>
        /// Gets search items
        /// </summary>
        /// <returns></returns>
        private BindingList<Item> GetSearchItems(bool useLastSearchCriteria = false)
        {
            var searchText = useLastSearchCriteria ? _lastSearchCriteria : textEditFind.Text;

            return _itemsManager.GetItems(new ItemsFilter() { SearchKey = searchText });
        }

        /// <summary>
        /// Find items logic
        /// </summary>
        private void Find()
        {
            if (!_itemSearchEnabled) return;

            if (string.IsNullOrEmpty(textEditFind.Text)) return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingItems);

            var items = GetSearchItems();

            UiHelperClass.HideSplash();

            if (!items.Any())
            {
                UiHelperClass.ShowInformation("No items found, please try a different search criteria.");

                textEditFind.Focus();
            }
            else
            {
                _searchCriteriaActive = true;
                _lastSearchCriteria = textEditFind.Text;

                UpdateCurrentTestResultChildIndex();

                CheckOneByOneModeForDisable();

                BackClicks += 1;

                InitGridItems(items, true, false, false);

                //adding the visited item of type protocol.
                VisitedItems.Add(new VisitedItem() { ItemId = 0, Type = StaticKeys.VisitedItemTypeSearch });

                SetCurrentNodeLabel(textEditFind.Text);

                IsFirstSwitch = false;
                CurrentNodeId = 0;
                PartHilight = 1;
            }   
        }

        /// <summary>
        /// Refreshes items during find logic
        /// </summary>
        private void RefreshFind(bool keepUserSelection, bool keepCurrentStep)
        {
            if (!_itemSearchEnabled) return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingItems);

            var items = GetSearchItems(true);

            UiHelperClass.HideSplash();

            if (items.Any())
            {
                InitGridItems(items, _isOneByOneTest ? !keepUserSelection : true, keepUserSelection, keepCurrentStep);
            }
        }

        /// <summary>
        /// Sets the find button enable state
        /// </summary>
        private void SetFindButtonEnableState(string findText)
        {
            if (!_itemSearchEnabled) return;

            if (findText == textEditFind.Properties.NullText)
            {
                findText = string.Empty;
            }

            simpleButtonFind.Enabled = !string.IsNullOrEmpty(findText);
            layoutControlItemClearFind.Visibility = simpleButtonFind.Enabled ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        /// <summary>
        /// Clears the find field
        /// </summary>
        private void ClearFindCriteria(bool calledByUser = false)
        {
            if (!_itemSearchEnabled) return;

            _searchCriteriaActive = false;
            _lastSearchCriteria = string.Empty;
            textEditFind.Text = string.Empty;
            SetFindButtonEnableState(textEditFind.Text);
            textEditFind.Focus();

            if (calledByUser)
            {
                BackToParentItems();
            }
        }

        /// <summary>
        /// Sets the visibility of search controls
        /// </summary>
        private void SetSearchControlsVisibility()
        {
            var searchControlsVisibility = _itemSearchEnabled ? LayoutVisibility.Always : LayoutVisibility.Never;

            layoutControlItemFind.Visibility = searchControlsVisibility;
            layoutControlItemFindButton.Visibility = searchControlsVisibility;
            layoutControlItemClearFind.Visibility = searchControlsVisibility;
        }

        #endregion

        #region Test Results Logic

        /// <summary>
        /// Checks if a specific test result is duplicated.
        /// </summary>
        /// <param name="testResult">The test result.</param>
        /// <returns></returns>
        public int CheckIfDuplicated(TestResult testResult)
        {
            for (var i = 0; i < TestResults.Count; i++)
            {
                if (TestResults[i].Item != null && TestResults[i].Item.Id == testResult.Item.Id)
                    return i;

            }

            return -1;
        }

        /// <summary>
        /// Checks if the item is visited before.
        /// </summary>
        /// <param name="visitedItem">The visited item.</param>
        /// <returns></returns>
        public bool CheckIfVisited(VisitedItem visitedItem)
        {
            return VisitedItems.Any(item => item.ItemId == visitedItem.ItemId && item.Type == visitedItem.Type);
        }

        /// <summary>
        /// Checks if a specific test step is duplicated
        /// </summary>
        /// <param name="testResult">The test result.</param>
        /// <returns></returns>
        public int CheckIfTestStepDuplicated(TestResult testResult)
        {
            if (testResult.StepType != null)
                for (var i = 0; i < TestResults.Count; i++)
                {
                    if (TestResults[i].StepType != null && TestResults[i].StepType.Id == testResult.StepType.Id)
                        return i;
                }

            if (testResult.TestProtocol != null)
                for (var i = 0; i < TestResults.Count; i++)
                {
                    if (TestResults[i].TestProtocol != null && TestResults[i].TestProtocol.Id == testResult.TestProtocol.Id)
                        return i;
                }

            return -1;
        }

        /// <summary>
        /// Unselect all the test results.
        /// </summary>
        private void UnselectAllTestResults()
        {
            foreach (var testResult in TestResults)
            {
                if (testResult.ObjectState != Business.Shared.DomainObjects.DomainEntityState.Temp)
                    testResult.IsCurrent = false;
            }
        }

        /// <summary>
        /// Gets the result that is marked is current.
        /// </summary>
        /// <returns>The current test result.</returns>
        public TestResult GetCurrentTestResult()
        {
            return TestResults == null ? null : TestResults.FirstOrDefault(testResult => testResult.IsCurrent);
        }

        /// <summary>
        /// Check if the item that being to insert is in the inserted items.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsInTheInsertedItems(Item item)
        {
            if (item == null) return false;

            return (InsertedItems == null || InsertedItems.FirstOrDefault(i => i.Id == item.Id) != null);
        }

        #endregion

        #region Actions Logic

        /// <summary>
        /// Clean the user selection.
        /// </summary>
        public void ClearSelection()
        {
            ClearNavGridViewFilters();
            gridControlVitalForce.ForceInitialize();
            gridViewItems.ClearSelection();
            gridControlVitalForce.Update();
            gridViewItems.Focus();
        }

        /// <summary>
        /// Back to the parent items list.
        /// </summary>
        public void BackToParentItems()
        {
            if (_itemSearchEnabled && _searchCriteriaActive)
            {
                ClearFindCriteria();
                RefreshItems(false, IsStepAutomated);
                return;
            }

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));
            
            //Store index of current selected item in list
            UpdateCurrentTestResultChildIndex();

            //resetting the show / hide items feature.
            ResetHideShowItemsFeature();

            /*to avoid the applying of this function [SelectNextRowBaseOnUserOldSelection()] 
             *at the back case, since its only applicable when the grid has the same
             *data source.
             */
            NextRowToSelectBaseOnUserSelection = -1;

            //Set the one by one toggle stated based on the setting and user explicit click state
            //also check if parent has custom steps (This check was here before my changes)
            checkButtonOneByOne.Checked = KeepOneByOneMode() || CheckIfParentHasCustomSteps();

            ClearNavGridViewFilters();

            if (TestResults != null && TestResults.Count > 0)
            {
                _backClicks -= 1;

                //gets the current test result (step)
                var currentTestResult = GetCurrentTestResult();
                //Keep a copy of the current result to be used later
                var previousTestResult = GetCurrentTestResult();

                //gets the parent of the current test result (the parent of the new step).
                var parent = currentTestResult == null ? null : currentTestResult.Parent;

                BindingList<Item> newGridDataSource;

                if (currentTestResult != null && parent != null)
                {
                    var parentItems = new BindingList<Item>();

                    //indicates that this test result is an item step (not a protocol step).
                    if (parent.Item != null)
                    {
                        //gets the children of the item.
                        parentItems = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = parent.Item.Id });

                        //sets the node label with the item name.
                        var parentName = parent.Item.Name;

                        if (parent.Item.Name == StaticKeys.Generic)
                        {
                            parentName = StaticKeys.GenericListTitle;
                        }
                        else if (parent.Item.Name == StaticKeys.FourCauses)
                        {
                            parentName = StaticKeys.FourCausesListTitle;
                        }

                        SetCurrentNodeLabel(parentName);
                    }
                    //indicates that this test result is a protocol step.
                    else if (parent.StepType != null)
                    {
                        if (parent.StepType.Value == EnumNameResolver.Resolve(ItemTypeEnum.Product))
                        {
                            //gets the items of a specific type 
                            
                            parentItems = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Products);
                            
                            //sets the node label with the value of the type.
                            SetCurrentNodeLabel(StaticKeys.Products);
                        }
                        else if (parent.StepType.Value == EnumNameResolver.Resolve(ItemTypeEnum.Bacteria))
                        {
                            var lookupBacteria = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Bacteria)).FirstOrDefault();

                            parentItems = _itemsManager.GetItems(new ItemsFilter()
                            {
                                TypeLookupId = lookupBacteria.Id,
                            });

                            //sets the node label with the value of the type.
                            SetCurrentNodeLabel(parent.StepType.Value);
                        }

                    }
                    else if (parent.TestProtocol != null)
                    {
                        //gets the items of a specific type 
                        var protocol = _testProtocolsManager.GetTestProtocolById(new SingleItemFilter() { ItemId = parent.TestProtocol.Id });

                        parentItems = GetProtocolItems(protocol);

                        //sets the node label with the value of the type.
                        SetCurrentNodeLabel(protocol.Name);
                    }

                    //assign the new items to the grid.
                    newGridDataSource = parentItems;

                    //sets the parent as a current test result (since its one step back).
                    parent.IsCurrent = true;

                    currentTestResult.IsCurrent = false;
                }
                //initializing or no back stages remaining
                else
                {
                    //assign the starting items 
                    newGridDataSource = StartingItems;

                    //no test result is current.
                    UnselectAllTestResults();

                    //sets the node label as a root.
                    SetCurrentNodeLabel(CurrentIssue == null ? TestProtocol.Name : CurrentIssue.Name);
                }

                SetItems(newGridDataSource, true, false, false,true,true);

                //If the option to keep one by one mode is active and user activated one by one then when going back to parent
                //select the item that the user drilled down to last time
                if (KeepOneByOneMode())
                {
                    var itemFound = previousTestResult.Item == null ? null: newGridDataSource.FirstOrDefault(i => i.Id == previousTestResult.Item.Id);

                    var newTestResult = GetCurrentTestResult();

                    if (itemFound == null)//If item wasn't found in the childrens list
                    {
                        if (newTestResult != null)
                        {
                             //Select the item based on the stored index in memeory
                            SelectRow(newTestResult.FocusedChildRowHandle);
                        }
                        else
                        {
                            SelectFirstItem();
                        }
                    }
                    else
                    {
                        var itemToHighlightIndex = newGridDataSource.IndexOf(itemFound);
                        var rowHandle = gridViewItems.GetRowHandle(itemToHighlightIndex);

                        //In case row handle was negative number since now row is found,
                        //we get the cached index and use it to select an item
                        SelectRow(rowHandle < 0 ? (newTestResult != null ? newTestResult.FocusedChildRowHandle : 0) : rowHandle);
                    }
                }

                SetEditMode(!_isEditable);
            }
            
            IsFirstSwitch = false;
            CurrentNodeId = 0;
            _partHilight = 1;

            gridViewItems.RefreshData();

            if (_itemSearchEnabled)
            {
                ClearFindCriteria();
            }

            //This call to force the system to throw the event of selected row changed.
            ProcessSelectedRowsChanged();

            gridViewItems.Focus();

           UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Add to test result actions.
        /// </summary>
        private void AddTestResult()
        {
            if (AddToTestResults == null) return;

            var selectedItems = GetSelectedItems();

            if (selectedItems.Count != 0)
            {
                InvokeAddToTestResults(this, selectedItems);
            }

            _lastItemRemovedCount = 0;

            gridViewItems.Focus();
        }

        /// <summary>
        /// Resets the hide and show of the items.
        /// </summary>
        public void ResetHideShowItemsFeature()
        {
            //resetting the button and hide show items feature.
            checkButtonHideShowItems.Checked = false;
            _showHiddenItems = false;
            checkButtonHideShowItems.Image = Resources.eyeHide;
        }

        /// <summary>
        /// Views the children of the parent item.
        /// </summary>
        public void ViewDetails()
        {
            var item = (Item)gridViewItems.GetRow(gridViewItems.FocusedRowHandle);

            if (item == null || !ProductSeatsHelper.ItemHasChilds(item, true)) 
                return;

            ViewDetails(item);
        }

        /// <summary>
        /// Sets the focused row handle in the current result in memory
        /// </summary>
        public void UpdateCurrentTestResultChildIndex()
        {
            var currentTestResult = GetCurrentTestResult();

            if (currentTestResult != null)
            {
                currentTestResult.FocusedChildRowHandle = gridViewItems.FocusedRowHandle;
            }
        }

        /// <summary>
        /// View Details for specific item.
        /// </summary>
        /// <param name="item"></param>
        private void ViewDetails(Item item)
        {
            if (item == null) return;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            if (_itemSearchEnabled)
            {
                ClearFindCriteria();
            }
            
            var items = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = item.Id });

            if (items == null || items.Count == 0)
            {
                UiHelperClass.HideSplash();
                return;
            }

            //Store the current row handle in memory in current result
            UpdateCurrentTestResultChildIndex();

            //It is important to clear the filters after getting the selected items because if we 
            //clear the filters before then it becomes really possible to get another row and not the
            //row the user selected.
            ClearNavGridViewFilters();

            ResetHideShowItemsFeature();

            //Set the one by one toggle stated based on the setting and user explicit click state
            checkButtonOneByOne.Checked = KeepOneByOneMode();

            CurrentStep = 1;

            /*to avoid the applying of this function [SelectNextRowBaseOnUserOldSelection()] 
             *at the drill down case, since its only applicable when the grid has the same
             *data source.
             */
            NextRowToSelectBaseOnUserSelection = -1;

            if (items.Count > 0)
            {
                if (TestResults.Count > 0)
                {
                    var parent = GetCurrentTestResult();

                    if (parent != null)
                        parent.IsCurrent = false;

                    var testResult = new TestResult
                    {
                        Item = item,
                        Parent = parent,
                        TestIssue = CurrentIssue,
                        IsCurrent = true,
                        DateTime = DateTime.Now,
                        Id = TestResults.Count > 0 ? TestResults.Max(tr => tr.Id) + 1 : 1
                    };

                    TestResults.Add(testResult);
                    gridViewItems.RefreshData();
                    SetCurrentNodeLabel(item.Name);
                }
                else
                {
                    var testResult = new TestResult
                    {
                        Item = item,
                        Parent = null,
                        TestIssue = CurrentIssue,
                        IsCurrent = true,
                        DateTime = DateTime.Now,
                        Id = TestResults.Count > 0 ? TestResults.Max(tr => tr.Id) + 1 : 1
                    };

                    TestResults.Add(testResult);
                }

                SetItems(items, true, false, false);

                //adding the visited item of type item.
                VisitedItems.Add(new VisitedItem() { ItemId = item.Id, Type = StaticKeys.VisitedItemTypeItem });

                _backClicks += 1;
                SetEditMode(!_isEditable);

                _partHilight = 1;
                IsFirstSwitch = false;
                CurrentNodeId = 0;

                gridViewItems.RefreshData();

                //This call to forec the system to trow the event of selected row changed, this because the row handler does not change so grid will not understatnd its selection had been changed.
                ProcessSelectedRowsChanged();

                gridViewItems.Focus();
            }

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Highlight the items within the current step.
        /// </summary>
        /// <param name="items">The items.</param>
        public void HilightSteps(BindingList<Item> items)
        {
            gridViewItems.FocusedRowHandle = 0;

            var selectedItems = 0;
            var visibleRowHandle = 0;

            if (!_isSwitched && !_isSplitted)
            {
                var visibleItems = items.Where(i => !i.Hidden).ToList();

                //check if there are items in the source.
                if (visibleItems.Count > 0)
                {
                    //clear the selected items.
                    gridViewItems.ClearSelection();

                    //select the items within the current step.
                    for (var i = 0; i < visibleItems.Count; i++)
                    {
                        if (visibleItems[i].Step == CurrentStep)
                        {
                            gridViewItems.SelectRow(i);
                            selectedItems += 1;

                            if (selectedItems == 1)
                            {
                                visibleRowHandle = i;
                            }
                        }
                    }

                    //if all the items within the current step is selected (added as test results), skipt this step.
                    if (selectedItems == 0)
                    {
                        if (CurrentStep < GetMaxStepNumber())
                            CurrentStep += 1;
                        else
                            CurrentStep = 1;

                        HilightSteps(items);
                    }

                    gridViewItems.FocusedRowHandle = gridViewItems.GetSelectedRows().LastOrDefault();
                    gridViewItems.MakeRowVisible(visibleRowHandle);
                    gridViewItems.RefreshData();
                }
            }
        }

        /// <summary>
        /// Highlight logic for the items in the grid.
        /// </summary>
        public void SwitchSelection()
        {
            IsSwitching = true;
            ImageRefreshNeeded = true;
            _isSwitched = true;
            var currentTestResult = GetCurrentTestResult();

            /*TODO: This checking is for determining if the navigation grid in the Items mode or in the major issues mode,
             *if the currentTestResult does equals null, then the navigation grid is in the issues mode, what should be done
             * here is declaring an enum or a flag to indicate the mode of the navigation grid.*/

            SwitchingLogic(_searchCriteriaActive? CurrentNodeId : currentTestResult != null ? currentTestResult.Id : TestProtocol.Id);

            _part = (_part == 1 ? _part + 1 : _part - 1);
            _partHilight = (_partHilight == 1 ? _partHilight + 1 : _partHilight - 1);

            gridViewItems.RefreshData();
            gridViewItems.MakeRowVisible(0);

            ClearSelection();

            //This call to force the system to throw the event of selected row changed.
            ProcessSelectedRowsChanged();

            gridViewItems.Focus();
            IsSwitching = false;
            ImageRefreshNeeded = false;
        }

        /// <summary>
        /// Contains the logic of switching or highlight the items in the grid.
        /// </summary>
        /// <param name="currentGridParentId">The current parent of the items in the navigation grid.</param>
        private void SwitchingLogic(int currentGridParentId)
        {
            var items = (BindingList<Item>)gridViewItems.DataSource;

            if (!_showHiddenItems)//*Fix
                items = items.Where(i => !i.Hidden).ToBindingList();

            //The current parent (can be a test protocol id , test result id) id does not equals the current node id means that its a new list in the navigation grid.
            if (currentGridParentId != CurrentNodeId)
            {
                //setting some flags and Int some variables
                IsFirstSwitch = true;
                IsTopListFirst = true;

                //setting the current node id.
                CurrentNodeId = currentGridParentId;

                //clearing the top and bottom items.
                TopItems.Clear();
                BottomItems.Clear();
            }
            //The case of switching the parts in the same list [not a new set of items]
            else
            {
                //Then its not the first switch
                IsFirstSwitch = false;
            }

            //If its the first switch
            if (IsFirstSwitch)
            {
                //Split the items into top (should contain the first part of the list) and bottom (should contain the remaining items) lists.
                for (var i = 0; i < items.Count; i++)
                {
                    //Put the first part in the Top Items list and the other in the Bottom Items list.
                    if (i >= 0 && i < (gridViewItems.DataRowCount / 2))
                    {
                        TopItems.Add(items[i]);
                    }
                    else
                    {
                        BottomItems.Add(items[i]);
                    }
                }

                //Clearing the data source items.
                items.Clear();

                //Switching operation, putting the bottom items first, then the top items.
                foreach (var bottomItem in BottomItems)
                {
                    items.Add(bottomItem);
                }

                foreach (var topItem in TopItems)
                {
                    items.Add(topItem);
                }

                //invert the isTopList flag
                IsTopListFirst = !IsTopListFirst;
            }
            //If its not the first switch
            else
            {
                //Clearing the data source items.
                items.Clear();

                //Check the position of the Top Items should it be the first part or the second part ?.
                if (!IsTopListFirst)
                {
                    //Switching operation, adding the Top items then adding the Bottom items.
                    foreach (var topItem in TopItems)
                    {
                        items.Add(topItem);
                    }

                    foreach (var bottomItem in BottomItems)
                    {
                        items.Add(bottomItem);
                    }
                }
                else
                {
                    //Switching operation, adding the Bottom items then adding the Top items.
                    foreach (var bottomItem in BottomItems)
                    {
                        items.Add(bottomItem);
                    }

                    foreach (var topItem in TopItems)
                    {
                        items.Add(topItem);
                    }
                }

                IsTopListFirst = !IsTopListFirst;
            }


            //Set the new data source
            //[ANAS: IMPORTANT: The last flag "updateTopAndBottom" has been set to false to prevent the SetItems method called inside InitGridItems from
            //refreshing the Top and Bottom items because in the switch logic those two lists has been switched and we don't want to flip them again]
            InitGridItems(items, false, false, false,false);

        }

        /// <summary>
        /// Refreshes the items list.
        /// </summary>
        public void RefreshItems(bool keepUserSelection, bool keepCurrentStep)
        {
            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            var currentTestResult = GetCurrentTestResult();

            if (_itemSearchEnabled && _searchCriteriaActive && !string.IsNullOrEmpty(_lastSearchCriteria))
            {
                RefreshFind(keepUserSelection, keepCurrentStep);
            }
            else if (currentTestResult != null && currentTestResult.Item != null)
            {
                var items = _itemsManager.GetItemChildren(new SingleItemFilter() { ItemId = currentTestResult.Item.Id });
                InitGridItems(items, _isOneByOneTest ? !keepUserSelection : true, keepUserSelection, keepCurrentStep);
            }
            else if (currentTestResult != null && currentTestResult.StepType != null)
            {
                var items = new BindingList<Item>();
                if (currentTestResult.StepType.Value == EnumNameResolver.Resolve(ItemTypeEnum.Bacteria))
                {
                    var lookupBacteria = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Bacteria)).FirstOrDefault();

                    items = _itemsManager.GetItems(new ItemsFilter()
                    {
                        TypeLookupId = lookupBacteria.Id,
                    });
                }
                else if (currentTestResult.StepType.Value == EnumNameResolver.Resolve(ItemTypeEnum.Product))
                {
                    items = (BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Products);
                }

                InitGridItems(items,_isOneByOneTest ? !keepUserSelection : true , keepUserSelection, keepCurrentStep);
            }
            else if (currentTestResult != null && currentTestResult.TestProtocol != null)
            {
                var items = GetProtocolItems(currentTestResult.TestProtocol);

                InitGridItems(items, _isOneByOneTest ? !keepUserSelection : true, keepUserSelection, keepCurrentStep);
            }
            else //this case happens when its a new test and no navigation occured on the grid.
            {
                InitGridItems(StartingItems, _isOneByOneTest ? !keepUserSelection : true, keepUserSelection, keepCurrentStep);
            }

            IsFirstSwitch = false;
            CurrentNodeId = 0;
            _partHilight = 1;

            if (_isOneByOneTest && keepUserSelection)
            {
                SelectNextRowBaseOnUserOldSelection();
            }
            else
            {
                //This call to force the system to throw the event of selected row changed.
                ProcessSelectedRowsChanged();
            }

            gridViewItems.Focus();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Return the user selection after refresh.
        /// </summary>
        private void SelectNextRowBaseOnUserOldSelection()
        {
            
            if (_nextRowToSelectBaseOnUserSelection == -1)
                return;

            if (_nextRowToSelectBaseOnUserSelection > 0 && _lastItemRemovedCount > 0)
            {
                _nextRowToSelectBaseOnUserSelection -= _lastItemRemovedCount;
            }

            var nextRow = _nextRowToSelectBaseOnUserSelectionTemp >= gridViewItems.DataRowCount - 1 ? gridViewItems.DataRowCount - 1 : _nextRowToSelectBaseOnUserSelectionTemp;

                if (nextRow < 0)
                    nextRow = 0;

            if (IsOneByOneTest && IsStepAutomated)
            {
                Item item;

                if (_showHiddenItems)
                {
                    nextRow++;
                    item = (Item) gridViewItems.GetRow(nextRow > gridViewItems.DataRowCount - 1 ? 0 : nextRow);
                }
                else
                {
                    item = (Item) gridViewItems.GetRow(nextRow);
                }

                if (item != null && item.Step != 0)
                {
                    CurrentStep = item.Step;
                    HilightSteps(GridItems);
                }
            }
            else
            {
                gridViewItems.ClearSelection();
                gridViewItems.SelectRow(nextRow);
                gridViewItems.MakeRowVisible(nextRow);
                gridViewItems.FocusedRowHandle = nextRow;
            }
        }

        /// <summary>
        /// Splits the items in the grid
        /// </summary>
        private void SplitItems()
        {
            _isSplitted = true;

            InitGridItems(GetSystemSelectionItems(), false, false, false);

            IsFirstSwitch = false;
            CurrentNodeId = 0;

            _partHilight = 1;
            gridViewItems.RefreshData();

            //This call to force the system to throw the event of selected row changed.
            ProcessSelectedRowsChanged();

            gridViewItems.Focus();
        }

        private bool KeepOneByOneMode()
        {
            //KEEP ONE BY ONE MODE TOGGELED ONLY IF SETTING IS ACTIVE AND IF THE LAST TOGGLE WAS DONE BY USER
            return checkEditKeepOneByOne.Checked && _oneByOneActivedByUser;
        }

        /// <summary>
        /// Checks the keep one by one mode setting and the current stsatus of the one by one toggle before trying to disable one by one mode
        /// </summary>
        public void CheckOneByOneModeForDisable()
        {
            //CALL THIS LOGIC ONLY IN PLACES WHERE ONE BY ONE MODE IS SUPPOSED TO BE DISABLED FIRST
            //THIS LOGIC IS IMPORTANT TO ALLOW ONE BY ONE TO WORK NORMALLY FOR NORMAL USERS AND TO KEEP IT TOGGELED FOR OTHERS WHO WANT IT THIS WAY

            IsOneByOneTest = KeepOneByOneMode();
        }

        /// <summary>
        /// Set one by one test mode;
        /// </summary>
        /// <param name="isOn"></param>
        private void SetOneByOneTestMode(bool isOn)
        {
            if (isOn)
            {
                checkButtonOneByOne.Image = Resources.OneByOneOn;
                gridViewItems.OptionsView.EnableAppearanceEvenRow = true;

                if (!IsStepAutomated && gridViewItems.RowCount > 0)
                {
                    SelectFirstItem();
                }
                else if (gridViewItems.RowCount > 0)
                {
                    HilightSteps(GridItems);
                }

                //This call to force the system to throw the event of selected row changed.
                ProcessSelectedRowsChanged();
            }
            else
            {
                //Reset the state of the flag that indicates that user explicitly clicked the toggle
                _oneByOneActivedByUser = false;
                checkButtonOneByOne.Image = Resources.OneByOneOff;
                gridViewItems.OptionsView.EnableAppearanceEvenRow = false;
                gridViewItems.SelectAll();
            }

            _isOneByOneTest = isOn;

            checkButtonOneByOne.Checked = isOn;

            SetEditMode(!_isEditable);
        }

        /// <summary>
        /// Select first item in the grid.
        /// </summary>
        private void SelectFirstItem()
        {
            gridViewItems.Focus();
            gridViewItems.FocusedRowHandle = 0;
            gridViewItems.ClearSelection();
            gridViewItems.SelectRow(0);
            gridViewItems.MakeRowVisible(0);
        }

        /// <summary>
        /// Selects a certain row handle
        /// </summary>
        /// <param name="rowHandle"></param>
        private void SelectRow(int rowHandle)
        {
            gridViewItems.FocusedRowHandle = rowHandle;
            gridViewItems.ClearSelection();
            gridViewItems.SelectRow(rowHandle);
            gridViewItems.FocusedRowHandle = rowHandle;
            gridViewItems.MakeRowVisible(rowHandle);
            gridViewItems.Focus();
        }

        /// <summary>
        /// Move the selection of the grid in the One By One Test mode.
        /// </summary>
        private void MoveSelcection()
        {
            if (gridViewItems.IsLastRow)
            {
                SelectFirstItem();
            }
            else
            {
                MoveDown();
            }
        }

        /// <summary>
        /// Move down in the grid
        /// </summary>
        private void MoveDown()
        {
            var nextRowHandle = gridViewItems.FocusedRowHandle + 1;
            gridViewItems.ClearSelection();
            gridViewItems.SelectRow(nextRowHandle);
            gridViewItems.FocusedRowHandle = nextRowHandle;
            gridViewItems.MakeRowVisible(nextRowHandle);
            gridViewItems.Focus();
        }


        #endregion

        #region Reading Logic

        /// <summary>
        /// Set the status test status bar.
        /// </summary>
        public void SetReadingStatusBarMode(TestBarStateEnum state, string actionName, float secondsToWait)
        {
            switch (state)
            {
                case TestBarStateEnum.TakeReading:
                    
                    layoutControlItemWaitingToTakeAction.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.Ready:
                    
                    layoutControlItemWaitingToTakeAction.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.Reading:
                    
                    layoutControlItemWaitingToTakeAction.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Always;
                    break;
                case TestBarStateEnum.WaitBeforTakeAction:
                    progressBarControlAutoTestWaitingStatus.EditValue = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime - (secondsToWait * 1000);
                    layoutControlItemWaitingToTakeAction.Visibility = LayoutVisibility.Always;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.HideStatus:
                    layoutControlItemWaitingToTakeAction.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;
                case TestBarStateEnum.WaitingToRelease:
                    layoutControlItemWaitingToTakeAction.Visibility = LayoutVisibility.Never;
                    layoutControlItemReadingInProgress.Visibility = LayoutVisibility.Never;
                    break;

            }


        }

        /// <summary>
        /// Set the Auto test play mode.
        /// </summary>
        /// <param name="isOn"></param>
        public void SetAutoTestPlayMode(bool isOn)
        {
            IsAutoNavigationOn = isOn;
            progressBarControlAutoTestWaitingStatus.EditValue = 0;
            progressBarControlAutoTestWaitingStatus.Properties.Maximum = CsaEmdUnitManager.Instance.ItemTestingAutoPlayWaitingTime;
            progressBarControlAutoTestWaitingStatus.Properties.Minimum = 0;

            if (!_isWaitingToTakeAction)
                SetReadingStatusBarMode(TestBarStateEnum.Ready, string.Empty, 0);

        }

        /// <summary>
        /// Starts the reading process
        /// </summary>
        public void StartReading()
        {

            if (IsAutoNavigationOn && !_isWaitingToTakeAction)
                SetAutoTestPlayMode(true);

            InvokeReadingRequest(this, CsaEmdUnitManager.Instance.IsBroadcastingOn ? GetItemsToBroadcast() : new List<Item>());


            IsWaitingReadingResponse = true;

        }

        /// <summary>
        /// Stops the reading process
        /// </summary>
        public void CancelReading()
        {
            InvokeCancelReadingRequest(this);

            IsWaitingReadingResponse = false;
            _isWaitingToTakeAction = false;
        }

        /// <summary>
        /// Restart the reading request, in case the selected item changed or spited or highlighted.
        /// </summary>
        public void RestartReadingRequest()
        {
            InvokeCancelReadingRequest(this);
            StartReading();
        }

        /// <summary>
        /// Process the response on the request a reading.
        /// </summary>
        /// <param name="reading">The reading</param>
        private void ProcessResponsedReading(int reading)
        {
            _lastReadingValue = reading;
            IsWaitingReadingResponse = false;

            if (IsAutoNavigationOn)
            {
                _isWaitingToTakeAction = true;
            }
        }

        #endregion
        
        #region Automation Logic
         
        /// <summary>
        /// Do the auto test action.
        /// </summary>
        public void DoAutoTestAction()
        {
            AutoTestActionEngine(false);
        }

        /// <summary>
        /// Gets the next action name that's going to do by the auto test.
        /// </summary>
        /// <returns></returns>
        public string GetNextActionName()
        {
            return AutoTestActionEngine(true);
        }

        /// <summary>
        /// Do the auto test action, the Engine.
        /// </summary>
        /// <param name="justChecking">The just checking flag, to tell the automation to execute the action or not.</param>
        /// <returns></returns>
        private string AutoTestActionEngine(bool justChecking)
        {
            string actionName;
            var userSelectedItems = GetSelectedItems();
            var systemSelectionItems = GetSystemSelectionItems();
            var currentItemCount = gridViewItems.DataRowCount;

            //isUserSelection : is the current selection is done by user or by the system it self, this case to check the Select All status when the  item list open.
            var isUserSelection = userSelectedItems.Count > 0 && userSelectedItems.Count < currentItemCount;

            if (CrossLayersSharedLogic.IsAcceptableReading(_lastReadingValue))
            {
                actionName = ProcessAutomatedYesAction(isUserSelection, userSelectedItems, justChecking, currentItemCount,
                                              systemSelectionItems, true);
            }
            else
            {
                actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, true);
            }

            if (!justChecking)
                _isWaitingToTakeAction = false;

            return actionName;
        }

        /// <summary>
        /// Process the No reading Automated actions.
        /// </summary>
        /// <param name="userSelectedItems">User selected Items.</param>
        /// <param name="justChecking">Is this call just for checking, so no actions will done. </param>
        /// <param name="currentItemCount">The count of items inside the current list.</param>
        /// <param name="systemSelectionItems">The system selection items.</param>
        /// <param name="checkForInsertOnNoItems">Check for insert on no property, 
        /// this flag to determine if the code will check for the insert on no items or not, 
        /// because if this check done before we should not repeat it 
        /// - this repeating will cause a stackOverflow exception because 
        /// both of ProcessAutomatedYesAction and ProcessAutomatedNoAction call each other when having one or more item with insert on no property</param>
        /// <returns>The action name.</returns>
        private string ProcessAutomatedNoAction(BindingList<Item> userSelectedItems, bool justChecking, int currentItemCount, BindingList<Item> systemSelectionItems, bool checkForInsertOnNoItems)
        {
            string actionName;

            //Case 1 : The One by one mode is on.
            if (_isOneByOneTest)
            {
                //Case 1 - Part 1 : Is the step automated.
                if (IsStepAutomated && CurrentIssue != null)
                {
                    //Case 1 - Part 1.1 : The current step has one or more insert on no item.
                    if (checkForInsertOnNoItems && GetInsertOnNoItems(userSelectedItems).Count > 0)
                    {
                        //Here the yes action process will filter the items and do the action base on the item type 
                        actionName = ProcessAutomatedYesAction(true, userSelectedItems, justChecking, currentItemCount,
                                                      systemSelectionItems, false);
                    }
                    //Case 1 - Part 1.2 : The current step has no one or more items with insert on no property.
                    else
                    {
                        actionName = StaticKeys.MovingAction;

                        if (!justChecking)
                        {
                            if (CurrentStep < GetMaxStepNumber())
                                CurrentStep += 1;
                            else
                                CurrentStep = 1;

                            HilightSteps(GridItems);
                        }
                    }
                    
                }
                //Case 1 - Part 2 : One by one only is on.
                else
                {
                    //Case 1 - Part 2.1 : The selected items list has one or more insert on no item. 
                    if (checkForInsertOnNoItems && GetInsertOnNoItems(userSelectedItems).Count > 0)
                    {
                        actionName = ProcessAutomatedYesAction(true, userSelectedItems, justChecking, currentItemCount,
                                                     systemSelectionItems, false);
                    }
                    //Case 1 - Part 2.2 : The selected items list has no insert on no items. 
                    else
                    {
                        actionName = StaticKeys.MovingAction;

                        if (!justChecking)
                        {
                            MoveSelcection();
                        }
                    }
                   
                }
                    
            }
            //Case 2 : Normal automation, there are a user selection, or there one item in the grid. 
            else if (userSelectedItems.Count > 0 || currentItemCount == 1)
            {
                //Case 2 - Part 1 :  The selected item(s) have one or more insert on no property. 
                if (checkForInsertOnNoItems && GetInsertOnNoItems(userSelectedItems).Count > 0)
                {
                    actionName = ProcessAutomatedYesAction(true, userSelectedItems, justChecking, currentItemCount,
                                                  systemSelectionItems, false);
                }
                //Case 2 - Part 2 : The selected item(s) have no insert on no property. 
                else
                {
                    if (currentItemCount > 1)
                    {
                        var clearOnNo = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.ClearSelectionOnNoReading));

                        //ToDo: this method  SetOrGetCachableData has some issue its not load the data depends on the passed keys and enums.
                        ////clearing on no reading.
                        //var clearOnNo = (Setting)CacheHelper.SetOrGetCachableData(new CachingFilter()
                        //                            {
                        //                                CacheKey = CachableDataEnum.SingleValues.ToString(),
                        //                                InformationType = CachableDataEnum.SingleValues,
                        //                                SingleValueType = SingleValueTypeEnum.Settings,
                        //                                SettingKey = SettingKeys.ClearSelectionOnNoReading
                        //                            });

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
                    else
                    {
                        actionName = StaticKeys.NoneAction;
                    }
                }
            }
            //Case 3 : There is one system selection and this call should check for insert on no items.
            else if(systemSelectionItems.Count == 1 && checkForInsertOnNoItems && IsInserOnNoItem(systemSelectionItems[0]))
            {
                actionName = ProcessAutomatedYesAction(true, userSelectedItems, justChecking, currentItemCount,
                                                  systemSelectionItems, false);
            }
            //Case 4 : There are no user selection and there are a system selection with more than one item selected by the system.
            else
            {
                actionName = StaticKeys.SwitchingAction;

                if (!justChecking)
                {
                    SwitchSelection();
                }
            }

            return actionName;
        }

        /// <summary>
        /// Process the Yes reading Automated actions.
        /// </summary>
        /// <param name="isUserSelection">Is current selection done by user or by system.</param>
        /// <param name="userSelectedItems">User selected Items.</param>
        /// <param name="justChecking">Is this call just for checking, so no actions will done. </param>
        /// <param name="currentItemCount">The count of items inside the current list.</param>
        /// <param name="systemSelectionItems">The system selection items.</param>
        /// <param name="checkForInsertOnNoItems">Check for insert on no property, 
        /// this flag to determine if the code will check for the insert on no items or not, 
        /// because if this check done before we should not repeat it 
        /// - this repeating will cause a stackOverflow exception because 
        /// both of ProcessAutomatedYesAction and ProcessAutomatedNoAction call each other when having one or more item with insert on no property</param>
        /// <returns>The action name.</returns>
        private string ProcessAutomatedYesAction(bool isUserSelection, BindingList<Item> userSelectedItems, bool justChecking, int currentItemCount, BindingList<Item> systemSelectionItems, bool checkForInsertOnNoItems)
        {
            var actionName = string.Empty; 

            //Case 1: User select one item.
            if (userSelectedItems.Count == 1)
            {
                //Case 1 - Part 1 : The current user control is using for Item List not for Issue.
                if (DontDrillDownInFirstInsert)
                {
                    //Case 1 - Part 1.1 : The user selected item is inserted before.
                    if (!IsInTheInsertedItems(userSelectedItems[0]))
                    {
                        //Case 1 - Part 1.1.1 : The user selected item has insert on no property.
                        if (checkForInsertOnNoItems && IsInserOnNoItem(userSelectedItems[0]))
                        {
                            actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, false);    
                        }
                        //Case 1 - Part 1.1.2 : The user selection has not insert on no property.
                        else
                        {
                            actionName = StaticKeys.InsertingAction;

                            if (!justChecking)
                                InsertedItems = InvokeAddToTestResults(this, userSelectedItems);
                        }

                        
                    }
                    //Case 1 - Part 1.2 : The user selected item is inserted before and has children.
                    else if (ProductSeatsHelper.ItemHasChilds(userSelectedItems[0], true))
                    {
                        //Case 1 - Part 1.2.1 : The user selected item has insert on no property.
                        if (checkForInsertOnNoItems && IsInserOnNoItem(userSelectedItems[0]))
                        {
                            actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, false);
                        }
                        //Case 1 - Part 1.2.2 : The user selection has not insert on no property.
                        else
                        {
                            actionName = StaticKeys.ViewDetailsAction;

                            if (!justChecking)
                                ViewDetails(userSelectedItems[0]);
                        }
                    }
                    //Case 1 - Part 1.3 : The user selected item is inserted before and has no children.
                    else
                    {
                        actionName = StaticKeys.NoneAction;
                    }
                }
                //Case 1 - Part 2 : The current user control is using for Issue.
                else
                {
                    //Case 1 - Part 2.1 : The user selected item has insert on no property.
                    if (checkForInsertOnNoItems && IsInserOnNoItem(userSelectedItems[0]))
                    {
                        actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, false);
                    }
                    //Case 1 - Part 2.2 : The user selected item has no insert on no property.
                    else
                    {
                        actionName = StaticKeys.InsertingAction;

                        if (!justChecking)
                        {
                            InvokeAddToTestResults(this, userSelectedItems);
                            InvokeSetImageIgnoreState(true);
                            var item = userSelectedItems.FirstOrDefault();


                            if (!_isOneByOneTest || (_isOneByOneTest && IsStepAutomated))
                                {
                                    if (item != null && ProductSeatsHelper.ItemHasChilds(item, true))
                                        ViewDetails(userSelectedItems.FirstOrDefault());
                                }
                            else if (!IsStepAutomated)
                            {
                                if (item != null && ProductSeatsHelper.ItemHasChilds(item, true))
                                    ViewDetails(userSelectedItems.FirstOrDefault());
                                else if(_showHiddenItems)
                                    MoveSelcection();
                            }
                            InvokeSetImageIgnoreState(false);
                        }
                    }
                    
                }
            }
            //Case 2 : User select many items.
            else if (userSelectedItems.Count > 1)
            {
                //Case 2 - Part 1 : Is the user select all the grid items.
                if (userSelectedItems.Count == currentItemCount && !IsStepAutomated)
                {
                    var insertOnNoItems = GetInsertOnNoItems(userSelectedItems);

                    //Case 2 - Part 1.1 : Is the list contains one ore more item that has a insert on no property.
                    if (checkForInsertOnNoItems && insertOnNoItems.Count > 0)
                    {
                        actionName = ProcessAutomatedNoAction(insertOnNoItems, justChecking, currentItemCount, systemSelectionItems, false);
                    }
                    //Case 2 - Part 1.2 : There are one or more item in the list has no insert on no property.
                    else if (!checkForInsertOnNoItems || insertOnNoItems.Count < userSelectedItems.Count)
                    {
                        if (currentItemCount > 1)
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
                    
                }
                //Case 2 - Part 2: Many User Selection with one by one mode and there are a steps.
                //[Oday] - This code should be refactored in terms of the following if statment conditions.
                else if ((_isOneByOneTest && IsStepAutomated) || _isOneByOneTest == true || _isOneByOneTest == false)
                {
                    var insertOnNoItems = GetInsertOnNoItems(userSelectedItems);

                    //Case 2 - Part 2.1 : Is the list contains one ore more item that has a insert on no property.
                    if (checkForInsertOnNoItems && insertOnNoItems.Count > 0)
                    {
                        actionName = ProcessAutomatedNoAction(insertOnNoItems, justChecking, currentItemCount, systemSelectionItems, false);
                    }
                    //Case 2 - Part 2.2 : There are one or more item in the list has no insert on no property.
                    else if(!checkForInsertOnNoItems && insertOnNoItems.Count > 0)
                    {
                        actionName = StaticKeys.InsertingAction;

                        if (!justChecking)
                        {
                            InvokeAddToTestResults(this, insertOnNoItems);
                        }
                    }

                    //Case 2 - Part 2.3 : There are one or more item in the list has no insert on no property and THIS CALL IS NOT FOR CHECKING THE INSER ON NO PROPERTY.
                    //Happens when calling is coming from the ProcessInserOnNo Action. so items have been processed and waiting to take action over the rest items (InsertOnYesItems).
                    if (checkForInsertOnNoItems && insertOnNoItems.Count < userSelectedItems.Count )
                    {
                        actionName = StaticKeys.InsertingAction;

                        if (!justChecking)
                        {
                            InvokeAddToTestResults(this, GetInsertOnYesItems(userSelectedItems));
                        }
                    }
                }
                //Case 2 - Part 3 : The One by one mode is off and no steps automated.
                else
                {
                    actionName = StaticKeys.NoneAction;
                }

            }
            //Case 3 : This is the last step while the user navigate some items, in this case the grid has at most 2 items one of them is selected by the system and reading is taking on it.
            else if (currentItemCount <= 2 || systemSelectionItems.Count == 1)
            {
                //Case 3 - Part 1 : This Grid is using for Item Not for Issues.
                if (DontDrillDownInFirstInsert)
                {
                    //Case 3 - Part 1.1 : The system selected item has not inserted to the issues yet.
                    if (!IsInTheInsertedItems(systemSelectionItems.FirstOrDefault()))
                    {
                        //Case 3 - Part 1.1.2 : The system selected item has insert on no property.
                        if (checkForInsertOnNoItems && IsInserOnNoItem(systemSelectionItems[0]))
                        {
                            actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, false);
                        }
                        //Case 3 - Part 1.1.3 : The system selected item has no insert on no property.
                        else
                        {
                            actionName = StaticKeys.InsertingAction;

                            if (!justChecking)
                                InsertedItems = InvokeAddToTestResults(this, systemSelectionItems);
                        }
                    }
                    //Case 3 - Part 1.2 : The system selected item had been added to issues and it has children.
                    else if (ProductSeatsHelper.ItemHasChilds(systemSelectionItems[0], true))
                    {
                        //Case 3 - Part 1.2.1 : The system selected item has insert on no property.
                        if (checkForInsertOnNoItems && IsInserOnNoItem(systemSelectionItems[0]))
                        {
                            actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, false);
                        }
                        //Case 3 - Part 1.2.2 : The system selected item has no insert on no property.
                        else
                        {
                            actionName = StaticKeys.ViewDetailsAction;

                            if (!justChecking)
                                ViewDetails(systemSelectionItems[0]);
                        }
                    }
                    //Case 3 - Part 1.3 : The system selected item has been added as a test result before and has no children.
                    else
                    {
                        actionName = StaticKeys.NoneAction;
                    }
                }
                //Case 3 - Part 2 :
                else
                {
                    //Case 3 - Part 2.1 :
                    if (checkForInsertOnNoItems && systemSelectionItems.Any() && IsInserOnNoItem(systemSelectionItems[0]))
                    {
                        actionName = ProcessAutomatedNoAction(userSelectedItems, justChecking, currentItemCount, systemSelectionItems, false);
                    }
                    //Case 3 - Part 2.2 :
                    else
                    {
                        actionName = StaticKeys.InsertingAction;

                        if (!justChecking)
                        {
                            gridViewItems.SelectRow(_partHilight - 1);

                            InvokeAddToTestResults(this, systemSelectionItems);
                            InvokeSetImageIgnoreState(true);
                            ViewDetails(systemSelectionItems.FirstOrDefault());
                            InvokeSetImageIgnoreState(false);
                        }
                    }
                }
            }
            //Case 4 : This is the normal case of automation, got a 50 reading on some system selection items.
            else
            {
                //Case 4 - Part 1 : Split the system selection items.
                if (userSelectedItems.Count == 0 && !isUserSelection)
                {
                    actionName = StaticKeys.SplittingAction;

                    if (!justChecking)
                    {
                        SplitItems();
                    }
                }
            }

            return actionName;
        }

        /// <summary>
        /// Clear the flags of the next user selection.
        /// </summary>
        private void ClearNextUserSelectionMembers()
        {
            _nextRowToSelectBaseOnUserSelection = -1;
            _lastItemRemovedCount = 0;
        }

        #endregion

        #endregion

        #region Events Handlers

        #region Button Events

        /// <summary>
        /// Handel the toggle change of show or hide inserted items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkButtonHideShowItems_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkButtonHideShowItems_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ToggleHideShowItems();
            }
        }

        /// <summary>
        /// Handles the click of the back button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonBack_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonBack_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                BackToParentItems();
            }
        }

        /// <summary>
        /// Handles the click of the view details button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonViewDetails_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonViewDetails_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ViewDetails();
            }
        }

        /// <summary>
        /// Handles the click of the switch button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonSwitch_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonSwitch_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SwitchSelection();
            }
        }

        /// <summary>
        /// Handles the click of the split button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonSplit_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonSplit_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SplitItems();
            }
        }

        /// <summary>
        /// Handles the click on the reload button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        private void simpleButtonReloadItems_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonReloadItems_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ClearSelection();
                ClearNextUserSelectionMembers();
                RefreshItems(false, IsStepAutomated);
            }
        }

        /// <summary>
        /// Handle the clear user selection button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleClearUserSelection_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleClearUserSelection_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ClearSelection();
            }
        }

        /// <summary>
        /// Handle the add to the test result button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonAddToTestResults_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonAddToTestResults_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                AddTestResult();
            }
        }

        /// <summary>
        /// Handle one by one check button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkButtonOneByOne_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkButtonOneByOne_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //We use the click event because it is the only way to know if the
                //check was clicked by user explicitly
                if (!checkButtonOneByOne.Checked)
                {
                    //Set flag value
                    _oneByOneActivedByUser = true;
                }
            }
        }

        /// <summary>
        /// On one by one check button changed.
        /// </summary>
        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(checkButton1_CheckedChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetOneByOneTestMode(checkButtonOneByOne.Checked);
            }
        }

        /// <summary>
        /// Handle checkEditKeepOneByOne EditValueChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditKeepOneByOne_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ChangingEventHandler(checkEditKeepOneByOne_EditValueChanging), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //Check if the control was focused to make sure user explicitly clicked it
                //otherwise the setting value in datbase will be updated during binding process
                if (checkEditKeepOneByOne.Focused)
                {
                    var updateSetting = false;

                    //If the checbox was unchecked, show a notification to confirm user actually wants it active
                    if (!((bool)e.OldValue))
                    {
                        if (UiHelperClass.ShowConfirmQuestion(StaticKeys.KeepOneByOneConfirm) == DialogResult.Yes)
                        {
                            updateSetting = true;
                        }
                        else
                        {
                            //If user cancelled then do not do action
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        updateSetting = true;
                    }

                    //This check is to make sure setting value is only updated by user and when confirmed
                    if (updateSetting)
                    {
                        UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.KeepOneByOneModeOn, (bool)e.NewValue, _settingsManager);
                    }   
                }
            }
        }

        /// <summary>
        /// Handle find button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonFind_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonFind_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                Find();
            }
        }

        #endregion

        #region GridView Events

        /// <summary>
        /// Handles the double click on the grid items.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewItemsNav_DoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewItemsNav_DoubleClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!_isEditable || !UiHelperClass.IsClickInRow(gridViewItems)) return;

                _lastDoubleClickedRowHandler = gridViewItems.FocusedRowHandle;
                ViewDetails();
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
                if (_isCurrentReadingOn || _isOneByOneTest) return;

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
        /// Handles the selection changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewItemsNav_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new SelectionChangedEventHandler(gridViewItemsNav_SelectionChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!IsWaitingReadingResponse)
                    return;

                ProcessSelectedRowsChanged();
            }
        }

        /// <summary>
        /// When gridview is filtered the split button gets disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItemsNav_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewItemsNav_ColumnFilterChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SetEditMode(!_isEditable);
            }
        }

        /// <summary>
        /// Handle the HasChilds Icon Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == gridColumnHasChilds)
            {
                var currentRow = e.Row as Item;

                e.Value = currentRow != null && ProductSeatsHelper.ItemHasChilds(currentRow, true);
            }
        }

        #endregion

        #region GridControl Events

        /// <summary>
        /// Handle Keydown events on the items gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlItemsNav_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(gridControlItemsNav_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isEditable)
                {
                    if (e.Alt && e.KeyCode == Keys.Enter)
                    {
                        ViewDetails();
                    }

                    if (e.Alt && e.KeyCode == Keys.Back)
                    {
                        if (BackClicks > 0)
                            BackToParentItems();
                    }

                    if (e.Alt && e.KeyCode == Keys.S)
                    {
                        SwitchSelection();
                    }
                }
            }
        }

        /// <summary>
        /// Handle the datasource changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlItemsNav_DataSourceChanged(object sender, EventArgs e)
        {

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridControlItemsNav_DataSourceChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                HandleDatasourceChange();
            }
        }

        /// <summary>
        /// Handel the focusedRowChanged on the gridViewItemsNav.
        /// </summary>
        private void gridViewItemsNav_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FocusedRowChangedEventHandler(gridViewItemsNav_FocusedRowChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _nextRowToSelectBaseOnUserSelection = gridViewItems.SelectedRowsCount > 0 ? e.FocusedRowHandle : -1;

                if (e.FocusedRowHandle < 0)
                {
                    gridViewItems.PaintAppearance.FocusedRow.BackColor = Color.White;
                    gridViewItems.PaintAppearance.FocusedRow.BackColor2 = Color.White;
                }
                else
                {
                    gridViewItems.PaintAppearance.FocusedRow.BackColor = Color.Orange;
                    gridViewItems.PaintAppearance.FocusedRow.BackColor2 = Color.FloralWhite;
                }
            }
        }

        /// <summary>
        /// Handle the Focus on the grid.
        /// </summary>
        private void gridViewItems_GotFocus(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
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
                ProcessSelectedRowsChanged();
            }
        }
        
        #endregion

        #region General Events
       
        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        public void NavGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(NavGrid_KeyDown), sender, e);
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
                    if (simpleButtonSwitch.Enabled)
                    {
                        SwitchSelection();
                    }
                }
                else if (e.KeyCode == Keys.F7)
                {
                    if (simpleButtonReloadItems.Enabled)
                    {
                        RefreshItems(false, IsStepAutomated);
                    }
                }
                else if (e.KeyCode == Keys.F5)
                {
                    if (simpleButtonSplit.Enabled)
                    {
                        SplitItems();
                    }
                }
                else if (e.KeyCode == Keys.F4)
                {
                    gridViewItems.SelectAll();
                }
                else if (e.KeyCode == Keys.Insert || (e.Control && e.KeyCode == Keys.I) || e.KeyCode == Keys.Oemtilde)
                {
                    if (simpleButtonAddToTestResults.Enabled)
                    {
                        AddTestResult();
                    }
                }
                else if (e.KeyCode == Keys.F9)
                {
                    if (simpleButtonBack.Enabled)
                    {
                        BackToParentItems();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (textEditFind.EditorContainsFocus)
                    {
                        Find();
                    }
                    else if (simpleButtonViewDetails.Enabled)
                    {
                        ViewDetails();
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (simpleButtonClearUserSelection.Enabled)
                    {
                        ClearSelection();
                    }
                }
                else if (e.KeyCode == Keys.Home)
                {
                    if (!textEditFind.EditorContainsFocus)
                    {
                        gridViewItems.Focus();
                        gridViewItems.MoveFirst();
                        gridViewItems.MakeRowVisible(gridViewItems.FocusedRowHandle);    
                    }
                }
                else if (e.KeyCode == Keys.End)
                {
                    if (!textEditFind.EditorContainsFocus)
                    {
                        gridViewItems.Focus();
                        gridViewItems.MoveLastVisible();
                    }
                }
                else if (e.KeyCode == Keys.F12 && checkButtonOneByOne.Enabled)
                {
                    if (!_isOneByOneTest)
                    {
                        //Set the flag value to indicate explicit activation of toggle
                        _oneByOneActivedByUser = true;
                    }
                    SetOneByOneTestMode(!_isOneByOneTest);
                }
                else if (e.KeyCode == Keys.F11 && checkButtonHideShowItems.Enabled)
                {
                    checkButtonHideShowItems.Checked = !checkButtonHideShowItems.Checked;
                    ToggleHideShowItems();
                }
                else if (e.Control && e.KeyCode == Keys.F)
                {
                    if (_itemSearchEnabled)
                    {
                        textEditFind.Focus();
                    }   
                }
            }
        }

        /// <summary>
        /// Handle find text field value changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditFind_EditValueChanging(object sender, ChangingEventArgs e)
        {
            SetFindButtonEnableState(e.NewValue == null? string.Empty:e.NewValue.ToString());
        }

        /// <summary>
        /// Handles clear find logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClearFind_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonClearFind_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ClearFindCriteria(true);
            }
        }

        #endregion

        #endregion        
    }
}
