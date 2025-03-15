using Vital.Business.Shared.DomainObjects.ShippingOrders;

namespace Vital.Business.Shared.Filters
{
    public class OrderItemsFilter : BaseFilter<OrderItem>
    {
        /// <summary>
        /// Gets or sets the shipping order id.
        /// </summary>
        public int ShippingOrderId
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
