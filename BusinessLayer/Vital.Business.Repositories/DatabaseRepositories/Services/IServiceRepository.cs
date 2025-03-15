using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Services
{
    public interface IServiceRepository
    {
        /// <summary>
        /// Loads service by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Service</returns>
        Service LoadServiceById(int id);

        /// <summary>
        /// Loads service by Key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The setting.</returns>
        Service LoadServiceByKey(string key);
        
        /// <summary>
        /// Loads a list of Services.
        /// </summary>
        /// <returns>List of Services.</returns>
        BindingList<Service> LoadServices(string key, string name, bool? isDefault, int typeLookupId);
        
        /// <summary>
        /// Saves a service.
        /// </summary>
        /// <param name="serviceToSave">The service.</param>
        /// <returns>The service.</returns>
        ProcessResult Save(Service serviceToSave);        

        /// <summary>
        /// Deletes a service.
        /// </summary>
        /// <param name="serviceToDelete">The service.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Service serviceToDelete);       
    }
} 