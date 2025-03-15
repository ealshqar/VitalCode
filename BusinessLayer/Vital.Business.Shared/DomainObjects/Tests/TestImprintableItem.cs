using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Tests
{
    public class TestImprintableItem : DomainEntity ,  IDXDataErrorInfo
    {
        #region Private Variables
        
        private Test _test;
        private Item _item;
        private TestImprintableItem _parent;
        private TestResult _testResult;
        private bool _isChecked;
        private bool _isImprinted;
        private int _order;
        private string _comments;
        private int _tempId;
        private int _level;
        private int _imprintingIndex;
        private int? _parentImprintableId;
        private int _levelAfterDelete;
        private TestImprintableItem _tempParentAfterDelete;

        #endregion
        
        #region Public Properties
                
        /// <summary>
        /// Gets or sets the Test.
        /// </summary>
        public Test Test
        {
            get { return  _test; }
            set
            {
                _test = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public Item Item
        {
            get { return  _item; }
            set
            {
                _item = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the Parent.
        /// </summary>
        public TestImprintableItem Parent
        {
            get { return  _parent; }
            set
            {
                _parent = value;
                
                //If the item is new, the temp parent can be the same as the normal parent because there is no parent stored in DB to be handled
                if (ObjectState == DomainEntityState.New)
                {
                    TempParentAfterDelete = _parent;
                }

                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Gets or sets the TestResult.
        /// </summary>
        public TestResult TestResult
        {
            get { return  _testResult; }
            set
            {
                _testResult = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the IsChecked.
        /// </summary>
        public bool IsChecked
        {
            get { return  _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }    
            }
        }
        
        /// <summary>
        /// Gets or sets the IsImprinted.
        /// </summary>
        public bool IsImprinted
        {
            get { return  _isImprinted; }
            set
            {
                if (_isImprinted != value)
                {
                    _isImprinted = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }    
            }
        }
        
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order
        {
            get { return  _order; }
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
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments
        {
            get { return  _comments; }
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
        /// Gets the parent Id
        /// </summary>
        public int? ParentId
        {
            get
            {
                return Parent == null ? (int?) null : Parent.TempId;
            }
        }

        public int? ParentImprintableId
        {
            get
            {
                return _parentImprintableId;
            }
            set
            {
                _parentImprintableId = value;
            }
        }

        /// <summary>
        /// Returns the image index based on the imprinting state
        /// </summary>
        public int ImageIndex
        {
            get
            {
                return IsImprinted ? 1 : 0;
            }
        }

        /// <summary>
        /// Returns the ID or a temp value if the ID is 0
        /// </summary>
        public int TempId
        {
            get
            {
                return Id == 0 ? _tempId : Id;
            }
            set
            {
                _tempId = value;
            }
        }

        /// <summary>
        /// A value in memory to determine level of the item
        /// </summary>
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        /// <summary>
        /// A value in memory to determine the order of the item during imprinting
        /// </summary>
        public int ImprintingIndex
        {
            get
            {
                return _imprintingIndex;
            }
            set
            {
                _imprintingIndex = value;
            }
        }

        /// <summary>
        /// Gets if the item is in the root
        /// </summary>
        /// <returns></returns>
        public bool IsRootItem()
        {
            return Parent == null;
        }

        /// <summary>
        /// Gets the parent Id when the record is deleted
        /// </summary>
        public int? DeletedParentId
        {
            get
            {
                return TempParentAfterDelete == null ? (int?)null : TempParentAfterDelete.TempId;
            }
        }

        /// <summary>
        /// Gets if the item is in the root
        /// </summary>
        /// <returns></returns>
        public bool IsRootItemAfterDelete()
        {
            return TempParentAfterDelete == null;
        }

        /// <summary>
        /// A value in memory to determine level of the item after delete
        /// </summary>
        public int LevelAfterDelete
        {
            get
            {
                return _levelAfterDelete;
            }
            set
            {
                _levelAfterDelete = value;
            }
        }

        /// <summary>
        /// Gets or sets the temp Parent that is used for after delete actions.
        /// </summary>
        public TestImprintableItem TempParentAfterDelete
        {
            get { return _tempParentAfterDelete; }
            set
            {
                _tempParentAfterDelete = value;
            }
        }

        #endregion
        
        #region Public Events
                
        
        
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