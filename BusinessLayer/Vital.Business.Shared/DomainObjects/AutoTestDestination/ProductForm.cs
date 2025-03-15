using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class ProductForm : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Product _product;
        private Lookup _status;
        private string _form;
        private string _suggestedUsage;
        private string _usageSchedule;

        private BindingList<DosageOption> _doageOptions;
        private BindingList<ProductSize> _productSizes;

        #endregion

        #region LookupEnums

        /// <summary>
        /// AutoItemStatus Enum
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
        /// Gets or sets the Product.
        /// </summary>
        public Product Product
        {
            get { return _product; }
            set
            {
                if (_product == null || !_product.Equals(value))
                {
                    _product = value;
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
        /// Gets or sets the Form.
        /// </summary>
        public string Form
        {
            get { return _form; }
            set
            {
                if (_form == null || !_form.Equals(value))
                {
                    _form = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the SuggestedUsage.
        /// </summary>
        public string SuggestedUsage
        {
            get { return _suggestedUsage; }
            set
            {
                if (_suggestedUsage == null || !_suggestedUsage.Equals(value))
                {
                    _suggestedUsage = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the UsageSchedule.
        /// </summary>
        public string UsageSchedule
        {
            get { return _usageSchedule; }
            set
            {
                if (_usageSchedule == null || !_usageSchedule.Equals(value))
                {
                    _usageSchedule = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the DoageOptions.
        /// </summary>
        public BindingList<DosageOption> DosageOptions
        {
            get { return _doageOptions; }
            set
            {
                if (_doageOptions != null && _doageOptions == value) return;

                _doageOptions = value;
                _doageOptions.RaiseListChangedEvents = true;
                _doageOptions.ListChanged += DoageOptions_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ProductSizes.
        /// </summary>
        public BindingList<ProductSize> ProductSizes
        {
            get { return _productSizes; }
            set
            {
                if (_productSizes != null && _productSizes == value) return;

                _productSizes = value;
                _productSizes.RaiseListChangedEvents = true;
                _productSizes.ListChanged += ProductSizes_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the DoageOptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoageOptions_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => DosageOptions), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => DosageOptions));
        }

        /// <summary>
        /// Notifies the change of the ProductSizes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductSizes_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ProductSizes), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ProductSizes));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Status) && !(Status != null ? Status.Validate() : true))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Form))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => SuggestedUsage))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => UsageSchedule))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => DosageOptions) && !ValidateDoageOptions())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ProductSizes) && !ValidateProductSizes())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the DoageOption.
        /// </summary>
        /// <returns></returns>
        public bool ValidateDoageOptions()
        {
            return DosageOptions.All(doageOptions => doageOptions.Validate());
        }

        /// <summary>
        /// Validates the ProductSize.
        /// </summary>
        /// <returns></returns>
        public bool ValidateProductSizes()
        {
            return ProductSizes.All(productSizes => productSizes.Validate());
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