using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Settings
{
    public class HwProfile : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _name;
        private string _key;
        private int _minReading;
        private int _disconnectedTimeout;
        private int _stabilityTimeout;
        private int _stabilityRange;
        private int _defaultMinReading;
        private int _defaultDisconnectedTimeout;
        private int _defaultStabilityTimeout;
        private int _defaultStabilityRange;
        private bool _isSystemProfile;
        private bool _isDefault;
        private byte[] _iamge;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Get or set the Key.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the MinReading.
        /// </summary>
        public int MinReading
        {
            get { return _minReading; }
            set
            {
                _minReading = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the DisconnectedTimeout.
        /// </summary>
        public int DisconnectedTimeout
        {
            get { return _disconnectedTimeout; }
            set
            {
                _disconnectedTimeout = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the StabilityTimeout.
        /// </summary>
        public int StabilityTimeout
        {
            get { return _stabilityTimeout; }
            set
            {
                _stabilityTimeout = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the StabilityRange.
        /// </summary>
        public int StabilityRange
        {
            get { return _stabilityRange; }
            set
            {
                _stabilityRange = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the MinReading.
        /// </summary>
        public int DefaultMinReading
        {
            get { return _defaultMinReading; }
            set
            {
                _defaultMinReading = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the DisconnectedTimeout.
        /// </summary>
        public int DefaultDisconnectedTimeout
        {
            get { return _defaultDisconnectedTimeout; }
            set
            {
                _defaultDisconnectedTimeout = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the StabilityTimeout.
        /// </summary>
        public int DefaultStabilityTimeout
        {
            get { return _defaultStabilityTimeout; }
            set
            {
                _defaultStabilityTimeout = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the StabilityRange.
        /// </summary>
        public int DefaultStabilityRange
        {
            get { return _defaultStabilityRange; }
            set
            {
                _defaultStabilityRange = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the IsSystemProfile.
        /// </summary>
        public bool IsSystemProfile
        {
            get { return _isSystemProfile; }
            set
            {
                _isSystemProfile = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the IsDefault.
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set
            {
                _isDefault = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Image.
        /// </summary>
        public byte[] Image
        {
            get { return _iamge; }
            set
            {
                _iamge = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Restore default values
        /// </summary>
        public void RestoreDefaults()
        {
            MinReading = DefaultMinReading;
            DisconnectedTimeout = DefaultDisconnectedTimeout;
            StabilityRange = DefaultStabilityRange;
            StabilityTimeout = DefaultStabilityTimeout;
        }

        /// <summary>
        /// Restore default values
        /// </summary>
        public void SetDefaultsInitialValues()
        {
            DefaultMinReading = MinReading;
            DefaultDisconnectedTimeout = DisconnectedTimeout;
            DefaultStabilityRange = StabilityRange;
            DefaultStabilityTimeout = StabilityTimeout;
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {
                IsRequiredStringPropertyValid(propertyName, Name, info);
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

            foreach (var property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }


            return IsValid;
        }

        #endregion
    }
}
