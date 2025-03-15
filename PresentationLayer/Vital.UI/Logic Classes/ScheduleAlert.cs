using System;
using System.Drawing;
using System.Windows.Forms;
using Vital.UI.Enums;

namespace Vital.UI.Logic_Classes
{
    public class ScheduleAlert
    {
        /// <summary>
        /// Gets or sets the Owner Form.
        /// </summary>
        public Form Owner { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public AlertKey Key { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the HoverMessage.
        /// </summary>
        public string HoverMessage { get; set; }

        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the ShowAt.
        /// </summary>
        public DateTime ShowAt { get; set; }

        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        public Image Image { get; set; }
    }
}