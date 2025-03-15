using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.FrequencyTests
{
    public class FrequencyTestDatabaseRepository : BaseRepository,IFrequencyTestRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<FrequencyTestEntity>, IPathEdgeRootParser<FrequencyTestEntity>> _pathEdgesFrequencyTest =
         p => p.Prefetch<FrequencyTestResultEntity>(c => c.FrequencyTestResults)
                        .SubPath(r => r.Prefetch<ItemEntity>(sr => sr.Item)
                                                .SubPath(it => it.Prefetch<ItemPropertyEntity>(itt => itt.Properties)
                                                                .SubPath(ipe=>ipe.Prefetch(ip=>ip.Property))))
                      .Prefetch(c => c.Patient);

        private readonly Func<IPathEdgeRootParser<FrequencyTestResultEntity>, IPathEdgeRootParser<FrequencyTestResultEntity>> _pathEdgesFrequencyTestResult =
            p => p.Prefetch<ItemEntity>(c => c.Item).SubPath(it =>
                                            it.Prefetch<ItemPropertyEntity>(itt => itt.Properties)
                                                        .SubPath(ipe => ipe.Prefetch(ip => ip.Property)))
                      .Prefetch(c => c.FrequencyTest);

        #endregion
        
        #region Public Methods

        #region FrequencyTests
        
        /// <summary>
        /// Loads FrequencyTest by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The FrequencyTest</returns>
        public FrequencyTest LoadFrequencyTestById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.FrequencyTest.Where(c => c.Id == id).WithPath(_pathEdgesFrequencyTest);

                    var frequencyTest = src.FirstOrDefault();

                    var frequencyTestObj = new FrequencyTest();

                    Mapper.Map(frequencyTest, frequencyTestObj);

                    return frequencyTestObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of FrequencyTests.
        /// </summary>
        /// <returns>List of FrequencyTests.</returns>
        public BindingList<FrequencyTest> LoadFrequencyTests(int patientId,string name, int userId, DateTime? creationDateTime, DateTime? updatedDateTime)
        {
            try
            {
                return LoadFrequencyTestsWorker(patientId, name , userId,creationDateTime,updatedDateTime, _pathEdgesFrequencyTest);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        /// <summary>
        /// Saves a frequencyTest.
        /// </summary>
        /// <param name="frequencyTestToSave">The frequencyTest.</param>
        /// <returns>The frequencyTest.</returns>
        public ProcessResult Save(FrequencyTest frequencyTestToSave)
        {
            Check.Argument.IsNotNull(frequencyTestToSave, "frequencyTest to save");

            try
            {
                var frequencyTestEntity = Mapper.Map<FrequencyTest, FrequencyTestEntity>(frequencyTestToSave);

                frequencyTestEntity.IsNew = frequencyTestEntity.Id <= 0;

                var processResult = CommonRepository.Save(frequencyTestEntity);

                frequencyTestToSave.Id = frequencyTestEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a frequencyTest.
        /// </summary>
        /// <param name="frequencyTestToDelete">The frequencyTest.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(FrequencyTest frequencyTestToDelete)
        {
            Check.Argument.IsNotNull(frequencyTestToDelete, "frequencyTest to delete");

            try
            {
                var frequencyTestEntity = Mapper.Map<FrequencyTest, FrequencyTestEntity>(frequencyTestToDelete);

                return CommonRepository.Delete(frequencyTestEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion
        
        #region FrequencyTestResults
        
        /// <summary>
        /// Loads FrequencyTestResult by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The FrequencyTestResult</returns>
        public FrequencyTestResult LoadFrequencyTestResultById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.FrequencyTestResult.Where(c => c.Id == id).WithPath(_pathEdgesFrequencyTestResult);

                    var frequencyTestResult = src.FirstOrDefault();

                    var frequencyTestResultObj = new FrequencyTestResult();

                    Mapper.Map(frequencyTestResult, frequencyTestResultObj);

                    return frequencyTestResultObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of FrequencyTestResults.
        /// </summary>
        /// <returns>List of FrequencyTestResults.</returns>
        public BindingList<FrequencyTestResult> LoadFrequencyTestResults(int frequencyTestId, int itemId,  int userId, DateTime? creationDateTime, DateTime? updatedDateTime)
        {
            try
            {
                return LoadFrequencyTestResultsWorker(frequencyTestId, itemId, userId, creationDateTime, updatedDateTime, _pathEdgesFrequencyTestResult);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        /// <summary>
        /// Saves a frequencyTestResult.
        /// </summary>
        /// <param name="frequencyTestResultToSave">The frequencyTestResult.</param>
        /// <returns>The frequencyTestResult.</returns>
        public ProcessResult Save(FrequencyTestResult frequencyTestResultToSave)
        {
            Check.Argument.IsNotNull(frequencyTestResultToSave, "frequencyTestResult to save");

            try
            {
                var frequencyTestResultEntity = Mapper.Map<FrequencyTestResult, FrequencyTestResultEntity>(frequencyTestResultToSave);

                frequencyTestResultEntity.IsNew = frequencyTestResultEntity.Id <= 0;

                var processResult = CommonRepository.Save(frequencyTestResultEntity);

                frequencyTestResultToSave.Id = frequencyTestResultEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a frequencyTestResult.
        /// </summary>
        /// <param name="frequencyTestResultToDelete">The frequencyTestResult.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(FrequencyTestResult frequencyTestResultToDelete)
        {
            Check.Argument.IsNotNull(frequencyTestResultToDelete, "frequencyTestResult to delete");

            try
            {
                var frequencyTestResultEntity = Mapper.Map<FrequencyTestResult, FrequencyTestResultEntity>(frequencyTestResultToDelete);

                return CommonRepository.Delete(frequencyTestResultEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of FrequencyTests.
        /// </summary>
        /// <returns></returns>
        private static BindingList<FrequencyTest> LoadFrequencyTestsWorker(int patientId, string name, int userId, DateTime? creationDatetTime, DateTime? updatedDateTime, Func<IPathEdgeRootParser<FrequencyTestEntity>, IPathEdgeRootParser<FrequencyTestEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var result = source.FrequencyTest.WithPath(pathEdges).AsQueryable();

                if (patientId > 0)
                    result = result.Where(c => c.PatientId == patientId);

                var frequencyTests = new BindingList<FrequencyTest>();

                Mapper.Map(result.ToList(), frequencyTests);

                return frequencyTests;
            }
        }
       
        /// <summary>
        /// Loads a list of FrequencyTestResults.
        /// </summary>
        /// <returns></returns>
        private static BindingList<FrequencyTestResult> LoadFrequencyTestResultsWorker(int frequencyTestId, int itemId, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, Func<IPathEdgeRootParser<FrequencyTestResultEntity>, IPathEdgeRootParser<FrequencyTestResultEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var result = source.FrequencyTestResult.WithPath(pathEdges).AsQueryable();

                if (frequencyTestId > 0)
                    result = result.Where(c => c.FrequencyTestId == frequencyTestId);

                if (itemId > 0)
                    result = result.Where(c => c.ItemId == itemId);

                var frequencyTestResults = new BindingList<FrequencyTestResult>();

                Mapper.Map(result.ToList(), frequencyTestResults);

                return frequencyTestResults;
            }
        }

        #endregion
    }
} 