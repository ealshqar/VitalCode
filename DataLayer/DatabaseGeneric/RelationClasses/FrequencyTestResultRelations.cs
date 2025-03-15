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
	/// <summary>Implements the relations factory for the entity: FrequencyTestResult. </summary>
	public partial class FrequencyTestResultRelations
	{
		/// <summary>CTor</summary>
		public FrequencyTestResultRelations()
		{
		}

		/// <summary>Gets all relations of the FrequencyTestResultEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.FrequencyTestEntityUsingFrequencyTestId);
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between FrequencyTestResultEntity and FrequencyTestEntity over the m:1 relation they have, using the relation between the fields:
		/// FrequencyTestResult.FrequencyTestId - FrequencyTest.Id
		/// </summary>
		public virtual IEntityRelation FrequencyTestEntityUsingFrequencyTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "FrequencyTest", false);
				relation.AddEntityFieldPair(FrequencyTestFields.Id, FrequencyTestResultFields.FrequencyTestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between FrequencyTestResultEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// FrequencyTestResult.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, FrequencyTestResultFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between FrequencyTestResultEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// FrequencyTestResult.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, FrequencyTestResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestResultEntity", true);
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
	internal static class StaticFrequencyTestResultRelations
	{
		internal static readonly IEntityRelation FrequencyTestEntityUsingFrequencyTestIdStatic = new FrequencyTestResultRelations().FrequencyTestEntityUsingFrequencyTestId;
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new FrequencyTestResultRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new FrequencyTestResultRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticFrequencyTestResultRelations()
		{
		}
	}
}
