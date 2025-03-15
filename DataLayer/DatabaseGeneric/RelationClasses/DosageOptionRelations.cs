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
	/// <summary>Implements the relations factory for the entity: DosageOption. </summary>
	public partial class DosageOptionRelations
	{
		/// <summary>CTor</summary>
		public DosageOptionRelations()
		{
		}

		/// <summary>Gets all relations of the DosageOptionEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ProductFormEntityUsingProductFormsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between DosageOptionEntity and ProductFormEntity over the m:1 relation they have, using the relation between the fields:
		/// DosageOption.ProductFormsId - ProductForm.Id
		/// </summary>
		public virtual IEntityRelation ProductFormEntityUsingProductFormsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ProductForm", false);
				relation.AddEntityFieldPair(ProductFormFields.Id, DosageOptionFields.ProductFormsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DosageOptionEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between DosageOptionEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// DosageOption.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, DosageOptionFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DosageOptionEntity", true);
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
	internal static class StaticDosageOptionRelations
	{
		internal static readonly IEntityRelation ProductFormEntityUsingProductFormsIdStatic = new DosageOptionRelations().ProductFormEntityUsingProductFormsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new DosageOptionRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticDosageOptionRelations()
		{
		}
	}
}
