using System;
using DevExpress.XtraWaitForm;

namespace Vital.UI.UI_Components.Forms
{
    public partial class WaitFormLoadingDataTopMost : WaitForm
    {
        #region Public Methods

        /// <summary>
        /// Loading form.
        /// </summary>
        public WaitFormLoadingDataTopMost()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        /// <summary>
        /// Sets the caption.
        /// </summary>
        /// <param name="caption">The caption.</param>
        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }

        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="description">The description</param>
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }

        #endregion
    }
}