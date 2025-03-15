using System;
using DevExpress.XtraSplashScreen;
using Vital.Business.Shared.Shared;

namespace Vital.UI.UI_Components.Forms
{
    public partial class SplashScreenVital : SplashScreen
    {
        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreenVital()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Performs logic for certain commands
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            var command = (SplashScreenCommand)cmd;
            if (command == SplashScreenCommand.SetText)
            {
                labelControl2.Text = (string)arg;                
            }
        }
    }
}