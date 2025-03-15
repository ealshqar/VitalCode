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
	/// <summary>Implements the relations factory for the entity: AutoProtocolStage. </summary>
	public partial class AutoProtocolStageRelations
	{
		/// <summary>CTor</summary>
		public AutoProtocolStageRelations()
		{
		}

		/// <summary>Gets all relations of the AutoProtocolStageEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoProtocolStageRevisionEntityUsingAutoProtocolStagesId);
			toReturn.Add(this.StageAutoItemEntityUsingAutoProtocolStagesId);
			toReturn.Add(this.AutoProtocolEntityUsingAutoProtocolsId);
			toReturn.Add(this.AutoTestStageEntityUsingAutoTestStagesId);
			toReturn.Add(this.LookupEntityUsingStageItemsOrderTypeLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageEntity and AutoProtocolStageRevisionEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocolStage.Id - AutoProtocolStageRevision.AutoProtocolStagesId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageRevisionEntityUsingAutoProtocolStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoProtocolStageRevisions" , true);
				relation.AddEntityFieldPair(AutoProtocolStageFields.Id, AutoProtocolStageRevisionFields.AutoProtocolStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocolStage.Id - StageAutoItem.AutoProtocolStagesId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingAutoProtocolStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "StageAutoItems" , true);
				relation.AddEntityFieldPair(AutoProtocolStageFields.Id, StageAutoItemFields.AutoProtocolStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageEntity and AutoProtocolEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStage.AutoProtocolsId - AutoProtocol.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolEntityUsingAutoProtocolsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocol", false);
				relation.AddEntityFieldPair(AutoProtocolFields.Id, AutoProtocolStageFields.AutoProtocolsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageEntity and AutoTestStageEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStage.AutoTestStagesId - AutoTestStage.Id
		/// </summary>
		public virtual IEntityRelation AutoTestStageEntityUsingAutoTestStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoTestStage", false);
				relation.AddEntityFieldPair(AutoTestStageFields.Id, AutoProtocolStageFields.AutoTestStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStage.StageItemsOrderTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStageItemsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "StageItemsOrderType", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoProtocolStageFields.StageItemsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStage.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolStageFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", true);
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
	internal static class StaticAutoProtocolStageRelations
	{
		internal static readonly IEntityRelation AutoProtocolStageRevisionEntityUsingAutoProtocolStagesIdStatic = new AutoProtocolStageRelations().AutoProtocolStageRevisionEntityUsingAutoProtocolStagesId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingAutoProtocolStagesIdStatic = new AutoProtocolStageRelations().StageAutoItemEntityUsingAutoProtocolStagesId;
		internal static readonly IEntityRelation AutoProtocolEntityUsingAutoProtocolsIdStatic = new AutoProtocolStageRelations().AutoProtocolEntityUsingAutoProtocolsId;
		internal static readonly IEntityRelation AutoTestStageEntityUsingAutoTestStagesIdStatic = new AutoProtocolStageRelations().AutoTestStageEntityUsingAutoTestStagesId;
		internal static readonly IEntityRelation LookupEntityUsingStageItemsOrderTypeLookupIdStatic = new AutoProtocolStageRelations().LookupEntityUsingStageItemsOrderTypeLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoProtocolStageRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoProtocolStageRelations()
		{
		}
	}
}
