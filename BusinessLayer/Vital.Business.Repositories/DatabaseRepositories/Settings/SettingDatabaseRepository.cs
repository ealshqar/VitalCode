using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Settings
{
    public class SettingDatabaseRepository : BaseRepository, ISettingRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<SettingEntity>, IPathEdgeRootParser<SettingEntity>> _pathEdgesSetting = 
            p => p.Prefetch(s => s.SettingGroupLookup)
                  .Prefetch(s => s.ValueTypeLookup);

        #endregion

        #region Setting
        
        #region Public Methods

                /// <summary>
                /// Loads setting by id.
                /// </summary>
                /// <param name="key">The key.</param>
                /// <returns>The setting.</returns>
                public Setting LoadSettingByKey(string key)
                {
                    Check.Argument.IsNotEmpty(key, "key");

                    try
                    {
                        using (var adapter = new DataAccessAdapter())
                        {
                            var data = new LinqMetaData(adapter);

                            var src = data.Setting.Where(c => c.Key == key).WithPath(_pathEdgesSetting);

                            var setting = src.FirstOrDefault();

                            var settingObj = new Setting();

                            Mapper.Map(setting, settingObj);

                            return settingObj;
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new VitalDatabaseException(exception);
                    }

                }

                /// <summary>
                /// Loads a list of settings.
                /// </summary>
                /// <returns>List of settings.</returns>
                public BindingList<Setting> LoadSettings(string name, string description, int settingTypeLookupId, int settingGroupLookupId, string caption, bool? isVisible)
                {
                    try
                    {
                        return LoadSettingsWorker(name, description, settingTypeLookupId, settingGroupLookupId, caption, isVisible, _pathEdgesSetting);
                    }
                    catch (Exception exception)
                    {
                        throw new VitalDatabaseException(exception);
                    }

                }


                /// <summary>
                /// Saves a setting.
                /// </summary>
                /// <param name="settingToSave">The setting.</param>
                /// <returns>The result.</returns>
                public ProcessResult Save(Setting settingToSave)
                {
                    Check.Argument.IsNotNull(settingToSave, "setting to save");

                    try
                    {
                        var settingEntity = Mapper.Map<Setting, SettingEntity>(settingToSave);

                        settingEntity.IsNew = settingEntity.Id <= 0;

                        var processResult = CommonRepository.Save(settingEntity);

                        settingToSave.Id = settingEntity.Id;

                        return processResult;
                    }
                    catch (Exception exception)
                    {
                        throw new VitalDatabaseException(exception);
                    }

                }

                /// <summary>
                /// Deletes a setting.
                /// </summary>
                /// <param name="settingToDelete">The setting.</param>
                /// <returns>The result.</returns>
                public ProcessResult Delete(Setting settingToDelete)
                {
                    Check.Argument.IsNotNull(settingToDelete, "setting to delete");

                    try
                    {
                        var settingEntity = Mapper.Map<Setting, SettingEntity>(settingToDelete);

                        return CommonRepository.Delete(settingEntity);
                    }
                    catch (Exception exception)
                    {
                        throw new VitalDatabaseException(exception);
                    }

                }


                #endregion

        #region Private Methods

        /// <summary>
        /// Load settings private worker depend on the passed filters.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="description">The Description.</param>
        /// <param name="settingTypeLookupId">The Setting Type Lookup Id.</param>
        /// <param name="settingGroupLookupId">The Setting Group type lookup Id.</param>
        /// <param name="caption">The Caption.</param>
        /// <param name="isVisible">Load visible settings. </param>
        /// <param name="pathEdges">The Path Edges.</param>
        /// <returns></returns>
        private static BindingList<Setting> LoadSettingsWorker(string name, string description, int settingTypeLookupId, int settingGroupLookupId, string caption, bool? isVisible, Func<IPathEdgeRootParser<SettingEntity>, IPathEdgeRootParser<SettingEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.Setting.WithPath(pathEdges);

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(s => s.Name.Equals(name));

                if (!string.IsNullOrEmpty(description))
                    src = src.Where(s => s.Value.ToLower().Contains(description. ToLower()));

                if (settingTypeLookupId > 0)
                    src = src.Where(s => s.ValueTypeLookupId == settingTypeLookupId);

                if (settingGroupLookupId > 0)
                    src = src.Where(s => s.SettingGroupLookupId == settingGroupLookupId);

                if (settingGroupLookupId > 0)
                    src = src.Where(s => s.SettingGroupLookupId == settingGroupLookupId);

                if (!string.IsNullOrEmpty(caption))
                    src = src.Where(s => s.Caption.Equals(caption));

                if(isVisible.HasValue)
                    src = src.Where(s => s.IsVisible.Value == isVisible.Value);

                var settings = src.ToList();

                var settingsObjList = new BindingList<Setting>();

                Mapper.Map(settings, settingsObjList);

                return settingsObjList;
            }
        }

        #endregion

        #endregion

        #region HWProfile

        #region PublicMethods

        /// <summary>
        /// Loads HwProfile by id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The HwProfile.</returns>
        public HwProfile LoadHwProfileById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.HwProfile.Where(c => c.Id == id);

                    var hwProfile = src.FirstOrDefault();

                    var hwProfileObj = new HwProfile();

                    Mapper.Map(hwProfile, hwProfileObj);

                    return hwProfileObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of HwProfiles.
        /// </summary>
        /// <returns>List of HwProfiles.</returns>
        public BindingList<HwProfile> LoadHwProfiles(string name, string key, int minReading, int disconnectedTimeout, int stabilityTimeout, int stabilityRange, bool? isSystemProfile, bool? isDefault)
        {
            try
            {
                return LoadHwProfilesWorker(name, key, minReading, disconnectedTimeout, stabilityTimeout, stabilityRange, isSystemProfile, isDefault);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a HwProfile.
        /// </summary>
        /// <param name="hwProfileToSave">The HwProfile.</param>
        /// <returns>The result.</returns>
        public ProcessResult Save(HwProfile hwProfileToSave)
        {
            Check.Argument.IsNotNull(hwProfileToSave, "hwProfileToSave to save");

            try
            {
                var hwProfileEntity = Mapper.Map<HwProfile, HwProfileEntity>(hwProfileToSave);

                hwProfileEntity.IsNew = hwProfileEntity.Id <= 0;

                var processResult = CommonRepository.Save(hwProfileEntity);

                hwProfileToSave.Id = hwProfileEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a HwProfile.
        /// </summary>
        /// <param name="hwProfileToSave">The HwProfile.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(HwProfile hwProfileToSave)
        {
            Check.Argument.IsNotNull(hwProfileToSave, "hwProfileToSave to delete");

            try
            {
                var hwProfileEntity = Mapper.Map<HwProfile, HwProfileEntity>(hwProfileToSave);

                return CommonRepository.Delete(hwProfileEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Load HwProfiles private worker depend on the passed filters.
        /// </summary>
        /// <returns></returns>
        private static BindingList<HwProfile> LoadHwProfilesWorker(string name, string key, int minReading, int disconnectedTimeout, int stabilityTimeout, int stabilityRange, bool? isSystemProfile, bool? isDefault)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.HwProfile.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(s => s.Name.Equals(name));

                if (!string.IsNullOrEmpty(key))
                    src = src.Where(s => s.Key.Equals(key));

                if (minReading > 0)
                    src = src.Where(s => s.MinReading == minReading);

                if (disconnectedTimeout > 0)
                    src = src.Where(s => s.DisconnectedTimeout == disconnectedTimeout);

                if (stabilityTimeout > 0)
                    src = src.Where(s => s.StabilityTimeout == stabilityTimeout);

                if (stabilityRange > 0)
                    src = src.Where(s => s.StabilityRange == stabilityRange);

                if (isSystemProfile.HasValue)
                    src = src.Where(s => s.IsSystemProfile == isSystemProfile.Value);

                if (isDefault.HasValue)
                    src = src.Where(s => s.IsDefault == isDefault.Value);

                var hwProfile = src.ToList();

                var hwProfilesObjList = new BindingList<HwProfile>();

                Mapper.Map(hwProfile, hwProfilesObjList);

                return hwProfilesObjList;
            }
        }

        #endregion

        #endregion
        
    }
}
