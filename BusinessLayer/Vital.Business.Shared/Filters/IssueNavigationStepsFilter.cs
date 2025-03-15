using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.Business.Shared.Filters
{
    public class IssueNavigationStepsFilter : BaseFilter<IssueNavigationStep>
    {
        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        public int Order
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the issue Id.
        /// </summary>
        public int IssueId
        {
            get;
            set;
        }
    }
} 