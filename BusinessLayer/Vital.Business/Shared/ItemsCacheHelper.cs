using System.ComponentModel;
using System.Linq;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared
{
    public class ItemsCacheHelper
    {
        private static BindingList<CachedParent> _cachedParents;

        /// <summary>
        /// Adds child items to cache and links them with their parent id
        /// </summary>
        /// <returns></returns>
        public static void AddChildItemsToCachedList(int itemId, bool loadHiddenItems, BindingList<Item> cachedList)
        {
            if (itemId != 0 && cachedList != null && cachedList.Any())
            {
                if (_cachedParents == null)
                {
                    _cachedParents = new BindingList<CachedParent>();
                }

                if (!_cachedParents.Any(c => c.ParentItemId == itemId && c.LoadHiddenItems == loadHiddenItems))
                {
                    _cachedParents.Add(new CachedParent { ParentItemId = itemId, CachedItems = cachedList });
                }
            }
        }

        /// <summary>
        /// Loads child items from cache in case they existed there by checking for parent id
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="loadHiddenItems"></param>
        /// <returns></returns>
        public static BindingList<Item> GetChildItemsFromCacheIfExisting(int itemId, bool loadHiddenItems)
        {
            BindingList<Item> cachedItems = null;

            if (itemId != 0)
            {
                if (_cachedParents != null)
                {
                    var parent = _cachedParents.FirstOrDefault(p => p.ParentItemId == itemId && p.LoadHiddenItems == loadHiddenItems);

                    if (parent != null)
                    {
                        cachedItems = parent.CachedItems;
                    }
                }
            }

            return cachedItems;
        }

        /// <summary>
        /// Clears the cache
        /// </summary>
        public static void ClearCache()
        {
            _cachedParents = null;
        }
    }
}
