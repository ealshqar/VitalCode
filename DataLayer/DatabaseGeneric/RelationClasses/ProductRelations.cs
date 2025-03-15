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
	/// <summary>Implements the relations factory for the entity: Product. </summary>
	public partial class ProductRelations
	{
		/// <summary>CTor</summary>
		public ProductRelations()
		{
		}

		/// <summary>Gets all relations of the ProductEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ClinicProductPricingEntityUsingProductsId);
			toReturn.Add(this.ProductFormEntityUsingProductsId);
			toReturn.Add(this.AutoItemEntityUsingAutoItemsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ProductEntity and ClinicProductPricingEntity over the 1:n relation they have, using the relation between the fields:
		/// Product.Id - ClinicProductPricing.ProductsId
		/// </summary>
		public virtual IEntityRelation ClinicProductPricingEntityUsingProductsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ClinicProductPricings" , true);
				relation.AddEntityFieldPair(ProductFields.Id, ClinicProductPricingFields.ProductsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ClinicProductPricingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ProductEntity and ProductFormEntity over the 1:n relation they have, using the relation between the fields:
		/// Product.Id - ProductForm.ProductsId
		/// </summary>
		public virtual IEntityRelation ProductFormEntityUsingProductsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ProductForms" , true);
				relation.AddEntityFieldPair(ProductFields.Id, ProductFormFields.ProductsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ProductEntity and AutoItemEntity over the m:1 relation they have, using the relation between the fields:
		/// Product.AutoItemsId - AutoItem.Id
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingAutoItemsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "AutoItem", false);
				relation.AddEntityFieldPair(AutoItemFields.Id, ProductFields.AutoItemsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProductEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Product.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ProductFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", true);
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
	internal static class StaticProductRelations
	{
		internal static readonly IEntityRelation ClinicProductPricingEntityUsingProductsIdStatic = new ProductRelations().ClinicProductPricingEntityUsingProductsId;
		internal static readonly IEntityRelation ProductFormEntityUsingProductsIdStatic = new ProductRelations().ProductFormEntityUsingProductsId;
		internal static readonly IEntityRelation AutoItemEntityUsingAutoItemsIdStatic = new ProductRelations().AutoItemEntityUsingAutoItemsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ProductRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticProductRelations()
		{
		}
	}
}
