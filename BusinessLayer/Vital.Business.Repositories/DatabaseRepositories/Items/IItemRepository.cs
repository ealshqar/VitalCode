using System;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Items
{
    public interface IItemRepository
    {
        #region Item CRUD

        /// <summary>
        /// Loads Item by Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>Item.</returns>
        Item LoadItemById(int id);

        /// <summary>
        /// Loads Item by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Item.</returns>
        Item LoadItemByKey(string key);

        /// <summary>
        /// Load items depends on the parameters.
        /// </summary>
        /// <param name="name">The Name.</param>
        /// <param name="targetTypeLookupId">The target Type Lookup Id.</param>
        /// <param name="itemSourceLookupId">The item source lookup id. </param>
        /// <param name="imageId">The Image Id.</param>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <param name="userId">The UserId.</param>
        /// <param name="creationDateTime">The CreatonDateTime.</param>
        /// <param name="updatedDateTime">The UpdatedDateTime.</param>
        /// <param name="fullName">The Item Full Name.</param>
        /// <param name="typeLookupId">The Type Lookup Id.</param>
        /// <param name="listTypeLookupId">The List Type Lookup Id.</param>
        /// <param name="orderById"></param>
        /// <param name="loadHiddenItems"></param>
        /// <returns>List of Items.</returns>
        BindingList<Item> LoadItems(string name, string key, string fullName, int typeLookupId, int listTypeLookupId, int targetTypeLookupId, int itemSourceLookupId, int imageId, int? x, int? y, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, bool orderById, string searchKey, bool loadHiddenItems, bool includeHiddenChilds);

        /// <summary>
        /// Load items depends on the parameters.
        /// </summary>        
        /// <param name="targetTypeLookupId">The TargetTypeLookupId.</param>
        /// <param name="loadHiddenItems"></param>
        /// <returns>List of Items.</returns>
        BindingList<Item> LoadItems(int targetTypeLookupId, bool loadHiddenItems);

        /// <summary>
        /// Saves the passed Item.
        /// </summary>
        /// <param name="item">The Item.</param>
        /// <returns>ProcessResult.</returns>
        ProcessResult Save(Item item);

        /// <summary>
        /// Deletes the passed Item.
        /// </summary>
        /// <param name="item">The Item.</param>
        /// <returns>ProcessResult.</returns>
        ProcessResult Delete(Item item);

        #endregion

        #region Item Details CRUD

        /// <summary>
        /// Loads Item Details.
        /// </summary>
        BindingList<ItemDetails> LoadItemDetails(int imageId, int? x, int? y, int userId, DateTime? creationDateTime, DateTime? updatedDateTime);

        #endregion

        #region ItemRelation CRUD

        /// <summary>
        /// Loads ItemRelation By Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>The ItemRelation.</returns>
        ItemRelation LoadItemRelationById(int id);

        /// <summary>
        /// Load ItemRelation depends on the passed parameters.
        /// </summary>
        /// <param name="childId">The ChiledId.</param>
        /// <param name="parentId">The ParentId.</param>
        /// <param name="userId">The UserId.</param>
        /// <param name="creationDateTime">The CreationDateTime.</param>
        /// <param name="updatedDateTime">The UpdatedDatetime.</param>
        /// <returns>List of itemRelation.</returns>
        BindingList<ItemRelation> LoadItemRelations(int childId, int parentId, int userId, DateTime? creationDateTime, DateTime? updatedDateTime, bool loadHiddenItems);

        /// <summary>
        /// Saves the passed ItemRelation.
        /// </summary>
        /// <param name="itemRelation">The ItemRleation.</param>
        /// <returns></returns>
        ProcessResult Save(ItemRelation itemRelation);

        /// <summary>
        /// Deletes the passed ItemRelation.
        /// </summary>
        /// <param name="itemRelation">The ItemRelation.</param>
        /// <returns></returns>
        ProcessResult Delete(ItemRelation itemRelation);

        #endregion

        #region ItemTarget CRUD

        /// <summary>
        /// Loads ItemTarget By Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns></returns>
        ItemTarget LoadItemTargetById(int id);

        /// <summary>
        /// Loads Item Targets.
        /// </summary>
        /// <param name="itemId">The Id.</param>
        /// <returns></returns>
        BindingList<ItemTarget> LoadItemTargets(int itemId);

        /// <summary>
        /// Saves the passed ItemTarget.
        /// </summary>
        /// <param name="itemTarget">The ItemTarget.</param>
        /// <returns></returns>
        ProcessResult Save(ItemTarget itemTarget);

        /// <summary>
        /// Deletes the passed ItemTarget.
        /// </summary>
        /// <param name="itemTarget">The ItemTarget.</param>
        /// <returns></returns>
        ProcessResult Delete(ItemTarget itemTarget);

        #endregion

        #region ItemProperty CRUD

        /// <summary>
        /// Load item property by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        ItemProperty LoadItemPropertyById(int id);

        /// <summary>
        /// Load item properties.
        /// </summary>
        /// <returns></returns>
        BindingList<ItemProperty> LoadItemProperties(int itemId, string key);

        /// <summary>
        /// Save item Property.
        /// </summary>
        /// <param name="itemProperty">The item property to save.</param>
        /// <returns></returns>
        ProcessResult Save(ItemProperty itemProperty);

        /// <summary>
        /// Delete item property.
        /// </summary>
        /// <param name="itemProperty">The item property to delete.</param>
        /// <returns></returns>
        ProcessResult Delete(ItemProperty itemProperty);

        #endregion

        #region ItemRelationProperty CRUD

        /// <summary>
        /// Load item relation property by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        ItemRelationProperty LoadItemRelationPropertyById(int id);

        /// <summary>
        /// Load item relation properties.
        /// </summary>
        /// <returns></returns>
        BindingList<ItemRelationProperty> LoadItemRelationProperties(int itemRelationId, string key);

        /// <summary>
        /// Save item relation Property.
        /// </summary>
        /// <param name="itemRelationProperty">The item property to save.</param>
        /// <returns></returns>
        ProcessResult Save(ItemRelationProperty itemRelationProperty);

        /// <summary>
        /// Delete item relation property.
        /// </summary>
        /// <param name="itemRelationProperty">The item property to delete.</param>
        /// <returns></returns>
        ProcessResult Delete(ItemRelationProperty itemRelationProperty);

        #endregion

    }
}
