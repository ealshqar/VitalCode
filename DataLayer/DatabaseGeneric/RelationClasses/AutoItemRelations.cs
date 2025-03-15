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
	/// <summary>Implements the relations factory for the entity: AutoItem. </summary>
	public partial class AutoItemRelations
	{
		/// <summary>CTor</summary>
		public AutoItemRelations()
		{
		}

		/// <summary>Gets all relations of the AutoItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoItemRelationEntityUsingAutoItemChildId);
			toReturn.Add(this.AutoItemRelationEntityUsingAutoItemParentId);
			toReturn.Add(this.AutoTestResultEntityUsingAutoItemsId);
			toReturn.Add(this.ProductEntityUsingAutoItemsId);
			toReturn.Add(this.StageAutoItemEntityUsingAutoItemsId);
			toReturn.Add(this.ImageEntityUsingImageId);
			toReturn.Add(this.LookupEntityUsingChildsOrderTypeLookupId);
			toReturn.Add(this.LookupEntityUsingChildsScanningTypeLookupId);
			toReturn.Add(this.LookupEntityUsingGenderLookupId);
			toReturn.Add(this.LookupEntityUsingScanningMethodLookupId);
			toReturn.Add(this.LookupEntityUsingStatusLookupId);
			toReturn.Add(this.LookupEntityUsingStructureTypeLookupId);
			toReturn.Add(this.LookupEntityUsingTypeLookupId);
			toReturn.Add(this.TestingPointEntityUsingTestingPointsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and AutoItemRelationEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoItem.Id - AutoItemRelation.AutoItemChildId
		/// </summary>
		public virtual IEntityRelation AutoItemRelationEntityUsingAutoItemChildId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Children" , true);
				relation.AddEntityFieldPair(AutoItemFields.Id, AutoItemRelationFields.AutoItemChildId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemRelationEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and AutoItemRelationEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoItem.Id - AutoItemRelation.AutoItemParentId
		/// </summary>
		public virtual IEntityRelation AutoItemRelationEntityUsingAutoItemParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Parents" , true);
				relation.AddEntityFieldPair(AutoItemFields.Id, AutoItemRelationFields.AutoItemParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemRelationEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and AutoTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoItem.Id - AutoTestResult.AutoItemsId
		/// </summary>
		public virtual IEntityRelation AutoTestResultEntityUsingAutoItemsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(AutoItemFields.Id, AutoTestResultFields.AutoItemsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and ProductEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoItem.Id - Product.AutoItemsId
		/// </summary>
		public virtual IEntityRelation ProductEntityUsingAutoItemsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Products" , true);
				relation.AddEntityFieldPair(AutoItemFields.Id, ProductFields.AutoItemsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// AutoItem.Id - StageAutoItem.AutoItemsId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingAutoItemsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(AutoItemFields.Id, StageAutoItemFields.AutoItemsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and ImageEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.ImageId - Image.Id
		/// </summary>
		public virtual IEntityRelation ImageEntityUsingImageId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Image", false);
				relation.AddEntityFieldPair(ImageFields.Id, AutoItemFields.ImageId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ImageEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.ChildsOrderTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingChildsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ChildsOrderType", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.ChildsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.ChildsScanningTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingChildsScanningTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ChildsScanningType", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.ChildsScanningTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.GenderLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Gender", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.ScanningMethodLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingScanningMethodLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ScanningMethod", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.ScanningMethodLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.StatusLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStatusLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Status", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.StatusLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.StructureTypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingStructureTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "StructureType", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.StructureTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.TypeLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Type", false);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and TestingPointEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.TestingPointsId - TestingPoint.Id
		/// </summary>
		public virtual IEntityRelation TestingPointEntityUsingTestingPointsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestingPoint", false);
				relation.AddEntityFieldPair(TestingPointFields.Id, AutoItemFields.TestingPointsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestingPointEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between AutoItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// AutoItem.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, AutoItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", true);
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
	internal static class StaticAutoItemRelations
	{
		internal static readonly IEntityRelation AutoItemRelationEntityUsingAutoItemChildIdStatic = new AutoItemRelations().AutoItemRelationEntityUsingAutoItemChildId;
		internal static readonly IEntityRelation AutoItemRelationEntityUsingAutoItemParentIdStatic = new AutoItemRelations().AutoItemRelationEntityUsingAutoItemParentId;
		internal static readonly IEntityRelation AutoTestResultEntityUsingAutoItemsIdStatic = new AutoItemRelations().AutoTestResultEntityUsingAutoItemsId;
		internal static readonly IEntityRelation ProductEntityUsingAutoItemsIdStatic = new AutoItemRelations().ProductEntityUsingAutoItemsId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingAutoItemsIdStatic = new AutoItemRelations().StageAutoItemEntityUsingAutoItemsId;
		internal static readonly IEntityRelation ImageEntityUsingImageIdStatic = new AutoItemRelations().ImageEntityUsingImageId;
		internal static readonly IEntityRelation LookupEntityUsingChildsOrderTypeLookupIdStatic = new AutoItemRelations().LookupEntityUsingChildsOrderTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingChildsScanningTypeLookupIdStatic = new AutoItemRelations().LookupEntityUsingChildsScanningTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingGenderLookupIdStatic = new AutoItemRelations().LookupEntityUsingGenderLookupId;
		internal static readonly IEntityRelation LookupEntityUsingScanningMethodLookupIdStatic = new AutoItemRelations().LookupEntityUsingScanningMethodLookupId;
		internal static readonly IEntityRelation LookupEntityUsingStatusLookupIdStatic = new AutoItemRelations().LookupEntityUsingStatusLookupId;
		internal static readonly IEntityRelation LookupEntityUsingStructureTypeLookupIdStatic = new AutoItemRelations().LookupEntityUsingStructureTypeLookupId;
		internal static readonly IEntityRelation LookupEntityUsingTypeLookupIdStatic = new AutoItemRelations().LookupEntityUsingTypeLookupId;
		internal static readonly IEntityRelation TestingPointEntityUsingTestingPointsIdStatic = new AutoItemRelations().TestingPointEntityUsingTestingPointsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new AutoItemRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticAutoItemRelations()
		{
		}
	}
}
