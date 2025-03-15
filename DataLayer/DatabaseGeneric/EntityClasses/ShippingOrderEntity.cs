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
	/// <summary>Entity class which represents the entity 'ShippingOrder'.<br/><br/></summary>
	[Serializable]
	public partial class ShippingOrderEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<OrderItemEntity> _orderItems;
		private LookupEntity _shippingMethod;
		private TestEntity _test;
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
			/// <summary>Member name ShippingMethod</summary>
			public static readonly string ShippingMethod = "ShippingMethod";
			/// <summary>Member name Test</summary>
			public static readonly string Test = "Test";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name OrderItems</summary>
			public static readonly string OrderItems = "OrderItems";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static ShippingOrderEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public ShippingOrderEntity():base("ShippingOrderEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ShippingOrderEntity(IEntityFields2 fields):base("ShippingOrderEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ShippingOrderEntity</param>
		public ShippingOrderEntity(IValidator validator):base("ShippingOrderEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for ShippingOrder which data should be fetched into this ShippingOrder object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ShippingOrderEntity(System.Int32 id):base("ShippingOrderEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for ShippingOrder which data should be fetched into this ShippingOrder object</param>
		/// <param name="validator">The custom validator object for this ShippingOrderEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ShippingOrderEntity(System.Int32 id, IValidator validator):base("ShippingOrderEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected ShippingOrderEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_orderItems = (EntityCollection<OrderItemEntity>)info.GetValue("_orderItems", typeof(EntityCollection<OrderItemEntity>));
				_shippingMethod = (LookupEntity)info.GetValue("_shippingMethod", typeof(LookupEntity));
				if(_shippingMethod!=null)
				{
					_shippingMethod.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_test = (TestEntity)info.GetValue("_test", typeof(TestEntity));
				if(_test!=null)
				{
					_test.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((ShippingOrderFieldIndex)fieldIndex)
			{
				case ShippingOrderFieldIndex.TestId:
					DesetupSyncTest(true, false);
					break;
				case ShippingOrderFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case ShippingOrderFieldIndex.ShippingMethodLookupId:
					DesetupSyncShippingMethod(true, false);
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
				case "ShippingMethod":
					this.ShippingMethod = (LookupEntity)entity;
					break;
				case "Test":
					this.Test = (TestEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "OrderItems":
					this.OrderItems.Add((OrderItemEntity)entity);
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
				case "ShippingMethod":
					toReturn.Add(Relations.LookupEntityUsingShippingMethodLookupId);
					break;
				case "Test":
					toReturn.Add(Relations.TestEntityUsingTestId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "OrderItems":
					toReturn.Add(Relations.OrderItemEntityUsingShippingOrderId);
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
				case "ShippingMethod":
					SetupSyncShippingMethod(relatedEntity);
					break;
				case "Test":
					SetupSyncTest(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "OrderItems":
					this.OrderItems.Add((OrderItemEntity)relatedEntity);
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
				case "ShippingMethod":
					DesetupSyncShippingMethod(false, true);
					break;
				case "Test":
					DesetupSyncTest(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "OrderItems":
					this.PerformRelatedEntityRemoval(this.OrderItems, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_shippingMethod!=null)
			{
				toReturn.Add(_shippingMethod);
			}
			if(_test!=null)
			{
				toReturn.Add(_test);
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
			toReturn.Add(this.OrderItems);
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
				info.AddValue("_orderItems", ((_orderItems!=null) && (_orderItems.Count>0) && !this.MarkedForDeletion)?_orderItems:null);
				info.AddValue("_shippingMethod", (!this.MarkedForDeletion?_shippingMethod:null));
				info.AddValue("_test", (!this.MarkedForDeletion?_test:null));
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
			return new ShippingOrderRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'OrderItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoOrderItems()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(OrderItemFields.ShippingOrderId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoShippingMethod()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ShippingMethodLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Test' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTest()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestFields.Id, null, ComparisonOperator.Equal, this.TestId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(ShippingOrderEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._orderItems);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._orderItems = (EntityCollection<OrderItemEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._orderItems != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<OrderItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(OrderItemEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("ShippingMethod", _shippingMethod);
			toReturn.Add("Test", _test);
			toReturn.Add("User", _user);
			toReturn.Add("OrderItems", _orderItems);
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
			_fieldsCustomProperties.Add("TestId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SentDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SendToClient", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Sent", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Comments", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientFirstName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientLastName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientAddress1", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientAddress2", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientCity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientState", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientZip", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientHomePhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientWorkPhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientCellPhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientFax", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientEmail", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TechnicianName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TechnicianAddress", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TechnicianState", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TechnicianZipCode", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TechnicianCity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TechnicianPhone", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ShippingMethodLookupId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _shippingMethod</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncShippingMethod(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _shippingMethod, new PropertyChangedEventHandler( OnShippingMethodPropertyChanged ), "ShippingMethod", Vital.DataLayer.RelationClasses.StaticShippingOrderRelations.LookupEntityUsingShippingMethodLookupIdStatic, true, signalRelatedEntity, "ShippingOrders", resetFKFields, new int[] { (int)ShippingOrderFieldIndex.ShippingMethodLookupId } );
			_shippingMethod = null;
		}

		/// <summary> setups the sync logic for member _shippingMethod</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncShippingMethod(IEntityCore relatedEntity)
		{
			if(_shippingMethod!=relatedEntity)
			{
				DesetupSyncShippingMethod(true, true);
				_shippingMethod = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _shippingMethod, new PropertyChangedEventHandler( OnShippingMethodPropertyChanged ), "ShippingMethod", Vital.DataLayer.RelationClasses.StaticShippingOrderRelations.LookupEntityUsingShippingMethodLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnShippingMethodPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _test</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTest(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _test, new PropertyChangedEventHandler( OnTestPropertyChanged ), "Test", Vital.DataLayer.RelationClasses.StaticShippingOrderRelations.TestEntityUsingTestIdStatic, true, signalRelatedEntity, "ShippingOrders", resetFKFields, new int[] { (int)ShippingOrderFieldIndex.TestId } );
			_test = null;
		}

		/// <summary> setups the sync logic for member _test</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTest(IEntityCore relatedEntity)
		{
			if(_test!=relatedEntity)
			{
				DesetupSyncTest(true, true);
				_test = (TestEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _test, new PropertyChangedEventHandler( OnTestPropertyChanged ), "Test", Vital.DataLayer.RelationClasses.StaticShippingOrderRelations.TestEntityUsingTestIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTestPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticShippingOrderRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "ShippingOrders", resetFKFields, new int[] { (int)ShippingOrderFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticShippingOrderRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this ShippingOrderEntity</param>
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
		public  static ShippingOrderRelations Relations
		{
			get	{ return new ShippingOrderRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'OrderItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathOrderItems
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<OrderItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(OrderItemEntityFactory))), (IEntityRelation)GetRelationsForField("OrderItems")[0], (int)Vital.DataLayer.EntityType.ShippingOrderEntity, (int)Vital.DataLayer.EntityType.OrderItemEntity, 0, null, null, null, null, "OrderItems", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathShippingMethod
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ShippingMethod")[0], (int)Vital.DataLayer.EntityType.ShippingOrderEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ShippingMethod", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Test' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTest
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestEntityFactory))),	(IEntityRelation)GetRelationsForField("Test")[0], (int)Vital.DataLayer.EntityType.ShippingOrderEntity, (int)Vital.DataLayer.EntityType.TestEntity, 0, null, null, null, null, "Test", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.ShippingOrderEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)ShippingOrderFieldIndex.Id, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.Id, value); }
		}

		/// <summary> The Number property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_Number"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Number
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.Number, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.Number, value); }
		}

		/// <summary> The TestId property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Test_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> TestId
		{
			get { return (Nullable<System.Int32>)GetValue((int)ShippingOrderFieldIndex.TestId, false); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TestId, value); }
		}

		/// <summary> The SentDate property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_SentDate"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> SentDate
		{
			get { return (Nullable<System.DateTime>)GetValue((int)ShippingOrderFieldIndex.SentDate, false); }
			set	{ SetValue((int)ShippingOrderFieldIndex.SentDate, value); }
		}

		/// <summary> The SendToClient property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_SendToClient"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean SendToClient
		{
			get { return (System.Boolean)GetValue((int)ShippingOrderFieldIndex.SendToClient, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.SendToClient, value); }
		}

		/// <summary> The Sent property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_Sent"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean Sent
		{
			get { return (System.Boolean)GetValue((int)ShippingOrderFieldIndex.Sent, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.Sent, value); }
		}

		/// <summary> The Comments property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_Comments"<br/>
		/// Table field type characteristics (type, precision, scale, length): NText, 0, 0, 1073741823<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Comments
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.Comments, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.Comments, value); }
		}

		/// <summary> The PatientFirstName property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientFirstName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientFirstName
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientFirstName, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientFirstName, value); }
		}

		/// <summary> The PatientLastName property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientLastName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientLastName
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientLastName, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientLastName, value); }
		}

		/// <summary> The PatientAddress1 property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientAddress1"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientAddress1
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientAddress1, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientAddress1, value); }
		}

		/// <summary> The PatientAddress2 property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientAddress2"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientAddress2
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientAddress2, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientAddress2, value); }
		}

		/// <summary> The PatientCity property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientCity"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientCity
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientCity, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientCity, value); }
		}

		/// <summary> The PatientState property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientState"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientState
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientState, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientState, value); }
		}

		/// <summary> The PatientZip property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientZip"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 10<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientZip
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientZip, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientZip, value); }
		}

		/// <summary> The PatientHomePhone property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientHomePhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientHomePhone
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientHomePhone, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientHomePhone, value); }
		}

		/// <summary> The PatientWorkPhone property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientWorkPhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientWorkPhone
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientWorkPhone, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientWorkPhone, value); }
		}

		/// <summary> The PatientCellPhone property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientCellPhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientCellPhone
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientCellPhone, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientCellPhone, value); }
		}

		/// <summary> The PatientFax property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientFax"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientFax
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientFax, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientFax, value); }
		}

		/// <summary> The PatientEmail property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_PatientEmail"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PatientEmail
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.PatientEmail, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.PatientEmail, value); }
		}

		/// <summary> The TechnicianName property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_TechnicianName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 100<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TechnicianName
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.TechnicianName, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TechnicianName, value); }
		}

		/// <summary> The TechnicianAddress property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_TechnicianAddress"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TechnicianAddress
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.TechnicianAddress, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TechnicianAddress, value); }
		}

		/// <summary> The TechnicianState property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_TechnicianState"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TechnicianState
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.TechnicianState, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TechnicianState, value); }
		}

		/// <summary> The TechnicianZipCode property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_TechnicianZipCode"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 10<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TechnicianZipCode
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.TechnicianZipCode, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TechnicianZipCode, value); }
		}

		/// <summary> The TechnicianCity property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_TechnicianCity"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TechnicianCity
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.TechnicianCity, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TechnicianCity, value); }
		}

		/// <summary> The TechnicianPhone property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_TechnicianPhone"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TechnicianPhone
		{
			get { return (System.String)GetValue((int)ShippingOrderFieldIndex.TechnicianPhone, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.TechnicianPhone, value); }
		}

		/// <summary> The CreationDateTime property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)ShippingOrderFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."Shipping_Order_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)ShippingOrderFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The UserId property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)ShippingOrderFieldIndex.UserId, true); }
			set	{ SetValue((int)ShippingOrderFieldIndex.UserId, value); }
		}

		/// <summary> The ShippingMethodLookupId property of the Entity ShippingOrder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Shipping_Orders"."ShippingMethod_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ShippingMethodLookupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)ShippingOrderFieldIndex.ShippingMethodLookupId, false); }
			set	{ SetValue((int)ShippingOrderFieldIndex.ShippingMethodLookupId, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'OrderItemEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(OrderItemEntity))]
		public virtual EntityCollection<OrderItemEntity> OrderItems
		{
			get { return GetOrCreateEntityCollection<OrderItemEntity, OrderItemEntityFactory>("ShippingOrder", true, false, ref _orderItems);	}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ShippingMethod
		{
			get	{ return _shippingMethod; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncShippingMethod(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "ShippingOrders", "ShippingMethod", _shippingMethod, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestEntity Test
		{
			get	{ return _test; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTest(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "ShippingOrders", "Test", _test, true); 
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
					SetSingleRelatedEntityNavigator(value, "ShippingOrders", "User", _user, true); 
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
			get { return (int)Vital.DataLayer.EntityType.ShippingOrderEntity; }
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
