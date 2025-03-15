using System;
using System.IO;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormPHR : DevExpress.XtraEditors.XtraForm
    {
        public XtraFormPHR()
        {
            InitializeComponent();
        }

        private void XtraFormPHR_Load(object sender, EventArgs e)
        {
            try
            {
                var curDir = Directory.GetCurrentDirectory();
                webBrowserPHR.Url = new Uri(String.Format("file:///{0}/PhysiciansHRAd/index.html", curDir));
            }
            catch (Exception exception)
            {
            }
        }
    }
}