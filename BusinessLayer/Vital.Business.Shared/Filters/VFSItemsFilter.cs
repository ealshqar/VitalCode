using Vital.Business.Shared.DomainObjects.VitalForceSheet;

namespace Vital.Business.Shared.Filters
{
    public class VFSItemsFilter : BaseFilter<VFSItem>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the Vfs Id.
        /// </summary>
        public int VfsId { get; set; }

        /// <summary>
        /// Gets or sets the ItemSourceId.
        /// </summary>
        public int ItemSourceId { get; set; }

        /// <summary>
        /// Gets or sets the ItemId.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets the PreviousV1.
        /// </summary>
        public string PreviousV1 { get; set; }

        /// <summary>
        /// Gets or sets the PreviousV1.
        /// </summary>
        public string PreviousV2 { get; set; }

        /// <summary>
        /// Gets or sets the CurrentV1.
        /// </summary>
        public string CurrentV1 { get; set; }

        /// <summary>
        /// Gets or sets the CurrentV2.
        /// </summary>
        public string CurrentV2 { get; set; }

        /// <summary>
        /// Gets or sets the IsSkipped.
        /// </summary>
        public bool IsSkipped { get; set; }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments { get; set; }

        #endregion
    }
}
