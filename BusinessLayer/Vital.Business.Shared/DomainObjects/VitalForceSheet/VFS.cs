using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.Business.Shared.DomainObjects.VitalForceSheet
{
    public class VFS : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Patient _patient;
        private string _name;
        private DateTime _dateTime;
        private int _thyroidNumOfIssues;
        private int _mercuryNumOfIssues;
        private string _emotionalIssues;
        private string _notes;

        private Test _test;

        private BindingList<VFSItem> _vfsItems;
        private BindingList<VFSSecondaryItem> _vfsSecondaryItems;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Test id.
        /// </summary>
        public int? TestId { set; get; }

        /// <summary>
        /// Gets or sets the patient.
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
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the VFS date and time.
        /// </summary>
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ThyroidNumOfIssues.
        /// </summary>
        public int ThyroidNumOfIssues
        {
            get { return _thyroidNumOfIssues; }
            set
            {
                if (_thyroidNumOfIssues != value)
                {
                    _thyroidNumOfIssues = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the MercuryNumOfIssues.
        /// </summary>
        public int MercuryNumOfIssues
        {
            get { return _mercuryNumOfIssues; }
            set
            {
                if (_mercuryNumOfIssues != value)
                {
                    _mercuryNumOfIssues = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the EmotionalIssues.
        /// </summary>
        public string EmotionalIssues
        {
            get { return _emotionalIssues; }
            set
            {
                if (_emotionalIssues != value)
                {
                    _emotionalIssues = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Test.
        /// </summary>
        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                if (_test == null) return;
                //Setting test object value shouldn't modify the state of the VFS object
                //SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the SectionLookup.
        /// </summary>
        public BindingList<VFSSecondaryItem> VfsSecondaryItems
        {
            get { return _vfsSecondaryItems; }
            set
            {
                _vfsSecondaryItems = value;
                _vfsSecondaryItems.RaiseListChangedEvents = true;
                //This line is commented because we handle VFS secondary item changes in a custom way in UI since we have 4 groups of secondary items
                //_vfsSecondaryItems.ListChanged += VfsSecondaryItems_ListChanged;
            }
        }

        /// <summary>
        /// Gets or sets the VfsItems.
        /// </summary>
        public BindingList<VFSItem> VfsItems
        {
            get { return _vfsItems; }
            set
            {
                _vfsItems = value;
                _vfsItems.RaiseListChangedEvents = true;
                _vfsItems.ListChanged += VfsItems_ListChanged;
            }
        }

        /// <summary>
        /// Primary Issues
        /// </summary>
        public BindingList<VFSSecondaryItem> PrimaryIssues
        {
            get
            {
                return
                    VfsSecondaryItems.Where(
                        s =>
                            EnumNameResolver.StringAsEnum<VFSSecondarySection>(s.SectionLookup.Value) ==
                            VFSSecondarySection.PrimaryIssues).ToBindingList();
            }
        }

        /// <summary>
        /// Secondary Issues
        /// </summary>
        public BindingList<VFSSecondaryItem> SecondaryIssues
        {
            get
            {
                return
                    VfsSecondaryItems.Where(
                        s =>
                            EnumNameResolver.StringAsEnum<VFSSecondarySection>(s.SectionLookup.Value) ==
                            VFSSecondarySection.SecondaryIssues).ToBindingList();
            }
        }

        /// <summary>
        /// Thyroid Issues
        /// </summary>
        public BindingList<VFSSecondaryItem> ThyroidIssues
        {
            get
            {
                return
                    VfsSecondaryItems.Where(
                        s =>
                            EnumNameResolver.StringAsEnum<VFSSecondarySection>(s.SectionLookup.Value) ==
                            VFSSecondarySection.ThyroidIssues).ToBindingList();
            }
        }


        /// <summary>
        /// Mercury Issues
        /// </summary>
        public BindingList<VFSSecondaryItem> MercuryIssues
        {
            get
            {
                return
                    VfsSecondaryItems.Where(
                        s =>
                            EnumNameResolver.StringAsEnum<VFSSecondarySection>(s.SectionLookup.Value) ==
                            VFSSecondarySection.MercuryIssues).ToBindingList();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the VFS item current value 1
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string GetVFSItemCurrentValue1ByItemName(string itemName, int sectionId, int groupId)
        {
            var vfsItem = VfsItems.FirstOrDefault(vfsi =>
                vfsi.Item != null &&
                !vfsi.IsOnFlyItem &&
                vfsi.SectionLookup != null && vfsi.SectionLookup.Id == sectionId &&
                vfsi.GroupLookup != null && vfsi.GroupLookup.Id == groupId &&
                vfsi.Item.Name == itemName);
            return vfsItem == null ? string.Empty : vfsItem.CurrentV1;
        }

        #endregion

        #region Events Handlers

        /// <summary>
        /// Notifies the change of the VfsSecondaryItems list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VfsSecondaryItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            //GetPropertyError(ExpressionHelper.GetPropertyName(() => VfsSecondaryItems), new ErrorInfo());
            //SetModifiedState(ExpressionHelper.GetPropertyName(() => VfsSecondaryItems));
        }


        /// <summary>
        /// Notifies the change of the VfsSecondaryItems list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VfsItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => VfsItems), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => VfsItems));
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