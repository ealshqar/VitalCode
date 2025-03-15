using Vital.Business.Shared.DomainObjects.AppImages;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.AppImages
{
    public interface IAppImagesRepository
    {
        /// <summary>
        /// Load AppImage by property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The AppImage</returns>
        AppImage LoadAppImageByProperty(string property);

        /// <summary>
        /// Saves AppImage.
        /// </summary>
        /// <param name="appImage">The app image.</param>
        /// <returns>The Process Result.</returns>
        ProcessResult Save(AppImage appImage);
    }
}
