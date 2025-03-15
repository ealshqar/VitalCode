using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormItemDescription : XtraForm
    {
        #region Fields

        private ItemsManager _itemsManager;
        private Item _itemToSave;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new XtraFormItemDescription.
        /// </summary>
        public XtraFormItemDescription()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Construct a new XtraFormItemDescription with the passed item.
        /// </summary>
        public XtraFormItemDescription(Item item) : this()
        {
            Item = item;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public Item Item { get; set; }

        #endregion

        #region Methods

        #region InitAndBinding

        /// <summary>
        /// Perform the initialization of XtraFormItemDescription.
        /// </summary>
        private void PerformSpecificIntializationSteps()
        {
            if (Item == null)
                return;

            _itemsManager = new ItemsManager();

            _itemToSave = _itemsManager.GetItemById(new SingleItemFilter() { ItemId = Item.Id });
        }

        /// <summary>
        /// Setup the handlers.
        /// </summary>
        private void SetupHandlers()
        {
            if (_itemToSave == null)
                return;

            _itemToSave.PropertyChanged += _itemToSave_PropertyChanged;
        }

        /// <summary>
        /// Set Control binding.
        /// </summary>
        private void SetBinding()
        {
            if (_itemToSave == null)
                return;

            Text = layoutControlGroupItemDescription.Text = string.Format(StaticKeys.ItemDescriptionTitle, _itemToSave.Name);

            UiHelperClass.BindControl(memoEditItemDescription, _itemToSave, () => _itemToSave.Description);

            memoEditItemDescription.Select(0, 0);
        }

        #endregion

        #region Logic

        /// <summary>
        /// Enables the Done button.
        /// </summary>
        private void EnableDoneButton()
        {
            barButtonItemSave.Enabled = true;
        }

        /// <summary>
        /// Performs the cancel action
        /// </summary>
        private void CancelAction()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Done Actions for the XtraFormItemDescription.
        /// </summary>
        private void DoneActions()
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);

            var result = _itemsManager.SaveItem(_itemToSave);

            if (result.IsSucceed)
            {
                Item.Description = _itemToSave.Description;
                DialogResult = DialogResult.OK;
                Close();
            }

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Can the form close. With user notification.
        /// </summary>
        private bool CanClose()
        {
            if (_itemToSave == null || !_itemToSave.IsChanged)
                return true;

            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage);

            return dialogResult == DialogResult.Yes;
        }

        #endregion

        #endregion
        
        #region Events

        /// <summary>
        /// Handel the Loading event for the form.
        /// </summary>
        private void XtraFormItemDescription_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraFormItemDescription_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.DataInitializationMessgae);
                PerformSpecificIntializationSteps();
                SetupHandlers();
                UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);
                SetBinding();
                UiHelperClass.HideSplash();
            }
        }

        /// <summary>
        /// Close the form by save or cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(barManager_ItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if(e.Item == barButtonItemCancel)
                {
                    CancelAction();
                }
                else if (e.Item == barButtonItemSave)
                {
                    DoneActions();
                }


                
            }
        }

        /// <summary>
        /// Handel the form closing.
        /// </summary>
        private void XtraFormItemDescription_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(XtraFormItemDescription_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!CanClose())
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        private void XtraFormItemDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(XtraFormItemDescription_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.KeyCode == Keys.S && e.Control)
                {
                    DoneActions();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    CancelAction();
                }
            }
        }

        /// <summary>
        /// Handel the changing on the item.
        /// </summary>
        private void _itemToSave_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new PropertyChangedEventHandler(_itemToSave_PropertyChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_itemToSave.ObjectState == DomainEntityState.Modified)
                    EnableDoneButton();
            }
        }

        #endregion        


      
    }
}