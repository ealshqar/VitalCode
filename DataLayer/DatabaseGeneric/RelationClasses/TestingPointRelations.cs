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
	/// <summary>Implements the relations factory for the entity: TestingPoint. </summary>
	public partial class TestingPointRelations
	{
		/// <summary>CTor</summary>
		public TestingPointRelations()
		{
		}

		/// <summary>Gets all relations of the TestingPointEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.AutoItemEntityUsingTestingPointsId);
			toReturn.Add(this.StageAutoItemEntityUsingTestingPointsId);
			toReturn.Add(this.UserEntityUsingUserId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between TestingPointEntity and AutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// TestingPoint.Id - AutoItem.TestingPointsId
		/// </summary>
		public virtual IEntityRelation AutoItemEntityUsingTestingPointsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "" , true);
				relation.AddEntityFieldPair(TestingPointFields.Id, AutoItemFields.TestingPointsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestingPointEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("AutoItemEntity", false);
				return relation;
			}
		}

		/// <summary>Returns a new IEntityRelation object, between TestingPointEntity and StageAutoItemEntity over the 1:n relation they have, using the relation between the fields:
		/// TestingPoint.Id - StageAutoItem.TestingPointsId
		/// </summary>
		public virtual IEntityRelation StageAutoItemEntityUsingTestingPointsId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "StageAutoItems" , true);
				relation.AddEntityFieldPair(TestingPointFields.Id, StageAutoItemFields.TestingPointsId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestingPointEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("StageAutoItemEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between TestingPointEntity and UserEntity over the m:1 relation they have, using the relation between the fields:
		/// TestingPoint.UserId - User.Id
		/// </summary>
		public virtual IEntityRelation UserEntityUsingUserId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "User", false);
				relation.AddEntityFieldPair(UserFields.Id, TestingPointFields.UserId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestingPointEntity", true);
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
	internal static class StaticTestingPointRelations
	{
		internal static readonly IEntityRelation AutoItemEntityUsingTestingPointsIdStatic = new TestingPointRelations().AutoItemEntityUsingTestingPointsId;
		internal static readonly IEntityRelation StageAutoItemEntityUsingTestingPointsIdStatic = new TestingPointRelations().StageAutoItemEntityUsingTestingPointsId;
		internal static readonly IEntityRelation UserEntityUsingUserIdStatic = new TestingPointRelations().UserEntityUsingUserId;

		/// <summary>CTor</summary>
		static StaticTestingPointRelations()
		{
		}
	}
}
