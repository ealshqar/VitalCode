using System;

namespace Vital.Business.Shared.Exceptions
{
    public class VitalDatabaseException : VitalBaseException
    {
        #region Constructors

        /// <summary>
        /// Construct the VitalDatabaseException depend on the base exception.
        /// </summary>
        /// <param name="baseException"></param>
        public VitalDatabaseException(Exception baseException) : base(baseException){}

        /// <summary>
        /// Construct the VitalDatabaseException depend on the message and base exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseException"></param>
        public VitalDatabaseException(string message, Exception baseException) : base(message, baseException){}

        /// <summary>
        /// Construct the VitalDatabaseException depend on the message.
        /// </summary>
        /// <param name="message"></param>
        public VitalDatabaseException(string message) : base(message) {}

        /// <summary>
        /// Construct the VitalDatabaseException depend on the base exception, set the inner exception also.
        /// </summary>
        /// <param name="baseException"></param>
        /// <param name="innerException"></param>
        public VitalDatabaseException(Exception baseException, Exception innerException) : base(baseException.Message, innerException){}

        /// <summary>
        /// Construct the VitalDatabaseException depend on the message, inner exception and base exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseException"></param>
        /// <param name="innerException"></param>
        public VitalDatabaseException(string message, Exception baseException, Exception innerException) : base(message, baseException, innerException){}

        #endregion
    }
}
