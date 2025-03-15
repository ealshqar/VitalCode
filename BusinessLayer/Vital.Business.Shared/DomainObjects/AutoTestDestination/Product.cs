using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class Product : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoItem _autoItem;
        private string _supplier;
        private string _ingredientsString;
        private string _supports;
        private string _usefulFor;
        private decimal _price;
        private decimal? _discountPercentage;
        private bool _hasDiscount;

        private BindingList<ProductForm> _productForms;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoItem.
        /// </summary>
        public AutoItem AutoItem
        {
            get { return _autoItem; }
            set
            {
                if (_autoItem == null || !_autoItem.Equals(value))
                {
                    _autoItem = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Supplier.
        /// </summary>
        public string Supplier
        {
            get { return _supplier; }
            set
            {
                if (_supplier == null || !_supplier.Equals(value))
                {
                    _supplier = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IngredientsString.
        /// </summary>
        public string IngredientsString
        {
            get { return _ingredientsString; }
            set
            {
                if (_ingredientsString == null || !_ingredientsString.Equals(value))
                {
                    _ingredientsString = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Supports.
        /// </summary>
        public string Supports
        {
            get { return _supports; }
            set
            {
                if (_supports == null || !_supports.Equals(value))
                {
                    _supports = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the UsefulFor.
        /// </summary>
        public string UsefulFor
        {
            get { return _usefulFor; }
            set
            {
                if (_usefulFor == null || !_usefulFor.Equals(value))
                {
                    _usefulFor = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (!_price.Equals(value))
                {
                    _price = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the DiscountPercentage.
        /// </summary>
        public decimal? DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                if (_discountPercentage == null || !_discountPercentage.Equals(value))
                {
                    _discountPercentage = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the HasDiscount.
        /// </summary>
        public bool HasDiscount
        {
            get { return _hasDiscount; }
            set
            {
                if (!_hasDiscount.Equals(value))
                {
                    _hasDiscount = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ProductForms.
        /// </summary>
        public BindingList<ProductForm> ProductForms
        {
            get { return _productForms; }
            set
            {
                if (_productForms != null && _productForms == value) return;

                _productForms = value;
                _productForms.RaiseListChangedEvents = true;
                _productForms.ListChanged += ProductForms_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the ProductForms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductForms_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ProductForms), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ProductForms));
        }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => Supplier))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IngredientsString))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Supports))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => UsefulFor))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Price))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => DiscountPercentage))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => HasDiscount))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ProductForms) && !ValidateProductForms())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }
        
        /// <summary>
        /// Validates the ProductForm.
        /// </summary>
        /// <returns></returns>
        public bool ValidateProductForms()
        {
            return ProductForms.All(productForms => productForms.Validate());
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