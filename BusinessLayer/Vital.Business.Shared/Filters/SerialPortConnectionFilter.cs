using System;
using Vital.Business.Shared.Enums;

namespace Vital.Business.Shared.Filters
{
    public class SerialPortConnectionFilter : SerailPortNumberFilter
    {

        #region Constructors

        public SerialPortConnectionFilter(HardwareType hardwareType)
        {
            switch (hardwareType)
            {
                case HardwareType.CSA:
                    BaudRate = 1200;
                    DataBit = 8;
                    Timeout = 1500;
                    Dtr = false;
                    Rts = true;
                    break;
                case HardwareType.Prototype:
                    BaudRate = 9600;
                    DataBit = 8;
                    Timeout = 2000;
                    Dtr = false;
                    Rts = true;
                    break;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set baudRate.
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// Get or set dataBit.
        /// </summary>
        public int DataBit { get; set; }

        /// <summary>
        /// Get or set timeout.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Get or set dtr.
        /// </summary>
        public bool Dtr { get; set; }

        /// <summary>
        /// Get or set rts.
        /// </summary>
        public bool Rts { get; set; }

        #endregion
    }
}
