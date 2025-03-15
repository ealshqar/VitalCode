using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class DosageOption : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private ProductForm _productForm;
        private int _order;
        private string _name;
        private string _usageSchedule;
        private string _suggestedUsage;

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
        /// Gets or sets the Order.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                if (_order == null || !_order.Equals(value))
                {
                    _order = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == null || !_name.Equals(value))
                {
                    _name = value;
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Order))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => UsageSchedule))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => SuggestedUsage))
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