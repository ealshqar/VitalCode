using System;
using Vital.Business.Shared.DomainObjects.Readings;

namespace Vital.Business.Shared.Filters
{
    public class ReadingsFilter : BaseFilter<Reading>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int? Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Rise.
        /// </summary>
        public int? Rise
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Fall.
        /// </summary>
        public int? Fall
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Max.
        /// </summary>
        public int? Max
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Min.
        /// </summary>
        public int? Min
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public int UserId
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
        /// Gets or sets the PointSetItemId.
        /// </summary>
        public int PointSetItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Test Id.
        /// </summary>
        public int TestId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Date Time.
        /// </summary>
        public DateTime? DateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the List Point Lookup Id.
        /// </summary>
        public int ListPointLookupId
        {
            get; 
            set;
        }

    }
}
