using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Images
{
    public interface IImageRepository
    {
        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The result</returns>
        ProcessResult Save(Image image);

        /// <summary>
        /// Loads Image by filter.
        /// </summary>
        /// <returns>The Image</returns>
        Image LoadImageById(int id);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<Image> LoadImages(int? imageId,
                                            int? oldImageBoxWidth,
                                            int? oldImageBoxHeight,
                                            string extension,
                                            string path,
                                            float? size,
                                            string description);

    }
}
