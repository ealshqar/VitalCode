using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.DomainObjects.Items;
using Vital.Business.Shared.DomainObjects.TestProtocols;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using System.ComponentModel;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TestProtocolsManagerTest and is intended
    ///to contain all TestProtocolsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestProtocolsManagerFixture
    {
        #region Private Variables

        private TestContext testContextInstance;

        #endregion

        #region Test Context

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

        #endregion

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
        ///A test for DeleteProtocolItem
        ///</summary>
        [TestMethod()]
        public void DeleteProtocolItemTest()
        {
            var target = new TestProtocolsManager();
            var protocolItem = new ProtocolItem(){ Id = 1 };
          
            var actual = target.DeleteProtocolItem(protocolItem);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for DeleteProtocolStep
        ///</summary>
        [TestMethod()]
        public void DeleteProtocolStepTest()
        {
            var target = new TestProtocolsManager();
            var protocolStep = new ProtocolStep() { Id = 1 };
            
            var actual = target.DeleteProtocolStep(protocolStep);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for DeleteTestProtocol
        ///</summary>
        [TestMethod()]
        public void DeleteTestProtocolTest()
        {
            var target = new TestProtocolsManager();

            var testProtocol = target.GetTestProtocolById(new SingleItemFilter() {ItemId = 16});

            var actual = target.DeleteTestProtocol(testProtocol);
            Assert.IsTrue(actual.IsSucceed);
        }

        #endregion

        #region Get Methods Tests

        /// <summary>
        ///A test for GetProtocolItemById
        ///</summary>
        [TestMethod()]
        public void GetProtocolItemByIdTest()
        {
            var target = new TestProtocolsManager(); 
            var filter = new SingleItemFilter() { ItemId = 1};
            
            var actual = target.GetProtocolItemById(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetProtocolItems
        ///</summary>
        [TestMethod()]
        public void GetProtocolItemsTest()
        {
            var target = new TestProtocolsManager();
            var filter = new ProtocolItemsFilter() { TestProtocolId = 1 , ItemId = 2};
            
            var actual = target.GetProtocolItems(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetProtocolStepById
        ///</summary>
        [TestMethod()]
        public void GetProtocolStepByIdTest()
        {
            var target = new TestProtocolsManager(); 
            var filter = new SingleItemFilter() { ItemId = 2 }; 
            
            var actual = target.GetProtocolStepById(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetProtocolSteps
        ///</summary>
        [TestMethod()]
        public void GetProtocolStepsTest()
        {
            var target = new TestProtocolsManager(); 
            var filter = new ProtocolStepsFilter() { TestProtocolId = 1 };
            
            var actual = target.GetProtocolSteps(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetTestProtocolById
        ///</summary>
        [TestMethod()]
        public void GetTestProtocolByIdTest()
        {
            var target = new TestProtocolsManager(); 
            var filter = new SingleItemFilter() { ItemId = 1 };
            
            var actual = target.GetTestProtocolById(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetTestProtocols
        ///</summary>
        [TestMethod()]
        public void GetTestProtocolsTest()
        {
            var target = new TestProtocolsManager();
            var filter = new TestProtocolsFilter();

            var actual = target.GetTestProtocols(filter);
            Assert.IsNotNull(actual);
        }

        #endregion

        #region Save Methods Tests

        /// <summary>
        ///A test for SaveProtocolItem
        ///</summary>
        [TestMethod()]
        public void SaveProtocolItemTest()
        {
            var target = new TestProtocolsManager(); 
            var protocolItem = new ProtocolItem(); 

            var actual = target.SaveProtocolItem(protocolItem);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for SaveProtocolStep
        ///</summary>
        [TestMethod()]
        public void SaveProtocolStepTest()
        {
            var target = new TestProtocolsManager(); 
            var protocolStep = new ProtocolStep();

            var actual = target.SaveProtocolStep(protocolStep);
            Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for SaveTestProtocol
        ///</summary>
        [TestMethod()]
        public void SaveTestProtocolTest()
        {
            var target = new TestProtocolsManager();
            var testProtocol = new TestProtocol() { 
                                                    ProtocolItems = new BindingList<ProtocolItem>() { new ProtocolItem() { Item = new Item() { Id = 3} }} ,
                                                    ProtocolSteps = new BindingList<ProtocolStep>(),
                                                    Name = "Protocol 1"
                                                   } ; 
            
            var actual = target.SaveTestProtocol(testProtocol);
            Assert.IsTrue(actual.IsSucceed);
        }

        #endregion
    }
}
