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
	/// <summary>Entity class which represents the entity 'TestResult'.<br/><br/></summary>
	[Serializable]
	public partial class TestResultEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<TestResultFactorsEntity> _testResultFactors;
		private ItemEntity _item;
		private ItemEntity _ratioItem;
		private ItemEntity _vitalForce;
		private LookupEntity _stepType;
		private TestIssueEntity _testIssue;
		private TestProtocolEntity _testProtocol;
		private TestResultEntity _parent;
		private TestResultEntity _selectedParent;
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
			/// <summary>Member name RatioItem</summary>
			public static readonly string RatioItem = "RatioItem";
			/// <summary>Member name VitalForce</summary>
			public static readonly string VitalForce = "VitalForce";
			/// <summary>Member name StepType</summary>
			public static readonly string StepType = "StepType";
			/// <summary>Member name TestIssue</summary>
			public static readonly string TestIssue = "TestIssue";
			/// <summary>Member name TestProtocol</summary>
			public static readonly string TestProtocol = "TestProtocol";
			/// <summary>Member name Parent</summary>
			public static readonly string Parent = "Parent";
			/// <summary>Member name SelectedParent</summary>
			public static readonly string SelectedParent = "SelectedParent";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name TestResultFactors</summary>
			public static readonly string TestResultFactors = "TestResultFactors";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static TestResultEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public TestResultEntity():base("TestResultEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public TestResultEntity(IEntityFields2 fields):base("TestResultEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this TestResultEntity</param>
		public TestResultEntity(IValidator validator):base("TestResultEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for TestResult which data should be fetched into this TestResult object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public TestResultEntity(System.Int32 id):base("TestResultEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for TestResult which data should be fetched into this TestResult object</param>
		/// <param name="validator">The custom validator object for this TestResultEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public TestResultEntity(System.Int32 id, IValidator validator):base("TestResultEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected TestResultEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_testResultFactors = (EntityCollection<TestResultFactorsEntity>)info.GetValue("_testResultFactors", typeof(EntityCollection<TestResultFactorsEntity>));
				_item = (ItemEntity)info.GetValue("_item", typeof(ItemEntity));
				if(_item!=null)
				{
					_item.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_ratioItem = (ItemEntity)info.GetValue("_ratioItem", typeof(ItemEntity));
				if(_ratioItem!=null)
				{
					_ratioItem.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_vitalForce = (ItemEntity)info.GetValue("_vitalForce", typeof(ItemEntity));
				if(_vitalForce!=null)
				{
					_vitalForce.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_stepType = (LookupEntity)info.GetValue("_stepType", typeof(LookupEntity));
				if(_stepType!=null)
				{
					_stepType.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_testIssue = (TestIssueEntity)info.GetValue("_testIssue", typeof(TestIssueEntity));
				if(_testIssue!=null)
				{
					_testIssue.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_testProtocol = (TestProtocolEntity)info.GetValue("_testProtocol", typeof(TestProtocolEntity));
				if(_testProtocol!=null)
				{
					_testProtocol.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_parent = (TestResultEntity)info.GetValue("_parent", typeof(TestResultEntity));
				if(_parent!=null)
				{
					_parent.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_selectedParent = (TestResultEntity)info.GetValue("_selectedParent", typeof(TestResultEntity));
				if(_selectedParent!=null)
				{
					_selectedParent.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((TestResultFieldIndex)fieldIndex)
			{
				case TestResultFieldIndex.IssueId:
					DesetupSyncTestIssue(true, false);
					break;
				case TestResultFieldIndex.ItemId:
					DesetupSyncItem(true, false);
					break;
				case TestResultFieldIndex.ParentId:
					DesetupSyncParent(true, false);
					break;
				case TestResultFieldIndex.SelectedParentId:
					DesetupSyncSelectedParent(true, false);
					break;
				case TestResultFieldIndex.VitalForceId:
					DesetupSyncVitalForce(true, false);
					break;
				case TestResultFieldIndex.StepTypeLookupId:
					DesetupSyncStepType(true, false);
					break;
				case TestResultFieldIndex.TestProtocolId:
					DesetupSyncTestProtocol(true, false);
					break;
				case TestResultFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case TestResultFieldIndex.ItemRatioId:
					DesetupSyncRatioItem(true, false);
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
				case "RatioItem":
					this.RatioItem = (ItemEntity)entity;
					break;
				case "VitalForce":
					this.VitalForce = (ItemEntity)entity;
					break;
				case "StepType":
					this.StepType = (LookupEntity)entity;
					break;
				case "TestIssue":
					this.TestIssue = (TestIssueEntity)entity;
					break;
				case "TestProtocol":
					this.TestProtocol = (TestProtocolEntity)entity;
					break;
				case "Parent":
					this.Parent = (TestResultEntity)entity;
					break;
				case "SelectedParent":
					this.SelectedParent = (TestResultEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "TestResultFactors":
					this.TestResultFactors.Add((TestResultFactorsEntity)entity);
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
				case "RatioItem":
					toReturn.Add(Relations.ItemEntityUsingItemRatioId);
					break;
				case "VitalForce":
					toReturn.Add(Relations.ItemEntityUsingVitalForceId);
					break;
				case "StepType":
					toReturn.Add(Relations.LookupEntityUsingStepTypeLookupId);
					break;
				case "TestIssue":
					toReturn.Add(Relations.TestIssueEntityUsingIssueId);
					break;
				case "TestProtocol":
					toReturn.Add(Relations.TestProtocolEntityUsingTestProtocolId);
					break;
				case "Parent":
					toReturn.Add(Relations.TestResultEntityUsingIdParentId);
					break;
				case "SelectedParent":
					toReturn.Add(Relations.TestResultEntityUsingIdSelectedParentId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "TestResultFactors":
					toReturn.Add(Relations.TestResultFactorsEntityUsingTestResultId);
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
				case "Item":
					return true;
				case "VitalForce":
					return true;
				case "Parent":
					return true;
				case "SelectedParent":
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
				case "Item":
					SetupSyncItem(relatedEntity);
					break;
				case "RatioItem":
					SetupSyncRatioItem(relatedEntity);
					break;
				case "VitalForce":
					SetupSyncVitalForce(relatedEntity);
					break;
				case "StepType":
					SetupSyncStepType(relatedEntity);
					break;
				case "TestIssue":
					SetupSyncTestIssue(relatedEntity);
					break;
				case "TestProtocol":
					SetupSyncTestProtocol(relatedEntity);
					break;
				case "Parent":
					SetupSyncParent(relatedEntity);
					break;
				case "SelectedParent":
					SetupSyncSelectedParent(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "TestResultFactors":
					this.TestResultFactors.Add((TestResultFactorsEntity)relatedEntity);
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
				case "RatioItem":
					DesetupSyncRatioItem(false, true);
					break;
				case "VitalForce":
					DesetupSyncVitalForce(false, true);
					break;
				case "StepType":
					DesetupSyncStepType(false, true);
					break;
				case "TestIssue":
					DesetupSyncTestIssue(false, true);
					break;
				case "TestProtocol":
					DesetupSyncTestProtocol(false, true);
					break;
				case "Parent":
					DesetupSyncParent(false, true);
					break;
				case "SelectedParent":
					DesetupSyncSelectedParent(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "TestResultFactors":
					this.PerformRelatedEntityRemoval(this.TestResultFactors, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_ratioItem!=null)
			{
				toReturn.Add(_ratioItem);
			}
			if(_vitalForce!=null)
			{
				toReturn.Add(_vitalForce);
			}
			if(_stepType!=null)
			{
				toReturn.Add(_stepType);
			}
			if(_testIssue!=null)
			{
				toReturn.Add(_testIssue);
			}
			if(_testProtocol!=null)
			{
				toReturn.Add(_testProtocol);
			}
			if(_parent!=null)
			{
				toReturn.Add(_parent);
			}
			if(_selectedParent!=null)
			{
				toReturn.Add(_selectedParent);
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
			toReturn.Add(this.TestResultFactors);
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
				info.AddValue("_testResultFactors", ((_testResultFactors!=null) && (_testResultFactors.Count>0) && !this.MarkedForDeletion)?_testResultFactors:null);
				info.AddValue("_item", (!this.MarkedForDeletion?_item:null));
				info.AddValue("_ratioItem", (!this.MarkedForDeletion?_ratioItem:null));
				info.AddValue("_vitalForce", (!this.MarkedForDeletion?_vitalForce:null));
				info.AddValue("_stepType", (!this.MarkedForDeletion?_stepType:null));
				info.AddValue("_testIssue", (!this.MarkedForDeletion?_testIssue:null));
				info.AddValue("_testProtocol", (!this.MarkedForDeletion?_testProtocol:null));
				info.AddValue("_parent", (!this.MarkedForDeletion?_parent:null));
				info.AddValue("_selectedParent", (!this.MarkedForDeletion?_selectedParent:null));
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
			return new TestResultRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TestResultFactors' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestResultFactors()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestResultFactorsFields.TestResultId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Item' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoItem()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemFields.Id, null, ComparisonOperator.Equal, this.ItemId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Item' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoRatioItem()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemFields.Id, null, ComparisonOperator.Equal, this.ItemRatioId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Item' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVitalForce()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ItemFields.Id, null, ComparisonOperator.Equal, this.VitalForceId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStepType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.StepTypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestIssue' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestIssue()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestIssueFields.Id, null, ComparisonOperator.Equal, this.IssueId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestProtocol' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestProtocol()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestProtocolFields.Id, null, ComparisonOperator.Equal, this.TestProtocolId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoParent()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestResultFields.Id, null, ComparisonOperator.Equal, this.ParentId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSelectedParent()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestResultFields.Id, null, ComparisonOperator.Equal, this.SelectedParentId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._testResultFactors);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._testResultFactors = (EntityCollection<TestResultFactorsEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._testResultFactors != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<TestResultFactorsEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestResultFactorsEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Item", _item);
			toReturn.Add("RatioItem", _ratioItem);
			toReturn.Add("VitalForce", _vitalForce);
			toReturn.Add("StepType", _stepType);
			toReturn.Add("TestIssue", _testIssue);
			toReturn.Add("TestProtocol", _testProtocol);
			toReturn.Add("Parent", _parent);
			toReturn.Add("SelectedParent", _selectedParent);
			toReturn.Add("User", _user);
			toReturn.Add("TestResultFactors", _testResultFactors);
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
			_fieldsCustomProperties.Add("IssueId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("DateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ParentId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SelectedParentId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("VitalForceId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("StepTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TestProtocolId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsSelected", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsCurrent", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsImprinted", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ItemRatioId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _item</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticTestResultRelations.ItemEntityUsingItemIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestResultFieldIndex.ItemId } );
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
				this.PerformSetupSyncRelatedEntity( _item, new PropertyChangedEventHandler( OnItemPropertyChanged ), "Item", Vital.DataLayer.RelationClasses.StaticTestResultRelations.ItemEntityUsingItemIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _ratioItem</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncRatioItem(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _ratioItem, new PropertyChangedEventHandler( OnRatioItemPropertyChanged ), "RatioItem", Vital.DataLayer.RelationClasses.StaticTestResultRelations.ItemEntityUsingItemRatioIdStatic, true, signalRelatedEntity, "TestResults", resetFKFields, new int[] { (int)TestResultFieldIndex.ItemRatioId } );
			_ratioItem = null;
		}

		/// <summary> setups the sync logic for member _ratioItem</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncRatioItem(IEntityCore relatedEntity)
		{
			if(_ratioItem!=relatedEntity)
			{
				DesetupSyncRatioItem(true, true);
				_ratioItem = (ItemEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _ratioItem, new PropertyChangedEventHandler( OnRatioItemPropertyChanged ), "RatioItem", Vital.DataLayer.RelationClasses.StaticTestResultRelations.ItemEntityUsingItemRatioIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnRatioItemPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _vitalForce</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncVitalForce(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _vitalForce, new PropertyChangedEventHandler( OnVitalForcePropertyChanged ), "VitalForce", Vital.DataLayer.RelationClasses.StaticTestResultRelations.ItemEntityUsingVitalForceIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestResultFieldIndex.VitalForceId } );
			_vitalForce = null;
		}

		/// <summary> setups the sync logic for member _vitalForce</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncVitalForce(IEntityCore relatedEntity)
		{
			if(_vitalForce!=relatedEntity)
			{
				DesetupSyncVitalForce(true, true);
				_vitalForce = (ItemEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _vitalForce, new PropertyChangedEventHandler( OnVitalForcePropertyChanged ), "VitalForce", Vital.DataLayer.RelationClasses.StaticTestResultRelations.ItemEntityUsingVitalForceIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnVitalForcePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _stepType</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncStepType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _stepType, new PropertyChangedEventHandler( OnStepTypePropertyChanged ), "StepType", Vital.DataLayer.RelationClasses.StaticTestResultRelations.LookupEntityUsingStepTypeLookupIdStatic, true, signalRelatedEntity, "TestResult", resetFKFields, new int[] { (int)TestResultFieldIndex.StepTypeLookupId } );
			_stepType = null;
		}

		/// <summary> setups the sync logic for member _stepType</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncStepType(IEntityCore relatedEntity)
		{
			if(_stepType!=relatedEntity)
			{
				DesetupSyncStepType(true, true);
				_stepType = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _stepType, new PropertyChangedEventHandler( OnStepTypePropertyChanged ), "StepType", Vital.DataLayer.RelationClasses.StaticTestResultRelations.LookupEntityUsingStepTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStepTypePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _testIssue</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTestIssue(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _testIssue, new PropertyChangedEventHandler( OnTestIssuePropertyChanged ), "TestIssue", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestIssueEntityUsingIssueIdStatic, true, signalRelatedEntity, "TestResults", resetFKFields, new int[] { (int)TestResultFieldIndex.IssueId } );
			_testIssue = null;
		}

		/// <summary> setups the sync logic for member _testIssue</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTestIssue(IEntityCore relatedEntity)
		{
			if(_testIssue!=relatedEntity)
			{
				DesetupSyncTestIssue(true, true);
				_testIssue = (TestIssueEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _testIssue, new PropertyChangedEventHandler( OnTestIssuePropertyChanged ), "TestIssue", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestIssueEntityUsingIssueIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTestIssuePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _testProtocol</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTestProtocol(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _testProtocol, new PropertyChangedEventHandler( OnTestProtocolPropertyChanged ), "TestProtocol", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestProtocolEntityUsingTestProtocolIdStatic, true, signalRelatedEntity, "TestResult", resetFKFields, new int[] { (int)TestResultFieldIndex.TestProtocolId } );
			_testProtocol = null;
		}

		/// <summary> setups the sync logic for member _testProtocol</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTestProtocol(IEntityCore relatedEntity)
		{
			if(_testProtocol!=relatedEntity)
			{
				DesetupSyncTestProtocol(true, true);
				_testProtocol = (TestProtocolEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _testProtocol, new PropertyChangedEventHandler( OnTestProtocolPropertyChanged ), "TestProtocol", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestProtocolEntityUsingTestProtocolIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTestProtocolPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _parent</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncParent(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _parent, new PropertyChangedEventHandler( OnParentPropertyChanged ), "Parent", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestResultEntityUsingIdParentIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestResultFieldIndex.ParentId } );
			_parent = null;
		}

		/// <summary> setups the sync logic for member _parent</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncParent(IEntityCore relatedEntity)
		{
			if(_parent!=relatedEntity)
			{
				DesetupSyncParent(true, true);
				_parent = (TestResultEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _parent, new PropertyChangedEventHandler( OnParentPropertyChanged ), "Parent", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestResultEntityUsingIdParentIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnParentPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _selectedParent</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncSelectedParent(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _selectedParent, new PropertyChangedEventHandler( OnSelectedParentPropertyChanged ), "SelectedParent", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestResultEntityUsingIdSelectedParentIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestResultFieldIndex.SelectedParentId } );
			_selectedParent = null;
		}

		/// <summary> setups the sync logic for member _selectedParent</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncSelectedParent(IEntityCore relatedEntity)
		{
			if(_selectedParent!=relatedEntity)
			{
				DesetupSyncSelectedParent(true, true);
				_selectedParent = (TestResultEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _selectedParent, new PropertyChangedEventHandler( OnSelectedParentPropertyChanged ), "SelectedParent", Vital.DataLayer.RelationClasses.StaticTestResultRelations.TestResultEntityUsingIdSelectedParentIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSelectedParentPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticTestResultRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)TestResultFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticTestResultRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this TestResultEntity</param>
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
		public  static TestResultRelations Relations
		{
			get	{ return new TestResultRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestResultFactors' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestResultFactors
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<TestResultFactorsEntity>(EntityFactoryCache2.GetEntityFactory(typeof(TestResultFactorsEntityFactory))), (IEntityRelation)GetRelationsForField("TestResultFactors")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.TestResultFactorsEntity, 0, null, null, null, null, "TestResultFactors", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Item' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathItem
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("Item")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "Item", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Item' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathRatioItem
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("RatioItem")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "RatioItem", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Item' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVitalForce
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ItemEntityFactory))),	(IEntityRelation)GetRelationsForField("VitalForce")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.ItemEntity, 0, null, null, null, null, "VitalForce", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStepType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("StepType")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "StepType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestIssue' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestIssue
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestIssueEntityFactory))),	(IEntityRelation)GetRelationsForField("TestIssue")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.TestIssueEntity, 0, null, null, null, null, "TestIssue", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestProtocol' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestProtocol
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestProtocolEntityFactory))),	(IEntityRelation)GetRelationsForField("TestProtocol")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.TestProtocolEntity, 0, null, null, null, null, "TestProtocol", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathParent
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory))),	(IEntityRelation)GetRelationsForField("Parent")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.TestResultEntity, 0, null, null, null, null, "Parent", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSelectedParent
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestResultEntityFactory))),	(IEntityRelation)GetRelationsForField("SelectedParent")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.TestResultEntity, 0, null, null, null, null, "SelectedParent", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.TestResultEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)TestResultFieldIndex.Id, true); }
			set	{ SetValue((int)TestResultFieldIndex.Id, value); }
		}

		/// <summary> The IssueId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Issue_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> IssueId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.IssueId, false); }
			set	{ SetValue((int)TestResultFieldIndex.IssueId, value); }
		}

		/// <summary> The ItemId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Item_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ItemId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.ItemId, false); }
			set	{ SetValue((int)TestResultFieldIndex.ItemId, value); }
		}

		/// <summary> The DateTime property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_DateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime DateTime
		{
			get { return (System.DateTime)GetValue((int)TestResultFieldIndex.DateTime, true); }
			set	{ SetValue((int)TestResultFieldIndex.DateTime, value); }
		}

		/// <summary> The ParentId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_Parent_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ParentId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.ParentId, false); }
			set	{ SetValue((int)TestResultFieldIndex.ParentId, value); }
		}

		/// <summary> The SelectedParentId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_Selected_Parent_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> SelectedParentId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.SelectedParentId, false); }
			set	{ SetValue((int)TestResultFieldIndex.SelectedParentId, value); }
		}

		/// <summary> The VitalForceId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Item_Vital_Force_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> VitalForceId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.VitalForceId, false); }
			set	{ SetValue((int)TestResultFieldIndex.VitalForceId, value); }
		}

		/// <summary> The StepTypeLookupId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Step_Type_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> StepTypeLookupId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.StepTypeLookupId, false); }
			set	{ SetValue((int)TestResultFieldIndex.StepTypeLookupId, value); }
		}

		/// <summary> The TestProtocolId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Protocol_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> TestProtocolId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.TestProtocolId, false); }
			set	{ SetValue((int)TestResultFieldIndex.TestProtocolId, value); }
		}

		/// <summary> The IsSelected property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_Is_Selected"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsSelected
		{
			get { return (System.Boolean)GetValue((int)TestResultFieldIndex.IsSelected, true); }
			set	{ SetValue((int)TestResultFieldIndex.IsSelected, value); }
		}

		/// <summary> The IsCurrent property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_Is_Current"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsCurrent
		{
			get { return (System.Boolean)GetValue((int)TestResultFieldIndex.IsCurrent, true); }
			set	{ SetValue((int)TestResultFieldIndex.IsCurrent, value); }
		}

		/// <summary> The UserId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)TestResultFieldIndex.UserId, true); }
			set	{ SetValue((int)TestResultFieldIndex.UserId, value); }
		}

		/// <summary> The CreationDateTime property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)TestResultFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)TestResultFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)TestResultFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)TestResultFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The IsImprinted property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Test_Result_IsImprinted"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Boolean> IsImprinted
		{
			get { return (Nullable<System.Boolean>)GetValue((int)TestResultFieldIndex.IsImprinted, false); }
			set	{ SetValue((int)TestResultFieldIndex.IsImprinted, value); }
		}

		/// <summary> The ItemRatioId property of the Entity TestResult<br/><br/></summary>
		/// <remarks>Mapped on  table field: "Test_Results"."Item_Ratio_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ItemRatioId
		{
			get { return (Nullable<System.Int32>)GetValue((int)TestResultFieldIndex.ItemRatioId, false); }
			set	{ SetValue((int)TestResultFieldIndex.ItemRatioId, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'TestResultFactorsEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TestResultFactorsEntity))]
		public virtual EntityCollection<TestResultFactorsEntity> TestResultFactors
		{
			get { return GetOrCreateEntityCollection<TestResultFactorsEntity, TestResultFactorsEntityFactory>("TestResult", true, false, ref _testResultFactors);	}
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

		/// <summary> Gets / sets related entity of type 'ItemEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ItemEntity RatioItem
		{
			get	{ return _ratioItem; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncRatioItem(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "TestResults", "RatioItem", _ratioItem, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'ItemEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ItemEntity VitalForce
		{
			get	{ return _vitalForce; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncVitalForce(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "VitalForce", _vitalForce, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity StepType
		{
			get	{ return _stepType; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncStepType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "TestResult", "StepType", _stepType, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestIssueEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestIssueEntity TestIssue
		{
			get	{ return _testIssue; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTestIssue(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "TestResults", "TestIssue", _testIssue, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestProtocolEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestProtocolEntity TestProtocol
		{
			get	{ return _testProtocol; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTestProtocol(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "TestResult", "TestProtocol", _testProtocol, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestResultEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestResultEntity Parent
		{
			get	{ return _parent; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncParent(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "Parent", _parent, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestResultEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestResultEntity SelectedParent
		{
			get	{ return _selectedParent; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncSelectedParent(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "SelectedParent", _selectedParent, false); 
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
			get { return (int)Vital.DataLayer.EntityType.TestResultEntity; }
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
