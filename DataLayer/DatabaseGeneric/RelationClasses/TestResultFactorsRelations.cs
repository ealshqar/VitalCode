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
	/// <summary>Implements the relations factory for the entity: TestResultFactors. </summary>
	public partial class TestResultFactorsRelations
	{
		/// <summary>CTor</summary>
		public TestResultFactorsRelations()
		{
		}

		/// <summary>Gets all relations of the TestResultFactorsEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingFactorItemId);
			toReturn.Add(this.ItemEntityUsingPotencyItemId);
			toReturn.Add(this.TestResultEntityUsingTestResultId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between TestResultFactorsEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResultFactors.FactorItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingFactorItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Factor", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFactorsFields.FactorItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultFactorsEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResultFactors.PotencyItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingPotencyItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Potency", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFactorsFields.PotencyItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultFactorsEntity and TestResultEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResultFactors.TestResultId - TestResult.Id
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingTestResultId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestResult", false);
				relation.AddEntityFieldPair(TestResultFields.Id, TestResultFactorsFields.TestResultId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultFactorsEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResultFactors.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestResultFactorsFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", true);
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
	internal static class StaticTestResultFactorsRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingFactorItemIdStatic = new TestResultFactorsRelations().ItemEntityUsingFactorItemId;
		internal static readonly IEntityRelation ItemEntityUsingPotencyItemIdStatic = new TestResultFactorsRelations().ItemEntityUsingPotencyItemId;
		internal static readonly IEntityRelation TestResultEntityUsingTestResultIdStatic = new TestResultFactorsRelations().TestResultEntityUsingTestResultId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestResultFactorsRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestResultFactorsRelations()
		{
		}
	}
}
