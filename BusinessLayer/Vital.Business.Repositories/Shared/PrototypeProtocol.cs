using System.Collections.Generic;
using Vital.Business.Shared.Enums;

namespace Vital.Business.Repositories.Shared
{
    public static class PrototypeProtocol
    {
        /// <summary>
        /// Data that hardware sends continuously while its connected to the port.  
        /// </summary>
        public const string AliveStreamData = "E";

        /// <summary>
        /// The hardware commands.
        /// </summary>
        public static readonly Dictionary<PrototypeCommand, string> Commands = new Dictionary<PrototypeCommand, string>
        {
            {PrototypeCommand.Reset, "0"},
            {PrototypeCommand.MoistureCheck, "M"},
            {PrototypeCommand.PressureCheck, "P"}
        };

        /// <summary>
        /// The hardware responses.
        /// </summary>
        public static readonly Dictionary<string, PrototypeResponse> Responses = new Dictionary<string, PrototypeResponse>
        {
            {"M", PrototypeResponse.ValidMoisture},
            {"N", PrototypeResponse.InvalidMoisture},
            {"P", PrototypeResponse.ValidPressure},
            {"Q", PrototypeResponse.InvalidPressure}
        };
    }
}
