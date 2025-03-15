using System;
using System.ComponentModel;
using Vital.Business.Repositories.DatabaseRepositories.ShippingOrders;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class ShippingOrdersManager
    {

        #region Private Variables

        private readonly IShippingOrderRepository _shippingOrderRepository;

        #endregion

        #region Private Related Managers
        

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShippingOrdersManager()
        {
            _shippingOrderRepository = new ShippingOrderDatabaseRepository();
        }

        #endregion

        #region Public Methods

        #region Shipping Orders

        /// <summary>
        /// Gets a ShippingOrder by id.
        /// </summary>
        /// <param name="filter">The Filter</param>
        /// <returns></returns>
        public ShippingOrder GetShippingOrderId(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            
            try
            {
                return _shippingOrderRepository.LoadShippingOrderId(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of ShippingOrder.
        /// </summary>
        /// <returns></returns>
        public BindingList<ShippingOrder> GetShippingOrders(ShippingOrdersFilter filter)
        {
            try
            {
                return _shippingOrderRepository.LoadShippingOrders(filter.Number, filter.TestId, filter.PatientId, filter.SentDate, filter.SendToClient, filter.Sent, filter.PatientFirstName, filter.PatientLastName,
                                          filter.PatientAddress1, filter.PatientAddress2, filter.PatientCity, filter.PatientState, filter.PatientZip,
                                          filter.PatientHomePhone, filter.PatientWorkPhone, filter.PatientCellPhone, filter.PatientFax, filter.PatientEmail,
                                          filter.TechnicianName, filter.TechnicianAddress, filter.TechnicianState, filter.TechnicianZipCode,
                                          filter.TechnicianCity, filter.TechnicianPhone, filter.CreationDateTime, filter.LoadingType);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves a shipping order.
        /// </summary>
        /// <param name="shippingOrder">The shipping order</param>
        /// <returns></returns>
        public ProcessResult Save(ShippingOrder shippingOrder)
        {
            Check.Argument.IsNotNull(() => shippingOrder);

            try
            {
                if (!shippingOrder.IsChanged)
                    return ProcessResult.Succeed;

                shippingOrder.SetUserAndDates();

                var processResult = _shippingOrderRepository.Save(shippingOrder);

                if (!processResult.IsSucceed) return processResult;

                //Save the order items.
                if (shippingOrder.OrderItems != null)
                {
                    processResult = SaveOrderItems(shippingOrder.OrderItems);
                    
                    if (!processResult.IsSucceed) return processResult;
                }

                shippingOrder.ObjectState = DomainEntityState.Unchanged;

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a shipping order.
        /// </summary>
        /// <param name="shippingOrder">The shipping order</param>
        /// <returns></returns>
        public ProcessResult Delete(ShippingOrder shippingOrder)
        {
            Check.Argument.IsNotNull(() => shippingOrder);

            try
            {
                ProcessResult processResult;

                //Delete the order items.
                shippingOrder.OrderItems= GetOrderItems(new OrderItemsFilter() { ShippingOrderId = shippingOrder.Id });

                if (shippingOrder.OrderItems != null)
                {
                    foreach (var orderItem in shippingOrder.OrderItems)
                    {
                        processResult = DeleteOrderItem(orderItem);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                processResult = _shippingOrderRepository.Delete(shippingOrder);

                if (processResult.IsSucceed) { shippingOrder.ObjectState = DomainEntityState.Deleted; }

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

        #region Order Items

        #region Public Methods

        /// <summary>
        /// Gets a OrderItem.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public OrderItem GetOrderItemById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _shippingOrderRepository.LoadOrderItemById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Gets a list of OrderItems.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<OrderItem> GetOrderItems(OrderItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _shippingOrderRepository.LoadOrderItems(filter.ShippingOrderId, filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the orderItem list.
        /// </summary>
        /// <param name="orderItems">The orderItem list.</param>
        /// <returns></returns>
        public ProcessResult SaveOrderItems(BindingList<OrderItem> orderItems)
        {
            Check.Argument.IsNotNull(() => orderItems);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                for (var i = 0; i < orderItems.Count; i++)
                {
                    var orderItem = orderItems[i];

                    orderItem.SetUserAndDates();

                    if (orderItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteOrderItem(orderItem);
                        orderItems.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveOrderItem(orderItem);
                    }

                    if (!processResult.IsSucceed) break;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the OrderItem.
        /// </summary>
        /// <param name="OrderItem">The OrderItem.</param>
        /// <returns></returns>
        public ProcessResult SaveOrderItem(OrderItem OrderItem)
        {
            Check.Argument.IsNotNull(() => OrderItem);

            if (!OrderItem.IsChanged) return ProcessResult.Succeed;

            try
            {
                OrderItem.SetUserAndDates();

                var processResult = _shippingOrderRepository.Save(OrderItem);

                if (processResult.IsSucceed) { OrderItem.ObjectState = DomainEntityState.Unchanged; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a OrderItem.
        /// </summary>
        /// <param name="OrderItem">The OrderItem.</param>
        /// <returns></returns>
        public ProcessResult DeleteOrderItem(OrderItem OrderItem)
        {
            Check.Argument.IsNotNull(() => OrderItem);

            try
            {
                var processResult = _shippingOrderRepository.Delete(OrderItem);

                if (processResult.IsSucceed) { OrderItem.ObjectState = DomainEntityState.Deleted; }

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

        #endregion
    }
}
