using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.DomainObjects.Tests;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Patients
{
    public class Patient : DomainEntity , IDXDataErrorInfo
    {
        #region Private Variables

        private int  _number;
        private string _firstName;
        private string _lastName;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        private string _homePhone;
        private string _workPhone;
        private string _cellPhone;
        private string _fax;
        private string _email;
        private string _notes;
        private DateTime ? _dateOfBirth;
        private Lookup _genderLookup;
        private BindingList<Test> _tests;
        private BindingList<SpotCheck> _spotChecks;
        private BindingList<FrequencyTest> _frequencyTests;
        private BindingList<PatientHistory> _patientHistory;
        private BindingList<ShippingOrders.ShippingOrder> _shippingOrders;
        private BindingList<VFS> _vfsRecords;
        private BindingList<AutoTest> _autoTests;

        #endregion

        #region Constructors

        /// <summary>
        /// Initilizes the gender lookup.
        /// </summary>
        public Patient()
        {
            _genderLookup = new Lookup();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gender Enum
        /// </summary>
        public GenderEnum GenderEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<GenderEnum>(GenderLookup.Value);
            }
        }

        /// <summary>
        /// Gets or sets the Tests.
        /// </summary>
        public BindingList<Test> Tests
        {
            get { return _tests; }
            set
            {
                _tests = value;
                /*Anas: This was disabled since changes in the test don't need to notify parent patient or set its state to modified
                if notification needed, we should at least disable state setting to modified.
                SetModifiedState(MethodBase.GetCurrentMethod().Name);*/
            }
        }

        /// <summary>
        /// Gets or sets the shipping orders.
        /// </summary>
        public BindingList<ShippingOrders.ShippingOrder> ShippingOrders
        {
            get { return _shippingOrders; }
            set
            {
                _shippingOrders = value;
            }
        }

        /// <summary>
        /// Gets or sets the VFS records.
        /// </summary>
        public BindingList<VFS> VFSRecords
        {
            get { return _vfsRecords; }
            set
            {
                _vfsRecords = value;
            }
        }

        /// <summary>
        /// Gets or sets the Tests.
        /// </summary>
        public BindingList<SpotCheck> SpotChecks
        {
            get { return _spotChecks; }
            set
            {
                _spotChecks = value;
            }
        }

        /// <summary>
        /// Gets or sets the Tests.
        /// </summary>
        public BindingList<FrequencyTest> FrequencyTests
        {
            get { return _frequencyTests; }
            set
            {
                _frequencyTests = value;
            }
        }

        /// <summary>
        /// Gets or sets the patient history.
        /// </summary>
        public BindingList<PatientHistory> PatientHistory
        {
            get { return  _patientHistory; }
            set
            {
                _patientHistory = value;
                _patientHistory.RaiseListChangedEvents = true;
                _patientHistory.ListChanged += new ListChangedEventHandler(PatientHistory_ListChanged);
                
            }
        }

        /// <summary>
        /// Gets or sets the AutoTests records.
        /// </summary>
        public BindingList<AutoTest> AutoTests
        {
            get { return _autoTests; }
            set
            {
                _autoTests = value;
            }
        }

        /// <summary>
        /// Notifies the change of the history list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PatientHistory_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => this.PatientHistory), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => this.PatientHistory));
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public int Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FullNameAndNumber
        {
            get { return _number + ", " + _firstName + " " + _lastName; }
        }

        /// <summary>
        /// Gets the full name
        /// </summary>
        public string FullName
        {
            get
            {
                return _lastName == string.Empty? _firstName: _firstName + " " + _lastName;
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Address 1.
        /// </summary>
        public string Address1
        {
            get { return _address1; }
            set
            {
                if (_address1 != value)
                {
                    _address1 = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2
        {
            get { return _address2; }
            set
            {
                if (_address2 != value)
                {
                    _address2 = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                if(_city != value)
                {
                    _city = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient state.
        /// </summary>
        public string State
        {
            get { return _state; }
            set
            {
                if(_state != value)
                {
                    _state = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ZIP code.
        /// </summary>
        public string Zip
        {
            get { return _zip; }
            set
            {
                if(_zip != value)
                {
                    _zip = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        public string HomePhone
        {
            get { return _homePhone; }
            set
            {
                if(_homePhone != value)
                {
                    _homePhone = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the work phone.
        /// </summary>
        public string WorkPhone
        {
            get { return _workPhone; }
            set
            {
                if (_workPhone != value)
                {
                    _workPhone = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the cell phone.
        /// </summary>
        public string CellPhone
        {
            get { return _cellPhone; }
            set
            {
                if(_cellPhone != value)
                {
                    _cellPhone = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        public string Fax
        {
            get { return _fax; }
            set
            {
                if (_fax != value)
                {
                    _fax = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public DateTime ? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Gender Lookup
        /// </summary>
        public Lookup GenderLookup
        {
            get { return _genderLookup; }
            set
            {
                _genderLookup = value;

                if (_genderLookup == null) return;
                _genderLookup.PropertyChanged += new PropertyChangedEventHandler(GenderLookoup_PropertyChanged);      
            }
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Notifies the change of the state for the gender lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GenderLookoup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => this.GenderLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => this.GenderLookup, () => this.GenderLookup.Id));            
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
            if(propertyName == ExpressionHelper.GetPropertyName(() => FirstName))
            {
                IsRequiredStringPropertyValid(propertyName, FirstName, info);
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => State) && State != null && State.Length < 2 && State.Length > 0)
            {
                info.ErrorText = StaticKeys.ValidationMessageMinimumLength;
                info.ErrorType = ErrorType.Warning;
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => GenderLookup) && !GenderLookup.Validate())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => PatientHistory) && !ValidateHistory())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => DateOfBirth))
            {
                if(DateOfBirth.HasValue)
                    IsDatePropertyValid(propertyName, DateOfBirth.Value  , info);
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

            foreach (var property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }          


            return IsValid;
        }

        /// <summary>
        /// Validates the history list.
        /// </summary>
        /// <returns></returns>
        public bool ValidateHistory()
        {
            return PatientHistory == null || PatientHistory.All(patientHistory => patientHistory.Validate());
        }

        #endregion
    }
}
