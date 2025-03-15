using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGauges.Core.Resources;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraBars;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.BaseForms;
using Vital.UI.UI_Components.UI_Classes;
using Vital.UI.UI_Components.User_Controls.Modules;
using Vital.Business.Shared.DomainObjects.Services;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmShippingOrder : VitalBaseForm
    {
        #region Fields
        
        private BarButtonItem _barButtonItemPrint;
        private SettingsManager _settingsManager;
        private ShippingOrdersManager _shippingOrdersManager;
        private bool _shipmentTargetChanged;
        private bool _shipmentMethodChanged;
        private bool _orderItemsChanged;
        private BindingList<OrderItem> _deletedOrderItems;
        private Item _tempItem;
        private XtraFormPatientSearch _patientSearch;
        private BindingList<Patient> _patientsList;
        private PatientsManager _patientsManager;
        private bool _isBounded;
        private bool _patientDetailsChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public frmShippingOrder()
        {
            InitializeComponent();
            CustomeInitializeComponent();            
        }

        #endregion

        #region Properties

        public Test TestObject { get; set; }

        public ShippingOrder ShippingOrderObject
        {
            get; set;
        }

        public XtraUserControlPrintingOptions PrintingOptions { get; set; }

        public bool IsAdhocOrder
        {
            get
            {
                return TestObject == null;    
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
        /// Gets a list of patients.
        /// </summary>
        public void GetPatients()
        {
            try
            {
                _patientsList = _patientsManager.GetPatients(new PatientsFilter());
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #region Methods

        #region Initialization & Binding & Helpers

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public override sealed void PerformSpecificIntializationSteps()
        {
            textEditFirstName.Properties.MaxLength = 50;
            textEditLastName.Properties.MaxLength = 50;
            textEditCity.Properties.MaxLength = 50;
            textEditState.Properties.MaxLength = 2;
            textEditZip.Properties.MaxLength = 10;
            textEditHomePhone.Properties.MaxLength = 50;
            textEditWorkPhone.Properties.MaxLength = 50;
            textEditCellPhone.Properties.MaxLength = 50;
            textEditFax.Properties.MaxLength = 50;

            textEditTechnicianZipCode.Properties.MaxLength = 10;
            textEditTechnicianCity.Properties.MaxLength = 50;
            textEditTechnicianName.Properties.MaxLength = 100;
            textEditTechnicianPhone.Properties.MaxLength = 50;
            textEditTechnicianCity.Properties.MaxLength = 50;

            FillLookUps();
            SetFormStatus(ShippingOrderObject.Id == 0);
            _settingsManager = new SettingsManager();
            _shippingOrdersManager = new ShippingOrdersManager();
            _deletedOrderItems = new BindingList<OrderItem>();
            _patientsManager = new PatientsManager();
            _patientSearch =new XtraFormPatientSearch();

            var fontSizeSetting =
                _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });

            var fontSize = float.Parse(fontSizeSetting.Value.ToString());
            
            SetFonts(fontSize);
            UiHelperClass.SetViewProperties(gridViewOrderItems);

            UpdateAdhocOrderUI();
        }

        /// <summary>
        /// Handles UI changes for adhoc orders
        /// </summary>
        private void UpdateAdhocOrderUI()
        {
            if (!IsAdhocOrder)
            {
                layoutControlItemFindPatient.Visibility = LayoutVisibility.Never;
                //emptySpaceItemFindPatient.Visibility = LayoutVisibility.Never;
                //layoutControlGroupClientInfo.Visibility = LayoutVisibility.Never;
                //layoutControlItemRadioGroup.Visibility = LayoutVisibility.Never;
                //emptySpaceItem2.Visibility = LayoutVisibility.Never;
                //layoutControlItem5.Visibility = LayoutVisibility.Never;
                //layoutControlItemBox.Visibility = LayoutVisibility.Never;
                //layoutControlItemRememberChoice.Visibility = LayoutVisibility.Never;
                //simpleLabelItem1.Visibility = LayoutVisibility.Never;
            }
            else
            {
                layoutControlItemUpdatePatientAddress.Visibility = LayoutVisibility.Never;
                //emptySpaceItemFindPatient.Visibility = LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// Set the controls font.
        /// </summary>
        private void SetFonts(float fontSize)
        {
            gridViewOrderItems.Appearance.Row.Font = UiHelperClass.GetFontWithSize(fontSize);
        }

        /// <summary>
        /// Fill the lookup controls with the collections of objects from the cache
        /// </summary>
        public override void FillLookUps()
        {
            try
            {
                repositoryItemSearchLookUpEditProducts.DataSource =
                    ((BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.AllProducts))
                    .Where(i => i.ItemSourceLookup == null || 
                          (i.ItemSourceLookup != null && i.ItemSourceLookup.Id == UiHelperClass.GetSystemItemSourceLookupId())).ToBindingList();

                lookUpEditShippingMethod.Properties.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.ShippingMethod);
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        public override sealed void CustomeInitializeComponent()
        {            
            _barButtonItemPrint = AddBarButtonItem("barButtonItemPrint", "Print Shipment Order", true,
                                                           Resources.Print, 6,
                                                           6, true,
                                                           new BarShortcut((Keys.Control | Keys.P)), true,
                                                           "Print Shipment Order",
                                                           "Click this button to print a shipment order based on the details specified.\r\n",
                                                           "You can use Ctrl+P to print the shipment order.", true);
        }

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public override void SetupHandllers()
        {
            ShippingOrderObject.PropertyChanged += ShippingOrder_PropertyChanged;
        }
        
        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public override void ClearHandlers()
        {
           
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public override void SetBinding()
        {
            UiHelperClass.BindControl(textEditOrderNumber, ShippingOrderObject, () => ShippingOrderObject.Number);
            UiHelperClass.BindControl(dateEditDateSent, ShippingOrderObject, () => ShippingOrderObject.SentDate);
            UiHelperClass.BindControl(checkEditOrderSent, ShippingOrderObject, () => ShippingOrderObject.Sent);
            UiHelperClass.BindControl(textEditFirstName, ShippingOrderObject, () => ShippingOrderObject.PatientFirstName);
            UiHelperClass.BindControl(textEditLastName, ShippingOrderObject, () => ShippingOrderObject.PatientLastName);
            UiHelperClass.BindControl(textEditAddress1, ShippingOrderObject, () => ShippingOrderObject.PatientAddress1);
            UiHelperClass.BindControl(textEditAddress2, ShippingOrderObject, () => ShippingOrderObject.PatientAddress2);
            UiHelperClass.BindControl(textEditCity, ShippingOrderObject, () => ShippingOrderObject.PatientCity);
            UiHelperClass.BindControl(textEditState, ShippingOrderObject, () => ShippingOrderObject.PatientState);
            UiHelperClass.BindControl(textEditZip, ShippingOrderObject, () => ShippingOrderObject.PatientZip);
            UiHelperClass.BindControl(textEditHomePhone, ShippingOrderObject, () => ShippingOrderObject.PatientHomePhone);
            UiHelperClass.BindControl(textEditWorkPhone, ShippingOrderObject, () => ShippingOrderObject.PatientWorkPhone);
            UiHelperClass.BindControl(textEditCellPhone, ShippingOrderObject, () => ShippingOrderObject.PatientCellPhone);
            UiHelperClass.BindControl(textEditFax, ShippingOrderObject, () => ShippingOrderObject.PatientFax);
            UiHelperClass.BindControl(textEditEmail, ShippingOrderObject, () => ShippingOrderObject.PatientEmail);
            UiHelperClass.BindControl(textEditTechnicianName, ShippingOrderObject, () => ShippingOrderObject.TechnicianName);
            UiHelperClass.BindControl(textEditTechnicianAddress, ShippingOrderObject, () => ShippingOrderObject.TechnicianAddress);
            UiHelperClass.BindControl(textEditTechnicianState, ShippingOrderObject, () => ShippingOrderObject.TechnicianState);
            UiHelperClass.BindControl(textEditTechnicianZipCode, ShippingOrderObject, () => ShippingOrderObject.TechnicianZipCode);
            UiHelperClass.BindControl(textEditTechnicianCity, ShippingOrderObject, () => ShippingOrderObject.TechnicianCity);
            UiHelperClass.BindControl(textEditTechnicianPhone, ShippingOrderObject, () => ShippingOrderObject.TechnicianPhone);
            UiHelperClass.BindControl(memoEditComments, ShippingOrderObject, () => ShippingOrderObject.Comments);          
            UiHelperClass.BindControl(radioGroupShippingTarget, StaticKeys.EditvaluePropertyname, ShippingOrderObject, () => ShippingOrderObject.SendToClient);
            UiHelperClass.BindControl(lookUpEditShippingMethod, ShippingOrderObject, () => ShippingOrderObject.ShippingMethod,() => ShippingOrderObject.ShippingMethod.Id);
            
            BindOrderItems();

            UpdateFormShippingTarget(ShippingOrderObject.SendToClient);
            UpdateBarButtonsState();
            _isBounded = true;
        }

        /// <summary>
        /// Binds the order items
        /// </summary>
        private void BindOrderItems()
        {
            gridControlOrderItems.DataBindings.Clear();
            UiHelperClass.BindControl(gridControlOrderItems, ShippingOrderObject, () => ShippingOrderObject.OrderItems);
            ShippingOrderObject.OrderItems.ListChanged += OrderItems_ListChanged;
            _orderItemsChanged = false;
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public override void ClearBinding()
        {
            
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public override void SetEditMode(bool isReadOnly)
        {
            isReadOnly = isReadOnly || ShippingOrderObject.Sent;
            
            _barButtonItemPrint.Enabled = !isReadOnly;

            checkEditOrderSent.Properties.ReadOnly = true;
            dateEditDateSent.Properties.ReadOnly = true;
            textEditOrderNumber.Properties.ReadOnly = true;

            simpleButtonSendShipment.Enabled = !isReadOnly;
            simpleButtonClearSelection.Enabled = !isReadOnly;
            simpleButtonDeleteSelected.Enabled = !isReadOnly;
            simpleButtonSelectAll.Enabled = !isReadOnly;
            simpleButtonFindPatient.Enabled = !isReadOnly;

            checkEditRememberChoice.Properties.ReadOnly = isReadOnly;
            radioGroupShippingTarget.Properties.ReadOnly = isReadOnly;

            gridViewOrderItems.OptionsBehavior.ReadOnly = isReadOnly;

            textEditFirstName.Properties.ReadOnly = isReadOnly;
            textEditLastName.Properties.ReadOnly = isReadOnly;
            textEditAddress1.Properties.ReadOnly = isReadOnly;
            textEditAddress2.Properties.ReadOnly = isReadOnly;
            textEditCity.Properties.ReadOnly = isReadOnly;
            textEditState.Properties.ReadOnly = isReadOnly;
            textEditZip.Properties.ReadOnly = isReadOnly;
            textEditHomePhone.Properties.ReadOnly = isReadOnly;
            textEditWorkPhone.Properties.ReadOnly = isReadOnly;
            textEditCellPhone.Properties.ReadOnly = isReadOnly;
            textEditFax.Properties.ReadOnly = isReadOnly;
            textEditEmail.Properties.ReadOnly = isReadOnly;
            textEditTechnicianName.Properties.ReadOnly = isReadOnly;
            textEditTechnicianAddress.Properties.ReadOnly = isReadOnly;
            textEditTechnicianState.Properties.ReadOnly = isReadOnly;
            textEditTechnicianZipCode.Properties.ReadOnly = isReadOnly;
            textEditTechnicianCity.Properties.ReadOnly = isReadOnly;
            textEditTechnicianPhone.Properties.ReadOnly = isReadOnly;
            memoEditComments.Properties.ReadOnly = isReadOnly;
            lookUpEditShippingMethod.Properties.ReadOnly = isReadOnly;

            _barButtonItemPrint.Enabled = true;
        }

        /// <summary>
        /// Customize for a new Items of the bar manager, or to do extra items;
        /// </summary>
        /// <param name="itemName">The Clicked item tag.</param>
        public override void CustomeBarManagerClickHandling(string itemName)
        {
            if (string.IsNullOrEmpty(itemName)) return;

            if (itemName.Equals("barButtonItemPrint"))
            {
                PostValues();
                UiHelperClass.PrintShippingOrderReport(ShippingOrderObject, PrintingOptions);
            }
        }
       
        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public override void SetupMainErrorProvider()
        {
            dxErrorProviderMain.DataSource = ShippingOrderObject;            
        }

        /// <summary>
        /// Show or Hide the errors upper control they not supported the DxErrorProvider.
        /// </summary>
        public override void ShowHideErrorIcons()
        {
            //var generalValid = ShippingOrderObject.ValidateOrderItems();
            //var hasDuplicates = ShippingOrderObject.OrderItemsHaveDuplicates();
            //if (generalValid || hasDuplicates)
            //{
            //    dxErrorProviderMain.SetError(gridControlOrderItems,hasDuplicates
            //            ? StaticKeys.ValidationMessageDuplicatedItem
            //            : StaticKeys.ValidationMessageGeneral,
            //        ErrorType.Critical);
            //}
            //else
            //{
            //    dxErrorProviderMain.SetError(gridControlOrderItems,"", ErrorType.None);
            //}
        }

        /// <summary>
        /// Before Save Actions.
        /// </summary>
        public override void BeforeSaveAction()
        {
            base.BeforeSaveAction();
            UpdateListsWithDeletedRows();
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public override void AfterLoadAction()
        {
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
                return SaveLogic();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty,exception);

                return false;
            }
        }

        /// <summary>
        /// Selects the tab that has errors
        /// </summary>
        /// <returns></returns>
        private bool HasTargetError()
        {
            return ShippingOrderObject.ValidationErrors.Count(
                er =>
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientAddress1) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientAddress2) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientCellPhone) ||
                    er.PropertyName == ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientCity) ||
                    er.PropertyName == ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientEmail) ||
                    er.PropertyName == ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientFax) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientFirstName) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientHomePhone) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientLastName) ||
                    er.PropertyName == ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientState) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientWorkPhone) ||
                    er.PropertyName == ExpressionHelper.GetPropertyName(() => ShippingOrderObject.PatientZip) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.TechnicianPhone) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.TechnicianAddress) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.TechnicianCity) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.TechnicianName) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.TechnicianState) ||
                    er.PropertyName ==
                    ExpressionHelper.GetPropertyName(() => ShippingOrderObject.TechnicianZipCode)) > 0;
        }

        /// <summary>
        /// Save Logic
        /// </summary>
        /// <returns></returns>
        private bool SaveLogic()
        {
            dxErrorProviderMain.UpdateBinding();
            if (!ShippingOrderObject.Validate())
            {
                //Focus the tab with errors
                tabbedControlGroup.SelectedTabPage = HasTargetError() ? layoutControlGroupShippingInfo : layoutControlGroupOrderInfo;

                UiHelperClass.ShowError("Verify all fields are correct", "Make sure all required fields are filled in correctly."); 
                return false;
            }

            UpdateTargetChoice();
            UpdateShippingMethodSetting();

            var result = _shippingOrdersManager.Save(ShippingOrderObject).IsSucceed;

            if (result)
            {
                ShippingOrderObject.Number = TestObject == null? ShippingOrderObject.Id.ToString() : 
                                             TestObject.Patient.Id + "-" +
                                             TestObject.Id + "-" +
                                             ShippingOrderObject.Id;
                result = _shippingOrdersManager.Save(ShippingOrderObject).IsSucceed;
            }

            UpdateBarButtonsState();

            return result;
        }

        /// <summary>
        /// Update the target choice
        /// </summary>
        private void UpdateTargetChoice()
        {
            if (_shipmentTargetChanged && checkEditRememberChoice.Checked)
            {
                UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.ShippingOrderSettings,
                                                      SettingKeys.SendShipmentToClient,
                                                      (bool)radioGroupShippingTarget.EditValue,
                                                      _settingsManager);
                _shipmentTargetChanged = false;
            }
        }

        /// <summary>
        /// Update the shipping method setting
        /// </summary>
        private void UpdateShippingMethodSetting()
        {
            if (_shipmentMethodChanged && checkEditDefaultShippingMethod.Checked)
            {
                var setting = UiHelperClass.GetSettingFromCache(SettingKeys.ShippingMethod,CachableDataEnum.VisibleSettings);

                if (setting != null)
                {
                    setting.Value = ShippingOrderObject.ShippingMethod.Id;
                    _settingsManager.Save(setting);
                }                
                _shipmentMethodChanged = false;
            }
        }

        /// <summary>
        /// Delete the current shipping order
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            try
            {
                PostValues();

                Revert();
                
                var result = _shippingOrdersManager.Delete(ShippingOrderObject);

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
        /// Actions After save.
        /// </summary>
        public override void AfterSaveAction()
        {
            _orderItemsChanged = false;
        }

        /// <summary>
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        public override bool HasChanges()
        {
            return false;
        }

        /// <summary>
        /// Posts the values in the controls that are not yet committed to the dataSource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public override void PostValues()
        {
            gridViewOrderItems.PostEditor();
            gridViewOrderItems.ValidateEditor();

            textEditFirstName.DoValidate();
            textEditLastName.DoValidate();
            textEditAddress1.DoValidate();
            textEditAddress2.DoValidate();
            textEditCity.DoValidate();
            textEditState.DoValidate();
            textEditZip.DoValidate();
            textEditHomePhone.DoValidate();
            textEditWorkPhone.DoValidate();
            textEditCellPhone.DoValidate();
            textEditFax.DoValidate();
            textEditEmail.DoValidate();
            textEditTechnicianName.DoValidate();
            textEditTechnicianAddress.DoValidate();
            textEditTechnicianState.DoValidate();
            textEditTechnicianZipCode.DoValidate();
            textEditTechnicianCity.DoValidate();
            textEditTechnicianPhone.DoValidate();
            memoEditComments.DoValidate();
            lookUpEditShippingMethod.DoValidate();
            lookUpEditShippingMethod.PostLookupValues(ShippingOrderObject.ShippingMethod);
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public override bool Revert()
        {
            ClearHandlers();
            ClearBinding();

            IsLoaded = false;
            _deletedOrderItems.Clear();
            ShippingOrderObject = _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter { ItemId = ShippingOrderObject.Id });

            Rebind();

            IsLoaded = true;

            UpdateBarButtonsState();

            return true;
        } 
              
        #endregion        
        
        #region Logic

        /// <summary>
        /// Updates the UI to match the current selected shipping target
        /// </summary>
        /// <param name="sendToClient"></param>
        private void UpdateFormShippingTarget(Boolean sendToClient)
        {
            if (ShippingOrderObject.Sent)
            {
                simpleButtonSendShipment.Text = sendToClient ? "Sent to Client" : "Sent to Me";
                simpleButtonSendShipment.Image = Resources.accept;
            }
            else
            {               
                simpleButtonSendShipment.Text = sendToClient ? "Send Shipment To Client" : "Send Shipment To Me";
            }

            layoutControlGroupClientInfo.CaptionImageVisible = sendToClient;
            layoutControlGroupTechnicianInfo.CaptionImageVisible = !sendToClient;
        }

        /// <summary>
        /// Updates the state of the delete button
        /// </summary>
        private void UpdateBarButtonsState()
        {
            var visibility = ShippingOrderObject.Sent ? BarItemVisibility.Never :
                                                        BarItemVisibility.Always;

            barButtonItemDelete.Visibility = ShippingOrderObject.ObjectState == DomainEntityState.New ? BarItemVisibility.Never : visibility;
            barButtonItemSave.Visibility = visibility;
            barButtonItemSaveAndClose.Visibility = visibility;
            barButtonItemEdit.Visibility = visibility;
            barButtonItemDisable.Visibility = visibility;
            barButtonItemUndo.Visibility = ShippingOrderObject.ObjectState == DomainEntityState.New ? BarItemVisibility.Never : visibility;
        }

        /// <summary>
        /// Add the deleted objects to the actual list again.
        /// </summary>
        private void UpdateListsWithDeletedRows()
        {
            foreach (var orderItem in _deletedOrderItems)
            {
                ShippingOrderObject.OrderItems.Add(orderItem);
            }

            _deletedOrderItems.Clear();            
        }

        /// <summary>
        /// Delete Order item
        /// </summary>
        private void DeleteOrderItem()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected product will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var focusedRow = gridViewOrderItems.GetFocusedRow() as OrderItem;

                    if (focusedRow != null && focusedRow.Id > 0)
                    {
                        //mark the object as deleted.
                        focusedRow.ObjectState = DomainEntityState.Deleted;
                        //add the deleted objects to a temporary list.
                        _deletedOrderItems.Add(focusedRow);
                    }

                    //delete the row 
                    gridViewOrderItems.DeleteRow(gridViewOrderItems.FocusedRowHandle);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Shows the search dialog
        /// </summary>
        private void ShowSearchDialog()
        {
            UiHelperClass.ShowWaitingPanel("Opening Patient Search ...");
            GetPatients();
            _patientSearch = new XtraFormPatientSearch();
            _patientSearch.SetPatientsDatasource(_patientsList);//Sets datasource for patients
            var showSearchAutomatically = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ShowPatientSearchAutomatically);

            _patientSearch.SetShowAutomaticallySetting(showSearchAutomatically);//Sets the auto show setting from DB

            UiHelperClass.HideSplash();

            var result = _patientSearch.ShowDialog();

            if (showSearchAutomatically != _patientSearch.AutoShowUpSetting)
            {
                showSearchAutomatically = _patientSearch.AutoShowUpSetting;//Get the auto show up setting
                var showSearchAuto = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShowPatientSearchAutomatically) });
                showSearchAuto.Value = showSearchAutomatically ? UiHelperClass.GetYesLookupId().ToString() : UiHelperClass.GetNoLookupId().ToString();
                _settingsManager.Save(showSearchAuto);
            }

            if (result == DialogResult.Yes)
            {
                var patient = _patientSearch.CurrentPatient;
                if (patient != null)
                {
                    ShippingOrderObject.PatientFirstName = patient.FirstName;
                    ShippingOrderObject.PatientLastName = patient.LastName;
                    ShippingOrderObject.PatientAddress1 = patient.Address1;
                    ShippingOrderObject.PatientAddress2 = patient.Address2;
                    ShippingOrderObject.PatientCity = patient.City;
                    ShippingOrderObject.PatientState = patient.State;
                    ShippingOrderObject.PatientZip = patient.Zip;
                    ShippingOrderObject.PatientHomePhone = patient.HomePhone;
                    ShippingOrderObject.PatientWorkPhone = patient.WorkPhone;
                    ShippingOrderObject.PatientCellPhone = patient.CellPhone;
                    ShippingOrderObject.PatientFax = patient.Fax;
                    ShippingOrderObject.PatientEmail = patient.Email;
                }
            }
        }

        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Handle the propriety changed event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        private void ShippingOrder_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(ShippingOrder_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                switch (ShippingOrderObject.ObjectState)
                {
                    case DomainEntityState.Modified:
                        FormStatus = FormStatusEnum.Modified;
                        break;
                    case DomainEntityState.New:
                        FormStatus = FormStatusEnum.Modified;
                        break;
                    case DomainEntityState.Unchanged:
                        FormStatus = FormStatusEnum.Unchanged;
                        break;
                }
            }
        }

        /// <summary>
        /// Handel the form closing event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(frmSettings_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                e.Cancel = !CanClose();

                if (e.Cancel)
                {
                    return;
                }
                else
                {
                    UpdateTargetChoice();
                    UpdateShippingMethodSetting();
                }
            }
        }        

        /// <summary>
        /// Handel on load event of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSettings_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(frmSettings_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                
            }
        }
        
        /// <summary>
        /// Handles changes in shipping target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupShippingTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupShippingTarget.EditValue != radioGroupShippingTarget.OldEditValue)
            {
                _shipmentTargetChanged = true;
                var visbility = /*IsAdhocOrder ? LayoutVisibility.Never : */LayoutVisibility.Always;
                layoutControlItemRememberChoice.Visibility = visbility;
                emptySpaceItem2.Visibility = visbility;    
            }            
            UpdateFormShippingTarget((bool)radioGroupShippingTarget.EditValue);
        }

        /// <summary>
        /// Handles sending the shipping order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSendShipment_Click(object sender, EventArgs e)
        {
            try
            {
                dxErrorProviderMain.UpdateBinding();
                PostValues();

                UiHelperClass.ShowWaitingPanel("Validation ...");

                if (!ShippingOrderObject.Validate())
                {
                    UiHelperClass.HideSplash();
                    UiHelperClass.ShowError("Missing Information",
                        "Please make sure all required information are provided.");
                    return;
                }                
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }
            finally
            {
                UiHelperClass.HideSplash();
            }

            if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ShippingConfirmation) == DialogResult.Yes)
            {
                PostValues();
                //Validate before saving, for example user can save an order without items to send but he can't send it
                //so rather than saving and then show an error that he can't send, we prevent saving in the first place.
                //he can still save normally but saving here can be surprising and not expected.
                if (ShippingOrderObject.ValidForShipment)
                {
                    if (SaveLogic())
                    {
                        var result = UiHelperClass.SendShipmentOrder(ShippingOrderObject, TestObject, true, true,
                            PrintingOptions, true);

                        if (result.IsSucceed)
                        {
                            SaveLogic();
                            SetEditModeBase(true);
                            simpleButtonSendShipment.Enabled = false;
                            UpdateFormShippingTarget(ShippingOrderObject.SendToClient);
                            UpdateBarButtonsState();
                        }
                        else
                        {
                            UiHelperClass.ShowError("Shipping Order Send Error",result.Message);
                        }
                    }
                    else
                    {
                        UiHelperClass.ShowError("Invalid Order Details", "Please check that products and other details are valid for shipment");
                    }
                }
                else
                {
                    UiHelperClass.ShowError("Invalid Order Details","Please check that products and other details are valid for shipment");
                }
            }
        }

        /// <summary>
        /// Handles changes in the order items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ListChangedEventHandler(OrderItems_ListChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                _orderItemsChanged = true;
            }
        }

        /// <summary>
        /// Handles order item initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewOrderItems_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new InitNewRowEventHandler(gridViewOrderItems_InitNewRow), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var newOrderItemRow = gridViewOrderItems.GetFocusedRow() as OrderItem;

                if (newOrderItemRow != null)
                {
                    //Set the first product in the list as a default product
                    Item firstItem = null;
                    if (_tempItem != null)
                    {
                        firstItem = _tempItem;
                    }
                    else
                    {
                        firstItem = ((BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.AllProducts))
                            .FirstOrDefault();
                    }
                        
                    if (firstItem != null)
                    {
                        newOrderItemRow.Item = new Item() { Id = firstItem.Id, Name = firstItem.Name, Description = firstItem.Description };
                    }
                    newOrderItemRow.Include = true;
                    newOrderItemRow.ShippingOrder = ShippingOrderObject;
                    newOrderItemRow.Quantity = 1;
                    _tempItem = null;
                }
            }
        }

        /// <summary>
        /// Handles context menu opnining
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripOrderItems_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripOrderItems_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == contextMenuStripOrderItems)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewOrderItems);
                    toolStripMenuItemDeleteOrderItem.Enabled =
                        UiHelperClass.IsClickInRowByMouse(gridViewOrderItems) && IsInEditMode && !ShippingOrderObject.Sent;
                }
            }
        }

        /// <summary>
        /// Handles context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripOrderItems_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStripOrderItems_ItemClicked), sender, e);
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

                if (sender == contextMenuStripOrderItems)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteOrderItem)
                    {
                        DeleteOrderItem();
                    }
                }
            }
        }

        /// <summary>
        /// Handles edit value changing for the product dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemSearchLookUpEditProducts_EditValueChanging(object sender, ChangingEventArgs e)
        {
            /*
             THE CODE BELOW IS VERY IMPORTANT:
             Using the code below we reset the Item object when doing a change in the dropdown selected value
             This makes sure that when the item selected is duplicated and another Order record has the same item
             both Order Item records won't get connected by the same item object by reference and that each one has
             his own object, this way, current record item will be separate, other records will be separate and the 
             produts cache will be separate.
             */
            if (e.NewValue == null) return;

            var foundItem =
                ((BindingList<Item>)CacheHelper.SetOrGetCachableData(CachableDataEnum.AllProducts)).FirstOrDefault(
                    i => i.Id == int.Parse(e.NewValue.ToString()));
            _tempItem = foundItem;
            var orderItem = (OrderItem)gridViewOrderItems.GetFocusedRow();

            if (foundItem != null && orderItem != null)
            {
                orderItem.Item = new Item() { Id = foundItem.Id, Name = foundItem.Name, Description = foundItem.Description };
                
                //Close editor and post its value
                gridViewOrderItems.PostEditor();
                gridViewOrderItems.ValidateEditor();
                gridViewOrderItems.CloseEditor();
                //Refresh the form and re-validate to update the error icons
                ShippingOrderObject.ValidateOrderItems();
                ShippingOrderObject.OrderItemsHaveDuplicates();
                gridControlOrderItems.RefreshDataSource();
            }            
        }

        /// <summary>
        /// Include all products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var orderItem in ShippingOrderObject.OrderItems)
            {
                orderItem.Include = true;
            }
        }

        /// <summary>
        /// Uncheck all products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClearSelection_Click(object sender, EventArgs e)
        {
            foreach (var orderItem in ShippingOrderObject.OrderItems)
            {
                orderItem.Include = false;
            }
        }

        /// <summary>
        /// Delete selected products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDeleteSelected_Click(object sender, EventArgs e)
        {
            var tempDeleted = new BindingList<OrderItem>();

            foreach (var orderItem in ShippingOrderObject.OrderItems)
            {
                if (orderItem.Include)
                {
                    tempDeleted.Add(orderItem);
                }
            }

            foreach (var orderItem in tempDeleted)
            {
                ShippingOrderObject.OrderItems.Remove(orderItem);
            }

            foreach (var orderItem in tempDeleted)
            {
                if (orderItem.ObjectState != DomainEntityState.New)
                {
                    orderItem.ObjectState = DomainEntityState.Deleted;
                    _deletedOrderItems.Add(orderItem);
                }
            }
        }

        /// <summary>
        /// Updates binding when changing selected tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabbedControlGroup_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            dxErrorProviderMain.UpdateBinding();
        }

        /// <summary>
        /// Handles finding patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonFindPatient_Click(object sender, EventArgs e)
        {
            ShowSearchDialog();
        }

        /// <summary>
        /// Saves address information to patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateAddress_Click(object sender, EventArgs e)
        {
            PostValues();
            if (UiHelperClass.ShowConfirmQuestion("This will override the patient address information, are you sure?") ==
                DialogResult.Yes)
            {
                var tempPatient = new Patient();

                tempPatient.DateOfBirth = DateTime.Now;
                tempPatient.GenderLookup = new Lookup() { Id = 1 };
                tempPatient.State = "aa";
                tempPatient.FirstName = ShippingOrderObject.PatientFirstName;
                tempPatient.LastName = ShippingOrderObject.PatientLastName;
                tempPatient.Address1 = ShippingOrderObject.PatientAddress1;
                tempPatient.Address2 = ShippingOrderObject.PatientAddress2;
                tempPatient.City = ShippingOrderObject.PatientCity;
                tempPatient.State = ShippingOrderObject.PatientState;
                tempPatient.Zip = ShippingOrderObject.PatientZip;
                tempPatient.HomePhone = ShippingOrderObject.PatientHomePhone;
                tempPatient.WorkPhone = ShippingOrderObject.PatientWorkPhone;
                tempPatient.CellPhone = ShippingOrderObject.PatientCellPhone;
                tempPatient.Fax = ShippingOrderObject.PatientFax;
                tempPatient.Email = ShippingOrderObject.PatientEmail;

                if (!tempPatient.Validate())
                {
                    UiHelperClass.ShowError("Please make sure all required fields are filled", "Invalid Information");
                    return;
                }

                var currentPatient = _patientsManager.GetPatientById(new SingleItemFilter() { ItemId = TestObject.Patient.Id });

                if (currentPatient != null)
                {
                    currentPatient.FirstName = ShippingOrderObject.PatientFirstName;
                    currentPatient.LastName = ShippingOrderObject.PatientLastName;
                    currentPatient.Address1 = ShippingOrderObject.PatientAddress1;
                    currentPatient.Address2 = ShippingOrderObject.PatientAddress2;
                    currentPatient.City = ShippingOrderObject.PatientCity;
                    currentPatient.State = ShippingOrderObject.PatientState;
                    currentPatient.Zip = ShippingOrderObject.PatientZip;
                    currentPatient.HomePhone = ShippingOrderObject.PatientHomePhone;
                    currentPatient.WorkPhone = ShippingOrderObject.PatientWorkPhone;
                    currentPatient.CellPhone = ShippingOrderObject.PatientCellPhone;
                    currentPatient.Fax = ShippingOrderObject.PatientFax;
                    currentPatient.Email = ShippingOrderObject.PatientEmail;
                    _patientsManager.SavePatient(currentPatient);
                    _patientDetailsChanged = true;

                    UiHelperClass.ShowInformation("Patient Information Updated Successfully.");
                }
                else
                {
                    UiHelperClass.ShowError("Not able to load patient profile, please make sure patient profile exists.", "Patient Loading Error");
                }
            }

        }

        /// <summary>
        /// Update technician information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonUpdateTechAddress_Click(object sender, EventArgs e)
        {
            PostValues();
            if (UiHelperClass.ShowConfirmQuestion("This will override technician information, are you sure?") ==
                DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(ShippingOrderObject.TechnicianName) ||
                    string.IsNullOrEmpty(ShippingOrderObject.TechnicianAddress) ||
                    string.IsNullOrEmpty(ShippingOrderObject.TechnicianState))
                {
                    UiHelperClass.ShowError("Verify all fields are correct", "Make sure all required fields are filled in correctly.");
                    return;
                }

                UiHelperClass.SaveChange(CachableDataEnum.VisibleSettings, SettingKeys.TechnicianName, ShippingOrderObject.TechnicianName, _settingsManager);
                UiHelperClass.SaveChange(CachableDataEnum.VisibleSettings, SettingKeys.TechnicianAddress, ShippingOrderObject.TechnicianAddress, _settingsManager);
                UiHelperClass.SaveChange(CachableDataEnum.VisibleSettings, SettingKeys.TechnicianCity, ShippingOrderObject.TechnicianCity, _settingsManager);
                UiHelperClass.SaveChange(CachableDataEnum.VisibleSettings, SettingKeys.TechnicianState, ShippingOrderObject.TechnicianState, _settingsManager);
                UiHelperClass.SaveChange(CachableDataEnum.VisibleSettings, SettingKeys.TechnicianZip, ShippingOrderObject.TechnicianZipCode, _settingsManager);
                UiHelperClass.SaveChange(CachableDataEnum.VisibleSettings, SettingKeys.TechnicianPhone, ShippingOrderObject.TechnicianPhone, _settingsManager);

                UiHelperClass.ShowInformation("Technician Information Updated Successfully.");
            }
        }

        /// <summary>
        /// Handles changing the selected shipping method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEditShippingMethod_EditValueChanged(object sender, EventArgs e)
        {
            if (_isBounded && lookUpEditShippingMethod.EditValue != lookUpEditShippingMethod.OldEditValue)
            {
                _shipmentMethodChanged = true;
                layoutControlItemDefaultShippingMethod.Visibility = LayoutVisibility.Always;
            }
        }

        #endregion
    }
}
