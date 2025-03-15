using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Filters
{
    public class TestIssuesFilter : BaseFilter<TestIssue>
    {        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the test Id
        /// </summary>
        public int TestId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the protocol step.
        /// </summary>
        public int ProtocolStepId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item ID.
        /// </summary>
        public int ItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates how to load the test issues
        /// </summary>
        public TestIssuesLoadingType IssuesLoadingType
        {
            get;
            set;
        }
    }
} 