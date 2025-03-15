using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.User_Controls.Modules;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmShipping : XtraForm
    {

        #region Fields

        private ShippingOrdersManager _shippingOrdersManager;
        private TestsManager _testsManager;
        private LookupsManager _lookupsManager;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the focused test.
        /// </summary>
        private Test FocusedTest
        {
            get
            {
                return (gridViewTests.IsDataRow(gridViewTests.FocusedRowHandle)) ?
                        gridViewTests.GetFocusedRow() as Test : null;
            }
        }

        /// <summary>
        /// Gets the test last shipping order
        /// </summary>
        private ShippingOrder LastTestOrder
        {
            get
            {
                return FocusedTest == null ? null : FocusedTest.LastOrder;
            }
        }


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

        /// <summary>
        /// Gets the current selected date
        /// </summary>
        /// <returns></returns>
        private DateTime SelectedDate()
        {
            return dateNavigatorShipping.Selection[0];
        }

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor.
        /// </summary>
        public frmShipping()
        {
            InitializeComponent();
        }

        #endregion

        #region General

        private void SetBinding()
        {
            BindShippingOrders();
            BindTestOrders();
            simpleLabelItemDate.Text = "Tests and Orders for " + SelectedDate().ToShortDateString();
        }

        #endregion

        #region Shipping Orders

        /// <summary>
        /// Logic for saving order
        /// </summary>
        /// <param name="isAdhocOrder"></param>
        /// <param name="order"></param>
        /// <param name="test"></param>
        /// <param name="rebind"></param>
        private ProcessResult SendOrderLogic(bool isAdhocOrder,ShippingOrder order, Test test, bool rebind, bool showErrors, bool checkInternet)
        {
            var result = ProcessResult.Failed;

            if (isAdhocOrder)
            {
                result = UiHelperClass.SendShipmentOrder(
                    order,
                    null,
                    showErrors,
                    true,
                    PrintingOptions, checkInternet);

                if (result.IsSucceed)
                {
                    _shippingOrdersManager.Save(order);
                    if (rebind)
                    {
                        BindShippingOrders();    
                    }
                }
            }
            else if (LastTestOrder != null)
            {
                var currentShippingOrder = _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter()
                {
                    ItemId = order.Id
                });

                result = UiHelperClass.SendShipmentOrder(
                    currentShippingOrder,
                    test,
                    showErrors,
                    true,
                    PrintingOptions, checkInternet);

                if (result.IsSucceed)
                {
                    _shippingOrdersManager.Save(currentShippingOrder);
                    _testsManager.SaveTest(test);
                    if (rebind)
                    {
                        BindTestOrders();    
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// Sends a shipping order email
        /// </summary>
        private void SendShippingOrder(bool isAdhocOrder)
        {
            if (UiHelperClass.ShowConfirmQuestion("Are you sure you would like to confirm and send the shipping order?") == DialogResult.Yes)
            {
                if (isAdhocOrder)
                {
                    SendOrderLogic(isAdhocOrder, FocusedAdhocShippingOrder, null, true,true,true);
                }
                else if (LastTestOrder != null)
                {
                    SendOrderLogic(isAdhocOrder, LastTestOrder, FocusedTest, true, true, true);
                }
            }
        }

        /// <summary>
        /// Open a Shipping Order.
        /// </summary>
        private void OpenShippingOrder(bool isNew, bool isAdhocOrder)
        {
            try
            {
                UiHelperClass.ShowWaitingPanel(isNew ? "New Shipping Order ..." : "Loading Shipping Order ...");

                ShippingOrder currentShippingOrder = null;

                if (isNew)
                {
                    int lookupId = int.Parse(UiHelperClass.GetSettingValueFromCache(SettingKeys.ShippingMethod,CachableDataEnum.VisibleSettings).ToString());
                    var shippingMethod = _lookupsManager.GetLookupById(new SingleItemFilter() {ItemId = lookupId});
                    
                    if (isAdhocOrder)
                    {
                        currentShippingOrder = new ShippingOrder
                        {
                            Number = "0",
                            Sent = false,
                            OrderItems = new BindingList<OrderItem>(),
                            ShippingMethod = shippingMethod,
                            SendToClient = UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings, SettingKeys.SendShipmentToClient),
                            TechnicianName = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianName),
                            TechnicianAddress = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianAddress),
                            TechnicianState = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianState),
                            TechnicianZipCode = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianZip),
                            TechnicianCity = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianCity),
                            TechnicianPhone = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianPhone),
                            Comments = string.Empty,
                            Test = null
                        };
                    }
                    else if (FocusedTest != null)
                    {
                        var currentTest = _testsManager.GetTestById(new SingleItemFilter() {ItemId = FocusedTest.Id});

                        currentShippingOrder = new ShippingOrder
                        {
                            Number = currentTest.Patient.Id.ToString() + "-" +
                                     currentTest.Id.ToString() + "-0",
                            Sent = false,
                            ShippingMethod = shippingMethod,
                            SendToClient = UiHelperClass.GetSettingCheckValue(CachableDataEnum.ShippingOrderSettings, SettingKeys.SendShipmentToClient),
                            PatientFirstName = currentTest.Patient.FirstName,
                            PatientLastName = currentTest.Patient.LastName,
                            PatientAddress1 = currentTest.Patient.Address1,
                            PatientAddress2 = currentTest.Patient.Address2,
                            PatientCity = currentTest.Patient.City,
                            PatientState = currentTest.Patient.State,
                            PatientZip = currentTest.Patient.Zip,
                            PatientHomePhone = currentTest.Patient.HomePhone,
                            PatientWorkPhone = currentTest.Patient.WorkPhone,
                            PatientCellPhone = currentTest.Patient.CellPhone,
                            PatientFax = currentTest.Patient.Fax,
                            PatientEmail = currentTest.Patient.Email,
                            TechnicianName = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianName),
                            TechnicianAddress = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianAddress),
                            TechnicianState = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianState),
                            TechnicianZipCode = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianZip),
                            TechnicianCity = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianCity),
                            TechnicianPhone = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianPhone),
                            Comments = string.Empty,
                            Test = currentTest
                        };

                        var orderItems = new BindingList<OrderItem>();
                        
                        foreach (var sl in currentTest.TestSchedule.ScheduleLines.Where(sl => !sl.IsDeleted && (sl.Item.ItemSourceLookup == null || (sl.Item.ItemSourceLookup != null && sl.Item.ItemSourceLookup.Id == UiHelperClass.GetSystemItemSourceLookupId()))))
                        {
                            orderItems.Add(new OrderItem() { ShippingOrder = currentShippingOrder, Include = true, Item = sl.Item, Quantity = int.Parse(sl.NoOfBottle) });
                        }
                        
                        currentShippingOrder.OrderItems = orderItems;
                    }
                }
                else
                {
                    if (isAdhocOrder)
                    {
                        currentShippingOrder = _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter() { ItemId = FocusedAdhocShippingOrder.Id });
                    }
                    else if (LastTestOrder != null)
                    {
                        currentShippingOrder = _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter()
                        {
                            ItemId = LastTestOrder.Id
                        });
                    }
                }
                

                var shippingOrderForm = new frmShippingOrder();
                shippingOrderForm.ShippingOrderObject = currentShippingOrder;
                shippingOrderForm.PrintingOptions = PrintingOptions;

                if (isAdhocOrder)
                {
                    shippingOrderForm.TestObject = null;
                }
                else if (FocusedTest != null)
                {
                    shippingOrderForm.TestObject = FocusedTest;
                }
                
                UiHelperClass.HideSplash();

                shippingOrderForm.ShowDialog();

                if (isAdhocOrder)
                {
                    BindShippingOrders();
                }
                else if (FocusedTest != null)
                {
                    if (currentShippingOrder.ObjectState != DomainEntityState.New)
                    {
                        var orders =
                        _shippingOrdersManager.GetShippingOrders(new ShippingOrdersFilter() {TestId = FocusedTest.Id});

                        FocusedTest.IsOrderSent = !(orders.Any(so => !so.Sent)) && orders.Any();
                        FocusedTest.StateLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TestState, FocusedTest.IsOrderSent ? TestStateEnum.DoneShipped : TestStateEnum.Done, false, false));
                        
                        _testsManager.SaveTest(FocusedTest);
                    }

                    BindTestOrders();
                }
                
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
            UiHelperClass.ShowWaitingPanel("Loading My Orders ...");

            gridControlShippingOrders.DataBindings.Clear();
            gridControlShippingOrders.DataSource = _shippingOrdersManager.GetShippingOrders(
                                                    new ShippingOrdersFilter()
                                                    {
                                                        CreationDateTime = SelectedDate(),

                                                    });

            UiHelperClass.HideSplash();
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

        #region Test Orders

        /// <summary>
        /// Binds test orders
        /// </summary>
        private void BindTestOrders()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.InitilizingUserInterface);

            var tests = new TestsManager();

            var testsList = tests.GetTests(new TestsFilter() { DateTime = SelectedDate() });

            gridControlTests.DataSource = testsList;

            UiHelperClass.HideSplash();
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
                    toolStripMenuItemNewOrder.Enabled = ordersCount == 0;
                    toolStripMenuItemOpenOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders);
                    toolStripMenuItemSendOrder.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewShippingOrders) && !AdhocOrderSent;

                }

                if (sender == contextMenuStripTests)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewTests);

                    ToolStripMenuItemNew.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewTests) && FocusedTest != null && (FocusedTest.IsOrderSent || FocusedTest.ShippingOrders.Count == 0);
                    ToolStripMenuItemOpen.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewTests) && FocusedTest != null && FocusedTest.ShippingOrders.Count > 0;
                    ToolStripMenuItemSend.Enabled = UiHelperClass.IsClickInRowByMouse(gridViewTests) && LastTestOrder != null && !LastTestOrder.Sent;
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
                        OpenShippingOrder(false,true);
                    }
                    else if (e.ClickedItem == toolStripMenuItemNewOrder)
                    {
                        OpenShippingOrder(true, true);
                    }
                    else if (e.ClickedItem == toolStripMenuItemSendOrder)
                    {
                        SendShippingOrder(true);
                    }
                }
                else if (sender == contextMenuStripTests)
                {
                    if (e.ClickedItem == ToolStripMenuItemNew)
                    {
                        OpenShippingOrder(true,false);
                    }
                    else if (e.ClickedItem == ToolStripMenuItemOpen)
                    {
                        OpenShippingOrder(false,false);
                    }
                    else if (e.ClickedItem == ToolStripMenuItemSend)
                    {
                        SendShippingOrder(false);
                    }
                }
            }
        }

        /// <summary>
        /// Handles form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmShipping_Load(object sender, EventArgs e)
        {
            _shippingOrdersManager = new ShippingOrdersManager();
            _testsManager = new TestsManager();
            _lookupsManager = new LookupsManager();
            PrintingOptions = new XtraUserControlPrintingOptions();
            PrintingOptions.UpdateOptions();

            var todayDates = new DevExpress.XtraEditors.Controls.DatesCollection();
            todayDates.Add(DateTime.Today);
            dateNavigatorShipping.Selection.Clear();
            dateNavigatorShipping.Selection.AddRange(todayDates);

            SetBinding();
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
                    OpenShippingOrder(false,true);
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

        /// <summary>
        /// Handles the select on a date.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void dateNavigatorShipping_EditDateModified(object sender, EventArgs e)
        {
            SetBinding();
        }

        /// <summary>
        /// Handle row style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewTests_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            UiHelperClass.HandleOrderColor(sender, e);
        }

        #endregion

        /// <summary>
        /// Logic for sending emails for all orders and tests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSendAll_Click(object sender, EventArgs e)
        {
            var succeded = true;
            var adhocOrders = ((BindingList<ShippingOrder>)gridControlShippingOrders.DataSource).Where(or => !or.Sent);
            var tests = ((BindingList<Test>)gridControlTests.DataSource).Where(t => !t.IsOrderSent && t.LastOrder != null);
            var testsWithoutOrders = ((BindingList<Test>)gridControlTests.DataSource).Where(t => !t.IsOrderSent && t.ShippingOrders.Count == 0);
            if (adhocOrders.Count() == 0 && tests.Count() == 0)
            {
                if (testsWithoutOrders.Count() > 0)
                {
                    UiHelperClass.ShowInformation(
                        "There are no orders to sent, please check that you created offline orders for your tests.", "No Orders to Sent");
                }
                else
                {
                    UiHelperClass.ShowInformation(
                        "There are no orders to sent for the selected date or all orders have been sent already.", "No Orders to Sent");
                }
                
                return;
            }

            if (UiHelperClass.ShowConfirmQuestion("Are you sure you would like to confirm and send the shipping orders?") == DialogResult.Yes)
            {
                UiHelperClass.ShowWaitingPanel("Checking Internet Connection ...");
                if (!UiHelperClass.IsInternetOnline())
                {
                    UiHelperClass.HideSplash();
                    UiHelperClass.ShowError("Internet Connection Needed",
                                                "Internet connection is needed to send order details, please check your internet connection and try again.");
                    return;
                }

                UiHelperClass.ShowWaitingPanel("Sending My Orders");

                foreach (var shippingOrder in adhocOrders)
                {
                    if (!SendOrderLogic(true, shippingOrder, null, false, false,false).IsSucceed)
                    {
                        succeded = false;
                    }
                }

                UiHelperClass.ShowWaitingPanel("Sending Test Orders");
                foreach (var test in tests)
                {
                    var currentShippingOrder = _shippingOrdersManager.GetShippingOrderId(new SingleItemFilter()
                    {
                        ItemId = test.LastOrder.Id
                    });

                    if (!SendOrderLogic(false, currentShippingOrder, test, false, false, false).IsSucceed)
                    {
                        succeded = false;
                    }
                }

                SetBinding();
            }
        }
    }
}
