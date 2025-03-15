using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.DatabaseRepositories.Lookups;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Items
{
	public class ItemDatabaseRepository : BaseRepository,IItemRepository
	{
		#region Path Edges

		private readonly Func<IPathEdgeRootParser<ItemEntity>, IPathEdgeRootParser<ItemEntity>> _pathEdgesItem =
			p =>
			p.Prefetch<ItemDetailsEntity>(a => a.ItemDetail)
					.SubPath(k => k.Prefetch<ImageEntity>(kk => kk.Image).Exclude(kk => kk.Data))
			 .Prefetch<ItemRelationEntity>(c => c.Children)
				.SubPath(cc => cc.Prefetch(c => c.Child))
			 .Prefetch<ItemRelationEntity>(t => t.Parents).OrderBy(m => m.Order)
				.SubPath(cp => cp.Prefetch(op => op.RelationTypeLookup)
								 .Prefetch<ItemEntity>(ccp => ccp.Child)
									.SubPath(k => k.Prefetch<LookupEntity>(ck => ck.GenderLookup)
												   .Prefetch<LookupEntity>(ck => ck.ListTypeLookup)
                                                   .Prefetch<LookupEntity>(ck => ck.ItemSourceLookup)
												   .Prefetch<LookupEntity>(ck => ck.TypeLookup)
                                                   .Prefetch<ItemPropertyEntity>(ck => ck.Properties)
                                                                                .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property).SubPath(pl => 
                                                                                               pl.Prefetch(vl => vl.ValueTypeLookup)
                                                                                                 .Prefetch(al => al.ApplicableTypeLookup)))))
			 .Prefetch<LookupEntity>(s => s.ListTypeLookup)
             .Prefetch<LookupEntity>(s => s.ItemSourceLookup)
			 .Prefetch<LookupEntity>(g => g.GenderLookup)
			 .Prefetch<LookupEntity>(l => l.TypeLookup)
			 .Prefetch<ItemPropertyEntity>(pl => pl.Properties)
				  .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property).SubPath(pl => pl.Prefetch(vl => vl.ValueTypeLookup).Prefetch(al => al.ApplicableTypeLookup)))
			 .Prefetch<UserEntity>(u => u.User)
			 .Prefetch<ItemTargetEntity>(it => it.ItemTargets)
				.SubPath(tl => tl.Prefetch(l => l.TargetTypeLookup));

		private readonly Func<IPathEdgeRootParser<ItemDetailsEntity>, IPathEdgeRootParser<ItemDetailsEntity>> _pathEdgesItemDetails =
			p => p.Prefetch<ImageEntity>(k => k.Image).Exclude(kk => kk.Data);
				
		private readonly Func<IPathEdgeRootParser<ItemRelationEntity>, IPathEdgeRootParser<ItemRelationEntity>>
			_pathEdgesItemRelation =
				p => p.Prefetch<ItemEntity>(t => t.Parent)
                        .SubPath(ch => ch.Prefetch(s => s.TypeLookup))
                        .SubPath(ch => ch.Prefetch(s => s.ListTypeLookup))
                        .SubPath(ch => ch.Prefetch(s => s.ItemSourceLookup))
                        .SubPath(m => m
                                .Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                    .SubPath(e => e
                                            .Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data)))
                                    .SubPath(irp => irp
                                            .Prefetch<ItemPropertyEntity>(ip => ip.Properties)
                                                    .SubPath(ipe => ipe
                                                            .Prefetch<PropertyEntity>(pr => pr.Property)
                                                                .SubPath(pl => pl
                                                                        .Prefetch(vl => vl.ValueTypeLookup)
                                                                        .Prefetch(al => al.ApplicableTypeLookup))))
					  .Prefetch<ItemEntity>(c => c.Child)
                        .SubPath(ch => ch.Prefetch(s => s.TypeLookup))
						.SubPath(ch => ch.Prefetch(s => s.ListTypeLookup))
                        .SubPath(ch => ch.Prefetch(s => s.ItemSourceLookup))
                        .SubPath(m => m
                                .Prefetch<ItemDetailsEntity>(zzz => zzz.ItemDetail)
                                    .SubPath(e => e
                                            .Prefetch<ImageEntity>(ee => ee.Image).Exclude(kk => kk.Data)))
                                    .SubPath(irp => irp
                                            .Prefetch<ItemPropertyEntity>(ip => ip.Properties)
                                                    .SubPath(ipe => ipe
                                                            .Prefetch<PropertyEntity>(pr => pr.Property)
                                                                .SubPath(pl => pl
                                                                        .Prefetch(vl => vl.ValueTypeLookup)
                                                                        .Prefetch(al => al.ApplicableTypeLookup))))
                        .SubPath(ir => ir.Prefetch(ire => ire.Children))
                        .SubPath(k => k.Prefetch<ItemRelationEntity>(kk => kk.Parents)
                                       .SubPath(cp => cp.Prefetch<ItemEntity>(ccp => ccp.Child)
                                                        .SubPath(a => a.Prefetch(aa => aa.TypeLookup))
                                                        .SubPath(a => a.Prefetch(aa => aa.ListTypeLookup))
                                                        .SubPath(a => a.Prefetch(aa => aa.ItemSourceLookup))
                                                        .SubPath(cpp => cpp.Prefetch<ItemPropertyEntity>(ck => ck.Properties)
                                                                                .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property).SubPath(pl =>
                                                                                               pl.Prefetch(vl => vl.ValueTypeLookup)
                                                                                                 .Prefetch(al => al.ApplicableTypeLookup))))))

					  .Prefetch<LookupEntity>(rt=>rt.RelationTypeLookup);

		private readonly Func<IPathEdgeRootParser<ItemTargetEntity>, IPathEdgeRootParser<ItemTargetEntity>>
			_pathEdgesItemTarget =
				p => p.Prefetch(it => it.Item)
					  .Prefetch(it => it.TargetTypeLookup);


		private readonly Func<IPathEdgeRootParser<ItemPropertyEntity>, IPathEdgeRootParser<ItemPropertyEntity>>
			_pathEdgeItemProperty =
				p => p.Prefetch(ip => ip.Property)
						 .Prefetch(ip => ip.Item);


		private readonly Func<IPathEdgeRootParser<ItemRelationPropertyEntity>, IPathEdgeRootParser<ItemRelationPropertyEntity>>
			_pathEdgeItemRelationProperty =
				p => p.Prefetch(ip => ip.Property)
						 .Prefetch(ip => ip.ItemRelation);
		
		#endregion

		#region public Mehtods

		#region Item CRUD

		/// <summary>
		/// Loads Item by Id.
		/// </summary>
		/// <param name="id">The Id.</param>
		/// <returns>Item.</returns>
		public Item LoadItemById(int id)
		{
			Check.Argument.IsNotNegativeOrZero(id, "Id");

			try
			{
				using (var adapter = new DataAccessAdapter())
				{
					var source = new LinqMetaData(adapter);

					var itemEntity = source.Item.WithPath(_pathEdgesItem).FirstOrDefault(f => f.Id == id);

					if (itemEntity == null) return null;

					var item = Mapper.Map<ItemEntity, Item>(itemEntity);

					return item;
				}
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
			
		}

        /// <summary>
        /// Loads Item by Key.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>Item.</returns>
        public Item LoadItemByKey(string key)
        {
            Check.Argument.IsNotNull(key, "Key");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var source = new LinqMetaData(adapter);

                    var itemEntity = source.Item.WithPath(_pathEdgesItem).FirstOrDefault(f => f.Key == key);

                    if (itemEntity == null) return null;

                    var item = Mapper.Map<ItemEntity, Item>(itemEntity);

                    return item;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

	    /// <summary>
	    /// Load items depends on the parameters.
	    /// </summary>
	    /// <param name="name">The Name.</param>
	    /// <param name="targetTypeLookupId">The TargetTypeLookupId.</param>
	    /// <param name="itemSourceLookupId">The Item source lookup id.</param>
	    /// <param name="imageId">The Image Id.</param>
	    /// <param name="x">The X.</param>
	    /// <param name="y">The Y.</param>
	    /// <param name="userId">The UserId.</param>
	    /// <param name="creationDateTime">The CreatonDateTime.</param>
	    /// <param name="updatedDateTime">The UpdatedDateTime.</param>
	    /// <param name="fullName">The Full Name.</param>
	    /// <param name="typeLookupId">The Type Lookup Id.</param>
	    /// <param name="listTypeLookupId">The List Type Lookup Id.</param>
	    /// <param name="loadHiddenItems"></param>
	    /// <returns>List of Items.</returns>
        public BindingList<Item> LoadItems(string name,
                                            string key,
                                            string fullName,
                                            int typeLookupId,
                                            int listTypeLookupId,
                                            int targetTypeLookupId,
                                            int itemSourceLookupId,
                                            int imageId,
                                            int? x,
                                            int? y,
                                            int userId,
                                            DateTime? creationDateTime,
                                            DateTime? updatedDateTime,
                                            bool orderById,
                                            string searchKey,
                                            bool loadHiddenItems = false,
                                            bool includeHiddenChilds = true)
		{
			try
			{
                return LoadItemsWorker(name,key, fullName, typeLookupId, listTypeLookupId, targetTypeLookupId, itemSourceLookupId, imageId, x, y, userId, creationDateTime, updatedDateTime, orderById,searchKey, _pathEdgesItem,loadHiddenItems, includeHiddenChilds);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
			
		}

		/// <summary>
		/// Load items depends on the parameters.
		/// </summary>        
		/// <param name="targetTypeLookupId">The TargetTypeLookupId.</param>        
		/// <returns>List of Items.</returns>
        public BindingList<Item> LoadItems(int targetTypeLookupId, bool loadHiddenItems)
		{
			try
			{
				return LoadItemsWorker(targetTypeLookupId, loadHiddenItems);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}

		}

        /// <summary>
		/// Saves the passed Item.
		/// </summary>
		/// <param name="item">The Item.</param>
		/// <returns>ProcessResult.</returns>
		public ProcessResult Save(Item item)
		{
			Check.Argument.IsNotNull(() => item);

			try
			{
				var itemEntity = Mapper.Map<Item, ItemEntity>(item);

				itemEntity.IsNew = itemEntity.Id <= 0;

				var processResult = CommonRepository.Save(itemEntity);

				item.Id = itemEntity.Id;

				return processResult;
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}

		}

		/// <summary>
		/// Deletes the passed Item.
		/// </summary>
		/// <param name="item">The Item.</param>
		/// <returns>ProcessResult.</returns>
		public ProcessResult Delete(Item item)
		{
			Check.Argument.IsNotNull(() => item);
			
			try
			{
				var itemEntity = Mapper.Map<Item, ItemEntity>(item);

				return CommonRepository.Delete(itemEntity);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
			
		}
			
		#endregion

		#region Item Details CRUD

		/// <summary>
		/// Loads Item Details.
		/// </summary>
		public BindingList<ItemDetails> LoadItemDetails(int imageId, int? x, int? y, int userId, DateTime? creationDateTime, DateTime? updatedDateTime)
		{
			try
			{
				return LoadItemDetailsWorker(imageId, x, y, userId, creationDateTime, updatedDateTime, _pathEdgesItemDetails);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}

		}

		#endregion

		#region ItemRelation CRUD

		/// <summary>
		/// Loads ItemRelation By Id.
		/// </summary>
		/// <param name="id">The Id.</param>
		/// <returns>The ItemRelation.</returns>
		public ItemRelation LoadItemRelationById(int id)
		{
			Check.Argument.IsNotNegativeOrZero(id, "Id");

			try
			{
				using (var adapter = new DataAccessAdapter())
				{
					var source = new LinqMetaData(adapter);

					var itemRelationsEntity =
						source.ItemRelation.WithPath(_pathEdgesItemRelation).FirstOrDefault(r => r.Id == id);

					if (itemRelationsEntity == null) return null;

					var itemRelation = Mapper.Map<ItemRelationEntity, ItemRelation>(itemRelationsEntity);

					return itemRelation;
				}
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		/// <summary>
		/// Load ItemRelation depends on the passed parameters.
		/// </summary>
		/// <param name="childId">The ChiledId.</param>
		/// <param name="parentId">The ParentId.</param>
		/// <param name="userId">The UserId.</param>
		/// <param name="creationDateTime">The CreationDateTime.</param>
		/// <param name="updatedDateTime">The UpdatedDatetime.</param>
		/// <returns>List of itemRelation.</returns>
        public BindingList<ItemRelation> LoadItemRelations(int childId, int parentId, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, bool loadHiddenItems)
		{
			try
			{
				return LoadItemRleationsWorker(childId, parentId, userId, creationDateTime, updatedDateTime,
											   _pathEdgesItemRelation,loadHiddenItems);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		/// <summary>
		/// Saves the passed ItemRelation.
		/// </summary>
		/// <param name="itemRelation">The ItemRleation.</param>
		/// <returns></returns>
		public ProcessResult Save(ItemRelation itemRelation)
		{
			Check.Argument.IsNotNull(() => itemRelation);

			try
			{
				var itemRelationEntity = Mapper.Map<ItemRelation, ItemRelationEntity>(itemRelation);

				itemRelationEntity.IsNew = itemRelationEntity.Id <= 0;

				var processResult = CommonRepository.Save(itemRelationEntity);

				itemRelation.Id = itemRelationEntity.Id;

				return processResult;
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}

		}

		/// <summary>
		/// Deletes the passed ItemRelation.
		/// </summary>
		/// <param name="itemRelation">The ItemRelation.</param>
		/// <returns></returns>
		public ProcessResult Delete(ItemRelation itemRelation)
		{
			Check.Argument.IsNotNull(() => itemRelation);

			try
			{
				var itemRelationEntity = Mapper.Map<ItemRelation, ItemRelationEntity>(itemRelation);

				return CommonRepository.Delete(itemRelationEntity);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		#endregion

		#region ItemTarget CRUD

		/// <summary>
		/// Loads ItemTarget By Id.
		/// </summary>
		/// <param name="id">The Id.</param>
		/// <returns></returns>
		public ItemTarget LoadItemTargetById(int id)
		{
			Check.Argument.IsNotNegativeOrZero(id, "Id");

			try
			{
				using (var adapter = new DataAccessAdapter())
				{
					var source = new LinqMetaData(adapter);

					var itemTargetEntity = source.ItemTarget.WithPath(_pathEdgesItemTarget).FirstOrDefault(f => f.Id == id);

					if (itemTargetEntity == null) return null;

					var itemTarget = Mapper.Map<ItemTargetEntity, ItemTarget>(itemTargetEntity);

					return itemTarget;
				}
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		public BindingList<ItemTarget> LoadItemTargets(int itemId)
		{
			Check.Argument.IsNotNegativeOrZero(itemId, "Id");

			try
			{
				return LoadItemTargetsWorker(itemId);
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		/// <summary>
		/// Saves the passed ItemTarget.
		/// </summary>
		/// <param name="itemTarget">The ItemTarget.</param>
		/// <returns></returns>
		public ProcessResult Save(ItemTarget itemTarget)
		{
			Check.Argument.IsNotNull(() => itemTarget);

			try
			{
				var itemTargetEntity = Mapper.Map<ItemTarget, ItemTargetEntity>(itemTarget);

				itemTargetEntity.IsNew = itemTargetEntity.Id <= 0;

				var processResult = CommonRepository.Save(itemTargetEntity);

				itemTarget.Id = itemTargetEntity.Id;

				return processResult;
				
			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		/// <summary>
		/// Deletes the passed ItemTarget.
		/// </summary>
		/// <param name="itemTarget">The ItemTarget.</param>
		/// <returns></returns>
		public ProcessResult Delete(ItemTarget itemTarget)
		{
			Check.Argument.IsNotNull(() => itemTarget);

			try
			{
				var itemTargetEntity = Mapper.Map<ItemTarget, ItemTargetEntity>(itemTarget);

				return CommonRepository.Delete(itemTargetEntity);

			}
			catch (Exception exception)
			{
				throw new VitalDatabaseException(exception);
			}
		}

		#endregion

		#region ItemProperty CRUD

		/// <summary>
		/// Load item property by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public ItemProperty LoadItemPropertyById(int id)
		{
			Check.Argument.IsNotNegativeOrZero(id, "Id");

			try
			{
				using (var adapter = new DataAccessAdapter())
				{
					var source = new LinqMetaData(adapter);

					var itemProertyEntity =
						source.ItemProperty.WithPath(_pathEdgeItemProperty).FirstOrDefault(p => p.Id == id);

					if (itemProertyEntity == null) return null;

					var itemProerty = Mapper.Map<ItemPropertyEntity, ItemProperty>(itemProertyEntity);

					return itemProerty;
				}
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		/// <summary>
		/// Load item properties.
		/// </summary>
		/// <returns></returns>
		public BindingList<ItemProperty> LoadItemProperties(int itemId, string key)
		{
			try
			{
				return LoadItemPropertyWorker(itemId, key, _pathEdgeItemProperty);
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		/// <summary>
		/// Save item Property.
		/// </summary>
		/// <param name="itemProperty">The item property to save.</param>
		/// <returns></returns>
		public ProcessResult Save(ItemProperty itemProperty)
		{
			Check.Argument.IsNotNull(() => itemProperty);

			try
			{
				var itemPropertyEntity = Mapper.Map<ItemProperty, ItemPropertyEntity>(itemProperty);

				itemPropertyEntity.IsNew = itemPropertyEntity.Id <= 0;

				var result =  CommonRepository.Save(itemPropertyEntity);

				itemProperty.Id = itemPropertyEntity.Id;

				return result;
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		/// <summary>
		/// Delete item property.
		/// </summary>
		/// <param name="itemProperty">The item property to delete.</param>
		/// <returns></returns>
		public ProcessResult Delete(ItemProperty itemProperty)
		{
			Check.Argument.IsNotNull(() => itemProperty);

			try
			{
				var itemPropertyEntity = Mapper.Map<ItemProperty, ItemPropertyEntity>(itemProperty);

				return CommonRepository.Delete(itemPropertyEntity);
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		#endregion

		#region ItemRelationProperty CRUD

		/// <summary>
		/// Load item relation property by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public ItemRelationProperty LoadItemRelationPropertyById(int id)
		{
			Check.Argument.IsNotNegativeOrZero(id, "Id");

			try
			{
				using (var adapter = new DataAccessAdapter())
				{
					var source = new LinqMetaData(adapter);

					var itemRelationProertyEntity =
						source.ItemRelationProperty.WithPath(_pathEdgeItemRelationProperty).FirstOrDefault(p => p.Id == id);

					if (itemRelationProertyEntity == null) return null;

					var itemRelationProerty = Mapper.Map<ItemRelationPropertyEntity, ItemRelationProperty>(itemRelationProertyEntity);

					return itemRelationProerty;
				}
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		/// <summary>
		/// Load item relation properties.
		/// </summary>
		/// <returns></returns>
		public BindingList<ItemRelationProperty> LoadItemRelationProperties(int itemRelationId, string key)
		{
			try
			{
				return LoadItemRelationPropertyWorker(itemRelationId, key, _pathEdgeItemRelationProperty);
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		/// <summary>
		/// Save item relation Property.
		/// </summary>
		/// <param name="itemRelationProperty">The item property to save.</param>
		/// <returns></returns>
		public ProcessResult Save(ItemRelationProperty itemRelationProperty)
		{
			Check.Argument.IsNotNull(() => itemRelationProperty);

			try
			{
				var itemRelationPropertyEntity = Mapper.Map<ItemRelationProperty, ItemRelationPropertyEntity>(itemRelationProperty);

				itemRelationPropertyEntity.IsNew = itemRelationPropertyEntity.Id <= 0;

				var result = CommonRepository.Save(itemRelationPropertyEntity);

				itemRelationProperty.Id = itemRelationPropertyEntity.Id;

				return result;
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		/// <summary>
		/// Delete item relation property.
		/// </summary>
		/// <param name="itemRelationProperty">The item property to delete.</param>
		/// <returns></returns>
		public ProcessResult Delete(ItemRelationProperty itemRelationProperty)
		{
			Check.Argument.IsNotNull(() => itemRelationProperty);

			try
			{
				var itemRelationPropertyEntity = Mapper.Map<ItemRelationProperty, ItemRelationPropertyEntity>(itemRelationProperty);

				return CommonRepository.Delete(itemRelationPropertyEntity);
			}
			catch (Exception ex)
			{
				throw new VitalDatabaseException(ex);
			}
		}

		#endregion

		#endregion

		#region Private Workers

        /// <summary>
        /// Filter hidden items from loaded item
        /// </summary>
        /// <param name="item"></param>
        private static BindingList<Item> FilterHiddenItems(BindingList<Item> items)
	    {
            var lookupDatabaseRepository = new LookupDatabaseRepository();
            var userHiddenItemStateLookup = lookupDatabaseRepository.LoadLookups(LookupTypes.ItemState.ToString(), ItemStateEnum.UserHidden.ToString()).FirstOrDefault();
            var systemHiddenItemStateLookup = lookupDatabaseRepository.LoadLookups(LookupTypes.ItemState.ToString(), ItemStateEnum.SystemHidden.ToString()).FirstOrDefault();

            if (userHiddenItemStateLookup != null && systemHiddenItemStateLookup != null)
            {
                items = items.Where(i => i.IsItemActive(userHiddenItemStateLookup.Id, systemHiddenItemStateLookup.Id)).ToBindingList();
            }

            return items;
	    }

        /// <summary>
        /// Load items worker.
        /// </summary>        
        /// <param name="targetTypeLookupId">The target lookup id.</param>        
        /// <returns>List of Items.</returns>
        private static BindingList<Item> LoadItemsWorker(int targetTypeLookupId, bool loadHiddenItems)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var source = new LinqMetaData(adapter);

                var itemEntities = source.Item.AsQueryable();

                if (targetTypeLookupId > 0)
                    itemEntities = itemEntities.Where(i => i.ItemTargets != null && i.ItemTargets.Count(it => it.TargetTypeLookupId == targetTypeLookupId) > 0);

                itemEntities = itemEntities.OrderBy(c => c.Name);

                var items = new BindingList<Item>();

                Mapper.Map(itemEntities.ToList(), items);

                if (!loadHiddenItems)
                {
                    items = FilterHiddenItems(items);
                }

                return items;
            }
        }

	    /// <summary>
	    /// Load items worker.
	    /// </summary>
	    /// <param name="name">The Name.</param>
	    /// <param name="targetTypeLookupId">The target lookup id.</param>
	    /// <param name="itemSourceLookupId">The item source lookup id.</param>
	    /// <param name="imageId">The Image Id.</param>
	    /// <param name="x">The X.</param>
	    /// <param name="y">The Y.</param>
	    /// <param name="userId">The UserId.</param>
	    /// <param name="creationDateTime">The CreatonDateTime.</param>
	    /// <param name="updatedDateTime">The UpdatedDateTime.</param>
	    /// <param name="pathEdgesItem">The pathEdges.</param>
	    /// <param name="fullName">The Item full name.</param>
	    /// <param name="typeLookupId">the </param>
	    /// <param name="listTypeLookupId">The list Type Lookup Id</param>
	    /// <param name="count">The count of the items.</param>
	    /// <param name="justForCount">Is this call just for counting, don't return a list of items.</param>
	    /// <returns>List of Items.</returns>
        private static BindingList<Item> LoadItemsWorker(string name,
                                                        string key,
                                                        string fullName,
                                                        int typeLookupId,
                                                        int listTypeLookupId,
                                                        int targetTypeLookupId,
                                                        int itemSourceLookupId,
                                                        int imageId,
                                                        int? x,
                                                        int? y,
                                                        int userId,
                                                        DateTime? creationDateTime,
                                                        DateTime? updatedDateTime,
                                                        bool orderById,
                                                        string searchKey,
                                                        Func<IPathEdgeRootParser<ItemEntity>,
                                                        IPathEdgeRootParser<ItemEntity>> pathEdgesItem,
                                                        bool loadHiddenItems = false,
                                                        bool includeHiddenChilds = true)
		{
			Check.Argument.IsNotNull(() => pathEdgesItem);

			using (var adapter = new DataAccessAdapter())
			{
				var source = new LinqMetaData(adapter);

			    var tempPathEdge = pathEdgesItem;

                //The logic here determines if we want to exclude childs that are hidden when loaded with their parent item, the flag is usually set to true by default
                //to avoid calling this logic unless it is needed, we created this logic to handle an issue with data management where we load childs with their parents
                //instead of loading them separately like the rest of the system, in other system areas this issue is resoved because we already filter our deleted items
                //when loading them however we never handled filtering out childs of a parent item when loading the parent which is the case when loading parents in
                //data management, to handle this issue we added the logic below where we use a custom pathedge that filters out childs that has the system hidden property
                //with the hidden value.
                //Notice below that we are using Properties.Count()==0 and not using Properties.Any() for example because it was causing an error when using it because of casting
                //issue but using Count() works, we also add conditions Child == Null or Properties == Null because we don't want to filter out the whole record if any of these came as
                //null just in case, Also notice that we are filtering out on System hidden items only without including check for UserHidden items because we want to hide system hidden
                //items only which would still allow the user to see their hidden items under the parents they have listed them under in data management even though they can't see them
                //during testing.
			    if (!includeHiddenChilds)
			    {
                    var lookupDatabaseRepository = new LookupDatabaseRepository();
                    var systemHiddenItemStateLookup = lookupDatabaseRepository.LoadLookups(LookupTypes.ItemState.ToString(), ItemStateEnum.SystemHidden.ToString()).FirstOrDefault();

			        if (systemHiddenItemStateLookup != null)
			        {
                        var systemHiddenId = systemHiddenItemStateLookup.Id.ToString();
			            var itemStateKey = PropertiesEnum.ItemState.ToString();

                        tempPathEdge = p =>
                                    p.Prefetch<ItemDetailsEntity>(a => a.ItemDetail)
                                            .SubPath(k => k.Prefetch<ImageEntity>(kk => kk.Image).Exclude(kk => kk.Data))
                                        .Prefetch<ItemRelationEntity>(c => c.Children)
                                        .SubPath(cc => cc.Prefetch(c => c.Child))
                                        .Prefetch<ItemRelationEntity>(t => t.Parents).OrderBy(m => m.Order).FilterOn(ch => ch.Child.Properties.Count(pr => pr.Property.Key == itemStateKey &&
                                                                                                                                                           pr.Value == systemHiddenId) == 0)
                                        .SubPath(cp => cp.Prefetch(op => op.RelationTypeLookup)
                                                            .Prefetch<ItemEntity>(ccp => ccp.Child)
                                                            .SubPath(k => k.Prefetch<LookupEntity>(ck => ck.GenderLookup)
                                                                            .Prefetch<LookupEntity>(ck => ck.ListTypeLookup)
                                                                            .Prefetch<LookupEntity>(ck => ck.ItemSourceLookup)
                                                                            .Prefetch<LookupEntity>(ck => ck.TypeLookup)
                                                                            .Prefetch<ItemPropertyEntity>(ck => ck.Properties)
                                                                                                        .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property).SubPath(pl =>
                                                                                                                        pl.Prefetch(vl => vl.ValueTypeLookup)
                                                                                                                            .Prefetch(al => al.ApplicableTypeLookup)))))
                                    .Prefetch<LookupEntity>(s => s.ListTypeLookup)
                                     .Prefetch<LookupEntity>(s => s.ItemSourceLookup)
                                     .Prefetch<LookupEntity>(g => g.GenderLookup)
                                     .Prefetch<LookupEntity>(l => l.TypeLookup)
                                     .Prefetch<ItemPropertyEntity>(pl => pl.Properties)
                                          .SubPath(pe => pe.Prefetch<PropertyEntity>(pro => pro.Property).SubPath(pl => pl.Prefetch(vl => vl.ValueTypeLookup).Prefetch(al => al.ApplicableTypeLookup)))
                                     .Prefetch<UserEntity>(u => u.User)
                                     .Prefetch<ItemTargetEntity>(it => it.ItemTargets)
                                        .SubPath(tl => tl.Prefetch(l => l.TargetTypeLookup));
			        }
                    
			    }

                var itemEntities = source.Item.WithPath(tempPathEdge);

			    if (!string.IsNullOrEmpty(name))
					itemEntities = itemEntities.Where(i => i.Name.ToLower().Equals(name.ToLower()));

                if (!string.IsNullOrEmpty(key))
                    itemEntities = itemEntities.Where(i => i.Key.ToLower().Equals(key.ToLower()));

				if (!string.IsNullOrEmpty(fullName))
					itemEntities = itemEntities.Where(i => i.FullName.ToLower().Equals(fullName.ToLower()));

				if (typeLookupId > 0)
					itemEntities = itemEntities.Where(i => i.TypeLookupId == typeLookupId);

				if (listTypeLookupId > 0)
					itemEntities = itemEntities.Where(i => i.ListTypeLookupId == listTypeLookupId);

				if (targetTypeLookupId > 0)
					itemEntities = itemEntities.Where(i => i.ItemTargets != null && i.ItemTargets.Count(it => it.TargetTypeLookupId == targetTypeLookupId) > 0);

                if(itemSourceLookupId > 0)
                    itemEntities = itemEntities.Where(i => i.ItemSourceLookup != null && i.ItemSourceLookup.Id == itemSourceLookupId);

				if (imageId > 0)
					itemEntities = itemEntities.Where(i => i.ItemDetail.ImageId == imageId);

				if (x.HasValue)
					itemEntities = itemEntities.Where(i => i.ItemDetail.X == x);

				if (y.HasValue)
					itemEntities = itemEntities.Where(i => i.ItemDetail.Y == y);

				if (userId > 0)
					itemEntities = itemEntities.Where(i => i.UserId == userId);

				if (creationDateTime.HasValue)
					itemEntities = itemEntities.Where(i => i.CreationDateTime.Equals(creationDateTime));

				if (updatedDateTime.HasValue)
					itemEntities = itemEntities.Where(i => i.UpdatedDateTime.Equals(updatedDateTime));

                if (!string.IsNullOrEmpty(searchKey))
                    itemEntities = itemEntities.Where(s => (s.Name ?? string.Empty).Contains(searchKey.ToFullTextSearch()));

				itemEntities = orderById ? itemEntities.OrderBy(c => c.Id) : itemEntities.OrderBy(c => c.Name);

				var items = new BindingList<Item>();

                Mapper.Map(itemEntities.ToList(), items);

                if (!loadHiddenItems)
                {
                    items = FilterHiddenItems(items);
                }

				return items;
			}
		}

        /// <summary>
        /// LoadItemDetailsWorker
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="userId"></param>
        /// <param name="creationDateTime"></param>
        /// <param name="updatedDateTime"></param>
        /// <param name="pathEdgesItemDetails"></param>
        /// <returns></returns>
		private static BindingList<ItemDetails> LoadItemDetailsWorker(int imageId, int? x, int? y, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, Func<IPathEdgeRootParser<ItemDetailsEntity>, IPathEdgeRootParser<ItemDetailsEntity>> pathEdgesItemDetails)
		{
			Check.Argument.IsNotNull(() => pathEdgesItemDetails);

			using (var adapter = new DataAccessAdapter())
			{
				var source = new LinqMetaData(adapter);

				var itemDetailEntities = source.ItemDetails.WithPath(pathEdgesItemDetails);
			  
				if (imageId > 0)
					itemDetailEntities = itemDetailEntities.Where(i => i.ImageId == imageId);

				if (x.HasValue)
					itemDetailEntities = itemDetailEntities.Where(i => i.X == x);

				if (y.HasValue)
					itemDetailEntities = itemDetailEntities.Where(i => i.Y == y);

				if (userId > 0)
					itemDetailEntities = itemDetailEntities.Where(i => i.UserId == userId);

				if (creationDateTime.HasValue)
					itemDetailEntities = itemDetailEntities.Where(i => i.CreationDateTime.Equals(creationDateTime));

				if (updatedDateTime.HasValue)
					itemDetailEntities = itemDetailEntities.Where(i => i.UpdatedDateTime.Equals(updatedDateTime));

				var itemDetails = new BindingList<ItemDetails>();

				Mapper.Map(itemDetailEntities.ToList(), itemDetails);

				return itemDetails;
			}
		}

	    /// <summary>
	    /// Load ItemRelation Worker.
	    /// </summary>
	    /// <param name="childId">The ChiledId.</param>
	    /// <param name="parentId">The ParentId.</param>
	    /// <param name="userId">The UserId.</param>
	    /// <param name="creationDateTime">The CreationDateTime.</param>
	    /// <param name="updatedDateTime">The UpdatedDatetime.</param>
	    /// <param name="pathEdgesItemRelation">The path edges.</param>
	    /// <param name="loadHiddenItems"></param>
	    /// <returns>List of itemRelation.</returns>
	    private BindingList<ItemRelation> LoadItemRleationsWorker(int childId, int parentId, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, Func<IPathEdgeRootParser<ItemRelationEntity>, IPathEdgeRootParser<ItemRelationEntity>> pathEdgesItemRelation, bool loadHiddenItems = false)
		{
			Check.Argument.IsNotNull(() => pathEdgesItemRelation);

			using (var adapter = new DataAccessAdapter())
			{
				var source = new LinqMetaData(adapter);

				var itemRelationsEntities = source.ItemRelation.WithPath(pathEdgesItemRelation);

				if (childId > 0)
					itemRelationsEntities = itemRelationsEntities.Where(r => r.ItemChildId == childId);

				if (parentId > 0)
					itemRelationsEntities = itemRelationsEntities.Where(r => r.ItemParentId == parentId);

				if (userId > 0)
					itemRelationsEntities = itemRelationsEntities.Where(r => r.UserId == userId);

				if (creationDateTime != null)
					itemRelationsEntities = itemRelationsEntities.Where(r => r.CreationDateTime.Equals(creationDateTime));

				if (updatedDateTime != null)
					itemRelationsEntities = itemRelationsEntities.Where(r => r.UpdatedDateTime.Equals(updatedDateTime));

				//This is important for sorting custom dilutions and reading order
				//don't remove it unless you have better solution!
				var itemRelationsList = itemRelationsEntities.ToList();
										
				itemRelationsEntities = itemRelationsList.Any(c => c.Order > 0) ? itemRelationsEntities.OrderBy(r => r.Order).ThenBy(c => c.Child.Id) : itemRelationsEntities.OrderBy(r => r.Child.Name);
   
				var itemRelations = new BindingList<ItemRelation>();
				
				Mapper.Map(itemRelationsEntities.ToList(), itemRelations);

                if (!loadHiddenItems)
                {
                    var lookupDatabaseRepository = new LookupDatabaseRepository();
                    var userHiddenItemStateLookup = lookupDatabaseRepository.LoadLookups(LookupTypes.ItemState.ToString(), ItemStateEnum.UserHidden.ToString()).FirstOrDefault();
                    var systemHiddenItemStateLookup = lookupDatabaseRepository.LoadLookups(LookupTypes.ItemState.ToString(), ItemStateEnum.SystemHidden.ToString()).FirstOrDefault();

                    if (userHiddenItemStateLookup != null && systemHiddenItemStateLookup != null)
                    {
                        if (childId > 0)
                        {
                            itemRelations = itemRelations.Where(i => i.Parent.IsItemActive(userHiddenItemStateLookup.Id, systemHiddenItemStateLookup.Id)).ToBindingList();
                        }
                        else if (parentId > 0)
                        {
                            itemRelations = itemRelations.Where(i => i.Child.IsItemActive(userHiddenItemStateLookup.Id, systemHiddenItemStateLookup.Id)).ToBindingList();
                        }                       
                    }
                }

				return itemRelations;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		private BindingList<ItemTarget> LoadItemTargetsWorker(int itemId)
		{
			Check.Argument.IsNotNull(() => itemId);

			using (var adapter = new DataAccessAdapter())
			{
				var source = new LinqMetaData(adapter);

				var itemTargetEntities = source.ItemTarget.Where(c => c.ItemId == itemId);
			 
				var itemTargets = new BindingList<ItemTarget>();

				Mapper.Map(itemTargetEntities.ToList(), itemTargets);

				return itemTargets;
			}
		}

		/// <summary>
		/// Load the item properties worker.
		/// </summary>
		/// <param name="itemId"></param>
		/// <param name="key"></param>
		/// <param name="pathEdgeItemProperty">The pathedge to use. </param>
		/// <returns></returns>
		private BindingList<ItemProperty> LoadItemPropertyWorker(int itemId, string key, Func<IPathEdgeRootParser<ItemPropertyEntity>, IPathEdgeRootParser<ItemPropertyEntity>> pathEdgeItemProperty)
		{
			using (var adapter = new DataAccessAdapter())
			{
				var source = new LinqMetaData(adapter);

				var itemPropertiesEntities = source.ItemProperty.WithPath(pathEdgeItemProperty);

				if (itemId > 0)
				{
					itemPropertiesEntities = itemPropertiesEntities.Where(p => p.ItemId == itemId);
				}
				else if (!string.IsNullOrEmpty(key))
				{
					itemPropertiesEntities = itemPropertiesEntities.Where(p => p.Property.Key.ToLower().Equals(key));
				}
				
				var itemProperties = new BindingList<ItemProperty>();

				Mapper.Map(itemPropertiesEntities.ToList(), itemProperties);

				return itemProperties;

			}
		}

		/// <summary>
		/// Load the item properties worker.
		/// </summary>
		/// <param name="itemRelationId"></param>
		/// <param name="key">The Key.</param>
		/// <param name="pathEdgeItemRelationProperty"></param>
		/// <returns></returns>
		private BindingList<ItemRelationProperty> LoadItemRelationPropertyWorker(int itemRelationId, string key, Func<IPathEdgeRootParser<ItemRelationPropertyEntity>, IPathEdgeRootParser<ItemRelationPropertyEntity>> pathEdgeItemRelationProperty)
		{
			using (var adapter = new DataAccessAdapter())
			{
				var source = new LinqMetaData(adapter);

				var itemRelationPropertiesEntities = source.ItemRelationProperty.WithPath(pathEdgeItemRelationProperty);

				if (itemRelationId > 0)
				{
					itemRelationPropertiesEntities =
						itemRelationPropertiesEntities.Where(p => p.ItemRelationId == itemRelationId);
				}
				else if (!string.IsNullOrEmpty(key))
				{
					itemRelationPropertiesEntities = itemRelationPropertiesEntities.Where(p => p.Property.Key.ToLower().Equals(key));
				}

				var itemRelationProperties = new BindingList<ItemRelationProperty>();

				Mapper.Map(itemRelationPropertiesEntities.ToList(), itemRelationProperties);

				return itemRelationProperties;

			}
		}

		#endregion
	}
}
