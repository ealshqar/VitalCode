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
	/// <summary>Implements the relations factory for the entity: VFSItemSource. </summary>
	public partial class VFSItemSourceRelations
	{
		/// <summary>CTor</summary>
		public VFSItemSourceRelations()
		{
		}

		/// <summary>Gets all relations of the VFSItemSourceEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.VFSItemEntityUsingVFSitemSourceId);
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.LookupEntityUsingGenderLookupId);
			toReturn.Add(this.LookupEntityUsingGridGroupLookupId);
			toReturn.Add(this.LookupEntityUsingGroupLookupId);
			toReturn.Add(this.LookupEntityUsingSectionLookupId);
			toReturn.Add(this.LookupEntityUsingV1TypeLookupId);
			toReturn.Add(this.LookupEntityUsingV2TypeLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// VFSItemSource.Id - VFSItem.VFSitemSourceId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingVFSitemSourceId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(VFSItemSourceFields.Id, VFSItemFields.VFSitemSourceId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, VFSItemSourceFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.GenderLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "GenderLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.GridGroupLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGridGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "GridGroupLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.GridGroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.GroupLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "GroupLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.GroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.SectionLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "SectionLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.V1TypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingV1TypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "V1TypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.V1TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.V2TypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingV2TypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "V2TypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.V2TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSItemSourceEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// VFSItemSource.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, VFSItemSourceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", true);
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
	internal static class StaticVFSItemSourceRelations
	{
		internal static readonly IEntityRelation VFSItemEntityUsingVFSitemSourceIdStatic = new VFSItemSourceRelations().VFSItemEntityUsingVFSitemSourceId;
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new VFSItemSourceRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation LookupEntityUsingGenderLookupIdStatic = new VFSItemSourceRelations().LookupEntityUsingGenderLookupId;
		internal static readonly IEntityRelation LookupEntityUsingGridGroupLookupIdStatic = new VFSItemSourceRelations().LookupEntityUsingGridGroupLookupId;
		internal static readonly IEntityRelation LookupEntityUsingGroupLookupIdStatic = new VFSItemSourceRelations().LookupEntityUsingGroupLookupId;
		internal static readonly IEntityRelation LookupEntityUsingSectionLookupIdStatic = new VFSItemSourceRelations().LookupEntityUsingSectionLookupId;
		internal static readonly IEntityRelation LookupEntityUsingV1TypeLookupIdStatic = new VFSItemSourceRelations().LookupEntityUsingV1TypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingV2TypeLookupIdStatic = new VFSItemSourceRelations().LookupEntityUsingV2TypeLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new VFSItemSourceRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticVFSItemSourceRelations()
		{
		}
	}
}
