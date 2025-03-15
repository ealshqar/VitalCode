using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using SD.LLBLGen.Pro.LinqSupportClasses;
using Vital.Business.Repositories.Shared;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;
using Vital.DataLayer.DatabaseSpecific;
using Vital.DataLayer.EntityClasses;
using Vital.DataLayer.Linq;

namespace Vital.Business.Repositories.DatabaseRepositories.Invoices
{
    public class InvoicesDatabaseRepository : BaseRepository, IInvoicesRepository
    {

        #region PathEdges

        private readonly Func<IPathEdgeRootParser<InvoiceEntity>, IPathEdgeRootParser<InvoiceEntity>>
            _pathEdgesInvoice = c => c.Prefetch(ts => ts.Test);

        #endregion

        #region Test Schedules

        #region Public Methods
        
        /// <summary>
        /// Loads an invoice by id.
        /// </summary>
        /// <param name="id">The invoice id.</param>
        /// <returns></returns>
        public Invoice LoadInvoiceById(int id)
        {
            Check.Argument.IsNotNegativeOrZero(id, "id");

            try
            {
                using (var adapter = new DataAccessAdapter())
                {
                    var data = new LinqMetaData(adapter);

                    var src = data.Invoice.Where(c => c.Id == id).WithPath(_pathEdgesInvoice);

                    var invoiceEntity = src.FirstOrDefault();

                    var invoiceObj = new Invoice();

                    Mapper.Map(invoiceEntity, invoiceObj);

                    return invoiceObj;
                }
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Loads a list of invoices.
        /// </summary>
        /// <param name="testId">The test id.</param>
        /// <param name="number">The number.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="totalAmount">The total amount.</param>
        /// <returns></returns>
        public BindingList<Invoice> LoadInvoices(int testId,string number, string comments, decimal totalAmount)
        {
            try
            {
                return LoadInvoicesWorker(testId,number,comments,totalAmount,_pathEdgesInvoice);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Saves an invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>The invoice.</returns>
        public ProcessResult Save(Invoice invoice)
        {
            Check.Argument.IsNotNull(invoice, "invoice");

            try
            {
                var invoiceEntity = Mapper.Map<Invoice, InvoiceEntity>(invoice);

                invoiceEntity.IsNew = invoiceEntity.Id <= 0;

                var processResult = CommonRepository.Save(invoiceEntity);

                invoice.Id = invoiceEntity.Id;

                return processResult;
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>The result.</returns>
        public ProcessResult Delete(Invoice invoice)
        {
            Check.Argument.IsNotNull(invoice, "invoice");

            try
            {
                var invoiceEntity = Mapper.Map<Invoice, InvoiceEntity>(invoice);

                return CommonRepository.Delete(invoiceEntity);
            }
            catch (Exception exception)
            {
                throw new VitalDatabaseException(exception);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads a list of invoices.
        /// </summary>
        /// <param name="testId">The test id.</param>
        /// <param name="number">The number.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="totalAmount">The total amount.</param>
        /// <param name="pathEdgesInvoice">The path edges.</param>
        /// <returns>List of invoices.</returns>
        private static BindingList<Invoice> LoadInvoicesWorker(int testId,string number, string comments, decimal totalAmount, Func<IPathEdgeRootParser<InvoiceEntity>, IPathEdgeRootParser<InvoiceEntity>> pathEdgesInvoice)
        {
            using (var adapter = new DataAccessAdapter())
            {
                var data = new LinqMetaData(adapter);

                var src = data.Invoice.AsQueryable();

                if (pathEdgesInvoice != null)
                    src = src.WithPath(pathEdgesInvoice).AsQueryable();

                if(!string.IsNullOrEmpty(number))
                    src = src.Where(c => c.Number.ToLower().Contains(number));

                if (testId > 0)
                    src = src.Where(c => c.TestId == testId);

                if (totalAmount > 0)
                    src = src.Where(c => c.TotalAmount == totalAmount);

                if (!string.IsNullOrEmpty(comments))
                    src = src.Where(c => c.Comments.ToLower().Contains(comments));

                var invoicesEntities = src.ToList();

                var invoicesList = new BindingList<Invoice>();

                Mapper.Map(invoicesEntities, invoicesList);

                return invoicesList;
            }
        }

        #endregion

        #endregion

    }
}
