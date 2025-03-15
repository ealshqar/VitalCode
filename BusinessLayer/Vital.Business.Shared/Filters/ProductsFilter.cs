using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class ProductsFilter : BaseFilter<Product>
    {
        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoItem.
        /// </summary>
        public int AutoItemsId { get; set; }
        
        /// <summary>
        /// Gets or sets the Supplier.
        /// </summary>
        public string Supplier { get; set; }
        
        /// <summary>
        /// Gets or sets the IngredientsString.
        /// </summary>
        public string IngredientsString { get; set; }
        
        /// <summary>
        /// Gets or sets the Supports.
        /// </summary>
        public string Supports { get; set; }
        
        /// <summary>
        /// Gets or sets the UsefulFor.
        /// </summary>
        public string UsefulFor { get; set; }
        
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Gets or sets the DiscountPercentage.
        /// </summary>
        public decimal DiscountPercentage { get; set; }
        
        /// <summary>
        /// Gets or sets the HasDiscount.
        /// </summary>
        public bool? HasDiscount { get; set; }
        
    }
}