using System.Linq;
using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using Vital.UI.Logic_Classes;

namespace Vital.UnitTests
{
    /// <summary>
    ///This is a test class for ServicesManagerTest and is intended
    ///to contain all ServicesManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServicesManagerFixture
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
        ///A test for SaveService
        ///</summary>
        [TestMethod()]
        public void TestDongle()
        {
            UiHelperClass.ReadDongleInfo();
            //Assert.IsTrue(actual.IsSucceed);
        }

        /// <summary>
        ///A test for SaveService
        ///</summary>
        [TestMethod()]
        public void SaveServiceTest()
        {
            var target = new ServicesManager();
            var Service = new Service()
            {
                Key = "TestServiceKey",
                Name = "Test Service",
                Description = "Test Service Description",
                Comments = "Test Service Comments",
                Price = 120,
                IsDefault = true,
                TypeLookup = new LookupsManager().GetLookups(LookupsFilter.As(LookupTypes.ServiceType, ServiceType.UserService,false,true)).FirstOrDefault()
            };

            var actual = target.SaveService(Service);
            Assert.IsTrue(actual.IsSucceed);
        }

        [TestMethod()]
        public void SaveEditedServiceTest()
        {
            var target = new ServicesManager();
            var Service = target.GetServiceById(new SingleItemFilter { ItemId = 3 });
            Service.Name = "Edited Name";
            var actual = target.SaveService(Service);
            Assert.IsTrue(actual.IsSucceed);
        }
        /// <summary>
        ///A test for GetServices
        ///</summary>
        [TestMethod()]
        public void GetServicesTest()
        {
            var target = new ServicesManager();
            var filter = new ServicesFilter() { Key = "TestServiceKey" };
            var actual = target.GetServices(filter);
            Assert.IsTrue(actual != null);
        }

        /// <summary>
        ///A test for GetServiceById
        ///</summary>
        [TestMethod()]
        public void GetServiceByIdTest()
        {
            var target = new ServicesManager();
            var filter = new SingleItemFilter() { ItemId = 2 };

            var actual = target.GetServiceById(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for DeleteService
        ///</summary>
        [TestMethod()]
        public void DeleteServiceTest()
        {
            var target = new ServicesManager();
            var Service = target.GetServiceById(new SingleItemFilter() { ItemId = 3 });
            var actual = target.DeleteService(Service);
            Assert.IsTrue(actual.IsSucceed);
        }
    }
}
