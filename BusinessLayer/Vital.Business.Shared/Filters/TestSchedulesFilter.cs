using Vital.Business.Shared.DomainObjects.PatientSchedules;

namespace Vital.Business.Shared.Filters
{
    public class TestSchedulesFilter : BaseFilter<TestSchedule>
    {
        public string Technical
        {
            get; set;
        }

        public string Address
        {
            get; set;
        }

        public string City
        {
            get; set;
        }

        public string State
        {
            get; set;
        }

        public string Zip
        {
            get; set;
        }

        public decimal Fee
        {
            get; set;
        }

        public string Phone
        {
            get; set;
        }

        public int ReevalInWeeks
        {
            get; set;
        }
    }
}
