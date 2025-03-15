///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET35
// Templates vendor: Solutions Design.
////////////////////////////////////////////////////////////// 
using System;
using Vital.DataLayer.EntityClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;

namespace Vital.DataLayer.FactoryClasses
{
	/// <summary>Factory class to produce DynamicQuery instances and EntityQuery instances</summary>
	public partial class QueryFactory
	{
		private int _aliasCounter = 0;

		/// <summary>Creates a new DynamicQuery instance with no alias set.</summary>
		/// <returns>Ready to use DynamicQuery instance</returns>
		public DynamicQuery Create()
		{
			return Create(string.Empty);
		}

		/// <summary>Creates a new DynamicQuery instance with the alias specified as the alias set.</summary>
		/// <param name="alias">The alias.</param>
		/// <returns>Ready to use DynamicQuery instance</returns>
		public DynamicQuery Create(string alias)
		{
			return new DynamicQuery(new ElementCreator(), alias, this.GetNextAliasCounterValue());
		}
	
		/// <summary>Creates a new EntityQuery for the entity of the type specified with no alias set.</summary>
		/// <typeparam name="TEntity">The type of the entity to produce the query for.</typeparam>
		/// <returns>ready to use EntityQuery instance</returns>
		public EntityQuery<TEntity> Create<TEntity>()
			where TEntity : IEntityCore
		{
			return Create<TEntity>(string.Empty);
		}

		/// <summary>Creates a new EntityQuery for the entity of the type specified with the alias specified as the alias set.</summary>
		/// <typeparam name="TEntity">The type of the entity to produce the query for.</typeparam>
		/// <param name="alias">The alias.</param>
		/// <returns>ready to use EntityQuery instance</returns>
		public EntityQuery<TEntity> Create<TEntity>(string alias)
			where TEntity : IEntityCore
		{
			return new EntityQuery<TEntity>(new ElementCreator(), alias, this.GetNextAliasCounterValue());
		}
				
		/// <summary>Creates a new field object with the name specified and of resulttype 'object'. Used for referring to aliased fields in another projection.</summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>Ready to use field object</returns>
		public EntityField2 Field(string fieldName)
		{
			return Field<object>(string.Empty, fieldName);
		}

		/// <summary>Creates a new field object with the name specified and of resulttype 'object'. Used for referring to aliased fields in another projection.</summary>
		/// <param name="targetAlias">The alias of the table/query to target.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>Ready to use field object</returns>
		public EntityField2 Field(string targetAlias, string fieldName)
		{
			return Field<object>(targetAlias, fieldName);
		}

		/// <summary>Creates a new field object with the name specified and of resulttype 'TValue'. Used for referring to aliased fields in another projection.</summary>
		/// <typeparam name="TValue">The type of the value represented by the field.</typeparam>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>Ready to use field object</returns>
		public EntityField2 Field<TValue>(string fieldName)
		{
			return Field<TValue>(string.Empty, fieldName);
		}

		/// <summary>Creates a new field object with the name specified and of resulttype 'TValue'. Used for referring to aliased fields in another projection.</summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="targetAlias">The alias of the table/query to target.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>Ready to use field object</returns>
		public EntityField2 Field<TValue>(string targetAlias, string fieldName)
		{
			return new EntityField2(fieldName, targetAlias, typeof(TValue));
		}
		
		/// <summary>Gets the next alias counter value to produce artifical aliases with</summary>
		private int GetNextAliasCounterValue()
		{
			_aliasCounter++;
			return _aliasCounter;
		}
		
		/// <summary>Creates and returns a new EntityQuery for the AppImage entity</summary>
		public EntityQuery<AppImageEntity> AppImage
		{
			get { return Create<AppImageEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AppInfo entity</summary>
		public EntityQuery<AppInfoEntity> AppInfo
		{
			get { return Create<AppInfoEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoItem entity</summary>
		public EntityQuery<AutoItemEntity> AutoItem
		{
			get { return Create<AutoItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoItemRelation entity</summary>
		public EntityQuery<AutoItemRelationEntity> AutoItemRelation
		{
			get { return Create<AutoItemRelationEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoProtocol entity</summary>
		public EntityQuery<AutoProtocolEntity> AutoProtocol
		{
			get { return Create<AutoProtocolEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoProtocolRevision entity</summary>
		public EntityQuery<AutoProtocolRevisionEntity> AutoProtocolRevision
		{
			get { return Create<AutoProtocolRevisionEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoProtocolStage entity</summary>
		public EntityQuery<AutoProtocolStageEntity> AutoProtocolStage
		{
			get { return Create<AutoProtocolStageEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoProtocolStageRevision entity</summary>
		public EntityQuery<AutoProtocolStageRevisionEntity> AutoProtocolStageRevision
		{
			get { return Create<AutoProtocolStageRevisionEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoTest entity</summary>
		public EntityQuery<AutoTestEntity> AutoTest
		{
			get { return Create<AutoTestEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoTestResult entity</summary>
		public EntityQuery<AutoTestResultEntity> AutoTestResult
		{
			get { return Create<AutoTestResultEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoTestResultProduct entity</summary>
		public EntityQuery<AutoTestResultProductEntity> AutoTestResultProduct
		{
			get { return Create<AutoTestResultProductEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the AutoTestStage entity</summary>
		public EntityQuery<AutoTestStageEntity> AutoTestStage
		{
			get { return Create<AutoTestStageEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ClinicProductPricing entity</summary>
		public EntityQuery<ClinicProductPricingEntity> ClinicProductPricing
		{
			get { return Create<ClinicProductPricingEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the DosageOption entity</summary>
		public EntityQuery<DosageOptionEntity> DosageOption
		{
			get { return Create<DosageOptionEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the FrequencyTest entity</summary>
		public EntityQuery<FrequencyTestEntity> FrequencyTest
		{
			get { return Create<FrequencyTestEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the FrequencyTestResult entity</summary>
		public EntityQuery<FrequencyTestResultEntity> FrequencyTestResult
		{
			get { return Create<FrequencyTestResultEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the HwProfile entity</summary>
		public EntityQuery<HwProfileEntity> HwProfile
		{
			get { return Create<HwProfileEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Image entity</summary>
		public EntityQuery<ImageEntity> Image
		{
			get { return Create<ImageEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Invoice entity</summary>
		public EntityQuery<InvoiceEntity> Invoice
		{
			get { return Create<InvoiceEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the IssueNavigationStep entity</summary>
		public EntityQuery<IssueNavigationStepEntity> IssueNavigationStep
		{
			get { return Create<IssueNavigationStepEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Item entity</summary>
		public EntityQuery<ItemEntity> Item
		{
			get { return Create<ItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ItemDetails entity</summary>
		public EntityQuery<ItemDetailsEntity> ItemDetails
		{
			get { return Create<ItemDetailsEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ItemProperty entity</summary>
		public EntityQuery<ItemPropertyEntity> ItemProperty
		{
			get { return Create<ItemPropertyEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ItemRelation entity</summary>
		public EntityQuery<ItemRelationEntity> ItemRelation
		{
			get { return Create<ItemRelationEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ItemRelationProperty entity</summary>
		public EntityQuery<ItemRelationPropertyEntity> ItemRelationProperty
		{
			get { return Create<ItemRelationPropertyEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ItemTarget entity</summary>
		public EntityQuery<ItemTargetEntity> ItemTarget
		{
			get { return Create<ItemTargetEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Lookup entity</summary>
		public EntityQuery<LookupEntity> Lookup
		{
			get { return Create<LookupEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the OrderItem entity</summary>
		public EntityQuery<OrderItemEntity> OrderItem
		{
			get { return Create<OrderItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Patient entity</summary>
		public EntityQuery<PatientEntity> Patient
		{
			get { return Create<PatientEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the PatientHistory entity</summary>
		public EntityQuery<PatientHistoryEntity> PatientHistory
		{
			get { return Create<PatientHistoryEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Product entity</summary>
		public EntityQuery<ProductEntity> Product
		{
			get { return Create<ProductEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ProductForm entity</summary>
		public EntityQuery<ProductFormEntity> ProductForm
		{
			get { return Create<ProductFormEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ProductSize entity</summary>
		public EntityQuery<ProductSizeEntity> ProductSize
		{
			get { return Create<ProductSizeEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Property entity</summary>
		public EntityQuery<PropertyEntity> Property
		{
			get { return Create<PropertyEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ProtocolItem entity</summary>
		public EntityQuery<ProtocolItemEntity> ProtocolItem
		{
			get { return Create<ProtocolItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ProtocolStep entity</summary>
		public EntityQuery<ProtocolStepEntity> ProtocolStep
		{
			get { return Create<ProtocolStepEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Reading entity</summary>
		public EntityQuery<ReadingEntity> Reading
		{
			get { return Create<ReadingEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ScheduleLine entity</summary>
		public EntityQuery<ScheduleLineEntity> ScheduleLine
		{
			get { return Create<ScheduleLineEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Service entity</summary>
		public EntityQuery<ServiceEntity> Service
		{
			get { return Create<ServiceEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Setting entity</summary>
		public EntityQuery<SettingEntity> Setting
		{
			get { return Create<SettingEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the ShippingOrder entity</summary>
		public EntityQuery<ShippingOrderEntity> ShippingOrder
		{
			get { return Create<ShippingOrderEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the SpotCheck entity</summary>
		public EntityQuery<SpotCheckEntity> SpotCheck
		{
			get { return Create<SpotCheckEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the SpotCheckResult entity</summary>
		public EntityQuery<SpotCheckResultEntity> SpotCheckResult
		{
			get { return Create<SpotCheckResultEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the StageAnnouncement entity</summary>
		public EntityQuery<StageAnnouncementEntity> StageAnnouncement
		{
			get { return Create<StageAnnouncementEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the StageAutoItem entity</summary>
		public EntityQuery<StageAutoItemEntity> StageAutoItem
		{
			get { return Create<StageAutoItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the Test entity</summary>
		public EntityQuery<TestEntity> Test
		{
			get { return Create<TestEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestImprintableItem entity</summary>
		public EntityQuery<TestImprintableItemEntity> TestImprintableItem
		{
			get { return Create<TestImprintableItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestingPoint entity</summary>
		public EntityQuery<TestingPointEntity> TestingPoint
		{
			get { return Create<TestingPointEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestIssue entity</summary>
		public EntityQuery<TestIssueEntity> TestIssue
		{
			get { return Create<TestIssueEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestProtocol entity</summary>
		public EntityQuery<TestProtocolEntity> TestProtocol
		{
			get { return Create<TestProtocolEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestResult entity</summary>
		public EntityQuery<TestResultEntity> TestResult
		{
			get { return Create<TestResultEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestResultFactors entity</summary>
		public EntityQuery<TestResultFactorsEntity> TestResultFactors
		{
			get { return Create<TestResultFactorsEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestSchedule entity</summary>
		public EntityQuery<TestScheduleEntity> TestSchedule
		{
			get { return Create<TestScheduleEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the TestService entity</summary>
		public EntityQuery<TestServiceEntity> TestService
		{
			get { return Create<TestServiceEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the User entity</summary>
		public EntityQuery<UserEntity> User
		{
			get { return Create<UserEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the VFS entity</summary>
		public EntityQuery<VFSEntity> VFS
		{
			get { return Create<VFSEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the VFSItem entity</summary>
		public EntityQuery<VFSItemEntity> VFSItem
		{
			get { return Create<VFSItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the VFSItemSource entity</summary>
		public EntityQuery<VFSItemSourceEntity> VFSItemSource
		{
			get { return Create<VFSItemSourceEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the VFSSecondaryItem entity</summary>
		public EntityQuery<VFSSecondaryItemEntity> VFSSecondaryItem
		{
			get { return Create<VFSSecondaryItemEntity>(); }
		}

		/// <summary>Creates and returns a new EntityQuery for the VFSSecondaryItemSource entity</summary>
		public EntityQuery<VFSSecondaryItemSourceEntity> VFSSecondaryItemSource
		{
			get { return Create<VFSSecondaryItemSourceEntity>(); }
		}

	}
}