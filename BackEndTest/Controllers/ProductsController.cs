using BackEndTest.Shared.Helpers;
using BackEndTest.Shared.Parameters;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using BackEndTest.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackEndTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct([FromBody] ProductRequest product)
        {
            if (product == null)
                return BadRequest("Error guardando el producto");
            try
            {
               Response rsp= await this._productService.CreateProductAsync(product);
                if (rsp.Success ==true)
                {
                    return StatusCode((int)HttpStatusCode.Created, rsp);
                }
                return StatusCode((int)HttpStatusCode.BadRequest, rsp);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { msg = "Error guardando el producto" });
            }

        }

        [HttpGet("All")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParameters parameters)
        {
            if (parameters == null)
                return null;
            try
            {
                PagedList<ProductResponse> products = await this._productService.GetProducts(parameters);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));
               
                return Ok(products);

            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { msg = "Error consultando el producto" });
            }
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(string ProductId)
        {
            if (ProductId == null)
                return BadRequest("Invalid ID");
            try
            {

                Response rsp = await _productService.GetProduct(ProductId);
                if (rsp.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, rsp);
                }
                return StatusCode((int)HttpStatusCode.OK, rsp);

            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { msg = "Error consultando el producto" });
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] ProductRequest product)
        {
            if (product == null)
                return BadRequest();
            try
            {
                Response rsp = await this._productService.UpdateProductAsync(product);
                if (rsp.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, rsp);
                }
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { msg = "Error guardando el producto" });
            }

        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            try
            {
                if (code == null)
                    return BadRequest("Invalid code");

                Response product = await _productService.GetProductByCode(code);

                if (product.Success == false)
                    return NotFound();

                Product data = (Product)product.Result;
                
                await this._productService.DeleteProductAsync(data);
                return NoContent();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { msg = "Error eliminando el producto" });
            }
        }

    }
}
