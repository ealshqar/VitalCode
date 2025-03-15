using System;
using DevExpress.XtraEditors;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormRestartRequired : XtraForm
    {
        #region Constructors

        public XtraFormRestartRequired()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handles
        
        /// <summary>
        /// Handle the Restart button clicked.
        /// </summary>
        private void simpleButtonRestart_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonRestart_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                System.Diagnostics.Process.Start("ShutDown", "-r -t 0");
                Close();
            }
        }

        /// <summary>
        /// Handle the Close button clicked.
        /// </summary>
        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                try
                {
                    if (IsDisposed) return;
                    Invoke(new EventHandler(simpleButtonClose_Click), sender, e);
                }
                catch
                {
                    // Nothing to do, form had been disposed.
                }
            }
            else
            {
                Close();
            }
        }

        #endregion

    }
}
