using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Vital.Business.Repositories.DatabaseRepositories.Invoices;
using Vital.Business.Repositories.DatabaseRepositories.Tests;
using Vital.Business.Shared;
using Vital.Business.Shared.DomainObjects;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.Tests;
using Vital.Business.Shared.Enums;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Filters;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class InvoicesManager : BaseManager
    {
        #region Private Variables

        private readonly IInvoicesRepository _invoicesRepository;
        private readonly ITestRepository _testsRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public InvoicesManager()
        {
            _invoicesRepository = new InvoicesDatabaseRepository();
            _testsRepository = new TestDatabaseRepository();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets an invoice.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Invoice GetInvoiceById(SingleItemFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);
            Check.Argument.IsNotNegativeOrZero(filter.ItemId, "item id");

            try
            {
                return _invoicesRepository.LoadInvoiceById(filter.ItemId);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets a list of invoices.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public BindingList<Invoice> GetInvoices(InvoicesFilter filter)
        {
            Check.Argument.IsNotNull(() => filter);

            try
            {
                return _invoicesRepository.LoadInvoices(filter.TestId,filter.Number , filter.Comments, filter.TotalAmount);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Saves the Invoice.
        /// </summary>
        /// <param name="invoice">The Invoice.</param>
        /// <returns></returns>
        public ProcessResult SaveInvoice(Invoice invoice)
        {
            Check.Argument.IsNotNull(() => invoice);

            if (!invoice.IsChanged) return ProcessResult.Succeed;

            try
            {
                invoice.SetUserAndDates();

                var processResult = _invoicesRepository.Save(invoice);

                if (processResult.IsSucceed)
                {
                    invoice.ObjectState = DomainEntityState.Unchanged;
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
        /// Deletes an invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns></returns>
        public ProcessResult DeleteInvoice(Invoice invoice)
        {
            Check.Argument.IsNotNull(() => invoice);

            try
            {
                var processResult = _invoicesRepository.Delete(invoice);

                if (processResult.IsSucceed)
                {
                    invoice.ObjectState = DomainEntityState.Deleted;
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
        /// Generate Number
        /// </summary>
        /// <returns></returns>
        public string GenerateInvoiceNumber(int patientId)
        {
            try
            {
                var tests = _testsRepository.LoadTests(string.Empty, patientId, 0, 0, null, LoadingTypeEnum.Light);

                var newInvoiceNumber = GetNextInvoiceNumber(tests).ToString(CultureInfo.InvariantCulture);

                return newInvoiceNumber;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                if (exception is VitalDatabaseException) throw;
                throw new VitalLogicalException(exception);
            }
        }

        /// <summary>
        /// Gets the next invoice number.
        /// </summary>
        /// <param name="tests">The tests list.</param>
        /// <returns></returns>
        private int GetNextInvoiceNumber(IEnumerable<Test> tests)
        {
            var patientInvoices = tests.SelectMany(test => test.Invoices).ToList();

            if (patientInvoices.Count == 0)
                return 1;

            var lastInvoiceDate = patientInvoices.Max(c => c.CreationDateTime);

            var lastInvoice = patientInvoices.FirstOrDefault(c => c.CreationDateTime == lastInvoiceDate);

            var lastInvoiceNumber = lastInvoice != null ? Convert.ToInt32(lastInvoice.Number) : 0;

            return lastInvoiceNumber + 1;
        }

        #endregion
    }
}