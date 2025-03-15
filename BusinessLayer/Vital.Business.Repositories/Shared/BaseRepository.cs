using Vital.Business.Repositories.AutoMappers;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;

namespace Vital.Business.Repositories.Shared
{
    public class BaseRepository
    {
        #region Members

        public static DataAccessAdapter DataAccessAdapter
        {
            get
            {
                return new DataAccessAdapter();
            }
        }

        #endregion

        #region Constructors

        public BaseRepository()
        {
            AutoMappersConfig.SetupInstance();
        }

        #endregion

        #region CommonActions

        /// <summary>
        /// Saves the passed entity.
        /// </summary>
        /// <param name="entityToSave">The entity.</param>
        /// <returns>The result.</returns>
        protected ProcessResult SaveEntity(CommonEntityBase entityToSave)
        {
            return CommonRepository.Save(entityToSave);
        }

        /// <summary>
        /// Deletes the passed entity.
        /// </summary>
        /// <param name="entityToDelete">The entity.</param>
        /// <returns>The result.</returns>
        protected ProcessResult DeleteEntity(CommonEntityBase entityToDelete)
        {
            return CommonRepository.Delete(entityToDelete);
        }

        #endregion
    }
}
