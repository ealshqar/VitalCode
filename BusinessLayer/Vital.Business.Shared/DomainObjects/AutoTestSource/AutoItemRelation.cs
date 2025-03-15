using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoItemRelation : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoItem _autoItemsParent;
        private AutoItem _autoItemsChild;
        private int _order;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoItemsParent.
        /// </summary>
        public AutoItem AutoItemsParent
        {
            get { return _autoItemsParent; }
            set
            {
                if (_autoItemsParent == null || !_autoItemsParent.Equals(value))
                {
                    _autoItemsParent = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_autoItemsParent == null) return;
                _autoItemsParent.PropertyChanged += AutoItemsParent_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the AutoItemsChild.
        /// </summary>
        public AutoItem AutoItemsChild
        {
            get { return _autoItemsChild; }
            set
            {
                if (_autoItemsChild == null || !_autoItemsChild.Equals(value))
                {
                    _autoItemsChild = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_autoItemsChild == null) return;
                _autoItemsChild.PropertyChanged += AutoItemsChild_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                if (!_order.Equals(value))
                {
                    _order = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }
        
        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the AutoItemsParent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoItemsParent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoItemsParent), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoItemsParent, () => AutoItemsParent.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the AutoItemsChild.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoItemsChild_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoItemsChild), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoItemsChild, () => AutoItemsChild.Id));
        }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoItemsParent))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => AutoItemsChild))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Order))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => IsDeleted))
            {

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