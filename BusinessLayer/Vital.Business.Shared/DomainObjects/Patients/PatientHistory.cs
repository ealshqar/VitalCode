using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Patients
{
    public class PatientHistory : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private string _name;
        private string _description;
        private Lookup _typeLookup;
        private Patient _patient;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
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
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get { return _description; }
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
        /// Gets or sets the Type.
        /// </summary>
        public Lookup TypeLookup
        {
            get { return _typeLookup; }
            set
            {
                _typeLookup = value;
                if (_typeLookup == null) return;
                _typeLookup.PropertyChanged += new PropertyChangedEventHandler(TypeLookoup_PropertyChanged);
            }
        }

        /// <summary>
        /// Gets or sets the Patient.
        /// </summary>
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        

        /// <summary>
        /// Notifies the change of the state for the gender lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TypeLookoup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => this.TypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => this.TypeLookup, () => this.TypeLookup.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => this.Name))
            {
                IsRequiredStringPropertyValid(propertyName, Name, info);
            }


            else if (propertyName == ExpressionHelper.GetPropertyName(() => this.TypeLookup) && !(TypeLookup != null ? TypeLookup.Validate() : true) )
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
