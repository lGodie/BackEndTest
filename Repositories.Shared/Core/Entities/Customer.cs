using System;
using System.Collections.Generic;

namespace Repositories.Shared.Core.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Invoice = new HashSet<Invoice>();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Cedula { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
