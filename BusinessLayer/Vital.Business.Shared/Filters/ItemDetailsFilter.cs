using System;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.Filters
{
    public class ItemsDetailsFilter : BaseFilter<ItemDetails>
    {       
        /// <summary>
        /// Get or set the Image.
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// Get or set the X.
        /// </summary>
        public int? X { get; set; }

        /// <summary>
        /// Get or set the Y.
        /// </summary>
        public int? Y { get; set; }

        /// <summary>
        /// Get or set the User.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Get or set the CreationDateTime.
        /// </summary>
        public DateTime? CreationDateTime { get; set; }

        /// <summary>
        /// Get or set the UpdatedDateTime.
        /// </summary>
        public DateTime? UpdatedDateTime { get; set; }

    }
}
