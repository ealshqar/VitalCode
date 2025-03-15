using System;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.DomainObjects.Hardware
{
    public class AutoCSAReadingModel
    {
        /// <summary>
        /// Construct a new reading model.
        /// </summary>
        /// <param name="value">The reading value.</param>
        /// <param name="min">The min reading value that has been registered in the current reading session.</param>
        /// <param name="max">The max reading value that has been registered in the current reading session. </param>
        /// <param name="stabilized">Reading was stabilized.</param>
        /// <param name="datetime">The reading datetime [default is DateTime.Now]</param>
        public AutoCSAReadingModel(int value, int min, int max, bool stabilized = false, DateTime? datetime = null)
        {
            Value = value;
            Max = max;
            Min = min;
            Stabilized = stabilized;
            DateTime = datetime ?? DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the reading value.
        /// </summary>
        public int Value { get; protected set; }

        /// <summary>
        /// Gets or sets the max reading value that has been registered in the current reading session.
        /// </summary>
        public int Max { get; protected set; }

        /// <summary>
        /// Gets or sets the min reading value that has been registered in the current reading session.
        /// </summary>
        public int Min { get; protected set; }

        /// <summary>
        /// Checks if the reading is stabilized.
        /// </summary>
        public bool Stabilized { get; protected set; }

        /// <summary>
        /// Indicates if the reading value is balanced
        /// </summary>
        public bool IsBalanced
        {
            get
            {
                return CrossLayersSharedLogic.IsAcceptableReading(Value);
            }
        }

        /// <summary>
        /// Gets or sets the reading datetime.
        /// </summary>
        public DateTime DateTime { get; protected set; }
    }
}