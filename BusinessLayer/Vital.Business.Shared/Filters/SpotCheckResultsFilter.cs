using Vital.Business.Shared.DomainObjects.SpotChecks;

namespace Vital.Business.Shared.Filters
{
    public class SpotCheckResultsFilter : BaseFilter<SpotCheckResult>
    {
        /// <summary>
        /// Gets or sets the spot check id.
        /// </summary>
        public int SpotCheckId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        public int ItemId
        {
            get;
            set;
        }
    }
}
