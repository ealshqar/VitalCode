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
	/// <summary>Implements the relations factory for the entity: ItemDetails. </summary>
	public partial class ItemDetailsRelations
	{
		/// <summary>CTor</summary>
		public ItemDetailsRelations()
		{
		}

		/// <summary>Gets all relations of the ItemDetailsEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemDetailId);
			toReturn.Add(this.ImageEntityUsingImageId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ItemDetailsEntity and ItemEntity over the 1:n relation they have, using the relation between the fields:
		/// ItemDetails.Id - Item.ItemDetailId
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemDetailId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Item" , true);
				relation.AddEntityFieldPair(ItemDetailsFields.Id, ItemFields.ItemDetailId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemDetailsEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ItemDetailsEntity and ImageEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemDetails.ImageId - Image.Id
		/// </summary>
		public virtual IEntityRelation ImageEntityUsingImageId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Image", false);
				relation.AddEntityFieldPair(ImageFields.Id, ItemDetailsFields.ImageId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ImageEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemDetailsEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ItemDetailsEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// ItemDetails.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ItemDetailsFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemDetailsEntity", true);
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
	internal static class StaticItemDetailsRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemDetailIdStatic = new ItemDetailsRelations().ItemEntityUsingItemDetailId;
		internal static readonly IEntityRelation ImageEntityUsingImageIdStatic = new ItemDetailsRelations().ImageEntityUsingImageId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ItemDetailsRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticItemDetailsRelations()
		{
		}
	}
}
