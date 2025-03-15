using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Items
{
    public class ItemProperty : DomainEntityPropertyRelational, IDXDataErrorInfo
    {
        #region Private Variables

        private Item _item;

        #endregion

        #region Public Properties

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
            if ((propertyName == ExpressionHelper.GetPropertyName(() => Property) && !Property.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => Item) && !Item.Validate()))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => Value) && Value == null))
            {
                info.ErrorText = StaticKeys.ValidationMessageBlankField;
                info.ErrorType = ErrorType.Critical;
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
            if (!IsChanged) return true;

            ValidationErrors.Clear();

            foreach (var property in GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }

        #endregion

        #region Event Handlers

        #endregion
    }
}
