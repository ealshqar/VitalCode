using Vital.Business.Shared.DomainObjects.VitalForceSheet;

namespace Vital.Business.Shared.Filters
{
    public class VFSSecondaryItemsFilter : BaseFilter<VFSSecondaryItem>
    {
        /// <summary>
        /// Gets or sets the VFS id.
        /// </summary>
        public int VFSId
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

        /// <summary>
        /// Get or set the SectionLookupId Value.
        /// </summary>
        public int SectionLookupId
        {
            get;
            set;
        }
    }
}