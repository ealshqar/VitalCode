using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Management;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using Vital.Business.Managers;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Properties;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.Forms.BaseForms;
using Vital.UI.UI_Components.Reports;
using System.Linq;
using System.Security.Cryptography;
using Vital.UI.UI_Components.User_Controls.BioDigital3DModel;
using Vital.UI.UI_Components.User_Controls.Modules;
using Vital.Update.Managers;

namespace Vital.UI.Logic_Classes
{
    public class UiHelperClass
    {
        #region Fields
        
        private static Form _reflictionGridForm;
        private static Form _overlayLayer;
        private const string CompanyFolder = "Standard Equipment Company";
        private const string ApplicationFolder = "Vital Expert";
        private static FilesDownloadDetail _releaseScriptsDownload;
        private static FilesDownloadDetail _dataScriptsDownload;
        private static FilesDownloadDetail _databaseDownload;
        private static FilesDownloadDetail _productsOnBackOrderDownload;
        private static FilesDownloadDetail _discontinuedProductsDownload;
        private static FilesDownloadDetail _imagesDownload;
        private static AppInfoManager _appInfoManager;
        private static SecurityManager _securityManager;
        
        public static string AppBranch;
        public static string TechnicianName;
        public static string TechnicianClinicName;
        public static string TechnicianClinicWebsite;
        public static string TechnicianAddress;
        public static string TechnicianCity;
        public static string TechnicianState;
        public static string TechnicianZip;
        public static string TechnicianPhone;
        public static string TechnicianEmail;
        public static string VitalKey;
        public static string VitalKeyDate;

        public static bool CheckForDongle;
        public static bool DonglePresent;
        public static DongleState DongleState;
        public static string DongleNumber;
        public static DateTime DongleExpiryDate;
        public static bool IsLicensedForVital;
        public static bool IsLicensedForCSAPV;
        #endregion

        #region Dongle Logic

        /// <summary>
        /// Read the current dongle info
        /// </summary>
        public static void ReadDongleInfo()
        {
            InitAppInfoManager();
            InitSecurityManager();

            DongleState = DongleState.NotCompitable;

            var checkForDongleAppInfo =  _appInfoManager.GetAppInfoByProperty(new AppInfoFilter {Property = Enum.GetName(typeof(AppInfoKeys),AppInfoKeys.CheckForDongle)});

            if (checkForDongleAppInfo == null) return;

            bool.TryParse(checkForDongleAppInfo.Value, out CheckForDongle);

            if (CheckForDongle)
            {
                try
                {
                    DonglePresent = SecurityManager.InitializeDongleActions();

                    if (DonglePresent)
                    {
                        SecurityManager.AuthorizeRead();

                        DongleState = _securityManager.CheckDongle();

                        var serialNumber = SecurityManager.ReadSerialNo();
                        DongleNumber = serialNumber.ToString();
                        DongleExpiryDate = SecurityManager.ReadExpirationDate();
                        IsLicensedForCSAPV = SecurityManager.DoesPositionHasValue(StaticKeys.CSAPVSecurityAddress, StaticKeys.CSAPVSecurityNumber);
                        IsLicensedForVital = SecurityManager.DoesPositionHasValue(StaticKeys.VitalSecurityAddress, StaticKeys.VitalSecurityNumber);
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        /// <summary>
        /// Shows message based on dongle state
        /// </summary>
        public static void ShowDongleCheckMessage()
        {
            if (DonglePresent)
            {
                switch (DongleState)
                {
                    case DongleState.NotCompitable:
                        ShowInformation(SecurityManager.DongleNotCompitable, SecurityManager.Title);
                        break;
                    case DongleState.LeaseExpired:
                        ShowInformation(SecurityManager.LeaseExpiredMessage, SecurityManager.Title);
                        break;
                    case DongleState.NonLeasedDongle:
                        break;
                    case DongleState.SysDateSetBack:
                        ShowError(SecurityManager.Title, SecurityManager.SysDateSetBackMessage);
                        break;
                    case DongleState.NoLeaseDate:
                        ShowError(SecurityManager.Title, SecurityManager.NoLeaseDateMessage);
                        break;
                    case DongleState.LeaseDateBad:
                        ShowError(SecurityManager.Title, SecurityManager.LeaseDateBadMessage);
                        break;
                    case DongleState.LastSysDateCorrupt:
                        ShowError(SecurityManager.Title, SecurityManager.LastSysDateCorruptMessage);
                        break;
                    case DongleState.LessThanTenDays:
                    case DongleState.LessThanFiveDays:
                        var days = DongleState == DongleState.LessThanFiveDays ? "5" : "10";
                        ShowInformation("You have less than " + days + " days until your dongle expires, please follow up with our staff to reset your dongle so you can continue using the software.", SecurityManager.Title);
                        break;
                    case DongleState.Default:
                        ShowWaitingPanel("Dongle Compatible ...");
                        break;
                }
            }
            else
            {
                ShowInformation(SecurityManager.NoKeyLoKMessage, SecurityManager.KeyLokStateTitle);
            }
        }

        /// <summary>
        /// This logic used to set the dongle date to yesterday to make sure that when the dongle is reset remotly by adding months that it will add months
        /// based on yesterday and and not based on the original expiry date which would cause the dongle to remain out of lease if the months are not enough
        /// </summary>
        public static void SetDongleExpiryDateToYesterday()
        {
            //We added this method to fix an issue that was caused by the fact that the months added to the dongle using the remote reset approach are 
            //added to its original expiry date and not to today's date, this means that if the user has their dongle expired and then 5 months layer they
            //contacted us to reset it then the months added will be added to the date 5 months ago and not to todays date, this causes us issues because
            //if we added 1 or 2 months for example the dongle will still remain out of lease and expired and so to overcome this we have to call this method
            //before resetting the dongle to make sure that we set the expiry date to a close date which is yesterday to also make sure the the user cannot use
            //the system before resetting their dongle.

            SecurityManager.AuthorizeWrite();

            var yesterdayDate = DateTime.Now.AddDays(-1);

            SecurityManager.SetExpDate(yesterdayDate.Month.ToString(CultureInfo.InvariantCulture),
                                       yesterdayDate.Day.ToString(CultureInfo.InvariantCulture),
                                       yesterdayDate.Year.ToString(CultureInfo.InvariantCulture));

            ReadDongleInfo();
        }

        #endregion

        #region Message Box

        /// <summary>
        /// Shows an error with exception
        /// </summary>
        /// <param name="title"></param>
        /// <param name="exception"></param>
        public static void ShowError(string title, Exception exception)
        {
            string errorText;
            var exceptionText = exception == null ? title : exception.Message;
            var innerExceptionText = exception == null || exception.InnerException == null ? string.Empty : exception.InnerException.Message;
            
            if (exception == null)
            {
                errorText = exceptionText;
            }
            else if (exception is VitalBaseException)
            {
                string errorType;
                if (exception is VitalDatabaseException)
                {
                    errorType = StaticKeys.ExceptionDatabaseErrorMessage;
                }               
                else if (exception is VitalLogicalException)
                {
                    errorType = StaticKeys.ExceptionLogicalErrorMessage;
                }
                else
                {
                    errorType = string.Empty;
                }
                errorText = string.Format("{0} {1} {2} {3}", errorType, exceptionText, "\n", innerExceptionText);
            }            
            else
            {
                errorText = string.Format("{0} {1} {2}", exceptionText, "\n", innerExceptionText);
            }
            
            HideSplash();
            var errorMessage = new XtraFormErrorMessage { ErrorText = errorText};
            if (title != string.Empty)
            {
                errorMessage.HeaderMessage = title;
            }
            
            errorMessage.ShowDialog();            
        }

        /// <summary>
        /// Shows an error with exception
        /// </summary>
        /// <param name="title"></param>
        /// <param name="headerMessage"></param>
        /// <param name="exception"></param>
        public static void ShowError(string title, string headerMessage, Exception exception)
        {
            var innerExceptionText = exception == null || exception.InnerException == null ? string.Empty : exception.InnerException.Message;
            var exceptionText = exception == null ? string.Empty : exception.Message;

            var errorText = exceptionText + "\n\n" + innerExceptionText;

            if (exception == null)
            {
                errorText = headerMessage == string.Empty ? title : headerMessage;
            }
            else if (exception is VitalBaseException)
            {
                string errorType;
                if (exception is VitalDatabaseException)
                {
                    errorType = StaticKeys.ExceptionDatabaseErrorMessage;
                }
                else if (exception is VitalLogicalException)
                {
                    errorType = StaticKeys.ExceptionLogicalErrorMessage;
                }
                else
                {
                    errorType = string.Empty;
                }
                errorText = string.Format("{0} {1}", errorType, exceptionText);
            }
            else
            {
                errorText = exceptionText;
            }

            HideSplash();
            var errorMessage = new XtraFormErrorMessage { MessageTitle = title, ErrorText = errorText, HeaderMessage = headerMessage };

            errorMessage.ShowDialog();
        }

        /// <summary>
        /// Shows an error with error text
        /// </summary>
        /// <param name="title"></param>
        /// <param name="errorText"></param>
        public static void ShowError(string title, string errorText)
        {
            HideSplash();
            var errorMessage = new XtraFormErrorMessage { ErrorText = errorText };
            if (title != string.Empty)
            {
                errorMessage.HeaderMessage = title;
            }

            errorMessage.ShowDialog();
        }

        /// <summary>
        /// Shows a warning message box.
        /// </summary>
        /// <param name="warningText">The warning text.</param>
        public static void ShowWarning(string warningText)
        {
            XtraMessageBox.Show(warningText, StaticKeys.WarningMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows an information message box.
        /// </summary>
        /// <param name="infoText">The info text.</param>
        /// <param name="parent">The parent form </param>
        public static void ShowInformation(string infoText, Form parent = null)
        {
            XtraMessageBox.Show(parent, infoText, StaticKeys.InformationMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows an information message box.
        /// </summary>
        /// <param name="infoText">The info text.</param>
        /// <param name="messageBoxTitle">The message box title.</param>
        public static void ShowInformation(string infoText, string messageBoxTitle)
        {
            XtraMessageBox.Show(infoText, messageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows a description message box.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="messge"></param>
        /// <param name="description"></param>
        public static void ShowdDescriptionMessage(string title, string messge, string description)
        {
            (new XtraFormDescriptionMessage() {Title = title, Message = messge, Description = description}).ShowDialog();
        }

        /// <summary>
        /// Shows the confirmation question message box.
        /// </summary>
        /// <param name="infoText">The info text.</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmQuestion(string infoText)
        {
            return XtraMessageBox.Show(infoText, StaticKeys.QuestionMessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Shows OK,Cancel message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static DialogResult ShowOkCancelMessageBox(string message)
        {
            return XtraMessageBox.Show(message, StaticKeys.QuestionMessageTitle, MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows Yes,No,Cancel message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static DialogResult ShowYesNoCancelMessageBox(string message)
        {
            return XtraMessageBox.Show(message, StaticKeys.QuestionMessageTitle, MessageBoxButtons.YesNoCancel,
                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        #endregion

        #region UI

        public static void ShowHideOverlay(bool showOverlay, Point location,Size size, IWin32Window owner, Form formToActivate)
        {
            //Create an overlay layer used for dialogs with meter to lower down confusion about meter
            if (_overlayLayer == null)
            {
                _overlayLayer = new Form
                {
                    BackColor = Color.DarkGray,
                    Opacity = 0.80,
                    FormBorderStyle = FormBorderStyle.None,
                    ControlBox = false,
                    ShowInTaskbar = false,
                    StartPosition = FormStartPosition.Manual,
                    AutoScaleMode = AutoScaleMode.None,
                    Location = location,
                    Size = size
                };
            }
            else
            {
                _overlayLayer.Location = location;
                _overlayLayer.Size = size;
            }

            if (showOverlay)
            {
                _overlayLayer.Location = location;
                _overlayLayer.Size = size;
                _overlayLayer.Hide();
                _overlayLayer.Show(owner);
            }
            else
            {
                _overlayLayer.Hide();
                if (formToActivate != null)
                {
                    formToActivate.Activate();    
                }                
            }
        }

        /// <summary>
        /// Fade in
        /// </summary>
        public static void FadeIn(Form sender, bool isFormLight)
        {
            var wait = isFormLight ? 10 : 1;
            var step = 0.1;

            for (double fadein = 0.0; fadein <= 1.1; fadein += step)
            {
                sender.Opacity = fadein;
                sender.Refresh();
                System.Threading.Thread.Sleep(wait);
            }
        }

        /// <summary>
        /// Fade out
        /// </summary>
        public static void FadeOut(Form sender, bool isFormLight)
        {
            var wait = isFormLight ? 10 : 0;
            var step = -0.1;

            for (double fadeout = 1.1; fadeout >= 0.0; fadeout += step)
            {
                sender.Opacity = fadeout;
                sender.Refresh();
                System.Threading.Thread.Sleep(wait);
            }
        }

        /// <summary>
        /// Handles coloring for order record
        /// </summary>
        public static void HandleOrderColor(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view != null && e.RowHandle >= 0)
            {
                
                bool sent = false;

                if (view.GetRow(e.RowHandle) is Test)
                {
                    var test = ((Test)view.GetRow(e.RowHandle));
                    sent = test != null && test.IsOrderSent;
                }
                else
                {
                    var order = ((ShippingOrder)view.GetRow(e.RowHandle));
                    sent = order != null && order.Sent;
                }

                if (sent)
                {
                    e.Appearance.BackColor = Color.YellowGreen;
                    e.Appearance.BackColor2 = Color.AliceBlue;
                }
                else
                {
                    e.Appearance.BackColor = Color.LightGray;
                    e.Appearance.BackColor2 = Color.AliceBlue;
                }
            }
        }

        /// <summary>
        /// Get a screenshot of the computer
        /// </summary>
        /// <returns></returns>
        public static MemoryStream GetSystemScreenShot()
        {
            try
            {
                MemoryStream screenShotStream;

                var bounds = Screen.GetBounds(Point.Empty);

                using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }

                    var imageConverter = new ImageConverter();
                    var bytesArray = (Byte[])imageConverter.ConvertTo(bitmap, typeof(Byte[]));

                    if (bytesArray != null)
                    {
                        screenShotStream = new MemoryStream(bytesArray);    
                    }
                    else
                    {
                        return null;
                    }
                }

                return screenShotStream;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get a screenshot of the computer
        /// </summary>
        /// <returns></returns>
        public static MemoryStream GetVitalScreenShot()
        {
            try
            {
                MemoryStream screenShotStream;

                var bounds = UibllInteraction.Instance.MainForm.Bounds;
                using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }

                    var imageConverter = new ImageConverter();
                    var bytesArray = (Byte[])imageConverter.ConvertTo(bitmap, typeof(Byte[]));

                    if (bytesArray != null)
                    {
                        screenShotStream = new MemoryStream(bytesArray);
                    }
                    else
                    {
                        return null;
                    }
                }

                return screenShotStream;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

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

        /// <summary>
        /// Returns an image that is loaded without keeping the origianl file locked
        /// </summary>
        /// <param name="path"></param>
        public static Image LoadImgeWithoutLocking(string path)
        {
            try
            {
                Image imageToLoad;

                using (var bmpTemp = new Bitmap(path))
                {
                    imageToLoad = new Bitmap(bmpTemp);
                }

                return imageToLoad;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 3D Interactive Anatomy

        /// <summary>
        /// BioDigital Model Control For Male
        /// </summary>
        public static XtraUserControlBioDigital3DModel BioDigitalModelMale { get; set; }

        /// <summary>
        /// BioDigital Model Control For BioDigitalModelFemale
        /// </summary>
        public static XtraUserControlBioDigital3DModel BioDigitalModelFemale { get; set; }

        #endregion

        #region Custom Fonts

        /// <summary>
        /// Gets font from list based on enum
        /// </summary>
        /// <param name="customFont"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Font GetCustomFont(CustomFonts customFont, float size)
        {
            //IMPORTANT:
            //General:
            //We used new types of fonts in Vital that are not normally installed on the user computer, these fonts will of course not show
            //up on the computer correctly unless we handle it, there are two ways to fix this:
            //1- Install the font on the computer. (Tool Complicated).
            //2- Add ability to show fonts without installation by adding fonts as resources and including them in something called PrivateFontCollection.
            //and then using these fonts programmatically by setting them to the fonts of the controls.
            //We used the second approach and added the fonts to a PrivateFontCollection. This approach can only be used in code so we can't use these 
            //fonts while in designer, because of this, the fonts needed to be installed on the devs computer so we can design correctly but in code we 
            //still needed to set the font manually to each control even though the font itself didn't change but it will be coming from different source, 
            //in designer it is coming from computer, in Vital it will be coming from the PrivateFontCollection.

            //Detail:
            //This method returns the font based on the passed enum value
            return UibllInteraction.Instance.GetCustomFont(customFont, size);
        }

        /// <summary>
        /// Gets the relative images folder path
        /// </summary>
        public static string ImagesFolderPath 
        {
            get
            {
                return Application.StartupPath + @"\" + StaticKeys.ImagesFolderName + @"\";
            }
        }
        #endregion

        #region Grid Views

        /// <summary>
        /// Determines if click action is in row
        /// </summary>
        /// <param name="view">The grid view.</param>
        public static bool IsClickInRow(GridView view)
        {
            return view.IsDataRow(view.FocusedRowHandle) && view.FocusedRowHandle >= 0;
        }

        /// <summary>
        ///Determines if the click action on one of the tree nodes.
        /// </summary>
        /// <param name="treeList">The tree list.</param>
        public static bool IsClickInRow(TreeList treeList)
        {
            return treeList.CalcHitInfo(treeList.PointToClient(Cursor.Position)).HitInfoType == HitInfoType.Cell;
        }

        /// <summary>
        /// Determines if click action is in row
        /// </summary>
        /// <param name="view"></param>
        public static bool IsClickInRowByMouse(GridView view)
        {
            return view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InRow && view.FocusedRowHandle >= 0;
        }

        /// <summary>
        /// Determines if click action is in row
        /// </summary>
        /// <param name="view"></param>
        public static bool IsClickInRowByMouse(LayoutView view)
        {
            return view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InCard && view.FocusedRowHandle >= 0;
        }

        /// <summary>
        /// Determines if click action is in group row
        /// </summary>
        /// <param name="view"></param>
        public static bool IsClickInGroupRowByMouse(GridView view)
        {
            return view.IsGroupRow(view.FocusedRowHandle);
        }

        /// <summary>
        /// Determines if gridview click action should be canceled
        /// </summary>
        /// <param name="view"></param>
        public static bool CancelClickAction(GridView view)
        {
            return view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InGroupPanel ||
                   view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InFilterPanel ||
                   view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InColumnPanel ||
                   view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position)).InGroupColumn;
        }

        /// <summary>
        /// Determines if treelist click action should be canceled
        /// </summary>
        /// <param name="treeList"></param>
        public static bool CancelClickAction(TreeList treeList)
        {
            return !IsClickInRow(treeList);
        }

        /// <summary>
        /// Determines if LayoutView click action should be canceled
        /// </summary>
        /// <param name="view"></param>
        public static bool CancelClickAction(LayoutView view)
        {
            var pt = view.GridControl.PointToClient(Cursor.Position);

            var info = view.CalcHitInfo(pt);

            return !info.InBounds;

        }

        #endregion

        #region Splash

        /// <summary>
        /// Shows the splash screen
        /// </summary>
        /// <param name="splashScreenType">The waiting for type</param>
        public static void ShowSplash(Type splashScreenType)
        {
            try
            {
                if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible)
                {
                    //PARENT IS SET TO NULL TO AVOID FOCUS PROBLEMS BETWEEN FORMS
                    SplashScreenManager.ShowForm(null, splashScreenType, true, true, false);
                }
            }
            catch (Exception e)
            {
                HideSplash();
            }
        }

        /// <summary>
        /// Sets the splash screen text
        /// </summary>
        public static void SetSplashText(String description)
        {
            try
            {
                if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.Default.SendCommand(SplashScreenCommand.SetText, description);
                }
            }
            catch (Exception)
            {
                HideSplash();
            }            
        }

        /// <summary>
        /// Shows the waiting panel with specific description
        /// </summary>        
        public static void ShowWaitingPanel(String description, bool topMost = false)
        {
            try
            {
                if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible)
                {
                    //PARENT IS SET TO NULL TO AVOID FOCUS PROBLEMS BETWEEN FORMS
                    SplashScreenManager.ShowForm(null, topMost ? typeof(WaitFormLoadingDataTopMost) : typeof(WaitFormLoadingData), true, true, false);
                }
                if (SplashScreenManager.Default != null) SplashScreenManager.Default.SetWaitFormDescription(description);

            }
            catch (Exception e)
            {
                HideSplash();    
            }
        }

        /// <summary>
        /// Shows the waiting panel with specific description
        /// </summary>        
        public static void ShowWaitingPanel(String description, bool useParentForm, bool topMost = false)
        {
            try
            {
                if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.ShowForm(useParentForm ? UibllInteraction.Instance.MainForm : null, topMost ? typeof(WaitFormLoadingDataTopMost) : typeof(WaitFormLoadingData), true, true, false);
                }
                if (SplashScreenManager.Default != null) SplashScreenManager.Default.SetWaitFormDescription(description);

            }
            catch (Exception e)
            {
                HideSplash();
            }  
        }

        /// <summary>
        /// Hides the splash screen
        /// </summary>
        public static void HideSplash()
        {
            try
            {
                SplashScreenManager.CloseForm(false);
            }
            catch (Exception e){}
        }

        #endregion

        #region Controls Properties

        /// <summary>
        /// Set common tab control properties.
        /// </summary>
        public static void SetTabControlProperties(XtraTabControl xtraTabControl)
        {
            xtraTabControl.ShowHeaderFocus = DefaultBoolean.True;
        }

        /// <summary>
        /// Set common Layout control properties.
        /// </summary>
        public static void SetLayoutControlProperties(LayoutControl layoutControl)
        {
            layoutControl.AllowCustomizationMenu = false;
            layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(526, 189, 250, 350);
            layoutControl.OptionsFocus.EnableAutoTabOrder = false;
        }

        /// <summary>
        /// Set common GridView properties.
        /// </summary>
        public static void SetViewProperties(GridView view)
        {
            //Common
            view.OptionsMenu.EnableColumnMenu = false;
            view.OptionsNavigation.UseTabKey = false;
            view.OptionsView.ShowDetailButtons = false;
            view.OptionsCustomization.AllowQuickHideColumns = false;
            view.OptionsDetail.EnableMasterViewMode = false;
            view.OptionsDetail.SmartDetailHeight = true;
            view.OptionsFind.AllowFindPanel = false;

            //Apperance
            //view.OptionsView.EnableAppearanceEvenRow = true;
            //view.OptionsSelection.EnableAppearanceFocusedCell = false;
            //view.OptionsSelection.EnableAppearanceHideSelection = false;

            //view.Appearance.FocusedRow.BackColor = System.Drawing.Color.Orange;
            //view.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            //view.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Silver;
            //view.Appearance.FocusedRow.Options.UseBackColor = true;
            //view.Appearance.FocusedRow.Options.UseBorderColor = true;
            //view.Appearance.SelectedRow.BackColor = System.Drawing.Color.Orange;
            //view.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FloralWhite;
            //view.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Silver;
            //view.Appearance.SelectedRow.Options.UseBackColor = true;
            //view.Appearance.SelectedRow.Options.UseBorderColor = true;


            //Customization
            //view.OptionsCustomization.AllowColumnMoving = false;
            //view.OptionsCustomization.AllowColumnResizing = false;
            //view.OptionsDetail.SmartDetailHeight = true;

            //Focus
            //view.OptionsNavigation.AutoMoveRowFocus = false;
            //view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            //view.OptionsBehavior.KeepFocusedRowOnUpdate = false;

            //Selection
            //view.OptionsSelection.MultiSelect = true;

            //Sorting
            //view.OptionsCustomization.AllowSort = false;

            //Filteration
            //view.OptionsCustomization.AllowFilter = false;
            //view.OptionsView.ShowAutoFilterRow = true;
            //view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            //Grouping
            //view.OptionsCustomization.AllowGroup = false;
            //view.OptionsView.ShowGroupPanel = false;

            //New Rows
            //view.OptionsNavigation.AutoFocusNewRow = true;
            //view.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;

            //Editing
            //view.OptionsBehavior.Editable = false;
            //view.OptionsBehavior.ReadOnly = true;
            //view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            //view.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            //view.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            //view.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;

            //Grid Control
            //view.GridControl.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// Gets a font with the passed size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Font GetFontWithSize(float size)
        {
            return new Font("Tahoma", size, FontStyle.Regular, GraphicsUnit.Point, 0);
        }        

        #endregion

        #region Files Download

        /// <summary>
        /// Files download details for the Release Script Files
        /// </summary>
        public static FilesDownloadDetail ReleaseScriptsDownload
        {
            get
            {
                return _releaseScriptsDownload ?? (_releaseScriptsDownload = new FilesDownloadDetail
                {
                    DownloadType = FilesDownloadType.ReleaseScripts,
                    DownloadFolderName = StaticKeys.ReleaseUpdatesFolderPath,
                    LogFile = StaticKeys.UpdateScriptsDownloadLog,
                    ResultFile = StaticKeys.UpdateScriptsDownloadResult,
                    ListFile = StaticKeys.UpdateScriptsFileName,
                    FilesUrl = StaticKeys.UpdateScriptsUrlConfigKey,
                    DeleteDownloadFolderOnCancel = true
                });
            }
        }

        /// <summary>
        /// Files download details for the data Script Files
        /// </summary>
        public static FilesDownloadDetail DataScriptsDownload
        {
            get
            {
                return _dataScriptsDownload ?? (_dataScriptsDownload = new FilesDownloadDetail
                {
                    DownloadType = FilesDownloadType.DataScripts,
                    DownloadFolderName = StaticKeys.DataScriptsFolderPath,
                    LogFile = StaticKeys.DataScriptsDownloadLog,
                    ResultFile = StaticKeys.DataScriptsDownloadResult,
                    ListFile = StaticKeys.DataScriptsFileName,
                    FilesUrl = StaticKeys.DataScriptsUrlConfigKey,
                    DeleteDownloadFolderOnCancel = true
                });
            }
        }

        /// <summary>
        /// Files download details for the database file
        /// </summary>
        public static FilesDownloadDetail DatabaseDownload
        {
            get
            {
                return _databaseDownload ?? (_databaseDownload = new FilesDownloadDetail
                {
                    DownloadType = FilesDownloadType.Database,
                    DownloadFolderName = StaticKeys.DatabaseFolderPath,
                    LogFile = StaticKeys.DatabaseDownloadLog,
                    ResultFile = StaticKeys.DatabaseDownloadResult,
                    ListFile = StaticKeys.DatabaseFileName,
                    FilesUrl = StaticKeys.DatabaseUrlConfigKey,
                    DeleteDownloadFolderOnCancel = true
                });
            }
        }

        /// <summary>
        /// Files download details for the images
        /// </summary>
        public static FilesDownloadDetail ImagesDownload
        {
            get
            {
                return _imagesDownload ?? (_imagesDownload = new FilesDownloadDetail
                {
                    DownloadType = FilesDownloadType.Images,
                    DownloadFolderName = StaticKeys.ImagesFolderName,
                    LogFile = StaticKeys.ImagesDownloadLog,
                    ResultFile = StaticKeys.ImagesDownloadResult,
                    ListFile = string.Empty,
                    FilesUrl = StaticKeys.ImagesUrlConfigKey,
                    DeleteDownloadFolderOnCancel = false, //Prevent deleting the download folder if the download was cancelled
                    DownloadBasedOnListFile = false,      //We don't use a file with list of image names on server so we set this approach to false
                    CacheFileListOnUpdateCheck = true     //This allows us to keep the list of files we found during update check process and reuse it during the download step if the user confirms, this is better than loading it twice
                });
            }
        }

        /// <summary>
        /// Files download details for the product on backorder
        /// </summary>
        public static FilesDownloadDetail ProductsOnBackorderDownload
        {
            get
            {
                return _productsOnBackOrderDownload ?? (_productsOnBackOrderDownload = new FilesDownloadDetail
                {
                    DownloadType = FilesDownloadType.ProductsOnBackorder,
                    DownloadFolderName = StaticKeys.ProductsOnBackorderFolderPath,
                    LogFile = StaticKeys.ProductsOnBackorderDownloadLog,
                    ResultFile = StaticKeys.ProductsOnBackorderDownloadResult,
                    ListFile = StaticKeys.ProductsOnBackorderFileName,
                    FilesUrl = StaticKeys.ProductsOnBackorderUrlConfigKey,
                    DeleteDownloadFolderOnCancel = true
                });
            }
        }

        /// <summary>
        /// Files download details for the discontinued products
        /// </summary>
        public static FilesDownloadDetail DiscontinuedProductsDownload
        {
            get
            {
                return _discontinuedProductsDownload ?? (_discontinuedProductsDownload = new FilesDownloadDetail
                {
                    DownloadType = FilesDownloadType.DiscontinuedProducts,
                    DownloadFolderName = StaticKeys.DiscontinuedProductsFolderPath,
                    LogFile = StaticKeys.DiscontinuedProductsDownloadLog,
                    ResultFile = StaticKeys.DiscontinuedProductsDownloadResult,
                    ListFile = StaticKeys.DiscontinuedProductsFileName,
                    FilesUrl = StaticKeys.DiscontinuedProductsUrlConfigKey,
                    DeleteDownloadFolderOnCancel = true
                });
            }
        }

        #endregion
        
        #region Database Updates

        #region Versions Information

        /// <summary>
        /// Gets the version info in DB
        /// </summary>
        /// <param name="appInfoManager"></param>
        /// <returns></returns>
        public static Version GetVitalStoredVersion(AppInfoManager appInfoManager)
        {
            AppInfo versionAppInfo = null;
            try
            {
                versionAppInfo = appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.Version) });
            }
            catch (Exception exception)
            {
                ShowInformation(StaticKeys.ErrorAccessingDatabase);
                return null;
            }

            return versionAppInfo != null ? new Version(versionAppInfo.Value) : null;
        }

        /// <summary>
        /// Gets the db  version info in DB
        /// </summary>
        /// <param name="appInfoManager"></param>
        /// <returns></returns>
        public static Version GetDbVersion(AppInfoManager appInfoManager)
        {
            AppInfo versionAppInfo = null;
            try
            {
                versionAppInfo = appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.DBVersion) });
            }
            catch (Exception exception)
            {
                ShowInformation(StaticKeys.ErrorAccessingDatabase);
                return null;
            }

            return versionAppInfo != null ? new Version(versionAppInfo.Value) : null;
        }

        /// <summary>
        /// Gets the version number from the name of a Vital release script file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Version GetReleaseScriptFileVersion(string fileName)
        {
            return string.IsNullOrEmpty(fileName) ? null : new Version(Path.GetFileNameWithoutExtension(fileName));
        }

        /// <summary>
        /// Gets vital version number from the name of a data script file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Version GetDataScriptVitalVersion(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var cleanFileName = Path.GetFileNameWithoutExtension(fileName);

            //Validate that the file name has two sections in it
            if (cleanFileName.Split(StaticKeys.DataScriptsFileNameSeparator).Count() != 2)
            {
                return null;
            }

            return new Version(cleanFileName.Split(StaticKeys.DataScriptsFileNameSeparator)[0]);
        }

        /// <summary>
        /// Gets db version number from the name of a data script file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Version GetDataScriptDbVersion(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var cleanFileName = Path.GetFileNameWithoutExtension(fileName);

            //Validate that the file name has two sections in it
            if (cleanFileName.Split(StaticKeys.DataScriptsFileNameSeparator).Count() != 2)
            {
                return null;
            }

            return new Version(cleanFileName.Split(StaticKeys.DataScriptsFileNameSeparator)[1]);
        }

        #endregion

        #region Database Download

        /// <summary>
        /// Checks if there are any available database file for current vital verstion and download and restore that DB file
        /// </summary>
        /// <returns></returns>
        public static DBCreateResult CheckDatabaseFileAndRestore(bool showError = false)
        {
            ShowWaitingPanel("Checking Vital Database ...");

            var result = DBCreateResult.None;

            var databaseCheckResult = DatabaseDownload.CheckIfUpdatesAreAvailable();

            if (databaseCheckResult == FileToDownloadCheck.UpdatesAvailable)
            {
                try
                {
                    if (DatabaseDownload.CheckIfDownloadedOrDownloadNow(false))
                    {
                        var createResult = CreateDownloadedVitalDatabase(showError);

                        result = createResult == DBCreationProcessState.DBCreationSuccessful ? DBCreateResult.Success : DBCreateResult.DBCreateError;

                        if (result == DBCreateResult.Success)
                        {
                            try
                            {
                                //Set product info after database creation to prevent notification on initial run
                                ProductInfoHelper.UpdateAllProductInfo();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    else
                    {
                        result = DBCreateResult.DownloadErrorOccurred;
                    }
                }
                catch (VitalDatabaseException exception)
                {
                    if (showError) ShowError(StaticKeys.ErrorDatabaseUpdate, exception);
                }
            }
            else if (databaseCheckResult == FileToDownloadCheck.NoUpdateAvailable)
            {
                result = DBCreateResult.NoFilesToDownload;
            }
            else if (databaseCheckResult == FileToDownloadCheck.UpdateCheckErrorOccured)
            {
                result = DBCreateResult.ErrorOccurred;
            }

            return result;
        }

        /// <summary>
        /// Restore Vital downloaded database
        /// </summary>
        /// <returns></returns>
        public static DBCreationProcessState CreateDownloadedVitalDatabase(bool showError = false)
        {
            var result = DBCreationProcessState.None;

            if (DatabaseDownload.GetDownloadState() == FilesDownloadState.Downloaded)
            {
                var downloadLogStreamReader = new StreamReader(DatabaseDownload.LogFilePath);

                try
                {
                    try
                    {
                        ShowWaitingPanel("Creating Database ...");
                        var sqlConfigManager = new SqlConfigManager();
                        var dbFileName = downloadLogStreamReader.ReadLine();

                        if (dbFileName != null)
                        {
                            var dbFilePath = DatabaseDownload.SetFolderFilePath(dbFileName);

                            if (!string.IsNullOrEmpty(dbFilePath) && File.Exists(dbFilePath))
                            {
                                var dbFileVersion = new Version(Path.GetFileNameWithoutExtension(dbFilePath));
                                var updateMangaer = new ApplicationUpdateManager();

                                if (dbFileVersion == updateMangaer.CurrentPublishVersion)
                                {
                                    try
                                    {
                                        sqlConfigManager.CreateDatabase(dbFilePath);
                                        result = DBCreationProcessState.DBCreationSuccessful;
                                    }
                                    catch (Exception exception)
                                    {
                                        result = DBCreationProcessState.DBCreationFailed;
                                    }
                                    
                                }
                                else
                                {
                                    result = DBCreationProcessState.DBIncompatibleVersion;
                                }
                            }
                            else
                            {
                                result = DBCreationProcessState.DBFileMissing;
                            }
                        }
                        else
                        {
                            result = DBCreationProcessState.DBFileMissing;
                        }
                        HideSplash();
                    }
                    catch (VitalDatabaseException exception)
                    {
                        ShowError(StaticKeys.ErrorDatabaseCreation, exception);
                    }
                }
                catch (Exception exception)
                {
                    if (showError) ShowInformation(StaticKeys.DataUpdateErrorOccured);
                    result = DBCreationProcessState.ErrorOccured;
                }

                downloadLogStreamReader.Close();
            }
            else
            {
                result = DBCreationProcessState.DBFileMissing;
            }

            HideSplash();

            //Delete the download folder regardless if scripts worked or not.
            DatabaseDownload.DeleteDownloadsFolder();

            return result;
        }

        #endregion

        #region Data Scripts

        /// <summary>
        /// Checks if there are any available data scripts for current db and vital versions and executes them if found
        /// </summary>
        /// <returns></returns>
        public static ScriptUpdateExecuteCheck CheckDataScriptsAndUpdate(bool backupDatabase = true, bool showError = false)
        {
            ShowWaitingPanel("Checking Data Updates ...");

            ScriptUpdateExecuteCheck result = ScriptUpdateExecuteCheck.None;

            var dataScriptsCheckResult = DataScriptsDownload.CheckIfUpdatesAreAvailable();

            if (dataScriptsCheckResult == FileToDownloadCheck.UpdatesAvailable)
            {
                try
                {
                    if (DataScriptsDownload.CheckIfDownloadedOrDownloadNow(false))
                    {
                        //Backup database
                        if (backupDatabase)
                        {
                            BackupToDefaultPath();    
                        }

                        var executionResult = ApplyDownloadedDataScripts(showError);

                        result = executionResult == DBScriptProcessState.ScriptSuccessful ? ScriptUpdateExecuteCheck.ScriptSuccess : ScriptUpdateExecuteCheck.ScriptFailed;
                    }
                    else
                    {
                        result = ScriptUpdateExecuteCheck.ScriptMissing;
                    }
                }
                catch (VitalDatabaseException exception)
                {
                    if (showError) ShowError(StaticKeys.ErrorDatabaseUpdate, exception);
                }
            }
            else if (dataScriptsCheckResult == FileToDownloadCheck.NoUpdateAvailable)
            {
                result = ScriptUpdateExecuteCheck.NoUpdateAvailable;
            }
            else if (dataScriptsCheckResult == FileToDownloadCheck.UpdateCheckErrorOccured)
            {
                result = ScriptUpdateExecuteCheck.UpdateCheckErrorOccured;
            }

            return result;
        }

        /// <summary>
        /// Loads the data scripts one by one and applies them to DB
        /// </summary>
        /// <returns></returns>
        public static DBScriptProcessState ApplyDownloadedDataScripts(bool showError = false)
        {
            var result = DBScriptProcessState.ScriptSuccessful;

            if (DataScriptsDownload.GetDownloadState() == FilesDownloadState.Downloaded)
            {
                var downloadLogStreamReader = new StreamReader(DataScriptsDownload.LogFilePath);

                try
                {
                    var appInfoManager = new AppInfoManager();
                    Version vitalStoredVersion;
                    Version dbVersion;

                    try
                    {
                        vitalStoredVersion = GetVitalStoredVersion(appInfoManager);
                        dbVersion = GetDbVersion(appInfoManager);
                    }
                    catch (Exception exception)
                    {
                        if(showError) ShowInformation(StaticKeys.ErrorVersionCannotBeIdentified);
                        return DBScriptProcessState.VersionInfoMissing;
                    }

                    if (vitalStoredVersion == null || dbVersion == null)
                    {
                        if (showError) ShowInformation(StaticKeys.ErrorVersionCannotBeIdentified);
                        return DBScriptProcessState.VersionInfoMissing;
                    }

                    try
                    {
                        string dataScriptFile;
                        var sqlConfigManager = new SqlConfigManager();

                        ShowWaitingPanel("Upgrading Database ...");

                        Version lastScriptDbVersion = null;

                        while ((dataScriptFile = downloadLogStreamReader.ReadLine()) != null)
                        {
                            var dataScriptFilePath = DataScriptsDownload.SetFolderFilePath(dataScriptFile);

                            if (!string.IsNullOrEmpty(dataScriptFilePath) && File.Exists(dataScriptFilePath))
                            {
                                var dataScriptVitalVersion = GetDataScriptVitalVersion(dataScriptFile);
                                var dataScriptDbVersion = GetDataScriptDbVersion(dataScriptFile);

                                if (dataScriptVitalVersion <= vitalStoredVersion && dataScriptDbVersion > dbVersion)
                                {
                                    lastScriptDbVersion = dataScriptDbVersion;

                                    try
                                    {
                                        var dataScript = File.ReadAllText(dataScriptFilePath);

                                        if (!string.IsNullOrEmpty(dataScript))
                                        {
                                            if (!sqlConfigManager.RunUpdateScript(dataScript))
                                            {
                                                if (showError) ShowInformation(StaticKeys.DataScriptFailed);
                                                result = DBScriptProcessState.ScriptFailed;
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        if (showError) ShowInformation(StaticKeys.DataScriptFailed);
                                        result = DBScriptProcessState.ScriptFailed;
                                    }
                                }
                                else
                                {
                                    if (showError) ShowInformation(StaticKeys.DataScriptIncompatible);
                                    result = DBScriptProcessState.ScriptIncompatibleVersion;
                                }

                                dbVersion = GetDbVersion(appInfoManager);
                            }
                            else
                            {
                                if (showError) ShowInformation(StaticKeys.DataScriptMissing);
                                result = DBScriptProcessState.ScriptMissing;
                            }

                            if (result != DBScriptProcessState.ScriptSuccessful)
                            {
                                break;
                            }
                        }

                        if (dbVersion != lastScriptDbVersion)
                        {
                            result = DBScriptProcessState.ScriptIncompatibleVersion;
                        }
                    }
                    catch (Exception exception)
                    {
                        if (showError) ShowInformation(StaticKeys.DataUpdateErrorOccured);
                        result = DBScriptProcessState.ErrorOccured;
                    }
                }
                catch (Exception exception)
                {
                    if (showError) ShowInformation(StaticKeys.DataUpdateErrorOccured);
                    result = DBScriptProcessState.ErrorOccured;
                }

                downloadLogStreamReader.Close();
            }
            else
            {
                result = DBScriptProcessState.ScriptMissing;
            }

            HideSplash();

            //Delete the download folder regardless if scripts worked or not.
            DataScriptsDownload.DeleteDownloadsFolder();

            return result;
        }

        #endregion

        #region Release Scripts
        
        /// <summary>
        /// Loads the update scripts one by one and applies them to DB
        /// </summary>
        /// <returns></returns>
        public static DBScriptProcessState ApplyDownloadedUpdateScripts()
        {
            var result = DBScriptProcessState.ScriptSuccessful;

            if (ReleaseScriptsDownload.GetDownloadState() == FilesDownloadState.Downloaded)
            {
                var downloadLogStreamReader = new StreamReader(ReleaseScriptsDownload.LogFilePath);

                try
                {
                    var appInfoManager = new AppInfoManager();
                    Version vitalStoredVersion;

                    try
                    {
                        vitalStoredVersion = GetVitalStoredVersion(appInfoManager);
                    }
                    catch (Exception exception)
                    {
                        ShowInformation(StaticKeys.ErrorVersionCannotBeIdentified);
                        return DBScriptProcessState.VersionInfoMissing;
                    }

                    if (vitalStoredVersion == null)
                    {
                        ShowInformation(StaticKeys.ErrorVersionCannotBeIdentified);
                        return DBScriptProcessState.VersionInfoMissing;
                    }

                    try
                    {
                        string updateScriptFile;
                        var sqlConfigManager = new SqlConfigManager();
                        var updateMangaer = new ApplicationUpdateManager();

                        ShowWaitingPanel("Upgrading Database ...");

                        while ((updateScriptFile = downloadLogStreamReader.ReadLine()) != null)
                        {
                            var updateScriptFilePath = ReleaseScriptsDownload.SetFolderFilePath(updateScriptFile);

                            if (!string.IsNullOrEmpty(updateScriptFile) && File.Exists(updateScriptFilePath))
                            {
                                var updateScriptVersion = new Version(Path.GetFileNameWithoutExtension(updateScriptFilePath));

                                //Run script only if the script version is higher than the current version in DB and only if the
                                //current DB version is less than the current publish version, this code only runs after Vital updates ClickOnce version
                                //and we need to make sure it runs only compatible scripts to make sure DBcurrent Version and App ClickOnce version are the same
                                //even if there are additional scripts that were downloaded by mistake and shouldn't be run
                                if (updateScriptVersion > vitalStoredVersion && vitalStoredVersion < updateMangaer.CurrentPublishVersion)
                                {
                                    try
                                    {
                                        var updateScript = File.ReadAllText(updateScriptFilePath);

                                        if (!string.IsNullOrEmpty(updateScript))
                                        {
                                            if (!sqlConfigManager.RunUpdateScript(updateScript))
                                            {
                                                ShowInformation(StaticKeys.UpdateScriptFailed);
                                                result = DBScriptProcessState.ScriptFailed;
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        ShowInformation(StaticKeys.UpdateScriptFailed);
                                        result = DBScriptProcessState.ScriptFailed;
                                    }
                                }
                                else
                                {
                                    ShowInformation(StaticKeys.UpdateScriptIncompatible);
                                    result = DBScriptProcessState.ScriptIncompatibleVersion;
                                }

                                vitalStoredVersion = GetVitalStoredVersion(appInfoManager);
                            }
                            else
                            {
                                ShowInformation(StaticKeys.UpdateScriptMissing);
                                result = DBScriptProcessState.ScriptMissing;
                            }

                            if (result != DBScriptProcessState.ScriptSuccessful)
                            {
                                break;
                            }

                            //After executing each release script, check for available data scripts and execute them and show an error if the
                            //execution or the check fails and do not continue the database update process
                            var dataScriptsResult = CheckDataScriptsAndUpdate(false);

                            if (dataScriptsResult != ScriptUpdateExecuteCheck.NoUpdateAvailable && dataScriptsResult != ScriptUpdateExecuteCheck.ScriptSuccess)
                            {
                                switch (dataScriptsResult)
                                {
                                    case ScriptUpdateExecuteCheck.ScriptFailed:
                                        result = DBScriptProcessState.ScriptFailed;
                                        break;
                                    case ScriptUpdateExecuteCheck.ScriptMissing:
                                        result = DBScriptProcessState.ScriptMissing;
                                        break;
                                    case ScriptUpdateExecuteCheck.UpdateCheckErrorOccured:
                                        result = DBScriptProcessState.ErrorOccured;
                                        break;
                                }

                                break;
                            }
                        }

                        if (vitalStoredVersion != updateMangaer.CurrentPublishVersion)
                        {
                            result = DBScriptProcessState.ScriptIncompatibleVersion;
                        }
                    }
                    catch (Exception exception)
                    {
                        ShowInformation(StaticKeys.UpdateErrorOccured);
                        result = DBScriptProcessState.ErrorOccured;
                    }
                }
                catch (Exception exception)
                {
                    ShowInformation(StaticKeys.UpdateErrorOccured);
                    result = DBScriptProcessState.ErrorOccured;
                }

                downloadLogStreamReader.Close();
            }
            else
            {
                result = DBScriptProcessState.ScriptMissing;
            }

            HideSplash();

            //Delete the download folder regardless if scripts worked or not.
            ReleaseScriptsDownload.DeleteDownloadsFolder();

            return result;
        }

        #endregion

        #endregion

        #region Images

        /// <summary>
        /// Checks if there are any available images and download them if found
        /// </summary>
        /// <returns></returns>
        public static ProcessResult CheckImagesAndUpdate()
        {
            ShowWaitingPanel("Checking Image Updates ...");

            var result = ProcessResult.Succeed;

            var imagesCheckResult = ImagesDownload.CheckIfUpdatesAreAvailable();

            if (imagesCheckResult == FileToDownloadCheck.UpdatesAvailable)
            {
                try
                {
                    if (ShowConfirmQuestion("An update to images is available with " + ImagesDownload.FilesList.Count +
                                            " images, would you like to download the images now?") == DialogResult.Yes)
                    {
                        result.IsSucceed = ImagesDownload.CheckIfDownloadedOrDownloadNow(true);
                    }
                }
                catch (VitalDatabaseException exception)
                {
                }
            }
            else if (imagesCheckResult == FileToDownloadCheck.UpdateCheckErrorOccured)
            {
                result.IsSucceed = false;
            }

            return result;
        }
        
        #endregion

        #region URL Directory

        /// <summary>
        /// Get list of files from a URL
        /// </summary>
        /// <param name="baseURL"></param>
        /// <returns></returns>
        public static List<string> GetFilesInURL(string baseURL)
        {
            var filesList = new List<string>();

            try
            {
                var baseUri = new Uri(baseURL.TrimEnd('/'));
                var rootUrl = baseUri.GetLeftPart(UriPartial.Authority);
                var regexFile = new Regex("[0-9] <a href=\"(http:|https:)?(?<file>.*?)\"", RegexOptions.IgnoreCase);
                var html = ReadHtmlContentFromUrl(baseURL);

                var matchesFile = regexFile.Matches(html);

                if (matchesFile.Count != 0)
                {
                    foreach (Match match in matchesFile)
                    {
                        if (match.Success)
                        {
                            filesList.Add(Path.GetFileName((new Uri(rootUrl + match.Groups["file"])).LocalPath));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return filesList;
        }

        /// <summary>
        /// Get the HTML content of a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReadHtmlContentFromUrl(string url)
        {
            var html = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        #endregion

        #region Administrative Access

        /// <summary>
        /// Checks if the current application is run in admin mode
        /// </summary>
        /// <returns></returns>
        public static bool IsRunAsAdministrator()
        {
            return VitalLogicExecuter.IsRunAsAdministrator();
        }

        #endregion

        #region Exceptions Reporting

        /// <summary>
        /// Send exception report
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="isHandledException"></param>
        /// <param name="showError"></param>
        public static void SendExceptionReport(Exception exception,bool isHandledException = true,  bool showError = false)
        {
            try
            {
                if (exception == null)
                {
                    return;
                }

                if (IsInternetOnline())
                {
                    var stackTrace = new StackTrace(exception, true);

                    var className = string.Empty;
                    var methodName = string.Empty;
                    var lineNumber = 0;
                    var columnNumber = 0;

                    try
                    {
                        className = stackTrace.GetFrame(0).GetMethod().ReflectedType.FullName;
                        methodName = stackTrace.GetFrame(0).GetMethod().Name;
                        lineNumber = stackTrace.GetFrame(0).GetFileLineNumber();
                        columnNumber = stackTrace.GetFrame(0).GetFileColumnNumber();
                    }
                    catch (Exception)
                    {
                    }

                    //Collect all inner exception messages in one message
                    var allExceptionMessages = exception.GetaAllMessages();

                    var fullExceptionMessage = string.Format(StaticKeys.ExceptionDetailsFormat,
                                                             className,
                                                             methodName,
                                                             lineNumber,
                                                             columnNumber,
                                                             exception.Message,
                                                             allExceptionMessages,
                                                             Environment.StackTrace);

                    //Set default exception email subject
                    var exceptionEmailSubject = StaticKeys.ExceptionReportTitle;

                    //Set email subject to include some of the exception message
                    if (!string.IsNullOrEmpty(exception.Message))
                    {
                        exceptionEmailSubject = "Exception:" + exception.Message;
                        exceptionEmailSubject = exceptionEmailSubject.Length <= StaticKeys.ExceptionEmailSubjectLength ? exceptionEmailSubject : exceptionEmailSubject.Substring(0, StaticKeys.ExceptionEmailSubjectLength - 1);
                    }

                    MailHelper.SendExcecptionMailIfOnline(GetVitalEmail().ExceptionSenderEmail,
                                                          StaticKeys.AppName,
                                                          exceptionEmailSubject,
                                                          fullExceptionMessage);
                }

                if (showError)
                {
                    ShowError(StaticKeys.ErrorMessageTitle, exception);    
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Common Logic

        /// <summary>
        /// Sets the datasource of a lookup
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="dataSource"></param>
        public static void FillLookup(LookUpEdit lookup, object dataSource)
        {
            lookup.Properties.DataSource = dataSource;
        }

        /// <summary>
        /// Sets the datasource of a reposiroty lookup
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="dataSource"></param>
        public static void FillLookup(RepositoryItemLookUpEdit lookup, object dataSource)
        {
            lookup.DataSource = dataSource;
            lookup.ForceInitialize();
        }

        /// <summary>
        /// Binds a control to the specified property in the data source
        /// </summary>
        /// <param name="bindedControl">The binded control.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="dataMember">The data member.</param>
        public static void BindControl(Control bindedControl, string propertyName, object dataSource, string dataMember)
        {
            try
            {
                bindedControl.DataBindings.Clear();
                bindedControl.DataBindings.Add(propertyName, dataSource, dataMember, true);
                bindedControl.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                bindedControl.DataBindings[0].ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
            }
            catch (NullReferenceException nullReferenceException)
            {
                throw new VitalBaseException(nullReferenceException.InnerException.Message);
            }
            catch (ArgumentException argumentException)
            {
                throw new VitalBaseException(argumentException.InnerException.Message);
            }
        }

        /// <summary>
        /// Binds a control to the specified property in the data source
        /// </summary>
        /// <param name="bindedControl">The binded control.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="expression">The expression of the property</param>
        public static void BindControl<T>(Control bindedControl, object dataSource, Expression<Func<T>> expression)
        {
            var propertyName = GetPropertyNameByControl(bindedControl);
            var dataMember = ExpressionHelper.GetPropertyName(expression);

            BindControl(bindedControl, propertyName, dataSource, dataMember);
        }

        /// <summary>
        /// Binds a control to the specified property in the data source
        /// </summary>
        /// <param name="bindedControl">The binded control.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="expression">The expression of the property</param>
        public static void BindControl<T>(Control bindedControl,  string propertyName, object dataSource, Expression<Func<T>> expression)
        {
            var dataMember = ExpressionHelper.GetPropertyName(expression);

            BindControl(bindedControl, propertyName, dataSource, dataMember);
        }

        /// <summary>
        /// Binds a control to the specified property in the data source
        /// </summary>
        /// <param name="bindedControl">The binded control.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="expression">The expression of the property</param>
        /// <param name="subExpression"></param>
        public static void BindControl<T, TS>(Control bindedControl, object dataSource, Expression<Func<T>> expression, Expression<Func<TS>> subExpression)
        {
            var propertyName = GetPropertyNameByControl(bindedControl);
            var dataMember = ExpressionHelper.GetPropertyName(expression, subExpression);

            BindControl(bindedControl, propertyName, dataSource, dataMember);
        }

        /// <summary>
        /// Gets the property name based on the control type
        /// </summary>
        /// <param name="bindedControl"></param>
        /// <returns></returns>
        private static string GetPropertyNameByControl(Control bindedControl)
        {
            if(bindedControl.GetType() == typeof(RichEditControl))
            {
                return StaticKeys.TextRtfPropertyName;
            }

            if (bindedControl.GetType() == typeof(TextEdit) ||
                bindedControl.GetType() == typeof(MemoEdit) ||
                bindedControl.GetType() == typeof(XtraTabPage))
            {
                return StaticKeys.TextPropertyname;
            }

            if (bindedControl.GetType() == typeof(SpinEdit) ||
                bindedControl.GetType() == typeof(LookUpEdit) ||
                bindedControl.GetType() == typeof(GridLookUpEdit) ||
                bindedControl.GetType() == typeof(DateEdit))
            {
                return StaticKeys.EditValuePropertyname;
            }

            if (bindedControl.GetType() == typeof(PictureEdit))
                return StaticKeys.ImagePropertyname;

            if (bindedControl.GetType() == typeof(CheckEdit))
            {
                return StaticKeys.CheckedPropertyname;
            }

            if (bindedControl.GetType() == typeof(GridControl) ||
                bindedControl.GetType() == typeof(TreeList))
            {
                return StaticKeys.DatasourcePropertyname;
            }

            return "Text";
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

        /// <summary>
        /// Expands all the detail views inside a grid
        /// </summary>
        /// <param name="view"></param>
        public static void ExpandAllRows(GridView view)
        {
            view.BeginUpdate();

            try
            {
                int dataRowCount = view.DataRowCount;
                for (var rHandle = 0; rHandle < dataRowCount; rHandle++)
                    view.SetMasterRowExpanded(rHandle, true);
            }
            finally
            {
                view.EndUpdate();
            }
        }

        /// <summary>
        /// Hides the expand button of the detail view in the grid when no details data exist
        /// </summary>
        /// <param name="view"></param>
        /// <param name="e"> </param>
        public static void HandleViewDetailExpandButton(GridView view,RowCellCustomDrawEventArgs e)
        {
            if (view != null)
            {
                if (e.Column.VisibleIndex == 0 && view.IsMasterRowEmpty(e.RowHandle))
                {
                    var cell = e.Cell as GridCellInfo;
                    if (cell != null)
                    {
                        cell.CellButtonRect = Rectangle.Empty;
                    }                    
                }
            }
        }

        /// <summary>
        /// Export the gridLookupEdit data to Csv file.
        /// </summary>
        /// <typeparam name="T">The data source list object Type</typeparam>
        /// <param name="view">The gridview.</param>
        /// <param name="owner">The Owner control.</param>
        /// <param name="defaultSaveGialogFileName">The default file name.</param>
        public static bool ExportToCsv<T>(GridView view, Form owner, string defaultSaveGialogFileName)
        {
            var selectedFileName = defaultSaveGialogFileName;

            try
            {
                var columns = view.Columns;
                var datasource = view.DataSource as BindingList<T>;

                if (columns == null || datasource == null)
                    return false;

                var saveFileDialog = new SaveFileDialog
                {
                    FileName = defaultSaveGialogFileName + StaticKeys.CsvFileExtintion,
                    Filter = StaticKeys.CsvFileFilter,
                    Title = StaticKeys.CsvSaveDialogTitle

                };

                if (saveFileDialog.ShowDialog(owner) == DialogResult.Cancel)
                    return false;

                ShowWaitingPanel(StaticKeys.ExportingToCsvWaitText);

                selectedFileName = saveFileDialog.FileName;

                var streamWriter = new StreamWriter(saveFileDialog.OpenFile());

                var excelRow = new StringBuilder();

                foreach (var column in columns)
                {
                    var gridColumn = column as GridColumn;

                    if (gridColumn == null || gridColumn.Tag.ToString() == StaticKeys.NotExportableColumn)
                        continue;

                    excelRow.Append(gridColumn.Caption);
                    excelRow.Append(",");
                }

                streamWriter.WriteLine(excelRow.ToString().TrimEnd(','));

                foreach (var data in datasource)
                {
                    excelRow.Clear();

                    foreach (var column in columns)
                    {
                        var gridColumn = column as GridColumn;

                        if (gridColumn == null || gridColumn.Tag.ToString() == StaticKeys.NotExportableColumn)
                            continue;

                        var dataValue = data.GetType().GetProperty(gridColumn.FieldName).GetValue(data, null);

                        var cellText = string.Empty;

                        if (dataValue != null)
                            cellText = dataValue.ToString();

                        excelRow.Append(cellText);
                        excelRow.Append(",");

                    }

                    streamWriter.WriteLine(excelRow.ToString().TrimEnd(','));
                }

                streamWriter.Close();

                HideSplash();

                return true;
            }
            catch (IOException exception)
            {
                HideSplash();
                ShowError(StaticKeys.ExportToCsvErrorTitle, string.Format(StaticKeys.ExportToCsvErrorText, selectedFileName));
                return false;
            }
            catch (Exception exception)
            {
                HideSplash();
                ShowError(StaticKeys.ExportToCsvErrorTitle, exception);
                return false;
            }

        }

        /// <summary>
        /// Get the character for certain keycodes
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static String KeycodeToChar(int keyCode)
        {
            var key = (Keys)keyCode;

            switch (key)
            {
                case Keys.Add:
                    return "+";
                case Keys.Decimal:
                    return ".";
                case Keys.Divide:
                    return "/";
                case Keys.Multiply:
                    return "*";
                case Keys.OemBackslash:
                    return "\\";
                case Keys.OemCloseBrackets:
                    return "]";
                case Keys.OemMinus:
                    return "-";
                case Keys.OemOpenBrackets:
                    return "[";
                case Keys.OemPeriod:
                    return ".";
                case Keys.OemPipe:
                    return "|";
                case Keys.OemQuestion:
                    return "/";
                case Keys.OemQuotes:
                    return "\"";
                case Keys.OemSemicolon:
                    return ";";
                case Keys.Oemcomma:
                    return ",";
                case Keys.Oemplus:
                    return "+";
                case Keys.Oemtilde:
                    return "`";
                case Keys.Separator:
                    return "-";
                case Keys.Subtract:
                    return "-";
                case Keys.D0:
                    return "0";
                case Keys.D1:
                    return "1";
                case Keys.D2:
                    return "2";
                case Keys.D3:
                    return "3";
                case Keys.D4:
                    return "4";
                case Keys.D5:
                    return "5";
                case Keys.D6:
                    return "6";
                case Keys.D7:
                    return "7";
                case Keys.D8:
                    return "8";
                case Keys.D9:
                    return "9";
                case Keys.Space:
                    return " ";
                default:
                    return key.ToString();
            }
        }

        /// <summary>
        /// Gets a specific lookup from cache
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Lookup GetSingleLookupFromCache(LookupsFilter filter)
        {
            //This code find the required lookup in cache but it then fills the values in it in a new lookup object
            //to prevent changes in the Lookups datasource that are also being loaded from the cache, if we use the
            //same object from cache then it will be loaded by reference and when makes changes to that lookup
            //it will also change the lookup that is in cache
            var cachedLookup = ((BindingList<Lookup>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Lookups))
                .FirstOrDefault(l => l.Type == filter.Type && l.Value == filter.Value);
            if (cachedLookup != null)
            {
                return new Lookup()
                {
                    CreationDateTime = cachedLookup.CreationDateTime,
                    UpdatedDateTime = cachedLookup.UpdatedDateTime,
                    ErrorSummary = cachedLookup.ErrorSummary,
                    Id = cachedLookup.Id,
                    ObjectState = cachedLookup.ObjectState,
                    User = cachedLookup.User,
                    Type = cachedLookup.Type,
                    Key = cachedLookup.Key,
                    ValidationErrors = cachedLookup.ValidationErrors,
                    Value = cachedLookup.Value
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a specific lookup from cache
        /// </summary>
        /// <returns></returns>
        public static Lookup GetSingleLookupFromCacheByKey(string key)
        {
            //This code find the required lookup in cache but it then fills the values in it in a new lookup object
            //to prevent changes in the Lookups datasource that are also being loaded from the cache, if we use the
            //same object from cache then it will be loaded by reference and when makes changes to that lookup
            //it will also change the lookup that is in cache
            var cachedLookup = ((BindingList<Lookup>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Lookups)) .FirstOrDefault(l => l.Key == key);
            if (cachedLookup != null)
            {
                return new Lookup()
                {
                    CreationDateTime = cachedLookup.CreationDateTime,
                    UpdatedDateTime = cachedLookup.UpdatedDateTime,
                    ErrorSummary = cachedLookup.ErrorSummary,
                    Id = cachedLookup.Id,
                    ObjectState = cachedLookup.ObjectState,
                    User = cachedLookup.User,
                    Type = cachedLookup.Type,
                    Key = cachedLookup.Key,
                    ValidationErrors = cachedLookup.ValidationErrors,
                    Value = cachedLookup.Value
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a specific lookup from cache
        /// </summary>
        /// <returns></returns>
        public static Lookup GetSingleLookupFromCache<T, TV>(T type, TV key, bool isMultiWordType = false, bool isMultiWordValue = false)
        {
            return GetSingleLookupFromCache(LookupsFilter.As(type, key, isMultiWordType, isMultiWordValue));
        }

        /// <summary>
        /// Gets a lookup by type from cache
        /// </summary>
        /// <returns></returns>
        public static List<Lookup> GetLookupByTypeFromCache<T>(T type, bool isMultiWordType = false)
        {
            var lookups = (List<Lookup>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Lookups);

            return new List<Lookup>(lookups.Where(l => l.Type == EnumNameResolver.Resolve(type, isMultiWordType)));
        }

        /// <summary>
        /// Gets a list of lookups as LookupEnumInfo from cache by lookup type
        /// </summary>
        /// <returns></returns>
        public static List<LookupEnumInfo<TV>> GetLookupEnumInfoByTypeFromCache<TV>(LookupTypes type, bool isMultiWordType = false)
        {
            //We pass the type value here even though we can extract it from the TV type, the reason we pass it is that it is safer this way
            //as we might run into situation where the passed TV type is not listed under the LookupTypes enum or it is listed but there is a typo
            //or the text is different.
            //Enable the line below only if you decided to remove the "type" parameter and you want to extract it from the TV type using the line below
            //var type = EnumNameResolver.StringAsEnum<LookupTypes>(typeof(TV).ToString());

            var lookups = (List<Lookup>)CacheHelper.SetOrGetCachableData(CachableDataEnum.Lookups);
            var filteredLookups = new List<Lookup>(lookups.Where(l => l.Type == EnumNameResolver.Resolve(type, isMultiWordType)));

            return filteredLookups.Select(lookup => new LookupEnumInfo<TV> { LookupBO = lookup }).ToList();
        }

        public static Lookup GetLookupBySetting(SettingKeys settingKey, CachableDataEnum settingsGroup)
        {
            int lookupId = int.Parse(GetSettingValueFromCache(settingKey,settingsGroup).ToString());
            var lookupsManager = new LookupsManager();
            return lookupsManager.GetLookupById(new SingleItemFilter() {ItemId = lookupId});
        }

        /// <summary>
        /// Gets a specific setting from cache
        /// </summary>        
        /// <returns></returns>
        public static Setting GetSettingFromCache(SettingKeys settingKey, CachableDataEnum settingsGroup)
        {
            var settingsList =CacheHelper.SetOrGetCachableData(settingsGroup) as BindingList<Setting>;

            if (settingsList == null)
                return null;

            var setting = settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(settingKey)));

            return setting;            
        }

        /// <summary>
        /// Gets the value of a technician info setting by key
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public static string GetTechnicianInfo(SettingKeys settingKey)
        {
            var value  = UiHelperClass.GetSettingValueFromCache(settingKey,CachableDataEnum.VisibleSettings);
            return value == null ? "":value.ToString();
        }

        /// <summary>
        /// Gets a specific setting value from cache
        /// </summary>        
        /// <returns></returns>
        public static Object GetSettingValueFromCache(SettingKeys settingKey, CachableDataEnum settingsGroup)
        {
            var settingsList = CacheHelper.SetOrGetCachableData(settingsGroup) as BindingList<Setting>;

            if (settingsList == null)
                return null;

            var setting = settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(settingKey)));

            return setting == null ? null : setting.Value;
        }

        /// <summary>
        /// Finds if a lookup matches a source lookup using filter by ID comparison
        /// </summary>
        /// <param name="lookup">Lookup to Check</param>
        /// <param name="filter">Filter used to find source lookup in cache</param>
        /// <returns></returns>
        public static bool IsLookupMatchingFilter(Lookup lookup,LookupsFilter filter)
        {
            var foundLookup = GetSingleLookupFromCache(filter);
            if (foundLookup == null || lookup == null) return false;

            return foundLookup.Id == lookup.Id;
        }

        /// <summary>
        /// Gets a lookup by type from cache
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static BindingList<Lookup> GetLookupByTypeFromCache(LookupsFilter filter)
        {
            var cacheValue = CacheHelper.SetOrGetCachableData(CachableDataEnum.Lookups);
            var allLookups = (BindingList<Lookup>)cacheValue;
            return allLookups.Where(l => l.Type == filter.Type).ToBindingList();
        }

        /// <summary>
        /// Get if the setting key for the option check box is checked or not.
        /// </summary>
        public static bool GetSettingCheckValue(CachableDataEnum cachableDataEnum, SettingKeys settingKey)
        {
            if (GetYesLookupId() == 0)
                return false;

            var setting = GetSetting(cachableDataEnum, settingKey);

            if (setting == null)
                return false;

            int settingValue;

            if (!int.TryParse(setting.Value.ToString(), out settingValue))
                return false;

            return settingValue == GetYesLookupId();
        }

        /// <summary>
        /// Process on check status change.
        /// </summary>
        public static void UpdateCheckSettingValue(CachableDataEnum cachableDataEnum, CheckEdit checkEdit, SettingsManager _settingsManager)
        {
            try
            {
                if (checkEdit == null || checkEdit.Tag == null)
                    return;

                ShowWaitingPanel(StaticKeys.ApplingChange);

                var settingKeyEnum = (SettingKeys)(checkEdit.Tag);

                SaveChange(cachableDataEnum, settingKeyEnum, checkEdit.Checked ? GetYesLookupId() : GetNoLookupId(), _settingsManager);

                HideSplash();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Process on check status change.
        /// </summary>
        public static bool UpdateCheckSettingValue(CachableDataEnum cachableDataEnum,
                                                    SettingKeys key,
                                                    bool value,
                                                    SettingsManager _settingsManager)
        {
            try
            {
                var result = true;

                ShowWaitingPanel(StaticKeys.ApplingChange);

                result = SaveChange(cachableDataEnum, key, value ? GetYesLookupId() : GetNoLookupId(), _settingsManager);

                HideSplash();
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Save the change on settings.
        /// </summary>
        public static bool SaveChange(CachableDataEnum cachableDataEnum, SettingKeys settingKey, object value, SettingsManager _settingsManager)
        {
            var settingsList =
                CacheHelper.SetOrGetCachableData(cachableDataEnum) as BindingList<Setting>;

            if (settingsList == null)
                return false;

            var setting = GetSetting(cachableDataEnum, settingKey);

            if (setting == null)
                return false;

            setting.Value = value.ToString();

            return _settingsManager.Save(setting).IsSucceed;
        }

        /// <summary>
        /// Get setting form cache.
        /// </summary>
        public static Setting GetSetting(CachableDataEnum cachableDataEnum, SettingKeys settingKey)
        {
            var settingsList = CacheHelper.SetOrGetCachableData(cachableDataEnum) as BindingList<Setting>;

            return settingsList == null ? null : settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(settingKey)));
        }

        /// <summary>
        /// Gets the yes lookup Id
        /// </summary>
        /// <returns></returns>
        public static int? GetYesLookupId()
        {
            var yeslookup = GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.Yes));
            if (yeslookup != null)
            {
                return yeslookup.Id;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the no lookup Id
        /// </summary>
        /// <returns></returns>
        public static int? GetNoLookupId()
        {
            var nolookup = GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.YesNo, YesNoEnum.No));
            if (nolookup != null)
            {
                return nolookup.Id;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the item system source lookup Id
        /// </summary>
        /// <returns></returns>
        public static int? GetSystemItemSourceLookupId()
        {
            var systemItemSourcelookup = GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SourceType, SourceTypeEnum.SystemItem));
            if (systemItemSourcelookup != null)
            {
                return systemItemSourcelookup.Id;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the item user new lookup Id
        /// </summary>
        /// <returns></returns>
        public static int? GetUserNewItemSourceLookupId()
        {
            var systemItemSourcelookup = GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SourceType, SourceTypeEnum.UserNewItem));
            if (systemItemSourcelookup != null)
            {
                return systemItemSourcelookup.Id;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check internet online connectivity
        /// </summary>
        /// <returns></returns>
        public static bool IsInternetOnline()
        {
            return InternetHelper.IsInternetOnline();
        }

        /// <summary>
        /// Adds line to message string
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="addNewLine"></param>
        /// <param name="prefix"></param>
        /// <param name="line"></param>
        private static void AddLineToMessage(ref string msg, bool addNewLine, string prefix, string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return;
            }

            msg += (addNewLine ? System.Environment.NewLine : string.Empty) + prefix + line;
        }

        /// <summary>
        /// Logic for sending a shipment order
        /// </summary>
        /// <param name="ShippingOrderObject"></param>
        /// <param name="TestObject"></param>
        /// <param name="showErrors"></param>
        /// <param name="sendAsHTML"></param>
        /// <param name="printingOptions"></param>
        /// <returns></returns>
        public static ProcessResult SendShipmentOrder(ShippingOrder ShippingOrderObject, 
                                                      Test TestObject,
                                                      bool showErrors,
                                                      bool sendAsHTML,
                                                      XtraUserControlPrintingOptions printingOptions,
                                                      bool checkInternet)
        {
            try
            {
                if (!ShippingOrderObject.ProductsIncluded)
                {
                    HideSplash();
                    if (showErrors)
                    {
                        ShowError("No Products Selected",
                                  "At least one product must be checked for shipment with at least one item to ship.");
                    }
                    return ProcessResult.Failed;
                }

                ShowWaitingPanel("Checking Internet Connection ...");
                if (checkInternet && !IsInternetOnline())
                {
                    HideSplash();
                    if (showErrors)
                    {
                        ShowError("Internet Connection Needed",
                            "Internet connection is needed to send order details, please check your internet connection and try again.");    
                    }

                    return ProcessResult.Failed;
                }
            }
            catch (Exception exception)
            {
                if (showErrors)
                {
                    ShowError(exception.Message, exception);    
                }
                HideSplash();
                return ProcessResult.Failed;
            }

            try
            {
                var result = ProcessResult.Failed;
                ShowWaitingPanel("Sending Shipping Order ...");

                var subject = "Order #:[" + ShippingOrderObject.Number +
                              "]-By-" + ShippingOrderObject.TechnicianName;

                ShippingOrderObject.Sent = true;
                ShippingOrderObject.SentDate = DateTime.Now;

                //if (!sendAsHTML)
                //{
                //    var messageBody = string.Empty;
                //    AddLineToMessage(ref messageBody, false, string.Empty, "Shipping Order:");
                //    AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorLong);
                //    AddLineToMessage(ref messageBody, true, "Number     :", ShippingOrderObject.Number.ToString());

                //    AddLineToMessage(ref messageBody, true, string.Empty, "Receiver Info:");                    

                //    if (!ShippingOrderObject.SendToClient)
                //    {
                //        AddLineToMessage(ref messageBody, true, string.Empty, "Send to Technician");
                //        AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorShort);
                //        AddLineToMessage(ref messageBody, true, "Name     :", ShippingOrderObject.TechnicianName);
                //        AddLineToMessage(ref messageBody, true, "Address :", ShippingOrderObject.TechnicianAddress);
                //        AddLineToMessage(ref messageBody, true, "City        :", ShippingOrderObject.TechnicianCity);
                //        AddLineToMessage(ref messageBody, true, "State      :", ShippingOrderObject.TechnicianState);
                //        AddLineToMessage(ref messageBody, true, "Zip Code:", ShippingOrderObject.TechnicianZipCode);
                //        AddLineToMessage(ref messageBody, true, "Phone    :", ShippingOrderObject.TechnicianPhone);
                //        AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorLong);
                //    }
                //    else
                //    {
                //        AddLineToMessage(ref messageBody, true, string.Empty, "Send to Client");
                //        AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorShort);
                //        AddLineToMessage(ref messageBody, true, "Name            :", ShippingOrderObject.PatientFirstName + " " + ShippingOrderObject.PatientLastName);
                //        AddLineToMessage(ref messageBody, true, "Address 1     :", ShippingOrderObject.PatientAddress1);
                //        AddLineToMessage(ref messageBody, true, "Address 2     :", ShippingOrderObject.PatientAddress2);
                //        AddLineToMessage(ref messageBody, true, "City                :", ShippingOrderObject.PatientCity);
                //        AddLineToMessage(ref messageBody, true, "State              :", ShippingOrderObject.PatientState);
                //        AddLineToMessage(ref messageBody, true, "Zip Code        :", ShippingOrderObject.PatientZip);
                //        AddLineToMessage(ref messageBody, true, "Home Phone :", ShippingOrderObject.PatientHomePhone);
                //        AddLineToMessage(ref messageBody, true, "Work Phone  :", ShippingOrderObject.PatientWorkPhone);
                //        AddLineToMessage(ref messageBody, true, "Cell Phone     :", ShippingOrderObject.PatientCellPhone);
                //        AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorLong);
                //    }

                //    AddLineToMessage(ref messageBody, true, string.Empty, "Shipment Details:");
                //    AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorShort);

                //    foreach (var product in ShippingOrderObject.OrderItems.Where(sl => sl.Include))
                //    {
                //        AddLineToMessage(ref messageBody, true, "- ", product.Item.Name + " : " +
                //                                                      product.Quantity +
                //                                                      (string.IsNullOrEmpty(product.Comments) ? "" :
                //                                                      " - " + product.Comments));
                //    }

                //    AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorLong);

                //    AddLineToMessage(ref messageBody, true, string.Empty, "Technician Comments:");
                //    AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorShort);
                //    AddLineToMessage(ref messageBody, true, string.Empty, ShippingOrderObject.Comments);
                //    AddLineToMessage(ref messageBody, true, string.Empty, StaticKeys.DashSeperatorLong);

                //    result = MailHelper.SendMail(StaticKeys.MailClientNetworkCredentialsUsername,
                //                                ShippingOrderObject.TechnicianName,
                //                                subject,
                //                                messageBody,false,true);
                //}
                //else
                //{
                    
                //}

                var report = new XtraReportShippingOrder()
                {
                    bindingSourceShippingOrder = { DataSource = ShippingOrderObject },
                    HideLogo = { Value = printingOptions.HideLogo },
                    ForEmail = { Value = true },
                };

                SetReportLogo(report.xrSubreportHeader);

                var shippingEmail = GetVitalEmail().ShippingTargetEmail;

                result = MailHelper.SendShippingReportInHTML(report,
                                                              shippingEmail,
                                                              ShippingOrderObject.TechnicianName,
                                                              subject);

                if (TestObject != null)
                {
                    TestObject.IsOrderSent = true;
                    TestObject.StateLookup = GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.TestState, TestStateEnum.DoneShipped, false, false));
                }

                if (!result.IsSucceed)
                {
                    ShippingOrderObject.Sent = false;
                    ShippingOrderObject.SentDate = null;
                }

                HideSplash();
                return result;
            }
            catch (Exception exception)
            {
                if (showErrors)
                {
                    ShowError(exception.Message, exception);    
                }
                HideSplash();
                return ProcessResult.Failed;
            }           
        }

        /// <summary>
        /// Convert to int representing bytes to a file size string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToFileSize(long source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }
            else if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }
            else //Bytes
            {
                return string.Concat(bytes, " Bytes");
            }
        }

        /// <summary>
        /// Returns true if the file path exists
        /// </summary>
        public static bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Removes all characters from string and only keeps letters and numberz
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string AlphaNumericOnly(string content)
        {
            return Regex.Replace(content, @"[^A-Za-z0-9]+", "");
        }

        /// <summary>
        /// Refreshes grid data
        /// </summary>
        /// <param name="gridControl"></param>
        public static void RefreshGridData(GridControl gridControl)
        {
            gridControl.PerformSafely(gridControl.RefreshDataSource);
            gridControl.PerformSafely(() => gridControl.DefaultView.RefreshData());
            gridControl.PerformSafely(gridControl.Refresh);
        }

        /// <summary>
        /// Refreshes treelist data
        /// </summary>
        /// <param name="treeList"></param>
        public static void RefreshTreeListData(TreeList treeList)
        {
            treeList.PerformSafely(treeList.RefreshDataSource);
            treeList.PerformSafely(treeList.Refresh);
        }

        #endregion

        #region Grid RepositoryItem Using Refliction Manager Methods

        /// <summary>
        /// Get the RepositoryItem base on reflection.
        /// </summary>
        /// <param name="currentRepositoryItem"></param>
        /// <param name="valueType"></param>
        /// <param name="sourceConfig"></param>
        /// <param name="membersConfig"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static RepositoryItem GetRepositoryItemByReflection(RepositoryItem currentRepositoryItem, ValueTypes valueType, string sourceConfig, string membersConfig, string caption)
        {
            var reflectionManager = new ReflectionManager();

            if (currentRepositoryItem != null)
            {
                return currentRepositoryItem;
            }

            RepositoryItem rep = null;

            switch (valueType)
            {
                case ValueTypes.List:

                    var tempRepository = new RepositoryItemLookUpEdit();

                    if (!string.IsNullOrEmpty(sourceConfig))
                    {
                        tempRepository.DataSource = reflectionManager.Exucute(sourceConfig);
                    }

                    if (!string.IsNullOrEmpty(membersConfig))
                    {
                        reflectionManager.SetMembers(tempRepository, membersConfig);
                    }

                    tempRepository.NullText = string.Empty;
                    tempRepository.Columns.Add(new LookUpColumnInfo(tempRepository.DisplayMember, caption));

                    rep = tempRepository;

                    break;

                case ValueTypes.Numerical:

                    var repositoryItemSpinEdit = new RepositoryItemSpinEdit();

                    if (!string.IsNullOrEmpty(membersConfig))
                    {
                        repositoryItemSpinEdit.Mask.ShowPlaceHolders = false;
                        repositoryItemSpinEdit.Mask.UseMaskAsDisplayFormat = true;
                        repositoryItemSpinEdit.Mask.MaskType = MaskType.Numeric;
                        repositoryItemSpinEdit.AllowNullInput = DefaultBoolean.False;
                        reflectionManager.SetMembers(repositoryItemSpinEdit.Mask, membersConfig);
                        reflectionManager.SetMembers(repositoryItemSpinEdit, membersConfig);
                    }

                    rep = repositoryItemSpinEdit;

                    break;

                case ValueTypes.Text:
                case ValueTypes.Memo:

                    var repositoryItemTextingEdit = valueType == ValueTypes.Text ? new RepositoryItemTextEdit() : new RepositoryItemMemoExEdit {ShowIcon = false, ScrollBars = ScrollBars.Vertical};

                    if (!string.IsNullOrEmpty(membersConfig))
                    {
                        repositoryItemTextingEdit.Mask.MaskType = MaskType.RegEx;
                        repositoryItemTextingEdit.Mask.ShowPlaceHolders = false;
                        reflectionManager.SetMembers(repositoryItemTextingEdit.Mask, membersConfig,true);
                    }

                    rep = repositoryItemTextingEdit;

                    break;

            }

            return rep;
        }

        /// <summary>
        /// Set the Reflection base grid properties.
        /// </summary>
        /// <param name="view"></param>
        public static void SetReflectionBaseViewProperties(GridView view)
        {
            SetViewProperties(view);
            _reflictionGridForm = view.GridControl.FindForm();
            view.CustomUnboundColumnData += GridViewReflectionBase_CustomUnboundColumnData;
            view.CustomRowCellEdit += GridViewReflectionBase_CustomRowCellEdit;
        }

        /// <summary>
        /// Bounds for unbounded cells.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The event.</param>
        private static void GridViewReflectionBase_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (_reflictionGridForm == null)
                return;

            if (_reflictionGridForm.IsHandleCreated && _reflictionGridForm.InvokeRequired)
            {
                try
                {
                    if (_reflictionGridForm.IsDisposed) return;
                    _reflictionGridForm.Invoke(new CustomColumnDataEventHandler(GridViewReflectionBase_CustomUnboundColumnData), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var rowObject = e.Row as DomainEntityRefluctionBase;

                if (rowObject == null || !e.Column.FieldName.Equals(StaticKeys.UnboundValueFieldName)) return;

                if (e.IsGetData)
                {
                    if (rowObject.ValueTypeLookup == null || rowObject.ValueTypeLookup.Value == null) return;

                    float valueAsNum;

                    var isNum = float.TryParse(rowObject.Value != null ? rowObject.Value.ToString() : "0",
                                               out valueAsNum);

                    var valueType = EnumNameResolver.LookupAsEnum<ValueTypes>(rowObject.ValueTypeLookup.Value);

                    e.Value = valueType == ValueTypes.Numerical
                                  ? valueAsNum
                                  : (isNum && valueType == ValueTypes.List) ? (int) valueAsNum : rowObject.Value;
                }
                else
                {
                    rowObject.Value = e.Value;
                }
            }
        }

        /// <summary>
        /// Custom row cell edit to assign editor in individual cells grid.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The event.</param>
        private static void GridViewReflectionBase_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (_reflictionGridForm == null)
                return;

            if (_reflictionGridForm.IsHandleCreated && _reflictionGridForm.InvokeRequired)
            {
                try
                {
                    if (_reflictionGridForm.IsDisposed) return;
                    _reflictionGridForm.Invoke(new CustomColumnDataEventHandler(GridViewReflectionBase_CustomUnboundColumnData), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                var gv = sender as GridView;

                if (gv != null)
                {
                    var rowObject = gv.GetRow(e.RowHandle) as DomainEntityRefluctionBase;
                    if (rowObject == null) return;


                    if (e.Column.FieldName.Equals(StaticKeys.UnboundValueFieldName))
                    {

                        RepositoryItem repositoryItem;

                        if (rowObject.UiItemRepository == null)
                        {
                            if (rowObject.ValueTypeLookup.Value == null) return;

                            repositoryItem = GetRepositoryItemByReflection(
                                rowObject.UiItemRepository as RepositoryItem,
                                EnumNameResolver.LookupAsEnum<ValueTypes>(rowObject.ValueTypeLookup.Value),
                                rowObject.SourceConfig, rowObject.MembersConfig,
                                rowObject.Caption);
                            if (repositoryItem == null) return;

                            gv.GridControl.RepositoryItems.Add(repositoryItem);
                            rowObject.UiItemRepository = repositoryItem;

                            if (rowObject.Value == null || !rowObject.Value.ToString().Equals(e.CellValue.ToString()))
                                rowObject.Value = e.CellValue;
                        }
                        else
                        {
                            repositoryItem = rowObject.UiItemRepository as RepositoryItem;
                        }

                        e.RepositoryItem = repositoryItem;
                    }
                }
            }
        }

        #endregion

        #region Report Logic

        /// <summary>
        /// Check if report has data to print
        /// </summary>
        /// <param name="printingOptions"></param>
        /// <returns></returns>
        public static bool HasPrintableInfo(XtraUserControlPrintingOptions printingOptions)
        {
            if (!printingOptions.ShowEDS && !printingOptions.ShowResults)
            {
                ShowInformation("At least EDS or Test Results should be checked in printing options.", "Nothing to print");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set the report logo
        /// </summary>
        /// <param name="subReport"></param>
        public static void SetReportLogo(XRSubreport subReport)
        {
            if (subReport.ReportSource.GetType() == typeof(XtraReportReportHeader))
            {
                ((XtraReportReportHeader)subReport.ReportSource).xrPictureBoxLogo.Image = (Image)CacheHelper.SetOrGetCachableData(CachableDataEnum.Logo);

                //Set report clinic info text
                var settingsList = CacheHelper.SetOrGetCachableData(CachableDataEnum.PrintingSettings) as BindingList<Setting>;

                var setting = settingsList == null ? null : settingsList.FirstOrDefault(s => s.Key.Equals(EnumNameResolver.Resolve(SettingKeys.ClinicInfo)));

                ((XtraReportReportHeader)subReport.ReportSource).xrRichTextClinicInfo.Rtf = setting == null ? null : setting.Value.ToString();
            }
            else if (subReport.ReportSource.GetType() == typeof(XtraReportReportHeaderLogoOnly))
            {
                ((XtraReportReportHeaderLogoOnly)subReport.ReportSource).xrPictureBoxLogo.Image = (Image)CacheHelper.SetOrGetCachableData(CachableDataEnum.Logo);
            }
        }

        /// <summary>
        /// Logic to group dilutions insde an issue
        /// </summary>
        /// <param name="testIssue"></param>
        /// <param name="itemTypePotencyId"></param>
        private static void GroupIssueDilutions(TestIssue testIssue, int itemTypePotencyId)
        {
            var inDilutionsGroup = false;
            TestResult currentDilutionGroupParent = null;

            foreach (var testResult in testIssue.TestResults)
            {
                if (testResult.IsSelected &&
                    testResult.Item != null &&
                    testResult.Item.TypeLookup != null &&
                    testResult.Item.TypeLookup.Id == itemTypePotencyId)
                {

                    if (inDilutionsGroup)
                    {
                        currentDilutionGroupParent.CustomDilutions += "," + testResult.Item.Name;
                    }
                    else
                    {
                        inDilutionsGroup = true;
                        testResult.IsDilutionGroupParent = true;
                        currentDilutionGroupParent = testResult;
                        currentDilutionGroupParent.CustomDilutions = currentDilutionGroupParent.Item.Name;
                    }
                }
                else
                {
                    inDilutionsGroup = false;
                    currentDilutionGroupParent = null;
                }
            }
        }

        /// <summary>
        /// Logic for printing
        /// </summary>
        public static void PrintTestReport(bool isPreview,
                                           SettingsManager settingsManager,
                                           Patient patient, 
                                           List<Test> selectedTests, 
                                           int itemTypeProductId, 
                                           XtraUserControlPrintingOptions printingOptions,
                                           int? nonelistTypeLookupId,
                                           int? yesLookupId,
                                           int itemTypePotencyId)
        {
            foreach (var selectedTest in selectedTests)
            {
                TestHelper.GenerateReadingPointSets(selectedTest);

                if (printingOptions.GroupCustomDilutions)
                {
                    foreach (var testIssue in selectedTest.TestIssues)
                    {
                        GroupIssueDilutions(testIssue, itemTypePotencyId);
                    }

                    GroupIssueDilutions(selectedTest.TestMainIssue, itemTypePotencyId);
                }
            }
            
            var techName = string.Empty;
            var techAddress = string.Empty;
            var techPhone = string.Empty;
            var techState = string.Empty;
            var techCity = string.Empty;
            var techZip = string.Empty;

            if (printingOptions.ShowTechnicianInfo)
            {
                GetTechnicianInfo(settingsManager, ref techName, ref techAddress, ref techPhone, ref techState, ref techCity, ref techZip);
            }

            var report = new XtraReportTest
            {
                PatientName = { Value = patient.FirstName + " " + patient.LastName },
                bindingSourcePatient = { DataSource = patient },
                bindingSourceTests = { DataSource = selectedTests },
                ProductTypeId = { Value = itemTypeProductId },
                PotencyTypeId = { Value = itemTypePotencyId },
                YesLookupId = { Value = yesLookupId },
                ShowEDS = { Value = printingOptions.ShowEDS },
                ShowTestResults = { Value = printingOptions.ShowResults },
                ShowDescription = { Value = printingOptions.ShowDescriptionTemporaryOption },
                ShowMeridian = { Value = printingOptions.ShowMeridian },
                ShowDateWithoutTime = { Value = printingOptions.ShowDateWithoutTime },
                ShowPointFullName = { Value = printingOptions.ShowFullName },
                HidePatientName = { Value = printingOptions.HidePatientName },
                HideLogo = { Value = printingOptions.HideLogo },
                ShowNotes = { Value = printingOptions.ShowNotes },
                GroupDilutions = { Value = printingOptions.GroupCustomDilutions },
                ShowTechnicianInfo = { Value = printingOptions.ShowTechnicianInfo },
                TechnicianName = { Value = techName },
                TechnicianAddress = { Value = techAddress },
                TechnicianCity = { Value = techCity },
                TechnicianPhone = { Value = techPhone },
                TechnicianState = { Value = techState },
                TechnicianZip = { Value = techZip },
            };
            var manager = new SettingsManager();
            var setting = manager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.ReportDisclaimer) });

            var showFactorFullName = false;

            var factorFullName = ((BindingList<Setting>)CacheHelper.SetOrGetCachableData(CachableDataEnum.VisibleSettings))
                                    .FirstOrDefault(s => s.Key == EnumNameResolver.Resolve(SettingKeys.FourFactorsFullName));

            if (factorFullName != null)
            {
                showFactorFullName = Convert.ToInt32(factorFullName.Value) == yesLookupId;    
            }
            
            report.Disclaimer.Value = setting.Value;
            report.NoneListTypeId.Value = nonelistTypeLookupId ?? report.NoneListTypeId.Value;
            report.ShowFFFullName.Value = showFactorFullName;

            SetReportLogo(report.xrSubreportHeader);
            
            if (isPreview)
            {
                var reportViewer = new XtraFormReportViewer();
                reportViewer.SetReport(report);
                reportViewer.ShowDialog();
            }
            else
            {
                report.PrintDialog();
            }
        }


        /// <summary>
        /// Logic for printing an auto test.
        /// </summary>
        public static void PrintAutoTestReport(bool isPreview,
            SettingsManager settingsManager,
            Patient patient,
            List<AutoTest> selectedAutoTests,
            int itemTypeProductId,
            XtraUserControlPrintingOptions printingOptions,
            int? nonelistTypeLookupId,
            int? yesLookupId,
            int itemTypePotencyId)
        {
            //ToDo: Implement print auto test report.
            MessageBox.Show("ToDo: Implement print auto test report");
        }

        /// <summary>
        /// Logic for printing
        /// </summary>
        public static void PrintTestNotesReport(bool isPreview,
                                               Patient patient,
                                               List<Test> selectedTests,
                                               XtraUserControlPrintingOptions printingOptions)
        {
            var report = new XtraReportTestNotes
            {
                PatientName = { Value = patient.FirstName + " " + patient.LastName },
                bindingSourcePatient = { DataSource = patient },
                bindingSourceTests = { DataSource = selectedTests },
                HidePatientName = { Value = printingOptions.HidePatientName },
                HideLogo = { Value = printingOptions.HideLogo },
            };
            
            SetReportLogo(report.xrSubreportHeader);

            if (isPreview)
            {
                var reportViewer = new XtraFormReportViewer();
                reportViewer.SetReport(report);
                reportViewer.ShowDialog();
            }
            else
            {
                report.PrintDialog();
            }
        }

        /// <summary>
        /// Logic for printing the patient schedule report
        /// </summary>
        /// <param name="testObject"></param>
        /// <param name="lookupsManager"></param>
        /// <param name="settingsManager"></param>
        /// <param name="printingOptions"></param>
        public static void PrintPatientScheduleReport(Test testObject,
                                                      LookupsManager lookupsManager,
                                                      SettingsManager settingsManager,
                                                      XtraUserControlPrintingOptions printingOptions)
        {
            ShowWaitingPanel("Preparing Report");

            var techName = string.Empty;
            var techAddress = string.Empty;
            var techPhone = string.Empty;
            var techState = string.Empty;
            var techCity = string.Empty;
            var techZip = string.Empty;

            if (printingOptions.ShowTechnicianInfo)
            {
                GetTechnicianInfo(settingsManager, ref techName, ref techAddress, ref techPhone, ref techState, ref techCity, ref techZip);
            }

            var activeSchedules = testObject.TestSchedule.ScheduleLines.Where(l => !l.IsDeleted).ToBindingList();

            var report = new XtraReportPatientSchedule
            {
                PatientName = { Value = testObject.Patient.FirstName + " " + testObject.Patient.LastName },
                bindingSourcePatient = { DataSource = testObject.Patient },
                bindingSourceTestSchedule = { DataSource = testObject.TestSchedule },
                bindingSourceScheduleLine = { DataSource = activeSchedules },
                HidePatientName = { Value = printingOptions.HidePatientName },
                HideLogo = { Value = printingOptions.HideLogo },
                ShowTechnicianInfo = { Value = printingOptions.ShowTechnicianInfo },
                TechnicianName = { Value = techName },
                TechnicianAddress = { Value = techAddress },
                TechnicianCity = { Value = techCity },
                TechnicianPhone = { Value = techPhone },
                TechnicianState = { Value = techState },
                TechnicianZip = { Value = techZip },
                ShowDefaultInstructions = { Value = GetSettingCheckValue(CachableDataEnum.Settings, SettingKeys.ShowScheduleReportDefaultInstructions)},
            };
            SetReportLogo(report.xrSubreportHeader);
            
            HideSplash();
            ShowReport(report);
        }

        /// <summary>
        /// Logic for printing the patient schedule report for an auto test
        /// </summary>
        /// <param name="autoTestObject"></param>
        /// <param name="lookupsManager"></param>
        /// <param name="settingsManager"></param>
        /// <param name="printingOptions"></param>
        public static void PrintPatientAutoTestScheduleReport(AutoTest autoTestObject,
            LookupsManager lookupsManager,
            SettingsManager settingsManager,
            XtraUserControlPrintingOptions printingOptions)
        {
            //ToDo: Implement print auto test schedule report.
            MessageBox.Show("ToDo: Implement print auto test schedule report");
        }

        /// <summary>
        /// Get the tech info.
        /// </summary>
        private static void GetTechnicianInfo(SettingsManager settingsManager, ref string techName, ref string techAddress,
            ref string techPhone, ref string techState, ref string techCity, ref string techZip)
        {
            var technicianInfoGroupLookup =
                GetSingleLookupFromCache(LookupsFilter.As(LookupTypes.SettingGroup, SettingGroups.TechnicianInfo,
                    false, true));

            if (technicianInfoGroupLookup != null)
            {
                var settingsList =
                    settingsManager.GetSettings(new SettingsFilter {SettingGroupLookupId = technicianInfoGroupLookup.Id});
                var name = settingsList.FirstOrDefault(s => s.Key == SettingKeys.TechnicianName.ToString());
                var address = settingsList.FirstOrDefault(s => s.Key == SettingKeys.TechnicianAddress.ToString());
                var city = settingsList.FirstOrDefault(s => s.Key == SettingKeys.TechnicianCity.ToString());
                var phone = settingsList.FirstOrDefault(s => s.Key == SettingKeys.TechnicianPhone.ToString());
                var state = settingsList.FirstOrDefault(s => s.Key == SettingKeys.TechnicianState.ToString());
                var zip = settingsList.FirstOrDefault(s => s.Key == SettingKeys.TechnicianZip.ToString());

                techName = (name != null) ? name.Value.ToString() : techName;
                techAddress = (address != null) ? address.Value.ToString() : techAddress;
                techPhone = (phone != null) ? phone.Value.ToString() : techPhone;
                techState = (state != null) ? state.Value.ToString() : techState;
                techCity = (city != null) ? city.Value.ToString() : techCity;
                techZip = (zip != null) ? zip.Value.ToString() : techZip;
            }
        }

        /// <summary>
        /// Initialize app info manager
        /// </summary>
        private static void InitAppInfoManager()
        {
            if (_appInfoManager == null) _appInfoManager = new AppInfoManager();
        }

        /// <summary>
        /// Initialize security manager
        /// </summary>
        private static void InitSecurityManager()
        {
            if (_securityManager == null) _securityManager = new SecurityManager();
        }

        /// <summary>
        /// Get the tech info.
        /// </summary>
        public static string GetTechAndVitalAppInfo()
        {
            BackupTechInfo();

            var updateManager = new ApplicationUpdateManager();
            var appCurrentVersion = updateManager.CurrentPublishVersion;
            var dbCurrentVersion = new Version();
            var dataVersion = new Version();

            var versionAppInfo = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.Version) });
            if (versionAppInfo != null)
            {
                dbCurrentVersion = new Version(versionAppInfo.Value);
            }

            var dataStoredVersion = GetDbVersion(_appInfoManager);
            if (dataStoredVersion != null)
            {
                dataVersion = dataStoredVersion;
            }

            return
                "Branch #: " + AppBranch + "\n" +
                "Version #: " + appCurrentVersion + "\n" +
                "DB Version #: " + dbCurrentVersion + "\n" +
                "Data Version #: " + dataVersion + "\n" +
                "Check Dongle : " + CheckForDongle + "\n" +
                "Dongle Present : " + DonglePresent + "\n" +

                (!CheckForDongle || !DonglePresent
                    ? string.Empty
                    : "Dongle Number : " + DongleNumber + "\n" +
                      "Dongle Expiry Date : " + DongleExpiryDate.ToShortDateString() + "\n" +
                      "Is Licensed for Vital : " + IsLicensedForVital + "\n" +
                      "Is Licensed for CSAPV : " + IsLicensedForCSAPV + "\n") +

                "Technician Name: " + TechnicianName + "\n" +
                "Clinic Name: " + TechnicianClinicName + "\n" +
                "Clinic Website: " + TechnicianClinicWebsite + "\n" +
                "Address: " + TechnicianAddress + "\n" +
                "City: " + TechnicianCity + "\n" +
                "State: " + TechnicianState + "\n" +
                "Zip: " + TechnicianZip + "\n" +
                "Phone: " + TechnicianPhone + "\n" +
                "Email: " + TechnicianEmail + "\n" +
                "Key: " + VitalKey;
        }

        /// <summary>
        /// Gets the tech & app info as a dictionary list
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetTechAndAppInfoDictionaryList()
        {
            BackupTechInfo();

            var updateManager = new ApplicationUpdateManager();
            var appCurrentVersion = updateManager.CurrentPublishVersion;
            var dbCurrentVersion = new Version();
            var dataVersion = new Version();

            var versionAppInfo = _appInfoManager.GetAppInfoByProperty(new AppInfoFilter { Property = Enum.GetName(typeof(AppInfoKeys), AppInfoKeys.Version) });
            if (versionAppInfo != null)
            {
                dbCurrentVersion = new Version(versionAppInfo.Value);
            }

            var dataStoredVersion = GetDbVersion(_appInfoManager);
            if (dataStoredVersion != null)
            {
                dataVersion = dataStoredVersion;
            }

            var techInfoList =  new Dictionary<string, object>()
                    {
                        {"Branch", AppBranch },
                        {"Version #", appCurrentVersion },
                        {"DB Version #", dbCurrentVersion },
                        {"Data Version #", dataVersion },
                        {"Is Licensed for Vital", IsLicensedForVital },
                        {"Is Licensed for CSAPV", IsLicensedForCSAPV },
                        {"Technician Name", TechnicianName },
                        {"Clinic Name", TechnicianClinicName },
                        {"Clinic Website", TechnicianClinicWebsite },
                        {"Address", TechnicianAddress },
                        {"City", TechnicianCity },
                        {"State", TechnicianState },
                        {"Zip", TechnicianZip },
                        {"Phone", TechnicianPhone },
                        {"Email", TechnicianEmail },
                        {"Key", VitalKey},
                        {"Check Dongle", CheckForDongle },
                        {"Dongle Present", DonglePresent }
                    };

            if (CheckForDongle && DonglePresent)
            {
                techInfoList.Add("Dongle Number", DongleNumber);
                techInfoList.Add("Dongle Expiry Date", DongleExpiryDate.ToShortDateString());
            }

            return techInfoList;
        }

        /// <summary>
        /// Gets the name of the selected payment method.
        /// </summary>
        /// <returns></returns>
        private static string GetSelectedPaymentMethod(TestSchedule testSchedule)
        {
            if (testSchedule.IsCash)
                return EnumNameResolver.Resolve(PaymentMethodsEnum.Cash);

            if (testSchedule.IsCreditCard)
                return EnumNameResolver.Resolve(PaymentMethodsEnum.CreditCard, true);

            if (testSchedule.IsCheck)
                return EnumNameResolver.Resolve(PaymentMethodsEnum.Cheque);

            return string.Empty;
        }

        /// <summary>
        /// Logic for printing the shipping order
        /// </summary>
        /// <param name="shippingOrder">The shipping order object,</param>
        /// <param name="printingOptions">The printing options.</param>
        public static void PrintShippingOrderReport(ShippingOrder shippingOrder, XtraUserControlPrintingOptions printingOptions)
        {
            ShowWaitingPanel("Preparing Report");

            var report = new XtraReportShippingOrder()
            {
                bindingSourceShippingOrder = {DataSource = shippingOrder},
                HideLogo = { Value = printingOptions.HideLogo },
                ForEmail = { Value = false },
            };

            SetReportLogo(report.xrSubreportHeader);

            HideSplash();
            ShowReport(report);
        }

        /// <summary>
        /// Logic for printing the patient schedule report
        /// </summary>
        /// <param name="testObject">The test object,</param>        
        /// <param name="printingOptions">The printing options.</param>
        public static void PrintClientInvoice(Test testObject, XtraUserControlPrintingOptions printingOptions)
        {
            ShowWaitingPanel("Preparing Report");

            var activeSchedules = testObject.TestSchedule.ScheduleLines.Where(l => !l.IsDeleted).ToBindingList();

            var invoicesManager = new InvoicesManager();

            var invoice = new Invoice
            {
                IsFirstTimeAfterClosing = true,
                Test = testObject,
                Number = invoicesManager.GenerateInvoiceNumber(testObject.Patient.Id),
                TotalAmount = testObject.TestTotal,
                ChequeNumber = testObject.TestSchedule.CheckNumber,
                PaymentMethod = GetSelectedPaymentMethod(testObject.TestSchedule),
            };

            var report = new XtraReportProductInvoice
            {
                PatientName = { Value = testObject.Patient.FirstName + " " + testObject.Patient.LastName },
                bindingSourcePatient = { DataSource = testObject.Patient },
                bindingSourceTest = { DataSource = testObject },
                bindingSourceTestServices = { DataSource = testObject.TestServices },
                bindingSourceScheduleLine = { DataSource = activeSchedules },
                bindingSourceInvoice = { DataSource = invoice },
                HidePatientName = { Value = printingOptions.HidePatientName },
                HideLogo = { Value = printingOptions.HideLogo },
                ShowAddressInfo = { Value = printingOptions.ShowAddressInfo },
            };

            SetReportLogo(report.xrSubreportHeader);
            HideSplash();
            ShowReport(report);
        }
       
        /// <summary>
        /// Show the passed report.
        /// </summary>
        /// <param name="report"></param>
        public static void ShowReport(XtraReport report)
        {
            var reportViewer = new XtraFormReportViewer();
            
            reportViewer.SetReport(report);
            
            reportViewer.ShowDialog();
        }

        #endregion

        #region Backup and Restore

        /// <summary>
        /// Checks for recent backups and reminds user in case of manual mode
        /// </summary>
        public static void CheckForBackupReminder(SettingsManager settingsManager)
        {
            var isBackupAutomatic = (bool)GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutomaticBackup);

            if (!isBackupAutomatic)
            {
                var showBackupReminder = (bool)GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.ShowBackupReminder);

                if (showBackupReminder)
                {
                    var lastBackupDate = DateTime.Parse(GetSettingValueFromCache(SettingKeys.LastBackupDate, CachableDataEnum.BackupAndRestore).ToString());
                    var lastBackupNotificationDate = DateTime.Parse(GetSettingValueFromCache(SettingKeys.LastBackupNotificationDate, CachableDataEnum.BackupAndRestore).ToString());

                    if (DateTime.Today.Date.Subtract(lastBackupDate.Date).Days > 14 &&
                        DateTime.Today.Date.Subtract(lastBackupNotificationDate.Date).Days > 14)
                    {
                        ShowInformation("It has been more than 2 weeks since your last backup on '" + lastBackupDate.ToShortDateString() + "', Please make sure to backup your Vital database.");
                        SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupNotificationDate, DateTime.Now.Date.ToShortDateString(), settingsManager);
                    }
                }
            }
        }

        /// <summary>
        /// Checks for recent backup and creates a new backup in case specified period is passed
        /// </summary>
        public static void CheckForAutoBackup(SettingsManager settingsManager)
        {
            ShowWaitingPanel("Automatic Database Backup Check ...");

            var isBackupAutomatic = (bool)GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutomaticBackup);
            var backupPeriod = int.Parse(GetSettingValueFromCache(SettingKeys.BackupPeriod, CachableDataEnum.BackupAndRestore).ToString());

            if (isBackupAutomatic)
            {
                var lastBackupDate = DateTime.Parse(GetSettingValueFromCache(SettingKeys.LastBackupDate, CachableDataEnum.BackupAndRestore).ToString());

                ShowWaitingPanel("Backup Period Check ...");

                if (DateTime.Today.Date.Subtract(lastBackupDate.Date).Days > backupPeriod * 7)
                {
                    ShowWaitingPanel("Backup Path Check ...");

                    var backupPath = GetSettingValueFromCache(SettingKeys.DefaultBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();

                    if (string.IsNullOrEmpty(backupPath))
                    {
                        ShowInformation("Vital couldn't create backup automatically, the default backup path is empty, " +
                                        "please make sure a correct path is selected in backup and restore screen.","Automatic Backup Failed");
                    }
                    else if (!Directory.Exists(backupPath))
                    {
                        var secondaryBackupPath = GetSettingValueFromCache(SettingKeys.DefaultSecondaryBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();

                        if (string.IsNullOrEmpty(secondaryBackupPath))
                        {
                            ShowInformation("Vital couldn't create backup automatically, the secondary backup path is empty, " +
                                            "please make sure a correct path is selected in backup and restore screen.", "Automatic Backup Failed");
                        }
                        else if (!Directory.Exists(secondaryBackupPath))
                        {
                            ShowInformation("Vital couldn't create backup automatically, the secondary backup path doesn't exist or was changed, " +
                                            "please make sure a correct path is selected in backup and restore screen.", "Automatic Backup Failed");
                        }
                        else
                        {
                            ShowWaitingPanel("Creating Backup To Secondary Location ...");
                            Backup(secondaryBackupPath, false, SettingKeys.DefaultSecondaryBackupRestoreLocation);
                            SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupDate, DateTime.Now.Date.ToShortDateString(), settingsManager);
                        }
                    }
                    else
                    {
                        ShowWaitingPanel("Creating Backup ...");
                        Backup(backupPath, false, SettingKeys.DefaultBackupRestoreLocation);
                        SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupDate, DateTime.Now.Date.ToShortDateString(), settingsManager);

                        var autoRemoveOldBackups = (bool)GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutoRemoveOldBackups);

                        if (autoRemoveOldBackups)
                        {
                            ShowWaitingPanel("Old Backups Clean Up ...");

                            var backupsCount = int.Parse(GetSettingValueFromCache(SettingKeys.RecentBackupFilesCount, CachableDataEnum.BackupAndRestore).ToString());

                            var backupsDirectory = new DirectoryInfo(backupPath);
                            var vitalBackupFiles = backupsDirectory.GetFiles("*.bak");
                            var filesOrderd = vitalBackupFiles.OrderBy(bk=>File.GetCreationTime(bk.FullName));
                            var recentFiles = filesOrderd.Skip(Math.Max(0, filesOrderd.Count() - backupsCount)).Take(backupsCount).ToList();

                            foreach (var file in vitalBackupFiles)
                            {
                                if (File.Exists(file.FullName) && recentFiles.All(rb => rb.FullName != file.FullName))
                                {
                                    File.Delete(file.FullName);
                                }
                            }
                        }
                    }
                    
                }
            }

            HideSplash();
        }

        /// <summary>
        /// Checks default and secondary backup locations
        /// </summary>
        /// <param name="settingsManager"></param>
        /// <param name="backupSettingKey"></param>
        private static void CheckUpdateBackupLocation(SettingsManager settingsManager, SettingKeys backupSettingKey)
        {
            var backupPath = GetSettingValueFromCache(backupSettingKey, CachableDataEnum.BackupAndRestore).ToString();

            //If backup path is empty .. check if the path exists on disk
            if (string.IsNullOrEmpty(backupPath))
            {
                var commonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                var companyFolderPath = Path.Combine(commonApplicationData, CompanyFolder);
                var applicationFolderPath = Path.Combine(companyFolderPath, ApplicationFolder);

                //If path exists, then use it in settings
                if (!Directory.Exists(applicationFolderPath))
                {
                    //If path doesn't exist then create it
                    try
                    {
                        ShowWaitingPanel("Creating Backup Location ...");

                        bool userCancelledPermission;

                        var createCompanyDirectoryScript = VitalLogicScripts.CreateDirectoryScript.Replace(StaticKeys.FolderLocationPlaceHolder, companyFolderPath);

                        createCompanyDirectoryScript = createCompanyDirectoryScript.Replace(StaticKeys.ValuePlaceHolder, string.Empty);

                        ShowWaitingPanel(StaticKeys.PerformingVitalSystemOperation,true);

                        VitalLogicExecuter.ExecuteMethod(VitalLogicScripts.CreateDirectoryMethod,
                                                                        createCompanyDirectoryScript,
                                                                        VitalLogicScripts.CreateDirectoryAssemblies,
                                                                        VitalLogicScripts.CreateDirectoryAssembliesDLLs,
                                                                        out userCancelledPermission);

                        HideSplash();
                        var createApplicationDirectoryScript = VitalLogicScripts.CreateDirectoryScript.Replace(StaticKeys.FolderLocationPlaceHolder, applicationFolderPath);

                        createApplicationDirectoryScript = createApplicationDirectoryScript.Replace(StaticKeys.ValuePlaceHolder, "InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly,");

                        ShowWaitingPanel(StaticKeys.PerformingVitalSystemOperation, true);
                        VitalLogicExecuter.ExecuteMethod(VitalLogicScripts.CreateDirectoryMethod,
                                                                        createApplicationDirectoryScript,
                                                                        VitalLogicScripts.CreateDirectoryAssemblies,
                                                                        VitalLogicScripts.CreateDirectoryAssembliesDLLs,
                                                                        out userCancelledPermission);
                        HideSplash();
                    }
                    catch (Exception exception)
                    {
                        HideSplash();
                        ShowError(StaticKeys.DatabaseBackupErrorOccured, exception);
                    } 
                }

                SaveChange(CachableDataEnum.BackupAndRestore, backupSettingKey, applicationFolderPath, settingsManager);
            }
        }
        
        /// <summary>
        /// Check for backup default location
        /// </summary>
        /// <param name="settingsManager"></param>
        public static void CheckBackupRestoreLocation(SettingsManager settingsManager)
        {
            ShowWaitingPanel("Checking Backup Locations ...");
            
            var isBackupAutomatic = (bool)GetSettingCheckValue(CachableDataEnum.BackupAndRestore, SettingKeys.AutomaticBackup);
            
            //Only for automatic mode
            if (isBackupAutomatic)
            {
                CheckUpdateBackupLocation(settingsManager, SettingKeys.DefaultBackupRestoreLocation);
                CheckUpdateBackupLocation(settingsManager, SettingKeys.DefaultSecondaryBackupRestoreLocation);
            }

            HideSplash();
        }

        /// <summary>
        /// Create backup in the default path automatically without messages
        /// </summary>
        public static void BackupToDefaultPath()
        {
            var backupPath = GetSettingValueFromCache(SettingKeys.DefaultBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();

            if (backupPath != string.Empty && Directory.Exists(backupPath))
            {
                Backup(backupPath, false, SettingKeys.DefaultBackupRestoreLocation);
            }
            else
            {
                var secondaryBackupPath = GetSettingValueFromCache(SettingKeys.DefaultSecondaryBackupRestoreLocation, CachableDataEnum.BackupAndRestore).ToString();

                if (secondaryBackupPath != string.Empty && Directory.Exists(secondaryBackupPath))
                {
                    Backup(secondaryBackupPath, false, SettingKeys.DefaultSecondaryBackupRestoreLocation);
                }
            }   
        }

        /// <summary>
        /// Check if path is writable
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Performs the backup logic
        /// </summary>
        public static ProcessResult Backup(string backupPath, bool showDoneMessage, SettingKeys backupSettingKey)
        {
            if (backupPath != string.Empty)
            {
                if (Directory.Exists(backupPath))
                {
                    if (HasWriteAccessToFolder(backupPath))
                    {
                        ShowWaitingPanel(
                            backupSettingKey == SettingKeys.DefaultBackupRestoreLocation
                                ? StaticKeys.DatabaseBackup
                                : StaticKeys.DatabaseBackupSecondary, true);
                        try
                        {
                            var sqlConfigManager = new SqlConfigManager();
                            var settingsManager = new SettingsManager();

                            var appInfoManager = new AppInfoManager();
                            var branch = appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.AppBranch);
                            var vitalCurrentVersion = appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.Version);
                            var dbVersion = appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.DBVersion);

                            var result = sqlConfigManager.Backup(backupPath, branch, vitalCurrentVersion, dbVersion);

                            if (result.IsSucceed)
                            {
                                SaveChange(CachableDataEnum.BackupAndRestore, backupSettingKey, backupPath, settingsManager);
                                SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupDate,
                                    DateTime.Today.Date.ToShortDateString(), settingsManager);
                                SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupNotificationDate,
                                    DateTime.Today.Date.ToShortDateString(), settingsManager);

                                var backupsCount =
                                    int.Parse(
                                        GetSettingValueFromCache(SettingKeys.OffSystemBackupCounter,
                                            CachableDataEnum.BackupAndRestore).ToString());

                                if (backupsCount == 10)
                                {
                                    SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.OffSystemBackupCounter, 0,
                                        settingsManager);
                                    ShowInformation(
                                        "Please make sure to copy at least one latest backup file off your system or in any cloud services that you use to keep your data safe.",
                                        "Off System Backup Reminder");
                                }
                                else
                                {
                                    backupsCount += 1;
                                    SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.OffSystemBackupCounter,
                                        backupsCount, settingsManager);
                                }

                                HideSplash();

                                if (showDoneMessage)
                                {
                                    ShowInformation(StaticKeys.DatabaseBackupSuccessful,
                                        StaticKeys.DatabaseBackupMessageTitle);
                                }
                                return ProcessResult.Succeed;
                            }

                            HideSplash();
                            ShowError(StaticKeys.DatabaseBackupErrorOccured, result.Message);
                        }
                        catch (VitalDatabaseException exception)
                        {
                            HideSplash();
                            ShowError(StaticKeys.DatabaseBackupErrorOccured, exception);
                        }
                    }
                    else
                    {
                        ShowInformation(backupSettingKey == SettingKeys.DefaultBackupRestoreLocation ?
                                   StaticKeys.DatabaseBackupDestinationNotWritable :
                                   StaticKeys.DatabaseBackupSecondaryDestinationNotWritable, StaticKeys.DatabaseBackupMessageTitle);
                    }
                }
                else
                {
                    ShowInformation(backupSettingKey == SettingKeys.DefaultBackupRestoreLocation ? 
                                    StaticKeys.DatabaseBackupDestinationDoesNotExist:
                                    StaticKeys.DatabaseBackupSecondaryDestinationDoesNotExist, StaticKeys.DatabaseBackupMessageTitle);
                }
            }
            else
            {
                ShowInformation(backupSettingKey == SettingKeys.DefaultBackupRestoreLocation ?
                                    StaticKeys.DatabaseBackupIsNotSpecified :
                                    StaticKeys.DatabaseSecondaryBackupIsNotSpecified, StaticKeys.DatabaseBackupMessageTitle);
            }

            return ProcessResult.Failed;
        }

        /// <summary>
        /// Performs the restore logic
        /// </summary>
        public static ProcessResult Restore(string restorePath)
        {
            if (restorePath != string.Empty)
            {
                if (File.Exists(restorePath))
                {
                    if (Path.GetExtension(restorePath) == ".bak")
                    {
                        ShowWaitingPanel(StaticKeys.DatabaseVerifyingBackup, true);
                        try
                        {
                            var sqlConfigManager = new SqlConfigManager();
                            var updateManager = new ApplicationUpdateManager();
                            var settingsManager = new SettingsManager();

                            var strDBInfo = string.Empty;

                            var databaseVerificationObject = sqlConfigManager.VerifyBackup(restorePath, ref strDBInfo);

                            if (!databaseVerificationObject.IsApplicationBranchCompatible)
                            {
                                HideSplash();
                                ShowError(StaticKeys.ApplicationBranchCompatabilityError, "");
                            }
                            else
                            {
                                var appCurrentVersion = updateManager.CurrentPublishVersion;
                                var vitalStoredVersion = new Version(databaseVerificationObject.Version);

                                HideSplash();

                                if (vitalStoredVersion == appCurrentVersion || vitalStoredVersion < appCurrentVersion)
                                {
                                    var confirmMessage =
                                        "Are you sure you want to restore the DB file selected with the following details?:\n\n" +
                                        "File Name                         : " + Path.GetFileName(restorePath) + "\n" +
                                        "Database Size                  : " + ToFileSize((new FileInfo(restorePath).Length)) + "\n" +
                                        "Backup File Creation Date: " + File.GetCreationTime(restorePath).ToShortDateString() + "\n\n" + strDBInfo;

                                    if (ShowConfirmQuestion(confirmMessage) == DialogResult.Yes)
                                    {
                                        ShowWaitingPanel(StaticKeys.DatabaseRestoringDatabase, true);
                                        try
                                        {
                                            BackupTechInfo();
                                            
                                            sqlConfigManager.Restore(restorePath);

                                            if (vitalStoredVersion < appCurrentVersion)
                                            {
                                                VitalInstanceController.UpgradeDatabase();
                                            }
                                            SetTechInfo();

                                            UibllInteraction.Instance.RefreshPatients();

                                            SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.DefaultBackupRestoreLocation, new FileInfo(restorePath).DirectoryName, settingsManager);
                                            SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupDate, DateTime.Today.Date.ToShortDateString(), settingsManager);
                                            SaveChange(CachableDataEnum.BackupAndRestore, SettingKeys.LastBackupNotificationDate, DateTime.Today.Date.ToShortDateString(), settingsManager);

                                            HideSplash();
                                            ShowInformation(StaticKeys.DatabaseRestoreSuccessful, StaticKeys.DatabaseRestoreMessageTitle);

                                            return ProcessResult.Succeed;
                                        }
                                        catch (VitalDatabaseException exception)
                                        {
                                            HideSplash();
                                            ShowError(StaticKeys.DatabaseBackupErrorOccured, exception);
                                        }
                                    }
                                }
                                else if (vitalStoredVersion > appCurrentVersion)
                                {
                                    ShowInformation(StaticKeys.DatabaseCodeIsOld, StaticKeys.DatabaseRestoreMessageTitle);
                                }
                                else
                                {
                                    ShowInformation(StaticKeys.DatabaseInvalidVersion, StaticKeys.DatabaseRestoreMessageTitle);
                                }
                            }
                        }
                        catch (VitalDatabaseException exception)
                        {
                            HideSplash();
                            ShowError(StaticKeys.DatabaseBackupErrorOccured, exception);
                        }
                    }
                    else
                    {
                        ShowInformation(StaticKeys.DatabaseInvalidExtension, StaticKeys.DatabaseRestoreMessageTitle);
                    }
                }
                else
                {
                    ShowInformation(StaticKeys.DatabaseFileDoesNotExist, StaticKeys.DatabaseRestoreMessageTitle);
                }
            }
            else
            {
                ShowInformation(StaticKeys.DatabaseRestoreIsNotSpecified, StaticKeys.DatabaseRestoreMessageTitle);
            }

            return ProcessResult.Failed;
        }

        /// <summary>
        /// Returns if a path is invalid for secondary backup in case of USB flash or USB HD Drive or Network Drive
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsInvalidBackupPath(string path)
        {
            return GetPathDriveType(path) != DriveType.Fixed || IsExternalDisk(Path.GetPathRoot(path));
        }

        /// <summary>
        /// Gets the drive type of the given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>DriveType of path</returns>
        public static DriveType GetPathDriveType(string path)
        {
            //OK, so UNC paths aren't 'drives', but this is still handy
            if(path.StartsWith(@"\\")) return DriveType.Network;  
            var info =  DriveInfo.GetDrives().FirstOrDefault(i => path.StartsWith(i.Name, StringComparison.OrdinalIgnoreCase));
            return info == null ? DriveType.Unknown : info.DriveType;
        }

        /// <summary>
        /// Gets if a drive belongs to an external disk
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns></returns>
        public static bool IsExternalDisk(string driveLetter)
        {
            var retVal = false;

            try
            {
                driveLetter = driveLetter.TrimEnd('\\');

                // browse all USB WMI physical disks
                foreach (ManagementObject drive in new ManagementObjectSearcher("select DeviceID, MediaType,InterfaceType from Win32_DiskDrive").Get())
                {
                    // associate physical disks with partitions
                    var partitionCollection = new ManagementObjectSearcher(String.Format("associators of {{Win32_DiskDrive.DeviceID='{0}'}} " + "where AssocClass = Win32_DiskDriveToDiskPartition", drive["DeviceID"])).Get();

                    foreach (ManagementObject partition in partitionCollection)
                    {
                        if (partition != null)
                        {
                            // associate partitions with logical disks (drive letter volumes)
                            var logicalCollection = new ManagementObjectSearcher(String.Format("associators of {{Win32_DiskPartition.DeviceID='{0}'}} " + "where AssocClass= Win32_LogicalDiskToPartition", partition["DeviceID"])).Get();

                            foreach (ManagementObject logical in logicalCollection)
                            {
                                if (logical != null)
                                {
                                    // finally find the logical disk entry
                                    var volumeEnumerator = new ManagementObjectSearcher(String.Format("select DeviceID from Win32_LogicalDisk " + "where Name='{0}'", logical["Name"])).Get().GetEnumerator();

                                    volumeEnumerator.MoveNext();

                                    var volume = (ManagementObject)volumeEnumerator.Current;

                                    if (driveLetter.ToLowerInvariant().Equals(volume["DeviceID"].ToString().ToLowerInvariant()) &&
                                        (drive["MediaType"].ToString().ToLowerInvariant().Contains("external") || drive["InterfaceType"].ToString().ToLowerInvariant().Contains("usb")))
                                    {
                                        retVal = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
            
            return retVal;
        }

        /// <summary>
        /// Backup the tech info and Vital key info in memory
        /// </summary>
        public static void BackupTechInfo()
        {
            InitAppInfoManager();

            AppBranch = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.AppBranch);
            TechnicianName = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianName);
            TechnicianClinicName = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianClinicName);
            TechnicianClinicWebsite = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianClinicWebsite);
            TechnicianAddress = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianAddress);
            TechnicianCity = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianCity);
            TechnicianState = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianState);
            TechnicianZip = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianZip);
            TechnicianPhone = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianPhone);
            TechnicianEmail = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.TechnicianEmail);
            VitalKey = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.VitalKey);
            VitalKeyDate = _appInfoManager.GetAppInfoValueByProperty(AppInfoKeys.VitalKeyDate);

            //Set temporary values instead of empty ones to be used for logging purposes
            TechnicianName = !string.IsNullOrEmpty(TechnicianName) ? TechnicianName : "Sample Tech Name";
            TechnicianClinicName = !string.IsNullOrEmpty(TechnicianClinicName) ? TechnicianClinicName : "Sample Clinic";
            TechnicianClinicWebsite = !string.IsNullOrEmpty(TechnicianClinicWebsite) ? TechnicianClinicWebsite : "Sample Website";
            TechnicianAddress = !string.IsNullOrEmpty(TechnicianAddress) ? TechnicianAddress : "Sample Address";
            TechnicianCity = !string.IsNullOrEmpty(TechnicianCity) ? TechnicianCity : "Sample City";
            TechnicianState = !string.IsNullOrEmpty(TechnicianState) ? TechnicianState : "Sample State";
            TechnicianZip = !string.IsNullOrEmpty(TechnicianZip) ? TechnicianZip : "Sample Zip";
            TechnicianPhone = !string.IsNullOrEmpty(TechnicianPhone) ? TechnicianPhone : "Sample Phone";
            TechnicianEmail = !string.IsNullOrEmpty(TechnicianEmail) ? TechnicianEmail : "test@vital.com";
            VitalKey = !string.IsNullOrEmpty(VitalKey) ? VitalKey : "Sample Key";
            VitalKeyDate = !string.IsNullOrEmpty(VitalKeyDate) ? VitalKeyDate : "Sample Date";
        }

        /// <summary>
        /// Sets the tech info and Vital key info into the database
        /// </summary>
        public static void SetTechInfo()
        {
            if (_appInfoManager == null) _appInfoManager = new AppInfoManager();

            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianName, TechnicianName);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianClinicName, TechnicianClinicName);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianClinicWebsite, TechnicianClinicWebsite);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianAddress, TechnicianAddress);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianCity, TechnicianCity);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianState, TechnicianState);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianZip, TechnicianZip);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianPhone, TechnicianPhone);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianEmail, TechnicianEmail);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.VitalKey, VitalKey);
            _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.VitalKeyDate, VitalKeyDate);
        }

        #endregion

        #region Vital Mail

        /// <summary>
        /// Gets the cached Vital Email value from database
        /// </summary>
        /// <returns></returns>
        public static VitalEmail GetVitalEmail()
        {
            return (VitalEmail)CacheHelper.SetOrGetCachableData(CachableDataEnum.VitalEmail);
        }

        #endregion

        #region Encryption and Decryption

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText)
        {
            var EncryptionKey = StaticKeys.EncryptionKey;

            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (VitalLogicalException exception)
            {
                ShowError(StaticKeys.EncryptionError, exception);
            }

            
            return clearText;
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            var EncryptionKey = StaticKeys.EncryptionKey;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (VitalLogicalException exception)
            {
                ShowError(StaticKeys.DecryptionError, exception);
            }

            return cipherText;
        }

        #endregion
    }
}
