using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoProtocolStageRevisionsFilter : BaseFilter<AutoProtocolStageRevision>
    {
        /// <summary>
        /// Gets or sets the AutoProtocolStageRevisionId.
        /// </summary>
        public int AutoProtocolStageRevisionId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocolRevision.
        /// </summary>
        public int AutoProtocolRevisionsId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocolStage.
        /// </summary>
        public int AutoProtocolStagesId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoTestStage.
        /// </summary>
        public int AutoTestStagesId { get; set; }
        
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }
        
    }
}