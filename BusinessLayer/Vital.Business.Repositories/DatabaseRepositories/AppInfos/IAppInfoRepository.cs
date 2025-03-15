using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.AppInfos
{
    public interface IAppInfoRepository
    {
        /// <summary>
        /// Load AppInfo by property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The AppInfo</returns>
        AppInfo LoadAppInfoByProperty(string property);

        /// <summary>
        /// Save the passed app info object.
        /// </summary>
        /// <param name="appInfo">The App info to save.</param>
        /// <returns></returns>
        ProcessResult Save(AppInfo appInfo);

    }
}
