using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.ShippingOrders
{
    public class ShippingOrder : DomainEntity, IDXDataErrorInfo
    {
        #region PrivateMembers

        private string _number;
        private Test _test;
        private DateTime? _sentDate;
        private bool _sendToClient;
        private bool _sent;
        private string _comments;
        private string _patientFirstName;
        private string _patientLastName;
        private string _patientAddress1;
        private string _patientAddress2;
        private string _patientCity;
        private string _patientState;
        private string _patientZip;
        private string _patientHomePhone;
        private string _patientWorkPhone;
        private string _patientCellPhone;
        private string _patientFax;
        private string _patientEmail;
        private string _technicianName;
        private string _technicianAddress;
        private string _technicianState;
        private string _technicianZipCode;
        private string _technicianCity;
        private string _technicianPhone;
        private BindingList<OrderItem> _orderItems;
        private Lookup _shippingMethod;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Number.
        /// </summary>
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Test.
        /// </summary>
        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the SentDate.
        /// </summary>
        public DateTime? SentDate
        {
            get { return _sentDate; }
            set
            {
                _sentDate = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the SendToClient.
        /// </summary>
        public bool SendToClient
        {
            get { return _sendToClient; }
            set
            {
                _sendToClient = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Sent.
        /// </summary>
        public bool Sent
        {
            get { return _sent; }
            set
            {
                _sent = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientFirstName.
        /// </summary>
        public string PatientFirstName
        {
            get { return _patientFirstName; }
            set
            {
                _patientFirstName = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientLastName.
        /// </summary>
        public string PatientLastName
        {
            get { return _patientLastName; }
            set
            {
                _patientLastName = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientAddress1.
        /// </summary>
        public string PatientAddress1
        {
            get { return _patientAddress1; }
            set
            {
                _patientAddress1 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientAddress2.
        /// </summary>
        public string PatientAddress2
        {
            get { return _patientAddress2; }
            set
            {
                _patientAddress2 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientCity.
        /// </summary>
        public string PatientCity
        {
            get { return _patientCity; }
            set
            {
                _patientCity = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientState.
        /// </summary>
        public string PatientState
        {
            get { return _patientState; }
            set
            {
                _patientState = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientZip.
        /// </summary>
        public string PatientZip
        {
            get { return _patientZip; }
            set
            {
                _patientZip = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientHomePhone.
        /// </summary>
        public string PatientHomePhone
        {
            get { return _patientHomePhone; }
            set
            {
                _patientHomePhone = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientWorkPhone.
        /// </summary>
        public string PatientWorkPhone
        {
            get { return _patientWorkPhone; }
            set
            {
                _patientWorkPhone = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientCellPhone.
        /// </summary>
        public string PatientCellPhone
        {
            get { return _patientCellPhone; }
            set
            {
                _patientCellPhone = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientFax.
        /// </summary>
        public string PatientFax
        {
            get { return _patientFax; }
            set
            {
                _patientFax = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the PatientEmail.
        /// </summary>
        public string PatientEmail
        {
            get { return _patientEmail; }
            set
            {
                _patientEmail = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TechnicianName.
        /// </summary>
        public string TechnicianName
        {
            get { return _technicianName; }
            set
            {
                _technicianName = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TechnicianAddress.
        /// </summary>
        public string TechnicianAddress
        {
            get { return _technicianAddress; }
            set
            {
                _technicianAddress = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TechnicianState.
        /// </summary>
        public string TechnicianState
        {
            get { return _technicianState; }
            set
            {
                _technicianState = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TechnicianZipCode.
        /// </summary>
        public string TechnicianZipCode
        {
            get { return _technicianZipCode; }
            set
            {
                _technicianZipCode = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TechnicianCity.
        /// </summary>
        public string TechnicianCity
        {
            get { return _technicianCity; }
            set
            {
                _technicianCity = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TechnicianPhone.
        /// </summary>
        public string TechnicianPhone
        {
            get { return _technicianPhone; }
            set
            {
                _technicianPhone = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Returns a string describing the shipping target
        /// </summary>
        public string ShippingTarget
        {
            get
            {
                return SendToClient ? "To Client" : "To Me";
            }
        }

        /// <summary>
        /// Gets or sets the Order Items.
        /// </summary>
        public BindingList<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                _orderItems.RaiseListChangedEvents = true;
                _orderItems.ListChanged += OrderItems_ListChanged;
            }
        }

        /// <summary>
        /// Checks if the order is valid for sending
        /// </summary>
        public bool ValidForShipment
        {
            get
            {
                return Validate() && ProductsIncluded;
            }
        }

        /// <summary>
        /// Checks if products are included in order with correct quantity
        /// </summary>
        public bool ProductsIncluded
        {
            get
            {
                return OrderItems != null &&
                       OrderItems.Count(oi => oi.Include &&
                                              oi.Quantity > 0) != 0 &&
                       OrderItems.Count(oi => oi.Include &&
                                              oi.Quantity == 0) == 0;
            }
        }

        public Lookup ShippingMethod
        {
            get { return _shippingMethod; }
            set
            {
                _shippingMethod = value;
                if (_shippingMethod == null) return;
                _shippingMethod.PropertyChanged += ShippingMethod_PropertyChanged;
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Sets the temporary ID for all new records
        /// </summary>
        public void UpdateOrderItemsTempIDs()
        {
            //This logic sets a temp ID for all new records so we can identify them in a unique way to handle
            //a logic in the UI the checks for duplicates
            if (OrderItems == null) return;

            foreach (var orderItem in OrderItems)
            {
                if (orderItem.ObjectState == DomainEntityState.New)
                {
                    orderItem.TempId = OrderItems.Max(oi => oi.TempId) + 1;
                }                
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Notifies the change of the OrderItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OrderItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            //Reset the Temp ID's on each change
            UpdateOrderItemsTempIDs();
            GetPropertyError(ExpressionHelper.GetPropertyName(() => OrderItems), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => OrderItems));            
        }

        /// <summary>
        /// Notifies the change of the state for the shipping method lookup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingMethod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ShippingMethod), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ShippingMethod, () => ShippingMethod.Id));
        }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (SendToClient)
            {
                if (propertyName == ExpressionHelper.GetPropertyName(() => PatientFirstName))
                {
                    IsRequiredStringPropertyValid(propertyName, PatientFirstName, info);
                }
                else if (propertyName == ExpressionHelper.GetPropertyName(() => PatientLastName))
                {
                    IsRequiredStringPropertyValid(propertyName, PatientLastName, info);
                }
                else if (propertyName == ExpressionHelper.GetPropertyName(() => PatientState))
                {
                    if (PatientState != null && PatientState.Length < 2 && PatientState.Length > 0)
                    {
                        info.ErrorText = StaticKeys.ValidationMessageMinimumLength;
                        info.ErrorType = ErrorType.Warning;
                    }
                    else
                    {
                        IsRequiredStringPropertyValid(propertyName, PatientState, info);
                    }
                }
                else if (propertyName == ExpressionHelper.GetPropertyName(() => PatientAddress1))
                {
                    IsRequiredStringPropertyValid(propertyName, PatientAddress1, info);
                }
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => TechnicianName))
            {
                IsRequiredStringPropertyValid(propertyName, TechnicianName, info);
            }
            else if (propertyName == ExpressionHelper.GetPropertyName(() => TechnicianAddress))
            {
                IsRequiredStringPropertyValid(propertyName, TechnicianAddress, info);
            }
            else if (propertyName == ExpressionHelper.GetPropertyName(() => TechnicianState))
            {
                if (TechnicianState != null && TechnicianState.Length < 2 && TechnicianState.Length > 0)
                {
                    info.ErrorText = StaticKeys.ValidationMessageMinimumLength;
                    info.ErrorType = ErrorType.Warning;
                }
                else
                {
                    IsRequiredStringPropertyValid(propertyName, TechnicianState, info);
                }
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => OrderItems))
            {
                if (!ValidateOrderItems())
                {
                    info.ErrorText = StaticKeys.ValidationMessageBlankField;
                    info.ErrorType = ErrorType.Critical;
                }
                else if (OrderItemsHaveDuplicates())
                {
                    info.ErrorText = StaticKeys.ValidationMessageDuplicatedItem;
                    info.ErrorType = ErrorType.Critical;
                }
            }

            UpdateErrorsSummary(info, propertyName);
        }

        /// <summary>
        /// Validates the order items.
        /// </summary>
        /// <returns></returns>
        public bool ValidateOrderItems()
        {
            return OrderItems != null && OrderItems.All(oi => oi.Validate());
        }

        /// <summary>
        /// Gets if the order items have any duplicates
        /// </summary>
        /// <returns></returns>
        public bool OrderItemsHaveDuplicates()
        {
            return OrderItems != null && OrderItems.Where(oi => oi.Item != null).GroupBy(oi => oi.Item.Id).Any(g => g.Count() > 1);
        }

        /// <summary>
        /// Implements the IDXErrorInfo.
        /// </summary>
        /// <param name="info"></param>
        public virtual void GetError(ErrorInfo info)
        {

        }

        /// <summary>
        /// Checks the properties by calling the Get Property error.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (!IsChanged) return true;

            ValidationErrors.Clear();

            foreach (var property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }
            
            return IsValid;
        }

        #endregion
    }
}
