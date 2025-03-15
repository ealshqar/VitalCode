using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class StageAnnouncement : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables
        
        private string _key;
        private string _text;
        private string _audioPath;
        
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
        /// Gets or sets the Text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == null || !_text.Equals(value))
                {
                    _text = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the AudioPath.
        /// </summary>
        public string AudioPath
        {
            get { return _audioPath; }
            set
            {
                if (_audioPath == null || !_audioPath.Equals(value))
                {
                    _audioPath = value;
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
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => Text))
            {
            
            }
                        
            if (propertyName == ExpressionHelper.GetPropertyName(() => AudioPath))
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