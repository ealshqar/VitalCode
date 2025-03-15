using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.TestProtocols
{
    public class TestProtocol : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private string _name;
        private string _description;

        private BindingList<ProtocolItem> _protocolItems;
        private BindingList<ProtocolStep> _protocolSteps;
        
        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return  _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
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
        /// Gets or sets the ProtocolItems.
        /// </summary>
        public BindingList<ProtocolItem> ProtocolItems
        {
            get { return _protocolItems; }
            set
            {
                _protocolItems = value;
                _protocolItems.RaiseListChangedEvents = true;
                _protocolItems.ListChanged += ProtocolItems_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the ProtocolSteps.
        /// </summary>
        public BindingList<ProtocolStep> ProtocolSteps
        {
            get { return _protocolSteps; }
            set
            {
                _protocolSteps = value;
                _protocolSteps.RaiseListChangedEvents = true;
                _protocolSteps.ListChanged += ProtocolSteps_ListChanged;
            }
        }
                
        #endregion
        
        #region Handlers
        
        /// <summary>
        /// Notifies the change of the ProtocolItems.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProtocolItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ProtocolItems), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ProtocolItems));
        }

        /// <summary>
        /// Notifies the change of the ProtocolSteps.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProtocolSteps_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ProtocolSteps), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ProtocolSteps));
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
            
            else if (propertyName == ExpressionHelper.GetPropertyName(() => ProtocolItems) && !ValidateProtocolItems())
            {
                info.ErrorText = StaticKeys.ValidationMessageAtLeastOneItem;
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


        /// <summary>
        /// Validates the ProtocolItems.
        /// </summary>
        /// <returns></returns>
        public bool ValidateProtocolItems()
        {
            return _protocolItems.All(protocolItems => protocolItems.Validate()) && _protocolItems.Count > 0;
        }

        /// <summary>
        /// Validates the ProtocolSteps.
        /// </summary>
        /// <returns></returns>
        public bool ValidateProtocolSteps()
        {
            return _protocolSteps.All(protocolSteps => protocolSteps.Validate()) && _protocolSteps.Count > 0;
        }            
            
        
        #endregion
    }
} 