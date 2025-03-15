using Vital.Business.Shared.DomainObjects.PatientSchedules;

namespace Vital.Business.Shared.Filters
{
    public class ScheduleLinesFilter : BaseFilter<ScheduleLine>
    {
        public int TestScheduleId
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }
    }
}
