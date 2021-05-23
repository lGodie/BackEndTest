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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private string includeProperties = string.Empty;
        private IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ProductService(IProductRepository repository) => this._repository = repository;

        public async Task<Response> CreateProductAsync(ProductRequest product)
        {
            var productBD = _mapper.Map<Product>(product);
            List<Product> productDB = this._repository.Get(x => x.Code == product.Code, null, includeProperties) as List<Product>;

            if (productDB.Count() >0)
            {
                return new Response
                {
                    Message = "Ya se encuentra un producto con este código",
                    Success = false
                };
            }

            Product rsp= await _repository.CreateAsync(productBD);
            var productResponse = _mapper.Map<ProductResponse>(rsp);

            return new Response
                {
                    Message = "Se guardo exitosamente",
                    Success = true,
                    Result= productResponse
            };
        }

        public async Task<int> DeleteProductAsync(Product product)
        {
            int deleted = 0;
            if (product != null)
            deleted = await this._repository.DeleteAsync(product);            

            return deleted;
        }

        public async Task<Response> GetProduct(string code)
        {
            ProductResponse result = null;
            List<ProductResponse> mapData =new List<ProductResponse>();
            includeProperties = "";
            List<Product> product = this._repository.Get(x => x.Code == code, null, includeProperties) as List<Product>;

            
            if (product.Count() == 0)
            {
                return new Response
                {
                    Message = "No se encuentra un producto con este código",
                    Success = false
                };
            }
            mapData = _mapper.Map<List<Product>, List<ProductResponse>>(product);
            result = mapData[0];
            return new Response
            {
                Message = "Consulta exitosa",
                Success = true,
                Result= result
            };

        }

        public async Task<PagedList<ProductResponse>> GetProducts(ProductParameters parameters)
        {
            List<Product> result = new List<Product>();
            if (parameters != null)
            {
                if (!string.IsNullOrEmpty(parameters.Name) && !string.IsNullOrEmpty(parameters.Code) )
                    result = this._repository.Get(x => x.Name.Contains(parameters.Name), x => x.OrderBy(y => y.Name), includeProperties) as List<Product>;
                else
                result = this._repository.Get(null, null, includeProperties) as List<Product>;
            }

            List<ProductResponse> mapData = _mapper.Map<List<Product>, List<ProductResponse>>(result);

            return await Task.Run(() => PagedList<ProductResponse>.ToPagedList(mapData, parameters.PageNumber, parameters.PageSize));

        }
        public async Task<Response> GetProductByCode(string code)
        {
            Product result = null;
            List<Product> mapData = new List<Product>();
            includeProperties = "";
            List<Product> product = this._repository.Get(x => x.Code == code, null, includeProperties) as List<Product>;


            if (product.Count() == 0)
            {
                return new Response
                {
                    Message = "No se encuentra un producto con este código",
                    Success = false
                };
            }
            mapData = product;
            result = mapData[0];
            return new Response
            {
                Message = "Consulta exitosa",
                Success = true,
                Result = result
            };

        }
        public async Task<Response> UpdateProductAsync(ProductRequest product)
        {
            Product productBD = _mapper.Map<Product>(product);
            productBD.ModifiedAt= DateTime.Now;
            Product resp=await this._repository.UpdateAsync(productBD, productBD.Id);

            if (resp == null)
            {
                return new Response
                {
                    Message = "No se encontro el producto",
                    Success = false
                };
            }

            return new Response
            {
                Message = "Consulta exitosa",
                Success = true,
                Result = _mapper.Map<ProductRequest>(resp)
            };
        }
    }
}
