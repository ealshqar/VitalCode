using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormSelectItem : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private Item _selectedItem;

        #endregion

        #region Properties

        /// <summary>
        /// Item selected by user
        /// </summary>
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormSelectItem()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Logic

        /// <summary>
        /// Selects an item and closes form if an item is selected
        /// </summary>
        private void SelectItem()
        {
            var showError = false;
            searchLookUpEditSelectItem.ClosePopup();

            if (searchLookUpEditSelectItem.EditValue == null && searchLookUpEditSelectItem.Properties.DataSource!= null)
            {
                showError = true;
            }
            else
            {
                var item = (searchLookUpEditSelectItem.Properties.DataSource as BindingList<Item>).FirstOrDefault(i => i.Id == int.Parse(searchLookUpEditSelectItem.EditValue.ToString()));
                if (item != null)
                {
                    SelectedItem = item;
                    DialogResult = DialogResult.Yes;
                    Close();
                }
            }

            if (showError)
            {
                UiHelperClass.ShowError("No Item Selected", "Please select an item or press Escape to cancel.");
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormSelectItem_Load(object sender, EventArgs e)
        {
            UiHelperClass.FadeIn(this, true);
            UiHelperClass.ShowWaitingPanel(StaticKeys.LoadingItems);
            searchLookUpEditSelectItem.Properties.DataSource = CacheHelper.SetOrGetCachableData(CachableDataEnum.AllItems) as BindingList<Item>;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Select Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSelect_Click(object sender, EventArgs e)
        {
            SelectItem();
        }

        /// <summary>
        /// Handles key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormSelectItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectItem();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectedItem = null;
                DialogResult = DialogResult.No;
                Close();
            }
        }

        /// <summary>
        /// Handles form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormSelectItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SelectedItem == null)
            {
                DialogResult = DialogResult.No;
            }
            UiHelperClass.FadeOut(this, true);
        }

        /// <summary>
        /// Hanldes searchLookupEdit auto open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormSelectItem_Activated(object sender, EventArgs e)
        {
            searchLookUpEditSelectItem.Focus();
            searchLookUpEditSelectItem.ShowPopup();
        }

        /// <summary>
        /// Handles the on key down event on the ribbon
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var popupForm = sender as PopupSearchLookUpEditForm;

                if (popupForm != null)
                {
                    var edit = popupForm.OwnerEdit;
                    if (edit.Properties.View.GetFocusedRow() != null)
                    {
                        var currentItem = edit.Properties.View.GetFocusedRow() as Item;
                        searchLookUpEditSelectItem.EditValue = currentItem.Id;
                        SelectItem();
                    }
                }
            }
        }

        /// <summary>
        /// Handle adding keydown event to searchlookupedit view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchLookUpEditSelectItem_CloseUp(object sender, CloseUpEventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            // ReSharper disable PossibleNullReferenceException
            var popupForm = ((IPopupControl)edit).PopupWindow as PopupSearchLookUpEditForm;
            // ReSharper restore PossibleNullReferenceException

            if (popupForm != null)
                popupForm.KeyDown -= OnKeyDown;
        }

        /// <summary>
        /// Handle removing keydown event from searchlookupedit view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchLookUpEditSelectItem_Popup(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            // ReSharper disable PossibleNullReferenceException
            var popupForm = ((IPopupControl)edit).PopupWindow as PopupSearchLookUpEditForm;
            // ReSharper restore PossibleNullReferenceException

            if (popupForm != null)
                popupForm.KeyDown += OnKeyDown;
        }

        #endregion    
    }
}