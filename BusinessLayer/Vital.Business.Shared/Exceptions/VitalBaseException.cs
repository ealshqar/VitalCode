using System;

namespace Vital.Business.Shared.Exceptions
{
    public class VitalBaseException : Exception
    {
        #region Constructors

        /// <summary>
        /// Construct the VitalBaseException depend on the base exception.
        /// </summary>
        /// <param name="baseException"></param>
        public VitalBaseException(Exception baseException): base(baseException.Message, baseException.InnerException)
        {
            BaseException = baseException;
        }

        /// <summary>
        /// Construct the VitalBaseException depend on the message and base exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseException"></param>
        public VitalBaseException(string message, Exception baseException): base(message)
        {
            BaseException = baseException;
        }

        /// <summary>
        /// Construct the VitalBaseException depend on the message.
        /// </summary>
        /// <param name="message"></param>
        public VitalBaseException(string message) : base(message) { }

        /// <summary>
        /// Construct the VitalBaseException depend on the base exception, set the inner exception also.
        /// </summary>
        /// <param name="baseException"></param>
        /// <param name="innerException"></param>
        public VitalBaseException(Exception baseException, Exception innerException): base(baseException.Message, innerException)
        {
            BaseException = baseException;
        }

        /// <summary>
        /// Construct the VitalBaseException depend on the message, inner exception and base exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseException"></param>
        /// <param name="innerException"></param>
        public VitalBaseException(string message, Exception baseException, Exception innerException): base(message, innerException)
        {
            BaseException = baseException;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get the Base Exception.
        /// </summary>
        public Exception BaseException
        {
            get;
            private set;
        }

        #endregion
    }
}

