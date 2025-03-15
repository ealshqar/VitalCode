using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using DevExpress.XtraTab;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.User_Controls.Entity_Tabs;

namespace Vital.UI
{
    public class UibllInteraction
    {
        #region Singleton Code

        private static UibllInteraction _instance;

        /// <summary>
        /// Returns the instance of the UibllInteraction.
        /// </summary>
        public static UibllInteraction Instance
        {
            get { return _instance ?? (_instance = new UibllInteraction()); }
        }

        #endregion    

        #region Custom Font DLL
        
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        #endregion

        #region Properties

        #region UI Related

        /// <summary>
        /// Gets or sets the Main Form.
        /// </summary>
        public ribbonFrmMain MainForm
        {
            get;
            set;
        }

        /// <summary>
        /// A pointer to the XtraTab control in the main form
        /// </summary>
        public XtraTabControl FormTabControl
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if custom fonts were initialized
        /// </summary>
        private bool _customFontsInitialized;

        /// <summary>
        /// Collection of custom fonts used in Vital
        /// </summary>
        private PrivateFontCollection _customFontsCollection;

        #endregion
        
        #endregion

        #region Methods

        #region Checking

        /// <summary>
        /// Check if a tab is opened or not, and if it is opened, only activate the tab and don't create a new one
        /// </summary>
        ///<param name="activateTab">flag to indicate if tab should be activated or not</param>
        /// <param name="openedObject">The entity to open the tab for</param>
        /// <param name="tabType">The type of the tab to open</param>
        /// <returns></returns>       
        public bool CheckIfTabOpened(DomainEntity openedObject, bool activateTab, TabTypes tabType)
        {            
            var tabsObjectDictionary = new Dictionary<DomainEntity, XtraTabPage>();
            var tabsIdDictionary = new Dictionary<int, XtraTabPage>();

            foreach (XtraTabPage tab in FormTabControl.TabPages)
            {
                try
                {
                    var inspectedEntity = tab.Controls[0] as XtraTabPageEntity;
                    if (inspectedEntity != null)
                    {
                        if (inspectedEntity.TabObject != null)
                        {
                            tabsObjectDictionary.Add(inspectedEntity.TabObject, tab);
                            if (openedObject != null)
                            {
                                if (inspectedEntity.TabObject.GetType() == openedObject.GetType())
                                {
                                    tabsIdDictionary.Add(Int32.Parse(inspectedEntity.TabObject.Id.ToString()), tab);
                                }
                            }
                        }
                    }
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

            XtraTabPage pageToActivate = null;

            if (openedObject != null)
            {
                tabsObjectDictionary.TryGetValue(openedObject, out pageToActivate);
            }

            if (pageToActivate == null)
            {
                if (openedObject != null)
                {
                    tabsIdDictionary.TryGetValue(Int32.Parse(openedObject.Id.ToString()), out pageToActivate);
                }
            }

            if (activateTab && pageToActivate != null)
            {
                ActivateTab(pageToActivate);
            }

            return (pageToActivate != null);
        }

        /// <summary>
        /// Check if the new tab for the passed tabType is opened.
        /// </summary>
        public bool CheckIfNewTabOpened(bool activateTab, TabTypes tabType)
        {
            foreach (XtraTabPage tab in FormTabControl.TabPages)
            {
                var inspectedEntity = tab.Controls[0] as XtraTabPageEntity;

                if (inspectedEntity != null)
                {
                    if (inspectedEntity.TabObject != null)
                    {
                        if(inspectedEntity.TabType == tabType && inspectedEntity.TabObject.Id == 0)
                        {
                            if(activateTab)
                            {
                                ActivateTab(tab);
                                return true;
                            }
                        }
                    }

                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the current tab is focused or not.
        /// </summary>
        /// <param name="tab">The tab to check</param>
        /// <returns>True if the tab is selected</returns>
        public bool IsThisTabSelected(XtraTabPage tab)
        {
            return (FormTabControl.SelectedTabPage == tab);
        }
        
        #endregion
        
        #region UI Changes
        
        #region Ribbon Related

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsSaveButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemSave.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsPreliminaryTestButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemStartPreliminary.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsAutoTestButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemAutoTest.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsVFSButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemVFS.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsSpotCheckButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemStartSpotCheck.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsFrequencyTestButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemStartFrequencyTest.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsSaveAndCloseButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemSaveClose.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsDeleteButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemDelete.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled.</param>
        public void IsCancelButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemCancel.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsEditButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemEdit.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception);
            }
        }

        /// <summary>
        /// Enables or disabled the button
        /// </summary>
        /// <param name="isEnabled">The is enabled boolean.</param>
        public void IsLockButtonEnabled(bool isEnabled)
        {
            try
            {
                MainForm.barButtonItemLock.Enabled = isEnabled;
            }
            catch (NullReferenceException exception)
            {
                UiHelperClass.ShowError(string.Empty, exception); 
            }
        }        

        #endregion

        #region Tab Related

        /// <summary>
        /// Activate the specified tab in the tab control
        /// </summary>
        /// <param name="tab"> The tab to be activated</param>
        /// <returns></returns>       
        private void ActivateTab(XtraTabPage tab)
        {
            if (tab != null)
            {
                try
                {
                    FormTabControl.SelectedTabPage = tab;
                }
                catch (NullReferenceException exception)
                {
                    UiHelperClass.ShowError(string.Empty, exception); 
                }
            }
        }

        /// <summary>
        /// Close the specified tab page
        /// </summary>
        /// <param name="tab"> The tab to be closed</param>
        /// <returns></returns>       
        public void CloseTabPage(XtraTabPage tab)
        {
            if (tab == null)
            {
            }
            else
            {
                try
                {
                    if (FormTabControl.TabPages.Count != 0 && FormTabControl.TabPages.Contains(tab))
                    {
                        var currentEntityTab = tab.Controls[0] as XtraTabPageEntity;
                        if (currentEntityTab != null)
                        {
                            currentEntityTab.PerformBeforeCloseActions();
                            currentEntityTab.ClearHandlers();
                        }                        
                        FormTabControl.TabPages.Remove(tab);
                        FormTabControl.SelectedTabPageIndex = FormTabControl.TabPages.Count - 1;
                    }
                }
                catch (NullReferenceException exception)
                {
                    UiHelperClass.ShowError(string.Empty, exception);
                }
            }
        }


        #endregion        
     
        #endregion

        #region Actions Select

        #region Tab Actions

        /// <summary>
        /// Opens a tab page and load it with the passed business object
        /// </summary>
        /// <param name="openedObject">The business object to be opened</param>
        public XtraTabPageEntity Open(ref DomainEntity openedObject)
        {
            //Check if the tab was opened before, and activate it in that case
            if (openedObject != null)
            {                
                try
                {
                    if (!CheckIfTabOpened(openedObject, true, 0))
                    {
                        var openedTab = new XtraTabPagePatient { TabType = TabTypes.Patient };
                        openedTab.Open(openedObject, 0);
                        return openedTab;
                    }
                }
                catch (NullReferenceException nullReferenceException)
                {
                    throw new VitalBaseException(nullReferenceException.InnerException.Message);
                }

                return new XtraTabPageEntity();
            }

            UiHelperClass.ShowError(string.Empty , new Exception("Record is corrupted"));

            return null;
        }

        /// <summary>
        /// Create a new tab for a specified object type
        /// </summary>
        /// <param name="parentObject">The parent object to the new tab</param>
        public XtraTabPageEntity New(DomainEntity parentObject)
        {
            try
            {
                if(CheckIfNewTabOpened(true, TabTypes.Patient))
                    return new XtraTabPageEntity();

                var newTab = new XtraTabPagePatient {TabType = TabTypes.Patient};
                newTab.New(parentObject);
                return newTab;
            }
            catch (NullReferenceException nullReferenceException)
            {
                throw new VitalBaseException(nullReferenceException.InnerException.Message);
            }
        }     

        #endregion
       
        #region Helper Methods

        /// <summary>
        /// Refresh all patients.
        /// </summary>
        public void RefreshPatients()
        {
            MainForm.GetPatients();
        }

        /// <summary>
        /// Update Shipping Orders count
        /// </summary>
        public void UpdateShippingOrdersCount()
        {
            MainForm.UpdateShippingOrdersCount();
        }

        #endregion

        #endregion

        #region Custom Fonts

        /// <summary>
        /// Initializes custom fonts used in Vital
        /// </summary>
        public void InitializeCustomFonts()
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

            if (!_customFontsInitialized)
            {
                _customFontsCollection = new PrivateFontCollection();

                //Add the custom fonts used to the collection
                AddCustomFont(Properties.Resources.OpenSansSemibold);
                AddCustomFont(Properties.Resources.OpenSansExtrabold);
                AddCustomFont(Properties.Resources.OpenSansLight);

                _customFontsInitialized = true;
            }
        }

        /// <summary>
        /// Initializes and adds custom font to collection
        /// </summary>
        /// <param name="fontData"></param>
        private void AddCustomFont(byte[] fontData)
        {
            uint dummy = 0;
            var fontPtr = Marshal.AllocCoTaskMem(fontData.Length);

            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

            _customFontsCollection.AddMemoryFont(fontPtr, fontData.Length);

            AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref dummy);

            Marshal.FreeCoTaskMem(fontPtr);
        }

        /// <summary>
        /// Gets font from list based on enum
        /// </summary>
        /// <param name="customFont"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Font GetCustomFont(CustomFonts customFont, float size)
        {
            //Here we extract the font family by name
            var fontFamily = _customFontsCollection.Families.FirstOrDefault(f => f.Name == EnumNameResolver.GetEnumNameOrDescription(customFont));
            
            //if the family wasn't found for any reason then we use Arial font to avoid issues.
            return fontFamily == null? new Font("Arial", size) : new Font(fontFamily, size);
        }

        #endregion

        #endregion
    }
}