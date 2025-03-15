using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.SpotChecks;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class SpotCheckManager : BaseManager
    {
        #region Private Variables

        private readonly ISpotChecksRepository _spotChecksRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public SpotCheckManager()
        {
            _spotChecksRepository = new SpotChecksDatabaseRepository();
        }

        #endregion

        #region Spot Checks

        #region Public Methods

        /// <summary>
        /// Gets a spot check.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public SpotCheck GetSpotCheckById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _spotChecksRepository.LoadSpotCheckById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of spot checks.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<SpotCheck> GetSpotChecks(SpotChecksFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _spotChecksRepository.LoadSpotChecks(filter.PatientId, filter.TestId, filter.Name, filter.UserId, null, null);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the spot check.
        /// </summary>
        /// <param name="spotCheck">The spot check.</param>
        /// <returns></returns>
        public ProcessResult SaveSpotCheck(SpotCheck spotCheck)
        {
            Check.Argument.IsNotNull(() => spotCheck);

            if (!spotCheck.IsChanged) return ProcessResult.Succeed;

            try
            {
                spotCheck.SetUserAndDates();

                var processResult = _spotChecksRepository.Save(spotCheck);

                if (!processResult.IsSucceed || spotCheck.SpotCheckResults == null)
                    return processResult;

                processResult =  SaveSpotCheckResults(spotCheck.SpotCheckResults);

                if (processResult.IsSucceed)
                {
                    spotCheck.ObjectState = DomainEntityState.Unchanged;
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
        /// Deletes a spot check.
        /// </summary>
        /// <param name="spotCheck">The spot check.</param>
        /// <returns></returns>
        public ProcessResult DeleteSpotCheck(SpotCheck spotCheck)
        {
            Check.Argument.IsNotNull(() => spotCheck);

            try
            {
                ProcessResult processResult;

                spotCheck.SpotCheckResults = GetSpotCheckResults(new SpotCheckResultsFilter {SpotCheckId = spotCheck.Id});

                if (spotCheck.SpotCheckResults != null)
                {
                    processResult = DeleteSpotCheckResults(spotCheck.SpotCheckResults);

                    if (!processResult.IsSucceed)
                        return processResult;
                }

                processResult = _spotChecksRepository.Delete(spotCheck);

                if (processResult.IsSucceed) { spotCheck.ObjectState = DomainEntityState.Deleted; }

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

        #region Spot Check Results

        #region Public Methods

        /// <summary>
        /// Gets a spot check result.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public SpotCheckResult GetSpotCheckResultById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _spotChecksRepository.LoadSpotCheckResultById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of spot checks results.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<SpotCheckResult> GetSpotCheckResults(SpotCheckResultsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _spotChecksRepository.LoadSpotCheckResults(filter.SpotCheckId, filter.ItemId,filter.UserId,null , null);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed spot check result list.
        /// </summary>
        public ProcessResult SaveSpotCheckResults(BindingList<SpotCheckResult> spotCheckResults)
        {
            Check.Argument.IsNotNull(() => spotCheckResults);

            try
            {
                foreach (var spotCheckResult in spotCheckResults)
                {

                   var result = spotCheckResult.ObjectState == DomainEntityState.Deleted ? DeleteSpotCheckResult(spotCheckResult) : SaveSpotCheckResult(spotCheckResult);

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
        /// Deletes the passed spot check result list.
        /// </summary>
        public ProcessResult DeleteSpotCheckResults(BindingList<SpotCheckResult> spotCheckResults)
        {
            Check.Argument.IsNotNull(() => spotCheckResults);

            try
            {
                foreach (var spotCheckResult in spotCheckResults)
                {
                    var result = DeleteSpotCheckResult(spotCheckResult);

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
        /// Saves the spot check.
        /// </summary>
        /// <param name="spotCheckResult">The spot check.</param>
        /// <returns></returns>
        public ProcessResult SaveSpotCheckResult(SpotCheckResult spotCheckResult)
        {
            Check.Argument.IsNotNull(() => spotCheckResult);

            if (!spotCheckResult.IsChanged) return ProcessResult.Succeed;

            try
            {
                spotCheckResult.SetUserAndDates();

                var processResult = _spotChecksRepository.Save(spotCheckResult);

                if (processResult.IsSucceed)
                {
                    spotCheckResult.ObjectState = DomainEntityState.Unchanged;
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
        /// Deletes a spot check.
        /// </summary>
        /// <param name="spotCheckResult">The spot check.</param>
        /// <returns></returns>
        public ProcessResult DeleteSpotCheckResult(SpotCheckResult spotCheckResult)
        {
            Check.Argument.IsNotNull(() => spotCheckResult);

            try
            {
                var processResult = _spotChecksRepository.Delete(spotCheckResult);

                if (processResult.IsSucceed) { spotCheckResult.ObjectState = DomainEntityState.Deleted; }

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
