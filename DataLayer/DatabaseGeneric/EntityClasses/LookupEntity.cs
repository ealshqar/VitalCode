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
	/// <summary>Entity class which represents the entity 'Lookup'.<br/><br/></summary>
	[Serializable]
	public partial class LookupEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<AutoItemEntity> _autoItems;
		private EntityCollection<PropertyEntity> _properties;
		private EntityCollection<PropertyEntity> _properties_;
		private EntityCollection<ShippingOrderEntity> _shippingOrders;
		private EntityCollection<SpotCheckResultEntity> _spotCheckResults;
		private EntityCollection<TestResultEntity> _testResult;
		private EntityCollection<TestScheduleEntity> _testSchedules_;
		private EntityCollection<TestScheduleEntity> _testSchedules;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		#endregion

		#region Statics
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name AutoItems</summary>
			public static readonly string AutoItems = "AutoItems";
			/// <summary>Member name Properties</summary>
			public static readonly string Properties = "Properties";
			/// <summary>Member name Properties_</summary>
			public static readonly string Properties_ = "Properties_";
			/// <summary>Member name ShippingOrders</summary>
			public static readonly string ShippingOrders = "ShippingOrders";
			/// <summary>Member name SpotCheckResults</summary>
			public static readonly string SpotCheckResults = "SpotCheckResults";
			/// <summary>Member name TestResult</summary>
			public static readonly string TestResult = "TestResult";
			/// <summary>Member name TestSchedules_</summary>
			public static readonly string TestSchedules_ = "TestSchedules_";
			/// <summary>Member name TestSchedules</summary>
			public static readonly string TestSchedules = "TestSchedules";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static LookupEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public LookupEntity():base("LookupEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public LookupEntity(IEntityFields2 fields):base("LookupEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this LookupEntity</param>
		public LookupEntity(IValidator validator):base("LookupEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for Lookup which data should be fetched into this Lookup object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public LookupEntity(System.Int32 id):base("LookupEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Lookup which data should be fetched into this Lookup object</param>
		/// <param name="validator">The custom validator object for this LookupEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public LookupEntity(System.Int32 id, IValidator validator):base("LookupEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected LookupEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_autoItems = (EntityCollection<AutoItemEntity>)info.GetValue("_autoItems", typeof(EntityCollection<AutoItemEntity>));
				_properties = (EntityCollection<PropertyEntity>)info.GetValue("_properties", typeof(EntityCollection<PropertyEntity>));
				_properties_ = (EntityCollection<PropertyEntity>)info.GetValue("_properties_", typeof(EntityCollection<PropertyEntity>));
				_shippingOrders = (EntityCollection<ShippingOrderEntity>)info.GetValue("_shippingOrders", typeof(EntityCollection<ShippingOrderEntity>));
				_spotCheckResults = (EntityCollection<SpotCheckResultEntity>)info.GetValue("_spotCheckResults", typeof(EntityCollection<SpotCheckResultEntity>));
				_testResult = (EntityCollection<TestResultEntity>)info.GetValue("_testResult", typeof(EntityCollection<TestResultEntity>));
				_testSchedules_ = (EntityCollection<TestScheduleEntity>)info.GetValue("_testSchedules_", typeof(EntityCollection<TestScheduleEntity>));
				_testSchedules = (EntityCollection<TestScheduleEntity>)info.GetValue("_testSchedules", typeof(EntityCollection<TestScheduleEntity>));
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
				case "AutoItems":
					this.AutoItems.Add((AutoItemEntity)entity);
					break;
				case "Properties":
					this.Properties.Add((PropertyEntity)entity);
					break;
				case "Properties_":
					this.Properties_.Add((PropertyEntity)entity);
					break;
				case "ShippingOrders":
					this.ShippingOrders.Add((ShippingOrderEntity)entity);
					break;
				case "SpotCheckResults":
					this.SpotCheckResults.Add((SpotCheckResultEntity)entity);
					break;
				case "TestResult":
					this.TestResult.Add((TestResultEntity)entity);
					break;
				case "TestSchedules_":
					this.TestSchedules_.Add((TestScheduleEntity)entity);
					break;
				case "TestSchedules":
					this.TestSchedules.Add((TestScheduleEntity)entity);
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
				case "AutoItems":
					toReturn.Add(Relations.AutoItemEntityUsingGenderLookupId);
					break;
				case "Properties":
					toReturn.Add(Relations.PropertyEntityUsingApplicableTypeLookupId);
					break;
				case "Properties_":
					toReturn.Add(Relations.PropertyEntityUsingValueTypeLookupId);
					break;
				case "ShippingOrders":
					toReturn.Add(Relations.ShippingOrderEntityUsingShippingMethodLookupId);
					break;
				case "SpotCheckResults":
					toReturn.Add(Relations.SpotCheckResultEntityUsingResultTypeId);
					break;
				case "TestResult":
					toReturn.Add(Relations.TestResultEntityUsingStepTypeLookupId);
					break;
				case "TestSchedules_":
					toReturn.Add(Relations.TestScheduleEntityUsingDiscountApplyLookupId);
					break;
				case "TestSchedules":
					toReturn.Add(Relations.TestScheduleEntityUsingEvalPeriodTypeLookupId);
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
				case "AutoItems":
					this.AutoItems.Add((AutoItemEntity)relatedEntity);
					break;
				case "Properties":
					this.Properties.Add((PropertyEntity)relatedEntity);
					break;
				case "Properties_":
					this.Properties_.Add((PropertyEntity)relatedEntity);
					break;
				case "ShippingOrders":
					this.ShippingOrders.Add((ShippingOrderEntity)relatedEntity);
					break;
				case "SpotCheckResults":
					this.SpotCheckResults.Add((SpotCheckResultEntity)relatedEntity);
					break;
				case "TestResult":
					this.TestResult.Add((TestResultEntity)relatedEntity);
					break;
				case "TestSchedules_":
					this.TestSchedules_.Add((TestScheduleEntity)relatedEntity);
					break;
				case "TestSchedules":
					this.TestSchedules.Add((TestScheduleEntity)relatedEntity);
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
				case "AutoItems":
					this.PerformRelatedEntityRemoval(this.AutoItems, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Properties":
					this.PerformRelatedEntityRemoval(this.Properties, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Properties_":
					this.PerformRelatedEntityRemoval(this.Properties_, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "ShippingOrders":
					this.PerformRelatedEntityRemoval(this.ShippingOrders, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "SpotCheckResults":
					this.PerformRelatedEntityRemoval(this.SpotCheckResults, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestResult":
					this.PerformRelatedEntityRemoval(this.TestResult, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestSchedules_":
					this.PerformRelatedEntityRemoval(this.TestSchedules_, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestSchedules":
					this.PerformRelatedEntityRemoval(this.TestSchedules, relatedEntity, signalRelatedEntityManyToOne);
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
			toReturn.Add(this.AutoItems);
			toReturn.Add(this.Properties);
			toReturn.Add(this.Properties_);
			toReturn.Add(this.ShippingOrders);
			toReturn.Add(this.SpotCheckResults);
			toReturn.Add(this.TestResult);
			toReturn.Add(this.TestSchedules_);
			toReturn.Add(this.TestSchedules);
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
				info.AddValue("_autoItems", ((_autoItems!=null) && (_autoItems.Count>0) && !this.MarkedForDeletion)?_autoItems:null);
				info.AddValue("_properties", ((_properties!=null) && (_properties.Count>0) && !this.MarkedForDeletion)?_properties:null);
				info.AddValue("_properties_", ((_properties_!=null) && (_properties_.Count>0) && !this.MarkedForDeletion)?_properties_:null);
				info.AddValue("_shippingOrders", ((_shippingOrders!=null) && (_shippingOrders.Count>0) && !this.MarkedForDeletion)?_shippingOrders:null);
				info.AddValue("_spotCheckResults", ((_spotCheckResults!=null) && (_spotCheckResults.Count>0) && !this.MarkedForDeletion)?_spotCheckResults:null);
				info.AddValue("_testResult", ((_testResult!=null) && (_testResult.Count>0) && !this.MarkedForDeletion)?_testResult:null);
				info.AddValue("_testSchedules_", ((_testSchedules_!=null) && (_testSchedules_.Count>0) && !this.MarkedForDeletion)?_testSchedules_:null);
				info.AddValue("_testSchedules", ((_testSchedules!=null) && (_testSchedules.Count>0) && !this.MarkedForDeletion)?_testSchedules:null);
			}
			// __LLBLGENPRO_USER_CODE_REGION_START GetObjectInfo
			// __LLBLGENPRO_USER_CODE_REGION_END
			base.GetObjectData(info, context);
		}


				
		/// <summary>Gets a list of all the EntityRelation objects the type of this instance has.</summary>
		/// <returns>A list of all the EntityRelation objects the type of this instance has. Hierarchy relations are excluded.</returns>
		protected override List<IEntityRelation> GetAllRelations()
		{
			return new LookupRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoItems()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoItemFields.GenderLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Property' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProperties()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(PropertyFields.ApplicableTypeLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Property' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProperties_()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(PropertyFields.ValueTypeLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ShippingOrder' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoShippingOrders()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ShippingOrderFields.ShippingMethodLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SpotCheckResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotCheckResults()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckResultFields.ResultTypeId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestResult()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestResultFields.StepTypeLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestSchedule' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestSchedules_()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestScheduleFields.DiscountApplyLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestSchedule' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestSchedules()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestScheduleFields.EvalPeriodTypeLookupId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}
		

		/// <summary>Creates a new instance of the factory related to this entity</summary>
		protected override IEntityFactory2 CreateEntityFactory()
		{
			return EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._autoItems);
			collectionsQueue.Enqueue(this._properties);
			collectionsQueue.Enqueue(this._properties_);
			collectionsQueue.Enqueue(this._shippingOrders);
			collectionsQueue.Enqueue(this._spotCheckResults);
			collectionsQueue.Enqueue(this._testResult);
			collectionsQueue.Enqueue(this._testSchedules_);
			collectionsQueue.Enqueue(this._testSchedules);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._autoItems = (EntityCollection<AutoItemEntity>) collectionsQueue.Dequeue();
			this._properties = (EntityCollection<PropertyEntity>) collectionsQueue.Dequeue();
			this._properties_ = (EntityCollection<PropertyEntity>) collectionsQueue.Dequeue();
			this._shippingOrders = (EntityCollection<ShippingOrderEntity>) collectionsQueue.Dequeue();
			this._spotCheckResults = (EntityCollection<SpotCheckResultEntity>) collectionsQueue.Dequeue();
			this._testResult = (EntityCollection<TestResultEntity>) collectionsQueue.Dequeue();
			this._testSchedules_ = (EntityCollection<TestScheduleEntity>) collectionsQueue.Dequeue();
			this._testSchedules = (EntityCollection<TestScheduleEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._autoItems != null);
			toReturn |=(this._properties != null);
			toReturn |=(this._properties_ != null);
			toReturn |=(this._shippingOrders != null);
			toReturn |=(this._spotCheckResults != null);
			toReturn |=(this._testResult != null);
			toReturn |=(this._testSchedules_ != null);
			toReturn |=(this._testSchedules != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<PropertyEntity>(EntityFactoryCache2.GetEntityFactory(typeof(PropertyEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<PropertyEntity>(EntityFactoryCache2.GetEntityFactory(typeof(PropertyEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ShippingOrderEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<SpotCheckResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestScheduleEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestScheduleEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestScheduleEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestScheduleEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("AutoItems", _autoItems);
			toReturn.Add("Properties", _properties);
			toReturn.Add("Properties_", _properties_);
			toReturn.Add("ShippingOrders", _shippingOrders);
			toReturn.Add("SpotCheckResults", _spotCheckResults);
			toReturn.Add("TestResult", _testResult);
			toReturn.Add("TestSchedules_", _testSchedules_);
			toReturn.Add("TestSchedules", _testSchedules);
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
			_fieldsCustomProperties.Add("Value", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Type", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Key", fieldHashtable);
		}
		#endregion

		/// <summary> Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this LookupEntity</param>
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
		public  static LookupRelations Relations
		{
			get	{ return new LookupRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoItems
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemEntityFactory))), (IEntityRelation)GetRelationsForField("AutoItems")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.AutoItemEntity, 0, null, null, null, null, "AutoItems", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Property' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProperties
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<PropertyEntity>(EntityFactoryCache2.GetEntityFactory(typeof(PropertyEntityFactory))), (IEntityRelation)GetRelationsForField("Properties")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.PropertyEntity, 0, null, null, null, null, "Properties", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Property' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProperties_
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<PropertyEntity>(EntityFactoryCache2.GetEntityFactory(typeof(PropertyEntityFactory))), (IEntityRelation)GetRelationsForField("Properties_")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.PropertyEntity, 0, null, null, null, null, "Properties_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ShippingOrder' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathShippingOrders
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ShippingOrderEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory))), (IEntityRelation)GetRelationsForField("ShippingOrders")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.ShippingOrderEntity, 0, null, null, null, null, "ShippingOrders", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheckResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotCheckResults
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<SpotCheckResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory))), (IEntityRelation)GetRelationsForField("SpotCheckResults")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, 0, null, null, null, null, "SpotCheckResults", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestResult
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory))), (IEntityRelation)GetRelationsForField("TestResult")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.TestResultEntity, 0, null, null, null, null, "TestResult", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestSchedule' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestSchedules_
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestScheduleEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestScheduleEntityFactory))), (IEntityRelation)GetRelationsForField("TestSchedules_")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.TestScheduleEntity, 0, null, null, null, null, "TestSchedules_", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestSchedule' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestSchedules
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestScheduleEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestScheduleEntityFactory))), (IEntityRelation)GetRelationsForField("TestSchedules")[0], (int)Vital.DataLayer.EntityType.LookupEntity, (int)Vital.DataLayer.EntityType.TestScheduleEntity, 0, null, null, null, null, "TestSchedules", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
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

		/// <summary> The Id property of the Entity Lookup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Lookups"."Lookup_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)LookupFieldIndex.Id, true); }
			set	{ SetValue((int)LookupFieldIndex.Id, value); }
		}

		/// <summary> The Value property of the Entity Lookup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Lookups"."Lookup_Value"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Value
		{
			get { return (System.String)GetValue((int)LookupFieldIndex.Value, true); }
			set	{ SetValue((int)LookupFieldIndex.Value, value); }
		}

		/// <summary> The Type property of the Entity Lookup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Lookups"."Lookup_Type"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Type
		{
			get { return (System.String)GetValue((int)LookupFieldIndex.Type, true); }
			set	{ SetValue((int)LookupFieldIndex.Type, value); }
		}

		/// <summary> The Key property of the Entity Lookup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Lookups"."Lookup_Key"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 250<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Key
		{
			get { return (System.String)GetValue((int)LookupFieldIndex.Key, true); }
			set	{ SetValue((int)LookupFieldIndex.Key, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoItemEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoItemEntity))]
		public virtual EntityCollection<AutoItemEntity> AutoItems
		{
			get { return GetOrCreateEntityCollection<AutoItemEntity, AutoItemEntityFactory>("Gender", true, false, ref _autoItems);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'PropertyEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(PropertyEntity))]
		public virtual EntityCollection<PropertyEntity> Properties
		{
			get { return GetOrCreateEntityCollection<PropertyEntity, PropertyEntityFactory>("ApplicableTypeLookup", true, false, ref _properties);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'PropertyEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(PropertyEntity))]
		public virtual EntityCollection<PropertyEntity> Properties_
		{
			get { return GetOrCreateEntityCollection<PropertyEntity, PropertyEntityFactory>("ValueTypeLookup", true, false, ref _properties_);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ShippingOrderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ShippingOrderEntity))]
		public virtual EntityCollection<ShippingOrderEntity> ShippingOrders
		{
			get { return GetOrCreateEntityCollection<ShippingOrderEntity, ShippingOrderEntityFactory>("ShippingMethod", true, false, ref _shippingOrders);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'SpotCheckResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SpotCheckResultEntity))]
		public virtual EntityCollection<SpotCheckResultEntity> SpotCheckResults
		{
			get { return GetOrCreateEntityCollection<SpotCheckResultEntity, SpotCheckResultEntityFactory>("ResultType", true, false, ref _spotCheckResults);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestResultEntity))]
		public virtual EntityCollection<TestResultEntity> TestResult
		{
			get { return GetOrCreateEntityCollection<TestResultEntity, TestResultEntityFactory>("StepType", true, false, ref _testResult);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestScheduleEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestScheduleEntity))]
		public virtual EntityCollection<TestScheduleEntity> TestSchedules_
		{
			get { return GetOrCreateEntityCollection<TestScheduleEntity, TestScheduleEntityFactory>("DiscountApply", true, false, ref _testSchedules_);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestScheduleEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestScheduleEntity))]
		public virtual EntityCollection<TestScheduleEntity> TestSchedules
		{
			get { return GetOrCreateEntityCollection<TestScheduleEntity, TestScheduleEntityFactory>("EvalPeriodType", true, false, ref _testSchedules);	}
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
			get { return (int)Vital.DataLayer.EntityType.LookupEntity; }
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
