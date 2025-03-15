using Vital.Business.Shared.DomainObjects.SpotChecks;

namespace Vital.Business.Shared.Filters
{
    public class SpotChecksFilter : BaseFilter<SpotCheck>
    {
        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public int PatientId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the test id.
        /// </summary>
        public int TestId
        {
            get; set;
        }
    }
}
