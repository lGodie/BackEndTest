using System;
using System.Collections.Generic;

namespace Repositories.Shared.Core.Entities
{
    public partial class InvoiceDetail
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid InventoryProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public virtual InventoryProduct InventoryProduct { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
