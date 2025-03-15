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
	/// <summary>Implements the relations factory for the entity: AutoTest. </summary>
	public partial class AutoTestRelations
	{
		/// <summary>CTor</summary>
		public AutoTestRelations()
		{
		}

		/// <summary>Gets all relations of the AutoTestEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoTestResultEntityUsingAutoTestsId);
			toReturn.Add(this.AutoProtocolRevisionEntityUsingAutoProtocolRevisionsId);
			toReturn.Add(this.PatientEntityUsingPatientId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoTestEntity and AutoTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoTest.Id - AutoTestResult.AutoTestsId
		/// </summary>
		public virtual IEntityRelation AutoTestResultEntityUsingAutoTestsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoTestResults" , true);
				relation.AddEntityFieldPair(AutoTestFields.Id, AutoTestResultFields.AutoTestsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoTestEntity and AutoProtocolRevisionEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTest.AutoProtocolRevisionsId - AutoProtocolRevision.Id
		/// </summary>
		public virtual IEntityRelation AutoProtocolRevisionEntityUsingAutoProtocolRevisionsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoProtocolRevision", false);
				relation.AddEntityFieldPair(AutoProtocolRevisionFields.Id, AutoTestFields.AutoProtocolRevisionsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestEntity and PatientEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTest.PatientId - Patient.Id
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Patient", false);
				relation.AddEntityFieldPair(PatientFields.Id, AutoTestFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoTestEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoTest.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", true);
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
	internal static class StaticAutoTestRelations
	{
		internal static readonly IEntityRelation AutoTestResultEntityUsingAutoTestsIdStatic = new AutoTestRelations().AutoTestResultEntityUsingAutoTestsId;
		internal static readonly IEntityRelation AutoProtocolRevisionEntityUsingAutoProtocolRevisionsIdStatic = new AutoTestRelations().AutoProtocolRevisionEntityUsingAutoProtocolRevisionsId;
		internal static readonly IEntityRelation PatientEntityUsingPatientIdStatic = new AutoTestRelations().PatientEntityUsingPatientId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoTestRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoTestRelations()
		{
		}
	}
}
