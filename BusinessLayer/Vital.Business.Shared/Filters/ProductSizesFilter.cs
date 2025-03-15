using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class ProductSizesFilter : BaseFilter<ProductSize>
    {
        /// <summary>
        /// Gets or sets the ProductSizeId.
        /// </summary>
        public int ProductSizeId { get; set; }
        
        /// <summary>
        /// Gets or sets the ProductForm.
        /// </summary>
        public int ProductFormsId { get; set; }
        
        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public int StatusLookupId { get; set; }
        
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