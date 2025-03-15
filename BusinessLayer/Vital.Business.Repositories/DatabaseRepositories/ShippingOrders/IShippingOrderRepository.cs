using System;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.ShippingOrders
{
    public interface IShippingOrderRepository
    {
        #region Shipping Orders

        /// <summary>
        /// Loads a ShippingOrder by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        ShippingOrder LoadShippingOrderId(int id);


        /// <summary>
        /// Loads a list of ShippingOrder.
        /// </summary>
        /// <returns></returns>
        BindingList<ShippingOrder> LoadShippingOrders(string number, int? testId, int? patientId, DateTime? sentDate, bool? sendToClient, bool? sent, string patientFirstName, string patientLastName, string patientAddress1, string patientAddress2, string patientCity, string patientState, string patientZip, string patientHomePhone, string patientWorkPhone, string patientCellPhone, string patientFax, string patientEmail, string technicianName, string technicianAddress, string technicianState, string technicianZipCode, string technicianCity, string technicianPhone, DateTime? creationDateTime, LoadingTypeEnum loadingType);

        /// <summary>
        /// Saves a shipping order.
        /// </summary>
        /// <param name="shippingOrder">The shipping order</param>
        /// <returns></returns>
        ProcessResult Save(ShippingOrder shippingOrder);

        /// <summary>
        /// Deletes a shipping order.
        /// </summary>
        /// <param name="shippingOrder">The shipping order</param>
        /// <returns></returns>
        ProcessResult Delete(ShippingOrder shippingOrder);

        #endregion

        #region Order Items

        /// <summary>
        /// Loads Order item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Order item.</returns>
        OrderItem LoadOrderItemById(int id);

        /// <summary>
        /// Loads a list of Order items.
        /// </summary>
        /// <param name="testOrderId">The test Order id.</param>
        /// <param name="itemId">The item id.</param>
        /// <returns>List of Order items.</returns>
        BindingList<OrderItem> LoadOrderItems(int testOrderId, int itemId);

        /// <summary>
        /// Saves the Order item.
        /// </summary>
        /// <param name="OrderItemToSave">The Order item to be saved.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(OrderItem OrderItemToSave);

        /// <summary>
        /// Deletes the Order item.
        /// </summary>
        /// <param name="OrderItemToDelete">The Order item.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(OrderItem OrderItemToDelete);

        #endregion
    }
}
