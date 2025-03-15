using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Filtering;
using Vital.Business.Repositories.DatabaseRepositories.VitalForceSheet;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class VitalForceSheetManager : BaseManager
    {
        #region Private Variables

        private readonly IVitalForceSheetRepository _iVitalForceSheetRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public VitalForceSheetManager()
        {
            _iVitalForceSheetRepository = new VitalForceSheetDatabaseRepository();
        }

        #endregion

        #region VFSSecondaryItemsSource

        #region Public Methods

        /// <summary>
        /// Gets a vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public VFSSecondaryItemSource GetVFSSecondaryItemsSourceById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _iVitalForceSheetRepository.LoadVFSSecondaryItemsSourceById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        
        /// <summary>
        /// Gets a list of vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<VFSSecondaryItemSource> GetVFSSecondaryItemsSource(VFSSecondaryItemSourceFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _iVitalForceSheetRepository.LoadVFSSecondaryItemsSource(filter.ItemId, filter.SectionLookupId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the VFSSecondaryItemSource list.
        /// </summary>
        /// <returns></returns>
        public ProcessResult SaveVFSSecondaryItemsSources(BindingList<VFSSecondaryItemSource> vfsSecondaryItemsSources)
        {
            Check.Argument.IsNotNull(() => vfsSecondaryItemsSources);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                for (var i = 0; i < vfsSecondaryItemsSources.Count; i++)
                {
                    var orderItem = vfsSecondaryItemsSources[i];

                    orderItem.SetUserAndDates();

                    if (orderItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteVFSSecondaryItemsSource(orderItem);
                        vfsSecondaryItemsSources.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveVFSSecondaryItemsSource(orderItem);
                    }

                    if (!processResult.IsSucceed) break;
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
        /// Saves the vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="vFSSecondaryItemsSource">The vFSSecondaryItemsSource.</param>
        /// <returns></returns>
        public ProcessResult SaveVFSSecondaryItemsSource(VFSSecondaryItemSource vFSSecondaryItemsSource)
        {
            Check.Argument.IsNotNull(() => vFSSecondaryItemsSource);

            try
            { 
                vFSSecondaryItemsSource.SetUserAndDates();

                var processResult = _iVitalForceSheetRepository.Save(vFSSecondaryItemsSource);

                if (processResult.IsSucceed ) { vFSSecondaryItemsSource.ObjectState = DomainEntityState.Unchanged; }

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
        /// Deletes a vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="vFSSecondaryItemsSource">The vFSSecondaryItemsSource.</param>
        /// <returns></returns>
        public ProcessResult DeleteVFSSecondaryItemsSource(VFSSecondaryItemSource vFSSecondaryItemsSource)
        {
            Check.Argument.IsNotNull(() => vFSSecondaryItemsSource);

            try
            {
                var  processResult = _iVitalForceSheetRepository.Delete(vFSSecondaryItemsSource);

                if (processResult.IsSucceed) { vFSSecondaryItemsSource.ObjectState = DomainEntityState.Deleted; }

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

        #region VFS Secondary Items

        #region Public Methods

        /// <summary>
        /// Gets a vFSSecondaryItem.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public VFSSecondaryItem GetVFSSecondaryItemById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _iVitalForceSheetRepository.LoadVFSSecondaryItemById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        
        /// <summary>
        /// Gets a list of vFSSecondaryItems.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<VFSSecondaryItem> GetVFSSecondaryItems(VFSSecondaryItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _iVitalForceSheetRepository.LoadVFSSecondaryItems(filter.VFSId, filter.ItemId, filter.SectionLookupId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the vFSSecondaryItem.
        /// </summary>
        /// <param name="vFSSecondaryItem">The vFSSecondaryItem.</param>
        /// <returns></returns>
        public ProcessResult SaveVFSSecondaryItem(VFSSecondaryItem vFSSecondaryItem)
        {
            Check.Argument.IsNotNull(() => vFSSecondaryItem);

            try
            { 
                vFSSecondaryItem.SetUserAndDates();

                var processResult = _iVitalForceSheetRepository.Save(vFSSecondaryItem);

                if (processResult.IsSucceed ) { vFSSecondaryItem.ObjectState = DomainEntityState.Unchanged; }

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
        /// Saves the passed VFS secondary items
        /// </summary>
        public ProcessResult SaveVFSSecondaryItems(BindingList<VFSSecondaryItem> vfsSecondaryItems)
        {
            Check.Argument.IsNotNull(() => vfsSecondaryItems);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true };

                for (var i = 0; i < vfsSecondaryItems.Count; i++)
                {
                    var vfsScondaryItem = vfsSecondaryItems[i];

                    vfsScondaryItem.SetUserAndDates();

                    if (vfsScondaryItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteVFSSecondaryItem(vfsScondaryItem);
                        vfsSecondaryItems.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveVFSSecondaryItem(vfsScondaryItem);
                    }

                    if (!processResult.IsSucceed) break;
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
        /// Deletes a vFSSecondaryItem.
        /// </summary>
        /// <param name="vFSSecondaryItem">The vFSSecondaryItem.</param>
        /// <returns></returns>
        public ProcessResult DeleteVFSSecondaryItem(VFSSecondaryItem vFSSecondaryItem)
        {
            Check.Argument.IsNotNull(() => vFSSecondaryItem);

            try
            {
                var  processResult = _iVitalForceSheetRepository.Delete(vFSSecondaryItem);

                if (processResult.IsSucceed) { vFSSecondaryItem.ObjectState = DomainEntityState.Deleted; }

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
        /// Deletes the passed VFS secondary item
        /// </summary>
        public ProcessResult DeleteVFSSecondaryItems(BindingList<VFSSecondaryItem> vfsSecondaryItems)
        {
            Check.Argument.IsNotNull(() => vfsSecondaryItems);

            try
            {
                foreach (var vfsSecondaryItem in vfsSecondaryItems)
                {
                    var result = DeleteVFSSecondaryItem(vfsSecondaryItem);

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

        #endregion

        #endregion

        #region VFS

        #region Public Methods

        /// <summary>
        /// Gets a Vital Force Sheet.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public VFS GetVFSById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _iVitalForceSheetRepository.LoadVFSById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Vital Force Sheets.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<VFS> GetVFSs(VFSsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _iVitalForceSheetRepository.LoadVFSs(filter.Name, filter.DateTime, filter.ThyroidNumOfIssues, filter.MercuryNumOfIssues, filter.EmotionalIssues, filter.Notes, filter.TestId, filter.PatientId, filter.LoadingType);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the Vital Force Sheets list.
        /// </summary>
        /// <returns></returns>
        public ProcessResult SaveVFS(BindingList<VFS> vFSs)
        {
            Check.Argument.IsNotNull(() => vFSs);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true };

                for (var i = 0; i < vFSs.Count; i++)
                {
                    var ovFSItem = vFSs[i];

                    ovFSItem.SetUserAndDates();

                    if (ovFSItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteVFS(ovFSItem);
                        vFSs.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveVFS(ovFSItem);
                    }

                    if (!processResult.IsSucceed) break;
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
        /// Saves the Vital Force Sheet.
        /// </summary>
        /// <param name="vFS">The Vital Force Sheet.</param>
        /// <returns></returns>
        public ProcessResult SaveVFS(VFS vFS)
        {
            Check.Argument.IsNotNull(() => vFS);

            try
            {
                if (!vFS.IsChanged)
                    return ProcessResult.Succeed;

                vFS.SetUserAndDates();

                var processResult = _iVitalForceSheetRepository.Save(vFS);

                if (!processResult.IsSucceed)
                    return processResult;

                processResult = SaveVFSSecondaryItems(vFS.VfsSecondaryItems);

                if (!processResult.IsSucceed)
                    return processResult;

                if (vFS.VfsItems != null)
                    processResult = SaveVFSItem(vFS.VfsItems);

                if (processResult.IsSucceed)
                {
                    vFS.ObjectState = DomainEntityState.Unchanged;
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
        /// Deletes a Vital Force Sheet.
        /// </summary>
        /// <param name="vFS">The Vital Force Sheet.</param>
        /// <returns></returns>
        public ProcessResult DeleteVFS(VFS vFS)
        {
            Check.Argument.IsNotNull(() => vFS);

            try
            {
                vFS.VfsSecondaryItems = GetVFSSecondaryItems(new VFSSecondaryItemsFilter(){VFSId = vFS.Id});

                ProcessResult processResult;

                if (vFS.VfsSecondaryItems != null)
                {
                    processResult = DeleteVFSSecondaryItems(vFS.VfsSecondaryItems);

                    if (!processResult.IsSucceed)
                        return processResult;
                }

                vFS.VfsItems = GetVFSItems(new VFSItemsFilter() { VfsId = vFS.Id });

                if (vFS.VfsItems != null)
                {
                    processResult = DeleteVFSItems(vFS.VfsItems);

                    if (!processResult.IsSucceed)
                        return processResult;
                }

                processResult = _iVitalForceSheetRepository.Delete(vFS);

                if (processResult.IsSucceed) { vFS.ObjectState = DomainEntityState.Deleted; }

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

        #region VFSItemSource

        #region Public Methods

        /// <summary>
        /// Gets a VFSItemsSource.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public VFSItemSource GetVFSItemsSourceById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _iVitalForceSheetRepository.LoadVFSItemsSourceById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of VFSItemsSource.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<VFSItemSource> GetVFSItemsSource(VFSItemSourceFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _iVitalForceSheetRepository.LoadVFSItemsSource(filter.ItemId,filter.SectionLookupId,filter.GroupLookupId,filter.GenderLookupId,filter.V1TypeLookupId,filter.V2TypeLookupId,filter.IsActive);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the VFSItemSource list.
        /// </summary>
        /// <returns></returns>
        public ProcessResult SaveVFSItemsSources(BindingList<VFSItemSource> vfsItemsSources)
        {
            Check.Argument.IsNotNull(() => vfsItemsSources);

            try
            {
                var processResult = new ProcessResult();
                processResult.IsSucceed = true;

                for (var i = 0; i < vfsItemsSources.Count; i++)
                {
                    var vfsItemSourceItem = vfsItemsSources[i];

                    vfsItemSourceItem.SetUserAndDates();

                    if (vfsItemSourceItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteVFSItemsSource(vfsItemSourceItem);
                        vfsItemsSources.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveVFSItemsSource(vfsItemSourceItem);
                    }

                    if (!processResult.IsSucceed) break;
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
        /// Saves the vfsItemsSource.
        /// </summary>
        /// <param name="vfsItemsSource">The vfsItemsSource.</param>
        /// <returns></returns>
        public ProcessResult SaveVFSItemsSource(VFSItemSource vfsItemsSource)
        {
            Check.Argument.IsNotNull(() => vfsItemsSource);

            try
            {
                vfsItemsSource.SetUserAndDates();

                var processResult = _iVitalForceSheetRepository.Save(vfsItemsSource);

                if (processResult.IsSucceed) { vfsItemsSource.ObjectState = DomainEntityState.Unchanged; }

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
        /// Deletes a vfsItemsSource.
        /// </summary>
        /// <param name="vfsItemsSource">The vfsItemsSource.</param>
        /// <returns></returns>
        public ProcessResult DeleteVFSItemsSource(VFSItemSource vfsItemsSource)
        {
            Check.Argument.IsNotNull(() => vfsItemsSource);

            try
            {
                var processResult = _iVitalForceSheetRepository.Delete(vfsItemsSource);

                if (processResult.IsSucceed) { vfsItemsSource.ObjectState = DomainEntityState.Deleted; }

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

        #region VFSItem

        #region Public Methods

        /// <summary>
        /// Gets a Vital Force Sheet Item.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public VFSItem GetVFSItemById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _iVitalForceSheetRepository.LoadVFSItemById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of Vital Force Sheet Items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<VFSItem> GetVFSItems(VFSItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _iVitalForceSheetRepository.LoadVFSItems(filter.VfsId, filter.ItemSourceId, filter.ItemId,
                    filter.PreviousV1, filter.PreviousV2, filter.CurrentV1, filter.CurrentV2, filter.IsSkipped,
                    filter.Comments);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the Vital Force Sheet Items list.
        /// </summary>
        /// <returns></returns>
        public ProcessResult SaveVFSItem(BindingList<VFSItem> vFsItems)
        {
            Check.Argument.IsNotNull(() => vFsItems);

            try
            {
                var processResult = new ProcessResult { IsSucceed = true };

                for (var i = 0; i < vFsItems.Count; i++)
                {
                    var oVFSItemItem = vFsItems[i];

                    oVFSItemItem.SetUserAndDates();

                    if (oVFSItemItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteVFSItem(oVFSItemItem);
                        vFsItems.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveVFSItem(oVFSItemItem);
                    }

                    if (!processResult.IsSucceed) break;
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
        /// Saves the Vital Force Sheet Item.
        /// </summary>
        /// <param name="vFsItem">The Vital Force Sheet Item.</param>
        /// <returns></returns>
        public ProcessResult SaveVFSItem(VFSItem vFsItem)
        {
            Check.Argument.IsNotNull(() => vFsItem);

            try
            {
                if (!vFsItem.IsChanged)
                    return ProcessResult.Succeed;

                vFsItem.SetUserAndDates();

                var processResult = _iVitalForceSheetRepository.Save(vFsItem);

                vFsItem.ObjectState = DomainEntityState.Unchanged;

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
        /// Deletes a Vital Force Sheet Item.
        /// </summary>
        /// <param name="vFsItem">The Vital Force Sheet Item.</param>
        /// <returns></returns>
        public ProcessResult DeleteVFSItem(VFSItem vFsItem)
        {
            Check.Argument.IsNotNull(() => vFsItem);

            try
            {
                var processResult = _iVitalForceSheetRepository.Delete(vFsItem);

                if (processResult.IsSucceed) { vFsItem.ObjectState = DomainEntityState.Deleted; }

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
        /// Deletes the passed VFS items
        /// </summary>
        public ProcessResult DeleteVFSItems(BindingList<VFSItem> vfsItems)
        {
            Check.Argument.IsNotNull(() => vfsItems);

            try
            {
                foreach (var vfsItem in vfsItems)
                {
                    var result = DeleteVFSItem(vfsItem);

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

        #endregion

        #endregion
    }
}