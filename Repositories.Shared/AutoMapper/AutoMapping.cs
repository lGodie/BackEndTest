using AutoMapper;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using Repositories.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Shared.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<Invoice, InvoiceResponse>();

            CreateMap<InvoiceDetail, InvoiceResponse>();

            CreateMap<ProductRequest, Product>();

            CreateMap<ProductResponse, Product>();

            CreateMap<Product , ProductRequest>();
        }
    }
}
