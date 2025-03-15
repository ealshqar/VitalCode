using Vital.Business.Shared.DomainObjects.Services;

namespace Vital.Business.Shared.Filters
{
    public class ServicesFilter : BaseFilter<Service>
    {
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IsDefault.
        /// </summary>
        public bool? IsDefault
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the TypeLookupId Value.
        /// </summary>
        public int TypeLookupId
        {
            get; 
            set;
        }
    }
}