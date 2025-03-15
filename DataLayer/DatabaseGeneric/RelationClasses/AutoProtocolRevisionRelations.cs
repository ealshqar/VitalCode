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
	/// <summary>Implements the relations factory for the entity: AutoProtocolRevision. </summary>
	public partial class AutoProtocolRevisionRelations
	{
		/// <summary>CTor</summary>
		public AutoProtocolRevisionRelations()
		{
		}

		/// <summary>Gets all relations of the AutoProtocolRevisionEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoProtocolStageRevisionEntityUsingAutoProtocolRevisionsId);
			toReturn.Add(this.AutoTestEntityUsingAutoProtocolRevisionsId);
			toReturn.Add(this.AutoProtocolEntityUsingAutoProtocolsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolRevisionEntity and AutoProtocolStageRevisionEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocolRevision.Id - AutoProtocolStageRevision.AutoProtocolRevisionsId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageRevisionEntityUsingAutoProtocolRevisionsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoProtocolStageRevisions" , true);
				relation.AddEntityFieldPair(AutoProtocolRevisionFields.Id, AutoProtocolStageRevisionFields.AutoProtocolRevisionsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolRevisionEntity and AutoTestEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocolRevision.Id - AutoTest.AutoProtocolRevisionsId
		/// </summary>
		public virtual IEntityRelation AutoTestEntityUsingAutoProtocolRevisionsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(AutoProtocolRevisionFields.Id, AutoTestFields.AutoProtocolRevisionsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoProtocolRevisionEntity and AutoProtocolEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolRevision.AutoProtocolsId - AutoProtocol.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolEntityUsingAutoProtocolsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocol", false);
				relation.AddEntityFieldPair(AutoProtocolFields.Id, AutoProtocolRevisionFields.AutoProtocolsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolRevisionEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolRevision.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolRevisionFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", true);
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
	internal static class StaticAutoProtocolRevisionRelations
	{
		internal static readonly IEntityRelation AutoProtocolStageRevisionEntityUsingAutoProtocolRevisionsIdStatic = new AutoProtocolRevisionRelations().AutoProtocolStageRevisionEntityUsingAutoProtocolRevisionsId;
		internal static readonly IEntityRelation AutoTestEntityUsingAutoProtocolRevisionsIdStatic = new AutoProtocolRevisionRelations().AutoTestEntityUsingAutoProtocolRevisionsId;
		internal static readonly IEntityRelation AutoProtocolEntityUsingAutoProtocolsIdStatic = new AutoProtocolRevisionRelations().AutoProtocolEntityUsingAutoProtocolsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoProtocolRevisionRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoProtocolRevisionRelations()
		{
		}
	}
}
