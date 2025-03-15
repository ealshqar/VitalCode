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
	/// <summary>Implements the relations factory for the entity: AutoProtocolStageRevision. </summary>
	public partial class AutoProtocolStageRevisionRelations
	{
		/// <summary>CTor</summary>
		public AutoProtocolStageRevisionRelations()
		{
		}

		/// <summary>Gets all relations of the AutoProtocolStageRevisionEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoTestResultEntityUsingAutoProtocolStageRevisionsId);
			toReturn.Add(this.AutoProtocolRevisionEntityUsingAutoProtocolRevisionsId);
			toReturn.Add(this.AutoProtocolStageEntityUsingAutoProtocolStagesId);
			toReturn.Add(this.AutoTestStageEntityUsingAutoTestStagesId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageRevisionEntity and AutoTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocolStageRevision.Id - AutoTestResult.AutoProtocolStageRevisionsId
		/// </summary>
		public virtual IEntityRelation AutoTestResultEntityUsingAutoProtocolStageRevisionsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(AutoProtocolStageRevisionFields.Id, AutoTestResultFields.AutoProtocolStageRevisionsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageRevisionEntity and AutoProtocolRevisionEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStageRevision.AutoProtocolRevisionsId - AutoProtocolRevision.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolRevisionEntityUsingAutoProtocolRevisionsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocolRevision", false);
				relation.AddEntityFieldPair(AutoProtocolRevisionFields.Id, AutoProtocolStageRevisionFields.AutoProtocolRevisionsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageRevisionEntity and AutoProtocolStageEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStageRevision.AutoProtocolStagesId - AutoProtocolStage.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageEntityUsingAutoProtocolStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocolStage", false);
				relation.AddEntityFieldPair(AutoProtocolStageFields.Id, AutoProtocolStageRevisionFields.AutoProtocolStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageRevisionEntity and AutoTestStageEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStageRevision.AutoTestStagesId - AutoTestStage.Id
		/// </summary>
		public virtual IEntityRelation AutoTestStageEntityUsingAutoTestStagesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoTestStage", false);
				relation.AddEntityFieldPair(AutoTestStageFields.Id, AutoProtocolStageRevisionFields.AutoTestStagesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoProtocolStageRevisionEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocolStageRevision.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolStageRevisionFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", true);
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
	internal static class StaticAutoProtocolStageRevisionRelations
	{
		internal static readonly IEntityRelation AutoTestResultEntityUsingAutoProtocolStageRevisionsIdStatic = new AutoProtocolStageRevisionRelations().AutoTestResultEntityUsingAutoProtocolStageRevisionsId;
		internal static readonly IEntityRelation AutoProtocolRevisionEntityUsingAutoProtocolRevisionsIdStatic = new AutoProtocolStageRevisionRelations().AutoProtocolRevisionEntityUsingAutoProtocolRevisionsId;
		internal static readonly IEntityRelation AutoProtocolStageEntityUsingAutoProtocolStagesIdStatic = new AutoProtocolStageRevisionRelations().AutoProtocolStageEntityUsingAutoProtocolStagesId;
		internal static readonly IEntityRelation AutoTestStageEntityUsingAutoTestStagesIdStatic = new AutoProtocolStageRevisionRelations().AutoTestStageEntityUsingAutoTestStagesId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoProtocolStageRevisionRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoProtocolStageRevisionRelations()
		{
		}
	}
}
