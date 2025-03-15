using System;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Properties;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class PropertiesManager : BaseManager
    {
        #region Private Members

        private readonly IPropertyRepository _propertyRepository;

        #endregion

        #region Consructors

        public PropertiesManager()
        {
            _propertyRepository = new PropertyDatabaseRepository();
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Gets Property by id.
        /// </summary>
        /// <param name="filter">The SingleItem Filter</param>
        /// <returns></returns>
        public Property GetPropertyById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _propertyRepository.LoadPropertyById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a single property by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Property GetPropertyByKey(PropertiesEnum key)
        {
            Check.Argument.IsNotNull(() => key);

            try
            {
                var properties = _propertyRepository.LoadProperties(key.ToString());

                return properties.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Loads the properties depends on passed filter.
        /// </summary>
        /// <param name="filter">The Properties Filter.</param>
        /// <returns></returns>
        public BindingList<Property> GetProperties(PropertiesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                var properties =  _propertyRepository.LoadProperties(filter.Key);

                //This Filtering is done here in the manager, we cannot put it in the repository because the linq cannot deal with the not constant conditions on the IQueryable object.
                //In the other hand, if we put it after we convert the IQueryable object to List or any type of IEnumrable this action will broke the logic of the repositories. so here is the best location to fit.
                if (filter.ApplicableTypeIds != null && filter.ApplicableTypeIds.Length > 0)
                {
                    properties = properties.Where(p => filter.ApplicableTypeIds.Where(t => t == p.ApplicableTypeLookup.Id).Count() > 0).ToBindingList();
                }

                return properties;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }


        }

        /// <summary>
        /// Save the passed Property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public ProcessResult SaveProperty(Property property)
        {
            Check.Argument.IsNotNull(() => property);

            try
            {
                return _propertyRepository.Save(property);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Delete the passed Property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public ProcessResult DeleteProperty(Property property)
        {
            Check.Argument.IsNotNull(() => property);

            try
            {
                return _propertyRepository.Delete(property);
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
