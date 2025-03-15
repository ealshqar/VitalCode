using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.Forms;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlVFSItems : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        private VFS _vfsObject;
        private BindingList<VFSItem> _deletedVFSItems;

        private LookupsManager _lookupsManager;
        private string _elevation;
        private string _normal;
        private string _decrease;
        private List<string> _changeTypes;
        private bool _isInEditMode;
        private bool _arrowNavigation;
        private bool _navigatingUp;
        private Lookup _ageGroupLookup;

        //private RepositoryItemSpinEdit _spinEditInteger;
        //private RepositoryItemSpinEdit _spinEditDecimal;
        //private RepositoryItemSpinEdit _spinEditPercentage;

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
        public BindingList<VFSItem> VFSItems
        {
            get
            {
                return VFSObject == null ? null : VFSObject.VfsItems;
            }
        }

        /// <summary>
        /// Gets the current focused VFS item
        /// </summary>
        /// <returns></returns>
        public VFSItem FocusedVFSItem
        {
            get
            {
                return (VFSItem)gridViewVFSItems.GetFocusedRow();
            }
        }

        /// <summary>
        /// List of deleted VFS Items
        /// </summary>
        public BindingList<VFSItem> DeletedVFSItems
        {
            get
            {
                return _deletedVFSItems;
            }
        }

        public string ElevationChangeTypeId 
        {
            get
            {
                return _elevation;
            }
        }

        public string NormalChangeTypeId
        {
            get
            {
                return _normal;
            }
        }

        public string DecreaseChangeTypeId
        {
            get
            {
                return _decrease;
            }
        }

        #endregion

        #region Constructor

        public XtraUserControlVFSItems()
        {
            InitializeComponent();
        }

        #endregion

        #region Logic

        #region Generic

        /// <summary>
        /// Initialize custom components
        /// </summary>
        public void CustomeInitializeComponent()
        {
            
            //Add Images and Combobox Items for handling four factor images, we are doing it like this because the ImageList control
            //doesn't allow adding images from Project Resources and we don't want to store images the in the resource file of the form itself
            //Using the code below, we are adding images from project resources and then creating combobox items based on them.
            imageListChangeType.Images.Add("Elevation", Vital.UI.Properties.Resources.Elevation);
            imageListChangeType.Images.Add("Normal", Vital.UI.Properties.Resources.Normal);
            imageListChangeType.Images.Add("Decrease", Vital.UI.Properties.Resources.Decrease);

            _lookupsManager = new LookupsManager();
            _changeTypes = new List<string>();

            _elevation = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.VFSSourceItemChangeType, VFSSourceItemChangeType.Elevation)).Id.ToString("n0");
            _normal = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.VFSSourceItemChangeType, VFSSourceItemChangeType.Normal)).Id.ToString("n0");
            _decrease = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.VFSSourceItemChangeType, VFSSourceItemChangeType.Decrease)).Id.ToString("n0");

            _changeTypes.Add(_elevation);
            _changeTypes.Add(_normal);
            _changeTypes.Add(_decrease);

            repositoryItemImageComboBoxChangeType.Items.AddRange(new ImageComboBoxItem[] {
            new ImageComboBoxItem("Elevation", _elevation, 0),
            new ImageComboBoxItem("Normal", _normal, 1),
            new ImageComboBoxItem("Decrease", _decrease, 2)});

            //_spinEditInteger = new RepositoryItemSpinEdit();
            //_spinEditInteger.AutoHeight = false;
            //_spinEditInteger.DisplayFormat.FormatString = "n0";
            //_spinEditInteger.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //_spinEditInteger.EditFormat.FormatString = "n0";
            //_spinEditInteger.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //_spinEditInteger.Mask.EditMask = "n0";
            //_spinEditInteger.AllowNullInput = DefaultBoolean.True;
            ////_spinEditInteger.Mask.UseMaskAsDisplayFormat = true;
            //_spinEditInteger.IsFloatValue = false;
            //_spinEditInteger.Name = "_spinEditInteger";
            ////_spinEditInteger.MaxValue = new decimal(new[] {90,0,0,0});
            ////_spinEditInteger.MinValue = new decimal(new [] {10,0,0,-2147483648});

            //_spinEditDecimal = new RepositoryItemSpinEdit();
            //_spinEditDecimal.AutoHeight = false;
            //_spinEditDecimal.DisplayFormat.FormatString = "n2";
            //_spinEditDecimal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //_spinEditDecimal.EditFormat.FormatString = "n2";
            //_spinEditDecimal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //_spinEditDecimal.Mask.EditMask = "n2";
            ////_spinEditDecimal.Mask.UseMaskAsDisplayFormat = true;
            //_spinEditDecimal.AllowNullInput = DefaultBoolean.True;
            //_spinEditDecimal.Name = "_spinEditDecimal";
            ////_spinEditDecimal.MaxValue = new decimal(new[] {90,0,0,0});
            ////_spinEditDecimal.MinValue = new decimal(new [] {10,0,0,-2147483648});

            //_spinEditPercentage = new RepositoryItemSpinEdit();
            //_spinEditPercentage.AutoHeight = false;
            //_spinEditPercentage.DisplayFormat.FormatString = "p";
            //_spinEditPercentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //_spinEditPercentage.EditFormat.FormatString = "p";
            //_spinEditPercentage.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //_spinEditPercentage.Mask.EditMask = "p";
            ////_spinEditPercentage.Mask.UseMaskAsDisplayFormat = true;
            //_spinEditPercentage.AllowNullInput = DefaultBoolean.True;
            //_spinEditPercentage.Name = "_spinEditPercentage";
            ////_spinEditPercentage.MaxValue = new decimal(new[] {90,0,0,0});
            ////_spinEditPercentage.MinValue = new decimal(new [] {10,0,0,-2147483648});
        }

        /// <summary>
        /// Set binding.
        /// </summary>
        public void SetBinding()
        {
            _deletedVFSItems = new BindingList<VFSItem>();
            UiHelperClass.BindControl(gridControlVFSItems, VFSObject, () => VFSObject.VfsItems);
        }

        /// <summary>
        /// Fill Lookups
        /// </summary>
        public void FillLookup()
        {
            UiHelperClass.FillLookup(repositoryItemLookUpEditSection, UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.VFSSourceItemSection)));
            UiHelperClass.FillLookup(repositoryItemLookUpEditGroup, UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.VFSSourceItemGridGroup)));

            _ageGroupLookup = UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.VFSSourceItemGridGroup)).FirstOrDefault(l => l.Value.Equals(EnumNameResolver.Resolve(VFSSourceItemGroup.Age)));

        }


        /// <summary>
        /// Set the edit mode.
        /// </summary>
        /// <param name="isReadonly"></param>
        public void SetEditMode(bool isReadonly)
        {
            _isInEditMode = !isReadonly;
            gridViewVFSItems.OptionsBehavior.ReadOnly = isReadonly;
            repositoryItemImageComboBoxChangeType.Buttons[1].Enabled = !isReadonly;
        }

        /// <summary>
        /// Post editors.
        /// </summary>
        public void PostValues()
        {
            gridViewVFSItems.PostEditor();
            gridViewVFSItems.ValidateEditor();
            gridViewVFSItems.UpdateCurrentRow();
        }

        /// <summary>
        /// Clear binding.
        /// </summary>
        public void ClearBinding()
        {
            gridControlVFSItems.DataBindings.Clear();
            _deletedVFSItems.Clear();
        }

        #endregion

        #region VFS Items

        /// <summary>
        /// Add VFS Item to list
        /// </summary>
        private void AddVFSItem(bool isGroupRow)
        {
            VFSItem siblingVFSItem = null;

            if (isGroupRow)
            {
                siblingVFSItem = (VFSItem)gridViewVFSItems.GetRow(gridViewVFSItems.GetChildRowHandle(gridViewVFSItems.FocusedRowHandle, 0));
            }
            else
            {
                siblingVFSItem = FocusedVFSItem;
            }

            var frmSelectItem = new XtraFormSelectItem();

            if (frmSelectItem.ShowDialog() == DialogResult.Yes && frmSelectItem.SelectedItem != null)
            {
                var newVFSItem = new VFSItem()
                {
                    VFS = VFSObject,
                    Item = new Item() { Id = frmSelectItem.SelectedItem.Id, Name = frmSelectItem.SelectedItem.Name},
                    SectionLookup = siblingVFSItem.SectionLookup,
                    GroupLookup = siblingVFSItem.GroupLookup,
                    GridGroupLookup = siblingVFSItem.GridGroupLookup
                };

                VFSObject.VfsItems.Add(newVFSItem);
                gridViewVFSItems.FocusedRowHandle = GridControl.NewItemRowHandle;
                gridViewVFSItems.FocusedColumn = gridColumnValue1C;
                gridViewVFSItems.ShowEditor();
            }
        }

        /// <summary>
        /// Delete VFSItem
        /// </summary>
        private void DeleteVFSItem()
        {
            try
            {
                if (UiHelperClass.ShowConfirmQuestion("The selected VFS item will be deleted, are you sure?") == DialogResult.Yes)
                {
                    var focusedRow = FocusedVFSItem;

                    if (focusedRow != null && focusedRow.Id > 0)
                    {
                        //mark the object as deleted.
                        focusedRow.ObjectState = DomainEntityState.Deleted;
                        //add the deleted objects to a temporary list.
                        _deletedVFSItems.Add(focusedRow);
                    }

                    //delete the row 
                    gridViewVFSItems.DeleteRow(gridViewVFSItems.FocusedRowHandle);
                }
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Get the correct repositoryItem for the current cell
        /// </summary>
        /// <param name="vfsItem"></param>
        /// <param name="focusedColumn"></param>
        /// <returns></returns>
        private RepositoryItem GetRepositoryItem(VFSItem vfsItem, GridColumn focusedColumn)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return null;
            if (vfsItem == null || focusedColumn == null) return null;

            RepositoryItem repoItem = null;
            var enumType = VFSSourceItemValueType.None;
            Decimal min = 0;
            Decimal max = 0;

            if (!vfsItem.IsOnFlyItem &&
                (((focusedColumn == gridColumnValue1C || focusedColumn == gridColumnValue1P) && vfsItem.VFSItemSource.V1TypeLookup != null) ||
                ((focusedColumn == gridColumnValue2C || focusedColumn == gridColumnValue2P) && vfsItem.VFSItemSource.V2TypeLookup != null)))
            {
                if ((focusedColumn == gridColumnValue1C || focusedColumn == gridColumnValue1P) && vfsItem.VFSItemSource.V1TypeLookup != null)
                {
                    enumType =
                        EnumNameResolver.LookupAsEnum<VFSSourceItemValueType>(vfsItem.VFSItemSource.V1TypeLookup.Value);
                    min = vfsItem.VFSItemSource.V1Min;
                    max = vfsItem.VFSItemSource.V1Max;
                }
                else if ((focusedColumn == gridColumnValue2C || focusedColumn == gridColumnValue2P) && vfsItem.VFSItemSource.V2TypeLookup != null)
                {
                    enumType =
                        EnumNameResolver.LookupAsEnum<VFSSourceItemValueType>(vfsItem.VFSItemSource.V2TypeLookup.Value);
                    min = vfsItem.VFSItemSource.V2Min;
                    max = vfsItem.VFSItemSource.V2Max;
                }

                if (enumType == VFSSourceItemValueType.Decimal ||
                    enumType == VFSSourceItemValueType.Integer ||
                    enumType == VFSSourceItemValueType.Percentage)
                {
                    repoItem = new RepositoryItemSpinEdit();
                    repoItem.AutoHeight = false;
                    repoItem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    repoItem.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    ((RepositoryItemSpinEdit)repoItem).Mask.UseMaskAsDisplayFormat = true;
                    ((RepositoryItemSpinEdit)repoItem).AllowNullInput = DefaultBoolean.True;
                    ((RepositoryItemSpinEdit)repoItem).Buttons[0].Visible = false;
                    repoItem.Appearance.Options.UseTextOptions = true;
                    repoItem.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    repoItem.Name = "repoSpintEdit";

                    if (!(min == 0 & max == 0))
                    {
                        ((RepositoryItemSpinEdit)repoItem).MinValue = min;
                        ((RepositoryItemSpinEdit)repoItem).MaxValue = max;
                    }
                }
                else if (enumType == VFSSourceItemValueType.Lookup)
                {
                    repoItem = repositoryItemImageComboBoxChangeType;
                }

                if (repoItem != null)
                {
                    var format = string.Empty;

                    switch (enumType)
                    {
                        case VFSSourceItemValueType.Integer:
                            format = "n0";
                            ((RepositoryItemSpinEdit)repoItem).IsFloatValue = false;
                            break;
                        case VFSSourceItemValueType.Decimal:
                            format = "n2";
                            break;
                        case VFSSourceItemValueType.Percentage:
                            format = "p";
                            ((RepositoryItemSpinEdit)repoItem).Increment = new decimal(new[] { 1, 0, 0, 65536 });
                            break;
                    }

                    if (enumType == VFSSourceItemValueType.Decimal ||
                        enumType == VFSSourceItemValueType.Integer ||
                        enumType == VFSSourceItemValueType.Percentage)
                    {
                        //repoItem.DisplayFormat.FormatString = format;
                        //repoItem.EditFormat.FormatString = format;
                        ((RepositoryItemSpinEdit)repoItem).Mask.EditMask = format;
                    }
                }
            }

            return repoItem;
        }

        /// <summary>
        /// Switches current change type
        /// </summary>
        private void SwitchChangeType()
        {
            FocusedVFSItem.CurrentV1 = _changeTypes.NextOf(FocusedVFSItem.CurrentV1);
            gridViewVFSItems.RefreshEditor(true);
            gridViewVFSItems.RefreshRowCell(gridViewVFSItems.FocusedRowHandle, gridViewVFSItems.FocusedColumn);
        }

        /// <summary>
        /// Move focus to first column
        /// </summary>
        public void FocusFirstRow()
        {
            gridControlVFSItems.Focus();
            gridViewVFSItems.Focus();
            gridViewVFSItems.FocusedRowHandle = 0;
            gridViewVFSItems.FocusedColumn = gridColumnValue1C;
            gridViewVFSItems.ShowEditor();
            gridViewVFSItems.ActiveEditor.SelectAll();
        }
        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Handles user control loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraUserControlVFSItems_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraUserControlVFSItems_Load), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                
            }
        }

        /// <summary>
        /// Handles context menu opening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripVFSItems_Opening(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(contextMenuStripVFSItems_Opening), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (sender == null) return;

                e.Cancel = UiHelperClass.CancelClickAction(gridViewVFSItems);

                var inGroupRow = UiHelperClass.IsClickInGroupRowByMouse(gridViewVFSItems);
                var inRow = UiHelperClass.IsClickInRowByMouse(gridViewVFSItems);

                //If click in row, check if the add option will be enabled
                if (inGroupRow)
                {
                    if (gridViewVFSItems.IsGroupRow(gridViewVFSItems.FocusedRowHandle))
                    {
                        var childRowHandle = gridViewVFSItems.GetChildRowHandle(gridViewVFSItems.FocusedRowHandle, 0);
                      
                        if (gridViewVFSItems.IsGroupRow(childRowHandle))
                        {
                            toolStripMenuItemAdd.Enabled = false;
                            toolStripMenuItemDelete.Enabled = false;
                        }
                        else
                        {
                            contextMenuStripVFSItems.Enabled = _isInEditMode;
                            toolStripMenuItemAdd.Enabled = _isInEditMode;
                            toolStripMenuItemDelete.Enabled = false;
                        }
                    }                     
                }
                else if (inRow)
                {
                    toolStripMenuItemAdd.Enabled = _isInEditMode;
                    contextMenuStripVFSItems.Enabled = _isInEditMode;

                    if (contextMenuStripVFSItems.Enabled)
                    {
                        if (FocusedVFSItem != null)
                        {
                            toolStripMenuItemDelete.Enabled = FocusedVFSItem.IsOnFlyItem;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles context menu item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripVFSItems_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ToolStripItemClickedEventHandler(contextMenuStripVFSItems_ItemClicked), sender, e);
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
                    DeleteVFSItem();
                }
                else if (e.ClickedItem == toolStripMenuItemAdd)
                {
                    AddVFSItem(gridViewVFSItems.IsGroupRow(gridViewVFSItems.FocusedRowHandle));
                }
            }
        }

        /// <summary>
        /// Handles showing cell editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CancelEventHandler(gridViewVFSItems_ShowingEditor), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //Hide editors for cells that are disabled
                //var hideForSourceBasedItems = gridViewVFSItems.FocusedColumn == gridColumnItem && !FocusedVFSItem.IsOnFlyItem;//Show Item Search lookupedit dropdown only for on fly items

                var hideForCurrentV1 = gridViewVFSItems.FocusedColumn == gridColumnValue1C && 
                                       !FocusedVFSItem.IsOnFlyItem &&
                                       !FocusedVFSItem.VFSItemSource.HasCurrentV1;

                var hideForCurrentV2 = gridViewVFSItems.FocusedColumn == gridColumnValue2C && 
                                       (FocusedVFSItem.IsOnFlyItem ||
                                       (!FocusedVFSItem.VFSItemSource.HasCurrentV2));

                if (hideForCurrentV1 || hideForCurrentV2)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Handles assigning cell editors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new CustomRowCellEditEventHandler(gridViewVFSItems_CustomRowCellEdit), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
                var currentVFSItem = gridViewVFSItems.GetRow(e.RowHandle) as VFSItem;
                var repoItem = GetRepositoryItem(currentVFSItem, e.Column);

                if (repoItem != null)
                {
                    e.RepositoryItem = repoItem;
                }
            }
        }

        /// <summary>
        /// Handles clicking a button in the Image combobox for the change type rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemImageComboBoxChangeType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //Here we handle the switching of the selected change type lookup value
            if (_isInEditMode && e.Button.Kind == ButtonPredefines.Glyph && (string)e.Button.Tag == "Switch")
            {
                SwitchChangeType();
            }
        }

        /// <summary>
        /// Handles showing button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_ShownEditor(object sender, EventArgs e)
        {
            //This handler is important to handle the case where we want to show the Switch buttons inside the ImageComboBoxEdit all the time
            //without showing the buttons inside other controls in other rows, to do that, we did the following:
            //1- In the gridColumnCurrentV1 we set the property ShowButton to Always
            //2- When creating a spinEdit repositoryItem, we hide the button for the spin
            //3- When Creaing a ImageComboBoxEdit we hide the dropdown button
            //4- In this case only the switch button will always be visible
            //5- In the event here we show buttons needed when editor becomes active
            if (gridViewVFSItems.FocusedColumn == gridColumnValue1C ||
                gridViewVFSItems.FocusedColumn == gridColumnValue2C)
            {
                if (gridViewVFSItems.ActiveEditor is SpinEdit)
                {
                    var editor = gridViewVFSItems.ActiveEditor as SpinEdit;
                    editor.Properties.Buttons[0].Visible = true;
                }
                else if (gridViewVFSItems.ActiveEditor is ImageComboBoxEdit)
                {
                    var editor = gridViewVFSItems.ActiveEditor as ImageComboBoxEdit;
                    editor.Properties.Buttons[0].Visible = true;
                }
            }
            //else if (gridViewVFSItems.FocusedColumn == gridColumnItem)
            //{
            //    if (gridViewVFSItems.ActiveEditor is SearchLookUpEdit)
            //    {
            //        var editor = gridViewVFSItems.ActiveEditor as SearchLookUpEdit;
            //        editor.Properties.Buttons[0].Visible = true;
            //    }
            //}
        }

        /// <summary>
        /// Show dash for cells that are disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            var currentVFSItem = gridViewVFSItems.GetRow(e.RowHandle) as VFSItem;

            if (currentVFSItem != null)
            {
                if (currentVFSItem.IsOnFlyItem)
                {
                    if (e.Column == gridColumnValue2C || e.Column == gridColumnValue2P)
                    {
                        e.DisplayText = "-";
                    }
                }
                else
                {
                    if ((e.Column == gridColumnValue1C && !currentVFSItem.VFSItemSource.HasCurrentV1) ||
                        (e.Column == gridColumnValue2C && !currentVFSItem.VFSItemSource.HasCurrentV2) ||
                        (e.Column == gridColumnValue1P && !currentVFSItem.VFSItemSource.HasPreviousV1) ||
                        (e.Column == gridColumnValue2P && !currentVFSItem.VFSItemSource.HasPreviousV2))
                    {
                        e.DisplayText = "-";
                    }
                }
            }
        }

        /// <summary>
        /// Handles key down click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                _arrowNavigation = true;
                _navigatingUp = e.KeyCode == Keys.Up;
            }
            else if (_isInEditMode && (e.KeyCode == Keys.Insert || (e.Control && e.KeyCode == Keys.I) || e.KeyCode == Keys.Oemtilde))
            {
                var allowAdd = true;

                if (gridViewVFSItems.IsGroupRow(gridViewVFSItems.FocusedRowHandle))
                {
                    var childRowHandle = gridViewVFSItems.GetChildRowHandle(gridViewVFSItems.FocusedRowHandle, 0);

                    if (gridViewVFSItems.IsGroupRow(childRowHandle))
                    {
                        allowAdd = false;
                    }
                }

                if (allowAdd)
                {
                    AddVFSItem(false);
                }
            }
            else if (_isInEditMode && e.KeyCode == Keys.Delete)
            {
                if (gridViewVFSItems.FocusedColumn == gridColumnItem && 
                    !(gridViewVFSItems.IsGroupRow(gridViewVFSItems.FocusedRowHandle) || !FocusedVFSItem.IsOnFlyItem))
                {
                    DeleteVFSItem();
                }
            }
        }

        /// <summary>
        /// Handling of arrow based navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //The logic below is required to help jump over groups when navigating by up or down arrows
            if (_arrowNavigation)
            {
                var viewInfo = gridViewVFSItems.GetViewInfo() as GridViewInfo;
                if (e.FocusedRowHandle == -2)
                {
                    _arrowNavigation = false;
                    gridViewVFSItems.FocusedRowHandle = e.PrevFocusedRowHandle;
                }
                else
                {
                    if (!gridViewVFSItems.IsGroupRow(e.FocusedRowHandle))
                    {
                        _arrowNavigation = false;
                        gridViewVFSItems.FocusedRowHandle = e.PrevFocusedRowHandle;
                    }

                    if (_navigatingUp)
                    {
                        gridViewVFSItems.MovePrev();
                    }
                    else
                    {
                        gridViewVFSItems.MoveNext();
                    }
                }
            }
        }

        /// <summary>
        /// Handle cell value changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //Notify object of the changes in the property
            //Here we called notify instead of posting value to the datasource because posting it will post value regardless of the mask used
            //and it would be more work to handle that where we only need to notify about the value change.
            FocusedVFSItem.NotifyPropertyChanged(ExpressionHelper.GetPropertyName(() => FocusedVFSItem.CurrentV1));
        }

        /// <summary>
        /// Handles change type image combo box key down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemImageComboBoxChangeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isInEditMode && e.KeyCode == Keys.Space)
            {
                SwitchChangeType();
            }
        }

        /// <summary>
        /// Handle showing the delete tooltip for on fly items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolTipControllerVFSItems_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControlVFSItems) return;
            ToolTipControlInfo info = null;

            var sTooltipItemDelete = new SuperToolTip();
            VFSItem currentVFSItem = null;

            try
            {
                var hitInfo = gridViewVFSItems.CalcHitInfo(e.ControlMousePosition);

                if (hitInfo.HitTest == GridHitTest.RowCell && hitInfo.Column == gridColumnItem)
                {
                    currentVFSItem = gridViewVFSItems.GetRow(hitInfo.RowHandle) as VFSItem;

                    if (currentVFSItem != null && currentVFSItem.IsOnFlyItem)
                    {
                        info = new ToolTipControlInfo(GridHitTest.RowIndicator.ToString() + hitInfo.RowHandle.ToString(), "Row Handle: " + hitInfo.RowHandle.ToString());
                        var item1 = new ToolTipItem();
                        item1.Text = "Use Delete key to remove row";
                        sTooltipItemDelete.Items.Add(item1);
                    }
                }
                if (currentVFSItem != null && currentVFSItem.IsOnFlyItem)
                {
                    info = new ToolTipControlInfo(hitInfo.HitTest, "");
                    info.SuperTip = sTooltipItemDelete;
                }
            }
            finally
            {
                if (currentVFSItem != null && currentVFSItem.IsOnFlyItem)
                {
                    e.Info = info;
                }
            }
        }

        /// <summary>
        /// Custom draw for group to handle actual age.
        /// </summary>
        private void gridViewVFSItems_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new RowObjectCustomDrawEventHandler(gridViewVFSItems_CustomDrawGroupRow), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                if (_ageGroupLookup == null)
                    return;
                
                var view = sender as GridView;

                if (view == null)
                    return;

                var info = e.Info as GridGroupRowInfo;

                if (info == null)
                    return;

                var handle = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
                int groupLookupId;

                if (!int.TryParse(view.GetRowCellValue(handle, gridColumnGroup).ToString(), out groupLookupId))
                    return;

                if (info.Column != gridColumnGroup || groupLookupId != _ageGroupLookup.Id || VFSObject.Patient == null ||
                    !VFSObject.Patient.DateOfBirth.HasValue)
                    return;
                
                var age = VFSObject.Patient.DateOfBirth.Value.Age();
                var oGroupText = new StringBuilder();
                oGroupText.Append(string.Format("Group: {0} - {1}: {2} ", _ageGroupLookup.Value, StaticKeys.ActualAge, age));
                oGroupText.Append(string.Format(StaticKeys.YearsPeriodName.ToLower(), age > 1 ? "s" : string.Empty));

                info.GroupText = oGroupText.ToString();
            }
        }


        /// <summary>
        /// Handles VFS Items grid data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewVFSItems_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            //if (IsHandleCreated && InvokeRequired)
            //{
            //    try
            //    {
            //        if (IsDisposed) return;
            //        Invoke(new CustomColumnDataEventHandler(gridViewVFSItems_CustomUnboundColumnData), sender, e);
            //    }
            //    catch
            //    {
            //        // Nothing to do, form had been disposed.
            //    }
            //}
            //else
            //{
            //    var currentVFSItem = e.Row as VFSItem;

            //    if (currentVFSItem != null && !currentVFSItem.IsOnFlyItem &&
            //        ((e.Column == gridColumnValue1C && currentVFSItem.VFSItemSource.V1TypeLookup != null) ||
            //        (e.Column == gridColumnValue2C && currentVFSItem.VFSItemSource.V2TypeLookup != null)))
            //    {
            //        var enumType = VFSSourceItemValueType.None;

            //        if (e.Column == gridColumnValue1C)
            //        {
            //            enumType =
            //            EnumNameResolver.LookupAsEnum<VFSSourceItemValueType>(currentVFSItem.VFSItemSource.V1TypeLookup.Value);
            //        }
            //        else if (e.Column == gridColumnValue2C)
            //        {
            //            enumType =
            //            EnumNameResolver.LookupAsEnum<VFSSourceItemValueType>(currentVFSItem.VFSItemSource.V2TypeLookup.Value);
            //        }

            //        if (e.IsGetData)
            //        {
            //            var value = string.Empty;

            //            if (e.Column == gridColumnValue1C)
            //            {
            //                value = currentVFSItem.CurrentV1;
            //            }
            //            else if (e.Column == gridColumnValue2C)
            //            {
            //                value = currentVFSItem.CurrentV2;
            //            }

            //            if (!string.IsNullOrEmpty(value))
            //            {
            //                switch (enumType)
            //                {
            //                    case VFSSourceItemValueType.Integer:
            //                        e.Value = int.Parse(value);
            //                        break;
            //                    case VFSSourceItemValueType.Decimal:
            //                        e.Value = decimal.Parse(value);
            //                        break;
            //                    case VFSSourceItemValueType.Percentage:
            //                        e.Value = decimal.Parse(value);
            //                        break;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (e.Column == gridColumnValue1C)
            //            {
            //                currentVFSItem.CurrentV1 = e.Value.ToString();
            //            }
            //            else if (e.Column == gridColumnValue2C)
            //            {
            //                currentVFSItem.CurrentV2 = e.Value.ToString();
            //            }
            //        }


            //    }
            //}
        }

        private void gridViewVFSItems_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //var currentVFSItem = gridViewVFSItems.GetRow(e.RowHandle) as VFSItem;

            //if (currentVFSItem != null)
            //{
            //    if (currentVFSItem.IsOnFlyItem)
            //    {
            //        if (e.Column == gridColumnValue2C || e.Column == gridColumnValue2P)
            //        {
            //            e.Appearance.BackColor = Color.LightGray;
            //        }
            //    }
            //    else
            //    {
            //        if ((e.Column == gridColumnValue1C && !currentVFSItem.VFSItemSource.HasCurrentV1) ||
            //            (e.Column == gridColumnValue2C && !currentVFSItem.VFSItemSource.HasCurrentV2) ||
            //            (e.Column == gridColumnValue1P && !currentVFSItem.VFSItemSource.HasPreviousV1) ||
            //            (e.Column == gridColumnValue2P && !currentVFSItem.VFSItemSource.HasPreviousV2))
            //        {
            //            e.Appearance.BackColor = Color.LightGray;
            //        }
            //    }
            //}
        }

        #endregion

    }
}
