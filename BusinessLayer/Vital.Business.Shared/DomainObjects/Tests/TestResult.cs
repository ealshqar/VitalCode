using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{    
    public class TestResult : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables
        
        private TestIssue _testIssue;
        private Item _item;
        private DateTime _dateTime;
        private TestResult _parent;
        private Item _vitalForce;
        private Item _ratioItem;
        private bool _isSelected;
        private bool _isCurrent;
        private bool _isImprinted;
        private TestResult _selectedparent;
        private Lookup _stepType;
        private BindingList<TestResultFactor> _testResultFactors;
        private TestProtocol _testProtocol;
        private int tempImprintingId;
        private int _focusedChildRowHandle;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// FocusedChildRowHandle
        /// </summary>
        public int FocusedChildRowHandle
        {
            get { return _focusedChildRowHandle; }
            set
            {
                _focusedChildRowHandle = value;
            }
        }

        /// <summary>
        /// Gets or sets the Steo Type lookup.
        /// </summary>
        public Lookup StepType
        {
            get
            {
                return _stepType;
            }
            set
            {
                _stepType = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the test protocol.
        /// </summary>
        public TestProtocol TestProtocol
        {
            get
            {
                return _testProtocol;
            }
            set
            {
                _testProtocol = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

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
        /// Gets the Item Name
        /// </summary>
        public string ItemName
        {
            get
            {
                return Item == null ? string.Empty : Item.Name;
            }
        }

        /// <summary>
        /// Get the name of the test result item and adds to it the ratio item name in case the result has a ratio item
        /// </summary>
        public string ItemNameWithRatio
        {
            get
            {
                return RatioItem == null ? ItemName : ItemName + " - " + RatioItem.Name;
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
        /// Gets or sets the DateTime.
        /// </summary>
        public DateTime DateTime
        {
            get { return  _dateTime; }
            set
            {
                    if(_dateTime != value)
                    {
                        _dateTime = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the TestResultParent.
        /// </summary>
        public TestResult Parent
        {
            get { return  _parent; }
            set
            {
                _parent = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets the ID of the parent test result if it exists, this is needed for the 
        /// handling of tree view of the test results in the test results tree in item testing
        /// </summary>
        public int ParentResultId
        {
            get { return Parent != null? Parent.Id:0; }            
        }

        /// <summary>
        /// Gets the ID of the parent test result if it exists, this is needed for the 
        /// handling of tree view of the test results in the test results tree in item testing
        /// </summary>
        public int SelectedParentResultId
        {
            get { return SelectedParent != null ? SelectedParent.Id : 0; }
        }

        /// <summary>
        /// Gets or sets the VitalForce.
        /// </summary>
        public Item VitalForce
        {
            get { return  _vitalForce; }
            set
            {
                _vitalForce = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
                if (_vitalForce == null) return;
                    _vitalForce.PropertyChanged += VitalForce_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the RatioItem.
        /// </summary>
        public Item RatioItem
        {
            get { return _ratioItem; }
            set
            {
                _ratioItem = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
                if (_ratioItem == null) return;
                _ratioItem.PropertyChanged += RatioItem_PropertyChanged;
            }
        }

        /// <summary>
        /// Notify a change in the Vital Force
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VitalForce_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => VitalForce), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => VitalForce, () => VitalForce.Id));
        }

        /// <summary>
        /// Notify a change in the RatioItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RatioItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => RatioItem), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => RatioItem, () => RatioItem.Id));
        }

        /// <summary>
        /// Gets or sets the TestResultFactors.
        /// </summary>
        public BindingList<TestResultFactor> TestResultFactors
        {
            get { return _testResultFactors; }
            set
            {
                _testResultFactors = value;
                _testResultFactors.RaiseListChangedEvents = true;
                _testResultFactors.ListChanged += TestResultFactors_ListChanged;
            }
        }

        /// <summary>
        /// Gets or Sets the IsSelected value. this flag will determine if the test result will be shown in the test results tree or not.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or Sets the IsCurrent value. this flag will determine if the test result is the current step in the testing.
        /// </summary>
        public bool IsCurrent
        {
            get { return _isCurrent; }
            set
            {
                _isCurrent = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the TestResultParent.
        /// </summary>
        public TestResult SelectedParent
        {
            get { return _selectedparent; }
            set
            {
                _selectedparent = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the IsImprinted.
        /// </summary>
        public bool IsImprinted
        {
            get { return _isImprinted; }
            set
            {
                if (_isImprinted != value)
                {
                    _isImprinted = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Include a list of dilutions related to the bacteria result
        /// </summary>
        public string CustomDilutions { get; set; }

        /// <summary>
        /// Determines if the test result is a Dilutions Group Parent
        /// </summary>
        public bool IsDilutionGroupParent { get; set; }

        /// <summary>
        /// Temporary ID for imprintable items check
        /// </summary>
        public int TempImprintingId
        {
            get
            {
                return ObjectState == DomainEntityState.New ? tempImprintingId : Id;
            }
            set
            {
                if (tempImprintingId == 0)
                {
                    tempImprintingId = value;    
                }
            }
        }

        #region FourFactorsProperties

        /// <summary>
        /// Gets a factor balancing state
        /// </summary>
        /// <param name="factorEnum"></param>
        /// <returns></returns>
        private FourFactorState GetFactorBalancingByEnumKey(FourFactors factorEnum)
        {
            if (TestResultFactors != null)
            {
                var factor = TestResultFactors
                    .FirstOrDefault(trf => trf.Factor.Name == EnumNameResolver.Resolve(factorEnum));

                return factor == null ? FourFactorState.Clear : factor.BalancingState;
            }

            return FourFactorState.Clear;
        }

        /// <summary>
        /// Get Lymphatic Factor balancing state
        /// </summary>
        public FourFactorState Lymphatic
        {
            get
            {
                return GetFactorBalancingByEnumKey(FourFactors.LY);
            }
        }

        /// <summary>
        /// Get nerve Factor balancing state
        /// </summary>
        public FourFactorState Nerve
        {
            get
            {
                return GetFactorBalancingByEnumKey(FourFactors.NE);
            }
        }

        /// <summary>
        /// Get Circulation Factor balancing state
        /// </summary>
        public FourFactorState Circulation
        {
            get
            {
                return GetFactorBalancingByEnumKey(FourFactors.CI);
            }
        }

        /// <summary>
        /// Get Organ Factor balancing state
        /// </summary>
        public FourFactorState Organ
        {
            get
            {
                return GetFactorBalancingByEnumKey(FourFactors.OR);
            }
        }
        
        #endregion

        #endregion
        
        #region Handlers
        
        /// <summary>
        /// Notifies the change of the TestResultFactors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestResultFactors_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestResultFactors), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestResultFactors));
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

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        
        /// <summary>
        /// Validates the TestResultFactors.
        /// </summary>
        /// <returns></returns>
        public bool ValidateTestResultFactors()
        {
            if (TestResultFactors == null) return true;
            return  TestResultFactors.All(testResultFactors => testResultFactors.Validate());
        }            
           
        
        #endregion
    }
} 