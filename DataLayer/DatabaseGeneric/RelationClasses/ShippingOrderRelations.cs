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
using System.Collections;
using System.Collections.Generic;
using Vital.DataLayer;
using Vital.DataLayer.FactoryClasses;
using Vital.DataLayer.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Vital.DataLayer.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: ShippingOrder. </summary>
	public partial class ShippingOrderRelations
	{
		/// <summary>CTor</summary>
		public ShippingOrderRelations()
		{
		}

		/// <summary>Gets all relations of the ShippingOrderEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.OrderItemEntityUsingShippingOrderId);
			toReturn.Add(this.LookupEntityUsingShippingMethodLookupId);
			toReturn.Add(this.TestEntityUsingTestId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between ShippingOrderEntity and OrderItemEntity over the 1:n relation they have, using the relation between the fields:
		/// ShippingOrder.Id - OrderItem.ShippingOrderId
		/// </summary>
		public virtual IEntityRelation OrderItemEntityUsingShippingOrderId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "OrderItems" , true);
				relation.AddEntityFieldPair(ShippingOrderFields.Id, OrderItemFields.ShippingOrderId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("OrderItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between ShippingOrderEntity and LookupEntity over the m:1 relation they have, using the relation between the fields:
		/// ShippingOrder.ShippingMethodLookupId - Lookup.Id
		/// </summary>
		public virtual IEntityRelation LookupEntityUsingShippingMethodLookupId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ShippingMethod", false);
				relation.AddEntityFieldPair(LookupFields.Id, ShippingOrderFields.ShippingMethodLookupId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("LookupEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ShippingOrderEntity and TestEntity over the m:1 relation they have, using the relation between the fields:
		/// ShippingOrder.TestId - Test.Id
		/// </summary>
		public virtual IEntityRelation TestEntityUsingTestId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Test", false);
				relation.AddEntityFieldPair(TestFields.Id, ShippingOrderFields.TestId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ShippingOrderEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// ShippingOrder.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ShippingOrderFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ShippingOrderEntity", true);
				return relation;
			}
		}
		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSubTypeRelation(string subTypeEntityName) { return null; }
		/// <summary>stub, not used in this entity, only for TargetPerEntity entities.</summary>
		public virtual IEntityRelation GetSuperTypeRelation() { return null;}
		#endregion

		#region Included Code

		#endregion
	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticShippingOrderRelations
	{
		internal static readonly IEntityRelation OrderItemEntityUsingShippingOrderIdStatic = new ShippingOrderRelations().OrderItemEntityUsingShippingOrderId;
		internal static readonly IEntityRelation LookupEntityUsingShippingMethodLookupIdStatic = new ShippingOrderRelations().LookupEntityUsingShippingMethodLookupId;
		internal static readonly IEntityRelation TestEntityUsingTestIdStatic = new ShippingOrderRelations().TestEntityUsingTestId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ShippingOrderRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticShippingOrderRelations()
		{
		}
	}
}
