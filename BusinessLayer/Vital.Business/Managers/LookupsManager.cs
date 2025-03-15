using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.Lookups;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class LookupsManager : BaseManager
    {
        #region Private Variables

        private readonly LookupDatabaseRepository _lookupDatabaseRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        public LookupsManager()
        {
            _lookupDatabaseRepository = new LookupDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets lookup by Id.
        /// </summary>
        public Lookup GetLookupById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _lookupDatabaseRepository.LoadLockupById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets lookups depends on the passed filter.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        /// <returns>List of lookups.</returns>
        public BindingList<Lookup> GetLookups(LookupsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _lookupDatabaseRepository.LoadLookups(filter.Type , filter.Value);
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
