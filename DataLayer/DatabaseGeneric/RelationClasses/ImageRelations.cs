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
	/// <summary>Implements the relations factory for the entity: Image. </summary>
	public partial class ImageRelations
	{
		/// <summary>CTor</summary>
		public ImageRelations()
		{
		}

		/// <summary>Gets all relations of the ImageEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoItemEntityUsingImageId);
			toReturn.Add(this.ItemDetailsEntityUsingImageId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ImageEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Image.Id - AutoItem.ImageId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingImageId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(ImageFields.Id, AutoItemFields.ImageId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ImageEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between ImageEntity and ItemDetailsEntity over the 1:n relation they have, using the relation between the fields:
		/// Image.Id - ItemDetails.ImageId
		/// </summary>
		public virtual IEntityRelation ItemDetailsEntityUsingImageId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ItemDetails" , true);
				relation.AddEntityFieldPair(ImageFields.Id, ItemDetailsFields.ImageId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ImageEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemDetailsEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ImageEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// Image.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ImageFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ImageEntity", true);
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
	internal static class StaticImageRelations
	{
		internal static readonly IEntityRelation AutoItemEntityUsingImageIdStatic = new ImageRelations().AutoItemEntityUsingImageId;
		internal static readonly IEntityRelation ItemDetailsEntityUsingImageIdStatic = new ImageRelations().ItemDetailsEntityUsingImageId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ImageRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticImageRelations()
		{
		}
	}
}
