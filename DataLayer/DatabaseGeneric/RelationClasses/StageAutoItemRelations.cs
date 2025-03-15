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
	/// <summary>Implements the relations factory for the entity: StageAutoItem. </summary>
	public partial class StageAutoItemRelations
	{
		/// <summary>CTor</summary>
		public StageAutoItemRelations()
		{
		}

		/// <summary>Gets all relations of the StageAutoItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.StageAutoItemEntityUsingStageAutoItemParentId);
			toReturn.Add(this.AutoItemEntityUsingAutoItemsId);
			toReturn.Add(this.AutoProtocolStageEntityUsingAutoProtocolStagesId);
			toReturn.Add(this.LookupEntityUsingChildsOrderTypeLookupId);
			toReturn.Add(this.LookupEntityUsingChildsScanningTypeLookupId);
			toReturn.Add(this.LookupEntityUsingScanningMethodLookupId);
			toReturn.Add(this.StageAutoItemEntityUsingIdStageAutoItemParentId);
			toReturn.Add(this.TestingPointEntityUsingTestingPointsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// StageAutoItem.Id - StageAutoItem.StageAutoItemParentId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingStageAutoItemParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "StageAutoItems" , true);
				relation.AddEntityFieldPair(StageAutoItemFields.Id, StageAutoItemFields.StageAutoItemParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and AutoItemEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.AutoItemsId - AutoItem.Id
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingAutoItemsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoItem", false);
				relation.AddEntityFieldPair(AutoItemFields.Id, StageAutoItemFields.AutoItemsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and AutoProtocolStageEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.AutoProtocolStagesId - AutoProtocolStage.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageEntityUsingAutoProtocolStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocolStage", false);
				relation.AddEntityFieldPair(AutoProtocolStageFields.Id, StageAutoItemFields.AutoProtocolStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.ChildsOrderTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingChildsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ChildsOrderType", false);
				relation.AddEntityFieldPair(LookupFields.Id, StageAutoItemFields.ChildsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.ChildsScanningTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingChildsScanningTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ChildsScanningType", false);
				relation.AddEntityFieldPair(LookupFields.Id, StageAutoItemFields.ChildsScanningTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.ScanningMethodLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingScanningMethodLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ScanningMethod", false);
				relation.AddEntityFieldPair(LookupFields.Id, StageAutoItemFields.ScanningMethodLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and StageAutoItemEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.StageAutoItemParentId - StageAutoItem.Id
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingIdStageAutoItemParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Parent", false);
				relation.AddEntityFieldPair(StageAutoItemFields.Id, StageAutoItemFields.StageAutoItemParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and TestingPointEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.TestingPointsId - TestingPoint.Id
		/// </summary>
		public virtual IEntityRelation TestingPointEntityUsingTestingPointsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestingPoint", false);
				relation.AddEntityFieldPair(TestingPointFields.Id, StageAutoItemFields.TestingPointsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestingPointEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between StageAutoItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// StageAutoItem.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, StageAutoItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", true);
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
	internal static class StaticStageAutoItemRelations
	{
		internal static readonly IEntityRelation StageAutoItemEntityUsingStageAutoItemParentIdStatic = new StageAutoItemRelations().StageAutoItemEntityUsingStageAutoItemParentId;
		internal static readonly IEntityRelation AutoItemEntityUsingAutoItemsIdStatic = new StageAutoItemRelations().AutoItemEntityUsingAutoItemsId;
		internal static readonly IEntityRelation AutoProtocolStageEntityUsingAutoProtocolStagesIdStatic = new StageAutoItemRelations().AutoProtocolStageEntityUsingAutoProtocolStagesId;
		internal static readonly IEntityRelation LookupEntityUsingChildsOrderTypeLookupIdStatic = new StageAutoItemRelations().LookupEntityUsingChildsOrderTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingChildsScanningTypeLookupIdStatic = new StageAutoItemRelations().LookupEntityUsingChildsScanningTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingScanningMethodLookupIdStatic = new StageAutoItemRelations().LookupEntityUsingScanningMethodLookupId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingIdStageAutoItemParentIdStatic = new StageAutoItemRelations().StageAutoItemEntityUsingIdStageAutoItemParentId;
		internal static readonly IEntityRelation TestingPointEntityUsingTestingPointsIdStatic = new StageAutoItemRelations().TestingPointEntityUsingTestingPointsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new StageAutoItemRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticStageAutoItemRelations()
		{
		}
	}
}
