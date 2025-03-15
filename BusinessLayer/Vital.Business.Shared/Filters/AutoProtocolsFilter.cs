using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoProtocolsFilter : BaseFilter<AutoProtocol>
    {
        /// <summary>
        /// Gets or sets the AutoProtocolId.
        /// </summary>
        public int AutoProtocolId { get; set; }
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Gets or sets the IsSystemProtocol.
        /// </summary>
        public bool? IsSystemProtocol { get; set; }
        
        /// <summary>
        /// Gets or sets the IsDefaultProtocol.
        /// </summary>
        public bool? IsDefaultProtocol { get; set; }
        
        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        public bool? IsDeleted { get; set; }
        
    }
}