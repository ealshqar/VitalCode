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
	/// <summary>Implements the relations factory for the entity: Service. </summary>
	public partial class ServiceRelations
	{
		/// <summary>CTor</summary>
		public ServiceRelations()
		{
		}

		/// <summary>Gets all relations of the ServiceEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.TestServiceEntityUsingServiceId);
			toReturn.Add(this.LookupEntityUsingTypeLookupId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ServiceEntity and TestServiceEntity over the 1:n relation they have, using the relation between the fields:
		/// Service.Id - TestService.ServiceId
		/// </summary>
		public virtual IEntityRelation TestServiceEntityUsingServiceId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ServiceFields.Id, TestServiceFields.ServiceId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ServiceEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestServiceEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ServiceEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// Service.TypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TypeLookup", false);
				relation.AddEntityFieldPair(LookupFields.Id, ServiceFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ServiceEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ServiceEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Service.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ServiceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ServiceEntity", true);
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
	internal static class StaticServiceRelations
	{
		internal static readonly IEntityRelation TestServiceEntityUsingServiceIdStatic = new ServiceRelations().TestServiceEntityUsingServiceId;
		internal static readonly IEntityRelation LookupEntityUsingTypeLookupIdStatic = new ServiceRelations().LookupEntityUsingTypeLookupId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ServiceRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticServiceRelations()
		{
		}
	}
}
