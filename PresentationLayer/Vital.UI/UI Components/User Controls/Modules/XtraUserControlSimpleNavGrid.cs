using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.Forms.DataManagementForms;
using Vital.UI.UI_Components.UI_Classes;
using Vital.Business.Shared.Caching;

namespace Vital.UI.UI_Components.User_Controls.Modules
{

    public partial class XtraUserControlSimpleNavGrid : XtraUserControl
    {
        #region Private Variables

        private readonly ItemsManager _itemsManager;
        private readonly LookupsManager _lookupsManager;
        private readonly AppInfoManager _appInfoManager;
        
        private readonly Lookup  _lookupMySubstanceList;
        private readonly Lookup _genderLookup;
        private readonly Lookup _typeLookup;
        private readonly Lookup _productTypeLookup;
        private readonly Lookup _userListTypeLookup;
        private readonly Lookup _noneListTypeLookup;
        private readonly Lookup _relationTypeLookup;
        private readonly Lookup _targetTypeLookup;
        private readonly Lookup _userNewItemLookup;
        private readonly Lookup _productsTargetTypeLookup;
        
        private DevExpress.XtraGrid.Columns.GridColumn _gridColumnIcon;

        #endregion

        #region Events & Delegates

        /// <summary>
        /// Handles the add to test result.
        /// </summary>
        public event OnAddItems AddItemRelationToGroups;
        
        /// <summary>
        /// Handles the item list changed event.
        /// </summary>
        public event OnListChanged ItemsListChanged;

        /// <summary>
        /// 
        /// </summary>
        public event OnDeleteItem DeleteItemFromUi;

        /// <summary>
        /// Delegate for AddToTestResults handler method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="items">Items To add.</param>
        public delegate void OnAddItems(XtraUserControlSimpleNavGrid sender, BindingList<Item> items);

        /// <summary>
        /// Delegate for the item list changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="formStatus">The form status.</param>
        public delegate void OnListChanged(XtraUserControlSimpleNavGrid sender, FormStatusEnum formStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="item">The item.</param>
        public delegate void OnDeleteItem(XtraUserControlSimpleNavGrid sender, Item item);

        #endregion

        #region Invokers

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="item">The item</param>
        public void OnDeleteItemFromUi(Item item)
        {
            if (AddItemRelationToGroups == null)
                return;
            
            DeleteItemFromUi(this, item);
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void OnAddItemRelationToGroups(BindingList<Item> items)
        {
            if (AddItemRelationToGroups == null)
                return;
            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);
            
            SaveChanges(ActionType.AddToTestResults);

            foreach (var item in items)
            {
                if(MovedItems.All(c => c.Id != item.Id))
                    MovedItems.Add(item);
            }

            AddItemRelationToGroups(this, items);

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Event that fires when the items of the data source change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="formstatus">The form status.</param>
        public void OnItemsListChanged(XtraUserControlSimpleNavGrid sender, FormStatusEnum formstatus)
        {
            if (ItemsListChanged == null)
                return;

            //_isItemListChanged = true;
            ItemsListChanged(this, formstatus);
        }

        #endregion

        #region The Constructor

        /// <summary>
        /// The constructor.
        /// </summary>
        public XtraUserControlSimpleNavGrid()
        {
            InitializeComponent();
            CreateIconColumn();
            RegisterDisabledControlsForTooltips();

            var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (designMode) return;

            _itemsManager = new ItemsManager();
            _lookupsManager = new LookupsManager();
            _appInfoManager = new AppInfoManager();
            IsFirstFetch = true;
            StartingItems = new BindingList<Item>();
            NavigationSteps = new BindingList<TestResult>();
            DeletedItems = new List<Item>();
            MovedItems = new List<Item>();

            _lookupMySubstanceList = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.TargetType,  TargetType.MySubstancesAdditionsList)).FirstOrDefault();
            _genderLookup = UiHelperClass.GetSingleLookupFromCacheByKey(ItemGender.ItemGenderBoth.ToString());
            _typeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Substance)).FirstOrDefault();
            _productTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Product)).FirstOrDefault();
            _noneListTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.None)).FirstOrDefault();
            _userListTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.UserList)).FirstOrDefault();
            _relationTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.RelationType, RelationTypeEnum.None)).FirstOrDefault();
            _targetTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.TargetType, TargetType.MySubstancesAdditionsList)).FirstOrDefault();
            _productsTargetTypeLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.TargetType, TargetType.MyProductsList)).FirstOrDefault();
            _userNewItemLookup = _lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.SourceType, SourceTypeEnum.UserNewItem)).FirstOrDefault();
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the default tooltip controller.
        /// </summary>
        public ToolTipController DefaultTooltipController
        {
            get { return ToolTipController.DefaultController; }
        }

        public List<DisabledControl> DisabledControls
        {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the starting items.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<Item> StartingItems
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the navigation steps.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<TestResult> NavigationSteps
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the deleted items.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Item> DeletedItems
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the moved item from the navigation grid to the relations grid.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Item> MovedItems
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data source items.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<Item> DatasourceItems
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the is read only flag.
        /// </summary>
        public bool IsReadOnly
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the is first fetch flag.
        /// </summary>
        public bool IsFirstFetch
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current navigation type.
        /// </summary>
        public NavigationGridType CurrentNavigationType
        {
            get; set;
        }

        /// <summary>
        /// Gets the current grid view.
        /// </summary>
        public GridView ItemsGridView
        {
            get { return gridViewItems; }
        }

        /// <summary>
        /// Gets or sets the current target type.
        /// </summary>
        public TargetType CurrentTargetType
        {
            get; set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the source of the grid.
        /// </summary>
        public void InitGridItems(BindingList<Item> items)
        {
            if (IsFirstFetch)
            {
                StartingItems = items;
                NavigationSteps.Clear();
                SetCurrentNodeLabel();
                IsFirstFetch = false;
            }

            CurrentNavigationType = CurrentTargetType == TargetType.MySubstancesAdditionsList || CurrentTargetType == TargetType.MyProductsList ?
                                    NavigationGridType.Editable :
                                    NavigationGridType.ReadOnly;

            //setting the grid properties based on the navigation mode
            EnableDisableGridEditing(CurrentNavigationType);

            SetItems(items);
        }

        /// <summary>
        /// Gets the current navigation step.
        /// </summary>
        /// <returns></returns>
        public TestResult GetCurrentNavigationStep()
        {
            return NavigationSteps.FirstOrDefault(c => c.IsCurrent);
        }

        /// <summary>
        /// Performs the needed actions of the edit mode.
        /// </summary>
        /// <param name="isReadOnly">The read only flag.</param>        
        public void SetEditMode(bool isReadOnly)
        {
            IsReadOnly = isReadOnly;
            simpleButtonViewDetails.Enabled = CheckIfDrillingDownAllowed();
            simpleButtonBack.Enabled = CheckIfBackAllowed();
            
            if(CurrentTargetType == TargetType.MyProductsList)
            {
                layoutControlItemAddProduct.Visibility = LayoutVisibility.Always;
                layoutControlItemAddProductGroup.Visibility = LayoutVisibility.Always;
                //layoutControlItemProductsCapacityInfo.Visibility = LayoutVisibility.Always;
                simpleButtonAddNewProduct.Enabled = CheckIfProductAdditionAllowed();
                simpleButtonAddNewProductGroup.Enabled = CheckIfGroupAdditionAllowed();
                _gridColumnIcon.Visible = true;
            }
            else
            {
                layoutControlItemAddProduct.Visibility = LayoutVisibility.Never;
                layoutControlItemAddProductGroup.Visibility = LayoutVisibility.Never;
                //layoutControlItemProductsCapacityInfo.Visibility = LayoutVisibility.Never;
                _gridColumnIcon.Visible = false;
            }
            

            simpleButtonMoveItems.Enabled = !isReadOnly && gridViewItems.DataRowCount > 0 && gridViewItems.FocusedRowHandle > -1;
            memoEditItemDescription.Properties.ReadOnly = isReadOnly || GetFocusedItem() == null;

            //enabling or disabling the grid based on the edit mode.
            EnableDisableGridEditing(!isReadOnly && CurrentNavigationType == NavigationGridType.Editable ? 
                                    NavigationGridType.Editable : 
                                    NavigationGridType.ReadOnly);
        }

        /// <summary>
        /// Sets the item of the grid
        /// </summary>
        public void SetItems(BindingList<Item> items)
        {
            DatasourceItems = items;

            ClearHandlers(items);
            SetHandlers(items);

            gridControlItems.DataSource = items;
            ItemsGridView.RefreshData();
            ItemsGridView.FocusedRowHandle = 0;
            ItemsGridView.MakeRowVisible(0);
            SetCurrentNodeLabel();
            
            SetEditMode(IsReadOnly);

        }

        /// <summary>
        /// Views the children of the parent item.
        /// </summary>
        public void ViewDetails()
        {
            if (IsValid())
            {
                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.NotValidItemError);
                return;
            }

            var item = (Item)ItemsGridView.GetRow(ItemsGridView.FocusedRowHandle);

            if (item == null)
                return;

            //check if the double click means opening the product form or drilling down.
            if (CurrentTargetType == TargetType.MyProductsList 
                //to check if the item type is product.
                && item.TypeLookup != null && item.TypeLookup.Id == _productTypeLookup.Id
                //to check if its a product and not a group of products.
                && item.ListTypeLookup != null && item.ListTypeLookup.Id == _noneListTypeLookup.Id)
            {
                OpenProduct(item);
                return;
            }

            if (!CheckIfDrillingDownAllowed())
                return;

            ViewDetails(item);
        }

        /// <summary>
        /// View Details for specific item.
        /// </summary>
        /// <param name="item">The item.</param>
        private void ViewDetails(Item item)
        {
            if (item == null) return;

            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingItems);

            SaveChanges(ActionType.DrillDown);

            ClearNavGridViewFilters();

            var items = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = item.Id }, CurrentTargetType == TargetType.MyProductsList);

            if (NavigationSteps.Count > 0)
            {
                var parent = GetCurrentNavigationStep();

                if (parent != null)
                    parent.IsCurrent = false;

                var testResult = new TestResult
                {
                    Item = item,
                    Parent = parent,
                    IsCurrent = true,
                };

                NavigationSteps.Add(testResult);
                ItemsGridView.RefreshData();
            }
            else
            {
                var testResult = new TestResult
                {
                    Item = item,
                    Parent = null,
                    IsCurrent = true,
                };

                NavigationSteps.Add(testResult);
            }

            SetItems(items);
            SetCurrentNodeLabel();
            gridViewItems.Focus();
            memoEditItemDescription.Text = string.Empty;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Back to the parent items list.
        /// </summary>
        public void BackToParentItems()
        {
            if(!CheckIfBackAllowed()) return;

            if (IsValid())
            {
                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.NotValidItemError);
                return;
            }

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            SaveChanges(ActionType.Back);

            ClearNavGridViewFilters();

            if (NavigationSteps != null && NavigationSteps.Count > 0)
            {
                //gets the current test result (step)
                var currentStep = GetCurrentNavigationStep();

                //gets the parent of the current test result (the parent of the new step).
                var parent = currentStep == null ? null : currentStep.Parent;

                if (currentStep != null && parent != null)
                {
                    var parentItems = new BindingList<Item>();

                    //indicates that this test result is an item step (not a protocol step).
                    if (parent.Item != null)
                    {
                        //gets the children of the item.
                        parentItems = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = parent.Item.Id }, CurrentTargetType == TargetType.MyProductsList);

                        //sets the node label with the item name.
                        SetCurrentNodeLabel();
                    }

                    //assign the new items to the grid.
                    BindingList<Item> newGridDataSource = parentItems;

                    //sets the parent as a current test result (since its one step back).
                    parent.IsCurrent = true;

                    currentStep.IsCurrent = false;

                    SetItems(newGridDataSource);
                }

                //initializing or no back stages remaining
                else
                {
                    ResetStartingItems();
                }
            }

            ItemsGridView.RefreshData();

            ItemsGridView.Focus();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Gets the selected items.
        /// </summary>
        /// <returns>The items as binding List.</returns>
        public BindingList<Item> GetSelectedItems()
        {
            var selectedItems = new BindingList<Item>();

            if (ItemsGridView.SelectedRowsCount > 0)
            {
                var selectedRowsIndex = ItemsGridView.GetSelectedRows();

                foreach (var item in selectedRowsIndex.Select(t => ItemsGridView.GetRow(t)).OfType<Item>())
                {
                    selectedItems.Add(item);
                }
            }

            return selectedItems;
        }

        /// <summary>
        /// Reset the grid item to the Starting Items.
        /// </summary>
        public void ResetStartingItems()
        {
            if (StartingItems == null)
                return;

            NavigationSteps.Clear();

            if (CurrentNavigationType == NavigationGridType.ReadOnly)
                SetItems(StartingItems);
            else
                ReloadStartingItems();
        }

        /// <summary>
        /// Validates the grid items.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return GetGridViewRows().Any(c => string.IsNullOrEmpty(c.Name));
        }

        /// <summary>
        /// Validates the grid items.
        /// </summary>
        /// <returns></returns>
        public bool IsValid(BindingList<Item> items)
        {
            return items.Any(c => string.IsNullOrEmpty(c.Name));
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Creates a column in the grid for showing icon for group and product
        /// </summary>
        private void CreateIconColumn()
        {
            //THIS IS CREATED IN CODE SINCE THE UNBOUND CODE HANDLING DOESN'T WORK FOR SOME REASON WHEN THIS IS CREATED IN DESGINER
            _gridColumnIcon = new DevExpress.XtraGrid.Columns.GridColumn();
            gridViewItems.Columns.Add(_gridColumnIcon);
            _gridColumnIcon.ColumnEdit = repositoryItemCheckEditIcon;
            _gridColumnIcon.Name = "gridColumnIcon";
            _gridColumnIcon.Caption = " ";
            _gridColumnIcon.OptionsColumn.AllowEdit = false;
            _gridColumnIcon.OptionsColumn.AllowFocus = false;
            _gridColumnIcon.OptionsColumn.AllowIncrementalSearch = false;
            _gridColumnIcon.OptionsColumn.AllowShowHide = false;
            _gridColumnIcon.OptionsColumn.ReadOnly = true;
            _gridColumnIcon.OptionsFilter.AllowFilter = false;
            _gridColumnIcon.OptionsFilter.AllowAutoFilter = false;
            _gridColumnIcon.OptionsFilter.AllowFilterModeChanging = DefaultBoolean.False;

            _gridColumnIcon.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            _gridColumnIcon.Visible = true;
            _gridColumnIcon.VisibleIndex = 0;
            gridColumnHasChilds.VisibleIndex = 1;
            gridColumnItem.VisibleIndex = 2;
            _gridColumnIcon.Width = 20;
        }

        /// <summary>
        /// Sets the handlers.
        /// </summary>
        /// <param name="items">The items.</param>
        private void SetHandlers(BindingList<Item> items)
        {
            items.ListChanged += ItemsOnListChanged;
        }

        /// <summary>
        /// Clears the handlers.
        /// </summary>
        /// <param name="items">The items.</param>
        private void ClearHandlers(BindingList<Item> items)
        {
            items.ListChanged -= ItemsOnListChanged;
        }

        /// <summary>
        /// Enables or disables the editing on the grid.
        /// </summary>
        private void EnableDisableGridEditing(NavigationGridType navigationGridType)
        {
            var editable = navigationGridType == NavigationGridType.Editable;

            ItemsGridView.OptionsView.NewItemRowPosition = editable && CurrentTargetType != TargetType.MyProductsList ? NewItemRowPosition.Top : NewItemRowPosition.None;
            ItemsGridView.OptionsBehavior.AllowAddRows = editable ? DefaultBoolean.True : DefaultBoolean.False;
            ItemsGridView.OptionsBehavior.AllowDeleteRows = editable ? DefaultBoolean.True : DefaultBoolean.False;
            gridColumnItem.OptionsColumn.AllowEdit = editable;
            gridColumnItem.OptionsColumn.ReadOnly = !editable;
            memoEditItemDescription.Enabled = editable;
        }

        /// <summary>
        /// Gets the focused row.
        /// </summary>
        /// <returns></returns>
        private Item GetFocusedItem()
        {
            return ItemsGridView.GetRow(ItemsGridView.GetSelectedRows().FirstOrDefault()) as Item;
        }

        /// <summary>
        /// Checks if the selected item has Childs.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfCurrentItemHasChilds()
        {
            var item = GetFocusedItem();

            return item != null && ProductSeatsHelper.ItemHasChilds(item, true);
        }

        /// <summary>
        /// Checks if the drill down is allowed.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfDrillingDownAllowed()
        {
            var item = GetFocusedItem();

            return     !IsReadOnly 
                    && ItemsGridView.SelectedRowsCount == 1
                    && ((CheckIfCurrentItemHasChilds()
                    //check if the navigation grid mode is MyAdditions mode (editable).(always can drill down.)
                    || CurrentTargetType == TargetType.MySubstancesAdditionsList && CurrentNavigationType == NavigationGridType.Editable)
                    //check if the navigation grid mode is products and the focused item is a group of products not a single product.
                    || CurrentTargetType == TargetType.MyProductsList && item.ListTypeLookup != null && item.ListTypeLookup.Id == _userListTypeLookup.Id);
        }

        /// <summary>
        /// Checks if the back action is allowed.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfBackAllowed()
        {
            var currentNavigationStep = GetCurrentNavigationStep();

            return !IsReadOnly && currentNavigationStep != null;
        }

        /// <summary>
        /// Checks if the group addition is allowed.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfGroupAdditionAllowed()
        {
            return !IsReadOnly && CurrentTargetType == TargetType.MyProductsList;
        }

        /// <summary>
        /// Checks if the product additions is allowed.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfProductAdditionAllowed()
        {
            var currentNavigationStep = GetCurrentNavigationStep();

            return !IsReadOnly && currentNavigationStep != null && CurrentTargetType == TargetType.MyProductsList;
        }

        /// <summary>
        /// Reloads the starting items, this function is used only when the navigation type is editable.
        /// </summary>
        private void ReloadStartingItems()
        {            
            if (_lookupMySubstanceList == null) return;

            var items = new BindingList<Item>(_itemsManager.GetItems(new ItemsFilter 
                                                {  TargetTypeLookupId = CurrentTargetType != TargetType.MyProductsList ? 
                                                    _lookupMySubstanceList.Id :
                                                    _productsTargetTypeLookup.Id ,
                                                   LoadHiddenItems = CurrentTargetType == TargetType.MyProductsList
                                                })).OrderBy(c => c.Name).ToBindingList();

            StartingItems = items;

            SetItems(items);
        }

        /// <summary>
        /// Saves the new items based on the action type.
        /// </summary>
        /// <param name="actionType">The action type.</param>
        public void SaveChanges(ActionType actionType)
        {
            if (actionType == ActionType.AddToTestResults)
            {
                var selectedItems = GetSelectedItems().Where(c => c.ObjectState == DomainEntityState.New);

                foreach (var newItem in selectedItems)
                {
                    SaveItem(newItem);
                }
            }
            else if (actionType == ActionType.Back || actionType == ActionType.DrillDown || actionType == ActionType.FullSave)
            {
                var itemsToBeSaved = GetGridViewRows().Where(c => c.ObjectState == DomainEntityState.Modified || c.ObjectState == DomainEntityState.New);

                var toBeSaved = itemsToBeSaved as List<Item> ?? itemsToBeSaved.ToList();

                foreach (var item in toBeSaved)
                {
                    SaveItem(item);
                }

                int deletedProductCount = 0;

                foreach (var deletedItem in DeletedItems)
                {
                    var deleteResult = _itemsManager.DeleteItem(deletedItem);

                    if (CurrentTargetType == TargetType.MyProductsList
                        && deletedItem.ListTypeLookup != null
                        && deletedItem.ListTypeLookup.Id == _noneListTypeLookup.Id
                        && deleteResult.IsSucceed)
                        deletedProductCount++;

                    RemoveDeletedItemFromMovedItems(deletedItem);
                }

                if (!toBeSaved.Any() && DeletedItems.Count == 0)
                    OnItemsListChanged(this, FormStatusEnum.Unchanged);
            }

            DeletedItems.Clear();
        }

        /// <summary>
        /// Gets the grid items.
        /// </summary>
        /// <returns></returns>
        private BindingList<Item> GetGridViewRows()
        {
            return (BindingList<Item>)ItemsGridView.DataSource;
        }

        /// <summary>
        /// Checks if the current items are saved or not.
        /// </summary>
        /// <returns></returns>
        public bool CheckIfCurrentViewNotSaved()
        {
            return GetGridViewRows().Any(c => c.ObjectState == DomainEntityState.New || c.ObjectState == DomainEntityState.Modified) || DeletedItems.Count > 0;
        }

        /// <summary>
        /// Refreshes the items in the grid.
        /// </summary>
        public void RefreshItems()
        {
            var currentNavigationStep = GetCurrentNavigationStep();

            if (currentNavigationStep == null)
            {
                DeletedItems.Clear();
                ResetStartingItems();
                return;
            }

            var items = _itemsManager.GetItemChildren(new SingleItemFilter { ItemId = currentNavigationStep.Item.Id }, CurrentTargetType == TargetType.MyProductsList);

            SetItems(items);
        }

        /// <summary>
        /// Saves the new item.
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns></returns>
        private void SaveItem(Item item)
        {
            var currentNavigationStep = GetCurrentNavigationStep();
            var newItemRelation = new ItemRelation();

            if(item.ObjectState == DomainEntityState.New)
            {
                item.GenderLookup = _genderLookup;
                item.TypeLookup = CurrentTargetType == TargetType.MyProductsList ? _productTypeLookup : _typeLookup ;
                item.ListTypeLookup = _userListTypeLookup;
                item.ItemSourceLookup = _userNewItemLookup;

                if (currentNavigationStep != null)
                {
                    var relationTypeLookup = _relationTypeLookup;

                    newItemRelation.RelationType = relationTypeLookup;
                    newItemRelation.Parent = currentNavigationStep.Item;
                    newItemRelation.Child = item;

                    item.Children = new BindingList<ItemRelation>
                                   {
                                       newItemRelation
                                   };
                }
                else
                {
                    var itemTarget = new ItemTarget
                                         {
                                             Item = item,
                                             TargetTypeLookup = CurrentTargetType == TargetType.MyProductsList ? 
                                                                _productsTargetTypeLookup : _targetTypeLookup
                                         };

                    item.ItemTargets = new BindingList<ItemTarget> { itemTarget };
                }
            }
            else
            {
                //some items has the gender lookup as null which will cause an exception in the database, so as a default value it will be set to both.
                item.GenderLookup = item.GenderLookup ?? _genderLookup;
            }

            _itemsManager.SaveItem(item);
            
        }

        /// <summary>
        /// Clear any filters in the NavGrid.
        /// </summary>
        private void ClearNavGridViewFilters()
        {
            ItemsGridView.ClearColumnsFilter();
        }

        /// <summary>
        /// Sets the current node label with the parent name.
        /// </summary>
        private void SetCurrentNodeLabel()
        {
            var currentNode = GetCurrentNavigationStep();

            labelControlCurrentNode.Text = currentNode != null ? currentNode.Item.Name : StaticKeys.Root;
        }

        /// <summary>
        /// Deletes the item if it satisfies the deletion conditions.
        /// </summary>
        /// <param name="focusedRowHandle">The focused row handle.</param>
        /// <returns></returns>
        private bool DeleteItem(int focusedRowHandle)
        {
            SaveChanges(ActionType.FullSave);
            var currentRow = ItemsGridView.GetRow(focusedRowHandle) as Item;

            if (currentRow == null) return false;


            //case: new item.
            if (currentRow.ObjectState == DomainEntityState.New)
            {
                ItemsGridView.DeleteRow(ItemsGridView.FocusedRowHandle);
                return true;
            }

            //case: there is an unsaved relation related to the deleted item and this item has no children.
            if (!CheckCanDeleteOnUi(currentRow) && !ProductSeatsHelper.ItemHasChilds(currentRow, true))
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ItemsHasRelatedDataDeletionConfirmationQuestion) == DialogResult.Yes)
                {
                    //remove the item relations (unsaved relations ) from the user groups.
                    OnDeleteItemFromUi(currentRow); 
                    //delete the current row from the current data source and mark the item as deleted.
                    DeleteItem(currentRow);
                    //save the changes which will delete the item from the database [it can be called for a specific delete event.]
                    SaveChanges(ActionType.FullSave);
                    //remove the item from the moved items list.
                    RemoveDeletedItemFromMovedItems(currentRow);
                }

                return true;
            }

            //check if the item can be deleted.
            var canDeleteItem = _itemsManager.CanDeleteItem(currentRow);
            var hasRelatedData = _itemsManager.ItemHasRelations(currentRow);

            //case: if the item has no important (cannot be deleted) relations and has children.
            if (!canDeleteItem.IsSucceed || ProductSeatsHelper.ItemHasChilds(currentRow, true))
            {
                UiHelperClass.ShowInformation(StaticKeys.CannotDeleteItem, StaticKeys.CannotDeleteItemTitle);

                return false;
            }
            
            //case: if the item is included as child in some relations but it can be deleted.
            if (!hasRelatedData.IsSucceed)
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ItemsHasRelatedDataDeletionConfirmationQuestion) == DialogResult.Yes)
                {
                    return DeleteItem(currentRow);
                }

                return false;
            }

            //case: if it has no children and none of the cases above applies.
            if (!ProductSeatsHelper.ItemHasChilds(currentRow, true))
            {
                DeleteItem(currentRow);
            }

            return false;
        }

        /// <summary>
        /// Removes the deleted item from the moved list.
        /// </summary>
        /// <param name="currentRow">The current row.</param>
        private void RemoveDeletedItemFromMovedItems(Item currentRow)
        {
            var item = MovedItems.FirstOrDefault(c => c.Id == currentRow.Id);

            if (item != null)
                MovedItems.Remove(item);
        }

        /// <summary>
        /// Checks if the item is related to any UI entry (unsaved entry).
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private bool CheckCanDeleteOnUi(Item item)
        {
            return MovedItems.All(c => c.Id != item.Id);
        }

        private void ClearDescription()
        {
            memoEditItemDescription.DataBindings.Clear();
            memoEditItemDescription.Text = string.Empty;
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="currentRow">The current item.</param>
        /// <returns></returns>
        private bool DeleteItem(Item currentRow)
        {
            try
            {
                currentRow.ObjectState = DomainEntityState.Deleted;
                DeletedItems.Add(currentRow);
                ItemsGridView.DeleteRow(ItemsGridView.FocusedRowHandle);

                SaveChanges(ActionType.FullSave);

                OnItemsListChanged(this, FormStatusEnum.Unchanged);
                ProductSeatsHelper.LoadUserProducts();
                CachingManager.RemoveAllItemsFromCache();
                ClearDescription();
                return true;
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, exception);
                return false;
            }
            
        }

        /// <summary>
        /// Opens the product dialog.
        /// </summary>
        /// <param name="item">The item.</param>
        private void OpenProduct(Item item)
        {
            SaveChanges(ActionType.FullSave);

            /*TODO:[Oday] Check why the gender lookup is null when its loaded from the database.
              remove the line below after fixing this to-do.*/
            if (item.GenderLookup == null)
            {
                item.GenderLookup = _genderLookup;
            }

            var itemManagementForm = new frmItemManagement(item);
            itemManagementForm.ShowDialog();
            OnItemsListChanged(this, FormStatusEnum.Unchanged);

            RefreshItems();

            CachingManager.RemoveAllItemsFromCache();
            ClearDescription();
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        private void AddNewProduct()
        {
            SaveChanges(ActionType.FullSave);

            var newProduct = new Item
            {
                Name = StaticKeys.NewProductName,
                ListTypeLookup = _noneListTypeLookup, // the list type for a single product is none.
                GenderLookup = _genderLookup,
                TypeLookup = _productTypeLookup,
                ItemSourceLookup = _userNewItemLookup
            };

            var itemRelation = new ItemRelation()
            {
                Parent = GetCurrentNavigationStep().Item,
                Child = newProduct,
                RelationType = _relationTypeLookup
            };

            newProduct.Children = new BindingList<ItemRelation> 
                                    {
                                        itemRelation
                                    };

            DatasourceItems.Add(newProduct);

            var itemManagement = new frmItemManagement(newProduct);
            
            itemManagement.ShowDialog();
            ProductSeatsHelper.LoadUserProducts();
            OnItemsListChanged(this, FormStatusEnum.Unchanged);

            RefreshItems();

            CachingManager.RemoveAllItemsFromCache();
        }

        /// <summary>
        /// Adds a new product group.
        /// </summary>
        private void AddNewProductGroup()
        {
            var newProductsGroup = new Item
            {
                Name = StaticKeys.NewProductGroup,
                
                /*we should initialize the list type here since its an important condition for
                  the drilling down action (even before saving it). */
                ListTypeLookup = _userListTypeLookup //the list type of a product group is user list
            };

            DatasourceItems.Add(newProductsGroup);

            OnItemsListChanged(this,FormStatusEnum.Modified);
        }

        #region Tooltips

        /// <summary>
        /// Registers the disabled controls to show their tooltips.
        /// </summary>
        private void RegisterDisabledControlsForTooltips()
        {
            //registering the controls.
            DisabledControls = new List<DisabledControl>
                                   {
                                       new DisabledControl()
                                           {
                                               Control = simpleButtonAddNewProduct,
                                               ToolTipType = ToolTipType.SuperTip
                                           }
                                   };

            //configuring the tooltip controller.
            DefaultTooltipController.UseWindowStyle = true;
            DefaultTooltipController.AutoPopDelay = 1000;
            DefaultTooltipController.ReshowDelay = 500;
        }

        /// <summary>
        /// Hides the tooltip.
        /// </summary>
        private void HideTooltip()
        {
            DefaultTooltipController.HideHint();
        }

        /// <summary>
        /// Shows the tooltip on the disabled control.
        /// </summary>
        /// <param name="disabledControl">The disabled control.</param>
        private void ShowTooltip(DisabledControl disabledControl)
        {
            Check.Argument.IsNotNull(disabledControl, "disabledControl");

            var currentControl = disabledControl.Control as SimpleButton;

            if (currentControl == null) return;

            var toolTipShowArguments = DefaultTooltipController.CreateShowArgs();

            toolTipShowArguments.SelectedControl = currentControl;

            toolTipShowArguments.ToolTipType = disabledControl.ToolTipType;

            if (disabledControl.ToolTipType == ToolTipType.SuperTip)
                toolTipShowArguments.SuperTip = currentControl.SuperTip;
            else
                toolTipShowArguments.ToolTip = currentControl.ToolTip;

            DefaultTooltipController.ShowHint(toolTipShowArguments);
        }

        /// <summary>
        /// Finds the disabled control.
        /// </summary>
        /// <param name="point">The point location.</param>
        private void FindToolTipControl(Point point)
        {
            foreach (var disabledControl in DisabledControls)
            {
                if (disabledControl.Control.Visible && disabledControl.Control.Bounds.X < point.X && disabledControl.Control.Bounds.Y < point.Y)
                {
                    ShowTooltip(disabledControl);
                }
                else
                {
                    HideTooltip();
                }
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the click on the back button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments,</param>
        private void simpleButtonBack_Click(object sender, EventArgs e)
        {
            BackToParentItems();
        }

        /// <summary>
        /// Handles the click on the view details button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments,</param>
        private void simpleButtonViewDetails_Click(object sender, EventArgs e)
        {
            ViewDetails();
        }

        /// <summary>
        /// Handles the click on the add relations button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonMoveItems_Click(object sender, EventArgs e)
        {
            if (IsValid(GetSelectedItems()))
            {
                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.NotValidItemError);
                return;
            }

            OnAddItemRelationToGroups(GetSelectedItems());
        }
        
        /// <summary>
        /// Handles the double click on one of the items.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments,</param>
        private void gridViewItems_DoubleClick(object sender, EventArgs e)
        {
            if(UiHelperClass.IsClickInRowByMouse(ItemsGridView))
                ViewDetails();
        }

        /// <summary>
        /// Handles the selection changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewItems_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var item = (Item)ItemsGridView.GetRow(ItemsGridView.FocusedRowHandle);

            if(item != null)
                UiHelperClass.BindControl(memoEditItemDescription, item, () => item.Description);
            
            SetEditMode(IsReadOnly);
        }

        /// <summary>
        /// Handles the click on the menu strip item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuStripItems_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sender != null)
            {
                ((ContextMenuStrip)sender).Hide();

                if (sender == contextMenuStripItems)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteItem)
                    {
                        DeleteItem(ItemsGridView.FocusedRowHandle);
                    }
                    else if (e.ClickedItem == toolStripMenuItemEdit)
                    {
                        var item = (Item)ItemsGridView.GetRow(ItemsGridView.FocusedRowHandle);

                        if (item != null)
                        {
                            OpenProduct(item);    
                        }                        
                    }
                }
            }
        }

        /// <summary>
        /// Handles the opening of the context menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuStripItems_Opening(object sender, CancelEventArgs e)
        {
            if (sender == contextMenuStripItems)
            {
                e.Cancel = UiHelperClass.CancelClickAction(ItemsGridView);

                var isEnabled = CurrentNavigationType == NavigationGridType.Editable && UiHelperClass.IsClickInRowByMouse(ItemsGridView) && !IsReadOnly;

                toolStripMenuItemDeleteItem.Enabled = isEnabled;

                var isProduct = false;
                var item = (Item)ItemsGridView.GetRow(ItemsGridView.FocusedRowHandle);
                //check if the double click means opening the product form or drilling down.
                if (item != null && CurrentTargetType == TargetType.MyProductsList
                    //to check if the item type is product.
                    && item.TypeLookup != null && item.TypeLookup.Id == _productTypeLookup.Id
                    //to check if its a product and not a group of products.
                    && item.ListTypeLookup != null && item.ListTypeLookup.Id == _noneListTypeLookup.Id)
                {
                    isProduct = true;
                }

                toolStripMenuItemEdit.Enabled = isEnabled && isProduct;
            }
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridControlItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReadOnly)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ViewDetails();
                }

                if (e.KeyCode == Keys.F9)
                {
                    BackToParentItems();
                }
            }
        }

        /// <summary>
        /// Handles the change on the list.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="listChangedEventArgs">The event arguments.</param>
        private void ItemsOnListChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            if (listChangedEventArgs.ListChangedType == ListChangedType.ItemAdded)
                return; 

            OnItemsListChanged(this, FormStatusEnum.Modified);
        }

        /// <summary>
        /// Handles the click on adding new product group button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonAddNewProductGroup_Click(object sender, EventArgs e)
        {
            AddNewProductGroup();
        }
        
        /// <summary>
        /// Handles the click on adding new product button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void simpleButtonAddNewProduct_Click(object sender, EventArgs e)
        {
            AddNewProduct();
        }

        /// <summary>
        /// Handle the data for the icon unbound column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewItems_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column == _gridColumnIcon && e.IsGetData)
            {
                var item = (Item)e.Row;
                e.Value = item.ListTypeLookup != null && item.ListTypeLookup.Id == _userListTypeLookup.Id;
            }
            else if (e.Column == gridColumnHasChilds)
            {
                var currentRow = e.Row as Item;

                e.Value = currentRow != null && ProductSeatsHelper.ItemHasChilds(currentRow, false);
            }
        }

        /// <summary>
        /// Handle the hiding of the icon in the filter row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewItems_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 && e.Column == _gridColumnIcon)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the mouse move event on a specific container.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void layoutControlMain_MouseMove(object sender, MouseEventArgs e)
        {
            FindToolTipControl(e.Location);
        }

        #endregion
    }
}
