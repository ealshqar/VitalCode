using System;
using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Tests
{
    public interface ITestRepository
    {
        #region Test

        /// <summary>
        /// Loads the test by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The test.</returns>
        Test LoadTestById(int id);

        /// <summary>
        /// Loads test and Major Issues and test results only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Test LoadTestAndMajorIssuesAndFactorsById(int id);

        /// <summary>
        /// Loads all tests.
        /// </summary>
        /// <returns>The list of tests.</returns>
        BindingList<Test> LoadTests(string name, int patientId, int itemId, int testProtocolId, DateTime? dateTime, LoadingTypeEnum loadingType);

        /// <summary>
        /// Saves the test.
        /// </summary>
        /// <param name="testToSave">The test to save.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(Test testToSave);

        /// <summary>
        /// Deletes the test.
        /// </summary>
        /// <param name="testToDelete">The test to delete.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Test testToDelete);

        #endregion

        #region Test Issue

        /// <summary>
        /// Loads TestIssue by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestIssue</returns>
        TestIssue LoadTestIssueById(int id);

        /// <summary>
        /// Loads a list of TestIssues.
        /// </summary>
        /// <returns>List of TestIssues.</returns>
        BindingList<TestIssue> LoadTestIssues(string name, int testId, int protocolStepId, int itemId, TestIssuesLoadingType issuesLoadingType);

        /// <summary>
        /// Saves a testIssue.
        /// </summary>
        /// <param name="testIssueToSave">The testIssue.</param>
        /// <returns>The testIssue.</returns>
        ProcessResult Save(TestIssue testIssueToSave);

        /// <summary>
        /// Deletes a testIssue.
        /// </summary>
        /// <param name="testIssueToDelete">The testIssue.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestIssue testIssueToDelete);

        #endregion

        #region Test Imprintable Item

        /// <summary>
        /// Loads TestImprintableItem by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The TestImprintableItem</returns>
        TestImprintableItem LoadTestImprintableItemById(int id);

        /// <summary>
        /// Loads a list of TestImprintableItems.
        /// </summary>
        /// <returns>List of TestImprintableItems.</returns>
        BindingList<TestImprintableItem> LoadTestImprintableItems(int testId, int itemId);

        /// <summary>
        /// Saves a TestImprintableItem.
        /// </summary>
        /// <param name="TestImprintableItemToSave">The TestImprintableItem.</param>
        /// <returns>The TestImprintableItem.</returns>
        ProcessResult Save(TestImprintableItem testImprintableItemToSave);

        /// <summary>
        /// Deletes a TestImprintableItem.
        /// </summary>
        /// <param name="TestImprintableItemToDelete">The TestImprintableItem.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestImprintableItem testImprintableItemToDelete);

        #endregion

        #region Test Service

        /// <summary>
        /// Loads TestService by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TestService LoadTestServiceById(int id);

        /// <summary>
        /// Loads a list of TestServices.
        /// </summary>
        /// <returns>List of TestServices.</returns>
        BindingList<TestService> LoadTestServices(string key, string name, int typeLookupId, int testId, int serviceId);

        /// <summary>
        /// Saves a testService.
        /// </summary>
        /// <param name="testServiceToSave">The testService.</param>
        /// <returns>The testService.</returns>
        ProcessResult Save(TestService testServiceToSave);

        /// <summary>
        /// Deletes a testService.
        /// </summary>
        /// <param name="testServiceToDelete">The testService.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestService testServiceToDelete);

        #endregion

        #region Test Result

        /// <summary>
        /// Gets a test result.
        /// </summary>
        /// <param name="id">The test result id.</param>
        /// <returns></returns>
        TestResult LoadTestResultById(int id);

        /// <summary>
        /// Gets a list of tests results
        /// </summary>
        /// <returns></returns>
        BindingList<TestResult> LoadTestResults(int testIssueId, int itemId, int parentId, int vitalForceId, bool isSelected);

        /// <summary>
        /// Saves the test result.
        /// </summary>
        /// <param name="testResultToSave">The test result to save.</param>
        /// <returns></returns>
        ProcessResult Save(TestResult testResultToSave);

        /// <summary>
        /// Deletes the test result.
        /// </summary>
        /// <param name="testResultToDelete"></param>
        /// <returns></returns>
        ProcessResult Delete(TestResult testResultToDelete);

        #endregion

        #region Navigation Step

        /// <summary>
        /// Loads IssueNavigationStep by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The IssueNavigationStep</returns>
        IssueNavigationStep LoadIssueNavigationStepById(int id);

        /// <summary>
        /// Loads a list of IssueNavigationSteps.
        /// </summary>
        /// <returns>List of IssueNavigationSteps.</returns>
        BindingList<IssueNavigationStep> LoadIssueNavigationSteps(int order, int issueId);

        /// <summary>
        /// Saves a issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationStepToSave">The issueNavigationStep.</param>
        /// <returns>The issueNavigationStep.</returns>
        ProcessResult Save(IssueNavigationStep issueNavigationStepToSave);

        /// <summary>
        /// Deletes a issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationStepToDelete">The issueNavigationStep.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(IssueNavigationStep issueNavigationStepToDelete);

        #endregion

        #region Test Result Factor

        /// <summary>
        /// Loads TestResultFactor by id.
        /// </summary>
        /// <param name="id">The id.</param>
        TestResultFactor LoadTestResultFactorById(int id);

        /// <summary>
        /// Loads a list of TestResultFactors.
        /// </summary>
        /// <returns>List of TestResultFactor.</returns>
        BindingList<TestResultFactor> LoadTestResultFactors(int factorId, int potencyId, int testResultId);

        /// <summary>
        /// Saves a TestResultFactor.
        /// </summary>
        /// <param name="testResultFactorToSave">The TestResultFactor.</param>
        /// <returns>The result.</returns>
        ProcessResult Save(TestResultFactor testResultFactorToSave);

        /// <summary>
        /// Deletes a TestResultFactor.
        /// </summary>
        /// <param name="testResultFactorToDelete">The TestResultFactor.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(TestResultFactor testResultFactorToDelete);

        #endregion
    }
}
