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
	/// <summary>Implements the relations factory for the entity: Test. </summary>
	public partial class TestRelations
	{
		/// <summary>CTor</summary>
		public TestRelations()
		{
		}

		/// <summary>Gets all relations of the TestEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.InvoiceEntityUsingTestId);
			toReturn.Add(this.ReadingEntityUsingTestId);
			toReturn.Add(this.ShippingOrderEntityUsingTestId);
			toReturn.Add(this.SpotCheckEntityUsingTestId);
			toReturn.Add(this.TestImprintableItemEntityUsingTestId);
			toReturn.Add(this.TestIssueEntityUsingTestId);
			toReturn.Add(this.TestServiceEntityUsingTestId);
			toReturn.Add(this.VFSEntityUsingTestId);
			toReturn.Add(this.ItemEntityUsingPointsGroupId);
			toReturn.Add(this.LookupEntityUsingListPointLookupId);
			toReturn.Add(this.LookupEntityUsingTestStateLookupId);
			toReturn.Add(this.LookupEntityUsingTestTypeLookupId);
			toReturn.Add(this.PatientEntityUsingPatientId);
			toReturn.Add(this.TestProtocolEntityUsingTestProtocolId);
			toReturn.Add(this.TestScheduleEntityUsingTestScheduleId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestEntity and InvoiceEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - Invoice.TestId
		/// </summary>
		public virtual IEntityRelation InvoiceEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Invoices" , true);
				relation.AddEntityFieldPair(TestFields.Id, InvoiceFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("InvoiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and ReadingEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - Reading.TestId
		/// </summary>
		public virtual IEntityRelation ReadingEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Readings" , true);
				relation.AddEntityFieldPair(TestFields.Id, ReadingFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and ShippingOrderEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - ShippingOrder.TestId
		/// </summary>
		public virtual IEntityRelation ShippingOrderEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ShippingOrders" , true);
				relation.AddEntityFieldPair(TestFields.Id, ShippingOrderFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and SpotCheckEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - SpotCheck.TestId
		/// </summary>
		public virtual IEntityRelation SpotCheckEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotChecks" , true);
				relation.AddEntityFieldPair(TestFields.Id, SpotCheckFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and TestImprintableItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - TestImprintableItem.TestId
		/// </summary>
		public virtual IEntityRelation TestImprintableItemEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestImprintableItems" , true);
				relation.AddEntityFieldPair(TestFields.Id, TestImprintableItemFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and TestIssueEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - TestIssue.TestId
		/// </summary>
		public virtual IEntityRelation TestIssueEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestIssues" , true);
				relation.AddEntityFieldPair(TestFields.Id, TestIssueFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and TestServiceEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - TestService.TestId
		/// </summary>
		public virtual IEntityRelation TestServiceEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestServices" , true);
				relation.AddEntityFieldPair(TestFields.Id, TestServiceFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestServiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestEntity and VFSEntity over the 1:n relation they have, using the relation between the fields:
		/// Test.Id - VFS.TestId
		/// </summary>
		public virtual IEntityRelation VFSEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Vfs" , true);
				relation.AddEntityFieldPair(TestFields.Id, VFSFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.PointsGroupId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingPointsGroupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, TestFields.PointsGroupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.ListPointLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingListPointLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListPointLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, TestFields.ListPointLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.TestStateLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingTestStateLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "StateLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, TestFields.TestStateLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.TestTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingTestTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, TestFields.TestTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and PatientEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.PatientId - Patient.Id
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Patient", false);
				relation.AddEntityFieldPair(PatientFields.Id, TestFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and TestProtocolEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.TestProtocolId - TestProtocol.Id
		/// </summary>
		public virtual IEntityRelation TestProtocolEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestProtocol", false);
				relation.AddEntityFieldPair(TestProtocolFields.Id, TestFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and TestScheduleEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.TestScheduleId - TestSchedule.Id
		/// </summary>
		public virtual IEntityRelation TestScheduleEntityUsingTestScheduleId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestSchedule", false);
				relation.AddEntityFieldPair(TestScheduleFields.Id, TestFields.TestScheduleId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Test.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", true);
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
	internal static class StaticTestRelations
	{
		internal static readonly IEntityRelation InvoiceEntityUsingTestIdStatic = new TestRelations().InvoiceEntityUsingTestId;
		internal static readonly IEntityRelation ReadingEntityUsingTestIdStatic = new TestRelations().ReadingEntityUsingTestId;
		internal static readonly IEntityRelation ShippingOrderEntityUsingTestIdStatic = new TestRelations().ShippingOrderEntityUsingTestId;
		internal static readonly IEntityRelation SpotCheckEntityUsingTestIdStatic = new TestRelations().SpotCheckEntityUsingTestId;
		internal static readonly IEntityRelation TestImprintableItemEntityUsingTestIdStatic = new TestRelations().TestImprintableItemEntityUsingTestId;
		internal static readonly IEntityRelation TestIssueEntityUsingTestIdStatic = new TestRelations().TestIssueEntityUsingTestId;
		internal static readonly IEntityRelation TestServiceEntityUsingTestIdStatic = new TestRelations().TestServiceEntityUsingTestId;
		internal static readonly IEntityRelation VFSEntityUsingTestIdStatic = new TestRelations().VFSEntityUsingTestId;
		internal static readonly IEntityRelation ItemEntityUsingPointsGroupIdStatic = new TestRelations().ItemEntityUsingPointsGroupId;
		internal static readonly IEntityRelation LookupEntityUsingListPointLookupIdStatic = new TestRelations().LookupEntityUsingListPointLookupId;
		internal static readonly IEntityRelation LookupEntityUsingTestStateLookupIdStatic = new TestRelations().LookupEntityUsingTestStateLookupId;
		internal static readonly IEntityRelation LookupEntityUsingTestTypeLookupIdStatic = new TestRelations().LookupEntityUsingTestTypeLookupId;
		internal static readonly IEntityRelation PatientEntityUsingPatientIdStatic = new TestRelations().PatientEntityUsingPatientId;
		internal static readonly IEntityRelation TestProtocolEntityUsingTestProtocolIdStatic = new TestRelations().TestProtocolEntityUsingTestProtocolId;
		internal static readonly IEntityRelation TestScheduleEntityUsingTestScheduleIdStatic = new TestRelations().TestScheduleEntityUsingTestScheduleId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestRelations()
		{
		}
	}
}
