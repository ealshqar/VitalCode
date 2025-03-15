using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.DomainObjects.Lookups;
using Vital.Business.Shared.DomainObjects.Patients;
using Vital.Business.Shared.DomainObjects.Users;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;
using System.Collections.Generic;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for PatientsManagerTest and is intended
    ///to contain all PatientsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PatientsManagerFixture
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
        ///A test for SavePatient
        ///</summary>
        [TestMethod()]
        public void SavePatientTest()
        {
            var target = new PatientsManager(); 
            var patient = new Patient()
                                  {
                                      Address1 = "USA",
                                      Address2 = "Canada",
                                      CellPhone = "00568745682",
                                      City = "Washington",
                                      DateOfBirth = new DateTime(1989, 10, 27),
                                      Email = "Mohammed.Zurba@iconnecths.com",
                                      Fax = string.Empty,
                                      FirstName = "Mohammed",
                                      HomePhone = "092354786",
                                      Tests = null,
                                      State = "aa",
                                  };

            var actual = target.SavePatient(patient);
            Assert.IsTrue(actual.IsSucceed);
        }

        [TestMethod()]
        public void SaveEditedPatientTest()
        {
            var target = new PatientsManager();
            var patient = target.GetPatientById(new SingleItemFilter { ItemId = 8 });
            patient.FirstName = "Edited Name";
            var actual = target.SavePatient(patient);
            Assert.IsTrue(actual.IsSucceed);
        }
        /// <summary>
        ///A test for GetPatients
        ///</summary>
        [TestMethod()]
        public void GetPatientsTest()
        {
            var target = new PatientsManager();
            var filter = new PatientsFilter() {Name = "Mohammed"};
            var actual = target.GetPatients(filter);
            Assert.IsTrue(actual != null);
        }

        /// <summary>
        ///A test for GetPatientById
        ///</summary>
        [TestMethod()]
        public void GetPatientByIdTest()
        {
            var target = new PatientsManager(); 
            var filter = new SingleItemFilter(){ItemId = 2};

            var actual = target.GetPatientById(filter);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for DeletePatient
        ///</summary>
        [TestMethod()]
        public void DeletePatientTest()
        {
            var target = new PatientsManager();
            var patient = target.GetPatientById(new SingleItemFilter(){ItemId = 2});
            var actual = target.DeletePatient(patient);
            Assert.IsTrue(actual.IsSucceed);
        }
    }
}
