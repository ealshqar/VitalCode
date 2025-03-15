using System;
using Vital.Business.Shared.DomainObjects.Items;

namespace Vital.Business.Shared.Filters
{
    public class ItemsFilter : BaseFilter<Item>
	{
	    public ItemsFilter()
	    {
	        IncludeHiddenChilds = true;
	    }

		/// <summary>
		/// Get or set the Name.
		/// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Get or set the Key.
        /// </summary>
        public string Key { get; set; }

		/// <summary>
		/// Get or set the Full Name.
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Get or set the TypeLookupId.
		/// </summary>
		public int TypeLookupId { get; set; }

		/// <summary>
		/// Get or set the ListTypeLookupId.
		/// </summary>
		public int ListTypeLookupId { get; set; }

		/// <summary>
		/// Get or set the TargetTypeLookupId.
		/// </summary>
		public int TargetTypeLookupId { get; set; }

		/// <summary>
		/// Gets or sets the item source lookup id.
		/// </summary>
		public int ItemSourceLookupId { get; set; }

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

		/// <summary>
		/// Choose to order by ID.
		/// </summary>
		public bool OrderById { get; set; }

		/// <summary>
		/// Indicate if User Hidden and System Hidden items will be included in the list of items to load
		/// DON'T SET THIS TO TRUE UNTIL YOU DOUBLE CHECK
		/// </summary>
		public bool LoadHiddenItems { get; set; }

        /// <summary>
        /// Indicate if System Hidden Childs will be included within the list of items to load
        /// </summary>
        public bool IncludeHiddenChilds { get; set; }

        /// <summary>
        /// A key used to allow searching multiple properties in item entity
        /// </summary>
        public string SearchKey { get; set; }

	}
}
