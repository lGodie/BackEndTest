using System;
using System.Collections.Generic;

namespace Repositories.Shared.Core.Entities
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
