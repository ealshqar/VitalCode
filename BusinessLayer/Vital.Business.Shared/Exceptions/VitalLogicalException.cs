using System;

namespace Vital.Business.Shared.Exceptions
{
    public class VitalLogicalException : VitalBaseException
    {
        #region Constructors

        /// <summary>
        /// Construct the VitalLogicalException depend on the base exception.
        /// </summary>
        /// <param name="baseException"></param>
        public VitalLogicalException(Exception baseException) : base(baseException){}

        /// <summary>
        /// Construct the VitalLogicalException depend on the message and base exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseException"></param>
        public VitalLogicalException(string message, Exception baseException) : base(message, baseException) { }

        /// <summary>
        /// Construct the Exception depend on the message.
        /// </summary>
        /// <param name="message"></param>
        public VitalLogicalException(string message) : base(message) { }

        /// <summary>
        /// Construct the VitalLogicalException depend on the base exception, set the inner exception also.
        /// </summary>
        /// <param name="baseException"></param>
        /// <param name="innerException"></param>
        public VitalLogicalException(Exception baseException, Exception innerException) : base(baseException.Message, innerException){}

        /// <summary>
        /// Construct the VitalLogicalException depend on the message, inner exception and base exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseException"></param>
        /// <param name="innerException"></param>
        public VitalLogicalException(string message, Exception baseException, Exception innerException)
            : base(message, baseException, innerException) { }

        #endregion
    }
}
