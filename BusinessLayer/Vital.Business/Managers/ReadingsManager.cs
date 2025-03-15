using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.Readings;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class ReadingsManager : BaseManager
    {
        #region Private Variables

        private readonly IReadingRepository _readingsRepository;

        #endregion

        #region Constructors
            
        /// <summary>
        /// The Constructor.
        /// </summary>
        public ReadingsManager()
        {
            _readingsRepository = new ReadingDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a reading.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Reading GetReadingById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId,"item id");

            try
            {
                return _readingsRepository.LoadReadingById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of readings.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Reading> GetReadings(ReadingsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _readingsRepository.LoadReadings(filter.TestId , filter.DateTime , filter.ItemId,filter.PointSetItemId, filter.ListPointLookupId, filter.Max , filter.Min , filter.Rise , filter.Fall , filter.Value  );
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the reading.
        /// </summary>
        /// <param name="reading">The reading.</param>
        /// <returns></returns>
        public ProcessResult SaveReading(Reading reading)
        {
            Check.Argument.IsNotNull(() => reading);

            if (!reading.IsChanged) return ProcessResult.Succeed;

            try
            {
                reading.SetUserAndDates();

                var processResult = _readingsRepository.Save(reading);

                if (processResult.IsSucceed) { reading.ObjectState = DomainEntityState.Unchanged; }

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
        /// Saves a list of readings.
        /// </summary>
        /// <param name="readings">The readings.</param>
        /// <returns></returns>
        public ProcessResult SaveReadings(List<Reading> readings)
        {
            try
            {
                foreach (var reading in readings)
                {
                    reading.SetUserAndDates();

                    reading.DateTime = DateTime.Now;

                    _readingsRepository.Save(reading);

                    reading.ObjectState = DomainEntityState.Unchanged;
                }

                return new ProcessResult() {IsSucceed = true};
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a reading.
        /// </summary>
        /// <param name="reading">The reading</param>
        /// <returns></returns>
        public ProcessResult DeleteReading(Reading reading)
        {
            try
            {
                var processResult = _readingsRepository.Delete(reading);

                if (processResult.IsSucceed) { reading.ObjectState = DomainEntityState.Deleted; }

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
        /// Deletes a readings.
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns></returns>
        public ProcessResult DeleteReadings(ReadingsFilter filter)
        {
            try
            {
                var readings = _readingsRepository.LoadReadings(filter.TestId, filter.DateTime, filter.ItemId,filter.PointSetItemId, filter.ListPointLookupId, filter.Max, filter.Min, filter.Fall, filter.Rise, filter.Value);

                foreach (var reading in readings)
                {
                    var processResult = _readingsRepository.Delete(reading);

                    if (processResult.IsSucceed)
                    {
                        reading.ObjectState = DomainEntityState.Deleted;
                    }
                }

                return new ProcessResult() {IsSucceed = true};
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
           
        }

        #endregion
    }
}
