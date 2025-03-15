using System;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Settings;
using Vital.Business.Repositories.DatabaseRepositories.TestProtocols;
using Vital.Business.Repositories.DatabaseRepositories.Tests;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class TestProtocolsManager : BaseManager
    {
        #region Private Variables

        private readonly ITestProtocolRepository _testProtocolsRepository;
        private readonly ITestRepository _testRepository;
        private readonly ISettingRepository _settingRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestProtocolsManager()
        {
            _testProtocolsRepository = new TestProtocolDatabaseRepository();
            _testRepository = new TestDatabaseRepository();
            _settingRepository = new SettingDatabaseRepository();
        }

        #endregion

        #region TestProtocols

        #region Public Methods

        /// <summary>
        /// Gets a testProtocol.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TestProtocol GetTestProtocolById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _testProtocolsRepository.LoadTestProtocolById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Gets a list of testProtocols.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<TestProtocol> GetTestProtocols(TestProtocolsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testProtocolsRepository.LoadTestProtocols(filter.Name , filter.Description, filter.LoadingType);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }



        /// <summary>
        /// Saves the testProtocol.
        /// </summary>
        /// <param name="testProtocol">The testProtocol.</param>
        /// <returns></returns>
        public ProcessResult SaveTestProtocol(TestProtocol testProtocol)
        {
            Check.Argument.IsNotNull(() => testProtocol);

            if (!testProtocol.IsChanged) return ProcessResult.Succeed;

            try
            { 
                testProtocol.SetUserAndDates();

                //save the test protocol.
                var processResult = _testProtocolsRepository.Save(testProtocol);

                //save the protocol steps.
                var processResultSteps = SaveProtocolSteps(testProtocol.ProtocolSteps);

                //save the protocol items.
                var processResultItems = SaveProtocolItems(testProtocol.ProtocolItems);

                if (processResult.IsSucceed && processResultItems.IsSucceed && processResultSteps.IsSucceed)
                {
                    testProtocol.ObjectState = DomainEntityState.Unchanged;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Check the passed protocol if can be deleted or not.
        /// </summary>
        /// <param name="testProtocol">The Test Protocol.</param>
        /// <returns>True : Can be delete. False: Cannot be deleted.</returns>
        public ProcessResult CanDeleteTestProtocol(TestProtocol testProtocol)
        {
            Check.Argument.IsNotNull(() => testProtocol);

            try
            {
                var hasTests =  _testRepository.LoadTests(string.Empty, 0, 0, testProtocol.Id, null, LoadingTypeEnum.None).Count() > 0;

                if(hasTests)
                    return new ProcessResult {IsSucceed = false, Message = StaticKeys.ProtocolHasRelatedTests};

                var defaultprotocol = _settingRepository.LoadSettingByKey(EnumNameResolver.Resolve(SettingKeys.DefaultTestProtocol));

                var isDefault = defaultprotocol == null ? false
                                                        : defaultprotocol.Value.Equals(testProtocol.Id.ToString());

                return isDefault ? new ProcessResult { IsSucceed = false, Message = StaticKeys.ProtocolIsDefault } 
                                 : new ProcessResult {IsSucceed = true};
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
            
        }


        /// <summary>
        /// Check the passed protocol if can be deleted or not.
        /// </summary>
        /// <param name="testProtocol">The Test Protocol.</param>
        /// <returns>True : Can be delete. False: Cannot be deleted.</returns>
        public ProcessResult CanEditTestProtocolSteps(TestProtocol testProtocol)
        {
            Check.Argument.IsNotNull(() => testProtocol);

            try
            {

                var hasTests = testProtocol.ObjectState != DomainEntityState.New && _testRepository.LoadTests(string.Empty, 0, 0, testProtocol.Id, null, LoadingTypeEnum.None).Count() > 0;

                return hasTests ? new ProcessResult { IsSucceed = false, Message = StaticKeys.ProtocolInUseForStepsEditing } : new ProcessResult { IsSucceed = true };
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }

        }

        /// <summary>
        /// Deletes a testProtocol.
        /// </summary>
        /// <param name="testProtocol">The testProtocol.</param>
        /// <returns></returns>
        public ProcessResult DeleteTestProtocol(TestProtocol testProtocol)
        {
            Check.Argument.IsNotNull(() => testProtocol);

            try
            {
                //delete protocol steps.
                var processResultSteps = DeleteProtocolSteps(testProtocol.ProtocolSteps);

                //delete protocol items.
                var processResultItems = DeleteProtocolItems(testProtocol.ProtocolItems);
                
                //delete test protocol.
                var  processResult = _testProtocolsRepository.Delete(testProtocol);

                if (processResult.IsSucceed && processResultItems.IsSucceed && processResultSteps.IsSucceed)
                {
                    testProtocol.ObjectState = DomainEntityState.Deleted;
                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the list of protocol steps.
        /// </summary>
        /// <param name="protocolSteps">The list of protocol steps.</param>
        /// <returns></returns>
        private ProcessResult SaveProtocolSteps(BindingList<ProtocolStep> protocolSteps)
        {
            Check.Argument.IsNotNull(() => protocolSteps);

            var stepsList = protocolSteps.ToList();

            try
            {
                var processResult = new ProcessResult { IsSucceed = true };

                foreach (var protocolStep in stepsList)
                {
                    if (protocolStep.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteProtocolStep(protocolStep);
                    }
                    else
                    {
                        protocolStep.SetUserAndDates();

                        processResult = _testProtocolsRepository.Save(protocolStep);
                    }

                    if (processResult.IsSucceed)
                    {
                        if (protocolStep.ObjectState != DomainEntityState.Deleted)
                            protocolStep.ObjectState = DomainEntityState.Unchanged;
                    }
                    else
                    {
                        processResult.IsSucceed = false;
                        break;
                    }

                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Saves the list of protocol items.
        /// </summary>
        /// <param name="protocolItems">The list of protocol items.</param>
        /// <returns></returns>
        private ProcessResult SaveProtocolItems(BindingList<ProtocolItem> protocolItems)
        {
            Check.Argument.IsNotNull(() => protocolItems);

            var itemsList = protocolItems.ToList();

            try
            {
                var processResult = new ProcessResult { IsSucceed = true };

                foreach (var protocolItem in itemsList)
                {
                    if (protocolItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteProtocolItem(protocolItem);
                    }
                    else
                    {
                        protocolItem.SetUserAndDates();

                        processResult = _testProtocolsRepository.Save(protocolItem);
                    }

                    if (processResult.IsSucceed)
                    {
                        if (protocolItem.ObjectState != DomainEntityState.Deleted)
                            protocolItem.ObjectState = DomainEntityState.Unchanged;
                    }
                    else
                    {
                        processResult.IsSucceed = false;
                        break;
                    }

                }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Deletes a list of protocol steps.
        /// </summary>
        /// <param name="protocolSteps">The list of protocol steps.</param>
        /// <returns></returns>
        public ProcessResult DeleteProtocolSteps(BindingList<ProtocolStep> protocolSteps)
        {
            Check.Argument.IsNotNull(() => protocolSteps);

            var result = new ProcessResult { IsSucceed = true };

            try
            {
                foreach (var protocolStep in protocolSteps)
                {
                    if (protocolStep.Id > 0)
                    {
                        result = _testProtocolsRepository.Delete(protocolStep);

                        protocolStep.ObjectState = DomainEntityState.Deleted;

                        if (!result.IsSucceed) return result;
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Deletes a list of protocol items.
        /// </summary>
        /// <param name="protocolItems">The list of protocol items.</param>
        /// <returns></returns>
        public ProcessResult DeleteProtocolItems(BindingList<ProtocolItem> protocolItems)
        {
            Check.Argument.IsNotNull(() => protocolItems);

            var result = new ProcessResult { IsSucceed = true };

            try
            {
                foreach (var protocolItem in protocolItems)
                {
                    if (protocolItem.Id > 0)
                    {
                        result = _testProtocolsRepository.Delete(protocolItem);

                        protocolItem.ObjectState = DomainEntityState.Deleted;

                        if (!result.IsSucceed) return result;
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion

        #endregion

        #region Protocol Steps

        #region Public Methods

        /// <summary>
        /// Gets a protocolStep.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ProtocolStep GetProtocolStepById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId,"item id");

            try
            {
                return _testProtocolsRepository.LoadProtocolStepById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Gets a list of protocolSteps.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<ProtocolStep> GetProtocolSteps(ProtocolStepsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testProtocolsRepository.LoadProtocolSteps(filter.TestProtocolId , filter.Order);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the protocolStep.
        /// </summary>
        /// <param name="protocolStep">The protocolStep.</param>
        /// <returns></returns>
        public ProcessResult SaveProtocolStep(ProtocolStep protocolStep)
        {
            Check.Argument.IsNotNull(() => protocolStep);

            if (!protocolStep.IsChanged) return ProcessResult.Succeed;

            try
            { 
                protocolStep.SetUserAndDates();

                var processResult = _testProtocolsRepository.Save(protocolStep);

                if (processResult.IsSucceed ) { protocolStep.ObjectState = DomainEntityState.Unchanged; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a protocolStep.
        /// </summary>
        /// <param name="protocolStep">The protocolStep.</param>
        /// <returns></returns>
        public ProcessResult DeleteProtocolStep(ProtocolStep protocolStep)
        {
            Check.Argument.IsNotNull(() => protocolStep);

            try
            {
                var processResult = _testProtocolsRepository.Delete(protocolStep);

                if (processResult.IsSucceed) { protocolStep.ObjectState = DomainEntityState.Deleted; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion

        #endregion  
        
        #region Protocol Items

        #region Public Methods

        /// <summary>
        /// Gets a protocolItem.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ProtocolItem GetProtocolItemById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _testProtocolsRepository.LoadProtocolItemById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }


        /// <summary>
        /// Gets a list of protocolItems.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<ProtocolItem> GetProtocolItems(ProtocolItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testProtocolsRepository.LoadProtocolItems(filter.TestProtocolId, filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the protocolItem.
        /// </summary>
        /// <param name="protocolItem">The protocolItem.</param>
        /// <returns></returns>
        public ProcessResult SaveProtocolItem(ProtocolItem protocolItem)
        {
            Check.Argument.IsNotNull(() => protocolItem);

            if (!protocolItem.IsChanged) return ProcessResult.Succeed;

            try
            { 
                protocolItem.SetUserAndDates();

                var processResult = _testProtocolsRepository.Save(protocolItem);

                if (processResult.IsSucceed ) { protocolItem.ObjectState = DomainEntityState.Unchanged; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a protocolItem.
        /// </summary>
        /// <param name="protocolItem">The protocolItem.</param>
        /// <returns></returns>
        public ProcessResult DeleteProtocolItem(ProtocolItem protocolItem)
        {
            Check.Argument.IsNotNull(() => protocolItem);

            try
            {
                var processResult = _testProtocolsRepository.Delete(protocolItem);

                if (processResult.IsSucceed) { protocolItem.ObjectState = DomainEntityState.Deleted; }

                return processResult;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        #endregion

        #endregion
    }
}