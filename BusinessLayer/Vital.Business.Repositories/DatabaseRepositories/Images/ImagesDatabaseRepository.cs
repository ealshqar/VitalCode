using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Images
{
    /// <summary>
    /// This repository is made for migration purposes.
    /// </summary>
    public class ImagesDatabaseRepository : BaseRepository, IImageRepository
    {
        /// <summary>
        /// Saves the passed image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public ProcessResult Save(Image image)
        {
            Check.Argument.IsNotNull(() => image);
            try
            {
                var imageEntity = Mapper.Map<Image, ImageEntity>(image);

                imageEntity.IsNew = imageEntity.Id > 0 ? false : true;

                var processResult = CommonRepository.Save(imageEntity);

                image.Id = imageEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Save the migrated images, used in the unit test only to migrate the images and save it to the db.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Obsolete("This method is used only while migration the old image")]
        public ProcessResult Save(ImageEntity image)
        {
            Check.Argument.IsNotNull(() => image);
            try
            {

                image.IsNew = image.Id > 0 ? false : true;

                var processResult = CommonRepository.Save(image);

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #region Images

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads an image by id.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        public Image LoadImageById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Image.AsQueryable();

                    src = src.Where(c => c.Id == id);

                    var imageEntity = src.FirstOrDefault();

                    var image = new Image();

                    Mapper.Map(imageEntity, image);

                    return image;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of images.
        /// </summary>
        /// <returns>List of images.</returns>
        public BindingList<Image> LoadImages(int? imageId,
                                            int? oldImageBoxWidth,
                                            int? oldImageBoxHeight,
                                            string extension,
                                            string path,
                                            float? size,
                                            string description)
        {
            try
            {
                return LoadImageWorker(imageId, oldImageBoxWidth, oldImageBoxHeight,extension,path,size,description);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads images.
        /// </summary>
        /// <param name="pathEdges">The path edges.</param>
        /// <returns></returns>
        private static BindingList<Image> LoadImageWorker(int? imageId,
                                                          int? oldImageBoxWidth,
                                                          int? oldImageBoxHeight,
                                                          string extension,
                                                          string path,
                                                          float? size,
                                                          string description)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.Image.AsQueryable();

                if (imageId.HasValue && imageId > 0)
                    src = src.Where(c => c.Id == imageId);

                if (oldImageBoxWidth.HasValue && oldImageBoxWidth > 0)
                    src = src.Where(c => c.OldImageBoxWidth == oldImageBoxWidth);

                if (oldImageBoxHeight.HasValue && oldImageBoxHeight > 0)
                    src = src.Where(c => c.OldImageBoxHeight == oldImageBoxHeight);

                if (size.HasValue && size > 0)
                    src = src.Where(c => c.Size == size);

                if (!string.IsNullOrEmpty(extension))
                    src = src.Where(c => c.Extension.ToLower().Contains(extension.ToLower()));

                if (!string.IsNullOrEmpty(path))
                    src = src.Where(c => c.Path.ToLower().Contains(path.ToLower()));

                if (!string.IsNullOrEmpty(description))
                    src = src.Where(c => c.Description.ToLower().Contains(description.ToLower()));

                var imageEntities = src.ToList();

                var imageList = new BindingList<Image>();

                Mapper.Map(imageEntities, imageList);

                return imageList;
            }
        }

        #endregion
    }
}
