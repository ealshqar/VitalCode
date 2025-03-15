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
	/// <summary>Implements the relations factory for the entity: FrequencyTest. </summary>
	public partial class FrequencyTestRelations
	{
		/// <summary>CTor</summary>
		public FrequencyTestRelations()
		{
		}

		/// <summary>Gets all relations of the FrequencyTestEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.FrequencyTestResultEntityUsingFrequencyTestId);
			toReturn.Add(this.PatientEntityUsingPatientId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between FrequencyTestEntity and FrequencyTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// FrequencyTest.Id - FrequencyTestResult.FrequencyTestId
		/// </summary>
		public virtual IEntityRelation FrequencyTestResultEntityUsingFrequencyTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "FrequencyTestResults" , true);
				relation.AddEntityFieldPair(FrequencyTestFields.Id, FrequencyTestResultFields.FrequencyTestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestResultEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between FrequencyTestEntity and PatientEntity over the m:1 relation they have, using the relation between the fields:
		/// FrequencyTest.PatientId - Patient.Id
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Patient", false);
				relation.AddEntityFieldPair(PatientFields.Id, FrequencyTestFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between FrequencyTestEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// FrequencyTest.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, FrequencyTestFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestEntity", true);
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
	internal static class StaticFrequencyTestRelations
	{
		internal static readonly IEntityRelation FrequencyTestResultEntityUsingFrequencyTestIdStatic = new FrequencyTestRelations().FrequencyTestResultEntityUsingFrequencyTestId;
		internal static readonly IEntityRelation PatientEntityUsingPatientIdStatic = new FrequencyTestRelations().PatientEntityUsingPatientId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new FrequencyTestRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticFrequencyTestRelations()
		{
		}
	}
}
