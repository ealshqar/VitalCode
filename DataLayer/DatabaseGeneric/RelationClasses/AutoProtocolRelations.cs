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
	/// <summary>Implements the relations factory for the entity: AutoProtocol. </summary>
	public partial class AutoProtocolRelations
	{
		/// <summary>CTor</summary>
		public AutoProtocolRelations()
		{
		}

		/// <summary>Gets all relations of the AutoProtocolEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoProtocolRevisionEntityUsingAutoProtocolsId);
			toReturn.Add(this.AutoProtocolStageEntityUsingAutoProtocolsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolEntity and AutoProtocolRevisionEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocol.Id - AutoProtocolRevision.AutoProtocolsId
		/// </summary>
		public virtual IEntityRelation AutoProtocolRevisionEntityUsingAutoProtocolsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoProtocolRevisions" , true);
				relation.AddEntityFieldPair(AutoProtocolFields.Id, AutoProtocolRevisionFields.AutoProtocolsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoProtocolEntity and AutoProtocolStageEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoProtocol.Id - AutoProtocolStage.AutoProtocolsId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageEntityUsingAutoProtocolsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoProtocolStages" , true);
				relation.AddEntityFieldPair(AutoProtocolFields.Id, AutoProtocolStageFields.AutoProtocolsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoProtocolEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoProtocol.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolEntity", true);
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
	internal static class StaticAutoProtocolRelations
	{
		internal static readonly IEntityRelation AutoProtocolRevisionEntityUsingAutoProtocolsIdStatic = new AutoProtocolRelations().AutoProtocolRevisionEntityUsingAutoProtocolsId;
		internal static readonly IEntityRelation AutoProtocolStageEntityUsingAutoProtocolsIdStatic = new AutoProtocolRelations().AutoProtocolStageEntityUsingAutoProtocolsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoProtocolRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoProtocolRelations()
		{
		}
	}
}
