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
using System.ComponentModel;
using System.Collections.Generic;
#if !CF
using System.Runtime.Serialization;
#endif
using System.Xml.Serialization;
using Vital.DataLayer;
using Vital.DataLayer.HelperClasses;
using Vital.DataLayer.FactoryClasses;
using Vital.DataLayer.RelationClasses;

using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Vital.DataLayer.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	/// <summary>Entity class which represents the entity 'Test'.<br/><br/></summary>
	[Serializable]
	public partial class TestEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<InvoiceEntity> _invoices;
		private EntityCollection<ReadingEntity> _readings;
		private EntityCollection<ShippingOrderEntity> _shippingOrders;
		private EntityCollection<SpotCheckEntity> _spotChecks;
		private EntityCollection<TestImprintableItemEntity> _testImprintableItems;
		private EntityCollection<TestIssueEntity> _testIssues;
		private EntityCollection<TestServiceEntity> _testServices;
		private EntityCollection<VFSEntity> _vfs;
		private ItemEntity _item;
		private LookupEntity _listPointLookup;
		private LookupEntity _stateLookup;
		private LookupEntity _typeLookup;
		private PatientEntity _patient;
		private TestProtocolEntity _testProtocol;
		private TestScheduleEntity _testSchedule;
		private UserEntity _user;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		#endregion

		#region Statics
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Item</summary>
			public static readonly string Item = "Item";
			/// <summary>Member name ListPointLookup</summary>
			public static readonly string ListPointLookup = "ListPointLookup";
			/// <summary>Member name StateLookup</summary>
			public static readonly string StateLookup = "StateLookup";
			/// <summary>Member name TypeLookup</summary>
			public static readonly string TypeLookup = "TypeLookup";
			/// <summary>Member name Patient</summary>
			public static readonly string Patient = "Patient";
			/// <summary>Member name TestProtocol</summary>
			public static readonly string TestProtocol = "TestProtocol";
			/// <summary>Member name TestSchedule</summary>
			public static readonly string TestSchedule = "TestSchedule";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name Invoices</summary>
			public static readonly string Invoices = "Invoices";
			/// <summary>Member name Readings</summary>
			public static readonly string Readings = "Readings";
			/// <summary>Member name ShippingOrders</summary>
			public static readonly string ShippingOrders = "ShippingOrders";
			/// <summary>Member name SpotChecks</summary>
			public static readonly string SpotChecks = "SpotChecks";
			/// <summary>Member name TestImprintableItems</summary>
			public static readonly string TestImprintableItems = "TestImprintableItems";
			/// <summary>Member name TestIssues</summary>
			public static readonly string TestIssues = "TestIssues";
			/// <summary>Member name TestServices</summary>
			public static readonly string TestServices = "TestServices";
			/// <summary>Member name Vfs</summary>
			public static readonly string Vfs = "Vfs";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static TestEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public TestEntity():base("TestEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public TestEntity(IEntityFields2 fields):base("TestEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this TestEntity</param>
		public TestEntity(IValidator validator):base("TestEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for Test which data should be fetched into this Test object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public TestEntity(System.Int32 id):base("TestEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Test which data should be fetched into this Test object</param>
		/// <param name="validator">The custom validator object for this TestEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public TestEntity(System.Int32 id, IValidator validator):base("TestEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected TestEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_invoices = (EntityCollection<InvoiceEntity>)info.GetValue("_invoices", typeof(EntityCollection<InvoiceEntity>));
				_readings = (EntityCollection<ReadingEntity>)info.GetValue("_readings", typeof(EntityCollection<ReadingEntity>));
				_shippingOrders = (EntityCollection<ShippingOrderEntity>)info.GetValue("_shippingOrders", typeof(EntityCollection<ShippingOrderEntity>));
				_spotChecks = (EntityCollection<SpotCheckEntity>)info.GetValue("_spotChecks", typeof(EntityCollection<SpotCheckEntity>));
				_testImprintableItems = (EntityCollection<TestImprintableItemEntity>)info.GetValue("_testImprintableItems", typeof(EntityCollection<TestImprintableItemEntity>));
				_testIssues = (EntityCollection<TestIssueEntity>)info.GetValue("_testIssues", typeof(EntityCollection<TestIssueEntity>));
				_testServices = (EntityCollection<TestServiceEntity>)info.GetValue("_testServices", typeof(EntityCollection<TestServiceEntity>));
				_vfs = (EntityCollection<VFSEntity>)info.GetValue("_vfs", typeof(EntityCollection<VFSEntity>));
				_item = (ItemEntity)info.GetValue("_item", typeof(ItemEntity));
				if(_item!=null)
				{
					_item.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_listPointLookup = (LookupEntity)info.GetValue("_listPointLookup", typeof(LookupEntity));
				if(_listPointLookup!=null)
				{
					_listPointLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_stateLookup = (LookupEntity)info.GetValue("_stateLookup", typeof(LookupEntity));
				if(_stateLookup!=null)
				{
					_stateLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_typeLookup = (LookupEntity)info.GetValue("_typeLookup", typeof(LookupEntity));
				if(_typeLookup!=null)
				{
					_typeLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_patient = (PatientEntity)info.GetValue("_patient", typeof(PatientEntity));
				if(_patient!=null)
				{
					_patient.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_testProtocol = (TestProtocolEntity)info.GetValue("_testProtocol", typeof(TestProtocolEntity));
				if(_testProtocol!=null)
				{
					_testProtocol.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_testSchedule = (TestScheduleEntity)info.GetValue("_testSchedule", typeof(TestScheduleEntity));
				if(_testSchedule!=null)
				{
					_testSchedule.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_user = (UserEntity)info.GetValue("_user", typeof(UserEntity));
				if(_user!=null)
				{
					_user.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				this.FixupDeserialization(FieldInfoProviderSingleton.GetInstance());
			}
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		
		/// <summary>Performs the desync setup when an FK field has been changed. The entity referenced based on the FK field will be dereferenced and sync info will be removed.</summary>
		/// <param name="fieldIndex">The fieldindex.</param>
		protected override void PerformDesyncSetupFKFieldChange(int fieldIndex)
		{
			switch((TestFieldIndex)fieldIndex)
			{
				case TestFieldIndex.PatientId:
					DesetupSyncPatient(true, false);
					break;
				case TestFieldIndex.TestScheduleId:
					DesetupSyncTestSchedule(true, false);
					break;
				case TestFieldIndex.PointsGroupId:
					DesetupSyncItem(true, false);
					break;
				case TestFieldIndex.TestProtocolId:
					DesetupSyncTestProtocol(true, false);
					break;
				case TestFieldIndex.TestTypeLookupId:
					DesetupSyncTypeLookup(true, false);
					break;
				case TestFieldIndex.ListPointLookupId:
					DesetupSyncListPointLookup(true, false);
					break;
				case TestFieldIndex.TestStateLookupId:
					DesetupSyncStateLookup(true, false);
					break;
				case TestFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				default:
					base.PerformDesyncSetupFKFieldChange(fieldIndex);
					break;
			}
		}

		/// <summary> Sets the related entity property to the entity specified. If the property is a collection, it will add the entity specified to that collection.</summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="entity">Entity to set as an related entity</param>
		/// <remarks>Used by prefetch path logic.</remarks>
		protected override void SetRelatedEntityProperty(string propertyName, IEntityCore entity)
		{
			switch(propertyName)
			{
				case "Item":
					this.Item = (ItemEntity)entity;
					break;
				case "ListPointLookup":
					this.ListPointLookup = (LookupEntity)entity;
					break;
				case "StateLookup":
					this.StateLookup = (LookupEntity)entity;
					break;
				case "TypeLookup":
					this.TypeLookup = (LookupEntity)entity;
					break;
				case "Patient":
					this.Patient = (PatientEntity)entity;
					break;
				case "TestProtocol":
					this.TestProtocol = (TestProtocolEntity)entity;
					break;
				case "TestSchedule":
					this.TestSchedule = (TestScheduleEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "Invoices":
					this.Invoices.Add((InvoiceEntity)entity);
					break;
				case "Readings":
					this.Readings.Add((ReadingEntity)entity);
					break;
				case "ShippingOrders":
					this.ShippingOrders.Add((ShippingOrderEntity)entity);
					break;
				case "SpotChecks":
					this.SpotChecks.Add((SpotCheckEntity)entity);
					break;
				case "TestImprintableItems":
					this.TestImprintableItems.Add((TestImprintableItemEntity)entity);
					break;
				case "TestIssues":
					this.TestIssues.Add((TestIssueEntity)entity);
					break;
				case "TestServices":
					this.TestServices.Add((TestServiceEntity)entity);
					break;
				case "Vfs":
					this.Vfs.Add((VFSEntity)entity);
					break;
				default:
					this.OnSetRelatedEntityProperty(propertyName, entity);
					break;
			}
		}
		
		/// <summary>Gets the relation objects which represent the relation the fieldName specified is mapped on. </summary>
		/// <param name="fieldName">Name of the field mapped onto the relation of which the relation objects have to be obtained.</param>
		/// <returns>RelationCollection with relation object(s) which represent the relation the field is maped on</returns>
		protected override RelationCollection GetRelationsForFieldOfType(string fieldName)
		{
			return GetRelationsForField(fieldName);
		}

		/// <summary>Gets the relation objects which represent the relation the fieldName specified is mapped on. </summary>
		/// <param name="fieldName">Name of the field mapped onto the relation of which the relation objects have to be obtained.</param>
		/// <returns>RelationCollection with relation object(s) which represent the relation the field is maped on</returns>
		internal static RelationCollection GetRelationsForField(string fieldName)
		{
			RelationCollection toReturn = new RelationCollection();
			switch(fieldName)
			{
				case "Item":
					toReturn.Add(Relations.ItemEntityUsingPointsGroupId);
					break;
				case "ListPointLookup":
					toReturn.Add(Relations.LookupEntityUsingListPointLookupId);
					break;
				case "StateLookup":
					toReturn.Add(Relations.LookupEntityUsingTestStateLookupId);
					break;
				case "TypeLookup":
					toReturn.Add(Relations.LookupEntityUsingTestTypeLookupId);
					break;
				case "Patient":
					toReturn.Add(Relations.PatientEntityUsingPatientId);
					break;
				case "TestProtocol":
					toReturn.Add(Relations.TestProtocolEntityUsingTestProtocolId);
					break;
				case "TestSchedule":
					toReturn.Add(Relations.TestScheduleEntityUsingTestScheduleId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "Invoices":
					toReturn.Add(Relations.InvoiceEntityUsingTestId);
					break;
				case "Readings":
					toReturn.Add(Relations.ReadingEntityUsingTestId);
					break;
				case "ShippingOrders":
					toReturn.Add(Relations.ShippingOrderEntityUsingTestId);
					break;
				case "SpotChecks":
					toReturn.Add(Relations.SpotCheckEntityUsingTestId);
					break;
				case "TestImprintableItems":
					toReturn.Add(Relations.TestImprintableItemEntityUsingTestId);
					break;
				case "TestIssues":
					toReturn.Add(Relations.TestIssueEntityUsingTestId);
					break;
				case "TestServices":
					toReturn.Add(Relations.TestServiceEntityUsingTestId);
					break;
				case "Vfs":
					toReturn.Add(Relations.VFSEntityUsingTestId);
					break;
				default:
					break;				
			}
			return toReturn;
		}
#if !CF
		/// <summary>Checks if the relation mapped by the property with the name specified is a one way / single sided relation. If the passed in name is null, it/ will return true if the entity has any single-sided relation</summary>
		/// <param name="propertyName">Name of the property which is mapped onto the relation to check, or null to check if the entity has any relation/ which is single sided</param>
		/// <returns>true if the relation is single sided / one way (so the opposite relation isn't present), false otherwise</returns>
		protected override bool CheckOneWayRelations(string propertyName)
		{
			int numberOfOneWayRelations = 0+1+1+1+1+1+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
				case "Item":
					return true;
				case "ListPointLookup":
					return true;
				case "StateLookup":
					return true;
				case "TypeLookup":
					return true;
				case "TestProtocol":
					return true;
				case "User":
					return true;
				default:
					return base.CheckOneWayRelations(propertyName);
			}
		}
#endif
		/// <summary> Sets the internal parameter related to the fieldname passed to the instance relatedEntity. </summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		protected override void SetRelatedEntity(IEntityCore relatedEntity, string fieldName)
		{
			switch(fieldName)
			{
				case "Item":
					SetupSyncItem(relatedEntity);
					break;
				case "ListPointLookup":
					SetupSyncListPointLookup(relatedEntity);
					break;
				case "StateLookup":
					SetupSyncStateLookup(relatedEntity);
					break;
				case "TypeLookup":
					SetupSyncTypeLookup(relatedEntity);
					break;
				case "Patient":
					SetupSyncPatient(relatedEntity);
					break;
				case "TestProtocol":
					SetupSyncTestProtocol(relatedEntity);
					break;
				case "TestSchedule":
					SetupSyncTestSchedule(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "Invoices":
					this.Invoices.Add((InvoiceEntity)relatedEntity);
					break;
				case "Readings":
					this.Readings.Add((ReadingEntity)relatedEntity);
					break;
				case "ShippingOrders":
					this.ShippingOrders.Add((ShippingOrderEntity)relatedEntity);
					break;
				case "SpotChecks":
					this.SpotChecks.Add((SpotCheckEntity)relatedEntity);
					break;
				case "TestImprintableItems":
					this.TestImprintableItems.Add((TestImprintableItemEntity)relatedEntity);
					break;
				case "TestIssues":
					this.TestIssues.Add((TestIssueEntity)relatedEntity);
					break;
				case "TestServices":
					this.TestServices.Add((TestServiceEntity)relatedEntity);
					break;
				case "Vfs":
					this.Vfs.Add((VFSEntity)relatedEntity);
					break;
				default:
					break;
			}
		}

		/// <summary> Unsets the internal parameter related to the fieldname passed to the instance relatedEntity. Reverses the actions taken by SetRelatedEntity() </summary>
		/// <param name="relatedEntity">Instance to unset as the related entity of type entityType</param>
		/// <param name="fieldName">Name of field mapped onto the relation which resolves in the instance relatedEntity</param>
		/// <param name="signalRelatedEntityManyToOne">if set to true it will notify the manytoone side, if applicable.</param>
		protected override void UnsetRelatedEntity(IEntityCore relatedEntity, string fieldName, bool signalRelatedEntityManyToOne)
		{
			switch(fieldName)
			{
				case "Item":
					DesetupSyncItem(false, true);
					break;
				case "ListPointLookup":
					DesetupSyncListPointLookup(false, true);
					break;
				case "StateLookup":
					DesetupSyncStateLookup(false, true);
					break;
				case "TypeLookup":
					DesetupSyncTypeLookup(false, true);
					break;
				case "Patient":
					DesetupSyncPatient(false, true);
					break;
				case "TestProtocol":
					DesetupSyncTestProtocol(false, true);
					break;
				case "TestSchedule":
					DesetupSyncTestSchedule(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "Invoices":
					this.PerformRelatedEntityRemoval(this.Invoices, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Readings":
					this.PerformRelatedEntityRemoval(this.Readings, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "ShippingOrders":
					this.PerformRelatedEntityRemoval(this.ShippingOrders, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "SpotChecks":
					this.PerformRelatedEntityRemoval(this.SpotChecks, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestImprintableItems":
					this.PerformRelatedEntityRemoval(this.TestImprintableItems, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestIssues":
					this.PerformRelatedEntityRemoval(this.TestIssues, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestServices":
					this.PerformRelatedEntityRemoval(this.TestServices, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Vfs":
					this.PerformRelatedEntityRemoval(this.Vfs, relatedEntity, signalRelatedEntityManyToOne);
					break;
				default:
					break;
			}
		}

		/// <summary> Gets a collection of related entities referenced by this entity which depend on this entity (this entity is the PK side of their FK fields). These entities will have to be persisted after this entity during a recursive save.</summary>
		/// <returns>Collection with 0 or more IEntity2 objects, referenced by this entity</returns>
		protected override List<IEntity2> GetDependingRelatedEntities()
		{
			List<IEntity2> toReturn = new List<IEntity2>();
			return toReturn;
		}
		
		/// <summary> Gets a collection of related entities referenced by this entity which this entity depends on (this entity is the FK side of their PK fields). These
		/// entities will have to be persisted before this entity during a recursive save.</summary>
		/// <returns>Collection with 0 or more IEntity2 objects, referenced by this entity</returns>
		protected override List<IEntity2> GetDependentRelatedEntities()
		{
			List<IEntity2> toReturn = new List<IEntity2>();
			if(_item!=null)
			{
				toReturn.Add(_item);
			}
			if(_listPointLookup!=null)
			{
				toReturn.Add(_listPointLookup);
			}
			if(_stateLookup!=null)
			{
				toReturn.Add(_stateLookup);
			}
			if(_typeLookup!=null)
			{
				toReturn.Add(_typeLookup);
			}
			if(_patient!=null)
			{
				toReturn.Add(_patient);
			}
			if(_testProtocol!=null)
			{
				toReturn.Add(_testProtocol);
			}
			if(_testSchedule!=null)
			{
				toReturn.Add(_testSchedule);
			}
			if(_user!=null)
			{
				toReturn.Add(_user);
			}
			return toReturn;
		}
		
		/// <summary>Gets a list of all entity collections stored as member variables in this entity. Only 1:n related collections are returned.</summary>
		/// <returns>Collection with 0 or more IEntityCollection2 objects, referenced by this entity</returns>
		protected override List<IEntityCollection2> GetMemberEntityCollections()
		{
			List<IEntityCollection2> toReturn = new List<IEntityCollection2>();
			toReturn.Add(this.Invoices);
			toReturn.Add(this.Readings);
			toReturn.Add(this.ShippingOrders);
			toReturn.Add(this.SpotChecks);
			toReturn.Add(this.TestImprintableItems);
			toReturn.Add(this.TestIssues);
			toReturn.Add(this.TestServices);
			toReturn.Add(this.Vfs);
			return toReturn;
		}

		/// <summary>ISerializable member. Does custom serialization so event handlers do not get serialized. Serializes members of this entity class and uses the base class' implementation to serialize the rest.</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				info.AddValue("_invoices", ((_invoices!=null) && (_invoices.Count>0) && !this.MarkedForDeletion)?_invoices:null);
				info.AddValue("_readings", ((_readings!=null) && (_readings.Count>0) && !this.MarkedForDeletion)?_readings:null);
				info.AddValue("_shippingOrders", ((_shippingOrders!=null) && (_shippingOrders.Count>0) && !this.MarkedForDeletion)?_shippingOrders:null);
				info.AddValue("_spotChecks", ((_spotChecks!=null) && (_spotChecks.Count>0) && !this.MarkedForDeletion)?_spotChecks:null);
				info.AddValue("_testImprintableItems", ((_testImprintableItems!=null) && (_testImprintableItems.Count>0) && !this.MarkedForDeletion)?_testImprintableItems:null);
				info.AddValue("_testIssues", ((_testIssues!=null) && (_testIssues.Count>0) && !this.MarkedForDeletion)?_testIssues:null);
				info.AddValue("_testServices", ((_testServices!=null) && (_testServices.Count>0) && !this.MarkedForDeletion)?_testServices:null);
				info.AddValue("_vfs", ((_vfs!=null) && (_vfs.Count>0) && !this.MarkedForDeletion)?_vfs:null);
				info.AddValue("_item", (!this.MarkedForDeletion?_item:null));
				info.AddValue("_listPointLookup", (!this.MarkedForDeletion?_listPointLookup:null));
				info.AddValue("_stateLookup", (!this.MarkedForDeletion?_stateLookup:null));
				info.AddValue("_typeLookup", (!this.MarkedForDeletion?_typeLookup:null));
				info.AddValue("_patient", (!this.MarkedForDeletion?_patient:null));
				info.AddValue("_testProtocol", (!this.MarkedForDeletion?_testProtocol:null));
				info.AddValue("_testSchedule", (!this.MarkedForDeletion?_testSchedule:null));
				info.AddValue("_user", (!this.MarkedForDeletion?_user:null));
			}
			// __LLBLGENPRO_USER_CODE_REGION_START GetObjectInfo
			// __LLBLGENPRO_USER_CODE_REGION_END
			base.GetObjectData(info, context);
		}


				
		/// <summary>Gets a list of all the EntityRelation objects the type of this instance has.</summary>
		/// <returns>A list of all the EntityRelation objects the type of this instance has. Hierarchy relations are excluded.</returns>
		protected override List<IEntityRelation> GetAllRelations()
		{
			return new TestRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Invoice' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoInvoices()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(InvoiceFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Reading' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoReadings()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ReadingFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ShippingOrder' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoShippingOrders()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ShippingOrderFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SpotCheck' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotChecks()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestImprintableItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestImprintableItems()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestImprintableItemFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestIssue' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestIssues()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestIssueFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestService' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestServices()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestServiceFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VFS' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVfs()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSFields.TestId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Item' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItem()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemFields.Id, null, ComparisonOperator.Equal, this.PointsGroupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoListPointLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ListPointLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStateLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.TestStateLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTypeLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.TestTypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Patient' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoPatient()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(PatientFields.Id, null, ComparisonOperator.Equal, this.PatientId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestProtocol' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestProtocol()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestProtocolFields.Id, null, ComparisonOperator.Equal, this.TestProtocolId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestSchedule' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestSchedule()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestScheduleFields.Id, null, ComparisonOperator.Equal, this.TestScheduleId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'User' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoUser()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(UserFields.Id, null, ComparisonOperator.Equal, this.UserId));
			return bucket;
		}
		

		/// <summary>Creates a new instance of the factory related to this entity</summary>
		protected override IEntityFactory2 CreateEntityFactory()
		{
			return EntityFactoryCache2.GetEntityFactory(typeof(TestEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._invoices);
			collectionsQueue.Enqueue(this._readings);
			collectionsQueue.Enqueue(this._shippingOrders);
			collectionsQueue.Enqueue(this._spotChecks);
			collectionsQueue.Enqueue(this._testImprintableItems);
			collectionsQueue.Enqueue(this._testIssues);
			collectionsQueue.Enqueue(this._testServices);
			collectionsQueue.Enqueue(this._vfs);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._invoices = (EntityCollection<InvoiceEntity>) collectionsQueue.Dequeue();
			this._readings = (EntityCollection<ReadingEntity>) collectionsQueue.Dequeue();
			this._shippingOrders = (EntityCollection<ShippingOrderEntity>) collectionsQueue.Dequeue();
			this._spotChecks = (EntityCollection<SpotCheckEntity>) collectionsQueue.Dequeue();
			this._testImprintableItems = (EntityCollection<TestImprintableItemEntity>) collectionsQueue.Dequeue();
			this._testIssues = (EntityCollection<TestIssueEntity>) collectionsQueue.Dequeue();
			this._testServices = (EntityCollection<TestServiceEntity>) collectionsQueue.Dequeue();
			this._vfs = (EntityCollection<VFSEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._invoices != null);
			toReturn |=(this._readings != null);
			toReturn |=(this._shippingOrders != null);
			toReturn |=(this._spotChecks != null);
			toReturn |=(this._testImprintableItems != null);
			toReturn |=(this._testIssues != null);
			toReturn |=(this._testServices != null);
			toReturn |=(this._vfs != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<InvoiceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(InvoiceEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ReadingEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ReadingEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ShippingOrderEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<SpotCheckEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestImprintableItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestImprintableItemEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestIssueEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestIssueEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestServiceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestServiceEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<VFSEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Item", _item);
			toReturn.Add("ListPointLookup", _listPointLookup);
			toReturn.Add("StateLookup", _stateLookup);
			toReturn.Add("TypeLookup", _typeLookup);
			toReturn.Add("Patient", _patient);
			toReturn.Add("TestProtocol", _testProtocol);
			toReturn.Add("TestSchedule", _testSchedule);
			toReturn.Add("User", _user);
			toReturn.Add("Invoices", _invoices);
			toReturn.Add("Readings", _readings);
			toReturn.Add("ShippingOrders", _shippingOrders);
			toReturn.Add("SpotChecks", _spotChecks);
			toReturn.Add("TestImprintableItems", _testImprintableItems);
			toReturn.Add("TestIssues", _testIssues);
			toReturn.Add("TestServices", _testServices);
			toReturn.Add("Vfs", _vfs);
			return toReturn;
		}

		/// <summary> Initializes the class members</summary>
		private void InitClassMembers()
		{
			PerformDependencyInjection();
			
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassMembers
			// __LLBLGENPRO_USER_CODE_REGION_END
			OnInitClassMembersComplete();
		}


		#region Custom Property Hashtable Setup
		/// <summary> Initializes the hashtables for the entity type and entity field custom properties. </summary>
		private static void SetupCustomPropertyHashtables()
		{
			_customProperties = new Dictionary<string, string>();
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Id", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TestScheduleId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PointsGroupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TestProtocolId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("DateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Description", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TestTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ListPointLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TestStateLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Notes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("NumberOfIssues", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("EvalPeriodChecked", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsOrderSent", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _item</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticTestRelations.ItemEntityUsingPointsGroupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestFieldIndex.PointsGroupId } );
			_item = null;
		}

		/// <summary> setups the sync logic for member _item</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncItem(IEntityCore relatedEntity)
		{
			if(_item!=relatedEntity)
			{
				DesetupSyncItem(true, true);
				_item = (ItemEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticTestRelations.ItemEntityUsingPointsGroupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnItemPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _listPointLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncListPointLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _listPointLookup, new PropertyChangedEventHandler( OnListPointLookupPropertyChanged ), "ListPointLookup", Vital.DataLayer.RelationClasses.StaticTestRelations.LookupEntityUsingListPointLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestFieldIndex.ListPointLookupId } );
			_listPointLookup = null;
		}

		/// <summary> setups the sync logic for member _listPointLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncListPointLookup(IEntityCore relatedEntity)
		{
			if(_listPointLookup!=relatedEntity)
			{
				DesetupSyncListPointLookup(true, true);
				_listPointLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _listPointLookup, new PropertyChangedEventHandler( OnListPointLookupPropertyChanged ), "ListPointLookup", Vital.DataLayer.RelationClasses.StaticTestRelations.LookupEntityUsingListPointLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnListPointLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _stateLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncStateLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _stateLookup, new PropertyChangedEventHandler( OnStateLookupPropertyChanged ), "StateLookup", Vital.DataLayer.RelationClasses.StaticTestRelations.LookupEntityUsingTestStateLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestFieldIndex.TestStateLookupId } );
			_stateLookup = null;
		}

		/// <summary> setups the sync logic for member _stateLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncStateLookup(IEntityCore relatedEntity)
		{
			if(_stateLookup!=relatedEntity)
			{
				DesetupSyncStateLookup(true, true);
				_stateLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _stateLookup, new PropertyChangedEventHandler( OnStateLookupPropertyChanged ), "StateLookup", Vital.DataLayer.RelationClasses.StaticTestRelations.LookupEntityUsingTestStateLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStateLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _typeLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTypeLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _typeLookup, new PropertyChangedEventHandler( OnTypeLookupPropertyChanged ), "TypeLookup", Vital.DataLayer.RelationClasses.StaticTestRelations.LookupEntityUsingTestTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestFieldIndex.TestTypeLookupId } );
			_typeLookup = null;
		}

		/// <summary> setups the sync logic for member _typeLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTypeLookup(IEntityCore relatedEntity)
		{
			if(_typeLookup!=relatedEntity)
			{
				DesetupSyncTypeLookup(true, true);
				_typeLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _typeLookup, new PropertyChangedEventHandler( OnTypeLookupPropertyChanged ), "TypeLookup", Vital.DataLayer.RelationClasses.StaticTestRelations.LookupEntityUsingTestTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTypeLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _patient</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncPatient(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _patient, new PropertyChangedEventHandler( OnPatientPropertyChanged ), "Patient", Vital.DataLayer.RelationClasses.StaticTestRelations.PatientEntityUsingPatientIdStatic, true, signalRelatedEntity, "Tests", resetFKFields, new int[] { (int)TestFieldIndex.PatientId } );
			_patient = null;
		}

		/// <summary> setups the sync logic for member _patient</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncPatient(IEntityCore relatedEntity)
		{
			if(_patient!=relatedEntity)
			{
				DesetupSyncPatient(true, true);
				_patient = (PatientEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _patient, new PropertyChangedEventHandler( OnPatientPropertyChanged ), "Patient", Vital.DataLayer.RelationClasses.StaticTestRelations.PatientEntityUsingPatientIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPatientPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _testProtocol</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTestProtocol(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _testProtocol, new PropertyChangedEventHandler( OnTestProtocolPropertyChanged ), "TestProtocol", Vital.DataLayer.RelationClasses.StaticTestRelations.TestProtocolEntityUsingTestProtocolIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestFieldIndex.TestProtocolId } );
			_testProtocol = null;
		}

		/// <summary> setups the sync logic for member _testProtocol</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTestProtocol(IEntityCore relatedEntity)
		{
			if(_testProtocol!=relatedEntity)
			{
				DesetupSyncTestProtocol(true, true);
				_testProtocol = (TestProtocolEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _testProtocol, new PropertyChangedEventHandler( OnTestProtocolPropertyChanged ), "TestProtocol", Vital.DataLayer.RelationClasses.StaticTestRelations.TestProtocolEntityUsingTestProtocolIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTestProtocolPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _testSchedule</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTestSchedule(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _testSchedule, new PropertyChangedEventHandler( OnTestSchedulePropertyChanged ), "TestSchedule", Vital.DataLayer.RelationClasses.StaticTestRelations.TestScheduleEntityUsingTestScheduleIdStatic, true, signalRelatedEntity, "Test", resetFKFields, new int[] { (int)TestFieldIndex.TestScheduleId } );
			_testSchedule = null;
		}

		/// <summary> setups the sync logic for member _testSchedule</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTestSchedule(IEntityCore relatedEntity)
		{
			if(_testSchedule!=relatedEntity)
			{
				DesetupSyncTestSchedule(true, true);
				_testSchedule = (TestScheduleEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _testSchedule, new PropertyChangedEventHandler( OnTestSchedulePropertyChanged ), "TestSchedule", Vital.DataLayer.RelationClasses.StaticTestRelations.TestScheduleEntityUsingTestScheduleIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTestSchedulePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _user</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncUser(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticTestRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestFieldIndex.UserId } );
			_user = null;
		}

		/// <summary> setups the sync logic for member _user</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncUser(IEntityCore relatedEntity)
		{
			if(_user!=relatedEntity)
			{
				DesetupSyncUser(true, true);
				_user = (UserEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticTestRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnUserPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this TestEntity</param>
		/// <param name="fields">Fields of this entity</param>
		private void InitClassEmpty(IValidator validator, IEntityFields2 fields)
		{
			OnInitializing();
			this.Fields = fields ?? CreateFields();
			this.Validator = validator;
			InitClassMembers();

			// __LLBLGENPRO_USER_CODE_REGION_START InitClassEmpty
			// __LLBLGENPRO_USER_CODE_REGION_END

			OnInitialized();

		}

		#region Class Property Declarations
		/// <summary> The relations object holding all relations of this entity with other entity classes.</summary>
		public  static TestRelations Relations
		{
			get	{ return new TestRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Invoice' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathInvoices
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<InvoiceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(InvoiceEntityFactory))), (IEntityRelation)GetRelationsForField("Invoices")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.InvoiceEntity, 0, null, null, null, null, "Invoices", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Reading' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathReadings
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ReadingEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ReadingEntityFactory))), (IEntityRelation)GetRelationsForField("Readings")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.ReadingEntity, 0, null, null, null, null, "Readings", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ShippingOrder' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathShippingOrders
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ShippingOrderEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory))), (IEntityRelation)GetRelationsForField("ShippingOrders")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.ShippingOrderEntity, 0, null, null, null, null, "ShippingOrders", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheck' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotChecks
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<SpotCheckEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))), (IEntityRelation)GetRelationsForField("SpotChecks")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.SpotCheckEntity, 0, null, null, null, null, "SpotChecks", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestImprintableItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestImprintableItems
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestImprintableItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestImprintableItemEntityFactory))), (IEntityRelation)GetRelationsForField("TestImprintableItems")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.TestImprintableItemEntity, 0, null, null, null, null, "TestImprintableItems", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestIssue' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestIssues
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestIssueEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestIssueEntityFactory))), (IEntityRelation)GetRelationsForField("TestIssues")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.TestIssueEntity, 0, null, null, null, null, "TestIssues", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestService' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestServices
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestServiceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestServiceEntityFactory))), (IEntityRelation)GetRelationsForField("TestServices")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.TestServiceEntity, 0, null, null, null, null, "TestServices", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFS' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVfs
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<VFSEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSEntityFactory))), (IEntityRelation)GetRelationsForField("Vfs")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.VFSEntity, 0, null, null, null, null, "Vfs", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Item' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathItem
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("Item")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "Item", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathListPointLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ListPointLookup")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ListPointLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStateLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("StateLookup")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "StateLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTypeLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("TypeLookup")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "TypeLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Patient' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathPatient
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(PatientEntityFactory))),	(IEntityRelation)GetRelationsForField("Patient")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.PatientEntity, 0, null, null, null, null, "Patient", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestProtocol' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestProtocol
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestProtocolEntityFactory))),	(IEntityRelation)GetRelationsForField("TestProtocol")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.TestProtocolEntity, 0, null, null, null, null, "TestProtocol", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestSchedule' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestSchedule
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestScheduleEntityFactory))),	(IEntityRelation)GetRelationsForField("TestSchedule")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.TestScheduleEntity, 0, null, null, null, null, "TestSchedule", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.TestEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}


		/// <summary> The custom properties for the type of this entity instance.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType
		{
			get { return CustomProperties;}
		}

		/// <summary> The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties
		{
			get { return _fieldsCustomProperties;}
		}

		/// <summary> The custom properties for the fields of the type of this entity instance. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType
		{
			get { return FieldsCustomProperties;}
		}

		/// <summary> The Id property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)TestFieldIndex.Id, true); }
			set	{ SetValue((int)TestFieldIndex.Id, value); }
		}

		/// <summary> The PatientId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Patient_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 PatientId
		{
			get { return (System.Int32)GetValue((int)TestFieldIndex.PatientId, true); }
			set	{ SetValue((int)TestFieldIndex.PatientId, value); }
		}

		/// <summary> The TestScheduleId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_Schedule_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> TestScheduleId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestFieldIndex.TestScheduleId, false); }
			set	{ SetValue((int)TestFieldIndex.TestScheduleId, value); }
		}

		/// <summary> The Name property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_Name"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)TestFieldIndex.Name, true); }
			set	{ SetValue((int)TestFieldIndex.Name, value); }
		}

		/// <summary> The PointsGroupId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Item_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> PointsGroupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestFieldIndex.PointsGroupId, false); }
			set	{ SetValue((int)TestFieldIndex.PointsGroupId, value); }
		}

		/// <summary> The TestProtocolId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_Protocol_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> TestProtocolId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestFieldIndex.TestProtocolId, false); }
			set	{ SetValue((int)TestFieldIndex.TestProtocolId, value); }
		}

		/// <summary> The DateTime property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_DateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> DateTime
		{
			get { return (Nullable<System.DateTime>)GetValue((int)TestFieldIndex.DateTime, false); }
			set	{ SetValue((int)TestFieldIndex.DateTime, value); }
		}

		/// <summary> The Description property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_Description"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Description
		{
			get { return (System.String)GetValue((int)TestFieldIndex.Description, true); }
			set	{ SetValue((int)TestFieldIndex.Description, value); }
		}

		/// <summary> The TestTypeLookupId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."TestType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 TestTypeLookupId
		{
			get { return (System.Int32)GetValue((int)TestFieldIndex.TestTypeLookupId, true); }
			set	{ SetValue((int)TestFieldIndex.TestTypeLookupId, value); }
		}

		/// <summary> The ListPointLookupId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."ListPoints_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ListPointLookupId
		{
			get { return (System.Int32)GetValue((int)TestFieldIndex.ListPointLookupId, true); }
			set	{ SetValue((int)TestFieldIndex.ListPointLookupId, value); }
		}

		/// <summary> The TestStateLookupId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."TestState_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 TestStateLookupId
		{
			get { return (System.Int32)GetValue((int)TestFieldIndex.TestStateLookupId, true); }
			set	{ SetValue((int)TestFieldIndex.TestStateLookupId, value); }
		}

		/// <summary> The Notes property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_Notes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Notes
		{
			get { return (System.String)GetValue((int)TestFieldIndex.Notes, true); }
			set	{ SetValue((int)TestFieldIndex.Notes, value); }
		}

		/// <summary> The NumberOfIssues property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_NumberOfIssues"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> NumberOfIssues
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestFieldIndex.NumberOfIssues, false); }
			set	{ SetValue((int)TestFieldIndex.NumberOfIssues, value); }
		}

		/// <summary> The UserId property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)TestFieldIndex.UserId, true); }
			set	{ SetValue((int)TestFieldIndex.UserId, value); }
		}

		/// <summary> The CreationDateTime property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)TestFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)TestFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)TestFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)TestFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The EvalPeriodChecked property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_EvalPeriodChecked"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> EvalPeriodChecked
		{
			get { return (Nullable<System.Boolean>)GetValue((int)TestFieldIndex.EvalPeriodChecked, false); }
			set	{ SetValue((int)TestFieldIndex.EvalPeriodChecked, value); }
		}

		/// <summary> The IsOrderSent property of the Entity Test<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Tests"."Test_IsOrderSent"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> IsOrderSent
		{
			get { return (Nullable<System.Boolean>)GetValue((int)TestFieldIndex.IsOrderSent, false); }
			set	{ SetValue((int)TestFieldIndex.IsOrderSent, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'InvoiceEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(InvoiceEntity))]
		public virtual EntityCollection<InvoiceEntity> Invoices
		{
			get { return GetOrCreateEntityCollection<InvoiceEntity, InvoiceEntityFactory>("Test", true, false, ref _invoices);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ReadingEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ReadingEntity))]
		public virtual EntityCollection<ReadingEntity> Readings
		{
			get { return GetOrCreateEntityCollection<ReadingEntity, ReadingEntityFactory>("Test", true, false, ref _readings);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ShippingOrderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ShippingOrderEntity))]
		public virtual EntityCollection<ShippingOrderEntity> ShippingOrders
		{
			get { return GetOrCreateEntityCollection<ShippingOrderEntity, ShippingOrderEntityFactory>("Test", true, false, ref _shippingOrders);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'SpotCheckEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SpotCheckEntity))]
		public virtual EntityCollection<SpotCheckEntity> SpotChecks
		{
			get { return GetOrCreateEntityCollection<SpotCheckEntity, SpotCheckEntityFactory>("Test", true, false, ref _spotChecks);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestImprintableItemEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestImprintableItemEntity))]
		public virtual EntityCollection<TestImprintableItemEntity> TestImprintableItems
		{
			get { return GetOrCreateEntityCollection<TestImprintableItemEntity, TestImprintableItemEntityFactory>("Test", true, false, ref _testImprintableItems);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestIssueEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestIssueEntity))]
		public virtual EntityCollection<TestIssueEntity> TestIssues
		{
			get { return GetOrCreateEntityCollection<TestIssueEntity, TestIssueEntityFactory>("Test", true, false, ref _testIssues);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestServiceEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestServiceEntity))]
		public virtual EntityCollection<TestServiceEntity> TestServices
		{
			get { return GetOrCreateEntityCollection<TestServiceEntity, TestServiceEntityFactory>("Test", true, false, ref _testServices);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'VFSEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VFSEntity))]
		public virtual EntityCollection<VFSEntity> Vfs
		{
			get { return GetOrCreateEntityCollection<VFSEntity, VFSEntityFactory>("Test", true, false, ref _vfs);	}
		}

		/// <summary> Gets / sets related entity of type 'ItemEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ItemEntity Item
		{
			get	{ return _item; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncItem(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "Item", _item, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ListPointLookup
		{
			get	{ return _listPointLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncListPointLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "ListPointLookup", _listPointLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity StateLookup
		{
			get	{ return _stateLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncStateLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "StateLookup", _stateLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity TypeLookup
		{
			get	{ return _typeLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTypeLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "TypeLookup", _typeLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'PatientEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual PatientEntity Patient
		{
			get	{ return _patient; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncPatient(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "Tests", "Patient", _patient, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestProtocolEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestProtocolEntity TestProtocol
		{
			get	{ return _testProtocol; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTestProtocol(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "TestProtocol", _testProtocol, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestScheduleEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestScheduleEntity TestSchedule
		{
			get	{ return _testSchedule; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTestSchedule(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "Test", "TestSchedule", _testSchedule, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'UserEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual UserEntity User
		{
			get	{ return _user; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncUser(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "User", _user, false); 
				}
			}
		}
	
		/// <summary> Gets the type of the hierarchy this entity is in. </summary>
		protected override InheritanceHierarchyType LLBLGenProIsInHierarchyOfType
		{
			get { return InheritanceHierarchyType.None;}
		}
		
		/// <summary> Gets or sets a value indicating whether this entity is a subtype</summary>
		protected override bool LLBLGenProIsSubType
		{
			get { return false;}
		}
		
		/// <summary>Returns the Vital.DataLayer.EntityType enum value for this entity.</summary>
		[Browsable(false), XmlIgnore]
		protected override int LLBLGenProEntityTypeValue 
		{ 
			get { return (int)Vital.DataLayer.EntityType.TestEntity; }
		}

		#endregion


		#region Custom Entity code
		
		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END
		#endregion

		#region Included code

		#endregion
	}
}
