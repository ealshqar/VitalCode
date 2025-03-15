using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.TestSchedules
{
    public class TestSchedulesDatabaseRepository : BaseRepository, ITestSchedulesRepository
    {

        #region PathEdges

        private readonly Func<IPathEdgeRootParser<TestScheduleEntity>, IPathEdgeRootParser<TestScheduleEntity>>
            _pathEdgesTestSchedule = c => c.Prefetch(ts => ts.ScheduleLines).Prefetch(tsr => tsr.EvalPeriodType);

        private readonly Func<IPathEdgeRootParser<ScheduleLineEntity>, IPathEdgeRootParser<ScheduleLineEntity>>
            _pathEdgesScheduleLine = c => c.Prefetch(scheduleLine => scheduleLine.Item).
                                            Prefetch(scheduleLine => scheduleLine.TestSchedule);
        
        #endregion

        #region Test Schedules

        #region Public Methods
        /// <summary>
        /// Loads Test Schedule
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Test Schedule</returns>
        public TestSchedule LoadTestScheduleById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestSchedule.Where(c => c.Id == id).WithPath(_pathEdgesTestSchedule);

                    var testScheduleEntity = src.FirstOrDefault();

                    var testSchedule = new TestSchedule();

                    Mapper.Map(testScheduleEntity, testSchedule);

                    return testSchedule;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Test schedules.
        /// </summary>
        /// <returns>List of Test schedules.</returns>
        public BindingList<TestSchedule> LoadTestSchedules(string technical, string address, string city, string state, string zip, string phone,
          decimal fee, int reevalInWeeks)
        {
            try
            {
                return LoadTestSchedulesWorker(technical, address, city , state, zip , phone , fee , reevalInWeeks, _pathEdgesTestSchedule);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a test schedule.
        /// </summary>
        /// <param name="testSchedule">The test schedule.</param>
        /// <returns>The testProtocol.</returns>
        public ProcessResult Save(TestSchedule testSchedule)
        {
            Check.Argument.IsNotNull(testSchedule, "testProtocol to save");

            try
            {
                var testScheduleEntity = Mapper.Map<TestSchedule, TestScheduleEntity>(testSchedule);

                testScheduleEntity.IsNew = testScheduleEntity.Id > 0 ? false : true;

                var processResult = CommonRepository.Save(testScheduleEntity);

                testSchedule.Id = testScheduleEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a test schedule.
        /// </summary>
        /// <param name="testScheduleToBeDelete">The test schedule.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestSchedule testScheduleToBeDelete)
        {
            Check.Argument.IsNotNull(testScheduleToBeDelete, "testProtocol to delete");

            try
            {
                var testScheduleEntity = Mapper.Map<TestSchedule, TestScheduleEntity>(testScheduleToBeDelete);

                return CommonRepository.Delete(testScheduleEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="technical"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <param name="fee"></param>
        /// <param name="reevalInWeeks"></param>
        /// <param name="pathEdgesTestSchedule"></param>
        /// <returns></returns>
        private static BindingList<TestSchedule> LoadTestSchedulesWorker(string technical, string address, string city, string state, string zip, string phone, decimal fee, int reevalInWeeks, Func<IPathEdgeRootParser<TestScheduleEntity>,IPathEdgeRootParser<TestScheduleEntity>> pathEdgesTestSchedule)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<TestScheduleEntity> src = data.TestSchedule;

                if (pathEdgesTestSchedule != null)
                    src = src.WithPath(pathEdgesTestSchedule);
                
                var testScheduleEntities = src.ToList();

                var testSchedulesObjList = new BindingList<TestSchedule>();

                Mapper.Map(testScheduleEntities, testSchedulesObjList);

                return testSchedulesObjList;
            }
        }

        #endregion

        #endregion

        #region Schedule Lines

        #region Public Methods

        /// <summary>
        /// Loads Schedule Line
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Schedule Line</returns>
        public ScheduleLine LoadScheduleLineById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.ScheduleLine.Where(c => c.Id == id);

                    var scheduleLineEntity = src.FirstOrDefault();

                    var scheduleLine = new ScheduleLine();

                    Mapper.Map(scheduleLine, scheduleLineEntity);

                    return scheduleLine;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of schedule lines.
        /// </summary>
        /// <param name="testScheduleId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public BindingList<ScheduleLine> LoadScheduleLines(int testScheduleId, decimal price)
        {
            try
            {
                return LoadScheduleLinesWorker(testScheduleId, price , _pathEdgesScheduleLine);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        


        /// <summary>
        /// Saves a schedule line.
        /// </summary>
        /// <param name="scheduleLine">The schedule line.</param>
        /// <returns>The schedule line.</returns>
        public ProcessResult Save(ScheduleLine scheduleLine)
        {
            Check.Argument.IsNotNull(scheduleLine, "scheduleLine to save");

            try
            {
                var scheduleLineEntity = Mapper.Map<ScheduleLine, ScheduleLineEntity>(scheduleLine);

                scheduleLineEntity.IsNew = scheduleLineEntity.Id > 0 ? false : true;

                var processResult = CommonRepository.Save(scheduleLineEntity);

                scheduleLine.Id = scheduleLineEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a scheduleLine.
        /// </summary>
        /// <param name="scheduleLineToBeDelete">The schedule line.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(ScheduleLine scheduleLineToBeDelete)
        {
            Check.Argument.IsNotNull(scheduleLineToBeDelete, "scheduleLineToBeDelete to delete");

            try
            {
                var scheduleLineEntity = Mapper.Map<ScheduleLine, ScheduleLineEntity>(scheduleLineToBeDelete);

                return CommonRepository.Delete(scheduleLineEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        #endregion

        #region Private Methods

        private static BindingList<ScheduleLine> LoadScheduleLinesWorker(int testScheduleId, decimal price, Func<IPathEdgeRootParser<ScheduleLineEntity>, IPathEdgeRootParser<ScheduleLineEntity>> pathEdgesScheduleLine)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<ScheduleLineEntity> src = data.ScheduleLine;

                if (pathEdgesScheduleLine != null)
                    src = src.WithPath(pathEdgesScheduleLine);

                if (testScheduleId > 0)
                    src = src.Where(cc => cc.TestScheduleId == testScheduleId);

                var scheduleLineEntities = src.ToList();

                var scheduleLinesObjList = new BindingList<ScheduleLine>();

                Mapper.Map(scheduleLineEntities, scheduleLinesObjList);

                return scheduleLinesObjList;
            }
        }

        #endregion

        #endregion
    }
}
