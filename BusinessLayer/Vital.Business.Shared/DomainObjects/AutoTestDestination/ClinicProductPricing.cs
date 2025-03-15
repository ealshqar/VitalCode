using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class ClinicProductPricing : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables
        
        private Product _product;
        private string _form;
        private string _size;
        private decimal _price;
        
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
                if (_price == null || !_price.Equals(value))
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Form))
            {
            
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