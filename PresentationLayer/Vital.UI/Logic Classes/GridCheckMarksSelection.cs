using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Vital.UI.Logic_Classes
{
	public class GridCheckMarksSelection
    {
        #region Fields

        private GridView _view;
        private readonly GridColumn _column;
        private readonly RepositoryItemCheckEdit _edit;
        private readonly ArrayList _selection;

        #endregion

        #region Constructor
      
        /// <summary>
        /// Constructor
        /// </summary>
        public GridCheckMarksSelection(GridView view, GridColumn gridColumn, RepositoryItemCheckEdit checkEdit)
        {
            _selection = new ArrayList();
            _column = gridColumn;
            _edit = checkEdit;
            View = view;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the GridView value.
        /// </summary>
        public GridView View
        {
            get
            {
                return _view;
            }
            set
            {
                if (_view == value || value == null) return;

                if (_view != null)
                {
                    _view.RowClick -= view_RowClick;
                    _view.CustomDrawColumnHeader -= View_CustomDrawColumnHeader;
                    _view.CustomDrawGroupRow -= View_CustomDrawGroupRow;
                    _view.CustomUnboundColumnData -= view_CustomUnboundColumnData;
                    _view.MouseDown -= view_MouseDown;
                    _view = null;    
                }
                        
                _selection.Clear();
                _view = value;
                _view.RowClick += view_RowClick;
                _edit.EditValueChanged += edit_EditValueChanged;
                _view.CustomDrawColumnHeader += View_CustomDrawColumnHeader;
                _view.CustomDrawGroupRow += View_CustomDrawGroupRow;
                _view.CustomUnboundColumnData += view_CustomUnboundColumnData;
                _view.MouseDown += view_MouseDown; // clear selection
            }
        }

        /// <summary>
        /// Gets the CheckMarkColumn value.
        /// </summary>
        public GridColumn CheckMarkColumn
        {
            get
            {
                return _column;
            }
        }

        /// <summary>
        /// Gets the SelectedCount Value.
        /// </summary>
        public int SelectedCount
        {
            get
            {
                return _selection.Count;
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Gets selected index in the selection list.
        /// </summary>
        public int GetSelectedIndex(int rowHandle)
        {
            return _selection.IndexOf(rowHandle);
        }

        /// <summary>
        /// Check if group row selected for the passed row handler.
        /// </summary>
        public bool IsGroupRowSelected(int rowHandle)
        {
            for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
            {
                var row = _view.GetChildRowHandle(rowHandle, i);

                if (_view.IsGroupRow(row))
                {
                    if (!IsGroupRowSelected(row)) return false;
                }
                else
                {
                    if (!IsRowSelected(row)) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check selection for the passed row handler.
        /// </summary>
        public bool IsRowSelected(int rowHandle)
        {
            if (_view.IsGroupRow(rowHandle))
                return IsGroupRowSelected(rowHandle);
            return GetSelectedIndex(rowHandle) != -1;
        }

        /// <summary>
        /// Gets the selected row handler for the passed selected index.
        /// </summary>
        public int GetSelectedRowHandle(int index)
        {
            return (int)_selection[index];
        }

        /// <summary>
        /// Draw the checkboxes on the grid.
        /// </summary>
        protected void DrawCheckBox(Graphics g, Rectangle r, bool isChecked)
        {
            var info = _edit.CreateViewInfo() as CheckEditViewInfo;
            var painter = _edit.CreatePainter() as CheckEditPainter;
            if (info != null)
            {
                info.EditValue = isChecked;
                info.Bounds = r;
                info.CalcViewInfo(g);
                var args = new ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
                if (painter != null) painter.Draw(args);
                args.Cache.Dispose();
            }            
        }

        /// <summary>
        /// Clear the selection.
        /// </summary>
        public void ClearSelection()
        {
            _selection.Clear();
            Invalidate();
        }

        /// <summary>
        /// Invalidate current editor.
        /// </summary>
        private void Invalidate()
        {
            _view.CloseEditor();
            _view.BeginUpdate();
            _view.EndUpdate();
        }

        /// <summary>
        /// Select all the rows.
        /// </summary>
        public void SelectAll()
        {
            _selection.Clear();

            for (var i = 0; i < _view.DataRowCount; i++)  // slow
                _selection.Add(i);
                
            Invalidate();
        }

        /// <summary>
        /// Select/Unselect a group.
        /// </summary>
        public void SelectGroup(int rowHandle, bool select)
        {
            if (IsGroupRowSelected(rowHandle) && select) return;
            for (int i = 0; i < _view.GetChildRowCount(rowHandle); i++)
            {
                int childRowHandle = _view.GetChildRowHandle(rowHandle, i);
                if (_view.IsGroupRow(childRowHandle))
                    SelectGroup(childRowHandle, select);
                else
                    SelectRow(childRowHandle, select, false);
            }
            Invalidate();
        }

        /// <summary>
        /// Select/Unselect a row.
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="select"></param>
        public void SelectRow(int rowHandle, bool select)
        {
            SelectRow(rowHandle, select, true);
        }

        /// <summary>
        /// Select/Unselect a row.
        /// </summary>
        private void SelectRow(int rowHandle, bool select, bool invalidate)
        {
            if (IsRowSelected(rowHandle) == select) return;
            if (select)
                _selection.Add(rowHandle);
            else
                _selection.Remove(rowHandle);
            if (invalidate)
            {
                Invalidate();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handel on row clicked action.
        /// </summary>
        private void view_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks != 1 || e.Button != MouseButtons.Left) return;

            var pt = _view.GridControl.PointToClient(Control.MousePosition);

            var info = _view.CalcHitInfo(pt);

            if (info.InRow && info.Column != _column && _view.IsDataRow(info.RowHandle))
            {
                SelectRow(info.RowHandle, !IsRowSelected(info.RowHandle));
            }
        }

        /// <summary>
        /// Handel on mouse down event.
        /// </summary>
        private void view_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left)
            {
                Point pt = _view.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo info = _view.CalcHitInfo(pt);

                if (info.InColumn && info.Column == _column)
                {
                    if (SelectedCount == _view.DataRowCount)
                        ClearSelection();
                    else
                        SelectAll();
                }

                if (info.InRow && _view.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton)
                {
                    bool selected = IsGroupRowSelected(info.RowHandle);
                    SelectGroup(info.RowHandle, !selected);
                }
            }
        }

        /// <summary>
        /// Handel CustomDrawColumnHeader event on the gridView.
        /// </summary>
        private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == _column)
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, SelectedCount == _view.DataRowCount);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handel CustomDrawGroupRow event on the gridView.
        /// </summary>
        private void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            var info = e.Info as GridGroupRowInfo;

            if (info != null) info.GroupText = "         " + info.GroupText.TrimStart();
            e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
            e.Painter.DrawObject(e.Info);

            if (info != null)
            {
                Rectangle r = info.ButtonBounds;
                r.Offset(r.Width * 2, 0);
                DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle));
            }
            e.Handled = true;
        }

        /// <summary>
        /// Handel CustomUnboundColumnData event on the gridView.
        /// </summary>
        private void view_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column != CheckMarkColumn) return;

            if (e.IsGetData)
            {
                e.Value = IsRowSelected(View.GetRowHandle(e.ListSourceRowIndex));
            }
            else
            {
                SelectRow(View.GetRowHandle(e.ListSourceRowIndex), (bool) e.Value);
            }
        }

        /// <summary>
        /// Handel the EditValueChanged on the itemRepository.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
	    private void edit_EditValueChanged(object sender, EventArgs e)
        {
            _view.PostEditor();
        }

        #endregion        
	}
}
