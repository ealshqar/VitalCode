using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Readings
{
    public interface IReadingRepository
    {

        /// <summary>
        /// Loads reading by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The reading.</returns>
        Reading LoadReadingById(int id);

        /// <summary>
        /// Loads all readings.
        /// </summary>
        /// <returns>The list of the readings.</returns>
        BindingList<Reading> LoadReadings(int testId, DateTime? dateTime, int itemId,int pointSetItemId, int listPointLookupId, int? max, int? min, int? fall, int? rise, int? value);

        /// <summary>
        /// Saves the reading.
        /// </summary>
        /// <param name="readingToSave">The reading to save.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(Reading readingToSave);

        /// <summary>
        /// Deletes the reading.
        /// </summary>
        /// <param name="readingToDelete">The reading to delete.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Reading readingToDelete);
    }
}
