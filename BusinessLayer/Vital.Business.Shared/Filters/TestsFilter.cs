using System;
using Vital.Business.Shared.DomainObjects.Tests;

namespace Vital.Business.Shared.Filters
{
    public class TestsFilter : BaseFilter<Test>
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
        /// Gets or sets the points group id.
        /// </summary>
        public int ItemId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Test Protocol Id.
        /// </summary>
        public int TestProtocolId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public int PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date time of the test.
        /// </summary>
        public DateTime? DateTime
        {
            get;
            set;
        }

    }
}
