///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET20
// Templates vendor: Solutions Design.
// Templates version: 
//////////////////////////////////////////////////////////////
using System;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Vital.DataLayer.HelperClasses
{
	
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	
	/// <summary>Singleton implementation of the FieldInfoProvider. This class is the singleton wrapper through which the actual instance is retrieved.</summary>
	/// <remarks>It uses a single instance of an internal class. The access isn't marked with locks as the FieldInfoProviderBase class is threadsafe.</remarks>
	internal static class FieldInfoProviderSingleton
	{
		#region Class Member Declarations
		private static readonly IFieldInfoProvider _providerInstance = new FieldInfoProviderCore();
		#endregion

		/// <summary>Dummy static constructor to make sure threadsafe initialization is performed.</summary>
		static FieldInfoProviderSingleton()
		{
		}

		/// <summary>Gets the singleton instance of the FieldInfoProviderCore</summary>
		/// <returns>Instance of the FieldInfoProvider.</returns>
		public static IFieldInfoProvider GetInstance()
		{
			return _providerInstance;
		}
	}

	/// <summary>Actual implementation of the FieldInfoProvider. Used by singleton wrapper.</summary>
	internal class FieldInfoProviderCore : FieldInfoProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="FieldInfoProviderCore"/> class.</summary>
		internal FieldInfoProviderCore()
		{
			Init();
		}

		/// <summary>Method which initializes the internal datastores.</summary>
		private void Init()
		{
			this.InitClass( (60 + 0));
			InitAppImageEntityInfos();
			InitAppInfoEntityInfos();
			InitAutoItemEntityInfos();
			InitAutoItemRelationEntityInfos();
			InitAutoProtocolEntityInfos();
			InitAutoProtocolRevisionEntityInfos();
			InitAutoProtocolStageEntityInfos();
			InitAutoProtocolStageRevisionEntityInfos();
			InitAutoTestEntityInfos();
			InitAutoTestResultEntityInfos();
			InitAutoTestResultProductEntityInfos();
			InitAutoTestStageEntityInfos();
			InitClinicProductPricingEntityInfos();
			InitDosageOptionEntityInfos();
			InitFrequencyTestEntityInfos();
			InitFrequencyTestResultEntityInfos();
			InitHwProfileEntityInfos();
			InitImageEntityInfos();
			InitInvoiceEntityInfos();
			InitIssueNavigationStepEntityInfos();
			InitItemEntityInfos();
			InitItemDetailsEntityInfos();
			InitItemPropertyEntityInfos();
			InitItemRelationEntityInfos();
			InitItemRelationPropertyEntityInfos();
			InitItemTargetEntityInfos();
			InitLookupEntityInfos();
			InitOrderItemEntityInfos();
			InitPatientEntityInfos();
			InitPatientHistoryEntityInfos();
			InitProductEntityInfos();
			InitProductFormEntityInfos();
			InitProductSizeEntityInfos();
			InitPropertyEntityInfos();
			InitProtocolItemEntityInfos();
			InitProtocolStepEntityInfos();
			InitReadingEntityInfos();
			InitScheduleLineEntityInfos();
			InitServiceEntityInfos();
			InitSettingEntityInfos();
			InitShippingOrderEntityInfos();
			InitSpotCheckEntityInfos();
			InitSpotCheckResultEntityInfos();
			InitStageAnnouncementEntityInfos();
			InitStageAutoItemEntityInfos();
			InitTestEntityInfos();
			InitTestImprintableItemEntityInfos();
			InitTestingPointEntityInfos();
			InitTestIssueEntityInfos();
			InitTestProtocolEntityInfos();
			InitTestResultEntityInfos();
			InitTestResultFactorsEntityInfos();
			InitTestScheduleEntityInfos();
			InitTestServiceEntityInfos();
			InitUserEntityInfos();
			InitVFSEntityInfos();
			InitVFSItemEntityInfos();
			InitVFSItemSourceEntityInfos();
			InitVFSSecondaryItemEntityInfos();
			InitVFSSecondaryItemSourceEntityInfos();

			this.ConstructElementFieldStructures(InheritanceInfoProviderSingleton.GetInstance());
		}

		/// <summary>Inits AppImageEntity's FieldInfo objects</summary>
		private void InitAppImageEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AppImageFieldIndex), "AppImageEntity");
			this.AddElementFieldInfo("AppImageEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AppImageFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AppImageEntity", "Property", typeof(System.String), false, false, false, false,  (int)AppImageFieldIndex.Property, 2147483647, 0, 0);
			this.AddElementFieldInfo("AppImageEntity", "Value", typeof(System.Byte[]), false, false, false, true,  (int)AppImageFieldIndex.Value, 2147483647, 0, 0);
		}
		/// <summary>Inits AppInfoEntity's FieldInfo objects</summary>
		private void InitAppInfoEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AppInfoFieldIndex), "AppInfoEntity");
			this.AddElementFieldInfo("AppInfoEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AppInfoFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AppInfoEntity", "Property", typeof(System.String), false, false, false, false,  (int)AppInfoFieldIndex.Property, 2147483647, 0, 0);
			this.AddElementFieldInfo("AppInfoEntity", "Value", typeof(System.String), false, false, false, false,  (int)AppInfoFieldIndex.Value, 2147483647, 0, 0);
		}
		/// <summary>Inits AutoItemEntity's FieldInfo objects</summary>
		private void InitAutoItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoItemFieldIndex), "AutoItemEntity");
			this.AddElementFieldInfo("AutoItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "TestingPointsId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.TestingPointsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "ImageId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)AutoItemFieldIndex.ImageId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "TypeLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "StructureTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.StructureTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "StatusLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.StatusLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "ChildsOrderTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.ChildsOrderTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "ChildsScanningTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.ChildsScanningTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "ScanningMethodLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.ScanningMethodLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "ScansNumber", typeof(System.Int32), false, false, false, false,  (int)AutoItemFieldIndex.ScansNumber, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "Key", typeof(System.String), false, false, false, false,  (int)AutoItemFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "Name", typeof(System.String), false, false, false, false,  (int)AutoItemFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "FullName", typeof(System.String), false, false, false, true,  (int)AutoItemFieldIndex.FullName, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "Description", typeof(System.String), false, false, false, true,  (int)AutoItemFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "Frequency", typeof(System.String), false, false, false, true,  (int)AutoItemFieldIndex.Frequency, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "UserNotes", typeof(System.String), false, false, false, true,  (int)AutoItemFieldIndex.UserNotes, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "IsUserItem", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.IsUserItem, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "IsSearchable", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.IsSearchable, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "InsertOnNo", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.InsertOnNo, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "IsImprintable", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.IsImprintable, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoItemFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "MatchesNumber", typeof(System.Int32), false, false, false, false,  (int)AutoItemFieldIndex.MatchesNumber, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemEntity", "FinishAllScanRounds", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.FinishAllScanRounds, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "AddResultOnMatch", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.AddResultOnMatch, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "ExcludeOnMatch", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.ExcludeOnMatch, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "AddAllChildesOnMatch", typeof(System.Boolean), false, false, false, false,  (int)AutoItemFieldIndex.AddAllChildesOnMatch, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "ModelIdentifier", typeof(System.String), false, false, false, true,  (int)AutoItemFieldIndex.ModelIdentifier, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "DirectAccessChecks", typeof(System.String), false, false, false, true,  (int)AutoItemFieldIndex.DirectAccessChecks, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoItemEntity", "GenderLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoItemFieldIndex.GenderLookupId, 0, 0, 10);
		}
		/// <summary>Inits AutoItemRelationEntity's FieldInfo objects</summary>
		private void InitAutoItemRelationEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoItemRelationFieldIndex), "AutoItemRelationEntity");
			this.AddElementFieldInfo("AutoItemRelationEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoItemRelationFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemRelationEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoItemRelationFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemRelationEntity", "AutoItemParentId", typeof(System.Int32), false, true, false, false,  (int)AutoItemRelationFieldIndex.AutoItemParentId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemRelationEntity", "AutoItemChildId", typeof(System.Int32), false, true, false, false,  (int)AutoItemRelationFieldIndex.AutoItemChildId, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemRelationEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)AutoItemRelationFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("AutoItemRelationEntity", "IsDeleted", typeof(System.Boolean), false, false, false, false,  (int)AutoItemRelationFieldIndex.IsDeleted, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemRelationEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoItemRelationFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoItemRelationEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoItemRelationFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits AutoProtocolEntity's FieldInfo objects</summary>
		private void InitAutoProtocolEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoProtocolFieldIndex), "AutoProtocolEntity");
			this.AddElementFieldInfo("AutoProtocolEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoProtocolFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolEntity", "Name", typeof(System.String), false, false, false, false,  (int)AutoProtocolFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoProtocolEntity", "Key", typeof(System.String), false, false, false, false,  (int)AutoProtocolFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoProtocolEntity", "IsSystemProtocol", typeof(System.Boolean), false, false, false, false,  (int)AutoProtocolFieldIndex.IsSystemProtocol, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolEntity", "IsDefaultProtocol", typeof(System.Boolean), false, false, false, false,  (int)AutoProtocolFieldIndex.IsDefaultProtocol, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolEntity", "IsDeleted", typeof(System.Boolean), false, false, false, false,  (int)AutoProtocolFieldIndex.IsDeleted, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits AutoProtocolRevisionEntity's FieldInfo objects</summary>
		private void InitAutoProtocolRevisionEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoProtocolRevisionFieldIndex), "AutoProtocolRevisionEntity");
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoProtocolRevisionFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolRevisionFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "AutoProtocolsId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolRevisionFieldIndex.AutoProtocolsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "Name", typeof(System.String), false, false, false, false,  (int)AutoProtocolRevisionFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "Key", typeof(System.String), false, false, false, false,  (int)AutoProtocolRevisionFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "IsSystemProtocol", typeof(System.Boolean), false, false, false, false,  (int)AutoProtocolRevisionFieldIndex.IsSystemProtocol, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolRevisionFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolRevisionEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolRevisionFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits AutoProtocolStageEntity's FieldInfo objects</summary>
		private void InitAutoProtocolStageEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoProtocolStageFieldIndex), "AutoProtocolStageEntity");
			this.AddElementFieldInfo("AutoProtocolStageEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoProtocolStageFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "AutoProtocolsId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageFieldIndex.AutoProtocolsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "AutoTestStagesId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageFieldIndex.AutoTestStagesId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "StageItemsOrderTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageFieldIndex.StageItemsOrderTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)AutoProtocolStageFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "IsDeleted", typeof(System.Boolean), false, false, false, false,  (int)AutoProtocolStageFieldIndex.IsDeleted, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolStageFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolStageEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolStageFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits AutoProtocolStageRevisionEntity's FieldInfo objects</summary>
		private void InitAutoProtocolStageRevisionEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoProtocolStageRevisionFieldIndex), "AutoProtocolStageRevisionEntity");
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoProtocolStageRevisionFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageRevisionFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "AutoProtocolRevisionsId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageRevisionFieldIndex.AutoProtocolRevisionsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "AutoProtocolStagesId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageRevisionFieldIndex.AutoProtocolStagesId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "AutoTestStagesId", typeof(System.Int32), false, true, false, false,  (int)AutoProtocolStageRevisionFieldIndex.AutoTestStagesId, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)AutoProtocolStageRevisionFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolStageRevisionFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoProtocolStageRevisionEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoProtocolStageRevisionFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits AutoTestEntity's FieldInfo objects</summary>
		private void InitAutoTestEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoTestFieldIndex), "AutoTestEntity");
			this.AddElementFieldInfo("AutoTestEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoTestFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoTestFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestEntity", "PatientId", typeof(System.Int32), false, true, false, false,  (int)AutoTestFieldIndex.PatientId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestEntity", "AutoProtocolRevisionsId", typeof(System.Int32), false, true, false, false,  (int)AutoTestFieldIndex.AutoProtocolRevisionsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestEntity", "Name", typeof(System.String), false, false, false, true,  (int)AutoTestFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestEntity", "Description", typeof(System.String), false, false, false, true,  (int)AutoTestFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestEntity", "Notes", typeof(System.String), false, false, false, true,  (int)AutoTestFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestEntity", "TestDate", typeof(System.DateTime), false, false, false, false,  (int)AutoTestFieldIndex.TestDate, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits AutoTestResultEntity's FieldInfo objects</summary>
		private void InitAutoTestResultEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoTestResultFieldIndex), "AutoTestResultEntity");
			this.AddElementFieldInfo("AutoTestResultEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoTestResultFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoTestResultFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "AutoTestsId", typeof(System.Int32), false, true, false, false,  (int)AutoTestResultFieldIndex.AutoTestsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "AutoItemsId", typeof(System.Int32), false, true, false, false,  (int)AutoTestResultFieldIndex.AutoItemsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "AutoProtocolStageRevisionsId", typeof(System.Int32), false, true, false, false,  (int)AutoTestResultFieldIndex.AutoProtocolStageRevisionsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "PreliminaryReading", typeof(Nullable<System.Int32>), false, false, false, true,  (int)AutoTestResultFieldIndex.PreliminaryReading, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "SummaryReading", typeof(Nullable<System.Int32>), false, false, false, true,  (int)AutoTestResultFieldIndex.SummaryReading, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultEntity", "IsAddedManually", typeof(System.Boolean), false, false, false, false,  (int)AutoTestResultFieldIndex.IsAddedManually, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestResultEntity", "Notes", typeof(System.String), false, false, false, true,  (int)AutoTestResultFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestResultEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestResultFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestResultEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestResultFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestResultEntity", "AutoTestResultsParentId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)AutoTestResultFieldIndex.AutoTestResultsParentId, 0, 0, 10);
		}
		/// <summary>Inits AutoTestResultProductEntity's FieldInfo objects</summary>
		private void InitAutoTestResultProductEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoTestResultProductFieldIndex), "AutoTestResultProductEntity");
			this.AddElementFieldInfo("AutoTestResultProductEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoTestResultProductFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoTestResultProductFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "AutoTestResultsId", typeof(System.Int32), false, true, false, false,  (int)AutoTestResultProductFieldIndex.AutoTestResultsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "Quantity", typeof(System.Int32), false, false, false, false,  (int)AutoTestResultProductFieldIndex.Quantity, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "Price", typeof(System.Decimal), false, false, false, false,  (int)AutoTestResultProductFieldIndex.Price, 0, 4, 19);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "Duration", typeof(System.String), false, false, false, true,  (int)AutoTestResultProductFieldIndex.Duration, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "Schedule", typeof(System.String), false, false, false, true,  (int)AutoTestResultProductFieldIndex.Schedule, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "SuggestedUsage", typeof(System.String), false, false, false, true,  (int)AutoTestResultProductFieldIndex.SuggestedUsage, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "Comments", typeof(System.String), false, false, false, true,  (int)AutoTestResultProductFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestResultProductFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestResultProductFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "IsChecked", typeof(System.Boolean), false, false, false, false,  (int)AutoTestResultProductFieldIndex.IsChecked, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "ProductFormsId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)AutoTestResultProductFieldIndex.ProductFormsId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestResultProductEntity", "ProductSizesId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)AutoTestResultProductFieldIndex.ProductSizesId, 0, 0, 10);
		}
		/// <summary>Inits AutoTestStageEntity's FieldInfo objects</summary>
		private void InitAutoTestStageEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(AutoTestStageFieldIndex), "AutoTestStageEntity");
			this.AddElementFieldInfo("AutoTestStageEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)AutoTestStageFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestStageEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)AutoTestStageFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestStageEntity", "StageItemsOrderTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)AutoTestStageFieldIndex.StageItemsOrderTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("AutoTestStageEntity", "Name", typeof(System.String), false, false, false, false,  (int)AutoTestStageFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "Key", typeof(System.String), false, false, false, false,  (int)AutoTestStageFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "Description", typeof(System.String), false, false, false, true,  (int)AutoTestStageFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "Dependencies", typeof(System.String), false, false, false, true,  (int)AutoTestStageFieldIndex.Dependencies, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "IsMultiLevel", typeof(System.Boolean), false, false, false, false,  (int)AutoTestStageFieldIndex.IsMultiLevel, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "ScanTypeEnabled", typeof(System.Boolean), false, false, false, false,  (int)AutoTestStageFieldIndex.ScanTypeEnabled, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestStageFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)AutoTestStageFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "StageTabKey", typeof(System.String), false, false, false, false,  (int)AutoTestStageFieldIndex.StageTabKey, 2147483647, 0, 0);
			this.AddElementFieldInfo("AutoTestStageEntity", "IsDestinationOnly", typeof(System.Boolean), false, false, false, false,  (int)AutoTestStageFieldIndex.IsDestinationOnly, 0, 0, 0);
		}
		/// <summary>Inits ClinicProductPricingEntity's FieldInfo objects</summary>
		private void InitClinicProductPricingEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ClinicProductPricingFieldIndex), "ClinicProductPricingEntity");
			this.AddElementFieldInfo("ClinicProductPricingEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ClinicProductPricingFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ClinicProductPricingFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "ProductsId", typeof(System.Int32), false, true, false, false,  (int)ClinicProductPricingFieldIndex.ProductsId, 0, 0, 10);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "Form", typeof(System.String), false, false, false, false,  (int)ClinicProductPricingFieldIndex.Form, 2147483647, 0, 0);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "Size", typeof(System.String), false, false, false, false,  (int)ClinicProductPricingFieldIndex.Size, 2147483647, 0, 0);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "Price", typeof(System.Decimal), false, false, false, false,  (int)ClinicProductPricingFieldIndex.Price, 0, 4, 19);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ClinicProductPricingFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ClinicProductPricingEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ClinicProductPricingFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits DosageOptionEntity's FieldInfo objects</summary>
		private void InitDosageOptionEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(DosageOptionFieldIndex), "DosageOptionEntity");
			this.AddElementFieldInfo("DosageOptionEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)DosageOptionFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("DosageOptionEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)DosageOptionFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("DosageOptionEntity", "ProductFormsId", typeof(System.Int32), false, true, false, false,  (int)DosageOptionFieldIndex.ProductFormsId, 0, 0, 10);
			this.AddElementFieldInfo("DosageOptionEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)DosageOptionFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("DosageOptionEntity", "Name", typeof(System.String), false, false, false, false,  (int)DosageOptionFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("DosageOptionEntity", "UsageSchedule", typeof(System.String), false, false, false, true,  (int)DosageOptionFieldIndex.UsageSchedule, 2147483647, 0, 0);
			this.AddElementFieldInfo("DosageOptionEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)DosageOptionFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("DosageOptionEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)DosageOptionFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("DosageOptionEntity", "SuggestedUsage", typeof(System.String), false, false, false, true,  (int)DosageOptionFieldIndex.SuggestedUsage, 2147483647, 0, 0);
		}
		/// <summary>Inits FrequencyTestEntity's FieldInfo objects</summary>
		private void InitFrequencyTestEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(FrequencyTestFieldIndex), "FrequencyTestEntity");
			this.AddElementFieldInfo("FrequencyTestEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)FrequencyTestFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestEntity", "PatientId", typeof(System.Int32), false, true, false, false,  (int)FrequencyTestFieldIndex.PatientId, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestEntity", "Name", typeof(System.String), false, false, false, true,  (int)FrequencyTestFieldIndex.Name, 200, 0, 0);
			this.AddElementFieldInfo("FrequencyTestEntity", "Notes", typeof(System.String), false, false, false, true,  (int)FrequencyTestFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("FrequencyTestEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)FrequencyTestFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("FrequencyTestEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)FrequencyTestFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("FrequencyTestEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)FrequencyTestFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits FrequencyTestResultEntity's FieldInfo objects</summary>
		private void InitFrequencyTestResultEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(FrequencyTestResultFieldIndex), "FrequencyTestResultEntity");
			this.AddElementFieldInfo("FrequencyTestResultEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)FrequencyTestResultFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "FrequencyTestId", typeof(System.Int32), false, true, false, false,  (int)FrequencyTestResultFieldIndex.FrequencyTestId, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "ItemId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)FrequencyTestResultFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "Notes", typeof(System.String), false, false, false, true,  (int)FrequencyTestResultFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)FrequencyTestResultFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)FrequencyTestResultFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)FrequencyTestResultFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "TimesPerWeek", typeof(Nullable<System.Int32>), false, false, false, true,  (int)FrequencyTestResultFieldIndex.TimesPerWeek, 0, 0, 10);
			this.AddElementFieldInfo("FrequencyTestResultEntity", "NumberOfWeeks", typeof(Nullable<System.Int32>), false, false, false, true,  (int)FrequencyTestResultFieldIndex.NumberOfWeeks, 0, 0, 10);
		}
		/// <summary>Inits HwProfileEntity's FieldInfo objects</summary>
		private void InitHwProfileEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(HwProfileFieldIndex), "HwProfileEntity");
			this.AddElementFieldInfo("HwProfileEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)HwProfileFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "Name", typeof(System.String), false, false, false, false,  (int)HwProfileFieldIndex.Name, 200, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "MinReading", typeof(System.Int32), false, false, false, false,  (int)HwProfileFieldIndex.MinReading, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "DisconnectedTimeout", typeof(System.Int32), false, false, false, false,  (int)HwProfileFieldIndex.DisconnectedTimeout, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "StabilityTimeout", typeof(System.Int32), false, false, false, false,  (int)HwProfileFieldIndex.StabilityTimeout, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "StabilityRange", typeof(System.Int32), false, false, false, false,  (int)HwProfileFieldIndex.StabilityRange, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "IsSystemProfile", typeof(System.Boolean), false, false, false, false,  (int)HwProfileFieldIndex.IsSystemProfile, 0, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "IsDefault", typeof(System.Boolean), false, false, false, false,  (int)HwProfileFieldIndex.IsDefault, 0, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "Image", typeof(System.Byte[]), false, false, false, true,  (int)HwProfileFieldIndex.Image, 2147483647, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)HwProfileFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)HwProfileFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)HwProfileFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "Key", typeof(System.String), false, false, false, true,  (int)HwProfileFieldIndex.Key, 200, 0, 0);
			this.AddElementFieldInfo("HwProfileEntity", "DefaultMinReading", typeof(Nullable<System.Int32>), false, false, false, true,  (int)HwProfileFieldIndex.DefaultMinReading, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "DefaultDisconnectedTimeout", typeof(Nullable<System.Int32>), false, false, false, true,  (int)HwProfileFieldIndex.DefaultDisconnectedTimeout, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "DefaultStabilityTimeout", typeof(Nullable<System.Int32>), false, false, false, true,  (int)HwProfileFieldIndex.DefaultStabilityTimeout, 0, 0, 10);
			this.AddElementFieldInfo("HwProfileEntity", "DefaultStabilityRange", typeof(Nullable<System.Int32>), false, false, false, true,  (int)HwProfileFieldIndex.DefaultStabilityRange, 0, 0, 10);
		}
		/// <summary>Inits ImageEntity's FieldInfo objects</summary>
		private void InitImageEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ImageFieldIndex), "ImageEntity");
			this.AddElementFieldInfo("ImageEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ImageFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ImageEntity", "Data", typeof(System.Byte[]), false, false, false, true,  (int)ImageFieldIndex.Data, 2147483647, 0, 0);
			this.AddElementFieldInfo("ImageEntity", "Extension", typeof(System.String), false, false, false, false,  (int)ImageFieldIndex.Extension, 4, 0, 0);
			this.AddElementFieldInfo("ImageEntity", "Path", typeof(System.String), false, false, false, true,  (int)ImageFieldIndex.Path, 2147483647, 0, 0);
			this.AddElementFieldInfo("ImageEntity", "Size", typeof(Nullable<System.Double>), false, false, false, true,  (int)ImageFieldIndex.Size, 0, 0, 38);
			this.AddElementFieldInfo("ImageEntity", "OldImageBoxWidth", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ImageFieldIndex.OldImageBoxWidth, 0, 0, 10);
			this.AddElementFieldInfo("ImageEntity", "OldImageBoxHeight", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ImageFieldIndex.OldImageBoxHeight, 0, 0, 10);
			this.AddElementFieldInfo("ImageEntity", "Description", typeof(System.String), false, false, false, true,  (int)ImageFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("ImageEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ImageFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ImageEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ImageFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ImageEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ImageFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits InvoiceEntity's FieldInfo objects</summary>
		private void InitInvoiceEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(InvoiceFieldIndex), "InvoiceEntity");
			this.AddElementFieldInfo("InvoiceEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)InvoiceFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("InvoiceEntity", "Number", typeof(System.String), false, false, false, true,  (int)InvoiceFieldIndex.Number, 2147483647, 0, 0);
			this.AddElementFieldInfo("InvoiceEntity", "TestId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)InvoiceFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("InvoiceEntity", "Comments", typeof(System.String), false, false, false, true,  (int)InvoiceFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("InvoiceEntity", "TotalAmount", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)InvoiceFieldIndex.TotalAmount, 0, 4, 12);
			this.AddElementFieldInfo("InvoiceEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)InvoiceFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("InvoiceEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)InvoiceFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("InvoiceEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)InvoiceFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("InvoiceEntity", "ChequeNumber", typeof(System.String), false, false, false, true,  (int)InvoiceFieldIndex.ChequeNumber, 200, 0, 0);
			this.AddElementFieldInfo("InvoiceEntity", "PaymentMethod", typeof(System.String), false, false, false, true,  (int)InvoiceFieldIndex.PaymentMethod, 200, 0, 0);
		}
		/// <summary>Inits IssueNavigationStepEntity's FieldInfo objects</summary>
		private void InitIssueNavigationStepEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(IssueNavigationStepFieldIndex), "IssueNavigationStepEntity");
			this.AddElementFieldInfo("IssueNavigationStepEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)IssueNavigationStepFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "TestIssueId", typeof(System.Int32), false, true, false, false,  (int)IssueNavigationStepFieldIndex.TestIssueId, 0, 0, 10);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)IssueNavigationStepFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "ParentId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)IssueNavigationStepFieldIndex.ParentId, 0, 0, 10);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)IssueNavigationStepFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)IssueNavigationStepFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)IssueNavigationStepFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("IssueNavigationStepEntity", "UserId", typeof(System.Int32), false, false, false, false,  (int)IssueNavigationStepFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits ItemEntity's FieldInfo objects</summary>
		private void InitItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ItemFieldIndex), "ItemEntity");
			this.AddElementFieldInfo("ItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "Name", typeof(System.String), false, false, false, false,  (int)ItemFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "FullName", typeof(System.String), false, false, false, true,  (int)ItemFieldIndex.FullName, 2147483647, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "Description", typeof(System.String), false, false, false, true,  (int)ItemFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "ItemMemo", typeof(System.String), false, false, false, true,  (int)ItemFieldIndex.ItemMemo, 2147483647, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "GenderLookupId", typeof(System.Int32), false, true, false, false,  (int)ItemFieldIndex.GenderLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "TypeLookupId", typeof(System.Int32), false, true, false, false,  (int)ItemFieldIndex.TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "ListTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)ItemFieldIndex.ListTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "ItemDetailId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ItemFieldIndex.ItemDetailId, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "Order", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ItemFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "ItemCsabinaryCode", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemFieldIndex.ItemCsabinaryCode, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "IsStarred", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)ItemFieldIndex.IsStarred, 0, 0, 0);
			this.AddElementFieldInfo("ItemEntity", "ItemSourceLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ItemFieldIndex.ItemSourceLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ItemEntity", "Key", typeof(System.String), false, false, false, true,  (int)ItemFieldIndex.Key, 100, 0, 0);
		}
		/// <summary>Inits ItemDetailsEntity's FieldInfo objects</summary>
		private void InitItemDetailsEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ItemDetailsFieldIndex), "ItemDetailsEntity");
			this.AddElementFieldInfo("ItemDetailsEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ItemDetailsFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ItemDetailsEntity", "ImageId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ItemDetailsFieldIndex.ImageId, 0, 0, 10);
			this.AddElementFieldInfo("ItemDetailsEntity", "X", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemDetailsFieldIndex.X, 0, 0, 10);
			this.AddElementFieldInfo("ItemDetailsEntity", "Y", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemDetailsFieldIndex.Y, 0, 0, 10);
			this.AddElementFieldInfo("ItemDetailsEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ItemDetailsFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ItemDetailsEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemDetailsFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ItemDetailsEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemDetailsFieldIndex.CreationDateTime, 0, 0, 0);
		}
		/// <summary>Inits ItemPropertyEntity's FieldInfo objects</summary>
		private void InitItemPropertyEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ItemPropertyFieldIndex), "ItemPropertyEntity");
			this.AddElementFieldInfo("ItemPropertyEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ItemPropertyFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ItemPropertyEntity", "PropertyId", typeof(System.Int32), false, true, false, false,  (int)ItemPropertyFieldIndex.PropertyId, 0, 0, 10);
			this.AddElementFieldInfo("ItemPropertyEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)ItemPropertyFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("ItemPropertyEntity", "Value", typeof(System.String), false, false, false, true,  (int)ItemPropertyFieldIndex.Value, 2147483647, 0, 0);
		}
		/// <summary>Inits ItemRelationEntity's FieldInfo objects</summary>
		private void InitItemRelationEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ItemRelationFieldIndex), "ItemRelationEntity");
			this.AddElementFieldInfo("ItemRelationEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ItemRelationFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "ItemParentId", typeof(System.Int32), false, true, false, false,  (int)ItemRelationFieldIndex.ItemParentId, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "ItemChildId", typeof(System.Int32), false, true, false, false,  (int)ItemRelationFieldIndex.ItemChildId, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "RelationTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)ItemRelationFieldIndex.RelationTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "Order", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemRelationFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "Step", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemRelationFieldIndex.Step, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "UserId", typeof(System.Int32), false, false, false, false,  (int)ItemRelationFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemRelationFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ItemRelationEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemRelationFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits ItemRelationPropertyEntity's FieldInfo objects</summary>
		private void InitItemRelationPropertyEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ItemRelationPropertyFieldIndex), "ItemRelationPropertyEntity");
			this.AddElementFieldInfo("ItemRelationPropertyEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ItemRelationPropertyFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationPropertyEntity", "PropertyId", typeof(System.Int32), false, true, false, false,  (int)ItemRelationPropertyFieldIndex.PropertyId, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationPropertyEntity", "ItemRelationId", typeof(System.Int32), false, true, false, false,  (int)ItemRelationPropertyFieldIndex.ItemRelationId, 0, 0, 10);
			this.AddElementFieldInfo("ItemRelationPropertyEntity", "Value", typeof(System.String), false, false, false, true,  (int)ItemRelationPropertyFieldIndex.Value, 2147483647, 0, 0);
		}
		/// <summary>Inits ItemTargetEntity's FieldInfo objects</summary>
		private void InitItemTargetEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ItemTargetFieldIndex), "ItemTargetEntity");
			this.AddElementFieldInfo("ItemTargetEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ItemTargetFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ItemTargetEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)ItemTargetFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("ItemTargetEntity", "TargetTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)ItemTargetFieldIndex.TargetTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ItemTargetEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ItemTargetFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ItemTargetEntity", "ItemTargetCreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemTargetFieldIndex.ItemTargetCreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ItemTargetEntity", "ItemTargetUpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ItemTargetFieldIndex.ItemTargetUpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ItemTargetEntity", "Order", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ItemTargetFieldIndex.Order, 0, 0, 10);
		}
		/// <summary>Inits LookupEntity's FieldInfo objects</summary>
		private void InitLookupEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(LookupFieldIndex), "LookupEntity");
			this.AddElementFieldInfo("LookupEntity", "Id", typeof(System.Int32), true, false, false, false,  (int)LookupFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("LookupEntity", "Value", typeof(System.String), false, false, false, false,  (int)LookupFieldIndex.Value, 50, 0, 0);
			this.AddElementFieldInfo("LookupEntity", "Type", typeof(System.String), false, false, false, false,  (int)LookupFieldIndex.Type, 50, 0, 0);
			this.AddElementFieldInfo("LookupEntity", "Key", typeof(System.String), false, false, false, true,  (int)LookupFieldIndex.Key, 250, 0, 0);
		}
		/// <summary>Inits OrderItemEntity's FieldInfo objects</summary>
		private void InitOrderItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(OrderItemFieldIndex), "OrderItemEntity");
			this.AddElementFieldInfo("OrderItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)OrderItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("OrderItemEntity", "ShippingOrderId", typeof(System.Int32), false, true, false, false,  (int)OrderItemFieldIndex.ShippingOrderId, 0, 0, 10);
			this.AddElementFieldInfo("OrderItemEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)OrderItemFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("OrderItemEntity", "Quantity", typeof(Nullable<System.Int32>), false, false, false, true,  (int)OrderItemFieldIndex.Quantity, 0, 0, 10);
			this.AddElementFieldInfo("OrderItemEntity", "Comments", typeof(System.String), false, false, false, true,  (int)OrderItemFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("OrderItemEntity", "Include", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)OrderItemFieldIndex.Include, 0, 0, 0);
			this.AddElementFieldInfo("OrderItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)OrderItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("OrderItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)OrderItemFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("OrderItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)OrderItemFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits PatientEntity's FieldInfo objects</summary>
		private void InitPatientEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(PatientFieldIndex), "PatientEntity");
			this.AddElementFieldInfo("PatientEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)PatientFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("PatientEntity", "Number", typeof(Nullable<System.Int32>), false, false, false, true,  (int)PatientFieldIndex.Number, 0, 0, 10);
			this.AddElementFieldInfo("PatientEntity", "FirstName", typeof(System.String), false, false, false, false,  (int)PatientFieldIndex.FirstName, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "LastName", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.LastName, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "Address1", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.Address1, 2147483647, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "Address2", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.Address2, 2147483647, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "City", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.City, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "State", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.State, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "Zip", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.Zip, 10, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "GenderLookupId", typeof(System.Int32), false, true, false, false,  (int)PatientFieldIndex.GenderLookupId, 0, 0, 10);
			this.AddElementFieldInfo("PatientEntity", "DateOfBirth", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)PatientFieldIndex.DateOfBirth, 0, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "HomePhone", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.HomePhone, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "WorkPhone", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.WorkPhone, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "CellPhone", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.CellPhone, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "Fax", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.Fax, 50, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "Email", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.Email, 2147483647, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "Notes", typeof(System.String), false, false, false, true,  (int)PatientFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)PatientFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("PatientEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)PatientFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("PatientEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)PatientFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits PatientHistoryEntity's FieldInfo objects</summary>
		private void InitPatientHistoryEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(PatientHistoryFieldIndex), "PatientHistoryEntity");
			this.AddElementFieldInfo("PatientHistoryEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)PatientHistoryFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("PatientHistoryEntity", "PatientId", typeof(System.Int32), false, true, false, false,  (int)PatientHistoryFieldIndex.PatientId, 0, 0, 10);
			this.AddElementFieldInfo("PatientHistoryEntity", "Name", typeof(System.String), false, false, false, false,  (int)PatientHistoryFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("PatientHistoryEntity", "Description", typeof(System.String), false, false, false, true,  (int)PatientHistoryFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("PatientHistoryEntity", "TypeLookupId", typeof(System.Int32), false, true, false, false,  (int)PatientHistoryFieldIndex.TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("PatientHistoryEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)PatientHistoryFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("PatientHistoryEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)PatientHistoryFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("PatientHistoryEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)PatientHistoryFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits ProductEntity's FieldInfo objects</summary>
		private void InitProductEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ProductFieldIndex), "ProductEntity");
			this.AddElementFieldInfo("ProductEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ProductFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ProductEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ProductFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ProductEntity", "AutoItemsId", typeof(System.Int32), false, true, false, false,  (int)ProductFieldIndex.AutoItemsId, 0, 0, 10);
			this.AddElementFieldInfo("ProductEntity", "Supplier", typeof(System.String), false, false, false, false,  (int)ProductFieldIndex.Supplier, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductEntity", "IngredientsString", typeof(System.String), false, false, false, true,  (int)ProductFieldIndex.IngredientsString, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductEntity", "Supports", typeof(System.String), false, false, false, true,  (int)ProductFieldIndex.Supports, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductEntity", "UsefulFor", typeof(System.String), false, false, false, true,  (int)ProductFieldIndex.UsefulFor, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductEntity", "Price", typeof(System.Decimal), false, false, false, false,  (int)ProductFieldIndex.Price, 0, 4, 19);
			this.AddElementFieldInfo("ProductEntity", "DiscountPercentage", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)ProductFieldIndex.DiscountPercentage, 0, 4, 19);
			this.AddElementFieldInfo("ProductEntity", "HasDiscount", typeof(System.Boolean), false, false, false, false,  (int)ProductFieldIndex.HasDiscount, 0, 0, 0);
			this.AddElementFieldInfo("ProductEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProductFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProductEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProductFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits ProductFormEntity's FieldInfo objects</summary>
		private void InitProductFormEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ProductFormFieldIndex), "ProductFormEntity");
			this.AddElementFieldInfo("ProductFormEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ProductFormFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ProductFormEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ProductFormFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ProductFormEntity", "ProductsId", typeof(System.Int32), false, true, false, false,  (int)ProductFormFieldIndex.ProductsId, 0, 0, 10);
			this.AddElementFieldInfo("ProductFormEntity", "StatusLookupId", typeof(System.Int32), false, true, false, false,  (int)ProductFormFieldIndex.StatusLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ProductFormEntity", "Form", typeof(System.String), false, false, false, false,  (int)ProductFormFieldIndex.Form, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductFormEntity", "SuggestedUsage", typeof(System.String), false, false, false, true,  (int)ProductFormFieldIndex.SuggestedUsage, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductFormEntity", "UsageSchedule", typeof(System.String), false, false, false, true,  (int)ProductFormFieldIndex.UsageSchedule, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductFormEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProductFormFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProductFormEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProductFormFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits ProductSizeEntity's FieldInfo objects</summary>
		private void InitProductSizeEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ProductSizeFieldIndex), "ProductSizeEntity");
			this.AddElementFieldInfo("ProductSizeEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ProductSizeFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ProductSizeEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ProductSizeFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ProductSizeEntity", "ProductFormsId", typeof(System.Int32), false, true, false, false,  (int)ProductSizeFieldIndex.ProductFormsId, 0, 0, 10);
			this.AddElementFieldInfo("ProductSizeEntity", "StatusLookupsId", typeof(System.Int32), false, true, false, false,  (int)ProductSizeFieldIndex.StatusLookupsId, 0, 0, 10);
			this.AddElementFieldInfo("ProductSizeEntity", "Size", typeof(System.String), false, false, false, false,  (int)ProductSizeFieldIndex.Size, 2147483647, 0, 0);
			this.AddElementFieldInfo("ProductSizeEntity", "Price", typeof(System.Decimal), false, false, false, false,  (int)ProductSizeFieldIndex.Price, 0, 4, 19);
			this.AddElementFieldInfo("ProductSizeEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProductSizeFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProductSizeEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProductSizeFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits PropertyEntity's FieldInfo objects</summary>
		private void InitPropertyEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(PropertyFieldIndex), "PropertyEntity");
			this.AddElementFieldInfo("PropertyEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)PropertyFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("PropertyEntity", "Name", typeof(System.String), false, false, false, false,  (int)PropertyFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("PropertyEntity", "Key", typeof(System.String), false, false, false, false,  (int)PropertyFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("PropertyEntity", "Description", typeof(System.String), false, false, false, true,  (int)PropertyFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("PropertyEntity", "ApplicableTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)PropertyFieldIndex.ApplicableTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("PropertyEntity", "ValueTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)PropertyFieldIndex.ValueTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("PropertyEntity", "SourceConfig", typeof(System.String), false, false, false, true,  (int)PropertyFieldIndex.SourceConfig, 2147483647, 0, 0);
			this.AddElementFieldInfo("PropertyEntity", "MembersConfig", typeof(System.String), false, false, false, true,  (int)PropertyFieldIndex.MembersConfig, 2147483647, 0, 0);
			this.AddElementFieldInfo("PropertyEntity", "Caption", typeof(System.String), false, false, false, true,  (int)PropertyFieldIndex.Caption, 100, 0, 0);
		}
		/// <summary>Inits ProtocolItemEntity's FieldInfo objects</summary>
		private void InitProtocolItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ProtocolItemFieldIndex), "ProtocolItemEntity");
			this.AddElementFieldInfo("ProtocolItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ProtocolItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolItemEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)ProtocolItemFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolItemEntity", "TestProtocolId", typeof(System.Int32), false, true, false, false,  (int)ProtocolItemFieldIndex.TestProtocolId, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProtocolItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProtocolItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProtocolItemFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProtocolItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ProtocolItemFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits ProtocolStepEntity's FieldInfo objects</summary>
		private void InitProtocolStepEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ProtocolStepFieldIndex), "ProtocolStepEntity");
			this.AddElementFieldInfo("ProtocolStepEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ProtocolStepFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolStepEntity", "TestProtocolId", typeof(System.Int32), false, true, false, false,  (int)ProtocolStepFieldIndex.TestProtocolId, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolStepEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)ProtocolStepFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolStepEntity", "TypeLookupId", typeof(System.Int32), false, true, false, false,  (int)ProtocolStepFieldIndex.TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ProtocolStepEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProtocolStepFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProtocolStepEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ProtocolStepFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ProtocolStepEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ProtocolStepFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits ReadingEntity's FieldInfo objects</summary>
		private void InitReadingEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ReadingFieldIndex), "ReadingEntity");
			this.AddElementFieldInfo("ReadingEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ReadingFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "TestId", typeof(System.Int32), false, true, false, false,  (int)ReadingFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "DateTime", typeof(System.DateTime), false, false, false, false,  (int)ReadingFieldIndex.DateTime, 0, 0, 0);
			this.AddElementFieldInfo("ReadingEntity", "ItemId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ReadingFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "Value", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ReadingFieldIndex.Value, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "Min", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ReadingFieldIndex.Min, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "Max", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ReadingFieldIndex.Max, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "Rise", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ReadingFieldIndex.Rise, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "Fall", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ReadingFieldIndex.Fall, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "ValueBalanced", typeof(Nullable<System.Int32>), false, false, false, true,  (int)ReadingFieldIndex.ValueBalanced, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "ListPointLookupId", typeof(System.Int32), false, true, false, false,  (int)ReadingFieldIndex.ListPointLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ReadingFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ReadingEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ReadingFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ReadingEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ReadingFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ReadingEntity", "PointSetItemId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ReadingFieldIndex.PointSetItemId, 0, 0, 10);
		}
		/// <summary>Inits ScheduleLineEntity's FieldInfo objects</summary>
		private void InitScheduleLineEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ScheduleLineFieldIndex), "ScheduleLineEntity");
			this.AddElementFieldInfo("ScheduleLineEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ScheduleLineFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ScheduleLineEntity", "TestScheduleId", typeof(System.Int32), false, true, false, false,  (int)ScheduleLineFieldIndex.TestScheduleId, 0, 0, 10);
			this.AddElementFieldInfo("ScheduleLineEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)ScheduleLineFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("ScheduleLineEntity", "Notes", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "Duration", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.Duration, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "ToBeShipped", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.ToBeShipped, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "WhenArising", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.WhenArising, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "Breakfast", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.Breakfast, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "BetweenMealsEarly", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.BetweenMealsEarly, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "Lunch", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.Lunch, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "BetweenMealsLate", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.BetweenMealsLate, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "Dinner", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.Dinner, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "BeforeSleep", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.BeforeSleep, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "NoPerBottle", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.NoPerBottle, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "NoOfBottle", typeof(System.String), false, false, false, true,  (int)ScheduleLineFieldIndex.NoOfBottle, 200, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "Price", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)ScheduleLineFieldIndex.Price, 0, 4, 12);
			this.AddElementFieldInfo("ScheduleLineEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ScheduleLineFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ScheduleLineFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ScheduleLineEntity", "UserId", typeof(System.Int32), false, false, false, false,  (int)ScheduleLineFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ScheduleLineEntity", "IsDeleted", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)ScheduleLineFieldIndex.IsDeleted, 0, 0, 0);
		}
		/// <summary>Inits ServiceEntity's FieldInfo objects</summary>
		private void InitServiceEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ServiceFieldIndex), "ServiceEntity");
			this.AddElementFieldInfo("ServiceEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ServiceFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ServiceEntity", "Key", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "Name", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "Description", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "Comments", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "Price", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)ServiceFieldIndex.Price, 0, 4, 12);
			this.AddElementFieldInfo("ServiceEntity", "IsDefault", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)ServiceFieldIndex.IsDefault, 0, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "TypeLookupId", typeof(System.Int32), false, true, false, false,  (int)ServiceFieldIndex.TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("ServiceEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ServiceFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ServiceEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ServiceFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ServiceFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "DefaultName", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.DefaultName, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "DefaultDescription", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.DefaultDescription, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "DefaultComments", typeof(System.String), false, false, false, true,  (int)ServiceFieldIndex.DefaultComments, 2147483647, 0, 0);
			this.AddElementFieldInfo("ServiceEntity", "DefaultPrice", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)ServiceFieldIndex.DefaultPrice, 0, 4, 12);
			this.AddElementFieldInfo("ServiceEntity", "DefaultIsDefault", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)ServiceFieldIndex.DefaultIsDefault, 0, 0, 0);
		}
		/// <summary>Inits SettingEntity's FieldInfo objects</summary>
		private void InitSettingEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(SettingFieldIndex), "SettingEntity");
			this.AddElementFieldInfo("SettingEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)SettingFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("SettingEntity", "Name", typeof(System.String), false, false, false, false,  (int)SettingFieldIndex.Name, 100, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "Key", typeof(System.String), false, false, false, false,  (int)SettingFieldIndex.Key, 100, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "Value", typeof(System.String), false, false, false, false,  (int)SettingFieldIndex.Value, 2147483647, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "Description", typeof(System.String), false, false, false, true,  (int)SettingFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "ValueTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)SettingFieldIndex.ValueTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("SettingEntity", "SettingGroupLookupId", typeof(System.Int32), false, true, false, false,  (int)SettingFieldIndex.SettingGroupLookupId, 0, 0, 10);
			this.AddElementFieldInfo("SettingEntity", "SourceConfig", typeof(System.String), false, false, false, false,  (int)SettingFieldIndex.SourceConfig, 2147483647, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "MembersConfig", typeof(System.String), false, false, false, true,  (int)SettingFieldIndex.MembersConfig, 2147483647, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "Caption", typeof(System.String), false, false, false, true,  (int)SettingFieldIndex.Caption, 100, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "DefaultValue", typeof(System.String), false, false, false, false,  (int)SettingFieldIndex.DefaultValue, 2147483647, 0, 0);
			this.AddElementFieldInfo("SettingEntity", "IsVisible", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)SettingFieldIndex.IsVisible, 0, 0, 0);
		}
		/// <summary>Inits ShippingOrderEntity's FieldInfo objects</summary>
		private void InitShippingOrderEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(ShippingOrderFieldIndex), "ShippingOrderEntity");
			this.AddElementFieldInfo("ShippingOrderEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)ShippingOrderFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("ShippingOrderEntity", "Number", typeof(System.String), false, false, false, false,  (int)ShippingOrderFieldIndex.Number, 2147483647, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TestId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ShippingOrderFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("ShippingOrderEntity", "SentDate", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)ShippingOrderFieldIndex.SentDate, 0, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "SendToClient", typeof(System.Boolean), false, false, false, false,  (int)ShippingOrderFieldIndex.SendToClient, 0, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "Sent", typeof(System.Boolean), false, false, false, false,  (int)ShippingOrderFieldIndex.Sent, 0, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "Comments", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.Comments, 1073741823, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientFirstName", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientFirstName, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientLastName", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientLastName, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientAddress1", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientAddress1, 2147483647, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientAddress2", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientAddress2, 2147483647, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientCity", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientCity, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientState", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientState, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientZip", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientZip, 10, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientHomePhone", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientHomePhone, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientWorkPhone", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientWorkPhone, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientCellPhone", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientCellPhone, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientFax", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientFax, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "PatientEmail", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.PatientEmail, 2147483647, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TechnicianName", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.TechnicianName, 100, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TechnicianAddress", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.TechnicianAddress, 2147483647, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TechnicianState", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.TechnicianState, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TechnicianZipCode", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.TechnicianZipCode, 10, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TechnicianCity", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.TechnicianCity, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "TechnicianPhone", typeof(System.String), false, false, false, true,  (int)ShippingOrderFieldIndex.TechnicianPhone, 50, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)ShippingOrderFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)ShippingOrderFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("ShippingOrderEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)ShippingOrderFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("ShippingOrderEntity", "ShippingMethodLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)ShippingOrderFieldIndex.ShippingMethodLookupId, 0, 0, 10);
		}
		/// <summary>Inits SpotCheckEntity's FieldInfo objects</summary>
		private void InitSpotCheckEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(SpotCheckFieldIndex), "SpotCheckEntity");
			this.AddElementFieldInfo("SpotCheckEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)SpotCheckFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "PatientId", typeof(System.Int32), false, true, false, false,  (int)SpotCheckFieldIndex.PatientId, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "Notes", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "Name", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.Name, 200, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)SpotCheckFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)SpotCheckFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)SpotCheckFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "CapsoleTnotes", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.CapsoleTnotes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "IngredientsNotes", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.IngredientsNotes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "MondayNotes", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.MondayNotes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "TuesdayNotes", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.TuesdayNotes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "WednesdayNotes", typeof(System.String), false, false, false, true,  (int)SpotCheckFieldIndex.WednesdayNotes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsThree", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsThree, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsOne", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsOne, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsIvPush", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsIvPush, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsSterlieWaterCc", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsSterlieWaterCc, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsSterlieWaterCcpriority", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsSterlieWaterCcpriority, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsDextroseCc", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsDextroseCc, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsDextroseCcpriority", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsDextroseCcpriority, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsNormalSalineCc", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsNormalSalineCc, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsNormalSalineCcpriority", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsNormalSalineCcpriority, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsIvperMin", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsIvperMin, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsPerWeek", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsPerWeek, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "MineralsEdta", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.MineralsEdta, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "TestId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)SpotCheckFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "IngredientsNumberOfBags", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.IngredientsNumberOfBags, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckEntity", "IngredientsNumberPerWeek", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckFieldIndex.IngredientsNumberPerWeek, 0, 0, 10);
		}
		/// <summary>Inits SpotCheckResultEntity's FieldInfo objects</summary>
		private void InitSpotCheckResultEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(SpotCheckResultFieldIndex), "SpotCheckResultEntity");
			this.AddElementFieldInfo("SpotCheckResultEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)SpotCheckResultFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "SpotCheckId", typeof(System.Int32), false, true, false, false,  (int)SpotCheckResultFieldIndex.SpotCheckId, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "ItemId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)SpotCheckResultFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)SpotCheckResultFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckResultEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)SpotCheckResultFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckResultEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)SpotCheckResultFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "YesNo", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)SpotCheckResultFieldIndex.YesNo, 0, 0, 0);
			this.AddElementFieldInfo("SpotCheckResultEntity", "NumberOfBags", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckResultFieldIndex.NumberOfBags, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "NumberOfWeeks", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckResultFieldIndex.NumberOfWeeks, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "Dosage", typeof(Nullable<System.Int32>), false, false, false, true,  (int)SpotCheckResultFieldIndex.Dosage, 0, 0, 10);
			this.AddElementFieldInfo("SpotCheckResultEntity", "Notes", typeof(System.String), false, false, false, true,  (int)SpotCheckResultFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("SpotCheckResultEntity", "ResultTypeId", typeof(System.Int32), false, true, false, false,  (int)SpotCheckResultFieldIndex.ResultTypeId, 0, 0, 10);
		}
		/// <summary>Inits StageAnnouncementEntity's FieldInfo objects</summary>
		private void InitStageAnnouncementEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(StageAnnouncementFieldIndex), "StageAnnouncementEntity");
			this.AddElementFieldInfo("StageAnnouncementEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)StageAnnouncementFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("StageAnnouncementEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)StageAnnouncementFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("StageAnnouncementEntity", "Key", typeof(System.String), false, false, false, false,  (int)StageAnnouncementFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("StageAnnouncementEntity", "Text", typeof(System.String), false, false, false, false,  (int)StageAnnouncementFieldIndex.Text, 2147483647, 0, 0);
			this.AddElementFieldInfo("StageAnnouncementEntity", "AudioPath", typeof(System.String), false, false, false, true,  (int)StageAnnouncementFieldIndex.AudioPath, 2147483647, 0, 0);
			this.AddElementFieldInfo("StageAnnouncementEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)StageAnnouncementFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("StageAnnouncementEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)StageAnnouncementFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits StageAutoItemEntity's FieldInfo objects</summary>
		private void InitStageAutoItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(StageAutoItemFieldIndex), "StageAutoItemEntity");
			this.AddElementFieldInfo("StageAutoItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)StageAutoItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)StageAutoItemFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "AutoProtocolStagesId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)StageAutoItemFieldIndex.AutoProtocolStagesId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "StageAutoItemParentId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)StageAutoItemFieldIndex.StageAutoItemParentId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "AutoItemsId", typeof(System.Int32), false, true, false, false,  (int)StageAutoItemFieldIndex.AutoItemsId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "ScanningMethodLookupId", typeof(System.Int32), false, true, false, false,  (int)StageAutoItemFieldIndex.ScanningMethodLookupId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "ChildsOrderTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)StageAutoItemFieldIndex.ChildsOrderTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "ChildsScanningTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)StageAutoItemFieldIndex.ChildsScanningTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "Order", typeof(System.Int32), false, false, false, false,  (int)StageAutoItemFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "ScansNumber", typeof(System.Int32), false, false, false, false,  (int)StageAutoItemFieldIndex.ScansNumber, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)StageAutoItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("StageAutoItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)StageAutoItemFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("StageAutoItemEntity", "MatchesNumber", typeof(System.Int32), false, false, false, false,  (int)StageAutoItemFieldIndex.MatchesNumber, 0, 0, 10);
			this.AddElementFieldInfo("StageAutoItemEntity", "FinishAllScanRounds", typeof(System.Boolean), false, false, false, false,  (int)StageAutoItemFieldIndex.FinishAllScanRounds, 0, 0, 0);
			this.AddElementFieldInfo("StageAutoItemEntity", "DirectAccessChecks", typeof(System.String), false, false, false, true,  (int)StageAutoItemFieldIndex.DirectAccessChecks, 2147483647, 0, 0);
			this.AddElementFieldInfo("StageAutoItemEntity", "TestingPointsId", typeof(System.Int32), false, true, false, false,  (int)StageAutoItemFieldIndex.TestingPointsId, 0, 0, 10);
		}
		/// <summary>Inits TestEntity's FieldInfo objects</summary>
		private void InitTestEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestFieldIndex), "TestEntity");
			this.AddElementFieldInfo("TestEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "PatientId", typeof(System.Int32), false, true, false, false,  (int)TestFieldIndex.PatientId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "TestScheduleId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestFieldIndex.TestScheduleId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "Name", typeof(System.String), false, false, false, true,  (int)TestFieldIndex.Name, 50, 0, 0);
			this.AddElementFieldInfo("TestEntity", "PointsGroupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestFieldIndex.PointsGroupId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "TestProtocolId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestFieldIndex.TestProtocolId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "DateTime", typeof(Nullable<System.DateTime>), false, false, false, true,  (int)TestFieldIndex.DateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestEntity", "Description", typeof(System.String), false, false, false, true,  (int)TestFieldIndex.Description, 50, 0, 0);
			this.AddElementFieldInfo("TestEntity", "TestTypeLookupId", typeof(System.Int32), false, true, false, false,  (int)TestFieldIndex.TestTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "ListPointLookupId", typeof(System.Int32), false, true, false, false,  (int)TestFieldIndex.ListPointLookupId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "TestStateLookupId", typeof(System.Int32), false, true, false, false,  (int)TestFieldIndex.TestStateLookupId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "Notes", typeof(System.String), false, false, false, true,  (int)TestFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestEntity", "NumberOfIssues", typeof(Nullable<System.Int32>), false, false, false, true,  (int)TestFieldIndex.NumberOfIssues, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestEntity", "EvalPeriodChecked", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestFieldIndex.EvalPeriodChecked, 0, 0, 0);
			this.AddElementFieldInfo("TestEntity", "IsOrderSent", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestFieldIndex.IsOrderSent, 0, 0, 0);
		}
		/// <summary>Inits TestImprintableItemEntity's FieldInfo objects</summary>
		private void InitTestImprintableItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestImprintableItemFieldIndex), "TestImprintableItemEntity");
			this.AddElementFieldInfo("TestImprintableItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestImprintableItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "TestId", typeof(System.Int32), false, true, false, false,  (int)TestImprintableItemFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)TestImprintableItemFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "ParentImprintableId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestImprintableItemFieldIndex.ParentImprintableId, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "TestResultId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestImprintableItemFieldIndex.TestResultId, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "IsChecked", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestImprintableItemFieldIndex.IsChecked, 0, 0, 0);
			this.AddElementFieldInfo("TestImprintableItemEntity", "IsImprinted", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestImprintableItemFieldIndex.IsImprinted, 0, 0, 0);
			this.AddElementFieldInfo("TestImprintableItemEntity", "Order", typeof(Nullable<System.Int32>), false, false, false, true,  (int)TestImprintableItemFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "Comments", typeof(System.String), false, false, false, true,  (int)TestImprintableItemFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestImprintableItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestImprintableItemFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestImprintableItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestImprintableItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestImprintableItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestImprintableItemFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits TestingPointEntity's FieldInfo objects</summary>
		private void InitTestingPointEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestingPointFieldIndex), "TestingPointEntity");
			this.AddElementFieldInfo("TestingPointEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestingPointFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestingPointEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestingPointFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestingPointEntity", "Key", typeof(System.String), false, false, false, false,  (int)TestingPointFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestingPointEntity", "Name", typeof(System.String), false, false, false, false,  (int)TestingPointFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestingPointEntity", "FullName", typeof(System.String), false, false, false, true,  (int)TestingPointFieldIndex.FullName, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestingPointEntity", "Hwidentifier", typeof(System.String), false, false, false, false,  (int)TestingPointFieldIndex.Hwidentifier, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestingPointEntity", "Description", typeof(System.String), false, false, false, true,  (int)TestingPointFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestingPointEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestingPointFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestingPointEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestingPointFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits TestIssueEntity's FieldInfo objects</summary>
		private void InitTestIssueEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestIssueFieldIndex), "TestIssueEntity");
			this.AddElementFieldInfo("TestIssueEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestIssueFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestIssueEntity", "TestId", typeof(System.Int32), false, true, false, false,  (int)TestIssueFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("TestIssueEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)TestIssueFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("TestIssueEntity", "ProtocolStepId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestIssueFieldIndex.ProtocolStepId, 0, 0, 10);
			this.AddElementFieldInfo("TestIssueEntity", "Name", typeof(System.String), false, false, false, true,  (int)TestIssueFieldIndex.Name, 50, 0, 0);
			this.AddElementFieldInfo("TestIssueEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestIssueFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestIssueEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestIssueFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestIssueEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestIssueFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestIssueEntity", "IsMainIssue", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestIssueFieldIndex.IsMainIssue, 0, 0, 0);
		}
		/// <summary>Inits TestProtocolEntity's FieldInfo objects</summary>
		private void InitTestProtocolEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestProtocolFieldIndex), "TestProtocolEntity");
			this.AddElementFieldInfo("TestProtocolEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestProtocolFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestProtocolEntity", "Name", typeof(System.String), false, false, false, false,  (int)TestProtocolFieldIndex.Name, 250, 0, 0);
			this.AddElementFieldInfo("TestProtocolEntity", "Description", typeof(System.String), false, false, false, true,  (int)TestProtocolFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestProtocolEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestProtocolFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestProtocolEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestProtocolFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestProtocolEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestProtocolFieldIndex.UserId, 0, 0, 10);
		}
		/// <summary>Inits TestResultEntity's FieldInfo objects</summary>
		private void InitTestResultEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestResultFieldIndex), "TestResultEntity");
			this.AddElementFieldInfo("TestResultEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestResultFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "IssueId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.IssueId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "ItemId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "DateTime", typeof(System.DateTime), false, false, false, false,  (int)TestResultFieldIndex.DateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestResultEntity", "ParentId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.ParentId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "SelectedParentId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.SelectedParentId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "VitalForceId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.VitalForceId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "StepTypeLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.StepTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "TestProtocolId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.TestProtocolId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "IsSelected", typeof(System.Boolean), false, false, false, false,  (int)TestResultFieldIndex.IsSelected, 0, 0, 0);
			this.AddElementFieldInfo("TestResultEntity", "IsCurrent", typeof(System.Boolean), false, false, false, false,  (int)TestResultFieldIndex.IsCurrent, 0, 0, 0);
			this.AddElementFieldInfo("TestResultEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestResultFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestResultFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestResultEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestResultFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestResultEntity", "IsImprinted", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestResultFieldIndex.IsImprinted, 0, 0, 0);
			this.AddElementFieldInfo("TestResultEntity", "ItemRatioId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFieldIndex.ItemRatioId, 0, 0, 10);
		}
		/// <summary>Inits TestResultFactorsEntity's FieldInfo objects</summary>
		private void InitTestResultFactorsEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestResultFactorsFieldIndex), "TestResultFactorsEntity");
			this.AddElementFieldInfo("TestResultFactorsEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestResultFactorsFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestResultFactorsEntity", "FactorItemId", typeof(System.Int32), false, true, false, false,  (int)TestResultFactorsFieldIndex.FactorItemId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultFactorsEntity", "TestResultId", typeof(System.Int32), false, true, false, false,  (int)TestResultFactorsFieldIndex.TestResultId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultFactorsEntity", "Reading", typeof(System.Int32), false, false, false, false,  (int)TestResultFactorsFieldIndex.Reading, 0, 0, 10);
			this.AddElementFieldInfo("TestResultFactorsEntity", "PotencyItemId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestResultFactorsFieldIndex.PotencyItemId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultFactorsEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestResultFactorsFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestResultFactorsEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestResultFactorsFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestResultFactorsEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestResultFactorsFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits TestScheduleEntity's FieldInfo objects</summary>
		private void InitTestScheduleEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestScheduleFieldIndex), "TestScheduleEntity");
			this.AddElementFieldInfo("TestScheduleEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestScheduleFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestScheduleEntity", "Tax", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)TestScheduleFieldIndex.Tax, 0, 4, 12);
			this.AddElementFieldInfo("TestScheduleEntity", "IsCash", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestScheduleFieldIndex.IsCash, 0, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "IsCheck", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestScheduleFieldIndex.IsCheck, 0, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "IsCreditCard", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestScheduleFieldIndex.IsCreditCard, 0, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "ReevalInWeeks", typeof(Nullable<System.Int32>), false, false, false, true,  (int)TestScheduleFieldIndex.ReevalInWeeks, 0, 0, 10);
			this.AddElementFieldInfo("TestScheduleEntity", "Notes", typeof(System.String), false, false, false, true,  (int)TestScheduleFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "SpecialInstructions", typeof(System.String), false, false, false, true,  (int)TestScheduleFieldIndex.SpecialInstructions, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "UserId", typeof(System.Int32), false, false, false, false,  (int)TestScheduleFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestScheduleEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestScheduleFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestScheduleFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "EvalPeriodTypeLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestScheduleFieldIndex.EvalPeriodTypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("TestScheduleEntity", "CheckNumber", typeof(System.String), false, false, false, true,  (int)TestScheduleFieldIndex.CheckNumber, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "Discount", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)TestScheduleFieldIndex.Discount, 0, 4, 12);
			this.AddElementFieldInfo("TestScheduleEntity", "DiscountAsPercentage", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)TestScheduleFieldIndex.DiscountAsPercentage, 0, 0, 0);
			this.AddElementFieldInfo("TestScheduleEntity", "DiscountApplyLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestScheduleFieldIndex.DiscountApplyLookupId, 0, 0, 10);
		}
		/// <summary>Inits TestServiceEntity's FieldInfo objects</summary>
		private void InitTestServiceEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(TestServiceFieldIndex), "TestServiceEntity");
			this.AddElementFieldInfo("TestServiceEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)TestServiceFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("TestServiceEntity", "TestId", typeof(System.Int32), false, true, false, false,  (int)TestServiceFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("TestServiceEntity", "ServiceId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)TestServiceFieldIndex.ServiceId, 0, 0, 10);
			this.AddElementFieldInfo("TestServiceEntity", "Key", typeof(System.String), false, false, false, true,  (int)TestServiceFieldIndex.Key, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestServiceEntity", "Name", typeof(System.String), false, false, false, true,  (int)TestServiceFieldIndex.Name, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestServiceEntity", "Description", typeof(System.String), false, false, false, true,  (int)TestServiceFieldIndex.Description, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestServiceEntity", "Comments", typeof(System.String), false, false, false, true,  (int)TestServiceFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("TestServiceEntity", "Price", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)TestServiceFieldIndex.Price, 0, 4, 12);
			this.AddElementFieldInfo("TestServiceEntity", "TypeLookupId", typeof(System.Int32), false, true, false, false,  (int)TestServiceFieldIndex.TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("TestServiceEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)TestServiceFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("TestServiceEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestServiceFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("TestServiceEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)TestServiceFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits UserEntity's FieldInfo objects</summary>
		private void InitUserEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(UserFieldIndex), "UserEntity");
			this.AddElementFieldInfo("UserEntity", "Id", typeof(System.Int32), true, false, false, false,  (int)UserFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("UserEntity", "Name", typeof(System.String), false, false, false, false,  (int)UserFieldIndex.Name, 50, 0, 0);
		}
		/// <summary>Inits VFSEntity's FieldInfo objects</summary>
		private void InitVFSEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(VFSFieldIndex), "VFSEntity");
			this.AddElementFieldInfo("VFSEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)VFSFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("VFSEntity", "Name", typeof(System.String), false, false, false, false,  (int)VFSFieldIndex.Name, 50, 0, 0);
			this.AddElementFieldInfo("VFSEntity", "TestId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSFieldIndex.TestId, 0, 0, 10);
			this.AddElementFieldInfo("VFSEntity", "DateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSFieldIndex.DateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSEntity", "ThyroidNumOfIssues", typeof(Nullable<System.Int32>), false, false, false, true,  (int)VFSFieldIndex.ThyroidNumOfIssues, 0, 0, 10);
			this.AddElementFieldInfo("VFSEntity", "MercuryNumOfIssues", typeof(Nullable<System.Int32>), false, false, false, true,  (int)VFSFieldIndex.MercuryNumOfIssues, 0, 0, 10);
			this.AddElementFieldInfo("VFSEntity", "EmotionalIssues", typeof(System.String), false, false, false, true,  (int)VFSFieldIndex.EmotionalIssues, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSEntity", "Notes", typeof(System.String), false, false, false, true,  (int)VFSFieldIndex.Notes, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)VFSFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("VFSEntity", "PatientId", typeof(System.Int32), false, true, false, false,  (int)VFSFieldIndex.PatientId, 0, 0, 10);
		}
		/// <summary>Inits VFSItemEntity's FieldInfo objects</summary>
		private void InitVFSItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(VFSItemFieldIndex), "VFSItemEntity");
			this.AddElementFieldInfo("VFSItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)VFSItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "VFSId", typeof(System.Int32), false, true, false, false,  (int)VFSItemFieldIndex.VFSId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "VFSitemSourceId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemFieldIndex.VFSitemSourceId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)VFSItemFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "PreviousV1", typeof(System.String), false, false, false, true,  (int)VFSItemFieldIndex.PreviousV1, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "PreviousV2", typeof(System.String), false, false, false, true,  (int)VFSItemFieldIndex.PreviousV2, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "CurrentV1", typeof(System.String), false, false, false, true,  (int)VFSItemFieldIndex.CurrentV1, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "CurrentV2", typeof(System.String), false, false, false, true,  (int)VFSItemFieldIndex.CurrentV2, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "IsSkipped", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSItemFieldIndex.IsSkipped, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "Comments", typeof(System.String), false, false, false, true,  (int)VFSItemFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)VFSItemFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSItemFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemEntity", "GroupLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemFieldIndex.GroupLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "SectionLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemFieldIndex.SectionLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemEntity", "GridGroupLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemFieldIndex.GridGroupLookupId, 0, 0, 10);
		}
		/// <summary>Inits VFSItemSourceEntity's FieldInfo objects</summary>
		private void InitVFSItemSourceEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(VFSItemSourceFieldIndex), "VFSItemSourceEntity");
			this.AddElementFieldInfo("VFSItemSourceEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)VFSItemSourceFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)VFSItemSourceFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V1Min", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V1Min, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V1Max", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V1Max, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V1MinIdeal", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V1MinIdeal, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V1MaxIdeal", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V1MaxIdeal, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V2Min", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V2Min, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V2Max", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V2Max, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V2MinIdeal", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V2MinIdeal, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V2MaxIdeal", typeof(Nullable<System.Decimal>), false, false, false, true,  (int)VFSItemSourceFieldIndex.V2MaxIdeal, 0, 4, 12);
			this.AddElementFieldInfo("VFSItemSourceEntity", "StartingValue1", typeof(System.String), false, false, false, true,  (int)VFSItemSourceFieldIndex.StartingValue1, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "StartingValue2", typeof(System.String), false, false, false, true,  (int)VFSItemSourceFieldIndex.StartingValue2, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "IsActive", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSItemSourceFieldIndex.IsActive, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V1TypeLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemSourceFieldIndex.V1TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V2TypeLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemSourceFieldIndex.V2TypeLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V1LookupType", typeof(System.String), false, false, false, true,  (int)VFSItemSourceFieldIndex.V1LookupType, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "V2LookupType", typeof(System.String), false, false, false, true,  (int)VFSItemSourceFieldIndex.V2LookupType, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "SectionLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemSourceFieldIndex.SectionLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "GroupLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemSourceFieldIndex.GroupLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "GenderLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemSourceFieldIndex.GenderLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "TestingOrder", typeof(Nullable<System.Int32>), false, false, false, true,  (int)VFSItemSourceFieldIndex.TestingOrder, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "HasPreviousV1", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSItemSourceFieldIndex.HasPreviousV1, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "HasPreviousV2", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSItemSourceFieldIndex.HasPreviousV2, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "HasCurrentV1", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSItemSourceFieldIndex.HasCurrentV1, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "HasCurrentV2", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSItemSourceFieldIndex.HasCurrentV2, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)VFSItemSourceFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("VFSItemSourceEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSItemSourceFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSItemSourceFieldIndex.UpdatedDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSItemSourceEntity", "GridGroupLookupId", typeof(Nullable<System.Int32>), false, true, false, true,  (int)VFSItemSourceFieldIndex.GridGroupLookupId, 0, 0, 10);
		}
		/// <summary>Inits VFSSecondaryItemEntity's FieldInfo objects</summary>
		private void InitVFSSecondaryItemEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(VFSSecondaryItemFieldIndex), "VFSSecondaryItemEntity");
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)VFSSecondaryItemFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "VfsId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemFieldIndex.VfsId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "SectionLookupId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemFieldIndex.SectionLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "Comments", typeof(System.String), false, false, false, true,  (int)VFSSecondaryItemFieldIndex.Comments, 2147483647, 0, 0);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "Checked", typeof(Nullable<System.Boolean>), false, false, false, true,  (int)VFSSecondaryItemFieldIndex.Checked, 0, 0, 0);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "Order", typeof(Nullable<System.Int32>), false, false, false, true,  (int)VFSSecondaryItemFieldIndex.Order, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSSecondaryItemFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSSecondaryItemEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSSecondaryItemFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		/// <summary>Inits VFSSecondaryItemSourceEntity's FieldInfo objects</summary>
		private void InitVFSSecondaryItemSourceEntityInfos()
		{
			this.AddFieldIndexEnumForElementName(typeof(VFSSecondaryItemSourceFieldIndex), "VFSSecondaryItemSourceEntity");
			this.AddElementFieldInfo("VFSSecondaryItemSourceEntity", "Id", typeof(System.Int32), true, false, true, false,  (int)VFSSecondaryItemSourceFieldIndex.Id, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemSourceEntity", "ItemId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemSourceFieldIndex.ItemId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemSourceEntity", "SectionLookupId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemSourceFieldIndex.SectionLookupId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemSourceEntity", "UserId", typeof(System.Int32), false, true, false, false,  (int)VFSSecondaryItemSourceFieldIndex.UserId, 0, 0, 10);
			this.AddElementFieldInfo("VFSSecondaryItemSourceEntity", "CreationDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSSecondaryItemSourceFieldIndex.CreationDateTime, 0, 0, 0);
			this.AddElementFieldInfo("VFSSecondaryItemSourceEntity", "UpdatedDateTime", typeof(System.DateTime), false, false, false, false,  (int)VFSSecondaryItemSourceFieldIndex.UpdatedDateTime, 0, 0, 0);
		}
		
	}
}




