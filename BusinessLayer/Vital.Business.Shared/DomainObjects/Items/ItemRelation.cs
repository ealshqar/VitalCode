using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Items
{
    public class ItemRelation : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private Item _parent;
        private Item _child;
        private Lookup _relationType;
        private BindingList<ItemRelationProperty> _properties;
        private int _order;
        private int _step;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the Parent.
        /// </summary>
        public Item Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Get or set the Child
        /// </summary>
        public Item Child
        {
            get { return _child; }
            set
            {
                _child = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }
        
        /// <summary>
        /// Get or set the TypeLookup.
        /// </summary>
        public Lookup RelationType
        {
            get { return _relationType; }
            set
            {
                _relationType = value;
                if (_relationType == null) return;
                _relationType.PropertyChanged += RelationType_PropertyChanged;
            }
        }

        /// <summary>
        /// get or set the Properties
        /// </summary>
        public BindingList<ItemRelationProperty> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                _properties.RaiseListChangedEvents = true;
                _properties.ListChanged += Properties_ListChanged;
            }
        }

        /// <summary>
        /// Get or set the Order
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        public int Step
        {
            get { return _step; }
            set
            {
                _step = value;
                SetModifiedState(MethodBase.GetCurrentMethod().Name);
            }  
        }

        public bool Checked
        {
            get; set;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handel RelationType property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RelationType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => RelationType), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => RelationType, () => RelationType.Id));
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
            if (!IsChanged) return true;

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
