using System;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;

namespace Vital.Business.Shared.Filters
{
    public class VFSsFilter : BaseFilter<VFS>
    {
        #region Public Members

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the DateTime.
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Gets or sets the ThyroidNumOfIssues.
        /// </summary>
        public int ThyroidNumOfIssues { get; set; }

        /// <summary>
        /// Gets or sets the MercuryNumOfIssues.
        /// </summary>
        public int MercuryNumOfIssues { get; set; }

        /// <summary>
        /// Gets or sets the EmotionalIssues.
        /// </summary>
        public string EmotionalIssues { get; set; }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the TestId.
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Gets or sets the PatientId.
        /// </summary>
        public int PatientId { get; set; }

        #endregion
    }
}
