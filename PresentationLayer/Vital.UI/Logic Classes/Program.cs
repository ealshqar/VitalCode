using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.UserSkins;
using Microsoft.VisualBasic.ApplicationServices;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;
using Vital.UI.UI_Components.Forms;
using Vital.UI.UI_Components.User_Controls.BioDigital3DModel;

namespace Vital.UI
{
    internal static class Program
    {

        #region Main

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            SkinManager.EnableFormSkins();//This enables the skin of devexpress on the titlebar area of the forms making them look much nicer
            SkinManager.EnableMdiFormSkins();

            //Determine if the 3D human anatomy UI is enabled or disabled
            var useHumanAnatomyView = ConfigurationManager.AppSettings[ConfigKeys.UseHumanAnatomyView.ToString()].ToBoolean();

            if (useHumanAnatomyView)
            {
                //Initialize the 3D BigDigital models for male & female as early as possible to increase the chance they are loaded by the time
                //the user opens the test screen.
                //TODO: We might enable this logic only for certain technicians
                UiHelperClass.BioDigitalModelMale = new XtraUserControlBioDigital3DModel(
                    ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyMaleModelIdConfigKey],
                    ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyMaleModelBaseObjIdConfigKey]) { InitializeInBackground = true };

                UiHelperClass.BioDigitalModelFemale = new XtraUserControlBioDigital3DModel(
                    ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyFemaleModelIdConfigKey],
                    ConfigurationManager.AppSettings[StaticKeys.BioDigitalFullBodyFemaleModelBaseObjIdConfigKey]) { InitializeInBackground = true };
            }

            try
            {
                if (!Properties.Settings.Default.IsInstalled)
                {
                    Properties.Settings.Default.IsInstalled = true;
                    Properties.Settings.Default.Save();
                    return;
                }

                var controller = new VitalInstanceController();
                controller.Run(args);
            }
            catch (NoStartupFormException)
            {

            }
        }

        #endregion

    }
}
