using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.ShippingOrders
{
    public class ShippingOrderDatabaseRepository : BaseRepository, IShippingOrderRepository
    {
        #region PathEdges

        private readonly Func<IPathEdgeRootParser<ShippingOrderEntity>, IPathEdgeRootParser<ShippingOrderEntity>>
            _pathEdgeShippingOrderEntity =
                p => p.Prefetch(o => o.Test)
                    .Prefetch(m=>m.ShippingMethod)
                    .Prefetch<OrderItemEntity>(oi => oi.OrderItems)
                        .SubPath(pi => pi.Prefetch(pii => pii.Item));

        private readonly Func<IPathEdgeRootParser<ShippingOrderEntity>, IPathEdgeRootParser<ShippingOrderEntity>>
            _pathEdgeShippingOrderEntityLight =
                p => p.Prefetch(o => o.Number);

        private readonly Func<IPathEdgeRootParser<OrderItemEntity>, IPathEdgeRootParser<OrderItemEntity>> _pathEdgesOrderItem =
            p => p.Prefetch(c => c.ShippingOrder)
                  .Prefetch<ItemEntity>(cc => cc.Item);

        #endregion

        #region Shipping Orders

        /// <summary>
        /// Loads a ShippingOrder by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ShippingOrder LoadShippingOrderId(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "Id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var shippingOrderEntity =
                        source.ShippingOrder.WithPath(_pathEdgeShippingOrderEntity).FirstOrDefault(f => f.Id == id);

                    if (shippingOrderEntity == null) return null;

                    var shippingOrder = Mapper.Map<ShippingOrderEntity, ShippingOrder>(shippingOrderEntity);

                    return shippingOrder;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }


        /// <summary>
        /// Loads a list of ShippingOrder.
        /// </summary>
        /// <returns></returns>
        public BindingList<ShippingOrder> LoadShippingOrders(string number, int? testId, int ? patientId, DateTime? sentDate,
                                                             bool? sendToClient, bool? sent,
                                                             string patientFirstName, string patientLastName,
                                                             string patientAddress1, string patientAddress2,
                                                             string patientCity, string patientState, string patientZip,
                                                             string patientHomePhone, string patientWorkPhone,
                                                             string patientCellPhone, string patientFax,
                                                             string patientEmail, string technicianName,
                                                             string technicianAddress, string technicianState,
                                                             string technicianZipCode, string technicianCity,
                                                             string technicianPhone,
                                                             DateTime? creationDateTime, LoadingTypeEnum loadingType)
        {
            try
            {
                return LoadShippingOrdersWorker(number, testId, patientId, sentDate, sendToClient, sent, patientFirstName, patientLastName,
                                          patientAddress1, patientAddress2, patientCity, patientState, patientZip,
                                          patientHomePhone, patientWorkPhone, patientCellPhone, patientFax, patientEmail,
                                          technicianName, technicianAddress, technicianState, technicianZipCode,
                                          technicianCity, technicianPhone, creationDateTime, loadingType,
                                          _pathEdgeShippingOrderEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves a shipping order.
        /// </summary>
        /// <param name="shippingOrder">The shipping order</param>
        /// <returns></returns>
        public  ProcessResult Save(ShippingOrder shippingOrder)
        {
            Check.Argument.IsNotNull(() => shippingOrder);

            try
            {
                var shippingOrderEntity = Mapper.Map<ShippingOrder, ShippingOrderEntity>(shippingOrder);

                shippingOrderEntity.IsNew = shippingOrderEntity.Id <= 0;

                var processResult = CommonRepository.Save(shippingOrderEntity);

                shippingOrder.Id = shippingOrderEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes a shipping order.
        /// </summary>
        /// <param name="shippingOrder">The shipping order</param>
        /// <returns></returns>
        public  ProcessResult Delete(ShippingOrder shippingOrder)
        {
            Check.Argument.IsNotNull(() => shippingOrder);

            try
            {
                var shippingOrderEntity = Mapper.Map<ShippingOrder, ShippingOrderEntity>(shippingOrder);

                return CommonRepository.Delete(shippingOrderEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Order Items

        #region Public Methods

        /// <summary>
        /// Loads OrderItem by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The OrderItem</returns>
        public OrderItem LoadOrderItemById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.OrderItem.Where(c => c.Id == id).WithPath(_pathEdgesOrderItem);

                    var OrderItem = src.FirstOrDefault();

                    var OrderItemObj = new OrderItem();

                    Mapper.Map(OrderItem, OrderItemObj);

                    return OrderItemObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Loads a list of OrderItems.
        /// </summary>
        /// <returns>List of OrderItems.</returns>
        public BindingList<OrderItem> LoadOrderItems(int testOrderId, int itemId)
        {
            try
            {
                return LoadOrderItemsWorker(testOrderId, itemId, _pathEdgesOrderItem);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }
        
        /// <summary>
        /// Saves a OrderItem.
        /// </summary>
        /// <param name="OrderItemToSave">The OrderItem.</param>
        /// <returns>The OrderItem.</returns>
        public ProcessResult Save(OrderItem OrderItemToSave)
        {
            Check.Argument.IsNotNull(OrderItemToSave, "OrderItem to save");

            try
            {
                var OrderItemEntity = Mapper.Map<OrderItem, OrderItemEntity>(OrderItemToSave);

                OrderItemEntity.IsNew = OrderItemEntity.Id <= 0;

                var processResult = CommonRepository.Save(OrderItemEntity);

                OrderItemToSave.Id = OrderItemEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        /// <summary>
        /// Deletes a OrderItem.
        /// </summary>
        /// <param name="OrderItemToDelete">The OrderItem.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(OrderItem OrderItemToDelete)
        {
            Check.Argument.IsNotNull(OrderItemToDelete, "OrderItem to delete");

            try
            {
                var OrderItemEntity = Mapper.Map<OrderItem, OrderItemEntity>(OrderItemToDelete);

                return CommonRepository.Delete(OrderItemEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of OrderItems.
        /// </summary>
        /// <returns></returns>
        private static BindingList<OrderItem> LoadOrderItemsWorker(int ShippingOrderId, int itemId, Func<IPathEdgeRootParser<OrderItemEntity>, IPathEdgeRootParser<OrderItemEntity>> pathEdges)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.OrderItem.WithPath(pathEdges);

                if (ShippingOrderId > 0)
                    src = src.Where(c => c.ShippingOrderId == ShippingOrderId);

                if (itemId > 0)
                    src = src.Where(c => c.ItemId == itemId);

                var OrderItems = src.ToList();

                var OrderItemsObjList = new BindingList<OrderItem>();

                Mapper.Map(OrderItems, OrderItemsObjList);

                return OrderItemsObjList;
            }
        }

        #endregion  

        #endregion

        #region PrivateWorkers

        private BindingList<ShippingOrder> LoadShippingOrdersWorker(string number, int? testId, int ? patientId, DateTime? sentDate,
                                                                    bool? sendToClient, bool? sent,
                                                                    string patientFirstName, string patientLastName,
                                                                    string patientAddress1, string patientAddress2,
                                                                    string patientCity, string patientState,
                                                                    string patientZip, string patientHomePhone,
                                                                    string patientWorkPhone, string patientCellPhone,
                                                                    string patientFax, string patientEmail,
                                                                    string technicianName, string technicianAddress,
                                                                    string technicianState, string technicianZipCode,
                                                                    string technicianCity, string technicianPhone, 
                                                                    DateTime? creationDateTime,
                                                                    LoadingTypeEnum loadingType,
                                                             Func
                                                                 <IPathEdgeRootParser<ShippingOrderEntity>,
                                                                 IPathEdgeRootParser<ShippingOrderEntity>> pathEdge)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);
                
                var src = (loadingType == LoadingTypeEnum.All) ?
                                         source.ShippingOrder.WithPath(pathEdge) : 
                                         source.ShippingOrder;

                if (!string.IsNullOrEmpty(number))
                    src = src.Where(s => s.Number.Equals(number));

                if (patientId.HasValue && patientId > 0)
                    src = src.Where(c => c.Test != null && c.Test.PatientId == patientId.Value);
                else
                {
                    if (testId.HasValue)
                    {
                        src = src.Where(s => s.TestId == testId);
                    }
                    else
                    {
                        src = src.Where(s => s.TestId == null);
                    }
                }

                
                if (sentDate.HasValue)
                    src = src.Where(s => s.SentDate.HasValue && s.SentDate.Value == sentDate.Value);

                if (creationDateTime.HasValue)
                    src = src.Where(s => s.CreationDateTime != null && 
                        s.CreationDateTime.Month == creationDateTime.Value.Month &&
                        s.CreationDateTime.Day == creationDateTime.Value.Day &&
                        s.CreationDateTime.Year == creationDateTime.Value.Year);

                if (sendToClient.HasValue)
                    src = src.Where(s => s.SendToClient == sendToClient.Value);

                if (sent.HasValue)
                    src = src.Where(s => s.Sent == sent.Value);

                if (!string.IsNullOrEmpty(patientFirstName))
                    src = src.Where(s => s.PatientFirstName.Contains(patientFirstName));


                if (!string.IsNullOrEmpty(patientLastName))
                    src = src.Where(s => s.PatientLastName.Contains(patientLastName));


                if (!string.IsNullOrEmpty(patientAddress1))
                    src = src.Where(s => s.PatientAddress1.Contains(patientAddress1));

                if (!string.IsNullOrEmpty(patientAddress2))
                    src = src.Where(s => s.PatientAddress2.Contains(patientAddress2));

                if (!string.IsNullOrEmpty(patientCity))
                    src = src.Where(s => s.PatientCity.Contains(patientCity));

                if (!string.IsNullOrEmpty(patientState))
                    src = src.Where(s => s.PatientState.Contains(patientState));

                if (!string.IsNullOrEmpty(patientZip))
                    src = src.Where(s => s.PatientZip.Contains(patientZip));

                if (!string.IsNullOrEmpty(patientHomePhone))
                    src = src.Where(s => s.PatientHomePhone.Contains(patientHomePhone));

                if (!string.IsNullOrEmpty(patientWorkPhone))
                    src = src.Where(s => s.PatientWorkPhone.Contains(patientWorkPhone));

                if (!string.IsNullOrEmpty(patientCellPhone))
                    src = src.Where(s => s.PatientCellPhone.Contains(patientCellPhone));

                if (!string.IsNullOrEmpty(patientFirstName))
                    src = src.Where(s => s.PatientFax.Contains(patientFax));

                if (!string.IsNullOrEmpty(patientEmail))
                    src = src.Where(s => s.PatientEmail.Contains(patientEmail));

                if (!string.IsNullOrEmpty(technicianName))
                    src = src.Where(s => s.TechnicianName.Contains(technicianName));

                if (!string.IsNullOrEmpty(technicianAddress))
                    src = src.Where(s => s.TechnicianAddress.Contains(technicianAddress));

                if (!string.IsNullOrEmpty(technicianState))
                    src = src.Where(s => s.TechnicianState.Contains(technicianState));

                if (!string.IsNullOrEmpty(technicianZipCode))
                    src = src.Where(s => s.TechnicianZipCode.Contains(technicianZipCode));

                if (!string.IsNullOrEmpty(technicianCity))
                    src = src.Where(s => s.TechnicianCity.Contains(technicianCity));

                if (!string.IsNullOrEmpty(technicianPhone))
                    src = src.Where(s => s.TechnicianPhone.Contains(technicianPhone));

                var shippingOrders = new BindingList<ShippingOrder>();

                Mapper.Map(src.ToList(), shippingOrders);

                return shippingOrders;
            }
        }

        #endregion
    }
}
