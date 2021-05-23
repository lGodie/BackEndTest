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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice([FromBody] InvoiceRequest invoice)
        {
            if (invoice == null)
                return BadRequest("Error guardando el factura");
            try
            {
                await this._invoiceService.CreateInviceAsync(invoice);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { msg = "Error guardando el factura" });
            }
        }

        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<InvoiceResponse>> GetInvoice(Guid invoiceId)
        {
            if (invoiceId == null)
                return BadRequest("Invalid ID");
            try
            {
                InvoiceResponse invoice = await _invoiceService.GetInvoice(invoiceId);

                if (invoice == null)
                    return NotFound("No se encontro factura");

                return invoice;
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { msg = "Error consultando el factura" });
            }

        }
        
        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> Delete(Guid invoiceId)
        {
            try
            {
                if (invoiceId == null)
                    return BadRequest("Invalid ID");


                await this._invoiceService.DeleteInvoiceAsync(invoiceId);
                return NoContent();
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new { msg = "Error eliminando el producto" });
            }
        }

    }
}
