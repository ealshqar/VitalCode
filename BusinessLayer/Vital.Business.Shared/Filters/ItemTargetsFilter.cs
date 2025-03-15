using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.Filters
{
    public class ItemTargetsFilter : BaseFilter<ItemTarget>
    {
        public int ItemId
        {
            get; set;
        }
    }
}
