using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.VitalForceSheet
{
    public class VFSSecondaryItem : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private VFS _vfs;
        private Item _item;
        private Lookup _sectionLookup;
        private string _comments;
        private bool _checked;
        private int _order;

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
        /// Gets or sets the VFSSIComments.
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
        /// Gets or sets the VFSSIChecked.
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the VFSSIOrder.
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

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the SectionLookup lookup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SectionLookup_PropertyChanged(object sender, PropertyChangedEventArgs e)
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