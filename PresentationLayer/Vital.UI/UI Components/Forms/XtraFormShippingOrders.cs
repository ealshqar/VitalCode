using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Vital.Business.Filters;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.User_Controls.Modules;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormShippingOrders : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private ShippingOrdersManager _shippingOrdersManager;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current focused shipping order
        /// </summary>
        private ShippingOrder FocusedAdhocShippingOrder
        {
            get
            {
                return (gridViewShippingOrders.IsDataRow(gridViewShippingOrders.FocusedRowHandle)) ?
                        gridViewShippingOrders.GetFocusedRow() as ShippingOrder : null;
            }
        }

        /// <summary>
        /// Gets if the focused shipping order is sent or not
        /// </summary>
        private bool AdhocOrderSent
        {
            get
            {
                return FocusedAdhocShippingOrder != null && FocusedAdhocShippingOrder.Sent;
            }
        }

        public XtraUserControlPrintingOptions PrintingOptions { get; set; }

        #endregion

        #region Constructor
        
        public XtraFormShippingOrders()
        {
            InitializeComponent();
        }

        #endregion

        #region Shipping Orders

        /// <summary>
        /// Sends a shipping order email
        /// </summary>
        private void SendShippingOrder()
        {
            if (UiHelperClass.ShowConfirmQuestion("Are you sure you would like to confirm and send the shipping order?") == DialogResult.Yes)
            {
                if (UiHelperClass.SendShipmentOrder(
                    FocusedAdhocShippingOrder,
                    null,
                    true,
                    true,
                    PrintingOptions, true).IsSucceed)
                {
                    _shippingOrdersManager.Save(FocusedAdhocShippingOrder);
                    BindShippingOrders();
                }
            }
        }

        /// <summary>
        /// Open a Shipping Order.
        /// </summary>
        private void OpenShippingOrder(bool isNew)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Shipping Order ..." : "Loading Shipping Order ...");

                var currentShippingOrder = isNew
                    ? new ShippingOrder
                    {
                        Number = "0",
                        Sent = false,
                        OrderItems = new BindingList<OrderItem>(),
                        SendToClient = false,
                        TechnicianName = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianName),
                        TechnicianAddress = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianAddress),
                        TechnicianState = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianState),
                        TechnicianZipCode = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianZip),
                        TechnicianCity = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianCity),
                        TechnicianPhone = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianPhone),
                        Comments = string.Empty,
                        Test = null
                    }
                    : _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter()
                    {
                        ItemId = FocusedAdhocShippingOrder.Id
                    });

                var shippingOrderForm = new frmShippingOrder();
                shippingOrderForm.ShippingOrderObject = currentShippingOrder;
                shippingOrderForm.PrintingOptions = PrintingOptions;
                shippingOrderForm.TestObject = null;

                UiHelperClass.HideSplash();

                shippingOrderForm.ShowDialog();
                BindShippingOrders();
                UibllInteraction.Instance.MainForm.UpdateShippingOrdersCount();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }
     
        /// <summary>
        /// Binds the shipping orders
        /// </summary>
        private void BindShippingOrders()
        {
            gridControlShippingOrders.DataBindings.Clear();
            gridControlShippingOrders.DataSource = _shippingOrdersManager.GetShippingOrders(
                                                    new ShippingOrdersFilter()
                                                    {
                                                        CreationDateTime = DateTime.Now,

                                                    });
        }

        /// <summary>
        /// Delete shipping order
        /// </summary>
        private void DeleteShippingOrder()
        {
            try
            {
                var shippingOrder = FocusedAdhocShippingOrder;

                if (shippingOrder.Sent)
                {
                    UiHelperClass.ShowInformation("Selected Shipping Order has been sent and can't be deleted.", "Delete Shipping Order");
                    return;
                }

                if (UiHelperClass.ShowConfirmQuestion("The selected order will be deleted, are you sure?") == DialogResult.Yes)
                {
                    _shippingOrdersManager.Delete(shippingOrder);
                    BindShippingOrders();
                    UibllInteraction.Instance.MainForm.UpdateShippingOrdersCount();
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(ContextMenuStripOpening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == contextMenuStripShippingOrders)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewShippingOrders);

                    var ordersCount = _shippingOrdersManager.GetShippingOrders(new ShippingOrdersFilter()
                                                                               {
                                                                                   LoadingType =
                                                                                       LoadingTypeEnum.Light,
                                                                                   CreationDateTime =
                                                                                       DateTime.Now,
                                                                                   Sent = false
                                                                               }).Count;

                    toolStripMenuItemDeleteOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders) && !AdhocOrderSent;
                    toolStripMenuItemNewOrder.Enabled = ordersCount  == 0;
                    toolStripMenuItemOpenOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders);
                    toolStripMenuItemSendOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders) && !AdhocOrderSent;

                }
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(ContextMenuStrip_ItemClicked), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                ((ContextMenuStrip)sender).Hide();

                if (sender == contextMenuStripShippingOrders)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteOrder)
                    {
                        DeleteShippingOrder();
                    }
                    else if (e.ClickedItem == toolStripMenuItemOpenOrder)
                    {
                        OpenShippingOrder(false);
                    }
                    else if (e.ClickedItem == toolStripMenuItemNewOrder)
                    {
                        OpenShippingOrder(true);
                    }
                    else if (e.ClickedItem == toolStripMenuItemSendOrder)
                    {
                        SendShippingOrder();
                    }
                }
            }
        }

        /// <summary>
        /// Handles form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormShippingOrders_Load(object sender, EventArgs e)
        {
            _shippingOrdersManager = new ShippingOrdersManager();
            BindShippingOrders();
            PrintingOptions = new XtraUserControlPrintingOptions();
            PrintingOptions.UpdateOptions();
        }

        /// <summary>
        /// Handles double clicking the shipping order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewShippingOrders_DoubleClick(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(gridViewShippingOrders_DoubleClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == gridViewShippingOrders && UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders))
                {
                    OpenShippingOrder(false);
                }
            }
        }

        /// <summary>
        /// Handles coloring of rows that are sent and rows that are not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewShippingOrders_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            UiHelperClass.HandleOrderColor(sender, e);
        }

        #endregion
    }
}