using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class DosageOptionsFilter : BaseFilter<DosageOption>
    {
        /// <summary>
        /// Gets or sets the DosageOptionId.
        /// </summary>
        public int DosageOptionId { get; set; }

        /// <summary>
        /// Gets or sets the Product.
        /// </summary>
        public int ProductsId { get; set; }

        /// <summary>
        /// Gets or sets the ProductForm.
        /// </summary>
        public int ProductFormsId { get; set; }

        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the UsageSchedule.
        /// </summary>
        public string UsageSchedule { get; set; }

        /// <summary>
        /// Gets or sets the SuggestedUsage.
        /// </summary>
        public string SuggestedUsage { get; set; }
    }
}