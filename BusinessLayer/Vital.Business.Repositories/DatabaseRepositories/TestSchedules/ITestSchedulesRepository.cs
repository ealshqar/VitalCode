using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.TestSchedules
{
    public interface ITestSchedulesRepository
    {
        #region Test Schedules

        /// <summary>
        /// Loads Test Schedule
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Test Schedule</returns>
        TestSchedule LoadTestScheduleById(int id);

        /// <summary>
        /// Loads a list of Test schedules.
        /// </summary>
        /// <returns>List of Test schedules.</returns>
        BindingList<TestSchedule> LoadTestSchedules(string technical, string address, string city , string state , string zip , string phone ,
          decimal fee, int reevalInWeeks);


        /// <summary>
        /// Saves a test schedule.
        /// </summary>
        /// <param name="testSchedule">The test schedule.</param>
        /// <returns>The testProtocol.</returns>
        ProcessResult Save(TestSchedule testSchedule);

        /// <summary>
        /// Deletes a test schedule.
        /// </summary>
        /// <param name="testSchedule">The test schedule.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestSchedule testSchedule);

        #endregion

        #region Schedule Lines

        /// <summary>
        /// Loads Schedule Line
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Schedule Line</returns>
        ScheduleLine LoadScheduleLineById(int id);

        /// <summary>
        /// Loads a list of schedule lines.
        /// </summary>
        /// <param name="testScheduleId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        BindingList<ScheduleLine> LoadScheduleLines(int testScheduleId, decimal price);


        /// <summary>
        /// Saves a schedule line.
        /// </summary>
        /// <param name="scheduleLine">The schedule line.</param>
        /// <returns>The schedule line.</returns>
        ProcessResult Save(ScheduleLine scheduleLine);

        /// <summary>
        /// Deletes a scheduleLine.
        /// </summary>
        /// <param name="scheduleLine">The schedule line.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(ScheduleLine scheduleLine);

        #endregion

    }
}
