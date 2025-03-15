using System.Drawing;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking.Helpers;

namespace Vital.UI.UI_Components.User_Controls.Modules.VitalDockManager
{
    public class VitalDockLayout : DockLayout
    {
        #region Public Properties

        /// <summary>
        /// Indicates if the header of the dock panel will be hidden
        /// </summary>
        public bool HideHeader { get; set; }

        /// <summary>
        /// This property is a base property that indicates if the caption can be shown or not
        /// </summary>
        protected override bool HasCaption
        {
            get
            {
                return !HideHeader;
            }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dock"></param>
        /// <param name="panel"></param>
        public VitalDockLayout(DockingStyle dock, DockPanel panel) : base(dock, panel) { }
        
        #endregion
    }
}
