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
	/// <summary>Implements the relations factory for the entity: AutoItemRelation. </summary>
	public partial class AutoItemRelationRelations
	{
		/// <summary>CTor</summary>
		public AutoItemRelationRelations()
		{
		}

		/// <summary>Gets all relations of the AutoItemRelationEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoItemEntityUsingAutoItemChildId);
			toReturn.Add(this.AutoItemEntityUsingAutoItemParentId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between AutoItemRelationEntity and AutoItemEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItemRelation.AutoItemChildId - AutoItem.Id
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingAutoItemChildId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Child", false);
				relation.AddEntityFieldPair(AutoItemFields.Id, AutoItemRelationFields.AutoItemChildId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemRelationEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemRelationEntity and AutoItemEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItemRelation.AutoItemParentId - AutoItem.Id
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingAutoItemParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Parent", false);
				relation.AddEntityFieldPair(AutoItemFields.Id, AutoItemRelationFields.AutoItemParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemRelationEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemRelationEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItemRelation.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoItemRelationFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemRelationEntity", true);
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
	internal static class StaticAutoItemRelationRelations
	{
		internal static readonly IEntityRelation AutoItemEntityUsingAutoItemChildIdStatic = new AutoItemRelationRelations().AutoItemEntityUsingAutoItemChildId;
		internal static readonly IEntityRelation AutoItemEntityUsingAutoItemParentIdStatic = new AutoItemRelationRelations().AutoItemEntityUsingAutoItemParentId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoItemRelationRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoItemRelationRelations()
		{
		}
	}
}
