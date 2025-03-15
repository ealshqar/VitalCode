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
	/// <summary>Implements the relations factory for the entity: User. </summary>
	public partial class UserRelations
	{
		/// <summary>CTor</summary>
		public UserRelations()
		{
		}

		/// <summary>Gets all relations of the UserEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoItemEntityUsingUserId);
			toReturn.Add(this.AutoItemRelationEntityUsingUserId);
			toReturn.Add(this.AutoProtocolEntityUsingUserId);
			toReturn.Add(this.AutoProtocolRevisionEntityUsingUserId);
			toReturn.Add(this.AutoProtocolStageEntityUsingUserId);
			toReturn.Add(this.AutoProtocolStageRevisionEntityUsingUserId);
			toReturn.Add(this.AutoTestEntityUsingUserId);
			toReturn.Add(this.AutoTestResultEntityUsingUserId);
			toReturn.Add(this.AutoTestResultProductEntityUsingUserId);
			toReturn.Add(this.AutoTestStageEntityUsingUserId);
			toReturn.Add(this.ClinicProductPricingEntityUsingUserId);
			toReturn.Add(this.DosageOptionEntityUsingUserId);
			toReturn.Add(this.FrequencyTestEntityUsingUserId);
			toReturn.Add(this.FrequencyTestResultEntityUsingUserId);
			toReturn.Add(this.HwProfileEntityUsingUserId);
			toReturn.Add(this.ImageEntityUsingUserId);
			toReturn.Add(this.InvoiceEntityUsingUserId);
			toReturn.Add(this.ItemEntityUsingUserId);
			toReturn.Add(this.ItemDetailsEntityUsingUserId);
			toReturn.Add(this.ItemTargetEntityUsingUserId);
			toReturn.Add(this.OrderItemEntityUsingUserId);
			toReturn.Add(this.PatientEntityUsingUserId);
			toReturn.Add(this.PatientHistoryEntityUsingUserId);
			toReturn.Add(this.ProductEntityUsingUserId);
			toReturn.Add(this.ProductFormEntityUsingUserId);
			toReturn.Add(this.ProductSizeEntityUsingUserId);
			toReturn.Add(this.ProtocolItemEntityUsingUserId);
			toReturn.Add(this.ProtocolStepEntityUsingUserId);
			toReturn.Add(this.ReadingEntityUsingUserId);
			toReturn.Add(this.ServiceEntityUsingUserId);
			toReturn.Add(this.ShippingOrderEntityUsingUserId);
			toReturn.Add(this.SpotCheckEntityUsingUserId);
			toReturn.Add(this.SpotCheckResultEntityUsingUserId);
			toReturn.Add(this.StageAnnouncementEntityUsingUserId);
			toReturn.Add(this.StageAutoItemEntityUsingUserId);
			toReturn.Add(this.TestEntityUsingUserId);
			toReturn.Add(this.TestImprintableItemEntityUsingUserId);
			toReturn.Add(this.TestingPointEntityUsingUserId);
			toReturn.Add(this.TestIssueEntityUsingUserId);
			toReturn.Add(this.TestProtocolEntityUsingUserId);
			toReturn.Add(this.TestResultEntityUsingUserId);
			toReturn.Add(this.TestResultFactorsEntityUsingUserId);
			toReturn.Add(this.TestServiceEntityUsingUserId);
			toReturn.Add(this.VFSEntityUsingUserId);
			toReturn.Add(this.VFSItemEntityUsingUserId);
			toReturn.Add(this.VFSItemSourceEntityUsingUserId);
			toReturn.Add(this.VFSSecondaryItemEntityUsingUserId);
			toReturn.Add(this.VFSSecondaryItemSourceEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoItem.UserId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoItemRelationEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoItemRelation.UserId
		/// </summary>
		public virtual IEntityRelation AutoItemRelationEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoItemRelationFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemRelationEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoProtocolEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoProtocol.UserId
		/// </summary>
		public virtual IEntityRelation AutoProtocolEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoProtocolRevisionEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoProtocolRevision.UserId
		/// </summary>
		public virtual IEntityRelation AutoProtocolRevisionEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolRevisionFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolRevisionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoProtocolStageEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoProtocolStage.UserId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolStageFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoProtocolStageRevisionEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoProtocolStageRevision.UserId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageRevisionEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoProtocolStageRevisionFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageRevisionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoTestEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoTest.UserId
		/// </summary>
		public virtual IEntityRelation AutoTestEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoTestResult.UserId
		/// </summary>
		public virtual IEntityRelation AutoTestResultEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoTestResultProductEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoTestResultProduct.UserId
		/// </summary>
		public virtual IEntityRelation AutoTestResultProductEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestResultProductFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestResultProductEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and AutoTestStageEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - AutoTestStage.UserId
		/// </summary>
		public virtual IEntityRelation AutoTestStageEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, AutoTestStageFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ClinicProductPricingEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ClinicProductPricing.UserId
		/// </summary>
		public virtual IEntityRelation ClinicProductPricingEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ClinicProductPricingFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ClinicProductPricingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and DosageOptionEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - DosageOption.UserId
		/// </summary>
		public virtual IEntityRelation DosageOptionEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, DosageOptionFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("DosageOptionEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and FrequencyTestEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - FrequencyTest.UserId
		/// </summary>
		public virtual IEntityRelation FrequencyTestEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "FrequencyTests" , true);
				relation.AddEntityFieldPair(UserFields.Id, FrequencyTestFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and FrequencyTestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - FrequencyTestResult.UserId
		/// </summary>
		public virtual IEntityRelation FrequencyTestResultEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "FrequencyTestResults" , true);
				relation.AddEntityFieldPair(UserFields.Id, FrequencyTestResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("FrequencyTestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and HwProfileEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - HwProfile.UserId
		/// </summary>
		public virtual IEntityRelation HwProfileEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Hwprofiles" , true);
				relation.AddEntityFieldPair(UserFields.Id, HwProfileFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("HwProfileEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ImageEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Image.UserId
		/// </summary>
		public virtual IEntityRelation ImageEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ImageFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ImageEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and InvoiceEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Invoice.UserId
		/// </summary>
		public virtual IEntityRelation InvoiceEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Invoices" , true);
				relation.AddEntityFieldPair(UserFields.Id, InvoiceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("InvoiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Item.UserId
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ItemDetailsEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ItemDetails.UserId
		/// </summary>
		public virtual IEntityRelation ItemDetailsEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ItemDetailsFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemDetailsEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ItemTargetEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ItemTarget.UserId
		/// </summary>
		public virtual IEntityRelation ItemTargetEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ItemTargetFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemTargetEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and OrderItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - OrderItem.UserId
		/// </summary>
		public virtual IEntityRelation OrderItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, OrderItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("OrderItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and PatientEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Patient.UserId
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, PatientFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and PatientHistoryEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - PatientHistory.UserId
		/// </summary>
		public virtual IEntityRelation PatientHistoryEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, PatientHistoryFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientHistoryEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ProductEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Product.UserId
		/// </summary>
		public virtual IEntityRelation ProductEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ProductFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ProductFormEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ProductForm.UserId
		/// </summary>
		public virtual IEntityRelation ProductFormEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ProductFormFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ProductSizeEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ProductSize.UserId
		/// </summary>
		public virtual IEntityRelation ProductSizeEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ProductSizeFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ProtocolItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ProtocolItem.UserId
		/// </summary>
		public virtual IEntityRelation ProtocolItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ProtocolItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ProtocolStepEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ProtocolStep.UserId
		/// </summary>
		public virtual IEntityRelation ProtocolStepEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ProtocolStepFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolStepEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ReadingEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Reading.UserId
		/// </summary>
		public virtual IEntityRelation ReadingEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ReadingFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ServiceEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Service.UserId
		/// </summary>
		public virtual IEntityRelation ServiceEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, ServiceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ServiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and ShippingOrderEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - ShippingOrder.UserId
		/// </summary>
		public virtual IEntityRelation ShippingOrderEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ShippingOrders" , true);
				relation.AddEntityFieldPair(UserFields.Id, ShippingOrderFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and SpotCheckEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - SpotCheck.UserId
		/// </summary>
		public virtual IEntityRelation SpotCheckEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotChecks" , true);
				relation.AddEntityFieldPair(UserFields.Id, SpotCheckFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and SpotCheckResultEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - SpotCheckResult.UserId
		/// </summary>
		public virtual IEntityRelation SpotCheckResultEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotCheckResults" , true);
				relation.AddEntityFieldPair(UserFields.Id, SpotCheckResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and StageAnnouncementEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - StageAnnouncement.UserId
		/// </summary>
		public virtual IEntityRelation StageAnnouncementEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, StageAnnouncementFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAnnouncementEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - StageAutoItem.UserId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, StageAutoItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - Test.UserId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestImprintableItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestImprintableItem.UserId
		/// </summary>
		public virtual IEntityRelation TestImprintableItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestImprintableItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestImprintableItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestingPointEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestingPoint.UserId
		/// </summary>
		public virtual IEntityRelation TestingPointEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestingPointFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestingPointEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestIssueEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestIssue.UserId
		/// </summary>
		public virtual IEntityRelation TestIssueEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestIssueFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestProtocolEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestProtocol.UserId
		/// </summary>
		public virtual IEntityRelation TestProtocolEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestProtocolFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestResult.UserId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestResultFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestResultFactorsEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestResultFactors.UserId
		/// </summary>
		public virtual IEntityRelation TestResultFactorsEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestResultFactorsFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultFactorsEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and TestServiceEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - TestService.UserId
		/// </summary>
		public virtual IEntityRelation TestServiceEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, TestServiceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestServiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and VFSEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - VFS.UserId
		/// </summary>
		public virtual IEntityRelation VFSEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, VFSFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - VFSItem.UserId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, VFSItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - VFSItemSource.UserId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "VfsitemsSources" , true);
				relation.AddEntityFieldPair(UserFields.Id, VFSItemSourceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and VFSSecondaryItemEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - VFSSecondaryItem.UserId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, VFSSecondaryItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between UserEntity and VFSSecondaryItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// User.Id - VFSSecondaryItemSource.UserId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemSourceEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(UserFields.Id, VFSSecondaryItemSourceFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemSourceEntity", false);
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
	internal static class StaticUserRelations
	{
		internal static readonly IEntityRelation AutoItemEntityUsingUserIdStatic = new UserRelations().AutoItemEntityUsingUserId;
		internal static readonly IEntityRelation AutoItemRelationEntityUsingUserIdStatic = new UserRelations().AutoItemRelationEntityUsingUserId;
		internal static readonly IEntityRelation AutoProtocolEntityUsingUserIdStatic = new UserRelations().AutoProtocolEntityUsingUserId;
		internal static readonly IEntityRelation AutoProtocolRevisionEntityUsingUserIdStatic = new UserRelations().AutoProtocolRevisionEntityUsingUserId;
		internal static readonly IEntityRelation AutoProtocolStageEntityUsingUserIdStatic = new UserRelations().AutoProtocolStageEntityUsingUserId;
		internal static readonly IEntityRelation AutoProtocolStageRevisionEntityUsingUserIdStatic = new UserRelations().AutoProtocolStageRevisionEntityUsingUserId;
		internal static readonly IEntityRelation AutoTestEntityUsingUserIdStatic = new UserRelations().AutoTestEntityUsingUserId;
		internal static readonly IEntityRelation AutoTestResultEntityUsingUserIdStatic = new UserRelations().AutoTestResultEntityUsingUserId;
		internal static readonly IEntityRelation AutoTestResultProductEntityUsingUserIdStatic = new UserRelations().AutoTestResultProductEntityUsingUserId;
		internal static readonly IEntityRelation AutoTestStageEntityUsingUserIdStatic = new UserRelations().AutoTestStageEntityUsingUserId;
		internal static readonly IEntityRelation ClinicProductPricingEntityUsingUserIdStatic = new UserRelations().ClinicProductPricingEntityUsingUserId;
		internal static readonly IEntityRelation DosageOptionEntityUsingUserIdStatic = new UserRelations().DosageOptionEntityUsingUserId;
		internal static readonly IEntityRelation FrequencyTestEntityUsingUserIdStatic = new UserRelations().FrequencyTestEntityUsingUserId;
		internal static readonly IEntityRelation FrequencyTestResultEntityUsingUserIdStatic = new UserRelations().FrequencyTestResultEntityUsingUserId;
		internal static readonly IEntityRelation HwProfileEntityUsingUserIdStatic = new UserRelations().HwProfileEntityUsingUserId;
		internal static readonly IEntityRelation ImageEntityUsingUserIdStatic = new UserRelations().ImageEntityUsingUserId;
		internal static readonly IEntityRelation InvoiceEntityUsingUserIdStatic = new UserRelations().InvoiceEntityUsingUserId;
		internal static readonly IEntityRelation ItemEntityUsingUserIdStatic = new UserRelations().ItemEntityUsingUserId;
		internal static readonly IEntityRelation ItemDetailsEntityUsingUserIdStatic = new UserRelations().ItemDetailsEntityUsingUserId;
		internal static readonly IEntityRelation ItemTargetEntityUsingUserIdStatic = new UserRelations().ItemTargetEntityUsingUserId;
		internal static readonly IEntityRelation OrderItemEntityUsingUserIdStatic = new UserRelations().OrderItemEntityUsingUserId;
		internal static readonly IEntityRelation PatientEntityUsingUserIdStatic = new UserRelations().PatientEntityUsingUserId;
		internal static readonly IEntityRelation PatientHistoryEntityUsingUserIdStatic = new UserRelations().PatientHistoryEntityUsingUserId;
		internal static readonly IEntityRelation ProductEntityUsingUserIdStatic = new UserRelations().ProductEntityUsingUserId;
		internal static readonly IEntityRelation ProductFormEntityUsingUserIdStatic = new UserRelations().ProductFormEntityUsingUserId;
		internal static readonly IEntityRelation ProductSizeEntityUsingUserIdStatic = new UserRelations().ProductSizeEntityUsingUserId;
		internal static readonly IEntityRelation ProtocolItemEntityUsingUserIdStatic = new UserRelations().ProtocolItemEntityUsingUserId;
		internal static readonly IEntityRelation ProtocolStepEntityUsingUserIdStatic = new UserRelations().ProtocolStepEntityUsingUserId;
		internal static readonly IEntityRelation ReadingEntityUsingUserIdStatic = new UserRelations().ReadingEntityUsingUserId;
		internal static readonly IEntityRelation ServiceEntityUsingUserIdStatic = new UserRelations().ServiceEntityUsingUserId;
		internal static readonly IEntityRelation ShippingOrderEntityUsingUserIdStatic = new UserRelations().ShippingOrderEntityUsingUserId;
		internal static readonly IEntityRelation SpotCheckEntityUsingUserIdStatic = new UserRelations().SpotCheckEntityUsingUserId;
		internal static readonly IEntityRelation SpotCheckResultEntityUsingUserIdStatic = new UserRelations().SpotCheckResultEntityUsingUserId;
		internal static readonly IEntityRelation StageAnnouncementEntityUsingUserIdStatic = new UserRelations().StageAnnouncementEntityUsingUserId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingUserIdStatic = new UserRelations().StageAutoItemEntityUsingUserId;
		internal static readonly IEntityRelation TestEntityUsingUserIdStatic = new UserRelations().TestEntityUsingUserId;
		internal static readonly IEntityRelation TestImprintableItemEntityUsingUserIdStatic = new UserRelations().TestImprintableItemEntityUsingUserId;
		internal static readonly IEntityRelation TestingPointEntityUsingUserIdStatic = new UserRelations().TestingPointEntityUsingUserId;
		internal static readonly IEntityRelation TestIssueEntityUsingUserIdStatic = new UserRelations().TestIssueEntityUsingUserId;
		internal static readonly IEntityRelation TestProtocolEntityUsingUserIdStatic = new UserRelations().TestProtocolEntityUsingUserId;
		internal static readonly IEntityRelation TestResultEntityUsingUserIdStatic = new UserRelations().TestResultEntityUsingUserId;
		internal static readonly IEntityRelation TestResultFactorsEntityUsingUserIdStatic = new UserRelations().TestResultFactorsEntityUsingUserId;
		internal static readonly IEntityRelation TestServiceEntityUsingUserIdStatic = new UserRelations().TestServiceEntityUsingUserId;
		internal static readonly IEntityRelation VFSEntityUsingUserIdStatic = new UserRelations().VFSEntityUsingUserId;
		internal static readonly IEntityRelation VFSItemEntityUsingUserIdStatic = new UserRelations().VFSItemEntityUsingUserId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingUserIdStatic = new UserRelations().VFSItemSourceEntityUsingUserId;
		internal static readonly IEntityRelation VFSSecondaryItemEntityUsingUserIdStatic = new UserRelations().VFSSecondaryItemEntityUsingUserId;
		internal static readonly IEntityRelation VFSSecondaryItemSourceEntityUsingUserIdStatic = new UserRelations().VFSSecondaryItemSourceEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticUserRelations()
		{
		}
	}
}
