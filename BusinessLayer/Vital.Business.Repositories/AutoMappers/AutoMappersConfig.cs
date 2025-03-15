using AutoMapper;
using Vital.Business.Repositories.DatabaseRepositories.AutoTestSource;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.AppImages;
using Vital.Business.Shared.DomainObjects.AppInfos;
using Vital.Business.Shared.DomainObjects.AutoTestDestination;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.DomainObjects.FrequencyTests;
using Vital.Business.Shared.DomainObjects.Hardware;
using Vital.Business.Shared.DomainObjects.Images;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Properties;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.DomainObjects.Settings;
using Vital.Business.Shared.DomainObjects.ShippingOrders;
using Vital.Business.Shared.DomainObjects.SpotChecks;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.DomainObjects.Users;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.VitalForceSheet;
using Vital.DataLayer.EntityClasses;
using Vital.Hardware.Entities;

namespace Vital.Business.Repositories.AutoMappers
{
    public static class AutoMappersConfig
    {
        #region Static & Private Variables

        private static bool _isAutoMappersConfiged;

        #endregion
        
        #region Config

        /// <summary>
        /// Setup AutoMapper Instance 
        /// </summary>
        public static void SetupInstance()
        {
            // Mappers had been configured.
            if (_isAutoMappersConfiged) 
                return; 

            #region Images

            Mapper.CreateMap<Image, ImageEntity>();

            Mapper.CreateMap<ImageEntity, Image>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region Items

            Mapper.CreateMap<Item, ItemEntity>()
                .ForMember(i => i.ItemMemo, opt => opt.MapFrom(ii => ii.Memo))
                .ForMember(i => i.Children, opt => opt.Ignore())
                .ForMember(i => i.Properties, opt => opt.Ignore())
                .ForMember(i => i.Parents, opt => opt.Ignore())
                .ForMember(i => i.ItemDetail, opt => opt.Ignore())
                .ForMember(i => i.ItemTargets, opt => opt.Ignore());

            Mapper.CreateMap<ItemEntity, Item>()
                .ForMember(dest => dest.Memo , opt => opt.MapFrom(c => c.ItemMemo))
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<ItemDetails, ItemDetailsEntity>();
            Mapper.CreateMap<ItemDetailsEntity, ItemDetails>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<ItemRelation, ItemRelationEntity>()
                .ForMember(dest => dest.RelationTypeLookup, opt => opt.MapFrom(w => w.RelationType))
                .ForMember(dest => dest.Properties, opt => opt.Ignore());

            Mapper.CreateMap<ItemRelationEntity, ItemRelation>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged))
                .ForMember(dest => dest.RelationType , opt => opt.MapFrom(w => w.RelationTypeLookup));

            Mapper.CreateMap<ItemTarget, ItemTargetEntity>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Item.Id))
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.ItemTargetUpdatedDateTime, opt => opt.MapFrom(c => c.UpdatedDateTime))
                .ForMember(dest => dest.ItemTargetCreationDateTime, opt => opt.MapFrom(c => c.CreationDateTime));

            Mapper.CreateMap<ItemTargetEntity, ItemTarget>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged))
                .ForMember(dest => dest.UpdatedDateTime, opt => opt.MapFrom(c => c.ItemTargetUpdatedDateTime))
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(c => c.ItemTargetCreationDateTime)); ;

            #endregion

            #region Lookups

            Mapper.CreateMap<Lookup, LookupEntity>();

            Mapper.CreateMap<LookupEntity, Lookup>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region Patients

            Mapper.CreateMap<Patient, PatientEntity>().ForMember(m => m.Tests, opt => opt.Ignore())
                                                      .ForMember(m => m.SpotChecks, opt => opt.Ignore())
                                                      .ForMember(m => m.VFSRecords, opt => opt.Ignore())
                                                      .ForMember(m => m.FrequencyTests, opt => opt.Ignore())
                                                      .ForMember(m => m.PatientHistory, opt => opt.Ignore())
                                                      .ForMember(m => m.AutoTests, opt => opt.Ignore())
                                                      .ForMember(m => m.Lookup, opt => opt.MapFrom(src => src.GenderLookup));

            Mapper.CreateMap<PatientEntity, Patient>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged))
                .ForMember(dest => dest.GenderLookup, opt => opt.MapFrom(src => src.Lookup));


            Mapper.CreateMap<PatientHistory, PatientHistoryEntity>()
                .ForMember(c => c.Type, opt => opt.MapFrom(a => a.TypeLookup)) ;

            Mapper.CreateMap<PatientHistoryEntity, PatientHistory>()
                .ForMember(c => c.TypeLookup, opt => opt.ResolveUsing<PatientHistoryCustomResolver>().ConstructedBy(() => new PatientHistoryCustomResolver()));

            #endregion

            #region Readings

            Mapper.CreateMap<Reading, ReadingEntity>().ForMember(z => z.Item, opt => opt.Ignore());

            Mapper.CreateMap<ReadingEntity, Reading>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region Tests

            Mapper.CreateMap<Test, TestEntity>()
                .ForMember(k => k.Readings, opt => opt.Ignore())
                .ForMember(t => t.TestIssues, opt => opt.Ignore())
                .ForMember(t => t.TestImprintableItems, opt => opt.Ignore())
                .ForMember(t => t.TestServices, opt => opt.Ignore())
                .ForMember(t => t.ShippingOrders, opt => opt.Ignore())
                .ForMember(t => t.Invoices, opt => opt.Ignore())
                .ForMember(t => t.TestScheduleId , opt => opt.MapFrom(c => c.TestSchedule != null ? c.TestSchedule.Id : 0));

            Mapper.CreateMap<TestEntity, Test>().ForMember(dest => dest.ObjectState,
                                                           opt => opt.UseValue(DomainEntityState.Unchanged));
                                                           

            Mapper.CreateMap<TestResult, TestResultEntity>()
                                .ForMember(k => k.TestResultFactors, opt => opt.Ignore())
                                .ForMember(k => k.Item, opt => opt.Ignore())
                                .ForMember(c => c.IssueId, opt => opt.MapFrom(cc => cc.TestIssue != null ? cc.TestIssue.Id : 0));

            Mapper.CreateMap<TestResultEntity, TestResult>().ForMember(k => k.ParentResultId, opt => opt.Ignore())
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<TestIssue, TestIssueEntity>().ForMember(t => t.IssueNavigationSteps, opt => opt.Ignore())
                .ForMember(t => t.ProtocolStep, opt => opt.Ignore())
                .ForMember(t => t.TestResults, opt => opt.Ignore())
                .ForMember(t => t.IssueNavigationSteps, opt => opt.Ignore())
                .ForMember(t=> t.Item, opt => opt.Ignore())
                .ForMember(t => t.ItemId, opt => opt.MapFrom(cc => cc.Item != null ? cc.Item.Id : 0));

            Mapper.CreateMap<TestServiceEntity, TestService>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<TestService, TestServiceEntity>();

            Mapper.CreateMap<TestIssueEntity, TestIssue>()
                .ForMember(t => t.ProtocolStep, opt => opt.Ignore())
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<IssueNavigationStep, IssueNavigationStepEntity>()
                  .ForMember(c => c.Item , opt => opt.Ignore())
                  .ForMember(c => c.ItemId, opt => opt.MapFrom(cc => cc.Item != null ? cc.Item.Id : 0));
            
            Mapper.CreateMap<IssueNavigationStepEntity, IssueNavigationStep>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<TestResultFactor, TestResultFactorsEntity>()
                    .ForMember(c => c.FactorItemId , opt => opt.MapFrom(o => o.Factor.Id))
                    .ForMember(c => c.Factor, opt => opt.Ignore())
                    .ForMember(c => c.TestResultId, opt => opt.MapFrom(o => o.TestResult != null ? o.TestResult.Id : 0))
                .ForMember(c => c.TestResult, opt => opt.Ignore())
                .ForMember(c => c.PotencyItemId, opt => opt.MapFrom(o => o.Potency != null ? (int?)o.Potency.Id : null))
                .ForMember(c => c.Potency, opt => opt.Ignore());

            Mapper.CreateMap<TestResultFactorsEntity, TestResultFactor>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<TestImprintableItem, TestImprintableItemEntity>()
                .ForMember(t => t.Item, opt => opt.Ignore())
                .ForMember(s => s.Test, opt => opt.Ignore())
                .ForMember(c => c.TestResult, opt => opt.Ignore())
                .ForMember(c => c.Parent, opt => opt.Ignore())
                .ForMember(t => t.ItemId, opt => opt.MapFrom(cc => cc.Item != null ? cc.Item.Id : 0))
                .ForMember(o => o.TestId, opt => opt.MapFrom(t => t.Test == null ? (int?)null : t.Test.Id))
                .ForMember(c => c.TestResultId, opt => opt.MapFrom(o => o.TestResult != null ? (int?)o.TestResult.Id : null))
                .ForMember(c => c.ParentImprintableId, opt => opt.MapFrom(o => o.Parent != null ? (int?)o.Parent.Id : null));

            Mapper.CreateMap<TestImprintableItemEntity, TestImprintableItem>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region Users

            Mapper.CreateMap<User, UserEntity>();

            Mapper.CreateMap<UserEntity, User>().ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region Settings

            Mapper.CreateMap<SettingEntity, Setting>().ForMember(des => des.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<Setting, SettingEntity>();

            #endregion

            #region Services

            Mapper.CreateMap<ServiceEntity, Service>().ForMember(des => des.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<Service, ServiceEntity>();

            #endregion

            #region AppInfo

            Mapper.CreateMap<AppInfoEntity, AppInfo>();

            Mapper.CreateMap<AppInfo, AppInfoEntity>();

            #endregion

            #region Test Protocols

            Mapper.CreateMap<TestProtocolEntity, TestProtocol>().ForMember(des => des.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<TestProtocol, TestProtocolEntity>()
                .ForMember(c => c.ProtocolItems, opt => opt.Ignore())
                .ForMember(c => c.ProtocolSteps, opt => opt.Ignore());

            Mapper.CreateMap<ProtocolStepEntity, ProtocolStep>()
                .ForMember(des => des.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged))
                .ForMember(c => c.Type, opt => opt.ResolveUsing<ProtocolStepCustomResolver>().ConstructedBy(() => new ProtocolStepCustomResolver()));
            
            Mapper.CreateMap<ProtocolStep, ProtocolStepEntity>();

            Mapper.CreateMap<ProtocolItemEntity, ProtocolItem>();

            Mapper.CreateMap<ProtocolItem, ProtocolItemEntity>()
                .ForMember(c => c.Item , opt => opt.Ignore())
                .ForMember(c => c.ItemId , opt => opt.MapFrom(cc => cc.Item.Id));

            #endregion

            #region Test Schedules

            Mapper.CreateMap<TestScheduleEntity, TestSchedule>()
                .ForMember(c => c.ScheduleLines, opt => opt.MapFrom(q => q.ScheduleLines));

            Mapper.CreateMap<TestSchedule, TestScheduleEntity>()
                .ForMember(c => c.Test, opt => opt.Ignore())
                .ForMember(c => c.ScheduleLines, opt => opt.Ignore())
                .ForMember(c => c.EvalPeriodType, opt => opt.Ignore())
                .ForMember(c => c.EvalPeriodTypeLookupId, opt => opt.MapFrom(d => d.EvalPeriodType.Id));

            Mapper.CreateMap<ScheduleLineEntity, ScheduleLine>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged)); 

            Mapper.CreateMap<ScheduleLine, ScheduleLineEntity>();

            #endregion

            #region Properties

            #region Property

            Mapper.CreateMap<PropertyEntity, Property>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<Property, PropertyEntity>()
                .ForMember(dest => dest.ApplicableTypeLookupId, opt => opt.MapFrom(d => d.ApplicableTypeLookup.Id))
                .ForMember(dest => dest.ValueTypeLookupId, opt => opt.MapFrom(d => d.ValueTypeLookup.Id))
                .ForMember(dest => dest.ApplicableTypeLookup, opt => opt.Ignore())
                .ForMember(dest => dest.ValueTypeLookup, opt => opt.Ignore());

            #endregion

            #region ItemProperty

            Mapper.CreateMap<ItemPropertyEntity, ItemProperty>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<ItemProperty, ItemPropertyEntity>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(d => d.Item.Id))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(d => d.Property.Id))
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore());

            #endregion

            #region ItemRelationProperty

            Mapper.CreateMap<ItemRelationPropertyEntity, ItemRelationProperty>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<ItemRelationProperty, ItemRelationPropertyEntity>()
                .ForMember(dest => dest.ItemRelationId, opt => opt.MapFrom(d => d.ItemRelation.Id))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(d => d.Property.Id))
                .ForMember(dest => dest.ItemRelation, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore());

            #endregion

            #endregion

            #region Hardware

            Mapper.CreateMap<ComPortInfoEntity, ComPortInfo>();

            #endregion

            #region AppImages

            Mapper.CreateMap<AppImageEntity, AppImage>();

            Mapper.CreateMap<AppImage, AppImageEntity>();

            #endregion

            #region Spot Checks

            Mapper.CreateMap<SpotCheckEntity, SpotCheck>().ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<SpotCheck, SpotCheckEntity>()
                .ForMember(s => s.Patient, opt => opt.Ignore())
                .ForMember(s => s.SpotCheckResults, opt => opt.Ignore())
                .ForMember(s => s.PatientId, opt => opt.MapFrom(s => s.Patient.Id));

            Mapper.CreateMap<SpotCheckResultEntity, SpotCheckResult>().ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<SpotCheckResult, SpotCheckResultEntity>()
                .ForMember(s => s.SpotCheck, opt => opt.Ignore())
                .ForMember(s => s.SpotCheckId, opt => opt.MapFrom(s => s.SpotCheck.Id));
                //.ForMember(s => s.ResultType, opt => opt.Ignore())
                //.ForMember(s => s.ItemId, opt => opt.MapFrom(s => s.Item.Id));

            #endregion

            #region Frequency Tests 

            Mapper.CreateMap<FrequencyTestEntity, FrequencyTest>()
                .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<FrequencyTest, FrequencyTestEntity>()
                .ForMember(s => s.Patient, opt => opt.Ignore())
                .ForMember(s => s.FrequencyTestResults, opt => opt.Ignore())
                .ForMember(s => s.PatientId, opt => opt.MapFrom(s => s.Patient.Id));

            Mapper.CreateMap<FrequencyTestResultEntity, FrequencyTestResult>()
                .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<FrequencyTestResult, FrequencyTestResultEntity>()
                .ForMember(s => s.FrequencyTest, opt => opt.Ignore())
                .ForMember(s => s.FrequencyTestId, opt => opt.MapFrom(s => s.FrequencyTest.Id));

            #endregion

            #region Invoices

            Mapper.CreateMap<InvoiceEntity, Invoice>()
                .ForMember(c => c.Test , opt => opt.Ignore());

            Mapper.CreateMap<Invoice, InvoiceEntity>()
                .ForMember(c => c.Test, opt => opt.Ignore())
                .ForMember(c => c.User, opt => opt.Ignore())
                .ForMember(c => c.TestId, opt => opt.MapFrom(c=> c.Test != null ? c.Test.Id : 0))
                .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : 0));

            #endregion

            #region ShippingOrders

            Mapper.CreateMap<ShippingOrderEntity, ShippingOrder>()
                .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));
            
            Mapper.CreateMap<ShippingOrder, ShippingOrderEntity>()
                .ForMember(c => c.OrderItems, opt => opt.Ignore())
                .ForMember(o => o.TestId, opt => opt.MapFrom(t => t.Test == null ? (int?)null : t.Test.Id))
                .ForMember(s => s.Test, opt => opt.Ignore());

            Mapper.CreateMap<OrderItemEntity, OrderItem>()
                .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<OrderItem, OrderItemEntity>()
                .ForMember(c => c.Item, opt => opt.Ignore())
                .ForMember(c => c.ItemId, opt => opt.MapFrom(cc => cc.Item.Id));

            #endregion

            #region VitalForceSheet

            #region VFS

            Mapper.CreateMap<VFSEntity, VFS>()
                .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<VFS, VFSEntity>()
                .ForMember(s => s.Patient, opt => opt.Ignore())
                .ForMember(c => c.VFSSecondaryItems, opt => opt.Ignore())
                .ForMember(c => c.VFSItems, opt => opt.Ignore())
                .ForMember(s => s.PatientId, opt => opt.MapFrom(s => s.Patient.Id))
                .ForMember(o => o.TestId, opt => opt.MapFrom(t => t.Test == null ? (int?)null : t.Test.Id))
                .ForMember(s => s.Test, opt => opt.Ignore());

            #endregion

            #region VFS Item Source

            Mapper.CreateMap<VFSItemSourceEntity, VFSItemSource>()
              .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<VFSItemSource, VFSItemSourceEntity>();

            #endregion

            #region VFS Items

            Mapper.CreateMap<VFSItemEntity, VFSItem>()
              .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<VFSItem, VFSItemEntity>()
                .ForMember(s => s.VFS, opt => opt.Ignore())
                .ForMember(o => o.VFSId, opt => opt.MapFrom(t => t.VFS.Id))
                .ForMember(s => s.Item, opt => opt.Ignore())
                .ForMember(o => o.ItemId, opt => opt.MapFrom(t => t.Item.Id))
                .ForMember(o => o.VFSitemSourceId, opt => opt.MapFrom(t => t.VFSItemSource == null ? (int?)null : t.VFSItemSource.Id))
                .ForMember(s => s.VFSItemSource, opt => opt.Ignore());

            #endregion

            #region VFS Secondary Items Source

            Mapper.CreateMap<VFSSecondaryItemSourceEntity, VFSSecondaryItemSource>()
              .ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<VFSSecondaryItemSource, VFSSecondaryItemSourceEntity>();

            #endregion

            #region VFS Secondary Items

            Mapper.CreateMap<VFSSecondaryItemEntity, VFSSecondaryItem>().ForMember(s => s.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<VFSSecondaryItem, VFSSecondaryItemEntity>()
                .ForMember(s => s.VFS, opt => opt.Ignore())
                .ForMember(o => o.VfsId, opt => opt.MapFrom(t => t.VFS.Id))
                .ForMember(s => s.Item, opt => opt.Ignore())
                .ForMember(o => o.ItemId, opt => opt.MapFrom(t => t.Item.Id));

            #endregion

            #endregion

            #region HwProfiles

            Mapper.CreateMap<HwProfileEntity, HwProfile>().ForMember(des => des.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            Mapper.CreateMap<HwProfile, HwProfileEntity>();

            #endregion

            #region TestingPoints

            Mapper.CreateMap<TestingPoint, TestingPointEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null));

            Mapper.CreateMap<TestingPointEntity, TestingPoint>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoItems

            Mapper.CreateMap<AutoItem, AutoItemEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.TestingPoint, opt => opt.Ignore())
                   .ForMember(c => c.Image, opt => opt.Ignore())
                   .ForMember(c => c.Type, opt => opt.Ignore())
                   .ForMember(c => c.Gender, opt => opt.Ignore())
                   .ForMember(c => c.StructureType, opt => opt.Ignore())
                   .ForMember(c => c.Status, opt => opt.Ignore())
                   .ForMember(c => c.ChildsOrderType, opt => opt.Ignore())
                   .ForMember(c => c.ChildsScanningType, opt => opt.Ignore())
                   .ForMember(c => c.ScanningMethod, opt => opt.Ignore())
                   .ForMember(c => c.Products, opt => opt.Ignore())
                   .ForMember(c => c.Parents, opt => opt.Ignore())
                   .ForMember(c => c.Children, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.TestingPointsId, opt => opt.MapFrom(c => c.TestingPoint != null ? c.TestingPoint.Id : (int?)null))
                   .ForMember(c => c.ImageId, opt => opt.MapFrom(c => c.Image != null ? c.Image.Id : (int?)null))
                   .ForMember(c => c.TypeLookupId, opt => opt.MapFrom(c => c.Type != null ? c.Type.Id : (int?)null))
                   .ForMember(c => c.GenderLookupId, opt => opt.MapFrom(c => c.Type != null ? c.Gender.Id : (int?)null))
                   .ForMember(c => c.StructureTypeLookupId, opt => opt.MapFrom(c => c.StructureType != null ? c.StructureType.Id : (int?)null))
                   .ForMember(c => c.StatusLookupId, opt => opt.MapFrom(c => c.Status != null ? c.Status.Id : (int?)null))
                   .ForMember(c => c.ChildsOrderTypeLookupId, opt => opt.MapFrom(c => c.ChildsOrderType != null ? c.ChildsOrderType.Id : (int?)null))
                   .ForMember(c => c.ChildsScanningTypeLookupId, opt => opt.MapFrom(c => c.ChildsScanningType != null ? c.ChildsScanningType.Id : (int?)null))
                   .ForMember(c => c.ScanningMethodLookupId, opt => opt.MapFrom(c => c.ScanningMethod != null ? c.ScanningMethod.Id : (int?)null));

            Mapper.CreateMap<AutoItemEntity, AutoItem>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region Products

            Mapper.CreateMap<Product, ProductEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoItem, opt => opt.Ignore())
                   .ForMember(c => c.ClinicProductPricings, opt => opt.Ignore())
                   .ForMember(c => c.ProductForms, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoItemsId, opt => opt.MapFrom(c => c.AutoItem != null ? c.AutoItem.Id : (int?)null));

            Mapper.CreateMap<ProductEntity, Product>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoProtocols

            Mapper.CreateMap<AutoProtocol, AutoProtocolEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolRevisions, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolStages, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null));

            Mapper.CreateMap<AutoProtocolEntity, AutoProtocol>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoTestStages

            Mapper.CreateMap<AutoTestStage, AutoTestStageEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.StageItemsOrderType, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.StageItemsOrderTypeLookupId, opt => opt.MapFrom(c => c.StageItemsOrderType != null ? c.StageItemsOrderType.Id : (int?)null));

            Mapper.CreateMap<AutoTestStageEntity, AutoTestStage>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoItemRelations

            Mapper.CreateMap<AutoItemRelation, AutoItemRelationEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.Parent, opt => opt.Ignore())
                   .ForMember(c => c.Child, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoItemParentId, opt => opt.MapFrom(c => c.AutoItemsParent != null ? c.AutoItemsParent.Id : (int?)null))
                   .ForMember(c => c.AutoItemChildId, opt => opt.MapFrom(c => c.AutoItemsChild != null ? c.AutoItemsChild.Id : (int?)null));

            Mapper.CreateMap<AutoItemRelationEntity, AutoItemRelation>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoProtocolStages

            Mapper.CreateMap<AutoProtocolStage, AutoProtocolStageEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocol, opt => opt.Ignore())
                   .ForMember(c => c.AutoTestStage, opt => opt.Ignore())
                   .ForMember(c => c.StageItemsOrderType, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolStageRevisions, opt => opt.Ignore())
                   .ForMember(c => c.StageAutoItems, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolsId, opt => opt.MapFrom(c => c.AutoProtocol != null ? c.AutoProtocol.Id : (int?)null))
                   .ForMember(c => c.AutoTestStagesId, opt => opt.MapFrom(c => c.AutoTestStage != null ? c.AutoTestStage.Id : (int?)null))
                   .ForMember(c => c.StageItemsOrderTypeLookupId, opt => opt.MapFrom(c => c.StageItemsOrderType != null ? c.StageItemsOrderType.Id : (int?)null));

            Mapper.CreateMap<AutoProtocolStageEntity, AutoProtocolStage>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region StageAutoItems

            Mapper.CreateMap<StageAutoItem, StageAutoItemEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolStage, opt => opt.Ignore())
                   .ForMember(c => c.Parent, opt => opt.Ignore())
                   .ForMember(c => c.AutoItem, opt => opt.Ignore())
                   .ForMember(c => c.TestingPoint, opt => opt.Ignore())
                   .ForMember(c => c.ScanningMethod, opt => opt.Ignore())
                   .ForMember(c => c.ChildsOrderType, opt => opt.Ignore())
                   .ForMember(c => c.ChildsScanningType, opt => opt.Ignore())
                   .ForMember(c => c.StageAutoItems, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolStagesId, opt => opt.MapFrom(c => c.AutoProtocolStage != null ? c.AutoProtocolStage.Id : (int?)null))
                   .ForMember(c => c.StageAutoItemParentId, opt => opt.MapFrom(c => c.Parent != null ? c.Parent.Id : (int?)null))
                   .ForMember(c => c.AutoItemsId, opt => opt.MapFrom(c => c.AutoItem != null ? c.AutoItem.Id : (int?)null))
                   .ForMember(c => c.TestingPointsId, opt => opt.MapFrom(c => c.TestingPoint != null ? c.TestingPoint.Id : (int?)null))
                   .ForMember(c => c.ScanningMethodLookupId, opt => opt.MapFrom(c => c.ScanningMethod != null ? c.ScanningMethod.Id : (int?)null))
                   .ForMember(c => c.ChildsOrderTypeLookupId, opt => opt.MapFrom(c => c.ChildsOrderType != null ? c.ChildsOrderType.Id : (int?)null))
                   .ForMember(c => c.ChildsScanningTypeLookupId, opt => opt.MapFrom(c => c.ChildsScanningType != null ? c.ChildsScanningType.Id : (int?)null));

            Mapper.CreateMap<StageAutoItemEntity, StageAutoItem>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged))
                  .ForMember(dest => dest.ChildesCount, opt => opt.MapFrom(x => AutoTestSourceDatabaseRepository.GetStageAutoItemsChildesCount(x.Id)));//Custom logic to get the count of child items from DB without loading the childes

            #endregion

            #region AutoProtocolRevisions

            Mapper.CreateMap<AutoProtocolRevision, AutoProtocolRevisionEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocol, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolStageRevisions, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolsId, opt => opt.MapFrom(c => c.AutoProtocol != null ? c.AutoProtocol.Id : (int?)null));

            Mapper.CreateMap<AutoProtocolRevisionEntity, AutoProtocolRevision>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoProtocolStageRevisions

            Mapper.CreateMap<AutoProtocolStageRevision, AutoProtocolStageRevisionEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolRevision, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolStage, opt => opt.Ignore())
                   .ForMember(c => c.AutoTestStage, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolRevisionsId, opt => opt.MapFrom(c => c.AutoProtocolRevision != null ? c.AutoProtocolRevision.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolStagesId, opt => opt.MapFrom(c => c.AutoProtocolStage != null ? c.AutoProtocolStage.Id : (int?)null))
                   .ForMember(c => c.AutoTestStagesId, opt => opt.MapFrom(c => c.AutoTestStage != null ? c.AutoTestStage.Id : (int?)null));

            Mapper.CreateMap<AutoProtocolStageRevisionEntity, AutoProtocolStageRevision>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region ProductForms

            Mapper.CreateMap<ProductForm, ProductFormEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.Product, opt => opt.Ignore())
                   .ForMember(c => c.Status, opt => opt.Ignore())
                   .ForMember(c => c.DosageOptions, opt => opt.Ignore())
                   .ForMember(c => c.ProductSizes, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.ProductsId, opt => opt.MapFrom(c => c.Product != null ? c.Product.Id : (int?)null))
                   .ForMember(c => c.StatusLookupId, opt => opt.MapFrom(c => c.Status != null ? c.Status.Id : (int?)null));

            Mapper.CreateMap<ProductFormEntity, ProductForm>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region ProductSizes

            Mapper.CreateMap<ProductSize, ProductSizeEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.ProductForm, opt => opt.Ignore())
                   .ForMember(c => c.Status, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.ProductFormsId, opt => opt.MapFrom(c => c.ProductForm != null ? c.ProductForm.Id : (int?)null))
                   .ForMember(c => c.StatusLookupsId, opt => opt.MapFrom(c => c.Status != null ? c.Status.Id : (int?)null));

            Mapper.CreateMap<ProductSizeEntity, ProductSize>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region DoageOptions

            Mapper.CreateMap<DosageOption, DosageOptionEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.ProductForm, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.ProductForm, opt => opt.MapFrom(c => c.ProductForm != null ? c.ProductForm.Id : (int?)null));

            Mapper.CreateMap<DosageOptionEntity, DosageOption>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoTest

            Mapper.CreateMap<AutoTest, AutoTestEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.Patient, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolRevision, opt => opt.Ignore())
                   .ForMember(c => c.AutoTestResults, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.PatientId, opt => opt.MapFrom(c => c.Patient != null ? c.Patient.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolRevisionsId, opt => opt.MapFrom(c => c.AutoProtocolRevision != null ? c.AutoProtocolRevision.Id : (int?)null));

            Mapper.CreateMap<AutoTestEntity, AutoTest>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoTestResult

            Mapper.CreateMap<AutoTestResult, AutoTestResultEntity>()
                   .ForMember(c => c.User, opt => opt.Ignore())
                   .ForMember(c => c.AutoTest, opt => opt.Ignore())
                   .ForMember(c => c.AutoItem, opt => opt.Ignore())
                   .ForMember(c => c.AutoProtocolStageRevision, opt => opt.Ignore())
                   .ForMember(c => c.AutoTestResultParent, opt => opt.Ignore())
                   .ForMember(c => c.AutoTestResultProducts, opt => opt.Ignore())
                   .ForMember(c => c.AutoTestResultChildes, opt => opt.Ignore())
                   .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                   .ForMember(c => c.AutoTestsId, opt => opt.MapFrom(c => c.AutoTest != null ? c.AutoTest.Id : (int?)null))
                   .ForMember(c => c.AutoItemsId, opt => opt.MapFrom(c => c.AutoItem != null ? c.AutoItem.Id : (int?)null))
                   .ForMember(c => c.AutoProtocolStageRevisionsId, opt => opt.MapFrom(c => c.AutoProtocolStageRevision != null ? c.AutoProtocolStageRevision.Id : (int?)null))
                   .ForMember(c => c.AutoTestResultsParentId, opt => opt.MapFrom(c => c.AutoTestResultParent != null ? c.AutoTestResultParent.Id : (int?)null));

            Mapper.CreateMap<AutoTestResultEntity, AutoTestResult>()
                  .ForMember(c => c.AutoTestResultParent, opt => opt.Ignore())
                  .ForMember(c => c.AutoTestResultChildes, opt => opt.Ignore())
                  .ForMember(dest => dest.StructureId, opt => opt.Ignore())
                  .ForMember(dest => dest.StructureParentId, opt => opt.Ignore())
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region AutoTestResultProduct

            Mapper.CreateMap<AutoTestResultProduct, AutoTestResultProductEntity>()
                .ForMember(c => c.User, opt => opt.Ignore())
                .ForMember(c => c.AutoTestResult, opt => opt.Ignore())
                .ForMember(c => c.ProductForm, opt => opt.Ignore())
                .ForMember(c => c.ProductSize, opt => opt.Ignore())
                .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?) null))
                .ForMember(c => c.AutoTestResultsId, opt => opt.MapFrom(c => c.AutoTestResult != null ? c.AutoTestResult.Id : (int?) null))
                .ForMember(c => c.ProductFormsId, opt => opt.MapFrom(c => c.ProductForm != null ? c.ProductForm.Id : (int?)null))
                .ForMember(c => c.ProductSizesId, opt => opt.MapFrom(c => c.ProductSize != null ? c.ProductSize.Id : (int?)null));

            Mapper.CreateMap<AutoTestResultProductEntity, AutoTestResultProduct>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region ClinicProductPricing

            Mapper.CreateMap<ClinicProductPricing, ClinicProductPricingEntity>()
                .ForMember(c => c.User, opt => opt.Ignore())
                .ForMember(c => c.Product, opt => opt.Ignore())
                .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?)null))
                .ForMember(c => c.ProductsId, opt => opt.MapFrom(c => c.Product != null ? c.Product.Id : (int?)null));

            Mapper.CreateMap<ClinicProductPricingEntity, ClinicProductPricing>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            #region StageAnnouncement

            Mapper.CreateMap<StageAnnouncement, StageAnnouncementEntity>()
                .ForMember(c => c.User, opt => opt.Ignore())
                .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User != null ? c.User.Id : (int?) null));

            Mapper.CreateMap<StageAnnouncementEntity, StageAnnouncement>()
                  .ForMember(dest => dest.ObjectState, opt => opt.UseValue(DomainEntityState.Unchanged));

            #endregion

            _isAutoMappersConfiged = true;

        }

        #region CustomResolverClasses

        public class PatientHistoryCustomResolver : ValueResolver<PatientHistoryEntity, Lookup>
        {
            protected override Lookup ResolveCore(PatientHistoryEntity source)
            {
                return new Lookup
                {
                    Id = source.Type.Id,
                    Value = source.Type.Value,
                    Type = source.Type.Type,
                    Key = source.Type.Key
                };
            }
        }

        public class IssueProtocolStepCustomResolver : ValueResolver<TestIssueEntity, ProtocolStep>
        {
            protected override ProtocolStep ResolveCore(TestIssueEntity source)
            {
                if (source.ProtocolStep == null) return new ProtocolStep();
                var protocolStep = new ProtocolStep();
                protocolStep.Id = source.ProtocolStep.Id;
                protocolStep.Type = new Lookup() { Id = source.ProtocolStep.Type.Id, Value = source.ProtocolStep.Type.Value };
                protocolStep.Order = source.ProtocolStep.Order;
                return protocolStep;
            }
        }

        public class ProtocolStepCustomResolver : ValueResolver<ProtocolStepEntity, Lookup>
        {
            protected override Lookup ResolveCore(ProtocolStepEntity source)
            {
                return new Lookup()
                {

                    Id = source.Type.Id,
                    Value = source.Type.Value,
                    Type = source.Type.Type,
                    Key = source.Type.Key
                };
            }
        }

        #endregion

        #endregion
    }
}
