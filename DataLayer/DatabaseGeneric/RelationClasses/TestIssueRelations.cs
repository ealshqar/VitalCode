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
	/// <summary>Implements the relations factory for the entity: TestIssue. </summary>
	public partial class TestIssueRelations
	{
		/// <summary>CTor</summary>
		public TestIssueRelations()
		{
		}

		/// <summary>Gets all relations of the TestIssueEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.IssueNavigationStepEntityUsingTestIssueId);
			toReturn.Add(this.TestResultEntityUsingIssueId);
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.ProtocolStepEntityUsingProtocolStepId);
			toReturn.Add(this.TestEntityUsingTestId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestIssueEntity and IssueNavigationStepEntity over the 1:n relation they have, using the relation between the fields:
		/// TestIssue.Id - IssueNavigationStep.TestIssueId
		/// </summary>
		public virtual IEntityRelation IssueNavigationStepEntityUsingTestIssueId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "IssueNavigationSteps" , true);
				relation.AddEntityFieldPair(TestIssueFields.Id, IssueNavigationStepFields.TestIssueId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestIssueEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// TestIssue.Id - TestResult.IssueId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingIssueId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestResults" , true);
				relation.AddEntityFieldPair(TestIssueFields.Id, TestResultFields.IssueId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestIssueEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestIssue.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestIssueFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestIssueEntity and ProtocolStepEntity over the m:1 relation they have, using the relation between the fields:
		/// TestIssue.ProtocolStepId - ProtocolStep.Id
		/// </summary>
		public virtual IEntityRelation ProtocolStepEntityUsingProtocolStepId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ProtocolStep", false);
				relation.AddEntityFieldPair(ProtocolStepFields.Id, TestIssueFields.ProtocolStepId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolStepEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestIssueEntity and TestEntity over the m:1 relation they have, using the relation between the fields:
		/// TestIssue.TestId - Test.Id
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Test", false);
				relation.AddEntityFieldPair(TestFields.Id, TestIssueFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestIssueEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// TestIssue.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestIssueFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", true);
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
	internal static class StaticTestIssueRelations
	{
		internal static readonly IEntityRelation IssueNavigationStepEntityUsingTestIssueIdStatic = new TestIssueRelations().IssueNavigationStepEntityUsingTestIssueId;
		internal static readonly IEntityRelation TestResultEntityUsingIssueIdStatic = new TestIssueRelations().TestResultEntityUsingIssueId;
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new TestIssueRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation ProtocolStepEntityUsingProtocolStepIdStatic = new TestIssueRelations().ProtocolStepEntityUsingProtocolStepId;
		internal static readonly IEntityRelation TestEntityUsingTestIdStatic = new TestIssueRelations().TestEntityUsingTestId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestIssueRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestIssueRelations()
		{
		}
	}
}
