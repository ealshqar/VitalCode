using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class TestingPoint : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _key;
        private string _name;
        private string _fullName;
        private string _hWIdentifier;
        private string _description;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key == null || !_key.Equals(value))
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
        /// Gets or sets the FullName.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName == null || !_fullName.Equals(value))
                {
                    _fullName = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the HWIdentifier.
        /// </summary>
        public string HWIdentifier
        {
            get { return _hWIdentifier; }
            set
            {
                if (_hWIdentifier == null || !_hWIdentifier.Equals(value))
                {
                    _hWIdentifier = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == null || !_description.Equals(value))
                {
                    _description = value;
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Key))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => FullName))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => HWIdentifier))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Description))
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