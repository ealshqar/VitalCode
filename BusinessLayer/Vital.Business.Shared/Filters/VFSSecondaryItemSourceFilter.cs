using Vital.Business.Shared.DomainObjects.VitalForceSheet;

namespace Vital.Business.Shared.Filters
{
    public class VFSSecondaryItemSourceFilter : BaseFilter<VFSSecondaryItemSource>
    {
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
