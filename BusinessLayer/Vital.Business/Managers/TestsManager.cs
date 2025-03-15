using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Invoices;
using Vital.Business.Repositories.DatabaseRepositories.Tests;
using Vital.Business.Repositories.DatabaseRepositories.TestSchedules;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class TestsManager : BaseManager
    {
        #region Private Variables

        private readonly ITestRepository _testsRepository;
        private readonly ITestSchedulesRepository _testSchedulesRepository;
        private readonly IInvoicesRepository _invoicesRepository;
        private readonly ReadingsManager _readingsManager;
        private readonly ShippingOrdersManager _shippingOrdersManager;

        #endregion

        #region Constructors
            
        /// <summary>
        /// The Constructor.
        /// </summary>
        public TestsManager()
        {
            _testsRepository = new TestDatabaseRepository();
            _testSchedulesRepository = new TestSchedulesDatabaseRepository();
            _invoicesRepository = new InvoicesDatabaseRepository();
            _readingsManager = new ReadingsManager();
            _shippingOrdersManager = new ShippingOrdersManager();
        }

        #endregion

        #region Public Methods

        #region Tests

        /// <summary>
        /// Gets a test with major issues and test results and their factors only
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Test GetTestAndMajorIssuesAndTestResultsById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestAndMajorIssuesAndFactorsById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a test.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Test GetTestById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                var test = _testsRepository.LoadTestById(filter.ItemId);
                var testMainIssue = GetTestMainIssue(test);

                if (testMainIssue != null)
                {
                    test.TestMainIssue = testMainIssue;
                    test.TestMainIssue.Test = test;
                }
               
                return test;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of tests.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Test> GetTests(TestsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTests(filter.Name, filter.PatientId, filter.ItemId, filter.TestProtocolId, filter.DateTime, filter.LoadingType);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }
        
        /// <summary>
        /// Saves the test.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <returns></returns>
        public ProcessResult SaveTest(Test test)
        {
            Check.Argument.IsNotNull(() => test);

            if (!test.IsChanged) return ProcessResult.Succeed;

            try
            {
                test.SetUserAndDates();

                //saving the test schedule
                var testScheduleProcessResult = SaveTestSchedule(test.TestSchedule);

                //Save the test.
                var processResult = _testsRepository.Save(test);

                if (!processResult.IsSucceed) return processResult;

                bool imprintableItemsSaved = false;

                //Save the deleted test imprintable items.
                if (test.DeletedTestImprintableItems != null && test.DeletedTestImprintableItems.Count > 0)
                {
                    //If there are test imprintable items to save and also there are deleted items that have been saved in DB before,
                    //then save all the items before deleting to make sure parent relations are cleared out.
                    if (test.TestImprintableItems != null && 
                        test.TestImprintableItems.Count > 0 &&
                        test.DeletedTestImprintableItems.Count(d=>d.Id != 0) != 0)
                    {
                        //Save test issues that are not deleted to prevent saving an imprintable item that is new before saving its test result in DB
                        var testIssuesToKeep = test.TestIssues.Where(ti => ti.ObjectState != DomainEntityState.Deleted).ToBindingList();
                        if (testIssuesToKeep != null && testIssuesToKeep.Count > 0)
                        {
                            processResult = SaveTestIssues(testIssuesToKeep);

                            if (!processResult.IsSucceed) return processResult;
                        }

                        //Save the test main issue since it will be kept anyway and it can't be removed by user
                        if (test.TestMainIssue != null)
                        {
                            processResult = SaveTestIssue(test.TestMainIssue);

                            if (!processResult.IsSucceed) return processResult;
                        }

                        //Order the items by their level to make sure that parents gets saved before their sub items which avoids having DB dependency issues
                        test.UpdateImprintableItemsLevel();

                        //First save rows that are not deleted to first clear the relation with rows that yet to be deleted later if any
                        processResult = SaveTestImprintableItems(test.TestImprintableItemsOrderdByLevel);

                        imprintableItemsSaved = true;

                        if (!processResult.IsSucceed) return processResult;
                    }
                    
                    //Delete the deleted test imprintable items after clearing the parent column in the DB for the items that are childs to those deleted items
                    if (test.DeletedTestImprintableItems != null)
                    {
                        //Order the items by their level to make sure that parents gets saved before their sub items which avoids having DB dependency issues
                        test.UpdateImprintableItemsLevel(true);

                        processResult = SaveTestImprintableItems(test.DeletedTestImprintableItemsOrderdByLevelReverse);

                        test.DeletedTestImprintableItems.Clear();

                        if (!processResult.IsSucceed) return processResult;
                    }

                    if (!processResult.IsSucceed) return processResult;
                }

                //Save the test issues.
                if (test.TestIssues != null)
                {
                    processResult = SaveTestIssues(test.TestIssues);
                    
                    if (!processResult.IsSucceed) return processResult;
                }

                //Save the test main issue.
                if (test.TestMainIssue != null)
                {
                    processResult = SaveTestIssue(test.TestMainIssue);

                    if (!processResult.IsSucceed) return processResult;
                }

                //Save the test imprintable items that are modified or new, this must be called after saving test issues and test results to prevent
                //dependancy errors
                if (!imprintableItemsSaved && test.TestImprintableItems != null && test.TestImprintableItems.Count > 0)
                {
                    test.UpdateImprintableItemsLevel();

                    //First save rows that are not deleted to first clear the relation with rows that yet to be deleted later if any
                    processResult = SaveTestImprintableItems(test.TestImprintableItemsOrderdByLevel);

                    imprintableItemsSaved = true;
                   
                    if (!processResult.IsSucceed) return processResult;
                }

                if (imprintableItemsSaved)
                {
                    //Load Test imprintable items since the original list will not be modified because we are saving using a custom ordered list
                    test.TestImprintableItems = GetTestImprintableItems(new TestImprintableItemsFilter() { TestId = test.Id });
                }

                //Save the test services.
                if (test.TestServices != null)
                {
                    processResult = SaveTestServices(test.TestServices);

                    if (!processResult.IsSucceed) return processResult;
                }
            
                test.ObjectState = DomainEntityState.Unchanged;             

                return processResult;

            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        private ProcessResult SaveTestSchedule(TestSchedule testSchedule)
        {
            Check.Argument.IsNotNull(() => testSchedule);
            
            try
            {
                 var processResultScheduleLines = new ProcessResult { IsSucceed = true };

                testSchedule.SetUserAndDates();

                var processResultTestSchedule = _testSchedulesRepository.Save(testSchedule);

                for (var i = 0; i < testSchedule.ScheduleLines.Count; i++)
                {
                    var scheduleLine = testSchedule.ScheduleLines[i];

                    if(scheduleLine.ObjectState == DomainEntityState.Deleted)
                    {
                        processResultScheduleLines = DeleteScheduleLine(scheduleLine);
                        testSchedule.ScheduleLines.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                         processResultScheduleLines = SaveScheduleLine(scheduleLine);
                    }

                    if (!processResultScheduleLines.IsSucceed) break;
                }

                return new ProcessResult()
                           {
                               IsSucceed = processResultScheduleLines.IsSucceed && processResultTestSchedule.IsSucceed
                           };
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Deletes a test.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <returns></returns>
        public ProcessResult DeleteTest(Test test)
        {
            Check.Argument.IsNotNull(() => test);

            try
            {
                var processResult = new ProcessResult() {IsSucceed = true};

                //Delete the readings.
                test.Readings = _readingsManager.GetReadings(new ReadingsFilter { TestId = test.Id });


                if (test.Readings != null)
                {
                    foreach (var reading in test.Readings)
                    {
                        processResult = _readingsManager.DeleteReading(reading);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                //Delete the test imprintable items.
                test.TestImprintableItems = GetTestImprintableItems(new TestImprintableItemsFilter() { TestId = test.Id });
                test.UpdateImprintableItemsLevel();

                if (test.TestImprintableItems != null)
                {
                    foreach (var imprintableItem in test.TestImprintableItemsOrderdByLevelReverese)
                    {
                        processResult = DeleteTestImprintableItem(imprintableItem);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                //Delete the test issues.
                test.TestIssues = GetTestIssues(new TestIssuesFilter { TestId = test.Id, IssuesLoadingType = TestIssuesLoadingType.NormalIssuesOnly });

                if (test.TestIssues != null)
                {
                    foreach (var issue in test.TestIssues)
                    {
                        processResult = DeleteTestIssue(issue);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                //Delete the Test Main Issue
                test.TestMainIssue = GetTestMainIssue(test);

                if (test.TestMainIssue != null)
                {
                    processResult = DeleteTestIssue(test.TestMainIssue);

                    if (!processResult.IsSucceed) return processResult;
                }

                //Delete the test services.
                test.TestServices = GetTestServices(new TestServicesFilter { TestId = test.Id });

                if (test.TestServices != null)
                {
                    foreach (var service in test.TestServices)
                    {
                        processResult = DeleteTestService(service);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                //Delete the shipping orders.
                test.ShippingOrders = _shippingOrdersManager.GetShippingOrders(new ShippingOrdersFilter() { TestId = test.Id });

                if (test.ShippingOrders != null)
                {
                    foreach (var order in test.ShippingOrders)
                    {
                        processResult = _shippingOrdersManager.Delete(order);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                var testSchedule = new TestSchedule();
                
                if(test.TestSchedule != null)
                {
                    testSchedule = _testSchedulesRepository.LoadTestScheduleById(test.TestSchedule.Id);

                    foreach (var scheduleLine in testSchedule.ScheduleLines)
                    {
                        processResult = _testSchedulesRepository.Delete(scheduleLine);

                        if (!processResult.IsSucceed) return processResult;
                    }

                }

                var spotCheckManager = new SpotCheckManager();

                var relatedSpotCheck = spotCheckManager.GetSpotChecks(new SpotChecksFilter()
                                            {
                                                TestId = test.Id

                                            }).FirstOrDefault();
                if (relatedSpotCheck != null)
                {
                    relatedSpotCheck.TestId = null;
                    //THIS IS A SPECIAL CASE SINCE SETTING THE TESTID TO NULL DOESN'T SET THE IsChanged PROPERTY OF THE
                    //SPOT CHECK AND SO THE SAVE DOESN'T WORK.
                    relatedSpotCheck.ObjectState = DomainEntityState.Modified;

                    processResult =  spotCheckManager.SaveSpotCheck(relatedSpotCheck);
                    if (!processResult.IsSucceed) return processResult;
                }

                //Unlink the related VFS.
                var vfsManager = new VitalForceSheetManager();

                var relatedVfs = vfsManager.GetVFSs(new VFSsFilter
                {
                    TestId = test.Id

                }).FirstOrDefault();

                if (relatedVfs != null)
                {
                    relatedVfs.Test = null;
                    relatedVfs.ObjectState = DomainEntityState.Modified;

                    processResult = vfsManager.SaveVFS(relatedVfs);
                    if (!processResult.IsSucceed) return processResult;
                }

                test.Invoices = GetTestInvoices(test.Id);

                if (test.Invoices != null)
                {
                    foreach (var invoice in test.Invoices)
                    {
                        processResult = _invoicesRepository.Delete(invoice);

                        if (!processResult.IsSucceed) return processResult;
                    }
                }

                if(processResult.IsSucceed)
                {
                    processResult = _testsRepository.Delete(test);
                

                    if (processResult.IsSucceed)
                    {
                        processResult = _testSchedulesRepository.Delete(testSchedule);

                        test.ObjectState = DomainEntityState.Deleted;
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

        #endregion

        #region Test Issues

        /// <summary>
        /// Gets a testIssue.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TestIssue GetTestIssueById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestIssueById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of testIssues.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<TestIssue> GetTestIssues(TestIssuesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestIssues(filter.Name, filter.TestId, filter.ProtocolStepId, filter.ItemId, filter.IssuesLoadingType);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets the main issue for a test
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public TestIssue GetTestMainIssue(Test test)
        {
            Check.Argument.IsNotNull(() => test);

            try
            {
                var testIssueFilter = new TestIssuesFilter(){TestId = test.Id, IssuesLoadingType = TestIssuesLoadingType.MainIssueOnly };
                var results = _testsRepository.LoadTestIssues(testIssueFilter.Name, testIssueFilter.TestId, testIssueFilter.ProtocolStepId, testIssueFilter.ItemId, testIssueFilter.IssuesLoadingType);

                return results == null ? null : results.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the testIssue.
        /// </summary>
        /// <param name="testIssue">The testIssue.</param>
        /// <returns></returns>
        public ProcessResult SaveTestIssue(TestIssue testIssue)
        {
            Check.Argument.IsNotNull(() => testIssue);

            if (testIssue.ObjectState == DomainEntityState.Deleted)
            {
                return DeleteTestIssue(testIssue);
            }

            if (!testIssue.IsChanged) return ProcessResult.Succeed;

            try
            {
                testIssue.SetUserAndDates();

                //Save the test Issue.
                var processResult = _testsRepository.Save(testIssue);

                if (!processResult.IsSucceed)
                    return processResult;

                //Save the test results.
                if (testIssue.TestResults != null)
                {
                    if (testIssue.TestResults.Count == 1)
                        testIssue.TestResults.FirstOrDefault().IsCurrent = true;

                    processResult = SaveTestResults(testIssue.TestResults);

                    if (!processResult.IsSucceed) 
                        return processResult;
                }

                if (processResult.IsSucceed)
                    testIssue.ObjectState = DomainEntityState.Unchanged;

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
        /// Saves the testIssue list.
        /// </summary>
        /// <param name="testIssues">The testIssue list.</param>
        /// <returns></returns>
        public ProcessResult SaveTestIssues(BindingList<TestIssue> testIssues)
        {
            Check.Argument.IsNotNull(() => testIssues);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                for (var i = 0; i < testIssues.Count; i++)
                {
                    var testIssue = testIssues[i];

                    testIssue.SetUserAndDates();

                    if (testIssue.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteTestIssue(testIssue);
                        testIssues.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveTestIssue(testIssue);
                    }
                    

                    if (!processResult.IsSucceed) break;
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
        /// Deletes a testIssue.
        /// </summary>
        /// <param name="testIssue">The testIssue.</param>
        /// <returns></returns>
        public ProcessResult DeleteTestIssue(TestIssue testIssue)
        {
            Check.Argument.IsNotNull(() => testIssue);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                //Delete the test results.
                testIssue.TestResults = GetTestResults(new TestResultsFilter() { TestIssueId = testIssue.Id });

                if (testIssue.TestResults != null)
                {
                    var rootTestResult = testIssue.TestResults.Where(p => p.Parent == null);

                    if (rootTestResult != null)
                    {
                        foreach (var testResult in rootTestResult)
                        {
                            processResult = DeleteTestResult(testResult);

                            if (!processResult.IsSucceed) return processResult;
                        }
                        
                    }
                }

                //Delete the test issue.
                processResult = _testsRepository.Delete(testIssue);

                if (processResult.IsSucceed) { testIssue.ObjectState = DomainEntityState.Deleted; }

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

        #region Test Imprintable Items

        /// <summary>
        /// Gets a test imprintable item.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TestImprintableItem GetTestImprintableItemById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestImprintableItemById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of TestImprintableItems.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<TestImprintableItem> GetTestImprintableItems(TestImprintableItemsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestImprintableItems(filter.TestId,filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the TestImprintableItem.
        /// </summary>
        /// <param name="testImprintableItem">The TestImprintableItem.</param>
        /// <returns></returns>
        public ProcessResult SaveTestImprintableItem(TestImprintableItem testImprintableItem)
        {
            Check.Argument.IsNotNull(() => testImprintableItem);

            if (!testImprintableItem.IsChanged) return ProcessResult.Succeed;

            try
            {
                testImprintableItem.SetUserAndDates();

                //Save the test Issue.
                var processResult = _testsRepository.Save(testImprintableItem);

                if (!processResult.IsSucceed)
                    return processResult;

                if (processResult.IsSucceed)
                    testImprintableItem.ObjectState = DomainEntityState.Unchanged;

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
        /// Saves the TestImprintableItem list.
        /// </summary>
        /// <param name="TestImprintableItems">The TestImprintableItem list.</param>
        /// <returns></returns>
        public ProcessResult SaveTestImprintableItems(BindingList<TestImprintableItem> testImprintableItems)
        {
            Check.Argument.IsNotNull(() => testImprintableItems);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                //Handle normal rows first to make sure deleted rows don't cause a forigen key issue
                foreach (var testImprintableItem in testImprintableItems)
                {
                    testImprintableItem.SetUserAndDates();

                    if (testImprintableItem.ObjectState != DomainEntityState.Deleted)
                    {
                        processResult = SaveTestImprintableItem(testImprintableItem);
                    }

                    if (!processResult.IsSucceed) break;
                }

                for (var i = 0; i < testImprintableItems.Count; i++)
                {
                    var testImprintableItem = testImprintableItems[i];

                    testImprintableItem.SetUserAndDates();

                    if (testImprintableItem.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteTestImprintableItem(testImprintableItem);
                        testImprintableItems.RemoveAt(i);
                        i--;
                    }
                   
                    if (!processResult.IsSucceed) break;
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
        /// Deletes a TestImprintableItem.
        /// </summary>
        /// <param name="TestImprintableItem">The TestImprintableItem.</param>
        /// <returns></returns>
        public ProcessResult DeleteTestImprintableItem(TestImprintableItem testImprintableItem)
        {
            Check.Argument.IsNotNull(() => testImprintableItem);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                if (testImprintableItem.Id != 0)
                {
                    processResult = _testsRepository.Delete(testImprintableItem);
                }
                
                if (processResult.IsSucceed) { testImprintableItem.ObjectState = DomainEntityState.Deleted; }

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

        #region Test Services

        /// <summary>
        /// Gets a testService.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TestService GetTestServiceById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestServiceById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of testServices.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<TestService> GetTestServices(TestServicesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestServices(filter.Name,filter.Name,filter.TypeLookupId,filter.TestId, filter.ServiceId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the testService.
        /// </summary>
        /// <param name="testService">The testService.</param>
        /// <returns></returns>
        public ProcessResult SaveTestService(TestService testService)
        {
            Check.Argument.IsNotNull(() => testService);

            if (!testService.IsChanged) return ProcessResult.Succeed;

            try
            {
                testService.SetUserAndDates();

                //Save the test Service.
                var processResult = _testsRepository.Save(testService);

                if (!processResult.IsSucceed)
                    return processResult;

                testService.ObjectState = DomainEntityState.Unchanged;

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
        /// Saves the testService list.
        /// </summary>
        /// <param name="testServices">The testService list.</param>
        /// <returns></returns>
        public ProcessResult SaveTestServices(BindingList<TestService> testServices)
        {
            Check.Argument.IsNotNull(() => testServices);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                for (var i = 0; i < testServices.Count; i++)
                {
                    var testService = testServices[i];

                    testService.SetUserAndDates();

                    if (testService.ObjectState == DomainEntityState.Deleted)
                    {
                        processResult = DeleteTestService(testService);
                        testServices.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        processResult = SaveTestService(testService);
                    }


                    if (!processResult.IsSucceed) break;
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
        /// Deletes a testService.
        /// </summary>
        /// <param name="testService">The testService.</param>
        /// <returns></returns>
        public ProcessResult DeleteTestService(TestService testService)
        {
            Check.Argument.IsNotNull(() => testService);

            try
            {
                var processResult = new ProcessResult() { IsSucceed = true };

                //Delete the test Service.
                processResult = _testsRepository.Delete(testService);

                if (processResult.IsSucceed) { testService.ObjectState = DomainEntityState.Deleted; }

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

        #region TestResults

        /// <summary>
        /// Gets a test result.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TestResult GetTestResultById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _testsRepository.LoadTestResultById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of test results.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<TestResult> GetTestResults(TestResultsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestResults(filter.TestIssueId, filter.ItemId, filter.ParentId,
                                                        filter.VitalForceId , filter.IsSelected);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the test result.
        /// </summary>
        /// <param name="testResult">The test result.</param>
        /// <returns></returns>
        public ProcessResult SaveTestResult(TestResult testResult)
        {
            Check.Argument.IsNotNull(() => testResult);

            if (!testResult.IsChanged || testResult.ObjectState == DomainEntityState.Temp) return ProcessResult.Succeed;

            try
            {
                var processResult = new ProcessResult() {IsSucceed = true};

                if (testResult.ObjectState == DomainEntityState.New)
                {
                    testResult.Id = 0;
                }

                testResult.SetUserAndDates();

                processResult = _testsRepository.Save(testResult);

                if (processResult.IsSucceed) { testResult.ObjectState = DomainEntityState.Unchanged; }

                //Save the test result parent in case its not deleted or not changed.
                if (testResult.Parent != null && testResult.Parent.ObjectState != DomainEntityState.Unchanged && testResult.Parent.ObjectState != DomainEntityState.Deleted)
                {
                    processResult = SaveTestResult(testResult.Parent);

                    if (!processResult.IsSucceed) return processResult;
                }

                //Save the test result factors.
                if (testResult.TestResultFactors != null)
                {
                    processResult = SaveTestResultFactors(testResult.TestResultFactors);

                    if (!processResult.IsSucceed) return processResult;
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
        /// Saves the test result list.
        /// </summary>
        /// <param name="testResults">The test result list.</param>
        /// <returns></returns>
        public ProcessResult SaveTestResults(BindingList<TestResult> testResults)
        {
            Check.Argument.IsNotNull(() => testResults);

            var testResultList = testResults.ToList();

            try
            {
                var processResult = new ProcessResult(){IsSucceed = true};

                foreach (var testResult in testResultList)
                {
                    testResult.SetUserAndDates();

                    processResult = testResult.ObjectState == DomainEntityState.Deleted ? DeleteTestResult(testResult) : SaveTestResult(testResult);

                    if (!processResult.IsSucceed) break;
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
        /// Deletes a test result.
        /// </summary>
        /// <param name="testResult">The test result.</param>
        /// <returns></returns>
        public ProcessResult DeleteTestResult(TestResult testResult)
        {
            Check.Argument.IsNotNull(() => testResult);

            try
            {
                var childrenTestResults = GetTestResults(new TestResultsFilter() {ParentId = testResult.Id});

                var processResult = new ProcessResult() { IsSucceed = true };

                if (childrenTestResults != null)
                {

                    foreach (var childTestResult in childrenTestResults)
                    {
                        if (childTestResult.ObjectState == DomainEntityState.Deleted) continue;

                        processResult = DeleteTestResult(childTestResult);

                        if (!processResult.IsSucceed) break;
                    }

                    if (!processResult.IsSucceed) return processResult;
                }

                var resultFactors = GetTestResultFactors(new TestResultFactorsFilter() {TestResultId = testResult.Id});

                if (resultFactors != null)
                {

                    foreach (var resultFactor in resultFactors)
                    {
                        processResult = DeleteTestResultFactor(resultFactor);

                        if (!processResult.IsSucceed) break;
                    }

                    if (!processResult.IsSucceed) return processResult;
                }

                if (testResult.ObjectState == DomainEntityState.Deleted) return processResult;

                processResult = _testsRepository.Delete(testResult);

                if (processResult.IsSucceed) { testResult.ObjectState = DomainEntityState.Deleted; }

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
        /// Gets duplicated test results in the test.
        /// </summary>
        private static  IEnumerable<TestResult> GetDuplicatedTestResults(Test test, TestResult testResult)
        {
            if (test == null || testResult == null || testResult.Item == null)
                return new List<TestResult>();

            return test.TestIssues.SelectMany(r => r.TestResults).Where(tr => tr.Item != null && tr.Item.Id == testResult.Item.Id);
        }

        /// <summary>
        /// Sets vital force for duplicated test results in the test.
        /// </summary>
        public static void SetVitalForceForDuplicatedTestResults(Test test, TestResult testResult, Item vitalForce)
        {
            if (test == null || testResult == null || testResult.Item == null || vitalForce == null)
                return;

            var duplicatedTestResults = GetDuplicatedTestResults(test, testResult);

            foreach (var duplicatedTestResult in duplicatedTestResults)
            {
                duplicatedTestResult.VitalForce = vitalForce;
            }
        }

        /// <summary>
        /// Gets vital force for duplicated test result in the test.
        /// </summary>
        public static Item GetVitalForceForDuplicatedTestResult(Test test, TestResult testResult)
        {
            if (test == null || testResult == null || testResult.Item == null)
                return null;

            return GetDuplicatedTestResults(test, testResult).Where(tr => tr.VitalForce != null).Select(tr => tr.VitalForce).FirstOrDefault();
        }

        #endregion

        #region TestResultFactor

        /// <summary>
        /// Loads TestResultFactor by id.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        public TestResultFactor GetTestResultFactorById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestResultFactorById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Loads a list of TestResultFactors.
        /// </summary>
        /// <param name="filter">The Filter.</param>
        /// <returns>List of TestResultFactor.</returns>
        public BindingList<TestResultFactor> GetTestResultFactors(TestResultFactorsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadTestResultFactors(filter.FactorId, filter.PotencyId, filter.TestResultId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves a TestResultFactor.
        /// </summary>
        /// <param name="testResultFactor">The TestResultFactor.</param>
        /// <returns>The result.</returns>
        public ProcessResult SaveTestResultFactor(TestResultFactor testResultFactor)
        {
            Check.Argument.IsNotNull(() => testResultFactor);

            if (!testResultFactor.IsChanged) return ProcessResult.Succeed;

            try
            {
                testResultFactor.SetUserAndDates();


                var processResult = _testsRepository.Save(testResultFactor);

                if (processResult.IsSucceed) testResultFactor.ObjectState = DomainEntityState.Unchanged;

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
        /// Saves a TestResultFactor list.
        /// </summary>
        /// <param name="testResultFactors">The TestResultFactor List.</param>
        /// <returns>The result.</returns>
        public ProcessResult SaveTestResultFactors(BindingList<TestResultFactor> testResultFactors)
        {
            Check.Argument.IsNotNull(() => testResultFactors);

            var testResultFactorsList = testResultFactors.ToList();

            try
            {
                var processResult = new ProcessResult() { IsSucceed = testResultFactors.Count == 0 };

                foreach (var testResultFactor in testResultFactorsList)
                {
                    processResult = testResultFactor.ObjectState == DomainEntityState.Deleted? DeleteTestResultFactor(testResultFactor) : SaveTestResultFactor(testResultFactor);

                    if (!processResult.IsSucceed) break;
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
        /// Deletes a TestResultFactor.
        /// </summary>
        /// <param name="testResultFactor">The TestResultFactor.</param>
        /// <returns>The result.</returns>
        public ProcessResult DeleteTestResultFactor(TestResultFactor testResultFactor)
        {
            Check.Argument.IsNotNull(() => testResultFactor);

            try
            {
                var processResult = _testsRepository.Delete(testResultFactor);

                if (processResult.IsSucceed) testResultFactor.ObjectState = DomainEntityState.Deleted;

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

        #region Navigation Steps

        /// <summary>
        /// Gets a issueNavigationStep.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IssueNavigationStep GetIssueNavigationStepById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadIssueNavigationStepById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of issueNavigationSteps.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<IssueNavigationStep> GetIssueNavigationSteps(IssueNavigationStepsFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _testsRepository.LoadIssueNavigationSteps(filter.Order, filter.IssueId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationStep">The issueNavigationStep.</param>
        /// <returns></returns>
        public ProcessResult SaveIssueNavigationStep(IssueNavigationStep issueNavigationStep)
        {
            Check.Argument.IsNotNull(() => issueNavigationStep);

            if (!issueNavigationStep.IsChanged) return ProcessResult.Succeed;

            try
            {
                ProcessResult processResult;

                if (issueNavigationStep.ParentStep != null && issueNavigationStep.ParentStep.ObjectState != DomainEntityState.Deleted && issueNavigationStep.ParentStep.ObjectState != DomainEntityState.Unchanged)
                {
                    processResult = SaveIssueNavigationStep(issueNavigationStep.ParentStep);

                    if (!processResult.IsSucceed) return processResult;
                }

                issueNavigationStep.SetUserAndDates();

                processResult = _testsRepository.Save(issueNavigationStep);

                if (processResult.IsSucceed) issueNavigationStep.ObjectState = DomainEntityState.Unchanged;

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
        /// Saves the navigation steps.
        /// </summary>
        /// <param name="issueNavigationSteps">The issue navigation steps.</param>
        /// <returns></returns>
        public ProcessResult SaveIssueNavigationSteps(BindingList<IssueNavigationStep> issueNavigationSteps)
        {
            Check.Argument.IsNotNull(() => issueNavigationSteps);

            var stepsList = issueNavigationSteps.ToList();

            try
            {
                var processResult = new ProcessResult { IsSucceed = true };

                foreach (var step in stepsList)
                {
                    processResult = step.ObjectState == DomainEntityState.Deleted
                                        ? DeleteIssueNavigationStep(step)
                                        : SaveIssueNavigationStep(step);

                    if(!processResult.IsSucceed) break;
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
        /// Deletes a issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationStep">The issueNavigationStep.</param>
        /// <returns></returns>
        public ProcessResult DeleteIssueNavigationStep(IssueNavigationStep issueNavigationStep)
        {
            Check.Argument.IsNotNull(() => issueNavigationStep);

            try
            {
                var processResult = _testsRepository.Delete(issueNavigationStep);

                if (processResult.IsSucceed) { issueNavigationStep.ObjectState = DomainEntityState.Deleted; }

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
        /// Deletes a issueNavigationStep.
        /// </summary>
        /// <param name="issueNavigationSteps">The issueNavigationStep.</param>
        /// <returns></returns>
        public ProcessResult DeleteIssueNavigationSteps(BindingList<IssueNavigationStep> issueNavigationSteps)
        {
            Check.Argument.IsNotNull(() => issueNavigationSteps);

            var stepsList = issueNavigationSteps.ToList();

            try
            {
                var processResult = new ProcessResult() {IsSucceed = true};

                foreach (var issueNavigationStep in stepsList)
                {
                    processResult = DeleteIssueNavigationStep(issueNavigationStep);
                    if(!processResult.IsSucceed) break;
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

        #region Test Schedule

        /// <summary>
        /// Deletes a schedule line.
        /// </summary>
        /// <param name="scheduleLine">The schedule line.</param>
        /// <returns></returns>
        public ProcessResult DeleteScheduleLine(ScheduleLine scheduleLine)
        {
            Check.Argument.IsNotNull(() => scheduleLine);

            try
            {
                var processResult = _testSchedulesRepository.Delete(scheduleLine);

                if (processResult.IsSucceed) { scheduleLine.ObjectState = DomainEntityState.Deleted; }

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
        /// Saves the schedule line.
        /// </summary>
        /// <param name="scheduleLine">The scheduleLine.</param>
        /// <returns></returns>
        public ProcessResult SaveScheduleLine(ScheduleLine scheduleLine)
        {
            Check.Argument.IsNotNull(() => scheduleLine);

            if (!scheduleLine.IsChanged) return ProcessResult.Succeed;

            try
            {
                scheduleLine.SetUserAndDates();

                ProcessResult processResult = _testSchedulesRepository.Save(scheduleLine);

                if (processResult.IsSucceed) scheduleLine.ObjectState = DomainEntityState.Unchanged;

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
        /// Set the default values for the passed scheduleLine.
        /// </summary>
        public static ProcessResult SetScheduleLineDefaultValues(ScheduleLine scheduleLine)
        {
            if (scheduleLine.Item == null)
                return ProcessResult.Failed;

            scheduleLine.Duration = scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultDuration);

            scheduleLine.WhenArising =
                scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultWhenArising);

            scheduleLine.Breakfast =
                scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultBreakfast);

            scheduleLine.BetweenMealsEarly =
                scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultBetweenMealsEarly);

            scheduleLine.Lunch = scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultLunch);

            scheduleLine.BetweenMealsLate =
                scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultBetweenMealsLate);

            scheduleLine.Dinner = scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultDinner);

            scheduleLine.BeforeSleep =
                scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultBeforeSleep);

            scheduleLine.NoPerBottle =
                scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultNoPerBottle);

            scheduleLine.NoOfBottle = "1";

            scheduleLine.Notes = scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultNotes);

            var defaultPrice = scheduleLine.Item.Properties.GetPropertyValueAsString(PropertiesEnum.DefaultPrice);
            decimal defaultPriceDecimal;
            if (decimal.TryParse(defaultPrice, out defaultPriceDecimal))
                scheduleLine.Price = defaultPrice != null ? defaultPriceDecimal : new decimal?();
            else
                scheduleLine.Price = 0;


            return ProcessResult.Succeed;
        }

        #endregion

        #region Test Invoices

        /// <summary>
        /// Gets the test invoices.
        /// </summary>
        /// <param name="testId">The test id.</param>
        /// <returns></returns>
        private BindingList<Invoice> GetTestInvoices(int testId)
        {
            return _invoicesRepository.LoadInvoices(testId, string.Empty, string.Empty, 0);
        }

        #endregion

        #endregion
    }
}
