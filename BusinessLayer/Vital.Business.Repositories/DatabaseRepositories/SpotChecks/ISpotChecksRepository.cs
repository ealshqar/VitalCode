using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.SpotChecks
{
    public interface ISpotChecksRepository
    {
        #region Spot Checks

        /// <summary>
        /// Loads a spot check by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        SpotCheck LoadSpotCheckById(int id);

        /// <summary>
        /// Loads a list of spot checks.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="testId"></param>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <param name="creationDateTime"></param>
        /// <param name="updatedDateTime"></param>
        /// <returns></returns>
        BindingList<SpotCheck> LoadSpotChecks(int patientId, int testId, string name, int userId, DateTime? creationDateTime, DateTime? updatedDateTime);

        /// <summary>
        /// Saves a spot check.
        /// </summary>
        /// <param name="spotCheck">The spot check</param>
        /// <returns></returns>
        ProcessResult Save(SpotCheck spotCheck);

        /// <summary>
        /// Deletes a spot check.
        /// </summary>
        /// <param name="spotCheck">The spot check.</param>
        /// <returns></returns>
        ProcessResult Delete(SpotCheck spotCheck);

        #endregion

        #region Spot Check Results

        /// <summary>
        /// Loads a spot check result by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        SpotCheckResult LoadSpotCheckResultById(int id);

        /// <summary>
        /// Loads a list of spot check results.
        /// </summary>
        /// <param name="spotCheckId">The spot check id.</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="creationDateTime">The creation date time.</param>
        /// <param name="updatedDateTime">The updated date time.</param>
        /// <returns></returns>
        BindingList<SpotCheckResult> LoadSpotCheckResults(int spotCheckId, int itemId,  int userId, DateTime? creationDateTime, DateTime? updatedDateTime);

        /// <summary>
        /// Saves the spot check result.
        /// </summary>
        /// <param name="spotCheckResult"></param>
        /// <returns></returns>
        ProcessResult Save(SpotCheckResult spotCheckResult );

        /// <summary>
        /// Deletes a spot check result.
        /// </summary>
        /// <param name="spotCheckResult">The spot check result.</param>
        /// <returns></returns>
        ProcessResult Delete(SpotCheckResult spotCheckResult);

        #endregion
    }
}
