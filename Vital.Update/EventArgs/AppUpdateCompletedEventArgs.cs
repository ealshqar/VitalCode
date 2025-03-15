using Vital.Update.Shared;

namespace Vital.Update.EventArgs
{
    public class AppUpdateCompletedEventArgs : ProcessResult
    {
        /// <summary>
        /// Gets if the deployment canceled or not. 
        /// </summary>
        public bool Cancelled { get; internal set; }
    }
}
