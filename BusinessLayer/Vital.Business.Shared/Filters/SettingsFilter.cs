namespace Vital.Business.Shared.Filters
{
    public class SettingsFilter
    {
        /// <summary>
        /// Get or set the Name Value.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Get or set the Name Value.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set Description Value.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the SettingTypeLookupId Value.
        /// </summary>
        public int SettingTypeLookupId { get; set; }

        /// <summary>
        /// Get or set the SettingGroupLookupId Value.
        /// </summary>
        public int SettingGroupLookupId { get; set; }

        /// <summary>
        /// Get or set the Caption Value.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the IsVisible value.
        /// </summary>
        public bool? IsVisible { get; set; }

    }
}
