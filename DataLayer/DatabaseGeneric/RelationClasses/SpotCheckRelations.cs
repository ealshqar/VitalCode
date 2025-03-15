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
	/// <summary>Implements the relations factory for the entity: SpotCheck. </summary>
	public partial class SpotCheckRelations
	{
		/// <summary>CTor</summary>
		public SpotCheckRelations()
		{
		}

		/// <summary>Gets all relations of the SpotCheckEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.SpotCheckResultEntityUsingSpotCheckId);
			toReturn.Add(this.PatientEntityUsingPatientId);
			toReturn.Add(this.TestEntityUsingTestId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between SpotCheckEntity and SpotCheckResultEntity over the 1:n relation they have, using the relation between the fields:
		/// SpotCheck.Id - SpotCheckResult.SpotCheckId
		/// </summary>
		public virtual IEntityRelation SpotCheckResultEntityUsingSpotCheckId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotCheckResults" , true);
				relation.AddEntityFieldPair(SpotCheckFields.Id, SpotCheckResultFields.SpotCheckId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between SpotCheckEntity and PatientEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheck.PatientId - Patient.Id
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingPatientId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Patient", false);
				relation.AddEntityFieldPair(PatientFields.Id, SpotCheckFields.PatientId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between SpotCheckEntity and TestEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheck.TestId - Test.Id
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Test", false);
				relation.AddEntityFieldPair(TestFields.Id, SpotCheckFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between SpotCheckEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// SpotCheck.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, SpotCheckFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", true);
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
	internal static class StaticSpotCheckRelations
	{
		internal static readonly IEntityRelation SpotCheckResultEntityUsingSpotCheckIdStatic = new SpotCheckRelations().SpotCheckResultEntityUsingSpotCheckId;
		internal static readonly IEntityRelation PatientEntityUsingPatientIdStatic = new SpotCheckRelations().PatientEntityUsingPatientId;
		internal static readonly IEntityRelation TestEntityUsingTestIdStatic = new SpotCheckRelations().TestEntityUsingTestId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new SpotCheckRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticSpotCheckRelations()
		{
		}
	}
}
