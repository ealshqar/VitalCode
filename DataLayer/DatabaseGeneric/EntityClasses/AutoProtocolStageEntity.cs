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
	/// <summary>Entity class which represents the entity 'AutoProtocolStage'.<br/><br/></summary>
	[Serializable]
	public partial class AutoProtocolStageEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<AutoProtocolStageRevisionEntity> _autoProtocolStageRevisions;
		private EntityCollection<StageAutoItemEntity> _stageAutoItems;
		private AutoProtocolEntity _autoProtocol;
		private AutoTestStageEntity _autoTestStage;
		private LookupEntity _stageItemsOrderType;
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
			/// <summary>Member name AutoProtocol</summary>
			public static readonly string AutoProtocol = "AutoProtocol";
			/// <summary>Member name AutoTestStage</summary>
			public static readonly string AutoTestStage = "AutoTestStage";
			/// <summary>Member name StageItemsOrderType</summary>
			public static readonly string StageItemsOrderType = "StageItemsOrderType";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name AutoProtocolStageRevisions</summary>
			public static readonly string AutoProtocolStageRevisions = "AutoProtocolStageRevisions";
			/// <summary>Member name StageAutoItems</summary>
			public static readonly string StageAutoItems = "StageAutoItems";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static AutoProtocolStageEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public AutoProtocolStageEntity():base("AutoProtocolStageEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public AutoProtocolStageEntity(IEntityFields2 fields):base("AutoProtocolStageEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this AutoProtocolStageEntity</param>
		public AutoProtocolStageEntity(IValidator validator):base("AutoProtocolStageEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoProtocolStage which data should be fetched into this AutoProtocolStage object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoProtocolStageEntity(System.Int32 id):base("AutoProtocolStageEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoProtocolStage which data should be fetched into this AutoProtocolStage object</param>
		/// <param name="validator">The custom validator object for this AutoProtocolStageEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoProtocolStageEntity(System.Int32 id, IValidator validator):base("AutoProtocolStageEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected AutoProtocolStageEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_autoProtocolStageRevisions = (EntityCollection<AutoProtocolStageRevisionEntity>)info.GetValue("_autoProtocolStageRevisions", typeof(EntityCollection<AutoProtocolStageRevisionEntity>));
				_stageAutoItems = (EntityCollection<StageAutoItemEntity>)info.GetValue("_stageAutoItems", typeof(EntityCollection<StageAutoItemEntity>));
				_autoProtocol = (AutoProtocolEntity)info.GetValue("_autoProtocol", typeof(AutoProtocolEntity));
				if(_autoProtocol!=null)
				{
					_autoProtocol.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_autoTestStage = (AutoTestStageEntity)info.GetValue("_autoTestStage", typeof(AutoTestStageEntity));
				if(_autoTestStage!=null)
				{
					_autoTestStage.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_stageItemsOrderType = (LookupEntity)info.GetValue("_stageItemsOrderType", typeof(LookupEntity));
				if(_stageItemsOrderType!=null)
				{
					_stageItemsOrderType.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((AutoProtocolStageFieldIndex)fieldIndex)
			{
				case AutoProtocolStageFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case AutoProtocolStageFieldIndex.AutoProtocolsId:
					DesetupSyncAutoProtocol(true, false);
					break;
				case AutoProtocolStageFieldIndex.AutoTestStagesId:
					DesetupSyncAutoTestStage(true, false);
					break;
				case AutoProtocolStageFieldIndex.StageItemsOrderTypeLookupId:
					DesetupSyncStageItemsOrderType(true, false);
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
				case "AutoProtocol":
					this.AutoProtocol = (AutoProtocolEntity)entity;
					break;
				case "AutoTestStage":
					this.AutoTestStage = (AutoTestStageEntity)entity;
					break;
				case "StageItemsOrderType":
					this.StageItemsOrderType = (LookupEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "AutoProtocolStageRevisions":
					this.AutoProtocolStageRevisions.Add((AutoProtocolStageRevisionEntity)entity);
					break;
				case "StageAutoItems":
					this.StageAutoItems.Add((StageAutoItemEntity)entity);
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
				case "AutoProtocol":
					toReturn.Add(Relations.AutoProtocolEntityUsingAutoProtocolsId);
					break;
				case "AutoTestStage":
					toReturn.Add(Relations.AutoTestStageEntityUsingAutoTestStagesId);
					break;
				case "StageItemsOrderType":
					toReturn.Add(Relations.LookupEntityUsingStageItemsOrderTypeLookupId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "AutoProtocolStageRevisions":
					toReturn.Add(Relations.AutoProtocolStageRevisionEntityUsingAutoProtocolStagesId);
					break;
				case "StageAutoItems":
					toReturn.Add(Relations.StageAutoItemEntityUsingAutoProtocolStagesId);
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
				case "AutoTestStage":
					return true;
				case "StageItemsOrderType":
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
				case "AutoProtocol":
					SetupSyncAutoProtocol(relatedEntity);
					break;
				case "AutoTestStage":
					SetupSyncAutoTestStage(relatedEntity);
					break;
				case "StageItemsOrderType":
					SetupSyncStageItemsOrderType(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "AutoProtocolStageRevisions":
					this.AutoProtocolStageRevisions.Add((AutoProtocolStageRevisionEntity)relatedEntity);
					break;
				case "StageAutoItems":
					this.StageAutoItems.Add((StageAutoItemEntity)relatedEntity);
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
				case "AutoProtocol":
					DesetupSyncAutoProtocol(false, true);
					break;
				case "AutoTestStage":
					DesetupSyncAutoTestStage(false, true);
					break;
				case "StageItemsOrderType":
					DesetupSyncStageItemsOrderType(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "AutoProtocolStageRevisions":
					this.PerformRelatedEntityRemoval(this.AutoProtocolStageRevisions, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "StageAutoItems":
					this.PerformRelatedEntityRemoval(this.StageAutoItems, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_autoProtocol!=null)
			{
				toReturn.Add(_autoProtocol);
			}
			if(_autoTestStage!=null)
			{
				toReturn.Add(_autoTestStage);
			}
			if(_stageItemsOrderType!=null)
			{
				toReturn.Add(_stageItemsOrderType);
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
			toReturn.Add(this.AutoProtocolStageRevisions);
			toReturn.Add(this.StageAutoItems);
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
				info.AddValue("_autoProtocolStageRevisions", ((_autoProtocolStageRevisions!=null) && (_autoProtocolStageRevisions.Count>0) && !this.MarkedForDeletion)?_autoProtocolStageRevisions:null);
				info.AddValue("_stageAutoItems", ((_stageAutoItems!=null) && (_stageAutoItems.Count>0) && !this.MarkedForDeletion)?_stageAutoItems:null);
				info.AddValue("_autoProtocol", (!this.MarkedForDeletion?_autoProtocol:null));
				info.AddValue("_autoTestStage", (!this.MarkedForDeletion?_autoTestStage:null));
				info.AddValue("_stageItemsOrderType", (!this.MarkedForDeletion?_stageItemsOrderType:null));
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
			return new AutoProtocolStageRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoProtocolStageRevision' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoProtocolStageRevisions()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoProtocolStageRevisionFields.AutoProtocolStagesId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'StageAutoItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStageAutoItems()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(StageAutoItemFields.AutoProtocolStagesId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoProtocol' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoProtocol()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoProtocolFields.Id, null, ComparisonOperator.Equal, this.AutoProtocolsId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoTestStage' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTestStage()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestStageFields.Id, null, ComparisonOperator.Equal, this.AutoTestStagesId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStageItemsOrderType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.StageItemsOrderTypeLookupId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(AutoProtocolStageEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._autoProtocolStageRevisions);
			collectionsQueue.Enqueue(this._stageAutoItems);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._autoProtocolStageRevisions = (EntityCollection<AutoProtocolStageRevisionEntity>) collectionsQueue.Dequeue();
			this._stageAutoItems = (EntityCollection<StageAutoItemEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._autoProtocolStageRevisions != null);
			toReturn |=(this._stageAutoItems != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoProtocolStageRevisionEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoProtocolStageRevisionEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<StageAutoItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(StageAutoItemEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("AutoProtocol", _autoProtocol);
			toReturn.Add("AutoTestStage", _autoTestStage);
			toReturn.Add("StageItemsOrderType", _stageItemsOrderType);
			toReturn.Add("User", _user);
			toReturn.Add("AutoProtocolStageRevisions", _autoProtocolStageRevisions);
			toReturn.Add("StageAutoItems", _stageAutoItems);
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
			_fieldsCustomProperties.Add("AutoProtocolsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AutoTestStagesId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("StageItemsOrderTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Order", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsDeleted", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _autoProtocol</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoProtocol(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoProtocol, new PropertyChangedEventHandler( OnAutoProtocolPropertyChanged ), "AutoProtocol", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.AutoProtocolEntityUsingAutoProtocolsIdStatic, true, signalRelatedEntity, "AutoProtocolStages", resetFKFields, new int[] { (int)AutoProtocolStageFieldIndex.AutoProtocolsId } );
			_autoProtocol = null;
		}

		/// <summary> setups the sync logic for member _autoProtocol</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoProtocol(IEntityCore relatedEntity)
		{
			if(_autoProtocol!=relatedEntity)
			{
				DesetupSyncAutoProtocol(true, true);
				_autoProtocol = (AutoProtocolEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoProtocol, new PropertyChangedEventHandler( OnAutoProtocolPropertyChanged ), "AutoProtocol", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.AutoProtocolEntityUsingAutoProtocolsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoProtocolPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _autoTestStage</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoTestStage(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoTestStage, new PropertyChangedEventHandler( OnAutoTestStagePropertyChanged ), "AutoTestStage", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.AutoTestStageEntityUsingAutoTestStagesIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoProtocolStageFieldIndex.AutoTestStagesId } );
			_autoTestStage = null;
		}

		/// <summary> setups the sync logic for member _autoTestStage</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoTestStage(IEntityCore relatedEntity)
		{
			if(_autoTestStage!=relatedEntity)
			{
				DesetupSyncAutoTestStage(true, true);
				_autoTestStage = (AutoTestStageEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoTestStage, new PropertyChangedEventHandler( OnAutoTestStagePropertyChanged ), "AutoTestStage", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.AutoTestStageEntityUsingAutoTestStagesIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoTestStagePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _stageItemsOrderType</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncStageItemsOrderType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _stageItemsOrderType, new PropertyChangedEventHandler( OnStageItemsOrderTypePropertyChanged ), "StageItemsOrderType", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.LookupEntityUsingStageItemsOrderTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoProtocolStageFieldIndex.StageItemsOrderTypeLookupId } );
			_stageItemsOrderType = null;
		}

		/// <summary> setups the sync logic for member _stageItemsOrderType</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncStageItemsOrderType(IEntityCore relatedEntity)
		{
			if(_stageItemsOrderType!=relatedEntity)
			{
				DesetupSyncStageItemsOrderType(true, true);
				_stageItemsOrderType = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _stageItemsOrderType, new PropertyChangedEventHandler( OnStageItemsOrderTypePropertyChanged ), "StageItemsOrderType", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.LookupEntityUsingStageItemsOrderTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStageItemsOrderTypePropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoProtocolStageFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoProtocolStageRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this AutoProtocolStageEntity</param>
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
		public  static AutoProtocolStageRelations Relations
		{
			get	{ return new AutoProtocolStageRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoProtocolStageRevision' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoProtocolStageRevisions
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoProtocolStageRevisionEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoProtocolStageRevisionEntityFactory))), (IEntityRelation)GetRelationsForField("AutoProtocolStageRevisions")[0], (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity, (int)Vital.DataLayer.EntityType.AutoProtocolStageRevisionEntity, 0, null, null, null, null, "AutoProtocolStageRevisions", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'StageAutoItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStageAutoItems
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<StageAutoItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(StageAutoItemEntityFactory))), (IEntityRelation)GetRelationsForField("StageAutoItems")[0], (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity, (int)Vital.DataLayer.EntityType.StageAutoItemEntity, 0, null, null, null, null, "StageAutoItems", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoProtocol' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoProtocol
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoProtocolEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoProtocol")[0], (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity, (int)Vital.DataLayer.EntityType.AutoProtocolEntity, 0, null, null, null, null, "AutoProtocol", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTestStage' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTestStage
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestStageEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoTestStage")[0], (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity, (int)Vital.DataLayer.EntityType.AutoTestStageEntity, 0, null, null, null, null, "AutoTestStage", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStageItemsOrderType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("StageItemsOrderType")[0], (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "StageItemsOrderType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."AutoProtocolStages_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)AutoProtocolStageFieldIndex.Id, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.Id, value); }
		}

		/// <summary> The UserId property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)AutoProtocolStageFieldIndex.UserId, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.UserId, value); }
		}

		/// <summary> The AutoProtocolsId property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."AutoProtocols_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 AutoProtocolsId
		{
			get { return (System.Int32)GetValue((int)AutoProtocolStageFieldIndex.AutoProtocolsId, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.AutoProtocolsId, value); }
		}

		/// <summary> The AutoTestStagesId property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."AutoTestStages_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 AutoTestStagesId
		{
			get { return (System.Int32)GetValue((int)AutoProtocolStageFieldIndex.AutoTestStagesId, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.AutoTestStagesId, value); }
		}

		/// <summary> The StageItemsOrderTypeLookupId property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."StageItemsOrderType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 StageItemsOrderTypeLookupId
		{
			get { return (System.Int32)GetValue((int)AutoProtocolStageFieldIndex.StageItemsOrderTypeLookupId, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.StageItemsOrderTypeLookupId, value); }
		}

		/// <summary> The Order property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."AutoProtocolStages_Order"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 Order
		{
			get { return (System.Int32)GetValue((int)AutoProtocolStageFieldIndex.Order, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.Order, value); }
		}

		/// <summary> The IsDeleted property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."IsDeleted"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsDeleted
		{
			get { return (System.Boolean)GetValue((int)AutoProtocolStageFieldIndex.IsDeleted, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.IsDeleted, value); }
		}

		/// <summary> The CreationDateTime property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."AutoProtocolStages_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoProtocolStageFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity AutoProtocolStage<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoProtocolStages"."AutoProtocolStages_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoProtocolStageFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)AutoProtocolStageFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoProtocolStageRevisionEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoProtocolStageRevisionEntity))]
		public virtual EntityCollection<AutoProtocolStageRevisionEntity> AutoProtocolStageRevisions
		{
			get { return GetOrCreateEntityCollection<AutoProtocolStageRevisionEntity, AutoProtocolStageRevisionEntityFactory>("AutoProtocolStage", true, false, ref _autoProtocolStageRevisions);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'StageAutoItemEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(StageAutoItemEntity))]
		public virtual EntityCollection<StageAutoItemEntity> StageAutoItems
		{
			get { return GetOrCreateEntityCollection<StageAutoItemEntity, StageAutoItemEntityFactory>("AutoProtocolStage", true, false, ref _stageAutoItems);	}
		}

		/// <summary> Gets / sets related entity of type 'AutoProtocolEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoProtocolEntity AutoProtocol
		{
			get	{ return _autoProtocol; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoProtocol(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoProtocolStages", "AutoProtocol", _autoProtocol, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'AutoTestStageEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoTestStageEntity AutoTestStage
		{
			get	{ return _autoTestStage; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoTestStage(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "AutoTestStage", _autoTestStage, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity StageItemsOrderType
		{
			get	{ return _stageItemsOrderType; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncStageItemsOrderType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "StageItemsOrderType", _stageItemsOrderType, false); 
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
			get { return (int)Vital.DataLayer.EntityType.AutoProtocolStageEntity; }
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
