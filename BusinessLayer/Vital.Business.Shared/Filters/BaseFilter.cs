using System;
using System.Collections.Generic;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Shared.Filters
{
    public class BaseFilter<T>
    {
        #region Constroctors

        public BaseFilter()
        {
            SortByKeys = new List<SortKey<T>>();
            LoadingType = LoadingTypeEnum.All;
        }

        #endregion

        #region PublicProperties

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the ItemsIds value.
        /// </summary>
        public List<int> ItemsIds { get; set; }

        /// <summary>
        /// Gets or sets the search key.
        /// </summary>
        public string SearchKey { get; set; }

        /// <summary>
        /// Gets or sets the SortBy keys.
        /// </summary>
        public IList<SortKey<T>> SortByKeys { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets SentDate.
        /// </summary>
        public DateTime? CreationDateTime { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        public DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets CreatedByUserId value.
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets UpdatedByUserId value.
        /// </summary>
        public int UpdatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted value.
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Indicates if deleted records should be ignored
        /// </summary>
        public bool? IncludeDeleted { get; set; }

        /// <summary>
        /// Gets or sets Loading Type.
        /// </summary>
        public LoadingTypeEnum LoadingType { get; set; }

        #endregion
    }
}
