using System;
using Vital.Update.Enums;
using Vital.Update.Shared;

namespace Vital.Update.EventArgs
{
    public class AppUpdateCheckCompletedEventArgs : ProcessResult
    {
        /// <summary>
        /// Gets the UpdateAvailable.
        /// </summary>
        public bool UpdateAvailable { get; internal set; }

        /// <summary>
        /// Gets the IsUpdateRequired value.
        /// </summary>
        public UpdateType UpdateType { get; internal set; }

        /// <summary>
        /// Gets the AvailableVersion value.
        /// </summary>
        public Version AvailableVersion { get; internal set; }

        /// <summary>
        /// Gets the Update Size Bytes
        /// </summary>
        public long UpdateSizeBytes { get; internal set; }
    }
}
