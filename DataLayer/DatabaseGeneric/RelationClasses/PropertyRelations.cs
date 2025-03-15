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
	/// <summary>Implements the relations factory for the entity: Property. </summary>
	public partial class PropertyRelations
	{
		/// <summary>CTor</summary>
		public PropertyRelations()
		{
		}

		/// <summary>Gets all relations of the PropertyEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemPropertyEntityUsingPropertyId);
			toReturn.Add(this.ItemRelationPropertyEntityUsingPropertyId);
			toReturn.Add(this.LookupEntityUsingApplicableTypeLookupId);
			toReturn.Add(this.LookupEntityUsingValueTypeLookupId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between PropertyEntity and ItemPropertyEntity over the 1:n relation they have, using the relation between the fields:
		/// Property.Id - ItemProperty.PropertyId
		/// </summary>
		public virtual IEntityRelation ItemPropertyEntityUsingPropertyId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(PropertyFields.Id, ItemPropertyFields.PropertyId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemPropertyEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between PropertyEntity and ItemRelationPropertyEntity over the 1:n relation they have, using the relation between the fields:
		/// Property.Id - ItemRelationProperty.PropertyId
		/// </summary>
		public virtual IEntityRelation ItemRelationPropertyEntityUsingPropertyId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(PropertyFields.Id, ItemRelationPropertyFields.PropertyId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationPropertyEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between PropertyEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Property.ApplicableTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingApplicableTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ApplicableTypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, PropertyFields.ApplicableTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between PropertyEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Property.ValueTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingValueTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ValueTypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, PropertyFields.ValueTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", true);
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
	internal static class StaticPropertyRelations
	{
		internal static readonly IEntityRelation ItemPropertyEntityUsingPropertyIdStatic = new PropertyRelations().ItemPropertyEntityUsingPropertyId;
		internal static readonly IEntityRelation ItemRelationPropertyEntityUsingPropertyIdStatic = new PropertyRelations().ItemRelationPropertyEntityUsingPropertyId;
		internal static readonly IEntityRelation LookupEntityUsingApplicableTypeLookupIdStatic = new PropertyRelations().LookupEntityUsingApplicableTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingValueTypeLookupIdStatic = new PropertyRelations().LookupEntityUsingValueTypeLookupId;

		/// <summary>CTor</summary>
		static StaticPropertyRelations()
		{
		}
	}
}
