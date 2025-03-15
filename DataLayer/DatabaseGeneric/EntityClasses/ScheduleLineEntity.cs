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
	/// <summary>Entity class which represents the entity 'ScheduleLine'.<br/><br/></summary>
	[Serializable]
	public partial class ScheduleLineEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private ItemEntity _item;
		private TestScheduleEntity _testSchedule;

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
			/// <summary>Member name TestSchedule</summary>
			public static readonly string TestSchedule = "TestSchedule";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static ScheduleLineEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public ScheduleLineEntity():base("ScheduleLineEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ScheduleLineEntity(IEntityFields2 fields):base("ScheduleLineEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ScheduleLineEntity</param>
		public ScheduleLineEntity(IValidator validator):base("ScheduleLineEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for ScheduleLine which data should be fetched into this ScheduleLine object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ScheduleLineEntity(System.Int32 id):base("ScheduleLineEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for ScheduleLine which data should be fetched into this ScheduleLine object</param>
		/// <param name="validator">The custom validator object for this ScheduleLineEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ScheduleLineEntity(System.Int32 id, IValidator validator):base("ScheduleLineEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected ScheduleLineEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_item = (ItemEntity)info.GetValue("_item", typeof(ItemEntity));
				if(_item!=null)
				{
					_item.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_testSchedule = (TestScheduleEntity)info.GetValue("_testSchedule", typeof(TestScheduleEntity));
				if(_testSchedule!=null)
				{
					_testSchedule.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((ScheduleLineFieldIndex)fieldIndex)
			{
				case ScheduleLineFieldIndex.TestScheduleId:
					DesetupSyncTestSchedule(true, false);
					break;
				case ScheduleLineFieldIndex.ItemId:
					DesetupSyncItem(true, false);
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
				case "TestSchedule":
					this.TestSchedule = (TestScheduleEntity)entity;
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
				case "TestSchedule":
					toReturn.Add(Relations.TestScheduleEntityUsingTestScheduleId);
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
				case "TestSchedule":
					SetupSyncTestSchedule(relatedEntity);
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
				case "TestSchedule":
					DesetupSyncTestSchedule(false, true);
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
			if(_testSchedule!=null)
			{
				toReturn.Add(_testSchedule);
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
				info.AddValue("_testSchedule", (!this.MarkedForDeletion?_testSchedule:null));
			}
			// __LLBLGENPRO_USER_CODE_REGION_START GetObjectInfo
			// __LLBLGENPRO_USER_CODE_REGION_END
			base.GetObjectData(info, context);
		}


				
		/// <summary>Gets a list of all the EntityRelation objects the type of this instance has.</summary>
		/// <returns>A list of all the EntityRelation objects the type of this instance has. Hierarchy relations are excluded.</returns>
		protected override List<IEntityRelation> GetAllRelations()
		{
			return new ScheduleLineRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Item' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItem()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemFields.Id, null, ComparisonOperator.Equal, this.ItemId));
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
		

		/// <summary>Creates a new instance of the factory related to this entity</summary>
		protected override IEntityFactory2 CreateEntityFactory()
		{
			return EntityFactoryCache2.GetEntityFactory(typeof(ScheduleLineEntityFactory));
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
			toReturn.Add("TestSchedule", _testSchedule);
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
			_fieldsCustomProperties.Add("TestScheduleId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Notes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Duration", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ToBeShipped", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("WhenArising", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Breakfast", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("BetweenMealsEarly", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Lunch", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("BetweenMealsLate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Dinner", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("BeforeSleep", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("NoPerBottle", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("NoOfBottle", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Price", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsDeleted", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _item</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticScheduleLineRelations.ItemEntityUsingItemIdStatic, true, signalRelatedEntity, "ScheduleLines", resetFKFields, new int[] { (int)ScheduleLineFieldIndex.ItemId } );
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
				this.PerformSetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticScheduleLineRelations.ItemEntityUsingItemIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _testSchedule</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTestSchedule(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _testSchedule, new PropertyChangedEventHandler( OnTestSchedulePropertyChanged ), "TestSchedule", Vital.DataLayer.RelationClasses.StaticScheduleLineRelations.TestScheduleEntityUsingTestScheduleIdStatic, true, signalRelatedEntity, "ScheduleLines", resetFKFields, new int[] { (int)ScheduleLineFieldIndex.TestScheduleId } );
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
				this.PerformSetupSyncRelatedEntity( _testSchedule, new PropertyChangedEventHandler( OnTestSchedulePropertyChanged ), "TestSchedule", Vital.DataLayer.RelationClasses.StaticScheduleLineRelations.TestScheduleEntityUsingTestScheduleIdStatic, true, new string[] {  } );
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

		/// <summary> Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this ScheduleLineEntity</param>
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
		public  static ScheduleLineRelations Relations
		{
			get	{ return new ScheduleLineRelations(); }
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
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("Item")[0], (int)Vital.DataLayer.EntityType.ScheduleLineEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "Item", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestSchedule' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestSchedule
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestScheduleEntityFactory))),	(IEntityRelation)GetRelationsForField("TestSchedule")[0], (int)Vital.DataLayer.EntityType.ScheduleLineEntity, (int)Vital.DataLayer.EntityType.TestScheduleEntity, 0, null, null, null, null, "TestSchedule", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)ScheduleLineFieldIndex.Id, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Id, value); }
		}

		/// <summary> The TestScheduleId property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Test_Schedule_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 TestScheduleId
		{
			get { return (System.Int32)GetValue((int)ScheduleLineFieldIndex.TestScheduleId, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.TestScheduleId, value); }
		}

		/// <summary> The ItemId property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Item_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ItemId
		{
			get { return (System.Int32)GetValue((int)ScheduleLineFieldIndex.ItemId, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.ItemId, value); }
		}

		/// <summary> The Notes property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Notes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Notes
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.Notes, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Notes, value); }
		}

		/// <summary> The Duration property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Duration"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Duration
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.Duration, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Duration, value); }
		}

		/// <summary> The ToBeShipped property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_ToBeShipped"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String ToBeShipped
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.ToBeShipped, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.ToBeShipped, value); }
		}

		/// <summary> The WhenArising property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_WhenArising"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String WhenArising
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.WhenArising, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.WhenArising, value); }
		}

		/// <summary> The Breakfast property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Breakfast"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Breakfast
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.Breakfast, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Breakfast, value); }
		}

		/// <summary> The BetweenMealsEarly property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_BetweenMealsEarly"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String BetweenMealsEarly
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.BetweenMealsEarly, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.BetweenMealsEarly, value); }
		}

		/// <summary> The Lunch property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Lunch"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Lunch
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.Lunch, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Lunch, value); }
		}

		/// <summary> The BetweenMealsLate property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_BetweenMealsLate"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String BetweenMealsLate
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.BetweenMealsLate, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.BetweenMealsLate, value); }
		}

		/// <summary> The Dinner property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Dinner"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Dinner
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.Dinner, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Dinner, value); }
		}

		/// <summary> The BeforeSleep property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_BeforeSleep"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String BeforeSleep
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.BeforeSleep, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.BeforeSleep, value); }
		}

		/// <summary> The NoPerBottle property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_NoPerBottle"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String NoPerBottle
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.NoPerBottle, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.NoPerBottle, value); }
		}

		/// <summary> The NoOfBottle property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_NoOfBottle"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 200<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String NoOfBottle
		{
			get { return (System.String)GetValue((int)ScheduleLineFieldIndex.NoOfBottle, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.NoOfBottle, value); }
		}

		/// <summary> The Price property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_Price"<br/>
		/// Table field type characteristics (type, precision, scale, length): Decimal, 12, 4, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Decimal> Price
		{
			get { return (Nullable<System.Decimal>)GetValue((int)ScheduleLineFieldIndex.Price, false); }
			set	{ SetValue((int)ScheduleLineFieldIndex.Price, value); }
		}

		/// <summary> The CreationDateTime property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)ScheduleLineFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)ScheduleLineFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The UserId property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)ScheduleLineFieldIndex.UserId, true); }
			set	{ SetValue((int)ScheduleLineFieldIndex.UserId, value); }
		}

		/// <summary> The IsDeleted property of the Entity ScheduleLine<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Schedule_Lines"."Schedule_Line_IsDeleted"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> IsDeleted
		{
			get { return (Nullable<System.Boolean>)GetValue((int)ScheduleLineFieldIndex.IsDeleted, false); }
			set	{ SetValue((int)ScheduleLineFieldIndex.IsDeleted, value); }
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
					SetSingleRelatedEntityNavigator(value, "ScheduleLines", "Item", _item, true); 
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
					SetSingleRelatedEntityNavigator(value, "ScheduleLines", "TestSchedule", _testSchedule, true); 
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
			get { return (int)Vital.DataLayer.EntityType.ScheduleLineEntity; }
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
