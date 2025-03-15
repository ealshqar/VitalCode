using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;


namespace Vital.Business.Shared.DomainObjects.Items
{
    public class Item : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _name;
        private string _fullName;
        private string _description;
        private string _memo;
        private Lookup _typeLookup;
        private Lookup _listTypeLookup;
        private Lookup _genderLookup;
        private Lookup _itemSourceLookup;
        private string _key;
        private BindingList<ItemTarget> _itemTargets;
        private BindingList<ItemProperty> _properties;
        private ItemDetails _itemDetail;

        private BindingList<ItemRelation> _children;
        private BindingList<ItemRelation> _parents;

        private int _order;

        private bool _isStarred;
        private bool _isChecked;
        private string _notes;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the item Detail.
        /// </summary>
        public ItemDetails ItemDetail
        {
            get { return _itemDetail; }
            set { _itemDetail = value; }
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the is starred flag.
        /// </summary>
        public bool IsStarred
        {
            get { return _isStarred; }
            set
            {
                if (_isStarred != value)
                {
                    _isStarred = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if( _name != value)
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the Full Name.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the Description.
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
        /// Gets or sets the memo.
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if(_memo != value)
                {
                    _memo = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Get or set the TypeLookup.
        /// </summary>
        public Lookup TypeLookup
        {
            get { return _typeLookup; }
            set
            {
                _typeLookup = value;
                if (_typeLookup == null) return;
                _typeLookup.PropertyChanged += TypeLookup_PropertyChanged;
            }
        }

        /// <summary>
        /// Get or set the ListTypeLookup
        /// </summary>
        public Lookup ListTypeLookup
        {
            get { return _listTypeLookup; }
            set
            {
                _listTypeLookup = value;
                if (_listTypeLookup == null) return;
                _listTypeLookup.PropertyChanged += ListTypeLookup_PropertyChanged;
            }
        }     

        /// <summary>
        /// Get or set the Gender Lookup.
        /// </summary>
        public Lookup GenderLookup
        {
            get { return _genderLookup; }
            set
            {
                _genderLookup = value;
                if (_genderLookup == null) return;
                _genderLookup.PropertyChanged += GenderLookup_PropertyChanged;
            }
        }

        /// <summary>
        /// Get or set the Item Source Lookup.
        /// </summary>
        public Lookup ItemSourceLookup
        {
            get { return _itemSourceLookup; }
            set
            {
                _itemSourceLookup = value;
                if (_itemSourceLookup == null) return;
                _itemSourceLookup.PropertyChanged += GenderLookup_PropertyChanged;
            }
        }

        /// <summary>
        /// Get or set the Key.
        /// </summary>
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// get or set the TargetTypeLookup
        /// </summary>
        public BindingList<ItemTarget> ItemTargets
        {
            get { return _itemTargets; }
            set
            {
                _itemTargets = value;
                _itemTargets.RaiseListChangedEvents = true;
                _itemTargets.ListChanged += Targets_ListChanged;
            }
        }

        /// <summary>
        /// get or set the Properties
        /// </summary>
        public BindingList<ItemProperty> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                //if (_properties == null) return;
                _properties.RaiseListChangedEvents = true;
                _properties.ListChanged += Properties_ListChanged;
            }
        }

        /// <summary>
        /// Get or set the Children.
        /// </summary>
        public BindingList<ItemRelation> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Parents.
        /// </summary>
        public BindingList<ItemRelation> Parents
        {
            get { return _parents; }
            set
            {
                _parents = value;
                _parents.RaiseListChangedEvents = true;
                _parents.ListChanged += Parents_ListChanged;                
            } 
        }

        /// <summary>
        /// Meridian name for points
        /// </summary>
        public string Meridian
        {
            get
            {
                var firstOrDefault = Parents.FirstOrDefault();
                return firstOrDefault != null && firstOrDefault.Child != null ? firstOrDefault.Child.Name : null;
            }
        }
        
        /// <summary>
        /// Gets or sets the Step, this property was added in the BO only since its just to pass the step value.
        /// </summary>
        public int Step
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the is checked for reading flag.
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        /// <summary>
        /// Gets or set the Item Notes.
        /// </summary>
        public string Notes
        {
            get
            {
                if (_notes == null && Properties != null)
                    _notes = Properties.GetPropertyValueAsString(PropertiesEnum.ItemNotes);

                return _notes;
            }
            set { _notes = value; }
        }

        /// <summary>
        /// Gets MineralMGML.
        /// </summary>
        public string MineralMGML
        {
            get { return Properties.GetPropertyValueAsString(PropertiesEnum.MineralMGML); }
        }

        /// <summary>
        /// Gets DosageMinimum.
        /// </summary>
        public string DosageMinimum
        {
            get { return Properties.GetPropertyValueAsString(PropertiesEnum.DosageMinimum); }
        }

        /// <summary>
        /// Gets DosageMaximum.
        /// </summary>
        public string DosageMaximum
        {
            get { return Properties.GetPropertyValueAsString(PropertiesEnum.DosageMaximum); }
        }

        /// <summary>
        /// Gets MineralDosageRange.
        /// </summary>
        public string MineralDosageRange
        {
            get
            {
                if (string.IsNullOrEmpty(DosageMaximum))
                {
                    return "(" + DosageMinimum + "cc)";
                }
                else
                {
                    return "(" + DosageMinimum + "-" + DosageMaximum + "cc)";
                }
            }
        }

        public int IsImprintableId
        {
            get
            {
                int result;
                int.TryParse(Properties.GetPropertyValueAsString(PropertiesEnum.IsImprintable), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets hidden Property.
        /// </summary>
        public bool Hidden { get; set; }

        #endregion

        #region

        /// <summary>
        /// Returns if the item is active
        /// </summary>
        /// <param name="userHiddenItemStateLookupId"></param>
        /// <param name="systemHiddenItemStateLookupId"></param>
        /// <returns></returns>
        public bool IsItemActive(int userHiddenItemStateLookupId, int systemHiddenItemStateLookupId)
        {
            return Properties != null &&
                   !Properties.HasProperty(PropertiesEnum.ItemState, userHiddenItemStateLookupId.ToString()) &&
                   !Properties.HasProperty(PropertiesEnum.ItemState, systemHiddenItemStateLookupId.ToString());
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void ListTypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ListTypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ListTypeLookup, () => ListTypeLookup.Id));
        }

        /// <summary>
        /// Handles the property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void GenderLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => GenderLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => GenderLookup, () => GenderLookup.Id));
        }

        /// <summary>
        /// Handles the property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void TypeLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => TypeLookup), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => TypeLookup, () => TypeLookup.Id));
        }

        /// <summary>
        /// Notifies the change of the parents list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Parents_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Parents), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Parents));
        }

        /// <summary>
        /// Notifies the change of the Properties list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Properties_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Properties), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Properties));
        }

        /// <summary>
        /// Notifies the change of the targets list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Targets_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => ItemTargets), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => ItemTargets));
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
                IsRequiredStringPropertyValid(propertyName, this.Name, info);
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => this.TypeLookup) &&  !TypeLookup.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => this.ListTypeLookup) && !ListTypeLookup.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => this.GenderLookup) && !GenderLookup.Validate()))
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
