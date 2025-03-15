using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Invoices
{
    public class Invoice : DomainEntity, IDXDataErrorInfo
    {
        #region Private Variables

        private string _number;
        private string _comments;
        private string _paymentMethod;
        private string _chequeNumber;
        private decimal _totalAmount;
        private Test _test;
        private bool _isFirstTimeAfterClosing;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Number
        /// </summary>
        public string Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Indicates if the printing form was closed.
        /// </summary>
        public bool IsFirstTimeAfterClosing
        {
            get { return _isFirstTimeAfterClosing; }
            set
            {
                if (_isFirstTimeAfterClosing != value)
                {
                    _isFirstTimeAfterClosing = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Payment Method
        /// </summary>
        public string PaymentMethod
        {
            get { return _paymentMethod; }
            set
            {
                if (_paymentMethod != value)
                {
                    _paymentMethod = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Cheque Number
        /// </summary>
        public string ChequeNumber
        {
            get { return _chequeNumber; }
            set
            {
                if (_chequeNumber != value)
                {
                    _chequeNumber = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Total Amount.
        /// </summary>
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    SetModifiedState(MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Comments.
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
        /// Gets or sets the Test
        /// </summary>
        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                if (_test == null) return;
                _test.PropertyChanged += Test_PropertyChanged;
            }
        }

        

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the property changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void Test_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GetPropertyError(ExpressionHelper.GetPropertyName(() => Test), new ErrorInfo());
            SetModifiedState(ExpressionHelper.GetPropertyName(() => Test, () => Test.Id));
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
            if (propertyName == ExpressionHelper.GetPropertyName(() => this.Number))
            {
                IsRequiredStringPropertyValid(propertyName, this.Number, info);
            }

            else if ((propertyName == ExpressionHelper.GetPropertyName(() => this.Test) && !Test.Validate()))
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

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                GetPropertyError(property.Name, new ErrorInfo());
            }

            return IsValid;
        }

        #endregion
    }
}
