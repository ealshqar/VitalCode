using System.Collections.Generic;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Lookups;

namespace Vital.Business.Repositories.DatabaseRepositories.Lookups
{
    public interface ILookupRepository
    {
        /// <summary>
        /// Loads the lookup by Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The Lookup.</returns>
        Lookup LoadLockupById(int id);

        /// <summary>
        /// Loads lookups set By lookup Type.
        /// </summary>
        /// <param name="lookupType">The Lookup Type : Can be null to load all.</param>
        /// <param name="lookupValue">The Lookup Value : Can be null to load all.</param>
        /// <returns>List of Lookups.</returns>
        BindingList<Lookup> LoadLookups(string lookupType, string lookupValue);



    }
}
