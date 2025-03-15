using Vital.Business.Shared.DomainObjects.Patients;

namespace Vital.Business.Shared.Filters
{
    public class PatientHistoryFilter : BaseFilter<PatientHistory>
    {
        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public int PatientId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type
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
    }
}
