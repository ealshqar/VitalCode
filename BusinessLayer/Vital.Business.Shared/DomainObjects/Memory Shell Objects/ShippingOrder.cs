using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Memory_Shell_Objects
{
    public class ShippingOrderMso : DomainEntity, IDXDataErrorInfo
    {
        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }        
        public string Email { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianAddress { get; set; }
        public string TechnicianState { get; set; }
        public string TechnicianZipCode { get; set; }
        public string TechnicianCity { get; set; }
        public string TechnicianPhone { get; set; }
        public string Comments { get; set; }

        #endregion

        #region Validation

        /// <summary>
        /// Gets the validation errors according to the below cases.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="info">The error info.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (propertyName == ExpressionHelper.GetPropertyName(() => FirstName))
            {
                IsRequiredStringPropertyValid(propertyName, FirstName, info);
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => State) && State != null && State.Length < 2 && State.Length > 0)
            {
                info.ErrorText = StaticKeys.ValidationMessageMinimumLength;
                info.ErrorType = ErrorType.Warning;
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => Address1))
            {
                IsRequiredStringPropertyValid(propertyName, Address1, info);
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => HomePhone))
            {
                IsRequiredStringPropertyValid(propertyName, HomePhone, info);
            }

            else if (propertyName == ExpressionHelper.GetPropertyName(() => TechnicianName))
            {
                IsRequiredStringPropertyValid(propertyName, TechnicianName, info);
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
    }
}
