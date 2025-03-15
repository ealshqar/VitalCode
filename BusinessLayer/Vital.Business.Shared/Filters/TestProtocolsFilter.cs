using Vital.Business.Shared.DomainObjects.TestProtocols;

namespace Vital.Business.Shared.Filters
{
    public class TestProtocolsFilter : BaseFilter<TestProtocol>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get; set;
        }
    }
}
