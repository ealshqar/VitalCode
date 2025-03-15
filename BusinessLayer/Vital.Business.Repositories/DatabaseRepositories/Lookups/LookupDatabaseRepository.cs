using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Lookups
{
    public class LookupDatabaseRepository : BaseRepository,ILookupRepository
    {
        #region Path Edges

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the lookup by Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The Lookup.</returns>
        public Lookup LoadLockupById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "Id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var lookupEntity = source.Lookup.FirstOrDefault(l => l.Id == id);

                    if (lookupEntity == null) return null;

                    var item = Mapper.Map<LookupEntity, Lookup>(lookupEntity);

                    return item;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads lookups set By lookup Type.
        /// </summary>
        /// <param name="lookupType">The Lookup Type : Can be null to load all.</param>
        /// <param name="lookupValue">The Lookup Value : Can be null to load all.</param>
        /// <returns></returns>
        public BindingList<Lookup> LoadLookups(string lookupType , string lookupValue)
        {
            try
            {
                return LoadLookupsWorker(lookupType, lookupValue);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }


        #endregion

        #region Private Workers

        /// <summary>
        /// Loads the lookups depend on the passed predicate.
        /// </summary>
        /// <returns>List of lookups.</returns>
        private static BindingList<Lookup> LoadLookupsWorker(string lookupType, string lookupValue)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var src = source.Lookup.AsQueryable();

                if (!string.IsNullOrEmpty(lookupType))
                    src = src.Where(c => c.Type == lookupType);

                if (!string.IsNullOrEmpty(lookupValue))
                    src = src.Where(c => c.Value == lookupValue);

                var lookups = new BindingList<Lookup>();

                Mapper.Map(src.ToList(), lookups);

                return lookups;
            }
        }

        #endregion
        
    }
}
