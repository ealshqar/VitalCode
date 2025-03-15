using System.Windows.Forms;
using DevExpress.Utils;

namespace Vital.UI.UI_Components.UI_Classes
{
    /// <summary>
    /// This class is used to workaround showing the tooltips on the disabled controls.
    /// </summary>
    public class DisabledControl
    {
        public Control Control
        {
            get;
            set;
        }

        public ToolTipType ToolTipType
        {
            get;
            set;
        }
    }
}
