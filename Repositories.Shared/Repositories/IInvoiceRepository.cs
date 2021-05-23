using BackEndTest.Shared.Repositories.Generic;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using Repositories.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Shared.Repositories.Generic
{
   public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<Response> SaveInvoiceAsync(InvoiceRequest invoiceRequest);
        Task<Response> GetInvoiceAsync(Guid InvoiceId);
        Task<Response> DeleteInvoice(Guid id);
    }
}
