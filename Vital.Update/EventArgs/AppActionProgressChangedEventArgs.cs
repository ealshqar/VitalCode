using Vital.Update.Enums;

namespace Vital.Update.EventArgs
{
    public class AppActionProgressChangedEventArgs
    {
        /// <summary>
        /// Gets the downloaded bytes count.
        /// </summary>
        public long BytesCompleted { get; internal set; }

        /// <summary>
        /// Gets the total bytes count to download.
        /// </summary>
        public long BytesTotal { get; internal set; }

        /// <summary>
        /// Gets the progress percentage out of 100.
        /// </summary>
        public int ProgressPercentage { get; internal set; }

        /// <summary>
        /// Gets the status of the update progress.
        /// </summary>
        public ApplicationUpdateProgressState State { get; internal set; }
    }
}
