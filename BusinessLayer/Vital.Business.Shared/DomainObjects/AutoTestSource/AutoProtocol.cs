using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoProtocol : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _name;
        private string _key;
        private bool _isSystemProtocol;
        private bool _isDefaultProtocol;
        
        private BindingList<AutoProtocolStage> _autoProtocolStages;
        private BindingList<AutoProtocolRevision> _autoProtocolRevisions; 

        #endregion

        #region Public Properties

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
        /// Gets or sets the IsSystemProtocol.
        /// </summary>
        public bool IsSystemProtocol
        {
            get { return _isSystemProtocol; }
            set
            {
                if (!_isSystemProtocol.Equals(value))
                {
                    _isSystemProtocol = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsDefaultProtocol.
        /// </summary>
        public bool IsDefaultProtocol
        {
            get { return _isDefaultProtocol; }
            set
            {
                if (!_isDefaultProtocol.Equals(value))
                {
                    _isDefaultProtocol = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoProtocolStages.
        /// </summary>
        public BindingList<AutoProtocolStage> AutoProtocolStages
        {
            get { return _autoProtocolStages; }
            set
            {
                if (_autoProtocolStages != null && _autoProtocolStages == value) return;

                _autoProtocolStages = value;
                _autoProtocolStages.RaiseListChangedEvents = true;
                _autoProtocolStages.ListChanged += AutoProtocolStages_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the AutoProtocolStages.
        /// </summary>
        public BindingList<AutoProtocolRevision> AutoProtocolRevisions
        {
            get { return _autoProtocolRevisions; }
            set
            {
                if (_autoProtocolRevisions != null && _autoProtocolRevisions == value) return;

                _autoProtocolRevisions = value;
                _autoProtocolRevisions.RaiseListChangedEvents = true;
                _autoProtocolRevisions.ListChanged += AutoProtocolRevisions_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the AutoProtocolStages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoProtocolStages_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoProtocolStages), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => AutoProtocolStages));
        }

        /// <summary>
        /// Notifies the change of the AutoProtocolRevisions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoProtocolRevisions_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoProtocolRevisions), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => AutoProtocolRevisions));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Key))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsSystemProtocol))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsDefaultProtocol))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsDeleted))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoProtocolStages) && !ValidateAutoProtocolStages())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the AutoProtocolStage.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAutoProtocolStages()
        {
            return AutoProtocolStages.All(autoProtocolStages => autoProtocolStages.Validate());
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