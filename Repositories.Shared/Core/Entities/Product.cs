using System;
using System.Collections.Generic;

namespace Repositories.Shared.Core.Entities
{
    public partial class Product
    {
        public Product()
        {
            InventoryProduct = new HashSet<InventoryProduct>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<InventoryProduct> InventoryProduct { get; set; }
    }
}
