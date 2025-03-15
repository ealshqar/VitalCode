using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;
using Vital.UI.UI_Components.Forms.DataManagementForms;
using Vital.UI.UI_Components.User_Controls.Modules;

namespace Vital.UI.UI_Components.Forms.DataManagement
{
    public partial class frmDataManagement : VitalBaseForm
    {
        #region Fields

        private TestProtocolsManager _testProtocolsManager;
        private LookupsManager _lookupsManager;
        private ItemsManager _itemsManager;
        private AppInfoManager _appInfoManager;
        private PropertiesManager _propertiesManager;

        private DataManagementTypes _dataManagementTypes;
        private TargetType _dataManagementTarget;
        private TargetType _dataManagementSource;
        private ItemTypeEnum _itemType;

        private BindingList<TestProtocol> _testProtocols;
        private BindingList<Item> _userGroups;
        private BindingList<Item> _deletedUserGroups;
        private BindingList<ItemRelation> _deletedUserGroupItemRelations;

        private Lookup _userListType;
        private Lookup _itemTypeLookup;
        private Lookup _genderLookup;
        private Lookup _targetType;

        private Item _currentUserGroup;
        
        private bool _isChanged;
        private bool _refreshProductsTab;
        private int _productsFocusedRowHandle;
        private int _productsTopRowIndex;

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public frmDataManagement()
        {
            InitializeComponent();
            CustomeInitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected user group.
        /// </summary>
        private Item FocusedUserGroupItem
        {
            get
            {
                return gridViewUserGroups.GetFocusedRow() as Item;
            }
        }

        #endregion

        #region Initialization & Binding

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        private void CustomeInitializeComponent()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingCustomComponents);

            imageListItemState.Images.Add("Active", Vital.UI.Properties.Resources.CircleBalanced);
            imageListItemState.Images.Add("Hidden", Vital.UI.Properties.Resources.CircleClear);

            repositoryItemImageComboBoxItemState.Items.AddRange(new ImageComboBoxItem[] {
            new ImageComboBoxItem("Active", true, 0),
            new ImageComboBoxItem("Hidden", false, 1)});
        }

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public override void PerformSpecificIntializationSteps()
        {
            _dataManagementTypes = DataManagementTypes.Points;
            _dataManagementTarget = TargetType.MyPointsList;
            _dataManagementSource = TargetType.SystemPointsList;
            _itemType = ItemTypeEnum.Point;

            _testProtocolsManager = new TestProtocolsManager();
            _lookupsManager = new LookupsManager();
            _itemsManager = new ItemsManager();
            _appInfoManager = new AppInfoManager();
            _propertiesManager = new PropertiesManager();

            _userGroups = new BindingList<Item>();
            _deletedUserGroups = new BindingList<Item>();
            _deletedUserGroupItemRelations = new BindingList<ItemRelation>();

            ProductSeatsHelper.InitializeFields();

            UpdateTypeVariables();

            SetFormStatus(false);
            
            UiHelperClass.SetTabControlProperties(xtraTabControlDataManagment);
            UiHelperClass.SetViewProperties(gridViewProtocols);

            var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (designMode) return;
            
            FillLookUps();

            UpdateNavigationGridItems();
        }

        /// <summary>
        /// Fill the form Lookups.
        /// </summary>
        public override void FillLookUps()
        {
            var listTypeLookups = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.UserList));

            if (listTypeLookups != null)
            {
                _userListType = listTypeLookups;
            }

            var itemGenderLookups = UiHelperClass.GetSingleLookupFromCacheByKey(ItemGender.ItemGenderBoth.ToString());

            if (itemGenderLookups != null)
            {
                _genderLookup = itemGenderLookups;
            }
        }

        /// <summary>
        /// Updates the grid with the correct items.
        /// </summary>
        private void UpdateNavigationGridItems()
        {
            UiHelperClass.ShowWaitingPanel("Loading Items ...");
            var lookupMySubstanceList = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, _dataManagementSource));

            if(lookupMySubstanceList == null)
                return;

            var items = new BindingList<Item>(_itemsManager.GetItems(new ItemsFilter { TargetTypeLookupId = lookupMySubstanceList.Id })).OrderBy(c => c.Name).ToBindingList();

            _userControlSimpleNavGrid.IsFirstFetch = true;
            _userControlSimpleNavGrid.CurrentTargetType = _dataManagementSource;
            _userControlSimpleNavGrid.InitGridItems(items);

            if (_refreshProductsTab)
            {
                _refreshProductsTab = false;
            }
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Set the gridControlProtocols datasource.
        /// </summary>
        public void BindTestProtocols()
        {
            _testProtocols = _testProtocolsManager.GetTestProtocols(new TestProtocolsFilter { LoadingType = LoadingTypeEnum.None });

            gridControlProtocols.DataSource = _testProtocols;
        }

        /// <summary>
        /// Set the gridControlProducts datasource.
        /// </summary>
        public void BindProducts()
        {
            UiHelperClass.ShowWaitingPanel("Loading Products ...");

            gridControlProducts.DataSource = ProductSeatsHelper.LoadUserProducts();
            gridViewProducts.FocusedRowHandle = _productsFocusedRowHandle;
            gridViewProducts.TopRowIndex = _productsTopRowIndex;
            UpdateProductsStats();
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Update the counts of seats and other stats
        /// </summary>
        private void UpdateProductsStats()
        {
            simpleLabelItemSeatsNumber.Text = ProductSeatsHelper.SeatsNumber.ToString("D");
            simpleLabelItemRemainingSeatsCount.Text = ProductSeatsHelper.RemainingSeatsCount.ToString("D");
            simpleLabelItemProductsTotalNumber.Text = ProductSeatsHelper.ProductsCount.ToString("D");
            simpleLabelItemActiveProductsNumber.Text = ProductSeatsHelper.ActiveProductsCount.ToString("D");
            simpleLabelItemHiddenProductsNumber.Text = ProductSeatsHelper.HiddenProductsCount.ToString("D");
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            BindTestProtocols();
            BindProducts();
            
            _isChanged = false;

            UpdateGroupsGridType();
        }
        
        /// <summary>
        /// Sets the edit mode of the controls
        /// </summary>
        /// <param name="isReadOnly"></param>
        public override void SetEditMode(bool isReadOnly)
        {
            gridViewUserGroups.OptionsBehavior.ReadOnly = isReadOnly;
            gridViewUserGroupItemRelations.OptionsBehavior.ReadOnly = isReadOnly;
            btnMoveUp.Enabled = !isReadOnly;
            btnMoveDown.Enabled = !isReadOnly;

            _userControlSimpleNavGrid.SetEditMode(isReadOnly);
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            gridControlProtocols.DataBindings.Clear();
            gridControlProtocols.DataSource = null;
            gridControlProtocols.Refresh();
            dxErrorProviderMain.ClearErrors();
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            _userGroups.ListChanged += userGroup_ListChanged;
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {            
            _userGroups.ListChanged -= userGroup_ListChanged;
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = _userGroups;
            dxErrorProviderMain.UpdateBinding();            
        }

        /// <summary>
        /// Sets Ui groups for user group data Enabled property.
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetUserGroupsDataGroupsStatus(bool isEnabled)
        {
            layoutControlGroupItems.Enabled = layoutControlGroupUserGroupItems.Enabled = isEnabled;
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
                GridViewPostValues(gridViewUserGroups);
                GridViewPostValues(_userControlSimpleNavGrid.ItemsGridView);

                if (AreGroupsValid() && !_userControlSimpleNavGrid.IsValid())
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);                   

                    var deleteItemRelationsResult = _deletedUserGroupItemRelations.Count == 0 ? ProcessResult.Succeed : _itemsManager.DeleteItemRelations(_deletedUserGroupItemRelations);

                    var resultDelete = _deletedUserGroups.Count == 0 ? ProcessResult.Succeed : _itemsManager.Save(_deletedUserGroups);

                    var resultSave = _itemsManager.Save(_userGroups);

                    _userControlSimpleNavGrid.SaveChanges(ActionType.FullSave);
                    _userControlSimpleNavGrid.MovedItems.Clear();

                    var result = resultSave.IsSucceed && resultDelete.IsSucceed && deleteItemRelationsResult.IsSucceed;

                    if (result)
                    {
                        ClearDeletedLists();
                        _isChanged = false;
                        CachingManager.RemoveFromCache(CachableDataEnum.ItemsGroup.ToString());
                        CachingManager.RemoveFromCache(CachableDataEnum.Products.ToString());
                        CachingManager.RemoveFromCache(CachableDataEnum.CustomDilutions.ToString());
                        CachingManager.RemoveFromCache(CachableDataEnum.VitalForce.ToString());
                    }

                    UiHelperClass.HideSplash();

                    return result;
                }

                UiHelperClass.ShowError(StaticKeys.ValidationMessageTitle, StaticKeys.ValidationMessageGeneral);
                return false;
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }

        /// <summary>
        /// Clear the deleted object list, This method should be called after save and undo actions.
        /// </summary>
        private void ClearDeletedLists()
        {
            _deletedUserGroups.Clear();    
            _deletedUserGroupItemRelations.Clear();
            _userControlSimpleNavGrid.DeletedItems.Clear();
        }

        /// <summary>
        /// Delete a user group
        /// </summary>
        /// <param name="rowHandler">The row handler</param>
        /// <returns></returns>
        public bool DeleteUserGroup(int rowHandler) 
        {
            var currentRow = gridViewUserGroups.GetRow(rowHandler) as Item;

            if (currentRow == null) return false;

            //case: new item.
            if (currentRow.ObjectState == DomainEntityState.New)
            {
                gridViewUserGroups.DeleteRow(gridViewUserGroups.FocusedRowHandle);
                return true;
            }

            //check if the item can be deleted.
            var canDeleteItem = _itemsManager.CanDeleteItem(currentRow);           

            //case: if the item has no important (cannot be deleted) relations and has children.
            if (!canDeleteItem.IsSucceed || ProductSeatsHelper.ItemHasChilds(currentRow, false))
            {
                UiHelperClass.ShowInformation(StaticKeys.CannotDeleteItem, StaticKeys.CannotDeleteItemTitle);

                return false;
            }

            currentRow.ObjectState = DomainEntityState.Deleted;
            _deletedUserGroups.Add(currentRow);
            gridViewUserGroups.DeleteRow(gridViewUserGroups.FocusedRowHandle);

            return true;
        }

        /// <summary>
        /// Delete a item relation.
        /// </summary>
        /// <param name="rowHandler">The row handler</param>
        /// <returns></returns>
        public bool DeleteItemRelation(int rowHandler)
        {
            var currentRow = gridViewUserGroupItemRelations.GetRow(rowHandler) as ItemRelation;

            if (currentRow == null) 
                return false;

            currentRow.ObjectState = DomainEntityState.Deleted;

            _deletedUserGroupItemRelations.Add(currentRow);

            gridViewUserGroupItemRelations.DeleteRow(gridViewUserGroupItemRelations.FocusedRowHandle);

            //Reset order of relations
            ResetRelationsOrder();

            return true;
        }

        /// <summary>
        /// Actions After save.
        /// </summary>
        public override void AfterSaveAction()
        {
            _isChanged = false;
            _userControlSimpleNavGrid.RefreshItems();

            Rebind();
        }

        /// <summary>
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        public override bool HasChanges()
        {
            return _isChanged;
        }
       
        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            ClearHandlers();
            ClearBinding();

            SetProperties();
            SetBinding();
            ClearDeletedLists();
            SetupMainErrorProvider();
            UpdateErrorProvider();
            SetupHandllers();
            _userControlSimpleNavGrid.RefreshItems();

            return true;
        }

        #endregion

        #region Logic

        #region Protocols

        /// <summary>
        /// Open the Test Protocol form.
        /// </summary>
        /// <param name="isNew">Is new Test Protocol.</param>
        private void OpenProtocol(bool isNew)
        {          
            try
            {
                UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

                var currentProtocol = isNew ? new TestProtocol() : GetFocusedTestProtocol();
                var testFrm = new frmTestProtocolManagement(currentProtocol);

                UiHelperClass.HideSplash();

                testFrm.ShowDialog();

                Rebind();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Delete the Test Protocol.
        /// </summary>        
        private void DeleteProtocol()
        {
            try
            {
                if (!CanDelete()) return;

                var currentProtocol = GetFocusedTestProtocol();

                if (currentProtocol == null) return;

                UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

                var canDeleteTestProtocol = _testProtocolsManager.CanDeleteTestProtocol(currentProtocol);

                if (!canDeleteTestProtocol.IsSucceed)
                {
                    UiHelperClass.ShowInformation(canDeleteTestProtocol.Message, StaticKeys.ProtocolInUseMessageTitle);

                    UiHelperClass.HideSplash();

                    return;
                }

                _testProtocolsManager.DeleteTestProtocol(currentProtocol);

                UiHelperClass.HideSplash();

                Rebind();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }

        }

        /// <summary>
        /// Get focused test.
        /// </summary>
        /// <returns></returns>
        private TestProtocol GetFocusedTestProtocol()
        {
            if (gridViewProtocols.GetFocusedRow() != null && _testProtocolsManager != null)
            {
                return _testProtocolsManager.GetTestProtocolById(new SingleItemFilter { ItemId = ((TestProtocol)gridViewProtocols.GetFocusedRow()).Id });
            }

            return null;
        }

        #endregion

        #region DataManagment

        /// <summary>
        /// Updates the item type lookup
        /// </summary>
        private void UpdateItemTypeLookup()
        {
            var itemTypeLookups = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ItemType, _itemType));

            if (itemTypeLookups != null)
            {
                _itemTypeLookup = itemTypeLookups;
            }
        }

        /// <summary>
        /// Updates the target type lookup
        /// </summary>
        private void UpdateTargetTypeLookup()
        {
            var targetTypeLookups = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, _dataManagementTarget));

            if (targetTypeLookups != null)
            {
                _targetType = targetTypeLookups;
            }
        }

        /// <summary>
        /// Update the current type and target for the data management
        /// </summary>
        private void UpdateDataManagementTypes()
        {
            var type = (DataManagementTypes)Enum.Parse(typeof(DataManagementTypes), radioGroupTypes.EditValue.ToString());

            if (Enum.IsDefined(typeof(DataManagementTypes), type))
            {
                _dataManagementTypes = type;
            }

            UpdateTypeVariables();
            UpdateGroupsGridType();
        }

        /// <summary>
        /// Update the enums of type and variables and groups text
        /// </summary>
        private void UpdateTypeVariables()
        {
            SetUserGroupName(string.Format("My {0} Groups", _dataManagementTypes));
            SetSystemItemsGroupName(string.Format("System {0}", _dataManagementTypes));

            switch (_dataManagementTypes)
            {
                case DataManagementTypes.Points:
                    _dataManagementTarget = TargetType.MyPointsList;
                    _dataManagementSource = TargetType.SystemPointsList;
                    _itemType = ItemTypeEnum.Point;
                    break;
                case DataManagementTypes.Substances:
                    _dataManagementTarget = TargetType.MySubstancesList;
                    _dataManagementSource = TargetType.SystemSubstancesList;
                    _itemType = ItemTypeEnum.Substance;
                    break;
                case DataManagementTypes.Potencies:
                    _dataManagementTarget = TargetType.MyPotenciesList;
                    _dataManagementSource = TargetType.SystemPotenciesList;
                    _itemType = ItemTypeEnum.Potency;
                    break;
                case DataManagementTypes.MyAdditions:
                    _dataManagementTarget = TargetType.MySubstancesList;
                    _dataManagementSource = TargetType.MySubstancesAdditionsList;
                    _itemType = ItemTypeEnum.Substance;
                    SetUserGroupName(string.Format("My {0}",DataManagementTypes.Substances));
                    SetSystemItemsGroupName("My Additions");
                    break;
                case DataManagementTypes.Products:
                    _dataManagementTarget = TargetType.MySubstancesList;
                    _dataManagementSource = TargetType.MyProductsList;
                    _itemType = ItemTypeEnum.Product;
                    SetUserGroupName(string.Format("My {0}", DataManagementTypes.Substances));
                    SetSystemItemsGroupName("My Products");
                    break;
            }
        }

        /// <summary>
        /// Sets the text for the system items group
        /// </summary>
        /// <param name="groupName"></param>
        private void SetSystemItemsGroupName(string groupName)
        {
            layoutControlGroupItems.Text = groupName;
        }

        /// <summary>
        /// Sets the name for the user gorup
        /// </summary>
        /// <param name="groupName"></param>
        private void SetUserGroupName(string groupName)
        {
            layoutControlGroupMyGroups.Text = groupName;
        }

        /// <summary>
        /// Sets the name of the group layout control based on current selected group
        /// </summary>
        /// <param name="_groupName"></param>
        private void SetGroupItemsGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                groupName = " ";

            layoutControlGroupUserGroupItems.Text = groupName;            
        }

        /// <summary>
        /// Sets the datasource for the groups grid
        /// </summary>
        private void UpdateGroupsGridType()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingData );

            UpdateItemTypeLookup();
            UpdateTargetTypeLookup();
            ClearHandlers();
            
            _userGroups = new BindingList<Item>();
            
            var targetTypeLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TargetType, _dataManagementTarget));

            if (targetTypeLookup != null)
            {
                _userGroups = _itemsManager.GetItems(new ItemsFilter
                {
                    TargetTypeLookupId = targetTypeLookup.Id,
                    IncludeHiddenChilds = false
                });

                //Ordering the itemRelations of the groups depends on both of Order and Name, 
                //If there is an Order in the list the relation items will be ordering by Order value at first then at relation item Name.
                //If there is no Order, the relation items will order depends on Name only, because all the orders will be 0.
                _userGroups.ToList().ForEach(ug => ug.Parents = ug.Parents.OrderBy(p => p.Order).ThenBy(p => p.Child.Name).ToBindingList());

            }

            _userGroups.RaiseListChangedEvents = true;
            SetupHandllers();

            gridControlUserGroups.DataSource = _userGroups;
            gridControlUserGroupItemRelations.DataBindings.Clear();

            UiHelperClass.BindControl(gridControlUserGroupItemRelations, _userGroups, () => new Item().Parents);
            
            SetupMainErrorProvider();
            _currentUserGroup = gridViewUserGroups.GetFocusedRow() as Item ?? _userGroups.FirstOrDefault();
            SetGroupItemsGroupName(_currentUserGroup.Name);
            
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Validate the groups.
        /// </summary>
        /// <returns></returns>
        private bool AreGroupsValid()
        {
            foreach (var group in _userGroups)
            {
                if (!group.Validate())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Reset order for relations
        /// </summary>
        public void ResetRelationsOrder()
        {
            var itemRelations = gridViewUserGroupItemRelations.DataSource as BindingList<ItemRelation>;

            if (itemRelations == null)
                return;

            var currentOrder = 1;

            foreach (var relation in itemRelations.OrderBy(r=>r.Order))
            {
                relation.Order = currentOrder;
                currentOrder += 1;
            }
        }

        /// <summary>
        /// Swap the order of item relations
        /// </summary>
        /// <param name="focusedRowHandle">Focused row handle.</param>
        /// <param name="isMovingUp">Step 2 index.</param>
        private void SwapRelations(int focusedRowHandle, bool isMovingUp)
        {
            var firstHandle = focusedRowHandle;
            var firstIndex = gridViewUserGroupItemRelations.GetDataSourceRowIndex(firstHandle);

            if (_currentUserGroup == null)
                return;

            var view = gridViewUserGroupItemRelations;

            if (!gridViewUserGroupItemRelations.IsDataRow(focusedRowHandle) ||
                view.IsNewItemRow(firstHandle) ||
                (isMovingUp && view.IsFirstRow) ||
                (!isMovingUp && view.IsLastRow)) return;

            view.GridControl.Focus();
            int secondRowHandle = isMovingUp ? firstHandle - 1 : firstHandle + 1;

            while (!view.IsDataRow(secondRowHandle))
            {
                secondRowHandle = isMovingUp ? firstHandle - 1 : firstHandle + 1;
            }

            int secondIndex = gridViewUserGroupItemRelations.GetDataSourceRowIndex(secondRowHandle);

            var tmpOrder = _currentUserGroup.Parents[firstIndex].Order;

            _currentUserGroup.Parents[firstIndex].Order = _currentUserGroup.Parents[secondIndex].Order;

            _currentUserGroup.Parents[secondIndex].Order = tmpOrder;

            var tmp = _currentUserGroup.Parents[firstIndex];

            _currentUserGroup.Parents[firstIndex] = _currentUserGroup.Parents[secondIndex];

            _currentUserGroup.Parents[secondIndex] = tmp;

            gridViewUserGroupItemRelations.FocusedRowHandle = isMovingUp ? firstHandle - 1 : firstHandle + 1;
        }

        /// <summary>
        /// Adds the item relations to the groups.
        /// </summary>
        /// <param name="items">The items.</param>
        private void AddItemRelationToGroups(BindingList<Item> items)
        {
            var selectedGroup = FocusedUserGroupItem;

            if (selectedGroup == null) return;

            var noneItemRelationLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.RelationType, RelationTypeEnum.None));

            //Get order of last relation and increase it for each added item
            var lastRelation = selectedGroup.Parents.OrderByDescending(r => r.Order).FirstOrDefault();
            var newOrder = lastRelation == null ? 1 : lastRelation.Order + 1;

            foreach (var item in items)
            {
                var itemRelation = new ItemRelation()
                {
                    Child = item,
                    Parent = selectedGroup,
                    RelationType = noneItemRelationLookup,
                    Order = newOrder
                };

                if (selectedGroup.Parents.All(c => c.Child.Id != item.Id))
                {
                    selectedGroup.Parents.Add(itemRelation);
                    //Increase order only if the item was added to list and wasn't existing already.
                    newOrder += 1;
                }
            }
        }

        /// <summary>
        /// Gets the current data source of the user groups grid.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Item> GetUserGroups()
        {
            return (BindingList<Item>)gridControlUserGroups.DataSource;
        }

        #endregion

        #region Products

        /// <summary>
        /// Saves the current row handle and top row index
        /// </summary>
        private void SetProductsScrollPosition()
        {
            _productsFocusedRowHandle = gridViewProducts.FocusedRowHandle;
            _productsTopRowIndex = gridViewProducts.TopRowIndex;
        }

        /// <summary>
        /// Opens the product dialog.
        /// </summary>
        private void OpenProduct()
        {
            SetProductsScrollPosition();

            var currentProduct = gridViewProducts.GetFocusedRow() as Item;
            
            if (currentProduct == null) return;;

            if (currentProduct.GenderLookup == null)
            {
                currentProduct.GenderLookup = _genderLookup;
            }

            var itemManagementForm = new frmItemManagement(currentProduct);
            itemManagementForm.ShowDialog();
            
            BindProducts();
            CachingManager.RemoveAllItemsFromCache();
        }

        /// <summary>
        /// Deletes the item if it satisfies the deletion conditions.
        /// </summary>
        /// <returns></returns>
        private void DeleteProduct()
        {
            SetProductsScrollPosition();

            var currentProduct = gridViewProducts.GetFocusedRow() as Item;

            if (currentProduct == null) return;

            var canDeleteItem = _itemsManager.CanDeleteItem(currentProduct);
            
            //case: there is an unsaved relation related to the deleted item and this item has no children.
            if (!ProductSeatsHelper.ItemHasChilds(currentProduct, false) && canDeleteItem.IsSucceed)
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ItemsHasRelatedDataDeletionConfirmationQuestion) == DialogResult.Yes)
                {
                    currentProduct.ObjectState = DomainEntityState.Deleted;
                    _itemsManager.DeleteItem(currentProduct);
                    gridViewProducts.DeleteRow(gridViewProducts.FocusedRowHandle);
                    BindProducts();
                    CachingManager.RemoveAllItemsFromCache();
                }
            }
            else
            {
                UiHelperClass.ShowInformation(StaticKeys.CannotDeleteItem, StaticKeys.CannotDeleteItemTitle);
            }
        }

        /// <summary>
        /// Toggles the Item State property
        /// </summary>
        private void SetActiveHiddenProperty()
        {
            SetProductsScrollPosition();

            var currentProduct = gridViewProducts.GetFocusedRow() as Item;

            if (currentProduct == null) return;

            var isActive = ProductSeatsHelper.IsItemActive(currentProduct);

            var hasItemStateProperty = currentProduct.Properties.HasProperty(PropertiesEnum.ItemState);
            var property = _propertiesManager.GetPropertyByKey(PropertiesEnum.ItemState);

            var itemStateValue = isActive ? ProductSeatsHelper.UserHiddenItemStateLookup.Id : ProductSeatsHelper.ActiveItemStateLookup.Id;

            if (hasItemStateProperty)
            {
                var itemProperty = currentProduct.Properties.FirstOrDefault(ip => ip.Property != null && ip.Property.Id == property.Id);

                if (itemProperty != null)
                {
                    itemProperty.Value = itemStateValue;
                }
                _itemsManager.SaveItemProperty(itemProperty);
            }
            else
            {
                var itemProperty = new ItemProperty
                {
                    Item = currentProduct,
                    Property = property,
                    Value = itemStateValue
                };
                _itemsManager.SaveItemProperty(itemProperty);
                currentProduct.Properties.Add(itemProperty);
            }
            BindProducts();
            CachingManager.RemoveAllItemsFromCache();
        }
        #endregion

        #endregion

        #region Handlers

        #region General Handlers

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            var currentProduct = gridViewProducts.GetFocusedRow() as Item;

            if (sender == contextMenuStripProducts)
            {
                e.Cancel = UiHelperClass.CancelClickAction(gridViewProducts);
                
                if (currentProduct != null)
                {
                    var isActive = ProductSeatsHelper.IsItemActive(currentProduct);
                    toolStripMenuItemActivateHide.Text = isActive ? "Hide" : "Activate";
                    toolStripMenuItemActivateHide.Image = isActive ? Resources.CircleClear : Resources.CircleBalanced;
                }
            }

            if (IsInEditMode)
            {
                if (sender == contextMenuStripProtocols)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewProtocols);

                    var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewProtocols);

                    toolStripMenuItemOpen.Enabled = isEnabled && gridViewProtocols.GetFocusedRow() != null;
                    toolStripMenuItemDelete.Enabled = isEnabled && gridViewProtocols.GetFocusedRow() != null;
                    toolStripMenuItemNew.Enabled = true;
                }
                else if (sender == contextMenuStripUserGroups)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewUserGroups);

                    var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewUserGroups);

                    toolStripMenuItemDeleteUserGroup.Enabled = isEnabled && gridViewUserGroups.GetFocusedRow() != null;
                }
                else if (sender == contextMenuStripUserGroupItemRelations)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewUserGroupItemRelations);

                    var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewUserGroupItemRelations) && IsInEditMode;

                    toolStripMenuItemDeleteItemRelation.Enabled = isEnabled;
                }
                else if (sender == contextMenuStripProducts)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewProducts);

                    var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewProducts) && IsInEditMode;

                    toolStripMenuItemOpenProduct.Enabled = isEnabled && gridViewProducts.GetFocusedRow() != null;
                    toolStripMenuItemDeleteProduct.Enabled = isEnabled && gridViewProducts.GetFocusedRow() != null;

                    var allowActivate = false;

                    if (currentProduct != null && isEnabled)
                    {
                        var isActive = ProductSeatsHelper.IsItemActive(currentProduct);
                        allowActivate = isActive || ProductSeatsHelper.AllowActiveOption;
                    }

                    toolStripMenuItemActivateHide.Enabled = isEnabled && allowActivate;
                }

                toolStripMenuItemNewItem.Enabled = true;
            }
            else
            {
                toolStripMenuItemOpenItem.Enabled = false;
                toolStripMenuItemDeleteItem.Enabled = false;
                toolStripMenuItemNewItem.Enabled = false;
                toolStripMenuItemOpen.Enabled = false;
                toolStripMenuItemDelete.Enabled = false;
                toolStripMenuItemNew.Enabled = false;
                toolStripMenuItemDeleteUserGroup.Enabled = false;
                toolStripMenuItemDeleteItemRelation.Enabled = false;
                toolStripMenuItemOpenProduct.Enabled = false;
                toolStripMenuItemDeleteProduct.Enabled = false;
                toolStripMenuItemActivateHide.Enabled = false;
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sender != null)
            {
                ((ContextMenuStrip)sender).Hide();

                if (sender == contextMenuStripProtocols)
                {
                    if (e.ClickedItem == toolStripMenuItemNew)
                    {
                        OpenProtocol(true);
                    }
                    else if (e.ClickedItem == toolStripMenuItemOpen)
                    {
                        OpenProtocol(false);
                    }
                    else if (e.ClickedItem == toolStripMenuItemDelete)
                    {
                        DeleteProtocol();
                    }
                }
                else if (sender == contextMenuStripUserGroups)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteUserGroup)
                    {
                        DeleteUserGroup(gridViewUserGroups.FocusedRowHandle);
                    }
                }
                else if (sender == contextMenuStripUserGroupItemRelations)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteItemRelation)
                    {
                        DeleteItemRelation(gridViewUserGroupItemRelations.FocusedRowHandle);
                    }
                }
                else if (sender == contextMenuStripProducts)
                {
                    if (e.ClickedItem == toolStripMenuItemOpenProduct)
                    {
                        var currentProduct = gridViewProducts.GetFocusedRow() as Item;
                        if (currentProduct != null)
                        {
                            OpenProduct();    
                        }
                    }
                    else if (e.ClickedItem == toolStripMenuItemDeleteProduct)
                    {
                        DeleteProduct();
                    }
                    else if (e.ClickedItem == toolStripMenuItemActivateHide)
                    {
                        SetActiveHiddenProperty();
                    }

                    _refreshProductsTab = true;
                }
            }
        }

        #endregion

        #region Proptocols Handlers

        /// <summary>
        /// Handles the doube click event on the Protocols grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewProtocolsDoubleClick(object sender, EventArgs e)
        {
            if (gridViewProtocols.GetFocusedRow() != null && UiHelperClass.IsClickInRow(gridViewProtocols))
            {
                OpenProtocol(false);
            }
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridControlProtocols_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gridViewProtocols.GetFocusedRow() != null && UiHelperClass.IsClickInRow(gridViewProtocols))
                {
                    OpenProtocol(false);
                }
            }
        }

        #endregion

        #region DataManagment Handlers

        /// <summary>
        /// Handles the changing in the index of the radio button group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var saveResult = true;

            if (_isChanged || _userControlSimpleNavGrid.CheckIfCurrentViewNotSaved())
            {
                //if (UiHelperClass.ShowConfirmQuestion(StaticKeys.SaveBeforChangeSelectedGroupType) == DialogResult.Yes)
                //{
                    
                //}
                //else
                //{
                //    _isChanged = false;
                //    //No need for revert changes, Changed user groups will be discarded.
                //    FormStatus = FormStatusEnum.Unchanged;
                //}
                //Save user changes automatically
                saveResult = SaveAction();
                UpdateNavigationGridItems();
            }

            if (saveResult)
            {
                UpdateDataManagementTypes();
                UpdateNavigationGridItems();
            }
            else
            {
                radioGroupTypes.SelectedIndexChanged -= radioGroupTypes_SelectedIndexChanged;
                radioGroupTypes.EditValue = radioGroupTypes.OldEditValue;
                radioGroupTypes.SelectedIndexChanged += radioGroupTypes_SelectedIndexChanged;
            }
        }

        /// <summary>
        /// handles changes in the user groups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewUserGroups_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //This line makes sure the Save buttons becomes enabled instantly when user edits a cell since devexpress
            //grid by default posts values to the underlying datasoure when leaving cell and changing focus.
            if (FormStatus != FormStatusEnum.Modified && UiHelperClass.IsClickInRowByMouse(gridViewUserGroups))
            {
                FormStatus = FormStatusEnum.Modified;
                _isChanged = true;
            }
            if (UiHelperClass.IsClickInRowByMouse(gridViewUserGroups))
            {
                SetGroupItemsGroupName(((Item)gridViewUserGroups.GetRow(e.RowHandle)).Name);
            }            
        }

        /// <summary>
        /// handles form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDataManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CanClose();
        }

        /// <summary>
        /// Handles initialization of new user groups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewUserGroups_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            var currentUserGroup = (Item)gridViewUserGroups.GetRow(e.RowHandle);
            currentUserGroup.ListTypeLookup = _userListType;
            currentUserGroup.TypeLookup = _itemTypeLookup;
            currentUserGroup.GenderLookup = _genderLookup;
            currentUserGroup.ObjectState = DomainEntityState.New;
            currentUserGroup.Parents = new BindingList<ItemRelation>();
            currentUserGroup.Children = new BindingList<ItemRelation>();
            currentUserGroup.Properties = new BindingList<ItemProperty>();
            currentUserGroup.ItemTargets = new BindingList<ItemTarget>();

            var target = new ItemTarget { TargetTypeLookup = _targetType, Item = currentUserGroup };
            currentUserGroup.ItemTargets.Add(target);
            SetUserGroupsDataGroupsStatus(true);
            SetGroupItemsGroupName(((Item)gridViewUserGroups.GetRow(e.RowHandle)).Name);
        }

        /// <summary>
        /// Handles list changes in the user groups
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void userGroup_ListChanged(object sender, ListChangedEventArgs e)
        {
            FormStatus = FormStatusEnum.Modified;
            _isChanged = true;
        }

        /// <summary>
        /// Handel moving step up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            SwapRelations(gridViewUserGroupItemRelations.FocusedRowHandle, true);
        }

        /// <summary>
        /// Handel the Moving step down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            SwapRelations(gridViewUserGroupItemRelations.FocusedRowHandle, false);
        }

        /// <summary>
        /// Handle the changing on the cell to set the form save mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewUserGroupItemRelations_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //This line makes sure the Save buttons becomes enabled instantly when user edits a cell since devexpress
            //grid by default posts values to the underlying datasoure when leaving cell and changing focus.
            if (FormStatus != FormStatusEnum.Modified && gridViewUserGroupItemRelations.CalcHitInfo(gridControlUserGroupItemRelations.PointToClient(Cursor.Position)).InRow && gridViewUserGroupItemRelations.FocusedRowHandle >= 0)
            {
                FormStatus = FormStatusEnum.Modified;
            }
        }

        /// <summary>
        /// Adds the item relations to the needed groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="items"></param>
        private void xtraUserControlSimpleNavGridItems_AddItemRelationToGroups(XtraUserControlSimpleNavGrid sender, BindingList<Item> items)
        {
            AddItemRelationToGroups(items);
        }

        /// <summary>
        /// Handles the change on the items list.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="formStatus">The event arguments.</param>
        private void xtraUserControlSimpleNavGridItems_ItemsListChanged(XtraUserControlSimpleNavGrid sender, FormStatusEnum formStatus)
        {
            if (formStatus == FormStatusEnum.Modified)
            {
                FormStatus = formStatus;
                _isChanged = true;
            }
                //case : the background saving is done on the items grid and no changes on the other two grids.
            else
            {
                FormStatus = _isChanged == false ? formStatus : FormStatusEnum.Modified;
            }
        }

        /// <summary>
        /// Handel the focused group changed to handle the focus for the new row.
        /// </summary>
        private void gridViewUserGroups_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridViewUserGroups.IsNewItemRow(e.FocusedRowHandle) || e.FocusedRowHandle < 0)
            {
                SetUserGroupsDataGroupsStatus(false);
                SetGroupItemsGroupName(string.Empty);
            }
            else
            {
                _currentUserGroup = gridViewUserGroups.GetRow(e.FocusedRowHandle) as Item ?? _currentUserGroup;

                SetUserGroupsDataGroupsStatus(true);

                SetGroupItemsGroupName(_currentUserGroup.Name);
            }
            
        }

        /// <summary>
        /// Handles the deletion of the item from the un saved lists. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="item">The item.</param>
        private void _userControlSimpleNavGrid_DeleteItemFromUi(XtraUserControlSimpleNavGrid sender, Item item)
        {
            //gets all the user groups.
            var userGroups = GetUserGroups();

            var changedUserGroups = new BindingList<Item>();

            //gets the changed user groups.
            foreach (var group in userGroups.Where(group => group.Parents.Any(c => c.ObjectState == DomainEntityState.New)))
            {
                changedUserGroups.Add(group);
            }

            //checking to remove the item from the user group.
            foreach (var group in changedUserGroups)
            {
                foreach (var child in group.Parents)
                {
                    var parents = group.Parents;

                    if (child.Child.Id != item.Id || child.ObjectState != DomainEntityState.New) continue;

                    parents.Remove(child);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the custom bound type column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewProducts_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == gridColumnIsActive)
            {
                var currentRow = e.Row as Item;

                e.Value = currentRow != null && ProductSeatsHelper.IsItemActive(currentRow);
            }
            else if (e.Column == gridColumnItemActiveIcon)
            {
                var currentRow = e.Row as Item;

                e.Value = currentRow != null && ProductSeatsHelper.IsItemActive(currentRow);
            }
        }

        /// <summary>
        /// Updates the product tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControlDataManagment_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (_refreshProductsTab &&
                e.Page == xtraTabPageDataManagment &&
                _dataManagementTypes == DataManagementTypes.Products)
            {
                UpdateNavigationGridItems();
            }
            else if (e.Page == xtraTabPageProductSeats && _isChanged)
            {
                SaveAction();
                BindProducts();
            }
        }

        /// <summary>
        /// Handles double click to open product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewProducts_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewProducts.GetFocusedRow() != null && UiHelperClass.IsClickInRow(gridViewProducts))
            {
                OpenProduct();
            }
        }

        private void gridViewProducts_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowCellStyleEventHandler(gridViewProducts_RowCellStyle), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Column == gridColumnItemActiveIcon)
                {
                    e.Appearance.BackColor = Color.Transparent;
                    e.Appearance.BackColor2 = Color.Transparent;
                }
                else
                {
                    var view = sender as GridView;
                    var currentRow = view.GetRow(e.RowHandle);

                    if (currentRow == null)
                        return;

                    var product = currentRow as Item;

                    if (product == null)
                        return;

                    if (ProductSeatsHelper.IsItemActive(product))
                        return;

                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.LightGray;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size + 1, FontStyle.Italic);
                }
            }
        }
        
        #endregion

        #endregion
    }
}
