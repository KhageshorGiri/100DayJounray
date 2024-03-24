using FluentAssertions;
using Moq;
using ProductPro.Domain.IRepositories;
using ProductPro.Domain.Models;
using ProjectPro.Application.Services;

namespace ProductPro.Application.Test.Services
{
    public class ProductServiceTest
    {
        private readonly Random rand = new Random();
        private readonly Mock<IProductRepository> repositoryStub = new();

        [Fact]
        public async Task GetAllAsync_WithExistingProductList_ReturnProductList()
        {
            // Arrange
            var expectedList = new[] { CreateRandomProduct(), CreateRandomProduct(), CreateRandomProduct() };

            repositoryStub.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedList);

            var service = new ProductService(repositoryStub.Object);

            // Act
            var result = await service.GetAllAsync();

            // Asserts
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(expectedList,
                option => option.ComparingByMembers<Product>());
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
