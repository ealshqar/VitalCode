using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.AutoTestSource;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.AutoTestSource
{
    public interface IAutoTestSourceRepository
    {
        #region TestingPoints

        /// <summary>
        /// Loads TestingPoint by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The TestingPoint</returns>
        TestingPoint LoadEntityById(TestingPointsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<TestingPoint> LoadEntities(TestingPointsFilter filter);

        /// <summary>
        /// Saves a testingPoint.
        /// </summary>
        /// <param name="testingPointToSave">The testingPoint.</param>
        /// <returns>The testingPoint.</returns>
        ProcessResult Save(TestingPoint testingPointToSave);

        /// <summary>
        /// Deletes a testingPoint.
        /// </summary>
        /// <param name="testingPointToDelete">The testingPoint.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestingPoint testingPointToDelete);

        #endregion

        #region AutoItems

        /// <summary>
        /// Loads AutoItem by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoItem</returns>
        AutoItem LoadEntityById(AutoItemsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoItem> LoadEntities(AutoItemsFilter filter);

        /// <summary>
        /// Saves a autoItem.
        /// </summary>
        /// <param name="autoItemToSave">The autoItem.</param>
        /// <returns>The autoItem.</returns>
        ProcessResult Save(AutoItem autoItemToSave);

        /// <summary>
        /// Deletes a autoItem.
        /// </summary>
        /// <param name="autoItemToDelete">The autoItem.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoItem autoItemToDelete);

        #endregion

        #region AutoItemRelations

        /// <summary>
        /// Loads AutoItemRelation by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoItemRelation</returns>
        AutoItemRelation LoadEntityById(AutoItemRelationsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoItemRelation> LoadEntities(AutoItemRelationsFilter filter);

        /// <summary>
        /// Saves a autoItemRelation.
        /// </summary>
        /// <param name="autoItemRelationToSave">The autoItemRelation.</param>
        /// <returns>The autoItemRelation.</returns>
        ProcessResult Save(AutoItemRelation autoItemRelationToSave);

        /// <summary>
        /// Deletes a autoItemRelation.
        /// </summary>
        /// <param name="autoItemRelationToDelete">The autoItemRelation.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoItemRelation autoItemRelationToDelete);

        #endregion

        #region AutoProtocols

        /// <summary>
        /// Loads AutoProtocol by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocol</returns>
        AutoProtocol LoadEntityById(AutoProtocolsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoProtocol> LoadEntities(AutoProtocolsFilter filter);

        /// <summary>
        /// Saves a autoProtocol.
        /// </summary>
        /// <param name="autoProtocolToSave">The autoProtocol.</param>
        /// <returns>The autoProtocol.</returns>
        ProcessResult Save(AutoProtocol autoProtocolToSave);

        /// <summary>
        /// Deletes a autoProtocol.
        /// </summary>
        /// <param name="autoProtocolToDelete">The autoProtocol.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoProtocol autoProtocolToDelete);

        #endregion

        #region AutoProtocolRevisions

        /// <summary>
        /// Loads AutoProtocolRevision by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocolRevision</returns>
        AutoProtocolRevision LoadEntityById(AutoProtocolRevisionsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoProtocolRevision> LoadEntities(AutoProtocolRevisionsFilter filter);

        /// <summary>
        /// Saves a autoProtocolRevision.
        /// </summary>
        /// <param name="autoProtocolRevisionToSave">The autoProtocolRevision.</param>
        /// <returns>The autoProtocolRevision.</returns>
        ProcessResult Save(AutoProtocolRevision autoProtocolRevisionToSave);

        /// <summary>
        /// Deletes a autoProtocolRevision.
        /// </summary>
        /// <param name="autoProtocolRevisionToDelete">The autoProtocolRevision.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoProtocolRevision autoProtocolRevisionToDelete);

        #endregion

        #region AutoProtocolStages

        /// <summary>
        /// Loads AutoProtocolStage by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocolStage</returns>
        AutoProtocolStage LoadEntityById(AutoProtocolStagesFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoProtocolStage> LoadEntities(AutoProtocolStagesFilter filter);

        /// <summary>
        /// Saves a autoProtocolStage.
        /// </summary>
        /// <param name="autoProtocolStageToSave">The autoProtocolStage.</param>
        /// <returns>The autoProtocolStage.</returns>
        ProcessResult Save(AutoProtocolStage autoProtocolStageToSave);

        /// <summary>
        /// Deletes a autoProtocolStage.
        /// </summary>
        /// <param name="autoProtocolStageToDelete">The autoProtocolStage.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoProtocolStage autoProtocolStageToDelete);

        #endregion

        #region AutoProtocolStageRevisions

        /// <summary>
        /// Loads AutoProtocolStageRevision by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoProtocolStageRevision</returns>
        AutoProtocolStageRevision LoadEntityById(AutoProtocolStageRevisionsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoProtocolStageRevision> LoadEntities(AutoProtocolStageRevisionsFilter filter);

        /// <summary>
        /// Saves a autoProtocolStageRevision.
        /// </summary>
        /// <param name="autoProtocolStageRevisionToSave">The autoProtocolStageRevision.</param>
        /// <returns>The autoProtocolStageRevision.</returns>
        ProcessResult Save(AutoProtocolStageRevision autoProtocolStageRevisionToSave);

        /// <summary>
        /// Deletes a autoProtocolStageRevision.
        /// </summary>
        /// <param name="autoProtocolStageRevisionToDelete">The autoProtocolStageRevision.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoProtocolStageRevision autoProtocolStageRevisionToDelete);

        #endregion

        #region AutoTestStages

        /// <summary>
        /// Loads AutoTestStage by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The AutoTestStage</returns>
        AutoTestStage LoadEntityById(AutoTestStagesFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<AutoTestStage> LoadEntities(AutoTestStagesFilter filter);

        /// <summary>
        /// Saves a autoTestStage.
        /// </summary>
        /// <param name="autoTestStageToSave">The autoTestStage.</param>
        /// <returns>The autoTestStage.</returns>
        ProcessResult Save(AutoTestStage autoTestStageToSave);

        /// <summary>
        /// Deletes a autoTestStage.
        /// </summary>
        /// <param name="autoTestStageToDelete">The autoTestStage.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(AutoTestStage autoTestStageToDelete);

        #endregion

        #region StageAnnouncements

        /// <summary>
        /// Loads StageAnnouncement by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The StageAnnouncement</returns>
        StageAnnouncement LoadEntityById(StageAnnouncementsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<StageAnnouncement> LoadEntities(StageAnnouncementsFilter filter);

        /// <summary>
        /// Saves a stageAnnouncement.
        /// </summary>
        /// <param name="stageAnnouncementToSave">The stageAnnouncement.</param>
        /// <returns>The stageAnnouncement.</returns>
        ProcessResult Save(StageAnnouncement stageAnnouncementToSave);

        /// <summary>
        /// Deletes a stageAnnouncement.
        /// </summary>
        /// <param name="stageAnnouncementToDelete">The stageAnnouncement.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(StageAnnouncement stageAnnouncementToDelete);

        #endregion

        #region StageAutoItems

        /// <summary>
        /// Loads StageAutoItem by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The StageAutoItem</returns>
        StageAutoItem LoadEntityById(StageAutoItemsFilter filter);

        /// <summary>
        /// Loads StageAutoItem by AutoItem Key.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The StageAutoItem</returns>
        StageAutoItem LoadEntityByAutoItemKey(AutoItemsFilter filter);

        /// <summary>
        /// Loads a list of Entities.
        /// </summary>
        /// <returns>List of Entities.</returns>
        BindingList<StageAutoItem> LoadEntities(StageAutoItemsFilter filter);

        /// <summary>
        /// Saves a stageAutoItem.
        /// </summary>
        /// <param name="stageAutoItemToSave">The stageAutoItem.</param>
        /// <returns>The stageAutoItem.</returns>
        ProcessResult Save(StageAutoItem stageAutoItemToSave);

        /// <summary>
        /// Deletes a stageAutoItem.
        /// </summary>
        /// <param name="stageAutoItemToDelete">The stageAutoItem.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(StageAutoItem stageAutoItemToDelete);

        #endregion

    }
}
