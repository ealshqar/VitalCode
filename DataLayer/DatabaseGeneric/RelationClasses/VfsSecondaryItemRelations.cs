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
	/// <summary>Implements the relations factory for the entity: VFSSecondaryItem. </summary>
	public partial class VFSSecondaryItemRelations
	{
		/// <summary>CTor</summary>
		public VFSSecondaryItemRelations()
		{
		}

		/// <summary>Gets all relations of the VFSSecondaryItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.LookupEntityUsingSectionLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			toReturn.Add(this.VFSEntityUsingVfsId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between VFSSecondaryItemEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSSecondaryItem.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, VFSSecondaryItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSSecondaryItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSSecondaryItem.SectionLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "SectionLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSSecondaryItemFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSSecondaryItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSSecondaryItem.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, VFSSecondaryItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSSecondaryItemEntity and VFSEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSSecondaryItem.VfsId - VFS.Id
		/// </summary>
		public virtual IEntityRelation VFSEntityUsingVfsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "VFS", false);
				relation.AddEntityFieldPair(VFSFields.Id, VFSSecondaryItemFields.VfsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", true);
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
	internal static class StaticVFSSecondaryItemRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new VFSSecondaryItemRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation LookupEntityUsingSectionLookupIdStatic = new VFSSecondaryItemRelations().LookupEntityUsingSectionLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new VFSSecondaryItemRelations().UserEntityUsingUserId;
		internal static readonly IEntityRelation VFSEntityUsingVfsIdStatic = new VFSSecondaryItemRelations().VFSEntityUsingVfsId;

		/// <summary>CTor</summary>
		static StaticVFSSecondaryItemRelations()
		{
		}
	}
}
