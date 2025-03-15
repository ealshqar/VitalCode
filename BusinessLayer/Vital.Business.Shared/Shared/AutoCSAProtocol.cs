using System.Collections.Generic;
using Vital.Business.Shared.Enums;

namespace Vital.Business.Shared.Shared
{
    public static class AutoCSAProtocol
    { 
        /// <summary>
        /// The Auto CSA hardware commands.
        /// </summary>
        public static readonly Dictionary<AutoCSACommand, string> Commands = new Dictionary<AutoCSACommand, string>
        {
            {AutoCSACommand.Reset, "R"},
            {AutoCSACommand.ActivateManualMode, "N"},
            {AutoCSACommand.ActivateTopPlate, "T"},
            {AutoCSACommand.ActivateImprinting, "I"},
            {AutoCSACommand.ActivateAutomationMode, "A"},
            {AutoCSACommand.StartAutomation, "S"},
            {AutoCSACommand.StopAutomation, "O"},
            {AutoCSACommand.HingeCheck, "H"},
            {AutoCSACommand.MoistureCheck, "M"},
            {AutoCSACommand.PressureCheck, "P"},
        };

        /// <summary>
        /// The Auto CSA hardware responses.
        /// </summary>
        public static readonly Dictionary<string, AutoCSAResponse> Responses = new Dictionary<string, AutoCSAResponse>
        {
            {"E", AutoCSAResponse.IdleMode},
            {"O", AutoCSAResponse.IdleAutomationMode},
            {"I", AutoCSAResponse.ImprintingModeActivated},
            {"H", AutoCSAResponse.ValidHinge},
            {"Z", AutoCSAResponse.InvalidHinge},
            {"L", AutoCSAResponse.ValidMoisture},
            {"X", AutoCSAResponse.InvalidMoisture},
            {"P", AutoCSAResponse.ValidPressure},
            {"Y", AutoCSAResponse.InvalidPressure},
            {"W", AutoCSAResponse.ManualProbesDisconnected}
        };
    }
}