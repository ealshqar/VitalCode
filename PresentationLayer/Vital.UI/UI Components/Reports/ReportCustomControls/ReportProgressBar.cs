using System.ComponentModel;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using Vital.UI.UI_Components.BaseForms;

namespace Vital.UI.UI_Components.Reports.ReportCustomControls
{
    // The DefaultBindableProperty attribute is intended to make the Position  
    // property bindable when an item is dropped from the Field List. 
    [ToolboxItem(true), DefaultBindableProperty("Position"), ToolboxBitmap(typeof (ReportProgressBar))]
    public class ReportProgressBar : XRControl
    {
        #region Fields

        private float _pos;
        private float _maxVal = 100;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ReportProgressBar()
        {
            ForeColor = SystemColors.Control;
        }

        #endregion

        #region Properties

        // Define the MaxValue property. 
        [DefaultValue(100)]
        public float MaxValue
        {
            get { return _maxVal; }
            set
            {
                if (value <= 0) return;
                _maxVal = value;
            }
        }

        // Define the Position property.  
        [DefaultValue(0), Bindable(true)]
        public float Position
        {
            get { return _pos; }
            set
            {
                if (value < 0 || value > _maxVal) return;
                _pos = value;
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Override the XRControl.CreateBrick method. 
        /// </summary>
        /// <param name="childrenBricks"></param>
        /// <returns></returns>
        protected override VisualBrick CreateBrick(VisualBrick[] childrenBricks)
        {
            // Use this code to make the progress bar control  
            // always represented as a Panel brick. 
            return new PanelBrick(this);
        }

        /// <summary>
        /// Override the XRControl.PutStateToBrick method. 
        /// </summary>
        /// <param name="brick"></param>
        /// <param name="ps"></param>
        protected override void PutStateToBrick(VisualBrick brick, PrintingSystemBase ps)
        {
            // Call the PutStateToBrick method of the base class. 
            base.PutStateToBrick(brick, ps);

            // Get the Panel brick which represents the current progress bar control. 
            var panel = (PanelBrick) brick;

            // Create a new VisualBrick to be inserted into the panel brick. 
            var progressBar = new VisualBrick(this)
                                  {
                                      Sides = BorderSide.None,
                                      BackColor = VitalBaseForm.GetRangeColor((int) Position),
                                      Rect = new RectangleF(0, 0, panel.Rect.Width*(Position/MaxValue),
                                                            panel.Rect.Height)
                                  };

            //Create a lable to show the reading value
            var labelBrick = new LabelBrick
                                 {
                                     Location = new PointF(0, 0),
                                     Size = new SizeF(panel.Rect.Width, panel.Rect.Height),
                                     Text = Position.ToString(),
                                     ForeColor = (progressBar.BackColor == Color.Firebrick) ? Color.White : Color.Black,
                                     BackColor = Color.Transparent,
                                     Font = new Font("Tahoma", 8F),
                                     HorzAlignment = HorzAlignment.Center,
                                     VertAlignment = VertAlignment.Center,
                                     BorderColor = Color.Transparent,
                                     BorderWidth = 0
                                 };

            // Add the VisualBrick to the panel. 
            panel.Bricks.Add(progressBar);
            panel.Bricks.Add(labelBrick);
        }

        #endregion
    }
}
