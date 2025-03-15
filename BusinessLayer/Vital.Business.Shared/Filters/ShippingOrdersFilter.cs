using System;
using Vital.Business.Shared.DomainObjects.ShippingOrders;

namespace Vital.Business.Shared.Filters
{
    public class ShippingOrdersFilter : BaseFilter<ShippingOrder>
    {
        
        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets Number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets TestId.
        /// </summary>
        public int? TestId { get; set; }

        /// <summary>
        /// Gets or sets SentDate.
        /// </summary>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Gets or sets SendToClient.
        /// </summary>
        public bool? SendToClient { get; set; }

        /// <summary>
        /// Gets or sets Sent.
        /// </summary>
        public bool? Sent { get; set; }

        /// <summary>
        /// Gets or sets PatientFirstName.
        /// </summary>
        public string PatientFirstName { get; set; }

        /// <summary>
        /// Gets or sets PatientLastName.
        /// </summary>
        public string PatientLastName { get; set; }

        /// <summary>
        /// Gets or sets PatientAddress1.
        /// </summary>
        public string PatientAddress1 { get; set; }

        /// <summary>
        /// Gets or sets PatientAddress2.
        /// </summary>
        public string PatientAddress2 { get; set; }

        /// <summary>
        /// Gets or sets PatientCity.
        /// </summary>
        public string PatientCity { get; set; }

        /// <summary>
        /// Gets or sets PatientState.
        /// </summary>
        public string PatientState { get; set; }

        /// <summary>
        /// Gets or sets PatientZip.
        /// </summary>
        public string PatientZip { get; set; }

        /// <summary>
        /// Gets or sets PatientHomePhone.
        /// </summary>
        public string PatientHomePhone { get; set; }

        /// <summary>
        /// Gets or sets PatientWorkPhone.
        /// </summary>
        public string PatientWorkPhone { get; set; }

        /// <summary>
        /// Gets or sets PatientCellPhone.
        /// </summary>
        public string PatientCellPhone { get; set; }

        /// <summary>
        /// Gets or sets PatientFax.
        /// </summary>
        public string PatientFax { get; set; }

        /// <summary>
        /// Gets or sets PatientEmail.
        /// </summary>
        public string PatientEmail { get; set; }

        /// <summary>
        /// Gets or sets TechnicianName.
        /// </summary>
        public string TechnicianName { get; set; }

        /// <summary>
        /// Gets or sets TechnicianAddress.
        /// </summary>
        public string TechnicianAddress { get; set; }

        /// <summary>
        /// Gets or sets TechnicianState.
        /// </summary>
        public string TechnicianState { get; set; }

        /// <summary>
        /// Gets or sets TechnicianZipCode.
        /// </summary>
        public string TechnicianZipCode { get; set; }

        /// <summary>
        /// Gets or sets TechnicianCity.
        /// </summary>
        public string TechnicianCity { get; set; }

        /// <summary>
        /// Gets or sets TechnicianPhone.
        /// </summary>
        public string TechnicianPhone { get; set; }
    }
}
