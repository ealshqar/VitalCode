using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Vital.Business.Shared.DomainObjects.Lookups;

namespace Vital.Business.Shared.DomainObjects.VitalForceSheet
{
    /// <summary>
    /// A shell class that is only used in VFS report to allow providing the required datasource structure needed to show
    /// multiple column layout needed in the report to match the paper design.
    /// </summary>
    public class VFSReportGroup
    {
        public Lookup GroupLookup { get; set; }
        public Lookup SectionLookup { get; set; } 
        public BindingList<VFSItem> VFSItems {get; set; }
    }
}
