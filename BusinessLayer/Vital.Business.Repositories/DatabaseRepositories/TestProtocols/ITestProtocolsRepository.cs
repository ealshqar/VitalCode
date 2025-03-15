using System.Collections.Generic;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.TestProtocols
{
    public interface ITestProtocolRepository
    {
        #region Test Prtocols
        
        /// <summary>
        /// Loads TestProtocol by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestProtocol</returns>
        TestProtocol LoadTestProtocolById(int id);

        /// <summary>
        /// Loads a list of TestProtocols.
        /// </summary>
        /// <returns>List of TestProtocols.</returns>
        BindingList<TestProtocol> LoadTestProtocols(string name, string description, LoadingTypeEnum loadingType);


        /// <summary>
        /// Saves a testProtocol.
        /// </summary>
        /// <param name="testProtocolToSave">The testProtocol.</param>
        /// <returns>The testProtocol.</returns>
        ProcessResult Save(TestProtocol testProtocolToSave);        

        /// <summary>
        /// Deletes a testProtocol.
        /// </summary>
        /// <param name="testProtocolToDelete">The testProtocol.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestProtocol testProtocolToDelete);    
   
        #endregion

        #region Protocol Steps

        /// <summary>
        /// Loads a protocol step.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The prtocol step.</returns>
        ProtocolStep LoadProtocolStepById(int id);

        /// <summary>
        /// Load a list of prtocol steps
        /// </summary>
        /// <param name="testProtocolId">The test protocol id.</param>
        /// <param name="order">The order.</param>
        /// <returns>The list of prtocol steps.</returns>
        BindingList<ProtocolStep> LoadProtocolSteps(int testProtocolId, int order);

        /// <summary>
        /// Saves the protocol step.
        /// </summary>
        /// <param name="protocolStepToSave">The protocol step.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(ProtocolStep protocolStepToSave);

        /// <summary>
        /// Deletes the prtocol step.
        /// </summary>
        /// <param name="protocolStepToDelete">The prtocol step to be deleted.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(ProtocolStep protocolStepToDelete);

        #endregion

        #region Protocol Items

        /// <summary>
        /// Loads protocol item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The protocol item.</returns>
        ProtocolItem LoadProtocolItemById(int id);

        /// <summary>
        /// Loads a list of protocol items.
        /// </summary>
        /// <param name="testProtocolId">The test protocol id.</param>
        /// <param name="itemId">The item id.</param>
        /// <returns>List of protocol items.</returns>
        BindingList<ProtocolItem> LoadProtocolItems(int testProtocolId, int itemId);

        /// <summary>
        /// Saves the protocol item.
        /// </summary>
        /// <param name="protocolItemToSave">The protocol item to be saved.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(ProtocolItem protocolItemToSave);

        /// <summary>
        /// Deletes the protocol item.
        /// </summary>
        /// <param name="protocolItemToDelete">The protocol item.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(ProtocolItem protocolItemToDelete);

        #endregion
    }
} 