using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;

namespace Vital.UI.UI_Components.User_Controls.Modules.VitalDockManager
{
    public class VitalDockManager : DockManager
    {
        #region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        public VitalDockManager(IContainer container) : base(container) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="form"></param>
        public VitalDockManager(ContainerControl form) : base(form) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public VitalDockManager() : base() { }

        #endregion

        #region Logic
        
      
        #endregion
    }
}
