using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class AutoTestResultProductFilter : BaseFilter<AutoTestResultProduct>
    {
        /// <summary>
        /// Gets or sets the AutoTestResultProductId.
        /// </summary>
        public int AutoTestResultProductId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoTestResult.
        /// </summary>
        public int AutoTestResultsId { get; set; }

        /// <summary>
        /// Gets or sets the ProductForm.
        /// </summary>
        public int ProductFormsId { get; set; }

        /// <summary>
        /// Gets or sets the ProductSize.
        /// </summary>
        public int ProductSizesId { get; set; }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the IsChecked.
        /// </summary>
        public bool? IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public string Duration { get; set; }
        
        /// <summary>
        /// Gets or sets the Schedule.
        /// </summary>
        public string Schedule { get; set; }
        
        /// <summary>
        /// Gets or sets the SuggestedUsage.
        /// </summary>
        public string SuggestedUsage { get; set; }
        
        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments { get; set; }
    }
}