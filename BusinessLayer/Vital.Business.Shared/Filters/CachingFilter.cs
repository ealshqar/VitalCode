using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Filters
{
    public class CachingFilter
    {
        /// <summary>
        /// Gets or sets the cache key.
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// Gets or sets the information type.
        /// </summary>
        public CachableDataEnum InformationType { get; set; }

        /// <summary>
        /// Gets or sets the data id.
        /// </summary>
        public int DataId { get; set; }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        public SingleValueTypeEnum SingleValueType { get; set; }

        /// <summary>
        /// Gets or sets the setting key.
        /// </summary>
        public SettingKeys SettingKey { get; set; }
    }
}
