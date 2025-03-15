using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Filters;

namespace Vital.Business.Shared
{
    public static class ItemExtensions
    {
        public static bool CheckIfHasChild(this Item item )
        {
            var itemsManager = new ItemsManager();

            return itemsManager.GetItemChildren(new SingleItemFilter() {ItemId = item.Id}).Count > 0;
        }
    }
}
