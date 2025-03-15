using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.DomainObjects.Readings;
using System.Collections.Generic;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for ReadingsManagerTest and is intended
    ///to contain all ReadingsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReadingsManagerFixture
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


        /// <summary>
        ///A test for GetReadings
        ///</summary>
        [TestMethod()]
        public void GetReadingsTest()
        {
            var target = new ReadingsManager(); 
            var actual = target.GetReadings(new ReadingsFilter());
           Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetReadingById
        ///</summary>
        [TestMethod()]
        public void GetReadingByIdTest()
        {
            ReadingsManager target = new ReadingsManager(); // TODO: Initialize to an appropriate value
            SingleItemFilter filter = null; // TODO: Initialize to an appropriate value
            Reading expected = null; // TODO: Initialize to an appropriate value
            Reading actual;
            actual = target.GetReadingById(filter);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteReading
        ///</summary>
        [TestMethod()]
        public void DeleteReadingTest()
        {
            ReadingsManager target = new ReadingsManager(); // TODO: Initialize to an appropriate value
            Reading reading = null; // TODO: Initialize to an appropriate value
            ProcessResult expected = null; // TODO: Initialize to an appropriate value
            ProcessResult actual;
            actual = target.DeleteReading(reading);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveReading
        ///</summary>
        [TestMethod()]
        public void SaveReadingTest()
        {
            var target = new ReadingsManager();
            var itemManager = new ItemsManager();
            var testmanager = new TestsManager();
            var reading = new Reading()
                              {
                                  Item = itemManager.GetItemById(new SingleItemFilter() {ItemId = 1}),
                                  Fall = 5354,
                                  Max = 40,
                                  Min = 30,
                                  Rise = 50,
                                  Value = 50,
                                  Test = testmanager.GetTestById(new SingleItemFilter() {ItemId = 79}),
                                  DateTime = DateTime.Now
                              };

            
            var actual = target.SaveReading(reading);
            Assert.IsTrue(actual.IsSucceed);
        }
    }
}
