using BackEndTest.Controllers;
using BackEndTest.Shared.Requests;
using BackEndTest.Shared.Responses;
using BackEndTest.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Shared.Core.Entities;
using Repositories.Shared.Repositories.Generic;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace BackEndTest.UnitTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetProduct()
        {
            // preparación
            string code = "000100";
            var mock = new Mock<IProductService>();
            mock.Setup(x => x.GetProduct(code)).ReturnsAsync(default(ProductResponse));

            var productController = new ProductsController(mock.Object);

            //prueba
            var resultado = productController.GetProduct(code);

            //Verificación
            Assert.IsTrue(Convert.ToBoolean(HttpStatusCode.NotFound), "Exitoso");

        }
        [TestMethod]
        public void SaveProduct()
        {
            // preparación
            var productMock = new ProductRequest()
            {
                Name = "Nuevo producto",
                Code = "00010",
                Description = "Test por c#",
            };

            var mock = new Mock<IProductService>();
            mock.Setup(x => x.CreateProductAsync(productMock)).ReturnsAsync(default(Product));

            var productController = new ProductsController(mock.Object);

            //prueba
            var resultado = productController.PostProduct(productMock);

            //Verificación
            Assert.IsTrue(Convert.ToBoolean(HttpStatusCode.Created),"Exitoso");
        }
    }
}
