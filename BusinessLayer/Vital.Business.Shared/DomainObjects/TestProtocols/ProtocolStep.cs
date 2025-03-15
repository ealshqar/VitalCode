using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.TestProtocols
{
    public class ProtocolStep : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private TestProtocol _testProtocol;
        private int _order;
        private Lookup _type;
        
        #endregion
        
        #region Constructors

        /// <summary>
        /// The Constructor
        /// </summary>
        public ProtocolStep()
        {
            _type = new Lookup();
        }

        #endregion

        #region Public Properties               
                
        /// <summary>
        /// Gets or sets the TestProtocol.
        /// </summary>
        public TestProtocol TestProtocol
        {
            get { return  _testProtocol; }
            set
            {
                _testProtocol = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order
        {
            get { return  _order; }
            set
            {
                    if(_order != value)
                    {
                        _order = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the TypeLookup.
        /// </summary>
        public Lookup Type
        {
            get { return  _type; }
            set
            {
                _type = value;
                    if (_type == null) return;
                    _type.PropertyChanged += new PropertyChangedEventHandler(Type_PropertyChanged);
            }
        }
        
        #endregion
        
        #region Public Events               
                
        /// <summary>
        /// Notifies the change of the state for the TypeLookup lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Type_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {            
            GetPropertyError(ExpressionHelper.GetPropertyName(() => this.Type), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => this.Type, () => this.Type.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => this.Type) && !(Type != null ? Type.Validate() : true) )
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

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        
        
        #endregion
    }
} 