using System;

namespace Vital.Business.Shared.Filters
{
    public class TestResultFactorsFilter
    {
        /// <summary>
        /// Get or set the FactorId.
        /// </summary>
        public int FactorId { get; set; }

        /// <summary>
        /// Get or set the PotencyId.
        /// </summary>
        public int PotencyId { get; set; }

        /// <summary>
        /// Get or set the TestResultId.
        /// </summary>
        public int TestResultId { get; set; }

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
