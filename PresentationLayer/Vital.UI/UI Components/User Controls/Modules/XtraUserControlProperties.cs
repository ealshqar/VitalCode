using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.User_Controls.Modules
{
    public partial class XtraUserControlProperties : XtraUserControl
    {

        #region Private Members

        private readonly PropertiesManager _propertiesManager;
        private readonly LookupsManager _lookupsManager;
        private BindingList<Property> _availableProperties;

        private bool _readOnly;
        private bool _showNewRow;
        
        #endregion

        #region Events

        /// <summary>
        /// Event raise when new property row start initializing.
        /// </summary>
        public event CustomPropertyRelationalInitHandler CustomPropertyRelationalInit;

        /// <summary>
        /// Event raise when a property being deleted. 
        /// </summary>
        public event OnDeletePropertyHandler PropertyDeleted;

        #endregion

        #region Constoroctors

        public XtraUserControlProperties()
        {
            _propertiesManager = new PropertiesManager();
            _lookupsManager = new LookupsManager();

            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PropertyApplicableType enum value.
        /// </summary>
        public List<ApplicableTypesEnum> PropertyApplicableTypes
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the Properties List.
        /// Warning : [This Properties list objects should be inherited from DomainEntityPropertyRelational].
        /// </summary>
        public object Properties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets  the ReadOnly value. 
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                SetEditMode(value);
                _readOnly = value;
            }
        }

        /// <summary>
        /// Gets or sets the property column caption.
        /// </summary>
        public string PropertyColumnCaption
        {
            get { return gridColumnProperty.Caption; }
            set { gridColumnProperty.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the value column caption.
        /// </summary>
        public string ValueColumnCaption
        {
            get { return gridColumnValue.Caption; }
            set { gridColumnValue.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the property column read only.
        /// </summary>
        public bool PropertyColumnReadOnly
        {
            get { return gridColumnProperty.OptionsColumn.AllowEdit; }
            set
            {
                gridColumnProperty.OptionsColumn.AllowEdit = !value;
            }
        }

        /// <summary>
        /// Gets or sets show new row value.
        /// </summary>
        public bool ShowNewRow
        {
            get
            {
                return _showNewRow;
            }
            set
            {
                _showNewRow = value;
                gridViewProperties.OptionsView.NewItemRowPosition = _showNewRow == false ? NewItemRowPosition.None : NewItemRowPosition.Bottom;
            }
        }

        /// <summary>
        /// Gets or sets allow delete value.
        /// </summary>
        public bool AllowDelete
        {
            get { return gridControlProperties.ContextMenuStrip != null; }
            set { gridControlProperties.ContextMenuStrip = value ? contextMenuStripProperties : null; }
        }

        #endregion
        
        #region UI Init

        /// <summary>
        /// Sets Binding.
        /// </summary>
        private bool SetBinding()
        {
            if (!FillLookups())
                return false;

            UiHelperClass.SetReflectionBaseViewProperties(gridViewProperties);

            if (Properties == null)
                Properties = new object();

            gridControlProperties.DataSource = Properties;

            return true;
        }

        /// <summary>
        /// Fills the lookups.
        /// </summary>
        private bool FillLookups()
        {
            if (PropertyApplicableTypes == null)
                return false;

            var propertyApplicableTypeLookups =
                UiHelperClass.GetLookupByTypeFromCache(LookupsFilter.As(LookupTypes.ApplicableType));

            if (propertyApplicableTypeLookups == null)
                return false;

            var applicableTypesToUse =
                propertyApplicableTypeLookups.Where(ap => PropertyApplicableTypes
                    .Any(t => EnumNameResolver.LookupAsEnum<ApplicableTypesEnum>( ap.Value) == t));

            _availableProperties = _propertiesManager.GetProperties(new PropertiesFilter 
            { ApplicableTypeIds = applicableTypesToUse.Select(ap => ap.Id).ToArray() });

            repositoryItemLookUpEditProperties.DataSource = _availableProperties;

            repositoryItemLookUpEditProperties.ForceInitialize();

            return true;
        }

        /// <summary>
        /// Bind the user control.
        /// </summary>
        public bool Bind(object properties, List<ApplicableTypesEnum> propertyApplicableType)
        {
            try
            {
                Properties = properties;
                PropertyApplicableTypes = propertyApplicableType;

                return SetBinding();
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
                return false;
            }
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Sets the edit mode.
        /// </summary>
        /// <param name="isReadOnly"></param>
        private void SetEditMode(bool isReadOnly)
        {
            gridViewProperties.OptionsBehavior.ReadOnly = isReadOnly;
            gridViewProperties.OptionsBehavior.Editable = !isReadOnly;
            gridViewProperties.OptionsView.NewItemRowPosition = !_showNewRow || isReadOnly ? NewItemRowPosition.None : NewItemRowPosition.Bottom;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Post editors.
        /// </summary>
        public void PostValues()
        {
            gridViewProperties.PostEditor();
            gridViewProperties.ValidateEditor();
            gridViewProperties.UpdateCurrentRow();
        }

        /// <summary>
        /// Validate the property value.
        /// </summary>
        /// <param name="propertyRelational"></param>
        /// <returns></returns>
        private bool isValidPropertyValue(DomainEntityPropertyRelational propertyRelational)
        {
            if (propertyRelational.ValueTypeLookup == null || string.IsNullOrEmpty(propertyRelational.ValueTypeLookup.Value))
                return false;

            var valueTypeEnum = EnumNameResolver.LookupAsEnum<ValueTypes>(propertyRelational.ValueTypeLookup.Value);

            if (propertyRelational.Value == null || string.IsNullOrEmpty(propertyRelational.Value.ToString()))
                return false;

            if (valueTypeEnum == ValueTypes.List)
            {
                int valueAsInt;
                var isInt = int.TryParse(propertyRelational.Value.ToString(), out valueAsInt);

                if (isInt && valueAsInt == 0)
                    return false;
            }

            return true;

        }

        #endregion

        #region Handlers

        /// <summary>
        /// Init new property row.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewProperties_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                var view = sender as GridView;

                if (view == null) return;

                var propertyRelational = view.GetRow(e.RowHandle) as DomainEntityPropertyRelational;

                if (propertyRelational != null && _availableProperties != null)
                {

                    propertyRelational.Property = new Property
                                                      {
                                                          ValueTypeLookup = new Lookup()
                                                      };

                }

                if (CustomPropertyRelationalInit != null)
                    CustomPropertyRelationalInit(this, propertyRelational);

                
            }
            catch (Exception ex)
            {
                UiHelperClass.ShowError(string.Empty, ex);
            }
        }

        /// <summary>
        /// Validate the editor for duplication Properties 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewProperties_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                var view = sender as GridView;

                if (view == null || e.Value == null) 
                    return;

                //Avoid validation on the value column.
                if(view.FocusedColumn == gridColumnValue)
                    return;

                for (var index = 0; index < gridViewProperties.DataRowCount; index++)
                {
                    if(index == gridViewProperties.FocusedRowHandle) continue;

                    var propertyRelational = gridViewProperties.GetRow(index) as DomainEntityPropertyRelational;

                    if (propertyRelational == null) continue;

                    int valueTemp;

                    if (!int.TryParse(e.Value.ToString(), out valueTemp)) 
                        return;

                    if (valueTemp != propertyRelational.Property.Id) continue;

                    e.Valid = false;
                    e.ErrorText = StaticKeys.PropertyAlreadyExists;
                    break;
                }

            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }

        }

        /// <summary>
        /// Handel the Hide for the editor on the Properties grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void gridViewProperties_HiddenEditor(object sender, EventArgs e)
        {
            try
            {
                var view = sender as GridView;

                if (view != null)
                {
                    view.PostEditor();

                    var propertyRelational = view.GetFocusedRow() as DomainEntityPropertyRelational;

                    if (propertyRelational == null || propertyRelational.Property == null) return;

                    if (propertyRelational.Property.Id == 0)
                    {
                        view.CancelUpdateCurrentRow();
                    }

                }
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }

        }

        /// <summary>
        /// Validate the modified or added row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewProperties_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                var view = sender as GridView;

                if (view == null || e.Row == null) return;

                var foucsedPropertyRelation = e.Row as DomainEntityPropertyRelational;

                if (foucsedPropertyRelation == null) return;

                if (!isValidPropertyValue(foucsedPropertyRelation))
                {
                    e.Valid = false;
                    var onOrderCol = view.Columns[StaticKeys.UnboundValueFieldName];
                    view.SetColumnError(onOrderCol, StaticKeys.ValidationMessageBlankFieldOptinal);
                }

            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Prevent the validation message box from show before validation error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewProperties_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        /// <summary>
        /// Handel the property value changed to fill the property inside the DomainEntityPropertyRelational object instead of its Id only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewProperties_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (!e.Column.FieldName.Equals(StaticKeys.PropertyId) || e.Value == null) return;

                var relationalProperty = gridViewProperties.GetRow(e.RowHandle) as DomainEntityPropertyRelational;

                if (relationalProperty == null) return;

                var matchedDatasourceProperty = _availableProperties.FirstOrDefault(p => p.Id == (int) e.Value);

                relationalProperty.Property = matchedDatasourceProperty == null ? null : (Property)matchedDatasourceProperty.Clone();

                relationalProperty.UiItemRepository = null;

                relationalProperty.Value = null;

            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(exception.Message, exception);
            }
        }

        /// <summary>
        /// handles the click on the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (sender != null)
            {
                ((ContextMenuStrip) sender).Hide();

                if (sender == contextMenuStripProperties)
                {
                    if (e.ClickedItem == deleteToolStripMenuItemDeleteProperty)
                    {
                        DeleteProperty();
                    }
                }
            }
       
        }

        /// <summary>
        /// Delete a selected property row.
        /// </summary>
        private void DeleteProperty()
        {
            var property = gridViewProperties.GetRow(gridViewProperties.FocusedRowHandle) as DomainEntityPropertyRelational;

            if(property == null) 
                return;

            gridViewProperties.DeleteRow(gridViewProperties.FocusedRowHandle);

            if (property.ObjectState == DomainEntityState.New) 
                return;

            property.ObjectState = DomainEntityState.Deleted;

            if (PropertyDeleted != null)
                PropertyDeleted(this, property);

            gridViewProperties.RefreshData();
        }

        /// <summary>
        /// Handles the opening of the context menu for a grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (!ReadOnly)
            {
                if (sender == contextMenuStripProperties)
                {
                    e.Cancel = UiHelperClass.CancelClickAction(gridViewProperties);
                    
                    var isEnabled = UiHelperClass.IsClickInRowByMouse(gridViewProperties);

                    deleteToolStripMenuItemDeleteProperty.Enabled = isEnabled;
                }
            }
            else
            {
                deleteToolStripMenuItemDeleteProperty.Enabled = false;
            }

        }

        /// <summary>
        /// Handles editor showing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewProperties_ShowingEditor(object sender, CancelEventArgs e)
        {
            var property = gridViewProperties.GetRow(gridViewProperties.FocusedRowHandle) as DomainEntityPropertyRelational;

            if (property == null)
                return;

            if (gridViewProperties.FocusedColumn.FieldName.Equals(StaticKeys.PropertyId) && isValidPropertyValue(property))
            {
                e.Cancel = true;
            }
        }

        #endregion        
    }

    #region Event Handler Delegates

    /// <summary>
    /// Delegate for CustomPropertyRelationalInit handler method.
    /// </summary>
    public delegate void CustomPropertyRelationalInitHandler(object sender, DomainEntityPropertyRelational propertyRelational);

    /// <summary>
    /// Delegate for OnDeleteProperty handler method.
    /// </summary>
    public delegate void OnDeletePropertyHandler(object sender, DomainEntityPropertyRelational propertyRelational);

    #endregion
}
