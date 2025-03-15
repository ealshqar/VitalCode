using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoProtocolRevision : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoProtocol _autoProtocol;
        private string _name;
        private string _key;
        private bool _isSystemProtocol;
        private BindingList<AutoProtocolStageRevision> _autoProtocolStageRevisions;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoProtocol.
        /// </summary>
        public AutoProtocol AutoProtocol
        {
            get { return _autoProtocol; }
            set
            {
                if (_autoProtocol == null || !_autoProtocol.Equals(value))
                {
                    _autoProtocol = value;
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
                if (_isSystemProtocol == null || !_isSystemProtocol.Equals(value))
                {
                    _isSystemProtocol = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the AutoProtocolStageRevisions.
        /// </summary>
        public BindingList<AutoProtocolStageRevision> AutoProtocolStageRevisions
        {
            get { return _autoProtocolStageRevisions; }
            set
            {
                if (_autoProtocolStageRevisions != null && _autoProtocolStageRevisions == value) return;

                _autoProtocolStageRevisions = value;
                _autoProtocolStageRevisions.RaiseListChangedEvents = true;
                _autoProtocolStageRevisions.ListChanged += AutoProtocolStageRevisions_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the AutoProtocolStageRevisions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoProtocolStageRevisions_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoProtocolStageRevisions), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => AutoProtocolStageRevisions));
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

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoProtocolStageRevisions) && !ValidateAutoProtocolStageRevisions())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the AutoProtocolStageRevision.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAutoProtocolStageRevisions()
        {
            return AutoProtocolStageRevisions.All(autoProtocolStageRevisions => autoProtocolStageRevisions.Validate());
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