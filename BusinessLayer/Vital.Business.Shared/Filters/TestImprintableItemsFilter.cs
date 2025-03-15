using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.Business.Shared.Filters
{
    public class TestImprintableItemsFilter : BaseFilter<TestImprintableItem>
    {
        /// <summary>
        /// Gets or sets the Test Id.
        /// </summary>
        public int TestId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Item Id.
        /// </summary>
        public int ItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ImprintableIsChecked.
        /// </summary>
        public bool IsChecked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ImprintableIsImprinted.
        /// </summary>
        public bool IsImprinted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ImprintableOrder.
        /// </summary>
        public int Order
        {
            get;
            set;
        }
    }
}