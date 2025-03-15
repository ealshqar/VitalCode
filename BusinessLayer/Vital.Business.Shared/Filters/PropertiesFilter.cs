using Vital.Business.Shared.DomainObjects.Properties;

namespace Vital.Business.Shared.Filters
{
    public class PropertiesFilter : BaseFilter<Property>
    {
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the Applicable types lookup id.
        /// </summary>
        public int[] ApplicableTypeIds { get; set; }

    }
}
