using System;
using System.Configuration;
using System.Windows.Forms;
using Vital.Business.Shared.Shared;
using Vital.UI.UI_Components.User_Controls.BioDigital3DModel;

namespace Vital.UI.UI_Components.Forms
{
    public partial class frm3DModelDemoMain : Form
    {
        private XtraUserControlBioDigital3DModel _bioDigitalMale3DModel;
        private XtraUserControlBioDigital3DModel _bioDigitalFemale3DModel;

        public frm3DModelDemoMain()
        {
            InitializeComponent();
        }

        private void simpleButtonOpenMaleModelForm_Click(object sender, EventArgs e)
        {
            new frm3DModelDemo(_bioDigitalMale3DModel, Properties.Resources.Male3dModelViewerPlaceholder).ShowDialog();
        }

        private void simpleButtonOpenFemaleModelForm_Click(object sender, EventArgs e)
        {
            new frm3DModelDemo(_bioDigitalFemale3DModel, Properties.Resources.Female3dModelViewerPlaceholder).ShowDialog();
        }

        private void frm3DModelDemoMain_Load(object sender, EventArgs e)
        {
            // Construct the model and add it to the form's controls so it start initializing it self in the background.
            _bioDigitalMale3DModel = new XtraUserControlBioDigital3DModel(
                ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyMaleModelIdConfigKey],
                ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyMaleModelBaseObjIdConfigKey]) { InitializeInBackground = true, XRayModeEnabled = true};
            Controls.Add((_bioDigitalMale3DModel));

            // ----- 

            _bioDigitalFemale3DModel = new XtraUserControlBioDigital3DModel(
                ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyFemaleModelIdConfigKey],
                ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyFemaleModelBaseObjIdConfigKey]) { InitializeInBackground = true, XRayModeEnabled = false };
            Controls.Add((_bioDigitalFemale3DModel));

//            // OR: Construct the model and add it to the form's controls so it start initializing manually.
//            _bioDigital3DModel = new XtraUserControlBioDigital3DModel(modelId, baseObjectId);
//            Controls.Add((_bioDigital3DModel));
//            _bioDigital3DModel.Initialize();

//            // You can also set the model source url after constructing the control.
//            _bioDigital3DModel = new XtraUserControlBioDigital3DModel() { InitializeInBackground = true };
//            _bioDigital3DModel.SetModel(modelId, baseObjectId);
//            Controls.Add((_bioDigital3DModel));
        }

       
        
    }
}
