using System;

namespace Vital.Business.Shared.Shared
{
    public class ProcessResult
    {
        /// <summary>
        /// Get or set the IsSucceed.
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Get or set the Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get or set the New Entity Id.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets new process result with IsSucceed = true.
        /// </summary>
        public static ProcessResult Succeed
        {
            get { return new ProcessResult() { IsSucceed = true }; }
        }

        /// <summary>
        /// Gets new process result with IsSucceed = false.
        /// </summary>
        public static ProcessResult Failed
        {
            get { return new ProcessResult() { IsSucceed = false }; }
        }

        /// <summary>
        /// Result exception in case of failure
        /// </summary>
        public Exception Exception { get; set; }
    }
}
