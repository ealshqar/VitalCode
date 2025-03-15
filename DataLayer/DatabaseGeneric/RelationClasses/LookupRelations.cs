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
	/// <summary>Implements the relations factory for the entity: Lookup. </summary>
	public partial class LookupRelations
	{
		/// <summary>CTor</summary>
		public LookupRelations()
		{
		}

		/// <summary>Gets all relations of the LookupEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoItemEntityUsingChildsOrderTypeLookupId);
			toReturn.Add(this.AutoItemEntityUsingChildsScanningTypeLookupId);
			toReturn.Add(this.AutoItemEntityUsingGenderLookupId);
			toReturn.Add(this.AutoItemEntityUsingScanningMethodLookupId);
			toReturn.Add(this.AutoItemEntityUsingStatusLookupId);
			toReturn.Add(this.AutoItemEntityUsingStructureTypeLookupId);
			toReturn.Add(this.AutoItemEntityUsingTypeLookupId);
			toReturn.Add(this.AutoProtocolStageEntityUsingStageItemsOrderTypeLookupId);
			toReturn.Add(this.AutoTestStageEntityUsingStageItemsOrderTypeLookupId);
			toReturn.Add(this.ItemEntityUsingGenderLookupId);
			toReturn.Add(this.ItemEntityUsingItemSourceLookupId);
			toReturn.Add(this.ItemEntityUsingListTypeLookupId);
			toReturn.Add(this.ItemEntityUsingTypeLookupId);
			toReturn.Add(this.ItemRelationEntityUsingRelationTypeLookupId);
			toReturn.Add(this.ItemTargetEntityUsingTargetTypeLookupId);
			toReturn.Add(this.PatientEntityUsingGenderLookupId);
			toReturn.Add(this.PatientHistoryEntityUsingTypeLookupId);
			toReturn.Add(this.ProductFormEntityUsingStatusLookupId);
			toReturn.Add(this.ProductSizeEntityUsingStatusLookupsId);
			toReturn.Add(this.PropertyEntityUsingApplicableTypeLookupId);
			toReturn.Add(this.PropertyEntityUsingValueTypeLookupId);
			toReturn.Add(this.ProtocolStepEntityUsingTypeLookupId);
			toReturn.Add(this.ReadingEntityUsingListPointLookupId);
			toReturn.Add(this.ServiceEntityUsingTypeLookupId);
			toReturn.Add(this.SettingEntityUsingSettingGroupLookupId);
			toReturn.Add(this.SettingEntityUsingValueTypeLookupId);
			toReturn.Add(this.ShippingOrderEntityUsingShippingMethodLookupId);
			toReturn.Add(this.SpotCheckResultEntityUsingResultTypeId);
			toReturn.Add(this.StageAutoItemEntityUsingChildsOrderTypeLookupId);
			toReturn.Add(this.StageAutoItemEntityUsingChildsScanningTypeLookupId);
			toReturn.Add(this.StageAutoItemEntityUsingScanningMethodLookupId);
			toReturn.Add(this.TestEntityUsingListPointLookupId);
			toReturn.Add(this.TestEntityUsingTestStateLookupId);
			toReturn.Add(this.TestEntityUsingTestTypeLookupId);
			toReturn.Add(this.TestResultEntityUsingStepTypeLookupId);
			toReturn.Add(this.TestScheduleEntityUsingDiscountApplyLookupId);
			toReturn.Add(this.TestScheduleEntityUsingEvalPeriodTypeLookupId);
			toReturn.Add(this.TestServiceEntityUsingTypeLookupId);
			toReturn.Add(this.VFSItemEntityUsingGridGroupLookupId);
			toReturn.Add(this.VFSItemEntityUsingGroupLookupId);
			toReturn.Add(this.VFSItemEntityUsingSectionLookupId);
			toReturn.Add(this.VFSItemSourceEntityUsingGenderLookupId);
			toReturn.Add(this.VFSItemSourceEntityUsingGridGroupLookupId);
			toReturn.Add(this.VFSItemSourceEntityUsingGroupLookupId);
			toReturn.Add(this.VFSItemSourceEntityUsingSectionLookupId);
			toReturn.Add(this.VFSItemSourceEntityUsingV1TypeLookupId);
			toReturn.Add(this.VFSItemSourceEntityUsingV2TypeLookupId);
			toReturn.Add(this.VFSSecondaryItemEntityUsingSectionLookupId);
			toReturn.Add(this.VFSSecondaryItemSourceEntityUsingSectionLookupId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.ChildsOrderTypeLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingChildsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.ChildsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.ChildsScanningTypeLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingChildsScanningTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.ChildsScanningTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.GenderLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "AutoItems" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.ScanningMethodLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingScanningMethodLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.ScanningMethodLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.StatusLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingStatusLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.StatusLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.StructureTypeLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingStructureTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.StructureTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoItem.TypeLookupId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoItemFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoProtocolStageEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoProtocolStage.StageItemsOrderTypeLookupId
		/// </summary>
		public virtual IEntityRelation AutoProtocolStageEntityUsingStageItemsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoProtocolStageFields.StageItemsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoProtocolStageEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and AutoTestStageEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - AutoTestStage.StageItemsOrderTypeLookupId
		/// </summary>
		public virtual IEntityRelation AutoTestStageEntityUsingStageItemsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, AutoTestStageFields.StageItemsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoTestStageEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Item.GenderLookupId
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Item.ItemSourceLookupId
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemSourceLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.ItemSourceLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Item.ListTypeLookupId
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingListTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.ListTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Item.TypeLookupId
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ItemFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ItemRelationEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - ItemRelation.RelationTypeLookupId
		/// </summary>
		public virtual IEntityRelation ItemRelationEntityUsingRelationTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ItemRelationFields.RelationTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemRelationEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ItemTargetEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - ItemTarget.TargetTypeLookupId
		/// </summary>
		public virtual IEntityRelation ItemTargetEntityUsingTargetTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ItemTargetFields.TargetTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemTargetEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and PatientEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Patient.GenderLookupId
		/// </summary>
		public virtual IEntityRelation PatientEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, PatientFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and PatientHistoryEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - PatientHistory.TypeLookupId
		/// </summary>
		public virtual IEntityRelation PatientHistoryEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, PatientHistoryFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PatientHistoryEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ProductFormEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - ProductForm.StatusLookupId
		/// </summary>
		public virtual IEntityRelation ProductFormEntityUsingStatusLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ProductFormFields.StatusLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductFormEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ProductSizeEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - ProductSize.StatusLookupsId
		/// </summary>
		public virtual IEntityRelation ProductSizeEntityUsingStatusLookupsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ProductSizeFields.StatusLookupsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProductSizeEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and PropertyEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Property.ApplicableTypeLookupId
		/// </summary>
		public virtual IEntityRelation PropertyEntityUsingApplicableTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Properties" , true);
				relation.AddEntityFieldPair(LookupFields.Id, PropertyFields.ApplicableTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and PropertyEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Property.ValueTypeLookupId
		/// </summary>
		public virtual IEntityRelation PropertyEntityUsingValueTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "Properties_" , true);
				relation.AddEntityFieldPair(LookupFields.Id, PropertyFields.ValueTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("PropertyEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ProtocolStepEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - ProtocolStep.TypeLookupId
		/// </summary>
		public virtual IEntityRelation ProtocolStepEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ProtocolStepFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolStepEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ReadingEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Reading.ListPointLookupId
		/// </summary>
		public virtual IEntityRelation ReadingEntityUsingListPointLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ReadingFields.ListPointLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ReadingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ServiceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Service.TypeLookupId
		/// </summary>
		public virtual IEntityRelation ServiceEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ServiceFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ServiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and SettingEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Setting.SettingGroupLookupId
		/// </summary>
		public virtual IEntityRelation SettingEntityUsingSettingGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, SettingFields.SettingGroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SettingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and SettingEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Setting.ValueTypeLookupId
		/// </summary>
		public virtual IEntityRelation SettingEntityUsingValueTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, SettingFields.ValueTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SettingEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and ShippingOrderEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - ShippingOrder.ShippingMethodLookupId
		/// </summary>
		public virtual IEntityRelation ShippingOrderEntityUsingShippingMethodLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ShippingOrders" , true);
				relation.AddEntityFieldPair(LookupFields.Id, ShippingOrderFields.ShippingMethodLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and SpotCheckResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - SpotCheckResult.ResultTypeId
		/// </summary>
		public virtual IEntityRelation SpotCheckResultEntityUsingResultTypeId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "SpotCheckResults" , true);
				relation.AddEntityFieldPair(LookupFields.Id, SpotCheckResultFields.ResultTypeId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("SpotCheckResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - StageAutoItem.ChildsOrderTypeLookupId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingChildsOrderTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, StageAutoItemFields.ChildsOrderTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - StageAutoItem.ChildsScanningTypeLookupId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingChildsScanningTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, StageAutoItemFields.ChildsScanningTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - StageAutoItem.ScanningMethodLookupId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingScanningMethodLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, StageAutoItemFields.ScanningMethodLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Test.ListPointLookupId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingListPointLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestFields.ListPointLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Test.TestStateLookupId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestStateLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestFields.TestStateLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - Test.TestTypeLookupId
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestFields.TestTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestResultEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - TestResult.StepTypeLookupId
		/// </summary>
		public virtual IEntityRelation TestResultEntityUsingStepTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestResult" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestResultFields.StepTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestResultEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestScheduleEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - TestSchedule.DiscountApplyLookupId
		/// </summary>
		public virtual IEntityRelation TestScheduleEntityUsingDiscountApplyLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestSchedules_" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestScheduleFields.DiscountApplyLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestScheduleEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - TestSchedule.EvalPeriodTypeLookupId
		/// </summary>
		public virtual IEntityRelation TestScheduleEntityUsingEvalPeriodTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "TestSchedules" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestScheduleFields.EvalPeriodTypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestScheduleEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and TestServiceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - TestService.TypeLookupId
		/// </summary>
		public virtual IEntityRelation TestServiceEntityUsingTypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, TestServiceFields.TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestServiceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItem.GridGroupLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingGridGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemFields.GridGroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItem.GroupLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemFields.GroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItem.SectionLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItemSource.GenderLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingGenderLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.GenderLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItemSource.GridGroupLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingGridGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.GridGroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItemSource.GroupLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingGroupLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.GroupLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItemSource.SectionLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItemSource.V1TypeLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingV1TypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.V1TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSItemSource.V2TypeLookupId
		/// </summary>
		public virtual IEntityRelation VFSItemSourceEntityUsingV2TypeLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSItemSourceFields.V2TypeLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSItemSourceEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSSecondaryItemEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSSecondaryItem.SectionLookupId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSSecondaryItemFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("VFSSecondaryItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between LookupEntity and VFSSecondaryItemSourceEntity over the 1:n relation they have, using the relation between the fields:
		/// Lookup.Id - VFSSecondaryItemSource.SectionLookupId
		/// </summary>
		public virtual IEntityRelation VFSSecondaryItemSourceEntityUsingSectionLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(LookupFields.Id, VFSSecondaryItemSourceFields.SectionLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", true);
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
	internal static class StaticLookupRelations
	{
		internal static readonly IEntityRelation AutoItemEntityUsingChildsOrderTypeLookupIdStatic = new LookupRelations().AutoItemEntityUsingChildsOrderTypeLookupId;
		internal static readonly IEntityRelation AutoItemEntityUsingChildsScanningTypeLookupIdStatic = new LookupRelations().AutoItemEntityUsingChildsScanningTypeLookupId;
		internal static readonly IEntityRelation AutoItemEntityUsingGenderLookupIdStatic = new LookupRelations().AutoItemEntityUsingGenderLookupId;
		internal static readonly IEntityRelation AutoItemEntityUsingScanningMethodLookupIdStatic = new LookupRelations().AutoItemEntityUsingScanningMethodLookupId;
		internal static readonly IEntityRelation AutoItemEntityUsingStatusLookupIdStatic = new LookupRelations().AutoItemEntityUsingStatusLookupId;
		internal static readonly IEntityRelation AutoItemEntityUsingStructureTypeLookupIdStatic = new LookupRelations().AutoItemEntityUsingStructureTypeLookupId;
		internal static readonly IEntityRelation AutoItemEntityUsingTypeLookupIdStatic = new LookupRelations().AutoItemEntityUsingTypeLookupId;
		internal static readonly IEntityRelation AutoProtocolStageEntityUsingStageItemsOrderTypeLookupIdStatic = new LookupRelations().AutoProtocolStageEntityUsingStageItemsOrderTypeLookupId;
		internal static readonly IEntityRelation AutoTestStageEntityUsingStageItemsOrderTypeLookupIdStatic = new LookupRelations().AutoTestStageEntityUsingStageItemsOrderTypeLookupId;
		internal static readonly IEntityRelation ItemEntityUsingGenderLookupIdStatic = new LookupRelations().ItemEntityUsingGenderLookupId;
		internal static readonly IEntityRelation ItemEntityUsingItemSourceLookupIdStatic = new LookupRelations().ItemEntityUsingItemSourceLookupId;
		internal static readonly IEntityRelation ItemEntityUsingListTypeLookupIdStatic = new LookupRelations().ItemEntityUsingListTypeLookupId;
		internal static readonly IEntityRelation ItemEntityUsingTypeLookupIdStatic = new LookupRelations().ItemEntityUsingTypeLookupId;
		internal static readonly IEntityRelation ItemRelationEntityUsingRelationTypeLookupIdStatic = new LookupRelations().ItemRelationEntityUsingRelationTypeLookupId;
		internal static readonly IEntityRelation ItemTargetEntityUsingTargetTypeLookupIdStatic = new LookupRelations().ItemTargetEntityUsingTargetTypeLookupId;
		internal static readonly IEntityRelation PatientEntityUsingGenderLookupIdStatic = new LookupRelations().PatientEntityUsingGenderLookupId;
		internal static readonly IEntityRelation PatientHistoryEntityUsingTypeLookupIdStatic = new LookupRelations().PatientHistoryEntityUsingTypeLookupId;
		internal static readonly IEntityRelation ProductFormEntityUsingStatusLookupIdStatic = new LookupRelations().ProductFormEntityUsingStatusLookupId;
		internal static readonly IEntityRelation ProductSizeEntityUsingStatusLookupsIdStatic = new LookupRelations().ProductSizeEntityUsingStatusLookupsId;
		internal static readonly IEntityRelation PropertyEntityUsingApplicableTypeLookupIdStatic = new LookupRelations().PropertyEntityUsingApplicableTypeLookupId;
		internal static readonly IEntityRelation PropertyEntityUsingValueTypeLookupIdStatic = new LookupRelations().PropertyEntityUsingValueTypeLookupId;
		internal static readonly IEntityRelation ProtocolStepEntityUsingTypeLookupIdStatic = new LookupRelations().ProtocolStepEntityUsingTypeLookupId;
		internal static readonly IEntityRelation ReadingEntityUsingListPointLookupIdStatic = new LookupRelations().ReadingEntityUsingListPointLookupId;
		internal static readonly IEntityRelation ServiceEntityUsingTypeLookupIdStatic = new LookupRelations().ServiceEntityUsingTypeLookupId;
		internal static readonly IEntityRelation SettingEntityUsingSettingGroupLookupIdStatic = new LookupRelations().SettingEntityUsingSettingGroupLookupId;
		internal static readonly IEntityRelation SettingEntityUsingValueTypeLookupIdStatic = new LookupRelations().SettingEntityUsingValueTypeLookupId;
		internal static readonly IEntityRelation ShippingOrderEntityUsingShippingMethodLookupIdStatic = new LookupRelations().ShippingOrderEntityUsingShippingMethodLookupId;
		internal static readonly IEntityRelation SpotCheckResultEntityUsingResultTypeIdStatic = new LookupRelations().SpotCheckResultEntityUsingResultTypeId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingChildsOrderTypeLookupIdStatic = new LookupRelations().StageAutoItemEntityUsingChildsOrderTypeLookupId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingChildsScanningTypeLookupIdStatic = new LookupRelations().StageAutoItemEntityUsingChildsScanningTypeLookupId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingScanningMethodLookupIdStatic = new LookupRelations().StageAutoItemEntityUsingScanningMethodLookupId;
		internal static readonly IEntityRelation TestEntityUsingListPointLookupIdStatic = new LookupRelations().TestEntityUsingListPointLookupId;
		internal static readonly IEntityRelation TestEntityUsingTestStateLookupIdStatic = new LookupRelations().TestEntityUsingTestStateLookupId;
		internal static readonly IEntityRelation TestEntityUsingTestTypeLookupIdStatic = new LookupRelations().TestEntityUsingTestTypeLookupId;
		internal static readonly IEntityRelation TestResultEntityUsingStepTypeLookupIdStatic = new LookupRelations().TestResultEntityUsingStepTypeLookupId;
		internal static readonly IEntityRelation TestScheduleEntityUsingDiscountApplyLookupIdStatic = new LookupRelations().TestScheduleEntityUsingDiscountApplyLookupId;
		internal static readonly IEntityRelation TestScheduleEntityUsingEvalPeriodTypeLookupIdStatic = new LookupRelations().TestScheduleEntityUsingEvalPeriodTypeLookupId;
		internal static readonly IEntityRelation TestServiceEntityUsingTypeLookupIdStatic = new LookupRelations().TestServiceEntityUsingTypeLookupId;
		internal static readonly IEntityRelation VFSItemEntityUsingGridGroupLookupIdStatic = new LookupRelations().VFSItemEntityUsingGridGroupLookupId;
		internal static readonly IEntityRelation VFSItemEntityUsingGroupLookupIdStatic = new LookupRelations().VFSItemEntityUsingGroupLookupId;
		internal static readonly IEntityRelation VFSItemEntityUsingSectionLookupIdStatic = new LookupRelations().VFSItemEntityUsingSectionLookupId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingGenderLookupIdStatic = new LookupRelations().VFSItemSourceEntityUsingGenderLookupId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingGridGroupLookupIdStatic = new LookupRelations().VFSItemSourceEntityUsingGridGroupLookupId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingGroupLookupIdStatic = new LookupRelations().VFSItemSourceEntityUsingGroupLookupId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingSectionLookupIdStatic = new LookupRelations().VFSItemSourceEntityUsingSectionLookupId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingV1TypeLookupIdStatic = new LookupRelations().VFSItemSourceEntityUsingV1TypeLookupId;
		internal static readonly IEntityRelation VFSItemSourceEntityUsingV2TypeLookupIdStatic = new LookupRelations().VFSItemSourceEntityUsingV2TypeLookupId;
		internal static readonly IEntityRelation VFSSecondaryItemEntityUsingSectionLookupIdStatic = new LookupRelations().VFSSecondaryItemEntityUsingSectionLookupId;
		internal static readonly IEntityRelation VFSSecondaryItemSourceEntityUsingSectionLookupIdStatic = new LookupRelations().VFSSecondaryItemSourceEntityUsingSectionLookupId;

		/// <summary>CTor</summary>
		static StaticLookupRelations()
		{
		}
	}
}
