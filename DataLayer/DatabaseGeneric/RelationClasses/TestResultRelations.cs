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
	/// <summary>Implements the relations factory for the entity: TestResult. </summary>
	public partial class TestResultRelations
	{
		/// <summary>CTor</summary>
		public TestResultRelations()
		{
		}

		/// <summary>Gets all relations of the TestResultEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.TestImprintableItemEntityUsingTestResultId);
			toReturn.Add(this.TestResultEntityUsingParentId);
			toReturn.Add(this.TestResultEntityUsingSelectedParentId);
			toReturn.Add(this.TestResultFactorsEntityUsingTestResultId);
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.ItemEntityUsingItemRatioId);
			toReturn.Add(this.ItemEntityUsingVitalForceId);
			toReturn.Add(this.LookupEntityUsingStepTypeLookupId);
			toReturn.Add(this.TestIssueEntityUsingIssueId);
			toReturn.Add(this.TestProtocolEntityUsingTestProtocolId);
			toReturn.Add(this.TestResultEntityUsingIdParentId);
			toReturn.Add(this.TestResultEntityUsingIdSelectedParentId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestImprintableItemEntity over the 1:n relation they have, using the relation between the fields:
		/// TestResult.Id - TestImprintableItem.TestResultId
		/// </summary>
		public virtual IEntityRelation TestImprintableItemEntityUsingTestResultId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(TestResultFields.Id, TestImprintableItemFields.TestResultId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// TestResult.Id - TestResult.ParentId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(TestResultFields.Id, TestResultFields.ParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// TestResult.Id - TestResult.SelectedParentId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingSelectedParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(TestResultFields.Id, TestResultFields.SelectedParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestResultFactorsEntity over the 1:n relation they have, using the relation between the fields:
		/// TestResult.Id - TestResultFactors.TestResultId
		/// </summary>
		public virtual IEntityRelation TestResultFactorsEntityUsingTestResultId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestResultFactors" , true);
				relation.AddEntityFieldPair(TestResultFields.Id, TestResultFactorsFields.TestResultId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.ItemRatioId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemRatioId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "RatioItem", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFields.ItemRatioId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.VitalForceId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingVitalForceId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "VitalForce", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestResultFields.VitalForceId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.StepTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStepTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "StepType", false);
				relation.AddEntityFieldPair(LookupFields.Id, TestResultFields.StepTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestIssueEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.IssueId - TestIssue.Id
		/// </summary>
		public virtual IEntityRelation TestIssueEntityUsingIssueId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestIssue", false);
				relation.AddEntityFieldPair(TestIssueFields.Id, TestResultFields.IssueId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestProtocolEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.TestProtocolId - TestProtocol.Id
		/// </summary>
		public virtual IEntityRelation TestProtocolEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestProtocol", false);
				relation.AddEntityFieldPair(TestProtocolFields.Id, TestResultFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestResultEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.ParentId - TestResult.Id
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingIdParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Parent", false);
				relation.AddEntityFieldPair(TestResultFields.Id, TestResultFields.ParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and TestResultEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.SelectedParentId - TestResult.Id
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingIdSelectedParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "SelectedParent", false);
				relation.AddEntityFieldPair(TestResultFields.Id, TestResultFields.SelectedParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestResultEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// TestResult.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", true);
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
	internal static class StaticTestResultRelations
	{
		internal static readonly IEntityRelation TestImprintableItemEntityUsingTestResultIdStatic = new TestResultRelations().TestImprintableItemEntityUsingTestResultId;
		internal static readonly IEntityRelation TestResultEntityUsingParentIdStatic = new TestResultRelations().TestResultEntityUsingParentId;
		internal static readonly IEntityRelation TestResultEntityUsingSelectedParentIdStatic = new TestResultRelations().TestResultEntityUsingSelectedParentId;
		internal static readonly IEntityRelation TestResultFactorsEntityUsingTestResultIdStatic = new TestResultRelations().TestResultFactorsEntityUsingTestResultId;
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new TestResultRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation ItemEntityUsingItemRatioIdStatic = new TestResultRelations().ItemEntityUsingItemRatioId;
		internal static readonly IEntityRelation ItemEntityUsingVitalForceIdStatic = new TestResultRelations().ItemEntityUsingVitalForceId;
		internal static readonly IEntityRelation LookupEntityUsingStepTypeLookupIdStatic = new TestResultRelations().LookupEntityUsingStepTypeLookupId;
		internal static readonly IEntityRelation TestIssueEntityUsingIssueIdStatic = new TestResultRelations().TestIssueEntityUsingIssueId;
		internal static readonly IEntityRelation TestProtocolEntityUsingTestProtocolIdStatic = new TestResultRelations().TestProtocolEntityUsingTestProtocolId;
		internal static readonly IEntityRelation TestResultEntityUsingIdParentIdStatic = new TestResultRelations().TestResultEntityUsingIdParentId;
		internal static readonly IEntityRelation TestResultEntityUsingIdSelectedParentIdStatic = new TestResultRelations().TestResultEntityUsingIdSelectedParentId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestResultRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestResultRelations()
		{
		}
	}
}
