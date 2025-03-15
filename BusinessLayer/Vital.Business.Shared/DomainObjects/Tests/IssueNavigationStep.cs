using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{
    public class IssueNavigationStep : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private TestIssue _testIssue;
        private Item _item;
        private IssueNavigationStep _parentStep;
        private int _order;
        
        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the TestIssue.
        /// </summary>
        public TestIssue TestIssue
        {
            get { return  _testIssue; }
            set
            {
                _testIssue = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

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
        /// Gets or sets the IssueNavigationStepParent.
        /// </summary>
        public IssueNavigationStep ParentStep
        {
            get { return _parentStep; }
            set
            {
                _parentStep = value;
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