using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Lookups
{
    public class Lookup : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _type;
        private string _value;
        private string _key;
        private string _memoryName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the lookup type.
        /// </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                if(_type !=  value)
                {
                    _type = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the lookup Value
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if(_value != value)
                {
                    _value = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the lookup key
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
        /// Get or set the Memory Name
        /// </summary>
        public string MemoryName
        {
            get
            {
                return string.IsNullOrEmpty(_memoryName) ? Value : _memoryName;
            }
            set
            {
                _memoryName = value;
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Clone the current lookup.
        /// </summary>
        /// <returns></returns>
        public Lookup Clone()
        {
            return new Lookup()
                       {
                           Value = _value,
                           Type = _type,
                           Key = _key
                       };
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Id))
            {
                IsRequiredStringOrZeroPropertyValid(propertyName, Id.ToString(), info);
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
            if (!IsChanged) return true;

            ValidationErrors.Clear();

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                GetPropertyError(property.Name,new ErrorInfo());
            }

            return IsValid;
        }

        #endregion
    }
}
