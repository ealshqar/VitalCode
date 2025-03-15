using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class ProductSize : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private ProductForm _productForm;
        private Lookup _status;
        private string _size;
        private decimal _price;

        #endregion

        #region LookupEnums

        /// <summary>
        /// ProductStatus Enum
        /// </summary>
        public AutoItemStatus StatusEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<AutoItemStatus>(Status);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the ProductForm.
        /// </summary>
        public ProductForm ProductForm
        {
            get { return _productForm; }
            set
            {
                if (_productForm == null || !_productForm.Equals(value))
                {
                    _productForm = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public Lookup Status
        {
            get { return _status; }
            set
            {
                if (_status == null || !_status.Equals(value))
                {
                    _status = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public string Size
        {
            get { return _size; }
            set
            {
                if (_size == null || !_size.Equals(value))
                {
                    _size = value;
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

        #endregion

        #region Public Events

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => Status) && !(Status != null ? Status.Validate() : true))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Size))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Price))
            {

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