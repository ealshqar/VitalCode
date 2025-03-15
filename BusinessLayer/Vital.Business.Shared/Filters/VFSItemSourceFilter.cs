using Vital.Business.Shared.DomainObjects.VitalForceSheet;

namespace Vital.Business.Shared.Filters
{
    public class VFSItemSourceFilter : BaseFilter<VFSItemSource>
    {

        /// <summary>
        /// Gets or sets the v1 min
        /// </summary>
        public double V1Min
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the v2 min
        /// </summary>
        public double V2Min
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the v1 max
        /// </summary>
        public double V1Max
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the v2 max
        /// </summary>
        public double V2Max
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the value 1 min ideal
        /// </summary>
        public double V1MinIdeal
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the value 2 min ideal
        /// </summary>
        public double V2MinIdeal
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the value 1 max ideal
        /// </summary>
        public double V1MaxIdeal
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the value 2 max ideal
        /// </summary>
        public double V2MaxIdeal
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the starting value 1
        /// </summary>
        public string StartingValu1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the starting value 2
        /// </summary>
        public string StartingValu2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the v2 lookup type
        /// </summary>
        public string V2LookupType
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the v1 lookup type
        /// </summary>
        public string V1LookupType
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the testing order.
        /// </summary>
        public int TestingOrder
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the has previous v1 flag.
        /// </summary>
        public bool HasPreviousV1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the has previous v2 flag.
        /// </summary>
        public bool HasPreviousV2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the has current v1 flag.
        /// </summary>
        public bool HasCurrentV1
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the has current v2 flag.
        /// </summary>
        public bool HasCurrentV2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Group Lookup.
        /// </summary>
        public int GroupLookupId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the GenderLookup.
        /// </summary>
        public int GenderLookupId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Value 1 Type Lookup.
        /// </summary>
        public int V1TypeLookupId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Value 2 Type Lookup.
        /// </summary>
        public int V2TypeLookupId
        {
            get; set;
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
