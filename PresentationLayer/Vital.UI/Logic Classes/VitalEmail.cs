
namespace Vital.UI.Logic_Classes
{
    public class VitalEmail
    {
        #region Target Emails

        /// <summary>
        /// Gets or sets the ShippingTargetEmail.
        /// </summary>
        public string ShippingTargetEmail { get; set; }

        /// <summary>
        /// Gets or sets the FeedbackTargetEmail.
        /// </summary>
        public string FeedbackTargetEmail { get; set; }

        #endregion

        #region Sender Emails

        /// <summary>
        /// Gets or sets the ShippingSenderEmail.
        /// </summary>
        public string ShippingSenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the ShippingSenderPass.
        /// </summary>
        public string ShippingSenderPass { get; set; }

        /// <summary>
        /// Gets or sets the FeedbackSenderEmail.
        /// </summary>
        public string FeedbackSenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the FeedbackSenderPass.
        /// </summary>
        public string FeedbackSenderPass { get; set; }

        /// <summary>
        /// Gets or sets the ExceptionSenderEmail.
        /// </summary>
        public string ExceptionSenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the ExceptionSenderPass.
        /// </summary>
        public string ExceptionSenderPass { get; set; }

        #endregion
    }
}
