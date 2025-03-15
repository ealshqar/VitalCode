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
	/// <summary>Implements the relations factory for the entity: Reading. </summary>
	public partial class ReadingRelations
	{
		/// <summary>CTor</summary>
		public ReadingRelations()
		{
		}

		/// <summary>Gets all relations of the ReadingEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.ItemEntityUsingPointSetItemId);
			toReturn.Add(this.LookupEntityUsingListPointLookupId);
			toReturn.Add(this.TestEntityUsingTestId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between ReadingEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// Reading.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, ReadingFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ReadingEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// Reading.PointSetItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingPointSetItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "PointSetItem", false);
				relation.AddEntityFieldPair(ItemFields.Id, ReadingFields.PointSetItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ReadingEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Reading.ListPointLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingListPointLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ListPointLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ReadingFields.ListPointLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ReadingEntity and TestEntity over the m:1 relation they have, using the relation between the fields:
		/// Reading.TestId - Test.Id
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Test", false);
				relation.AddEntityFieldPair(TestFields.Id, ReadingFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ReadingEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Reading.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ReadingFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", true);
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
	internal static class StaticReadingRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new ReadingRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation ItemEntityUsingPointSetItemIdStatic = new ReadingRelations().ItemEntityUsingPointSetItemId;
		internal static readonly IEntityRelation LookupEntityUsingListPointLookupIdStatic = new ReadingRelations().LookupEntityUsingListPointLookupId;
		internal static readonly IEntityRelation TestEntityUsingTestIdStatic = new ReadingRelations().TestEntityUsingTestId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ReadingRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticReadingRelations()
		{
		}
	}
}
