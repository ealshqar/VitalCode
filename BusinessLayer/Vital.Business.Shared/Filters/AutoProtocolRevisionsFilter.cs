using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoProtocolRevisionsFilter : BaseFilter<AutoProtocolRevision>
    {
        /// <summary>
        /// Gets or sets the AutoProtocolRevisionId.
        /// </summary>
        public int AutoProtocolRevisionId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocol.
        /// </summary>
        public int AutoProtocolsId { get; set; }
        
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
        
    }
}