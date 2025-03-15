using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.Forms;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlVFSSecondaryItems : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        private VFS _vfsObject;
        private BindingList<VFSSecondaryItem> _deletedVFSSecondaryItems;
        private BindingList<VFSSecondaryItem> _vfsSecondaryItems;

        private LookupsManager _lookupsManager;
        private bool _isInEditMode;
        private Lookup _sectionLookup;
        private VFSSecondarySection _section;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the VFS item
        /// </summary>
        public VFS VFSObject
        {
            get { return _vfsObject; }
            set
            {
                _vfsObject = value;
            }
        }

        /// <summary>
        /// VFS Items List
        /// </summary>
        public BindingList<VFSSecondaryItem> VFSSecondaryItems
        {
            get
            {
                return _vfsSecondaryItems;
            }
            set
            {
                _vfsSecondaryItems = value;
            }
        }

        /// <summary>
        /// Gets the current focused VFS item
        /// </summary>
        /// <returns></returns>
        public VFSSecondaryItem FocusedVFSSecondaryItem
        {
            get
            {
                return (VFSSecondaryItem)gridViewVFSSecondaryItems.GetFocusedRow();
            }
        }

        /// <summary>
        /// List of deleted VFS Items
        /// </summary>
        public BindingList<VFSSecondaryItem> DeletedVFSSecondaryItems
        {
            get
            {
                return _deletedVFSSecondaryItems;
            }
        }

        /// <summary>
        /// Section Lookup for current user control
        /// </summary>
        public Lookup SectionLookup
        {
            get
            {
                return _sectionLookup;
            }
            set
            {
                _sectionLookup = value;
            }
        }

        /// <summary>
        /// Secondary Items Section
        /// </summary>
        public VFSSecondarySection Section
        {
            get
            {
                return _section;
            }
            set
            {
                _section = value;
            }
        }
        #endregion

        #region Constructor

        public XtraUserControlVFSSecondaryItems()
        {
            InitializeComponent();
            _section = VFSSecondarySection.None;
        }

        #endregion

        #region Logic

        #region Generic

        /// <summary>
        /// Initialize custom components
        /// </summary>
        public void CustomeInitializeComponent()
        {
            
            
        }

        /// <summary>
        /// Set binding.
        /// </summary>
        public void SetBinding()
        {
            _deletedVFSSecondaryItems = new BindingList<VFSSecondaryItem>();
            _vfsSecondaryItems = VFSObject.VfsSecondaryItems.Where(v=>v.SectionLookup.Id == SectionLookup.Id).ToBindingList();
            _vfsSecondaryItems.RaiseListChangedEvents = true;
            _vfsSecondaryItems.ListChanged += VFSSecondaryItems_ListChanged;
            gridControlVFSSecondaryItems.DataSource = _vfsSecondaryItems;

            switch (Section)
            {
                case VFSSecondarySection.PrimaryIssues:
                case VFSSecondarySection.SecondaryIssues:
                    gridColumnIsIssue.Visible = false;
                    gridViewVFSSecondaryItems.OptionsCustomization.AllowSort = false;
                    gridColumnOrder.SortOrder = ColumnSortOrder.Ascending;
                    break;
                case VFSSecondarySection.ThyroidIssues:
                case VFSSecondarySection.MercuryIssues:
                    gridColumnOrder.Visible = false;
                    layoutControlItemMoveDown.Visibility = LayoutVisibility.Never;
                    layoutControlItemMoveUp.Visibility = LayoutVisibility.Never;
                    emptySpaceItem2.Visibility = LayoutVisibility.Never;
                    emptySpaceItem3.Visibility = LayoutVisibility.Never;
                    break;
            }
            if (SectionLookup != null)
            {
                gridViewVFSSecondaryItems.ActiveFilterString = "SectionLookup.Id = " + SectionLookup.Id;
            }
        }

        /// <summary>
        /// Fill Lookups
        /// </summary>
        public void FillLookup()
        {
            SectionLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.VFSSecondarySection, Section));
        }

        /// <summary>
        /// Set the edit mode.
        /// </summary>
        /// <param name="isReadonly"></param>
        public void SetEditMode(bool isReadonly)
        {
            _isInEditMode = !isReadonly;
            gridViewVFSSecondaryItems.OptionsBehavior.ReadOnly = isReadonly;
            simpleButtonAddIssue.Enabled = !isReadonly;
            UpdateUpDownArrowEnableState();
        }

        /// <summary>
        /// Post editors.
        /// </summary>
        public void PostValues()
        {
            gridViewVFSSecondaryItems.PostEditor();
            gridViewVFSSecondaryItems.ValidateEditor();
            gridViewVFSSecondaryItems.UpdateCurrentRow();
        }

        /// <summary>
        /// Clear binding.
        /// </summary>
        public void ClearBinding()
        {
            gridControlVFSSecondaryItems.DataBindings.Clear();
            _deletedVFSSecondaryItems.Clear();
            _vfsSecondaryItems.ListChanged -= VFSSecondaryItems_ListChanged;
        }

        private void UpdateUpDownArrowEnableState()
        {
            var enabled = false;

            if (gridControlVFSSecondaryItems.DataSource != null)
            {
                enabled = (gridControlVFSSecondaryItems.DataSource as BindingList<VFSSecondaryItem>).Count >= 1;
            }
            
            simpleButtonMoveUp.Enabled = enabled && _isInEditMode;
            simpleButtonMoveDown.Enabled = enabled && _isInEditMode;

            if (gridViewVFSSecondaryItems.IsFirstRow)
            {
                simpleButtonMoveUp.Enabled = false;
            }
            else if (gridViewVFSSecondaryItems.IsLastRow)
            {
                simpleButtonMoveDown.Enabled = false;
            }
        }

        #endregion

        #region VFS Secondary Items

        /// <summary>
        /// Add VFS Item to list
        /// </summary>
        private void AddVFSSecondaryItem()
        {
            var frmSelectItem = new XtraFormSelectItem();

            if (frmSelectItem.ShowDialog() == DialogResult.Yes && frmSelectItem.SelectedItem != null)
            {
                var newVFSItem = new VFSSecondaryItem()
                {
                    VFS = VFSObject,
                    Item = new Item() { Id = frmSelectItem.SelectedItem.Id, Name = frmSelectItem.SelectedItem.Name},
                    SectionLookup = SectionLookup,
                    Checked = true
                };

                var siblings = VFSSecondaryItems.Where(s => s.SectionLookup != null && s.SectionLookup.Id == SectionLookup.Id);

                if (!siblings.Any())
                {
                    newVFSItem.Order = 1;
                }
                else
                {
                    newVFSItem.Order = siblings.Where(s => s.SectionLookup.Id == SectionLookup.Id).Max(i => i.Order) + 1;
                }

                VFSSecondaryItems.Add(newVFSItem);
            }
        }

        /// <summary>
        /// Delete VFSItem
        /// </summary>
        private void DeleteVFSSecondaryItem()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected VFS item will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var focusedRow = FocusedVFSSecondaryItem;

                    if (focusedRow != null && focusedRow.Id > 0)
                    {
                        //mark the object as deleted.
                        focusedRow.ObjectState = DomainEntityState.Deleted;
                        //add the deleted objects to a temporary list.
                        _deletedVFSSecondaryItems.Add(focusedRow);
                    }

                    //delete the row 
                    gridViewVFSSecondaryItems.DeleteRow(gridViewVFSSecondaryItems.FocusedRowHandle);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Generate ordering id where not exists.
        /// </summary>
        public void GenerateOrdaringIdNotExists()
        {
            var items = gridViewVFSSecondaryItems.DataSource as BindingList<VFSSecondaryItem>;

            if (items == null)
                return;

            var maxOrder = 0;

            foreach (var item in items)
            {
                if (item.Order == 0)
                {
                    item.Order = ++maxOrder;
                }
                else if (maxOrder < item.Order)
                {
                    maxOrder = item.Order;
                }
            }
        }

        /// <summary>
        /// Swap the order of items
        /// </summary>
        /// <param name="focusedRowHandle">Focused row handle.</param>
        /// <param name="isMovingUp">Step 2 index.</param>
        private void SwapOrders(int focusedRowHandle, bool isMovingUp)
        {
            var firstHandle = focusedRowHandle;
            var secondRowHandle = isMovingUp ? firstHandle - 1 : firstHandle + 1;

            var view = gridViewVFSSecondaryItems;

            if (!gridViewVFSSecondaryItems.IsDataRow(focusedRowHandle) ||
                view.IsNewItemRow(firstHandle) ||
                (isMovingUp && view.IsFirstRow) ||
                (!isMovingUp && view.IsLastRow)) return;

            var firstRow = gridViewVFSSecondaryItems.GetRow(firstHandle) as VFSSecondaryItem;
            var secondRow = gridViewVFSSecondaryItems.GetRow(secondRowHandle) as VFSSecondaryItem;
            var tempOrder = firstRow.Order;
            firstRow.Order = secondRow.Order;
            secondRow.Order = tempOrder;

            gridViewVFSSecondaryItems.FocusedRowHandle = secondRowHandle;
        }

        /// <summary>
        /// Move focus to first row
        /// </summary>
        public void FocusFirstRow()
        {
            gridControlVFSSecondaryItems.Focus();
            gridViewVFSSecondaryItems.Focus();
            gridViewVFSSecondaryItems.FocusedRowHandle = 0;
        }

        #endregion

        #endregion

        #region Handlers
        /// <summary>
        /// Handles loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraUserControlVFSSecondaryItems_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handle context menu opening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripVFSSecondaryItems_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripVFSSecondaryItems_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                e.Cancel = UiHelperClass.CancelClickAction(gridViewVFSSecondaryItems);

                var enabled = UiHelperClass.IsClickInRowByMouse(gridViewVFSSecondaryItems) && _isInEditMode;
                toolStripMenuItemAdd.Enabled = _isInEditMode;
                toolStripMenuItemDelete.Enabled = enabled;
            }
        }

        /// <summary>
        /// Handle context menu item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripVFSSecondaryItems_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStripVFSSecondaryItems_ItemClicked), sender, e);
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

                if (e.ClickedItem == toolStripMenuItemDelete)
                {
                    DeleteVFSSecondaryItem();
                }
                else if (e.ClickedItem == toolStripMenuItemAdd)
                {
                    AddVFSSecondaryItem();
                }
            }
        }

        /// <summary>
        /// Handles key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSSecondaryItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(gridViewVFSSecondaryItems_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var allowedColumns = gridViewVFSSecondaryItems.FocusedColumn == gridColumnIsIssue ||
                                     gridViewVFSSecondaryItems.FocusedColumn == gridColumnOrder ||
                                     gridViewVFSSecondaryItems.FocusedColumn == gridColumnItemName;

                if (_isInEditMode && (e.KeyCode == Keys.Insert || (e.Control && e.KeyCode == Keys.I) || e.KeyCode == Keys.Oemtilde))
                {
                    AddVFSSecondaryItem();
                }
                else if (_isInEditMode && allowedColumns && e.KeyCode == Keys.Space)
                {
                    FocusedVFSSecondaryItem.Checked = !FocusedVFSSecondaryItem.Checked;
                }
                else if (_isInEditMode && allowedColumns && e.KeyCode == Keys.Delete)
                {
                    DeleteVFSSecondaryItem();
                }
                else if (_isInEditMode && allowedColumns && e.KeyCode == Keys.Up && e.Control)
                {
                    SwapOrders(gridViewVFSSecondaryItems.FocusedRowHandle, true);
                    gridViewVFSSecondaryItems.MoveNext();//Needed because arrow moves a step more that should be cancelled
                }
                else if (_isInEditMode && allowedColumns && e.KeyCode == Keys.Down && e.Control)
                {
                    SwapOrders(gridViewVFSSecondaryItems.FocusedRowHandle, false);
                    gridViewVFSSecondaryItems.MovePrev();//Needed because arrow moves a step more that should be cancelled
                }
            }
        }

        /// <summary>
        /// Handle add issue button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAddIssue_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonAddIssue_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                AddVFSSecondaryItem();
            }
        }

        /// <summary>
        /// Handle move up button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMoveUp_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonMoveUp_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SwapOrders(gridViewVFSSecondaryItems.FocusedRowHandle, true);
            }
        }

        /// <summary>
        /// Handle move down button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonMoveDown_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonMoveDown_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                SwapOrders(gridViewVFSSecondaryItems.FocusedRowHandle, false);
            }
        }

        /// <summary>
        /// Handle focus change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSSecondaryItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateUpDownArrowEnableState();
        }

        /// <summary>
        /// Handle list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VFSSecondaryItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            VFSObject.ObjectState = DomainEntityState.Modified;
            VFSObject.NotifyPropertyChanged(ExpressionHelper.GetPropertyName(() => VFSObject.VfsSecondaryItems));
        }

        /// <summary>
        /// Handles cell value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSSecondaryItems_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            VFSObject.ObjectState = DomainEntityState.Modified;
            VFSObject.NotifyPropertyChanged(ExpressionHelper.GetPropertyName(() => VFSObject.VfsSecondaryItems));
        }

        /// <summary>
        /// Handle row click to allow check or uncheck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSSecondaryItems_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == gridColumnItemName)
            {
                FocusedVFSSecondaryItem.Checked = !FocusedVFSSecondaryItem.Checked;
            }
        }

        #endregion          
    }
}
