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
	/// <summary>Implements the relations factory for the entity: SpotCheckResult. </summary>
	public partial class SpotCheckResultRelations
	{
		/// <summary>CTor</summary>
		public SpotCheckResultRelations()
		{
		}

		/// <summary>Gets all relations of the SpotCheckResultEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.LookupEntityUsingResultTypeId);
			toReturn.Add(this.SpotCheckEntityUsingSpotCheckId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between SpotCheckResultEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheckResult.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, SpotCheckResultFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between SpotCheckResultEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheckResult.ResultTypeId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingResultTypeId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ResultType", false);
				relation.AddEntityFieldPair(LookupFields.Id, SpotCheckResultFields.ResultTypeId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between SpotCheckResultEntity and SpotCheckEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheckResult.SpotCheckId - SpotCheck.Id
		/// </summary>
		public virtual IEntityRelation SpotCheckEntityUsingSpotCheckId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "SpotCheck", false);
				relation.AddEntityFieldPair(SpotCheckFields.Id, SpotCheckResultFields.SpotCheckId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between SpotCheckResultEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheckResult.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, SpotCheckResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", true);
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
	internal static class StaticSpotCheckResultRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new SpotCheckResultRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation LookupEntityUsingResultTypeIdStatic = new SpotCheckResultRelations().LookupEntityUsingResultTypeId;
		internal static readonly IEntityRelation SpotCheckEntityUsingSpotCheckIdStatic = new SpotCheckResultRelations().SpotCheckEntityUsingSpotCheckId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new SpotCheckResultRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticSpotCheckResultRelations()
		{
		}
	}
}
