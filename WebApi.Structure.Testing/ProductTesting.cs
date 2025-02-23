using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Web.Http;
using System.Web.Http.Results;
using WebAPI_Structure.App.DTO;
using WebAPI_Structure.Controllers;
using WebAPI_Structure.Core.Models;
using WebAPI_Structure.Infra.Services.Products;
using Xunit;

namespace WebApi.Structure.Testing
{
    public class ProductTesting
    {
        [Fact]
        public async void CreateProductController()
        {
            var service = new Mock<IProductsServices>().Object; 
            var controller = new ProductController(service);

            ProductDTO product =  new ProductDTO 
            {
                ProductId = 0,
                ProductName = "Test",
                Description = "Test",
                ProductPrice = 1,
                Notes = "Test",
                ProductImageUrl = "Test"
            };
           
            // Act  
            var actionResult = await controller.CreateOrUpdateProduct(product);
            var result = ((OkObjectResult)actionResult).StatusCode;

            Assert.NotNull(actionResult);
            Assert.Equal(200, result);
            // Assert  

        }

        [Fact]
        public async void UpdateProductController()
        {
            var service = new Mock<IProductsServices>().Object;
            var controller = new ProductController(service);

            ProductDTO product = new ProductDTO
            {
                ProductId = 1,
                ProductName = "Test",
                Description = "Test",
                ProductPrice = 1,
                Notes = "Test",
                ProductImageUrl = "Test"
            };

         
            // Act  
            var actionResult = await controller.CreateOrUpdateProduct(product);
            var result = ((OkObjectResult)actionResult).StatusCode;
            // Assert  
            Assert.NotNull(actionResult);
            Assert.Equal(200, result);
        }

        [Fact]
        public async void DeleteProductController()
        {
            var service = new Mock<IProductsServices>().Object;
            var controller = new ProductController(service);
            var productId = 1;
            // Act  
            var actionResult = await controller.DeleteProduct(productId);
            var result = ((OkObjectResult)actionResult).Value;
            // Assert  
            Assert.NotNull(actionResult);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetAllProductsController()
        {
            var service = new Mock<IProductsServices>().Object;
            var controller = new ProductController(service);
          
            // Act  
            var actionResult = await controller.GetAllProduct();
            var result = ((OkObjectResult)actionResult).Value;
            // Assert  
            Assert.NotNull(actionResult);
            Assert.Null(result);
        }
    }
}