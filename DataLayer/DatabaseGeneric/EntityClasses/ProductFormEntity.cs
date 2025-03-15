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
	/// <summary>Entity class which represents the entity 'ProductForm'.<br/><br/></summary>
	[Serializable]
	public partial class ProductFormEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private EntityCollection<AutoTestResultProductEntity> _autoTestResultProducts;
		private EntityCollection<DosageOptionEntity> _dosageOptions;
		private EntityCollection<ProductSizeEntity> _productSizes;
		private LookupEntity _status;
		private ProductEntity _product;
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
			/// <summary>Member name Status</summary>
			public static readonly string Status = "Status";
			/// <summary>Member name Product</summary>
			public static readonly string Product = "Product";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
			/// <summary>Member name AutoTestResultProducts</summary>
			public static readonly string AutoTestResultProducts = "AutoTestResultProducts";
			/// <summary>Member name DosageOptions</summary>
			public static readonly string DosageOptions = "DosageOptions";
			/// <summary>Member name ProductSizes</summary>
			public static readonly string ProductSizes = "ProductSizes";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static ProductFormEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public ProductFormEntity():base("ProductFormEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ProductFormEntity(IEntityFields2 fields):base("ProductFormEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ProductFormEntity</param>
		public ProductFormEntity(IValidator validator):base("ProductFormEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for ProductForm which data should be fetched into this ProductForm object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ProductFormEntity(System.Int32 id):base("ProductFormEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for ProductForm which data should be fetched into this ProductForm object</param>
		/// <param name="validator">The custom validator object for this ProductFormEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public ProductFormEntity(System.Int32 id, IValidator validator):base("ProductFormEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected ProductFormEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_autoTestResultProducts = (EntityCollection<AutoTestResultProductEntity>)info.GetValue("_autoTestResultProducts", typeof(EntityCollection<AutoTestResultProductEntity>));
				_dosageOptions = (EntityCollection<DosageOptionEntity>)info.GetValue("_dosageOptions", typeof(EntityCollection<DosageOptionEntity>));
				_productSizes = (EntityCollection<ProductSizeEntity>)info.GetValue("_productSizes", typeof(EntityCollection<ProductSizeEntity>));
				_status = (LookupEntity)info.GetValue("_status", typeof(LookupEntity));
				if(_status!=null)
				{
					_status.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_product = (ProductEntity)info.GetValue("_product", typeof(ProductEntity));
				if(_product!=null)
				{
					_product.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((ProductFormFieldIndex)fieldIndex)
			{
				case ProductFormFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case ProductFormFieldIndex.ProductsId:
					DesetupSyncProduct(true, false);
					break;
				case ProductFormFieldIndex.StatusLookupId:
					DesetupSyncStatus(true, false);
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
				case "Status":
					this.Status = (LookupEntity)entity;
					break;
				case "Product":
					this.Product = (ProductEntity)entity;
					break;
				case "User":
					this.User = (UserEntity)entity;
					break;
				case "AutoTestResultProducts":
					this.AutoTestResultProducts.Add((AutoTestResultProductEntity)entity);
					break;
				case "DosageOptions":
					this.DosageOptions.Add((DosageOptionEntity)entity);
					break;
				case "ProductSizes":
					this.ProductSizes.Add((ProductSizeEntity)entity);
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
				case "Status":
					toReturn.Add(Relations.LookupEntityUsingStatusLookupId);
					break;
				case "Product":
					toReturn.Add(Relations.ProductEntityUsingProductsId);
					break;
				case "User":
					toReturn.Add(Relations.UserEntityUsingUserId);
					break;
				case "AutoTestResultProducts":
					toReturn.Add(Relations.AutoTestResultProductEntityUsingProductFormsId);
					break;
				case "DosageOptions":
					toReturn.Add(Relations.DosageOptionEntityUsingProductFormsId);
					break;
				case "ProductSizes":
					toReturn.Add(Relations.ProductSizeEntityUsingProductFormsId);
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
			int numberOfOneWayRelations = 0+1+1;
			switch(propertyName)
			{
				case null:
					return ((numberOfOneWayRelations > 0) || base.CheckOneWayRelations(null));
				case "Status":
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
				case "Status":
					SetupSyncStatus(relatedEntity);
					break;
				case "Product":
					SetupSyncProduct(relatedEntity);
					break;
				case "User":
					SetupSyncUser(relatedEntity);
					break;
				case "AutoTestResultProducts":
					this.AutoTestResultProducts.Add((AutoTestResultProductEntity)relatedEntity);
					break;
				case "DosageOptions":
					this.DosageOptions.Add((DosageOptionEntity)relatedEntity);
					break;
				case "ProductSizes":
					this.ProductSizes.Add((ProductSizeEntity)relatedEntity);
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
				case "Status":
					DesetupSyncStatus(false, true);
					break;
				case "Product":
					DesetupSyncProduct(false, true);
					break;
				case "User":
					DesetupSyncUser(false, true);
					break;
				case "AutoTestResultProducts":
					this.PerformRelatedEntityRemoval(this.AutoTestResultProducts, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "DosageOptions":
					this.PerformRelatedEntityRemoval(this.DosageOptions, relatedEntity, signalRelatedEntityManyToOne);
					break;
				case "ProductSizes":
					this.PerformRelatedEntityRemoval(this.ProductSizes, relatedEntity, signalRelatedEntityManyToOne);
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
			if(_status!=null)
			{
				toReturn.Add(_status);
			}
			if(_product!=null)
			{
				toReturn.Add(_product);
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
			toReturn.Add(this.AutoTestResultProducts);
			toReturn.Add(this.DosageOptions);
			toReturn.Add(this.ProductSizes);
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
				info.AddValue("_autoTestResultProducts", ((_autoTestResultProducts!=null) && (_autoTestResultProducts.Count>0) && !this.MarkedForDeletion)?_autoTestResultProducts:null);
				info.AddValue("_dosageOptions", ((_dosageOptions!=null) && (_dosageOptions.Count>0) && !this.MarkedForDeletion)?_dosageOptions:null);
				info.AddValue("_productSizes", ((_productSizes!=null) && (_productSizes.Count>0) && !this.MarkedForDeletion)?_productSizes:null);
				info.AddValue("_status", (!this.MarkedForDeletion?_status:null));
				info.AddValue("_product", (!this.MarkedForDeletion?_product:null));
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
			return new ProductFormRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'AutoTestResultProduct' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTestResultProducts()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestResultProductFields.ProductFormsId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'DosageOption' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoDosageOptions()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(DosageOptionFields.ProductFormsId, null, ComparisonOperator.Equal, this.Id));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ProductSize' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductSizes()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ProductSizeFields.ProductFormsId, null, ComparisonOperator.Equal, this.Id));
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

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProduct()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ProductFields.Id, null, ComparisonOperator.Equal, this.ProductsId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(ProductFormEntityFactory));
		}
#if !CF
		/// <summary>Adds the member collections to the collections queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void AddToMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue) 
		{
			base.AddToMemberEntityCollectionsQueue(collectionsQueue);
			collectionsQueue.Enqueue(this._autoTestResultProducts);
			collectionsQueue.Enqueue(this._dosageOptions);
			collectionsQueue.Enqueue(this._productSizes);
		}
		
		/// <summary>Gets the member collections queue from the queue (base first)</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		protected override void GetFromMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue)
		{
			base.GetFromMemberEntityCollectionsQueue(collectionsQueue);
			this._autoTestResultProducts = (EntityCollection<AutoTestResultProductEntity>) collectionsQueue.Dequeue();
			this._dosageOptions = (EntityCollection<DosageOptionEntity>) collectionsQueue.Dequeue();
			this._productSizes = (EntityCollection<ProductSizeEntity>) collectionsQueue.Dequeue();

		}
		
		/// <summary>Determines whether the entity has populated member collections</summary>
		/// <returns>true if the entity has populated member collections.</returns>
		protected override bool HasPopulatedMemberEntityCollections()
		{
			bool toReturn = false;
			toReturn |=(this._autoTestResultProducts != null);
			toReturn |=(this._dosageOptions != null);
			toReturn |=(this._productSizes != null);
			return toReturn ? true : base.HasPopulatedMemberEntityCollections();
		}
		
		/// <summary>Creates the member entity collections queue.</summary>
		/// <param name="collectionsQueue">The collections queue.</param>
		/// <param name="requiredQueue">The required queue.</param>
		protected override void CreateMemberEntityCollectionsQueue(Queue<IEntityCollection2> collectionsQueue, Queue<bool> requiredQueue) 
		{
			base.CreateMemberEntityCollectionsQueue(collectionsQueue, requiredQueue);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<AutoTestResultProductEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultProductEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<DosageOptionEntity>(EntityFactoryCache2.GetEntityFactory(typeof(DosageOptionEntityFactory))) : null);
			collectionsQueue.Enqueue(requiredQueue.Dequeue() ? new EntityCollection<ProductSizeEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ProductSizeEntityFactory))) : null);
		}
#endif
		/// <summary>Gets all related data objects, stored by name. The name is the field name mapped onto the relation for that particular data element.</summary>
		/// <returns>Dictionary with per name the related referenced data element, which can be an entity collection or an entity or null</returns>
		protected override Dictionary<string, object> GetRelatedData()
		{
			Dictionary<string, object> toReturn = new Dictionary<string, object>();
			toReturn.Add("Status", _status);
			toReturn.Add("Product", _product);
			toReturn.Add("User", _user);
			toReturn.Add("AutoTestResultProducts", _autoTestResultProducts);
			toReturn.Add("DosageOptions", _dosageOptions);
			toReturn.Add("ProductSizes", _productSizes);
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
			_fieldsCustomProperties.Add("ProductsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("StatusLookupId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Form", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SuggestedUsage", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UsageSchedule", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _status</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncStatus(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _status, new PropertyChangedEventHandler( OnStatusPropertyChanged ), "Status", Vital.DataLayer.RelationClasses.StaticProductFormRelations.LookupEntityUsingStatusLookupIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ProductFormFieldIndex.StatusLookupId } );
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
				this.PerformSetupSyncRelatedEntity( _status, new PropertyChangedEventHandler( OnStatusPropertyChanged ), "Status", Vital.DataLayer.RelationClasses.StaticProductFormRelations.LookupEntityUsingStatusLookupIdStatic, true, new string[] {  } );
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

		/// <summary> Removes the sync logic for member _product</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncProduct(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _product, new PropertyChangedEventHandler( OnProductPropertyChanged ), "Product", Vital.DataLayer.RelationClasses.StaticProductFormRelations.ProductEntityUsingProductsIdStatic, true, signalRelatedEntity, "ProductForms", resetFKFields, new int[] { (int)ProductFormFieldIndex.ProductsId } );
			_product = null;
		}

		/// <summary> setups the sync logic for member _product</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncProduct(IEntityCore relatedEntity)
		{
			if(_product!=relatedEntity)
			{
				DesetupSyncProduct(true, true);
				_product = (ProductEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _product, new PropertyChangedEventHandler( OnProductPropertyChanged ), "Product", Vital.DataLayer.RelationClasses.StaticProductFormRelations.ProductEntityUsingProductsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnProductPropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticProductFormRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)ProductFormFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticProductFormRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this ProductFormEntity</param>
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
		public  static ProductFormRelations Relations
		{
			get	{ return new ProductFormRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTestResultProduct' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTestResultProducts
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<AutoTestResultProductEntity>(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultProductEntityFactory))), (IEntityRelation)GetRelationsForField("AutoTestResultProducts")[0], (int)Vital.DataLayer.EntityType.ProductFormEntity, (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity, 0, null, null, null, null, "AutoTestResultProducts", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'DosageOption' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathDosageOptions
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<DosageOptionEntity>(EntityFactoryCache2.GetEntityFactory(typeof(DosageOptionEntityFactory))), (IEntityRelation)GetRelationsForField("DosageOptions")[0], (int)Vital.DataLayer.EntityType.ProductFormEntity, (int)Vital.DataLayer.EntityType.DosageOptionEntity, 0, null, null, null, null, "DosageOptions", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductSize' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductSizes
		{
			get	{ return new PrefetchPathElement2( new EntityCollection<ProductSizeEntity>(EntityFactoryCache2.GetEntityFactory(typeof(ProductSizeEntityFactory))), (IEntityRelation)GetRelationsForField("ProductSizes")[0], (int)Vital.DataLayer.EntityType.ProductFormEntity, (int)Vital.DataLayer.EntityType.ProductSizeEntity, 0, null, null, null, null, "ProductSizes", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany);	}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Lookup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStatus
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(LookupEntityFactory))),	(IEntityRelation)GetRelationsForField("Status")[0], (int)Vital.DataLayer.EntityType.ProductFormEntity, (int)Vital.DataLayer.EntityType.LookupEntity, 0, null, null, null, null, "Status", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProduct
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ProductEntityFactory))),	(IEntityRelation)GetRelationsForField("Product")[0], (int)Vital.DataLayer.EntityType.ProductFormEntity, (int)Vital.DataLayer.EntityType.ProductEntity, 0, null, null, null, null, "Product", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.ProductFormEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."ProductForms_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)ProductFormFieldIndex.Id, true); }
			set	{ SetValue((int)ProductFormFieldIndex.Id, value); }
		}

		/// <summary> The UserId property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)ProductFormFieldIndex.UserId, true); }
			set	{ SetValue((int)ProductFormFieldIndex.UserId, value); }
		}

		/// <summary> The ProductsId property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."Products_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ProductsId
		{
			get { return (System.Int32)GetValue((int)ProductFormFieldIndex.ProductsId, true); }
			set	{ SetValue((int)ProductFormFieldIndex.ProductsId, value); }
		}

		/// <summary> The StatusLookupId property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."Status_LookupId"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 StatusLookupId
		{
			get { return (System.Int32)GetValue((int)ProductFormFieldIndex.StatusLookupId, true); }
			set	{ SetValue((int)ProductFormFieldIndex.StatusLookupId, value); }
		}

		/// <summary> The Form property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."ProductForms_Form"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Form
		{
			get { return (System.String)GetValue((int)ProductFormFieldIndex.Form, true); }
			set	{ SetValue((int)ProductFormFieldIndex.Form, value); }
		}

		/// <summary> The SuggestedUsage property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."ProductForms_SuggestedUsage"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String SuggestedUsage
		{
			get { return (System.String)GetValue((int)ProductFormFieldIndex.SuggestedUsage, true); }
			set	{ SetValue((int)ProductFormFieldIndex.SuggestedUsage, value); }
		}

		/// <summary> The UsageSchedule property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."ProductForms_UsageSchedule"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String UsageSchedule
		{
			get { return (System.String)GetValue((int)ProductFormFieldIndex.UsageSchedule, true); }
			set	{ SetValue((int)ProductFormFieldIndex.UsageSchedule, value); }
		}

		/// <summary> The CreationDateTime property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."ProductForms_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)ProductFormFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)ProductFormFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity ProductForm<br/><br/></summary>
		/// <remarks>Mapped on  table field: "ProductForms"."ProductForms_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)ProductFormFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)ProductFormFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'AutoTestResultProductEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AutoTestResultProductEntity))]
		public virtual EntityCollection<AutoTestResultProductEntity> AutoTestResultProducts
		{
			get { return GetOrCreateEntityCollection<AutoTestResultProductEntity, AutoTestResultProductEntityFactory>("ProductForm", true, false, ref _autoTestResultProducts);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'DosageOptionEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(DosageOptionEntity))]
		public virtual EntityCollection<DosageOptionEntity> DosageOptions
		{
			get { return GetOrCreateEntityCollection<DosageOptionEntity, DosageOptionEntityFactory>("ProductForm", true, false, ref _dosageOptions);	}
		}

		/// <summary> Gets the EntityCollection with the related entities of type 'ProductSizeEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductSizeEntity))]
		public virtual EntityCollection<ProductSizeEntity> ProductSizes
		{
			get { return GetOrCreateEntityCollection<ProductSizeEntity, ProductSizeEntityFactory>("ProductForm", true, false, ref _productSizes);	}
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

		/// <summary> Gets / sets related entity of type 'ProductEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ProductEntity Product
		{
			get	{ return _product; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncProduct(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "ProductForms", "Product", _product, true); 
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
			get { return (int)Vital.DataLayer.EntityType.ProductFormEntity; }
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
