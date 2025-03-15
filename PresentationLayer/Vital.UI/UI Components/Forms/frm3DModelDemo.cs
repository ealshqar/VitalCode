using System.Drawing;
using System.Windows.Forms;
using Vital.UI.UI_Components.User_Controls.BioDigital3DModel;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frm3DModelDemo : Form
    {
        public frm3DModelDemo(XtraUserControlBioDigital3DModel model, Image placeholderImage)
        {
            InitializeComponent();
            xtraUserControlBioDigital3DModelViewer.PlaceholderImage = placeholderImage;
            xtraUserControlBioDigital3DModelViewer.Open(model);
        }

        private void frm3DModelDemo_FormClosed(object sender, FormClosedEventArgs e)
        {
            // IMPORTANT: Dispose the model control before close the from (or the viewer) to avoid conflicts when add it to another viewer.
            xtraUserControlBioDigital3DModelViewer.DisposeControl();
        }

        private void buttonSelectLiver_Click(object sender, System.EventArgs e)
        {
            xtraUserControlBioDigital3DModelViewer.SelectObject("digestive_system-liver_ID");
        }

        private void buttonResetView_Click(object sender, System.EventArgs e)
        {
            xtraUserControlBioDigital3DModelViewer.ResetView();
        }

        private void buttonSetDarkBg_Click(object sender, System.EventArgs e)
        {
            xtraUserControlBioDigital3DModelViewer.SetBgColor("#192734", "#192734");
        }

        private void buttonIsReady_Click(object sender, System.EventArgs e)
        {
            if (xtraUserControlBioDigital3DModelViewer.Ready)
            {
                MessageBox.Show(this, "Model is ready.", "Is Ready?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "Model is not ready yet.", "Is Ready?", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }

        private void buttonEnableXRayMode_Click(object sender, System.EventArgs e)
        {
            xtraUserControlBioDigital3DModelViewer.SetXRayMode(true);
        }

        private void buttonDisableXRayMode_Click(object sender, System.EventArgs e)
        {
            xtraUserControlBioDigital3DModelViewer.SetXRayMode(false);
        }
        
    }
}
