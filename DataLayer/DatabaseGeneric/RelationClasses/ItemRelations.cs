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
using System.Collections.Generic;
using Vital.DataLayer;
using Vital.DataLayer.FactoryClasses;
using Vital.DataLayer.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Vital.DataLayer.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Item. </summary>
	public partial class ItemRelations
	{
		/// <summary>CTor</summary>
		public ItemRelations()
		{
		}

		/// <summary>Gets all relations of the ItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.FrequencyTestResultEntityUsingItemId);
			toReturn.Add(this.IssueNavigationStepEntityUsingItemId);
			toReturn.Add(this.ItemPropertyEntityUsingItemId);
			toReturn.Add(this.ItemRelationEntityUsingItemChildId);
			toReturn.Add(this.ItemRelationEntityUsingItemParentId);
			toReturn.Add(this.ItemTargetEntityUsingItemId);
			toReturn.Add(this.OrderItemEntityUsingItemId);
			toReturn.Add(this.ProtocolItemEntityUsingItemId);
			toReturn.Add(this.ReadingEntityUsingItemId);
			toReturn.Add(this.ReadingEntityUsingPointSetItemId);
			toReturn.Add(this.ScheduleLineEntityUsingItemId);
			toReturn.Add(this.SpotCheckResultEntityUsingItemId);
			toReturn.Add(this.TestEntityUsingPointsGroupId);
			toReturn.Add(this.TestImprintableItemEntityUsingItemId);
			toReturn.Add(this.TestIssueEntityUsingItemId);
			toReturn.Add(this.TestResultEntityUsingItemId);
			toReturn.Add(this.TestResultEntityUsingItemRatioId);
			toReturn.Add(this.TestResultEntityUsingVitalForceId);
			toReturn.Add(this.TestResultFactorsEntityUsingFactorItemId);
			toReturn.Add(this.TestResultFactorsEntityUsingPotencyItemId);
			toReturn.Add(this.VFSItemEntityUsingItemId);
			toReturn.Add(this.VFSItemSourceEntityUsingItemId);
			toReturn.Add(this.VFSSecondaryItemEntityUsingItemId);
			toReturn.Add(this.VFSSecondaryItemSourceEntityUsingItemId);
			toReturn.Add(this.ItemDetailsEntityUsingItemDetailId);
			toReturn.Add(this.LookupEntityUsingGenderLookupId);
			toReturn.Add(this.LookupEntityUsingItemSourceLookupId);
			toReturn.Add(this.LookupEntityUsingListTypeLookupId);
			toReturn.Add(this.LookupEntityUsingTypeLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and FrequencyTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - FrequencyTestResult.ItemId
		/// </summary>
		public virtual IEntityRelation FrequencyTestResultEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "FrequencyTestResults" , true);
				relation.AddEntityFieldPair(ItemFields.Id, FrequencyTestResultFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and IssueNavigationStepEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - IssueNavigationStep.ItemId
		/// </summary>
		public virtual IEntityRelation IssueNavigationStepEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, IssueNavigationStepFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ItemPropertyEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - ItemProperty.ItemId
		/// </summary>
		public virtual IEntityRelation ItemPropertyEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Properties" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ItemPropertyFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemPropertyEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ItemRelationEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - ItemRelation.ItemChildId
		/// </summary>
		public virtual IEntityRelation ItemRelationEntityUsingItemChildId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Children" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ItemRelationFields.ItemChildId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ItemRelationEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - ItemRelation.ItemParentId
		/// </summary>
		public virtual IEntityRelation ItemRelationEntityUsingItemParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Parents" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ItemRelationFields.ItemParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ItemTargetEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - ItemTarget.ItemId
		/// </summary>
		public virtual IEntityRelation ItemTargetEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ItemTargets" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ItemTargetFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemTargetEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and OrderItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - OrderItem.ItemId
		/// </summary>
		public virtual IEntityRelation OrderItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, OrderItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("OrderItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ProtocolItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - ProtocolItem.ItemId
		/// </summary>
		public virtual IEntityRelation ProtocolItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ProtocolItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ReadingEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - Reading.ItemId
		/// </summary>
		public virtual IEntityRelation ReadingEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ReadingFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ReadingEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - Reading.PointSetItemId
		/// </summary>
		public virtual IEntityRelation ReadingEntityUsingPointSetItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ReadingFields.PointSetItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ScheduleLineEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - ScheduleLine.ItemId
		/// </summary>
		public virtual IEntityRelation ScheduleLineEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ScheduleLines" , true);
				relation.AddEntityFieldPair(ItemFields.Id, ScheduleLineFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ScheduleLineEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and SpotCheckResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - SpotCheckResult.ItemId
		/// </summary>
		public virtual IEntityRelation SpotCheckResultEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotCheckResults" , true);
				relation.AddEntityFieldPair(ItemFields.Id, SpotCheckResultFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - Test.PointsGroupId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingPointsGroupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestFields.PointsGroupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestImprintableItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestImprintableItem.ItemId
		/// </summary>
		public virtual IEntityRelation TestImprintableItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestImprintableItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestIssueEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestIssue.ItemId
		/// </summary>
		public virtual IEntityRelation TestIssueEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestIssueFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestResult.ItemId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestResult.ItemRatioId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingItemRatioId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestResults" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFields.ItemRatioId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestResult.VitalForceId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingVitalForceId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFields.VitalForceId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestResultFactorsEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestResultFactors.FactorItemId
		/// </summary>
		public virtual IEntityRelation TestResultFactorsEntityUsingFactorItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFactorsFields.FactorItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and TestResultFactorsEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - TestResultFactors.PotencyItemId
		/// </summary>
		public virtual IEntityRelation TestResultFactorsEntityUsingPotencyItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFactorsFields.PotencyItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - VFSItem.ItemId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, VFSItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - VFSItemSource.ItemId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, VFSItemSourceFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and VFSSecondaryItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - VFSSecondaryItem.ItemId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, VFSSecondaryItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ItemEntity and VFSSecondaryItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Item.Id - VFSSecondaryItemSource.ItemId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemSourceEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ItemFields.Id, VFSSecondaryItemSourceFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemSourceEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ItemEntity and ItemDetailsEntity over the m:1 relation they have, using the relation between the fields:
		/// Item.ItemDetailId - ItemDetails.Id
		/// </summary>
		public virtual IEntityRelation ItemDetailsEntityUsingItemDetailId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ItemDetail", false);
				relation.AddEntityFieldPair(ItemDetailsFields.Id, ItemFields.ItemDetailId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemDetailsEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Item.GenderLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "GenderLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Item.ItemSourceLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingItemSourceLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ItemSourceLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.ItemSourceLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Item.ListTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingListTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListTypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.ListTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Item.TypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Item.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", true);
				return relation;
			}
		}
		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSubTypeRelation(string subTypeEntityName) { return null; }
		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSuperTypeRelation() { return null;}
		#endregion

		#region Included Code

		#endregion
	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticItemRelations
	{
		internal static readonly IEntityRelation FrequencyTestResultEntityUsingItemIdStatic = new ItemRelations().FrequencyTestResultEntityUsingItemId;
		internal static readonly IEntityRelation IssueNavigationStepEntityUsingItemIdStatic = new ItemRelations().IssueNavigationStepEntityUsingItemId;
		internal static readonly IEntityRelation ItemPropertyEntityUsingItemIdStatic = new ItemRelations().ItemPropertyEntityUsingItemId;
		internal static readonly IEntityRelation ItemRelationEntityUsingItemChildIdStatic = new ItemRelations().ItemRelationEntityUsingItemChildId;
		internal static readonly IEntityRelation ItemRelationEntityUsingItemParentIdStatic = new ItemRelations().ItemRelationEntityUsingItemParentId;
		internal static readonly IEntityRelation ItemTargetEntityUsingItemIdStatic = new ItemRelations().ItemTargetEntityUsingItemId;
		internal static readonly IEntityRelation OrderItemEntityUsingItemIdStatic = new ItemRelations().OrderItemEntityUsingItemId;
		internal static readonly IEntityRelation ProtocolItemEntityUsingItemIdStatic = new ItemRelations().ProtocolItemEntityUsingItemId;
		internal static readonly IEntityRelation ReadingEntityUsingItemIdStatic = new ItemRelations().ReadingEntityUsingItemId;
		internal static readonly IEntityRelation ReadingEntityUsingPointSetItemIdStatic = new ItemRelations().ReadingEntityUsingPointSetItemId;
		internal static readonly IEntityRelation ScheduleLineEntityUsingItemIdStatic = new ItemRelations().ScheduleLineEntityUsingItemId;
		internal static readonly IEntityRelation SpotCheckResultEntityUsingItemIdStatic = new ItemRelations().SpotCheckResultEntityUsingItemId;
		internal static readonly IEntityRelation TestEntityUsingPointsGroupIdStatic = new ItemRelations().TestEntityUsingPointsGroupId;
		internal static readonly IEntityRelation TestImprintableItemEntityUsingItemIdStatic = new ItemRelations().TestImprintableItemEntityUsingItemId;
		internal static readonly IEntityRelation TestIssueEntityUsingItemIdStatic = new ItemRelations().TestIssueEntityUsingItemId;
		internal static readonly IEntityRelation TestResultEntityUsingItemIdStatic = new ItemRelations().TestResultEntityUsingItemId;
		internal static readonly IEntityRelation TestResultEntityUsingItemRatioIdStatic = new ItemRelations().TestResultEntityUsingItemRatioId;
		internal static readonly IEntityRelation TestResultEntityUsingVitalForceIdStatic = new ItemRelations().TestResultEntityUsingVitalForceId;
		internal static readonly IEntityRelation TestResultFactorsEntityUsingFactorItemIdStatic = new ItemRelations().TestResultFactorsEntityUsingFactorItemId;
		internal static readonly IEntityRelation TestResultFactorsEntityUsingPotencyItemIdStatic = new ItemRelations().TestResultFactorsEntityUsingPotencyItemId;
		internal static readonly IEntityRelation VFSItemEntityUsingItemIdStatic = new ItemRelations().VFSItemEntityUsingItemId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingItemIdStatic = new ItemRelations().VFSItemSourceEntityUsingItemId;
		internal static readonly IEntityRelation VFSSecondaryItemEntityUsingItemIdStatic = new ItemRelations().VFSSecondaryItemEntityUsingItemId;
		internal static readonly IEntityRelation VFSSecondaryItemSourceEntityUsingItemIdStatic = new ItemRelations().VFSSecondaryItemSourceEntityUsingItemId;
		internal static readonly IEntityRelation ItemDetailsEntityUsingItemDetailIdStatic = new ItemRelations().ItemDetailsEntityUsingItemDetailId;
		internal static readonly IEntityRelation LookupEntityUsingGenderLookupIdStatic = new ItemRelations().LookupEntityUsingGenderLookupId;
		internal static readonly IEntityRelation LookupEntityUsingItemSourceLookupIdStatic = new ItemRelations().LookupEntityUsingItemSourceLookupId;
		internal static readonly IEntityRelation LookupEntityUsingListTypeLookupIdStatic = new ItemRelations().LookupEntityUsingListTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingTypeLookupIdStatic = new ItemRelations().LookupEntityUsingTypeLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ItemRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticItemRelations()
		{
		}
	}
}
