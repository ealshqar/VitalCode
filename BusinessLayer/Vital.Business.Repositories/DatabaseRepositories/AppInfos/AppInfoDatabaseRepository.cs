using System;
using System.Linq;
using AutoMapper;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.AppInfos
{
    public class AppInfoDatabaseRepository : BaseRepository, IAppInfoRepository
    {
        #region public Mehtods

        /// <summary>
        /// Load AppInfo by property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The AppInfo</returns>
        public AppInfo LoadAppInfoByProperty(string property)
        {
            Check.Argument.IsNotEmpty(property, "property");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var appInfoEntity = source.AppInfo.Where(f => f.Property == property).FirstOrDefault();

                    if (appInfoEntity == null) return null;

                    var appInfo = Mapper.Map<AppInfoEntity, AppInfo>(appInfoEntity);

                    return appInfo;
                }

            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Save the passed app info object.
        /// </summary>
        /// <param name="appInfo">The App info to save.</param>
        /// <returns></returns>
        public ProcessResult Save(AppInfo appInfo)
        {
            Check.Argument.IsNotNull(() =>appInfo);

            try
            {
                var appInfoEntity = Mapper.Map<AppInfo, AppInfoEntity>(appInfo);

                if (appInfoEntity.Id <= 0)
                    return ProcessResult.Failed;

                appInfoEntity.IsNew = false;

                var processResult = CommonRepository.Save(appInfoEntity);

                appInfo.Id = appInfoEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion
    }
}