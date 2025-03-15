using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class ProductFormsFilter : BaseFilter<ProductForm>
    {
        /// <summary>
        /// Gets or sets the ProductFormId.
        /// </summary>
        public int ProductFormId { get; set; }
        
        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        public int ProductsId { get; set; }
        
        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public int StatusLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the Form.
        /// </summary>
        public string Form { get; set; }
        
        /// <summary>
        /// Gets or sets the SuggestedUsage.
        /// </summary>
        public string SuggestedUsage { get; set; }
        
        /// <summary>
        /// Gets or sets the UsageSchedule.
        /// </summary>
        public string UsageSchedule { get; set; }
    }
}