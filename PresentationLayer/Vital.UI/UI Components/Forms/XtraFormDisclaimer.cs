using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Vital.Business.Managers;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormDisclaimer : XtraForm
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public XtraFormDisclaimer()
        {
            var settingsManager = new SettingsManager();
            var skinNameSetting = settingsManager.GetSetting(new SettingsFilter { Key = EnumNameResolver.Resolve(SettingKeys.SkinName) });
            var skinName = skinNameSetting != null ? skinNameSetting.Value.ToString() : "Office 2010 Blue";

            InitializeComponent();

            defaultLookAndFeelDisclaimer.LookAndFeel.SkinName = skinName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do the accept actions.
        /// </summary>
        private void AcceptAction()
        {
            Hide();
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Event Handlers 
        
        /// <summary>
        /// The logic of the click on this button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonAcceptDisclaimer_Click(object sender, EventArgs e)
        {
            AcceptAction();
        }

        /// <summary>
        /// Handel the Key down event for Shortcuts.
        /// </summary>
        private void XtraFormDisclaimer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    AcceptAction();
                    break;
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
            }
        }

        #endregion
    }
}