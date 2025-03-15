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
	/// <summary>Entity class which represents the entity 'VFS'.<br/><br/></summary>
	[Serializable]
	public partial class VFSEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<VFSItemEntity> _vFSItems;
		private EntityCollection<VFSSecondaryItemEntity> _vFSSecondaryItems;
		private PatientEntity _patient;
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
			/// <summary>Member name Patient</summary>
			public static readonly string Patient = "Patient";
			/// <summary>Member name Test</summary>
			public static readonly string Test = "Test";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name VFSItems</summary>
			public static readonly string VFSItems = "VFSItems";
			/// <summary>Member name VFSSecondaryItems</summary>
			public static readonly string VFSSecondaryItems = "VFSSecondaryItems";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static VFSEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public VFSEntity():base("VFSEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public VFSEntity(IEntityFields2 fields):base("VFSEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this VFSEntity</param>
		public VFSEntity(IValidator validator):base("VFSEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for VFS which data should be fetched into this VFS object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public VFSEntity(System.Int32 id):base("VFSEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for VFS which data should be fetched into this VFS object</param>
		/// <param name="validator">The custom validator object for this VFSEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public VFSEntity(System.Int32 id, IValidator validator):base("VFSEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected VFSEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_vFSItems = (EntityCollection<VFSItemEntity>)info.GetValue("_vFSItems", typeof(EntityCollection<VFSItemEntity>));
				_vFSSecondaryItems = (EntityCollection<VFSSecondaryItemEntity>)info.GetValue("_vFSSecondaryItems", typeof(EntityCollection<VFSSecondaryItemEntity>));
				_patient = (PatientEntity)info.GetValue("_patient", typeof(PatientEntity));
				if(_patient!=null)
				{
					_patient.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((VFSFieldIndex)fieldIndex)
			{
				case VFSFieldIndex.TestId:
					DesetupSyncTest(true, false);
					break;
				case VFSFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case VFSFieldIndex.PatientId:
					DesetupSyncPatient(true, false);
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
				case "Patient":
					this.Patient = (PatientEntity)entity;
					break;
				case "Test":
					this.Test = (TestEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "VFSItems":
					this.VFSItems.Add((VFSItemEntity)entity);
					break;
				case "VFSSecondaryItems":
					this.VFSSecondaryItems.Add((VFSSecondaryItemEntity)entity);
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
				case "Patient":
					toReturn.Add(Relations.PatientEntityUsingPatientId);
					break;
				case "Test":
					toReturn.Add(Relations.TestEntityUsingTestId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "VFSItems":
					toReturn.Add(Relations.VFSItemEntityUsingVFSId);
					break;
				case "VFSSecondaryItems":
					toReturn.Add(Relations.VFSSecondaryItemEntityUsingVfsId);
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
			int numberOfOneWayRelations = 0+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
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
				case "Patient":
					SetupSyncPatient(relatedEntity);
					break;
				case "Test":
					SetupSyncTest(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "VFSItems":
					this.VFSItems.Add((VFSItemEntity)relatedEntity);
					break;
				case "VFSSecondaryItems":
					this.VFSSecondaryItems.Add((VFSSecondaryItemEntity)relatedEntity);
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
				case "Patient":
					DesetupSyncPatient(false, true);
					break;
				case "Test":
					DesetupSyncTest(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "VFSItems":
					this.PerformRelatedEntityRemoval(this.VFSItems, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "VFSSecondaryItems":
					this.PerformRelatedEntityRemoval(this.VFSSecondaryItems, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_patient!=null)
			{
				toReturn.Add(_patient);
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
			toReturn.Add(this.VFSItems);
			toReturn.Add(this.VFSSecondaryItems);
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
				info.AddValue("_vFSItems", ((_vFSItems!=null) && (_vFSItems.Count>0) && !this.MarkedForDeletion)?_vFSItems:null);
				info.AddValue("_vFSSecondaryItems", ((_vFSSecondaryItems!=null) && (_vFSSecondaryItems.Count>0) && !this.MarkedForDeletion)?_vFSSecondaryItems:null);
				info.AddValue("_patient", (!this.MarkedForDeletion?_patient:null));
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
			return new VFSRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VFSItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVFSItems()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSItemFields.VFSId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VFSSecondaryItem' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVFSSecondaryItems()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSSecondaryItemFields.VfsId, null, ComparisonOperator.Equal, this.Id));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(VFSEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._vFSItems);
			collectionsQueue.Enqueue(this._vFSSecondaryItems);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._vFSItems = (EntityCollection<VFSItemEntity>) collectionsQueue.Dequeue();
			this._vFSSecondaryItems = (EntityCollection<VFSSecondaryItemEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._vFSItems != null);
			toReturn |=(this._vFSSecondaryItems != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<VFSItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSItemEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<VFSSecondaryItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSSecondaryItemEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Patient", _patient);
			toReturn.Add("Test", _test);
			toReturn.Add("User", _user);
			toReturn.Add("VFSItems", _vFSItems);
			toReturn.Add("VFSSecondaryItems", _vFSSecondaryItems);
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
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TestId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("DateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ThyroidNumOfIssues", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("MercuryNumOfIssues", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("EmotionalIssues", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Notes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PatientId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _patient</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncPatient(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _patient, new PropertyChangedEventHandler( OnPatientPropertyChanged ), "Patient", Vital.DataLayer.RelationClasses.StaticVFSRelations.PatientEntityUsingPatientIdStatic, true, signalRelatedEntity, "VFSRecords", resetFKFields, new int[] { (int)VFSFieldIndex.PatientId } );
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
				this.PerformSetupSyncRelatedEntity( _patient, new PropertyChangedEventHandler( OnPatientPropertyChanged ), "Patient", Vital.DataLayer.RelationClasses.StaticVFSRelations.PatientEntityUsingPatientIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _test</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTest(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _test, new PropertyChangedEventHandler( OnTestPropertyChanged ), "Test", Vital.DataLayer.RelationClasses.StaticVFSRelations.TestEntityUsingTestIdStatic, true, signalRelatedEntity, "Vfs", resetFKFields, new int[] { (int)VFSFieldIndex.TestId } );
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
				this.PerformSetupSyncRelatedEntity( _test, new PropertyChangedEventHandler( OnTestPropertyChanged ), "Test", Vital.DataLayer.RelationClasses.StaticVFSRelations.TestEntityUsingTestIdStatic, true, new string[] {  } );
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticVFSRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticVFSRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this VFSEntity</param>
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
		public  static VFSRelations Relations
		{
			get	{ return new VFSRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFSItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVFSItems
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<VFSItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSItemEntityFactory))), (IEntityRelation)GetRelationsForField("VFSItems")[0], (int)Vital.DataLayer.EntityType.VFSEntity, (int)Vital.DataLayer.EntityType.VFSItemEntity, 0, null, null, null, null, "VFSItems", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFSSecondaryItem' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVFSSecondaryItems
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<VFSSecondaryItemEntity>(EntityFactoryCache2.GetEntityFactory(typeof(VFSSecondaryItemEntityFactory))), (IEntityRelation)GetRelationsForField("VFSSecondaryItems")[0], (int)Vital.DataLayer.EntityType.VFSEntity, (int)Vital.DataLayer.EntityType.VFSSecondaryItemEntity, 0, null, null, null, null, "VFSSecondaryItems", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Patient' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathPatient
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(PatientEntityFactory))),	(IEntityRelation)GetRelationsForField("Patient")[0], (int)Vital.DataLayer.EntityType.VFSEntity, (int)Vital.DataLayer.EntityType.PatientEntity, 0, null, null, null, null, "Patient", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Test' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTest
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestEntityFactory))),	(IEntityRelation)GetRelationsForField("Test")[0], (int)Vital.DataLayer.EntityType.VFSEntity, (int)Vital.DataLayer.EntityType.TestEntity, 0, null, null, null, null, "Test", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.VFSEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)VFSFieldIndex.Id, true); }
			set	{ SetValue((int)VFSFieldIndex.Id, value); }
		}

		/// <summary> The Name property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_Name"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)VFSFieldIndex.Name, true); }
			set	{ SetValue((int)VFSFieldIndex.Name, value); }
		}

		/// <summary> The TestId property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."Test_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> TestId
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSFieldIndex.TestId, false); }
			set	{ SetValue((int)VFSFieldIndex.TestId, value); }
		}

		/// <summary> The DateTime property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_DateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime DateTime
		{
			get { return (System.DateTime)GetValue((int)VFSFieldIndex.DateTime, true); }
			set	{ SetValue((int)VFSFieldIndex.DateTime, value); }
		}

		/// <summary> The ThyroidNumOfIssues property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_ThyroidNumOfIssues"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ThyroidNumOfIssues
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSFieldIndex.ThyroidNumOfIssues, false); }
			set	{ SetValue((int)VFSFieldIndex.ThyroidNumOfIssues, value); }
		}

		/// <summary> The MercuryNumOfIssues property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_MercuryNumOfIssues"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> MercuryNumOfIssues
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSFieldIndex.MercuryNumOfIssues, false); }
			set	{ SetValue((int)VFSFieldIndex.MercuryNumOfIssues, value); }
		}

		/// <summary> The EmotionalIssues property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_EmotionalIssues"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String EmotionalIssues
		{
			get { return (System.String)GetValue((int)VFSFieldIndex.EmotionalIssues, true); }
			set	{ SetValue((int)VFSFieldIndex.EmotionalIssues, value); }
		}

		/// <summary> The Notes property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_Notes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Notes
		{
			get { return (System.String)GetValue((int)VFSFieldIndex.Notes, true); }
			set	{ SetValue((int)VFSFieldIndex.Notes, value); }
		}

		/// <summary> The CreationDateTime property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)VFSFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)VFSFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."VFS_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)VFSFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)VFSFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The UserId property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)VFSFieldIndex.UserId, true); }
			set	{ SetValue((int)VFSFieldIndex.UserId, value); }
		}

		/// <summary> The PatientId property of the Entity VFS<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFS"."Patient_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 PatientId
		{
			get { return (System.Int32)GetValue((int)VFSFieldIndex.PatientId, true); }
			set	{ SetValue((int)VFSFieldIndex.PatientId, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'VFSItemEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VFSItemEntity))]
		public virtual EntityCollection<VFSItemEntity> VFSItems
		{
			get { return GetOrCreateEntityCollection<VFSItemEntity, VFSItemEntityFactory>("VFS", true, false, ref _vFSItems);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'VFSSecondaryItemEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VFSSecondaryItemEntity))]
		public virtual EntityCollection<VFSSecondaryItemEntity> VFSSecondaryItems
		{
			get { return GetOrCreateEntityCollection<VFSSecondaryItemEntity, VFSSecondaryItemEntityFactory>("VFS", true, false, ref _vFSSecondaryItems);	}
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
					SetSingleRelatedEntityNavigator(value, "VFSRecords", "Patient", _patient, true); 
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
					SetSingleRelatedEntityNavigator(value, "Vfs", "Test", _test, true); 
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
			get { return (int)Vital.DataLayer.EntityType.VFSEntity; }
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
