using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEndTest.Shared.Core
{
    public static class DbInitializer
    {
        public static async System.Threading.Tasks.Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var _context = new testContext(serviceProvider.GetRequiredService<DbContextOptions<testContext>>()))
            {
                if (_context.Category.Any())
                    return;
                _context.Category.AddRange(
                    new Category { Name = "Deportiva", Code = "001", Description = "Solo para deporte", CreatedAt = DateTime.Now },
                    new Category { Name = "Camisas", Code = "002", Description = "Para todos", CreatedAt = DateTime.Now },
                    new Category { Name = "Pantalones", Code = "003", Description = "Para todos", CreatedAt = DateTime.Now }
                 );

                if (_context.Product.Any())
                    return;
                _context.SaveChanges();

                Category FindCategory = await _context.Category.FirstOrDefaultAsync();

                _context.Product.AddRange(
                    new Product { Name = "Balón",  Code = "001", Description = "Para fútbol", CreatedAt = DateTime.Now, Category= FindCategory },
                    new Product { Name = "Camisa", Code = "002", Description = "para niño", CreatedAt = DateTime.Now, Category = FindCategory },
                    new Product { Name = "Pantalon", Code = "003", Description = "para hombre", CreatedAt = DateTime.Now, Category = FindCategory }
                 );

                _context.SaveChanges();
            }
        }
    }
}
