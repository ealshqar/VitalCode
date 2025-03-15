using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.VitalForceSheet
{
    public class VFSItem : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private VFS _vfs;
        private VFSItemSource _vfsItemSource;
        private Item _item;
        private Lookup _sectionLookup;
        private Lookup _groupLookup;
        private Lookup _gridGroupLookup;
        private string _previousV1;
        private string _previousV2;
        private string _currentV1;
        private string _currentV2;
        private bool _isSkipped;
        private string _comments;		

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Vfs.
        /// </summary>
        public VFS VFS
        {
            get { return _vfs; }
            set
            {
                _vfs = value;
                if (_vfs == null) return;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the VFSItemSource.
        /// </summary>
        public VFSItemSource VFSItemSource
        {
            get { return _vfsItemSource; }
            set
            {
                _vfsItemSource = value;
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
                if (_item == null) return;
                _item.PropertyChanged += Item_PropertyChanged;
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
        /// Gets or sets the PreviousV1.
        /// </summary>
        public string PreviousV1
        {
            get { return _previousV1; }
            set
            {
                if (_previousV1 != value)
                {
                    _previousV1 = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the PreviousV1.
        /// </summary>
        public string PreviousV2
        {
            get { return _previousV2; }
            set
            {
                if (_previousV2 != value)
                {
                    _previousV2 = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CurrentV1.
        /// </summary>
        public string CurrentV1
        {
            get { return _currentV1; }
            set
            {
                if (_currentV1 != value)
                {
                    _currentV1 = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CurrentV2.
        /// </summary>
        public string CurrentV2
        {
            get { return _currentV2; }
            set
            {
                if (_currentV2 != value)
                {
                    _currentV2 = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsSkipped.
        /// </summary>
        public bool IsSkipped
        {
            get { return _isSkipped; }
            set
            {
                if (_isSkipped != value)
                {
                    _isSkipped = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Returns true if the current VFS item is on fly item
        /// </summary>
        public bool IsOnFlyItem
        {
            get
            {
                return VFSItemSource == null;
            }
        }

        /// <summary>
        /// Ideal Range 1
        /// </summary>
        public string IdealRange1
        {
            get
            {
                return VFSItemSource == null ||
                ( VFSItemSource.V1MinIdeal == 0 &&
                  VFSItemSource.V1MaxIdeal == 0) ? string.Empty : VFSItemSource.V1MinIdeal.ToString("G29") + " - " + VFSItemSource.V1MaxIdeal.ToString("G29");
            }
        }

        /// <summary>
        /// Ideal Range 2
        /// </summary>
        public string IdealRange2
        {
            get
            {
                return VFSItemSource == null ||
                (VFSItemSource.V2MinIdeal == 0 &&
                  VFSItemSource.V2MaxIdeal == 0) ? string.Empty : VFSItemSource.V2MinIdeal.ToString("G29") + " - " + VFSItemSource.V2MaxIdeal.ToString("G29");
            }
        }

        /// <summary>
        /// Returns Current Value 2 instead of Current Value 1 in specific case
        /// </summary>
        public string CurrentValueNoLookup
        {
            get
            {
                return !IsOnFlyItem &&
                        VFSItemSource.V1TypeLookup != null &&
                        EnumNameResolver.LookupAsEnum<VFSSourceItemValueType>(VFSItemSource.V1TypeLookup.Value) == VFSSourceItemValueType.Lookup
                        ? CurrentV2:CurrentV1;             
            }
        }

        /// <summary>
        /// Returns Previous Value 2 instead of Previous Value 1 in specific case
        /// </summary>
        public string PreviousValueNoLookup
        {
            get
            {
                return !IsOnFlyItem &&
                        VFSItemSource.V1TypeLookup != null &&
                        EnumNameResolver.LookupAsEnum<VFSSourceItemValueType>(VFSItemSource.V1TypeLookup.Value) == VFSSourceItemValueType.Lookup
                        ? PreviousV2 : PreviousV1;
            }
        }

        #endregion

        #region Events Handlers

        /// <summary>
        /// Notifies the change of the state for the item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Item), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Item));
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
