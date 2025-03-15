///////////////////////////////////////////////////////////////
// This is generated code. 
//////////////////////////////////////////////////////////////
// Code is generated using LLBLGen Pro version: 3.5
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates.NET35
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

using Vital.DataLayer;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.FactoryClasses;
using Vital.DataLayer.HelperClasses;
using Vital.DataLayer.RelationClasses;

namespace Vital.DataLayer.Linq
{
	/// <summary>Meta-data class for the construction of Linq queries which are to be executed using LLBLGen Pro code.</summary>
	public partial class LinqMetaData: ILinqMetaData
	{
		#region Class Member Declarations
		private IDataAccessAdapter _adapterToUse;
		private FunctionMappingStore _customFunctionMappings;
		private Context _contextToUse;
		#endregion
		
		/// <summary>CTor. Using this ctor will leave the IDataAccessAdapter object to use empty. To be able to execute the query, an IDataAccessAdapter instance
		/// is required, and has to be set on the LLBLGenProProvider2 object in the query to execute. </summary>
		public LinqMetaData() : this(null, null)
		{
		}
		
		/// <summary>CTor which accepts an IDataAccessAdapter implementing object, which will be used to execute queries created with this metadata class.</summary>
		/// <param name="adapterToUse">the IDataAccessAdapter to use in queries created with this meta data</param>
		/// <remarks> Be aware that the IDataAccessAdapter object set via this property is kept alive by the LLBLGenProQuery objects created with this meta data
		/// till they go out of scope.</remarks>
		public LinqMetaData(IDataAccessAdapter adapterToUse) : this (adapterToUse, null)
		{
		}

		/// <summary>CTor which accepts an IDataAccessAdapter implementing object, which will be used to execute queries created with this metadata class.</summary>
		/// <param name="adapterToUse">the IDataAccessAdapter to use in queries created with this meta data</param>
		/// <param name="customFunctionMappings">The custom function mappings to use. These take higher precedence than the ones in the DQE to use.</param>
		/// <remarks> Be aware that the IDataAccessAdapter object set via this property is kept alive by the LLBLGenProQuery objects created with this meta data
		/// till they go out of scope.</remarks>
		public LinqMetaData(IDataAccessAdapter adapterToUse, FunctionMappingStore customFunctionMappings)
		{
			_adapterToUse = adapterToUse;
			_customFunctionMappings = customFunctionMappings;
		}
	
		/// <summary>returns the datasource to use in a Linq query for the entity type specified</summary>
		/// <param name="typeOfEntity">the type of the entity to get the datasource for</param>
		/// <returns>the requested datasource</returns>
		public IDataSource GetQueryableForEntity(int typeOfEntity)
		{
			IDataSource toReturn = null;
			switch((Vital.DataLayer.EntityType)typeOfEntity)
			{
				case Vital.DataLayer.EntityType.AppImageEntity:
					toReturn = this.AppImage;
					break;
				case Vital.DataLayer.EntityType.AppInfoEntity:
					toReturn = this.AppInfo;
					break;
				case Vital.DataLayer.EntityType.AutoItemEntity:
					toReturn = this.AutoItem;
					break;
				case Vital.DataLayer.EntityType.AutoItemRelationEntity:
					toReturn = this.AutoItemRelation;
					break;
				case Vital.DataLayer.EntityType.AutoProtocolEntity:
					toReturn = this.AutoProtocol;
					break;
				case Vital.DataLayer.EntityType.AutoProtocolRevisionEntity:
					toReturn = this.AutoProtocolRevision;
					break;
				case Vital.DataLayer.EntityType.AutoProtocolStageEntity:
					toReturn = this.AutoProtocolStage;
					break;
				case Vital.DataLayer.EntityType.AutoProtocolStageRevisionEntity:
					toReturn = this.AutoProtocolStageRevision;
					break;
				case Vital.DataLayer.EntityType.AutoTestEntity:
					toReturn = this.AutoTest;
					break;
				case Vital.DataLayer.EntityType.AutoTestResultEntity:
					toReturn = this.AutoTestResult;
					break;
				case Vital.DataLayer.EntityType.AutoTestResultProductEntity:
					toReturn = this.AutoTestResultProduct;
					break;
				case Vital.DataLayer.EntityType.AutoTestStageEntity:
					toReturn = this.AutoTestStage;
					break;
				case Vital.DataLayer.EntityType.ClinicProductPricingEntity:
					toReturn = this.ClinicProductPricing;
					break;
				case Vital.DataLayer.EntityType.DosageOptionEntity:
					toReturn = this.DosageOption;
					break;
				case Vital.DataLayer.EntityType.FrequencyTestEntity:
					toReturn = this.FrequencyTest;
					break;
				case Vital.DataLayer.EntityType.FrequencyTestResultEntity:
					toReturn = this.FrequencyTestResult;
					break;
				case Vital.DataLayer.EntityType.HwProfileEntity:
					toReturn = this.HwProfile;
					break;
				case Vital.DataLayer.EntityType.ImageEntity:
					toReturn = this.Image;
					break;
				case Vital.DataLayer.EntityType.InvoiceEntity:
					toReturn = this.Invoice;
					break;
				case Vital.DataLayer.EntityType.IssueNavigationStepEntity:
					toReturn = this.IssueNavigationStep;
					break;
				case Vital.DataLayer.EntityType.ItemEntity:
					toReturn = this.Item;
					break;
				case Vital.DataLayer.EntityType.ItemDetailsEntity:
					toReturn = this.ItemDetails;
					break;
				case Vital.DataLayer.EntityType.ItemPropertyEntity:
					toReturn = this.ItemProperty;
					break;
				case Vital.DataLayer.EntityType.ItemRelationEntity:
					toReturn = this.ItemRelation;
					break;
				case Vital.DataLayer.EntityType.ItemRelationPropertyEntity:
					toReturn = this.ItemRelationProperty;
					break;
				case Vital.DataLayer.EntityType.ItemTargetEntity:
					toReturn = this.ItemTarget;
					break;
				case Vital.DataLayer.EntityType.LookupEntity:
					toReturn = this.Lookup;
					break;
				case Vital.DataLayer.EntityType.OrderItemEntity:
					toReturn = this.OrderItem;
					break;
				case Vital.DataLayer.EntityType.PatientEntity:
					toReturn = this.Patient;
					break;
				case Vital.DataLayer.EntityType.PatientHistoryEntity:
					toReturn = this.PatientHistory;
					break;
				case Vital.DataLayer.EntityType.ProductEntity:
					toReturn = this.Product;
					break;
				case Vital.DataLayer.EntityType.ProductFormEntity:
					toReturn = this.ProductForm;
					break;
				case Vital.DataLayer.EntityType.ProductSizeEntity:
					toReturn = this.ProductSize;
					break;
				case Vital.DataLayer.EntityType.PropertyEntity:
					toReturn = this.Property;
					break;
				case Vital.DataLayer.EntityType.ProtocolItemEntity:
					toReturn = this.ProtocolItem;
					break;
				case Vital.DataLayer.EntityType.ProtocolStepEntity:
					toReturn = this.ProtocolStep;
					break;
				case Vital.DataLayer.EntityType.ReadingEntity:
					toReturn = this.Reading;
					break;
				case Vital.DataLayer.EntityType.ScheduleLineEntity:
					toReturn = this.ScheduleLine;
					break;
				case Vital.DataLayer.EntityType.ServiceEntity:
					toReturn = this.Service;
					break;
				case Vital.DataLayer.EntityType.SettingEntity:
					toReturn = this.Setting;
					break;
				case Vital.DataLayer.EntityType.ShippingOrderEntity:
					toReturn = this.ShippingOrder;
					break;
				case Vital.DataLayer.EntityType.SpotCheckEntity:
					toReturn = this.SpotCheck;
					break;
				case Vital.DataLayer.EntityType.SpotCheckResultEntity:
					toReturn = this.SpotCheckResult;
					break;
				case Vital.DataLayer.EntityType.StageAnnouncementEntity:
					toReturn = this.StageAnnouncement;
					break;
				case Vital.DataLayer.EntityType.StageAutoItemEntity:
					toReturn = this.StageAutoItem;
					break;
				case Vital.DataLayer.EntityType.TestEntity:
					toReturn = this.Test;
					break;
				case Vital.DataLayer.EntityType.TestImprintableItemEntity:
					toReturn = this.TestImprintableItem;
					break;
				case Vital.DataLayer.EntityType.TestingPointEntity:
					toReturn = this.TestingPoint;
					break;
				case Vital.DataLayer.EntityType.TestIssueEntity:
					toReturn = this.TestIssue;
					break;
				case Vital.DataLayer.EntityType.TestProtocolEntity:
					toReturn = this.TestProtocol;
					break;
				case Vital.DataLayer.EntityType.TestResultEntity:
					toReturn = this.TestResult;
					break;
				case Vital.DataLayer.EntityType.TestResultFactorsEntity:
					toReturn = this.TestResultFactors;
					break;
				case Vital.DataLayer.EntityType.TestScheduleEntity:
					toReturn = this.TestSchedule;
					break;
				case Vital.DataLayer.EntityType.TestServiceEntity:
					toReturn = this.TestService;
					break;
				case Vital.DataLayer.EntityType.UserEntity:
					toReturn = this.User;
					break;
				case Vital.DataLayer.EntityType.VFSEntity:
					toReturn = this.VFS;
					break;
				case Vital.DataLayer.EntityType.VFSItemEntity:
					toReturn = this.VFSItem;
					break;
				case Vital.DataLayer.EntityType.VFSItemSourceEntity:
					toReturn = this.VFSItemSource;
					break;
				case Vital.DataLayer.EntityType.VFSSecondaryItemEntity:
					toReturn = this.VFSSecondaryItem;
					break;
				case Vital.DataLayer.EntityType.VFSSecondaryItemSourceEntity:
					toReturn = this.VFSSecondaryItemSource;
					break;
				default:
					toReturn = null;
					break;
			}
			return toReturn;
		}

		/// <summary>returns the datasource to use in a Linq query when targeting AppImageEntity instances in the database.</summary>
		public DataSource2<AppImageEntity> AppImage
		{
			get { return new DataSource2<AppImageEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AppInfoEntity instances in the database.</summary>
		public DataSource2<AppInfoEntity> AppInfo
		{
			get { return new DataSource2<AppInfoEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoItemEntity instances in the database.</summary>
		public DataSource2<AutoItemEntity> AutoItem
		{
			get { return new DataSource2<AutoItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoItemRelationEntity instances in the database.</summary>
		public DataSource2<AutoItemRelationEntity> AutoItemRelation
		{
			get { return new DataSource2<AutoItemRelationEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoProtocolEntity instances in the database.</summary>
		public DataSource2<AutoProtocolEntity> AutoProtocol
		{
			get { return new DataSource2<AutoProtocolEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoProtocolRevisionEntity instances in the database.</summary>
		public DataSource2<AutoProtocolRevisionEntity> AutoProtocolRevision
		{
			get { return new DataSource2<AutoProtocolRevisionEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoProtocolStageEntity instances in the database.</summary>
		public DataSource2<AutoProtocolStageEntity> AutoProtocolStage
		{
			get { return new DataSource2<AutoProtocolStageEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoProtocolStageRevisionEntity instances in the database.</summary>
		public DataSource2<AutoProtocolStageRevisionEntity> AutoProtocolStageRevision
		{
			get { return new DataSource2<AutoProtocolStageRevisionEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoTestEntity instances in the database.</summary>
		public DataSource2<AutoTestEntity> AutoTest
		{
			get { return new DataSource2<AutoTestEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoTestResultEntity instances in the database.</summary>
		public DataSource2<AutoTestResultEntity> AutoTestResult
		{
			get { return new DataSource2<AutoTestResultEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoTestResultProductEntity instances in the database.</summary>
		public DataSource2<AutoTestResultProductEntity> AutoTestResultProduct
		{
			get { return new DataSource2<AutoTestResultProductEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting AutoTestStageEntity instances in the database.</summary>
		public DataSource2<AutoTestStageEntity> AutoTestStage
		{
			get { return new DataSource2<AutoTestStageEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ClinicProductPricingEntity instances in the database.</summary>
		public DataSource2<ClinicProductPricingEntity> ClinicProductPricing
		{
			get { return new DataSource2<ClinicProductPricingEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting DosageOptionEntity instances in the database.</summary>
		public DataSource2<DosageOptionEntity> DosageOption
		{
			get { return new DataSource2<DosageOptionEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting FrequencyTestEntity instances in the database.</summary>
		public DataSource2<FrequencyTestEntity> FrequencyTest
		{
			get { return new DataSource2<FrequencyTestEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting FrequencyTestResultEntity instances in the database.</summary>
		public DataSource2<FrequencyTestResultEntity> FrequencyTestResult
		{
			get { return new DataSource2<FrequencyTestResultEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting HwProfileEntity instances in the database.</summary>
		public DataSource2<HwProfileEntity> HwProfile
		{
			get { return new DataSource2<HwProfileEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ImageEntity instances in the database.</summary>
		public DataSource2<ImageEntity> Image
		{
			get { return new DataSource2<ImageEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting InvoiceEntity instances in the database.</summary>
		public DataSource2<InvoiceEntity> Invoice
		{
			get { return new DataSource2<InvoiceEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting IssueNavigationStepEntity instances in the database.</summary>
		public DataSource2<IssueNavigationStepEntity> IssueNavigationStep
		{
			get { return new DataSource2<IssueNavigationStepEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ItemEntity instances in the database.</summary>
		public DataSource2<ItemEntity> Item
		{
			get { return new DataSource2<ItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ItemDetailsEntity instances in the database.</summary>
		public DataSource2<ItemDetailsEntity> ItemDetails
		{
			get { return new DataSource2<ItemDetailsEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ItemPropertyEntity instances in the database.</summary>
		public DataSource2<ItemPropertyEntity> ItemProperty
		{
			get { return new DataSource2<ItemPropertyEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ItemRelationEntity instances in the database.</summary>
		public DataSource2<ItemRelationEntity> ItemRelation
		{
			get { return new DataSource2<ItemRelationEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ItemRelationPropertyEntity instances in the database.</summary>
		public DataSource2<ItemRelationPropertyEntity> ItemRelationProperty
		{
			get { return new DataSource2<ItemRelationPropertyEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ItemTargetEntity instances in the database.</summary>
		public DataSource2<ItemTargetEntity> ItemTarget
		{
			get { return new DataSource2<ItemTargetEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting LookupEntity instances in the database.</summary>
		public DataSource2<LookupEntity> Lookup
		{
			get { return new DataSource2<LookupEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting OrderItemEntity instances in the database.</summary>
		public DataSource2<OrderItemEntity> OrderItem
		{
			get { return new DataSource2<OrderItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting PatientEntity instances in the database.</summary>
		public DataSource2<PatientEntity> Patient
		{
			get { return new DataSource2<PatientEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting PatientHistoryEntity instances in the database.</summary>
		public DataSource2<PatientHistoryEntity> PatientHistory
		{
			get { return new DataSource2<PatientHistoryEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ProductEntity instances in the database.</summary>
		public DataSource2<ProductEntity> Product
		{
			get { return new DataSource2<ProductEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ProductFormEntity instances in the database.</summary>
		public DataSource2<ProductFormEntity> ProductForm
		{
			get { return new DataSource2<ProductFormEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ProductSizeEntity instances in the database.</summary>
		public DataSource2<ProductSizeEntity> ProductSize
		{
			get { return new DataSource2<ProductSizeEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting PropertyEntity instances in the database.</summary>
		public DataSource2<PropertyEntity> Property
		{
			get { return new DataSource2<PropertyEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ProtocolItemEntity instances in the database.</summary>
		public DataSource2<ProtocolItemEntity> ProtocolItem
		{
			get { return new DataSource2<ProtocolItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ProtocolStepEntity instances in the database.</summary>
		public DataSource2<ProtocolStepEntity> ProtocolStep
		{
			get { return new DataSource2<ProtocolStepEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ReadingEntity instances in the database.</summary>
		public DataSource2<ReadingEntity> Reading
		{
			get { return new DataSource2<ReadingEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ScheduleLineEntity instances in the database.</summary>
		public DataSource2<ScheduleLineEntity> ScheduleLine
		{
			get { return new DataSource2<ScheduleLineEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ServiceEntity instances in the database.</summary>
		public DataSource2<ServiceEntity> Service
		{
			get { return new DataSource2<ServiceEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting SettingEntity instances in the database.</summary>
		public DataSource2<SettingEntity> Setting
		{
			get { return new DataSource2<SettingEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting ShippingOrderEntity instances in the database.</summary>
		public DataSource2<ShippingOrderEntity> ShippingOrder
		{
			get { return new DataSource2<ShippingOrderEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting SpotCheckEntity instances in the database.</summary>
		public DataSource2<SpotCheckEntity> SpotCheck
		{
			get { return new DataSource2<SpotCheckEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting SpotCheckResultEntity instances in the database.</summary>
		public DataSource2<SpotCheckResultEntity> SpotCheckResult
		{
			get { return new DataSource2<SpotCheckResultEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting StageAnnouncementEntity instances in the database.</summary>
		public DataSource2<StageAnnouncementEntity> StageAnnouncement
		{
			get { return new DataSource2<StageAnnouncementEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting StageAutoItemEntity instances in the database.</summary>
		public DataSource2<StageAutoItemEntity> StageAutoItem
		{
			get { return new DataSource2<StageAutoItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestEntity instances in the database.</summary>
		public DataSource2<TestEntity> Test
		{
			get { return new DataSource2<TestEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestImprintableItemEntity instances in the database.</summary>
		public DataSource2<TestImprintableItemEntity> TestImprintableItem
		{
			get { return new DataSource2<TestImprintableItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestingPointEntity instances in the database.</summary>
		public DataSource2<TestingPointEntity> TestingPoint
		{
			get { return new DataSource2<TestingPointEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestIssueEntity instances in the database.</summary>
		public DataSource2<TestIssueEntity> TestIssue
		{
			get { return new DataSource2<TestIssueEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestProtocolEntity instances in the database.</summary>
		public DataSource2<TestProtocolEntity> TestProtocol
		{
			get { return new DataSource2<TestProtocolEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestResultEntity instances in the database.</summary>
		public DataSource2<TestResultEntity> TestResult
		{
			get { return new DataSource2<TestResultEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestResultFactorsEntity instances in the database.</summary>
		public DataSource2<TestResultFactorsEntity> TestResultFactors
		{
			get { return new DataSource2<TestResultFactorsEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestScheduleEntity instances in the database.</summary>
		public DataSource2<TestScheduleEntity> TestSchedule
		{
			get { return new DataSource2<TestScheduleEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting TestServiceEntity instances in the database.</summary>
		public DataSource2<TestServiceEntity> TestService
		{
			get { return new DataSource2<TestServiceEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting UserEntity instances in the database.</summary>
		public DataSource2<UserEntity> User
		{
			get { return new DataSource2<UserEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting VFSEntity instances in the database.</summary>
		public DataSource2<VFSEntity> VFS
		{
			get { return new DataSource2<VFSEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting VFSItemEntity instances in the database.</summary>
		public DataSource2<VFSItemEntity> VFSItem
		{
			get { return new DataSource2<VFSItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting VFSItemSourceEntity instances in the database.</summary>
		public DataSource2<VFSItemSourceEntity> VFSItemSource
		{
			get { return new DataSource2<VFSItemSourceEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting VFSSecondaryItemEntity instances in the database.</summary>
		public DataSource2<VFSSecondaryItemEntity> VFSSecondaryItem
		{
			get { return new DataSource2<VFSSecondaryItemEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		/// <summary>returns the datasource to use in a Linq query when targeting VFSSecondaryItemSourceEntity instances in the database.</summary>
		public DataSource2<VFSSecondaryItemSourceEntity> VFSSecondaryItemSource
		{
			get { return new DataSource2<VFSSecondaryItemSourceEntity>(_adapterToUse, new ElementCreator(), _customFunctionMappings, _contextToUse); }
		}
		
		
		#region Class Property Declarations
		/// <summary> Gets / sets the IDataAccessAdapter to use for the queries created with this meta data object.</summary>
		/// <remarks> Be aware that the IDataAccessAdapter object set via this property is kept alive by the LLBLGenProQuery objects created with this meta data
		/// till they go out of scope.</remarks>
		public IDataAccessAdapter AdapterToUse
		{
			get { return _adapterToUse;}
			set { _adapterToUse = value;}
		}

		/// <summary>Gets or sets the custom function mappings to use. These take higher precedence than the ones in the DQE to use</summary>
		public FunctionMappingStore CustomFunctionMappings
		{
			get { return _customFunctionMappings; }
			set { _customFunctionMappings = value; }
		}
		
		/// <summary>Gets or sets the Context instance to use for entity fetches.</summary>
		public Context ContextToUse
		{
			get { return _contextToUse;}
			set { _contextToUse = value;}
		}
		#endregion
	}
}