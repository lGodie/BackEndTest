using BackEndTest.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Shared.Responses
{
    public class InvoiceResponse
    {
        public Guid Id { get; set; }
        public List<ProductsInvoice> Products { get; set; }
        public Guid CustomerId { get; set; }
        public int Total { get; set; }
        public DateTime DatePurchase { get; set; }
    }

}
