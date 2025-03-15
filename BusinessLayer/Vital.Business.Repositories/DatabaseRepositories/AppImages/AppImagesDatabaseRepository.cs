using System;
using System.Linq;
using AutoMapper;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.AppImages;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.AppImages
{
    public class AppImagesDatabaseRepository : BaseRepository , IAppImagesRepository
    {
        #region public Mehtods

        /// <summary>
        /// Load AppImage by property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The AppImage</returns>
        public AppImage LoadAppImageByProperty(string property)
        {
            Check.Argument.IsNotEmpty(property, "property");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var appImageEntity = source.AppImage.FirstOrDefault(f => f.Property == property);

                    if (appImageEntity == null) return null;

                    var appImage = Mapper.Map<AppImageEntity, AppImage>(appImageEntity);

                    return appImage;
                }

            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }


        /// <summary>
        /// Saves AppImage.
        /// </summary>
        /// <param name="appImage">The app image.</param>
        /// <returns>The Process Result.</returns>
        public ProcessResult Save(AppImage appImage)
        {
            Check.Argument.IsNotNull(appImage, "app image to save");

            try
            {
                var appImageEntity = Mapper.Map<AppImage, AppImageEntity>(appImage);

                appImageEntity.IsNew = appImageEntity.Id <= 0;

                var processResult = CommonRepository.Save(appImageEntity);

                appImageEntity.Id = appImageEntity.Id;

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
