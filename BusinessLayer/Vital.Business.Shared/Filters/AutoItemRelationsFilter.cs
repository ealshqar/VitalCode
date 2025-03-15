using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoItemRelationsFilter : BaseFilter<AutoItemRelation>
    {
        /// <summary>
        /// Gets or sets the AutoItemRelationId.
        /// </summary>
        public int AutoItemRelationId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoItemsParent.
        /// </summary>
        public int AutoItemsParentId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoItemsChild.
        /// </summary>
        public int AutoItemsChildId { get; set; }
        
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        public bool? IsDeleted { get; set; }
        
    }
}