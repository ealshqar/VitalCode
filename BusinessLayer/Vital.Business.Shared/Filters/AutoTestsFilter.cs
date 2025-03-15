using System;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;

namespace Vital.Business.Shared.Filters
{
    public class AutoTestsFilter : BaseFilter<AutoTest>
    {
        /// <summary>
        /// Gets or sets the AutoTestId.
        /// </summary>
        public int AutoTestId { get; set; }
        
        /// <summary>
        /// Gets or sets the Patient.
        /// </summary>
        public int PatientId { get; set; }
        
        /// <summary>
        /// Gets or sets the AutoProtocolRevision.
        /// </summary>
        public int AutoProtocolRevisionsId { get; set; }
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Gets or sets the TestDate.
        /// </summary>
        public DateTime? TestDate { get; set; }
        
    }
}