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
	/// <summary>Entity class which represents the entity 'AutoTestResult'.<br/><br/></summary>
	[Serializable]
	public partial class AutoTestResultEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<AutoTestResultEntity> _autoTestResultChildes;
		private EntityCollection<AutoTestResultProductEntity> _autoTestResultProducts;
		private AutoItemEntity _autoItem;
		private AutoProtocolStageRevisionEntity _autoProtocolStageRevision;
		private AutoTestEntity _autoTest;
		private AutoTestResultEntity _autoTestResultParent;
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
			/// <summary>Member name AutoItem</summary>
			public static readonly string AutoItem = "AutoItem";
			/// <summary>Member name AutoProtocolStageRevision</summary>
			public static readonly string AutoProtocolStageRevision = "AutoProtocolStageRevision";
			/// <summary>Member name AutoTest</summary>
			public static readonly string AutoTest = "AutoTest";
			/// <summary>Member name AutoTestResultParent</summary>
			public static readonly string AutoTestResultParent = "AutoTestResultParent";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name AutoTestResultChildes</summary>
			public static readonly string AutoTestResultChildes = "AutoTestResultChildes";
			/// <summary>Member name AutoTestResultProducts</summary>
			public static readonly string AutoTestResultProducts = "AutoTestResultProducts";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static AutoTestResultEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public AutoTestResultEntity():base("AutoTestResultEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public AutoTestResultEntity(IEntityFields2 fields):base("AutoTestResultEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this AutoTestResultEntity</param>
		public AutoTestResultEntity(IValidator validator):base("AutoTestResultEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoTestResult which data should be fetched into this AutoTestResult object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoTestResultEntity(System.Int32 id):base("AutoTestResultEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoTestResult which data should be fetched into this AutoTestResult object</param>
		/// <param name="validator">The custom validator object for this AutoTestResultEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoTestResultEntity(System.Int32 id, IValidator validator):base("AutoTestResultEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected AutoTestResultEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_autoTestResultChildes = (EntityCollection<AutoTestResultEntity>)info.GetValue("_autoTestResultChildes", typeof(EntityCollection<AutoTestResultEntity>));
				_autoTestResultProducts = (EntityCollection<AutoTestResultProductEntity>)info.GetValue("_autoTestResultProducts", typeof(EntityCollection<AutoTestResultProductEntity>));
				_autoItem = (AutoItemEntity)info.GetValue("_autoItem", typeof(AutoItemEntity));
				if(_autoItem!=null)
				{
					_autoItem.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_autoProtocolStageRevision = (AutoProtocolStageRevisionEntity)info.GetValue("_autoProtocolStageRevision", typeof(AutoProtocolStageRevisionEntity));
				if(_autoProtocolStageRevision!=null)
				{
					_autoProtocolStageRevision.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_autoTest = (AutoTestEntity)info.GetValue("_autoTest", typeof(AutoTestEntity));
				if(_autoTest!=null)
				{
					_autoTest.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_autoTestResultParent = (AutoTestResultEntity)info.GetValue("_autoTestResultParent", typeof(AutoTestResultEntity));
				if(_autoTestResultParent!=null)
				{
					_autoTestResultParent.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((AutoTestResultFieldIndex)fieldIndex)
			{
				case AutoTestResultFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case AutoTestResultFieldIndex.AutoTestsId:
					DesetupSyncAutoTest(true, false);
					break;
				case AutoTestResultFieldIndex.AutoItemsId:
					DesetupSyncAutoItem(true, false);
					break;
				case AutoTestResultFieldIndex.AutoProtocolStageRevisionsId:
					DesetupSyncAutoProtocolStageRevision(true, false);
					break;
				case AutoTestResultFieldIndex.AutoTestResultsParentId:
					DesetupSyncAutoTestResultParent(true, false);
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
				case "AutoItem":
					this.AutoItem = (AutoItemEntity)entity;
					break;
				case "AutoProtocolStageRevision":
					this.AutoProtocolStageRevision = (AutoProtocolStageRevisionEntity)entity;
					break;
				case "AutoTest":
					this.AutoTest = (AutoTestEntity)entity;
					break;
				case "AutoTestResultParent":
					this.AutoTestResultParent = (AutoTestResultEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "AutoTestResultChildes":
					this.AutoTestResultChildes.Add((AutoTestResultEntity)entity);
					break;
				case "AutoTestResultProducts":
					this.AutoTestResultProducts.Add((AutoTestResultProductEntity)entity);
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
				case "AutoItem":
					toReturn.Add(Relations.AutoItemEntityUsingAutoItemsId);
					break;
				case "AutoProtocolStageRevision":
					toReturn.Add(Relations.AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsId);
					break;
				case "AutoTest":
					toReturn.Add(Relations.AutoTestEntityUsingAutoTestsId);
					break;
				case "AutoTestResultParent":
					toReturn.Add(Relations.AutoTestResultEntityUsingIdAutoTestResultsParentId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "AutoTestResultChildes":
					toReturn.Add(Relations.AutoTestResultEntityUsingAutoTestResultsParentId);
					break;
				case "AutoTestResultProducts":
					toReturn.Add(Relations.AutoTestResultProductEntityUsingAutoTestResultsId);
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
			int numberOfOneWayRelations = 0+1+1+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
				case "AutoItem":
					return true;
				case "AutoProtocolStageRevision":
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
				case "AutoItem":
					SetupSyncAutoItem(relatedEntity);
					break;
				case "AutoProtocolStageRevision":
					SetupSyncAutoProtocolStageRevision(relatedEntity);
					break;
				case "AutoTest":
					SetupSyncAutoTest(relatedEntity);
					break;
				case "AutoTestResultParent":
					SetupSyncAutoTestResultParent(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "AutoTestResultChildes":
					this.AutoTestResultChildes.Add((AutoTestResultEntity)relatedEntity);
					break;
				case "AutoTestResultProducts":
					this.AutoTestResultProducts.Add((AutoTestResultProductEntity)relatedEntity);
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
				case "AutoItem":
					DesetupSyncAutoItem(false, true);
					break;
				case "AutoProtocolStageRevision":
					DesetupSyncAutoProtocolStageRevision(false, true);
					break;
				case "AutoTest":
					DesetupSyncAutoTest(false, true);
					break;
				case "AutoTestResultParent":
					DesetupSyncAutoTestResultParent(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "AutoTestResultChildes":
					this.PerformRelatedEntityRemoval(this.AutoTestResultChildes, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "AutoTestResultProducts":
					this.PerformRelatedEntityRemoval(this.AutoTestResultProducts, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_autoItem!=null)
			{
				toReturn.Add(_autoItem);
			}
			if(_autoProtocolStageRevision!=null)
			{
				toReturn.Add(_autoProtocolStageRevision);
			}
			if(_autoTest!=null)
			{
				toReturn.Add(_autoTest);
			}
			if(_autoTestResultParent!=null)
			{
				toReturn.Add(_autoTestResultParent);
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
			toReturn.Add(this.AutoTestResultChildes);
			toReturn.Add(this.AutoTestResultProducts);
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
				info.AddValue("_autoTestResultChildes", ((_autoTestResultChildes!=null) && (_autoTestResultChildes.Count>0) && !this.MarkedForDeletion)?_autoTestResultChildes:null);
				info.AddValue("_autoTestResultProducts", ((_autoTestResultProducts!=null) && (_autoTestResultProducts.Count>0) && !this.MarkedForDeletion)?_autoTestResultProducts:null);
				info.AddValue("_autoItem", (!this.MarkedForDeletion?_autoItem:null));
				info.AddValue("_autoProtocolStageRevision", (!this.MarkedForDeletion?_autoProtocolStageRevision:null));
				info.AddValue("_autoTest", (!this.MarkedForDeletion?_autoTest:null));
				info.AddValue("_autoTestResultParent", (!this.MarkedForDeletion?_autoTestResultParent:null));
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
			return new AutoTestResultRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoTestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTestResultChildes()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestResultFields.AutoTestResultsParentId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoTestResultProduct' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTestResultProducts()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestResultProductFields.AutoTestResultsId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoItem()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoItemFields.Id, null, ComparisonOperator.Equal, this.AutoItemsId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoProtocolStageRevision' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoProtocolStageRevision()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoProtocolStageRevisionFields.Id, null, ComparisonOperator.Equal, this.AutoProtocolStageRevisionsId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoTest' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTest()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestFields.Id, null, ComparisonOperator.Equal, this.AutoTestsId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoTestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTestResultParent()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestResultFields.Id, null, ComparisonOperator.Equal, this.AutoTestResultsParentId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._autoTestResultChildes);
			collectionsQueue.Enqueue(this._autoTestResultProducts);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._autoTestResultChildes = (EntityCollection<AutoTestResultEntity>) collectionsQueue.Dequeue();
			this._autoTestResultProducts = (EntityCollection<AutoTestResultProductEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._autoTestResultChildes != null);
			toReturn |=(this._autoTestResultProducts != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoTestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoTestResultProductEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultProductEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("AutoItem", _autoItem);
			toReturn.Add("AutoProtocolStageRevision", _autoProtocolStageRevision);
			toReturn.Add("AutoTest", _autoTest);
			toReturn.Add("AutoTestResultParent", _autoTestResultParent);
			toReturn.Add("User", _user);
			toReturn.Add("AutoTestResultChildes", _autoTestResultChildes);
			toReturn.Add("AutoTestResultProducts", _autoTestResultProducts);
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
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AutoTestsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AutoItemsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AutoProtocolStageRevisionsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PreliminaryReading", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SummaryReading", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsAddedManually", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Notes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AutoTestResultsParentId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _autoItem</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoItem, new PropertyChangedEventHandler( OnAutoItemPropertyChanged ), "AutoItem", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoItemEntityUsingAutoItemsIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoTestResultFieldIndex.AutoItemsId } );
			_autoItem = null;
		}

		/// <summary> setups the sync logic for member _autoItem</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoItem(IEntityCore relatedEntity)
		{
			if(_autoItem!=relatedEntity)
			{
				DesetupSyncAutoItem(true, true);
				_autoItem = (AutoItemEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoItem, new PropertyChangedEventHandler( OnAutoItemPropertyChanged ), "AutoItem", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoItemEntityUsingAutoItemsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoItemPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _autoProtocolStageRevision</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoProtocolStageRevision(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoProtocolStageRevision, new PropertyChangedEventHandler( OnAutoProtocolStageRevisionPropertyChanged ), "AutoProtocolStageRevision", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoTestResultFieldIndex.AutoProtocolStageRevisionsId } );
			_autoProtocolStageRevision = null;
		}

		/// <summary> setups the sync logic for member _autoProtocolStageRevision</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoProtocolStageRevision(IEntityCore relatedEntity)
		{
			if(_autoProtocolStageRevision!=relatedEntity)
			{
				DesetupSyncAutoProtocolStageRevision(true, true);
				_autoProtocolStageRevision = (AutoProtocolStageRevisionEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoProtocolStageRevision, new PropertyChangedEventHandler( OnAutoProtocolStageRevisionPropertyChanged ), "AutoProtocolStageRevision", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoProtocolStageRevisionEntityUsingAutoProtocolStageRevisionsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoProtocolStageRevisionPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _autoTest</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoTest(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoTest, new PropertyChangedEventHandler( OnAutoTestPropertyChanged ), "AutoTest", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoTestEntityUsingAutoTestsIdStatic, true, signalRelatedEntity, "AutoTestResults", resetFKFields, new int[] { (int)AutoTestResultFieldIndex.AutoTestsId } );
			_autoTest = null;
		}

		/// <summary> setups the sync logic for member _autoTest</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoTest(IEntityCore relatedEntity)
		{
			if(_autoTest!=relatedEntity)
			{
				DesetupSyncAutoTest(true, true);
				_autoTest = (AutoTestEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoTest, new PropertyChangedEventHandler( OnAutoTestPropertyChanged ), "AutoTest", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoTestEntityUsingAutoTestsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoTestPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _autoTestResultParent</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoTestResultParent(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoTestResultParent, new PropertyChangedEventHandler( OnAutoTestResultParentPropertyChanged ), "AutoTestResultParent", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoTestResultEntityUsingIdAutoTestResultsParentIdStatic, true, signalRelatedEntity, "AutoTestResultChildes", resetFKFields, new int[] { (int)AutoTestResultFieldIndex.AutoTestResultsParentId } );
			_autoTestResultParent = null;
		}

		/// <summary> setups the sync logic for member _autoTestResultParent</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoTestResultParent(IEntityCore relatedEntity)
		{
			if(_autoTestResultParent!=relatedEntity)
			{
				DesetupSyncAutoTestResultParent(true, true);
				_autoTestResultParent = (AutoTestResultEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoTestResultParent, new PropertyChangedEventHandler( OnAutoTestResultParentPropertyChanged ), "AutoTestResultParent", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.AutoTestResultEntityUsingIdAutoTestResultsParentIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoTestResultParentPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoTestResultFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoTestResultRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this AutoTestResultEntity</param>
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
		public  static AutoTestResultRelations Relations
		{
			get	{ return new AutoTestResultRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTestResultChildes
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoTestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultEntityFactory))), (IEntityRelation)GetRelationsForField("AutoTestResultChildes")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.AutoTestResultEntity, 0, null, null, null, null, "AutoTestResultChildes", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTestResultProduct' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTestResultProducts
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoTestResultProductEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultProductEntityFactory))), (IEntityRelation)GetRelationsForField("AutoTestResultProducts")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity, 0, null, null, null, null, "AutoTestResultProducts", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoItem
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoItem")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.AutoItemEntity, 0, null, null, null, null, "AutoItem", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoProtocolStageRevision' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoProtocolStageRevision
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoProtocolStageRevisionEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoProtocolStageRevision")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.AutoProtocolStageRevisionEntity, 0, null, null, null, null, "AutoProtocolStageRevision", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTest' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTest
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoTest")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.AutoTestEntity, 0, null, null, null, null, "AutoTest", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTestResultParent
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoTestResultParent")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.AutoTestResultEntity, 0, null, null, null, null, "AutoTestResultParent", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.AutoTestResultEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)AutoTestResultFieldIndex.Id, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.Id, value); }
		}

		/// <summary> The UserId property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)AutoTestResultFieldIndex.UserId, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.UserId, value); }
		}

		/// <summary> The AutoTestsId property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTests_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 AutoTestsId
		{
			get { return (System.Int32)GetValue((int)AutoTestResultFieldIndex.AutoTestsId, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.AutoTestsId, value); }
		}

		/// <summary> The AutoItemsId property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoItems_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 AutoItemsId
		{
			get { return (System.Int32)GetValue((int)AutoTestResultFieldIndex.AutoItemsId, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.AutoItemsId, value); }
		}

		/// <summary> The AutoProtocolStageRevisionsId property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoProtocolStageRevisions_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 AutoProtocolStageRevisionsId
		{
			get { return (System.Int32)GetValue((int)AutoTestResultFieldIndex.AutoProtocolStageRevisionsId, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.AutoProtocolStageRevisionsId, value); }
		}

		/// <summary> The PreliminaryReading property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_PreliminaryReading"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> PreliminaryReading
		{
			get { return (Nullable<System.Int32>)GetValue((int)AutoTestResultFieldIndex.PreliminaryReading, false); }
			set	{ SetValue((int)AutoTestResultFieldIndex.PreliminaryReading, value); }
		}

		/// <summary> The SummaryReading property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_SummaryReading"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> SummaryReading
		{
			get { return (Nullable<System.Int32>)GetValue((int)AutoTestResultFieldIndex.SummaryReading, false); }
			set	{ SetValue((int)AutoTestResultFieldIndex.SummaryReading, value); }
		}

		/// <summary> The IsAddedManually property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_IsAddedManually"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsAddedManually
		{
			get { return (System.Boolean)GetValue((int)AutoTestResultFieldIndex.IsAddedManually, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.IsAddedManually, value); }
		}

		/// <summary> The Notes property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_Notes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Notes
		{
			get { return (System.String)GetValue((int)AutoTestResultFieldIndex.Notes, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.Notes, value); }
		}

		/// <summary> The CreationDateTime property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoTestResultFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoTestResultFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)AutoTestResultFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The AutoTestResultsParentId property of the Entity AutoTestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResults"."AutoTestResults_ParentId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> AutoTestResultsParentId
		{
			get { return (Nullable<System.Int32>)GetValue((int)AutoTestResultFieldIndex.AutoTestResultsParentId, false); }
			set	{ SetValue((int)AutoTestResultFieldIndex.AutoTestResultsParentId, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoTestResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoTestResultEntity))]
		public virtual EntityCollection<AutoTestResultEntity> AutoTestResultChildes
		{
			get { return GetOrCreateEntityCollection<AutoTestResultEntity, AutoTestResultEntityFactory>("AutoTestResultParent", true, false, ref _autoTestResultChildes);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoTestResultProductEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoTestResultProductEntity))]
		public virtual EntityCollection<AutoTestResultProductEntity> AutoTestResultProducts
		{
			get { return GetOrCreateEntityCollection<AutoTestResultProductEntity, AutoTestResultProductEntityFactory>("AutoTestResult", true, false, ref _autoTestResultProducts);	}
		}

		/// <summary> Gets / sets related entity of type 'AutoItemEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoItemEntity AutoItem
		{
			get	{ return _autoItem; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoItem(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "AutoItem", _autoItem, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'AutoProtocolStageRevisionEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoProtocolStageRevisionEntity AutoProtocolStageRevision
		{
			get	{ return _autoProtocolStageRevision; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoProtocolStageRevision(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "AutoProtocolStageRevision", _autoProtocolStageRevision, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'AutoTestEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoTestEntity AutoTest
		{
			get	{ return _autoTest; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoTest(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoTestResults", "AutoTest", _autoTest, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'AutoTestResultEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoTestResultEntity AutoTestResultParent
		{
			get	{ return _autoTestResultParent; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoTestResultParent(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoTestResultChildes", "AutoTestResultParent", _autoTestResultParent, true); 
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
			get { return (int)Vital.DataLayer.EntityType.AutoTestResultEntity; }
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
