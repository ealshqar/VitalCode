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
	/// <summary>Implements the relations factory for the entity: IssueNavigationStep. </summary>
	public partial class IssueNavigationStepRelations
	{
		/// <summary>CTor</summary>
		public IssueNavigationStepRelations()
		{
		}

		/// <summary>Gets all relations of the IssueNavigationStepEntity as a list of IEntityRelation objects.</summary>
		/// <returns>a list of IEntityRelation objects</returns>
		public virtual List<IEntityRelation> GetAllRelations()
		{
			List<IEntityRelation> toReturn = new List<IEntityRelation>();
			toReturn.Add(this.IssueNavigationStepEntityUsingParentId);
			toReturn.Add(this.IssueNavigationStepEntityUsingIdParentId);
			toReturn.Add(this.ItemEntityUsingItemId);
			toReturn.Add(this.TestIssueEntityUsingTestIssueId);
			return toReturn;
		}

		#region Class Property Declarations

		/// <summary>Returns a new IEntityRelation object, between IssueNavigationStepEntity and IssueNavigationStepEntity over the 1:n relation they have, using the relation between the fields:
		/// IssueNavigationStep.Id - IssueNavigationStep.ParentId
		/// </summary>
		public virtual IEntityRelation IssueNavigationStepEntityUsingParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "ChildSteps" , true);
				relation.AddEntityFieldPair(IssueNavigationStepFields.Id, IssueNavigationStepFields.ParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", true);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", false);
				return relation;
			}
		}


		/// <summary>Returns a new IEntityRelation object, between IssueNavigationStepEntity and IssueNavigationStepEntity over the m:1 relation they have, using the relation between the fields:
		/// IssueNavigationStep.ParentId - IssueNavigationStep.Id
		/// </summary>
		public virtual IEntityRelation IssueNavigationStepEntityUsingIdParentId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ParentStep", false);
				relation.AddEntityFieldPair(IssueNavigationStepFields.Id, IssueNavigationStepFields.ParentId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between IssueNavigationStepEntity and ItemEntity over the m:1 relation they have, using the relation between the fields:
		/// IssueNavigationStep.ItemId - Item.Id
		/// </summary>
		public virtual IEntityRelation ItemEntityUsingItemId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "Item", false);
				relation.AddEntityFieldPair(ItemFields.Id, IssueNavigationStepFields.ItemId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ItemEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", true);
				return relation;
			}
		}
		/// <summary>Returns a new IEntityRelation object, between IssueNavigationStepEntity and TestIssueEntity over the m:1 relation they have, using the relation between the fields:
		/// IssueNavigationStep.TestIssueId - TestIssue.Id
		/// </summary>
		public virtual IEntityRelation TestIssueEntityUsingTestIssueId
		{
			get
			{
				IEntityRelation relation = new EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "TestIssue", false);
				relation.AddEntityFieldPair(TestIssueFields.Id, IssueNavigationStepFields.TestIssueId);
				relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("TestIssueEntity", false);
				relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("IssueNavigationStepEntity", true);
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
	internal static class StaticIssueNavigationStepRelations
	{
		internal static readonly IEntityRelation IssueNavigationStepEntityUsingParentIdStatic = new IssueNavigationStepRelations().IssueNavigationStepEntityUsingParentId;
		internal static readonly IEntityRelation IssueNavigationStepEntityUsingIdParentIdStatic = new IssueNavigationStepRelations().IssueNavigationStepEntityUsingIdParentId;
		internal static readonly IEntityRelation ItemEntityUsingItemIdStatic = new IssueNavigationStepRelations().ItemEntityUsingItemId;
		internal static readonly IEntityRelation TestIssueEntityUsingTestIssueIdStatic = new IssueNavigationStepRelations().TestIssueEntityUsingTestIssueId;

		/// <summary>CTor</summary>
		static StaticIssueNavigationStepRelations()
		{
		}
	}
}
