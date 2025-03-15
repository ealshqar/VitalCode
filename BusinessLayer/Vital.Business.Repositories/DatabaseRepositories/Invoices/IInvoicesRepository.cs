using System.ComponentModel;
using Vital.Business.Shared.DomainObjects.Invoices;
using Vital.Business.Shared.DomainObjects.PatientSchedules;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Repositories.DatabaseRepositories.Invoices
{
    public interface IInvoicesRepository
    {
        #region Invoice

        /// <summary>
        /// Loads Invoice
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Invoice</returns>
        Invoice LoadInvoiceById(int id);

        /// <summary>
        /// Loads a list of invoices.
        /// </summary>
        /// <returns>List of Invoices.</returns>
        BindingList<Invoice> LoadInvoices(int testId,string number, string comments, decimal totalAmount);

        /// <summary>
        /// Saves an invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>The invoice.</returns>
        ProcessResult Save(Invoice invoice);

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>The result.</returns>
        ProcessResult Delete(Invoice invoice);

        #endregion
    }
}
