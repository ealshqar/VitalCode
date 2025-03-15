using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.Shared
{
    public class CachedParent
    {
        /// <summary>
        /// The cached parent item id
        /// </summary>
        public int ParentItemId { get; set; }

        /// <summary>
        /// Cached Items
        /// </summary>
        public BindingList<Item> CachedItems { get; set; }

        /// <summary>
        /// The value of the LoadHiddenItems flag that was used in loading call
        /// </summary>
        public bool LoadHiddenItems { get; set; }
    }
}
