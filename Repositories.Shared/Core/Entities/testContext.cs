using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repositories.Shared.Core.Entities
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<InventoryProduct> InventoryProduct { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
