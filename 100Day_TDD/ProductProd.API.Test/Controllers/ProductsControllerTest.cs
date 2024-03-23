using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductPro.API.Controllers;
using ProductPro.Domain.Models;
using ProjectPro.Application.IServices;

namespace ProductProd.API.Test.Controllers
{
    public class ProductsControllerTest
    {

        private readonly Mock<IProductService> serviceStub = new();
        private readonly Mock<ILogger<ProductsController>> loggerStub = new();
        private readonly Random rand = new();

        // Naming Convention
        [Fact]
        public void TestMethodName_WhatShouldHappen_ExpectedResults()
        {

        }

        [Fact]
        public async Task GetAllProducts_WithExistingProducts_ReturnExpectedProducts()
        {
            // Arrange
            var expectedProducts = new[]
            {
                CreateRandomProduct(), CreateRandomProduct(), CreateRandomProduct()
            };

            serviceStub.Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedProducts);

            var controller = new ProductsController(serviceStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetAllProducts();

            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedProducts ,
                option => option.ComparingByMembers<Product>());
        }

        [Fact]
        public async Task GetProduct_WithUnexistingItem_ReturnNotFound()
        {
            // Arragnge
            serviceStub.Setup(service => service.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var controller = new ProductsController(serviceStub.Object,loggerStub.Object);

            // Act 
            var result = await controller.GetProduct(rand.Next(1000));

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetProduct_WithExistingProduct_ReturnExpectedproduct()
        {
            // Arrange
            var expectedProduct = CreateRandomProduct();

            serviceStub.Setup(service => service.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedProduct);

            var controller = new ProductsController(serviceStub.Object, loggerStub.Object);

            // Act 
            var result = await controller.GetProduct(rand.Next(100));

            // Assert
            result.Value.Should().BeEquivalentTo(expectedProduct, 
                option=>option.ComparingByMembers<Product>());
        }

        [Fact]
        public async Task AddProduct_WithProductToCreate_ReturnResponseOk()
        {
            // Arrange
            var productToCreate = CreateRandomProduct();

            var controller = new ProductsController(serviceStub.Object, loggerStub.Object);

            // Act
            var result = await controller.AddProduct(productToCreate);

            // Assert
            result.Should().BeEquivalentTo(
                productToCreate,
                option => option.ComparingByMembers<Product>());
        }

        private Product CreateRandomProduct()
        {
            return new()
            {
                Id = rand.Next(100),
                ProductName = "Test Product",
                ProductDescription = "Test Description",
                Price = rand.Next(100)

            };
        }
    }
}
