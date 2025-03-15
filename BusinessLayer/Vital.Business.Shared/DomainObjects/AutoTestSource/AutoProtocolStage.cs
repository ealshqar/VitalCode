using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.AutoTestSource
{
    public class AutoProtocolStage : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private AutoProtocol _autoProtocol;
        private AutoTestStage _autoTestStage;
        private Lookup _stageItemsOrderType;
        private int _order;
        
        private BindingList<StageAutoItem> _stageAutoItems;

        #endregion

        #region LookupEnums

        /// <summary>
        /// AutoItemChildsOrderType Enum
        /// </summary>
        public ChildsOrderType StageItemsOrderTypeEnum
        {
            get
            {
                return EnumNameResolver.LookupAsEnum<ChildsOrderType>(StageItemsOrderType);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the AutoProtocol.
        /// </summary>
        public AutoProtocol AutoProtocol
        {
            get { return _autoProtocol; }
            set
            {
                if (_autoProtocol == null || !_autoProtocol.Equals(value))
                {
                    _autoProtocol = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

               // if (_autoProtocol == null) return;
               // _autoProtocol.PropertyChanged += AutoProtocol_PropertyChanged;
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

                if (_autoTestStage == null) return;
                _autoTestStage.PropertyChanged += AutoTestStage_PropertyChanged;
            }
        }

        /// <summary>
        /// Gets or sets the StageItemsOrderType.
        /// </summary>
        public Lookup StageItemsOrderType
        {
            get { return _stageItemsOrderType; }
            set
            {
                if (_stageItemsOrderType == null || !_stageItemsOrderType.Equals(value))
                {
                    _stageItemsOrderType = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }

                if (_stageItemsOrderType == null) return;
                _stageItemsOrderType.PropertyChanged += StageItemsOrderType_PropertyChanged;
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
        public BindingList<StageAutoItem> StageAutoItems
        {
            get { return _stageAutoItems; }
            set
            {
                if (_stageAutoItems != null && _stageAutoItems == value) return;

                _stageAutoItems = value;
                _stageAutoItems.RaiseListChangedEvents = true;
                _stageAutoItems.ListChanged += StageAutoItems_ListChanged;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies the change of the state for the AutoProtocol.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoProtocol_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => AutoProtocol), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => AutoProtocol, () => AutoProtocol.Id));
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

        /// <summary>
        /// Notifies the change of the state for the StageItemsOrderType.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageItemsOrderType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => StageItemsOrderType), new ErrorInfo());
            SetModifiedEntity(ExpressionHelper.GetPropertyName(() => StageItemsOrderType, () => StageItemsOrderType.Id));
        }

        /// <summary>
        /// Notifies the change of the StageAutoItems.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageAutoItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => StageAutoItems), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => StageAutoItems));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => StageItemsOrderType) && !(StageItemsOrderType == null || StageItemsOrderType.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => Order))
            {

            }

            if (propertyName == ExpressionHelper.GetPropertyName(() => StageAutoItems) && !ValidateStageAutoItems())
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            UpdateErrorsSummary(info);
        }

        /// <summary>
        /// Validates the StageAutoItem.
        /// </summary>
        /// <returns></returns>
        public bool ValidateStageAutoItems()
        {
            return StageAutoItems.All(stageAutoItems => stageAutoItems.Validate());
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