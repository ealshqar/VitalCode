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
	/// <summary>Entity class which represents the entity 'Item'.<br/><br/></summary>
	[Serializable]
	public partial class ItemEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<FrequencyTestResultEntity> _frequencyTestResults;
		private EntityCollection<ItemPropertyEntity> _properties;
		private EntityCollection<ItemRelationEntity> _children;
		private EntityCollection<ItemRelationEntity> _parents;
		private EntityCollection<ItemTargetEntity> _itemTargets;
		private EntityCollection<ScheduleLineEntity> _scheduleLines;
		private EntityCollection<SpotCheckResultEntity> _spotCheckResults;
		private EntityCollection<TestResultEntity> _testResults;
		private ItemDetailsEntity _itemDetail;
		private LookupEntity _genderLookup;
		private LookupEntity _itemSourceLookup;
		private LookupEntity _listTypeLookup;
		private LookupEntity _typeLookup;
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
			/// <summary>Member name ItemDetail</summary>
			public static readonly string ItemDetail = "ItemDetail";
			/// <summary>Member name GenderLookup</summary>
			public static readonly string GenderLookup = "GenderLookup";
			/// <summary>Member name ItemSourceLookup</summary>
			public static readonly string ItemSourceLookup = "ItemSourceLookup";
			/// <summary>Member name ListTypeLookup</summary>
			public static readonly string ListTypeLookup = "ListTypeLookup";
			/// <summary>Member name TypeLookup</summary>
			public static readonly string TypeLookup = "TypeLookup";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name FrequencyTestResults</summary>
			public static readonly string FrequencyTestResults = "FrequencyTestResults";
			/// <summary>Member name Properties</summary>
			public static readonly string Properties = "Properties";
			/// <summary>Member name Children</summary>
			public static readonly string Children = "Children";
			/// <summary>Member name Parents</summary>
			public static readonly string Parents = "Parents";
			/// <summary>Member name ItemTargets</summary>
			public static readonly string ItemTargets = "ItemTargets";
			/// <summary>Member name ScheduleLines</summary>
			public static readonly string ScheduleLines = "ScheduleLines";
			/// <summary>Member name SpotCheckResults</summary>
			public static readonly string SpotCheckResults = "SpotCheckResults";
			/// <summary>Member name TestResults</summary>
			public static readonly string TestResults = "TestResults";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static ItemEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public ItemEntity():base("ItemEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ItemEntity(IEntityFields2 fields):base("ItemEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ItemEntity</param>
		public ItemEntity(IValidator validator):base("ItemEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for Item which data should be fetched into this Item object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ItemEntity(System.Int32 id):base("ItemEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Item which data should be fetched into this Item object</param>
		/// <param name="validator">The custom validator object for this ItemEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ItemEntity(System.Int32 id, IValidator validator):base("ItemEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected ItemEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_frequencyTestResults = (EntityCollection<FrequencyTestResultEntity>)info.GetValue("_frequencyTestResults", typeof(EntityCollection<FrequencyTestResultEntity>));
				_properties = (EntityCollection<ItemPropertyEntity>)info.GetValue("_properties", typeof(EntityCollection<ItemPropertyEntity>));
				_children = (EntityCollection<ItemRelationEntity>)info.GetValue("_children", typeof(EntityCollection<ItemRelationEntity>));
				_parents = (EntityCollection<ItemRelationEntity>)info.GetValue("_parents", typeof(EntityCollection<ItemRelationEntity>));
				_itemTargets = (EntityCollection<ItemTargetEntity>)info.GetValue("_itemTargets", typeof(EntityCollection<ItemTargetEntity>));
				_scheduleLines = (EntityCollection<ScheduleLineEntity>)info.GetValue("_scheduleLines", typeof(EntityCollection<ScheduleLineEntity>));
				_spotCheckResults = (EntityCollection<SpotCheckResultEntity>)info.GetValue("_spotCheckResults", typeof(EntityCollection<SpotCheckResultEntity>));
				_testResults = (EntityCollection<TestResultEntity>)info.GetValue("_testResults", typeof(EntityCollection<TestResultEntity>));
				_itemDetail = (ItemDetailsEntity)info.GetValue("_itemDetail", typeof(ItemDetailsEntity));
				if(_itemDetail!=null)
				{
					_itemDetail.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_genderLookup = (LookupEntity)info.GetValue("_genderLookup", typeof(LookupEntity));
				if(_genderLookup!=null)
				{
					_genderLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_itemSourceLookup = (LookupEntity)info.GetValue("_itemSourceLookup", typeof(LookupEntity));
				if(_itemSourceLookup!=null)
				{
					_itemSourceLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_listTypeLookup = (LookupEntity)info.GetValue("_listTypeLookup", typeof(LookupEntity));
				if(_listTypeLookup!=null)
				{
					_listTypeLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_typeLookup = (LookupEntity)info.GetValue("_typeLookup", typeof(LookupEntity));
				if(_typeLookup!=null)
				{
					_typeLookup.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((ItemFieldIndex)fieldIndex)
			{
				case ItemFieldIndex.GenderLookupId:
					DesetupSyncGenderLookup(true, false);
					break;
				case ItemFieldIndex.TypeLookupId:
					DesetupSyncTypeLookup(true, false);
					break;
				case ItemFieldIndex.ListTypeLookupId:
					DesetupSyncListTypeLookup(true, false);
					break;
				case ItemFieldIndex.ItemDetailId:
					DesetupSyncItemDetail(true, false);
					break;
				case ItemFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case ItemFieldIndex.ItemSourceLookupId:
					DesetupSyncItemSourceLookup(true, false);
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
				case "ItemDetail":
					this.ItemDetail = (ItemDetailsEntity)entity;
					break;
				case "GenderLookup":
					this.GenderLookup = (LookupEntity)entity;
					break;
				case "ItemSourceLookup":
					this.ItemSourceLookup = (LookupEntity)entity;
					break;
				case "ListTypeLookup":
					this.ListTypeLookup = (LookupEntity)entity;
					break;
				case "TypeLookup":
					this.TypeLookup = (LookupEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "FrequencyTestResults":
					this.FrequencyTestResults.Add((FrequencyTestResultEntity)entity);
					break;
				case "Properties":
					this.Properties.Add((ItemPropertyEntity)entity);
					break;
				case "Children":
					this.Children.Add((ItemRelationEntity)entity);
					break;
				case "Parents":
					this.Parents.Add((ItemRelationEntity)entity);
					break;
				case "ItemTargets":
					this.ItemTargets.Add((ItemTargetEntity)entity);
					break;
				case "ScheduleLines":
					this.ScheduleLines.Add((ScheduleLineEntity)entity);
					break;
				case "SpotCheckResults":
					this.SpotCheckResults.Add((SpotCheckResultEntity)entity);
					break;
				case "TestResults":
					this.TestResults.Add((TestResultEntity)entity);
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
				case "ItemDetail":
					toReturn.Add(Relations.ItemDetailsEntityUsingItemDetailId);
					break;
				case "GenderLookup":
					toReturn.Add(Relations.LookupEntityUsingGenderLookupId);
					break;
				case "ItemSourceLookup":
					toReturn.Add(Relations.LookupEntityUsingItemSourceLookupId);
					break;
				case "ListTypeLookup":
					toReturn.Add(Relations.LookupEntityUsingListTypeLookupId);
					break;
				case "TypeLookup":
					toReturn.Add(Relations.LookupEntityUsingTypeLookupId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "FrequencyTestResults":
					toReturn.Add(Relations.FrequencyTestResultEntityUsingItemId);
					break;
				case "Properties":
					toReturn.Add(Relations.ItemPropertyEntityUsingItemId);
					break;
				case "Children":
					toReturn.Add(Relations.ItemRelationEntityUsingItemChildId);
					break;
				case "Parents":
					toReturn.Add(Relations.ItemRelationEntityUsingItemParentId);
					break;
				case "ItemTargets":
					toReturn.Add(Relations.ItemTargetEntityUsingItemId);
					break;
				case "ScheduleLines":
					toReturn.Add(Relations.ScheduleLineEntityUsingItemId);
					break;
				case "SpotCheckResults":
					toReturn.Add(Relations.SpotCheckResultEntityUsingItemId);
					break;
				case "TestResults":
					toReturn.Add(Relations.TestResultEntityUsingItemRatioId);
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
			int numberOfOneWayRelations = 0+1+1+1+1+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
				case "GenderLookup":
					return true;
				case "ItemSourceLookup":
					return true;
				case "ListTypeLookup":
					return true;
				case "TypeLookup":
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
				case "ItemDetail":
					SetupSyncItemDetail(relatedEntity);
					break;
				case "GenderLookup":
					SetupSyncGenderLookup(relatedEntity);
					break;
				case "ItemSourceLookup":
					SetupSyncItemSourceLookup(relatedEntity);
					break;
				case "ListTypeLookup":
					SetupSyncListTypeLookup(relatedEntity);
					break;
				case "TypeLookup":
					SetupSyncTypeLookup(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "FrequencyTestResults":
					this.FrequencyTestResults.Add((FrequencyTestResultEntity)relatedEntity);
					break;
				case "Properties":
					this.Properties.Add((ItemPropertyEntity)relatedEntity);
					break;
				case "Children":
					this.Children.Add((ItemRelationEntity)relatedEntity);
					break;
				case "Parents":
					this.Parents.Add((ItemRelationEntity)relatedEntity);
					break;
				case "ItemTargets":
					this.ItemTargets.Add((ItemTargetEntity)relatedEntity);
					break;
				case "ScheduleLines":
					this.ScheduleLines.Add((ScheduleLineEntity)relatedEntity);
					break;
				case "SpotCheckResults":
					this.SpotCheckResults.Add((SpotCheckResultEntity)relatedEntity);
					break;
				case "TestResults":
					this.TestResults.Add((TestResultEntity)relatedEntity);
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
				case "ItemDetail":
					DesetupSyncItemDetail(false, true);
					break;
				case "GenderLookup":
					DesetupSyncGenderLookup(false, true);
					break;
				case "ItemSourceLookup":
					DesetupSyncItemSourceLookup(false, true);
					break;
				case "ListTypeLookup":
					DesetupSyncListTypeLookup(false, true);
					break;
				case "TypeLookup":
					DesetupSyncTypeLookup(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "FrequencyTestResults":
					this.PerformRelatedEntityRemoval(this.FrequencyTestResults, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Properties":
					this.PerformRelatedEntityRemoval(this.Properties, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Children":
					this.PerformRelatedEntityRemoval(this.Children, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Parents":
					this.PerformRelatedEntityRemoval(this.Parents, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "ItemTargets":
					this.PerformRelatedEntityRemoval(this.ItemTargets, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "ScheduleLines":
					this.PerformRelatedEntityRemoval(this.ScheduleLines, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "SpotCheckResults":
					this.PerformRelatedEntityRemoval(this.SpotCheckResults, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "TestResults":
					this.PerformRelatedEntityRemoval(this.TestResults, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_itemDetail!=null)
			{
				toReturn.Add(_itemDetail);
			}
			if(_genderLookup!=null)
			{
				toReturn.Add(_genderLookup);
			}
			if(_itemSourceLookup!=null)
			{
				toReturn.Add(_itemSourceLookup);
			}
			if(_listTypeLookup!=null)
			{
				toReturn.Add(_listTypeLookup);
			}
			if(_typeLookup!=null)
			{
				toReturn.Add(_typeLookup);
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
			toReturn.Add(this.FrequencyTestResults);
			toReturn.Add(this.Properties);
			toReturn.Add(this.Children);
			toReturn.Add(this.Parents);
			toReturn.Add(this.ItemTargets);
			toReturn.Add(this.ScheduleLines);
			toReturn.Add(this.SpotCheckResults);
			toReturn.Add(this.TestResults);
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
				info.AddValue("_frequencyTestResults", ((_frequencyTestResults!=null) && (_frequencyTestResults.Count>0) && !this.MarkedForDeletion)?_frequencyTestResults:null);
				info.AddValue("_properties", ((_properties!=null) && (_properties.Count>0) && !this.MarkedForDeletion)?_properties:null);
				info.AddValue("_children", ((_children!=null) && (_children.Count>0) && !this.MarkedForDeletion)?_children:null);
				info.AddValue("_parents", ((_parents!=null) && (_parents.Count>0) && !this.MarkedForDeletion)?_parents:null);
				info.AddValue("_itemTargets", ((_itemTargets!=null) && (_itemTargets.Count>0) && !this.MarkedForDeletion)?_itemTargets:null);
				info.AddValue("_scheduleLines", ((_scheduleLines!=null) && (_scheduleLines.Count>0) && !this.MarkedForDeletion)?_scheduleLines:null);
				info.AddValue("_spotCheckResults", ((_spotCheckResults!=null) && (_spotCheckResults.Count>0) && !this.MarkedForDeletion)?_spotCheckResults:null);
				info.AddValue("_testResults", ((_testResults!=null) && (_testResults.Count>0) && !this.MarkedForDeletion)?_testResults:null);
				info.AddValue("_itemDetail", (!this.MarkedForDeletion?_itemDetail:null));
				info.AddValue("_genderLookup", (!this.MarkedForDeletion?_genderLookup:null));
				info.AddValue("_itemSourceLookup", (!this.MarkedForDeletion?_itemSourceLookup:null));
				info.AddValue("_listTypeLookup", (!this.MarkedForDeletion?_listTypeLookup:null));
				info.AddValue("_typeLookup", (!this.MarkedForDeletion?_typeLookup:null));
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
			return new ItemRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'FrequencyTestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoFrequencyTestResults()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(FrequencyTestResultFields.ItemId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ItemProperty' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProperties()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemPropertyFields.ItemId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ItemRelation' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoChildren()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemRelationFields.ItemChildId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ItemRelation' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoParents()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemRelationFields.ItemParentId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ItemTarget' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItemTargets()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemTargetFields.ItemId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ScheduleLine' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoScheduleLines()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ScheduleLineFields.ItemId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SpotCheckResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSpotCheckResults()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(SpotCheckResultFields.ItemId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestResults()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestResultFields.ItemRatioId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'ItemDetails' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItemDetail()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemDetailsFields.Id, null, ComparisonOperator.Equal, this.ItemDetailId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoGenderLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.GenderLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItemSourceLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ItemSourceLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoListTypeLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ListTypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTypeLookup()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.TypeLookupId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._frequencyTestResults);
			collectionsQueue.Enqueue(this._properties);
			collectionsQueue.Enqueue(this._children);
			collectionsQueue.Enqueue(this._parents);
			collectionsQueue.Enqueue(this._itemTargets);
			collectionsQueue.Enqueue(this._scheduleLines);
			collectionsQueue.Enqueue(this._spotCheckResults);
			collectionsQueue.Enqueue(this._testResults);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._frequencyTestResults = (EntityCollection<FrequencyTestResultEntity>) collectionsQueue.Dequeue();
			this._properties = (EntityCollection<ItemPropertyEntity>) collectionsQueue.Dequeue();
			this._children = (EntityCollection<ItemRelationEntity>) collectionsQueue.Dequeue();
			this._parents = (EntityCollection<ItemRelationEntity>) collectionsQueue.Dequeue();
			this._itemTargets = (EntityCollection<ItemTargetEntity>) collectionsQueue.Dequeue();
			this._scheduleLines = (EntityCollection<ScheduleLineEntity>) collectionsQueue.Dequeue();
			this._spotCheckResults = (EntityCollection<SpotCheckResultEntity>) collectionsQueue.Dequeue();
			this._testResults = (EntityCollection<TestResultEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._frequencyTestResults != null);
			toReturn |=(this._properties != null);
			toReturn |=(this._children != null);
			toReturn |=(this._parents != null);
			toReturn |=(this._itemTargets != null);
			toReturn |=(this._scheduleLines != null);
			toReturn |=(this._spotCheckResults != null);
			toReturn |=(this._testResults != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<FrequencyTestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ItemPropertyEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemPropertyEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemRelationEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemRelationEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ItemTargetEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemTargetEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ScheduleLineEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ScheduleLineEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<SpotCheckResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("ItemDetail", _itemDetail);
			toReturn.Add("GenderLookup", _genderLookup);
			toReturn.Add("ItemSourceLookup", _itemSourceLookup);
			toReturn.Add("ListTypeLookup", _listTypeLookup);
			toReturn.Add("TypeLookup", _typeLookup);
			toReturn.Add("User", _user);
			toReturn.Add("FrequencyTestResults", _frequencyTestResults);
			toReturn.Add("Properties", _properties);
			toReturn.Add("Children", _children);
			toReturn.Add("Parents", _parents);
			toReturn.Add("ItemTargets", _itemTargets);
			toReturn.Add("ScheduleLines", _scheduleLines);
			toReturn.Add("SpotCheckResults", _spotCheckResults);
			toReturn.Add("TestResults", _testResults);
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
			_fieldsCustomProperties.Add("FullName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Description", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemMemo", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("GenderLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ListTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemDetailId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Order", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemCsabinaryCode", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsStarred", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemSourceLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Key", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _itemDetail</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItemDetail(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _itemDetail, new PropertyChangedEventHandler( OnItemDetailPropertyChanged ), "ItemDetail", Vital.DataLayer.RelationClasses.StaticItemRelations.ItemDetailsEntityUsingItemDetailIdStatic, true, signalRelatedEntity, "Item", resetFKFields, new int[] { (int)ItemFieldIndex.ItemDetailId } );
			_itemDetail = null;
		}

		/// <summary> setups the sync logic for member _itemDetail</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncItemDetail(IEntityCore relatedEntity)
		{
			if(_itemDetail!=relatedEntity)
			{
				DesetupSyncItemDetail(true, true);
				_itemDetail = (ItemDetailsEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _itemDetail, new PropertyChangedEventHandler( OnItemDetailPropertyChanged ), "ItemDetail", Vital.DataLayer.RelationClasses.StaticItemRelations.ItemDetailsEntityUsingItemDetailIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnItemDetailPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _genderLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncGenderLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _genderLookup, new PropertyChangedEventHandler( OnGenderLookupPropertyChanged ), "GenderLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingGenderLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ItemFieldIndex.GenderLookupId } );
			_genderLookup = null;
		}

		/// <summary> setups the sync logic for member _genderLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncGenderLookup(IEntityCore relatedEntity)
		{
			if(_genderLookup!=relatedEntity)
			{
				DesetupSyncGenderLookup(true, true);
				_genderLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _genderLookup, new PropertyChangedEventHandler( OnGenderLookupPropertyChanged ), "GenderLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingGenderLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGenderLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _itemSourceLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItemSourceLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _itemSourceLookup, new PropertyChangedEventHandler( OnItemSourceLookupPropertyChanged ), "ItemSourceLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingItemSourceLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ItemFieldIndex.ItemSourceLookupId } );
			_itemSourceLookup = null;
		}

		/// <summary> setups the sync logic for member _itemSourceLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncItemSourceLookup(IEntityCore relatedEntity)
		{
			if(_itemSourceLookup!=relatedEntity)
			{
				DesetupSyncItemSourceLookup(true, true);
				_itemSourceLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _itemSourceLookup, new PropertyChangedEventHandler( OnItemSourceLookupPropertyChanged ), "ItemSourceLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingItemSourceLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnItemSourceLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _listTypeLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncListTypeLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _listTypeLookup, new PropertyChangedEventHandler( OnListTypeLookupPropertyChanged ), "ListTypeLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingListTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ItemFieldIndex.ListTypeLookupId } );
			_listTypeLookup = null;
		}

		/// <summary> setups the sync logic for member _listTypeLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncListTypeLookup(IEntityCore relatedEntity)
		{
			if(_listTypeLookup!=relatedEntity)
			{
				DesetupSyncListTypeLookup(true, true);
				_listTypeLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _listTypeLookup, new PropertyChangedEventHandler( OnListTypeLookupPropertyChanged ), "ListTypeLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingListTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnListTypeLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _typeLookup</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTypeLookup(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _typeLookup, new PropertyChangedEventHandler( OnTypeLookupPropertyChanged ), "TypeLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ItemFieldIndex.TypeLookupId } );
			_typeLookup = null;
		}

		/// <summary> setups the sync logic for member _typeLookup</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTypeLookup(IEntityCore relatedEntity)
		{
			if(_typeLookup!=relatedEntity)
			{
				DesetupSyncTypeLookup(true, true);
				_typeLookup = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _typeLookup, new PropertyChangedEventHandler( OnTypeLookupPropertyChanged ), "TypeLookup", Vital.DataLayer.RelationClasses.StaticItemRelations.LookupEntityUsingTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTypeLookupPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticItemRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ItemFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticItemRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this ItemEntity</param>
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
		public  static ItemRelations Relations
		{
			get	{ return new ItemRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'FrequencyTestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathFrequencyTestResults
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<FrequencyTestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(FrequencyTestResultEntityFactory))), (IEntityRelation)GetRelationsForField("FrequencyTestResults")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.FrequencyTestResultEntity, 0, null, null, null, null, "FrequencyTestResults", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ItemProperty' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProperties
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ItemPropertyEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemPropertyEntityFactory))), (IEntityRelation)GetRelationsForField("Properties")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.ItemPropertyEntity, 0, null, null, null, null, "Properties", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ItemRelation' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathChildren
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemRelationEntityFactory))), (IEntityRelation)GetRelationsForField("Children")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.ItemRelationEntity, 0, null, null, null, null, "Children", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ItemRelation' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathParents
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemRelationEntityFactory))), (IEntityRelation)GetRelationsForField("Parents")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.ItemRelationEntity, 0, null, null, null, null, "Parents", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ItemTarget' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathItemTargets
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ItemTargetEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ItemTargetEntityFactory))), (IEntityRelation)GetRelationsForField("ItemTargets")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.ItemTargetEntity, 0, null, null, null, null, "ItemTargets", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ScheduleLine' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathScheduleLines
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ScheduleLineEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ScheduleLineEntityFactory))), (IEntityRelation)GetRelationsForField("ScheduleLines")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.ScheduleLineEntity, 0, null, null, null, null, "ScheduleLines", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SpotCheckResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSpotCheckResults
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<SpotCheckResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(SpotCheckResultEntityFactory))), (IEntityRelation)GetRelationsForField("SpotCheckResults")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.SpotCheckResultEntity, 0, null, null, null, null, "SpotCheckResults", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestResults
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestResultEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory))), (IEntityRelation)GetRelationsForField("TestResults")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.TestResultEntity, 0, null, null, null, null, "TestResults", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ItemDetails' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathItemDetail
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemDetailsEntityFactory))),	(IEntityRelation)GetRelationsForField("ItemDetail")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.ItemDetailsEntity, 0, null, null, null, null, "ItemDetail", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathGenderLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("GenderLookup")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "GenderLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathItemSourceLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ItemSourceLookup")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ItemSourceLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathListTypeLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ListTypeLookup")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ListTypeLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTypeLookup
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("TypeLookup")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "TypeLookup", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.ItemEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)ItemFieldIndex.Id, true); }
			set	{ SetValue((int)ItemFieldIndex.Id, value); }
		}

		/// <summary> The Name property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Name"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)ItemFieldIndex.Name, true); }
			set	{ SetValue((int)ItemFieldIndex.Name, value); }
		}

		/// <summary> The FullName property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_FullName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String FullName
		{
			get { return (System.String)GetValue((int)ItemFieldIndex.FullName, true); }
			set	{ SetValue((int)ItemFieldIndex.FullName, value); }
		}

		/// <summary> The Description property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Description"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Description
		{
			get { return (System.String)GetValue((int)ItemFieldIndex.Description, true); }
			set	{ SetValue((int)ItemFieldIndex.Description, value); }
		}

		/// <summary> The ItemMemo property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Memo"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String ItemMemo
		{
			get { return (System.String)GetValue((int)ItemFieldIndex.ItemMemo, true); }
			set	{ SetValue((int)ItemFieldIndex.ItemMemo, value); }
		}

		/// <summary> The GenderLookupId property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Gender_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 GenderLookupId
		{
			get { return (System.Int32)GetValue((int)ItemFieldIndex.GenderLookupId, true); }
			set	{ SetValue((int)ItemFieldIndex.GenderLookupId, value); }
		}

		/// <summary> The TypeLookupId property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Type_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 TypeLookupId
		{
			get { return (System.Int32)GetValue((int)ItemFieldIndex.TypeLookupId, true); }
			set	{ SetValue((int)ItemFieldIndex.TypeLookupId, value); }
		}

		/// <summary> The ListTypeLookupId property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."ListType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ListTypeLookupId
		{
			get { return (System.Int32)GetValue((int)ItemFieldIndex.ListTypeLookupId, true); }
			set	{ SetValue((int)ItemFieldIndex.ListTypeLookupId, value); }
		}

		/// <summary> The ItemDetailId property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Detail_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ItemDetailId
		{
			get { return (Nullable<System.Int32>)GetValue((int)ItemFieldIndex.ItemDetailId, false); }
			set	{ SetValue((int)ItemFieldIndex.ItemDetailId, value); }
		}

		/// <summary> The Order property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Order"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Order
		{
			get { return (Nullable<System.Int32>)GetValue((int)ItemFieldIndex.Order, false); }
			set	{ SetValue((int)ItemFieldIndex.Order, value); }
		}

		/// <summary> The UserId property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)ItemFieldIndex.UserId, true); }
			set	{ SetValue((int)ItemFieldIndex.UserId, value); }
		}

		/// <summary> The CreationDateTime property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)ItemFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)ItemFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)ItemFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)ItemFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The ItemCsabinaryCode property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_CSABinaryCode"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ItemCsabinaryCode
		{
			get { return (Nullable<System.Int32>)GetValue((int)ItemFieldIndex.ItemCsabinaryCode, false); }
			set	{ SetValue((int)ItemFieldIndex.ItemCsabinaryCode, value); }
		}

		/// <summary> The IsStarred property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_IsStarred"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> IsStarred
		{
			get { return (Nullable<System.Boolean>)GetValue((int)ItemFieldIndex.IsStarred, false); }
			set	{ SetValue((int)ItemFieldIndex.IsStarred, value); }
		}

		/// <summary> The ItemSourceLookupId property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."ItemSource_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ItemSourceLookupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)ItemFieldIndex.ItemSourceLookupId, false); }
			set	{ SetValue((int)ItemFieldIndex.ItemSourceLookupId, value); }
		}

		/// <summary> The Key property of the Entity Item<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Items"."Item_Key"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 100<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Key
		{
			get { return (System.String)GetValue((int)ItemFieldIndex.Key, true); }
			set	{ SetValue((int)ItemFieldIndex.Key, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'FrequencyTestResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(FrequencyTestResultEntity))]
		public virtual EntityCollection<FrequencyTestResultEntity> FrequencyTestResults
		{
			get { return GetOrCreateEntityCollection<FrequencyTestResultEntity, FrequencyTestResultEntityFactory>("Item", true, false, ref _frequencyTestResults);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ItemPropertyEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ItemPropertyEntity))]
		public virtual EntityCollection<ItemPropertyEntity> Properties
		{
			get { return GetOrCreateEntityCollection<ItemPropertyEntity, ItemPropertyEntityFactory>("Item", true, false, ref _properties);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ItemRelationEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ItemRelationEntity))]
		public virtual EntityCollection<ItemRelationEntity> Children
		{
			get { return GetOrCreateEntityCollection<ItemRelationEntity, ItemRelationEntityFactory>("Child", true, false, ref _children);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ItemRelationEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ItemRelationEntity))]
		public virtual EntityCollection<ItemRelationEntity> Parents
		{
			get { return GetOrCreateEntityCollection<ItemRelationEntity, ItemRelationEntityFactory>("Parent", true, false, ref _parents);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ItemTargetEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ItemTargetEntity))]
		public virtual EntityCollection<ItemTargetEntity> ItemTargets
		{
			get { return GetOrCreateEntityCollection<ItemTargetEntity, ItemTargetEntityFactory>("Item", true, false, ref _itemTargets);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ScheduleLineEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ScheduleLineEntity))]
		public virtual EntityCollection<ScheduleLineEntity> ScheduleLines
		{
			get { return GetOrCreateEntityCollection<ScheduleLineEntity, ScheduleLineEntityFactory>("Item", true, false, ref _scheduleLines);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'SpotCheckResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SpotCheckResultEntity))]
		public virtual EntityCollection<SpotCheckResultEntity> SpotCheckResults
		{
			get { return GetOrCreateEntityCollection<SpotCheckResultEntity, SpotCheckResultEntityFactory>("Item", true, false, ref _spotCheckResults);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestResultEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestResultEntity))]
		public virtual EntityCollection<TestResultEntity> TestResults
		{
			get { return GetOrCreateEntityCollection<TestResultEntity, TestResultEntityFactory>("RatioItem", true, false, ref _testResults);	}
		}

		/// <summary> Gets / sets related entity of type 'ItemDetailsEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ItemDetailsEntity ItemDetail
		{
			get	{ return _itemDetail; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncItemDetail(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "Item", "ItemDetail", _itemDetail, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity GenderLookup
		{
			get	{ return _genderLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncGenderLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "GenderLookup", _genderLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ItemSourceLookup
		{
			get	{ return _itemSourceLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncItemSourceLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "ItemSourceLookup", _itemSourceLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ListTypeLookup
		{
			get	{ return _listTypeLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncListTypeLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "ListTypeLookup", _listTypeLookup, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity TypeLookup
		{
			get	{ return _typeLookup; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTypeLookup(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "TypeLookup", _typeLookup, false); 
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
			get { return (int)Vital.DataLayer.EntityType.ItemEntity; }
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
