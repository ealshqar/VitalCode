using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.UI_Classes;

namespace Vital.UI.UI_Components.Forms.DataManagement
{
    public partial class frmNewTestProtocolStartingItems : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private readonly ItemsManager _itemsManager;
        private readonly LookupsManager _lookupsManager;

        #endregion

        #region Properties
        
        public BindingList<Item> Items { get; set; }
        public BindingList<Item> SelectedItems { get; set; }
        public BindingList<Lookup> Types { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="selectedItems">The selected items list.</param>
        public frmNewTestProtocolStartingItems(BindingList<Item> selectedItems )
        {
            InitializeComponent();

            _itemsManager = new ItemsManager();
            _lookupsManager = new LookupsManager();
            Items = new BindingList<Item>();
            
            BindTypes();
            
            SelectedItems = selectedItems;
            gridControlSelectedItems.DataSource = SelectedItems;

            UiHelperClass.SetLayoutControlProperties(layoutControl1);
            UiHelperClass.SetViewProperties(gridViewItems);
        }

        #endregion

        #region Binding & Initilization.

        /// <summary>
        /// Binds the types drop down.
        /// </summary>
        private void BindTypes()
        {
            var itemTypeLookup = new BindingList<Lookup>(
                UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ItemType))
                .Where(l=>l.Value == "Substance" || l.Value == "Product").ToList());
            
            Types = itemTypeLookup;
             
            if(Types.Count > 0)
            {
                lookupEditTypes.Properties.DataSource = Types;
            }
        }    

        #endregion

        #region Events Handlers

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event argument.</param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (sender == contextMenuStripSelectedItems)
            {
                e.Cancel = UiHelperClass.CancelClickAction(gridViewSelectedItems);

                var isEnabled = UiHelperClass.IsClickInRow(gridViewSelectedItems);

                toolStripMenuItemDeleteSelectedItem.Enabled = isEnabled;
            }
        }

        /// <summary>
        /// Handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sender != null)
            {
                ((ContextMenuStrip)sender).Hide();

                if (sender == contextMenuStripSelectedItems)
                {
                    if (e.ClickedItem == toolStripMenuItemDeleteSelectedItem)
                    {
                        if (UiHelperClass.ShowConfirmQuestion(StaticKeys.DeleteConfirmQuestion) == DialogResult.Yes)
                        {
                            gridViewSelectedItems.DeleteRow(gridViewSelectedItems.FocusedRowHandle);    
                            
                            EnableDoneButton();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle clicking a button in the toolbar
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        public void barManagerTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Item.Name)) return;

            if (e.Item == barButtonItemDone)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (e.Item == barButtonItemCancel)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }

        }

        /// <summary>
        /// Handles the selection change of the types drop down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void lookupEditTypes_EditValueChanged(object sender, EventArgs e)
        {
            if(CheckIfAnyItemIsSelected())
            {
                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.WarningMessageUnSelectingItems) == DialogResult.Yes)
                {
                    ChangeItemsOnTypeSelection();
                    
                }
                else
                {
                    lookupEditTypes.EditValueChanged -= lookupEditTypes_EditValueChanged;
                    lookupEditTypes.EditValue = lookupEditTypes.OldEditValue;
                    lookupEditTypes.EditValueChanged += lookupEditTypes_EditValueChanged;
                }
            }
            else
            {
                ChangeItemsOnTypeSelection();
            }
        }

        /// <summary>
        /// Handles the click event on the select button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event argument.</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            var selectedIndexes = new List<int>();
            var selectedItemsGroup = new List<Item>();
            
            for (int i = 0; i < gridViewItems.DataRowCount; i++)
            {
                var isSelected = gridViewItems.GetRowCellValue(i, gridViewItems.Columns[0]);

                if (isSelected != null && (bool)isSelected)
                {
                    var item = ((Item)gridViewItems.GetRow(i));

                    selectedIndexes.Add(i);

                    selectedItemsGroup.Add(item);
                }
            }

            var filteredList = GetDestinctItemsList(selectedIndexes);
            
            if (filteredList.Count() > 0)
            {
                foreach (var item in filteredList)
                {
                    SelectedItems.Add(item);
                }

                gridControlSelectedItems.DataSource = SelectedItems;
                EnableDoneButton();
            }

            ClearGridSelection(selectedIndexes);
        }

        /// <summary>
        /// Handles the mouse wheel - a work around to remove focus on the cells after checking the checkbox.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewItems_MouseWheel(object sender, MouseEventArgs e)
        {

            btnSelect.Focus();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Binds the new items in the grid based on the selected type.
        /// </summary>
        private void ChangeItemsOnTypeSelection()
        {
            UiHelperClass.ShowSplash(typeof(WaitFormLoadingData));

            var value = lookupEditTypes.EditValue;

            Items = _itemsManager.GetItems(new ItemsFilter()
            {
                TypeLookupId = (int)value
            });

            gridControlStartingItems.DataSource = Items;

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Checks if there are any selected items in the grid.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfAnyItemIsSelected()
        {
            for (int i = 0; i < gridViewItems.DataRowCount; i++)
            {
                var isSelected = gridViewItems.GetRowCellValue(i, gridViewItems.Columns[0]);

                if ((bool)isSelected)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Enables the Done button.
        /// </summary>
        private void EnableDoneButton()
        {
            barButtonItemDone.Enabled = true;
        }

        /// <summary>
        /// Filters the list of the newly selected items and removes the duplications.
        /// </summary>
        /// <param name="selectedIndexes">The list of selected indexes</param>
        /// <returns>The filtered list of items.</returns>
        private IEnumerable<Item> GetDestinctItemsList(IEnumerable<int> selectedIndexes)
        {
            return (from index in selectedIndexes
                    select ((Item) gridViewItems.GetRow(index))
                    into item let isSelected = SelectedItems.Any(c => c.Id == item.Id) where !isSelected select item).ToList();
        }

        /// <summary>
        /// Clears the selection of the items grid checkboxes.
        /// </summary>
        /// <param name="selectedIndexes">The list of the selected items indexes.</param>
        private void ClearGridSelection(IEnumerable<int> selectedIndexes)
        {
            foreach (var index in selectedIndexes)
            {
                gridViewItems.SetRowCellValue(index, gridViewItems.Columns[0], false);
            }
        }

        #endregion
    }
}
