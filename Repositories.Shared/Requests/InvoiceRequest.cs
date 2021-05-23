using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Shared.Requests
{
    public class InvoiceRequest
    {
        public Guid Id { get; set; }
        public List<ProductsInvoice> Products { get; set; }
        public Guid CustomerId { get; set; }
        public string DatePurchase { get; set; }
    }
    public class ProductsInvoice
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
