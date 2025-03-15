using Vital.Business.Shared.DomainObjects.TestProtocols;

namespace Vital.Business.Shared.Filters
{
    public class ProtocolItemsFilter : BaseFilter<ProtocolItem>
    {
        /// <summary>
        /// Gets or sets the test protocol id.
        /// </summary>
        public int TestProtocolId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        public int ItemId
        {
            get; set;
        }
    }
}
