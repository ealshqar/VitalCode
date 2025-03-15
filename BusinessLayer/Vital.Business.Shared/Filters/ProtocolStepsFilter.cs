using Vital.Business.Shared.DomainObjects.TestProtocols;

namespace Vital.Business.Shared.Filters
{
    public class ProtocolStepsFilter : BaseFilter<ProtocolStep>
    {
        /// <summary>
        /// Gets or sets the test protocol id.
        /// </summary>
        public int TestProtocolId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public int Order
        {
            get; set;
        }
    }
}
