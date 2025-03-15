using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;

namespace Vital.Business.Repositories.Shared
{
    public class CommonRepository
    {
        
        /// <summary>
        /// Saves the passed entity.
        /// </summary>
        /// <param name="entityToSave">The entity.</param>
        /// <returns>The result.</returns>
        public static ProcessResult Save(CommonEntityBase entityToSave)
        {
            Check.Argument.IsNotNull(() => entityToSave);
            
            using (var adapter = new DataAccessAdapter())
            {
                var result = adapter.SaveEntity(entityToSave, true, false);

                return new ProcessResult()
                           {
                               IsSucceed = result,
                               Message =
                                   result == false
                                       ? string.Format("{0}", "The entity was not saved.")
                                       : string.Empty
                           };
            }
        }


        /// <summary>
        /// Deletes the passed entity.
        /// </summary>
        /// <param name="entityToDelete">The entity.</param>
        /// <returns>The result.</returns>
        public static ProcessResult Delete(CommonEntityBase entityToDelete)
        {
            Check.Argument.IsNotNull(() => entityToDelete);

            using (var adapter = new DataAccessAdapter())
            {

                var result = adapter.DeleteEntity(entityToDelete);

                return new ProcessResult()
                           {
                               IsSucceed = result,
                               Message =
                                   result == false ? string.Format("{0}", "The entity was not deleted.") : string.Empty
                           };
            }
        }



    }
}
