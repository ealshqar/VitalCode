using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vital.Business.Shared.Shared
{
    public class DBCheckState
    {
        /// <summary>
        /// Returns the check status
        /// </summary>
        public DBCheckStatusEnum CheckStatus { get; set; }

        /// <summary>
        /// Returns the exception occured during check if any
        /// </summary>
        public Exception CheckException { get; set; }

        /// <summary>
        /// Gets the exception message
        /// </summary>
        public string Message
        {
            get
            {
                var message = HasException ? CheckException.Message : string.Empty;
                var innerMessage = HasException && CheckException.InnerException != null ? CheckException.InnerException.Message : string.Empty;

                return message + "\n" + innerMessage;
            }
        }

        /// <summary>
        /// Returns if the check status has an exception
        /// </summary>
        public bool HasException
        {
            get
            {
                return CheckException != null;
            }
        }
    }
}
