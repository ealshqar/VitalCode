using System;
using Vital.Business.Shared.DomainObjects.Patients;

namespace Vital.Business.Shared.Filters
{
    public class PatientsFilter : BaseFilter<Patient>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public int Number
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ZIP code.
        /// </summary>
        public string Zip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public DateTime DateOfBirth
        {
            get;
            set;
        }
    }
}
