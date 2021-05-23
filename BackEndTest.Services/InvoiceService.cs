using AutoMapper;
using BackEndTest.Shared.Helpers;
using BackEndTest.Shared.Parameters;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using BackEndTest.Shared.Services;
using Repositories.Shared.Core.Entities;
using Repositories.Shared.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private string includeProperties = string.Empty;
        private IMapper _mapper;
        public InvoiceService(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response> CreateInviceAsync(InvoiceRequest invoice)
        {
         return await _repository.SaveInvoiceAsync(invoice);
        }
        public async Task<InvoiceResponse> GetInvoice(Guid id)
        {
            InvoiceResponse result = null;

            Response resp = await _repository.GetInvoiceAsync(id);

            if (resp.Success == true)
            {
                InvoiceResponse invoice = _castInvoice((Invoice)resp.Result);

                result = invoice;
            }
            return result;

        }
        public async Task<int> DeleteInvoiceAsync(Guid id)
        {
            Response resp = await _repository.DeleteInvoice(id);
            if (resp.Success == true) return 1;
            return 0;
        }
        
        private InvoiceResponse _castInvoice(Invoice item)
        {
            return new InvoiceResponse
            {
                Id = item.Id,
                CustomerId = item.CustomerId,
                Total = item.Total,
                DatePurchase = item.DatePurchase,
                Products = item.InvoiceDetail.Select(a => new ProductsInvoice()
                {
                    Id = a.InventoryProduct.ProductId,
                    Quantity = a.Quantity,
                    Total = a.Price
                }).ToList()
            };
        }
        
    };
        
}
  
