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
	/// <summary>Entity class which represents the entity 'VFSItem'.<br/><br/></summary>
	[Serializable]
	public partial class VFSItemEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private ItemEntity _item;
		private LookupEntity _gridGroupLookup;
		private LookupEntity _groupLookup;
		private LookupEntity _sectionLookup;
		private UserEntity _user;
		private VFSEntity _vFS;
		private VFSItemSourceEntity _vFSItemSource;

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
			/// <summary>Member name GridGroupLookup</summary>
			public static readonly string GridGroupLookup = "GridGroupLookup";
			/// <summary>Member name GroupLookup</summary>
			public static readonly string GroupLookup = "GroupLookup";
			/// <summary>Member name SectionLookup</summary>
			public static readonly string SectionLookup = "SectionLookup";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name VFS</summary>
			public static readonly string VFS = "VFS";
			/// <summary>Member name VFSItemSource</summary>
			public static readonly string VFSItemSource = "VFSItemSource";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static VFSItemEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public VFSItemEntity():base("VFSItemEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public VFSItemEntity(IEntityFields2 fields):base("VFSItemEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this VFSItemEntity</param>
		public VFSItemEntity(IValidator validator):base("VFSItemEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for VFSItem which data should be fetched into this VFSItem object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public VFSItemEntity(System.Int32 id):base("VFSItemEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for VFSItem which data should be fetched into this VFSItem object</param>
		/// <param name="validator">The custom validator object for this VFSItemEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public VFSItemEntity(System.Int32 id, IValidator validator):base("VFSItemEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected VFSItemEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_item = (ItemEntity)info.GetValue("_item", typeof(ItemEntity));
				if(_item!=null)
				{
					_item.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_gridGroupLookup = (LookupEntity)info.GetValue("_gridGroupLookup", typeof(LookupEntity));
				if(_gridGroupLookup!=null)
				{
					_gridGroupLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_groupLookup = (LookupEntity)info.GetValue("_groupLookup", typeof(LookupEntity));
				if(_groupLookup!=null)
				{
					_groupLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_sectionLookup = (LookupEntity)info.GetValue("_sectionLookup", typeof(LookupEntity));
				if(_sectionLookup!=null)
				{
					_sectionLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_user = (UserEntity)info.GetValue("_user", typeof(UserEntity));
				if(_user!=null)
				{
					_user.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_vFS = (VFSEntity)info.GetValue("_vFS", typeof(VFSEntity));
				if(_vFS!=null)
				{
					_vFS.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_vFSItemSource = (VFSItemSourceEntity)info.GetValue("_vFSItemSource", typeof(VFSItemSourceEntity));
				if(_vFSItemSource!=null)
				{
					_vFSItemSource.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((VFSItemFieldIndex)fieldIndex)
			{
				case VFSItemFieldIndex.VFSId:
					DesetupSyncVFS(true, false);
					break;
				case VFSItemFieldIndex.VFSitemSourceId:
					DesetupSyncVFSItemSource(true, false);
					break;
				case VFSItemFieldIndex.ItemId:
					DesetupSyncItem(true, false);
					break;
				case VFSItemFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case VFSItemFieldIndex.GroupLookupId:
					DesetupSyncGroupLookup(true, false);
					break;
				case VFSItemFieldIndex.SectionLookupId:
					DesetupSyncSectionLookup(true, false);
					break;
				case VFSItemFieldIndex.GridGroupLookupId:
					DesetupSyncGridGroupLookup(true, false);
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
				case "GridGroupLookup":
					this.GridGroupLookup = (LookupEntity)entity;
					break;
				case "GroupLookup":
					this.GroupLookup = (LookupEntity)entity;
					break;
				case "SectionLookup":
					this.SectionLookup = (LookupEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "VFS":
					this.VFS = (VFSEntity)entity;
					break;
				case "VFSItemSource":
					this.VFSItemSource = (VFSItemSourceEntity)entity;
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
				case "GridGroupLookup":
					toReturn.Add(Relations.LookupEntityUsingGridGroupLookupId);
					break;
				case "GroupLookup":
					toReturn.Add(Relations.LookupEntityUsingGroupLookupId);
					break;
				case "SectionLookup":
					toReturn.Add(Relations.LookupEntityUsingSectionLookupId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "VFS":
					toReturn.Add(Relations.VFSEntityUsingVFSId);
					break;
				case "VFSItemSource":
					toReturn.Add(Relations.VFSItemSourceEntityUsingVFSitemSourceId);
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
				case "GridGroupLookup":
					return true;
				case "GroupLookup":
					return true;
				case "SectionLookup":
					return true;
				case "User":
					return true;
				case "VFSItemSource":
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
				case "GridGroupLookup":
					SetupSyncGridGroupLookup(relatedEntity);
					break;
				case "GroupLookup":
					SetupSyncGroupLookup(relatedEntity);
					break;
				case "SectionLookup":
					SetupSyncSectionLookup(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "VFS":
					SetupSyncVFS(relatedEntity);
					break;
				case "VFSItemSource":
					SetupSyncVFSItemSource(relatedEntity);
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
				case "GridGroupLookup":
					DesetupSyncGridGroupLookup(false, true);
					break;
				case "GroupLookup":
					DesetupSyncGroupLookup(false, true);
					break;
				case "SectionLookup":
					DesetupSyncSectionLookup(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "VFS":
					DesetupSyncVFS(false, true);
					break;
				case "VFSItemSource":
					DesetupSyncVFSItemSource(false, true);
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
			if(_gridGroupLookup!=null)
			{
				toReturn.Add(_gridGroupLookup);
			}
			if(_groupLookup!=null)
			{
				toReturn.Add(_groupLookup);
			}
			if(_sectionLookup!=null)
			{
				toReturn.Add(_sectionLookup);
			}
			if(_user!=null)
			{
				toReturn.Add(_user);
			}
			if(_vFS!=null)
			{
				toReturn.Add(_vFS);
			}
			if(_vFSItemSource!=null)
			{
				toReturn.Add(_vFSItemSource);
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
				info.AddValue("_gridGroupLookup", (!this.MarkedForDeletion?_gridGroupLookup:null));
				info.AddValue("_groupLookup", (!this.MarkedForDeletion?_groupLookup:null));
				info.AddValue("_sectionLookup", (!this.MarkedForDeletion?_sectionLookup:null));
				info.AddValue("_user", (!this.MarkedForDeletion?_user:null));
				info.AddValue("_vFS", (!this.MarkedForDeletion?_vFS:null));
				info.AddValue("_vFSItemSource", (!this.MarkedForDeletion?_vFSItemSource:null));
			}
			// __LLBLGENPRO_USER_CODE_REGION_START GetObjectInfo
			// __LLBLGENPRO_USER_CODE_REGION_END
			base.GetObjectData(info, context);
		}


				
		/// <summary>Gets a list of all the EntityRelation objects the type of this instance has.</summary>
		/// <returns>A list of all the EntityRelation objects the type of this instance has. Hierarchy relations are excluded.</returns>
		protected override List<IEntityRelation> GetAllRelations()
		{
			return new VFSItemRelations().GetAllRelations();
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
		public virtual IRelationPredicateBucket GetRelationInfoGridGroupLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.GridGroupLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoGroupLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.GroupLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSectionLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.SectionLookupId));
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

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'VFS' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVFS()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSFields.Id, null, ComparisonOperator.Equal, this.VFSId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'VFSItemSource' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVFSItemSource()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(VFSItemSourceFields.Id, null, ComparisonOperator.Equal, this.VFSitemSourceId));
			return bucket;
		}
		

		/// <summary>Creates a new instance of the factory related to this entity</summary>
		protected override IEntityFactory2 CreateEntityFactory()
		{
			return EntityFactoryCache2.GetEntityFactory(typeof(VFSItemEntityFactory));
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
			toReturn.Add("GridGroupLookup", _gridGroupLookup);
			toReturn.Add("GroupLookup", _groupLookup);
			toReturn.Add("SectionLookup", _sectionLookup);
			toReturn.Add("User", _user);
			toReturn.Add("VFS", _vFS);
			toReturn.Add("VFSItemSource", _vFSItemSource);
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
			_fieldsCustomProperties.Add("VFSId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("VFSitemSourceId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PreviousV1", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("PreviousV2", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CurrentV1", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CurrentV2", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsSkipped", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Comments", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("GroupLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SectionLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("GridGroupLookupId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _item</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.ItemEntityUsingItemIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSItemFieldIndex.ItemId } );
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
				this.PerformSetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.ItemEntityUsingItemIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _gridGroupLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncGridGroupLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _gridGroupLookup, new PropertyChangedEventHandler( OnGridGroupLookupPropertyChanged ), "GridGroupLookup", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.LookupEntityUsingGridGroupLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSItemFieldIndex.GridGroupLookupId } );
			_gridGroupLookup = null;
		}

		/// <summary> setups the sync logic for member _gridGroupLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncGridGroupLookup(IEntityCore relatedEntity)
		{
			if(_gridGroupLookup!=relatedEntity)
			{
				DesetupSyncGridGroupLookup(true, true);
				_gridGroupLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _gridGroupLookup, new PropertyChangedEventHandler( OnGridGroupLookupPropertyChanged ), "GridGroupLookup", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.LookupEntityUsingGridGroupLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGridGroupLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _groupLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncGroupLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _groupLookup, new PropertyChangedEventHandler( OnGroupLookupPropertyChanged ), "GroupLookup", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.LookupEntityUsingGroupLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSItemFieldIndex.GroupLookupId } );
			_groupLookup = null;
		}

		/// <summary> setups the sync logic for member _groupLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncGroupLookup(IEntityCore relatedEntity)
		{
			if(_groupLookup!=relatedEntity)
			{
				DesetupSyncGroupLookup(true, true);
				_groupLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _groupLookup, new PropertyChangedEventHandler( OnGroupLookupPropertyChanged ), "GroupLookup", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.LookupEntityUsingGroupLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGroupLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _sectionLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncSectionLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _sectionLookup, new PropertyChangedEventHandler( OnSectionLookupPropertyChanged ), "SectionLookup", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.LookupEntityUsingSectionLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSItemFieldIndex.SectionLookupId } );
			_sectionLookup = null;
		}

		/// <summary> setups the sync logic for member _sectionLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncSectionLookup(IEntityCore relatedEntity)
		{
			if(_sectionLookup!=relatedEntity)
			{
				DesetupSyncSectionLookup(true, true);
				_sectionLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _sectionLookup, new PropertyChangedEventHandler( OnSectionLookupPropertyChanged ), "SectionLookup", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.LookupEntityUsingSectionLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSectionLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSItemFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _vFS</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncVFS(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _vFS, new PropertyChangedEventHandler( OnVFSPropertyChanged ), "VFS", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.VFSEntityUsingVFSIdStatic, true, signalRelatedEntity, "VFSItems", resetFKFields, new int[] { (int)VFSItemFieldIndex.VFSId } );
			_vFS = null;
		}

		/// <summary> setups the sync logic for member _vFS</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncVFS(IEntityCore relatedEntity)
		{
			if(_vFS!=relatedEntity)
			{
				DesetupSyncVFS(true, true);
				_vFS = (VFSEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _vFS, new PropertyChangedEventHandler( OnVFSPropertyChanged ), "VFS", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.VFSEntityUsingVFSIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnVFSPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _vFSItemSource</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncVFSItemSource(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _vFSItemSource, new PropertyChangedEventHandler( OnVFSItemSourcePropertyChanged ), "VFSItemSource", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.VFSItemSourceEntityUsingVFSitemSourceIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)VFSItemFieldIndex.VFSitemSourceId } );
			_vFSItemSource = null;
		}

		/// <summary> setups the sync logic for member _vFSItemSource</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncVFSItemSource(IEntityCore relatedEntity)
		{
			if(_vFSItemSource!=relatedEntity)
			{
				DesetupSyncVFSItemSource(true, true);
				_vFSItemSource = (VFSItemSourceEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _vFSItemSource, new PropertyChangedEventHandler( OnVFSItemSourcePropertyChanged ), "VFSItemSource", Vital.DataLayer.RelationClasses.StaticVFSItemRelations.VFSItemSourceEntityUsingVFSitemSourceIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnVFSItemSourcePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this VFSItemEntity</param>
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
		public  static VFSItemRelations Relations
		{
			get	{ return new VFSItemRelations(); }
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
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("Item")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "Item", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathGridGroupLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("GridGroupLookup")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "GridGroupLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathGroupLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("GroupLookup")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "GroupLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSectionLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("SectionLookup")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "SectionLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFS' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVFS
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(VFSEntityFactory))),	(IEntityRelation)GetRelationsForField("VFS")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.VFSEntity, 0, null, null, null, null, "VFS", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VFSItemSource' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVFSItemSource
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(VFSItemSourceEntityFactory))),	(IEntityRelation)GetRelationsForField("VFSItemSource")[0], (int)Vital.DataLayer.EntityType.VFSItemEntity, (int)Vital.DataLayer.EntityType.VFSItemSourceEntity, 0, null, null, null, null, "VFSItemSource", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)VFSItemFieldIndex.Id, true); }
			set	{ SetValue((int)VFSItemFieldIndex.Id, value); }
		}

		/// <summary> The VFSId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFS_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 VFSId
		{
			get { return (System.Int32)GetValue((int)VFSItemFieldIndex.VFSId, true); }
			set	{ SetValue((int)VFSItemFieldIndex.VFSId, value); }
		}

		/// <summary> The VFSitemSourceId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItemSource_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> VFSitemSourceId
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSItemFieldIndex.VFSitemSourceId, false); }
			set	{ SetValue((int)VFSItemFieldIndex.VFSitemSourceId, value); }
		}

		/// <summary> The ItemId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."Item_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ItemId
		{
			get { return (System.Int32)GetValue((int)VFSItemFieldIndex.ItemId, true); }
			set	{ SetValue((int)VFSItemFieldIndex.ItemId, value); }
		}

		/// <summary> The PreviousV1 property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_Previous_V1"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PreviousV1
		{
			get { return (System.String)GetValue((int)VFSItemFieldIndex.PreviousV1, true); }
			set	{ SetValue((int)VFSItemFieldIndex.PreviousV1, value); }
		}

		/// <summary> The PreviousV2 property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_Previous_V2"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String PreviousV2
		{
			get { return (System.String)GetValue((int)VFSItemFieldIndex.PreviousV2, true); }
			set	{ SetValue((int)VFSItemFieldIndex.PreviousV2, value); }
		}

		/// <summary> The CurrentV1 property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_Current_V1"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String CurrentV1
		{
			get { return (System.String)GetValue((int)VFSItemFieldIndex.CurrentV1, true); }
			set	{ SetValue((int)VFSItemFieldIndex.CurrentV1, value); }
		}

		/// <summary> The CurrentV2 property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_Current_V2"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String CurrentV2
		{
			get { return (System.String)GetValue((int)VFSItemFieldIndex.CurrentV2, true); }
			set	{ SetValue((int)VFSItemFieldIndex.CurrentV2, value); }
		}

		/// <summary> The IsSkipped property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_IsSkipped"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> IsSkipped
		{
			get { return (Nullable<System.Boolean>)GetValue((int)VFSItemFieldIndex.IsSkipped, false); }
			set	{ SetValue((int)VFSItemFieldIndex.IsSkipped, value); }
		}

		/// <summary> The Comments property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_Comments"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Comments
		{
			get { return (System.String)GetValue((int)VFSItemFieldIndex.Comments, true); }
			set	{ SetValue((int)VFSItemFieldIndex.Comments, value); }
		}

		/// <summary> The UserId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)VFSItemFieldIndex.UserId, true); }
			set	{ SetValue((int)VFSItemFieldIndex.UserId, value); }
		}

		/// <summary> The CreationDateTime property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)VFSItemFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)VFSItemFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."VFSItem_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)VFSItemFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)VFSItemFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The GroupLookupId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."Group_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> GroupLookupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSItemFieldIndex.GroupLookupId, false); }
			set	{ SetValue((int)VFSItemFieldIndex.GroupLookupId, value); }
		}

		/// <summary> The SectionLookupId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."Section_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> SectionLookupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSItemFieldIndex.SectionLookupId, false); }
			set	{ SetValue((int)VFSItemFieldIndex.SectionLookupId, value); }
		}

		/// <summary> The GridGroupLookupId property of the Entity VFSItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "VFSItems"."GridGroup_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> GridGroupLookupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)VFSItemFieldIndex.GridGroupLookupId, false); }
			set	{ SetValue((int)VFSItemFieldIndex.GridGroupLookupId, value); }
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
		public virtual LookupEntity GridGroupLookup
		{
			get	{ return _gridGroupLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncGridGroupLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "GridGroupLookup", _gridGroupLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity GroupLookup
		{
			get	{ return _groupLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncGroupLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "GroupLookup", _groupLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity SectionLookup
		{
			get	{ return _sectionLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncSectionLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "SectionLookup", _sectionLookup, false); 
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

		/// <summary> Gets / sets related entity of type 'VFSEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual VFSEntity VFS
		{
			get	{ return _vFS; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncVFS(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "VFSItems", "VFS", _vFS, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'VFSItemSourceEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual VFSItemSourceEntity VFSItemSource
		{
			get	{ return _vFSItemSource; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncVFSItemSource(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "VFSItemSource", _vFSItemSource, false); 
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
			get { return (int)Vital.DataLayer.EntityType.VFSItemEntity; }
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
