using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Vital.Business.Managers;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormTechInfo : XtraForm
    {
        private readonly AppInfoManager _appInfoManager;
        private bool _infoSaved;
        public bool ExitVital;

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormTechInfo()
        {
            InitializeComponent();
            _appInfoManager = new AppInfoManager();
            ExitVital = true;
        }

        #endregion

        /// <summary>
        /// Performs the done logic
        /// </summary>
        private void DoneLogic()
        {
            if (IsValid())
            {
                if (UiHelperClass.ShowConfirmQuestion("The information entered cannot be edited later, are you sure it is correct?") == DialogResult.Yes)
                {
                    UiHelperClass.ShowWaitingPanel("Saving ...");

                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianName, textEditTechName.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianClinicName,textEditClinicName.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianClinicWebsite,textEditClinicWebsite.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianAddress,textEditAddress.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianCity,textEditCity.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianState,textEditState.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianZip,textEditZip.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianPhone,textEditPhone.Text);
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.TechnicianEmail,textEditEmail.Text);
                                                         
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.VitalKey,Guid.NewGuid().ToString());
                    _appInfoManager.SetAppInfoValueByKey(AppInfoKeys.VitalKeyDate,DateTime.Now.ToString(CultureInfo.InvariantCulture));

                    _infoSaved = true;
                    ExitVital = false;

                    UiHelperClass.HideSplash();

                    Close();
                }
            }
        }

        /// <summary>
        /// Returns if fields are valid
        /// </summary>
        /// <returns></returns>
        private bool IsValid(bool showError = true)
        {
            var isValid =  SetError(textEditTechName) &
                           SetError(textEditClinicName) &
                           SetError(textEditPhone) &
                           SetError(textEditEmail);

            if (!isValid && showError)
            {
                UiHelperClass.ShowError(StaticKeys.ValidationMessageTitle, StaticKeys.ValidationMessageGeneral);
            }

            return isValid;
        }

        /// <summary>
        /// Checks if field is valid and sets error
        /// </summary>
        /// <param name="textEdit"></param>
        /// <returns></returns>
        private bool SetError(TextEdit textEdit)
        {
            var isValid = !string.IsNullOrEmpty(textEdit.Text);
            dxErrorProviderMain.SetError(textEdit, isValid ? string.Empty : "This field is required.", isValid ? ErrorType.None : ErrorType.Critical);
            return isValid;
        }

        private bool ConfirmVitalExit()
        {
            var dialogResult = UiHelperClass.ShowConfirmQuestion(StaticKeys.VitalExitConfirmation);

            return dialogResult == DialogResult.Yes;
        }

        #region Handlers

        /// <summary>
        /// Handle form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormTechInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_infoSaved)
                e.Cancel = !ConfirmVitalExit();
        }

        /// <summary>
        /// Handle done button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDone_Click(object sender, EventArgs e)
        {
            DoneLogic();
        }

        /// <summary>
        /// Handle edit value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEdit_EditValueChanged(object sender, EventArgs e)
        {
            IsValid(false);
        }

        /// <summary>
        ///  Handle exit vital button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonExitVital_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}