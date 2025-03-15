using System.Drawing;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Skins.XtraForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace Vital.UI.Logic_Classes
{
    /// <summary>
    /// This is a custom form painter that we use to allow changing the font of the XtraForm since there is no direct way of changing
    /// it within the form itself.
    /// </summary>
    public class MyFormPainter : FormPainter
    {
        #region Properties

        /// <summary>
        /// Custom font used for drawing
        /// </summary>
        public Font CustomFont { get; set; }

        /// <summary>
        /// Current Form
        /// </summary>
        public XtraForm CurrentForm { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="provider"></param>
        /// <param name="customFont"></param>
        public MyFormPainter(Control owner, ISkinProvider provider, Font customFont, XtraForm form) : base(owner, provider)
        {
            CustomFont = customFont;
            CurrentForm = form;
        }
        
        #endregion

        #region Logic
        
        /// <summary>
        /// Custom draw text
        /// </summary>
        /// <param name="cache"></param>
        protected override void DrawText(DevExpress.Utils.Drawing.GraphicsCache cache)
        {
            var text = Text;

            if (string.IsNullOrEmpty(text) || TextBounds.IsEmpty) return;

            //Create appearance based on custom font passed to the form painter
            var appearance = new AppearanceObject(GetDefaultAppearance()) { Font = CustomFont };
            appearance.TextOptions.Trimming = Trimming.EllipsisCharacter;
            appearance.TextOptions.VAlignment = VertAlignment.Center;

            //Calculate text height based on font
            var newHeight = appearance.CalcDefaultTextSize(cache.Graphics).Height;

            //Determine the font position within the form header
            var newYPosition = (int)((cache.Graphics.VisibleClipBounds.Height - newHeight) / 2);//Here we deduct the font height from the form header height to make sure the font shows up in the middle
            var newXPosition = TextBounds.X;

            //var newYPosition = TextBounds.Y;
            //var newXPosition = TextBounds.X;

            //We found an issue where if teh form is maximized then the font will somehow show up at the edge of the form header
            //In such case we add 5 to the position from top and from left to add some spacing between the text and the edge of the form
            //to make the title look better
            if (CurrentForm.WindowState == FormWindowState.Maximized)
            {
                newYPosition += 5;
                newXPosition += 5;
            }

            var r = RectangleHelper.GetCenterBounds(new Rectangle(newXPosition, newYPosition, TextBounds.Width, TextBounds.Height), new Size(TextBounds.Width, newHeight));

            //Draw title within the header
            DrawTextShadow(cache, appearance, r);
            cache.DrawString(text, appearance.Font, appearance.GetForeBrush(cache), r, appearance.GetStringFormat());
        }

        #endregion
    }
}
