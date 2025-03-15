using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.SpotChecks
{
    public class SpotChecksDatabaseRepository : BaseRepository, ISpotChecksRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<SpotCheckEntity>, IPathEdgeRootParser<SpotCheckEntity>>
            _pathEdgeSpotCheck =
                p => p.Prefetch<SpotCheckResultEntity>(c => c.SpotCheckResults)
                        .SubPath(r => r.Prefetch<ItemEntity>(sr => sr.Item)
                                                .SubPath(it => it.Prefetch<ItemPropertyEntity>(itt => itt.Properties)
                                                                .SubPath(ipe=>ipe.Prefetch(ip=>ip.Property)))
                                       .Prefetch(sr => sr.ResultType))
                      .Prefetch(c => c.Patient);

        private readonly Func<IPathEdgeRootParser<SpotCheckResultEntity>, IPathEdgeRootParser<SpotCheckResultEntity>>
            _pathEdgeSpotCheckResult =
                p => p.Prefetch<ItemEntity>(c => c.Item).SubPath(it =>
                                            it.Prefetch<ItemPropertyEntity>(itt => itt.Properties)
                                                        .SubPath(ipe => ipe.Prefetch(ip => ip.Property)))
                      .Prefetch(c => c.ResultType)
                      .Prefetch(c => c.SpotCheck);


        #endregion

        #region Spot Checks

        /// <summary>
        /// Loads a spot check by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public SpotCheck LoadSpotCheckById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "Id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var spotCheckEntity = source.SpotCheck.WithPath(_pathEdgeSpotCheck).FirstOrDefault(f => f.Id == id);

                    if (spotCheckEntity == null) return null;

                    var spotCheck = Mapper.Map<SpotCheckEntity, SpotCheck>(spotCheckEntity);

                    return spotCheck;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of spot checks.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <param name="creationDateTime"></param>
        /// <param name="updatedDateTime"></param>
        /// <returns></returns>
        public BindingList<SpotCheck> LoadSpotChecks(int patientId,int testId, string name, int userId, DateTime? creationDateTime, DateTime? updatedDateTime)
        {
            try
            {
                return LoadSpotChecksWorker(patientId,testId, name , userId,creationDateTime,updatedDateTime,_pathEdgeSpotCheck);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a spot check.
        /// </summary>
        /// <param name="spotCheck">The spot check</param>
        /// <returns></returns>
        public ProcessResult Save(SpotCheck spotCheck)
        {
            Check.Argument.IsNotNull(() => spotCheck);

            try
            {
                var spotCheckEntity = Mapper.Map<SpotCheck, SpotCheckEntity>(spotCheck);

                spotCheckEntity.IsNew = spotCheckEntity.Id <= 0;

                var processResult = CommonRepository.Save(spotCheckEntity);

                spotCheck.Id = spotCheckEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a spot check.
        /// </summary>
        /// <param name="spotCheck">The spot check.</param>
        /// <returns></returns>
        public ProcessResult Delete(SpotCheck spotCheck)
        {
            Check.Argument.IsNotNull(() => spotCheck);

            try
            {
                var spotCheckEntity = Mapper.Map<SpotCheck, SpotCheckEntity>(spotCheck);

                return CommonRepository.Delete(spotCheckEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Spot Check Results

        /// <summary>
        /// Loads a spot check result by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public SpotCheckResult LoadSpotCheckResultById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "Id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var spotCheckResultEntity = source.SpotCheckResult.WithPath(_pathEdgeSpotCheckResult).FirstOrDefault(f => f.Id == id);

                    if (spotCheckResultEntity == null) return null;

                    var spotCheckResult = Mapper.Map<SpotCheckResultEntity, SpotCheckResult>(spotCheckResultEntity);

                    return spotCheckResult;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of spot check results.
        /// </summary>
        /// <param name="spotCheckId">The spot check id.</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="creationDateTime">The creation date time.</param>
        /// <param name="updatedDateTime">The updated date time.</param>
        /// <returns></returns>
        public BindingList<SpotCheckResult> LoadSpotCheckResults(int spotCheckId, int itemId,  int userId, DateTime? creationDateTime, DateTime? updatedDateTime)
        {
            try
            {
                return LoadSpotCheckResultsWorker(spotCheckId, itemId,  userId, creationDateTime, updatedDateTime, _pathEdgeSpotCheckResult);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        

        /// <summary>
        /// Saves the spot check result.
        /// </summary>
        /// <param name="spotCheckResult"></param>
        /// <returns></returns>
        public ProcessResult Save(SpotCheckResult spotCheckResult )
        {
            Check.Argument.IsNotNull(() => spotCheckResult);

            try
            {
                var spotCheckResultEntity = Mapper.Map<SpotCheckResult, SpotCheckResultEntity>(spotCheckResult);

                spotCheckResultEntity.IsNew = spotCheckResultEntity.Id <= 0;

                var processResult = CommonRepository.Save(spotCheckResultEntity);

                spotCheckResult.Id = spotCheckResultEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a spot check result.
        /// </summary>
        /// <param name="spotCheckResult">The spot check result.</param>
        /// <returns></returns>
        public ProcessResult Delete(SpotCheckResult spotCheckResult)
        {
            Check.Argument.IsNotNull(() => spotCheckResult);

            try
            {
                var spotCheckResultEntity = Mapper.Map<SpotCheckResult, SpotCheckResultEntity>(spotCheckResult);

                return CommonRepository.Delete(spotCheckResultEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Workers

        /// <summary>
        /// Loads the spot checks.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <param name="testId">The Test id.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="creationDatetTime">The creation date time.</param>
        /// <param name="updatedDateTime">The updated date time.</param>
        /// <param name="pathEdges">The path edges.</param>
        /// <returns></returns>
        private static BindingList<SpotCheck> LoadSpotChecksWorker( int patientId, int testId, string name, int userId, DateTime? creationDatetTime, DateTime? updatedDateTime, Func<IPathEdgeRootParser<SpotCheckEntity>, IPathEdgeRootParser<SpotCheckEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var result = source.SpotCheck.WithPath(pathEdges).AsQueryable();

                if (patientId > 0)
                    result = result.Where(c => c.PatientId == patientId);

                if (testId > 0)
                    result = result.Where(c => c.TestId == testId);

                var spotChecks = new BindingList<SpotCheck>();

                Mapper.Map(result.ToList(), spotChecks);

                return spotChecks;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spotCheckId"></param>
        /// <param name="itemId"></param>
        /// <param name="question"></param>
        /// <param name="userId"></param>
        /// <param name="reading"></param>
        /// <param name="creationDateTime"></param>
        /// <param name="updatedDateTime"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        private static BindingList<SpotCheckResult> LoadSpotCheckResultsWorker(int spotCheckId, int itemId, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, Func<IPathEdgeRootParser<SpotCheckResultEntity>, IPathEdgeRootParser<SpotCheckResultEntity>> pathEdge)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var result = source.SpotCheckResult.WithPath(pathEdge).AsQueryable();

                if (spotCheckId > 0)
                    result = result.Where(c => c.SpotCheckId == spotCheckId);

                if (itemId > 0)
                    result = result.Where(c => c.ItemId == itemId);

                var spotCheckResults = new BindingList<SpotCheckResult>();

                Mapper.Map(result.ToList(), spotCheckResults);

                return spotCheckResults;
            }

        }

        #endregion
    }
}
