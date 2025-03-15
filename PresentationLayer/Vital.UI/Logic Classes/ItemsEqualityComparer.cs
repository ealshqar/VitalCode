using System.Collections.Generic;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.UI.Logic_Classes
{
    /// <summary>
    /// This Class is used in the (Except) Linq extension.
    /// </summary>
    public class ItemsEqualityComparer : IEqualityComparer<Item>
    {
        /// <summary>
        /// Determines whether the specified items are equal.
        /// </summary>
        /// <param name="x">The first item.</param>
        /// <param name="y">The second item.</param>
        /// <returns></returns>
        public bool Equals(Item x, Item y)
        {
            return x.Id == y.Id;
        }

        /// <summary>
        /// Returns a hash code for the specified item.
        /// </summary>
        /// <param name="obj">The item.</param>
        /// <returns></returns>
        public int GetHashCode(Item obj)
        {
            return obj.Id;
        }
    }
}
