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
	/// <summary>Implements the relations factory for the entity: AutoTestResult. </summary>
	public partial class AutoTestResultRelations
	{
		/// <summary>CTor</summary>
		public AutoTestResultRelations()
		{
		}

		/// <summary>Gets all relations of the AutoTestResultEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoTestResultEntityUsingAutoTestResultsParentId);
			toReturn.Add(this.AutoTestResultProductEntityUsingAutoTestResultsId);
			toReturn.Add(this.AutoItemEntityUsingAutoItemsId);
			toReturn.Add(this.AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsId);
			toReturn.Add(this.AutoTestEntityUsingAutoTestsId);
			toReturn.Add(this.AutoTestResultEntityUsingIdAutoTestResultsParentId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and AutoTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoTestResult.Id - AutoTestResult.AutoTestResultsParentId
		/// </summary>
		public virtual IEntityRelation AutoTestResultEntityUsingAutoTestResultsParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoTestResultChildes" , true);
				relation.AddEntityFieldPair(AutoTestResultFields.Id, AutoTestResultFields.AutoTestResultsParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and AutoTestResultProductEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoTestResult.Id - AutoTestResultProduct.AutoTestResultsId
		/// </summary>
		public virtual IEntityRelation AutoTestResultProductEntityUsingAutoTestResultsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoTestResultProducts" , true);
				relation.AddEntityFieldPair(AutoTestResultFields.Id, AutoTestResultProductFields.AutoTestResultsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultProductEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and AutoItemEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestResult.AutoItemsId - AutoItem.Id
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingAutoItemsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoItem", false);
				relation.AddEntityFieldPair(AutoItemFields.Id, AutoTestResultFields.AutoItemsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and AutoProtocolStageRevisionEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestResult.AutoProtocolStageRevisionsId - AutoProtocolStageRevision.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocolStageRevision", false);
				relation.AddEntityFieldPair(AutoProtocolStageRevisionFields.Id, AutoTestResultFields.AutoProtocolStageRevisionsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and AutoTestEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestResult.AutoTestsId - AutoTest.Id
		/// </summary>
		public virtual IEntityRelation AutoTestEntityUsingAutoTestsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoTest", false);
				relation.AddEntityFieldPair(AutoTestFields.Id, AutoTestResultFields.AutoTestsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and AutoTestResultEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestResult.AutoTestResultsParentId - AutoTestResult.Id
		/// </summary>
		public virtual IEntityRelation AutoTestResultEntityUsingIdAutoTestResultsParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoTestResultParent", false);
				relation.AddEntityFieldPair(AutoTestResultFields.Id, AutoTestResultFields.AutoTestResultsParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestResultEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTestResult.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", true);
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
	internal static class StaticAutoTestResultRelations
	{
		internal static readonly IEntityRelation AutoTestResultEntityUsingAutoTestResultsParentIdStatic = new AutoTestResultRelations().AutoTestResultEntityUsingAutoTestResultsParentId;
		internal static readonly IEntityRelation AutoTestResultProductEntityUsingAutoTestResultsIdStatic = new AutoTestResultRelations().AutoTestResultProductEntityUsingAutoTestResultsId;
		internal static readonly IEntityRelation AutoItemEntityUsingAutoItemsIdStatic = new AutoTestResultRelations().AutoItemEntityUsingAutoItemsId;
		internal static readonly IEntityRelation AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsIdStatic = new AutoTestResultRelations().AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsId;
		internal static readonly IEntityRelation AutoTestEntityUsingAutoTestsIdStatic = new AutoTestResultRelations().AutoTestEntityUsingAutoTestsId;
		internal static readonly IEntityRelation AutoTestResultEntityUsingIdAutoTestResultsParentIdStatic = new AutoTestResultRelations().AutoTestResultEntityUsingIdAutoTestResultsParentId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoTestResultRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoTestResultRelations()
		{
		}
	}
}
