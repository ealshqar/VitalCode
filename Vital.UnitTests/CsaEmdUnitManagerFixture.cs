using System.Diagnostics;
using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CsaEmdUnitManagerTest and is intended
    ///to contain all CsaEmdUnitManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CsaEmdUnitManagerFixture
    {

        CsaEmdUnitManager target = new CsaEmdUnitManager();
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
        ///A test for CloseConnection
        ///</summary>
        [TestMethod()]
        public void CloseConnectionTest()
        {
             
            var actual = target.CloseConnection();
            //Assert.IsTrue(actual.IsSucceed);
            if(actual.IsSucceed)
            Trace.WriteLine("connection has been closed\n");
            else
            {
                Trace.WriteLine("Closing :"+actual.IsSucceed+"\n");
            }
        }

        /// <summary>
        ///A test for OpenConnection
        ///</summary>
        [TestMethod()]
        public void OpenConnectionTest()
        {
            var actual = target.OpenConnection(new SerialPortConnectionFilter(HardwareType.CSA) { ComPortNumber = 3 });
            //Assert.IsTrue(actual.IsSucceed);
            if (actual.IsSucceed)
                Trace.WriteLine("connection has been opened\n");
            else
            {
                Trace.WriteLine("opening :" + actual.IsSucceed + "\n");
            }
        }

        /// <summary>
        ///A test for StartReading
        ///</summary>
        [TestMethod()]
        public void StartReadingTest()
        {
            //var actual = target.StartReading();
            //target.MeterValueChanged += new CsaEmdUnitManager.MeterValueChangedHandle(target_MeterValueChanged);
            ////Assert.IsTrue(actual.IsSucceed);

            //if (actual.IsSucceed)
            //    Trace.WriteLine("reading has been started\n");
            //else
            //{
            //    Trace.WriteLine("start Reading :" + actual.IsSucceed + "\n");
            //}
        }

        void target_MeterValueChanged(object sender, int reading, int min, int max)
        {
            Trace.WriteLine("Reading : " + reading + "\n");
        }

        /// <summary>
        ///A test for StartResetting
        ///</summary>
        [TestMethod()]
        public void StartResettingTest()
        {
            var actual = target.StartResetting();
            target.ResettingFinished += new CsaEmdUnitManager.OnResettingFinishedHandle(target_ResettingFinished);
            //Assert.IsTrue(actual.IsSucceed);

            if (actual.IsSucceed)
                Trace.WriteLine("Resetting has been started\n");
            else
            {
                Trace.WriteLine("start Resetting :" + actual.IsSucceed + "\n");
            }
        }

        void target_ResettingFinished(object sender)
        {
            Trace.WriteLine("Resetting has been stopped\n");
        }

        /// <summary>
        ///A test for StopReading
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Vital.Business.dll")]
        public void StopReadingTest()
        {
            //var actual = target.StopReading();
            ////Assert.IsTrue(actual.IsSucceed);
            //if (actual.IsSucceed)
            //    Trace.WriteLine("Reading has been stopped\n");
            //else
            //{
            //    Trace.WriteLine("Reading Resetting :" + actual.IsSucceed + "\n");
            //}
            
        }

        /// <summary>
        ///A test for StopResetting
        ///</summary>
        [TestMethod()]
        public void StopResettingTest()
        {
            var actual = target.StopResetting();
            //Assert.IsTrue(actual.IsSucceed);
            if (actual.IsSucceed)
                Trace.WriteLine("Resetting has been stopped\n");
            else
            {
                Trace.WriteLine("stop Resetting :" + actual.IsSucceed + "\n");
            }
        }


        /// <summary>
        ///A test for _csaEmdUnitRepository_ResettingFinished
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Vital.Business.dll")]
        public void _csaEmdUnitRepository_ResettingFinishedTest()
        {
            CsaEmdUnitManager_Accessor target = new CsaEmdUnitManager_Accessor(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            target._csaEmdUnitRepository_ResettingFinished(sender);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }



        [TestMethod()]
        public void DoEvents()
        {
            OpenConnectionTest();
            
            StartReadingTest();

            StartResettingTest();
            StopResettingTest();

            StopReadingTest();
            CloseConnectionTest();
        }
    }
}
