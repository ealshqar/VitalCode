using System;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Settings;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class SettingsManager : BaseManager
    {
        #region Private Variables

        private readonly ISettingRepository _settingRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public SettingsManager()
        {
            _settingRepository = new SettingDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets Setting depends on the passed key inside the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Setting.</returns>
        public Setting GetSetting(SettingsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _settingRepository.LoadSettingByKey(filter.Key);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets the settings depends on the passed filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Setting> GetSettings(SettingsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _settingRepository.LoadSettings(filter.Name, filter.Description,
                                                       filter.SettingTypeLookupId, filter.SettingGroupLookupId,
                                                       filter.Caption, filter.IsVisible);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed Setting.
        /// </summary>
        /// <param name="setting">The Setting.</param>
        /// <returns></returns>
        public ProcessResult Save(Setting setting)
        {
            Check.Argument.IsNotNull(() => setting);

            if (!setting.IsChanged) return ProcessResult.Succeed;

            try
            {
                return _settingRepository.Save(setting);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed Setting list.
        /// </summary>
        /// <param name="settingList">The Setting.</param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<Setting> settingList)
        {
            Check.Argument.IsNotNull(() => settingList);

            try
            {
                var result = new ProcessResult {IsSucceed = false};

                foreach (var setting in settingList)
                {
                    result = _settingRepository.Save(setting);

                    if (result.IsSucceed == false)
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

        #endregion

        #region HwProfiles

        #region Public Methods

        /// <summary>
        /// Gets HwProfile depends on the passed key inside the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>HwProfile.</returns>
        public HwProfile GetHwProfileById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _settingRepository.LoadHwProfileById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets the HwProfiles depends on the passed filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<HwProfile> GetHwProfiles(HwProfilesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _settingRepository.LoadHwProfiles(filter.Name, filter.Key, filter.MinReading,
                    filter.DisconnectedTimeout, filter.StabilityTimeout,
                    filter.StabilityRange,
                    filter.IsSystemProfile, filter.IsDefault);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed HwProfile.
        /// </summary>
        /// <param name="hwProfile">The hwProfile.</param>
        /// <returns></returns>
        public ProcessResult Save(HwProfile hwProfile)
        {
            Check.Argument.IsNotNull(() => hwProfile);

            if (!hwProfile.IsChanged) return ProcessResult.Succeed;

            try
            {
                hwProfile.SetUserAndDates();
                return _settingRepository.Save(hwProfile);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed hwProfiles list.
        /// </summary>
        /// <param name="hwProfiles">The hwProfiles.</param>
        /// <returns></returns>
        public ProcessResult Save(BindingList<HwProfile> hwProfiles)
        {
            Check.Argument.IsNotNull(() => hwProfiles);

            try
            {
                var result = new ProcessResult { IsSucceed = false };

                foreach (var hwProfile in hwProfiles)
                {
                    result = hwProfile.ObjectState == DomainEntityState.Deleted ? Delete(hwProfile) : Save(hwProfile);

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
        /// Deletes the passed HwProfile.
        /// </summary>
        /// <param name="hwProfile">The hwProfile.</param>
        /// <returns></returns>
        public ProcessResult Delete(HwProfile hwProfile)
        {
            Check.Argument.IsNotNull(() => hwProfile);

            try
            {
                return _settingRepository.Delete(hwProfile);
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

        #region Specific Setting Methods

        /// <summary>
        /// Gets a list of points.
        /// </summary>
        /// <returns></returns>
        public BindingList<Item> GetPoints()
        {
            var lookupsManager = new LookupsManager();
            var itemManager = new ItemsManager();

            var listTypeLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.UserList));
            var itemTypeLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Point));
            var targetTypeLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.TargetType, TargetType.MyPointsList));

            return itemManager.GetItems(new ItemsFilter
            {
                TargetTypeLookupId = targetTypeLookup.Count > 0 ? targetTypeLookup.FirstOrDefault().Id : 0,
                TypeLookupId = itemTypeLookup.Count > 0 ? itemTypeLookup.FirstOrDefault().Id : 0,
                ListTypeLookupId = listTypeLookup.Count > 0 ? listTypeLookup.FirstOrDefault().Id : 0,
            });
        }

        /// <summary>
        /// Gets a list of potencies.
        /// </summary>
        /// <returns></returns>
        public BindingList<Item> GetPotencies()
        {
            var lookupsManager = new LookupsManager();
            var itemManager = new ItemsManager();

            var listTypeLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ListType, ListTypeEnum.SystemList));
            var itemTypeLookup = lookupsManager.GetLookups(LookupsFilter.As(LookupTypes.ItemType, ItemTypeEnum.Potency));
            
            return itemManager.GetItems(new ItemsFilter
            {
                TypeLookupId = itemTypeLookup.Count > 0 ? itemTypeLookup.FirstOrDefault().Id : 0,
                ListTypeLookupId = listTypeLookup.Count > 0 ? listTypeLookup.FirstOrDefault().Id : 0,
            });
        }

        /// <summary>
        /// Gets a list of available ports.
        /// </summary>
        /// <returns></returns>
        public BindingList<ComPortInfo> GetComPorts()
        {
            return CsaEmdUnitManager.Instance.GetComPorts();
        }

        /// <summary>
        /// Gets a list of available ports for prototype.
        /// </summary>
        /// <returns></returns>
        public BindingList<ComPortInfo> GetPrototypeComPorts()
        {
            return AutoCsaEmdUnitManager.Instance.GetComPorts(false);
        }
        
        #endregion
    }
}
