using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;

namespace Vital.Business.Shared.DomainObjects.SpotChecks
{
    public class SpotCheckResult : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private SpotCheck _spotCheck;
        private Item _item;
        private bool _yesNo;
        private int _numberOfBags;
        private int _numberOfWeeks;
        private int _dosage;
        private string _notes;
        private Lookup _resultType;
 
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Result Type.
        /// </summary>
        public Lookup ResultType
        {
            get { return _resultType; }
            set
            {
                _resultType = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the dosage.
        /// </summary>
        public int Dosage
        {
            get { return _dosage; }
            set
            {
                _dosage = value;
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

        /// <summary>
        /// Gets or sets the Number Of Bags.
        /// </summary>
        public int NumberOfBags
        {
            get { return _numberOfBags; }
            set
            {
                _numberOfBags = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the YesNo.
        /// </summary>
        public bool YesNo
        {
            get { return _yesNo; }
            set
            {
                _yesNo = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
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
                _notes = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the Spot Check.
        /// </summary>
        public SpotCheck SpotCheck
        {
            get { return _spotCheck; }
            set
            {
                _spotCheck = value;
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

        #endregion
        
        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {

        }

        /// <summary>
        /// Gets the errors by updating the Error Info.
        /// </summary>
        /// <param name="info"></param>
        public void GetError(ErrorInfo info)
        {

        }

        #endregion
    }
}
