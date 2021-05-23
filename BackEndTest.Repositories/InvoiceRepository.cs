using BackEndTest.Shared.Repositories.Generic;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using presupuestoback.Shared.v1.repositories;
using Repositories.Shared.Core.Entities;
using Repositories.Shared.Repositories.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        private readonly testContext _context;

        public InvoiceRepository(IUnitOfWork _unitOfWork, testContext context)
            : base(_unitOfWork)
        {
            _context = context;
        }

        public async Task<Response> DeleteInvoice(Guid id)
        {         
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                if (id == null)
                {
                    return new Response
                    {
                        Message = "No fue posible eliminar la factura",
                        Success = false
                    };
                }

                Invoice existInvoice = await FindInvoiceById(id,false);

                if (existInvoice == null)
                {
                    return new Response
                    {
                        Message = "No fue posible eliminar la factura",
                        Success = false
                    };
                }
                existInvoice.IsCancelled = true;
                _context.Invoice.Update(existInvoice);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new Response
                {
                    Message = "Se elimino exitosamente la factura",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response
                {
                    Message = "No fue posible eliminar la factura",
                    Success = false,
                };
            }
        }
        public async Task<Response> SaveInvoiceAsync(InvoiceRequest invoiceRequest)
        {
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Invoice existInvoice = await FindInvoiceById(invoiceRequest.Id,false);

            if (existInvoice == null)
            {
                Invoice invoice= await saveInvoice(invoiceRequest);

                foreach (ProductsInvoice productRequest in invoiceRequest.Products)
                {

                      InventoryProduct inventoryProduct = await _context.InventoryProduct
                                                        .Include(t=>t.Product)
                                                        .Where(t=>t.ProductId== productRequest.Id)
                                                        .FirstOrDefaultAsync();

                        if (inventoryProduct == null)
                             return new Response
                            {
                                Message = "No se encontro este producto en el inventario",
                                Success = false,
                            };
                        
                        if (inventoryProduct.Stock <=5)
                        {
                            return new Response
                            {
                                Message = "El producto se encuentra agotado",
                                Success = false,
                            };
                        }

                   await SaveInvoiceDetail(productRequest, invoice, inventoryProduct);
                   await ReduceInventoryExistence(inventoryProduct, productRequest.Quantity);
                }                
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new Response
            {
                Message = "Succes",
                Success = true,
            };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response
                {
                    Message = "No fue posible guardar la factura",
                    Success = false,
                };
            }
        }

        public async Task<Response> GetInvoiceAsync(Guid InvoiceId)
        {
            Invoice resp = await FindInvoiceById(InvoiceId,false);
            if (resp == null)
            {
                return new Response
                {
                    Message = "No se encontro factura",
                    Success = false
                };
            }
            return new Response
            {
                Result = resp,
                Success = true,
                Message = "Exitosa"
            };
        }

        private async Task<Invoice> saveInvoice(InvoiceRequest invoiceRequest)
        {
            Invoice invoice = new Invoice();
            invoice.CustomerId = invoiceRequest.CustomerId;
            invoice.IsCancelled = true;
            invoice.DatePurchase = Convert.ToDateTime(invoiceRequest.DatePurchase);
            invoice.CreatedAt = DateTime.Now;
            invoice.Total = invoiceRequest.Products.Sum(t => t.Total);
            await _context.Invoice.AddAsync(invoice);
            return invoice;
        }
        private async Task<InvoiceDetail> SaveInvoiceDetail(ProductsInvoice productsInvoice, Invoice invoice, InventoryProduct inventoryProduct)
        {
            InvoiceDetail invoiceDetail = new InvoiceDetail();
            invoiceDetail.Invoice = invoice;
            invoiceDetail.InventoryProduct = inventoryProduct;
            invoiceDetail.Quantity = productsInvoice.Quantity;
            invoiceDetail.Price = productsInvoice.Total;

            await _context.InvoiceDetail.AddAsync(invoiceDetail);
            return invoiceDetail;
        }
        private async Task<InventoryProduct> ReduceInventoryExistence(InventoryProduct inventoryProduct,int ProductQuantity)
        {
            inventoryProduct.Stock -= ProductQuantity;
            _context.InventoryProduct.Update(inventoryProduct);
            return inventoryProduct;
        }
        private async Task<Invoice> FindInvoiceById(Guid Id,bool isCancelled)
        {
            Invoice existInvoice = await _context.Invoice
                                       .Include(t => t.Customer)
                                       .Include(t => t.InvoiceDetail)
                                       .ThenInclude(t => t.InventoryProduct)
                                       .ThenInclude(t => t.Product)
                                       .Where(e => e.Id == Id && e.IsCancelled == isCancelled)
                                       .FirstOrDefaultAsync();
            return existInvoice;
        }
    }
}
