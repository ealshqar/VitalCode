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
	/// <summary>Implements the relations factory for the entity: TestImprintableItem. </summary>
	public partial class TestImprintableItemRelations
	{
		/// <summary>CTor</summary>
		public TestImprintableItemRelations()
		{
		}

		/// <summary>Gets all relations of the TestImprintableItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.TestImprintableItemEntityUsingParentImprintableId);
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.TestEntityUsingTestId);
			toReturn.Add(this.TestImprintableItemEntityUsingIdParentImprintableId);
			toReturn.Add(this.TestResultEntityUsingTestResultId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestImprintableItemEntity and TestImprintableItemEntity over the 1:n relation they have, using the relation between the fields:
		/// TestImprintableItem.Id - TestImprintableItem.ParentImprintableId
		/// </summary>
		public virtual IEntityRelation TestImprintableItemEntityUsingParentImprintableId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(TestImprintableItemFields.Id, TestImprintableItemFields.ParentImprintableId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestImprintableItemEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestImprintableItem.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestImprintableItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestImprintableItemEntity and TestEntity over the m:1 relation they have, using the relation between the fields:
		/// TestImprintableItem.TestId - Test.Id
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Test", false);
				relation.AddEntityFieldPair(TestFields.Id, TestImprintableItemFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestImprintableItemEntity and TestImprintableItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestImprintableItem.ParentImprintableId - TestImprintableItem.Id
		/// </summary>
		public virtual IEntityRelation TestImprintableItemEntityUsingIdParentImprintableId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Parent", false);
				relation.AddEntityFieldPair(TestImprintableItemFields.Id, TestImprintableItemFields.ParentImprintableId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestImprintableItemEntity and TestResultEntity over the m:1 relation they have, using the relation between the fields:
		/// TestImprintableItem.TestResultId - TestResult.Id
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingTestResultId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestResult", false);
				relation.AddEntityFieldPair(TestResultFields.Id, TestImprintableItemFields.TestResultId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestImprintableItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// TestImprintableItem.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestImprintableItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", true);
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
	internal static class StaticTestImprintableItemRelations
	{
		internal static readonly IEntityRelation TestImprintableItemEntityUsingParentImprintableIdStatic = new TestImprintableItemRelations().TestImprintableItemEntityUsingParentImprintableId;
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new TestImprintableItemRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation TestEntityUsingTestIdStatic = new TestImprintableItemRelations().TestEntityUsingTestId;
		internal static readonly IEntityRelation TestImprintableItemEntityUsingIdParentImprintableIdStatic = new TestImprintableItemRelations().TestImprintableItemEntityUsingIdParentImprintableId;
		internal static readonly IEntityRelation TestResultEntityUsingTestResultIdStatic = new TestImprintableItemRelations().TestResultEntityUsingTestResultId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestImprintableItemRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestImprintableItemRelations()
		{
		}
	}
}
