using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frmProductsInfo : XtraForm
    {
        #region Constants

        #endregion

        #region Fields

        private AppInfoManager _appInfoManager;
        private SettingsManager _settingsManager;
        private bool notificationOptionChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public frmProductsInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the object of the form if it is new object and initialize some properties
        /// </summary>
        public void PerformSpecificIntializationSteps()
        {
            UiHelperClass.ShowWaitingPanel("Loading ...");

            _appInfoManager = new AppInfoManager();
            _settingsManager = new SettingsManager();

            SetBinding();

            richEditControlProductsOnBackorder.Document.DefaultCharacterProperties.FontName = "Calibri";
            richEditControlDiscontinuedProducts.Document.DefaultCharacterProperties.FontName = "Calibri";

            UiHelperClass.HideSplash();
        }

        /// <summary>
        /// Binds the fields
        /// </summary>
        public void SetBinding()
        {
            checkEditEnableNotifications.Checked = ProductInfoHelper.EnableProductInfoNotification;

            ProductInfoHelper.UpdateAllProductInfo();
            
            richEditControlProductsOnBackorder.Text = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ProductsOnBackorder);
            textEditProdcutsOnBackorderUpdatedOn.Text = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ProductsOnBackorderLastModified);

            richEditControlDiscontinuedProducts.Text = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.DiscontinuedProducts);
            textEditDiscontinuedProductsUpdatedOn.Text = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.DiscontinuedProductsLastModified);
        }

        /// <summary>
        /// Saves current backup/restore settings
        /// </summary>
        /// <returns></returns>
        private void Save()
        {
            try
            {
                if (notificationOptionChanged)
                {
                    ProductInfoHelper.EnableProductInfoNotification = checkEditEnableNotifications.Checked;
                    UiHelperClass.UpdateCheckSettingValue(CachableDataEnum.Settings, SettingKeys.ProductInfoUpdateNotificationEnabled, checkEditEnableNotifications.Checked, _settingsManager);
                    ProductInfoHelper.EnableDisableProductInfoCheck(ProductInfoHelper.EnableProductInfoNotification);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Handlers

        #region Others

        /// <summary>
        /// Performs the loading logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            PerformSpecificIntializationSteps();
        }

        /// <summary>
        /// Handles form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBackupRestore_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Handle checkedit changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEditEnableNotifications_CheckedChanged(object sender, EventArgs e)
        {
            notificationOptionChanged = true;
        }

        #endregion        

        #endregion
    }
}