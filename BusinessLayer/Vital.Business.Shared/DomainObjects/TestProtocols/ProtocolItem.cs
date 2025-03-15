using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.DomainObjects.TestProtocols
{
    public class ProtocolItem : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private Item _item;
        private TestProtocol _testProtocol;

        
        #endregion
        
        #region Public Properties               
                
        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public Item Item
        {
            get { return  _item; }
            set
            {
                _item = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

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
        
        
        #endregion
        
        #region Validation
        
        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
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