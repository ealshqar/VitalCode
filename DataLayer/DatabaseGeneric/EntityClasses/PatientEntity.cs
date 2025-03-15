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
	/// <summary>Entity class which represents the entity 'Patient'.<br/><br/></summary>
	[Serializable]
	public partial class PatientEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<AutoTestEntity> _autoTests;
		private EntityCollection<FrequencyTestEntity> _frequencyTests;
		private EntityCollection<PatientHistoryEntity> _patientHistory;
		private EntityCollection<SpotCheckEntity> _spotChecks;
		private EntityCollection<TestEntity> _tests;
		private EntityCollection<VFSEntity> _vFSRecords;
		private LookupEntity _lookup;
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
			/// <summary>Member name Lookup</summary>
			public static readonly string Lookup = "Lookup";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name AutoTests</summary>
			public static readonly string AutoTests = "AutoTests";
			/// <summary>Member name FrequencyTests</summary>
			public static readonly string FrequencyTests = "FrequencyTests";
			/// <summary>Member name PatientHistory</summary>
			public static readonly string PatientHistory = "PatientHistory";
			/// <summary>Member name SpotChecks</summary>
			public static readonly string SpotChecks = "SpotChecks";
			/// <summary>Member name Tests</summary>
			public static readonly string Tests = "Tests";
			/// <summary>Member name VFSRecords</summary>
			public static readonly string VFSRecords = "VFSRecords";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static PatientEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public PatientEntity():base("PatientEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public PatientEntity(IEntityFields2 fields):base("PatientEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this PatientEntity</param>
		public PatientEntity(IValidator validator):base("PatientEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for Patient which data should be fetched into this Patient object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public PatientEntity(System.Int32 id):base("PatientEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Patient which data should be fetched into this Patient object</param>
		/// <param name="validator">The custom validator object for this PatientEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public PatientEntity(System.Int32 id, IValidator validator):base("PatientEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected PatientEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_autoTests = (EntityCollection<AutoTestEntity>)info.GetValue("_autoTests", typeof(EntityCollection<AutoTestEntity>));
				_frequencyTests = (EntityCollection<FrequencyTestEntity>)info.GetValue("_frequencyTests", typeof(EntityCollection<FrequencyTestEntity>));
				_patientHistory = (EntityCollection<PatientHistoryEntity>)info.GetValue("_patientHistory", typeof(EntityCollection<PatientHistoryEntity>));
				_spotChecks = (EntityCollection<SpotCheckEntity>)info.GetValue("_spotChecks", typeof(EntityCollection<SpotCheckEntity>));
				_tests = (EntityCollection<TestEntity>)info.GetValue("_tests", typeof(EntityCollection<TestEntity>));
				_vFSRecords = (EntityCollection<VFSEntity>)info.GetValue("_vFSRecords", typeof(EntityCollection<VFSEntity>));
				_lookup = (LookupEntity)info.GetValue("_lookup", typeof(LookupEntity));
				if(_lookup!=null)
				{
					_lookup.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((PatientFieldIndex)fieldIndex)
			{
				case PatientFieldIndex.GenderLookupId:
					DesetupSyncLookup(true, false);
					break;
				case PatientFieldIndex.UserId:
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
				case "Lookup":
					this.Lookup = (LookupEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "AutoTests":
					this.AutoTests.Add((AutoTestEntity)entity);
					break;
				case "FrequencyTests":
					this.FrequencyTests.Add((FrequencyTestEntity)entity);
					break;
				case "PatientHistory":
					this.PatientHistory.Add((PatientHistoryEntity)entity);
					break;
				case "SpotChecks":
					this.SpotChecks.Add((SpotCheckEntity)entity);
					break;
				case "Tests":
					this.Tests.Add((TestEntity)entity);
					break;
				case "VFSRecords":
					this.VFSRecords.Add((VFSEntity)entity);
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
				case "Lookup":
					toReturn.Add(Relations.LookupEntityUsingGenderLookupId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "AutoTests":
					toReturn.Add(Relations.AutoTestEntityUsingPatientId);
					break;
				case "FrequencyTests":
					toReturn.Add(Relations.FrequencyTestEntityUsingPatientId);
					break;
				case "PatientHistory":
					toReturn.Add(Relations.PatientHistoryEntityUsingPatientId);
					break;
				case "SpotChecks":
					toReturn.Add(Relations.SpotCheckEntityUsingPatientId);
					break;
				case "Tests":
					toReturn.Add(Relations.TestEntityUsingPatientId);
					break;
				case "VFSRecords":
					toReturn.Add(Relations.VFSEntityUsingPatientId);
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
			int numberOfOneWayRelations = 0+1+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
				case "Lookup":
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
				case "Lookup":
					SetupSyncLookup(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "AutoTests":
					this.AutoTests.Add((AutoTestEntity)relatedEntity);
					break;
				case "FrequencyTests":
					this.FrequencyTests.Add((FrequencyTestEntity)relatedEntity);
					break;
				case "PatientHistory":
					this.PatientHistory.Add((PatientHistoryEntity)relatedEntity);
					break;
				case "SpotChecks":
					this.SpotChecks.Add((SpotCheckEntity)relatedEntity);
					break;
				case "Tests":
					this.Tests.Add((TestEntity)relatedEntity);
					break;
				case "VFSRecords":
					this.VFSRecords.Add((VFSEntity)relatedEntity);
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
				case "Lookup":
					DesetupSyncLookup(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "AutoTests":
					this.PerformRelatedEntityRemoval(this.AutoTests, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "FrequencyTests":
					this.PerformRelatedEntityRemoval(this.FrequencyTests, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "PatientHistory":
					this.PerformRelatedEntityRemoval(this.PatientHistory, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "SpotChecks":
					this.PerformRelatedEntityRemoval(this.SpotChecks, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Tests":
					this.PerformRelatedEntityRemoval(this.Tests, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "VFSRecords":
					this.PerformRelatedEntityRemoval(this.VFSRecords, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_lookup!=null)
			{
				toReturn.Add(_lookup);
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
			toReturn.Add(this.AutoTests);
			toReturn.Add(this.FrequencyTests);
			toReturn.Add(this.PatientHistory);
			toReturn.Add(this.SpotChecks);
			toReturn.Add(this.Tests);
			toReturn.Add(this.VFSRecords);
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
				info.AddValue("_autoTests", ((_autoTests!=null) && (_autoTests.Count>0) && !this.MarkedForDeletion)?_autoTests:null);
				info.AddValue("_frequencyTests", ((_frequencyTests!=null) && (_frequencyTests.Count>0) && !this.MarkedForDeletion)?_frequencyTests:null);
				info.AddValue("_patientHistory", ((_patientHistory!=null) && (_patientHistory.Count>0) && !this.MarkedForDeletion)?_patientHistory:null);
				info.AddValue("_spotChecks", ((_spotChecks!=null) && (_spotChecks.Count>0) && !this.MarkedForDeletion)?_spotChecks:null);
				info.AddValue("_tests", ((_tests!=null) && (_tests.Count>0) && !this.MarkedForDeletion)?_tests:null);
				info.AddValue("_vFSRecords", ((_vFSRecords!=null) && (_vFSRecords.Count>0) && !this.MarkedForDeletion)?_vFSRecords:null);
				info.AddValue("_lookup", (!this.MarkedForDeletion?_lookup:null));
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
			return new PatientRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoTest' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTests()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestFields.PatientId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'FrequencyTest' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoFrequencyTests()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(FrequencyTestFields.PatientId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'PatientHistory' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoPatientHistory()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(PatientHistoryFields.PatientId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SpotCheck' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotChecks()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckFields.PatientId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Test' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTests()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestFields.PatientId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VFS' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVFSRecords()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSFields.PatientId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.GenderLookupId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(PatientEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._autoTests);
			collectionsQueue.Enqueue(this._frequencyTests);
			collectionsQueue.Enqueue(this._patientHistory);
			collectionsQueue.Enqueue(this._spotChecks);
			collectionsQueue.Enqueue(this._tests);
			collectionsQueue.Enqueue(this._vFSRecords);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._autoTests = (EntityCollection<AutoTestEntity>) collectionsQueue.Dequeue();
			this._frequencyTests = (EntityCollection<FrequencyTestEntity>) collectionsQueue.Dequeue();
			this._patientHistory = (EntityCollection<PatientHistoryEntity>) collectionsQueue.Dequeue();
			this._spotChecks = (EntityCollection<SpotCheckEntity>) collectionsQueue.Dequeue();
			this._tests = (EntityCollection<TestEntity>) collectionsQueue.Dequeue();
			this._vFSRecords = (EntityCollection<VFSEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._autoTests != null);
			toReturn |=(this._frequencyTests != null);
			toReturn |=(this._patientHistory != null);
			toReturn |=(this._spotChecks != null);
			toReturn |=(this._tests != null);
			toReturn |=(this._vFSRecords != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoTestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<FrequencyTestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<PatientHistoryEntity>(EntityFactoryCache2.GetEntityFactory(typeof(PatientHistoryEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<SpotCheckEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<VFSEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Lookup", _lookup);
			toReturn.Add("User", _user);
			toReturn.Add("AutoTests", _autoTests);
			toReturn.Add("FrequencyTests", _frequencyTests);
			toReturn.Add("PatientHistory", _patientHistory);
			toReturn.Add("SpotChecks", _spotChecks);
			toReturn.Add("Tests", _tests);
			toReturn.Add("VFSRecords", _vFSRecords);
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
			_fieldsCustomProperties.Add("Number", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("FirstName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("LastName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Address1", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Address2", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("City", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("State", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Zip", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("GenderLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("DateOfBirth", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("HomePhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("WorkPhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CellPhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Fax", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Email", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Notes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _lookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _lookup, new PropertyChangedEventHandler( OnLookupPropertyChanged ), "Lookup", Vital.DataLayer.RelationClasses.StaticPatientRelations.LookupEntityUsingGenderLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)PatientFieldIndex.GenderLookupId } );
			_lookup = null;
		}

		/// <summary> setups the sync logic for member _lookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncLookup(IEntityCore relatedEntity)
		{
			if(_lookup!=relatedEntity)
			{
				DesetupSyncLookup(true, true);
				_lookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _lookup, new PropertyChangedEventHandler( OnLookupPropertyChanged ), "Lookup", Vital.DataLayer.RelationClasses.StaticPatientRelations.LookupEntityUsingGenderLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticPatientRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)PatientFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticPatientRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this PatientEntity</param>
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
		public  static PatientRelations Relations
		{
			get	{ return new PatientRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTest' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTests
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoTestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestEntityFactory))), (IEntityRelation)GetRelationsForField("AutoTests")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.AutoTestEntity, 0, null, null, null, null, "AutoTests", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'FrequencyTest' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathFrequencyTests
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<FrequencyTestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestEntityFactory))), (IEntityRelation)GetRelationsForField("FrequencyTests")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.FrequencyTestEntity, 0, null, null, null, null, "FrequencyTests", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'PatientHistory' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathPatientHistory
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<PatientHistoryEntity>(EntityFactoryCache2.GetEntityFactory(typeof(PatientHistoryEntityFactory))), (IEntityRelation)GetRelationsForField("PatientHistory")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.PatientHistoryEntity, 0, null, null, null, null, "PatientHistory", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheck' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotChecks
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<SpotCheckEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))), (IEntityRelation)GetRelationsForField("SpotChecks")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.SpotCheckEntity, 0, null, null, null, null, "SpotChecks", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Test' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTests
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestEntityFactory))), (IEntityRelation)GetRelationsForField("Tests")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.TestEntity, 0, null, null, null, null, "Tests", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFS' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVFSRecords
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<VFSEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSEntityFactory))), (IEntityRelation)GetRelationsForField("VFSRecords")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.VFSEntity, 0, null, null, null, null, "VFSRecords", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("Lookup")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "Lookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.PatientEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)PatientFieldIndex.Id, true); }
			set	{ SetValue((int)PatientFieldIndex.Id, value); }
		}

		/// <summary> The Number property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Number"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Number
		{
			get { return (Nullable<System.Int32>)GetValue((int)PatientFieldIndex.Number, false); }
			set	{ SetValue((int)PatientFieldIndex.Number, value); }
		}

		/// <summary> The FirstName property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_FirstName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String FirstName
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.FirstName, true); }
			set	{ SetValue((int)PatientFieldIndex.FirstName, value); }
		}

		/// <summary> The LastName property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_LastName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String LastName
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.LastName, true); }
			set	{ SetValue((int)PatientFieldIndex.LastName, value); }
		}

		/// <summary> The Address1 property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Address1"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Address1
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.Address1, true); }
			set	{ SetValue((int)PatientFieldIndex.Address1, value); }
		}

		/// <summary> The Address2 property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Address2"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Address2
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.Address2, true); }
			set	{ SetValue((int)PatientFieldIndex.Address2, value); }
		}

		/// <summary> The City property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_City"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String City
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.City, true); }
			set	{ SetValue((int)PatientFieldIndex.City, value); }
		}

		/// <summary> The State property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_State"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String State
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.State, true); }
			set	{ SetValue((int)PatientFieldIndex.State, value); }
		}

		/// <summary> The Zip property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Zip"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 10<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Zip
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.Zip, true); }
			set	{ SetValue((int)PatientFieldIndex.Zip, value); }
		}

		/// <summary> The GenderLookupId property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Gender_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 GenderLookupId
		{
			get { return (System.Int32)GetValue((int)PatientFieldIndex.GenderLookupId, true); }
			set	{ SetValue((int)PatientFieldIndex.GenderLookupId, value); }
		}

		/// <summary> The DateOfBirth property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_DateOfBirth"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> DateOfBirth
		{
			get { return (Nullable<System.DateTime>)GetValue((int)PatientFieldIndex.DateOfBirth, false); }
			set	{ SetValue((int)PatientFieldIndex.DateOfBirth, value); }
		}

		/// <summary> The HomePhone property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_HomePhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String HomePhone
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.HomePhone, true); }
			set	{ SetValue((int)PatientFieldIndex.HomePhone, value); }
		}

		/// <summary> The WorkPhone property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_WorkPhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String WorkPhone
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.WorkPhone, true); }
			set	{ SetValue((int)PatientFieldIndex.WorkPhone, value); }
		}

		/// <summary> The CellPhone property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_CellPhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String CellPhone
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.CellPhone, true); }
			set	{ SetValue((int)PatientFieldIndex.CellPhone, value); }
		}

		/// <summary> The Fax property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Fax"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Fax
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.Fax, true); }
			set	{ SetValue((int)PatientFieldIndex.Fax, value); }
		}

		/// <summary> The Email property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Email"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Email
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.Email, true); }
			set	{ SetValue((int)PatientFieldIndex.Email, value); }
		}

		/// <summary> The Notes property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_Notes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Notes
		{
			get { return (System.String)GetValue((int)PatientFieldIndex.Notes, true); }
			set	{ SetValue((int)PatientFieldIndex.Notes, value); }
		}

		/// <summary> The UserId property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)PatientFieldIndex.UserId, true); }
			set	{ SetValue((int)PatientFieldIndex.UserId, value); }
		}

		/// <summary> The CreationDateTime property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)PatientFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)PatientFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity Patient<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Patients"."Patient_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)PatientFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)PatientFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoTestEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoTestEntity))]
		public virtual EntityCollection<AutoTestEntity> AutoTests
		{
			get { return GetOrCreateEntityCollection<AutoTestEntity, AutoTestEntityFactory>("Patient", true, false, ref _autoTests);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'FrequencyTestEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(FrequencyTestEntity))]
		public virtual EntityCollection<FrequencyTestEntity> FrequencyTests
		{
			get { return GetOrCreateEntityCollection<FrequencyTestEntity, FrequencyTestEntityFactory>("Patient", true, false, ref _frequencyTests);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'PatientHistoryEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(PatientHistoryEntity))]
		public virtual EntityCollection<PatientHistoryEntity> PatientHistory
		{
			get { return GetOrCreateEntityCollection<PatientHistoryEntity, PatientHistoryEntityFactory>("Patient", true, false, ref _patientHistory);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'SpotCheckEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SpotCheckEntity))]
		public virtual EntityCollection<SpotCheckEntity> SpotChecks
		{
			get { return GetOrCreateEntityCollection<SpotCheckEntity, SpotCheckEntityFactory>("Patient", true, false, ref _spotChecks);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestEntity))]
		public virtual EntityCollection<TestEntity> Tests
		{
			get { return GetOrCreateEntityCollection<TestEntity, TestEntityFactory>("Patient", true, false, ref _tests);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'VFSEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VFSEntity))]
		public virtual EntityCollection<VFSEntity> VFSRecords
		{
			get { return GetOrCreateEntityCollection<VFSEntity, VFSEntityFactory>("Patient", true, false, ref _vFSRecords);	}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity Lookup
		{
			get	{ return _lookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "Lookup", _lookup, false); 
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
			get { return (int)Vital.DataLayer.EntityType.PatientEntity; }
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
