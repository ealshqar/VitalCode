using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Settings
{
    public interface ISettingRepository
    {
        #region Setting

        /// <summary>
        /// Loads setting by id.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The setting.</returns>
        Setting LoadSettingByKey(string key);

        /// <summary>
        /// Loads a list of settings.
        /// </summary>
        /// <returns>List of settings.</returns>
        BindingList<Setting> LoadSettings(string name, string description, int settingTypeLookupId, int settingGroupLookupId, string caption, bool? isVisible);
        /// <summary>
        /// Saves a setting.
        /// </summary>
        /// <param name="settingToSave">The setting.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(Setting settingToSave);

        /// <summary>
        /// Deletes a setting.
        /// </summary>
        /// <param name="settingToDelete">The setting.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Setting settingToDelete);

        #endregion

        #region HWProfile

        /// <summary>
        /// Loads HwProfile by id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The HwProfile.</returns>
        HwProfile LoadHwProfileById(int id);

        /// <summary>
        /// Loads a list of HwProfiles.
        /// </summary>
        /// <returns>List of HwProfiles.</returns>
        BindingList<HwProfile> LoadHwProfiles(string name, string key, int minReading, int disconnectedTimeout, int stabilityTimeout, int stabilityRange, bool? isSystemProfile, bool? isDefault);

        /// <summary>
        /// Saves a HwProfile.
        /// </summary>
        /// <param name="hwProfileToSave">The HwProfile.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(HwProfile hwProfileToSave);

        /// <summary>
        /// Deletes a HwProfile.
        /// </summary>
        /// <param name="hwProfileToSave">The HwProfile.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(HwProfile hwProfileToSave);

        #endregion
        
    }
}
