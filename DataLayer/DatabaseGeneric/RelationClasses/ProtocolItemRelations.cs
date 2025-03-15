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
	/// <summary>Implements the relations factory for the entity: ProtocolItem. </summary>
	public partial class ProtocolItemRelations
	{
		/// <summary>CTor</summary>
		public ProtocolItemRelations()
		{
		}

		/// <summary>Gets all relations of the ProtocolItemEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.TestProtocolEntityUsingTestProtocolId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations



		/// <summary>Returns a new IEntityRelation object, between ProtocolItemEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// ProtocolItem.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, ProtocolItemFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProtocolItemEntity and TestProtocolEntity over the m:1 relation they have, using the relation between the fields:
		/// ProtocolItem.TestProtocolId - TestProtocol.Id
		/// </summary>
		public virtual IEntityRelation TestProtocolEntityUsingTestProtocolId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestProtocol", false);
				relation.AddEntityFieldPair(TestProtocolFields.Id, ProtocolItemFields.TestProtocolId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestProtocolEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolItemEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between ProtocolItemEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// ProtocolItem.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, ProtocolItemFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ProtocolItemEntity", true);
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
	internal static class StaticProtocolItemRelations
	{
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new ProtocolItemRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation TestProtocolEntityUsingTestProtocolIdStatic = new ProtocolItemRelations().TestProtocolEntityUsingTestProtocolId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new ProtocolItemRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticProtocolItemRelations()
		{
		}
	}
}
