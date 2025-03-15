using Vital.Business.Shared.DomainObjects.Invoices;

namespace Vital.Business.Shared.Filters
{
    public class InvoicesFilter : BaseFilter<Invoice>
    {
        public string Number { get; set; }

        public string Comments { get; set; }

        public decimal TotalAmount { get; set; }

        public int TestId { get; set; }
    }
}
