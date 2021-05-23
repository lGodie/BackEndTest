using System;
using System.Collections.Generic;

namespace Repositories.Shared.Core.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Total { get; set; }
        public bool? IsCancelled { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
