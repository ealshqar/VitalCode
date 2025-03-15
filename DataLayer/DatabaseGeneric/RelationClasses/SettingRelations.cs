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
	/// <summary>Implements the relations factory for the entity: Setting. </summary>
	public partial class SettingRelations
	{
		/// <summary>CTor</summary>
		public SettingRelations()
		{
		}

		/// <summary>Gets all relations of the SettingEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.LookupEntityUsingSettingGroupLookupId);
			toReturn.Add(this.LookupEntityUsingValueTypeLookupId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between SettingEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Setting.SettingGroupLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingSettingGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "SettingGroupLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, SettingFields.SettingGroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SettingEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between SettingEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Setting.ValueTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingValueTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ValueTypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, SettingFields.ValueTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SettingEntity", true);
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
	internal static class StaticSettingRelations
	{
		internal static readonly IEntityRelation LookupEntityUsingSettingGroupLookupIdStatic = new SettingRelations().LookupEntityUsingSettingGroupLookupId;
		internal static readonly IEntityRelation LookupEntityUsingValueTypeLookupIdStatic = new SettingRelations().LookupEntityUsingValueTypeLookupId;

		/// <summary>CTor</summary>
		static StaticSettingRelations()
		{
		}
	}
}
