using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Services
{
    public class ServiceDatabaseRepository : BaseRepository, IServiceRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<ServiceEntity>, IPathEdgeRootParser<ServiceEntity>> _pathEdgesService
            =
            s => s.Prefetch(cc => cc.TypeLookup);

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads service by Key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The setting.</returns>
        public Service LoadServiceByKey(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Service.Where(c => c.Key == key).WithPath(_pathEdgesService);

                    var service = src.FirstOrDefault();

                    var serviceObj = new Service();

                    Mapper.Map(service, serviceObj);

                    return serviceObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads Service by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Service</returns>
        public Service LoadServiceById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Service.Where(c => c.Id == id).WithPath(_pathEdgesService);

                    var service = src.FirstOrDefault();

                    var serviceObj = new Service();

                    Mapper.Map(service, serviceObj);

                    return serviceObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of Services.
        /// </summary>
        /// <returns>List of Services.</returns>
        public BindingList<Service> LoadServices(string key, string name, bool? isDefault, int typeLookupId)
        {
            try
            {
                return LoadServicesWorker(key, name, isDefault, typeLookupId, _pathEdgesService);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        /// <summary>
        /// Saves a service.
        /// </summary>
        /// <param name="serviceToSave">The service.</param>
        /// <returns>The service.</returns>
        public ProcessResult Save(Service serviceToSave)
        {
            Check.Argument.IsNotNull(serviceToSave, "service to save");

            try
            {
                var serviceEntity = Mapper.Map<Service, ServiceEntity>(serviceToSave);

                serviceEntity.IsNew = serviceEntity.Id <= 0;

                var processResult = CommonRepository.Save(serviceEntity);

                serviceToSave.Id = serviceEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a service.
        /// </summary>
        /// <param name="serviceToDelete">The service.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(Service serviceToDelete)
        {
            Check.Argument.IsNotNull(serviceToDelete, "service to delete");

            try
            {
                var serviceEntity = Mapper.Map<Service, ServiceEntity>(serviceToDelete);

                return CommonRepository.Delete(serviceEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }  

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of Services.
        /// </summary>
        /// <returns></returns>
        private static BindingList<Service> LoadServicesWorker(string key, string name, bool? isDefault,
                                                               int typeLookupId,
                                                               Func
                                                                   <IPathEdgeRootParser<ServiceEntity>,
                                                                   IPathEdgeRootParser<ServiceEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.Service.WithPath(pathEdges);

                if (!string.IsNullOrEmpty(key))
                    src = src.Where(s => s.Key.Equals(key));

                if (!string.IsNullOrEmpty(name))
                    src = src.Where(s => s.Name.Equals(name));

                if (isDefault.HasValue)
                    src = src.Where(s => s.IsDefault.Value == isDefault.Value);

                if (typeLookupId > 0)
                    src = src.Where(s => s.TypeLookupId == typeLookupId);

                var services = src.ToList();

                var servicesObjList = new BindingList<Service>();

                Mapper.Map(services, servicesObjList);

                return servicesObjList;
            }
        }

        #endregion
    }
}