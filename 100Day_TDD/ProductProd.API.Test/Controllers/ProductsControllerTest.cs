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
        private readonly Random rand = new Random();
        private readonly Mock<IProductService> serviceStub = new Mock<IProductService>();
        private readonly Mock<ILogger<ProductsController>> logger = new();

        [Fact]
        public async Task GetProduct_WhenUnExistingProduct_ReturnNotFound()
        {
            // Arrange
            serviceStub.Setup(service => service.GetAsync(It.IsAny<int>())).
                ReturnsAsync((Product)null);

            var controller = new ProductsController(serviceStub.Object, logger.Object);

            // Act
            var result = await controller.GetProduct(rand.Next(1000));

            // Assert
            Assert.Null(result.Value);
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetProduct_WhenExistingProduct_ReturnProduct()
        {
            // Arrange

            var expectedProduct = CreateRandomProduct();

           
            serviceStub.Setup(service => service.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedProduct);

            var controller = new ProductsController(serviceStub.Object, logger.Object);

            // Act
            var result = await controller.GetProduct(It.IsAny<int>());

            // Assert
            result.Value.Should().BeEquivalentTo(expectedProduct, 
                option => option.ComparingByMembers<Product>());
        }

        [Fact]
        public async Task GetAllProducts_WithExistingProducts_ReturnExpectedProducts()
        {
            // Arrange
            var expectedResult = new[] { CreateRandomProduct(), CreateRandomProduct(), CreateRandomProduct() };

            serviceStub.Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedResult);

            var controller = new ProductsController(serviceStub.Object, logger.Object);

            // Act
            var result = await controller.GetAllProducts();

            // Assert
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expectedResult,
                optins => optins.ComparingByMembers<Product>());
        }

        [Fact]
        public async Task AddProduct_WithProductToAdd_ReturnResponseOk()
        {
            // Arrange
            var productToAdd = CreateRandomProduct();
           
            var controller = new ProductsController(serviceStub.Object, logger.Object);

            // Act
            var result = await controller.AddProduct(productToAdd);

            // Assert
            result.Should().BeAssignableTo<OkResult>();
        }

        [Fact]
        public async Task UpdateProduct_WithUnexistingProduct_ReturnNotFound()
        {
            // Arrange
            var exisgintProduct = CreateRandomProduct();

            serviceStub.Setup(service => service.UpdateAsync(It.IsAny<int>(), exisgintProduct))
                .ReturnsAsync((Product)null);

            var controller = new ProductsController(serviceStub.Object, logger.Object);

            // Act
            var result = await controller.UpdateProduct(It.IsAny<int>(), exisgintProduct);

            // Assert
            Assert.Null(result.Value);
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateProduct_WithExistingProduct_ReturnUpdatedProduct()
        {
            // Arrange
            var exisgintProduct = CreateRandomProduct();

            var controller = new ProductsController(serviceStub.Object, logger.Object);

            // Act
            var result = await controller.UpdateProduct(exisgintProduct.Id, exisgintProduct);


            // Assert
            result.Should().BeAssignableTo<OkResult>();
        }


        private Product CreateRandomProduct()
        {
            return new()
            {
                Id = rand.Next(100),
                ProductName = "Test Prduct",
                ProductDescription = "Test Description",
                Price = rand.Next(1000)
            };
        }
    }
}
