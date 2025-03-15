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
	/// <summary>Implements the relations factory for the entity: VFSItem. </summary>
	public partial class VFSItemRelations
	{
		/// <summary>CTor</summary>
		public VFSItemRelations()
		{
		}

		/// <summary>Gets all relations of the VFSItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.LookupEntityUsingGridGroupLookupId);
			toReturn.Add(this.LookupEntityUsingGroupLookupId);
			toReturn.Add(this.LookupEntityUsingSectionLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			toReturn.Add(this.VFSEntityUsingVFSId);
			toReturn.Add(this.VFSItemSourceEntityUsingVFSitemSourceId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, VFSItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.GridGroupLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGridGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "GridGroupLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemFields.GridGroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.GroupLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "GroupLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemFields.GroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.SectionLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "SectionLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, VFSItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and VFSEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.VFSId - VFS.Id
		/// </summary>
		public virtual IEntityRelation VFSEntityUsingVFSId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "VFS", false);
				relation.AddEntityFieldPair(VFSFields.Id, VFSItemFields.VFSId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemEntity and VFSItemSourceEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItem.VFSitemSourceId - VFSItemSource.Id
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingVFSitemSourceId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "VFSItemSource", false);
				relation.AddEntityFieldPair(VFSItemSourceFields.Id, VFSItemFields.VFSitemSourceId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", true);
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
	internal static class StaticVFSItemRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new VFSItemRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation LookupEntityUsingGridGroupLookupIdStatic = new VFSItemRelations().LookupEntityUsingGridGroupLookupId;
		internal static readonly IEntityRelation LookupEntityUsingGroupLookupIdStatic = new VFSItemRelations().LookupEntityUsingGroupLookupId;
		internal static readonly IEntityRelation LookupEntityUsingSectionLookupIdStatic = new VFSItemRelations().LookupEntityUsingSectionLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new VFSItemRelations().UserEntityUsingUserId;
		internal static readonly IEntityRelation VFSEntityUsingVFSIdStatic = new VFSItemRelations().VFSEntityUsingVFSId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingVFSitemSourceIdStatic = new VFSItemRelations().VFSItemSourceEntityUsingVFSitemSourceId;

		/// <summary>CTor</summary>
		static StaticVFSItemRelations()
		{
		}
	}
}
