using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Properties;

namespace Vital.UI.Logic_Classes
{
    public class ProductInfoHelper
    {
        #region Fields

        private static AppInfoManager _appInfoManager;
        private static Timer _productInfoTimer;

        #endregion

        #region Properties

        /// <summary>
        /// List of Product Infos
        /// </summary>
        public static List<ProductInfo> ProductInfos { get; set; }

        /// <summary>
        /// Enable or disable product info check
        /// </summary>
        public static bool EnableProductInfoNotification {get;set;}

        #endregion

        #region Logic

        /// <summary>
        /// Get cached product info by key
        /// </summary>
        /// <param name="productInfoKey"></param>
        /// <returns></returns>
        public static ProductInfo GetCachedProductInfoByKey(ProductInfoEnum productInfoKey)
        {
            return ProductInfos.FirstOrDefault(p => p.Key == productInfoKey);
        }

        /// <summary>
        /// Converts an appinfokey value into a datetime
        /// </summary>
        /// <param name="appInfoKey"></param>
        /// <returns></returns>
        private static DateTime GetAppInfoAsDateByKey(AppInfoKeys appInfoKey)
        {
            DateTime dateTimeValue;
            DateTime.TryParse(_appInfoManager.GetAppInfoValueByProperty(appInfoKey), out dateTimeValue);
            return dateTimeValue;
        }

        /// <summary>
        /// Setup Product Info Settings
        /// </summary>
        private static void SetupProductInfoHelper()
        {
            if (_appInfoManager == null)
            {
                _appInfoManager = new AppInfoManager();

                //Initialize the ProductInfo list with values from database since we will be using this memory list to get our data to avoid
                //too many hits on database when doing checks but this list will be updated after doing an online check
                ProductInfos = new List<ProductInfo>
                {
                    new ProductInfo() {Key = ProductInfoEnum.ProductsOnBackorder, 
                                       Contents = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.ProductsOnBackorder),
                                       LastModified =  GetAppInfoAsDateByKey(AppInfoKeys.ProductsOnBackorderLastModified) },
                    new ProductInfo() {Key = ProductInfoEnum.DiscontinuedProducts, 
                                       Contents = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.DiscontinuedProducts),
                                       LastModified =  GetAppInfoAsDateByKey(AppInfoKeys.DiscontinuedProductsLastModified) }
                };
            }
        }

        /// <summary>
        /// Check if product information has any updates
        /// </summary>
        public static bool CheckProductInfoHasUpdate()
        {
            return CheckProductInfoHasUpdateByKey(ProductInfoEnum.ProductsOnBackorder) ||
                   CheckProductInfoHasUpdateByKey(ProductInfoEnum.DiscontinuedProducts);
        }

        /// <summary>
        /// Gets if product info has update by key
        /// </summary>
        public static bool CheckProductInfoHasUpdateByKey(ProductInfoEnum productInfoKey)
        {
            SetupProductInfoHelper();

            var productInfoHasUpdate = false;

            try
            {
                var productInfo = new DownloadedFileInfo() { DownloadResut = FilesDownloadResult.ErrorOccurred };

                switch (productInfoKey)
                {
                    case ProductInfoEnum.ProductsOnBackorder:
                        productInfo =  UiHelperClass.ProductsOnBackorderDownload.ReadDownloadFile();
                        break;
                    case ProductInfoEnum.DiscontinuedProducts:
                        productInfo = UiHelperClass.DiscontinuedProductsDownload.ReadDownloadFile();
                        break;
                }

                if (productInfo.DownloadResut == FilesDownloadResult.Sucess)
                {
                    productInfoHasUpdate = productInfo.LastModified != GetCachedProductInfoByKey(productInfoKey).LastModified;
                }
            }
            catch
            {
            }

            return productInfoHasUpdate;
        }

        /// <summary>
        /// Updates all product information
        /// </summary>
        public static void UpdateAllProductInfo()
        {
            UpdateProductInfo(ProductInfoEnum.ProductsOnBackorder);
            UpdateProductInfo(ProductInfoEnum.DiscontinuedProducts);
        }

        /// <summary>
        /// Gets Product info based on key
        /// </summary>
        public static DownloadedFileInfo UpdateProductInfo(ProductInfoEnum productInfoKey, bool showWaitingPanel = false)
        {
            SetupProductInfoHelper();

            try
            {
                var productInfo = new DownloadedFileInfo() { DownloadResut = FilesDownloadResult.ErrorOccurred };

                var appInfoKey = productInfoKey == ProductInfoEnum.ProductsOnBackorder ? AppInfoKeys.ProductsOnBackorder : AppInfoKeys.DiscontinuedProducts;
                var appInfoKeyLastModified = productInfoKey == ProductInfoEnum.ProductsOnBackorder ? AppInfoKeys.ProductsOnBackorderLastModified : AppInfoKeys.DiscontinuedProductsLastModified;

                switch (productInfoKey)
                {
                    case ProductInfoEnum.ProductsOnBackorder:
                        productInfo = UiHelperClass.ProductsOnBackorderDownload.ReadDownloadFile(showWaitingPanel);
                        break;
                    case ProductInfoEnum.DiscontinuedProducts:
                        productInfo = UiHelperClass.DiscontinuedProductsDownload.ReadDownloadFile(showWaitingPanel);
                        break;
                }

                if (productInfo.DownloadResut == FilesDownloadResult.Sucess)
                {
                    _appInfoManager.SetAppInfoValueByKey(appInfoKey, productInfo.Contents);
                    _appInfoManager.SetAppInfoValueByKey(appInfoKeyLastModified, productInfo.LastModified.ToString());

                    if (ProductInfos.All(p => p.Key != productInfoKey))
                    {
                        ProductInfos.Add(new ProductInfo() { Key = productInfoKey, 
                                                             Contents = productInfo.Contents, 
                                                             LastModified = productInfo.LastModified });
                    }
                    else
                    {
                        var existingProductInfo = GetCachedProductInfoByKey(productInfoKey);
                        existingProductInfo.Contents = productInfo.Contents;
                        existingProductInfo.LastModified = productInfo.LastModified;
                    }
                }
                
                return productInfo;
            }
            catch
            {
                return new DownloadedFileInfo() { DownloadResut = FilesDownloadResult.ErrorOccurred };
            }
        }

        /// <summary>
        /// Checks for product info updates and shows alert if there are an update
        /// </summary>
        public static void CheckProductInfoAndShowAlert()
        {
            if (CheckProductInfoHasUpdate())
            {
                AlertsHelper.Instance.Show(UibllInteraction.Instance.MainForm, StaticKeys.ProductInfoUpdateAvialableTitle, StaticKeys.ProductInfoUpdateAvialableMessage, 0, Resources.BlueBell16);
                UpdateAllProductInfo();
            }
        }

        /// <summary>
        /// Enables or disabled product info check timer
        /// </summary>
        /// <param name="isEnabled"></param>
        public static void EnableDisableProductInfoCheck(bool isEnabled)
        {
            _productInfoTimer.Enabled = isEnabled;
        }

        /// <summary>
        /// SetupProductInfoCheckAndUpdates
        /// </summary>
        public static void SetupProductInfoCheckAndUpdates()
        {
            EnableProductInfoNotification = UiHelperClass.GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ProductInfoUpdateNotificationEnabled);

            _productInfoTimer = new Timer { Interval = 3600000 }; //1 Hour Interval
            _productInfoTimer.Tick += _productInfoTimer_Tick;

            //Update the product info only if user doesn't want notification, this is to make sure that if the information got updated while the user
            //has his vital app closed that he would still get a notification about it when he starts vital and not just update his copy of the info without
            //notifying him.
            if (!EnableProductInfoNotification)
            {
                UpdateAllProductInfo();
            }
            else
            {

                //Check for updates and show notification since the first tick happens after an hour of timer start and only do it if user wants notifications
                CheckProductInfoAndShowAlert();
            }

            EnableDisableProductInfoCheck(EnableProductInfoNotification);
        }

        /// <summary>
        /// Handle the check for product info updates
        /// </summary>
        private static void _productInfoTimer_Tick(object sender, EventArgs e)
        {
            CheckProductInfoAndShowAlert();
        }

        #endregion
    }
}
