using System;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.VitalForceSheet
{
    public interface IVitalForceSheetRepository
    {
        #region VFS Secondary Item Source

        /// <summary>
        /// Loads VFSSecondaryItemsSource by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSSecondaryItemsSource</returns>
        VFSSecondaryItemSource LoadVFSSecondaryItemsSourceById(int id);
        
        /// <summary>
        /// Loads a list of VFSSecondaryItemsSource.
        /// </summary>
        /// <returns>List of VFSSecondaryItemsSource.</returns>
        BindingList<VFSSecondaryItemSource> LoadVFSSecondaryItemsSource(int itemID, int sectionTypeLookupId);
        
        /// <summary>
        /// Saves a vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="vFSSecondaryItemsSourceToSave">The vFSSecondaryItemsSource.</param>
        /// <returns>The vFSSecondaryItemsSource.</returns>
        ProcessResult Save(VFSSecondaryItemSource vFSSecondaryItemsSourceToSave);        

        /// <summary>
        /// Deletes a vFSSecondaryItemsSource.
        /// </summary>
        /// <param name="vFSSecondaryItemsSourceToDelete">The vFSSecondaryItemsSource.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(VFSSecondaryItemSource vFSSecondaryItemsSourceToDelete);

        #endregion

        #region VFS Secondary Items

        /// <summary>
        /// Loads VFSSecondaryItem by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSSecondaryItem</returns>
        VFSSecondaryItem LoadVFSSecondaryItemById(int id);
        
        /// <summary>
        /// Loads a list of VFSSecondaryItems.
        /// </summary>
        /// <returns>List of VFSSecondaryItems.</returns>
        BindingList<VFSSecondaryItem> LoadVFSSecondaryItems(int vfsID, int itemId, int sectionLookupId);
        
        /// <summary>
        /// Saves a vFSSecondaryItem.
        /// </summary>
        /// <param name="vFSSecondaryItemToSave">The vFSSecondaryItem.</param>
        /// <returns>The vFSSecondaryItem.</returns>
        ProcessResult Save(VFSSecondaryItem vFSSecondaryItemToSave);        

        /// <summary>
        /// Deletes a vFSSecondaryItem.
        /// </summary>
        /// <param name="vFSSecondaryItemToDelete">The vFSSecondaryItem.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(VFSSecondaryItem vFSSecondaryItemToDelete);       

        #endregion

        #region VFS

        /// <summary>
        /// Loads Vital Force Sheet by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFS.</returns>
        VFS LoadVFSById(int id);

        /// <summary>
        /// Loads a list of Vital Force Sheets.
        /// </summary>
        /// <returns>List of Vital Force Sheets.</returns>
        BindingList<VFS> LoadVFSs(string name, DateTime? dateTime, int thyroidNumOfIssues, int mercuryNumOfIssues, string emotionalIssues, string notes, int testId, int patientId, LoadingTypeEnum loadingType);

        /// <summary>
        /// Saves a Vital Force Sheet.
        /// </summary>
        /// <param name="vFS">The Vital Force Sheet.</param>
        /// <returns>The vFSSecondaryItemsSource.</returns>
        ProcessResult Save(VFS vFS);

        /// <summary>
        /// Deletes a Vital Force Sheet.
        /// </summary>
        /// <param name="vFS">The Vital Force Sheet.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(VFS vFS);

        #endregion

        #region VFS Item Source

        /// <summary>
        /// Loads VFSItemSource by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSItemSource</returns>
        VFSItemSource LoadVFSItemsSourceById(int id);

        /// <summary>
        /// Loads a list of VFSItemSource.
        /// </summary>
        /// <returns>List of VFSItemSource.</returns>
        BindingList<VFSItemSource> LoadVFSItemsSource(int itemId, int sectionTypeLookupId, int groupLookupId, int genderLookupId, int v1TypeLookupId, int v2TypeLookupId, bool isActive);

        /// <summary>
        /// Saves a VFSItemSource.
        /// </summary>
        /// <param name="vfsItemsSourceToSave">The VFSItemsSourceToSave.</param>
        /// <returns>The VFSItemsSourceToSave.</returns>
        ProcessResult Save(VFSItemSource vfsItemsSourceToSave);

        /// <summary>
        /// Deletes a VFSItemSource.
        /// </summary>
        /// <param name="vfsItemsSourceToDelete">The VFSItemsSourceToDelete.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(VFSItemSource vfsItemsSourceToDelete);

        #endregion

        #region VFSI tem

        /// <summary>
        /// Loads Vital Force Sheet Item by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The VFSItem.</returns>
        VFSItem LoadVFSItemById(int id);

        /// <summary>
        /// Loads a list of Vital Force Sheet Items.
        /// </summary>
        /// <returns>List of Vital Force Sheet Items.</returns>
        BindingList<VFSItem> LoadVFSItems(int vfsId, int itemSourceId, int itemId, string previousV1, string previousV2, string currentV1, string currentV2, bool? isSkipped, string comments);

        /// <summary>
        /// Saves a Vital Force Sheet Item.
        /// </summary>
        /// <param name="vFSItem">The Vital Force Sheet.</param>
        /// <returns>The Result.</returns>
        ProcessResult Save(VFSItem vFSItem);

        /// <summary>
        /// Deletes a Vital Force Sheet Item.
        /// </summary>
        /// <param name="vFSItem">The Vital Force Sheet.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(VFSItem vFSItem);

        #endregion
    }
} 