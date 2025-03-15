using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{
    public class TestIssue : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private Test _test;
        private Item _item;
        private ProtocolStep _protocolStep;
        private string _name;
        private bool _isMainIssue;

        private BindingList<IssueNavigationStep> _issueNavigationSteps;
        private BindingList<TestResult> _testResults;
        private string _issueNameAndNumber;
        private int _issueNumber;

        #endregion
        
        #region Public Properties

        public int IssueNumber 
        {
            get
            {
                return  _issueNumber;
            }
            set { _issueNumber = value; }
        }

        /// <summary>
        /// Gets or sets the protocol step.
        /// </summary>
        public ProtocolStep ProtocolStep
        {
            get
            {
                return _protocolStep;
            }
            set
            {
                _protocolStep = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
                if (_protocolStep == null) return;
                _protocolStep.PropertyChanged += new PropertyChangedEventHandler(ProtocolStep_PropertyChanged);                
            }
        }

        /// <summary>
        /// Notify a change in the Protocol Step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProtocolStep_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => this.ProtocolStep), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => this.ProtocolStep, () => this.ProtocolStep.Id));
        }

        /// <summary>
        /// Gets or sets the Test.
        /// </summary>
        public Test Test
        {
            get { return  _test; }
            set
            {
                _test = value;
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
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return  _name; }
            set
            {
                    if(_name != value)
                    {
                        _name = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        /// <summary>
        /// Gets or sets the IssueNavigationSteps.
        /// </summary>
        public BindingList<TestResult> TestResults
        {
            get { return _testResults; }
            set
            {
                if(value == null) return;
                _testResults = value;
                _testResults.RaiseListChangedEvents = true;
                _testResults.ListChanged += TestResults_ListChanged;
            }
        }

        public string IssueNameAndNumber
        {
            get { return _issueNameAndNumber; }
            set { _issueNameAndNumber = value; }
        }

        /// <summary>
        /// Gets or sets IsMainIssue falg.
        /// </summary>
        public bool IsMainIssue
        {
            get { return _isMainIssue; }
            set
            {
                if (_isMainIssue != value)
                {
                    _isMainIssue = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets the next test result temp ID
        /// </summary>
        public int NextResultId
        {
            get
            {
                return TestResults != null && TestResults.Count != 0 ? TestResults.Max(tr => tr.Id) + 1 : 1;
            }
        }

        #endregion
        
        #region Public Events


        /// <summary>
        /// Notifies the change of the TestResults.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TestResults_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TestResults), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TestResults));
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
            
            /*No validation is needed for test results but this is kept here in case we need it
            Notice that this validation slows down the inline editing of test issue name since this 
            code is called each time you edit the issue name
            if (propertyName == ExpressionHelper.GetPropertyName(() => TestResults) && !ValidateTestResults())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }*/
                    
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

            if (ObjectState == DomainEntityState.Deleted) return true;

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        
        /// <summary>
        /// Validates the IssueNavigationSteps.
        /// </summary>
        /// <returns></returns>
        public bool ValidateTestResults()
        {
            return TestResults.All(testResults => testResults.Validate());
        }    
        
        #endregion
    }
} 
