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
	/// <summary>Implements the relations factory for the entity: ProductSize. </summary>
	public partial class ProductSizeRelations
	{
		/// <summary>CTor</summary>
		public ProductSizeRelations()
		{
		}

		/// <summary>Gets all relations of the ProductSizeEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoTestResultProductEntityUsingProductSizesId);
			toReturn.Add(this.LookupEntityUsingStatusLookupsId);
			toReturn.Add(this.ProductFormEntityUsingProductFormsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ProductSizeEntity and AutoTestResultProductEntity over the 1:n relation they have, using the relation between the fields:
		/// ProductSize.Id - AutoTestResultProduct.ProductSizesId
		/// </summary>
		public virtual IEntityRelation AutoTestResultProductEntityUsingProductSizesId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoTestResultProducts" , true);
				relation.AddEntityFieldPair(ProductSizeFields.Id, AutoTestResultProductFields.ProductSizesId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultProductEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ProductSizeEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// ProductSize.StatusLookupsId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStatusLookupsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Status", false);
				relation.AddEntityFieldPair(LookupFields.Id, ProductSizeFields.StatusLookupsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProductSizeEntity and ProductFormEntity over the m:1 relation they have, using the relation between the fields:
		/// ProductSize.ProductFormsId - ProductForm.Id
		/// </summary>
		public virtual IEntityRelation ProductFormEntityUsingProductFormsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ProductForm", false);
				relation.AddEntityFieldPair(ProductFormFields.Id, ProductSizeFields.ProductFormsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProductSizeEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// ProductSize.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ProductSizeFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", true);
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
	internal static class StaticProductSizeRelations
	{
		internal static readonly IEntityRelation AutoTestResultProductEntityUsingProductSizesIdStatic = new ProductSizeRelations().AutoTestResultProductEntityUsingProductSizesId;
		internal static readonly IEntityRelation LookupEntityUsingStatusLookupsIdStatic = new ProductSizeRelations().LookupEntityUsingStatusLookupsId;
		internal static readonly IEntityRelation ProductFormEntityUsingProductFormsIdStatic = new ProductSizeRelations().ProductFormEntityUsingProductFormsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ProductSizeRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticProductSizeRelations()
		{
		}
	}
}
