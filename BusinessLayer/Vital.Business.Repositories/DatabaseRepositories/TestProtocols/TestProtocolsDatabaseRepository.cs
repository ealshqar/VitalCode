using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.TestProtocols
{
    public class TestProtocolDatabaseRepository : BaseRepository,ITestProtocolRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<TestProtocolEntity>, IPathEdgeRootParser<TestProtocolEntity>> _pathEdgesTestProtocol = 
            p => p.Prefetch<ProtocolStepEntity>(c => c.ProtocolSteps)
                    .SubPath(a => a.Prefetch(aa => aa.Type))
                  .Prefetch<ProtocolItemEntity>(cc => cc.ProtocolItems)
                    .SubPath(pi => pi.Prefetch<ItemEntity>(pii => pii.Item)
                        .SubPath(aaa=>aaa.Prefetch(e=>e.TypeLookup))
                            .SubPath(a => a.Prefetch<ItemPropertyEntity>(aa => aa.Properties)
                                .SubPath(aaa => aaa.Prefetch<PropertyEntity>(aaaa => aaaa.Property)
                                    .SubPath(pr => pr
                                        .Prefetch(pat => pat.ApplicableTypeLookup)
                                        .Prefetch(pvt => pvt.ValueTypeLookup))))
                           .SubPath(i => i.Prefetch(c => c.Parents).Prefetch<ItemDetailsEntity>(id => id.ItemDetail)
                               .SubPath(ide => ide.Prefetch<ImageEntity>(img => img.Image).Exclude(kk => kk.Data))));

        private readonly Func<IPathEdgeRootParser<ProtocolStepEntity>, IPathEdgeRootParser<ProtocolStepEntity>> _pathEdgesProtocolStep = 
            p => p.Prefetch(c => c.TestProtocol)
                  .Prefetch(cc => cc.Type);

        private readonly Func<IPathEdgeRootParser<ProtocolItemEntity>, IPathEdgeRootParser<ProtocolItemEntity>> _pathEdgesProtocolItem =
            p => p.Prefetch(c => c.TestProtocol)
                  .Prefetch<ItemEntity>(cc => cc.Item)
                    .SubPath(z => z.Prefetch(zz => zz.TypeLookup))
                    .SubPath(z => z.Prefetch(zz => zz.Children))
                    .SubPath(a => a.Prefetch<ItemRelationEntity>(kk => kk.Parents)
                                       .SubPath(cp => cp.Prefetch<ItemEntity>(ccp => ccp.Child)
                                                        .SubPath(cpp => cpp.Prefetch<ItemPropertyEntity>(ck => ck.Properties)
                                                                                .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property).SubPath(pl =>
                                                                                               pl.Prefetch(vl => vl.ValueTypeLookup)
                                                                                                 .Prefetch(al => al.ApplicableTypeLookup))))))
                    .SubPath(i => i.Prefetch<ItemDetailsEntity>(id => id.ItemDetail)
                        .SubPath(ide => ide.Prefetch<ImageEntity>(img => img.Image).Exclude(kk => kk.Data)))
                    .SubPath(pp=> pp.Prefetch<ItemPropertyEntity>(pl => pl.Properties)
                        .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property)
                            .SubPath(pl => pl.Prefetch(vl => vl.ValueTypeLookup)
                                .Prefetch(al => al.ApplicableTypeLookup))));
        
        #endregion

        #region Test Protocols

        #region Public Methods

        /// <summary>
        /// Loads TestProtocol by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestProtocol</returns>
        public TestProtocol LoadTestProtocolById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.TestProtocol.Where(c => c.Id == id).WithPath(_pathEdgesTestProtocol);

                    var testProtocol = src.FirstOrDefault();

                    var testProtocolObj = new TestProtocol();

                    Mapper.Map(testProtocol, testProtocolObj);

                    return testProtocolObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of TestProtocols.
        /// </summary>
        /// <returns>List of TestProtocols.</returns>
        public BindingList<TestProtocol> LoadTestProtocols(string name, string description, LoadingTypeEnum loadingType)
        {
            try
            {
                return LoadTestProtocolsWorker(name, description, loadingType == LoadingTypeEnum.All ? _pathEdgesTestProtocol : null);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a testProtocol.
        /// </summary>
        /// <param name="testProtocolToSave">The testProtocol.</param>
        /// <returns>The testProtocol.</returns>
        public ProcessResult Save(TestProtocol testProtocolToSave)
        {
            Check.Argument.IsNotNull(testProtocolToSave, "testProtocol to save");

            try
            {
                var testProtocolEntity = Mapper.Map<TestProtocol, TestProtocolEntity>(testProtocolToSave);

                testProtocolEntity.IsNew = testProtocolEntity.Id <= 0;

                var processResult = CommonRepository.Save(testProtocolEntity);

                testProtocolToSave.Id = testProtocolEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a testProtocol.
        /// </summary>
        /// <param name="testProtocolToDelete">The testProtocol.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(TestProtocol testProtocolToDelete)
        {
            Check.Argument.IsNotNull(testProtocolToDelete, "testProtocol to delete");

            try
            {
                var testProtocolEntity = Mapper.Map<TestProtocol, TestProtocolEntity>(testProtocolToDelete);

                return CommonRepository.Delete(testProtocolEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of TestProtocols.
        /// </summary>
        /// <returns></returns>
        private static BindingList<TestProtocol> LoadTestProtocolsWorker(string name , string  description, Func<IPathEdgeRootParser<TestProtocolEntity>, IPathEdgeRootParser<TestProtocolEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                IQueryable<TestProtocolEntity> src = data.TestProtocol;

                if (pathEdges != null)
                    src = src.WithPath(pathEdges);

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(cc => cc.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

                if (!string.IsNullOrEmpty(description))
                    src = src.Where(cc => cc.Description.ToLowerInvariant().Contains(description.ToLowerInvariant()));

                var testProtocols = src.ToList();

                var testProtocolsObjList = new BindingList<TestProtocol>();

                Mapper.Map(testProtocols, testProtocolsObjList);

                return testProtocolsObjList;
            }
        }
       
        #endregion   

        #endregion        

        #region Protocol Steps

        #region Public Methods
        
        /// <summary>
        /// Loads ProtocolStep by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The ProtocolStep</returns>
        public ProtocolStep LoadProtocolStepById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.ProtocolStep.Where(c => c.Id == id).WithPath(_pathEdgesProtocolStep);

                    var protocolStep = src.FirstOrDefault();

                    var protocolStepObj = new ProtocolStep();

                    Mapper.Map(protocolStep, protocolStepObj);

                    return protocolStepObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of ProtocolSteps.
        /// </summary>
        /// <returns>List of ProtocolSteps.</returns>
        public BindingList<ProtocolStep> LoadProtocolSteps(int testProtocolId , int order )
        {
            try
            {
                return LoadProtocolStepsWorker(testProtocolId , order , _pathEdgesProtocolStep);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a protocolStep.
        /// </summary>
        /// <param name="protocolStepToSave">The protocolStep.</param>
        /// <returns>The protocolStep.</returns>
        public ProcessResult Save(ProtocolStep protocolStepToSave)
        {
            Check.Argument.IsNotNull(protocolStepToSave, "protocolStep to save");

            try
            {
                var protocolStepEntity = Mapper.Map<ProtocolStep, ProtocolStepEntity>(protocolStepToSave);

                protocolStepEntity.IsNew = protocolStepEntity.Id <= 0;

                var processResult = CommonRepository.Save(protocolStepEntity);

                protocolStepToSave.Id = protocolStepEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a protocolStep.
        /// </summary>
        /// <param name="protocolStepToDelete">The protocolStep.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(ProtocolStep protocolStepToDelete)
        {
            Check.Argument.IsNotNull(protocolStepToDelete, "protocolStep to delete");

            try
            {
                var protocolStepEntity = Mapper.Map<ProtocolStep, ProtocolStepEntity>(protocolStepToDelete);

                return CommonRepository.Delete(protocolStepEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of ProtocolSteps.
        /// </summary>
        /// <returns></returns>
        private static BindingList<ProtocolStep> LoadProtocolStepsWorker(int testProtocolId , int order , Func<IPathEdgeRootParser<ProtocolStepEntity>, IPathEdgeRootParser<ProtocolStepEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.ProtocolStep.WithPath(pathEdges);

                if (testProtocolId > 0)
                    src = src.Where(c => c.TestProtocolId == testProtocolId);

                if (order > 0)
                    src = src.Where(c => c.Order == order);

                src = src.OrderBy(c => c.Order);

                var protocolSteps = src.ToList();

                var protocolStepsObjList = new BindingList<ProtocolStep>();

                Mapper.Map(protocolSteps, protocolStepsObjList);

                return protocolStepsObjList;
            }
        }
       
        #endregion 

        #endregion

        #region Protocol Items

        #region Public Methods
        
        /// <summary>
        /// Loads ProtocolItem by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The ProtocolItem</returns>
        public ProtocolItem LoadProtocolItemById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.ProtocolItem.Where(c => c.Id == id).WithPath(_pathEdgesProtocolItem);

                    var protocolItem = src.FirstOrDefault();

                    var protocolItemObj = new ProtocolItem();

                    Mapper.Map(protocolItem, protocolItemObj);

                    return protocolItemObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of ProtocolItems.
        /// </summary>
        /// <returns>List of ProtocolItems.</returns>
        public BindingList<ProtocolItem> LoadProtocolItems(int testProtocolId , int itemId)
        {
            try
            {
                return LoadProtocolItemsWorker(testProtocolId , itemId , _pathEdgesProtocolItem);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a protocolItem.
        /// </summary>
        /// <param name="protocolItemToSave">The protocolItem.</param>
        /// <returns>The protocolItem.</returns>
        public ProcessResult Save(ProtocolItem protocolItemToSave)
        {
            Check.Argument.IsNotNull(protocolItemToSave, "protocolItem to save");

            try
            {
                var protocolItemEntity = Mapper.Map<ProtocolItem, ProtocolItemEntity>(protocolItemToSave);

                protocolItemEntity.IsNew = protocolItemEntity.Id <= 0;

                var processResult = CommonRepository.Save(protocolItemEntity);

                protocolItemToSave.Id = protocolItemEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a protocolItem.
        /// </summary>
        /// <param name="protocolItemToDelete">The protocolItem.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(ProtocolItem protocolItemToDelete)
        {
            Check.Argument.IsNotNull(protocolItemToDelete, "protocolItem to delete");

            try
            {
                var protocolItemEntity = Mapper.Map<ProtocolItem, ProtocolItemEntity>(protocolItemToDelete);

                return CommonRepository.Delete(protocolItemEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of ProtocolItems.
        /// </summary>
        /// <returns></returns>
        private static BindingList<ProtocolItem> LoadProtocolItemsWorker(int testProtocolId , int itemId , Func<IPathEdgeRootParser<ProtocolItemEntity>, IPathEdgeRootParser<ProtocolItemEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.ProtocolItem.WithPath(pathEdges);

                if (testProtocolId > 0)
                    src = src.Where(c => c.TestProtocolId == testProtocolId);

                if (itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);

                var protocolItems = src.ToList();

                var protocolItemsObjList = new BindingList<ProtocolItem>();

                Mapper.Map(protocolItems, protocolItemsObjList);

                return protocolItemsObjList;
            }
        }
       
        #endregion  

        #endregion
    }
}
