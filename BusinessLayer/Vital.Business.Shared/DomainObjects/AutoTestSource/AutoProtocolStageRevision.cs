using System.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoProtocolStageRevision : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoProtocolRevision _autoProtocolRevision;
        private AutoProtocolStage _autoProtocolStage;
        private AutoTestStage _autoTestStage;
        private int _order;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoProtocolRevision.
        /// </summary>
        public AutoProtocolRevision AutoProtocolRevision
        {
            get { return _autoProtocolRevision; }
            set
            {
                if (_autoProtocolRevision == null || !_autoProtocolRevision.Equals(value))
                {
                    _autoProtocolRevision = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                //if (_autoProtocolRevision == null) return;
               // _autoProtocolRevision.PropertyChanged += AutoProtocolRevision_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the AutoProtocolStage.
        /// </summary>
        public AutoProtocolStage AutoProtocolStage
        {
            get { return _autoProtocolStage; }
            set
            {
                if (_autoProtocolStage == null || !_autoProtocolStage.Equals(value))
                {
                    _autoProtocolStage = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                //if (_autoProtocolStage == null) return;
                //_autoProtocolStage.PropertyChanged += AutoProtocolStage_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the AutoTestStage.
        /// </summary>
        public AutoTestStage AutoTestStage
        {
            get { return _autoTestStage; }
            set
            {
                if (_autoTestStage == null || !_autoTestStage.Equals(value))
                {
                    _autoTestStage = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                //if (_autoTestStage == null) return;
               // _autoTestStage.PropertyChanged += AutoTestStage_PropertyChanged;
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

        /// <summary>
        /// Gets or sets the StageAutoItems.
        /// </summary>
        public BindingList<StageAutoItem> StageAutoItemsForStages
        {
            get
            {
                return _autoProtocolStage == null || AutoTestStage == null ? null :
                    AutoTestStage.IsMultiLevel ? _autoProtocolStage.StageAutoItems :
                    new BindingList<StageAutoItem>(); //If the AutoTestStage doesn't have multi-levels then skip including StageAutoItems in list
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the AutoProtocolRevision.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoProtocolRevision_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoProtocolRevision), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoProtocolRevision, () => AutoProtocolRevision.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the AutoProtocolStage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoProtocolStage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoProtocolStage), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoProtocolStage, () => AutoProtocolStage.Id));
        }

        /// <summary>
        /// Notifies the change of the state for the AutoTestStage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTestStage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoTestStage), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoTestStage, () => AutoTestStage.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => Order))
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