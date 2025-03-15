using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.AutoTestSource
{
    public class AutoTestSourceDatabaseRepository : BaseRepository, IAutoTestSourceRepository
    {
        #region Static Members

        private static DataAccessAdapter _dataAccessAdapter;
        private static LinqMetaData _linqMetaData;

        #endregion

        #region Path Edges

        #region TestingPoints

        private readonly Func<IPathEdgeRootParser<TestingPointEntity>, IPathEdgeRootParser<TestingPointEntity>> _pathEdgesTestingPoint = p => p.Prefetch(cr => cr.User);

        #endregion

        #region AutoItems

        private readonly Func<IPathEdgeRootParser<AutoItemEntity>, IPathEdgeRootParser<AutoItemEntity>> _pathEdgesAutoItem =
            p => p
                .Prefetch(te => te.TestingPoint)
                .Prefetch(im => im.Image)
                .Prefetch(ty => ty.Type)
                .Prefetch(ge => ge.Gender)
                .Prefetch(st => st.StructureType)
                .Prefetch(st => st.Status)
                .Prefetch(ch => ch.ChildsOrderType)
                .Prefetch(ch => ch.ChildsScanningType)
                .Prefetch(sc => sc.ScanningMethod)
                .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                    .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                        .Prefetch(i => i.ProductSizes)
                        .Prefetch(i => i.DosageOptions)))
                .Prefetch(cr => cr.User);

        #endregion

        #region AutoItemRelations

        private readonly Func<IPathEdgeRootParser<AutoItemRelationEntity>, IPathEdgeRootParser<AutoItemRelationEntity>> _pathEdgesAutoItemRelation =
            p => p
                .Prefetch(au => au.Parent)
                .Prefetch(au => au.Child)
                .Prefetch(cr => cr.User);

        #endregion

        #region AutoProtocols

        private readonly Func<IPathEdgeRootParser<AutoProtocolEntity>, IPathEdgeRootParser<AutoProtocolEntity>>
            _pathEdgesAutoProtocol =
                p => p
                    .Prefetch(au => au.AutoProtocolStages)
                    .Prefetch(cr => cr.User);

        #endregion

        #region AutoProtocolRevisions

        private readonly
            Func<IPathEdgeRootParser<AutoProtocolRevisionEntity>, IPathEdgeRootParser<AutoProtocolRevisionEntity>>
            _pathEdgesAutoProtocolRevision =
                p => p
                    .Prefetch(au => au.AutoProtocol)
                    .Prefetch<AutoProtocolStageRevisionEntity>(au => au.AutoProtocolStageRevisions).SubPath(au => au
                        .Prefetch(ts => ts.AutoTestStage)
                        .Prefetch<AutoProtocolStageEntity>(pr => pr.AutoProtocolStage).SubPath(pr => pr
                            .Prefetch(ts => ts.StageItemsOrderType)
                            .Prefetch<StageAutoItemEntity>(ts => ts.StageAutoItems).SubPath(ts => ts
                                    .Prefetch(i => i.ChildsOrderType)
                                    .Prefetch(i => i.ChildsScanningType)
                                    .Prefetch(i => i.ScanningMethod)
                                    .Prefetch(i => i.TestingPoint)
                                    .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                                        .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                            .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                .Prefetch(i => i.ProductSizes)))
                                    .Prefetch(i => i.ChildsOrderType)
                                    .Prefetch(i => i.ChildsScanningType)
                                    .Prefetch(i => i.ScanningMethod)
                                    .Prefetch(i => i.Status)
                                    .Prefetch(i => i.StructureType)
                                    .Prefetch(i => i.TestingPoint)
                                    .Prefetch(i => i.Type)
                                    .Prefetch(i => i.Gender)
                                    .Prefetch(i => i.Parents)
                                    .Prefetch(i => i.Image))
                                .Prefetch<StageAutoItemEntity>(ai => ai.StageAutoItems).SubPath(si => si
                                    .Prefetch(i => i.ChildsOrderType)
                                    .Prefetch(i => i.ChildsScanningType)
                                    .Prefetch(i => i.ScanningMethod)
                                    .Prefetch(i => i.TestingPoint)
                                    .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                                        .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                            .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                .Prefetch(i => i.ProductSizes)))
                                        .Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                        .Prefetch(i => i.Status)
                                        .Prefetch(i => i.StructureType)
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch(i => i.Type)
                                        .Prefetch(i => i.Gender)
                                        .Prefetch(i => i.Parents)
                                        .Prefetch(i => i.Image))
                                    //IMPORTANT: The level below is the 3rd level and this wasn't actually used in Phase 1 and in Phase 2 it should be loaded on demand
                                    //IMPORTANT: not automatically but we kept the code here to help solve any possible issues
                                    .Prefetch<StageAutoItemEntity>(sai => sai.StageAutoItems).SubPath(ssi => ssi
                                        .Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                                            .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                                .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                                    .Prefetch(i => i.ProductSizes)))
                                            .Prefetch(i => i.ChildsOrderType)
                                            .Prefetch(i => i.ChildsScanningType)
                                            .Prefetch(i => i.ScanningMethod)
                                            .Prefetch(i => i.Status)
                                            .Prefetch(i => i.StructureType)
                                            .Prefetch(i => i.TestingPoint)
                                            .Prefetch(i => i.Type)
                                            .Prefetch(i => i.Gender)
                                            .Prefetch(i => i.Parents)
                                            .Prefetch(i => i.Image)))
                                            ))))
                    .Prefetch(cr => cr.User);

        /*
         .Prefetch<StageAutoItemEntity>(ai => ai.StageAutoItems)
                                .SubPath(si => si.Prefetch<AutoItemEntity>(ai => ai.AutoItem)
                                    .SubPath(ai => ai.Prefetch(i => i.ChildsOrderType)
                                        .Prefetch(i => i.ChildsScanningType)
                                        .Prefetch(i => i.ScanningMethod)
                                        .Prefetch(i => i.Status)
                                        .Prefetch(i => i.StructureType)
                                        .Prefetch(i => i.TestingPoint)
                                        .Prefetch(i => i.Type)
                                        .Prefetch(i => i.Parents)
                                        .Prefetch(i => i.Image)))
         */

        #endregion

        #region AutoProtocolStages

        private readonly Func<IPathEdgeRootParser<AutoProtocolStageEntity>, IPathEdgeRootParser<AutoProtocolStageEntity>> _pathEdgesAutoProtocolStage =
            p => p
                .Prefetch(au => au.AutoProtocol)
                .Prefetch(au => au.AutoTestStage)
                .Prefetch(st => st.StageItemsOrderType)
                .Prefetch(au => au.AutoProtocolStageRevisions)
                .Prefetch(st => st.StageAutoItems)
                .Prefetch(cr => cr.User);

        #endregion

        #region AutoProtocolStageRevisions

        private readonly Func<IPathEdgeRootParser<AutoProtocolStageRevisionEntity>, IPathEdgeRootParser<AutoProtocolStageRevisionEntity>> _pathEdgesAutoProtocolStageRevision =
            p => p
                .Prefetch(au => au.AutoProtocolRevision)
                .Prefetch(au => au.AutoProtocolStage)
                .Prefetch(au => au.AutoTestStage)
                .Prefetch(cr => cr.User);

        #endregion

        #region AutoTestStages

        private readonly Func<IPathEdgeRootParser<AutoTestStageEntity>, IPathEdgeRootParser<AutoTestStageEntity>> _pathEdgesAutoTestStage =
            p => p
                .Prefetch(st => st.StageItemsOrderType)
                .Prefetch(cr => cr.User);

        #endregion

        #region StageAutoItems

        private readonly Func<IPathEdgeRootParser<StageAutoItemEntity>, IPathEdgeRootParser<StageAutoItemEntity>> _pathEdgesStageAutoItem =
            p => p
                .Prefetch(i => i.TestingPoint)
                .Prefetch(i => i.ChildsOrderType)
                .Prefetch(i => i.ChildsScanningType)
                .Prefetch(i => i.ScanningMethod)
                .Prefetch(au => au.AutoProtocolStage)
                .Prefetch(st => st.Parent)
                .Prefetch(cr => cr.User)
                .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                    .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                        .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                            .Prefetch(i => i.ProductSizes)))
                    .Prefetch(i => i.Status)
                    .Prefetch(i => i.StructureType)
                    .Prefetch(i => i.TestingPoint)
                    .Prefetch(i => i.Type)
                    .Prefetch(i => i.Gender)
                    .Prefetch(i => i.Parents)
                    .Prefetch(i => i.Image));

        private readonly Func<IPathEdgeRootParser<StageAutoItemEntity>, IPathEdgeRootParser<StageAutoItemEntity>> _pathEdgesStageAutoItemWithChildes =
            p => p
                .Prefetch(i => i.TestingPoint)
                .Prefetch(i => i.ChildsOrderType)
                .Prefetch(i => i.ChildsScanningType)
                .Prefetch(i => i.ScanningMethod)
                .Prefetch(au => au.AutoProtocolStage)
                .Prefetch(st => st.Parent)
                .Prefetch<StageAutoItemEntity>(st => st.StageAutoItems).SubPath(ssi => ssi
                        .Prefetch(i => i.ChildsOrderType)
                        .Prefetch(i => i.ChildsScanningType)
                        .Prefetch(i => i.ScanningMethod)
                        .Prefetch(i => i.TestingPoint)
                        .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                            .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                                .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                                    .Prefetch(i => i.ProductSizes)))
                            .Prefetch(i => i.ChildsOrderType)
                            .Prefetch(i => i.ChildsScanningType)
                            .Prefetch(i => i.ScanningMethod)
                            .Prefetch(i => i.Status)
                            .Prefetch(i => i.StructureType)
                            .Prefetch(i => i.TestingPoint)
                            .Prefetch(i => i.Type)
                            .Prefetch(i => i.Gender)
                            .Prefetch(i => i.Parents)
                            .Prefetch(i => i.Image)))
                .Prefetch(cr => cr.User)
                .Prefetch<AutoItemEntity>(ai => ai.AutoItem).SubPath(ai => ai
                    .Prefetch<ProductEntity>(i => i.Products).SubPath(pro => pro
                        .Prefetch<ProductFormEntity>(i => i.ProductForms).SubPath(pfo => pfo
                            .Prefetch(i => i.ProductSizes)))
                    .Prefetch(i => i.Status)
                    .Prefetch(i => i.StructureType)
                    .Prefetch(i => i.TestingPoint)
                    .Prefetch(i => i.Type)
                    .Prefetch(i => i.Gender)
                    .Prefetch(i => i.Parents)
                    .Prefetch(i => i.Image));

        #endregion

        #region StageAnnouncements

        private readonly Func<IPathEdgeRootParser<StageAnnouncementEntity>, IPathEdgeRootParser<StageAnnouncementEntity>> _pathEdgesStageAnnouncement =
            p => p
                .Prefetch(cr => cr.User);

        #endregion

        #endregion

        #region Public Methods

        #region TestingPoints

        /// <summary>
        /// Loads TestingPoint by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The TestingPoint</returns>
        public TestingPoint LoadEntityById(TestingPointsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.TestingPoint.WithPath(_pathEdgesTestingPoint).FirstOrDefault(c => c.Id == filter.TestingPointId);
                    return entity == null ? null : Mapper.Map(entity, new TestingPoint());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<TestingPoint> LoadEntities(TestingPointsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a testingPoint.
        /// </summary>
        /// <param name="testingPointToSave">The testingPoint.</param>
        /// <returns>The testingPoint.</returns>
        public ProcessResult Save(TestingPoint testingPointToSave)
        {
            Check.Argument.IsNotNull(testingPointToSave, "testingPoint to save");

            try
            {
                var entity = Mapper.Map<TestingPoint, TestingPointEntity>(testingPointToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                testingPointToSave.Id = entity.Id;
                testingPointToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a testingPoint.
        /// </summary>
        /// <param name="testingPointToDelete">The testingPoint.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestingPoint testingPointToDelete)
        {
            Check.Argument.IsNotNull(testingPointToDelete, "testingPoint to delete");

            try
            {
                var entity = Mapper.Map<TestingPoint, TestingPointEntity>(testingPointToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoItems

        /// <summary>
        /// Loads AutoItem by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoItem</returns>
        public AutoItem LoadEntityById(AutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoItem.WithPath(_pathEdgesAutoItem).FirstOrDefault(c => c.Id == filter.AutoItemId);
                    return entity == null ? null : Mapper.Map(entity, new AutoItem());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoItem> LoadEntities(AutoItemsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoItem.
        /// </summary>
        /// <param name="autoItemToSave">The autoItem.</param>
        /// <returns>The autoItem.</returns>
        public ProcessResult Save(AutoItem autoItemToSave)
        {
            Check.Argument.IsNotNull(autoItemToSave, "autoItem to save");

            try
            {
                var entity = Mapper.Map<AutoItem, AutoItemEntity>(autoItemToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoItemToSave.Id = entity.Id;
                autoItemToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoItem.
        /// </summary>
        /// <param name="autoItemToDelete">The autoItem.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoItem autoItemToDelete)
        {
            Check.Argument.IsNotNull(autoItemToDelete, "autoItem to delete");

            try
            {
                var entity = Mapper.Map<AutoItem, AutoItemEntity>(autoItemToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoItemRelations

        /// <summary>
        /// Loads AutoItemRelation by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoItemRelation</returns>
        public AutoItemRelation LoadEntityById(AutoItemRelationsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoItemRelation.WithPath(_pathEdgesAutoItemRelation).FirstOrDefault(c => c.Id == filter.AutoItemRelationId);
                    return entity == null ? null : Mapper.Map(entity, new AutoItemRelation());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoItemRelation> LoadEntities(AutoItemRelationsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoItemRelation.
        /// </summary>
        /// <param name="autoItemRelationToSave">The autoItemRelation.</param>
        /// <returns>The autoItemRelation.</returns>
        public ProcessResult Save(AutoItemRelation autoItemRelationToSave)
        {
            Check.Argument.IsNotNull(autoItemRelationToSave, "autoItemRelation to save");

            try
            {
                var entity = Mapper.Map<AutoItemRelation, AutoItemRelationEntity>(autoItemRelationToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoItemRelationToSave.Id = entity.Id;
                autoItemRelationToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoItemRelation.
        /// </summary>
        /// <param name="autoItemRelationToDelete">The autoItemRelation.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoItemRelation autoItemRelationToDelete)
        {
            Check.Argument.IsNotNull(autoItemRelationToDelete, "autoItemRelation to delete");

            try
            {
                var entity = Mapper.Map<AutoItemRelation, AutoItemRelationEntity>(autoItemRelationToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoProtocols

        /// <summary>
        /// Loads AutoProtocol by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocol</returns>
        public AutoProtocol LoadEntityById(AutoProtocolsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoProtocol.WithPath(_pathEdgesAutoProtocol).FirstOrDefault(c => c.Id == filter.AutoProtocolId);
                    return entity == null ? null : Mapper.Map(entity, new AutoProtocol());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoProtocol> LoadEntities(AutoProtocolsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoProtocol.
        /// </summary>
        /// <param name="autoProtocolToSave">The autoProtocol.</param>
        /// <returns>The autoProtocol.</returns>
        public ProcessResult Save(AutoProtocol autoProtocolToSave)
        {
            Check.Argument.IsNotNull(autoProtocolToSave, "autoProtocol to save");

            try
            {
                var entity = Mapper.Map<AutoProtocol, AutoProtocolEntity>(autoProtocolToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoProtocolToSave.Id = entity.Id;
                autoProtocolToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoProtocol.
        /// </summary>
        /// <param name="autoProtocolToDelete">The autoProtocol.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoProtocol autoProtocolToDelete)
        {
            Check.Argument.IsNotNull(autoProtocolToDelete, "autoProtocol to delete");

            try
            {
                var entity = Mapper.Map<AutoProtocol, AutoProtocolEntity>(autoProtocolToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoProtocolRevisions

        /// <summary>
        /// Loads AutoProtocolRevision by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocolRevision</returns>
        public AutoProtocolRevision LoadEntityById(AutoProtocolRevisionsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoProtocolRevision.WithPath(_pathEdgesAutoProtocolRevision).FirstOrDefault(c => c.Id == filter.AutoProtocolRevisionId);
                    return entity == null ? null : Mapper.Map(entity, new AutoProtocolRevision());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoProtocolRevision> LoadEntities(AutoProtocolRevisionsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoProtocolRevision.
        /// </summary>
        /// <param name="autoProtocolRevisionToSave">The autoProtocolRevision.</param>
        /// <returns>The autoProtocolRevision.</returns>
        public ProcessResult Save(AutoProtocolRevision autoProtocolRevisionToSave)
        {
            Check.Argument.IsNotNull(autoProtocolRevisionToSave, "autoProtocolRevision to save");

            try
            {
                var entity = Mapper.Map<AutoProtocolRevision, AutoProtocolRevisionEntity>(autoProtocolRevisionToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoProtocolRevisionToSave.Id = entity.Id;
                autoProtocolRevisionToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoProtocolRevision.
        /// </summary>
        /// <param name="autoProtocolRevisionToDelete">The autoProtocolRevision.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoProtocolRevision autoProtocolRevisionToDelete)
        {
            Check.Argument.IsNotNull(autoProtocolRevisionToDelete, "autoProtocolRevision to delete");

            try
            {
                var entity = Mapper.Map<AutoProtocolRevision, AutoProtocolRevisionEntity>(autoProtocolRevisionToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoProtocolStages

        /// <summary>
        /// Loads AutoProtocolStage by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocolStage</returns>
        public AutoProtocolStage LoadEntityById(AutoProtocolStagesFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoProtocolStage.WithPath(_pathEdgesAutoProtocolStage).FirstOrDefault(c => c.Id == filter.AutoProtocolStageId);
                    return entity == null ? null : Mapper.Map(entity, new AutoProtocolStage());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoProtocolStage> LoadEntities(AutoProtocolStagesFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoProtocolStage.
        /// </summary>
        /// <param name="autoProtocolStageToSave">The autoProtocolStage.</param>
        /// <returns>The autoProtocolStage.</returns>
        public ProcessResult Save(AutoProtocolStage autoProtocolStageToSave)
        {
            Check.Argument.IsNotNull(autoProtocolStageToSave, "autoProtocolStage to save");

            try
            {
                var entity = Mapper.Map<AutoProtocolStage, AutoProtocolStageEntity>(autoProtocolStageToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoProtocolStageToSave.Id = entity.Id;
                autoProtocolStageToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoProtocolStage.
        /// </summary>
        /// <param name="autoProtocolStageToDelete">The autoProtocolStage.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoProtocolStage autoProtocolStageToDelete)
        {
            Check.Argument.IsNotNull(autoProtocolStageToDelete, "autoProtocolStage to delete");

            try
            {
                var entity = Mapper.Map<AutoProtocolStage, AutoProtocolStageEntity>(autoProtocolStageToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoProtocolStageRevisions

        /// <summary>
        /// Loads AutoProtocolStageRevision by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocolStageRevision</returns>
        public AutoProtocolStageRevision LoadEntityById(AutoProtocolStageRevisionsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoProtocolStageRevision.WithPath(_pathEdgesAutoProtocolStageRevision).FirstOrDefault(c => c.Id == filter.AutoProtocolStageRevisionId);
                    return entity == null ? null : Mapper.Map(entity, new AutoProtocolStageRevision());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoProtocolStageRevision> LoadEntities(AutoProtocolStageRevisionsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoProtocolStageRevision.
        /// </summary>
        /// <param name="autoProtocolStageRevisionToSave">The autoProtocolStageRevision.</param>
        /// <returns>The autoProtocolStageRevision.</returns>
        public ProcessResult Save(AutoProtocolStageRevision autoProtocolStageRevisionToSave)
        {
            Check.Argument.IsNotNull(autoProtocolStageRevisionToSave, "autoProtocolStageRevision to save");

            try
            {
                var entity = Mapper.Map<AutoProtocolStageRevision, AutoProtocolStageRevisionEntity>(autoProtocolStageRevisionToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoProtocolStageRevisionToSave.Id = entity.Id;
                autoProtocolStageRevisionToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoProtocolStageRevision.
        /// </summary>
        /// <param name="autoProtocolStageRevisionToDelete">The autoProtocolStageRevision.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoProtocolStageRevision autoProtocolStageRevisionToDelete)
        {
            Check.Argument.IsNotNull(autoProtocolStageRevisionToDelete, "autoProtocolStageRevision to delete");

            try
            {
                var entity = Mapper.Map<AutoProtocolStageRevision, AutoProtocolStageRevisionEntity>(autoProtocolStageRevisionToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region AutoTestStages

        /// <summary>
        /// Loads AutoTestStage by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTestStage</returns>
        public AutoTestStage LoadEntityById(AutoTestStagesFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.AutoTestStage.WithPath(_pathEdgesAutoTestStage).FirstOrDefault(c => c.Id == filter.AutoTestStageId);
                    return entity == null ? null : Mapper.Map(entity, new AutoTestStage());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<AutoTestStage> LoadEntities(AutoTestStagesFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a autoTestStage.
        /// </summary>
        /// <param name="autoTestStageToSave">The autoTestStage.</param>
        /// <returns>The autoTestStage.</returns>
        public ProcessResult Save(AutoTestStage autoTestStageToSave)
        {
            Check.Argument.IsNotNull(autoTestStageToSave, "autoTestStage to save");

            try
            {
                var entity = Mapper.Map<AutoTestStage, AutoTestStageEntity>(autoTestStageToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                autoTestStageToSave.Id = entity.Id;
                autoTestStageToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a autoTestStage.
        /// </summary>
        /// <param name="autoTestStageToDelete">The autoTestStage.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(AutoTestStage autoTestStageToDelete)
        {
            Check.Argument.IsNotNull(autoTestStageToDelete, "autoTestStage to delete");

            try
            {
                var entity = Mapper.Map<AutoTestStage, AutoTestStageEntity>(autoTestStageToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region StageAutoItems

        /// <summary>
        /// Gets StageAutoItem pathedge based on loading type
        /// </summary>
        /// <param name="loadingType"></param>
        /// <returns></returns>
        public Func<IPathEdgeRootParser<StageAutoItemEntity>, IPathEdgeRootParser<StageAutoItemEntity>> GetStageAutoItemPathEdge(LoadingTypeEnum loadingType)
        {
            switch (loadingType)
            {
                case LoadingTypeEnum.All:
                    return _pathEdgesStageAutoItemWithChildes;
                default:
                    return _pathEdgesStageAutoItem;
            }
        }

        /// <summary>
        /// Loads StageAutoItem by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The StageAutoItem</returns>
        public StageAutoItem LoadEntityById(StageAutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.StageAutoItem.WithPath(GetStageAutoItemPathEdge(filter.LoadingType)).FirstOrDefault(c => c.Id == filter.StageAutoItemId);
                    return entity == null ? null : Mapper.Map(entity, new StageAutoItem());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads StageAutoItem by AutoItem Key.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The StageAutoItem</returns>
        public StageAutoItem LoadEntityByAutoItemKey(AutoItemsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.StageAutoItem.WithPath(GetStageAutoItemPathEdge(filter.LoadingType)).FirstOrDefault(c => c.AutoItem.Key == filter.Key);
                    return entity == null ? null : Mapper.Map(entity, new StageAutoItem());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<StageAutoItem> LoadEntities(StageAutoItemsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a stageAutoItem.
        /// </summary>
        /// <param name="stageAutoItemToSave">The stageAutoItem.</param>
        /// <returns>The stageAutoItem.</returns>
        public ProcessResult Save(StageAutoItem stageAutoItemToSave)
        {
            Check.Argument.IsNotNull(stageAutoItemToSave, "stageAutoItem to save");

            try
            {
                var entity = Mapper.Map<StageAutoItem, StageAutoItemEntity>(stageAutoItemToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                stageAutoItemToSave.Id = entity.Id;
                stageAutoItemToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a stageAutoItem.
        /// </summary>
        /// <param name="stageAutoItemToDelete">The stageAutoItem.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(StageAutoItem stageAutoItemToDelete)
        {
            Check.Argument.IsNotNull(stageAutoItemToDelete, "stageAutoItem to delete");

            try
            {
                var entity = Mapper.Map<StageAutoItem, StageAutoItemEntity>(stageAutoItemToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Logic to get the count of childes for a StageAutoItem from DB without loading the child items
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static int GetStageAutoItemsChildesCount(int parentId)
        {
            try
            {
                //Initialize the adapter and the data only the first time to avoid creating many instances
                if (_dataAccessAdapter == null)
                {
                    _dataAccessAdapter = new DataAccessAdapter();
                    _linqMetaData = new LinqMetaData(_dataAccessAdapter);
                }

                //Get the number of child items
                return _linqMetaData.StageAutoItem.Count(item => item.StageAutoItemParentId == parentId);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region StageAnnouncements

        /// <summary>
        /// Loads StageAnnouncement by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The StageAnnouncement</returns>
        public StageAnnouncement LoadEntityById(StageAnnouncementsFilter filter)
        {
            Check.Argument.IsNotNull(filter, "filter");

            try
            {
                using (var adapter = DataAccessAdapter)
                {
                    var data = new LinqMetaData(adapter);
                    var entity = data.StageAnnouncement.WithPath(_pathEdgesStageAnnouncement).FirstOrDefault(c => c.Id == filter.StageAnnouncementId);
                    return entity == null ? null : Mapper.Map(entity, new StageAnnouncement());
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        public BindingList<StageAnnouncement> LoadEntities(StageAnnouncementsFilter filter)
        {
            try
            {
                return LoadEntitiesWorker(filter);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a stageAnnouncement.
        /// </summary>
        /// <param name="stageAnnouncementToSave">The stageAnnouncement.</param>
        /// <returns>The stageAnnouncement.</returns>
        public ProcessResult Save(StageAnnouncement stageAnnouncementToSave)
        {
            Check.Argument.IsNotNull(stageAnnouncementToSave, "stageAnnouncement to save");

            try
            {
                var entity = Mapper.Map<StageAnnouncement, StageAnnouncementEntity>(stageAnnouncementToSave);

                entity.IsNew = entity.Id <= 0;

                var processResult = SaveEntity(entity);

                stageAnnouncementToSave.Id = entity.Id;
                stageAnnouncementToSave.ResetStatus();

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a stageAnnouncement.
        /// </summary>
        /// <param name="stageAnnouncementToDelete">The stageAnnouncement.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(StageAnnouncement stageAnnouncementToDelete)
        {
            Check.Argument.IsNotNull(stageAnnouncementToDelete, "stageAnnouncement to delete");

            try
            {
                var entity = Mapper.Map<StageAnnouncement, StageAnnouncementEntity>(stageAnnouncementToDelete);

                var processResult = DeleteEntity(entity);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #endregion

        #region Private Methods

        #region TestingPoints

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<TestingPoint> LoadEntitiesWorker(TestingPointsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.TestingPoint : data.TestingPoint.WithPath(_pathEdgesTestingPoint);

                src = src.ApplyFilter(filter.Key, x => x.Key, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.FullName, x => x.FullName, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.HWIdentifier, x => x.Hwidentifier, FilterType.StringEqual);
                src = src.ApplyFilter(filter.Description, x => x.Description, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Key, 
                                    x => x.Name, 
                                    x => x.FullName, 
                                    x => x.HWIdentifier, 
                                    x => x.Description);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoItems

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoItem> LoadEntitiesWorker(AutoItemsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoItem : data.AutoItem.WithPath(_pathEdgesAutoItem);

                src = src.ApplyFilter(filter.TestingPointsId, x => x.TestingPointsId, FilterType.Equal);
                src = src.ApplyFilter(filter.ImageId, x => x.ImageId, FilterType.Equal);
                src = src.ApplyFilter(filter.TypeLookupId, x => x.TypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.GenderLookupId, x => x.GenderLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.StructureTypeLookupId, x => x.StructureTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.StatusLookupId, x => x.StatusLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.ChildsOrderTypeLookupId, x => x.ChildsOrderTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.ChildsScanningTypeLookupId, x => x.ChildsScanningTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.ScanningMethodLookupId, x => x.ScanningMethodLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.ScansNumber, x => x.ScansNumber, FilterType.Equal);
                src = src.ApplyFilter(filter.MatchesNumber, x => x.MatchesNumber, FilterType.Equal);
                src = src.ApplyFilter(filter.Key, x => x.Key, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.FullName, x => x.FullName, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Description, x => x.Description, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Frequency, x => x.Frequency, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.ModelIdentifier, x => x.ModelIdentifier, FilterType.StringEqual);
                src = src.ApplyFilter(filter.UserNotes, x => x.UserNotes, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.DirectAccessChecks, x => x.DirectAccessChecks, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.IsUserItem, x => x.IsUserItem, FilterType.Equal);
                src = src.ApplyFilter(filter.IsSearchable, x => x.IsSearchable, FilterType.Equal);
                src = src.ApplyFilter(filter.InsertOnNo, x => x.InsertOnNo, FilterType.Equal);
                src = src.ApplyFilter(filter.FinishAllScanRounds, x => x.FinishAllScanRounds, FilterType.Equal);
                src = src.ApplyFilter(filter.AddResultOnMatch, x => x.AddResultOnMatch, FilterType.Equal);
                src = src.ApplyFilter(filter.ExcludeOnMatch, x => x.ExcludeOnMatch, FilterType.Equal);
                src = src.ApplyFilter(filter.AddAllChildesOnMatch, x => x.AddAllChildesOnMatch, FilterType.Equal);
                src = src.ApplyFilter(filter.IsImprintable, x => x.IsImprintable, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Key, 
                                    x => x.Name, 
                                    x => x.FullName, 
                                    x => x.Description, 
                                    x => x.Frequency, 
                                    x => x.ModelIdentifier
                                    x => x.UserNotes, 
                                    x => x.GoToAutoItem);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoItemRelations

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoItemRelation> LoadEntitiesWorker(AutoItemRelationsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoItemRelation : data.AutoItemRelation.WithPath(_pathEdgesAutoItemRelation);

                src = src.ApplyFilter(filter.AutoItemsParentId, x => x.AutoItemParentId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoItemsChildId, x => x.AutoItemChildId, FilterType.Equal);
                src = src.ApplyFilter(filter.Order, x => x.Order, FilterType.Equal);
                src = src.ApplyFilter(filter.IsDeleted, x => x.IsDeleted, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoProtocols

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoProtocol> LoadEntitiesWorker(AutoProtocolsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoProtocol : data.AutoProtocol.WithPath(_pathEdgesAutoProtocol);

                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Key, x => x.Key, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.IsSystemProtocol, x => x.IsSystemProtocol, FilterType.Equal);
                src = src.ApplyFilter(filter.IsDefaultProtocol, x => x.IsDefaultProtocol, FilterType.Equal);
                src = src.ApplyFilter(filter.IsDeleted, x => x.IsDeleted, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Name, 
                                    x => x.Key);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoProtocolRevisions

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoProtocolRevision> LoadEntitiesWorker(AutoProtocolRevisionsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoProtocolRevision : data.AutoProtocolRevision.WithPath(_pathEdgesAutoProtocolRevision);

                src = src.ApplyFilter(filter.AutoProtocolsId, x => x.AutoProtocolsId, FilterType.Equal);
                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Key, x => x.Key, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.IsSystemProtocol, x => x.IsSystemProtocol, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Name, 
                                    x => x.Key);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoProtocolStages

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoProtocolStage> LoadEntitiesWorker(AutoProtocolStagesFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoProtocolStage : data.AutoProtocolStage.WithPath(_pathEdgesAutoProtocolStage);

                src = src.ApplyFilter(filter.AutoProtocolsId, x => x.AutoProtocolsId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoTestStagesId, x => x.AutoTestStagesId, FilterType.Equal);
                src = src.ApplyFilter(filter.StageItemsOrderTypeLookupId, x => x.StageItemsOrderTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.Order, x => x.Order, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoProtocolStageRevisions

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoProtocolStageRevision> LoadEntitiesWorker(AutoProtocolStageRevisionsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoProtocolStageRevision : data.AutoProtocolStageRevision.WithPath(_pathEdgesAutoProtocolStageRevision);

                src = src.ApplyFilter(filter.AutoProtocolRevisionsId, x => x.AutoProtocolRevisionsId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoProtocolStagesId, x => x.AutoProtocolStagesId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoTestStagesId, x => x.AutoTestStagesId, FilterType.Equal);
                src = src.ApplyFilter(filter.Order, x => x.Order, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region AutoTestStages

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<AutoTestStage> LoadEntitiesWorker(AutoTestStagesFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.AutoTestStage : data.AutoTestStage.WithPath(_pathEdgesAutoTestStage);

                src = src.ApplyFilter(filter.StageItemsOrderTypeLookupId, x => x.StageItemsOrderTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.Name, x => x.Name, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Key, x => x.Key, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.StageTabKey, x => x.StageTabKey, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Description, x => x.Description, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Dependencies, x => x.Dependencies, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.IsMultiLevel, x => x.IsMultiLevel, FilterType.Equal);
                src = src.ApplyFilter(filter.IsDestinationOnly, x => x.IsDestinationOnly, FilterType.Equal);
                src = src.ApplyFilter(filter.ScanTypeEnabled, x => x.ScanTypeEnabled, FilterType.Equal);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Name, 
                                    x => x.Key, 
                                    x => x.StageTabKey, 
                                    x => x.Description, 
                                    x => x.Dependencies);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region StageAutoItems

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<StageAutoItem> LoadEntitiesWorker(StageAutoItemsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.StageAutoItem : data.StageAutoItem.WithPath(GetStageAutoItemPathEdge(filter.LoadingType));

                src = src.ApplyFilter(filter.AutoProtocolStagesId, x => x.AutoProtocolStagesId, FilterType.Equal);
                src = src.ApplyFilter(filter.StageAutoItemsParentId, x => x.StageAutoItemParentId, FilterType.Equal);
                src = src.ApplyFilter(filter.AutoItemsId, x => x.AutoItemsId, FilterType.Equal);
                src = src.ApplyFilter(filter.TestingPointsId, x => x.TestingPointsId, FilterType.Equal);
                src = src.ApplyFilter(filter.ScanningMethodLookupId, x => x.ScanningMethodLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.ChildsOrderTypeLookupId, x => x.ChildsOrderTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.ChildsScanningTypeLookupId, x => x.ChildsScanningTypeLookupId, FilterType.Equal);
                src = src.ApplyFilter(filter.Order, x => x.Order, FilterType.Equal);
                src = src.ApplyFilter(filter.ScansNumber, x => x.ScansNumber, FilterType.Equal);
                src = src.ApplyFilter(filter.MatchesNumber, x => x.MatchesNumber, FilterType.Equal);
                src = src.ApplyFilter(filter.FinishAllScanRounds, x => x.FinishAllScanRounds, FilterType.Equal);
                src = src.ApplyFilter(filter.DirectAccessChecks, x => x.DirectAccessChecks, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #region StageAnnouncements

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns></returns>
        private BindingList<StageAnnouncement> LoadEntitiesWorker(StageAnnouncementsFilter filter)
        {
            using (var adapter = DataAccessAdapter)
            {
                var data = new LinqMetaData(adapter);

                var src = filter.LoadingType == LoadingTypeEnum.None ? data.StageAnnouncement : data.StageAnnouncement.WithPath(_pathEdgesStageAnnouncement);

                src = src.ApplyFilter(filter.Key, x => x.Key, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.Text, x => x.Text, FilterType.StringEqualOrContains);
                src = src.ApplyFilter(filter.AudioPath, x => x.AudioPath, FilterType.StringEqualOrContains);

                //String filters If Any
                /*src = src.ApplySearchKey(filter.SearchKey, 
                                    x => x.Key, 
                                    x => x.Text, 
                                    x => x.AudioPath);*/

                //Generic Filters
                src = src.ApplyFilter(filter.CreationDateTime, x => x.CreationDateTime, FilterType.Equal);
                src = src.ApplyFilter(filter.UpdatedDateTime, x => x.UpdatedDateTime, FilterType.Equal);

                return src.GetMappedPageList(filter);
            }
        }

        #endregion

        #endregion
    }
}
