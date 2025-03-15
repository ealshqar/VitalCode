using System;
using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Properties
{
    public class Property : DomainEntityRefluctionBase, IDXDataErrorInfo, ICloneable
    {
        #region Private Variables

        private string _name;
        private string _description;
        private Lookup _applicableTypeLookup;

        #endregion

        #region Constroctor

        public Property()
        {
            ApplicableTypeLookup = new Lookup();
        }

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the Description value.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ApplicableTypeLookup.
        /// </summary>
        public Lookup ApplicableTypeLookup
        {
            get { return _applicableTypeLookup; }
            set
            {
                _applicableTypeLookup = value;
                if (_applicableTypeLookup == null) return;
                _applicableTypeLookup.PropertyChanged += ApplicableTypeLookup_PropertyChanged;
            }
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
            else if (propertyName == ExpressionHelper.GetPropertyName(() => Key))
            {
                IsRequiredStringPropertyValid(propertyName, Key, info);
            }
            else if ((propertyName == ExpressionHelper.GetPropertyName(() => ValueTypeLookup) && !ValueTypeLookup.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => ApplicableTypeLookup) && !ApplicableTypeLookup.Validate()))
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
            if (!IsChanged) return true;

            ValidationErrors.Clear();

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }

        #endregion

        #region Events Handlers

        /// <summary>
        /// Handel the ApplicableTypeLookup changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ApplicableTypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ApplicableTypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ApplicableTypeLookup, () => ApplicableTypeLookup.Id));
        }

        #endregion
 
        #region ICloneable Implementation

        public object Clone()
        {
            return new Property
            {
                ApplicableTypeLookup = this.ApplicableTypeLookup.Clone(),
                Name = Name.Clone().ToString(),
                Description = Description.Clone().ToString(),
                ValueTypeLookup = ValueTypeLookup.Clone(),
                Caption = Caption,
                CreationDateTime = CreationDateTime,
                UiItemRepository = UiItemRepository,
                Key = Key,
                MembersConfig = MembersConfig,
                ObjectState = ObjectState,
                SourceConfig = SourceConfig,
                UpdatedDateTime = UpdatedDateTime,
                User = User,
                Value = Value,
                Id = Id
            };
        }

        #endregion
        
    }
}
