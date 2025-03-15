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
	/// <summary>Implements the relations factory for the entity: ScheduleLine. </summary>
	public partial class ScheduleLineRelations
	{
		/// <summary>CTor</summary>
		public ScheduleLineRelations()
		{
		}

		/// <summary>Gets all relations of the ScheduleLineEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.TestScheduleEntityUsingTestScheduleId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between ScheduleLineEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// ScheduleLine.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, ScheduleLineFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ScheduleLineEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ScheduleLineEntity and TestScheduleEntity over the m:1 relation they have, using the relation between the fields:
		/// ScheduleLine.TestScheduleId - TestSchedule.Id
		/// </summary>
		public virtual IEntityRelation TestScheduleEntityUsingTestScheduleId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestSchedule", false);
				relation.AddEntityFieldPair(TestScheduleFields.Id, ScheduleLineFields.TestScheduleId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ScheduleLineEntity", true);
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
	internal static class StaticScheduleLineRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new ScheduleLineRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation TestScheduleEntityUsingTestScheduleIdStatic = new ScheduleLineRelations().TestScheduleEntityUsingTestScheduleId;

		/// <summary>CTor</summary>
		static StaticScheduleLineRelations()
		{
		}
	}
}
