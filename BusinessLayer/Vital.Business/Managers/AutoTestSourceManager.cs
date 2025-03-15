using System;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.AutoTestSource;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class AutoTestSourceManager : BaseManager
    {
        #region Private Variables

        private readonly IAutoTestSourceRepository _autoTestSourceRepository;

        #endregion

        #region Private Related Managers

        private readonly AutoTestDestinationManager _autoTestDestinationManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoTestSourceManager()
        {
            _autoTestSourceRepository = new AutoTestSourceDatabaseRepository();

            _autoTestDestinationManager = new AutoTestDestinationManager();

        }

        #endregion

        #region Public Methods

        #region TestingPoints

        /// <summary>
        /// Gets a testingPoint.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TestingPoint GetTestingPointById(TestingPointsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<TestingPoint> GetTestingPoints(TestingPointsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the testingPoint.
        /// </summary>
        /// <param name="testingPoint">The testingPoint.</param>
        /// <returns></returns>
        public ProcessResult Save(TestingPoint testingPoint)
        {
            Check.Argument.IsNotNull(() => testingPoint);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = testingPoint.Id };

                if (!testingPoint.IsChanged) return processResult;

                testingPoint.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(testingPoint);

                if (processResult.IsSucceed)
                {
                    testingPoint.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the TestingPoints collection.
        /// </summary>
        /// <param name="testingPoints"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<TestingPoint> testingPoints)
        {
            Check.Argument.IsNotNull(() => testingPoints);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var testingPoint in testingPoints)
                {
                    result = testingPoint.IsDeleted ? Delete(testingPoint) : Save(testingPoint);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a testingPoint.
        /// </summary>
        /// <param name="testingPoint">The testingPoint.</param>
        /// <returns></returns>
        public ProcessResult Delete(TestingPoint testingPoint)
        {
            Check.Argument.IsNotNull(() => testingPoint);

            try
            {
                var processResult = _autoTestSourceRepository.Delete(testingPoint);

                if (processResult.IsSucceed)
                {
                    testingPoint.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the TestingPoints collection.
        /// </summary>
        /// <param name="testingPoints"></param>
        public ProcessResult Delete(BindingList<TestingPoint> testingPoints)
        {
            Check.Argument.IsNotNull(() => testingPoints);

            try
            {
                foreach (var testingPoint in testingPoints)
                {
                    var result = Delete(testingPoint);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoItems

        /// <summary>
        /// Gets a autoItem.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoItem GetAutoItemById(AutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoItem> GetAutoItems(AutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoItem.
        /// </summary>
        /// <param name="autoItem">The autoItem.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoItem autoItem)
        {
            Check.Argument.IsNotNull(() => autoItem);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoItem.Id };

                if (!autoItem.IsChanged) return processResult;

                autoItem.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoItem);

                if (processResult.IsSucceed)
                {
                    autoItem.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the autoItem with details
        /// </summary>
        /// <param name="autoItem">The autoItem.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(AutoItem autoItem)
        {
            Check.Argument.IsNotNull(() => autoItem);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoItem.Id };

                if (!autoItem.IsChanged &&
                     autoItem.Products.All(a => !a.IsChanged)) return processResult;

                autoItem.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoItem);

                //Set the autoItem inside the Products
                if (autoItem.Products != null)
                    autoItem.Products.ToList().ForEach(s => s.AutoItem = autoItem);


                var processResultProducts = _autoTestDestinationManager.Save(autoItem.Products);

                if (processResult.IsSucceed &&
                    processResultProducts.IsSucceed)
                {
                    autoItem.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoItems collection.
        /// </summary>
        /// <param name="autoItems"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoItem> autoItems)
        {
            Check.Argument.IsNotNull(() => autoItems);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoItem in autoItems)
                {
                    result = autoItem.IsDeleted ? Delete(autoItem) : Save(autoItem);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoItem.
        /// </summary>
        /// <param name="autoItem">The autoItem.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoItem autoItem)
        {
            Check.Argument.IsNotNull(() => autoItem);

            try
            {

                var autoTestResults = _autoTestDestinationManager.GetAutoTestResults(new AutoTestResultsFilter { AutoItemsId = autoItem.Id });
                var processResultAutoTestResults = _autoTestDestinationManager.Delete(autoTestResults);

                autoItem.Products = _autoTestDestinationManager.GetProducts(new ProductsFilter { AutoItemsId = autoItem.Id });
                var processResultProducts = _autoTestDestinationManager.Delete(autoItem.Products);

                var stageAutoItems = GetStageAutoItems(new StageAutoItemsFilter { AutoItemsId = autoItem.Id });
                var processResultStageAutoItems = Delete(stageAutoItems);

                var processResult = _autoTestSourceRepository.Delete(autoItem);

                if (processResult.IsSucceed &&
                    processResultAutoTestResults.IsSucceed &&
                    processResultProducts.IsSucceed &&
                    processResultStageAutoItems.IsSucceed)
                {
                    autoItem.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoItems collection.
        /// </summary>
        /// <param name="autoItems"></param>
        public ProcessResult Delete(BindingList<AutoItem> autoItems)
        {
            Check.Argument.IsNotNull(() => autoItems);

            try
            {
                foreach (var autoItem in autoItems)
                {
                    var result = Delete(autoItem);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoItemRelations

        /// <summary>
        /// Gets a autoItemRelation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoItemRelation GetAutoItemRelationById(AutoItemRelationsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoItemRelation> GetAutoItemRelations(AutoItemRelationsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoItemRelation.
        /// </summary>
        /// <param name="autoItemRelation">The autoItemRelation.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoItemRelation autoItemRelation)
        {
            Check.Argument.IsNotNull(() => autoItemRelation);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoItemRelation.Id };

                if (!autoItemRelation.IsChanged) return processResult;

                autoItemRelation.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoItemRelation);

                if (processResult.IsSucceed)
                {
                    autoItemRelation.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoItemRelations collection.
        /// </summary>
        /// <param name="autoItemRelations"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoItemRelation> autoItemRelations)
        {
            Check.Argument.IsNotNull(() => autoItemRelations);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoItemRelation in autoItemRelations)
                {
                    result = autoItemRelation.IsDeleted ? Delete(autoItemRelation) : Save(autoItemRelation);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoItemRelation.
        /// </summary>
        /// <param name="autoItemRelation">The autoItemRelation.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoItemRelation autoItemRelation)
        {
            Check.Argument.IsNotNull(() => autoItemRelation);

            try
            {
                var processResult = _autoTestSourceRepository.Delete(autoItemRelation);

                if (processResult.IsSucceed)
                {
                    autoItemRelation.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoItemRelations collection.
        /// </summary>
        /// <param name="autoItemRelations"></param>
        public ProcessResult Delete(BindingList<AutoItemRelation> autoItemRelations)
        {
            Check.Argument.IsNotNull(() => autoItemRelations);

            try
            {
                foreach (var autoItemRelation in autoItemRelations)
                {
                    var result = Delete(autoItemRelation);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoProtocols

        /// <summary>
        /// Gets a autoProtocol.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoProtocol GetAutoProtocolById(AutoProtocolsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoProtocol> GetAutoProtocols(AutoProtocolsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoProtocol.
        /// </summary>
        /// <param name="autoProtocol">The autoProtocol.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoProtocol autoProtocol)
        {
            Check.Argument.IsNotNull(() => autoProtocol);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocol.Id };

                if (!autoProtocol.IsChanged) return processResult;

                autoProtocol.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocol);

                if (processResult.IsSucceed)
                {
                    autoProtocol.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the autoProtocol with details
        /// </summary>
        /// <param name="autoProtocol">The autoProtocol.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(AutoProtocol autoProtocol)
        {
            Check.Argument.IsNotNull(() => autoProtocol);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocol.Id };

                if (!autoProtocol.IsChanged &&
                     autoProtocol.AutoProtocolStages.All(a => !a.IsChanged)) return processResult;

                autoProtocol.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocol);

                //Set the autoProtocol inside the AutoProtocolStages
                if (autoProtocol.AutoProtocolStages != null)
                    autoProtocol.AutoProtocolStages.ToList().ForEach(s => s.AutoProtocol = autoProtocol);

                var processResultAutoProtocolStages = Save(autoProtocol.AutoProtocolStages);

                if (processResult.IsSucceed &&
                    processResultAutoProtocolStages.IsSucceed)
                {
                    autoProtocol.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoProtocols collection.
        /// </summary>
        /// <param name="autoProtocols"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoProtocol> autoProtocols)
        {
            Check.Argument.IsNotNull(() => autoProtocols);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoProtocol in autoProtocols)
                {
                    result = autoProtocol.IsDeleted ? Delete(autoProtocol) : Save(autoProtocol);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoProtocol.
        /// </summary>
        /// <param name="autoProtocol">The autoProtocol.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoProtocol autoProtocol)
        {
            Check.Argument.IsNotNull(() => autoProtocol);

            try
            {
                var autoProtocolRevisions = GetAutoProtocolRevisions(new AutoProtocolRevisionsFilter { AutoProtocolsId = autoProtocol.Id });
                var processResultAutoProtocolRevisions = Delete(autoProtocolRevisions);

                autoProtocol.AutoProtocolStages = GetAutoProtocolStages(new AutoProtocolStagesFilter { AutoProtocolsId = autoProtocol.Id });
                var processResultAutoProtocolStages = Delete(autoProtocol.AutoProtocolStages);

                var processResult = _autoTestSourceRepository.Delete(autoProtocol);

                if (processResult.IsSucceed &&
                    processResultAutoProtocolRevisions.IsSucceed &&
                    processResultAutoProtocolStages.IsSucceed)
                {
                    autoProtocol.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoProtocols collection.
        /// </summary>
        /// <param name="autoProtocols"></param>
        public ProcessResult Delete(BindingList<AutoProtocol> autoProtocols)
        {
            Check.Argument.IsNotNull(() => autoProtocols);

            try
            {
                foreach (var autoProtocol in autoProtocols)
                {
                    var result = Delete(autoProtocol);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoProtocolRevisions

        /// <summary>
        /// Gets a autoProtocolRevision.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoProtocolRevision GetAutoProtocolRevisionById(AutoProtocolRevisionsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoProtocolRevision> GetAutoProtocolRevisions(AutoProtocolRevisionsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoProtocolRevision.
        /// </summary>
        /// <param name="autoProtocolRevision">The autoProtocolRevision.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoProtocolRevision autoProtocolRevision)
        {
            Check.Argument.IsNotNull(() => autoProtocolRevision);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocolRevision.Id };

                if (!autoProtocolRevision.IsChanged) return processResult;

                autoProtocolRevision.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocolRevision);

                if (processResult.IsSucceed)
                {
                    autoProtocolRevision.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the autoProtocolRevision with details
        /// </summary>
        /// <param name="autoProtocolRevision">The autoProtocolRevision.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(AutoProtocolRevision autoProtocolRevision)
        {
            Check.Argument.IsNotNull(() => autoProtocolRevision);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocolRevision.Id };

                if (!autoProtocolRevision.IsChanged &&
                     autoProtocolRevision.AutoProtocolStageRevisions.All(a => !a.IsChanged)) return processResult;

                autoProtocolRevision.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocolRevision);

                //Set the autoProtocolRevision inside the AutoProtocolStageRevisions
                if (autoProtocolRevision.AutoProtocolStageRevisions != null)
                    autoProtocolRevision.AutoProtocolStageRevisions.ToList().ForEach(s => s.AutoProtocolRevision = autoProtocolRevision);

                var processResultAutoProtocolStageRevisions = Save(autoProtocolRevision.AutoProtocolStageRevisions);

                if (processResult.IsSucceed &&
                    processResultAutoProtocolStageRevisions.IsSucceed)
                {
                    autoProtocolRevision.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoProtocolRevisions collection.
        /// </summary>
        /// <param name="autoProtocolRevisions"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoProtocolRevision> autoProtocolRevisions)
        {
            Check.Argument.IsNotNull(() => autoProtocolRevisions);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoProtocolRevision in autoProtocolRevisions)
                {
                    result = autoProtocolRevision.IsDeleted ? Delete(autoProtocolRevision) : Save(autoProtocolRevision);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoProtocolRevision.
        /// </summary>
        /// <param name="autoProtocolRevision">The autoProtocolRevision.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoProtocolRevision autoProtocolRevision)
        {
            Check.Argument.IsNotNull(() => autoProtocolRevision);

            try
            {
                autoProtocolRevision.AutoProtocolStageRevisions = GetAutoProtocolStageRevisions(new AutoProtocolStageRevisionsFilter { AutoProtocolRevisionsId = autoProtocolRevision.Id });
                var processResultAutoProtocolStageRevisions = Delete(autoProtocolRevision.AutoProtocolStageRevisions);

                var processResult = _autoTestSourceRepository.Delete(autoProtocolRevision);

                if (processResult.IsSucceed &&
                    processResultAutoProtocolStageRevisions.IsSucceed)
                {
                    autoProtocolRevision.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoProtocolRevisions collection.
        /// </summary>
        /// <param name="autoProtocolRevisions"></param>
        public ProcessResult Delete(BindingList<AutoProtocolRevision> autoProtocolRevisions)
        {
            Check.Argument.IsNotNull(() => autoProtocolRevisions);

            try
            {
                foreach (var autoProtocolRevision in autoProtocolRevisions)
                {
                    var result = Delete(autoProtocolRevision);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoProtocolStages

        /// <summary>
        /// Gets a autoProtocolStage.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoProtocolStage GetAutoProtocolStageById(AutoProtocolStagesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoProtocolStage> GetAutoProtocolStages(AutoProtocolStagesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoProtocolStage.
        /// </summary>
        /// <param name="autoProtocolStage">The autoProtocolStage.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoProtocolStage autoProtocolStage)
        {
            Check.Argument.IsNotNull(() => autoProtocolStage);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocolStage.Id };

                if (!autoProtocolStage.IsChanged) return processResult;

                autoProtocolStage.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocolStage);

                if (processResult.IsSucceed)
                {
                    autoProtocolStage.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the autoProtocolStage with details
        /// </summary>
        /// <param name="autoProtocolStage">The autoProtocolStage.</param>
        /// <returns></returns>
        public ProcessResult SaveWithDetails(AutoProtocolStage autoProtocolStage)
        {
            Check.Argument.IsNotNull(() => autoProtocolStage);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocolStage.Id };

                if (!autoProtocolStage.IsChanged &&
                     autoProtocolStage.StageAutoItems.All(a => !a.IsChanged)) return processResult;

                autoProtocolStage.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocolStage);

                //Set the autoProtocolStage inside the StageAutoItems
                if (autoProtocolStage.StageAutoItems != null)
                    autoProtocolStage.StageAutoItems.ToList().ForEach(s => s.AutoProtocolStage = autoProtocolStage);

                var processResultStageAutoItems = Save(autoProtocolStage.StageAutoItems);

                if (processResult.IsSucceed &&
                    processResultStageAutoItems.IsSucceed)
                {
                    autoProtocolStage.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoProtocolStages collection.
        /// </summary>
        /// <param name="autoProtocolStages"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoProtocolStage> autoProtocolStages)
        {
            Check.Argument.IsNotNull(() => autoProtocolStages);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoProtocolStage in autoProtocolStages)
                {
                    result = autoProtocolStage.IsDeleted ? Delete(autoProtocolStage) : Save(autoProtocolStage);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoProtocolStage.
        /// </summary>
        /// <param name="autoProtocolStage">The autoProtocolStage.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoProtocolStage autoProtocolStage)
        {
            Check.Argument.IsNotNull(() => autoProtocolStage);

            try
            {
                var autoProtocolStageRevisions = GetAutoProtocolStageRevisions(new AutoProtocolStageRevisionsFilter { AutoProtocolStagesId = autoProtocolStage.Id });
                var processResultAutoProtocolStageRevisions = Delete(autoProtocolStageRevisions);

                autoProtocolStage.StageAutoItems = GetStageAutoItems(new StageAutoItemsFilter { AutoProtocolStagesId = autoProtocolStage.Id });
                var processResultStageAutoItems = Delete(autoProtocolStage.StageAutoItems);

                var processResult = _autoTestSourceRepository.Delete(autoProtocolStage);

                if (processResult.IsSucceed &&
                    processResultAutoProtocolStageRevisions.IsSucceed &&
                    processResultStageAutoItems.IsSucceed)
                {
                    autoProtocolStage.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoProtocolStages collection.
        /// </summary>
        /// <param name="autoProtocolStages"></param>
        public ProcessResult Delete(BindingList<AutoProtocolStage> autoProtocolStages)
        {
            Check.Argument.IsNotNull(() => autoProtocolStages);

            try
            {
                foreach (var autoProtocolStage in autoProtocolStages)
                {
                    var result = Delete(autoProtocolStage);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoProtocolStageRevisions

        /// <summary>
        /// Gets a autoProtocolStageRevision.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoProtocolStageRevision GetAutoProtocolStageRevisionById(AutoProtocolStageRevisionsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoProtocolStageRevision> GetAutoProtocolStageRevisions(AutoProtocolStageRevisionsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoProtocolStageRevision.
        /// </summary>
        /// <param name="autoProtocolStageRevision">The autoProtocolStageRevision.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoProtocolStageRevision autoProtocolStageRevision)
        {
            Check.Argument.IsNotNull(() => autoProtocolStageRevision);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocolStageRevision.Id };

                if (!autoProtocolStageRevision.IsChanged) return processResult;

                autoProtocolStageRevision.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocolStageRevision);

                if (processResult.IsSucceed)
                {
                    autoProtocolStageRevision.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the autoProtocolStageRevision with details
        /// </summary>
        /// <param name="autoProtocolStageRevision">The autoProtocolStageRevision.</param>
        /// <returns></returns>
        [Obsolete("Use Save method instead. No details to be saved, we kept this for future changes.")]
        public ProcessResult SaveWithDetails(AutoProtocolStageRevision autoProtocolStageRevision)
        {
            Check.Argument.IsNotNull(() => autoProtocolStageRevision);

            try
            {
                // No details to be saved, we kept this for future changes.
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoProtocolStageRevision.Id };

                autoProtocolStageRevision.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoProtocolStageRevision);

                if (processResult.IsSucceed)
                {
                    autoProtocolStageRevision.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoProtocolStageRevisions collection.
        /// </summary>
        /// <param name="autoProtocolStageRevisions"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoProtocolStageRevision> autoProtocolStageRevisions)
        {
            Check.Argument.IsNotNull(() => autoProtocolStageRevisions);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoProtocolStageRevision in autoProtocolStageRevisions)
                {
                    result = autoProtocolStageRevision.IsDeleted ? Delete(autoProtocolStageRevision) : Save(autoProtocolStageRevision);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoProtocolStageRevision.
        /// </summary>
        /// <param name="autoProtocolStageRevision">The autoProtocolStageRevision.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoProtocolStageRevision autoProtocolStageRevision)
        {
            Check.Argument.IsNotNull(() => autoProtocolStageRevision);

            try
            {
                var autoTestResults = _autoTestDestinationManager.GetAutoTestResults(new AutoTestResultsFilter { AutoProtocolStageRevisionsId = autoProtocolStageRevision.Id });
                var processResultAutoTestResults = _autoTestDestinationManager.Delete(autoTestResults);

                var processResult = _autoTestSourceRepository.Delete(autoProtocolStageRevision);

                if (processResult.IsSucceed &&
                    processResultAutoTestResults.IsSucceed)
                {
                    autoProtocolStageRevision.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoProtocolStageRevisions collection.
        /// </summary>
        /// <param name="autoProtocolStageRevisions"></param>
        public ProcessResult Delete(BindingList<AutoProtocolStageRevision> autoProtocolStageRevisions)
        {
            Check.Argument.IsNotNull(() => autoProtocolStageRevisions);

            try
            {
                foreach (var autoProtocolStageRevision in autoProtocolStageRevisions)
                {
                    var result = Delete(autoProtocolStageRevision);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region AutoTestStages

        /// <summary>
        /// Gets a autoTestStage.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public AutoTestStage GetAutoTestStageById(AutoTestStagesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<AutoTestStage> GetAutoTestStages(AutoTestStagesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the autoTestStage.
        /// </summary>
        /// <param name="autoTestStage">The autoTestStage.</param>
        /// <returns></returns>
        public ProcessResult Save(AutoTestStage autoTestStage)
        {
            Check.Argument.IsNotNull(() => autoTestStage);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTestStage.Id };

                if (!autoTestStage.IsChanged) return processResult;

                autoTestStage.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoTestStage);

                if (processResult.IsSucceed)
                {
                    autoTestStage.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the autoTestStage with details
        /// </summary>
        /// <param name="autoTestStage">The autoTestStage.</param>
        /// <returns></returns>
        [Obsolete("Use Save method instead. No details to be saved, we kept this for future changes.")]
        public ProcessResult SaveWithDetails(AutoTestStage autoTestStage)
        {
            Check.Argument.IsNotNull(() => autoTestStage);

            try
            {
                // No details to be saved, we kept this for future changes.
                var processResult = new ProcessResult { IsSucceed = true, EntityId = autoTestStage.Id };

                if (!autoTestStage.IsChanged) return processResult;

                autoTestStage.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(autoTestStage);

                if (processResult.IsSucceed)
                {
                    autoTestStage.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the AutoTestStages collection.
        /// </summary>
        /// <param name="autoTestStages"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<AutoTestStage> autoTestStages)
        {
            Check.Argument.IsNotNull(() => autoTestStages);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var autoTestStage in autoTestStages)
                {
                    result = autoTestStage.IsDeleted ? Delete(autoTestStage) : Save(autoTestStage);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a autoTestStage.
        /// </summary>
        /// <param name="autoTestStage">The autoTestStage.</param>
        /// <returns></returns>
        public ProcessResult Delete(AutoTestStage autoTestStage)
        {
            Check.Argument.IsNotNull(() => autoTestStage);

            try
            {
                var autoProtocolStageRevisions = GetAutoProtocolStageRevisions(new AutoProtocolStageRevisionsFilter { AutoTestStagesId = autoTestStage.Id });
                var processResultAutoProtocolStageRevisions = Delete(autoProtocolStageRevisions);

                var autoProtocolStages = GetAutoProtocolStages(new AutoProtocolStagesFilter { AutoTestStagesId = autoTestStage.Id });
                var processResultAutoProtocolStages = Delete(autoProtocolStages);

                var processResult = _autoTestSourceRepository.Delete(autoTestStage);

                if (processResult.IsSucceed &&
                    processResultAutoProtocolStageRevisions.IsSucceed &&
                    processResultAutoProtocolStages.IsSucceed)
                {
                    autoTestStage.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the AutoTestStages collection.
        /// </summary>
        /// <param name="autoTestStages"></param>
        public ProcessResult Delete(BindingList<AutoTestStage> autoTestStages)
        {
            Check.Argument.IsNotNull(() => autoTestStages);

            try
            {
                foreach (var autoTestStage in autoTestStages)
                {
                    var result = Delete(autoTestStage);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region StageAutoItems

        /// <summary>
        /// Gets a stageAutoItem.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public StageAutoItem GetStageAutoItemById(StageAutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a stageAutoItem by AutoItem Key
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public StageAutoItem GetStageAutoItemByAutoItemKey(AutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityByAutoItemKey(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<StageAutoItem> GetStageAutoItems(StageAutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the stageAutoItem.
        /// </summary>
        /// <param name="stageAutoItem">The stageAutoItem.</param>
        /// <returns></returns>
        public ProcessResult Save(StageAutoItem stageAutoItem)
        {
            Check.Argument.IsNotNull(() => stageAutoItem);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = stageAutoItem.Id };

                if (!stageAutoItem.IsChanged) return processResult;

                stageAutoItem.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(stageAutoItem);

                if (processResult.IsSucceed)
                {
                    stageAutoItem.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the StageAutoItems collection.
        /// </summary>
        /// <param name="stageAutoItems"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<StageAutoItem> stageAutoItems)
        {
            Check.Argument.IsNotNull(() => stageAutoItems);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var stageAutoItem in stageAutoItems)
                {
                    result = stageAutoItem.IsDeleted ? Delete(stageAutoItem) : Save(stageAutoItem);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a stageAutoItem.
        /// </summary>
        /// <param name="stageAutoItem">The stageAutoItem.</param>
        /// <returns></returns>
        public ProcessResult Delete(StageAutoItem stageAutoItem)
        {
            Check.Argument.IsNotNull(() => stageAutoItem);

            try
            {
                var processResult = _autoTestSourceRepository.Delete(stageAutoItem);

                if (processResult.IsSucceed)
                {
                    stageAutoItem.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the StageAutoItems collection.
        /// </summary>
        /// <param name="stageAutoItems"></param>
        public ProcessResult Delete(BindingList<StageAutoItem> stageAutoItems)
        {
            Check.Argument.IsNotNull(() => stageAutoItems);

            try
            {
                foreach (var stageAutoItem in stageAutoItems)
                {
                    var result = Delete(stageAutoItem);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region StageAnnouncements

        /// <summary>
        /// Gets a stageAnnouncement.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public StageAnnouncement GetStageAnnouncementById(StageAnnouncementsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntityById(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<StageAnnouncement> GetStageAnnouncements(StageAnnouncementsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _autoTestSourceRepository.LoadEntities(filter);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the stageAnnouncement.
        /// </summary>
        /// <param name="stageAnnouncement">The stageAnnouncement.</param>
        /// <returns></returns>
        public ProcessResult Save(StageAnnouncement stageAnnouncement)
        {
            Check.Argument.IsNotNull(() => stageAnnouncement);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true, EntityId = stageAnnouncement.Id };

                if (!stageAnnouncement.IsChanged) return processResult;

                stageAnnouncement.SetUserAndDates();

                processResult = _autoTestSourceRepository.Save(stageAnnouncement);

                if (processResult.IsSucceed)
                {
                    stageAnnouncement.ObjectState = DomainEntityState.Unchanged;
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
        /// Saves the StageAnnouncements collection.
        /// </summary>
        /// <param name="stageAnnouncements"></param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<StageAnnouncement> stageAnnouncements)
        {
            Check.Argument.IsNotNull(() => stageAnnouncements);

            var result = ProcessResult.Succeed;

            try
            {
                foreach (var stageAnnouncement in stageAnnouncements)
                {
                    result = stageAnnouncement.IsDeleted ? Delete(stageAnnouncement) : Save(stageAnnouncement);

                    if (!result.IsSucceed)
                        return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a stageAnnouncement.
        /// </summary>
        /// <param name="stageAnnouncement">The stageAnnouncement.</param>
        /// <returns></returns>
        public ProcessResult Delete(StageAnnouncement stageAnnouncement)
        {
            Check.Argument.IsNotNull(() => stageAnnouncement);

            try
            {
                var processResult = _autoTestSourceRepository.Delete(stageAnnouncement);

                if (processResult.IsSucceed)
                {
                    stageAnnouncement.ObjectState = DomainEntityState.Deleted;
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
        /// Deletes  the StageAnnouncements collection.
        /// </summary>
        /// <param name="stageAnnouncements"></param>
        public ProcessResult Delete(BindingList<StageAnnouncement> stageAnnouncements)
        {
            Check.Argument.IsNotNull(() => stageAnnouncements);

            try
            {
                foreach (var stageAnnouncement in stageAnnouncements)
                {
                    var result = Delete(stageAnnouncement);
                    if (!result.IsSucceed) return result;
                }

                return ProcessResult.Succeed;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #endregion
    }
}
