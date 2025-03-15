using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoProtocolStagesFilter : BaseFilter<AutoProtocolStage>
    {
        /// <summary>
        /// Gets or sets the AutoProtocolStageId.
        /// </summary>
        public int AutoProtocolStageId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocol.
        /// </summary>
        public int AutoProtocolsId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoTestStage.
        /// </summary>
        public int AutoTestStagesId { get; set; }
        
        /// <summary>
        /// Gets or sets the StageItemsOrderType.
        /// </summary>
        public int StageItemsOrderTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }
        
    }
}