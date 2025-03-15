using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vital.Business.Shared.Filters;

namespace Vital.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for AutoTestsManagersFixture and is intended
    ///to contain all AutoTestsManagersFixture Unit Tests
    ///</summary>
    [TestClass()]
    public class AutoTestsManagersFixture
    {               
        #region AutoTestSourceManager Tests

        /// <summary>
        ///A test for GetAutoProtocols
        ///</summary>
        [TestMethod()]
        public void GetAutoProtocolsTest()
        {
            var target = new AutoTestSourceManager();
            var results = target.GetAutoProtocols(new AutoProtocolsFilter());
            Assert.IsTrue(results.Count > 0);
        }

        /// <summary>
        ///A test for GetAutoProtocolRevisions
        ///</summary>
        [TestMethod()]
        public void GetAutoProtocolRevisionsTest()
        {
            var target = new AutoTestSourceManager();
            var results = target.GetAutoProtocolRevisions(new AutoProtocolRevisionsFilter());
            Assert.IsTrue(results.Count > 0);
        }

        /// <summary>
        ///A test for GetStageAutoItems
        ///</summary>
        [TestMethod()]
        public void GetStageAutoItemsTest()
        {
            var target = new AutoTestSourceManager();
            var results = target.GetStageAutoItems(new StageAutoItemsFilter());
            Assert.IsTrue(results.Count > 0);
        }

        #endregion

        #region AutoTestDestinationManager Tests

        /// <summary>
        ///A test for GetStageAutoItems
        ///</summary>
        [TestMethod()]
        public void GetAutoTestsTest()
        {
            var target = new AutoTestDestinationManager();
            var results = target.GetAutoTests(new AutoTestsFilter());
            Assert.IsTrue(results.Count > 0);
        }

        #endregion
    }
}
