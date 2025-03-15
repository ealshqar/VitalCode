namespace Vital.Business.Shared.Filters
{
    public class HwProfilesFilter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the MinReading.
        /// </summary>
        public int MinReading { get; set; }

        /// <summary>
        /// Gets or sets the DisconnectedTimeout.
        /// </summary>
        public int DisconnectedTimeout { get; set; }

        /// <summary>
        /// Gets or sets the StabilityTimeout.
        /// </summary>
        public int StabilityTimeout { get; set; }

        /// <summary>
        /// Gets or sets the StabilityRange.
        /// </summary>
        public int StabilityRange { get; set; }

        /// <summary>
        /// Gets or sets the IsSystemProfile.
        /// </summary>
        public bool? IsSystemProfile { get; set; }

        /// <summary>
        /// Gets or sets the IsDefault.
        /// </summary>
        public bool? IsDefault { get; set; }
    }
}
