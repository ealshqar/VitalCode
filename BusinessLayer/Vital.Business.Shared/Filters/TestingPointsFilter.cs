using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class TestingPointsFilter : BaseFilter<TestingPoint>
    {
        /// <summary>
        /// Gets or sets the TestingPointId.
        /// </summary>
        public int TestingPointId { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the FullName.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the HWIdentifier.
        /// </summary>
        public string HWIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

    }
}
