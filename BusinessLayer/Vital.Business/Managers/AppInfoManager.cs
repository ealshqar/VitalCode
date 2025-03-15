using System;
using Vital.Business.Repositories.DatabaseRepositories.AppInfos;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class AppInfoManager
    {
        #region Private Variables
        
        private readonly IAppInfoRepository _appInfosRepository;
        
        #endregion

        #region Constructors

        /// <summary>
        /// AppInfoManager Constructor.
        /// </summary>
        public AppInfoManager()
        {
            _appInfosRepository = new AppInfoDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the value of an appinfo property
        /// </summary>
        /// <param name="appInfoKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ProcessResult SetAppInfoValueByKey(AppInfoKeys appInfoKey, string value)
        {
            var result = ProcessResult.Failed;

            var appInfoProperty = GetAppInfoByPropertyKey(appInfoKey);

            if (appInfoProperty != null)
            {
                appInfoProperty.Value = value;
                result = Save(appInfoProperty);
            }

            return result;
        }

        /// <summary>
        /// Gets the AppInfo By property.
        /// </summary>
        /// <returns>The app info.</returns>
        public String GetAppInfoValueByProperty(AppInfoKeys key)
        {
            return GetAppInfoByProperty(new AppInfoFilter() { Property = key.ToString() }).Value;
        }

        /// <summary>
        /// Gets the AppInfo By key.
        /// </summary>
        /// <param name="appInfoKey">The key.</param>
        /// <returns>The app info.</returns>
        public AppInfo GetAppInfoByPropertyKey(AppInfoKeys appInfoKey)
        {
            return GetAppInfoByProperty(new AppInfoFilter() { Property = appInfoKey.ToString()});
        }

        /// <summary>
        /// Gets the AppInfo By property.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The app info.</returns>
        public AppInfo GetAppInfoByProperty(AppInfoFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _appInfosRepository.LoadAppInfoByProperty(filter.Property);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
            
        }

        /// <summary>
        /// Saves the passed appInfo object.
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public ProcessResult Save(AppInfo appInfo)
        {
            Check.Argument.IsNotNull(() => appInfo);

            try
            {
                return _appInfosRepository.Save(appInfo);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion
    }
}
