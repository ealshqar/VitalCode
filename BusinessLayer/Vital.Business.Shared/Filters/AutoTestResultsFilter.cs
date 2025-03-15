using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class AutoTestResultsFilter : BaseFilter<AutoTestResult>
    {
        /// <summary>
        /// Gets or sets the AutoTestResultId.
        /// </summary>
        public int AutoTestResultId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoTest.
        /// </summary>
        public int AutoTestsId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoItem.
        /// </summary>
        public int AutoItemsId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocolStageRevision.
        /// </summary>
        public int AutoProtocolStageRevisionsId { get; set; }

        /// <summary>
        /// Gets or sets the AutoTestResultsParent.
        /// </summary>
        public int AutoTestResultParentId { get; set; }

        /// <summary>
        /// Gets or sets the PreliminaryReading.
        /// </summary>
        public int PreliminaryReading { get; set; }
        
        /// <summary>
        /// Gets or sets the SummaryReading.
        /// </summary>
        public int SummaryReading { get; set; }
        
        /// <summary>
        /// Gets or sets the IsAddedManually.
        /// </summary>
        public bool? IsAddedManually { get; set; }
        
        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes { get; set; }
        
    }
}