using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.FrequencyTests;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class FrequencyTestsManager : BaseManager
    {
        #region Private Variables

        private readonly IFrequencyTestRepository _frequencyTestsRepository;

        #endregion

        #region Private Related Managers



        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrequencyTestsManager()
        {
            _frequencyTestsRepository = new FrequencyTestDatabaseRepository();
        }

        #endregion

        #region Public Methods

        #region Frequency Tests

        /// <summary>
        /// Gets a frequencyTest.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public FrequencyTest GetFrequencyTestById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _frequencyTestsRepository.LoadFrequencyTestById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        
        /// <summary>
        /// Gets a list of frequencyTests.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<FrequencyTest> GetFrequencyTests(FrequencyTestsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _frequencyTestsRepository.LoadFrequencyTests(filter.PatientId, filter.Name, filter.UserId, null, null);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the frequencyTest.
        /// </summary>
        /// <param name="frequencyTest">The frequencyTest.</param>
        /// <returns></returns>
        public ProcessResult SaveFrequencyTest(FrequencyTest frequencyTest)
        {
            Check.Argument.IsNotNull(() => frequencyTest);

            if (!frequencyTest.IsChanged) return ProcessResult.Succeed;

            try
            {
                frequencyTest.SetUserAndDates();

                var processResult = _frequencyTestsRepository.Save(frequencyTest);

                if (!processResult.IsSucceed || frequencyTest.FrequencyTestResults == null)
                    return processResult;

                processResult = SaveFrequencyTestResults(frequencyTest.FrequencyTestResults);

                if (processResult.IsSucceed)
                {
                    frequencyTest.ObjectState = DomainEntityState.Unchanged;
                }

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
        /// Deletes a frequencyTest.
        /// </summary>
        /// <param name="frequencyTest">The frequencyTest.</param>
        /// <returns></returns>
        public ProcessResult DeleteFrequencyTest(FrequencyTest frequencyTest)
        {
            Check.Argument.IsNotNull(() => frequencyTest);

            try
            {
                ProcessResult processResult;

                frequencyTest.FrequencyTestResults = GetFrequencyTestResults(new FrequencyTestResultsFilter { FrequencyTestId = frequencyTest.Id });

                if (frequencyTest.FrequencyTestResults != null)
                {
                    processResult = DeleteFrequencyTestResults(frequencyTest.FrequencyTestResults);

                    if (!processResult.IsSucceed)
                        return processResult;
                }

                processResult = _frequencyTestsRepository.Delete(frequencyTest);

                if (processResult.IsSucceed) { frequencyTest.ObjectState = DomainEntityState.Deleted; }

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

        #region Frequency Test Results

        /// <summary>
        /// Gets a frequencyTestResult.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public FrequencyTestResult GetFrequencyTestResultById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _frequencyTestsRepository.LoadFrequencyTestResultById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        
        /// <summary>
        /// Gets a list of frequencyTestResults.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<FrequencyTestResult> GetFrequencyTestResults(FrequencyTestResultsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _frequencyTestsRepository.LoadFrequencyTestResults(filter.FrequencyTestId, filter.ItemId, filter.UserId, null, null);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed frequency Test Result list.
        /// </summary>
        public ProcessResult SaveFrequencyTestResults(BindingList<FrequencyTestResult> frequencyTestResults)
        {
            Check.Argument.IsNotNull(() => frequencyTestResults);

            try
            {
                foreach (var frequencyTestResult in frequencyTestResults)
                {

                    var result = frequencyTestResult.ObjectState == DomainEntityState.Deleted ? DeleteFrequencyTestResult(frequencyTestResult) : SaveFrequencyTestResult(frequencyTestResult);

                    if (!result.IsSucceed)
                        return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes the passed frequency Test Result list.
        /// </summary>
        public ProcessResult DeleteFrequencyTestResults(BindingList<FrequencyTestResult> frequencyTestResults)
        {
            Check.Argument.IsNotNull(() => frequencyTestResults);

            try
            {
                foreach (var frequencyTestResult in frequencyTestResults)
                {
                    var result = DeleteFrequencyTestResult(frequencyTestResult);

                    if (!result.IsSucceed)
                        return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the frequencyTestResult.
        /// </summary>
        /// <param name="frequencyTestResult">The frequencyTestResult.</param>
        /// <returns></returns>
        public ProcessResult SaveFrequencyTestResult(FrequencyTestResult frequencyTestResult)
        {
            Check.Argument.IsNotNull(() => frequencyTestResult);

            try
            { 
                frequencyTestResult.SetUserAndDates();

                var processResult = _frequencyTestsRepository.Save(frequencyTestResult);

                if (processResult.IsSucceed ) { frequencyTestResult.ObjectState = DomainEntityState.Unchanged; }

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
        /// Deletes a frequencyTestResult.
        /// </summary>
        /// <param name="frequencyTestResult">The frequencyTestResult.</param>
        /// <returns></returns>
        public ProcessResult DeleteFrequencyTestResult(FrequencyTestResult frequencyTestResult)
        {
            Check.Argument.IsNotNull(() => frequencyTestResult);

            try
            {
                var  processResult = _frequencyTestsRepository.Delete(frequencyTestResult);

                if (processResult.IsSucceed) { frequencyTestResult.ObjectState = DomainEntityState.Deleted; }

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