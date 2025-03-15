using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.TestSchedules;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class TestSchedulesManager : BaseManager
    {
        #region Private Variables

		private readonly ITestSchedulesRepository _testSchedulesRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// The Constructor
		/// </summary>
        public TestSchedulesManager()
		{
			_testSchedulesRepository = new TestSchedulesDatabaseRepository();
		}

		#endregion

        #region Public Methods

        #region Test Schedules

        /// <summary>
        /// Gets single Test Schedule By Id.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        /// <returns>The Item.</returns>
        public TestSchedule GetTestScheduleById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testSchedulesRepository.LoadTestScheduleById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }

        }

        /// <summary>
        /// Gets Test Schedules based on the passed filter.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        /// <returns>List of Test Schedules.</returns>
        public BindingList<TestSchedule> GetTestSchedules(TestSchedulesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testSchedulesRepository.LoadTestSchedules(filter.Technical, filter.Address, filter.City,
                                                                  filter.State, filter.Zip, filter.Phone, filter.Fee,
                                                                  filter.ReevalInWeeks);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed Test Schedule.
        /// </summary>
        /// <param name="testSchedule">The test schedule</param>
        /// <returns></returns>
        public ProcessResult SaveTestSchedule(TestSchedule testSchedule)
        {
            Check.Argument.IsNotNull(() => testSchedule);

            if (!testSchedule.IsChanged) return ProcessResult.Succeed;

            try
            {
                testSchedule.SetUserAndDates();

                var processResult = _testSchedulesRepository.Save(testSchedule);

                if (processResult.IsSucceed) { testSchedule.ObjectState = DomainEntityState.Unchanged; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes the passed Test Schedule.
        /// </summary>
        /// <param name="testSchedule"></param>
        /// <returns></returns>
        public ProcessResult DeleteItem(TestSchedule testSchedule)
        {
            Check.Argument.IsNotNull(() => testSchedule);

            try
            {
                var processResult = _testSchedulesRepository.Delete(testSchedule);

                if (processResult.IsSucceed) { testSchedule.ObjectState = DomainEntityState.Deleted; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion


        #region Schedule Lines


        /// <summary>
        /// Gets single Schedule Line By Id.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        /// <returns>The Item.</returns>
        public ScheduleLine GetScheduleLineById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testSchedulesRepository.LoadScheduleLineById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }

        }

        /// <summary>
        /// Gets Schedule Lines based on the passed filter.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        /// <returns>List of Test Schedules.</returns>
        public BindingList<ScheduleLine> GetScheduleLines(ScheduleLinesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testSchedulesRepository.LoadScheduleLines(filter.TestScheduleId,filter.Price);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed Test Schedule.
        /// </summary>
        /// <param name="scheduleLine">The schedule line.</param>
        /// <returns></returns>
        public ProcessResult SaveScheduleLine(ScheduleLine scheduleLine)
        {
            Check.Argument.IsNotNull(() => scheduleLine);

            if (!scheduleLine.IsChanged) return ProcessResult.Succeed;

            try
            {
                scheduleLine.SetUserAndDates();

                var processResult = _testSchedulesRepository.Save(scheduleLine);

                if (processResult.IsSucceed) { scheduleLine.ObjectState = DomainEntityState.Unchanged; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes the passed Schedule Line.
        /// </summary>
        /// <param name="scheduleLine"></param>
        /// <returns></returns>
        public ProcessResult DeleteScheduleLine(ScheduleLine scheduleLine)
        {
            Check.Argument.IsNotNull(() => scheduleLine);

            try
            {
                var processResult = _testSchedulesRepository.Delete(scheduleLine);

                if (processResult.IsSucceed) { scheduleLine.ObjectState = DomainEntityState.Deleted; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        #endregion

        #endregion
    }
}
