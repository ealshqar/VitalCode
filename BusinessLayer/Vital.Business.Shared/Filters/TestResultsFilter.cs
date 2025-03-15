using System;
using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.Business.Shared.Filters
{
    public class TestResultsFilter : BaseFilter<TestResult>
    {
        /// <summary>
        /// Gets or sets the TestIssueId
        /// </summary>
        public int TestIssueId { get; set; }

        /// <summary>
        /// Gets or sets the ItemId
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets the DateTime
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Gets or sets the ParentId
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the VitalForceId
        /// </summary>
        public int VitalForceId { get; set; }
        
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
        /// 
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
