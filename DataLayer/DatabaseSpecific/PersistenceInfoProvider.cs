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
using System.Collections;
using System.Data;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Vital.DataLayer.DatabaseSpecific
{
	/// <summary>Singleton implementation of the PersistenceInfoProvider. This class is the singleton wrapper through which the actual instance is retrieved.</summary>
	/// <remarks>It uses a single instance of an internal class. The access isn't marked with locks as the PersistenceInfoProviderBase class is threadsafe.</remarks>
	internal static class PersistenceInfoProviderSingleton
	{
		#region Class Member Declarations
		private static readonly IPersistenceInfoProvider _providerInstance = new PersistenceInfoProviderCore();
		#endregion

		/// <summary>Dummy static constructor to make sure threadsafe initialization is performed.</summary>
		static PersistenceInfoProviderSingleton()
		{
		}

		/// <summary>Gets the singleton instance of the PersistenceInfoProviderCore</summary>
		/// <returns>Instance of the PersistenceInfoProvider.</returns>
		public static IPersistenceInfoProvider GetInstance()
		{
			return _providerInstance;
		}
	}

	/// <summary>Actual implementation of the PersistenceInfoProvider. Used by singleton wrapper.</summary>
	internal class PersistenceInfoProviderCore : PersistenceInfoProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="PersistenceInfoProviderCore"/> class.</summary>
		internal PersistenceInfoProviderCore()
		{
			Init();
		}

		/// <summary>Method which initializes the internal datastores with the structure of hierarchical types.</summary>
		private void Init()
		{
			this.InitClass((60 + 0));
			InitAppImageEntityMappings();
			InitAppInfoEntityMappings();
			InitAutoItemEntityMappings();
			InitAutoItemRelationEntityMappings();
			InitAutoProtocolEntityMappings();
			InitAutoProtocolRevisionEntityMappings();
			InitAutoProtocolStageEntityMappings();
			InitAutoProtocolStageRevisionEntityMappings();
			InitAutoTestEntityMappings();
			InitAutoTestResultEntityMappings();
			InitAutoTestResultProductEntityMappings();
			InitAutoTestStageEntityMappings();
			InitClinicProductPricingEntityMappings();
			InitDosageOptionEntityMappings();
			InitFrequencyTestEntityMappings();
			InitFrequencyTestResultEntityMappings();
			InitHwProfileEntityMappings();
			InitImageEntityMappings();
			InitInvoiceEntityMappings();
			InitIssueNavigationStepEntityMappings();
			InitItemEntityMappings();
			InitItemDetailsEntityMappings();
			InitItemPropertyEntityMappings();
			InitItemRelationEntityMappings();
			InitItemRelationPropertyEntityMappings();
			InitItemTargetEntityMappings();
			InitLookupEntityMappings();
			InitOrderItemEntityMappings();
			InitPatientEntityMappings();
			InitPatientHistoryEntityMappings();
			InitProductEntityMappings();
			InitProductFormEntityMappings();
			InitProductSizeEntityMappings();
			InitPropertyEntityMappings();
			InitProtocolItemEntityMappings();
			InitProtocolStepEntityMappings();
			InitReadingEntityMappings();
			InitScheduleLineEntityMappings();
			InitServiceEntityMappings();
			InitSettingEntityMappings();
			InitShippingOrderEntityMappings();
			InitSpotCheckEntityMappings();
			InitSpotCheckResultEntityMappings();
			InitStageAnnouncementEntityMappings();
			InitStageAutoItemEntityMappings();
			InitTestEntityMappings();
			InitTestImprintableItemEntityMappings();
			InitTestingPointEntityMappings();
			InitTestIssueEntityMappings();
			InitTestProtocolEntityMappings();
			InitTestResultEntityMappings();
			InitTestResultFactorsEntityMappings();
			InitTestScheduleEntityMappings();
			InitTestServiceEntityMappings();
			InitUserEntityMappings();
			InitVFSEntityMappings();
			InitVFSItemEntityMappings();
			InitVFSItemSourceEntityMappings();
			InitVFSSecondaryItemEntityMappings();
			InitVFSSecondaryItemSourceEntityMappings();

		}


		/// <summary>Inits AppImageEntity's mappings</summary>
		private void InitAppImageEntityMappings()
		{
			this.AddElementMapping( "AppImageEntity", @"VitalExpert", @"dbo", "AppImages", 3 );
			this.AddElementFieldMapping( "AppImageEntity", "Id", "AppImages_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AppImageEntity", "Property", "AppImages_Property", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "AppImageEntity", "Value", "AppImages_Value", true, "Image", 2147483647, 0, 0, false, "", null, typeof(System.Byte[]), 2 );
		}
		/// <summary>Inits AppInfoEntity's mappings</summary>
		private void InitAppInfoEntityMappings()
		{
			this.AddElementMapping( "AppInfoEntity", @"VitalExpert", @"dbo", "AppInfo", 3 );
			this.AddElementFieldMapping( "AppInfoEntity", "Id", "AppInfo_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AppInfoEntity", "Property", "AppInfo_Property", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "AppInfoEntity", "Value", "AppInfo_Value", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
		}
		/// <summary>Inits AutoItemEntity's mappings</summary>
		private void InitAutoItemEntityMappings()
		{
			this.AddElementMapping( "AutoItemEntity", @"VitalExpert", @"dbo", "AutoItems", 31 );
			this.AddElementFieldMapping( "AutoItemEntity", "Id", "AutoItems_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoItemEntity", "TestingPointsId", "TestingPoints_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoItemEntity", "ImageId", "Image_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoItemEntity", "TypeLookupId", "Type_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "AutoItemEntity", "StructureTypeLookupId", "StructureType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "AutoItemEntity", "StatusLookupId", "Status_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "AutoItemEntity", "ChildsOrderTypeLookupId", "ChildsOrderType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "AutoItemEntity", "ChildsScanningTypeLookupId", "ChildsScanningType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "AutoItemEntity", "ScanningMethodLookupId", "ScanningMethod_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "AutoItemEntity", "ScansNumber", "AutoItems_ScansNumber", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
			this.AddElementFieldMapping( "AutoItemEntity", "Key", "AutoItems_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "AutoItemEntity", "Name", "AutoItems_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "AutoItemEntity", "FullName", "AutoItems_FullName", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 13 );
			this.AddElementFieldMapping( "AutoItemEntity", "Description", "AutoItems_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 14 );
			this.AddElementFieldMapping( "AutoItemEntity", "Frequency", "AutoItems_Frequency", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 15 );
			this.AddElementFieldMapping( "AutoItemEntity", "UserNotes", "AutoItems_UserNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 16 );
			this.AddElementFieldMapping( "AutoItemEntity", "IsUserItem", "AutoItems_IsUserItem", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 17 );
			this.AddElementFieldMapping( "AutoItemEntity", "IsSearchable", "AutoItems_IsSearchable", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 18 );
			this.AddElementFieldMapping( "AutoItemEntity", "InsertOnNo", "AutoItems_InsertOnNo", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 19 );
			this.AddElementFieldMapping( "AutoItemEntity", "IsImprintable", "AutoItems_IsImprintable", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 20 );
			this.AddElementFieldMapping( "AutoItemEntity", "CreationDateTime", "AutoItems_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 21 );
			this.AddElementFieldMapping( "AutoItemEntity", "UpdatedDateTime", "AutoItems_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 22 );
			this.AddElementFieldMapping( "AutoItemEntity", "MatchesNumber", "AutoItems_MatchesNumber", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 23 );
			this.AddElementFieldMapping( "AutoItemEntity", "FinishAllScanRounds", "AutoItems_FinishAllScanRounds", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 24 );
			this.AddElementFieldMapping( "AutoItemEntity", "AddResultOnMatch", "AutoItems_AddResultOnMatch", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 25 );
			this.AddElementFieldMapping( "AutoItemEntity", "ExcludeOnMatch", "AutoItems_ExcludeOnMatch", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 26 );
			this.AddElementFieldMapping( "AutoItemEntity", "AddAllChildesOnMatch", "AutoItems_AddAllChildesOnMatch", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 27 );
			this.AddElementFieldMapping( "AutoItemEntity", "ModelIdentifier", "AutoItems_ModelIdentifier", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 28 );
			this.AddElementFieldMapping( "AutoItemEntity", "DirectAccessChecks", "AutoItems_DirectAccessChecks", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 29 );
			this.AddElementFieldMapping( "AutoItemEntity", "GenderLookupId", "Gender_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 30 );
		}
		/// <summary>Inits AutoItemRelationEntity's mappings</summary>
		private void InitAutoItemRelationEntityMappings()
		{
			this.AddElementMapping( "AutoItemRelationEntity", @"VitalExpert", @"dbo", "AutoItemRelations", 8 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "Id", "AutoItemRelations_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "AutoItemParentId", "AutoItems_ParentId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "AutoItemChildId", "AutoItems_ChildId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "Order", "AutoItemRelations_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "IsDeleted", "AutoItemRelations_IsDeleted", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "CreationDateTime", "AutoItemRelations_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "AutoItemRelationEntity", "UpdatedDateTime", "AutoItemRelations_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits AutoProtocolEntity's mappings</summary>
		private void InitAutoProtocolEntityMappings()
		{
			this.AddElementMapping( "AutoProtocolEntity", @"VitalExpert", @"dbo", "AutoProtocols", 9 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "Id", "AutoProtocols_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "Name", "AutoProtocols_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "Key", "AutoProtocols_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "IsSystemProtocol", "AutoProtocols_IsSystemProtocol", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 4 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "IsDefaultProtocol", "AutoProtocols_IsDefaultProtocol", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "IsDeleted", "AutoProtocols_IsDeleted", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 6 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "CreationDateTime", "AutoProtocols_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "AutoProtocolEntity", "UpdatedDateTime", "AutoProtocols_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
		}
		/// <summary>Inits AutoProtocolRevisionEntity's mappings</summary>
		private void InitAutoProtocolRevisionEntityMappings()
		{
			this.AddElementMapping( "AutoProtocolRevisionEntity", @"VitalExpert", @"dbo", "AutoProtocolRevisions", 8 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "Id", "AutoProtocolRevisions_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "AutoProtocolsId", "AutoProtocols_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "Name", "AutoProtocolRevisions_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "Key", "AutoProtocolRevisions_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "IsSystemProtocol", "AutoProtocolRevisions_IsSystemProtocol", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "CreationDateTime", "AutoProtocolRevisions_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "AutoProtocolRevisionEntity", "UpdatedDateTime", "AutoProtocolRevisions_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits AutoProtocolStageEntity's mappings</summary>
		private void InitAutoProtocolStageEntityMappings()
		{
			this.AddElementMapping( "AutoProtocolStageEntity", @"VitalExpert", @"dbo", "AutoProtocolStages", 9 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "Id", "AutoProtocolStages_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "AutoProtocolsId", "AutoProtocols_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "AutoTestStagesId", "AutoTestStages_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "StageItemsOrderTypeLookupId", "StageItemsOrderType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "Order", "AutoProtocolStages_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "IsDeleted", "IsDeleted", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 6 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "CreationDateTime", "AutoProtocolStages_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "AutoProtocolStageEntity", "UpdatedDateTime", "AutoProtocolStages_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
		}
		/// <summary>Inits AutoProtocolStageRevisionEntity's mappings</summary>
		private void InitAutoProtocolStageRevisionEntityMappings()
		{
			this.AddElementMapping( "AutoProtocolStageRevisionEntity", @"VitalExpert", @"dbo", "AutoProtocolStageRevisions", 8 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "Id", "AutoProtocolStageRevisions_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "AutoProtocolRevisionsId", "AutoProtocolRevisions_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "AutoProtocolStagesId", "AutoProtocolStages_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "AutoTestStagesId", "AutoTestStages_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "Order", "AutoProtocolStageRevisions_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "CreationDateTime", "AutoProtocolStageRevisions_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "AutoProtocolStageRevisionEntity", "UpdatedDateTime", "AutoProtocolStageRevisions_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits AutoTestEntity's mappings</summary>
		private void InitAutoTestEntityMappings()
		{
			this.AddElementMapping( "AutoTestEntity", @"VitalExpert", @"dbo", "AutoTests", 10 );
			this.AddElementFieldMapping( "AutoTestEntity", "Id", "AutoTests_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoTestEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoTestEntity", "PatientId", "Patient_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoTestEntity", "AutoProtocolRevisionsId", "AutoProtocolRevisions_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoTestEntity", "Name", "AutoTests_Name", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "AutoTestEntity", "Description", "AutoTests_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "AutoTestEntity", "Notes", "AutoTests_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "AutoTestEntity", "TestDate", "AutoTests_TestDate", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "AutoTestEntity", "CreationDateTime", "AutoTests_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
			this.AddElementFieldMapping( "AutoTestEntity", "UpdatedDateTime", "AutoTests_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
		}
		/// <summary>Inits AutoTestResultEntity's mappings</summary>
		private void InitAutoTestResultEntityMappings()
		{
			this.AddElementMapping( "AutoTestResultEntity", @"VitalExpert", @"dbo", "AutoTestResults", 12 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "Id", "AutoTestResults_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "AutoTestsId", "AutoTests_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "AutoItemsId", "AutoItems_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "AutoProtocolStageRevisionsId", "AutoProtocolStageRevisions_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "PreliminaryReading", "AutoTestResults_PreliminaryReading", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "SummaryReading", "AutoTestResults_SummaryReading", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "IsAddedManually", "AutoTestResults_IsAddedManually", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 7 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "Notes", "AutoTestResults_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "CreationDateTime", "AutoTestResults_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "UpdatedDateTime", "AutoTestResults_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "AutoTestResultEntity", "AutoTestResultsParentId", "AutoTestResults_ParentId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 11 );
		}
		/// <summary>Inits AutoTestResultProductEntity's mappings</summary>
		private void InitAutoTestResultProductEntityMappings()
		{
			this.AddElementMapping( "AutoTestResultProductEntity", @"VitalExpert", @"dbo", "AutoTestResultProduct", 14 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "Id", "AutoTestResultProduct_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "AutoTestResultsId", "AutoTestResults_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "Quantity", "AutoTestResultProduct_Quantity", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "Price", "AutoTestResultProduct_Price", false, "Decimal", 0, 4, 19, false, "", null, typeof(System.Decimal), 4 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "Duration", "AutoTestResultProduct_Duration", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "Schedule", "AutoTestResultProduct_Schedule", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "SuggestedUsage", "AutoTestResultProduct_SuggestedUsage", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "Comments", "AutoTestResultProduct_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "CreationDateTime", "AutoTestResultProduct_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "UpdatedDateTime", "AutoTestResultProduct_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "IsChecked", "AutoTestResultProduct_IsChecked", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 11 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "ProductFormsId", "ProductForms_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 12 );
			this.AddElementFieldMapping( "AutoTestResultProductEntity", "ProductSizesId", "ProductSizes_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 13 );
		}
		/// <summary>Inits AutoTestStageEntity's mappings</summary>
		private void InitAutoTestStageEntityMappings()
		{
			this.AddElementMapping( "AutoTestStageEntity", @"VitalExpert", @"dbo", "AutoTestStages", 13 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "Id", "AutoTestStages_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "StageItemsOrderTypeLookupId", "StageItemsOrderType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "Name", "AutoTestStages_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "Key", "AutoTestStages_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "Description", "AutoTestStages_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "Dependencies", "AutoTestStages_Dependencies", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "IsMultiLevel", "AutoTestStages_IsMultiLevel", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 7 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "ScanTypeEnabled", "AutoTestStages_ScanTypeEnabled", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 8 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "CreationDateTime", "AutoTestStages_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "UpdatedDateTime", "AutoTestStages_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "StageTabKey", "AutoTestStages_StageTabKey", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "AutoTestStageEntity", "IsDestinationOnly", "AutoTestStages_IsDestinationOnly", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 12 );
		}
		/// <summary>Inits ClinicProductPricingEntity's mappings</summary>
		private void InitClinicProductPricingEntityMappings()
		{
			this.AddElementMapping( "ClinicProductPricingEntity", @"VitalExpert", @"dbo", "ClinicProductPricings", 8 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "Id", "ClinicProductPricings_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "ProductsId", "Products_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "Form", "ClinicProductPricings_Form", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "Size", "ClinicProductPricings_Size", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "Price", "ClinicProductPricings_Price", false, "Decimal", 0, 4, 19, false, "", null, typeof(System.Decimal), 5 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "CreationDateTime", "ClinicProductPricings_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "ClinicProductPricingEntity", "UpdatedDateTime", "ClinicProductPricings_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits DosageOptionEntity's mappings</summary>
		private void InitDosageOptionEntityMappings()
		{
			this.AddElementMapping( "DosageOptionEntity", @"VitalExpert", @"dbo", "DosageOptions", 9 );
			this.AddElementFieldMapping( "DosageOptionEntity", "Id", "DosageOptions_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "DosageOptionEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "DosageOptionEntity", "ProductFormsId", "ProductForms_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "DosageOptionEntity", "Order", "DosageOptions_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "DosageOptionEntity", "Name", "DosageOptions_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "DosageOptionEntity", "UsageSchedule", "DosageOptions_UsageSchedule", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "DosageOptionEntity", "CreationDateTime", "DosageOptions_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "DosageOptionEntity", "UpdatedDateTime", "DosageOptions_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "DosageOptionEntity", "SuggestedUsage", "DosageOptions_SuggestedUsage", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 8 );
		}
		/// <summary>Inits FrequencyTestEntity's mappings</summary>
		private void InitFrequencyTestEntityMappings()
		{
			this.AddElementMapping( "FrequencyTestEntity", @"VitalExpert", @"dbo", "Frequency_Tests", 7 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "Id", "Frequency_Test_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "PatientId", "Patient_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "Name", "Frequency_Test_Name", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "Notes", "Frequency_Test_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "CreationDateTime", "Frequency_Test_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "UpdatedDateTime", "Frequency_Test_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "FrequencyTestEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
		}
		/// <summary>Inits FrequencyTestResultEntity's mappings</summary>
		private void InitFrequencyTestResultEntityMappings()
		{
			this.AddElementMapping( "FrequencyTestResultEntity", @"VitalExpert", @"dbo", "Frequency_Test_Results", 9 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "Id", "Frequency_Test_Result_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "FrequencyTestId", "Frequency_Test_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "ItemId", "Item_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "Notes", "Frequency_Test_Result_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "CreationDateTime", "Frequency_Test_Result_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "UpdatedDateTime", "Frequency_Test_Result_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "TimesPerWeek", "Frequency_Test_Result_TimesPerWeek", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "FrequencyTestResultEntity", "NumberOfWeeks", "Frequency_Test_Result_NumberOfWeeks", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
		}
		/// <summary>Inits HwProfileEntity's mappings</summary>
		private void InitHwProfileEntityMappings()
		{
			this.AddElementMapping( "HwProfileEntity", @"VitalExpert", @"dbo", "HWProfiles", 17 );
			this.AddElementFieldMapping( "HwProfileEntity", "Id", "HWProfile_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "HwProfileEntity", "Name", "HWProfile_Name", false, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "HwProfileEntity", "MinReading", "HWProfile_MinReading", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "HwProfileEntity", "DisconnectedTimeout", "HWProfile_DisconnectedTimeout", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "HwProfileEntity", "StabilityTimeout", "HWProfile_StabilityTimeout", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "HwProfileEntity", "StabilityRange", "HWProfile_StabilityRange", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "HwProfileEntity", "IsSystemProfile", "HWProfile_IsSystemProfile", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 6 );
			this.AddElementFieldMapping( "HwProfileEntity", "IsDefault", "HWProfile_IsDefault", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 7 );
			this.AddElementFieldMapping( "HwProfileEntity", "Image", "HWProfile_Image", true, "Image", 2147483647, 0, 0, false, "", null, typeof(System.Byte[]), 8 );
			this.AddElementFieldMapping( "HwProfileEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "HwProfileEntity", "CreationDateTime", "HWProfile_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "HwProfileEntity", "UpdatedDateTime", "HWProfile_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
			this.AddElementFieldMapping( "HwProfileEntity", "Key", "HWProfile_Key", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "HwProfileEntity", "DefaultMinReading", "HWProfile_DefaultMinReading", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 13 );
			this.AddElementFieldMapping( "HwProfileEntity", "DefaultDisconnectedTimeout", "HWProfile_DefaultDisconnectedTimeout", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 14 );
			this.AddElementFieldMapping( "HwProfileEntity", "DefaultStabilityTimeout", "HWProfile_DefaultStabilityTimeout", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
			this.AddElementFieldMapping( "HwProfileEntity", "DefaultStabilityRange", "HWProfile_DefaultStabilityRange", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 16 );
		}
		/// <summary>Inits ImageEntity's mappings</summary>
		private void InitImageEntityMappings()
		{
			this.AddElementMapping( "ImageEntity", @"VitalExpert", @"dbo", "Images", 11 );
			this.AddElementFieldMapping( "ImageEntity", "Id", "Image_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ImageEntity", "Data", "Image_Data", true, "Image", 2147483647, 0, 0, false, "", null, typeof(System.Byte[]), 1 );
			this.AddElementFieldMapping( "ImageEntity", "Extension", "Image_Extension", false, "VarChar", 4, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "ImageEntity", "Path", "Image_Path", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "ImageEntity", "Size", "Image_Size", true, "Float", 0, 0, 38, false, "", null, typeof(System.Double), 4 );
			this.AddElementFieldMapping( "ImageEntity", "OldImageBoxWidth", "Image_OldImageBoxWidth", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "ImageEntity", "OldImageBoxHeight", "Image_OldImageBoxHeight", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "ImageEntity", "Description", "Image_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "ImageEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "ImageEntity", "CreationDateTime", "Image_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "ImageEntity", "UpdatedDateTime", "Image_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
		}
		/// <summary>Inits InvoiceEntity's mappings</summary>
		private void InitInvoiceEntityMappings()
		{
			this.AddElementMapping( "InvoiceEntity", @"VitalExpert", @"dbo", "Invoices", 10 );
			this.AddElementFieldMapping( "InvoiceEntity", "Id", "Invoice_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "InvoiceEntity", "Number", "Invoice_Number", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "InvoiceEntity", "TestId", "Test_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "InvoiceEntity", "Comments", "Invoice_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "InvoiceEntity", "TotalAmount", "Invoice_TotalAmount", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 4 );
			this.AddElementFieldMapping( "InvoiceEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "InvoiceEntity", "CreationDateTime", "Invoice_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "InvoiceEntity", "UpdatedDateTime", "Invoice_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "InvoiceEntity", "ChequeNumber", "Invoice_ChequeNumber", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "InvoiceEntity", "PaymentMethod", "Invoice_PaymentMethod", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 9 );
		}
		/// <summary>Inits IssueNavigationStepEntity's mappings</summary>
		private void InitIssueNavigationStepEntityMappings()
		{
			this.AddElementMapping( "IssueNavigationStepEntity", @"VitalExpert", @"dbo", "Issue_Navigation_Steps", 8 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "Id", "Issue_Navigation_Step_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "TestIssueId", "Test_Issue_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "ParentId", "Issue_Navigation_Step_Parent_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "Order", "Issue_Navigation_Step_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "CreationDateTime", "Issue_Navigation_Step_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "UpdatedDateTime", "Issue_Navigation_Step_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "IssueNavigationStepEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
		}
		/// <summary>Inits ItemEntity's mappings</summary>
		private void InitItemEntityMappings()
		{
			this.AddElementMapping( "ItemEntity", @"VitalExpert", @"dbo", "Items", 17 );
			this.AddElementFieldMapping( "ItemEntity", "Id", "Item_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ItemEntity", "Name", "Item_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "ItemEntity", "FullName", "Item_FullName", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "ItemEntity", "Description", "Item_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "ItemEntity", "ItemMemo", "Item_Memo", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ItemEntity", "GenderLookupId", "Gender_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "ItemEntity", "TypeLookupId", "Type_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "ItemEntity", "ListTypeLookupId", "ListType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "ItemEntity", "ItemDetailId", "Item_Detail_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "ItemEntity", "Order", "Item_Order", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "ItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
			this.AddElementFieldMapping( "ItemEntity", "CreationDateTime", "Item_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
			this.AddElementFieldMapping( "ItemEntity", "UpdatedDateTime", "Item_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 12 );
			this.AddElementFieldMapping( "ItemEntity", "ItemCsabinaryCode", "Item_CSABinaryCode", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 13 );
			this.AddElementFieldMapping( "ItemEntity", "IsStarred", "Item_IsStarred", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 14 );
			this.AddElementFieldMapping( "ItemEntity", "ItemSourceLookupId", "ItemSource_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
			this.AddElementFieldMapping( "ItemEntity", "Key", "Item_Key", true, "NVarChar", 100, 0, 0, false, "", null, typeof(System.String), 16 );
		}
		/// <summary>Inits ItemDetailsEntity's mappings</summary>
		private void InitItemDetailsEntityMappings()
		{
			this.AddElementMapping( "ItemDetailsEntity", @"VitalExpert", @"dbo", "Item_Details", 7 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "Id", "Item_Detail_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "ImageId", "Image_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "X", "Item_Detail_X", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "Y", "Item_Detail_Y", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "UpdatedDateTime", "Item_Detail_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "ItemDetailsEntity", "CreationDateTime", "Item_Detail_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
		}
		/// <summary>Inits ItemPropertyEntity's mappings</summary>
		private void InitItemPropertyEntityMappings()
		{
			this.AddElementMapping( "ItemPropertyEntity", @"VitalExpert", @"dbo", "Item_Properties", 4 );
			this.AddElementFieldMapping( "ItemPropertyEntity", "Id", "Item_Property_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ItemPropertyEntity", "PropertyId", "Property_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ItemPropertyEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ItemPropertyEntity", "Value", "Item_Property_Value", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
		}
		/// <summary>Inits ItemRelationEntity's mappings</summary>
		private void InitItemRelationEntityMappings()
		{
			this.AddElementMapping( "ItemRelationEntity", @"VitalExpert", @"dbo", "Item_Relation", 9 );
			this.AddElementFieldMapping( "ItemRelationEntity", "Id", "Item_Relation_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ItemRelationEntity", "ItemParentId", "Item_ParentId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ItemRelationEntity", "ItemChildId", "Item_ChildId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ItemRelationEntity", "RelationTypeLookupId", "RelationType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ItemRelationEntity", "Order", "Item_Relation_Order", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "ItemRelationEntity", "Step", "Item_Relation_Step", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "ItemRelationEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "ItemRelationEntity", "CreationDateTime", "Item_Relation_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "ItemRelationEntity", "UpdatedDateTime", "Item_Relation_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
		}
		/// <summary>Inits ItemRelationPropertyEntity's mappings</summary>
		private void InitItemRelationPropertyEntityMappings()
		{
			this.AddElementMapping( "ItemRelationPropertyEntity", @"VitalExpert", @"dbo", "Item_Relation_Properties", 4 );
			this.AddElementFieldMapping( "ItemRelationPropertyEntity", "Id", "Item_Relation_Property_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ItemRelationPropertyEntity", "PropertyId", "Property_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ItemRelationPropertyEntity", "ItemRelationId", "Item_Relation_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ItemRelationPropertyEntity", "Value", "Item_Property_Value", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
		}
		/// <summary>Inits ItemTargetEntity's mappings</summary>
		private void InitItemTargetEntityMappings()
		{
			this.AddElementMapping( "ItemTargetEntity", @"VitalExpert", @"dbo", "Item_Targets", 7 );
			this.AddElementFieldMapping( "ItemTargetEntity", "Id", "Item_Target_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ItemTargetEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ItemTargetEntity", "TargetTypeLookupId", "TargetType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ItemTargetEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ItemTargetEntity", "ItemTargetCreationDateTime", "Item_Target_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "ItemTargetEntity", "ItemTargetUpdatedDateTime", "Item_Target_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "ItemTargetEntity", "Order", "Item_Target_Order", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
		}
		/// <summary>Inits LookupEntity's mappings</summary>
		private void InitLookupEntityMappings()
		{
			this.AddElementMapping( "LookupEntity", @"VitalExpert", @"dbo", "Lookups", 4 );
			this.AddElementFieldMapping( "LookupEntity", "Id", "Lookup_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "LookupEntity", "Value", "Lookup_Value", false, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "LookupEntity", "Type", "Lookup_Type", false, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "LookupEntity", "Key", "Lookup_Key", true, "NVarChar", 250, 0, 0, false, "", null, typeof(System.String), 3 );
		}
		/// <summary>Inits OrderItemEntity's mappings</summary>
		private void InitOrderItemEntityMappings()
		{
			this.AddElementMapping( "OrderItemEntity", @"VitalExpert", @"dbo", "Order_Items", 9 );
			this.AddElementFieldMapping( "OrderItemEntity", "Id", "Order_Item_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "OrderItemEntity", "ShippingOrderId", "Shipping_Order_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "OrderItemEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "OrderItemEntity", "Quantity", "Order_Item_Quantity", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "OrderItemEntity", "Comments", "Order_Item_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "OrderItemEntity", "Include", "Order_Item_Include", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "OrderItemEntity", "CreationDateTime", "Order_Item_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "OrderItemEntity", "UpdatedDateTime", "Order_Item_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "OrderItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
		}
		/// <summary>Inits PatientEntity's mappings</summary>
		private void InitPatientEntityMappings()
		{
			this.AddElementMapping( "PatientEntity", @"VitalExpert", @"dbo", "Patients", 20 );
			this.AddElementFieldMapping( "PatientEntity", "Id", "Patient_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "PatientEntity", "Number", "Patient_Number", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "PatientEntity", "FirstName", "Patient_FirstName", false, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "PatientEntity", "LastName", "Patient_LastName", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "PatientEntity", "Address1", "Patient_Address1", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "PatientEntity", "Address2", "Patient_Address2", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "PatientEntity", "City", "Patient_City", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "PatientEntity", "State", "Patient_State", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "PatientEntity", "Zip", "Patient_Zip", true, "NVarChar", 10, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "PatientEntity", "GenderLookupId", "Gender_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "PatientEntity", "DateOfBirth", "Patient_DateOfBirth", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "PatientEntity", "HomePhone", "Patient_HomePhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "PatientEntity", "WorkPhone", "Patient_WorkPhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "PatientEntity", "CellPhone", "Patient_CellPhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 13 );
			this.AddElementFieldMapping( "PatientEntity", "Fax", "Patient_Fax", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 14 );
			this.AddElementFieldMapping( "PatientEntity", "Email", "Patient_Email", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 15 );
			this.AddElementFieldMapping( "PatientEntity", "Notes", "Patient_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 16 );
			this.AddElementFieldMapping( "PatientEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 17 );
			this.AddElementFieldMapping( "PatientEntity", "CreationDateTime", "Patient_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 18 );
			this.AddElementFieldMapping( "PatientEntity", "UpdatedDateTime", "Patient_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 19 );
		}
		/// <summary>Inits PatientHistoryEntity's mappings</summary>
		private void InitPatientHistoryEntityMappings()
		{
			this.AddElementMapping( "PatientHistoryEntity", @"VitalExpert", @"dbo", "Patient_History", 8 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "Id", "Patient_History_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "PatientId", "Patient_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "Name", "Patient_History_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "Description", "Patient_History_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "TypeLookupId", "PatientHistoryType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "CreationDateTime", "Patient_History_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "PatientHistoryEntity", "UpdatedDateTime", "Patient_History_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits ProductEntity's mappings</summary>
		private void InitProductEntityMappings()
		{
			this.AddElementMapping( "ProductEntity", @"VitalExpert", @"dbo", "Products", 12 );
			this.AddElementFieldMapping( "ProductEntity", "Id", "Products_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ProductEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ProductEntity", "AutoItemsId", "AutoItems_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ProductEntity", "Supplier", "Products_Supplier", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "ProductEntity", "IngredientsString", "Products_IngredientsString", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ProductEntity", "Supports", "Products_Supports", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "ProductEntity", "UsefulFor", "Products_UsefulFor", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "ProductEntity", "Price", "Products_Price", false, "Decimal", 0, 4, 19, false, "", null, typeof(System.Decimal), 7 );
			this.AddElementFieldMapping( "ProductEntity", "DiscountPercentage", "Products_DiscountPercentage", true, "Decimal", 0, 4, 19, false, "", null, typeof(System.Decimal), 8 );
			this.AddElementFieldMapping( "ProductEntity", "HasDiscount", "Products_HasDiscount", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 9 );
			this.AddElementFieldMapping( "ProductEntity", "CreationDateTime", "Products_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "ProductEntity", "UpdatedDateTime", "Products_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
		}
		/// <summary>Inits ProductFormEntity's mappings</summary>
		private void InitProductFormEntityMappings()
		{
			this.AddElementMapping( "ProductFormEntity", @"VitalExpert", @"dbo", "ProductForms", 9 );
			this.AddElementFieldMapping( "ProductFormEntity", "Id", "ProductForms_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ProductFormEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ProductFormEntity", "ProductsId", "Products_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ProductFormEntity", "StatusLookupId", "Status_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ProductFormEntity", "Form", "ProductForms_Form", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ProductFormEntity", "SuggestedUsage", "ProductForms_SuggestedUsage", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "ProductFormEntity", "UsageSchedule", "ProductForms_UsageSchedule", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "ProductFormEntity", "CreationDateTime", "ProductForms_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "ProductFormEntity", "UpdatedDateTime", "ProductForms_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
		}
		/// <summary>Inits ProductSizeEntity's mappings</summary>
		private void InitProductSizeEntityMappings()
		{
			this.AddElementMapping( "ProductSizeEntity", @"VitalExpert", @"dbo", "ProductSizes", 8 );
			this.AddElementFieldMapping( "ProductSizeEntity", "Id", "ProductSizes_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ProductSizeEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ProductSizeEntity", "ProductFormsId", "ProductForms_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ProductSizeEntity", "StatusLookupsId", "Status_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ProductSizeEntity", "Size", "ProductSizes_Size", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ProductSizeEntity", "Price", "ProductSizes_Price", false, "Decimal", 0, 4, 19, false, "", null, typeof(System.Decimal), 5 );
			this.AddElementFieldMapping( "ProductSizeEntity", "CreationDateTime", "ProductSizes_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "ProductSizeEntity", "UpdatedDateTime", "ProductSizes_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits PropertyEntity's mappings</summary>
		private void InitPropertyEntityMappings()
		{
			this.AddElementMapping( "PropertyEntity", @"VitalExpert", @"dbo", "Properties", 9 );
			this.AddElementFieldMapping( "PropertyEntity", "Id", "Property_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "PropertyEntity", "Name", "Property_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "PropertyEntity", "Key", "Property_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "PropertyEntity", "Description", "Property_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "PropertyEntity", "ApplicableTypeLookupId", "ApplicableType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "PropertyEntity", "ValueTypeLookupId", "ValueType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "PropertyEntity", "SourceConfig", "Property_SourceConfig", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "PropertyEntity", "MembersConfig", "Property_MembersConfig", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "PropertyEntity", "Caption", "Property_Caption", true, "NVarChar", 100, 0, 0, false, "", null, typeof(System.String), 8 );
		}
		/// <summary>Inits ProtocolItemEntity's mappings</summary>
		private void InitProtocolItemEntityMappings()
		{
			this.AddElementMapping( "ProtocolItemEntity", @"VitalExpert", @"dbo", "Protocol_Items", 6 );
			this.AddElementFieldMapping( "ProtocolItemEntity", "Id", "ProtocolItem_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ProtocolItemEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ProtocolItemEntity", "TestProtocolId", "Test_Protocol_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ProtocolItemEntity", "CreationDateTime", "ProtocolItem_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "ProtocolItemEntity", "UpdatedDateTime", "ProtocolItem_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "ProtocolItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
		}
		/// <summary>Inits ProtocolStepEntity's mappings</summary>
		private void InitProtocolStepEntityMappings()
		{
			this.AddElementMapping( "ProtocolStepEntity", @"VitalExpert", @"dbo", "Protocol_Steps", 7 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "Id", "Protocol_Step_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "TestProtocolId", "Test_Protocol_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "Order", "Protocol_Step_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "TypeLookupId", "Type_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "CreationDateTime", "Test_Protocol_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "UpdatedDateTime", "Test_Protocol_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "ProtocolStepEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
		}
		/// <summary>Inits ReadingEntity's mappings</summary>
		private void InitReadingEntityMappings()
		{
			this.AddElementMapping( "ReadingEntity", @"VitalExpert", @"dbo", "Readings", 15 );
			this.AddElementFieldMapping( "ReadingEntity", "Id", "Reading_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ReadingEntity", "TestId", "Test_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ReadingEntity", "DateTime", "Reading_DateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 2 );
			this.AddElementFieldMapping( "ReadingEntity", "ItemId", "Item_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "ReadingEntity", "Value", "Reading_Value", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "ReadingEntity", "Min", "Reading_Min", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "ReadingEntity", "Max", "Reading_Max", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "ReadingEntity", "Rise", "Reading_Rise", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "ReadingEntity", "Fall", "Reading_Fall", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "ReadingEntity", "ValueBalanced", "Reading_Value_Balanced", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "ReadingEntity", "ListPointLookupId", "ListPoints_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
			this.AddElementFieldMapping( "ReadingEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 11 );
			this.AddElementFieldMapping( "ReadingEntity", "CreationDateTime", "Reading_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 12 );
			this.AddElementFieldMapping( "ReadingEntity", "UpdatedDateTime", "Reading_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 13 );
			this.AddElementFieldMapping( "ReadingEntity", "PointSetItemId", "PointSet_ItemId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 14 );
		}
		/// <summary>Inits ScheduleLineEntity's mappings</summary>
		private void InitScheduleLineEntityMappings()
		{
			this.AddElementMapping( "ScheduleLineEntity", @"VitalExpert", @"dbo", "Schedule_Lines", 20 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Id", "Schedule_Line_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "TestScheduleId", "Test_Schedule_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Notes", "Schedule_Line_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Duration", "Schedule_Line_Duration", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "ToBeShipped", "Schedule_Line_ToBeShipped", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "WhenArising", "Schedule_Line_WhenArising", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Breakfast", "Schedule_Line_Breakfast", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "BetweenMealsEarly", "Schedule_Line_BetweenMealsEarly", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Lunch", "Schedule_Line_Lunch", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 9 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "BetweenMealsLate", "Schedule_Line_BetweenMealsLate", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 10 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Dinner", "Schedule_Line_Dinner", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "BeforeSleep", "Schedule_Line_BeforeSleep", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "NoPerBottle", "Schedule_Line_NoPerBottle", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 13 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "NoOfBottle", "Schedule_Line_NoOfBottle", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 14 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "Price", "Schedule_Line_Price", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 15 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "CreationDateTime", "Schedule_Line_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 16 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "UpdatedDateTime", "Schedule_Line_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 17 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 18 );
			this.AddElementFieldMapping( "ScheduleLineEntity", "IsDeleted", "Schedule_Line_IsDeleted", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 19 );
		}
		/// <summary>Inits ServiceEntity's mappings</summary>
		private void InitServiceEntityMappings()
		{
			this.AddElementMapping( "ServiceEntity", @"VitalExpert", @"dbo", "Services", 16 );
			this.AddElementFieldMapping( "ServiceEntity", "Id", "Service_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ServiceEntity", "Key", "Service_Key", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "ServiceEntity", "Name", "Service_Name", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "ServiceEntity", "Description", "Service_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "ServiceEntity", "Comments", "Service_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "ServiceEntity", "Price", "Service_Price", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 5 );
			this.AddElementFieldMapping( "ServiceEntity", "IsDefault", "Service_IsDefault", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 6 );
			this.AddElementFieldMapping( "ServiceEntity", "TypeLookupId", "ServiceType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "ServiceEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "ServiceEntity", "CreationDateTime", "Service_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "ServiceEntity", "UpdatedDateTime", "Service_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "ServiceEntity", "DefaultName", "Service_DefaultName", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "ServiceEntity", "DefaultDescription", "Service_DefaultDescription", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "ServiceEntity", "DefaultComments", "Service_DefaultComments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 13 );
			this.AddElementFieldMapping( "ServiceEntity", "DefaultPrice", "Service_DefaultPrice", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 14 );
			this.AddElementFieldMapping( "ServiceEntity", "DefaultIsDefault", "Service_DefaultIsDefault", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 15 );
		}
		/// <summary>Inits SettingEntity's mappings</summary>
		private void InitSettingEntityMappings()
		{
			this.AddElementMapping( "SettingEntity", @"VitalExpert", @"dbo", "Settings", 12 );
			this.AddElementFieldMapping( "SettingEntity", "Id", "Setting_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "SettingEntity", "Name", "Setting_Name", false, "NVarChar", 100, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "SettingEntity", "Key", "Setting_Key", false, "NVarChar", 100, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "SettingEntity", "Value", "Setting_Value", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "SettingEntity", "Description", "Setting_Description", true, "Text", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "SettingEntity", "ValueTypeLookupId", "ValueType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "SettingEntity", "SettingGroupLookupId", "SettingGroup_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "SettingEntity", "SourceConfig", "Setting_SourceConfig", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "SettingEntity", "MembersConfig", "Setting_MembersConfig", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "SettingEntity", "Caption", "Setting_Caption", true, "NVarChar", 100, 0, 0, false, "", null, typeof(System.String), 9 );
			this.AddElementFieldMapping( "SettingEntity", "DefaultValue", "Setting_Default_Value", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 10 );
			this.AddElementFieldMapping( "SettingEntity", "IsVisible", "Setting_IsVisible", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 11 );
		}
		/// <summary>Inits ShippingOrderEntity's mappings</summary>
		private void InitShippingOrderEntityMappings()
		{
			this.AddElementMapping( "ShippingOrderEntity", @"VitalExpert", @"dbo", "Shipping_Orders", 29 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "Id", "Shipping_Order_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "Number", "Shipping_Order_Number", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TestId", "Test_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "SentDate", "Shipping_Order_SentDate", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "SendToClient", "Shipping_Order_SendToClient", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 4 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "Sent", "Shipping_Order_Sent", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "Comments", "Shipping_Order_Comments", true, "NText", 1073741823, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientFirstName", "Shipping_Order_PatientFirstName", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientLastName", "Shipping_Order_PatientLastName", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientAddress1", "Shipping_Order_PatientAddress1", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 9 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientAddress2", "Shipping_Order_PatientAddress2", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 10 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientCity", "Shipping_Order_PatientCity", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientState", "Shipping_Order_PatientState", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientZip", "Shipping_Order_PatientZip", true, "NVarChar", 10, 0, 0, false, "", null, typeof(System.String), 13 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientHomePhone", "Shipping_Order_PatientHomePhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 14 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientWorkPhone", "Shipping_Order_PatientWorkPhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 15 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientCellPhone", "Shipping_Order_PatientCellPhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 16 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientFax", "Shipping_Order_PatientFax", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 17 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "PatientEmail", "Shipping_Order_PatientEmail", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 18 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TechnicianName", "Shipping_Order_TechnicianName", true, "NVarChar", 100, 0, 0, false, "", null, typeof(System.String), 19 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TechnicianAddress", "Shipping_Order_TechnicianAddress", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 20 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TechnicianState", "Shipping_Order_TechnicianState", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 21 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TechnicianZipCode", "Shipping_Order_TechnicianZipCode", true, "NVarChar", 10, 0, 0, false, "", null, typeof(System.String), 22 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TechnicianCity", "Shipping_Order_TechnicianCity", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 23 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "TechnicianPhone", "Shipping_Order_TechnicianPhone", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 24 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "CreationDateTime", "Shipping_Order_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 25 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "UpdatedDateTime", "Shipping_Order_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 26 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 27 );
			this.AddElementFieldMapping( "ShippingOrderEntity", "ShippingMethodLookupId", "ShippingMethod_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 28 );
		}
		/// <summary>Inits SpotCheckEntity's mappings</summary>
		private void InitSpotCheckEntityMappings()
		{
			this.AddElementMapping( "SpotCheckEntity", @"VitalExpert", @"dbo", "Spot_Checks", 27 );
			this.AddElementFieldMapping( "SpotCheckEntity", "Id", "Spot_Check_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "SpotCheckEntity", "PatientId", "Patient_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "SpotCheckEntity", "Notes", "Spot_Check_MteNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "SpotCheckEntity", "Name", "Spot_Check_Name", true, "NVarChar", 200, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "SpotCheckEntity", "CreationDateTime", "Spot_Check_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "SpotCheckEntity", "UpdatedDateTime", "Spot_Check_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "SpotCheckEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "SpotCheckEntity", "CapsoleTnotes", "Spot_Check_CapsoleTNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "SpotCheckEntity", "IngredientsNotes", "Spot_Check_MineralsNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MondayNotes", "Spot_Check_MondayNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 9 );
			this.AddElementFieldMapping( "SpotCheckEntity", "TuesdayNotes", "Spot_Check_TuesdayNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 10 );
			this.AddElementFieldMapping( "SpotCheckEntity", "WednesdayNotes", "Spot_Check_WednesdayNotes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsThree", "Spot_Check_MineralsThree", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 12 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsOne", "Spot_Check_MineralsOne", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 13 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsIvPush", "Spot_Check_MineralsIvPush", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 14 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsSterlieWaterCc", "Spot_Check_MineralsSterlieWaterCC", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsSterlieWaterCcpriority", "Spot_Check_MineralsSterlieWaterCCPriority", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 16 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsDextroseCc", "Spot_Check_MineralsDextroseCC", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 17 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsDextroseCcpriority", "Spot_Check_MineralsDextroseCCPriority", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 18 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsNormalSalineCc", "Spot_Check_MineralsNormalSalineCC", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 19 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsNormalSalineCcpriority", "Spot_Check_MineralsNormalSalineCCPriority", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 20 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsIvperMin", "Spot_Check_MineralsIVPerMin", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 21 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsPerWeek", "Spot_Check_MineralsPerWeek", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 22 );
			this.AddElementFieldMapping( "SpotCheckEntity", "MineralsEdta", "Spot_Check_MineralsEDTA", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 23 );
			this.AddElementFieldMapping( "SpotCheckEntity", "TestId", "Test_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 24 );
			this.AddElementFieldMapping( "SpotCheckEntity", "IngredientsNumberOfBags", "Spot_Check_IngredientsNumberOfBags", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 25 );
			this.AddElementFieldMapping( "SpotCheckEntity", "IngredientsNumberPerWeek", "Spot_Check_IngredientsNumberPerWeek", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 26 );
		}
		/// <summary>Inits SpotCheckResultEntity's mappings</summary>
		private void InitSpotCheckResultEntityMappings()
		{
			this.AddElementMapping( "SpotCheckResultEntity", @"VitalExpert", @"dbo", "Spot_Check_Results", 12 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "Id", "Spot_Check_Result_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "SpotCheckId", "Spot_Check_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "ItemId", "Item_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "CreationDateTime", "Spot_Check_Result_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "UpdatedDateTime", "Spot_Check_Result_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "YesNo", "Spot_Check_Result_YesNo", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 6 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "NumberOfBags", "Spot_Check_Result_NumberOfBags", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "NumberOfWeeks", "Spot_Check_Result_NumberOfWeeks", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "Dosage", "Spot_Check_Result_Dosage", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "Notes", "Spot_Check_Result_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 10 );
			this.AddElementFieldMapping( "SpotCheckResultEntity", "ResultTypeId", "SpotCheckResultType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 11 );
		}
		/// <summary>Inits StageAnnouncementEntity's mappings</summary>
		private void InitStageAnnouncementEntityMappings()
		{
			this.AddElementMapping( "StageAnnouncementEntity", @"VitalExpert", @"dbo", "StageAnnouncements", 7 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "Id", "StageAnnouncements_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "Key", "StageAnnouncements_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "Text", "StageAnnouncements_Text", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "AudioPath", "StageAnnouncements_AudioPath", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "CreationDateTime", "StageAnnouncements_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "StageAnnouncementEntity", "UpdatedDateTime", "StageAnnouncements_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
		}
		/// <summary>Inits StageAutoItemEntity's mappings</summary>
		private void InitStageAutoItemEntityMappings()
		{
			this.AddElementMapping( "StageAutoItemEntity", @"VitalExpert", @"dbo", "StageAutoItems", 16 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "Id", "StageAutoItems_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "AutoProtocolStagesId", "AutoProtocolStages_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "StageAutoItemParentId", "StageAutoItems_ParentId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "AutoItemsId", "AutoItems_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "ScanningMethodLookupId", "ScanningMethod_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "ChildsOrderTypeLookupId", "ChildsOrderType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "ChildsScanningTypeLookupId", "ChildsScanningType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "Order", "StageAutoItems_Order", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "ScansNumber", "StageAutoItems_ScansNumber", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "CreationDateTime", "StageAutoItems_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "UpdatedDateTime", "StageAutoItems_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "MatchesNumber", "StageAutoItems_MatchesNumber", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 12 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "FinishAllScanRounds", "StageAutoItems_FinishAllScanRounds", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 13 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "DirectAccessChecks", "StageAutoItems_DirectAccessChecks", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 14 );
			this.AddElementFieldMapping( "StageAutoItemEntity", "TestingPointsId", "TestingPoints_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
		}
		/// <summary>Inits TestEntity's mappings</summary>
		private void InitTestEntityMappings()
		{
			this.AddElementMapping( "TestEntity", @"VitalExpert", @"dbo", "Tests", 18 );
			this.AddElementFieldMapping( "TestEntity", "Id", "Test_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestEntity", "PatientId", "Patient_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestEntity", "TestScheduleId", "Test_Schedule_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "TestEntity", "Name", "Test_Name", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "TestEntity", "PointsGroupId", "Item_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "TestEntity", "TestProtocolId", "Test_Protocol_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "TestEntity", "DateTime", "Test_DateTime", true, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "TestEntity", "Description", "Test_Description", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "TestEntity", "TestTypeLookupId", "TestType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "TestEntity", "ListPointLookupId", "ListPoints_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "TestEntity", "TestStateLookupId", "TestState_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
			this.AddElementFieldMapping( "TestEntity", "Notes", "Test_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "TestEntity", "NumberOfIssues", "Test_NumberOfIssues", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 12 );
			this.AddElementFieldMapping( "TestEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 13 );
			this.AddElementFieldMapping( "TestEntity", "CreationDateTime", "Test_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 14 );
			this.AddElementFieldMapping( "TestEntity", "UpdatedDateTime", "Test_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 15 );
			this.AddElementFieldMapping( "TestEntity", "EvalPeriodChecked", "Test_EvalPeriodChecked", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 16 );
			this.AddElementFieldMapping( "TestEntity", "IsOrderSent", "Test_IsOrderSent", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 17 );
		}
		/// <summary>Inits TestImprintableItemEntity's mappings</summary>
		private void InitTestImprintableItemEntityMappings()
		{
			this.AddElementMapping( "TestImprintableItemEntity", @"VitalExpert", @"dbo", "Test_ImprintableItems", 12 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "Id", "Imprintable_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "TestId", "Test_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "ParentImprintableId", "ParentImprintable_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "TestResultId", "Test_Result_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "IsChecked", "Imprintable_IsChecked", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "IsImprinted", "Imprintable_IsImprinted", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 6 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "Order", "Imprintable_Order", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "Comments", "Imprintable_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 8 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "CreationDateTime", "Imprintable_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "TestImprintableItemEntity", "UpdatedDateTime", "Imprintable_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
		}
		/// <summary>Inits TestingPointEntity's mappings</summary>
		private void InitTestingPointEntityMappings()
		{
			this.AddElementMapping( "TestingPointEntity", @"VitalExpert", @"dbo", "TestingPoints", 9 );
			this.AddElementFieldMapping( "TestingPointEntity", "Id", "TestingPoints_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestingPointEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestingPointEntity", "Key", "TestingPoints_Key", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "TestingPointEntity", "Name", "TestingPoints_Name", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "TestingPointEntity", "FullName", "TestingPoints_FullName", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "TestingPointEntity", "Hwidentifier", "TestingPoints_HWIdentifier", false, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "TestingPointEntity", "Description", "TestingPoints_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "TestingPointEntity", "CreationDateTime", "TestingPoints_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
			this.AddElementFieldMapping( "TestingPointEntity", "UpdatedDateTime", "TestingPoints_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
		}
		/// <summary>Inits TestIssueEntity's mappings</summary>
		private void InitTestIssueEntityMappings()
		{
			this.AddElementMapping( "TestIssueEntity", @"VitalExpert", @"dbo", "Test_Issues", 9 );
			this.AddElementFieldMapping( "TestIssueEntity", "Id", "Test_Issue_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestIssueEntity", "TestId", "Test_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestIssueEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "TestIssueEntity", "ProtocolStepId", "Protocol_Step_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "TestIssueEntity", "Name", "Test_Issue_Name", true, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "TestIssueEntity", "CreationDateTime", "Test_Issue_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
			this.AddElementFieldMapping( "TestIssueEntity", "UpdatedDateTime", "Test_Issue_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "TestIssueEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "TestIssueEntity", "IsMainIssue", "Test_Issue_IsMainIssue", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 8 );
		}
		/// <summary>Inits TestProtocolEntity's mappings</summary>
		private void InitTestProtocolEntityMappings()
		{
			this.AddElementMapping( "TestProtocolEntity", @"VitalExpert", @"dbo", "Test_Protocols", 6 );
			this.AddElementFieldMapping( "TestProtocolEntity", "Id", "Test_Protocol_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestProtocolEntity", "Name", "Test_Protocol_Name", false, "NVarChar", 250, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "TestProtocolEntity", "Description", "Test_Protocol_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 2 );
			this.AddElementFieldMapping( "TestProtocolEntity", "CreationDateTime", "Test_Protocol_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "TestProtocolEntity", "UpdatedDateTime", "Test_Protocol_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "TestProtocolEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
		}
		/// <summary>Inits TestResultEntity's mappings</summary>
		private void InitTestResultEntityMappings()
		{
			this.AddElementMapping( "TestResultEntity", @"VitalExpert", @"dbo", "Test_Results", 16 );
			this.AddElementFieldMapping( "TestResultEntity", "Id", "Test_Result_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestResultEntity", "IssueId", "Test_Issue_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestResultEntity", "ItemId", "Item_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "TestResultEntity", "DateTime", "Test_Result_DateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "TestResultEntity", "ParentId", "Test_Result_Parent_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "TestResultEntity", "SelectedParentId", "Test_Result_Selected_Parent_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "TestResultEntity", "VitalForceId", "Item_Vital_Force_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "TestResultEntity", "StepTypeLookupId", "Step_Type_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "TestResultEntity", "TestProtocolId", "Test_Protocol_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "TestResultEntity", "IsSelected", "Test_Result_Is_Selected", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 9 );
			this.AddElementFieldMapping( "TestResultEntity", "IsCurrent", "Test_Result_Is_Current", false, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 10 );
			this.AddElementFieldMapping( "TestResultEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 11 );
			this.AddElementFieldMapping( "TestResultEntity", "CreationDateTime", "Test_Result_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 12 );
			this.AddElementFieldMapping( "TestResultEntity", "UpdatedDateTime", "Test_Result_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 13 );
			this.AddElementFieldMapping( "TestResultEntity", "IsImprinted", "Test_Result_IsImprinted", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 14 );
			this.AddElementFieldMapping( "TestResultEntity", "ItemRatioId", "Item_Ratio_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
		}
		/// <summary>Inits TestResultFactorsEntity's mappings</summary>
		private void InitTestResultFactorsEntityMappings()
		{
			this.AddElementMapping( "TestResultFactorsEntity", @"VitalExpert", @"dbo", "Test_Result_Factors", 8 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "Id", "Test_Result_Factor_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "FactorItemId", "Factor_Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "TestResultId", "Test_Result_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "Reading", "Reading", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "PotencyItemId", "Potency_Item_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "CreationDateTime", "Test_Result_Factor_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 6 );
			this.AddElementFieldMapping( "TestResultFactorsEntity", "UpdatedDateTime", "Test_Result_Factor_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 7 );
		}
		/// <summary>Inits TestScheduleEntity's mappings</summary>
		private void InitTestScheduleEntityMappings()
		{
			this.AddElementMapping( "TestScheduleEntity", @"VitalExpert", @"dbo", "Test_Schedules", 16 );
			this.AddElementFieldMapping( "TestScheduleEntity", "Id", "Test_Schedule_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestScheduleEntity", "Tax", "Test_Schedule_Tax", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 1 );
			this.AddElementFieldMapping( "TestScheduleEntity", "IsCash", "Test_Schedule_IsCash", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 2 );
			this.AddElementFieldMapping( "TestScheduleEntity", "IsCheck", "Test_Schedule_IsCheck", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 3 );
			this.AddElementFieldMapping( "TestScheduleEntity", "IsCreditCard", "Test_Schedule_IsCreditCard", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 4 );
			this.AddElementFieldMapping( "TestScheduleEntity", "ReevalInWeeks", "Test_Schedule_ReevalInWeeks", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "TestScheduleEntity", "Notes", "Test_Schedule_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "TestScheduleEntity", "SpecialInstructions", "Test_Schedule_SpecialInstructions", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "TestScheduleEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "TestScheduleEntity", "CreationDateTime", "Test_Schedule_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "TestScheduleEntity", "UpdatedDateTime", "Test_Schedule_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "TestScheduleEntity", "EvalPeriodTypeLookupId", "EvalPeriodType_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 11 );
			this.AddElementFieldMapping( "TestScheduleEntity", "CheckNumber", "Test_Schedule_CheckNumber", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 12 );
			this.AddElementFieldMapping( "TestScheduleEntity", "Discount", "Test_Schedule_Discount", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 13 );
			this.AddElementFieldMapping( "TestScheduleEntity", "DiscountAsPercentage", "Test_Schedule_DiscountAsPercentage", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 14 );
			this.AddElementFieldMapping( "TestScheduleEntity", "DiscountApplyLookupId", "DiscountApply_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
		}
		/// <summary>Inits TestServiceEntity's mappings</summary>
		private void InitTestServiceEntityMappings()
		{
			this.AddElementMapping( "TestServiceEntity", @"VitalExpert", @"dbo", "Test_Services", 12 );
			this.AddElementFieldMapping( "TestServiceEntity", "Id", "TestService_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "TestServiceEntity", "TestId", "Test_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "TestServiceEntity", "ServiceId", "Service_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "TestServiceEntity", "Key", "TestService_Key", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 3 );
			this.AddElementFieldMapping( "TestServiceEntity", "Name", "TestService_Name", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "TestServiceEntity", "Description", "TestService_Description", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "TestServiceEntity", "Comments", "TestService_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "TestServiceEntity", "Price", "TestService_Price", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 7 );
			this.AddElementFieldMapping( "TestServiceEntity", "TypeLookupId", "TestServiceType_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 8 );
			this.AddElementFieldMapping( "TestServiceEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 9 );
			this.AddElementFieldMapping( "TestServiceEntity", "CreationDateTime", "TestService_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 10 );
			this.AddElementFieldMapping( "TestServiceEntity", "UpdatedDateTime", "TestService_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
		}
		/// <summary>Inits UserEntity's mappings</summary>
		private void InitUserEntityMappings()
		{
			this.AddElementMapping( "UserEntity", @"VitalExpert", @"dbo", "Users", 2 );
			this.AddElementFieldMapping( "UserEntity", "Id", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "UserEntity", "Name", "User_Name", false, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 1 );
		}
		/// <summary>Inits VFSEntity's mappings</summary>
		private void InitVFSEntityMappings()
		{
			this.AddElementMapping( "VFSEntity", @"VitalExpert", @"dbo", "VFS", 12 );
			this.AddElementFieldMapping( "VFSEntity", "Id", "VFS_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "VFSEntity", "Name", "VFS_Name", false, "NVarChar", 50, 0, 0, false, "", null, typeof(System.String), 1 );
			this.AddElementFieldMapping( "VFSEntity", "TestId", "Test_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "VFSEntity", "DateTime", "VFS_DateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 3 );
			this.AddElementFieldMapping( "VFSEntity", "ThyroidNumOfIssues", "VFS_ThyroidNumOfIssues", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 4 );
			this.AddElementFieldMapping( "VFSEntity", "MercuryNumOfIssues", "VFS_MercuryNumOfIssues", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 5 );
			this.AddElementFieldMapping( "VFSEntity", "EmotionalIssues", "VFS_EmotionalIssues", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "VFSEntity", "Notes", "VFS_Notes", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "VFSEntity", "CreationDateTime", "VFS_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
			this.AddElementFieldMapping( "VFSEntity", "UpdatedDateTime", "VFS_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
			this.AddElementFieldMapping( "VFSEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
			this.AddElementFieldMapping( "VFSEntity", "PatientId", "Patient_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 11 );
		}
		/// <summary>Inits VFSItemEntity's mappings</summary>
		private void InitVFSItemEntityMappings()
		{
			this.AddElementMapping( "VFSItemEntity", @"VitalExpert", @"dbo", "VFSItems", 16 );
			this.AddElementFieldMapping( "VFSItemEntity", "Id", "VFSItem_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "VFSItemEntity", "VFSId", "VFS_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "VFSItemEntity", "VFSitemSourceId", "VFSItemSource_Id", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "VFSItemEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "VFSItemEntity", "PreviousV1", "VFSItem_Previous_V1", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "VFSItemEntity", "PreviousV2", "VFSItem_Previous_V2", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 5 );
			this.AddElementFieldMapping( "VFSItemEntity", "CurrentV1", "VFSItem_Current_V1", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 6 );
			this.AddElementFieldMapping( "VFSItemEntity", "CurrentV2", "VFSItem_Current_V2", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 7 );
			this.AddElementFieldMapping( "VFSItemEntity", "IsSkipped", "VFSItem_IsSkipped", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 8 );
			this.AddElementFieldMapping( "VFSItemEntity", "Comments", "VFSItem_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 9 );
			this.AddElementFieldMapping( "VFSItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 10 );
			this.AddElementFieldMapping( "VFSItemEntity", "CreationDateTime", "VFSItem_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 11 );
			this.AddElementFieldMapping( "VFSItemEntity", "UpdatedDateTime", "VFSItem_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 12 );
			this.AddElementFieldMapping( "VFSItemEntity", "GroupLookupId", "Group_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 13 );
			this.AddElementFieldMapping( "VFSItemEntity", "SectionLookupId", "Section_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 14 );
			this.AddElementFieldMapping( "VFSItemEntity", "GridGroupLookupId", "GridGroup_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 15 );
		}
		/// <summary>Inits VFSItemSourceEntity's mappings</summary>
		private void InitVFSItemSourceEntityMappings()
		{
			this.AddElementMapping( "VFSItemSourceEntity", @"VitalExpert", @"dbo", "VFSItemsSource", 29 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "Id", "VFSItemSource_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V1Min", "VFSItemSource_V1_Min", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 2 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V1Max", "VFSItemSource_V1_Max", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 3 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V1MinIdeal", "VFSItemSource_V1_Min_Ideal", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 4 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V1MaxIdeal", "VFSItemSource_V1_Max_Ideal", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 5 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V2Min", "VFSItemSource_V2_Min", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 6 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V2Max", "VFSItemSource_V2_Max", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 7 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V2MinIdeal", "VFSItemSource_V2_Min_Ideal", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 8 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V2MaxIdeal", "VFSItemSource_V2_Max_Ideal", true, "Decimal", 0, 4, 12, false, "", null, typeof(System.Decimal), 9 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "StartingValue1", "VFSItemSource_StartingValue1", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 10 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "StartingValue2", "VFSItemSource_StartingValue2", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 11 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "IsActive", "VFSItemSource_IsActive", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 12 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V1TypeLookupId", "V1Type_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 13 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V2TypeLookupId", "V2Type_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 14 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V1LookupType", "VFSItemSource_V1LookupType", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 15 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "V2LookupType", "VFSItemSource_V2LookupType", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 16 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "SectionLookupId", "Section_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 17 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "GroupLookupId", "Group_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 18 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "GenderLookupId", "Gender_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 19 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "TestingOrder", "VFSItemSource_Testing_Order", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 20 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "HasPreviousV1", "VFSItemSource_HasPreviousV1", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 21 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "HasPreviousV2", "VFSItemSource_HasPreviousV2", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 22 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "HasCurrentV1", "VFSItemSource_HasCurrentV1", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 23 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "HasCurrentV2", "VFSItemSource_HasCurrentV2", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 24 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 25 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "CreationDateTime", "VFSItemSource_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 26 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "UpdatedDateTime", "VFSItemSource_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 27 );
			this.AddElementFieldMapping( "VFSItemSourceEntity", "GridGroupLookupId", "GridGroup_LookupId", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 28 );
		}
		/// <summary>Inits VFSSecondaryItemEntity's mappings</summary>
		private void InitVFSSecondaryItemEntityMappings()
		{
			this.AddElementMapping( "VFSSecondaryItemEntity", @"VitalExpert", @"dbo", "VFSSecondaryItems", 10 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "Id", "VFSSI_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "VfsId", "VFS_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "SectionLookupId", "Section_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "Comments", "VFSSI_Comments", true, "NVarChar", 2147483647, 0, 0, false, "", null, typeof(System.String), 4 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "Checked", "VFSSI_Checked", true, "Bit", 0, 0, 0, false, "", null, typeof(System.Boolean), 5 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "Order", "VFSSI_Order", true, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 6 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 7 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "CreationDateTime", "VFSSI_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 8 );
			this.AddElementFieldMapping( "VFSSecondaryItemEntity", "UpdatedDateTime", "VFSSI_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 9 );
		}
		/// <summary>Inits VFSSecondaryItemSourceEntity's mappings</summary>
		private void InitVFSSecondaryItemSourceEntityMappings()
		{
			this.AddElementMapping( "VFSSecondaryItemSourceEntity", @"VitalExpert", @"dbo", "VFSSecondaryItemsSource", 6 );
			this.AddElementFieldMapping( "VFSSecondaryItemSourceEntity", "Id", "VFSSIS_Id", false, "Int", 0, 0, 10, true, "SCOPE_IDENTITY()", null, typeof(System.Int32), 0 );
			this.AddElementFieldMapping( "VFSSecondaryItemSourceEntity", "ItemId", "Item_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 1 );
			this.AddElementFieldMapping( "VFSSecondaryItemSourceEntity", "SectionLookupId", "Section_LookupId", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 2 );
			this.AddElementFieldMapping( "VFSSecondaryItemSourceEntity", "UserId", "User_Id", false, "Int", 0, 0, 10, false, "", null, typeof(System.Int32), 3 );
			this.AddElementFieldMapping( "VFSSecondaryItemSourceEntity", "CreationDateTime", "VFSSIS_CreationDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 4 );
			this.AddElementFieldMapping( "VFSSecondaryItemSourceEntity", "UpdatedDateTime", "VFSSIS_UpdatedDateTime", false, "DateTime", 0, 0, 0, false, "", null, typeof(System.DateTime), 5 );
		}

	}
}