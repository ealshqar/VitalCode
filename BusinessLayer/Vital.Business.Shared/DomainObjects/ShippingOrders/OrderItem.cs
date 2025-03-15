using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.ShippingOrders
{
    public class OrderItem : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private ShippingOrder _shippingOrder;
        private Item _item;
        private int _quantity;
        private string _comments;
        private bool _include;
        private int _tempId;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Temporary ID to identify records that are new for a unique dependent logic
        /// </summary>
        public int TempId
        {
            get
            {
                return Id == 0 ? _tempId : Id;
            }
            set
            {
                _tempId = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the ShippingOrder.
        /// </summary>
        public ShippingOrder ShippingOrder
        {
            get { return  _shippingOrder; }
            set
            {
                _shippingOrder = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public Item Item
        {
            get { return  _item; }
            set
            {
                _item = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    _item.PropertyChanged += Item_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        public int Quantity
        {
            get { return  _quantity; }
            set
            {
                    if(_quantity != value)
                    {
                        _quantity = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments
        {
            get { return  _comments; }
            set
            {
                    if(_comments != value)
                    {
                        _comments = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the Include.
        /// </summary>
        public bool Include
        {
            get { return  _include; }
            set
            {
                    if(_include != value)
                    {
                        _include = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        #endregion
        
        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Item), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Item, () => Item.Id));
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
            //Here we are using GetPropertyName(() => Item, () => Item.Id) to make sure the error icon shows up in the grid
            if (propertyName == ExpressionHelper.GetPropertyName(() => Item, () => Item.Id))
            {
                if (Item == null)
                {
                    info.ErrorText = StaticKeys.ValidationMessageBlankField;
                    info.ErrorType = ErrorType.Critical;
                }
                //This logic uses a temp ID for all records including new ones where the ID is zero 
                //so we can identify them in a unique way to handle a logic in the UI the checks for duplicates
                else if (ShippingOrder != null && 
                         ShippingOrder.OrderItems.Count(
                        oi => oi.Item != null && oi.Item.Id == Item.Id && oi.TempId != TempId) > 0)
                {
                    info.ErrorText = StaticKeys.ValidationMessageDuplicatedItem;
                    info.ErrorType = ErrorType.Critical;
                }
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Quantity))
            {
                if (Include && Quantity == 0)
                {
                    info.ErrorText = StaticKeys.ValidationMessageAtLeastOneItem;
                    info.ErrorType = ErrorType.Critical;
                }
            }

            UpdateErrorsSummary(info);
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
            ValidationErrors.Clear();

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        
        #endregion
    }
} 