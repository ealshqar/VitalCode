using Vital.Business.Shared.DomainObjects.FrequencyTests;

namespace Vital.Business.Shared.Filters
{
    public class FrequencyTestsFilter : BaseFilter<FrequencyTest>
    {
        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public int PatientId
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
        /// Gets or sets the Notes.
        /// </summary>
        public int Notes
        {
            get;
            set;
        }
    }
}