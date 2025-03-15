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
	/// <summary>Implements the relations factory for the entity: TestSchedule. </summary>
	public partial class TestScheduleRelations
	{
		/// <summary>CTor</summary>
		public TestScheduleRelations()
		{
		}

		/// <summary>Gets all relations of the TestScheduleEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ScheduleLineEntityUsingTestScheduleId);
			toReturn.Add(this.TestEntityUsingTestScheduleId);
			toReturn.Add(this.LookupEntityUsingDiscountApplyLookupId);
			toReturn.Add(this.LookupEntityUsingEvalPeriodTypeLookupId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestScheduleEntity and ScheduleLineEntity over the 1:n relation they have, using the relation between the fields:
		/// TestSchedule.Id - ScheduleLine.TestScheduleId
		/// </summary>
		public virtual IEntityRelation ScheduleLineEntityUsingTestScheduleId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ScheduleLines" , true);
				relation.AddEntityFieldPair(TestScheduleFields.Id, ScheduleLineFields.TestScheduleId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ScheduleLineEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestScheduleEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// TestSchedule.Id - Test.TestScheduleId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestScheduleId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Test" , true);
				relation.AddEntityFieldPair(TestScheduleFields.Id, TestFields.TestScheduleId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestScheduleEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// TestSchedule.DiscountApplyLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingDiscountApplyLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "DiscountApply", false);
				relation.AddEntityFieldPair(LookupFields.Id, TestScheduleFields.DiscountApplyLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between TestScheduleEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// TestSchedule.EvalPeriodTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingEvalPeriodTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "EvalPeriodType", false);
				relation.AddEntityFieldPair(LookupFields.Id, TestScheduleFields.EvalPeriodTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", true);
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
	internal static class StaticTestScheduleRelations
	{
		internal static readonly IEntityRelation ScheduleLineEntityUsingTestScheduleIdStatic = new TestScheduleRelations().ScheduleLineEntityUsingTestScheduleId;
		internal static readonly IEntityRelation TestEntityUsingTestScheduleIdStatic = new TestScheduleRelations().TestEntityUsingTestScheduleId;
		internal static readonly IEntityRelation LookupEntityUsingDiscountApplyLookupIdStatic = new TestScheduleRelations().LookupEntityUsingDiscountApplyLookupId;
		internal static readonly IEntityRelation LookupEntityUsingEvalPeriodTypeLookupIdStatic = new TestScheduleRelations().LookupEntityUsingEvalPeriodTypeLookupId;

		/// <summary>CTor</summary>
		static StaticTestScheduleRelations()
		{
		}
	}
}
