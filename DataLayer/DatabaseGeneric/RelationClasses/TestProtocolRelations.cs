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
	/// <summary>Implements the relations factory for the entity: TestProtocol. </summary>
	public partial class TestProtocolRelations
	{
		/// <summary>CTor</summary>
		public TestProtocolRelations()
		{
		}

		/// <summary>Gets all relations of the TestProtocolEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ProtocolItemEntityUsingTestProtocolId);
			toReturn.Add(this.ProtocolStepEntityUsingTestProtocolId);
			toReturn.Add(this.TestEntityUsingTestProtocolId);
			toReturn.Add(this.TestResultEntityUsingTestProtocolId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestProtocolEntity and ProtocolItemEntity over the 1:n relation they have, using the relation between the fields:
		/// TestProtocol.Id - ProtocolItem.TestProtocolId
		/// </summary>
		public virtual IEntityRelation ProtocolItemEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ProtocolItems" , true);
				relation.AddEntityFieldPair(TestProtocolFields.Id, ProtocolItemFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestProtocolEntity and ProtocolStepEntity over the 1:n relation they have, using the relation between the fields:
		/// TestProtocol.Id - ProtocolStep.TestProtocolId
		/// </summary>
		public virtual IEntityRelation ProtocolStepEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ProtocolSteps" , true);
				relation.AddEntityFieldPair(TestProtocolFields.Id, ProtocolStepFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolStepEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestProtocolEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// TestProtocol.Id - Test.TestProtocolId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(TestProtocolFields.Id, TestFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestProtocolEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// TestProtocol.Id - TestResult.TestProtocolId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestResult" , true);
				relation.AddEntityFieldPair(TestProtocolFields.Id, TestResultFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestProtocolEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// TestProtocol.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestProtocolFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", true);
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
	internal static class StaticTestProtocolRelations
	{
		internal static readonly IEntityRelation ProtocolItemEntityUsingTestProtocolIdStatic = new TestProtocolRelations().ProtocolItemEntityUsingTestProtocolId;
		internal static readonly IEntityRelation ProtocolStepEntityUsingTestProtocolIdStatic = new TestProtocolRelations().ProtocolStepEntityUsingTestProtocolId;
		internal static readonly IEntityRelation TestEntityUsingTestProtocolIdStatic = new TestProtocolRelations().TestEntityUsingTestProtocolId;
		internal static readonly IEntityRelation TestResultEntityUsingTestProtocolIdStatic = new TestProtocolRelations().TestResultEntityUsingTestProtocolId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestProtocolRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestProtocolRelations()
		{
		}
	}
}
