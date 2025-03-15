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
	/// <summary>Entity class which represents the entity 'User'.<br/><br/></summary>
	[Serializable]
	public partial class UserEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<FrequencyTestEntity> _frequencyTests;
		private EntityCollection<FrequencyTestResultEntity> _frequencyTestResults;
		private EntityCollection<HwProfileEntity> _hwprofiles;
		private EntityCollection<InvoiceEntity> _invoices;
		private EntityCollection<ShippingOrderEntity> _shippingOrders;
		private EntityCollection<SpotCheckEntity> _spotChecks;
		private EntityCollection<SpotCheckResultEntity> _spotCheckResults;
		private EntityCollection<VFSItemSourceEntity> _vfsitemsSources;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		#endregion

		#region Statics
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name FrequencyTests</summary>
			public static readonly string FrequencyTests = "FrequencyTests";
			/// <summary>Member name FrequencyTestResults</summary>
			public static readonly string FrequencyTestResults = "FrequencyTestResults";
			/// <summary>Member name Hwprofiles</summary>
			public static readonly string Hwprofiles = "Hwprofiles";
			/// <summary>Member name Invoices</summary>
			public static readonly string Invoices = "Invoices";
			/// <summary>Member name ShippingOrders</summary>
			public static readonly string ShippingOrders = "ShippingOrders";
			/// <summary>Member name SpotChecks</summary>
			public static readonly string SpotChecks = "SpotChecks";
			/// <summary>Member name SpotCheckResults</summary>
			public static readonly string SpotCheckResults = "SpotCheckResults";
			/// <summary>Member name VfsitemsSources</summary>
			public static readonly string VfsitemsSources = "VfsitemsSources";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static UserEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public UserEntity():base("UserEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public UserEntity(IEntityFields2 fields):base("UserEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this UserEntity</param>
		public UserEntity(IValidator validator):base("UserEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for User which data should be fetched into this User object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public UserEntity(System.Int32 id):base("UserEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for User which data should be fetched into this User object</param>
		/// <param name="validator">The custom validator object for this UserEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public UserEntity(System.Int32 id, IValidator validator):base("UserEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected UserEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_frequencyTests = (EntityCollection<FrequencyTestEntity>)info.GetValue("_frequencyTests", typeof(EntityCollection<FrequencyTestEntity>));
				_frequencyTestResults = (EntityCollection<FrequencyTestResultEntity>)info.GetValue("_frequencyTestResults", typeof(EntityCollection<FrequencyTestResultEntity>));
				_hwprofiles = (EntityCollection<HwProfileEntity>)info.GetValue("_hwprofiles", typeof(EntityCollection<HwProfileEntity>));
				_invoices = (EntityCollection<InvoiceEntity>)info.GetValue("_invoices", typeof(EntityCollection<InvoiceEntity>));
				_shippingOrders = (EntityCollection<ShippingOrderEntity>)info.GetValue("_shippingOrders", typeof(EntityCollection<ShippingOrderEntity>));
				_spotChecks = (EntityCollection<SpotCheckEntity>)info.GetValue("_spotChecks", typeof(EntityCollection<SpotCheckEntity>));
				_spotCheckResults = (EntityCollection<SpotCheckResultEntity>)info.GetValue("_spotCheckResults", typeof(EntityCollection<SpotCheckResultEntity>));
				_vfsitemsSources = (EntityCollection<VFSItemSourceEntity>)info.GetValue("_vfsitemsSources", typeof(EntityCollection<VFSItemSourceEntity>));
				this.FixupDeserialization(FieldInfoProviderSingleton.GetInstance());
			}
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}


		/// <summary> Sets the related entity property to the entity specified. If the property is a collection, it will add the entity specified to that collection.</summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="entity">Entity to set as an related entity</param>
		/// <remarks>Used by prefetch path logic.</remarks>
		protected override void SetRelatedEntityProperty(string propertyName, IEntityCore entity)
		{
			switch(propertyName)
			{
				case "FrequencyTests":
					this.FrequencyTests.Add((FrequencyTestEntity)entity);
					break;
				case "FrequencyTestResults":
					this.FrequencyTestResults.Add((FrequencyTestResultEntity)entity);
					break;
				case "Hwprofiles":
					this.Hwprofiles.Add((HwProfileEntity)entity);
					break;
				case "Invoices":
					this.Invoices.Add((InvoiceEntity)entity);
					break;
				case "ShippingOrders":
					this.ShippingOrders.Add((ShippingOrderEntity)entity);
					break;
				case "SpotChecks":
					this.SpotChecks.Add((SpotCheckEntity)entity);
					break;
				case "SpotCheckResults":
					this.SpotCheckResults.Add((SpotCheckResultEntity)entity);
					break;
				case "VfsitemsSources":
					this.VfsitemsSources.Add((VFSItemSourceEntity)entity);
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
				case "FrequencyTests":
					toReturn.Add(Relations.FrequencyTestEntityUsingUserId);
					break;
				case "FrequencyTestResults":
					toReturn.Add(Relations.FrequencyTestResultEntityUsingUserId);
					break;
				case "Hwprofiles":
					toReturn.Add(Relations.HwProfileEntityUsingUserId);
					break;
				case "Invoices":
					toReturn.Add(Relations.InvoiceEntityUsingUserId);
					break;
				case "ShippingOrders":
					toReturn.Add(Relations.ShippingOrderEntityUsingUserId);
					break;
				case "SpotChecks":
					toReturn.Add(Relations.SpotCheckEntityUsingUserId);
					break;
				case "SpotCheckResults":
					toReturn.Add(Relations.SpotCheckResultEntityUsingUserId);
					break;
				case "VfsitemsSources":
					toReturn.Add(Relations.VFSItemSourceEntityUsingUserId);
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
			int numberOfOneWayRelations = 0;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
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
				case "FrequencyTests":
					this.FrequencyTests.Add((FrequencyTestEntity)relatedEntity);
					break;
				case "FrequencyTestResults":
					this.FrequencyTestResults.Add((FrequencyTestResultEntity)relatedEntity);
					break;
				case "Hwprofiles":
					this.Hwprofiles.Add((HwProfileEntity)relatedEntity);
					break;
				case "Invoices":
					this.Invoices.Add((InvoiceEntity)relatedEntity);
					break;
				case "ShippingOrders":
					this.ShippingOrders.Add((ShippingOrderEntity)relatedEntity);
					break;
				case "SpotChecks":
					this.SpotChecks.Add((SpotCheckEntity)relatedEntity);
					break;
				case "SpotCheckResults":
					this.SpotCheckResults.Add((SpotCheckResultEntity)relatedEntity);
					break;
				case "VfsitemsSources":
					this.VfsitemsSources.Add((VFSItemSourceEntity)relatedEntity);
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
				case "FrequencyTests":
					this.PerformRelatedEntityRemoval(this.FrequencyTests, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "FrequencyTestResults":
					this.PerformRelatedEntityRemoval(this.FrequencyTestResults, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Hwprofiles":
					this.PerformRelatedEntityRemoval(this.Hwprofiles, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Invoices":
					this.PerformRelatedEntityRemoval(this.Invoices, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "ShippingOrders":
					this.PerformRelatedEntityRemoval(this.ShippingOrders, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "SpotChecks":
					this.PerformRelatedEntityRemoval(this.SpotChecks, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "SpotCheckResults":
					this.PerformRelatedEntityRemoval(this.SpotCheckResults, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "VfsitemsSources":
					this.PerformRelatedEntityRemoval(this.VfsitemsSources, relatedEntity, signalRelatedEntityManyToOne);
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
			return toReturn;
		}
		
		/// <summary>Gets a list of all entity collections stored as member variables in this entity. Only 1:n related collections are returned.</summary>
		/// <returns>Collection with 0 or more IEntityCollection2 objects, referenced by this entity</returns>
		protected override List<IEntityCollection2> GetMemberEntityCollections()
		{
			List<IEntityCollection2> toReturn = new List<IEntityCollection2>();
			toReturn.Add(this.FrequencyTests);
			toReturn.Add(this.FrequencyTestResults);
			toReturn.Add(this.Hwprofiles);
			toReturn.Add(this.Invoices);
			toReturn.Add(this.ShippingOrders);
			toReturn.Add(this.SpotChecks);
			toReturn.Add(this.SpotCheckResults);
			toReturn.Add(this.VfsitemsSources);
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
				info.AddValue("_frequencyTests", ((_frequencyTests!=null) && (_frequencyTests.Count>0) && !this.MarkedForDeletion)?_frequencyTests:null);
				info.AddValue("_frequencyTestResults", ((_frequencyTestResults!=null) && (_frequencyTestResults.Count>0) && !this.MarkedForDeletion)?_frequencyTestResults:null);
				info.AddValue("_hwprofiles", ((_hwprofiles!=null) && (_hwprofiles.Count>0) && !this.MarkedForDeletion)?_hwprofiles:null);
				info.AddValue("_invoices", ((_invoices!=null) && (_invoices.Count>0) && !this.MarkedForDeletion)?_invoices:null);
				info.AddValue("_shippingOrders", ((_shippingOrders!=null) && (_shippingOrders.Count>0) && !this.MarkedForDeletion)?_shippingOrders:null);
				info.AddValue("_spotChecks", ((_spotChecks!=null) && (_spotChecks.Count>0) && !this.MarkedForDeletion)?_spotChecks:null);
				info.AddValue("_spotCheckResults", ((_spotCheckResults!=null) && (_spotCheckResults.Count>0) && !this.MarkedForDeletion)?_spotCheckResults:null);
				info.AddValue("_vfsitemsSources", ((_vfsitemsSources!=null) && (_vfsitemsSources.Count>0) && !this.MarkedForDeletion)?_vfsitemsSources:null);
			}
			// __LLBLGENPRO_USER_CODE_REGION_START GetObjectInfo
			// __LLBLGENPRO_USER_CODE_REGION_END
			base.GetObjectData(info, context);
		}


				
		/// <summary>Gets a list of all the EntityRelation objects the type of this instance has.</summary>
		/// <returns>A list of all the EntityRelation objects the type of this instance has. Hierarchy relations are excluded.</returns>
		protected override List<IEntityRelation> GetAllRelations()
		{
			return new UserRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'FrequencyTest' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoFrequencyTests()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(FrequencyTestFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'FrequencyTestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoFrequencyTestResults()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(FrequencyTestResultFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'HwProfile' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoHwprofiles()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(HwProfileFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Invoice' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoInvoices()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(InvoiceFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ShippingOrder' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoShippingOrders()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ShippingOrderFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SpotCheck' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotChecks()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SpotCheckResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotCheckResults()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckResultFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VFSItemSource' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVfsitemsSources()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSItemSourceFields.UserId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}
		

		/// <summary>Creates a new instance of the factory related to this entity</summary>
		protected override IEntityFactory2 CreateEntityFactory()
		{
			return EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._frequencyTests);
			collectionsQueue.Enqueue(this._frequencyTestResults);
			collectionsQueue.Enqueue(this._hwprofiles);
			collectionsQueue.Enqueue(this._invoices);
			collectionsQueue.Enqueue(this._shippingOrders);
			collectionsQueue.Enqueue(this._spotChecks);
			collectionsQueue.Enqueue(this._spotCheckResults);
			collectionsQueue.Enqueue(this._vfsitemsSources);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._frequencyTests = (EntityCollection<FrequencyTestEntity>) collectionsQueue.Dequeue();
			this._frequencyTestResults = (EntityCollection<FrequencyTestResultEntity>) collectionsQueue.Dequeue();
			this._hwprofiles = (EntityCollection<HwProfileEntity>) collectionsQueue.Dequeue();
			this._invoices = (EntityCollection<InvoiceEntity>) collectionsQueue.Dequeue();
			this._shippingOrders = (EntityCollection<ShippingOrderEntity>) collectionsQueue.Dequeue();
			this._spotChecks = (EntityCollection<SpotCheckEntity>) collectionsQueue.Dequeue();
			this._spotCheckResults = (EntityCollection<SpotCheckResultEntity>) collectionsQueue.Dequeue();
			this._vfsitemsSources = (EntityCollection<VFSItemSourceEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._frequencyTests != null);
			toReturn |=(this._frequencyTestResults != null);
			toReturn |=(this._hwprofiles != null);
			toReturn |=(this._invoices != null);
			toReturn |=(this._shippingOrders != null);
			toReturn |=(this._spotChecks != null);
			toReturn |=(this._spotCheckResults != null);
			toReturn |=(this._vfsitemsSources != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<FrequencyTestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<FrequencyTestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<HwProfileEntity>(EntityFactoryCache2.GetEntityFactory(typeof(HwProfileEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<InvoiceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(InvoiceEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ShippingOrderEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<SpotCheckEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<SpotCheckResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<VFSItemSourceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSItemSourceEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("FrequencyTests", _frequencyTests);
			toReturn.Add("FrequencyTestResults", _frequencyTestResults);
			toReturn.Add("Hwprofiles", _hwprofiles);
			toReturn.Add("Invoices", _invoices);
			toReturn.Add("ShippingOrders", _shippingOrders);
			toReturn.Add("SpotChecks", _spotChecks);
			toReturn.Add("SpotCheckResults", _spotCheckResults);
			toReturn.Add("VfsitemsSources", _vfsitemsSources);
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
			_fieldsCustomProperties.Add("Name", fieldHashtable);
		}
		#endregion

		/// <summary> Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this UserEntity</param>
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
		public  static UserRelations Relations
		{
			get	{ return new UserRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'FrequencyTest' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathFrequencyTests
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<FrequencyTestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestEntityFactory))), (IEntityRelation)GetRelationsForField("FrequencyTests")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.FrequencyTestEntity, 0, null, null, null, null, "FrequencyTests", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'FrequencyTestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathFrequencyTestResults
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<FrequencyTestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestResultEntityFactory))), (IEntityRelation)GetRelationsForField("FrequencyTestResults")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.FrequencyTestResultEntity, 0, null, null, null, null, "FrequencyTestResults", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'HwProfile' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathHwprofiles
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<HwProfileEntity>(EntityFactoryCache2.GetEntityFactory(typeof(HwProfileEntityFactory))), (IEntityRelation)GetRelationsForField("Hwprofiles")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.HwProfileEntity, 0, null, null, null, null, "Hwprofiles", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Invoice' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathInvoices
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<InvoiceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(InvoiceEntityFactory))), (IEntityRelation)GetRelationsForField("Invoices")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.InvoiceEntity, 0, null, null, null, null, "Invoices", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ShippingOrder' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathShippingOrders
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ShippingOrderEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory))), (IEntityRelation)GetRelationsForField("ShippingOrders")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.ShippingOrderEntity, 0, null, null, null, null, "ShippingOrders", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheck' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotChecks
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<SpotCheckEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))), (IEntityRelation)GetRelationsForField("SpotChecks")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.SpotCheckEntity, 0, null, null, null, null, "SpotChecks", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheckResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotCheckResults
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<SpotCheckResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory))), (IEntityRelation)GetRelationsForField("SpotCheckResults")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, 0, null, null, null, null, "SpotCheckResults", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFSItemSource' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVfsitemsSources
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<VFSItemSourceEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSItemSourceEntityFactory))), (IEntityRelation)GetRelationsForField("VfsitemsSources")[0], (int)Vital.DataLayer.EntityType.UserEntity, (int)Vital.DataLayer.EntityType.VFSItemSourceEntity, 0, null, null, null, null, "VfsitemsSources", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
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

		/// <summary> The Id property of the Entity User<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Users"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)UserFieldIndex.Id, true); }
			set	{ SetValue((int)UserFieldIndex.Id, value); }
		}

		/// <summary> The Name property of the Entity User<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Users"."User_Name"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)UserFieldIndex.Name, true); }
			set	{ SetValue((int)UserFieldIndex.Name, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'FrequencyTestEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(FrequencyTestEntity))]
		public virtual EntityCollection<FrequencyTestEntity> FrequencyTests
		{
			get { return GetOrCreateEntityCollection<FrequencyTestEntity, FrequencyTestEntityFactory>("User", true, false, ref _frequencyTests);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'FrequencyTestResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(FrequencyTestResultEntity))]
		public virtual EntityCollection<FrequencyTestResultEntity> FrequencyTestResults
		{
			get { return GetOrCreateEntityCollection<FrequencyTestResultEntity, FrequencyTestResultEntityFactory>("User", true, false, ref _frequencyTestResults);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'HwProfileEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(HwProfileEntity))]
		public virtual EntityCollection<HwProfileEntity> Hwprofiles
		{
			get { return GetOrCreateEntityCollection<HwProfileEntity, HwProfileEntityFactory>("User", true, false, ref _hwprofiles);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'InvoiceEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(InvoiceEntity))]
		public virtual EntityCollection<InvoiceEntity> Invoices
		{
			get { return GetOrCreateEntityCollection<InvoiceEntity, InvoiceEntityFactory>("User", true, false, ref _invoices);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ShippingOrderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ShippingOrderEntity))]
		public virtual EntityCollection<ShippingOrderEntity> ShippingOrders
		{
			get { return GetOrCreateEntityCollection<ShippingOrderEntity, ShippingOrderEntityFactory>("User", true, false, ref _shippingOrders);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'SpotCheckEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SpotCheckEntity))]
		public virtual EntityCollection<SpotCheckEntity> SpotChecks
		{
			get { return GetOrCreateEntityCollection<SpotCheckEntity, SpotCheckEntityFactory>("User", true, false, ref _spotChecks);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'SpotCheckResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SpotCheckResultEntity))]
		public virtual EntityCollection<SpotCheckResultEntity> SpotCheckResults
		{
			get { return GetOrCreateEntityCollection<SpotCheckResultEntity, SpotCheckResultEntityFactory>("User", true, false, ref _spotCheckResults);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'VFSItemSourceEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VFSItemSourceEntity))]
		public virtual EntityCollection<VFSItemSourceEntity> VfsitemsSources
		{
			get { return GetOrCreateEntityCollection<VFSItemSourceEntity, VFSItemSourceEntityFactory>("User", true, false, ref _vfsitemsSources);	}
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
			get { return (int)Vital.DataLayer.EntityType.UserEntity; }
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
