using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Vital.Business.Shared.Shared
{
    public class VitalValidationError
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error type.
        /// </summary>
        public ErrorType Type
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        #endregion
    }
}
