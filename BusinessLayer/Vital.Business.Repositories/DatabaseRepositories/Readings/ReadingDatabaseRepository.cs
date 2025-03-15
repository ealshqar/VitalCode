using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Readings
{
    public class ReadingDatabaseRepository : BaseRepository, IReadingRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<ReadingEntity>, IPathEdgeRootParser<ReadingEntity>> _pathEdgesReading
            =
            p => p.Prefetch<ItemEntity>(c => c.Item)
                     .SubPath(zz => zz.Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                        .SubPath(e => e.Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data)))
                    .Prefetch(cc => cc.Test)
                    .Prefetch(lt => lt.ListPointLookup)
                    .Prefetch(ccc => ccc.User);

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads reading by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The reading</returns>
        public Reading LoadReadingById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Reading.Where(c => c.Id == id).WithPath(_pathEdgesReading);

                    var reading = src.FirstOrDefault();

                    var readingObj = new Reading();

                    Mapper.Map(reading, readingObj);

                    return readingObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of readings.
        /// </summary>
        /// <returns>List of readings.</returns>
        public BindingList<Reading> LoadReadings(int testId, DateTime? dateTime, int itemId,int pointSetItemId, int listPointLookupId,
                                                 int? max, int? min, int? fall, int? rise, int? value)
        {
            try
            {
                return LoadReadingsWorker(testId, dateTime, itemId,pointSetItemId, listPointLookupId, max, min, fall, rise, value,
                                          _pathEdgesReading);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }


        /// <summary>
        /// Saves a reading.
        /// </summary>
        /// <param name="readingToSave">The reading.</param>
        /// <returns>The result.</returns>
        public ProcessResult Save(Reading readingToSave)
        {
            Check.Argument.IsNotNull(readingToSave, "reading to save");

            try
            {
                var readingEntity = Mapper.Map<Reading, ReadingEntity>(readingToSave);

                readingEntity.IsNew = readingEntity.Id <= 0;

                var processResult = CommonRepository.Save(readingEntity);

                readingToSave.Id = readingEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a reading.
        /// </summary>
        /// <param name="readingToDelete">The reading.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(Reading readingToDelete)
        {
            Check.Argument.IsNotNull(readingToDelete, "reading to delete");

            try
            {
                var readingEntity = Mapper.Map<Reading, ReadingEntity>(readingToDelete);

                return CommonRepository.Delete(readingEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of readings.
        /// </summary>
        /// <param name="testId">The test id.</param>
        /// <param name="dateTime">The date time</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="listPointLookupId">The listPointLookupId.</param>
        /// <param name="max">The max value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="fall">The fall value.</param>
        /// <param name="rise">The rise value.</param>
        /// <param name="value">The value.</param>
        /// <param name="pathEdges">The path edges.</param>
        /// <returns></returns>
        private static BindingList<Reading> LoadReadingsWorker(int testId, DateTime? dateTime, int itemId, int pointSetItemId,
                                                               int listPointLookupId, int? max, int? min, int? fall,
                                                               int? rise, int? value,
                                                               Func
                                                                   <IPathEdgeRootParser<ReadingEntity>,
                                                                   IPathEdgeRootParser<ReadingEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.Reading.WithPath(pathEdges);

                if (testId > 0)
                    src = src.Where(c => c.TestId == testId);

                if (pointSetItemId > 0)
                    src = src.Where(c => c.PointSetItemId == pointSetItemId);

                if (itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);

                if (dateTime != null)
                    src = src.Where(c => c.DateTime.Equals(dateTime));

                if (max != null)
                    src = src.Where(c => c.Max == max);

                if (min != null)
                    src = src.Where(c => c.Min == min);

                if (fall != null)
                    src = src.Where(c => c.Fall == fall);

                if (rise != null)
                    src = src.Where(c => c.Rise == rise);

                if (value != null)
                    src = src.Where(c => c.Value == value);

                if (listPointLookupId > 0)
                    src = src.Where(c => c.ListPointLookupId == listPointLookupId);

                var readings = src.ToList();

                var readingsObjList = new BindingList<Reading>();

                Mapper.Map(readings, readingsObjList);

                return readingsObjList;
            }
        }

        #endregion
    }
}
