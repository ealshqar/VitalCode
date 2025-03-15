using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestDestination
{
    public class AutoTestResultProduct : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables
        
        private AutoTestResult _autoTestResult;
        private ProductForm _productForm;
        private ProductSize _productSize;
        private int _quantity;
        private decimal _price;
        private bool _isChecked;
        private string _duration;
        private string _schedule;
        private string _suggestedUsage;
        private string _comments;

        private int? _productFormId;
        private int? _productSizeId;
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Gets or sets the AutoTestResult.
        /// </summary>
        public AutoTestResult AutoTestResult
        {
            get { return _autoTestResult; }
            set
            {
                if (_autoTestResult == null || !_autoTestResult.Equals(value))
                {
                    _autoTestResult = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }                
            }
        }

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

                    //IMPORTANT: Here we set the _productFormId whenever ProductForm gets set to make sure the _productFormId matches the ProductForm always
                    if (_productForm != null)
                    {
                        _productFormId = _productForm.Id;    
                    }
                    
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ProductSize.
        /// </summary>
        public ProductSize ProductSize
        {
            get { return _productSize; }
            set
            {
                if (_productSize == null || !_productSize.Equals(value))
                {
                    _productSize = value;

                    //IMPORTANT: Here we set the _productSizeId whenever ProductSize gets set to make sure the _productSizeId matches the ProductSize always
                    if (_productSize != null)
                    {
                        _productSizeId = _productSize.Id;
                    }

                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (!_quantity.Equals(value))
                {
                    _quantity = value;
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
        /// Gets or sets the IsChecked.
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (!_isChecked.Equals(value))
                {
                    _isChecked = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public string Duration
        {
            get { return _duration; }
            set
            {
                if (_duration == null || !_duration.Equals(value))
                {
                    _duration = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the Schedule.
        /// </summary>
        public string Schedule
        {
            get { return _schedule; }
            set
            {
                if (_schedule == null || !_schedule.Equals(value))
                {
                    _schedule = value;
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
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set
            {
                if (_comments == null || !_comments.Equals(value))
                {
                    _comments = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        #endregion

        #region MemoryOnly Properties

        /// <summary>
        /// The Id of the ProductForm and is used for UI purposes to allow selection of ProductForm in grid
        /// </summary>
        public int? ProductFormId
        {
            get
            {
                return _productFormId;
            }
            set
            {
                _productFormId = value;
            }
        }

        /// <summary>
        /// The Id of the ProductSize and is used for UI purposes to allow selection of ProductSize in grid
        /// </summary>
        public int? ProductSizeId
        {
            get
            {
                return _productSizeId;
            }
            set
            {
                _productSizeId = value;
            }
        }

        /// <summary>
        /// Extracts the Product from the parent AutoTestResult
        /// </summary>
        public Product Product
        {
            get
            {
                //Extract the Product from the parent AutoItem in the parent AutoTestResult
                return AutoTestResult == null ||
                       AutoTestResult.AutoItem == null ||
                       AutoTestResult.AutoItem.Products == null ||
                      !AutoTestResult.AutoItem.Products.Any()? null : AutoTestResult.AutoItem.Products[0];
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Quantity))
            {
                if (IsChecked && Quantity == 0)
                {
                    info.ErrorText = StaticKeys.ValidationMessageAtLeastOneItem;
                    info.ErrorType = ErrorType.Critical;
                }
            }
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => Price))
            {
            
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ProductForm))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ProductSize))
            {

            }
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => Duration))
            {
            
            }
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => Schedule))
            {
            
            }
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => SuggestedUsage))
            {
            
            }
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => Comments))
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