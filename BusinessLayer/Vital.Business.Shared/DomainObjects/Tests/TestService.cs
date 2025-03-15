using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{
    public class TestService : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private Test _test;
        private Service _service;
        private Lookup _typeLookup;

        private string _key;
        private string _name;
        private string _description;
        private string _comments;
        private decimal _price;
        
        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the Test.
        /// </summary>
        public Test Test
        {
            get { return  _test; }
            set
            {
                _test = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the Service.
        /// </summary>
        public Service Service
        {
            get { return  _service; }
            set
            {
                _service = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                    {
                        _key = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return  _name; }
            set
            {
                    if(_name != value)
                    {
                        _name = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get { return  _description; }
            set
            {
                    if(_description != value)
                    {
                        _description = value;
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
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price
        {
            get { return  _price; }
            set
            {
                    if(_price != value)
                    {
                        _price = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the TypeLookup.
        /// </summary>
        public Lookup TypeLookup
        {
            get { return  _typeLookup; }
            set
            {
                _typeLookup = value;
                    if (_typeLookup == null) return;
                    _typeLookup.PropertyChanged += TypeLookup_PropertyChanged;
            }
        }
        
        
        #endregion
        
        #region Public Events
                
        /// <summary>
        /// Notifies the change of the state for the TypeLookup lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {            
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TypeLookup, () => TypeLookup.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Key))
            {
                IsRequiredStringPropertyValid(propertyName, Name, info);
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {
                IsRequiredStringPropertyValid(propertyName, Name, info);
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => TypeLookup) && !(TypeLookup == null || TypeLookup.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
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