using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGauges.Core.Resources;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Vital.Business.Managers;
using Vital.Business.Shared.Caching;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Enums;
using Vital.UI.Logic_Classes;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.Forms.BaseForms;
using Vital.UI.UI_Components.Forms.DataManagement;
using Vital.UI.UI_Components.Reports;
using Vital.UI.UI_Components.User_Controls.BioDigital3DModel;
using Vital.Update.Managers;
using Application = System.Windows.Forms.Application;
using Timer = System.Windows.Forms.Timer;

#region Enum

/// <summary>
/// Enum that contains the types of the entities.
/// </summary>
public enum EntityType
{
    Patient,
    Test
}

#endregion

namespace Vital.UI
{
    // ReSharper disable InconsistentNaming
    public partial class ribbonFrmMain : RibbonForm
    // ReSharper restore InconsistentNaming
    {
        #region Private Variables

        private PatientsManager _patientsManager;
        private AppInfoManager _appInfoManager;
        private readonly SettingsManager _settingsManager;
        private readonly ApplicationUpdateManager _updateManager;
        private bool _autoUpdateCheck = true;
        private bool _autoUpdateCheckDone;
        private bool _isUpdating;
        private bool _updateCheckInProgress;
        private XtraFormPatientSearch _patientSearch;
        private BindingList<Patient> _patientsList;
        private readonly LookupsManager _lookupsManager;
        private int _yesLookupId;
        private int _noLookupId;
        private bool _showSearchAutomatically;
        private readonly TestsManager _testsManager;
        private ShippingOrdersManager _shippingOrdersManager;
        private int _leftLookupId;
        private int _rightLookupId;
        private bool _closingByUpdate;

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if a tab is currently open and selected
        /// </summary>
        private bool IsTabFoucsed
        {
            get { return xtraTabControlDetailsArea.TabPages.Count != 0 && xtraTabControlDetailsArea.SelectedTabPage != null; }
        }

        /// <summary>
        /// Property that indicates if the main form was closed by update restart process
        /// </summary>
        public bool ClosingByUpdate 
        {
            get
            {
                return _closingByUpdate;
            }
            set
            {
                _closingByUpdate = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///The Constructor.
        /// </summary>
        public ribbonFrmMain()
        {
            //Opacity was set to zero at the beginning to prevent showing form during data loading.            
            InitializeComponent();

            //Set the main form instance inside UIBLLInteraction early here inside the constructor and before the update logic because the update logic was causing an issue
            //since it was trying to access the instance during restart but the instance was not being set yet so this is why we call the logic here.
            SetUibllInteractionDetailsTab();

            _patientsManager = new PatientsManager();
            _appInfoManager = new AppInfoManager();
            _settingsManager = new SettingsManager();
            _testsManager = new TestsManager();
            _shippingOrdersManager = new ShippingOrdersManager();
            _lookupsManager = new LookupsManager();
            _updateManager = new ApplicationUpdateManager();
            _updateManager.ApplicationUpdateCheckCompleted += _updateManager_ApplicationUpdateCheckCompleted;
        }        

        #endregion

        #region Methods

        #region UI Methods

        #region Initialization
      
        /// <summary>
        /// Performs initialization for skins and fonts
        /// </summary>
        private void InitializeSkinsAndFont()
        {
            //Show/Hide automated testing button
            var showAutomatedTestScreenAccessUI = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.AutomatedTestScreenEnabled);
            barButtonItemAutoTest.Visibility = showAutomatedTestScreenAccessUI ? BarItemVisibility.Always : BarItemVisibility.Never;

            UiHelperClass.ShowWaitingPanel(StaticKeys.InitializationApplication);
            SetApplicationIcon();
            
            var skinNameSetting = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.SkinName) });
            var skinName = skinNameSetting != null ? skinNameSetting.Value.ToString() : "Office 2010 Blue";
            var defaultSkinName = skinNameSetting != null ? skinNameSetting.DefaultValue.ToString() : "Office 2010 Blue";

            defaultLookAndFeelDisclaimer.LookAndFeel.SkinName = skinName;

            defaultLookAndFeelDisclaimer.LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;

            SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true, true);

            SkinHelper.InitSkinPopupMenu(popupMenu1);

            ReorderDefaultSkinInSkinsGallery(defaultSkinName);

            FillLookupsId();

            var fontSize =
                _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.FontSize) });
            var font = UiHelperClass.GetFontWithSize(float.Parse(fontSize.Value.ToString()));

            AppearanceObject.DefaultFont = font;
            AppearanceObject.ControlAppearance.Font = font;
            AppearanceDefault.Control.Font = font;
            AppearanceDefault.Window.Font = font;
            BarAndDockingController.Default.AppearancesBar.ItemsFont = font;
            BarAndDockingController.Default.AppearancesBar.MainMenu.Font = font;
            BarAndDockingController.Default.AppearancesBar.MainMenuAppearance.SetFont(font);
            BarAndDockingController.Default.AppearancesBar.StatusBar.Font = font;
            BarAndDockingController.Default.AppearancesBar.StatusBarAppearance.SetFont(font);
            BarAndDockingController.Default.AppearancesBar.Bar.Font = font;
            BarAndDockingController.Default.AppearancesBar.BarAppearance.SetFont(font);
            
            xtraTabControlDetailsArea.AppearancePage.Header.Font = font;
            xtraTabControlDetailsArea.AppearancePage.HeaderActive.Font = font;
            xtraTabControlDetailsArea.AppearancePage.HeaderDisabled.Font = font;
            xtraTabControlDetailsArea.AppearancePage.HeaderHotTracked.Font = font;
            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Reorder default skin to the first of the skins gallery.
        /// </summary>
        /// <param name="skinName">The skin name.</param>
        private void ReorderDefaultSkinInSkinsGallery(string skinName)
        {
            foreach (GalleryItemGroup group in ribbonGalleryBarItem1.Gallery.Groups)
            {
                foreach (GalleryItem item in group.Items)
                {
                    if (item.Caption != skinName) continue;

                    item.Caption += StaticKeys.DefaultSkinRestName;
                    item.Hint += StaticKeys.DefaultSkinRestName;

                    group.Items.Remove(item);
                    group.Items.Insert(0, item);

                    ribbonGalleryBarItem1.Refresh();

                    return;
                }
            }

        }

        /// <summary>
        /// Activates the BioDigital models by adding them to main form controls
        /// </summary>
        private void ActivateBioDigitalModels()
        {
            //Determine if the 3D human anatomy UI is enabled or disabled
            var useHumanAnatomyView = ConfigurationManager.AppSettings[ConfigKeys.UseHumanAnatomyView.ToString()].ToBoolean();

            if (useHumanAnatomyView)
            {
                //IMPORTANT: EMPTY CORNER BOX FIX HERE WAS DISABLED ... CHECK OUT FIX IN XtraUserControlBioDigital3DModel>XtraUserControlBioDigital3DModel(string modelId, string baseObejctId) : this()
                //Update the location of the BioDigital Male/Female user controls to the bottom left of the screen, this is used to avoid the
                //issue we noticed about that empty square that showed up in Vital in the top left corner, we are not %100 sure it is casued
                //by the 3D component because we haven't seen it locally but there is a strong change that it is the cause.
                //var screenBounds = Screen.GetBounds(Bounds);
                //var newPosition = new Point(0, screenBounds.Y + screenBounds.Height);

                //UiHelperClass.BioDigitalModelMale.Location = newPosition;
                //UiHelperClass.BioDigitalModelFemale.Location = newPosition;

                //We add the models to main form even thought they are used in other screens to maintain their presense and keep them initialized and loaded
                //or otherwise we would need to load them and wait each time.
                //IMPORTANT: Add the control to the list of controls, otherwise it won't work
                Controls.Add((UiHelperClass.BioDigitalModelMale));
                Controls.Add((UiHelperClass.BioDigitalModelFemale));
            }
        }

        #endregion

        #region Ribbon Items Handling

        /// <summary>
        /// Calls the appropriate action when clicking a menu item.
        /// </summary>
        /// <param name="e"></param>
        private void PerformRibbonItemClick(ItemClickEventArgs e)
        {
            PerformItemAction(e.Item);
        }

        /// <summary>
        /// Check if the item clicked is a list item and performs the specified aciton
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void PerformItemAction(BarItem item)
        {
            if (item == null) return;

            if (item == barButtonItemUpdates)
            {
                if (xtraTabControlDetailsArea.TabPages.Count==0)
                {
                    if (!_updateCheckInProgress)
                    {
                        _autoUpdateCheck = false;
                        CheckForUpdates(true, true);
                    }
                    else
                    {
                        UiHelperClass.ShowInformation("A check for update is already in progress.");
                    }
                }
                else
                {
                    UiHelperClass.ShowInformation("Please close opened patient profiles before checking for updates.");   
                }  

            }
            else if (item == barButtonItemShipping)
            {
                var frmshipping = new frmShipping();
                frmshipping.ShowDialog();
                UpdateShippingOrdersCount();
            }
            else if (item == barButtonItemProductInfo)
            {
                var frmProductsInfo = new frmProductsInfo();
                frmProductsInfo.ShowDialog();
            }
            else if (item == barButtonItemPreferences)
            {
                new frmSettings().ShowDialog();
                UpdateTimingBar();
            }else if (item == barButtonItemBackupRestore)
            {
                if (xtraTabControlDetailsArea.TabPages.Count==0)
                {
                    new frmBackupRestore().ShowDialog();
                }
                else
                {
                    UiHelperClass.ShowInformation("Please close opened patient profiles before using the Backup/Restore feature.");   
                }                
            }
            else if (item == barButtonItemDataManagement)
            {
                new frmDataManagement().ShowDialog();
            }
            else if (item == barButtonItemHelp)
            {
                if (!File.Exists(Application.StartupPath + @"\" + "Help.chm"))
                {
                    UiHelperClass.ShowError("The help file is missing.", "Help File Error");
                    return;
                }
                Help.ShowHelp(this, Application.StartupPath + @"\" + "Help.chm");
            }
            else if (item == barButtonItemAbout)
            {
                var frmAbout = new XtraFormAbout();
                frmAbout.ShowDialog();                
            }
            else if (item == barButtonItemPhysiciansHR)
            {
                new XtraFormPHR().ShowDialog();
            }
            else if (item == barButtonItemDongleInfo)
            {
                new XtraFormDongleInfo().ShowDialog();
            }
            else if (item == barButtonItemClientsReport)
            {
                UiHelperClass.ShowWaitingPanel("Opening Client List ...", true);

                var patientsManager = new PatientsManager();
                var patients = patientsManager.GetPatients(new PatientsFilter());

                var report = new XtraReportClients()
                {
                    bindingSourceClients = { DataSource = patients },
                };

                var reportViewer = new XtraFormReportViewer();
                reportViewer.SetReport(report);

                UiHelperClass.HideSplash();

                reportViewer.ShowDialog();
            }
            else if (item == barButtonItemFeedback)
            {
                UiHelperClass.ShowWaitingPanel("Opening Feedback Form ...", true);     

                var feedbackForm = new XtraFormMailClient();

                UiHelperClass.HideSplash();

                feedbackForm.ShowDialog();
            }
            else if (item == barButtonItemNewPatient)
            {
                UiHelperClass.ShowWaitingPanel("Opening ...",true);                

                UibllInteraction.Instance.New(null);

                UiHelperClass.HideSplash();
            }
            else if(item == barButtonItemPatientSearch)
            {
                 ShowSearchDialog(string.Empty);   
            }
            else
            {
                if (xtraTabControlDetailsArea.TabPages.Count == 0 || xtraTabControlDetailsArea.SelectedTabPage == null) return;
                var currentTabToEdit = xtraTabControlDetailsArea.SelectedTabPage.Controls[0] as XtraTabPageEntity;
                if (currentTabToEdit == null) return;

                if (item == barButtonItemEdit)
                {
                    currentTabToEdit.UnlockTab();
                }
                else if (item == barButtonItemCancel)
                {
                    currentTabToEdit.CancelOrCancelCloseAction(false);
                }
                else if (item == barButtonItemSave)
                {
                    currentTabToEdit.SaveAction();
                }
                else if (item == barButtonItemStartPreliminary || 
                         item == barButtonItemAutoTest ||
                         item == barButtonItemStartSpotCheck ||
                         item == barButtonItemStartFrequencyTest ||
                         item == barButtonItemVFS)
                {
                    currentTabToEdit.PerformCustomAction(item.Name);
                }
                else if (item == barButtonItemSaveClose)
                {
                    currentTabToEdit.SaveAndCloseAction();
                }
                else if (item == barButtonItemLock)
                {
                    currentTabToEdit.LockAction();
                }
                else if (item == barButtonItemDelete)
                {
                    currentTabToEdit.DeleteAction();
                }
            }            
        }

        /// <summary>
        /// Updates the state of the edit buttons according to the current selected tab page.
        /// </summary>
        private void UpdateItemsAccordingToSelectedPage()
        {
            if (IsTabFoucsed)
            {
                var currentSelectedTab = xtraTabControlDetailsArea.SelectedTabPage.Controls[0] as XtraTabPageEntity;
                if (currentSelectedTab != null && currentSelectedTab.TabObject != null)
                {
                    currentSelectedTab.UpdateActionButtons();
                    currentSelectedTab.PerfromAfterSelectionAction();
                }
            }
            else
            {
                UibllInteraction.Instance.IsPreliminaryTestButtonEnabled(false);
                UibllInteraction.Instance.IsAutoTestButtonEnabled(false);
                UibllInteraction.Instance.IsVFSButtonEnabled(false);
                UibllInteraction.Instance.IsSpotCheckButtonEnabled(false);
                UibllInteraction.Instance.IsFrequencyTestButtonEnabled(false);
                UibllInteraction.Instance.IsSaveButtonEnabled(false);
                UibllInteraction.Instance.IsSaveAndCloseButtonEnabled(false);
                UibllInteraction.Instance.IsCancelButtonEnabled(false);
                UibllInteraction.Instance.IsEditButtonEnabled(false);
                UibllInteraction.Instance.IsLockButtonEnabled(false);
                UibllInteraction.Instance.IsDeleteButtonEnabled(false);
            }
            ShowHideLogo();
        }

        #endregion                        

        #region Buttons Enable/Disable

        /// <summary>
        /// Returns true if the save button is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsSaveButtonEnabled()
        {
            return barButtonItemSave.Enabled;
        }

        /// <summary>
        /// Returns true if the edit button is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsEditButtonEnabled()
        {
            return barButtonItemEdit.Enabled;
        }

        /// <summary>
        /// Returns true if the disable button is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsDisableButtonEnabled()
        {
            return barButtonItemLock.Enabled;
        }

        /// <summary>
        /// Returns true if the cancel button is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsCancelButtonEnabled()
        {
            return barButtonItemCancel.Enabled;
        }        

        #endregion        

        #region Other
        
        /// <summary>
        /// Show the main window of the application by restoring opacity value
        /// </summary>
        private void ShowApplicationWindow()
        {            
            ribbon.Manager.UseAltKeyForMenu = false;
            ribbon.Manager.UseF10KeyForMenu = false;
            Opacity = 100;
        }

        /// <summary>
        /// Sets the Icon of the application
        /// </summary>
        private void SetApplicationIcon()
        {
            IntPtr ptr = Resources.VitalPNG.GetHicon();
            Icon toolIcon = Icon.FromHandle(ptr);
            Icon = toolIcon;
        }       

        /// <summary>
        /// Close the selected tab of the tab control by asking for Ok method of the tab page, this will make sure
        /// that the user saved his changes before closing.
        /// </summary>
        private static void CloseTabPage(Control tabPage)
        {
            //Determine the status of the tab when the close button was clicked, if it is a new tab, then this should
            //mean a tab cancel operation, but if it is an existing one, then this should mean finished work on a tab
            try
            {
                if (tabPage != null)
                {
                    var closedTab = tabPage.Controls[0] as XtraTabPageEntity;
                    if (closedTab != null)
                    {
                        closedTab.CancelOrCancelCloseAction(true);
                    }
                }
            }
            catch (NullReferenceException nullReferenceException)
            {
                throw new VitalBaseException(nullReferenceException.InnerException.Message);
            }
        }

        /// <summary>
        /// Set the tab in the UIBLLInteraction object to able to use it for tabbed pages handling
        /// </summary>
        /// <returns></returns>
        public void SetUibllInteractionDetailsTab()
        {
            try
            {
                UibllInteraction.Instance.FormTabControl = xtraTabControlDetailsArea;
                UibllInteraction.Instance.MainForm = this;
                UibllInteraction.Instance.InitializeCustomFonts();
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(StaticKeys.MainFormIsNUllError, exception);
            }
        }

        /// <summary>
        /// Shows or hide the main logo based on opened tabs
        /// </summary>
        public void ShowHideLogo()
        {
            layoutControlItemLogo.Visibility = IsTabFoucsed ? LayoutVisibility.Never : LayoutVisibility.Always;
            barStaticItemSearchHint.Caption = IsTabFoucsed ? StaticKeys.SearchHintWithTabs : StaticKeys.SearchHintNoTabs;            
            layoutControlItemTabControl.Visibility = IsTabFoucsed ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        /// <summary>
        /// Updates the timing bar settings
        /// </summary>
        public void UpdateTimingBar()
        {
            SetDefaultShippingBarPositionSettings();
            SetDefaultShowShippingBarSettings();
            xtraUserControlShippingTimeBar.UpdateOptions();
        }


        /// <summary>
        /// Updates the number of shipping orders for today's tests
        /// </summary>
        public void UpdateShippingOrdersCount()
        {
            UiHelperClass.SetSplashText(StaticKeys.LoadingShippingCount);


            var testCount = _testsManager.
                GetTests(new TestsFilter()
                         {LoadingType = LoadingTypeEnum.Light}).Count(t => t.DateTime.HasValue &&
                                                                           t.DateTime.Value.Date == DateTime.Now.Date &&
                                                                           !t.IsOrderSent);
            var ordersCount = _shippingOrdersManager.
                              GetShippingOrders(new ShippingOrdersFilter()
                                                {
                                                    LoadingType = LoadingTypeEnum.Light, 
                                                    CreationDateTime = DateTime.Now,
                                                    Sent = false
                                                }).Count();

            UpdateShippingIcon(testCount+ordersCount);

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Update the icon of the shipping orders button
        /// </summary>
        /// <param name="count"></param>
        private void UpdateShippingIcon(int ordersCount)
        {
            //Shipping count icon image
            var shippingCountIcon = new Bitmap(Resources.ShippingCount);
            var shippingIconGraphic = Graphics.FromImage(shippingCountIcon);

            //Smooth graphics is nice.
            shippingIconGraphic.SmoothingMode = SmoothingMode.AntiAlias;

            //Setting correct text based on count
            var countString = ordersCount > 99 ? "..." : ordersCount == 0 ? "\u221A" : ordersCount.ToString();
            //"\u221A" This means Checkmark

            //Drawing text to the circle
            shippingIconGraphic.DrawString(
               countString,
               new Font("Tahoma", 11, FontStyle.Bold),
               SystemBrushes.WindowText,
               float.Parse(shippingCountIcon.Width.ToString()) / 2,
               float.Parse(shippingCountIcon.Height.ToString()) / 2,
               new StringFormat()
               {
                   Alignment = StringAlignment.Center,
                   LineAlignment = StringAlignment.Center
               });

            var size = 38;
            //Shipping image
            var shippingImage = new Bitmap(Resources.Shipping);
            var shippingGraphic = Graphics.FromImage(shippingImage);

            shippingGraphic.SmoothingMode = SmoothingMode.AntiAlias;

            //Draw the shipping count circle over the shipping box
            shippingGraphic.DrawImage(shippingCountIcon, shippingImage.Width - size, shippingImage.Height - size, size, size);

            //Set the image as the icon for the button
            barButtonItemShipping.LargeGlyph = shippingImage;

            //Clean house.
            shippingIconGraphic.Dispose();
            shippingCountIcon.Dispose();
            //We don't clear the ShippingImage as it will be needed for the icon!!
            shippingGraphic.Dispose();
        }

        #endregion

        #region Updates

        /// <summary>
        /// Handles checking for updates
        /// </summary>
        /// <param name="showMessages"></param>
        /// <param name="showLoading"> </param>
        private void CheckForUpdates(bool showMessages, bool showLoading)
        {
            if (_updateCheckInProgress) return;
            
            try
            {
                _updateCheckInProgress = true;
                if (showLoading)
                {
                    UiHelperClass.ShowWaitingPanel(StaticKeys.CheckingForUpdates);
                }
                var result = _updateManager.CheckForUpdate();
                if (!result.IsSucceed)
                {
                    if (showLoading)
                    {
                        UiHelperClass.HideSplash();    
                    }
                    _updateCheckInProgress = false;
                    if (showMessages)//Prevent messages when system checks for update automatically
                    {
                        UiHelperClass.ShowInformation(result.Message);
                    }
                }              
            }
            catch (Exception exception)
            {
                if (showLoading)
                {
                    UiHelperClass.HideSplash();
                }
                _updateCheckInProgress = false;
                if (showMessages)
                {
                    UiHelperClass.ShowError(StaticKeys.UpdateErrorOccured, exception);    
                }                
            }
        }

        #endregion

        #endregion

        #region Data

        /// <summary>
        /// Fill local lookups id.
        /// </summary>
        private void FillLookupsId()
        {
            var leftLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Left));

            var rightLookup =
                UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.LeftRight, LeftRightEnum.Right));

            _leftLookupId = leftLookup != null ? leftLookup.Id : 0;
            _rightLookupId = rightLookup != null ? rightLookup.Id : 0;
        }

        /// <summary>
        /// Sets position of the shipping bar to default one.
        /// </summary>
        private void SetDefaultShippingBarPositionSettings()
        {
            var position =
                _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShippingBarPosition) });

            int positionValue;

            if (position != null && int.TryParse(position.Value.ToString(), out positionValue))
            {
                var currentPosition = dockPanelShippingBar.Dock;
                var newPosition = positionValue == _leftLookupId ? DockingStyle.Left : DockingStyle.Right;

                if (currentPosition == newPosition)
                    return;

                //Workaround For Devexpress Dock panel Docking issue ( Panel should be visible before change its docking location ).
                dockPanelShippingBar.VisibilityChanged -= dockPanelShippingBar_VisibilityChanged;
                var currentVisiblity = dockPanelShippingBar.Visibility;
                dockPanelShippingBar.Visibility = DockVisibility.Visible;
                dockPanelShippingBar.Dock = positionValue == _leftLookupId ? DockingStyle.Left : DockingStyle.Right;
                dockPanelShippingBar.Visibility = currentVisiblity;
                dockPanelShippingBar.VisibilityChanged += dockPanelShippingBar_VisibilityChanged;
            }
        }

        /// <summary>
        /// Sets showing of the shipping bar to default one.
        /// </summary>
        private void SetDefaultShowShippingBarSettings()
        {
            var showSetting =
                _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShowShippingBar) });

            int showSettingValue;

            if (showSetting != null && int.TryParse(showSetting.Value.ToString(), out showSettingValue))
            {
                dockPanelShippingBar.Visibility = dockPanelShippingBar.RootPanel.Visibility = showSettingValue == _yesLookupId
                                                      ? DockVisibility.Visible
                                                      : DockVisibility.AutoHide;

            }
        }

        /// <summary>
        /// Check for showing PhysiciansHR Ad
        /// </summary>
        private void CheckForPhysiciansHRAd()
        {
            _appInfoManager = new AppInfoManager();

            var showPhysiciansHRAdAppInfo = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.ShowPhysiciansHRAd) });

            if (showPhysiciansHRAdAppInfo != null)
            {
                bool showPhysiciansHRAd;
                bool.TryParse(showPhysiciansHRAdAppInfo.Value, out showPhysiciansHRAd);

                if (showPhysiciansHRAd)
                {
                    new XtraFormPHR().ShowDialog();

                    showPhysiciansHRAdAppInfo.Value = "False";
                    _appInfoManager.Save(showPhysiciansHRAdAppInfo);
                }
            }
        }

        /// <summary>
        /// Check for asking user to provide Info for his Vital copy
        /// </summary>
        private void CheckForVitalKeyAndTechInfo()
        {
            //Only check Vital & Tech info if the application is deployed and if it is not the test or dev branches

            var branch = ConfigurationManager.AppSettings["AppBranch"];

            if (ApplicationDeployment.IsNetworkDeployed && branch != "DEV")
            {
                _appInfoManager = new AppInfoManager();

                //Check if info was sent to server
                var isVitalKeyDataSentToServer = _appInfoManager.GetAppInfoByProperty(
                                                    new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.VitalKeyDataSentToServer) });

                if (isVitalKeyDataSentToServer != null &&
                    isVitalKeyDataSentToServer.Value != string.Empty &&
                    bool.Parse(isVitalKeyDataSentToServer.Value)) return;

                //Check if key was generated
                var isVitalKeyGenerated = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.VitalKey) });

                //Show dialog only if the key wasn't generated yet
                if (isVitalKeyGenerated == null || isVitalKeyGenerated.Value == string.Empty)
                {
                    var xtraFormTechInfo = new XtraFormTechInfo();
                    xtraFormTechInfo.ShowDialog();
                }

                //Send technician's info to the server for logging
                if (!UiHelperClass.IsInternetOnline()) return;

                var infoLoggingThread = new Thread(t => LogTechInfo())
                {
                    IsBackground = true
                };

                //Start the info logging thread in the background
                infoLoggingThread.Start();
            }
        }

        /// <summary>
        /// Send info to service on server to log tech info
        /// </summary>
        private void LogTechInfo()
        {
            try
            {
                var apiUrl = string.Format("{0}{1}", ConfigurationManager.AppSettings[StaticKeys.VitalApiBaseUrlConfigKey],
                                                     ConfigurationManager.AppSettings[StaticKeys.UpdateUserInfoApiRouteConfigKey]);

                var techInfo = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.VitalKey) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.VitalKeyDate) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.AppBranch) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ProductsEntryCapacity) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianName) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianClinicName) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianClinicWebsite) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianAddress) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianCity) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianState) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianZip) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianPhone) + "," +
                                _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianEmail);

                WebApiConsumer.Post(apiUrl, new Dictionary<string, string> { { "", techInfo } },
                response =>
                {
                    if (response == HttpStatusCode.OK)
                    {
                        //Info stored at server successfully
                        var isVitalKeyDataSentToServer = _appInfoManager.GetAppInfoByProperty(
                                                    new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.VitalKeyDataSentToServer) });
                        isVitalKeyDataSentToServer.Value = "True";
                        _appInfoManager.Save(isVitalKeyDataSentToServer);
                    }
                },
                exception =>
                {
                    //Suppress error and do nothing
                });
            }
            catch (Exception exception)
            {
                //suppress error
            }
        }

        /// <summary>
        /// Check if we should show release notes
        /// </summary>
        private void CheckForShowReleaseNotes()
        {
            if (!Settings.Default.ShowReleaseNotes)
                return;

            new XtraFormAbout(AboutFormType.ReleaseNotes).ShowDialog();

            Settings.Default.ShowReleaseNotes = false;

            Settings.Default.Save();
        }

        /// <summary>
        /// Loads patients list
        /// </summary>
        private void LoadInitialData()
        {
            UiHelperClass.ShowSplash(typeof(SplashScreenVital));
            
            UiHelperClass.SetSplashText(StaticKeys.LoadingPatients);
            _patientsManager = new PatientsManager();

            GetPatients();
            UiHelperClass.SetSplashText(StaticKeys.CachingCommonData);
            CacheLookups();

            UiHelperClass.SetSplashText(StaticKeys.CachingProducts);
            CacheProducts();

            UiHelperClass.SetSplashText(StaticKeys.LoadingSettings);
            CacheSettings();

            UiHelperClass.SetSplashText(StaticKeys.LoadingHwProfiles);
            CacheHwProfiles();
            CheckAndInitHwProfilesFirstUse();
            CheckDongleExpiry();

            UiHelperClass.SetSplashText(StaticKeys.LoadingServices);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.VFSItemsSource);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.VFSSecondaryItemsSource);

            var yesLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));
            var noLookup = UiHelperClass.GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.No));

            if (yesLookup != null)
            {
                _yesLookupId = yesLookup.Id;    
            }

            if (noLookup != null)
            {
                _noLookupId = noLookup.Id;
            }

            UpdateShippingOrdersCount();

            //If this is a run after an update then update product info to prevent notification
            if (Settings.Default.ShowReleaseNotes)
            {
                ProductInfoHelper.UpdateAllProductInfo();
            }

            UiHelperClass.SetSplashText(StaticKeys.LoadingProductInfoProfiles);
            ProductInfoHelper.SetupProductInfoCheckAndUpdates();

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Cache settings data
        /// </summary>
        private void CacheSettings()
        {
            CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.PrintingSettings);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.Logo);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.ShippingOrderSettings);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.ShippingOrdersTimeInfo);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.BackupAndRestore);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.ShippingMethod);
        }

        /// <summary>
        /// Caches all system products
        /// </summary>
        private void CacheProducts()
        {
            CacheHelper.SetOrGetCachableData(CachableDataEnum.AllProducts);
        }

        /// <summary>
        /// Cache hwProfiles data
        /// </summary>
        private void CacheHwProfiles()
        {
            CacheHelper.SetOrGetCachableData(CachableDataEnum.HwProfiles);
        }
        
        /// <summary>
        /// Cache lookups data
        /// </summary>
        private void CacheLookups()
        {
            CacheHelper.SetOrGetCachableData(CachableDataEnum.Lookups);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.ItemsGroup);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.TestProtocols );
            CacheHelper.SetOrGetCachableData(CachableDataEnum.TestTypes);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.TestStates);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.PointsList);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.Settings);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.DBVersion);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.FourFactors);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.FourCauses);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.Ratios);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.Products);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.CustomDilutions);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.VitalForce);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.Top10);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.GenericList);
            CacheHelper.SetOrGetCachableData(CachableDataEnum.AllItems);
        }

        /// <summary>
        /// Gets a list of patients.
        /// </summary>
        public void GetPatients()
        {
            try
            {
                _patientsList = _patientsManager.GetPatients(new PatientsFilter());                
            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        ///  Check and Initialize Hardware Profiles For First Use
        /// </summary>
        public void CheckAndInitHwProfilesFirstUse()
        {
            try
            {
                var hwProfiles = ((BindingList<HwProfile>) CacheHelper.SetOrGetCachableData(CachableDataEnum.HwProfiles));
                
                if(hwProfiles == null)
                    return;

                var oldHwProfileNeedImage = hwProfiles.FirstOrDefault(p => p.Key.Equals(EnumNameResolver.Resolve(HwProfileKeyEnum.OldHwProfile)) && (p.Image == null || p.Image.Length == 0));
                var newHwProfileNeedImage = hwProfiles.FirstOrDefault(p => p.Key.Equals(EnumNameResolver.Resolve(HwProfileKeyEnum.NewHwProfile)) && (p.Image == null || p.Image.Length == 0));
                var changed = false;

                if (oldHwProfileNeedImage != null)
                {
                    oldHwProfileNeedImage.Image = new ImageConverter().ConvertTo(Resources.OldHardware, typeof(byte[])) as byte[];
                    changed = true;
                }

                if (newHwProfileNeedImage != null)
                {
                    newHwProfileNeedImage.Image = new ImageConverter().ConvertTo(Resources.NewHardware, typeof(byte[])) as byte[];
                    changed = true;
                }

                if (changed)
                {
                    UiHelperClass.SetSplashText(StaticKeys.InitializingHwProfilesFirstUse);
                    var settingsManager = new SettingsManager();
                    settingsManager.Save(hwProfiles);
                    CachingManager.RemoveFromCache(CachableDataEnum.HwProfiles.ToString());
                    CacheHelper.SetOrGetCachableData(CachableDataEnum.HwProfiles);
                }

            }
            catch (VitalBaseException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Check for dongle expiry
        /// </summary>
        private void CheckDongleExpiry()
        {
            var showDongleResetReminder = false;

            if (UiHelperClass.CheckForDongle)
            {
                if (UiHelperClass.DonglePresent)
                {
                    if (UiHelperClass.DongleState == DongleState.LessThanTenDays ||
                        UiHelperClass.DongleState == DongleState.LessThanFiveDays)
                    {
                        var expiryDate = SecurityManager.ReadExpirationDate().ToShortDateString();
                        simpleLabelItemDongleReset.Text = simpleLabelItemDongleReset.Text.Replace(StaticKeys.DongleExpiryDateString, expiryDate);
                        showDongleResetReminder = true;
                    }
                }
            }

            layoutControlGroupDongleResetReminder.Visibility = showDongleResetReminder? LayoutVisibility.Always: LayoutVisibility.Never;
        }

        /// <summary>
        /// Show dongle reset dialog
        /// </summary>
        private void ShowDongleResetDialog()
        {
            var xtraFormDongleReset = new XtraFormDongleReset(false);

            var result = xtraFormDongleReset.ShowDialog();

            if (result == DialogResult.OK)
            {
                UiHelperClass.ReadDongleInfo();
            }
        }

        #endregion

        #endregion

        #region Handlers
        
        /// <summary>
        /// Opens the selected patient.
        /// </summary>        
        private void OpenSelectedPatient()
        {
            UiHelperClass.ShowWaitingPanel("Opening ...", true);

            var currentPatient =
                (DomainEntity)_patientsManager.GetPatientById(new SingleItemFilter { ItemId = _patientSearch.CurrentPatient.Id });

            UibllInteraction.Instance.Open(ref currentPatient); 

            UiHelperClass.HideSplash();
        }        

        /// <summary>
        /// Handles Keydown of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonFrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new KeyEventHandler(ribbonFrmMain_KeyDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {

                if (e.KeyCode == Keys.F12)
                {
                    ShowSearchDialog(string.Empty);
                }
                else if(xtraUserControlShippingTimeBar.IsEditorActive)
                {
                    return;
                }
                else if (IsTabFoucsed)
                {
                    var currentTab = xtraTabControlDetailsArea.SelectedTabPage.Controls[0] as XtraTabPageEntity;
                    if (currentTab != null)
                    {
                        currentTab.PerformHotKeyAction(e);
                    }
                }  
                else
                {
                    if (!(e.KeyData >= Keys.F1 && e.KeyData <= Keys.F12))
                    {
                        var key = (char) e.KeyData;
                        if (char.IsLetterOrDigit(key))
                        {
                            ShowSearchDialog(UiHelperClass.KeycodeToChar((int)e.KeyCode).ToLower());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Shows the search dialog
        /// </summary>
        /// <param name="initialSearch"></param>
        private void ShowSearchDialog(string initialSearch)
        {
            _patientSearch = new XtraFormPatientSearch();
            _patientSearch.SetPatientsDatasource(_patientsList);//Sets datasource for patients
            _patientSearch.SetShowAutomaticallySetting(_showSearchAutomatically);//Sets teh auto show setting from DB

            //Set search intial character if user show up seach by entering search string right away in main screen
            if (initialSearch != string.Empty)
            {
                _patientSearch.SetInitialSearch(initialSearch);
            }

            var result = _patientSearch.ShowDialog();
            
            if(_showSearchAutomatically != _patientSearch.AutoShowUpSetting)
            {
                _showSearchAutomatically = _patientSearch.AutoShowUpSetting;//Get the auto show up setting
                var showSearchAuto = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShowPatientSearchAutomatically) });
                showSearchAuto.Value = _showSearchAutomatically ? _yesLookupId.ToString() : _noLookupId.ToString();
                _settingsManager.Save(showSearchAuto);
            }

            if (result == DialogResult.Yes)
            {
                OpenSelectedPatient();
            }
        }

        /// <summary>
        /// Handel the skin change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(LookAndFeel_StyleChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.ShowWaitingPanel(StaticKeys.SavingMessgae);
                var skinName = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.SkinName) });
                skinName.Value = defaultLookAndFeelDisclaimer.LookAndFeel.SkinName;
                _settingsManager.Save(skinName);
                UiHelperClass.HideSplash();
            }
        }
        
        /// <summary>
        /// Event fires when the update check is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void _updateManager_ApplicationUpdateCheckCompleted(object sender, Update.EventArgs.AppUpdateCheckCompletedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new AppUpdateCheckCompletedHandler(_updateManager_ApplicationUpdateCheckCompleted), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (_isUpdating) return;

                if ((_autoUpdateCheck && !_autoUpdateCheckDone) || (!_autoUpdateCheck && _autoUpdateCheckDone))
                {
                    try
                    {
                        if (_autoUpdateCheck)
                        {
                            _autoUpdateCheckDone = true;
                        }
                        UiHelperClass.HideSplash();
                        if (e.Exception != null)
                        {
                            if (!_autoUpdateCheck)
                            {
                                UiHelperClass.ShowError(StaticKeys.UpdateErrorOccured, StaticKeys.ServerNotAccessible);
                            }
                        }
                        else
                        {
                            if (e.UpdateAvailable)
                            {
                                if (UiHelperClass.ShowConfirmQuestion(StaticKeys.UpdateConfirmationQuestion) == DialogResult.Yes)
                                {
                                    UiHelperClass.BackupToDefaultPath();
                                    _isUpdating = true;

                                    var showUpdateDialog = UiHelperClass.ReleaseScriptsDownload.CheckIfDownloadedOrDownloadNow(true);

                                    if (showUpdateDialog)
                                    {
                                        var updateForm = new XtraFormUpdate { NewVersion = e.AvailableVersion};
                                        updateForm.ShowDialog();
                                    }
                                }
                            }
                            else if (!_autoUpdateCheck)//Prevent messages when system checks for update automatically
                            {
                                UiHelperClass.ShowInformation(StaticKeys.VersionIsUptoDate);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        if (!_autoUpdateCheck)
                        {
                            UiHelperClass.ShowError(StaticKeys.UpdateErrorOccured, exception);
                        }
                    }
                    _isUpdating = false;
                }
                _updateCheckInProgress = false;

                if (UibllInteraction.Instance.MainForm.ClosingByUpdate)
                {
                    Application.Restart();
                }
            }
        }

        /// <summary>
        /// Handles form showing and shows the form window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonFrmMain_Shown(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(ribbonFrmMain_Shown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowApplicationWindow();
                CheckForPhysiciansHRAd();
                CheckForShowReleaseNotes();
                CheckForVitalKeyAndTechInfo();
                UiHelperClass.CheckForBackupReminder(_settingsManager);
                UiHelperClass.CheckBackupRestoreLocation(_settingsManager);
                UiHelperClass.BackupTechInfo();
                CheckShowSearchAutomatically();
            }
        }

        /// <summary>
        /// Prevent the showing of the PopupMenu for the dock panel, (this PopupMenu contains close option that we need to disable)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockManagerMain_PopupMenuShowing(object sender,
                                                      DevExpress.XtraBars.Docking.PopupMenuShowingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                // Catch the invoking for _csaManager_MeterValueChanged when the form had been disposed.
                try
                {
                    if (IsDisposed) return;
                    Invoke(
                        new DevExpress.XtraBars.Docking.PopupMenuShowingEventHandler(dockManagerMain_PopupMenuShowing),
                        sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles icon backcolor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItemShippingCount_ShownEditor(object sender, ItemClickEventArgs e)
        {
            repositoryItemPictureEditShippingCount.Appearance.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Handle dongle reset button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDongleReset_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonDongleReset_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                ShowDongleResetDialog();
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// Calls a method to close the selected tab page of the tab control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        private void XtraTabControlDetailsAreaCloseButtonClick(object sender, EventArgs e)
        {

            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(XtraTabControlDetailsAreaCloseButtonClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (xtraTabControlDetailsArea != null)
                {
                    var arg = e as ClosePageButtonEventArgs;

                    try
                    {
                        if (arg != null) CloseTabPage(arg.Page as XtraTabPage);
                    }
                    catch (NullReferenceException)
                    {
                    }

                    UpdateItemsAccordingToSelectedPage();
                }
            }
        }

        /// <summary>
        /// Calls a method to close the hottracked tab when clicking by middle mouse button
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The Mouse Event Args</param>
        private void XtraTabControlDetailsAreaMouseDown(object sender, MouseEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new MouseEventHandler(XtraTabControlDetailsAreaMouseDown), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (e.Button == MouseButtons.Middle)
                {
                    if (xtraTabControlDetailsArea != null)
                    {
                        if (xtraTabControlDetailsArea.HotTrackedTabPage != null)
                        {
                            CloseTabPage(xtraTabControlDetailsArea.HotTrackedTabPage);
                            UpdateItemsAccordingToSelectedPage();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calls methods that should be called on the load event of the form
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        private void RibbonFrmMainLoad(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(RibbonFrmMainLoad), sender, e);
            }
            else
            {
                InitializeSkinsAndFont();

                //Activates the BioDigital models by adding them to main form controls
                ActivateBioDigitalModels();

                LoadInitialData();
                UpdateTimingBar();

                UiHelperClass.GetTechAndVitalAppInfo();

                ribbonPageGroupDongleInfo.Visible = UiHelperClass.CheckForDongle;
                dockPanelShippingBar.DockChanged += dockPanelShippingBar_DockChanged;

                CheckForUpdates(false, false);
            }
        }

        /// <summary>
        /// Checks if search should be shown automatically
        /// </summary>
        private void CheckShowSearchAutomatically()
        {
            var showSearchAuto = _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShowPatientSearchAutomatically) });
            _showSearchAutomatically = showSearchAuto.Value.ToString() == _yesLookupId.ToString();
            
            if (_showSearchAutomatically)
            {
                ShowSearchDialog(string.Empty);
            }
        }        

        /// <summary>
        /// On the form close action, the recent projects XML file should be updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonFrmMainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new FormClosingEventHandler(RibbonFrmMainFormClosing), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                if (!UibllInteraction.Instance.MainForm.ClosingByUpdate)
                {
                    bool shouldClose = true;

                    foreach (XtraTabPage tab in xtraTabControlDetailsArea.TabPages)
                    {
                        if (tab != null)
                        {
                            var entityControl = tab.Controls[0] as XtraTabPageEntity;
                            if (entityControl != null)
                            {
                                if (entityControl.TabType == TabTypes.Patient)
                                {
                                    if (entityControl.TabState == EntityTabState.Modified)
                                    {
                                        shouldClose = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (shouldClose)
                    {
                        //Confirm closing
                        var showScreenClosingConfirmation = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ShowScreenClosingConfirmation);

                        if (showScreenClosingConfirmation)
                        {
                            if (UiHelperClass.ShowConfirmQuestion(StaticKeys.ClosingScreenConfirmation) != DialogResult.Yes)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                    }

                    if (!shouldClose)
                    {
                        e.Cancel = true;

                        UiHelperClass.ShowInformation(StaticKeys.TabsUnsavedError, StaticKeys.InformationMessageTitle);
                    }
                    else if (!_closingByUpdate)
                    {
                        /*
                         * This check (e.CloseReason != CloseReason.ApplicationExitCall), to avoid the exceptions that may
                         * happens when the application is restarting or forced to stop.
                         */

                        UiHelperClass.CheckForAutoBackup(_settingsManager);
                    }
                }
            }
            
        }

        /// <summary>
        /// Handle dockPanelShippingBar visibility changed
        /// </summary>
        private void dockPanelShippingBar_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new VisibilityChangedEventHandler(dockPanelShippingBar_VisibilityChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UiHelperClass.SetSplashText(StaticKeys.SavingMessgae);
    
                //Save shipping bar visibility
                var showBarSetting =
                    _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShowShippingBar) });
                int oldshowBarSettingVal;

                int.TryParse(showBarSetting.Value.ToString(), out oldshowBarSettingVal);
                int newShowBarSettingVal = dockPanelShippingBar.Visibility == DockVisibility.Visible
                                               ? _yesLookupId
                                               : _noLookupId;

                if (newShowBarSettingVal != oldshowBarSettingVal)
                {
                    try
                    {
                        showBarSetting.Value = newShowBarSettingVal;
                        _settingsManager.Save(showBarSetting);
                        CachingManager.RemoveFromCache(CachableDataEnum.VisibleSettings.ToString());
                    }
                    catch (Exception)
                    {
                        UiHelperClass.HideSplash();
                        UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.SettingError);
                    }

                    UiHelperClass.HideSplash();
                }
            }
        }

        /// <summary>
        /// Handle dockPanelShippingBar Dock changed
        /// </summary>
        private void dockPanelShippingBar_DockChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(dockPanelShippingBar_DockChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                //saving the shipping bar position
                if (dockPanelShippingBar.Dock == DockingStyle.Right || dockPanelShippingBar.Dock == DockingStyle.Left)
                {
                    var position =
                        _settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ShippingBarPosition) });
                    int oldPosition;

                    int.TryParse(position.Value.ToString(), out oldPosition);
                    int newPositionValue = dockPanelShippingBar.Dock == DockingStyle.Left
                                               ? _leftLookupId
                                               : _rightLookupId;

                    if (oldPosition != newPositionValue)
                    {
                        try
                        {
                            position.Value = newPositionValue;
                            _settingsManager.Save(position);
                            CachingManager.RemoveFromCache(CachableDataEnum.VisibleSettings.ToString());
                        }
                        catch (Exception)
                        {
                            UiHelperClass.HideSplash();
                            UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle, StaticKeys.SettingError);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calls the right action when clicking a ribbon button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonItemClick(object sender, ItemClickEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new ItemClickEventHandler(RibbonItemClick), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                PerformRibbonItemClick(e);
            }
        }

        /// <summary>
        /// When selecting a tab, this method will update the satate of the ribbon button according to the selected tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraTabControlDetailsAreaSelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new TabPageChangedEventHandler(XtraTabControlDetailsAreaSelectedPageChanged), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                UpdateItemsAccordingToSelectedPage();
            }
        }

        #endregion
    }
}
