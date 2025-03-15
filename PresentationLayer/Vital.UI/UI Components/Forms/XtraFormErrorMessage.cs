using System;
using System.Windows.Forms;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormErrorMessage : DevExpress.XtraEditors.XtraForm
    {
        #region Constructor

        public XtraFormErrorMessage()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the message title
        /// </summary>
        public string MessageTitle
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        /// <summary>
        /// The error text
        /// </summary>
        public string ErrorText
        {
            get
            {
                return memoEditError.Text;
            }
            set
            {
                memoEditError.Text = value;
                simpleButtonCopy.Enabled = !string.IsNullOrEmpty(value);
            }
        }

        /// <summary>
        /// Gets or sets the Header Message.
        /// </summary>
        public string HeaderMessage
        {
            get
            {
                return memoEditHeaderMessage.Text;
            }
            set
            {
                memoEditHeaderMessage.Text = value;
            }
        }

        public string ButtonText
        {
            set { simpleButtonClose.Text = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Copies the error to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ErrorText);
        }

        #endregion
    }
}