using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class StageAutoItemsFilter : BaseFilter<StageAutoItem>
    {
        /// <summary>
        /// Gets or sets the StageAutoItemId.
        /// </summary>
        public int StageAutoItemId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocolStage.
        /// </summary>
        public int AutoProtocolStagesId { get; set; }
        
        /// <summary>
        /// Gets or sets the StageAutoItemsParent.
        /// </summary>
        public int StageAutoItemsParentId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoItem.
        /// </summary>
        public int AutoItemsId { get; set; }

        /// <summary>
        /// Gets or sets the TestingPoint.
        /// </summary>
        public int TestingPointsId { get; set; }

        /// <summary>
        /// Gets or sets the ScanningMethod.
        /// </summary>
        public int ScanningMethodLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the ChildsOrderType.
        /// </summary>
        public int ChildsOrderTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the ChildsScanningType.
        /// </summary>
        public int ChildsScanningTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// Gets or sets the ScansNumber.
        /// </summary>
        public int ScansNumber { get; set; }

        /// <summary>
        /// Gets or sets the MatchesNumber.
        /// </summary>
        public int MatchesNumber { get; set; }

        /// <summary>
        /// Gets or sets the FinishAllScanRounds.
        /// </summary>
        public bool? FinishAllScanRounds { get; set; }

        /// <summary>
        /// Gets or sets the DirectAccessChecks.
        /// </summary>
        public string DirectAccessChecks { get; set; }
    }
}