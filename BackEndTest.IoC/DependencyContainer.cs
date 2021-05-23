using BackEndTest.Repositories;
using BackEndTest.Services;
using BackEndTest.Shared.Repositories.Generic;
using BackEndTest.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using presupuestoback.Shared.v1.repositories;
using Repositories.Shared.Repositories.Generic;
using System;

namespace BackEndTest.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // Inject the service generic repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>(); 
            services.AddScoped<IProductRepository, ProductRepository>(); 
            services.AddScoped<IInvoiceService, InvoiceService>(); 
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            return services;
        }
    }
}
