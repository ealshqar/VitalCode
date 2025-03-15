using System.ComponentModel;
using System.Linq;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Readings;

namespace Vital.Business.Shared.DomainObjects.Memory_Shell_Objects
{
    public class ReadingPointSet
    {
        /// <summary>
        /// Inidicate if the readings in the pointset are all empty as hasn't been tested
        /// </summary>
        public bool AllReadingsEmpty
        {
            get
            {
                return Readings == null || Readings.All(r => r.Value == 0 && r.ValueBalanced == 0);
            }
        }

        /// <summary>
        /// The pointset item
        /// </summary>
        public Item PointSetItem { get; set; }

        /// <summary>
        /// Readings that belong to the pointset
        /// </summary>
        public BindingList<Reading> Readings { get; set; }
    }
}
