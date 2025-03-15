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
	/// <summary>Implements the relations factory for the entity: VFS. </summary>
	public partial class VFSRelations
	{
		/// <summary>CTor</summary>
		public VFSRelations()
		{
		}

		/// <summary>Gets all relations of the VFSEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.VFSItemEntityUsingVFSId);
			toReturn.Add(this.VFSSecondaryItemEntityUsingVfsId);
			toReturn.Add(this.PatientEntityUsingPatientId);
			toReturn.Add(this.TestEntityUsingTestId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between VFSEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// VFS.Id - VFSItem.VFSId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingVFSId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "VFSItems" , true);
				relation.AddEntityFieldPair(VFSFields.Id, VFSItemFields.VFSId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between VFSEntity and VFSSecondaryItemEntity over the 1:n relation they have, using the relation between the fields:
		/// VFS.Id - VFSSecondaryItem.VfsId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemEntityUsingVfsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "VFSSecondaryItems" , true);
				relation.AddEntityFieldPair(VFSFields.Id, VFSSecondaryItemFields.VfsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between VFSEntity and PatientEntity over the m:1 relation they have, using the relation between the fields:
		/// VFS.PatientId - Patient.Id
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Patient", false);
				relation.AddEntityFieldPair(PatientFields.Id, VFSFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSEntity and TestEntity over the m:1 relation they have, using the relation between the fields:
		/// VFS.TestId - Test.Id
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Test", false);
				relation.AddEntityFieldPair(TestFields.Id, VFSFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between VFSEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// VFS.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, VFSFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", true);
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
	internal static class StaticVFSRelations
	{
		internal static readonly IEntityRelation VFSItemEntityUsingVFSIdStatic = new VFSRelations().VFSItemEntityUsingVFSId;
		internal static readonly IEntityRelation VFSSecondaryItemEntityUsingVfsIdStatic = new VFSRelations().VFSSecondaryItemEntityUsingVfsId;
		internal static readonly IEntityRelation PatientEntityUsingPatientIdStatic = new VFSRelations().PatientEntityUsingPatientId;
		internal static readonly IEntityRelation TestEntityUsingTestIdStatic = new VFSRelations().TestEntityUsingTestId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new VFSRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticVFSRelations()
		{
		}
	}
}
