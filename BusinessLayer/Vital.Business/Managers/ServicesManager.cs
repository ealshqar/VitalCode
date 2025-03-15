using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.Services;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class ServicesManager : BaseManager
    {
        #region Services

        #region Private Variables

        private readonly IServiceRepository _servicesRepository;

        #endregion

        #region Private Related Managers
        

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public ServicesManager()
        {
            _servicesRepository = new ServiceDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a service by ID.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Service GetServiceById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _servicesRepository.LoadServiceById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a service.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Service GetService(ServicesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _servicesRepository.LoadServiceByKey(filter.Key);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of services.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Service> GetServices(ServicesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _servicesRepository.LoadServices(filter.Key, filter.Name, filter.IsDefault, filter.TypeLookupId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public ProcessResult SaveService(Service service)
        {
            Check.Argument.IsNotNull(() => service);

            if (service.ObjectState == DomainEntityState.Deleted)
            {
                var deletionProcessResult = _servicesRepository.Delete(service);

                return deletionProcessResult;
            }

            if (!service.IsChanged) return ProcessResult.Succeed;

            try
            {
                service.SetUserAndDates();
                return _servicesRepository.Save(service);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the passed services list.
        /// </summary>
        /// <returns></returns>
        public ProcessResult Save(BindingList<Service> serviceList)
        {
            Check.Argument.IsNotNull(() => serviceList);

            try
            {
                var result = new ProcessResult { IsSucceed = false };

                foreach (var service in serviceList)
                {
                    result = SaveService(service);

                    if (result.IsSucceed == false)
                        return result;
                }

                return result;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public ProcessResult DeleteService(Service service)
        {
            Check.Argument.IsNotNull(() => service);

            try
            {
                var  processResult = _servicesRepository.Delete(service);

                if (processResult.IsSucceed) { service.ObjectState = DomainEntityState.Deleted; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion

        #endregion
    }
}