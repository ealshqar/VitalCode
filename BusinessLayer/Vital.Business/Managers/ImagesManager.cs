using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.Images;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class ImagesManager : BaseManager
    {
        #region Private Variables

        private readonly IImageRepository _imagesRepository;

        public ImagesManager()
        {
            _imagesRepository = new ImagesDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public ProcessResult Save(Image image)
        {
            Check.Argument.IsNotNull(() => image);

            if (!image.IsChanged) return ProcessResult.Succeed;

            try
            {
                image.SetUserAndDates();

                var processResult = _imagesRepository.Save(image);

                if (processResult.IsSucceed) { image.ObjectState = DomainEntityState.Unchanged; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Loads image by id.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Image GetImageById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "image id.");

            try
            {
                return _imagesRepository.LoadImageById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of patient history.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Image> GetImages(ImagesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _imagesRepository.LoadImages(filter.ImageId,
                                                    filter.OldImageBoxWidth,
                                                    filter.OldImageBoxHeight,
                                                    filter.Extension,
                                                    filter.Path,
                                                    filter.Size,
                                                    filter.Description);
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
