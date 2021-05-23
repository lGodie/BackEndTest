using System;
using System.Collections.Generic;

namespace Repositories.Shared.Core.Entities
{
    public partial class InventoryProduct
    {
        public InventoryProduct()
        {
            InvoiceDetail = new HashSet<InvoiceDetail>();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public int PriceUnit { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
