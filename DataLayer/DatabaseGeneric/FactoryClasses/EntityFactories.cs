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
using System.Collections.Generic;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.HelperClasses;
using Vital.DataLayer.RelationClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Vital.DataLayer.FactoryClasses
{
	
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	
	/// <summary>general base class for the generated factories</summary>
	[Serializable]
	public partial class EntityFactoryBase2<TEntity> : EntityFactoryCore2
		where TEntity : EntityBase2, IEntity2
	{
		private readonly Vital.DataLayer.EntityType _typeOfEntity;
		private readonly bool _isInHierarchy;
		
		/// <summary>CTor</summary>
		/// <param name="entityName">Name of the entity.</param>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <param name="isInHierarchy">If true, the entity of this factory is in an inheritance hierarchy, false otherwise</param>
		public EntityFactoryBase2(string entityName, Vital.DataLayer.EntityType typeOfEntity, bool isInHierarchy) : base(entityName)
		{
			_typeOfEntity = typeOfEntity;
			_isInHierarchy = isInHierarchy;
		}
		
		/// <summary>Creates, using the generated EntityFieldsFactory, the IEntityFields2 object for the entity to create.</summary>
		/// <returns>Empty IEntityFields2 object.</returns>
		public override IEntityFields2 CreateFields()
		{
			return EntityFieldsFactory.CreateEntityFieldsObject(_typeOfEntity);
		}
		
		/// <summary>Creates a new entity instance using the GeneralEntityFactory in the generated code, using the passed in entitytype value</summary>
		/// <param name="entityTypeValue">The entity type value of the entity to create an instance for.</param>
		/// <returns>new IEntity instance</returns>
		public override IEntity2 CreateEntityFromEntityTypeValue(int entityTypeValue)
		{
			return GeneralEntityFactory.Create((Vital.DataLayer.EntityType)entityTypeValue);
		}

		/// <summary>Creates the relations collection to the entity to join all targets so this entity can be fetched. </summary>
		/// <param name="objectAlias">The object alias to use for the elements in the relations.</param>
		/// <returns>null if the entity isn't in a hierarchy of type TargetPerEntity, otherwise the relations collection needed to join all targets together to fetch all subtypes of this entity and this entity itself</returns>
		public override IRelationCollection CreateHierarchyRelations(string objectAlias) 
		{
			return InheritanceInfoProviderSingleton.GetInstance().GetHierarchyRelations(this.ForEntityName, objectAlias);
		}

		/// <summary>This method retrieves, using the InheritanceInfoprovider, the factory for the entity represented by the values passed in.</summary>
		/// <param name="fieldValues">Field values read from the db, to determine which factory to return, based on the field values passed in.</param>
		/// <param name="entityFieldStartIndexesPerEntity">indexes into values where per entity type their own fields start.</param>
		/// <returns>the factory for the entity which is represented by the values passed in.</returns>
		public override IEntityFactory2 GetEntityFactory(object[] fieldValues, Dictionary<string, int> entityFieldStartIndexesPerEntity) 
		{
			IEntityFactory2 toReturn = (IEntityFactory2)InheritanceInfoProviderSingleton.GetInstance().GetEntityFactory(this.ForEntityName, fieldValues, entityFieldStartIndexesPerEntity);
			if(toReturn == null)
			{
				toReturn = this;
			}
			return toReturn;
		}
		
		/// <summary>Gets a predicateexpression which filters on the entity with type belonging to this factory.</summary>
		/// <param name="negate">Flag to produce a NOT filter, (true), or a normal filter (false). </param>
		/// <param name="objectAlias">The object alias to use for the predicate(s).</param>
		/// <returns>ready to use predicateexpression, or an empty predicate expression if the belonging entity isn't a hierarchical type.</returns>
		public override IPredicateExpression GetEntityTypeFilter(bool negate, string objectAlias) 
		{
			return InheritanceInfoProviderSingleton.GetInstance().GetEntityTypeFilter(this.ForEntityName, objectAlias, negate);
		}
						
		/// <summary>Creates a new generic EntityCollection(Of T) for the entity which this factory belongs to.</summary>
		/// <returns>ready to use generic EntityCollection(Of T) with this factory set as the factory</returns>
		public override IEntityCollection2 CreateEntityCollection()
		{
			return new EntityCollection<TEntity>(this);
		}
		
		/// <summary>Creates the hierarchy fields for the entity to which this factory belongs.</summary>
		/// <returns>IEntityFields2 object with the fields of all the entities in teh hierarchy of this entity or the fields of this entity if the entity isn't in a hierarchy.</returns>
		public override IEntityFields2 CreateHierarchyFields() 
		{
			return _isInHierarchy ? new EntityFields2(InheritanceInfoProviderSingleton.GetInstance().GetHierarchyFields(this.ForEntityName), InheritanceInfoProviderSingleton.GetInstance(), null) : base.CreateHierarchyFields();
		}
	}

	/// <summary>Factory to create new, empty AppImageEntity objects.</summary>
	[Serializable]
	public partial class AppImageEntityFactory : EntityFactoryBase2<AppImageEntity> {
		/// <summary>CTor</summary>
		public AppImageEntityFactory() : base("AppImageEntity", Vital.DataLayer.EntityType.AppImageEntity, false) { }
		
		/// <summary>Creates a new AppImageEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AppImageEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAppImageUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AppInfoEntity objects.</summary>
	[Serializable]
	public partial class AppInfoEntityFactory : EntityFactoryBase2<AppInfoEntity> {
		/// <summary>CTor</summary>
		public AppInfoEntityFactory() : base("AppInfoEntity", Vital.DataLayer.EntityType.AppInfoEntity, false) { }
		
		/// <summary>Creates a new AppInfoEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AppInfoEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAppInfoUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoItemEntity objects.</summary>
	[Serializable]
	public partial class AutoItemEntityFactory : EntityFactoryBase2<AutoItemEntity> {
		/// <summary>CTor</summary>
		public AutoItemEntityFactory() : base("AutoItemEntity", Vital.DataLayer.EntityType.AutoItemEntity, false) { }
		
		/// <summary>Creates a new AutoItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoItemRelationEntity objects.</summary>
	[Serializable]
	public partial class AutoItemRelationEntityFactory : EntityFactoryBase2<AutoItemRelationEntity> {
		/// <summary>CTor</summary>
		public AutoItemRelationEntityFactory() : base("AutoItemRelationEntity", Vital.DataLayer.EntityType.AutoItemRelationEntity, false) { }
		
		/// <summary>Creates a new AutoItemRelationEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoItemRelationEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoItemRelationUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoProtocolEntity objects.</summary>
	[Serializable]
	public partial class AutoProtocolEntityFactory : EntityFactoryBase2<AutoProtocolEntity> {
		/// <summary>CTor</summary>
		public AutoProtocolEntityFactory() : base("AutoProtocolEntity", Vital.DataLayer.EntityType.AutoProtocolEntity, false) { }
		
		/// <summary>Creates a new AutoProtocolEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoProtocolEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoProtocolUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoProtocolRevisionEntity objects.</summary>
	[Serializable]
	public partial class AutoProtocolRevisionEntityFactory : EntityFactoryBase2<AutoProtocolRevisionEntity> {
		/// <summary>CTor</summary>
		public AutoProtocolRevisionEntityFactory() : base("AutoProtocolRevisionEntity", Vital.DataLayer.EntityType.AutoProtocolRevisionEntity, false) { }
		
		/// <summary>Creates a new AutoProtocolRevisionEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoProtocolRevisionEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoProtocolRevisionUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoProtocolStageEntity objects.</summary>
	[Serializable]
	public partial class AutoProtocolStageEntityFactory : EntityFactoryBase2<AutoProtocolStageEntity> {
		/// <summary>CTor</summary>
		public AutoProtocolStageEntityFactory() : base("AutoProtocolStageEntity", Vital.DataLayer.EntityType.AutoProtocolStageEntity, false) { }
		
		/// <summary>Creates a new AutoProtocolStageEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoProtocolStageEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoProtocolStageUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoProtocolStageRevisionEntity objects.</summary>
	[Serializable]
	public partial class AutoProtocolStageRevisionEntityFactory : EntityFactoryBase2<AutoProtocolStageRevisionEntity> {
		/// <summary>CTor</summary>
		public AutoProtocolStageRevisionEntityFactory() : base("AutoProtocolStageRevisionEntity", Vital.DataLayer.EntityType.AutoProtocolStageRevisionEntity, false) { }
		
		/// <summary>Creates a new AutoProtocolStageRevisionEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoProtocolStageRevisionEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoProtocolStageRevisionUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoTestEntity objects.</summary>
	[Serializable]
	public partial class AutoTestEntityFactory : EntityFactoryBase2<AutoTestEntity> {
		/// <summary>CTor</summary>
		public AutoTestEntityFactory() : base("AutoTestEntity", Vital.DataLayer.EntityType.AutoTestEntity, false) { }
		
		/// <summary>Creates a new AutoTestEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoTestEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoTestUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoTestResultEntity objects.</summary>
	[Serializable]
	public partial class AutoTestResultEntityFactory : EntityFactoryBase2<AutoTestResultEntity> {
		/// <summary>CTor</summary>
		public AutoTestResultEntityFactory() : base("AutoTestResultEntity", Vital.DataLayer.EntityType.AutoTestResultEntity, false) { }
		
		/// <summary>Creates a new AutoTestResultEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoTestResultEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoTestResultUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoTestResultProductEntity objects.</summary>
	[Serializable]
	public partial class AutoTestResultProductEntityFactory : EntityFactoryBase2<AutoTestResultProductEntity> {
		/// <summary>CTor</summary>
		public AutoTestResultProductEntityFactory() : base("AutoTestResultProductEntity", Vital.DataLayer.EntityType.AutoTestResultProductEntity, false) { }
		
		/// <summary>Creates a new AutoTestResultProductEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoTestResultProductEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoTestResultProductUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty AutoTestStageEntity objects.</summary>
	[Serializable]
	public partial class AutoTestStageEntityFactory : EntityFactoryBase2<AutoTestStageEntity> {
		/// <summary>CTor</summary>
		public AutoTestStageEntityFactory() : base("AutoTestStageEntity", Vital.DataLayer.EntityType.AutoTestStageEntity, false) { }
		
		/// <summary>Creates a new AutoTestStageEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new AutoTestStageEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewAutoTestStageUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ClinicProductPricingEntity objects.</summary>
	[Serializable]
	public partial class ClinicProductPricingEntityFactory : EntityFactoryBase2<ClinicProductPricingEntity> {
		/// <summary>CTor</summary>
		public ClinicProductPricingEntityFactory() : base("ClinicProductPricingEntity", Vital.DataLayer.EntityType.ClinicProductPricingEntity, false) { }
		
		/// <summary>Creates a new ClinicProductPricingEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ClinicProductPricingEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewClinicProductPricingUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty DosageOptionEntity objects.</summary>
	[Serializable]
	public partial class DosageOptionEntityFactory : EntityFactoryBase2<DosageOptionEntity> {
		/// <summary>CTor</summary>
		public DosageOptionEntityFactory() : base("DosageOptionEntity", Vital.DataLayer.EntityType.DosageOptionEntity, false) { }
		
		/// <summary>Creates a new DosageOptionEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new DosageOptionEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewDosageOptionUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty FrequencyTestEntity objects.</summary>
	[Serializable]
	public partial class FrequencyTestEntityFactory : EntityFactoryBase2<FrequencyTestEntity> {
		/// <summary>CTor</summary>
		public FrequencyTestEntityFactory() : base("FrequencyTestEntity", Vital.DataLayer.EntityType.FrequencyTestEntity, false) { }
		
		/// <summary>Creates a new FrequencyTestEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new FrequencyTestEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewFrequencyTestUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty FrequencyTestResultEntity objects.</summary>
	[Serializable]
	public partial class FrequencyTestResultEntityFactory : EntityFactoryBase2<FrequencyTestResultEntity> {
		/// <summary>CTor</summary>
		public FrequencyTestResultEntityFactory() : base("FrequencyTestResultEntity", Vital.DataLayer.EntityType.FrequencyTestResultEntity, false) { }
		
		/// <summary>Creates a new FrequencyTestResultEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new FrequencyTestResultEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewFrequencyTestResultUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty HwProfileEntity objects.</summary>
	[Serializable]
	public partial class HwProfileEntityFactory : EntityFactoryBase2<HwProfileEntity> {
		/// <summary>CTor</summary>
		public HwProfileEntityFactory() : base("HwProfileEntity", Vital.DataLayer.EntityType.HwProfileEntity, false) { }
		
		/// <summary>Creates a new HwProfileEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new HwProfileEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewHwProfileUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ImageEntity objects.</summary>
	[Serializable]
	public partial class ImageEntityFactory : EntityFactoryBase2<ImageEntity> {
		/// <summary>CTor</summary>
		public ImageEntityFactory() : base("ImageEntity", Vital.DataLayer.EntityType.ImageEntity, false) { }
		
		/// <summary>Creates a new ImageEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ImageEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewImageUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty InvoiceEntity objects.</summary>
	[Serializable]
	public partial class InvoiceEntityFactory : EntityFactoryBase2<InvoiceEntity> {
		/// <summary>CTor</summary>
		public InvoiceEntityFactory() : base("InvoiceEntity", Vital.DataLayer.EntityType.InvoiceEntity, false) { }
		
		/// <summary>Creates a new InvoiceEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new InvoiceEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewInvoiceUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty IssueNavigationStepEntity objects.</summary>
	[Serializable]
	public partial class IssueNavigationStepEntityFactory : EntityFactoryBase2<IssueNavigationStepEntity> {
		/// <summary>CTor</summary>
		public IssueNavigationStepEntityFactory() : base("IssueNavigationStepEntity", Vital.DataLayer.EntityType.IssueNavigationStepEntity, false) { }
		
		/// <summary>Creates a new IssueNavigationStepEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new IssueNavigationStepEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewIssueNavigationStepUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ItemEntity objects.</summary>
	[Serializable]
	public partial class ItemEntityFactory : EntityFactoryBase2<ItemEntity> {
		/// <summary>CTor</summary>
		public ItemEntityFactory() : base("ItemEntity", Vital.DataLayer.EntityType.ItemEntity, false) { }
		
		/// <summary>Creates a new ItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ItemDetailsEntity objects.</summary>
	[Serializable]
	public partial class ItemDetailsEntityFactory : EntityFactoryBase2<ItemDetailsEntity> {
		/// <summary>CTor</summary>
		public ItemDetailsEntityFactory() : base("ItemDetailsEntity", Vital.DataLayer.EntityType.ItemDetailsEntity, false) { }
		
		/// <summary>Creates a new ItemDetailsEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ItemDetailsEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewItemDetailsUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ItemPropertyEntity objects.</summary>
	[Serializable]
	public partial class ItemPropertyEntityFactory : EntityFactoryBase2<ItemPropertyEntity> {
		/// <summary>CTor</summary>
		public ItemPropertyEntityFactory() : base("ItemPropertyEntity", Vital.DataLayer.EntityType.ItemPropertyEntity, false) { }
		
		/// <summary>Creates a new ItemPropertyEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ItemPropertyEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewItemPropertyUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ItemRelationEntity objects.</summary>
	[Serializable]
	public partial class ItemRelationEntityFactory : EntityFactoryBase2<ItemRelationEntity> {
		/// <summary>CTor</summary>
		public ItemRelationEntityFactory() : base("ItemRelationEntity", Vital.DataLayer.EntityType.ItemRelationEntity, false) { }
		
		/// <summary>Creates a new ItemRelationEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ItemRelationEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewItemRelationUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ItemRelationPropertyEntity objects.</summary>
	[Serializable]
	public partial class ItemRelationPropertyEntityFactory : EntityFactoryBase2<ItemRelationPropertyEntity> {
		/// <summary>CTor</summary>
		public ItemRelationPropertyEntityFactory() : base("ItemRelationPropertyEntity", Vital.DataLayer.EntityType.ItemRelationPropertyEntity, false) { }
		
		/// <summary>Creates a new ItemRelationPropertyEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ItemRelationPropertyEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewItemRelationPropertyUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ItemTargetEntity objects.</summary>
	[Serializable]
	public partial class ItemTargetEntityFactory : EntityFactoryBase2<ItemTargetEntity> {
		/// <summary>CTor</summary>
		public ItemTargetEntityFactory() : base("ItemTargetEntity", Vital.DataLayer.EntityType.ItemTargetEntity, false) { }
		
		/// <summary>Creates a new ItemTargetEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ItemTargetEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewItemTargetUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty LookupEntity objects.</summary>
	[Serializable]
	public partial class LookupEntityFactory : EntityFactoryBase2<LookupEntity> {
		/// <summary>CTor</summary>
		public LookupEntityFactory() : base("LookupEntity", Vital.DataLayer.EntityType.LookupEntity, false) { }
		
		/// <summary>Creates a new LookupEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new LookupEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewLookupUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty OrderItemEntity objects.</summary>
	[Serializable]
	public partial class OrderItemEntityFactory : EntityFactoryBase2<OrderItemEntity> {
		/// <summary>CTor</summary>
		public OrderItemEntityFactory() : base("OrderItemEntity", Vital.DataLayer.EntityType.OrderItemEntity, false) { }
		
		/// <summary>Creates a new OrderItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new OrderItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewOrderItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty PatientEntity objects.</summary>
	[Serializable]
	public partial class PatientEntityFactory : EntityFactoryBase2<PatientEntity> {
		/// <summary>CTor</summary>
		public PatientEntityFactory() : base("PatientEntity", Vital.DataLayer.EntityType.PatientEntity, false) { }
		
		/// <summary>Creates a new PatientEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new PatientEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewPatientUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty PatientHistoryEntity objects.</summary>
	[Serializable]
	public partial class PatientHistoryEntityFactory : EntityFactoryBase2<PatientHistoryEntity> {
		/// <summary>CTor</summary>
		public PatientHistoryEntityFactory() : base("PatientHistoryEntity", Vital.DataLayer.EntityType.PatientHistoryEntity, false) { }
		
		/// <summary>Creates a new PatientHistoryEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new PatientHistoryEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewPatientHistoryUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ProductEntity objects.</summary>
	[Serializable]
	public partial class ProductEntityFactory : EntityFactoryBase2<ProductEntity> {
		/// <summary>CTor</summary>
		public ProductEntityFactory() : base("ProductEntity", Vital.DataLayer.EntityType.ProductEntity, false) { }
		
		/// <summary>Creates a new ProductEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ProductEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewProductUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ProductFormEntity objects.</summary>
	[Serializable]
	public partial class ProductFormEntityFactory : EntityFactoryBase2<ProductFormEntity> {
		/// <summary>CTor</summary>
		public ProductFormEntityFactory() : base("ProductFormEntity", Vital.DataLayer.EntityType.ProductFormEntity, false) { }
		
		/// <summary>Creates a new ProductFormEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ProductFormEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewProductFormUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ProductSizeEntity objects.</summary>
	[Serializable]
	public partial class ProductSizeEntityFactory : EntityFactoryBase2<ProductSizeEntity> {
		/// <summary>CTor</summary>
		public ProductSizeEntityFactory() : base("ProductSizeEntity", Vital.DataLayer.EntityType.ProductSizeEntity, false) { }
		
		/// <summary>Creates a new ProductSizeEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ProductSizeEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewProductSizeUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty PropertyEntity objects.</summary>
	[Serializable]
	public partial class PropertyEntityFactory : EntityFactoryBase2<PropertyEntity> {
		/// <summary>CTor</summary>
		public PropertyEntityFactory() : base("PropertyEntity", Vital.DataLayer.EntityType.PropertyEntity, false) { }
		
		/// <summary>Creates a new PropertyEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new PropertyEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewPropertyUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ProtocolItemEntity objects.</summary>
	[Serializable]
	public partial class ProtocolItemEntityFactory : EntityFactoryBase2<ProtocolItemEntity> {
		/// <summary>CTor</summary>
		public ProtocolItemEntityFactory() : base("ProtocolItemEntity", Vital.DataLayer.EntityType.ProtocolItemEntity, false) { }
		
		/// <summary>Creates a new ProtocolItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ProtocolItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewProtocolItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ProtocolStepEntity objects.</summary>
	[Serializable]
	public partial class ProtocolStepEntityFactory : EntityFactoryBase2<ProtocolStepEntity> {
		/// <summary>CTor</summary>
		public ProtocolStepEntityFactory() : base("ProtocolStepEntity", Vital.DataLayer.EntityType.ProtocolStepEntity, false) { }
		
		/// <summary>Creates a new ProtocolStepEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ProtocolStepEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewProtocolStepUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ReadingEntity objects.</summary>
	[Serializable]
	public partial class ReadingEntityFactory : EntityFactoryBase2<ReadingEntity> {
		/// <summary>CTor</summary>
		public ReadingEntityFactory() : base("ReadingEntity", Vital.DataLayer.EntityType.ReadingEntity, false) { }
		
		/// <summary>Creates a new ReadingEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ReadingEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewReadingUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ScheduleLineEntity objects.</summary>
	[Serializable]
	public partial class ScheduleLineEntityFactory : EntityFactoryBase2<ScheduleLineEntity> {
		/// <summary>CTor</summary>
		public ScheduleLineEntityFactory() : base("ScheduleLineEntity", Vital.DataLayer.EntityType.ScheduleLineEntity, false) { }
		
		/// <summary>Creates a new ScheduleLineEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ScheduleLineEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewScheduleLineUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ServiceEntity objects.</summary>
	[Serializable]
	public partial class ServiceEntityFactory : EntityFactoryBase2<ServiceEntity> {
		/// <summary>CTor</summary>
		public ServiceEntityFactory() : base("ServiceEntity", Vital.DataLayer.EntityType.ServiceEntity, false) { }
		
		/// <summary>Creates a new ServiceEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ServiceEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewServiceUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty SettingEntity objects.</summary>
	[Serializable]
	public partial class SettingEntityFactory : EntityFactoryBase2<SettingEntity> {
		/// <summary>CTor</summary>
		public SettingEntityFactory() : base("SettingEntity", Vital.DataLayer.EntityType.SettingEntity, false) { }
		
		/// <summary>Creates a new SettingEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new SettingEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewSettingUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty ShippingOrderEntity objects.</summary>
	[Serializable]
	public partial class ShippingOrderEntityFactory : EntityFactoryBase2<ShippingOrderEntity> {
		/// <summary>CTor</summary>
		public ShippingOrderEntityFactory() : base("ShippingOrderEntity", Vital.DataLayer.EntityType.ShippingOrderEntity, false) { }
		
		/// <summary>Creates a new ShippingOrderEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new ShippingOrderEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewShippingOrderUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty SpotCheckEntity objects.</summary>
	[Serializable]
	public partial class SpotCheckEntityFactory : EntityFactoryBase2<SpotCheckEntity> {
		/// <summary>CTor</summary>
		public SpotCheckEntityFactory() : base("SpotCheckEntity", Vital.DataLayer.EntityType.SpotCheckEntity, false) { }
		
		/// <summary>Creates a new SpotCheckEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new SpotCheckEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewSpotCheckUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty SpotCheckResultEntity objects.</summary>
	[Serializable]
	public partial class SpotCheckResultEntityFactory : EntityFactoryBase2<SpotCheckResultEntity> {
		/// <summary>CTor</summary>
		public SpotCheckResultEntityFactory() : base("SpotCheckResultEntity", Vital.DataLayer.EntityType.SpotCheckResultEntity, false) { }
		
		/// <summary>Creates a new SpotCheckResultEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new SpotCheckResultEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewSpotCheckResultUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty StageAnnouncementEntity objects.</summary>
	[Serializable]
	public partial class StageAnnouncementEntityFactory : EntityFactoryBase2<StageAnnouncementEntity> {
		/// <summary>CTor</summary>
		public StageAnnouncementEntityFactory() : base("StageAnnouncementEntity", Vital.DataLayer.EntityType.StageAnnouncementEntity, false) { }
		
		/// <summary>Creates a new StageAnnouncementEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new StageAnnouncementEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewStageAnnouncementUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty StageAutoItemEntity objects.</summary>
	[Serializable]
	public partial class StageAutoItemEntityFactory : EntityFactoryBase2<StageAutoItemEntity> {
		/// <summary>CTor</summary>
		public StageAutoItemEntityFactory() : base("StageAutoItemEntity", Vital.DataLayer.EntityType.StageAutoItemEntity, false) { }
		
		/// <summary>Creates a new StageAutoItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new StageAutoItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewStageAutoItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestEntity objects.</summary>
	[Serializable]
	public partial class TestEntityFactory : EntityFactoryBase2<TestEntity> {
		/// <summary>CTor</summary>
		public TestEntityFactory() : base("TestEntity", Vital.DataLayer.EntityType.TestEntity, false) { }
		
		/// <summary>Creates a new TestEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestImprintableItemEntity objects.</summary>
	[Serializable]
	public partial class TestImprintableItemEntityFactory : EntityFactoryBase2<TestImprintableItemEntity> {
		/// <summary>CTor</summary>
		public TestImprintableItemEntityFactory() : base("TestImprintableItemEntity", Vital.DataLayer.EntityType.TestImprintableItemEntity, false) { }
		
		/// <summary>Creates a new TestImprintableItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestImprintableItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestImprintableItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestingPointEntity objects.</summary>
	[Serializable]
	public partial class TestingPointEntityFactory : EntityFactoryBase2<TestingPointEntity> {
		/// <summary>CTor</summary>
		public TestingPointEntityFactory() : base("TestingPointEntity", Vital.DataLayer.EntityType.TestingPointEntity, false) { }
		
		/// <summary>Creates a new TestingPointEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestingPointEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestingPointUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestIssueEntity objects.</summary>
	[Serializable]
	public partial class TestIssueEntityFactory : EntityFactoryBase2<TestIssueEntity> {
		/// <summary>CTor</summary>
		public TestIssueEntityFactory() : base("TestIssueEntity", Vital.DataLayer.EntityType.TestIssueEntity, false) { }
		
		/// <summary>Creates a new TestIssueEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestIssueEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestIssueUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestProtocolEntity objects.</summary>
	[Serializable]
	public partial class TestProtocolEntityFactory : EntityFactoryBase2<TestProtocolEntity> {
		/// <summary>CTor</summary>
		public TestProtocolEntityFactory() : base("TestProtocolEntity", Vital.DataLayer.EntityType.TestProtocolEntity, false) { }
		
		/// <summary>Creates a new TestProtocolEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestProtocolEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestProtocolUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestResultEntity objects.</summary>
	[Serializable]
	public partial class TestResultEntityFactory : EntityFactoryBase2<TestResultEntity> {
		/// <summary>CTor</summary>
		public TestResultEntityFactory() : base("TestResultEntity", Vital.DataLayer.EntityType.TestResultEntity, false) { }
		
		/// <summary>Creates a new TestResultEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestResultEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestResultUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestResultFactorsEntity objects.</summary>
	[Serializable]
	public partial class TestResultFactorsEntityFactory : EntityFactoryBase2<TestResultFactorsEntity> {
		/// <summary>CTor</summary>
		public TestResultFactorsEntityFactory() : base("TestResultFactorsEntity", Vital.DataLayer.EntityType.TestResultFactorsEntity, false) { }
		
		/// <summary>Creates a new TestResultFactorsEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestResultFactorsEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestResultFactorsUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestScheduleEntity objects.</summary>
	[Serializable]
	public partial class TestScheduleEntityFactory : EntityFactoryBase2<TestScheduleEntity> {
		/// <summary>CTor</summary>
		public TestScheduleEntityFactory() : base("TestScheduleEntity", Vital.DataLayer.EntityType.TestScheduleEntity, false) { }
		
		/// <summary>Creates a new TestScheduleEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestScheduleEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestScheduleUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty TestServiceEntity objects.</summary>
	[Serializable]
	public partial class TestServiceEntityFactory : EntityFactoryBase2<TestServiceEntity> {
		/// <summary>CTor</summary>
		public TestServiceEntityFactory() : base("TestServiceEntity", Vital.DataLayer.EntityType.TestServiceEntity, false) { }
		
		/// <summary>Creates a new TestServiceEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new TestServiceEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewTestServiceUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty UserEntity objects.</summary>
	[Serializable]
	public partial class UserEntityFactory : EntityFactoryBase2<UserEntity> {
		/// <summary>CTor</summary>
		public UserEntityFactory() : base("UserEntity", Vital.DataLayer.EntityType.UserEntity, false) { }
		
		/// <summary>Creates a new UserEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new UserEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewUserUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty VFSEntity objects.</summary>
	[Serializable]
	public partial class VFSEntityFactory : EntityFactoryBase2<VFSEntity> {
		/// <summary>CTor</summary>
		public VFSEntityFactory() : base("VFSEntity", Vital.DataLayer.EntityType.VFSEntity, false) { }
		
		/// <summary>Creates a new VFSEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new VFSEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewVFSUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty VFSItemEntity objects.</summary>
	[Serializable]
	public partial class VFSItemEntityFactory : EntityFactoryBase2<VFSItemEntity> {
		/// <summary>CTor</summary>
		public VFSItemEntityFactory() : base("VFSItemEntity", Vital.DataLayer.EntityType.VFSItemEntity, false) { }
		
		/// <summary>Creates a new VFSItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new VFSItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewVFSItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty VFSItemSourceEntity objects.</summary>
	[Serializable]
	public partial class VFSItemSourceEntityFactory : EntityFactoryBase2<VFSItemSourceEntity> {
		/// <summary>CTor</summary>
		public VFSItemSourceEntityFactory() : base("VFSItemSourceEntity", Vital.DataLayer.EntityType.VFSItemSourceEntity, false) { }
		
		/// <summary>Creates a new VFSItemSourceEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new VFSItemSourceEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewVFSItemSourceUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty VFSSecondaryItemEntity objects.</summary>
	[Serializable]
	public partial class VFSSecondaryItemEntityFactory : EntityFactoryBase2<VFSSecondaryItemEntity> {
		/// <summary>CTor</summary>
		public VFSSecondaryItemEntityFactory() : base("VFSSecondaryItemEntity", Vital.DataLayer.EntityType.VFSSecondaryItemEntity, false) { }
		
		/// <summary>Creates a new VFSSecondaryItemEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new VFSSecondaryItemEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewVFSSecondaryItemUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty VFSSecondaryItemSourceEntity objects.</summary>
	[Serializable]
	public partial class VFSSecondaryItemSourceEntityFactory : EntityFactoryBase2<VFSSecondaryItemSourceEntity> {
		/// <summary>CTor</summary>
		public VFSSecondaryItemSourceEntityFactory() : base("VFSSecondaryItemSourceEntity", Vital.DataLayer.EntityType.VFSSecondaryItemSourceEntity, false) { }
		
		/// <summary>Creates a new VFSSecondaryItemSourceEntity instance but uses a special constructor which will set the Fields object of the new IEntity2 instance to the passed in fields object.</summary>
		/// <param name="fields">Populated IEntityFields2 object for the new IEntity2 to create</param>
		/// <returns>Fully created and populated (due to the IEntityFields2 object) IEntity2 object</returns>
		public override IEntity2 Create(IEntityFields2 fields) {
			IEntity2 toReturn = new VFSSecondaryItemSourceEntity(fields);
			// __LLBLGENPRO_USER_CODE_REGION_START CreateNewVFSSecondaryItemSourceUsingFields
			// __LLBLGENPRO_USER_CODE_REGION_END
			return toReturn;
		}
		#region Included Code

		#endregion
	}

	/// <summary>Factory to create new, empty Entity objects based on the entity type specified. Uses  entity specific factory objects</summary>
	[Serializable]
	public partial class GeneralEntityFactory
	{
		/// <summary>Creates a new, empty Entity object of the type specified</summary>
		/// <param name="entityTypeToCreate">The entity type to create.</param>
		/// <returns>A new, empty Entity object.</returns>
		public static IEntity2 Create(Vital.DataLayer.EntityType entityTypeToCreate)
		{
			IEntityFactory2 factoryToUse = null;
			switch(entityTypeToCreate)
			{
				case Vital.DataLayer.EntityType.AppImageEntity:
					factoryToUse = new AppImageEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AppInfoEntity:
					factoryToUse = new AppInfoEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoItemEntity:
					factoryToUse = new AutoItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoItemRelationEntity:
					factoryToUse = new AutoItemRelationEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoProtocolEntity:
					factoryToUse = new AutoProtocolEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoProtocolRevisionEntity:
					factoryToUse = new AutoProtocolRevisionEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoProtocolStageEntity:
					factoryToUse = new AutoProtocolStageEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoProtocolStageRevisionEntity:
					factoryToUse = new AutoProtocolStageRevisionEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoTestEntity:
					factoryToUse = new AutoTestEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoTestResultEntity:
					factoryToUse = new AutoTestResultEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoTestResultProductEntity:
					factoryToUse = new AutoTestResultProductEntityFactory();
					break;
				case Vital.DataLayer.EntityType.AutoTestStageEntity:
					factoryToUse = new AutoTestStageEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ClinicProductPricingEntity:
					factoryToUse = new ClinicProductPricingEntityFactory();
					break;
				case Vital.DataLayer.EntityType.DosageOptionEntity:
					factoryToUse = new DosageOptionEntityFactory();
					break;
				case Vital.DataLayer.EntityType.FrequencyTestEntity:
					factoryToUse = new FrequencyTestEntityFactory();
					break;
				case Vital.DataLayer.EntityType.FrequencyTestResultEntity:
					factoryToUse = new FrequencyTestResultEntityFactory();
					break;
				case Vital.DataLayer.EntityType.HwProfileEntity:
					factoryToUse = new HwProfileEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ImageEntity:
					factoryToUse = new ImageEntityFactory();
					break;
				case Vital.DataLayer.EntityType.InvoiceEntity:
					factoryToUse = new InvoiceEntityFactory();
					break;
				case Vital.DataLayer.EntityType.IssueNavigationStepEntity:
					factoryToUse = new IssueNavigationStepEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ItemEntity:
					factoryToUse = new ItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ItemDetailsEntity:
					factoryToUse = new ItemDetailsEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ItemPropertyEntity:
					factoryToUse = new ItemPropertyEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ItemRelationEntity:
					factoryToUse = new ItemRelationEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ItemRelationPropertyEntity:
					factoryToUse = new ItemRelationPropertyEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ItemTargetEntity:
					factoryToUse = new ItemTargetEntityFactory();
					break;
				case Vital.DataLayer.EntityType.LookupEntity:
					factoryToUse = new LookupEntityFactory();
					break;
				case Vital.DataLayer.EntityType.OrderItemEntity:
					factoryToUse = new OrderItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.PatientEntity:
					factoryToUse = new PatientEntityFactory();
					break;
				case Vital.DataLayer.EntityType.PatientHistoryEntity:
					factoryToUse = new PatientHistoryEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ProductEntity:
					factoryToUse = new ProductEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ProductFormEntity:
					factoryToUse = new ProductFormEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ProductSizeEntity:
					factoryToUse = new ProductSizeEntityFactory();
					break;
				case Vital.DataLayer.EntityType.PropertyEntity:
					factoryToUse = new PropertyEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ProtocolItemEntity:
					factoryToUse = new ProtocolItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ProtocolStepEntity:
					factoryToUse = new ProtocolStepEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ReadingEntity:
					factoryToUse = new ReadingEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ScheduleLineEntity:
					factoryToUse = new ScheduleLineEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ServiceEntity:
					factoryToUse = new ServiceEntityFactory();
					break;
				case Vital.DataLayer.EntityType.SettingEntity:
					factoryToUse = new SettingEntityFactory();
					break;
				case Vital.DataLayer.EntityType.ShippingOrderEntity:
					factoryToUse = new ShippingOrderEntityFactory();
					break;
				case Vital.DataLayer.EntityType.SpotCheckEntity:
					factoryToUse = new SpotCheckEntityFactory();
					break;
				case Vital.DataLayer.EntityType.SpotCheckResultEntity:
					factoryToUse = new SpotCheckResultEntityFactory();
					break;
				case Vital.DataLayer.EntityType.StageAnnouncementEntity:
					factoryToUse = new StageAnnouncementEntityFactory();
					break;
				case Vital.DataLayer.EntityType.StageAutoItemEntity:
					factoryToUse = new StageAutoItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestEntity:
					factoryToUse = new TestEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestImprintableItemEntity:
					factoryToUse = new TestImprintableItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestingPointEntity:
					factoryToUse = new TestingPointEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestIssueEntity:
					factoryToUse = new TestIssueEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestProtocolEntity:
					factoryToUse = new TestProtocolEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestResultEntity:
					factoryToUse = new TestResultEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestResultFactorsEntity:
					factoryToUse = new TestResultFactorsEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestScheduleEntity:
					factoryToUse = new TestScheduleEntityFactory();
					break;
				case Vital.DataLayer.EntityType.TestServiceEntity:
					factoryToUse = new TestServiceEntityFactory();
					break;
				case Vital.DataLayer.EntityType.UserEntity:
					factoryToUse = new UserEntityFactory();
					break;
				case Vital.DataLayer.EntityType.VFSEntity:
					factoryToUse = new VFSEntityFactory();
					break;
				case Vital.DataLayer.EntityType.VFSItemEntity:
					factoryToUse = new VFSItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.VFSItemSourceEntity:
					factoryToUse = new VFSItemSourceEntityFactory();
					break;
				case Vital.DataLayer.EntityType.VFSSecondaryItemEntity:
					factoryToUse = new VFSSecondaryItemEntityFactory();
					break;
				case Vital.DataLayer.EntityType.VFSSecondaryItemSourceEntity:
					factoryToUse = new VFSSecondaryItemSourceEntityFactory();
					break;
			}
			IEntity2 toReturn = null;
			if(factoryToUse != null)
			{
				toReturn = factoryToUse.Create();
			}
			return toReturn;
		}		
	}
		
	/// <summary>Class which is used to obtain the entity factory based on the .NET type of the entity. </summary>
	[Serializable]
	public static class EntityFactoryFactory
	{
#if CF
		/// <summary>Gets the factory of the entity with the Vital.DataLayer.EntityType specified</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>factory to use or null if not found</returns>
		public static IEntityFactory2 GetFactory(Vital.DataLayer.EntityType typeOfEntity)
		{
			return GeneralEntityFactory.Create(typeOfEntity).GetEntityFactory();
		}
#else
		private static Dictionary<Type, IEntityFactory2> _factoryPerType = new Dictionary<Type, IEntityFactory2>();

		/// <summary>Initializes the <see cref="EntityFactoryFactory"/> class.</summary>
		static EntityFactoryFactory()
		{
			Array entityTypeValues = Enum.GetValues(typeof(Vital.DataLayer.EntityType));
			foreach(int entityTypeValue in entityTypeValues)
			{
				IEntity2 dummy = GeneralEntityFactory.Create((Vital.DataLayer.EntityType)entityTypeValue);
				_factoryPerType.Add(dummy.GetType(), dummy.GetEntityFactory());
			}
		}

		/// <summary>Gets the factory of the entity with the .NET type specified</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>factory to use or null if not found</returns>
		public static IEntityFactory2 GetFactory(Type typeOfEntity)
		{
			IEntityFactory2 toReturn = null;
			_factoryPerType.TryGetValue(typeOfEntity, out toReturn);
			return toReturn;
		}

		/// <summary>Gets the factory of the entity with the Vital.DataLayer.EntityType specified</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>factory to use or null if not found</returns>
		public static IEntityFactory2 GetFactory(Vital.DataLayer.EntityType typeOfEntity)
		{
			return GetFactory(GeneralEntityFactory.Create(typeOfEntity).GetType());
		}
#endif		
	}
		
	/// <summary>Element creator for creating project elements from somewhere else, like inside Linq providers.</summary>
	public class ElementCreator : ElementCreatorBase, IElementCreator2
	{
		/// <summary>Gets the factory of the Entity type with the Vital.DataLayer.EntityType value passed in</summary>
		/// <param name="entityTypeValue">The entity type value.</param>
		/// <returns>the entity factory of the entity type or null if not found</returns>
		public IEntityFactory2 GetFactory(int entityTypeValue)
		{
			return (IEntityFactory2)this.GetFactoryImpl(entityTypeValue);
		}
		
		/// <summary>Gets the factory of the Entity type with the .NET type passed in</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>the entity factory of the entity type or null if not found</returns>
		public IEntityFactory2 GetFactory(Type typeOfEntity)
		{
			return (IEntityFactory2)this.GetFactoryImpl(typeOfEntity);
		}

		/// <summary>Creates a new resultset fields object with the number of field slots reserved as specified</summary>
		/// <param name="numberOfFields">The number of fields.</param>
		/// <returns>ready to use resultsetfields object</returns>
		public IEntityFields2 CreateResultsetFields(int numberOfFields)
		{
			return new ResultsetFields(numberOfFields);
		}

		/// <summary>Creates a new dynamic relation instance</summary>
		/// <param name="leftOperand">The left operand.</param>
		/// <returns>ready to use dynamic relation</returns>
		public override IDynamicRelation CreateDynamicRelation(DerivedTableDefinition leftOperand)
		{
			return new DynamicRelation(leftOperand);
		}

		/// <summary>Creates a new dynamic relation instance</summary>
		/// <param name="leftOperand">The left operand.</param>
		/// <param name="joinType">Type of the join. If None is specified, Inner is assumed.</param>
		/// <param name="rightOperand">The right operand.</param>
		/// <param name="onClause">The on clause for the join.</param>
		/// <returns>ready to use dynamic relation</returns>
		public override IDynamicRelation CreateDynamicRelation(DerivedTableDefinition leftOperand, JoinHint joinType, DerivedTableDefinition rightOperand, IPredicate onClause)
		{
			return new DynamicRelation(leftOperand, joinType, rightOperand, onClause);
		}

		/// <summary>Creates a new dynamic relation instance</summary>
		/// <param name="leftOperand">The left operand.</param>
		/// <param name="joinType">Type of the join. If None is specified, Inner is assumed.</param>
		/// <param name="rightOperandEntityName">Name of the entity, which is used as the right operand.</param>
		/// <param name="aliasRightOperand">The alias of the right operand. If you don't want to / need to alias the right operand (only alias if you have to), specify string.Empty.</param>
		/// <param name="onClause">The on clause for the join.</param>
		/// <returns>ready to use dynamic relation</returns>
		public override IDynamicRelation CreateDynamicRelation(DerivedTableDefinition leftOperand, JoinHint joinType, string rightOperandEntityName, string aliasRightOperand, IPredicate onClause)
		{
			return new DynamicRelation(leftOperand, joinType, (Vital.DataLayer.EntityType)Enum.Parse(typeof(Vital.DataLayer.EntityType), rightOperandEntityName, false), aliasRightOperand, onClause);
		}

		/// <summary>Creates a new dynamic relation instance</summary>
		/// <param name="leftOperandEntityName">Name of the entity which is used as the left operand.</param>
		/// <param name="joinType">Type of the join. If None is specified, Inner is assumed.</param>
		/// <param name="rightOperandEntityName">Name of the entity, which is used as the right operand.</param>
		/// <param name="aliasLeftOperand">The alias of the left operand. If you don't want to / need to alias the right operand (only alias if you have to), specify string.Empty.</param>
		/// <param name="aliasRightOperand">The alias of the right operand. If you don't want to / need to alias the right operand (only alias if you have to), specify string.Empty.</param>
		/// <param name="onClause">The on clause for the join.</param>
		/// <returns>ready to use dynamic relation</returns>
		public override IDynamicRelation CreateDynamicRelation(string leftOperandEntityName, JoinHint joinType, string rightOperandEntityName, string aliasLeftOperand, string aliasRightOperand, IPredicate onClause)
		{
			return new DynamicRelation((Vital.DataLayer.EntityType)Enum.Parse(typeof(Vital.DataLayer.EntityType), leftOperandEntityName, false), joinType, (Vital.DataLayer.EntityType)Enum.Parse(typeof(Vital.DataLayer.EntityType), rightOperandEntityName, false), aliasLeftOperand, aliasRightOperand, onClause);
		}
		
		/// <summary>Obtains the inheritance info provider instance from the singleton </summary>
		/// <returns>The singleton instance of the inheritance info provider</returns>
		public override IInheritanceInfoProvider ObtainInheritanceInfoProviderInstance()
		{
			return InheritanceInfoProviderSingleton.GetInstance();
		}
		
		/// <summary>Implementation of the routine which gets the factory of the Entity type with the Vital.DataLayer.EntityType value passed in</summary>
		/// <param name="entityTypeValue">The entity type value.</param>
		/// <returns>the entity factory of the entity type or null if not found</returns>
		protected override IEntityFactoryCore GetFactoryImpl(int entityTypeValue)
		{
			return EntityFactoryFactory.GetFactory((Vital.DataLayer.EntityType)entityTypeValue);
		}
#if !CF		
		/// <summary>Implementation of the routine which gets the factory of the Entity type with the .NET type passed in</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>the entity factory of the entity type or null if not found</returns>
		protected override IEntityFactoryCore GetFactoryImpl(Type typeOfEntity)
		{
			return EntityFactoryFactory.GetFactory(typeOfEntity);
		}
#endif
	}
}
