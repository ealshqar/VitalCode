using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vital.Business.Repositories.DatabaseRepositories.AppImages;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.AppImages;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class AppImagesManager : BaseManager
    {
         #region Private Variables
        
        private readonly IAppImagesRepository _appImagesRepository;
        
        #endregion

        #region Constructors

        /// <summary>
        /// AppInfoManager Constructor.
        /// </summary>
        public AppImagesManager()
        {
            _appImagesRepository = new AppImagesDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the AppInfo By property.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The app info.</returns>
        public AppImage GetAppImageByProperty(AppImageFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _appImagesRepository.LoadAppImageByProperty(filter.Property);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
            
        }

        /// <summary>
        /// Gets the AppInfo By property.
        /// </summary>
        /// <param name="appImage">The AppImage.</param>
        /// <returns>The app info.</returns>
        public ProcessResult SaveAppImage(AppImage appImage)
        {
            Check.Argument.IsNotNull(() => appImage);

            try
            {
                return _appImagesRepository.Save(appImage);
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
