using System.Linq;
using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.Readings;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using System.ComponentModel;
using Vital.UI.Logic_Classes;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TestsManagerTest and is intended
    ///to contain all TestsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestsManagerFixture
    {        
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion               
        
        #region Delete Methods Tests

        /// <summary>
        ///A test for DeleteTestIssue
        ///</summary>
        [TestMethod()]
        public void DeleteTestIssueTest()
        {
            var target = new TestsManager();
            var testIssue = new TestIssue() { Id = 2 };

            var actual = target.DeleteTestIssue(testIssue);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for DeleteIssueNavigationStep
        ///</summary>
        [TestMethod()]
        public void DeleteNavigationStepTest()
        {
            var target = new TestsManager();
            var navStep = new IssueNavigationStep() { Id = 1 };

            var actual = target.DeleteIssueNavigationStep(navStep);
            Assert.IsTrue(actual.IsSucceed);
        }
      
        #endregion

        #region Get Methods Tests

        /// <summary>
        ///A test for GetTestById
        ///</summary>
        [TestMethod()]
        public void GetTestByIdTest()
        {
            TestsManager target = new TestsManager(); // TODO: Initialize to an appropriate value
            SingleItemFilter filter = new SingleItemFilter() { ItemId = 111 };
            Test actual = target.GetTestById(filter);
            Assert.IsTrue(actual.Id > 0);
        }

        /// <summary>
        ///A test for GetTestIssueById
        ///</summary>
        [TestMethod()]
        public void GetTestIssueByIdTest()
        {
            var target = new TestsManager();
            var filter = new SingleItemFilter() { ItemId = 7 };

            var actual = target.GetTestIssueById(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetProtocolItems
        ///</summary>
        [TestMethod()]
        public void GetTestIssuesTest()
        {
            var target = new TestsManager();
            var filter = new TestIssuesFilter();

            var actual = target.GetTestIssues(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetProtocolItems
        ///</summary>
        [TestMethod()]
        public void GetTestResult()
        {
            var target = new TestsManager();
            var filter = new TestResultsFilter();
            filter.TestIssueId = 52;
            var actual = target.GetTestResults(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetIssueNavigationSteps
        ///</summary>
        [TestMethod()]
        public void GetIssueNavigationStepsTest()
        {
            var target = new TestsManager();
            var filter = new IssueNavigationStepsFilter() {IssueId = 2};

            var actual = target.GetIssueNavigationSteps(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetIssueNavigationStepById
        ///</summary>
        [TestMethod()]
        public void GetIssueNavigationStepByIdTest()
        {
            var target = new TestsManager();
            var filter = new SingleItemFilter() { ItemId = 1 };

            var actual = target.GetIssueNavigationStepById(filter);
            Assert.IsNotNull(actual);
        }

        #endregion

        #region Save Methods Tests

        /// <summary>
        ///A test for Save test
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            var target = new TestsManager(); // TODO: Initialize to an appropriate value
            var targetReadomgs = new ReadingsManager(); // TODO: Initialize to an appropriate value

            var filter = new SingleItemFilter() { ItemId = 111 };

            //Test actual = target.GetTestById(filter);

            //var readings = actual.Readings;

            //var firstItem = readings.FirstOrDefault();

            //firstItem.Fall = 300;

            var test = new Test();

            test.DateTime = DateTime.Now;
            test.Description = "Desc";
            test.Item = new ItemsManager().GetItemById(new SingleItemFilter() {ItemId = 1});
            test.ListPointLookup = new LookupsManager().GetLookups(new LookupsFilter()).FirstOrDefault();
            test.Name = "Final Test ...";
            test.Patient = new PatientsManager().GetPatients(new PatientsFilter()).FirstOrDefault();
            test.Readings = new BindingList<Reading>();
            test.StateLookup = new LookupsManager().GetLookups(new LookupsFilter()).FirstOrDefault();
            test.TestProtocol = new TestProtocolsManager().GetTestProtocols(new TestProtocolsFilter()).FirstOrDefault();
            test.TypeLookup = new LookupsManager().GetLookups(new LookupsFilter()).FirstOrDefault();
            
            test.SetUserAndDates();

            var issues = new BindingList<TestIssue>();

            var issue = new TestIssue() {};

            issue.Test = test;

            issue.SetUserAndDates();

            issue.TestResults = new BindingList<TestResult>();

            var testResult = new TestResult()
                                 {
                                     Item = new ItemsManager().GetItemById(new SingleItemFilter()
                                                                               {
                                                                                   ItemId = 1
                                                                               }),

                                     TestIssue = issue,
                                     VitalForce = new ItemsManager().GetItemById(new SingleItemFilter()
                                                                                     {
                                                                                         ItemId = 1
                                                                                     }),
                                     TestResultFactors = new BindingList<TestResultFactor>(),
                                     DateTime = DateTime.Now

                                 };

            var testFactorResult = new TestResultFactor()
                                       {
                                           Potency = new ItemsManager().GetItemById(new SingleItemFilter()
                                                                                            {
                                                                                                ItemId = 1
                                                                                            }),
                                           Factor = new ItemsManager().GetItemById(new SingleItemFilter()
                                                                                           {
                                                                                               ItemId = 1
                                                                                           }),
                                           Reading = 15,
                                           TestResult = testResult
                                       };

            testFactorResult.SetUserAndDates();

            testResult.TestResultFactors.Add(testFactorResult);
            testResult.SetUserAndDates();
                                    
            issue.TestResults.Add(testResult);

            var navstep =
                new IssueNavigationStep()
                {
                    TestIssue = issue,
                    Item = new ItemsManager().GetItemById(new SingleItemFilter()
                    {
                        ItemId = 1
                    })
                    
                };

            navstep.SetUserAndDates();

            //issue.IssueNavigationSteps = new BindingList<IssueNavigationStep>()
            //                                 {
            //                                     navstep
            //                                 };

            issues.Add(issue);

            test.TestIssues = issues;
            issue.Name = "Final Test Issue.1";
            issue.Item = new ItemsManager().GetItemById(new SingleItemFilter()
                                                            {
                                                                ItemId = 1
                                                            });
            var s = target.SaveTest(test);
            
            Assert.IsTrue(s.IsSucceed);

        }

        /// <summary>
        ///A test for SaveTestIssue
        ///</summary>
        [TestMethod()]
        public void SaveTestIssueTest()
        {
            var target = new TestsManager();
            var itemManager = new ItemsManager();

            var filter = new SingleItemFilter() { ItemId = 1 };

            Item item = itemManager.GetItemById(filter);
            Test test = target.GetTestById(filter);
            
            var testIssue = new TestIssue();
            testIssue.Item = item;
            testIssue.Name = "Issue2";
            testIssue.Test = test;

            var actual = target.SaveTestIssue(testIssue);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for SaveIssueNavigationStep
        ///</summary>
        [TestMethod()]
        public void SaveIssueNavigationStepTest()
        {
            var target = new TestsManager();
            var filter = new SingleItemFilter() { ItemId = 2 };
            TestIssue testIssue = target.GetTestIssueById(filter);

            var itemManager = new ItemsManager();
            
            Item item = itemManager.GetItemById(filter);
            
            var navStep = new IssueNavigationStep();
            navStep.TestIssue = testIssue;
            navStep.Item = item;

            var actual = target.SaveIssueNavigationStep(navStep);
            Assert.IsTrue(actual.IsSucceed);
        }

        #endregion



        /// <summary>
        ///A test for DeleteTest
        ///</summary>
        [TestMethod()]
        public void DeleteTestTest()
        {
            var target = new TestsManager();
            var test = target.GetTestById(new SingleItemFilter() { ItemId = 6209 });

            if (test == null) return;

            var actual = target.DeleteTest(test);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for product info download
        ///</summary>
        [TestMethod()]
        public void TestProductInfoDownload()
        {
            var productsOnBackOrder = UiHelperClass.ProductsOnBackorderDownload.ReadDownloadFile();

            Assert.IsTrue(productsOnBackOrder.Contents != string.Empty);
        }
    }
}
