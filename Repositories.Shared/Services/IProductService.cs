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
    public interface IProductService
    {
        Task<PagedList<ProductResponse>> GetProducts(ProductParameters parameters);
        Task<Response> GetProduct(string code);
        Task<Response> CreateProductAsync(ProductRequest product);
        Task<Response> UpdateProductAsync(ProductRequest product);
        Task<Response> GetProductByCode(string code);
        Task<int> DeleteProductAsync(Product product);
    }
}
