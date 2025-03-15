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
	/// <summary>Implements the relations factory for the entity: Patient. </summary>
	public partial class PatientRelations
	{
		/// <summary>CTor</summary>
		public PatientRelations()
		{
		}

		/// <summary>Gets all relations of the PatientEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoTestEntityUsingPatientId);
			toReturn.Add(this.FrequencyTestEntityUsingPatientId);
			toReturn.Add(this.PatientHistoryEntityUsingPatientId);
			toReturn.Add(this.SpotCheckEntityUsingPatientId);
			toReturn.Add(this.TestEntityUsingPatientId);
			toReturn.Add(this.VFSEntityUsingPatientId);
			toReturn.Add(this.LookupEntityUsingGenderLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between PatientEntity and AutoTestEntity over the 1:n relation they have, using the relation between the fields:
		/// Patient.Id - AutoTest.PatientId
		/// </summary>
		public virtual IEntityRelation AutoTestEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoTests" , true);
				relation.AddEntityFieldPair(PatientFields.Id, AutoTestFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between PatientEntity and FrequencyTestEntity over the 1:n relation they have, using the relation between the fields:
		/// Patient.Id - FrequencyTest.PatientId
		/// </summary>
		public virtual IEntityRelation FrequencyTestEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "FrequencyTests" , true);
				relation.AddEntityFieldPair(PatientFields.Id, FrequencyTestFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between PatientEntity and PatientHistoryEntity over the 1:n relation they have, using the relation between the fields:
		/// Patient.Id - PatientHistory.PatientId
		/// </summary>
		public virtual IEntityRelation PatientHistoryEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "PatientHistory" , true);
				relation.AddEntityFieldPair(PatientFields.Id, PatientHistoryFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientHistoryEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between PatientEntity and SpotCheckEntity over the 1:n relation they have, using the relation between the fields:
		/// Patient.Id - SpotCheck.PatientId
		/// </summary>
		public virtual IEntityRelation SpotCheckEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotChecks" , true);
				relation.AddEntityFieldPair(PatientFields.Id, SpotCheckFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between PatientEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// Patient.Id - Test.PatientId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Tests" , true);
				relation.AddEntityFieldPair(PatientFields.Id, TestFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between PatientEntity and VFSEntity over the 1:n relation they have, using the relation between the fields:
		/// Patient.Id - VFS.PatientId
		/// </summary>
		public virtual IEntityRelation VFSEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "VFSRecords" , true);
				relation.AddEntityFieldPair(PatientFields.Id, VFSFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between PatientEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Patient.GenderLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Lookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, PatientFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between PatientEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Patient.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, PatientFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", true);
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
	internal static class StaticPatientRelations
	{
		internal static readonly IEntityRelation AutoTestEntityUsingPatientIdStatic = new PatientRelations().AutoTestEntityUsingPatientId;
		internal static readonly IEntityRelation FrequencyTestEntityUsingPatientIdStatic = new PatientRelations().FrequencyTestEntityUsingPatientId;
		internal static readonly IEntityRelation PatientHistoryEntityUsingPatientIdStatic = new PatientRelations().PatientHistoryEntityUsingPatientId;
		internal static readonly IEntityRelation SpotCheckEntityUsingPatientIdStatic = new PatientRelations().SpotCheckEntityUsingPatientId;
		internal static readonly IEntityRelation TestEntityUsingPatientIdStatic = new PatientRelations().TestEntityUsingPatientId;
		internal static readonly IEntityRelation VFSEntityUsingPatientIdStatic = new PatientRelations().VFSEntityUsingPatientId;
		internal static readonly IEntityRelation LookupEntityUsingGenderLookupIdStatic = new PatientRelations().LookupEntityUsingGenderLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new PatientRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticPatientRelations()
		{
		}
	}
}
