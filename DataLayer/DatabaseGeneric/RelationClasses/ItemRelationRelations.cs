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
	/// <summary>Implements the relations factory for the entity: ItemRelation. </summary>
	public partial class ItemRelationRelations
	{
		/// <summary>CTor</summary>
		public ItemRelationRelations()
		{
		}

		/// <summary>Gets all relations of the ItemRelationEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemRelationPropertyEntityUsingItemRelationId);
			toReturn.Add(this.ItemEntityUsingItemChildId);
			toReturn.Add(this.ItemEntityUsingItemParentId);
			toReturn.Add(this.LookupEntityUsingRelationTypeLookupId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ItemRelationEntity and ItemRelationPropertyEntity over the 1:n relation they have, using the relation between the fields:
		/// ItemRelation.Id - ItemRelationProperty.ItemRelationId
		/// </summary>
		public virtual IEntityRelation ItemRelationPropertyEntityUsingItemRelationId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Properties" , true);
				relation.AddEntityFieldPair(ItemRelationFields.Id, ItemRelationPropertyFields.ItemRelationId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationPropertyEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ItemRelationEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemRelation.ItemChildId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemChildId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Child", false);
				relation.AddEntityFieldPair(ItemFields.Id, ItemRelationFields.ItemChildId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemRelationEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemRelation.ItemParentId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Parent", false);
				relation.AddEntityFieldPair(ItemFields.Id, ItemRelationFields.ItemParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemRelationEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemRelation.RelationTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingRelationTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "RelationTypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ItemRelationFields.RelationTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", true);
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
	internal static class StaticItemRelationRelations
	{
		internal static readonly IEntityRelation ItemRelationPropertyEntityUsingItemRelationIdStatic = new ItemRelationRelations().ItemRelationPropertyEntityUsingItemRelationId;
		internal static readonly IEntityRelation ItemEntityUsingItemChildIdStatic = new ItemRelationRelations().ItemEntityUsingItemChildId;
		internal static readonly IEntityRelation ItemEntityUsingItemParentIdStatic = new ItemRelationRelations().ItemEntityUsingItemParentId;
		internal static readonly IEntityRelation LookupEntityUsingRelationTypeLookupIdStatic = new ItemRelationRelations().LookupEntityUsingRelationTypeLookupId;

		/// <summary>CTor</summary>
		static StaticItemRelationRelations()
		{
		}
	}
}
