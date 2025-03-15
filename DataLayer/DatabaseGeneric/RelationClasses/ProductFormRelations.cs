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
	/// <summary>Implements the relations factory for the entity: ProductForm. </summary>
	public partial class ProductFormRelations
	{
		/// <summary>CTor</summary>
		public ProductFormRelations()
		{
		}

		/// <summary>Gets all relations of the ProductFormEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoTestResultProductEntityUsingProductFormsId);
			toReturn.Add(this.DosageOptionEntityUsingProductFormsId);
			toReturn.Add(this.ProductSizeEntityUsingProductFormsId);
			toReturn.Add(this.LookupEntityUsingStatusLookupId);
			toReturn.Add(this.ProductEntityUsingProductsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ProductFormEntity and AutoTestResultProductEntity over the 1:n relation they have, using the relation between the fields:
		/// ProductForm.Id - AutoTestResultProduct.ProductFormsId
		/// </summary>
		public virtual IEntityRelation AutoTestResultProductEntityUsingProductFormsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoTestResultProducts" , true);
				relation.AddEntityFieldPair(ProductFormFields.Id, AutoTestResultProductFields.ProductFormsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultProductEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ProductFormEntity and DosageOptionEntity over the 1:n relation they have, using the relation between the fields:
		/// ProductForm.Id - DosageOption.ProductFormsId
		/// </summary>
		public virtual IEntityRelation DosageOptionEntityUsingProductFormsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "DosageOptions" , true);
				relation.AddEntityFieldPair(ProductFormFields.Id, DosageOptionFields.ProductFormsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DosageOptionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ProductFormEntity and ProductSizeEntity over the 1:n relation they have, using the relation between the fields:
		/// ProductForm.Id - ProductSize.ProductFormsId
		/// </summary>
		public virtual IEntityRelation ProductSizeEntityUsingProductFormsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ProductSizes" , true);
				relation.AddEntityFieldPair(ProductFormFields.Id, ProductSizeFields.ProductFormsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ProductFormEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// ProductForm.StatusLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStatusLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Status", false);
				relation.AddEntityFieldPair(LookupFields.Id, ProductFormFields.StatusLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProductFormEntity and ProductEntity over the m:1 relation they have, using the relation between the fields:
		/// ProductForm.ProductsId - Product.Id
		/// </summary>
		public virtual IEntityRelation ProductEntityUsingProductsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Product", false);
				relation.AddEntityFieldPair(ProductFields.Id, ProductFormFields.ProductsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProductFormEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// ProductForm.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ProductFormFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", true);
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
	internal static class StaticProductFormRelations
	{
		internal static readonly IEntityRelation AutoTestResultProductEntityUsingProductFormsIdStatic = new ProductFormRelations().AutoTestResultProductEntityUsingProductFormsId;
		internal static readonly IEntityRelation DosageOptionEntityUsingProductFormsIdStatic = new ProductFormRelations().DosageOptionEntityUsingProductFormsId;
		internal static readonly IEntityRelation ProductSizeEntityUsingProductFormsIdStatic = new ProductFormRelations().ProductSizeEntityUsingProductFormsId;
		internal static readonly IEntityRelation LookupEntityUsingStatusLookupIdStatic = new ProductFormRelations().LookupEntityUsingStatusLookupId;
		internal static readonly IEntityRelation ProductEntityUsingProductsIdStatic = new ProductFormRelations().ProductEntityUsingProductsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ProductFormRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticProductFormRelations()
		{
		}
	}
}
