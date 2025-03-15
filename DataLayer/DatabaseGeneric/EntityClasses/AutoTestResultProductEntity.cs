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
	/// <summary>Entity class which represents the entity 'AutoTestResultProduct'.<br/><br/></summary>
	[Serializable]
	public partial class AutoTestResultProductEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		#region Class Member Declarations
		private AutoTestResultEntity _autoTestResult;
		private ProductFormEntity _productForm;
		private ProductSizeEntity _productSize;
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
			/// <summary>Member name AutoTestResult</summary>
			public static readonly string AutoTestResult = "AutoTestResult";
			/// <summary>Member name ProductForm</summary>
			public static readonly string ProductForm = "ProductForm";
			/// <summary>Member name ProductSize</summary>
			public static readonly string ProductSize = "ProductSize";
			/// <summary>Member name User</summary>
			public static readonly string User = "User";
		}
		#endregion
		
		/// <summary> Static CTor for setting up custom property hashtables. Is executed before the first instance of this entity class or derived classes is constructed. </summary>
		static AutoTestResultProductEntity()
		{
			SetupCustomPropertyHashtables();
		}
		
		/// <summary> CTor</summary>
		public AutoTestResultProductEntity():base("AutoTestResultProductEntity")
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <remarks>For framework usage.</remarks>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public AutoTestResultProductEntity(IEntityFields2 fields):base("AutoTestResultProductEntity")
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this AutoTestResultProductEntity</param>
		public AutoTestResultProductEntity(IValidator validator):base("AutoTestResultProductEntity")
		{
			InitClassEmpty(validator, null);
		}
				
		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoTestResultProduct which data should be fetched into this AutoTestResultProduct object</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoTestResultProductEntity(System.Int32 id):base("AutoTestResultProductEntity")
		{
			InitClassEmpty(null, null);
			this.Id = id;
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for AutoTestResultProduct which data should be fetched into this AutoTestResultProduct object</param>
		/// <param name="validator">The custom validator object for this AutoTestResultProductEntity</param>
		/// <remarks>The entity is not fetched by this constructor. Use a DataAccessAdapter for that.</remarks>
		public AutoTestResultProductEntity(System.Int32 id, IValidator validator):base("AutoTestResultProductEntity")
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary> Protected CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected AutoTestResultProductEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if(SerializationHelper.Optimization != SerializationOptimization.Fast) 
			{
				_autoTestResult = (AutoTestResultEntity)info.GetValue("_autoTestResult", typeof(AutoTestResultEntity));
				if(_autoTestResult!=null)
				{
					_autoTestResult.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_productForm = (ProductFormEntity)info.GetValue("_productForm", typeof(ProductFormEntity));
				if(_productForm!=null)
				{
					_productForm.AfterSave+=new EventHandler(OnEntityAfterSave);
				}
				_productSize = (ProductSizeEntity)info.GetValue("_productSize", typeof(ProductSizeEntity));
				if(_productSize!=null)
				{
					_productSize.AfterSave+=new EventHandler(OnEntityAfterSave);
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
			switch((AutoTestResultProductFieldIndex)fieldIndex)
			{
				case AutoTestResultProductFieldIndex.UserId:
					DesetupSyncUser(true, false);
					break;
				case AutoTestResultProductFieldIndex.AutoTestResultsId:
					DesetupSyncAutoTestResult(true, false);
					break;
				case AutoTestResultProductFieldIndex.ProductFormsId:
					DesetupSyncProductForm(true, false);
					break;
				case AutoTestResultProductFieldIndex.ProductSizesId:
					DesetupSyncProductSize(true, false);
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
				case "AutoTestResult":
					this.AutoTestResult = (AutoTestResultEntity)entity;
					break;
				case "ProductForm":
					this.ProductForm = (ProductFormEntity)entity;
					break;
				case "ProductSize":
					this.ProductSize = (ProductSizeEntity)entity;
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
				case "AutoTestResult":
					toReturn.Add(Relations.AutoTestResultEntityUsingAutoTestResultsId);
					break;
				case "ProductForm":
					toReturn.Add(Relations.ProductFormEntityUsingProductFormsId);
					break;
				case "ProductSize":
					toReturn.Add(Relations.ProductSizeEntityUsingProductSizesId);
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
				case "AutoTestResult":
					SetupSyncAutoTestResult(relatedEntity);
					break;
				case "ProductForm":
					SetupSyncProductForm(relatedEntity);
					break;
				case "ProductSize":
					SetupSyncProductSize(relatedEntity);
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
				case "AutoTestResult":
					DesetupSyncAutoTestResult(false, true);
					break;
				case "ProductForm":
					DesetupSyncProductForm(false, true);
					break;
				case "ProductSize":
					DesetupSyncProductSize(false, true);
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
			if(_autoTestResult!=null)
			{
				toReturn.Add(_autoTestResult);
			}
			if(_productForm!=null)
			{
				toReturn.Add(_productForm);
			}
			if(_productSize!=null)
			{
				toReturn.Add(_productSize);
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
				info.AddValue("_autoTestResult", (!this.MarkedForDeletion?_autoTestResult:null));
				info.AddValue("_productForm", (!this.MarkedForDeletion?_productForm:null));
				info.AddValue("_productSize", (!this.MarkedForDeletion?_productSize:null));
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
			return new AutoTestResultProductRelations().GetAllRelations();
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'AutoTestResult' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAutoTestResult()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(AutoTestResultFields.Id, null, ComparisonOperator.Equal, this.AutoTestResultsId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'ProductForm' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductForm()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ProductFormFields.Id, null, ComparisonOperator.Equal, this.ProductFormsId));
			return bucket;
		}

		/// <summary> Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'ProductSize' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductSize()
		{
			IRelationPredicateBucket bucket = new RelationPredicateBucket();
			bucket.PredicateExpression.Add(new FieldCompareValuePredicate(ProductSizeFields.Id, null, ComparisonOperator.Equal, this.ProductSizesId));
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
			return EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultProductEntityFactory));
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
			toReturn.Add("AutoTestResult", _autoTestResult);
			toReturn.Add("ProductForm", _productForm);
			toReturn.Add("ProductSize", _productSize);
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
			_fieldsCustomProperties.Add("UserId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("AutoTestResultsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Quantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Price", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Duration", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Schedule", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("SuggestedUsage", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("Comments", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("CreationDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("UpdatedDateTime", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("IsChecked", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ProductFormsId", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			_fieldsCustomProperties.Add("ProductSizesId", fieldHashtable);
		}
		#endregion

		/// <summary> Removes the sync logic for member _autoTestResult</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncAutoTestResult(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _autoTestResult, new PropertyChangedEventHandler( OnAutoTestResultPropertyChanged ), "AutoTestResult", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.AutoTestResultEntityUsingAutoTestResultsIdStatic, true, signalRelatedEntity, "AutoTestResultProducts", resetFKFields, new int[] { (int)AutoTestResultProductFieldIndex.AutoTestResultsId } );
			_autoTestResult = null;
		}

		/// <summary> setups the sync logic for member _autoTestResult</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncAutoTestResult(IEntityCore relatedEntity)
		{
			if(_autoTestResult!=relatedEntity)
			{
				DesetupSyncAutoTestResult(true, true);
				_autoTestResult = (AutoTestResultEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _autoTestResult, new PropertyChangedEventHandler( OnAutoTestResultPropertyChanged ), "AutoTestResult", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.AutoTestResultEntityUsingAutoTestResultsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAutoTestResultPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _productForm</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncProductForm(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _productForm, new PropertyChangedEventHandler( OnProductFormPropertyChanged ), "ProductForm", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.ProductFormEntityUsingProductFormsIdStatic, true, signalRelatedEntity, "AutoTestResultProducts", resetFKFields, new int[] { (int)AutoTestResultProductFieldIndex.ProductFormsId } );
			_productForm = null;
		}

		/// <summary> setups the sync logic for member _productForm</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncProductForm(IEntityCore relatedEntity)
		{
			if(_productForm!=relatedEntity)
			{
				DesetupSyncProductForm(true, true);
				_productForm = (ProductFormEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _productForm, new PropertyChangedEventHandler( OnProductFormPropertyChanged ), "ProductForm", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.ProductFormEntityUsingProductFormsIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnProductFormPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			switch( e.PropertyName )
			{
				default:
					break;
			}
		}

		/// <summary> Removes the sync logic for member _productSize</summary>
		/// <param name="signalRelatedEntity">If set to true, it will call the related entity's UnsetRelatedEntity method</param>
		/// <param name="resetFKFields">if set to true it will also reset the FK fields pointing to the related entity</param>
		private void DesetupSyncProductSize(bool signalRelatedEntity, bool resetFKFields)
		{
			this.PerformDesetupSyncRelatedEntity( _productSize, new PropertyChangedEventHandler( OnProductSizePropertyChanged ), "ProductSize", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.ProductSizeEntityUsingProductSizesIdStatic, true, signalRelatedEntity, "AutoTestResultProducts", resetFKFields, new int[] { (int)AutoTestResultProductFieldIndex.ProductSizesId } );
			_productSize = null;
		}

		/// <summary> setups the sync logic for member _productSize</summary>
		/// <param name="relatedEntity">Instance to set as the related entity of type entityType</param>
		private void SetupSyncProductSize(IEntityCore relatedEntity)
		{
			if(_productSize!=relatedEntity)
			{
				DesetupSyncProductSize(true, true);
				_productSize = (ProductSizeEntity)relatedEntity;
				this.PerformSetupSyncRelatedEntity( _productSize, new PropertyChangedEventHandler( OnProductSizePropertyChanged ), "ProductSize", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.ProductSizeEntityUsingProductSizesIdStatic, true, new string[] {  } );
			}
		}
		
		/// <summary>Handles property change events of properties in a related entity.</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnProductSizePropertyChanged( object sender, PropertyChangedEventArgs e )
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
			this.PerformDesetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.UserEntityUsingUserIdStatic, true, signalRelatedEntity, "", resetFKFields, new int[] { (int)AutoTestResultProductFieldIndex.UserId } );
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
				this.PerformSetupSyncRelatedEntity( _user, new PropertyChangedEventHandler( OnUserPropertyChanged ), "User", Vital.DataLayer.RelationClasses.StaticAutoTestResultProductRelations.UserEntityUsingUserIdStatic, true, new string[] {  } );
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
		/// <param name="validator">The validator object for this AutoTestResultProductEntity</param>
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
		public  static AutoTestResultProductRelations Relations
		{
			get	{ return new AutoTestResultProductRelations(); }
		}
		
		/// <summary> The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties
		{
			get { return _customProperties;}
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'AutoTestResult' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAutoTestResult
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(AutoTestResultEntityFactory))),	(IEntityRelation)GetRelationsForField("AutoTestResult")[0], (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity, (int)Vital.DataLayer.EntityType.AutoTestResultEntity, 0, null, null, null, null, "AutoTestResult", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductForm' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductForm
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ProductFormEntityFactory))),	(IEntityRelation)GetRelationsForField("ProductForm")[0], (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity, (int)Vital.DataLayer.EntityType.ProductFormEntity, 0, null, null, null, null, "ProductForm", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductSize' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductSize
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(ProductSizeEntityFactory))),	(IEntityRelation)GetRelationsForField("ProductSize")[0], (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity, (int)Vital.DataLayer.EntityType.ProductSizeEntity, 0, null, null, null, null, "ProductSize", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
		}

		/// <summary> Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'User' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUser
		{
			get	{ return new PrefetchPathElement2(new EntityCollection(EntityFactoryCache2.GetEntityFactory(typeof(UserEntityFactory))),	(IEntityRelation)GetRelationsForField("User")[0], (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity, (int)Vital.DataLayer.EntityType.UserEntity, 0, null, null, null, null, "User", SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne); }
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

		/// <summary> The Id property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)AutoTestResultProductFieldIndex.Id, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.Id, value); }
		}

		/// <summary> The UserId property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."User_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 UserId
		{
			get { return (System.Int32)GetValue((int)AutoTestResultProductFieldIndex.UserId, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.UserId, value); }
		}

		/// <summary> The AutoTestResultsId property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResults_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 AutoTestResultsId
		{
			get { return (System.Int32)GetValue((int)AutoTestResultProductFieldIndex.AutoTestResultsId, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.AutoTestResultsId, value); }
		}

		/// <summary> The Quantity property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_Quantity"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 Quantity
		{
			get { return (System.Int32)GetValue((int)AutoTestResultProductFieldIndex.Quantity, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.Quantity, value); }
		}

		/// <summary> The Price property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_Price"<br/>
		/// Table field type characteristics (type, precision, scale, length): Decimal, 19, 4, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal Price
		{
			get { return (System.Decimal)GetValue((int)AutoTestResultProductFieldIndex.Price, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.Price, value); }
		}

		/// <summary> The Duration property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_Duration"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Duration
		{
			get { return (System.String)GetValue((int)AutoTestResultProductFieldIndex.Duration, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.Duration, value); }
		}

		/// <summary> The Schedule property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_Schedule"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Schedule
		{
			get { return (System.String)GetValue((int)AutoTestResultProductFieldIndex.Schedule, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.Schedule, value); }
		}

		/// <summary> The SuggestedUsage property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_SuggestedUsage"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String SuggestedUsage
		{
			get { return (System.String)GetValue((int)AutoTestResultProductFieldIndex.SuggestedUsage, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.SuggestedUsage, value); }
		}

		/// <summary> The Comments property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_Comments"<br/>
		/// Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 2147483647<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Comments
		{
			get { return (System.String)GetValue((int)AutoTestResultProductFieldIndex.Comments, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.Comments, value); }
		}

		/// <summary> The CreationDateTime property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_CreationDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime CreationDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoTestResultProductFieldIndex.CreationDateTime, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.CreationDateTime, value); }
		}

		/// <summary> The UpdatedDateTime property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_UpdatedDateTime"<br/>
		/// Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime UpdatedDateTime
		{
			get { return (System.DateTime)GetValue((int)AutoTestResultProductFieldIndex.UpdatedDateTime, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.UpdatedDateTime, value); }
		}

		/// <summary> The IsChecked property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."AutoTestResultProduct_IsChecked"<br/>
		/// Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsChecked
		{
			get { return (System.Boolean)GetValue((int)AutoTestResultProductFieldIndex.IsChecked, true); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.IsChecked, value); }
		}

		/// <summary> The ProductFormsId property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."ProductForms_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ProductFormsId
		{
			get { return (Nullable<System.Int32>)GetValue((int)AutoTestResultProductFieldIndex.ProductFormsId, false); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.ProductFormsId, value); }
		}

		/// <summary> The ProductSizesId property of the Entity AutoTestResultProduct<br/><br/></summary>
		/// <remarks>Mapped on  table field: "AutoTestResultProduct"."ProductSizes_Id"<br/>
		/// Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0<br/>
		/// Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ProductSizesId
		{
			get { return (Nullable<System.Int32>)GetValue((int)AutoTestResultProductFieldIndex.ProductSizesId, false); }
			set	{ SetValue((int)AutoTestResultProductFieldIndex.ProductSizesId, value); }
		}

		/// <summary> Gets / sets related entity of type 'AutoTestResultEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual AutoTestResultEntity AutoTestResult
		{
			get	{ return _autoTestResult; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncAutoTestResult(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoTestResultProducts", "AutoTestResult", _autoTestResult, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'ProductFormEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ProductFormEntity ProductForm
		{
			get	{ return _productForm; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncProductForm(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoTestResultProducts", "ProductForm", _productForm, true); 
				}
			}
		}

		/// <summary> Gets / sets related entity of type 'ProductSizeEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(true)]
		public virtual ProductSizeEntity ProductSize
		{
			get	{ return _productSize; }
			set
			{
				if(this.IsDeserializing)
				{
					SetupSyncProductSize(value);
				}
				else
				{
					SetSingleRelatedEntityNavigator(value, "AutoTestResultProducts", "ProductSize", _productSize, true); 
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
			get { return (int)Vital.DataLayer.EntityType.AutoTestResultProductEntity; }
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
