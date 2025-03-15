using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using DevExpress.Internal;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.VitalForceSheet
{
    public class VitalForceSheetDatabaseRepository : BaseRepository, IVitalForceSheetRepository
    {
        #region Path Edges

        #region VFS Items Source

        private readonly Func<IPathEdgeRootParser<VFSItemSourceEntity>, IPathEdgeRootParser<VFSItemSourceEntity>>
            _pathEdgesVFSItemSource
                =
                s => s.Prefetch(cc => cc.SectionLookup)
                    .Prefetch(cc => cc.GroupLookup)
                    .Prefetch(cc => cc.GridGroupLookup)
                    .Prefetch(cc => cc.GenderLookup)
                    .Prefetch(cc => cc.V1TypeLookup)
                    .Prefetch(cc => cc.V2TypeLookup)
                    .Prefetch(cc => cc.Item);

        #endregion

        #region VFS Secondary Item Source

        private readonly Func<IPathEdgeRootParser<VFSSecondaryItemSourceEntity>, IPathEdgeRootParser<VFSSecondaryItemSourceEntity>> _pathEdgesVFSSecondaryItemSource
            =
            s => s.Prefetch(cc => cc.SectionLookup)
                  .Prefetch(cc => cc.Item);

        #endregion

        #region VFS

        private readonly Func<IPathEdgeRootParser<VFSEntity>, IPathEdgeRootParser<VFSEntity>> _pathEdgesVFSLight = s => s
                .Prefetch(cc => cc.Test)
                .Prefetch(cc => cc.Patient);

        private readonly Func<IPathEdgeRootParser<VFSEntity>, IPathEdgeRootParser<VFSEntity>> _pathEdgesVFS = s => s
                .Prefetch(cc => cc.Test)
                .Prefetch(cc=>cc.Patient)
                .Prefetch<VFSSecondaryItemEntity>(cc => cc.VFSSecondaryItems).SubPath(ci => ci
                        .Prefetch(i => i.Item)
                        .Prefetch(i => i.SectionLookup))
                .Prefetch<VFSItemEntity>(cc => cc.VFSItems).SubPath(ci => ci
                        .Prefetch(i => i.Item)
                        .Prefetch<VFSItemSourceEntity>(i => i.VFSItemSource).SubPath(vis=> vis
                            .Prefetch(viss => viss.V1TypeLookup)
                            .Prefetch(visss => visss.V2TypeLookup))
                        .Prefetch(i=> i.SectionLookup)
                        .Prefetch(i => i.GroupLookup)
                        .Prefetch(i => i.GridGroupLookup));

        #endregion

        #region VFSI tem

        private readonly Func<IPathEdgeRootParser<VFSItemEntity>, IPathEdgeRootParser<VFSItemEntity>>
            _pathEdgesVFSItem = s => s.Prefetch(c => c.VFS)
                .Prefetch(cc => cc.Item)
                .Prefetch<VFSItemSourceEntity>(cc => cc.VFSItemSource)
                .SubPath(ci => ci.Prefetch(i => i.Item));


        #endregion

        #region VFS Secondary Items

        private readonly Func<IPathEdgeRootParser<VFSSecondaryItemEntity>, IPathEdgeRootParser<VFSSecondaryItemEntity>> _pathEdgesVFSSecondaryItem
            =  s => s.Prefetch(cc => cc.Item)
                     .Prefetch(cc => cc.VFS);

        #endregion

        #endregion

        #region VFS Secondary Item Source

        #region Public Methods

        #region VFS Secondary Items Source

        /// <summary>
        /// Loads VFSSecondaryItemsSource by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSSecondaryItemsSource</returns>
        public VFSSecondaryItemSource LoadVFSSecondaryItemsSourceById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.VFSSecondaryItemSource.Where(c => c.Id == id).WithPath(_pathEdgesVFSSecondaryItemSource);

                    var vFSSecondaryItemsSource = src.FirstOrDefault();

                    var vFSSecondaryItemsSourceObj = new VFSSecondaryItemSource();

                    Mapper.Map(vFSSecondaryItemsSource, vFSSecondaryItemsSourceObj);

                    return vFSSecondaryItemsSourceObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of VFSSecondaryItemsSource.
        /// </summary>
        /// <returns>List of VFSSecondaryItemsSource.</returns>
        public BindingList<VFSSecondaryItemSource> LoadVFSSecondaryItemsSource(int itemID, int sectionTypeLookupId)
        {
            try
            {
                return LoadVFSSecondaryItemsSourceWorker(itemID, sectionTypeLookupId, _pathEdgesVFSSecondaryItemSource);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Saves a vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="vFSSecondaryItemsSourceToSave">The vFSSecondaryItemsSource.</param>
        /// <returns>The vFSSecondaryItemsSource.</returns>
        public ProcessResult Save(VFSSecondaryItemSource vFSSecondaryItemsSourceToSave)
        {
            Check.Argument.IsNotNull(vFSSecondaryItemsSourceToSave, "vfs seconday source item to save");

            try
            {
                var vfsSecondaryItemSourceEntity = Mapper.Map<VFSSecondaryItemSource, VFSSecondaryItemSourceEntity>(vFSSecondaryItemsSourceToSave);

                vfsSecondaryItemSourceEntity.IsNew = vfsSecondaryItemSourceEntity.Id <= 0;

                var processResult = CommonRepository.Save(vfsSecondaryItemSourceEntity);

                vFSSecondaryItemsSourceToSave.Id = vfsSecondaryItemSourceEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="vFSSecondaryItemsSourceToDelete">The vFSSecondaryItemsSource.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(VFSSecondaryItemSource vFSSecondaryItemsSourceToDelete)
        {
            Check.Argument.IsNotNull(vFSSecondaryItemsSourceToDelete, "vFSSecondaryItemsSource to delete");

            try
            {
                var vFSSecondaryItemsSourceEntity = Mapper.Map<VFSSecondaryItemSource, VFSSecondaryItemSourceEntity>(vFSSecondaryItemsSourceToDelete);

                return CommonRepository.Delete(vFSSecondaryItemsSourceEntity);
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
        /// Loads a list of VFSSecondaryItemsSource.
        /// </summary>
        /// <returns></returns>
        private static BindingList<VFSSecondaryItemSource> LoadVFSSecondaryItemsSourceWorker(int itemID, int sectionTypeLookupId, Func<IPathEdgeRootParser<VFSSecondaryItemSourceEntity>, IPathEdgeRootParser<VFSSecondaryItemSourceEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.VFSSecondaryItemSource.WithPath(pathEdges);

                if (itemID > 0)
                    src = src.Where(s => s.ItemId == itemID);

                if (sectionTypeLookupId > 0)
                    src = src.Where(s => s.SectionLookupId == sectionTypeLookupId);

                var vFSSecondaryItemsSource = src.ToList();

                var vFSSecondaryItemsSourceObjList = new BindingList<VFSSecondaryItemSource>();

                Mapper.Map(vFSSecondaryItemsSource, vFSSecondaryItemsSourceObjList);

                return vFSSecondaryItemsSourceObjList;
            }
        }

        #endregion        

        #endregion

        #region VFS Secondary Items

        #region Public Method
        
        /// <summary>
        /// Loads VFSSecondaryItem by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSSecondaryItem</returns>
        public VFSSecondaryItem LoadVFSSecondaryItemById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.VFSSecondaryItem.Where(c => c.Id == id).WithPath(_pathEdgesVFSSecondaryItem);

                    var vFSSecondaryItem = src.FirstOrDefault();

                    var vFSSecondaryItemObj = new VFSSecondaryItem();

                    Mapper.Map(vFSSecondaryItem, vFSSecondaryItemObj);

                    return vFSSecondaryItemObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of VFSSecondaryItems.
        /// </summary>
        /// <returns>List of VFSSecondaryItems.</returns>
        public BindingList<VFSSecondaryItem> LoadVFSSecondaryItems(int vfsID,int itemId, int sectionLookupId)
        {
            try
            {
                return LoadVFSSecondaryItemsWorker(vfsID,itemId,sectionLookupId, _pathEdgesVFSSecondaryItem);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a vFSSecondaryItem.
        /// </summary>
        /// <param name="vFSSecondaryItemToSave">The vFSSecondaryItem.</param>
        /// <returns>The vFSSecondaryItem.</returns>
        public ProcessResult Save(VFSSecondaryItem vFSSecondaryItemToSave)
        {
            Check.Argument.IsNotNull(vFSSecondaryItemToSave, "vFSSecondaryItem to save");

            try
            {
                var vFSSecondaryItemEntity = Mapper.Map<VFSSecondaryItem, VFSSecondaryItemEntity>(vFSSecondaryItemToSave);

                vFSSecondaryItemEntity.IsNew = vFSSecondaryItemEntity.Id > 0 ? false : true;

                var processResult = CommonRepository.Save(vFSSecondaryItemEntity);

                vFSSecondaryItemToSave.Id = vFSSecondaryItemEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a vFSSecondaryItem.
        /// </summary>
        /// <param name="vFSSecondaryItemToDelete">The vFSSecondaryItem.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(VFSSecondaryItem vFSSecondaryItemToDelete)
        {
            Check.Argument.IsNotNull(vFSSecondaryItemToDelete, "vFSSecondaryItem to delete");

            try
            {
                var vFSSecondaryItemEntity = Mapper.Map<VFSSecondaryItem, VFSSecondaryItemEntity>(vFSSecondaryItemToDelete);

                return CommonRepository.Delete(vFSSecondaryItemEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Method

        /// <summary>
        /// Loads a list of VFSSecondaryItems.
        /// </summary>
        /// <returns></returns>
        private static BindingList<VFSSecondaryItem> LoadVFSSecondaryItemsWorker(int vfsID, int itemId, int sectionLookupId, Func<IPathEdgeRootParser<VFSSecondaryItemEntity>, IPathEdgeRootParser<VFSSecondaryItemEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.VFSSecondaryItem.WithPath(pathEdges);

                if (vfsID > 0)
                    src = src.Where(c => c.VfsId == vfsID);

                if (itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);

                if (sectionLookupId > 0)
                    src = src.Where(s => s.SectionLookupId == sectionLookupId);
                
                var vFSSecondaryItems = src.ToList();

                var vFSSecondaryItemsObjList = new BindingList<VFSSecondaryItem>();

                Mapper.Map(vFSSecondaryItems, vFSSecondaryItemsObjList);

                return vFSSecondaryItemsObjList;
            }
        }

        #endregion

        #endregion

        #region VFS

        #region Public Methods

        /// <summary>
        /// Loads Vital Force Sheet by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFS.</returns>
        public VFS LoadVFSById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.VFS.Where(c => c.Id == id).WithPath(_pathEdgesVFS);

                    var vFS = src.FirstOrDefault();

                    var vFSObj = new VFS();

                    Mapper.Map(vFS, vFSObj);

                    return vFSObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Vital Force Sheets.
        /// </summary>
        /// <returns>List of Vital Force Sheets.</returns>
        public BindingList<VFS> LoadVFSs(string name, DateTime? dateTime, int thyroidNumOfIssues, int mercuryNumOfIssues, string emotionalIssues, string notes, int testId, int patientId, LoadingTypeEnum loadingType)
        {
            try
            {
                return LoadVFSsWorker(name, dateTime, thyroidNumOfIssues, mercuryNumOfIssues, emotionalIssues, notes, testId, patientId,loadingType == LoadingTypeEnum.All ? _pathEdgesVFS : null);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a Vital Force Sheet.
        /// </summary>
        /// <param name="vFS">The Vital Force Sheet.</param>
        /// <returns>The vFSSecondaryItemsSource.</returns>
        public ProcessResult Save(VFS vFS)
        {
            Check.Argument.IsNotNull(vFS, "vfs to save");

            try
            {
                var vfsEntity = Mapper.Map<VFS, VFSEntity>(vFS);

                vfsEntity.IsNew = vfsEntity.Id <= 0;

                var processResult = CommonRepository.Save(vfsEntity);

                vFS.Id = vfsEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a Vital Force Sheet.
        /// </summary>
        /// <param name="vFS">The Vital Force Sheet.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(VFS vFS)
        {
            Check.Argument.IsNotNull(vFS, "vFS to delete");

            try
            {
                var vFSEntity = Mapper.Map<VFS, VFSEntity>(vFS);

                return CommonRepository.Delete(vFSEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of Vital Force Sheets private worker.
        /// </summary>
        /// <returns>List of Vital Force Sheets.</returns>
        private BindingList<VFS> LoadVFSsWorker(string name, DateTime? dateTime, int thyroidNumOfIssues, int mercuryNumOfIssues, string emotionalIssues, string notes, int testId, int patientId, Func<IPathEdgeRootParser<VFSEntity>, IPathEdgeRootParser<VFSEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = pathEdges == null ? data.VFS : data.VFS.WithPath(pathEdges);

                if(!string.IsNullOrEmpty(name))
                    src = src.Where(s => s.Name.Equals(name));

                if(dateTime.HasValue)
                    src = src.Where(s => s.DateTime == dateTime);

                if (thyroidNumOfIssues > 0)
                    src = src.Where(s => s.ThyroidNumOfIssues.HasValue && s.ThyroidNumOfIssues.Value == thyroidNumOfIssues);

                if (mercuryNumOfIssues > 0)
                    src = src.Where(s => s.MercuryNumOfIssues.HasValue && s.MercuryNumOfIssues.Value == mercuryNumOfIssues);

                if (!string.IsNullOrEmpty(emotionalIssues))
                    src = src.Where(s => s.EmotionalIssues.Contains(emotionalIssues));

                if (!string.IsNullOrEmpty(notes))
                    src = src.Where(s => s.Notes.Contains(notes));

                if (testId > 0)
                    src = src.Where(s => s.TestId == testId);

                if (patientId > 0)
                    src = src.Where(s => s.PatientId == patientId);

                var vFS = src.ToList();

                var vFSObjList = new BindingList<VFS>();

                Mapper.Map(vFS, vFSObjList);

                return vFSObjList;
            }
        }

        #endregion

        #endregion

        #region VFS Item Source

        #region Public Methods

        /// <summary>
        /// Loads VFSItemSource by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSItemSource</returns>
        public VFSItemSource LoadVFSItemsSourceById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.VFSItemSource.Where(c => c.Id == id).WithPath(_pathEdgesVFSItemSource);

                    var vfsItemsSource = src.FirstOrDefault();

                    var vfsItemsSourceObj = new VFSItemSource();

                    Mapper.Map(vfsItemsSource, vfsItemsSourceObj);

                    return vfsItemsSourceObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of VFSItemSource.
        /// </summary>
        /// <returns>List of VFSItemSource.</returns>
        public BindingList<VFSItemSource> LoadVFSItemsSource(int itemId, int sectionTypeLookupId, int groupLookupId,int genderLookupId,int v1TypeLookupId,int v2TypeLookupId, bool isActive)
        {
            try
            {
                return LoadVFSItemsSourceWorker(itemId, sectionTypeLookupId,groupLookupId,genderLookupId,v1TypeLookupId,v2TypeLookupId,isActive, _pathEdgesVFSItemSource);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a VFSItemSource.
        /// </summary>
        /// <param name="vfsItemsSourceToSave">The VFSItemsSourceToSave.</param>
        /// <returns>The VFSItemsSourceToSave.</returns>
        public ProcessResult Save(VFSItemSource vfsItemsSourceToSave)
        {
            Check.Argument.IsNotNull(vfsItemsSourceToSave, "vfs source item to save");

            try
            {
                var vfsItemSourceEntity = Mapper.Map<VFSItemSource, VFSItemSourceEntity>(vfsItemsSourceToSave);

                vfsItemSourceEntity.IsNew = vfsItemSourceEntity.Id <= 0;

                var processResult = CommonRepository.Save(vfsItemSourceEntity);

                vfsItemsSourceToSave.Id = vfsItemSourceEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a VFSItemSource.
        /// </summary>
        /// <param name="vfsItemsSourceToDelete">The VFSItemsSourceToDelete.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(VFSItemSource vfsItemsSourceToDelete)
        {
            Check.Argument.IsNotNull(vfsItemsSourceToDelete, "vfs Items Source to delete");

            try
            {
                var vfsItemsSourceEntity = Mapper.Map<VFSItemSource, VFSItemSourceEntity>(vfsItemsSourceToDelete);

                return CommonRepository.Delete(vfsItemsSourceEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of VFSItemsSource.
        /// </summary>
        /// <returns></returns>
        private static BindingList<VFSItemSource> LoadVFSItemsSourceWorker(int itemId, int sectionTypeLookupId,int groupLookupId, int v1TypeLookupId,int v2TypeLookupId, int genderLookupId, bool isActive, Func<IPathEdgeRootParser<VFSItemSourceEntity>, IPathEdgeRootParser<VFSItemSourceEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.VFSItemSource.WithPath(pathEdges);

                if (itemId > 0)
                    src = src.Where(s => s.ItemId == itemId);

                if (sectionTypeLookupId > 0)
                    src = src.Where(s => s.SectionLookupId == sectionTypeLookupId);

                if (groupLookupId > 0)
                    src = src.Where(s => s.GroupLookupId == groupLookupId);

                if (v1TypeLookupId > 0)
                    src = src.Where(s => s.V1TypeLookupId == v1TypeLookupId);

                if (v2TypeLookupId > 0)
                    src = src.Where(s => s.V2TypeLookupId == v2TypeLookupId);

                if (genderLookupId > 0)
                    src = src.Where(s => s.GenderLookupId == genderLookupId);

                if (isActive)
                    src = src.Where(s => s.IsActive ==isActive);

                var vfsItemsSource = src.ToList();

                var vfsItemsSourceObjList = new BindingList<VFSItemSource>();

                Mapper.Map(vfsItemsSource, vfsItemsSourceObjList);

                return vfsItemsSourceObjList;
            }
        }

        #endregion
        #endregion

        #region VFSI tem

        #region Public Methods

        /// <summary>
        /// Loads Vital Force Sheet Item by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSItem.</returns>
        public VFSItem LoadVFSItemById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.VFSItem.Where(c => c.Id == id).WithPath(_pathEdgesVFSItem);

                    var vFS = src.FirstOrDefault();

                    var vFSiObj = new VFSItem();

                    Mapper.Map(vFS, vFSiObj);

                    return vFSiObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of Vital Force Sheet Items.
        /// </summary>
        /// <returns>List of Vital Force Sheet Items.</returns>
        public BindingList<VFSItem> LoadVFSItems(int vfsId, int itemSourceId, int itemId, string previousV1, string previousV2,
            string currentV1, string currentV2, bool? isSkipped, string comments)
        {
            try
            {
                return LoadVFSItemsWorker(vfsId, itemSourceId, itemId, previousV1, previousV2,
                    currentV1, currentV2, isSkipped, comments, _pathEdgesVFSItem);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a Vital Force Sheet Item.
        /// </summary>
        /// <param name="vFSItem">The Vital Force Sheet.</param>
        /// <returns>The Result.</returns>
        public ProcessResult Save(VFSItem vFSItem)
        {
            Check.Argument.IsNotNull(vFSItem, "vfs item to save");

            try
            {
                var vfsItemEntity = Mapper.Map<VFSItem, VFSItemEntity>(vFSItem);

                vfsItemEntity.IsNew = vfsItemEntity.Id <= 0;

                var processResult = CommonRepository.Save(vfsItemEntity);

                vFSItem.Id = vfsItemEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a Vital Force Sheet Item.
        /// </summary>
        /// <param name="vFSItem">The Vital Force Sheet.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(VFSItem vFSItem)
        {
            Check.Argument.IsNotNull(vFSItem, "vfs Item to delete");

            try
            {
                var vfsItemsEntity = Mapper.Map<VFSItem, VFSItemEntity>(vFSItem);

                return CommonRepository.Delete(vfsItemsEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Private Workers

        /// <summary>
        /// Loads a list of Vital Force Sheet Items.
        /// </summary>
        /// <returns>List of Vital Force Sheet Items.</returns>
        private BindingList<VFSItem> LoadVFSItemsWorker(int vfsId, int itemSourceId, int itemId, string previousV1,
            string previousV2, string currentV1, string currentV2, bool? isSkipped, string comments,
            Func<IPathEdgeRootParser<VFSItemEntity>, IPathEdgeRootParser<VFSItemEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.VFSItem.WithPath(pathEdges);

                if (vfsId > 0)
                    src = src.Where(s => s.VFSId == vfsId);

                if (itemSourceId > 0)
                    src = src.Where(s => s.VFSitemSourceId == itemSourceId);

                if (itemId > 0)
                    src = src.Where(s => s.ItemId == itemId);

                if (!string.IsNullOrEmpty(previousV1))
                    src = src.Where(s => s.PreviousV1.Equals(previousV1));

                if (!string.IsNullOrEmpty(previousV2))
                    src = src.Where(s => s.PreviousV2.Equals(previousV2));

                if (!string.IsNullOrEmpty(currentV1))
                    src = src.Where(s => s.CurrentV1.Equals(currentV1));

                if (!string.IsNullOrEmpty(currentV2))
                    src = src.Where(s => s.CurrentV1.Equals(currentV2));

                if (isSkipped.HasValue)
                    src = src.Where(s => s.IsSkipped == isSkipped);

                if (!string.IsNullOrEmpty(comments))
                    src = src.Where(s => s.Comments.Contains(comments));

                var vFS = src.ToList();

                var vFSObjList = new BindingList<VFSItem>();

                Mapper.Map(vFS, vFSObjList);

                return vFSObjList;
            }
        }


        #endregion
        
        #endregion
    }
} 