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
	/// <summary>Entity class which represents the entity 'AutoItem'.<br/><br/></summary>
	[Serializable]
	public partial class AutoItemEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<AutoItemRelationEntity> _children;
		private EntityCollection<AutoItemRelationEntity> _parents;
		private EntityCollection<ProductEntity> _products;
		private ImageEntity _image;
		private LookupEntity _childsOrderType;
		private LookupEntity _childsScanningType;
		private LookupEntity _gender;
		private LookupEntity _scanningMethod;
		private LookupEntity _status;
		private LookupEntity _structureType;
		private LookupEntity _type;
		private TestingPointEntity _testingPoint;
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
			/// <summary>Member name Image</summary>
			public static readonly string Image = "Image";
			/// <summary>Member name ChildsOrderType</summary>
			public static readonly string ChildsOrderType = "ChildsOrderType";
			/// <summary>Member name ChildsScanningType</summary>
			public static readonly string ChildsScanningType = "ChildsScanningType";
			/// <summary>Member name Gender</summary>
			public static readonly string Gender = "Gender";
			/// <summary>Member name ScanningMethod</summary>
			public static readonly string ScanningMethod = "ScanningMethod";
			/// <summary>Member name Status</summary>
			public static readonly string Status = "Status";
			/// <summary>Member name StructureType</summary>
			public static readonly string StructureType = "StructureType";
			/// <summary>Member name Type</summary>
			public static readonly string Type = "Type";
			/// <summary>Member name TestingPoint</summary>
			public static readonly string TestingPoint = "TestingPoint";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name Children</summary>
			public static readonly string Children = "Children";
			/// <summary>Member name Parents</summary>
			public static readonly string Parents = "Parents";
			/// <summary>Member name Products</summary>
			public static readonly string Products = "Products";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static AutoItemEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public AutoItemEntity():base("AutoItemEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public AutoItemEntity(IEntityFields2 fields):base("AutoItemEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this AutoItemEntity</param>
		public AutoItemEntity(IValidator validator):base("AutoItemEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoItem which data should be fetched into this AutoItem object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoItemEntity(System.Int32 id):base("AutoItemEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoItem which data should be fetched into this AutoItem object</param>
		/// <param name="validator">The custom validator object for this AutoItemEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoItemEntity(System.Int32 id, IValidator validator):base("AutoItemEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected AutoItemEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_children = (EntityCollection<AutoItemRelationEntity>)info.GetValue("_children", typeof(EntityCollection<AutoItemRelationEntity>));
				_parents = (EntityCollection<AutoItemRelationEntity>)info.GetValue("_parents", typeof(EntityCollection<AutoItemRelationEntity>));
				_products = (EntityCollection<ProductEntity>)info.GetValue("_products", typeof(EntityCollection<ProductEntity>));
				_image = (ImageEntity)info.GetValue("_image", typeof(ImageEntity));
				if(_image!=null)
				{
					_image.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_childsOrderType = (LookupEntity)info.GetValue("_childsOrderType", typeof(LookupEntity));
				if(_childsOrderType!=null)
				{
					_childsOrderType.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_childsScanningType = (LookupEntity)info.GetValue("_childsScanningType", typeof(LookupEntity));
				if(_childsScanningType!=null)
				{
					_childsScanningType.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_gender = (LookupEntity)info.GetValue("_gender", typeof(LookupEntity));
				if(_gender!=null)
				{
					_gender.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_scanningMethod = (LookupEntity)info.GetValue("_scanningMethod", typeof(LookupEntity));
				if(_scanningMethod!=null)
				{
					_scanningMethod.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_status = (LookupEntity)info.GetValue("_status", typeof(LookupEntity));
				if(_status!=null)
				{
					_status.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_structureType = (LookupEntity)info.GetValue("_structureType", typeof(LookupEntity));
				if(_structureType!=null)
				{
					_structureType.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_type = (LookupEntity)info.GetValue("_type", typeof(LookupEntity));
				if(_type!=null)
				{
					_type.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_testingPoint = (TestingPointEntity)info.GetValue("_testingPoint", typeof(TestingPointEntity));
				if(_testingPoint!=null)
				{
					_testingPoint.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((AutoItemFieldIndex)fieldIndex)
			{
				case AutoItemFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case AutoItemFieldIndex.TestingPointsId:
					DesetupSyncTestingPoint(true, false);
					break;
				case AutoItemFieldIndex.ImageId:
					DesetupSyncImage(true, false);
					break;
				case AutoItemFieldIndex.TypeLookupId:
					DesetupSyncType(true, false);
					break;
				case AutoItemFieldIndex.StructureTypeLookupId:
					DesetupSyncStructureType(true, false);
					break;
				case AutoItemFieldIndex.StatusLookupId:
					DesetupSyncStatus(true, false);
					break;
				case AutoItemFieldIndex.ChildsOrderTypeLookupId:
					DesetupSyncChildsOrderType(true, false);
					break;
				case AutoItemFieldIndex.ChildsScanningTypeLookupId:
					DesetupSyncChildsScanningType(true, false);
					break;
				case AutoItemFieldIndex.ScanningMethodLookupId:
					DesetupSyncScanningMethod(true, false);
					break;
				case AutoItemFieldIndex.GenderLookupId:
					DesetupSyncGender(true, false);
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
				case "Image":
					this.Image = (ImageEntity)entity;
					break;
				case "ChildsOrderType":
					this.ChildsOrderType = (LookupEntity)entity;
					break;
				case "ChildsScanningType":
					this.ChildsScanningType = (LookupEntity)entity;
					break;
				case "Gender":
					this.Gender = (LookupEntity)entity;
					break;
				case "ScanningMethod":
					this.ScanningMethod = (LookupEntity)entity;
					break;
				case "Status":
					this.Status = (LookupEntity)entity;
					break;
				case "StructureType":
					this.StructureType = (LookupEntity)entity;
					break;
				case "Type":
					this.Type = (LookupEntity)entity;
					break;
				case "TestingPoint":
					this.TestingPoint = (TestingPointEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "Children":
					this.Children.Add((AutoItemRelationEntity)entity);
					break;
				case "Parents":
					this.Parents.Add((AutoItemRelationEntity)entity);
					break;
				case "Products":
					this.Products.Add((ProductEntity)entity);
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
				case "Image":
					toReturn.Add(Relations.ImageEntityUsingImageId);
					break;
				case "ChildsOrderType":
					toReturn.Add(Relations.LookupEntityUsingChildsOrderTypeLookupId);
					break;
				case "ChildsScanningType":
					toReturn.Add(Relations.LookupEntityUsingChildsScanningTypeLookupId);
					break;
				case "Gender":
					toReturn.Add(Relations.LookupEntityUsingGenderLookupId);
					break;
				case "ScanningMethod":
					toReturn.Add(Relations.LookupEntityUsingScanningMethodLookupId);
					break;
				case "Status":
					toReturn.Add(Relations.LookupEntityUsingStatusLookupId);
					break;
				case "StructureType":
					toReturn.Add(Relations.LookupEntityUsingStructureTypeLookupId);
					break;
				case "Type":
					toReturn.Add(Relations.LookupEntityUsingTypeLookupId);
					break;
				case "TestingPoint":
					toReturn.Add(Relations.TestingPointEntityUsingTestingPointsId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "Children":
					toReturn.Add(Relations.AutoItemRelationEntityUsingAutoItemChildId);
					break;
				case "Parents":
					toReturn.Add(Relations.AutoItemRelationEntityUsingAutoItemParentId);
					break;
				case "Products":
					toReturn.Add(Relations.ProductEntityUsingAutoItemsId);
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
			int numberOfOneWayRelations = 0+1+1+1+1+1+1+1+1+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
				case "Image":
					return true;
				case "ChildsOrderType":
					return true;
				case "ChildsScanningType":
					return true;
				case "ScanningMethod":
					return true;
				case "Status":
					return true;
				case "StructureType":
					return true;
				case "Type":
					return true;
				case "TestingPoint":
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
				case "Image":
					SetupSyncImage(relatedEntity);
					break;
				case "ChildsOrderType":
					SetupSyncChildsOrderType(relatedEntity);
					break;
				case "ChildsScanningType":
					SetupSyncChildsScanningType(relatedEntity);
					break;
				case "Gender":
					SetupSyncGender(relatedEntity);
					break;
				case "ScanningMethod":
					SetupSyncScanningMethod(relatedEntity);
					break;
				case "Status":
					SetupSyncStatus(relatedEntity);
					break;
				case "StructureType":
					SetupSyncStructureType(relatedEntity);
					break;
				case "Type":
					SetupSyncType(relatedEntity);
					break;
				case "TestingPoint":
					SetupSyncTestingPoint(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "Children":
					this.Children.Add((AutoItemRelationEntity)relatedEntity);
					break;
				case "Parents":
					this.Parents.Add((AutoItemRelationEntity)relatedEntity);
					break;
				case "Products":
					this.Products.Add((ProductEntity)relatedEntity);
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
				case "Image":
					DesetupSyncImage(false, true);
					break;
				case "ChildsOrderType":
					DesetupSyncChildsOrderType(false, true);
					break;
				case "ChildsScanningType":
					DesetupSyncChildsScanningType(false, true);
					break;
				case "Gender":
					DesetupSyncGender(false, true);
					break;
				case "ScanningMethod":
					DesetupSyncScanningMethod(false, true);
					break;
				case "Status":
					DesetupSyncStatus(false, true);
					break;
				case "StructureType":
					DesetupSyncStructureType(false, true);
					break;
				case "Type":
					DesetupSyncType(false, true);
					break;
				case "TestingPoint":
					DesetupSyncTestingPoint(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "Children":
					this.PerformRelatedEntityRemoval(this.Children, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Parents":
					this.PerformRelatedEntityRemoval(this.Parents, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "Products":
					this.PerformRelatedEntityRemoval(this.Products, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_image!=null)
			{
				toReturn.Add(_image);
			}
			if(_childsOrderType!=null)
			{
				toReturn.Add(_childsOrderType);
			}
			if(_childsScanningType!=null)
			{
				toReturn.Add(_childsScanningType);
			}
			if(_gender!=null)
			{
				toReturn.Add(_gender);
			}
			if(_scanningMethod!=null)
			{
				toReturn.Add(_scanningMethod);
			}
			if(_status!=null)
			{
				toReturn.Add(_status);
			}
			if(_structureType!=null)
			{
				toReturn.Add(_structureType);
			}
			if(_type!=null)
			{
				toReturn.Add(_type);
			}
			if(_testingPoint!=null)
			{
				toReturn.Add(_testingPoint);
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
			toReturn.Add(this.Children);
			toReturn.Add(this.Parents);
			toReturn.Add(this.Products);
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
				info.AddValue("_children", ((_children!=null) && (_children.Count>0) && !this.MarkedForDeletion)?_children:null);
				info.AddValue("_parents", ((_parents!=null) && (_parents.Count>0) && !this.MarkedForDeletion)?_parents:null);
				info.AddValue("_products", ((_products!=null) && (_products.Count>0) && !this.MarkedForDeletion)?_products:null);
				info.AddValue("_image", (!this.MarkedForDeletion?_image:null));
				info.AddValue("_childsOrderType", (!this.MarkedForDeletion?_childsOrderType:null));
				info.AddValue("_childsScanningType", (!this.MarkedForDeletion?_childsScanningType:null));
				info.AddValue("_gender", (!this.MarkedForDeletion?_gender:null));
				info.AddValue("_scanningMethod", (!this.MarkedForDeletion?_scanningMethod:null));
				info.AddValue("_status", (!this.MarkedForDeletion?_status:null));
				info.AddValue("_structureType", (!this.MarkedForDeletion?_structureType:null));
				info.AddValue("_type", (!this.MarkedForDeletion?_type:null));
				info.AddValue("_testingPoint", (!this.MarkedForDeletion?_testingPoint:null));
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
			return new AutoItemRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoItemRelation' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoChildren()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoItemRelationFields.AutoItemChildId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoItemRelation' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoParents()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoItemRelationFields.AutoItemParentId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProducts()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ProductFields.AutoItemsId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Image' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoImage()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ImageFields.Id, null, ComparisonOperator.Equal, this.ImageId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoChildsOrderType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ChildsOrderTypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoChildsScanningType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ChildsScanningTypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoGender()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.GenderLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoScanningMethod()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.ScanningMethodLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStatus()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.StatusLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStructureType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.StructureTypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Lookup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoType()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(LookupFields.Id, null, ComparisonOperator.Equal, this.TypeLookupId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'TestingPoint' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTestingPoint()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(TestingPointFields.Id, null, ComparisonOperator.Equal, this.TestingPointsId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(AutoItemEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._children);
			collectionsQueue.Enqueue(this._parents);
			collectionsQueue.Enqueue(this._products);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._children = (EntityCollection<AutoItemRelationEntity>) collectionsQueue.Dequeue();
			this._parents = (EntityCollection<AutoItemRelationEntity>) collectionsQueue.Dequeue();
			this._products = (EntityCollection<ProductEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._children != null);
			toReturn |=(this._parents != null);
			toReturn |=(this._products != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemRelationEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemRelationEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ProductEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ProductEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Image", _image);
			toReturn.Add("ChildsOrderType", _childsOrderType);
			toReturn.Add("ChildsScanningType", _childsScanningType);
			toReturn.Add("Gender", _gender);
			toReturn.Add("ScanningMethod", _scanningMethod);
			toReturn.Add("Status", _status);
			toReturn.Add("StructureType", _structureType);
			toReturn.Add("Type", _type);
			toReturn.Add("TestingPoint", _testingPoint);
			toReturn.Add("User", _user);
			toReturn.Add("Children", _children);
			toReturn.Add("Parents", _parents);
			toReturn.Add("Products", _products);
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
			_fieldsCustomProperties.Add("TestingPointsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ImageId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("TypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("StructureTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("StatusLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ChildsOrderTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ChildsScanningTypeLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ScanningMethodLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ScansNumber", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Key", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("FullName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Description", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Frequency", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UserNotes", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsUserItem", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsSearchable", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("InsertOnNo", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsImprintable", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("MatchesNumber", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("FinishAllScanRounds", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AddResultOnMatch", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ExcludeOnMatch", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AddAllChildesOnMatch", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ModelIdentifier", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("DirectAccessChecks", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("GenderLookupId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _image</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncImage(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _image, new PropertyChangedEventHandler( OnImagePropertyChanged ), "Image", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.ImageEntityUsingImageIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.ImageId } );
			_image = null;
		}

		/// <summary> setups the sync logic for member _image</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncImage(IEntityCore relatedEntity)
		{
			if(_image!=relatedEntity)
			{
				DesetupSyncImage(true, true);
				_image = (ImageEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _image, new PropertyChangedEventHandler( OnImagePropertyChanged ), "Image", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.ImageEntityUsingImageIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnImagePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _childsOrderType</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncChildsOrderType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _childsOrderType, new PropertyChangedEventHandler( OnChildsOrderTypePropertyChanged ), "ChildsOrderType", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingChildsOrderTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.ChildsOrderTypeLookupId } );
			_childsOrderType = null;
		}

		/// <summary> setups the sync logic for member _childsOrderType</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncChildsOrderType(IEntityCore relatedEntity)
		{
			if(_childsOrderType!=relatedEntity)
			{
				DesetupSyncChildsOrderType(true, true);
				_childsOrderType = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _childsOrderType, new PropertyChangedEventHandler( OnChildsOrderTypePropertyChanged ), "ChildsOrderType", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingChildsOrderTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChildsOrderTypePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _childsScanningType</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncChildsScanningType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _childsScanningType, new PropertyChangedEventHandler( OnChildsScanningTypePropertyChanged ), "ChildsScanningType", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingChildsScanningTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.ChildsScanningTypeLookupId } );
			_childsScanningType = null;
		}

		/// <summary> setups the sync logic for member _childsScanningType</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncChildsScanningType(IEntityCore relatedEntity)
		{
			if(_childsScanningType!=relatedEntity)
			{
				DesetupSyncChildsScanningType(true, true);
				_childsScanningType = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _childsScanningType, new PropertyChangedEventHandler( OnChildsScanningTypePropertyChanged ), "ChildsScanningType", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingChildsScanningTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChildsScanningTypePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _gender</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncGender(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _gender, new PropertyChangedEventHandler( OnGenderPropertyChanged ), "Gender", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingGenderLookupIdStatic, true, signalRelatedEntity, "AutoItems", resetFKFields, new int[] { (int)AutoItemFieldIndex.GenderLookupId } );
			_gender = null;
		}

		/// <summary> setups the sync logic for member _gender</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncGender(IEntityCore relatedEntity)
		{
			if(_gender!=relatedEntity)
			{
				DesetupSyncGender(true, true);
				_gender = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _gender, new PropertyChangedEventHandler( OnGenderPropertyChanged ), "Gender", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingGenderLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGenderPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _scanningMethod</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncScanningMethod(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _scanningMethod, new PropertyChangedEventHandler( OnScanningMethodPropertyChanged ), "ScanningMethod", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingScanningMethodLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.ScanningMethodLookupId } );
			_scanningMethod = null;
		}

		/// <summary> setups the sync logic for member _scanningMethod</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncScanningMethod(IEntityCore relatedEntity)
		{
			if(_scanningMethod!=relatedEntity)
			{
				DesetupSyncScanningMethod(true, true);
				_scanningMethod = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _scanningMethod, new PropertyChangedEventHandler( OnScanningMethodPropertyChanged ), "ScanningMethod", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingScanningMethodLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnScanningMethodPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _status</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncStatus(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _status, new PropertyChangedEventHandler( OnStatusPropertyChanged ), "Status", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingStatusLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.StatusLookupId } );
			_status = null;
		}

		/// <summary> setups the sync logic for member _status</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncStatus(IEntityCore relatedEntity)
		{
			if(_status!=relatedEntity)
			{
				DesetupSyncStatus(true, true);
				_status = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _status, new PropertyChangedEventHandler( OnStatusPropertyChanged ), "Status", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingStatusLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStatusPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _structureType</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncStructureType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _structureType, new PropertyChangedEventHandler( OnStructureTypePropertyChanged ), "StructureType", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingStructureTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.StructureTypeLookupId } );
			_structureType = null;
		}

		/// <summary> setups the sync logic for member _structureType</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncStructureType(IEntityCore relatedEntity)
		{
			if(_structureType!=relatedEntity)
			{
				DesetupSyncStructureType(true, true);
				_structureType = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _structureType, new PropertyChangedEventHandler( OnStructureTypePropertyChanged ), "StructureType", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingStructureTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnStructureTypePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _type</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncType(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _type, new PropertyChangedEventHandler( OnTypePropertyChanged ), "Type", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingTypeLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.TypeLookupId } );
			_type = null;
		}

		/// <summary> setups the sync logic for member _type</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncType(IEntityCore relatedEntity)
		{
			if(_type!=relatedEntity)
			{
				DesetupSyncType(true, true);
				_type = (LookupEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _type, new PropertyChangedEventHandler( OnTypePropertyChanged ), "Type", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.LookupEntityUsingTypeLookupIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTypePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _testingPoint</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncTestingPoint(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _testingPoint, new PropertyChangedEventHandler( OnTestingPointPropertyChanged ), "TestingPoint", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.TestingPointEntityUsingTestingPointsIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.TestingPointsId } );
			_testingPoint = null;
		}

		/// <summary> setups the sync logic for member _testingPoint</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncTestingPoint(IEntityCore relatedEntity)
		{
			if(_testingPoint!=relatedEntity)
			{
				DesetupSyncTestingPoint(true, true);
				_testingPoint = (TestingPointEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _testingPoint, new PropertyChangedEventHandler( OnTestingPointPropertyChanged ), "TestingPoint", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.TestingPointEntityUsingTestingPointsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTestingPointPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoItemFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoItemRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this AutoItemEntity</param>
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
		public  static AutoItemRelations Relations
		{
			get	{ return new AutoItemRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoItemRelation' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathChildren
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemRelationEntityFactory))), (IEntityRelation)GetRelationsForField("Children")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.AutoItemRelationEntity, 0, null, null, null, null, "Children", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoItemRelation' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathParents
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoItemRelationEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoItemRelationEntityFactory))), (IEntityRelation)GetRelationsForField("Parents")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.AutoItemRelationEntity, 0, null, null, null, null, "Parents", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProducts
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ProductEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ProductEntityFactory))), (IEntityRelation)GetRelationsForField("Products")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.ProductEntity, 0, null, null, null, null, "Products", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Image' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathImage
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ImageEntityFactory))),	(IEntityRelation)GetRelationsForField("Image")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.ImageEntity, 0, null, null, null, null, "Image", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathChildsOrderType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ChildsOrderType")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ChildsOrderType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathChildsScanningType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ChildsScanningType")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ChildsScanningType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathGender
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("Gender")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "Gender", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathScanningMethod
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("ScanningMethod")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "ScanningMethod", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStatus
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("Status")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "Status", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStructureType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("StructureType")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "StructureType", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathType
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("Type")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "Type", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TestingPoint' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTestingPoint
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(TestingPointEntityFactory))),	(IEntityRelation)GetRelationsForField("TestingPoint")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.TestingPointEntity, 0, null, null, null, null, "TestingPoint", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.AutoItemEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.Id, true); }
			set	{ SetValue((int)AutoItemFieldIndex.Id, value); }
		}

		/// <summary> The UserId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.UserId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.UserId, value); }
		}

		/// <summary> The TestingPointsId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."TestingPoints_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 TestingPointsId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.TestingPointsId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.TestingPointsId, value); }
		}

		/// <summary> The ImageId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."Image_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ImageId
		{
			get { return (Nullable<System.Int32>)GetValue((int)AutoItemFieldIndex.ImageId, false); }
			set	{ SetValue((int)AutoItemFieldIndex.ImageId, value); }
		}

		/// <summary> The TypeLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."Type_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 TypeLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.TypeLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.TypeLookupId, value); }
		}

		/// <summary> The StructureTypeLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."StructureType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 StructureTypeLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.StructureTypeLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.StructureTypeLookupId, value); }
		}

		/// <summary> The StatusLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."Status_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 StatusLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.StatusLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.StatusLookupId, value); }
		}

		/// <summary> The ChildsOrderTypeLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."ChildsOrderType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ChildsOrderTypeLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.ChildsOrderTypeLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.ChildsOrderTypeLookupId, value); }
		}

		/// <summary> The ChildsScanningTypeLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."ChildsScanningType_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ChildsScanningTypeLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.ChildsScanningTypeLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.ChildsScanningTypeLookupId, value); }
		}

		/// <summary> The ScanningMethodLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."ScanningMethod_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ScanningMethodLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.ScanningMethodLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.ScanningMethodLookupId, value); }
		}

		/// <summary> The ScansNumber property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_ScansNumber"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ScansNumber
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.ScansNumber, true); }
			set	{ SetValue((int)AutoItemFieldIndex.ScansNumber, value); }
		}

		/// <summary> The Key property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_Key"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Key
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.Key, true); }
			set	{ SetValue((int)AutoItemFieldIndex.Key, value); }
		}

		/// <summary> The Name property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_Name"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.Name, true); }
			set	{ SetValue((int)AutoItemFieldIndex.Name, value); }
		}

		/// <summary> The FullName property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_FullName"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String FullName
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.FullName, true); }
			set	{ SetValue((int)AutoItemFieldIndex.FullName, value); }
		}

		/// <summary> The Description property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_Description"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Description
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.Description, true); }
			set	{ SetValue((int)AutoItemFieldIndex.Description, value); }
		}

		/// <summary> The Frequency property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_Frequency"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Frequency
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.Frequency, true); }
			set	{ SetValue((int)AutoItemFieldIndex.Frequency, value); }
		}

		/// <summary> The UserNotes property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_UserNotes"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String UserNotes
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.UserNotes, true); }
			set	{ SetValue((int)AutoItemFieldIndex.UserNotes, value); }
		}

		/// <summary> The IsUserItem property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_IsUserItem"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsUserItem
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.IsUserItem, true); }
			set	{ SetValue((int)AutoItemFieldIndex.IsUserItem, value); }
		}

		/// <summary> The IsSearchable property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_IsSearchable"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsSearchable
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.IsSearchable, true); }
			set	{ SetValue((int)AutoItemFieldIndex.IsSearchable, value); }
		}

		/// <summary> The InsertOnNo property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_InsertOnNo"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean InsertOnNo
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.InsertOnNo, true); }
			set	{ SetValue((int)AutoItemFieldIndex.InsertOnNo, value); }
		}

		/// <summary> The IsImprintable property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_IsImprintable"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsImprintable
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.IsImprintable, true); }
			set	{ SetValue((int)AutoItemFieldIndex.IsImprintable, value); }
		}

		/// <summary> The CreationDateTime property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoItemFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)AutoItemFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoItemFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)AutoItemFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The MatchesNumber property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_MatchesNumber"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 MatchesNumber
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.MatchesNumber, true); }
			set	{ SetValue((int)AutoItemFieldIndex.MatchesNumber, value); }
		}

		/// <summary> The FinishAllScanRounds property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_FinishAllScanRounds"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean FinishAllScanRounds
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.FinishAllScanRounds, true); }
			set	{ SetValue((int)AutoItemFieldIndex.FinishAllScanRounds, value); }
		}

		/// <summary> The AddResultOnMatch property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_AddResultOnMatch"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean AddResultOnMatch
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.AddResultOnMatch, true); }
			set	{ SetValue((int)AutoItemFieldIndex.AddResultOnMatch, value); }
		}

		/// <summary> The ExcludeOnMatch property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_ExcludeOnMatch"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean ExcludeOnMatch
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.ExcludeOnMatch, true); }
			set	{ SetValue((int)AutoItemFieldIndex.ExcludeOnMatch, value); }
		}

		/// <summary> The AddAllChildesOnMatch property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_AddAllChildesOnMatch"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean AddAllChildesOnMatch
		{
			get { return (System.Boolean)GetValue((int)AutoItemFieldIndex.AddAllChildesOnMatch, true); }
			set	{ SetValue((int)AutoItemFieldIndex.AddAllChildesOnMatch, value); }
		}

		/// <summary> The ModelIdentifier property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_ModelIdentifier"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String ModelIdentifier
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.ModelIdentifier, true); }
			set	{ SetValue((int)AutoItemFieldIndex.ModelIdentifier, value); }
		}

		/// <summary> The DirectAccessChecks property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."AutoItems_DirectAccessChecks"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String DirectAccessChecks
		{
			get { return (System.String)GetValue((int)AutoItemFieldIndex.DirectAccessChecks, true); }
			set	{ SetValue((int)AutoItemFieldIndex.DirectAccessChecks, value); }
		}

		/// <summary> The GenderLookupId property of the Entity AutoItem<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoItems"."Gender_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 GenderLookupId
		{
			get { return (System.Int32)GetValue((int)AutoItemFieldIndex.GenderLookupId, true); }
			set	{ SetValue((int)AutoItemFieldIndex.GenderLookupId, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoItemRelationEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoItemRelationEntity))]
		public virtual EntityCollection<AutoItemRelationEntity> Children
		{
			get { return GetOrCreateEntityCollection<AutoItemRelationEntity, AutoItemRelationEntityFactory>("Child", true, false, ref _children);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoItemRelationEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoItemRelationEntity))]
		public virtual EntityCollection<AutoItemRelationEntity> Parents
		{
			get { return GetOrCreateEntityCollection<AutoItemRelationEntity, AutoItemRelationEntityFactory>("Parent", true, false, ref _parents);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ProductEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductEntity))]
		public virtual EntityCollection<ProductEntity> Products
		{
			get { return GetOrCreateEntityCollection<ProductEntity, ProductEntityFactory>("AutoItem", true, false, ref _products);	}
		}

		/// <summary> Gets / sets related entity of type 'ImageEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ImageEntity Image
		{
			get	{ return _image; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncImage(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "Image", _image, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ChildsOrderType
		{
			get	{ return _childsOrderType; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncChildsOrderType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "ChildsOrderType", _childsOrderType, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ChildsScanningType
		{
			get	{ return _childsScanningType; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncChildsScanningType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "ChildsScanningType", _childsScanningType, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity Gender
		{
			get	{ return _gender; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncGender(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoItems", "Gender", _gender, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity ScanningMethod
		{
			get	{ return _scanningMethod; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncScanningMethod(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "ScanningMethod", _scanningMethod, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity Status
		{
			get	{ return _status; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncStatus(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "Status", _status, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity StructureType
		{
			get	{ return _structureType; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncStructureType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "StructureType", _structureType, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'LookupEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual LookupEntity Type
		{
			get	{ return _type; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncType(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "Type", _type, false); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'TestingPointEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual TestingPointEntity TestingPoint
		{
			get	{ return _testingPoint; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncTestingPoint(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "", "TestingPoint", _testingPoint, false); 
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
			get { return (int)Vital.DataLayer.EntityType.AutoItemEntity; }
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
