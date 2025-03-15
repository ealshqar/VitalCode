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
	/// <summary>Entity class which represents the entity 'SpotCheckResult'.<br/><br/></summary>
	[Serializable]
	public partial class SpotCheckResultEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private ItemEntity _item;
		private LookupEntity _resultType;
		private SpotCheckEntity _spotCheck;
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
			/// <summary>Member name ResultType</summary>
			public static readonly string ResultType = "ResultType";
			/// <summary>Member name SpotCheck</summary>
			public static readonly string SpotCheck = "SpotCheck";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static SpotCheckResultEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public SpotCheckResultEntity():base("SpotCheckResultEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SpotCheckResultEntity(IEntityFields2 fields):base("SpotCheckResultEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SpotCheckResultEntity</param>
		public SpotCheckResultEntity(IValidator validator):base("SpotCheckResultEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for SpotCheckResult which data should be fetched into this SpotCheckResult object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public SpotCheckResultEntity(System.Int32 id):base("SpotCheckResultEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for SpotCheckResult which data should be fetched into this SpotCheckResult object</param>
		/// <param name="validator">The custom validator object for this SpotCheckResultEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public SpotCheckResultEntity(System.Int32 id, IValidator validator):base("SpotCheckResultEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected SpotCheckResultEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_item = (ItemEntity)info.GetValue("_item", typeof(ItemEntity));
				if(_item!=null)
				{
					_item.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_resultType = (LookupEntity)info.GetValue("_resultType", typeof(LookupEntity));
				if(_resultType!=null)
				{
					_resultType.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_spotCheck = (SpotCheckEntity)info.GetValue("_spotCheck", typeof(SpotCheckEntity));
				if(_spotCheck!=null)
				{
					_spotCheck.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((SpotCheckResultFieldIndex)fieldIndex)
			{
				case SpotCheckResultFieldIndex.SpotCheckId:
					DesetupSyncSpotCheck(true, false);
					break;
				case SpotCheckResultFieldIndex.ItemId:
					DesetupSyncItem(true, false);
					break;
				case SpotCheckResultFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case SpotCheckResultFieldIndex.ResultTypeId:
					DesetupSyncResultType(true, false);
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
				case "ResultType":
					this.ResultType = (LookupEntity)entity;
					break;
				case "SpotCheck":
					this.SpotCheck = (SpotCheckEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
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
					toReturn.Add(Relations.ItemEntityUsingItemId);
					break;
				case "ResultType":
					toReturn.Add(Relations.LookupEntityUsingResultTypeId);
					break;
				case "SpotCheck":
					toReturn.Add(Relations.SpotCheckEntityUsingSpotCheckId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
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
				case "Item":
					SetupSyncItem(relatedEntity);
					break;
				case "ResultType":
					SetupSyncResultType(relatedEntity);
					break;
				case "SpotCheck":
					SetupSyncSpotCheck(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
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
				case "ResultType":
					DesetupSyncResultType(false, true);
					break;
				case "SpotCheck":
					DesetupSyncSpotCheck(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
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
			if(_resultType!=null)
			{
				toReturn.Add(_resultType);
			}
			if(_spotCheck!=null)
			{
				toReturn.Add(_spotCheck);
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
				info.AddValue("_item", (!this.MarkedForDeletion?_item:null));
				info.AddValue("_resultType", (!this.MarkedForDeletion?_resultType:null));
				info.AddValue("_spotCheck", (!this.MarkedForDeletion?_spotCheck:null));
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
			return new SpotCheckResultRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Item' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItem()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemFields.Id, null, ComparisonOperator.Equal, this.ItemId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoResultType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ResultTypeId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'SpotCheck' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotCheck()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckFields.Id, null, ComparisonOperator.Equal, this.SpotCheckId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Item", _item);
			toReturn.Add("ResultType", _resultType);
			toReturn.Add("SpotCheck", _spotCheck);
			toReturn.Add("User", _user);
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
			_fieldsCustomProperties.Add("SpotCheckId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("YesNo", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("NumberOfBags", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("NumberOfWeeks", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Dosage", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Notes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ResultTypeId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _item</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.ItemEntityUsingItemIdStatic, true, signalRelatedEntity, "SpotCheckResults", resetFKFields, new int[] { (int)SpotCheckResultFieldIndex.ItemId } );
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
				this.PerformSetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.ItemEntityUsingItemIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _resultType</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncResultType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _resultType, new PropertyChangedEventHandler( OnResultTypePropertyChanged ), "ResultType", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.LookupEntityUsingResultTypeIdStatic, true, signalRelatedEntity, "SpotCheckResults", resetFKFields, new int[] { (int)SpotCheckResultFieldIndex.ResultTypeId } );
			_resultType = null;
		}

		/// <summary> setups the sync logic for member _resultType</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncResultType(IEntityCore relatedEntity)
		{
			if(_resultType!=relatedEntity)
			{
				DesetupSyncResultType(true, true);
				_resultType = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _resultType, new PropertyChangedEventHandler( OnResultTypePropertyChanged ), "ResultType", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.LookupEntityUsingResultTypeIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnResultTypePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _spotCheck</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncSpotCheck(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _spotCheck, new PropertyChangedEventHandler( OnSpotCheckPropertyChanged ), "SpotCheck", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.SpotCheckEntityUsingSpotCheckIdStatic, true, signalRelatedEntity, "SpotCheckResults", resetFKFields, new int[] { (int)SpotCheckResultFieldIndex.SpotCheckId } );
			_spotCheck = null;
		}

		/// <summary> setups the sync logic for member _spotCheck</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncSpotCheck(IEntityCore relatedEntity)
		{
			if(_spotCheck!=relatedEntity)
			{
				DesetupSyncSpotCheck(true, true);
				_spotCheck = (SpotCheckEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _spotCheck, new PropertyChangedEventHandler( OnSpotCheckPropertyChanged ), "SpotCheck", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.SpotCheckEntityUsingSpotCheckIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSpotCheckPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "SpotCheckResults", resetFKFields, new int[] { (int)SpotCheckResultFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticSpotCheckResultRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this SpotCheckResultEntity</param>
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
		public  static SpotCheckResultRelations Relations
		{
			get	{ return new SpotCheckResultRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Item' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathItem
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("Item")[0], (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "Item", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathResultType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ResultType")[0], (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ResultType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheck' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotCheck
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckEntityFactory))),	(IEntityRelation)GetRelationsForField("SpotCheck")[0], (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, (int)Vital.DataLayer.EntityType.SpotCheckEntity, 0, null, null, null, null, "SpotCheck", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SpotCheckResultFieldIndex.Id, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.Id, value); }
		}

		/// <summary> The SpotCheckId property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 SpotCheckId
		{
			get { return (System.Int32)GetValue((int)SpotCheckResultFieldIndex.SpotCheckId, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.SpotCheckId, value); }
		}

		/// <summary> The ItemId property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Item_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ItemId
		{
			get { return (Nullable<System.Int32>)GetValue((int)SpotCheckResultFieldIndex.ItemId, false); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.ItemId, value); }
		}

		/// <summary> The CreationDateTime property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)SpotCheckResultFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)SpotCheckResultFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The UserId property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)SpotCheckResultFieldIndex.UserId, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.UserId, value); }
		}

		/// <summary> The YesNo property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_YesNo"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> YesNo
		{
			get { return (Nullable<System.Boolean>)GetValue((int)SpotCheckResultFieldIndex.YesNo, false); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.YesNo, value); }
		}

		/// <summary> The NumberOfBags property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_NumberOfBags"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> NumberOfBags
		{
			get { return (Nullable<System.Int32>)GetValue((int)SpotCheckResultFieldIndex.NumberOfBags, false); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.NumberOfBags, value); }
		}

		/// <summary> The NumberOfWeeks property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_NumberOfWeeks"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> NumberOfWeeks
		{
			get { return (Nullable<System.Int32>)GetValue((int)SpotCheckResultFieldIndex.NumberOfWeeks, false); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.NumberOfWeeks, value); }
		}

		/// <summary> The Dosage property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_Dosage"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Dosage
		{
			get { return (Nullable<System.Int32>)GetValue((int)SpotCheckResultFieldIndex.Dosage, false); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.Dosage, value); }
		}

		/// <summary> The Notes property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."Spot_Check_Result_Notes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Notes
		{
			get { return (System.String)GetValue((int)SpotCheckResultFieldIndex.Notes, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.Notes, value); }
		}

		/// <summary> The ResultTypeId property of the Entity SpotCheckResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Spot_Check_Results"."SpotCheckResultType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ResultTypeId
		{
			get { return (System.Int32)GetValue((int)SpotCheckResultFieldIndex.ResultTypeId, true); }
			set	{ SetValue((int)SpotCheckResultFieldIndex.ResultTypeId, value); }
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
					SetSingleRelatedEntityNavigator(value, "SpotCheckResults", "Item", _item, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ResultType
		{
			get	{ return _resultType; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncResultType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "SpotCheckResults", "ResultType", _resultType, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'SpotCheckEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual SpotCheckEntity SpotCheck
		{
			get	{ return _spotCheck; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncSpotCheck(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "SpotCheckResults", "SpotCheck", _spotCheck, true); 
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
					SetSingleRelatedEntityNavigator(value, "SpotCheckResults", "User", _user, true); 
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
			get { return (int)Vital.DataLayer.EntityType.SpotCheckResultEntity; }
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
