using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.Filters
{
    public class RelationalPropertiesFilter : BaseFilter<ItemRelationProperty>
    {
        /// <summary>
        /// Gets or sets the Related(Item or Item Relation) Entity Id.
        /// </summary>
        public int RelatedEntityId { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }

    }
}
