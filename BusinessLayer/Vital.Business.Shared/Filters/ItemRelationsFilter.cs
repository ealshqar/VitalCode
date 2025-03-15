using System;

namespace Vital.Business.Shared.Filters
{
    public class ItemRelationsFilter
    {
        /// <summary>
        /// Get or set the Parent.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Get or set the Child
        /// </summary>
        public int ChildId { get; set; }

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

        /// <summary>
        /// Indicate if User Hidden and System Hidden items will be included in the list of items to load
        /// DON'T SET THIS TO TRUE UNTIL YOU DOUBLE CHECK
        /// </summary>
        public bool LoadHiddenItems { get; set; }

    }
}
