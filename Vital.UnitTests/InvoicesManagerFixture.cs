using System.Linq;
using Vital.Business.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Services;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.UnitTests
{
    /// <summary>
    ///This is a test class for Invoices and is intended
    ///to contain all Invoices Manager Unit Tests
    ///</summary>
    [TestClass()]
    public class InvoicesManagerFixture
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        /// <summary>
        ///A test for SaveService
        ///</summary>
        [TestMethod()]
        public void SaveServiceTest()
        {
            var target = new InvoicesManager();

            var invoice = new Invoice()
            {
                Number = "1234",
                Comments = "Notes",
                TotalAmount = 1600,
                Test = new Test() { Id = 1}
            };

            var actual = target.SaveInvoice(invoice);

            Assert.IsTrue(actual.IsSucceed);
        }


        [TestMethod()]
        public void SaveInvoice()
        {
            var target = new InvoicesManager();

            var invoice = new Invoice()
            {
                Number = target.GenerateInvoiceNumber(2),
                Comments = "Notes",
                TotalAmount = 1600,
                Test = new Test() { Id = 3 },
                CreationDateTime = DateTime.Now,
            };

            var actual = target.SaveInvoice(invoice);

            Assert.IsTrue(actual.IsSucceed);
        }

        [TestMethod()]
        public void SaveEditedInvoice()
        {
            var target = new InvoicesManager();

            var invoice = new Invoice()
            {
                Id = 1,
                Number = "12345",
                Comments = "Notes",
                TotalAmount = 1600,
                Test = new Test() { Id = 1 },
                CreationDateTime = DateTime.Now,
            };

            var actual = target.SaveInvoice(invoice);

            Assert.IsTrue(actual.IsSucceed);
        }
        

        [TestMethod()]
        public void GetInvoiceById()
        {
            var target = new InvoicesManager();

            var filter = new SingleItemFilter() {ItemId = 1};

            var actual = target.GetInvoiceById(filter);

            Assert.IsTrue(actual != null);
        }

        /// <summary>
        ///A test for GetServiceById
        ///</summary>
        [TestMethod()]
        public void GetInvoices()
        {
            var target = new InvoicesManager();

            var invoicesFilter = new InvoicesFilter();

            var actual = target.GetInvoices(invoicesFilter);

            Assert.IsNotNull(actual);
        }

        
        [TestMethod()]
        public void DeleteServiceTest()
        {
            var target = new InvoicesManager();

            var invoice = new Invoice()
            {
                Id = 1,
            };

            var actual = target.DeleteInvoice(invoice);

            Assert.IsTrue(actual.IsSucceed);
        }
    }
}
