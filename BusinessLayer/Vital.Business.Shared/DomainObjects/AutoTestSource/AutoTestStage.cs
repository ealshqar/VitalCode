using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoTestStage : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Lookup _stageItemsOrderType;
        private string _name;
        private string _key;
        private string _stageTabKey;
        private string _description;
        private string _dependencies;
        private bool _isMultiLevel;
        private bool _isDestinationOnly;
        private bool _scanTypeEnabled;

        #endregion

        #region LookupEnums

        /// <summary>
        /// AutoItemChildsOrderType Enum
        /// </summary>
        public ChildsOrderType StageItemsOrderTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ChildsOrderType>(StageItemsOrderType);
            }
        }

        /// <summary>
        /// StageTabKey Enum
        /// </summary>
        public StageTabKey StageTabKeyEnum
        {
            get
            {
                return EnumNameResolver.StringAsEnum<StageTabKey>(StageTabKey);
            }
        }

        /// <summary>
        /// StageKey Enum
        /// </summary>
        public StageKey StageKeyEnum
        {
            get
            {
                return EnumNameResolver.StringAsEnum<StageKey>(Key);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the StageItemsOrderType.
        /// </summary>
        public Lookup StageItemsOrderType
        {
            get { return _stageItemsOrderType; }
            set
            {
                if (_stageItemsOrderType == null || !_stageItemsOrderType.Equals(value))
                {
                    _stageItemsOrderType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_stageItemsOrderType == null) return;
                _stageItemsOrderType.PropertyChanged += StageItemsOrderType_PropertyChanged;
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
        /// Gets or sets the StageTabKey.
        /// </summary>
        public string StageTabKey
        {
            get { return _stageTabKey; }
            set
            {
                if (_stageTabKey == null || !_stageTabKey.Equals(value))
                {
                    _stageTabKey = value;
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

        /// <summary>
        /// Gets or sets the Dependencies.
        /// </summary>
        public string Dependencies
        {
            get { return _dependencies; }
            set
            {
                if (_dependencies == null || !_dependencies.Equals(value))
                {
                    _dependencies = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsMultiLevel.
        /// </summary>
        public bool IsMultiLevel
        {
            get { return _isMultiLevel; }
            set
            {
                if (!_isMultiLevel.Equals(value))
                {
                    _isMultiLevel = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsDestinationOnly.
        /// </summary>
        public bool IsDestinationOnly
        {
            get { return _isDestinationOnly; }
            set
            {
                if (!_isDestinationOnly.Equals(value))
                {
                    _isDestinationOnly = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ScanTypeEnabled.
        /// </summary>
        public bool ScanTypeEnabled
        {
            get { return _scanTypeEnabled; }
            set
            {
                if (!_scanTypeEnabled.Equals(value))
                {
                    _scanTypeEnabled = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        #endregion

        #region Memory Only Properties

        /// <summary>
        /// Indicates if the current ScanBookmark was counted in progress calculation
        /// </summary>
        public bool ProgressRecorded { get; set; }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the StageItemsOrderType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageItemsOrderType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => StageItemsOrderType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => StageItemsOrderType, () => StageItemsOrderType.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => StageItemsOrderType) && !(StageItemsOrderType == null || StageItemsOrderType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Key))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Description))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Dependencies))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsMultiLevel))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => ScanTypeEnabled))
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