using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.PatientSchedules
{
    public class TestSchedule : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _technical;
        private string _address;
        private string _city;
        private string _state;
        private string _zip;
        private string _phone;
        private decimal _fee;
        private decimal _tax;
        private int _reevalInWeeks;
        private string _notes;
        private string _specialInstructions;
        private bool _isCash;
        private bool _isCheck;
        private bool _isCreditCard;
        private Lookup _evalPeriodType;
        private Lookup _discountApply;
        private decimal _discount;
        private bool _discountAsPercentage;

        private BindingList<ScheduleLine> _scheduleLines;
        private string _checkNumber;

        #endregion

        #region Constructors

        public TestSchedule()
        {
            _evalPeriodType = new Lookup();
        }

        #endregion

        #region Public Properties
       
        public BindingList<ScheduleLine> ScheduleLines
        {
            get { return _scheduleLines; }
            set
            {
                _scheduleLines = value;
                _scheduleLines.RaiseListChangedEvents = true;
                _scheduleLines.ListChanged += new ListChangedEventHandler(ScheduleLines_ListChanged);
            }
        }

        public bool IsCash
        {
            get { return _isCash; }
            set
            {
                if (_isCash != value)
                {
                    _isCash = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                if (_isCheck != value)
                {
                    _isCheck = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string CheckNumber
        {
            get { return _checkNumber; }
            set
            {
                if (_checkNumber != value)
                {
                    _checkNumber = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public bool IsCreditCard
        {
            get { return _isCreditCard; }
            set
            {
                if (_isCreditCard != value)
                {
                    _isCreditCard = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string Technical
        {
            get { return _technical; }
            set
            {
                if (_technical != value)
                {
                    _technical = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string Address
        {
            get { return _address;} 
            set
            {
                if(_address != value)
                {
                    _address = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string City
        { 
            get{ return _city; }
            set
            {
                if(_city != value)
                {
                    _city = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

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

        public string Phone
        {
            get { return _phone; }
            set
            {
                if(_phone != value)
                {
                    _phone = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public decimal Tax
        {
            get { return _tax; }
            set
            {
                if (_tax != value)
                {
                    _tax = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public int ReevalInWeeks
        {
            get { return _reevalInWeeks; }
            set
            {
                if(_reevalInWeeks != value)
                {
                    _reevalInWeeks = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                if(_notes != value)
                {
                    _notes = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string SpecialInstructions
        {
            get { return _specialInstructions; }
            set
            {
                if(_specialInstructions != value)
                {
                    _specialInstructions = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public Lookup EvalPeriodType
        {
            get { return _evalPeriodType; }
            set
            {
                _evalPeriodType = value;
                if (_evalPeriodType == null) return;
                _evalPeriodType.PropertyChanged += EvalPeriodType_PropertyChanged;
            }
        }

        public decimal Discount
        {
            get { return _discount; }
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public bool DiscountAsPercentage
        {
            get { return _discountAsPercentage; }
            set
            {
                if (_discountAsPercentage != value)
                {
                    _discountAsPercentage = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public Lookup DiscountApply
        {
            get { return _discountApply; }
            set
            {
                _discountApply = value;
                if (_discountApply == null) return;
                _discountApply.PropertyChanged += _discountApply_PropertyChanged;
            }
        }

        #endregion

        #region Handlers

        private void ScheduleLines_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => this.ScheduleLines), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => this.ScheduleLines));
        }

        /// <summary>
        /// Notifies the change of the state for the state lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvalPeriodType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => EvalPeriodType), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => EvalPeriodType, () => EvalPeriodType.Id));
        }

        /// <summary>
        /// Notifies the change of the lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _discountApply_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {            
            GetPropertyError(ExpressionHelper.GetPropertyName(() => DiscountApply), new ErrorInfo());
            SetModifiedStateLookup(ExpressionHelper.GetPropertyName(() => DiscountApply, () => DiscountApply.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => ScheduleLines) && !ValidateScheduleLines())
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

            foreach (var property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }


            return IsValid;
        }

        /// <summary>
        /// Validates the schedule lines list.
        /// </summary>
        /// <returns></returns>
        public bool ValidateScheduleLines()
        {
            return ScheduleLines.All(scheduleLine => scheduleLine.Validate());
        }

        #endregion
    }
}
