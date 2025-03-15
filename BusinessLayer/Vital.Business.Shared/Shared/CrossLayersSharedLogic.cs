using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vital.Business.Shared.Shared
{
    public class CrossLayersSharedLogic
    {
        /// <summary>
        /// Check the passed reading if acceptable or not.
        /// </summary>
        /// <param name="reading">The reading.</param>
        /// <returns></returns>
        public static bool IsAcceptableReading(int reading)
        {
            return reading >= StaticKeys.MeterMinAcceptableReading && reading <= StaticKeys.MeterMaxAcceptableReading;
        }

    }
}
