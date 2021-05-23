using BackEndTest.Shared.Helpers;
using BackEndTest.Shared.Parameters;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using Repositories.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Shared.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetInvoice(Guid Id);
        Task<Response> CreateInviceAsync(InvoiceRequest invoice);
        Task<int> DeleteInvoiceAsync(Guid invoice);
    }
}
