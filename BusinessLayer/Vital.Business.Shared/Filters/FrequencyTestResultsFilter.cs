using Vital.Business.Shared.DomainObjects.FrequencyTests;

namespace Vital.Business.Shared.Filters
{
    public class FrequencyTestResultsFilter : BaseFilter<FrequencyTestResult>
    {
        /// <summary>
        /// Gets or sets the FrequencyTestId.
        /// </summary>
        public int FrequencyTestId
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public int Notes
        {
            get;
            set;
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