using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.VitalForceSheet
{
    public class VFSItemSource : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Item _item;
        private Lookup _sectionLookup;
        private Lookup _groupLookup;
        private Lookup _gridGroupLookup;
        private Lookup _genderLpLookup;
        private Lookup _v1TypeLookup;
        private Lookup _v2TypeLookup;

        private decimal _v1Min;
        private decimal _v1Max;
        private decimal _v1MinIdeal;
        private decimal _v1MaxIdeal;

        private decimal _v2Min;
        private decimal _v2Max;
        private decimal _v2MinIdeal;
        private decimal _v2MaxIdeal;
        private string _startingValue1;
        private string _startingValue2;
        private bool _isActive;

        private string _v1LookupType;
        private string _v2LookupType;
        private int _testingOrder;

        private bool _hasPreviousV1;
        private bool _hasPreviousV2;
        private bool _hasCurrentV1;
        private bool _hasCurrentV2;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the v1 min
        /// </summary>
        public decimal V1Min
        {
            get { return _v1Min; }
            set
            {
                _v1Min = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the v2 min
        /// </summary>
        public decimal V2Min
        {
            get { return _v2Min; }
            set
            {
                _v2Min = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the v1 max
        /// </summary>
        public decimal V1Max
        {
            get { return _v1Max; }
            set
            {
                _v1Max = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the v2 max
        /// </summary>
        public decimal V2Max
        {
            get { return _v2Max; }
            set
            {
                _v2Max = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the value 1 min ideal
        /// </summary>
        public decimal V1MinIdeal
        {
            get { return _v1MinIdeal; }
            set
            {
                _v1MinIdeal = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the value 2 min ideal
        /// </summary>
        public decimal V2MinIdeal
        {
            get { return _v2MinIdeal; }
            set
            {
                _v2MinIdeal = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the value 1 max ideal
        /// </summary>
        public decimal V1MaxIdeal
        {
            get { return _v1MaxIdeal; }
            set
            {
                _v1MaxIdeal = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the value 2 max ideal
        /// </summary>
        public decimal V2MaxIdeal
        {
            get { return _v2MaxIdeal; }
            set
            {
                _v2MaxIdeal = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the starting value 1
        /// </summary>
        public string StartingValu1
        {
            get { return _startingValue1; }
            set
            {
                _startingValue1 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the starting value 2
        /// </summary>
        public string StartingValu2
        {
            get { return _startingValue2; }
            set
            {
                _startingValue2 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the v2 lookup type
        /// </summary>
        public string V2LookupType
        {
            get { return _v2LookupType; }
            set
            {
                _v2LookupType = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the v1 lookup type
        /// </summary>
        public string V1LookupType
        {
            get { return _v1LookupType; }
            set
            {
                _v1LookupType = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the testing order.
        /// </summary>
        public int TestingOrder
        {
            get { return _testingOrder; }
            set
            {
                _testingOrder = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the has previous v1 flag.
        /// </summary>
        public bool HasPreviousV1
        {
            get { return _hasPreviousV1; }
            set
            {
                _hasPreviousV1 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the has previous v2 flag.
        /// </summary>
        public bool HasPreviousV2
        {
            get { return _hasPreviousV2; }
            set
            {
                _hasPreviousV2 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the has current v1 flag.
        /// </summary>
        public bool HasCurrentV1
        {
            get { return _hasCurrentV1; }
            set
            {
                _hasCurrentV1 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the has current v2 flag.
        /// </summary>
        public bool HasCurrentV2
        {
            get { return _hasCurrentV2; }
            set
            {
                _hasCurrentV2 = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the SectionLookup.
        /// </summary>
        public Lookup SectionLookup
        {
            get { return _sectionLookup; }
            set
            {
                _sectionLookup = value;
                if (_sectionLookup == null) return;
                _sectionLookup.PropertyChanged += new PropertyChangedEventHandler(SectionLookup_PropertyChanged);
            }
        }

        /// <summary>
        /// Gets or sets the Group Lookup.
        /// </summary>
        public Lookup GroupLookup
        {
            get { return _groupLookup; }
            set
            {
                _groupLookup = value;
                if (_groupLookup == null) return;
                _groupLookup.PropertyChanged += new PropertyChangedEventHandler(GroupLookup_PropertyChanged);
            }
        }

        /// <summary>
        /// Gets or sets the grid Group Lookup.
        /// </summary>
        public Lookup GridGroupLookup
        {
            get { return _gridGroupLookup; }
            set
            {
                _gridGroupLookup = value;
                if (_gridGroupLookup == null) return;
                _gridGroupLookup.PropertyChanged += new PropertyChangedEventHandler(GridGroupLookup_PropertyChanged);
            }
        }

        /// <summary>
        /// Gets or sets the GenderLookup.
        /// </summary>
        public Lookup GenderLookup
        {
            get { return _genderLpLookup; }
            set
            {
                _genderLpLookup = value;
                if (_genderLpLookup == null) return;
                _genderLpLookup.PropertyChanged += new PropertyChangedEventHandler(GenderLookup_PropertyChanged);
            }
        }

        /// <summary>
        /// Gets or sets the Value 1 Type Lookup.
        /// </summary>
        public Lookup V1TypeLookup
        {
            get { return _v1TypeLookup; }
            set
            {
                _v1TypeLookup = value;
                if (_v1TypeLookup == null) return;
                _v1TypeLookup.PropertyChanged += new PropertyChangedEventHandler(V1TypeLookup_PropertyChanged);
            }
        }

        /// <summary>
        /// Gets or sets the Value 2 Type Lookup.
        /// </summary>
        public Lookup V2TypeLookup
        {
            get { return _v2TypeLookup; }
            set
            {
                _v2TypeLookup = value;
                if (_v2TypeLookup == null) return;
                _v2TypeLookup.PropertyChanged += new PropertyChangedEventHandler(V2TypeLookup_PropertyChanged);
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the V2TypeLookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V2TypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => V2TypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => V2TypeLookup, () => V2TypeLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the V1TypeLookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V1TypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => V1TypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => V1TypeLookup, () => V1TypeLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the GenderLookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GenderLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => GenderLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => GenderLookup, () => GenderLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the GroupLookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GroupLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => GroupLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => GroupLookup, () => GroupLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the GridGroupLookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GridGroupLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => GridGroupLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => GridGroupLookup, () => GridGroupLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the SectionLookup lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SectionLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => SectionLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => SectionLookup, () => SectionLookup.Id));
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