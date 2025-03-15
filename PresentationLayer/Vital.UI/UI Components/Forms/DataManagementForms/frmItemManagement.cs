using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.BaseForms;
using System.Linq;

namespace Vital.UI.UI_Components.Forms.DataManagementForms
{
    public partial class frmItemManagement : VitalBaseForm
    {
        #region Fields

        private ItemsManager _itemsManager;
        private LookupsManager _lookupsManager;
        private SettingsManager _settingsManager;
        private PropertiesManager _propertiesManager;
        private AppInfoManager _appInfoManager;

        private List<ItemRelation> _deletedItemRelationObjects;
        private List<ItemTarget> _deletedItemTargetObjects;
        //private List<ItemProperty> _deletedItemPropertyObjects;

        private BindingList<ItemProperty> _itemProperties;
        private BindingList<ItemProperty> _productDosages; 

        private bool _shouldClose;
        private int _productApplicableTypeLookupId;

        private bool _isNewItem;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public Item Item
        {
            get; set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public frmItemManagement(Item item)
        {
            InitializeComponent();
            Item = item;
            _isNewItem = item != null && item.ObjectState == DomainEntityState.New;
        }

        #endregion

        #region Methods 

        #region Initialization & Binding

        /// <summary>
        /// Performs some steps to initialize the form.
        /// </summary>
        public override void PerformSpecificIntializationSteps()
        {
            _itemsManager = new ItemsManager();
            _lookupsManager = new LookupsManager();
            _settingsManager = new SettingsManager();
            _propertiesManager = new PropertiesManager();
            _appInfoManager = new AppInfoManager();

            SetFormTitle(Item.Name);

            var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

            if (!designMode)
                FillLocalLookupIds();
            
            _deletedItemRelationObjects = new List<ItemRelation>();
            _deletedItemTargetObjects = new List<ItemTarget>();
            //_deletedItemPropertyObjects = new List<ItemProperty>();

            SetFormStatus(Item.ObjectState == DomainEntityState.New);
        }

        /// <summary>
        /// Init properties Lists
        /// </summary>
        private void InitPropertiesLists()
        {
            if (Item.ObjectState == DomainEntityState.New)
            {
                _productDosages = new BindingList<ItemProperty>();
                _itemProperties = new BindingList<ItemProperty>();

            }
            else
            {
                //This separation of properties made for make a different dataSource for each of properties grid and dosages grid.
                _productDosages = Item.Properties.Where(p => p.Property.ApplicableTypeLookup.Id == _productApplicableTypeLookupId).ToBindingList();
                _itemProperties = Item.Properties.Where(p => p.Property.ApplicableTypeLookup.Id != _productApplicableTypeLookupId).ToBindingList();
            }

            //_itemProperties.RaiseListChangedEvents = true;
            //_itemProperties.ListChanged += _itemProperties_ListChanged;

            _productDosages.RaiseListChangedEvents = true;
            _productDosages.ListChanged += _productDosages_ListChanged;
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            Item.PropertyChanged += Item_PropertyChanged;
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
            Item.PropertyChanged -= Item_PropertyChanged;
            //_itemProperties.ListChanged -= _itemProperties_ListChanged;
            _productDosages.ListChanged -= _productDosages_ListChanged;
        }

        /// <summary>
        /// Binds the values with its controls.
        /// </summary>
        public override void SetBinding()
        {
            FillLookUps();
            InitPropertiesLists();

            UiHelperClass.BindControl(this, Item, () => Item.Name);

            UiHelperClass.SetLayoutControlProperties(layoutControlItem);

            UiHelperClass.BindControl(memoEditMemo, Item, () => Item.Memo);
            UiHelperClass.BindControl(memoEditDescription, Item, () => Item.Description);
            UiHelperClass.BindControl(textEditName, Item, () => Item.Name);
            UiHelperClass.BindControl(textEditFullName, Item, () => Item.FullName);

            //xtraUserControlPropertiesItemProperties.Bind(_itemProperties, new List<ApplicableTypesEnum> { ApplicableTypesEnum.ItemAndItemRelation});
            xtraUserControlPropertiesItemDefaultDosages.Bind(_productDosages, new List<ApplicableTypesEnum> { ApplicableTypesEnum.Product});
            
            layoutControlGroupCountDown.Visibility = Item.ObjectState == DomainEntityState.New? LayoutVisibility.Always:LayoutVisibility.Never;
            
            
            var defaultWaitingEnabled = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.NewSubstanceWaiting) });

            var onOffLookups = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.OnOff));

            if (defaultWaitingEnabled != null && onOffLookups != null)
            {
                var lookup = onOffLookups.FirstOrDefault(h => h.Id == Int32.Parse(defaultWaitingEnabled.Value.ToString()));
                
                if (lookup != null)
                {
                    var countDownOn = lookup.Value == StaticKeys.TimerOn;

                    if (countDownOn)
                    {
                        radioGroupCountDownOnOff.EditValue = true;
                    }

                    var defaultWaitingTime = _settingsManager.GetSetting(new SettingsFilter{Key = EnumNameResolver.Resolve(SettingKeys.NewSubstanceWaitingTime)});
                           
                    digitalGaugeCountDown.Text = defaultWaitingTime == null ? StaticKeys.TimerDefaultTimeWaiting : defaultWaitingTime.Value.ToString();
                }
            }
        }

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = Item;
            dxErrorProviderMain.UpdateBinding();
        }

        /// <summary>
        /// Clears the bindings of the controls.
        /// </summary>
        public override void ClearBinding()
        {
            memoEditMemo.DataBindings.Clear();
            memoEditDescription.DataBindings.Clear();
            
            textEditFullName.DataBindings.Clear();
            textEditName.DataBindings.Clear();
                        
            dxErrorProviderMain.ClearErrors();            
        }
       
        /// <summary>
        /// Sets the edit mode of the form.
        /// </summary>
        /// <param name="isReadOnly">The is read only flag.</param>
        public override void SetEditMode(bool isReadOnly)
        {            
            memoEditMemo.Properties.ReadOnly = isReadOnly;
            memoEditDescription.Properties.ReadOnly = isReadOnly;
            textEditFullName.Properties.ReadOnly = isReadOnly;
            textEditName.Properties.ReadOnly = isReadOnly;
            simpleButtonGenerateDosages.Enabled = !isReadOnly;
            radioGroupCountDownOnOff.Enabled = !isReadOnly && Item.ObjectState == DomainEntityState.New;

            //xtraUserControlPropertiesItemProperties.PropertyColumnReadOnly = false;
            xtraUserControlPropertiesItemDefaultDosages.PropertyColumnReadOnly = true;

            //xtraUserControlPropertiesItemProperties.ReadOnly = isReadOnly;
            xtraUserControlPropertiesItemDefaultDosages.ReadOnly = isReadOnly;
        }

        /// <summary>
        /// Fill the local lookups ids.
        /// </summary>
        private void FillLocalLookupIds()
        {
            var productApplicableTypeLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.ApplicableType, ApplicableTypesEnum.Product));

            _productApplicableTypeLookupId = productApplicableTypeLookup == null ? 0 : productApplicableTypeLookup.Id;
        }

        #endregion

        #region Save related actions

        /// <summary>
        /// Performs custom logic for the save action
        /// </summary>
        public override bool SaveAction()
        {
            bool isSuccessful = false;

            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            BeforeSaveAction();

            if (Item.Validate() && Validate())// && xtraUserControlPropertiesItemProperties.Validate())
            {
                var canSaveItem = _itemsManager.CanSaveItem(Item);

                if (canSaveItem.IsSucceed)
                {
                    if (Item.ObjectState == DomainEntityState.New && (bool)radioGroupCountDownOnOff.EditValue)
                    {
                        SetEditModeBase(true);
                        timerCountDown.Start();
                    }
                    else                    
                    {
                        if (Save(false))
                        {
                            AfterSaveAction();
                            _isNewItem = false;
                            FormStatus = FormStatusEnum.Disabled;
                            isSuccessful = true;
                        }
                    }

                    
                }
            }          
            
            UiHelperClass.HideSplash();

            return isSuccessful;
        }

        /// <summary>
        /// Performs the logic of save and close action
        /// </summary>
        public override void SaveAndCloseAction()
        {
            _shouldClose = true;

            if(SaveAction())
            {
                Close();
            }
        }

        /// <summary>
        /// Handles saving using timer option
        /// </summary>
        private void TimerSaveAction()
        {
            timerCountDown.Stop();
            timerCountDown.Enabled = false;

            if (Save(false))
            {
                AfterSaveAction();

                if (_shouldClose)
                {
                    Close();
                }
                else
                {
                    SetEditModeBase(false);
                    FormStatus = FormStatusEnum.Disabled;
                }
            }

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <returns></returns>
        public override bool Save(bool isClosing)
        {
            try
            {
                CollectProperties();

                UpdateListsWithDeletedRows();

                var result = _itemsManager.SaveItem(Item);

                if (result.IsSucceed)
                {
                    ClearDeletedLists();
                    return true;
                }

                return false;                
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }

        /// <summary>
        /// Show\Hide error icons.
        /// </summary>
        public override void ShowHideErrorIcons()
        {            
            dxErrorProviderMain.UpdateBinding();            
        }

        /// <summary>
        /// Post values if the item.
        /// </summary>
        public override void PostValues()
        {
            textEditFullName.DoValidate();
            textEditName.DoValidate();
            memoEditMemo.DoValidate();
            memoEditDescription.DoValidate();
            //xtraUserControlPropertiesItemProperties.PostValues();
            xtraUserControlPropertiesItemDefaultDosages.PostValues();
        }

        /// <summary>
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        public override bool HasChanges()
        {
            return Item.ObjectState != DomainEntityState.Unchanged &&
                   Item.ObjectState != DomainEntityState.Deleted;
        }

        /// <summary>
        /// Reverts the changes.
        /// </summary>
        /// <returns></returns>
        public override bool Revert()
        {
            try
            {
                IsLoaded = false;

                Item = Item.ObjectState == DomainEntityState.New ? new Item { Name = StaticKeys.NewItem } : _itemsManager.GetItemById(new SingleItemFilter { ItemId = Item.Id });

                Rebind();

                ClearDeletedLists();

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
        /// Check if the item can delete, also get a confirmation from the user.
        /// </summary>
        /// <returns></returns>
        public override bool CanDelete()
        {                
            if(!base.CanDelete()) return false;

            var canDelete = _itemsManager.CanDeleteItem(Item);

            if(!canDelete.IsSucceed)
                UiHelperClass.ShowInformation(canDelete.Message,StaticKeys.CannotDeleteItemTitle);

            return canDelete.IsSucceed;
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            try
            {
                var result = _itemsManager.DeleteItem(Item);

                if (result.IsSucceed) Close();

                return result.IsSucceed;   
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);

                return false;
            }
        }

        /// <summary>
        /// Before delete actions.
        /// </summary>
        public override void BeforeDeleteAction()
        {
            PostValues();
            Revert();
        }
      
        #endregion

        #region Logic

        /// <summary>
        /// Add the deleted objects to the actual list again.
        /// </summary>
        private void UpdateListsWithDeletedRows()
        {          
            foreach (var relation in _deletedItemRelationObjects)
            {
                Item.Parents.Add(relation);
            }

            foreach (var target in _deletedItemTargetObjects)
            {
                Item.ItemTargets.Add(target);
            }

            //foreach (var property in _deletedItemPropertyObjects)
            //{
            //    Item.Properties.Add(property);
            //}
        }

        /// <summary>
        /// Generate dosages for current Item.
        /// </summary>
        public void GenerateDosages()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.GeneratingMessage);

            if(_productDosages == null)
                _productDosages = new BindingList<ItemProperty>();

            var productProperties = _propertiesManager.GetProperties(new PropertiesFilter { ApplicableTypeIds = new[] { _productApplicableTypeLookupId } });

            foreach (var productProperty in productProperties)
            {
                var existesDosage = _productDosages.FirstOrDefault(ip => ip.Property.Id == productProperty.Id);

                if(existesDosage != null)
                    continue;

                _productDosages.Add(new ItemProperty() { Item = Item, Property = productProperty });
            }

            xtraUserControlPropertiesItemDefaultDosages.Bind(_productDosages, new List<ApplicableTypesEnum>
                                                                                  {
                                                                                      ApplicableTypesEnum.Product
                                                                                  });

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Collect the properties from the product dosages list(_productDosages) and item properties list (_itemProperties) to the Item.Properties list again.
        /// </summary>
        private void CollectProperties()
        {
            Item.Properties = new BindingList<ItemProperty>();

            foreach (var itemProperty in _itemProperties)
            {
                Item.Properties.Add(itemProperty);
            }

            foreach (var itemDosage in _productDosages)
            {
                Item.Properties.Add(itemDosage);
            }

            if (Item.ObjectState == DomainEntityState.New)
            {
                var itemStateProperty = new ItemProperty
                {
                    Item = Item,
                    Property = _propertiesManager.GetPropertyByKey(PropertiesEnum.ItemState),
                    Value = ProductSeatsHelper.NewProductItemState
                };

                Item.Properties.Add(itemStateProperty);
            }
        }

        /// <summary>
        /// Clear the deleted object list, This method should be called after save and undo actions.
        /// </summary>
        private void ClearDeletedLists()
        {
            _deletedItemRelationObjects.Clear();
            _deletedItemTargetObjects.Clear();
            //_deletedItemPropertyObjects.Clear();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the form loaded event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmItemManagement_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(frmItemManagement_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                IsLoaded = true;
            }
        }

        /// <summary>
        /// Handel the object changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (Item.ObjectState)
            {
                case DomainEntityState.Modified:
                    FormStatus = FormStatusEnum.Modified;
                    break;
                case DomainEntityState.Deleted:
                    FormStatus = FormStatusEnum.Locked;
                    break;
                case DomainEntityState.Unchanged:
                    FormStatus = FormStatusEnum.Unchanged;
                    break;
            }
        }

        /// <summary>
        /// Handel the changes on product dosages list.
        /// </summary>
        void _productDosages_ListChanged(object sender, ListChangedEventArgs e)
        {
            FormStatus = FormStatusEnum.Modified;
        }

        /// <summary>
        /// Handel the changes on item properties list.
        /// </summary>
        void _itemProperties_ListChanged(object sender, ListChangedEventArgs e)
        {
            FormStatus = FormStatusEnum.Modified;
        }

        /// <summary>
        /// Handles the tick of the timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            var nextCount = int.Parse(digitalGaugeCountDown.Text) - 1;

            if (nextCount >= 0)
            {
                digitalGaugeCountDown.Text = nextCount.ToString();
            }
            else
            {
                TimerSaveAction();
            }
        }

        /// <summary>
        /// Handles form closing action
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void frmItemManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerCountDown.Enabled)
            {
                e.Cancel = true;
                UiHelperClass.ShowInformation(StaticKeys.WaitTimerMessgae, StaticKeys.WaitTimerMessgaeTitile);
            }
            else
            {
                e.Cancel = !CanClose();                
            }
        }      

        /// <summary>
        /// Customize the new property initializing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyRelational"></param>
        private void xtraUserControlPropertiesItemProperties_CustomPropertyRelationalInit(object sender, DomainEntityPropertyRelational propertyRelational)
        {
            //if(propertyRelational == null) 
            //    return;

            //var itemProperty = propertyRelational as ItemProperty;

            //if(itemProperty == null) 
            //    return;

            //itemProperty.Item = Item;
        }

        /// <summary>
        /// Process deletion for property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyRelational"></param>
        private void xtraUserControlPropertiesItemProperties_PropertyDeleted(object sender, DomainEntityPropertyRelational propertyRelational)
        {
            //var itemPeoperty = propertyRelational as ItemProperty;

            //if (itemPeoperty == null)
            //    return;

            //_deletedItemPropertyObjects.Add(itemPeoperty);
        }

        /// <summary>
        /// Handel the clicking on simpleButtonGenerateDosages button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonGenerateDosages_Click(object sender, EventArgs e)
        {
            GenerateDosages();
        }

        #endregion                

        #endregion
    }
}
