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
	/// <summary>Implements the relations factory for the entity: AutoTestStage. </summary>
	public partial class AutoTestStageRelations
	{
		/// <summary>CTor</summary>
		public AutoTestStageRelations()
		{
		}

		/// <summary>Gets all relations of the AutoTestStageEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoProtocolStageEntityUsingAutoTestStagesId);
			toReturn.Add(this.AutoProtocolStageRevisionEntityUsingAutoTestStagesId);
			toReturn.Add(this.LookupEntityUsingStageItemsOrderTypeLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoTestStageEntity and AutoProtocolStageEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoTestStage.Id - AutoProtocolStage.AutoTestStagesId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageEntityUsingAutoTestStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(AutoTestStageFields.Id, AutoProtocolStageFields.AutoTestStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoTestStageEntity and AutoProtocolStageRevisionEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoTestStage.Id - AutoProtocolStageRevision.AutoTestStagesId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageRevisionEntityUsingAutoTestStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(AutoTestStageFields.Id, AutoProtocolStageRevisionFields.AutoTestStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoTestStageEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestStage.StageItemsOrderTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStageItemsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "StageItemsOrderType", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoTestStageFields.StageItemsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestStageEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestStage.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestStageFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", true);
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
	internal static class StaticAutoTestStageRelations
	{
		internal static readonly IEntityRelation AutoProtocolStageEntityUsingAutoTestStagesIdStatic = new AutoTestStageRelations().AutoProtocolStageEntityUsingAutoTestStagesId;
		internal static readonly IEntityRelation AutoProtocolStageRevisionEntityUsingAutoTestStagesIdStatic = new AutoTestStageRelations().AutoProtocolStageRevisionEntityUsingAutoTestStagesId;
		internal static readonly IEntityRelation LookupEntityUsingStageItemsOrderTypeLookupIdStatic = new AutoTestStageRelations().LookupEntityUsingStageItemsOrderTypeLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoTestStageRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoTestStageRelations()
		{
		}
	}
}
