using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.FrequencyTests
{
    public class FrequencyTest : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private Patient _patient;
        private string _name;
        private string _notes;

        private BindingList<FrequencyTestResult> _frequencyTestResults;
        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the Patient.
        /// </summary>
        public Patient Patient
        {
            get { return  _patient; }
            set
            {
                _patient = value;
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
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes
        {
            get { return  _notes; }
            set
            {
                    if(_notes != value)
                    {
                        _notes = value;
                        SetModifiedState(MethodBase.GetCurrentMethod().Name);
                    }    
            }
        }
        
        
        /// <summary>
        /// Gets or sets the FrequencyTestResults.
        /// </summary>
        public BindingList<FrequencyTestResult> FrequencyTestResults
        {
            get { return _frequencyTestResults; }
            set
            {
                _frequencyTestResults = value;
                _frequencyTestResults.RaiseListChangedEvents = true;
                _frequencyTestResults.ListChanged += FrequencyTestResults_ListChanged;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        #endregion
        
        #region Public Events
                
        
        
        /// <summary>
        /// Notifies the change of the FrequencyTestResults.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrequencyTestResults_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => FrequencyTestResults), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => FrequencyTestResults));
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
                
            if (propertyName == ExpressionHelper.GetPropertyName(() => Patient))
            {
            
            }
            
                        
                
            if (propertyName == ExpressionHelper.GetPropertyName(() => Name))
            {
            
            }
            
                        
                
            if (propertyName == ExpressionHelper.GetPropertyName(() => Notes))
            {
            
            }
            
                        
            
            
            if (propertyName == ExpressionHelper.GetPropertyName(() => FrequencyTestResults) && !ValidateFrequencyTestResults())
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
            ValidationErrors.Clear();

            foreach (PropertyInfo property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }
        
            
        /// <summary>
        /// Validates the FrequencyTestResults.
        /// </summary>
        /// <returns></returns>
        public bool ValidateFrequencyTestResults()
        {
            return  FrequencyTestResults.All(frequencyTestResults => frequencyTestResults.Validate());
        }            
            
        
        #endregion
    }
} 