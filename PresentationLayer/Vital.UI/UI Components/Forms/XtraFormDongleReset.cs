using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vital.Business.Managers;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormDongleReset : DevExpress.XtraEditors.XtraForm
    {
        #region Private Variables

        private readonly SecurityManager _securityManager;
        private const string ReplaceString = "@ResetCode@";
        private readonly bool _useDongleExpiredMessage;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public XtraFormDongleReset(bool useDongleExpiredMessage)
        {
            InitializeComponent();
            _securityManager = new SecurityManager();
            _useDongleExpiredMessage = useDongleExpiredMessage;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Get the request code
        /// </summary>
        /// <returns></returns>
        private void GetRequestCode()
        {
            try
            {
                var dongleCodes = _securityManager.GetDongleRequestCode();
                memoEditInfo.Text = memoEditInfo.Text.Replace(ReplaceString, dongleCodes); 
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(StaticKeys.DongleResetError, exception);
                Close();
            }
        }

        /// <summary>
        /// Reset Dongle logic
        /// </summary>
        private void ResetDongle()
        {
            try
            {
                var resetResult = _securityManager.UpdateDongle(int.Parse(textEditCode1.Text),
                                                                int.Parse(textEditCode2.Text),
                                                                int.Parse(textEditCode3.Text),
                                                                int.Parse(textEditCode4.Text));

                if (resetResult.IsSucceed)
                {
                    var dongleState = _securityManager.CheckDongle();

                    if (dongleState == DongleState.LeaseExpired)
                    {
                        resetResult.IsSucceed = false;
                        resetResult.Message = StaticKeys.ErrorDuringDongleReset;
                    }
                }

                if (resetResult.IsSucceed)
                {
                    UiHelperClass.ShowInformation(StaticKeys.DongleResetSucceeded, StaticKeys.DongleResetTitle);

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    UiHelperClass.ShowError(StaticKeys.DongleResetError, resetResult.Message);
                }
            }
            catch (Exception exception)
            {
                UiHelperClass.ShowError(StaticKeys.DongleResetError, exception);
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// On load event handler.
        /// </summary>
        private void XtraFormAbout_Load(object sender, EventArgs e)
        {
            var expiryDate = SecurityManager.ReadExpirationDate().ToShortDateString();
            simpleLabelItemDongleExpiredInfo.Text =  
                (_useDongleExpiredMessage? 
                    StaticKeys.DongleExpiredMessage: 
                    StaticKeys.DongleWillExpireMessage).Replace(StaticKeys.DongleExpiryDateString, expiryDate);
            
            GetRequestCode();
        }

        /// <summary>
        /// Handle code fields text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditCode_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            simpleButtonResetDongle.Enabled = textEditCode1.Text != string.Empty &&
                                              textEditCode2.Text != string.Empty &&
                                              textEditCode3.Text != string.Empty &&
                                              textEditCode4.Text != string.Empty;
        }

        /// <summary>
        /// Handle Reset logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonResetDongle_Click(object sender, EventArgs e)
        {
            ResetDongle();
        }

        /// <summary>
        /// Handle Keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFormDongleReset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && simpleButtonResetDongle.Enabled)
            {
                ResetDongle();
            }
        }

        #endregion
    }
}