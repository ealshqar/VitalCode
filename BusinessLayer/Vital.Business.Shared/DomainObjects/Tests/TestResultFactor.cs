using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{
    public class TestResultFactor : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private Item _factor;
        private TestResult _testResult;
        private int _reading;
        private Item _potency;

        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the FactorItem.
        /// </summary>
        public Item Factor
        {
            get { return  _factor; }
            set
            {
                _factor = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the TestResult.
        /// </summary>
        public TestResult TestResult
        {
            get { return  _testResult; }
            set
            {
                _testResult = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the Reading.
        /// </summary>
        public int Reading
        {
            get { return  _reading; }
            set
            {
                    if(_reading != value)
                    {
                        _reading = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the PotencyItem.
        /// </summary>
        public Item Potency
        {
            get { return  _potency; }
            set
            {
                _potency = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets a factor balancing state
        /// </summary>
        /// <returns></returns>
        public FourFactorState BalancingState
        {
            get
            {
                if (Reading == 0)
                {
                    return FourFactorState.Clear;
                }
                return CrossLayersSharedLogic.IsAcceptableReading(Reading)
                    ? FourFactorState.Balanced
                    : FourFactorState.UnBalanced;
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

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        
        #endregion
    }
} 