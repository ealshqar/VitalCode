using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoTestStagesFilter : BaseFilter<AutoTestStage>
    {
        /// <summary>
        /// Gets or sets the AutoTestStageId.
        /// </summary>
        public int AutoTestStageId { get; set; }
        
        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public int TypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the StageItemsOrderType.
        /// </summary>
        public int StageItemsOrderTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the StageTabKey.
        /// </summary>
        public string StageTabKey { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the Dependencies.
        /// </summary>
        public string Dependencies { get; set; }
        
        /// <summary>
        /// Gets or sets the IsMultiLevel.
        /// </summary>
        public bool? IsMultiLevel { get; set; }

        /// <summary>
        /// Gets or sets the IsDestinationOnly.
        /// </summary>
        public bool? IsDestinationOnly { get; set; }

        /// <summary>
        /// Gets or sets the ScanTypeEnabled.
        /// </summary>
        public bool? ScanTypeEnabled { get; set; }
        
    }
}