using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.Forms.BaseForms;

namespace Vital.UI.UI_Components.BaseForms
{
    public partial class VitalBaseForm : DevExpress.XtraEditors.XtraForm
    {
        #region Events

        public delegate void OnFormStatusChanged(FormStatusEnum newStatus);
        public event OnFormStatusChanged FormStatusChanged;

        #endregion
         
        #region Fields

        private bool _isNew;
        private bool _isInEditMode;
        
        private FormStatusEnum _formStatus;
        private FormStatusEnum? _oldFormStatus;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the IsFormLoaded.
        /// </summary>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Get or set the IsInEditMode.
        /// </summary>
        public bool IsInEditMode { get { return _isInEditMode; }}

        /// <summary>
        /// Get or set IsClosing value.
        /// </summary>
        public bool IsClosing { get; set; }

        /// <summary>
        /// Sets or gets the form status changed.
        /// </summary>
        public FormStatusEnum FormStatus
        {
            get
            {
                return _formStatus;
            }
            set
            {
                if(_formStatus == value) return;
                if (value == FormStatusEnum.Locked)
                    _oldFormStatus = _formStatus;
                
                _formStatus = value;
                if (FormStatusChanged != null)
                    FormStatusChanged(_formStatus);
            }
        }

        /// <summary>
        /// Show the edit hint when the form is disabled.
        /// </summary>
        public bool ShowEditHint { get; set; }

        #endregion

        #region UI Additions

        /// <summary>
        /// Get or set the BarCrud
        /// </summary>
        public Bar BarCrud
        {
            get { return barCRUD; }
            set { barCRUD = value; }
        }

        /// <summary>
        /// Get or set the BarManager
        /// </summary>
        public BarManager BarManager
        {
            get { return barManager; }
            set { barManager = value; }
        }

        /// <summary>
        /// Set the Visibility of Edit button.
        /// </summary>
        public BarItemVisibility ShowEditButton
        {
            get { return barButtonItemEdit.Visibility; }
            set { barButtonItemEdit.Visibility = value; }
        }

        /// <summary>
        /// Set the Visibility of Disable button.
        /// </summary>
        public BarItemVisibility ShowDisableButton
        {
            get { return barButtonItemDisable.Visibility; }
            set { barButtonItemDisable.Visibility = value; }
        }

        /// <summary>
        /// Set the Visibility of Save button.
        /// </summary>
        public BarItemVisibility ShowSaveButton
        {
            get { return barButtonItemSave.Visibility; }
            set { barButtonItemSave.Visibility = value; }
        }

        /// <summary>
        /// Set the Visibility of Save and close button.
        /// </summary>
        public BarItemVisibility ShowSaveAndCloseButton
        {
            get { return barButtonItemSaveAndClose.Visibility; }
            set { barButtonItemSaveAndClose.Visibility = value; }
        }

        /// <summary>
        /// Set the Visibility of Save and close button.
        /// </summary>
        public BarItemVisibility ShowDeleteButton
        {
            get { return barButtonItemDelete.Visibility; }
            set { barButtonItemDelete.Visibility = value; }
        }

        /// <summary>
        /// Set the Visibility of Undo button.
        /// </summary>
        public BarItemVisibility ShowUndoButton
        {
            get { return barButtonItemUndo.Visibility; }
            set { barButtonItemUndo.Visibility = value; }
        }

        /// <summary>
        /// [Move]Add new bar button to the base bar manager.
        /// </summary>
        /// <param name="name">Item Name.</param>
        /// <param name="caption">The button caption.</param>
        /// <param name="enabled">Is the button enabled.</param>
        /// <param name="glyph">The button glyph.</param>
        /// <param name="id">Item Id.</param>
        /// <param name="possition">The position in the bar.</param>
        /// <param name="beginGroup">Begin new Group.</param>
        /// <param name="itemShortcut">The Item shortcut.</param>
        /// <param name="showTooltip">Show the tooltip.</param>
        /// <param name="toolTipUpperText">Upper tooltip text.</param>
        /// <param name="toolTipCenterText">Center tooltip text.</param>
        /// <param name="toolTipBottomText">Bottom tooltip text.</param>
        /// <param name="usetoolTipSeparatorItem">Use the tooltip separator.</param>
        public BarButtonItem AddBarButtonItem(string name, string caption, bool enabled, Image glyph, int id, int possition, bool beginGroup, BarShortcut itemShortcut, bool showTooltip, string toolTipUpperText, string toolTipCenterText, string toolTipBottomText, bool usetoolTipSeparatorItem)
        {
            barManager.BeginUpdate();

            var superToolTip = new DevExpress.Utils.SuperToolTip();

            var barButtonItem = new BarButtonItem
                                    {
                                        Caption = caption,
                                        Enabled = enabled,
                                        Glyph = glyph,
                                        Id = id,
                                        ItemShortcut = itemShortcut,
                                        Name = name
                                    };
            if (showTooltip)
            {
                if (!string.IsNullOrEmpty(toolTipUpperText))
                {
                    var toolTipTitleItemUpper = new DevExpress.Utils.ToolTipTitleItem
                                                    {
                                                        Text = toolTipUpperText
                                                    };
                    superToolTip.Items.Add(toolTipTitleItemUpper);
                }

                if (!string.IsNullOrEmpty(toolTipCenterText))
                {
                    var toolTipItem = new DevExpress.Utils.ToolTipItem();
                    toolTipItem.Appearance.Image = Resources.Help;
                    toolTipItem.Appearance.Options.UseImage = true;
                    toolTipItem.Image = Resources.Info;
                    toolTipItem.LeftIndent = 6;
                    toolTipItem.Text = toolTipCenterText;
                    superToolTip.Items.Add(toolTipItem);
                }

                if (usetoolTipSeparatorItem)
                {
                    superToolTip.Items.Add(new DevExpress.Utils.ToolTipSeparatorItem());
                }
                
                if (!string.IsNullOrEmpty(toolTipBottomText))
                {

                    var toolTipTitleItem = new DevExpress.Utils.ToolTipTitleItem
                                               {
                                                   LeftIndent = 6,
                                                   Text = toolTipBottomText
                                               };

                    superToolTip.Items.Add(toolTipTitleItem);
                }

                barButtonItem.SuperTip = superToolTip;
            }

            barButtonItem.GroupIndex = 0;

            if (possition < 0 && possition > barManager.Items.Count)
                possition = barManager.Items.Count;

            barManager.Items.Insert(possition, barButtonItem);

            barCRUD.LinksPersistInfo.Insert(possition,
                new LinkPersistInfo(BarLinkUserDefines.PaintStyle,
                                                        barButtonItem, string.Empty, beginGroup, true, true, 0, null,
                                                        BarItemPaintStyle.CaptionGlyph));
           
            barManager.EndUpdate();

            return barButtonItem;
        }

        #endregion
       
        #region Constructors

        public VitalBaseForm()
        {
            InitializeComponent();
            
        }
        
        #endregion

        #region Methods

        #region Initialization & Binding & Helpers

        /// <summary>
        /// Set up the Icon and Title for the form
        /// </summary>
        /// <param name="title">The title.</param>
        public void SetFormTitle(string title)
        {
            Text = title;
        }      

        /// <summary>
        /// Set the initial form status
        /// </summary>
        /// <param name="isNew">Is new Test.</param>
        public virtual void SetFormStatus(bool isNew)
        {
            FormStatus = isNew ? FormStatusEnum.New : FormStatusEnum.Disabled;
        }

        /// <summary>
        /// Performs some steps to initialize the form.
        /// </summary>
        public virtual void PerformSpecificIntializationSteps() {}

        /// <summary>
        /// Set the handlers.
        /// </summary>
        public virtual void SetupHandllers()
        {
           
        }

        /// <summary>
        /// Clear the handlers.
        /// </summary>
        public virtual void ClearHandlers()
        {
            
        }

        /// <summary>
        /// Bind the controls of the form.
        /// </summary>
        public virtual void SetBinding() {}

        /// <summary>
        /// Set up the Main Error Provider.
        /// </summary>
        public virtual void SetupMainErrorProvider() {}

        /// <summary>
        /// Updates the binding of error provider
        /// </summary>
        public void UpdateErrorProvider()
        {
            //This is needed because of a crash in designer since the datasoruce is not defined yet
            if (dxErrorProviderMain.DataSource != null)
            {
                dxErrorProviderMain.ClearErrors();
                dxErrorProviderMain.UpdateBinding();
            }
        }

        /// <summary>
        /// Validate the current form.
        /// </summary>
        public void ValidateForm()
        {
            ShowHideErrorIcons();
        }

        /// <summary>
        /// Show or Hide the errors upper control they not supported the DxErrorProvider.
        /// </summary>
        public virtual void ShowHideErrorIcons()
        {
            
        }

        /// <summary>
        /// Setting some properties.
        /// </summary>
        public virtual void SetProperties()
        {
            
        }

        /// <summary>
        /// Clear the Binding.
        /// </summary>
        public virtual void ClearBinding()
        {
            
        }

        /// <summary>
        /// Sets the edit mode of the tab base
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public void SetEditModeBase(bool isReadOnly)
        {            
            barButtonItemEdit.Enabled = isReadOnly;
            barButtonItemDisable.Enabled = !isReadOnly && FormStatus != FormStatusEnum.New && FormStatus != FormStatusEnum.Modified && FormStatus != FormStatusEnum.Disabled;
            barButtonItemDelete.Enabled = !_isNew;
            
            if(isReadOnly)
            {
                SetSaveMode(false);
            }
            
            SetEditMode(isReadOnly);
        }

        /// <summary>
        /// Sets the edit mode of the tab
        /// </summary>
        /// <param name="isReadOnly">if true then the form will be in ready only mode</param>
        public virtual void SetEditMode(bool isReadOnly) {}

        /// <summary>
        /// Set the Save and Undo changes buttons mode.
        /// </summary>
        /// <param name="isChanged">Is object changed.</param>
        private void SetSaveMode(bool isChanged)
        {
            barButtonItemEdit.Enabled = !isChanged && !_isNew && FormStatus != FormStatusEnum.Unchanged;
            barButtonItemDisable.Enabled = !_isNew && FormStatus != FormStatusEnum.Modified && FormStatus != FormStatusEnum.Disabled;
            barButtonItemSave.Enabled = isChanged;
            barButtonItemSaveAndClose.Enabled = isChanged;
            barButtonItemUndo.Enabled = isChanged && !_isNew;
        }

        /// <summary>
        /// Lock the form bar.
        /// </summary>
        public void Lock()
        {
            SetEditModeBase(true);
            barButtonItemEdit.Enabled = false;
            barButtonItemDelete.Enabled = false;
            barButtonItemDisable.Enabled = false;
        }

        /// <summary>
        /// Revert the old status of the form when it be unlocked.
        /// </summary>
        private void RevertFormStatus()
        {
            if (_isNew)
            {
                SetEditModeBase(false);
                FormStatus = FormStatusEnum.New;
            }
            else if (_oldFormStatus.HasValue)
            {
                SetEditModeBase(false);
                FormStatus = _oldFormStatus.Value;
            }
        }

        /// <summary>
        /// Fill the lookup controls with the collections of objects from the cache
        /// </summary>
        public virtual void FillLookUps()
        {
            
        }

        #endregion

        #region Save related actions

        /// <summary>
        /// Process Save & Save and Close Actions.
        /// </summary>
        public bool SaveOrSaveAndClose(bool isClosing)
        {
            UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);            

            BeforeSaveAction();

            var isSuccessful = Save(isClosing);

            if (isSuccessful)
            {
                AfterSaveAction();
                FormStatus = FormStatusEnum.Unchanged;

                if (isClosing)
                {
                    Close();
                }
            }

            UiHelperClass.HideSplash();

            return isSuccessful;
        }

        /// <summary>
        /// Performs the logic of save action
        /// </summary>
        public virtual bool SaveAction()
        {
            return SaveOrSaveAndClose(false);
        }

        /// <summary>
        /// Performs the logic of save and close action
        /// </summary>
        public virtual void SaveAndCloseAction()
        {
            SaveOrSaveAndClose(true);
        }
 
        /// <summary>
        /// Uses the Tests manager to save the test        
        /// </summary>
        public virtual bool Save(bool isClosing)
        {
            return true;
        }

        /// <summary>
        /// Delete the current test object.
        /// </summary>
        /// <returns></returns>
        public virtual bool Delete()
        {
            return true;
        }

        /// <summary>
        /// Confirm the delete operation.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanDelete()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.DeleteConfirmQuestion);
           
            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// A virtual method to be overriden so form can tell if it is has changes or not
        /// </summary>
        /// <returns></returns>
        public virtual bool HasChanges()
        {
            return false;
        }

        /// <summary>
        /// Can the form close. With user notification.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanClose()
        {
            if (!HasChanges()) return true;

            var dialogResult = SaveOrSaveAndClose(true) ? DialogResult.Yes : DialogResult.No;
            
            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Posts the values in the controls that are not yet comitted to the datasource because the user
        /// clicked save or cancel without leaving the editor to another editor first.
        /// </summary>
        public virtual void PostValues()
        {

        }

        /// <summary>
        /// Check if the object can revert or not.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanRevert()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.ChangesWillBeCanceledMessage);

            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Revert the Test object.      
        /// </summary>
        public virtual bool Revert()
        {
            return false;
        }

        /// <summary>
        /// Rebinds the object.
        /// </summary>
        public virtual void Rebind()
        {
            ClearBinding();
            ClearHandlers();
            SetProperties();
            SetBinding();
            SetupMainErrorProvider();
            UpdateErrorProvider();
            ShowHideErrorIcons();
            SetupHandllers();
        }

        /// <summary>
        /// Do some operations after deleting.
        /// </summary>
        public virtual void AfterDeleteAction()
        {

        }

        /// <summary>
        /// Do some operation before deleting.
        /// </summary>
        public virtual void BeforeDeleteAction()
        {

        }

        /// <summary>
        /// Do some operations after saving.
        /// </summary>
        public virtual void AfterSaveAction()
        {
            
        }

        /// <summary>
        /// Do some operation before saving.
        /// </summary>
        public virtual void BeforeSaveAction()
        {
            PostValues();
            ValidateForm();
        }

        /// <summary>
        /// Do some operations after reverting.
        /// </summary>
        public virtual void AfterRevertAction()
        {

        }

        /// <summary>
        /// Do some operation before reverting.
        /// </summary>
        public virtual void BeforeRevertAction()
        {
            
        }

        /// <summary>
        /// Actions after the form benign finished binding.
        /// </summary>
        public virtual void AfterLoadAction()
        {

        }

        /// <summary>
        /// Customize for a new Items of the bar manager, or to do extra items;
        /// </summary>
        /// <param name="itemName">The Clicked item tag.</param>
        public virtual void CustomeBarManagerClickHandling(string itemName)
        {
            
        }

        /// <summary>
        /// Customize the Initialize Component, for adding remove or change some components.
        /// </summary>
        public virtual void CustomeInitializeComponent()
        {
            
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get the optimal color for the current region.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color GetRangeColor(int value)
        {
            if (value < 26)
            {
                return Color.Black;
            }

            if (value >= 26 && value <= 45)
            {
                return Color.Gray;
            }

            if (value >= 46 && value <= 54)
            {
                return Color.LimeGreen;
            }

            if (value >= 55 && value <= 75)
            {
                return Color.Orange;
            }

            if (value >= 76 && value <= 100)
            {
                return Color.Firebrick;
            }

            return Color.Black;
        }

        #endregion

        #region HintsLogic

        /// <summary>
        /// Show hint.
        /// </summary>
        public void ShowHint(BarButtonItem barButtonItem, string titile, string text,ToolTipLocation location, bool silent, int duration, bool avoidUserSetings = false)
        {
            if (barButtonItem == null || (!avoidUserSetings && !IsUserAlowstHints()))
                return;

            toolTipControllerBase.AutoPopDelay = duration;

            if(barButtonItem.Links.Count == 0)
                return;

            var itemLocation = barButtonItem.Links[0].ScreenBounds.Location;
            var locationOnScreen = new Point(itemLocation.X + barButtonItem.Width, itemLocation.Y);
            toolTipControllerBase.ToolTipLocation = location;
            barButtonItem.Manager.GetToolTipController().ShowHint(StaticKeys.HintTextPrefix + text, titile, locationOnScreen);

            if (!silent)
                PlayHintSound();
        }

        /// <summary>
        /// Show hint.
        /// </summary>
        public void ShowHint(Control control, string titile, string text, ToolTipLocation location, bool silent, int duration, bool avoidUserSetings = false)
        {
            if (control == null || (!avoidUserSetings && !IsUserAlowstHints()))
                return;

            toolTipControllerBase.AutoPopDelay = duration;
            toolTipControllerBase.ShowHint(StaticKeys.HintTextPrefix + text, titile, control, location);

            if (!silent)
                PlayHintSound();
        }

        /// <summary>
        /// Hide hint.
        /// </summary>
        public void HideHint()
        {
            toolTipControllerBase.HideHint();
        }

        /// <summary>
        /// Check for edit hint and show it if need.
        /// </summary>
        private void CheckForEditHint(bool silent)
        {
            if (ShowEditHint && IsUserAlowstHints() && ShowEditHint && FormStatus == FormStatusEnum.Disabled)
            {
                ShowHint(barButtonItemEdit, StaticKeys.HintTitleEdit, StaticKeys.HintTextEdit, ToolTipLocation.BottomRight, silent, 7000);
            }
        }

        /// <summary>
        /// Play Hint Sound.
        /// </summary>
        private void PlayHintSound()
        {
            try
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Enables form for editing
        /// </summary>
        private void SetEditable()
        {
            FormStatus = FormStatusEnum.Editable;
            HideHint();
        }

        /// <summary>
        /// Setup for edit hints.
        /// </summary>
        private void SetupEditHintsOrAutEdit()
        {
            if (DesignMode || FormStatus == FormStatusEnum.New) return;

            var autoEditSetting = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings)).FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.AutoEnableEdit));
            
            if (autoEditSetting == null || 
                UiHelperClass.GetYesLookupId() ==null || 
                Convert.ToInt32(autoEditSetting.Value) != UiHelperClass.GetYesLookupId())
            {
                if (!ShowEditHint)
                    return;

                KeyDown += VitalBaseForm_KeyDown;

                CheckForEditHint(true);   
            }
            else
            {
                SetEditable();
            }
        }

        /// <summary>
        /// Check if user allow showing for hints.
        /// </summary>
        /// <returns></returns>
        private bool IsUserAlowstHints()
        {
            var settingsList = CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings) as BindingList<Setting>;

            if (settingsList == null)
                return false;

            var setting = settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(SettingKeys.ShowHints)));

            if (setting == null)
                return false;

            int settingValue;

            if (!int.TryParse(setting.Value.ToString(), out settingValue))
                return false;

            return settingValue == UiHelperClass.GetYesLookupId();

        }

        #endregion

        #region Logic

        /// <summary>
        /// Translate Zoom Mouse Position.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="myImage">The Image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static Point TranslateZoomMousePosition(Point coordinates, Image myImage, int width, int height)
        {
            // test to make sure our image is not null
            if (myImage == null) return coordinates;
            // Make sure our control width and height are not 0 and our 
            // image width and height are not 0
            if (width == 0 || height == 0 || myImage.Width == 0 || myImage.Height == 0) return coordinates;
            // This is the one that gets a little tricky. Essentially, need to check 
            // the aspect ratio of the image to the aspect ratio of the control
            // to determine how it is being rendered
            float imageAspect = (float)myImage.Width / myImage.Height;
            float controlAspect = (float)width / height;
            float newX = coordinates.X;
            float newY = coordinates.Y;
            if (imageAspect > controlAspect)
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                float ratiowidth = (float)myImage.Width / width;
                newX *= ratiowidth;
                float scale = (float)width / myImage.Width;
                float displayheight = scale * myImage.Height;
                float diffheight = height - displayheight;
                diffheight /= 2;
                newY -= diffheight;
                newY /= scale;
            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                float ratioheight = (float)myImage.Height / height;
                newY *= ratioheight;
                float scale = (float)height / myImage.Height;
                float displaywidth = scale * myImage.Width;
                float diffwidth = width - displaywidth;
                diffwidth /= 2;
                newX -= diffwidth;
                newX /= scale;
            }
            return new Point((int)newX, (int)newY);
        }

        /// <summary>
        /// Posts the data in the current view to the datasource
        /// </summary>
        /// <param name="view"></param>
        public static void GridViewPostValues(GridView view)
        {
            if (view != null)
            {
                view.PostEditor();
                view.ValidateEditor();
                view.UpdateCurrentRow();
            }
        }

        #endregion

        #region Overlay

        public void ShowHideOverlay(bool showOverlay)
        {
            UiHelperClass.ShowHideOverlay(showOverlay, Location, Size, this, this);
        }

        #endregion

        #endregion

        #region Handlers

        /// <summary>
        /// Handle the specifics of form load
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        public virtual void BaseForm_Load(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(BaseForm_Load), sender, e);
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
                SetProperties();
                UiHelperClass.ShowWaitingPanel(StaticKeys.BindingInformationMessgae);
                SetBinding();
                UiHelperClass.ShowWaitingPanel(StaticKeys.FinalizingMessage);
                SetupMainErrorProvider();
                UpdateErrorProvider();
                SetupHandllers();
                UiHelperClass.HideSplash();
                FormStatusChanged += VitalBaseForm_FormStatusChanged;
                VitalBaseForm_FormStatusChanged(FormStatus);
                AfterLoadAction();
            }
        }

        /// <summary>
        /// Handel the shown event for the form.
        /// </summary>
        private void VitalBaseForm_Shown(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(VitalBaseForm_Shown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (IsLoaded && !DesignMode)
                    SetupEditHintsOrAutEdit();
            }
        }

        private void VitalBaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(VitalBaseForm_FormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                IsClosing = true;
            }
        }

        /// <summary>
        /// Custom bar item edit mode.
        /// </summary>
        /// <param name="barButtonName">The name of the item.</param>
        /// <param name="isReadOnly">The is read only flag.</param>
        public void CustomBarItemEditMode(string barButtonName , bool isReadOnly)
        {
            barManager.Items[barButtonName].Enabled = !isReadOnly;
        }

        /// <summary>
        /// Locks or unlocks All Bar button items
        /// </summary>
        public void SetAllToolbarItemsEditState(bool isReadOnly)
        {
            foreach (BarItem barButtomItem in barManager.Items)
            {
                barButtomItem.Enabled = !isReadOnly;
            }
        }

        /// <summary>
        /// Handle clicking a button in the toolbar
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Event.</param>
        public void barManagerTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(barManagerTest_ItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (string.IsNullOrEmpty(e.Item.Name)) return;

                if (e.Item.Name.Equals("barButtonItemSave"))
                {
                    SaveAction();
                }
                else if (e.Item.Name.Equals("barButtonItemSaveAndClose"))
                {
                    SaveAndCloseAction();
                }
                else if (e.Item.Name.Equals("barButtonItemUndo"))
                {
                    if (!CanRevert()) return;
                    UiHelperClass.ShowSplash(typeof (WaitFormLoadingData));
                    BeforeRevertAction();
                    if (Revert())
                    {
                        AfterRevertAction();
                        if (FormStatus != FormStatusEnum.New)
                            FormStatus = FormStatusEnum.Unchanged;
                    }
                    UiHelperClass.HideSplash();
                }
                else if (e.Item.Name.Equals("barButtonItemEdit"))
                {
                    SetEditable();
                }
                else if (e.Item.Name.Equals("barButtonItemDisable"))
                {
                    FormStatus = FormStatusEnum.Disabled;
                }
                else if (e.Item.Name.Equals("barButtonItemDelete"))
                {
                    if (!CanDelete()) return;
                    UiHelperClass.ShowSplash(typeof (WaitFormLoadingData));
                    BeforeDeleteAction();
                    if (Delete())
                    {
                        AfterDeleteAction();
                        FormStatus = FormStatusEnum.Disabled;
                    }
                    UiHelperClass.HideSplash();
                }

                CustomeBarManagerClickHandling(e.Item.Name);
            }
        }

        /// <summary>
        /// Handles the form status change events.
        /// </summary>
        /// <param name="newStatus"></param>
        public virtual void VitalBaseForm_FormStatusChanged(FormStatusEnum newStatus)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new OnFormStatusChanged(VitalBaseForm_FormStatusChanged), newStatus);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                switch (newStatus)
                {
                    case FormStatusEnum.Disabled:
                        _isInEditMode = false;
                        _isNew = false;
                        SetEditModeBase(true);
                        break;
                    case FormStatusEnum.Modified:
                        if (!_isNew && _isInEditMode)
                            SetSaveMode(true);
                        break;
                    case FormStatusEnum.New:
                        _isInEditMode = true;
                        _isNew = true;
                        SetSaveMode(true);
                        SetEditModeBase(false);
                        break;
                    case FormStatusEnum.Editable:
                        _isInEditMode = true;
                        SetEditModeBase(false);
                        break;
                    case FormStatusEnum.Unchanged:
                        _isNew = false;
                        SetSaveMode(false);
                        SetEditModeBase(false);
                        break;
                    case FormStatusEnum.Locked:
                        Lock();
                        break;
                    case FormStatusEnum.UnLocked:
                        RevertFormStatus();
                        break;
                }
            }

        }

        /// <summary>
        /// Handel the key down on the form to show up the hints.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VitalBaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(VitalBaseForm_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                CheckForEditHint(e.Alt || e.Control || e.Shift);
            }
        }

        /// <summary>
        /// Handel the deactivation of the form to hide the hints.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VitalBaseForm_Deactivate(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(VitalBaseForm_Deactivate), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                HideHint();
            }
        }

        /// <summary>
        /// Handel the location changed for the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VitalBaseForm_LocationChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(VitalBaseForm_LocationChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                HideHint();
            }
        }

        /// <summary>
        /// Handel the resize event on the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VitalBaseForm_Resize(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(VitalBaseForm_Resize), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                HideHint();
            }
        }

        #endregion

        #region Reports

        /// <summary>
        /// Show the passed report.
        /// </summary>
        /// <param name="report"></param>
        public void ShowReport(DevExpress.XtraReports.UI.XtraReport report)
        {
            var reportViewer = new XtraFormReportViewer();

            reportViewer.SetReport(report);

            reportViewer.ShowDialog();
        }

        #endregion

    }

    
}
