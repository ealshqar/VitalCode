using System;
using System.Collections.Generic;

namespace Vital.Business.Shared.AmplitudeHelper
{
    public class AmplitudeEvent
    {
        #region Constructors

        /// <summary>
        /// Constructs a AmplitudeEvent. 
        /// </summary>
        public AmplitudeEvent()
        {
            DateTime = System.DateTime.UtcNow;
        }

        /// <summary>
        /// Constructs a AmplitudeEvent with an event type. 
        /// </summary>
        public AmplitudeEvent(string eventType)
            : this()
        {
            EventType = eventType;
        }

        #endregion

        #region InternalMembers

        /// <summary>
        /// Gets or sets the session Id.
        /// </summary>
        internal long SessionId { get; set; }

        #endregion

        #region PublicProperties

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the event properties.
        /// </summary>
        public IDictionary<string, object> EventProperties { get; set; }

        /// <summary>
        /// Gets or sets the User properties.
        /// </summary>
        public IDictionary<string, object> UserProperties { get; set; }

        /// <summary>
        /// Gets or sets the API properties.
        /// </summary>
        public IDictionary<string, object> ApiProperties { get; set; }

        /// <summary>
        /// Gets or sets the event dateTime.
        /// </summary>
        public DateTime? DateTime { get; set; }

        #endregion
    }
}