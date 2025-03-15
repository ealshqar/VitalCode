using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Properties
{
    public class PropertyDatabaseRepository : BaseRepository, IPropertyRepository
    {
        #region Path Edges

        private readonly Func<IPathEdgeRootParser<PropertyEntity>, IPathEdgeRootParser<PropertyEntity>>
            _pathEdgeProperty =
                p => p.Prefetch(pr => pr.ApplicableTypeLookup)
                         .Prefetch(pr => pr.ValueTypeLookup);

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the property by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Property LoadPropertyById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "Id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var properteEntity = source.Property.WithPath(_pathEdgeProperty).FirstOrDefault(p => p.Id == id);

                    if (properteEntity == null) return null;

                    var property = Mapper.Map<PropertyEntity, Property>(properteEntity);

                    return property;
                }
            }
            catch (Exception ex)
            {
                throw new VitalDatabaseException(ex);
            }
        }

        /// <summary>
        /// Loads the Properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public BindingList<Property> LoadProperties(string key)
        {
            try
            {
                return LoadPropertiesWorker(key, _pathEdgeProperty);
            }
            catch (Exception ex)
            {
                throw new VitalDatabaseException(ex);
            }
        }

        /// <summary>
        /// Saves the passed Property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public ProcessResult Save(Property property)
        {
             Check.Argument.IsNotNull(() => property);

            try
            {
                var propertyEntity = Mapper.Map<Property, PropertyEntity>(property);

                propertyEntity.IsNew = propertyEntity.Id <= 0;

                var result = CommonRepository.Save(propertyEntity);

                property.Id = propertyEntity.Id;

                return result;
            }
            catch (Exception ex)
            {
                throw new VitalDatabaseException(ex);
            }
        }

        /// <summary>
        /// Delete the passed property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public ProcessResult Delete(Property property)
        {
            Check.Argument.IsNotNull(() => property);

            try
            {
                var propertyEntity =
                    Mapper.Map<Property, PropertyEntity>(property);

                return CommonRepository.Delete(propertyEntity);
            }
            catch (Exception ex)
            {
                throw new VitalDatabaseException(ex);
            }
        }

        #endregion

        #region Private Workers

        /// <summary>
        /// Load properties worker.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="pathEdgeProperty">The pathEdge</param>
        /// <returns></returns>
        private BindingList<Property> LoadPropertiesWorker(string key, Func<IPathEdgeRootParser<PropertyEntity>, IPathEdgeRootParser<PropertyEntity>> pathEdgeProperty)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var propertiesEntities = source.Property.WithPath(pathEdgeProperty);

                if (!string.IsNullOrEmpty(key))
                {
                    propertiesEntities = propertiesEntities.Where(p => p.Key.ToLower().Equals(key));
                }

                var properties = new BindingList<Property>();

                Mapper.Map(propertiesEntities, properties);

                return properties;
            }
        }

        #endregion
        
    }
}
