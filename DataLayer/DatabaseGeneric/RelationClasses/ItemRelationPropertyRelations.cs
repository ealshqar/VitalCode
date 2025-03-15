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
	/// <summary>Implements the relations factory for the entity: ItemRelationProperty. </summary>
	public partial class ItemRelationPropertyRelations
	{
		/// <summary>CTor</summary>
		public ItemRelationPropertyRelations()
		{
		}

		/// <summary>Gets all relations of the ItemRelationPropertyEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemRelationEntityUsingItemRelationId);
			toReturn.Add(this.PropertyEntityUsingPropertyId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between ItemRelationPropertyEntity and ItemRelationEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemRelationProperty.ItemRelationId - ItemRelation.Id
		/// </summary>
		public virtual IEntityRelation ItemRelationEntityUsingItemRelationId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ItemRelation", false);
				relation.AddEntityFieldPair(ItemRelationFields.Id, ItemRelationPropertyFields.ItemRelationId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationPropertyEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemRelationPropertyEntity and PropertyEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemRelationProperty.PropertyId - Property.Id
		/// </summary>
		public virtual IEntityRelation PropertyEntityUsingPropertyId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Property", false);
				relation.AddEntityFieldPair(PropertyFields.Id, ItemRelationPropertyFields.PropertyId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationPropertyEntity", true);
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
	internal static class StaticItemRelationPropertyRelations
	{
		internal static readonly IEntityRelation ItemRelationEntityUsingItemRelationIdStatic = new ItemRelationPropertyRelations().ItemRelationEntityUsingItemRelationId;
		internal static readonly IEntityRelation PropertyEntityUsingPropertyIdStatic = new ItemRelationPropertyRelations().PropertyEntityUsingPropertyId;

		/// <summary>CTor</summary>
		static StaticItemRelationPropertyRelations()
		{
		}
	}
}
