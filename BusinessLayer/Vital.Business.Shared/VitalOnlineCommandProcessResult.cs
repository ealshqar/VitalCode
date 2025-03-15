using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared
{
    public class VitalOnlineCommandProcessResult: ProcessResult
    {
        /// <summary>
        /// Indicates if Vital is allowed to run
        /// </summary>
        public bool AllowVitalToRun { get; set; }
    }
}
