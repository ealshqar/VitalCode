using System;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.FrequencyTests
{
    public interface IFrequencyTestRepository
    {
        /// <summary>
        /// Loads FrequencyTest by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The FrequencyTest</returns>
        FrequencyTest LoadFrequencyTestById(int id);
        
        /// <summary>
        /// Loads a list of FrequencyTests.
        /// </summary>
        /// <returns>List of FrequencyTests.</returns>
        BindingList<FrequencyTest> LoadFrequencyTests(int patientId, string name, int userId, DateTime? creationDateTime, DateTime? updatedDateTime);
        
        /// <summary>
        /// Saves a frequencyTest.
        /// </summary>
        /// <param name="frequencyTestToSave">The frequencyTest.</param>
        /// <returns>The frequencyTest.</returns>
        ProcessResult Save(FrequencyTest frequencyTestToSave);        

        /// <summary>
        /// Deletes a frequencyTest.
        /// </summary>
        /// <param name="frequencyTestToDelete">The frequencyTest.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(FrequencyTest frequencyTestToDelete);       

        /// <summary>
        /// Loads FrequencyTestResult by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The FrequencyTestResult</returns>
        FrequencyTestResult LoadFrequencyTestResultById(int id);
        
        /// <summary>
        /// Loads a list of FrequencyTestResults.
        /// </summary>
        /// <returns>List of FrequencyTestResults.</returns>
        BindingList<FrequencyTestResult> LoadFrequencyTestResults(int frequencyTestId, int itemId, int userId, DateTime? creationDateTime, DateTime? updatedDateTime);
        
        /// <summary>
        /// Saves a frequencyTestResult.
        /// </summary>
        /// <param name="frequencyTestResultToSave">The frequencyTestResult.</param>
        /// <returns>The frequencyTestResult.</returns>
        ProcessResult Save(FrequencyTestResult frequencyTestResultToSave);        

        /// <summary>
        /// Deletes a frequencyTestResult.
        /// </summary>
        /// <param name="frequencyTestResultToDelete">The frequencyTestResult.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(FrequencyTestResult frequencyTestResultToDelete);
    }
} 