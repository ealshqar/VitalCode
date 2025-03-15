using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.Reports;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormMailClient : DevExpress.XtraEditors.XtraForm
    {
        #region Constructors
        
        /// <summary>
        /// The Constructor. 
        /// </summary>
        public XtraFormMailClient()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs some initialization steps.
        /// </summary>
        private void PerformInitializationSteps()
        {
            xtraUserControlVitalRichEditMail.ReadOnly = false;
        }

        private bool ValidateForm()
        {
            return textEditName.Text.Trim() != string.Empty;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Handles the click on the send button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void barButtonItemSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidateForm())
            {
                UiHelperClass.ShowError(StaticKeys.MailClientValidationMessageTitle,StaticKeys.MailClientNameValidationMessage);
                return;
            }

            UiHelperClass.ShowWaitingPanel(StaticKeys.MailClientSendingMail);

            var feedbackEmail = UiHelperClass.GetVitalEmail().FeedbackTargetEmail;

            var result = MailHelper.SendMail(feedbackEmail,
                                            textEditName.Text,
                                            textEditTechnicianEmail.Text,
                                            textEditTechnicianPhone.Text,
                                            textEditSubject.Text,
                                            xtraUserControlVitalRichEditMail.Control);

            UiHelperClass.HideSplash();

            if (result.IsSucceed)
            {
                UiHelperClass.ShowInformation(StaticKeys.MailClientMessageSent);
                Close();
            }
            else
                UiHelperClass.ShowError(StaticKeys.ErrorMessageTitle,result.Message);
        }

        #endregion

        /// <summary>
        /// Handles form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormMailClient_Load(object sender, System.EventArgs e)
        {
            PerformInitializationSteps();
            textEditName.Text = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianName);
            textEditTechnicianPhone.Text = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianPhone);
            textEditTechnicianEmail.Text = UiHelperClass.GetTechnicianInfo(SettingKeys.TechnicianEmail);
        }
    }
}
