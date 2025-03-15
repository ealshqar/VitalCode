using System;

namespace Vital.Update.Shared
{
    public class ProcessResult
    {
        /// <summary>
        /// Gets the IsSucceed.
        /// </summary>
        public bool IsSucceed { get; internal set; }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Gets the update exception.
        /// </summary>
        public Exception Exception { get; internal set; }
    }
}
