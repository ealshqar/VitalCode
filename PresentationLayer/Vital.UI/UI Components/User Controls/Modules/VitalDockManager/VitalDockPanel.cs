using DevExpress.XtraBars.Docking;

namespace Vital.UI.UI_Components.User_Controls.Modules.VitalDockManager
{
    public class VitalDockPanel : DockPanel
    {
        #region Private Members
        
        private bool _hideHeader;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Indicates if the header of the dock panel will be hidden
        /// </summary>
        public bool HideHeader
	    {
            get { return _hideHeader; }
	        set
	        {
                _hideHeader = value;
                //Here we set the "ShowHeader" property of the DockLayout because it is the class that actually shows or hides the caption
                //not the DockPanel itself, we are just using the dock panel as means to communicate with with the DockLayout since the DockLayout
                //doesn't actually appear in form.
                //Notice that we set the property here inside the property setter because if we don't do so then the DockLayout will never get the right
                //property value since it gets set when the DockPanel is initialized where it will also initialize the DockLayout and that the point the
                //property ShowHeader is not being set yet, the solution would be to pass it in the constructor but this approach of using the setter is
                //just cleaner and easier.

	            if (DockLayout != null)
	            {
                    (DockLayout as VitalDockLayout).HideHeader = value;    
	            }
                
	        }
	    }

        #endregion

        #region Contructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        public VitalDockPanel(): base()
		{
		    if (!(DockLayout is VitalDockLayout))
		    {
                DockLayout = new VitalDockLayout(Dock, this);
		    }
		}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createControlContainer"></param>
        /// <param name="dock"></param>
        /// <param name="dockManager"></param>
		protected internal VitalDockPanel(bool createControlContainer, DockingStyle dock, DockManager dockManager): base(createControlContainer, dock, dockManager)
		{
		    if (!(DockLayout is VitalDockLayout))
		    {
                DockLayout = new VitalDockLayout(Dock, this);
		    }
        }

        #endregion
    }
}
