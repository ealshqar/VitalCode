using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class AutoItemsFilter : BaseFilter<AutoItem>
    {
        /// <summary>
        /// Gets or sets the AutoItemId.
        /// </summary>
        public int AutoItemId { get; set; }
        
        /// <summary>
        /// Gets or sets the TestingPoint.
        /// </summary>
        public int TestingPointsId { get; set; }
        
        /// <summary>
        /// Gets or sets the Image.
        /// </summary>
        public int ImageId { get; set; }
        
        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public int TypeLookupId { get; set; }

        /// <summary>
        /// Gets or sets the GenderLookup.
        /// </summary>
        public int GenderLookupId { get; set; }

        /// <summary>
        /// Gets or sets the StructureType.
        /// </summary>
        public int StructureTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public int StatusLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the ChildsOrderType.
        /// </summary>
        public int ChildsOrderTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the ChildsScanningType.
        /// </summary>
        public int ChildsScanningTypeLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the ScanningMethod.
        /// </summary>
        public int ScanningMethodLookupId { get; set; }
        
        /// <summary>
        /// Gets or sets the ScansNumber.
        /// </summary>
        public int ScansNumber { get; set; }

        /// <summary>
        /// Gets or sets the MatchesNumber.
        /// </summary>
        public int MatchesNumber { get; set; }

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
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the Frequency.
        /// </summary>
        public string Frequency { get; set; }

        /// <summary>
        /// Gets or sets the ModelIdentifier.
        /// </summary>
        public string ModelIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the UserNotes.
        /// </summary>
        public string UserNotes { get; set; }

        /// <summary>
        /// Gets or sets the DirectAccessChecks.
        /// </summary>
        public string DirectAccessChecks { get; set; }
        
        /// <summary>
        /// Gets or sets the IsUserItem.
        /// </summary>
        public bool? IsUserItem { get; set; }
        
        /// <summary>
        /// Gets or sets the IsSearchable.
        /// </summary>
        public bool? IsSearchable { get; set; }
        
        /// <summary>
        /// Gets or sets the InsertOnNo.
        /// </summary>
        public bool? InsertOnNo { get; set; }

        /// <summary>
        /// Gets or sets the FinishAllScanRounds.
        /// </summary>
        public bool? FinishAllScanRounds { get; set; }

        /// <summary>
        /// Gets or sets the AddResultOnMatch.
        /// </summary>
        public bool? AddResultOnMatch { get; set; }

        /// <summary>
        /// Gets or sets the ExcludeOnMatch.
        /// </summary>
        public bool? ExcludeOnMatch { get; set; }

        /// <summary>
        /// Gets or sets the AddAllChildesOnMatch.
        /// </summary>
        public bool? AddAllChildesOnMatch { get; set; }

        /// <summary>
        /// Gets or sets the CheckVitalForce.
        /// </summary>
        public bool? CheckVitalForce { get; set; }
        
        /// <summary>
        /// Gets or sets the CheckFourFactors.
        /// </summary>
        public bool? CheckFourFactors { get; set; }
        
        /// <summary>
        /// Gets or sets the CheckRatios.
        /// </summary>
        public bool? CheckRatios { get; set; }
        
        /// <summary>
        /// Gets or sets the CheckDilutions.
        /// </summary>
        public bool? CheckDilutions { get; set; }
        
        /// <summary>
        /// Gets or sets the IsImprintable.
        /// </summary>
        public bool? IsImprintable { get; set; }
        
    }
}