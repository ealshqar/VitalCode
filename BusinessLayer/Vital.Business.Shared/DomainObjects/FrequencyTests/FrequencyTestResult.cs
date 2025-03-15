using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.FrequencyTests
{
    public class FrequencyTestResult : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private FrequencyTest _frequencyTest;
        private Item _item;
        private string _notes;
        private int _timesPerWeek;
        private int _numberOfWeeks;

        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the FrequencyTest.
        /// </summary>
        public FrequencyTest FrequencyTest
        {
            get { return  _frequencyTest; }
            set
            {
                _frequencyTest = value;
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
        /// Gets or sets the Times Per Week.
        /// </summary>
        public int TimesPerWeek
        {
            get { return _timesPerWeek; }
            set
            {
                _timesPerWeek = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Number Of Weeks.
        /// </summary>
        public int NumberOfWeeks
        {
            get { return _numberOfWeeks; }
            set
            {
                _numberOfWeeks = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
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
                
            if (propertyName == ExpressionHelper.GetPropertyName(() => FrequencyTest))
            {
            
            }
            
                        
                
            if (propertyName == ExpressionHelper.GetPropertyName(() => Item))
            {
            
            }
            
                        
                
            if (propertyName == ExpressionHelper.GetPropertyName(() => Notes))
            {
            
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
        
        
        #endregion
    }
} 