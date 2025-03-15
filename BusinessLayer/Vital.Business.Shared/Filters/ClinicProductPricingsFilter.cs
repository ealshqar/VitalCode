using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class ClinicProductPricingsFilter : BaseFilter<ClinicProductPricing>
    {
        /// <summary>
        /// Gets or sets the ClinicProductPricingId.
        /// </summary>
        public int ClinicProductPricingId { get; set; }
        
        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        public int ProductsId { get; set; }
        
        /// <summary>
        /// Gets or sets the Form.
        /// </summary>
        public string Form { get; set; }
        
        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public string Size { get; set; }
        
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price { get; set; }
        
    }
}