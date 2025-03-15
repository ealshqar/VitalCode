using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.DomainObjects.PatientSchedules
{
    public class ScheduleLine : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Item _item;
        private TestSchedule _testSchedule;

        private string _notes;
        private string _duration;
        private string _toBeShipped;
        private string _whenArising;
        private string _breakfast;
        private string _betweenMealsEarly;
        private string _lunch;
        private string _betweenMealsLate;
        private string _dinner;
        private string _beforeSleep;
        private string _noPerBottle;
        private string _noOfBottle;
        private decimal? _price;
        private bool _isDeleted;
        private bool _include = true;
        private bool _selected;

        #endregion

        #region Public Properties

        /// <summary>
        /// Selected flag.
        /// </summary>
        public bool Selected
        {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public Item Item
        {
            get { return _item; }
            set
            {
                if(_item != value)
                {
                    _item = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public TestSchedule TestSchedule
        {
            get { return _testSchedule; }
            set
            {
                if(_testSchedule != value)
                {
                    _testSchedule = value;
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

        public string Duration
        {
            get { return _duration; }
            set
            {
                if(_duration != value)
                {
                    _duration = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string ToBeShipped
        {
            get { return _toBeShipped; }
            set
            {
                if(_toBeShipped != value)
                {
                    _toBeShipped = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string WhenArising
        {
            get { return _whenArising; }
            set
            {
                if(_whenArising != value)
                {
                    _whenArising = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string Breakfast
        {
            get { return _breakfast; }
            set
            {
                if(_breakfast != value)
                {
                    _breakfast = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string BetweenMealsEarly
        {
            get { return _betweenMealsEarly; }
            set
            {
                if(_betweenMealsEarly != value)
                {
                    _betweenMealsEarly = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string Lunch
        {
            get { return _lunch; }
            set
            {
                if(_lunch != value)
                {
                    _lunch = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string BetweenMealsLate
        {
            get { return _betweenMealsLate; }
            set
            {
                if(_betweenMealsLate != value)
                {
                    _betweenMealsLate = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string Dinner
        {
            get { return _dinner; }
            set
            {
                if(_dinner != value)
                {
                    _dinner = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string BeforeSleep
        {
            get { return _beforeSleep; }
            set
            {
                if(_beforeSleep != value)
                {
                    _beforeSleep = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string NoPerBottle
        {
            get { return _noPerBottle; }
            set
            {
                if(_noPerBottle != value)
                {
                    _noPerBottle = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public string NoOfBottle
        {
            get { return _noOfBottle; }
            set
            {
                if(_noOfBottle != value)
                {
                    _noOfBottle = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public decimal? Price
        {
            get { return _price; }
            set
            {
                if(_price != value)
                {
                    _price = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets Is Deleted value, this flag is to determine if current schedule line is deleted or not.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                if (_isDeleted == value)
                    return;

                _isDeleted = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        public bool Include
        {
            get
            {
                return _include;
            }
            set
            {               
                _include = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion

        #region  Validation

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

            foreach (var property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }


            return IsValid;
        }

        #endregion
    }
}
