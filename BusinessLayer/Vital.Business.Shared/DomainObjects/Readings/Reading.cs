using System;
using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Readings
{
    public class Reading : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private DateTime _dateTime;
        private int _min;
        private int _value;
        private int _fall;
        private int _max;
        private int _rise;
        private Test _test;
        private Item _item;
        private int? _pointSetItemId;
        private int? _listPointLookupId;
        private int _valueBalanced;

        #endregion
        
        #region Constructor

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the reading date and time
        /// </summary>
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if(_dateTime != value)
                {
                    _dateTime = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value balanced
        /// </summary>
        public int ValueBalanced
        {
            get { return _valueBalanced; }
            set
            {
                if (_valueBalanced != value)
                {
                    _valueBalanced = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the item
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
        /// Gets or sets the min.
        /// </summary>
        public int Min
        {
            get { return _min; }
            set
            {
                if(_min != value)
                {
                    _min = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value
        {
            get { return _value;}
            set
            {
                if(_value != value)
                {
                    _value = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the fall.
        /// </summary>
        public int Fall
        {
            get { return _fall; }
            set
            {
                if(_fall != value)
                {
                    _fall = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        public int Max
        {
            get { return _max; }
            set
            {
                if(_max != value)
                {
                    _max = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the rise.
        /// </summary>
        public int Rise
        {
            get { return _rise; }
            set
            {
                if(_rise != value)
                {
                    _rise = value;
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
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// PointSetItemId
        /// </summary>
        public int? PointSetItemId
        {
            get
            {
                return _pointSetItemId;
            }
            set
            {
                _pointSetItemId = value;
            }
        }

        /// <summary>
        /// ListPointLookupId
        /// </summary>
        public int? ListPointLookupId
        {
            get
            {
                return _listPointLookupId;
            }
            set
            {
                _listPointLookupId = value;
            }
        }

        #endregion

        #region Mehtods

        /// <summary>
        /// Clone the current Reading.
        /// </summary>
        /// <returns></returns>
        public Reading Clone()
        {
            return new Reading()
                       {
                           DateTime = _dateTime,
                           Min = _min,
                           Value = _value,
                           Fall = _fall,
                           Max = _max,
                           Rise = _rise,
                           Test = _test,
                           Item = _item,
                           PointSetItemId = _pointSetItemId,
                           ListPointLookupId = _listPointLookupId
                       };
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
