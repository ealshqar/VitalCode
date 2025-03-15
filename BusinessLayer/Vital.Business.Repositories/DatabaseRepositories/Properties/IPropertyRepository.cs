using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Properties
{
    public interface IPropertyRepository
    {
        /// <summary>
        /// Loads the property by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Property LoadPropertyById(int id);

        /// <summary>
        /// Loads the Properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        BindingList<Property> LoadProperties(string key);

        /// <summary>
        /// Saves the passed Property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        ProcessResult Save(Property property);

        /// <summary>
        /// Delete the passed property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        ProcessResult Delete(Property property);
    } 
}
