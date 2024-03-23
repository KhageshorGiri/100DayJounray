using ProductPro.Domain.IRepositories;
using ProductPro.Domain.Models;
using ProjectPro.Application.IServices;

namespace ProjectPro.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepo)
        {

            _productRepository = productRepo;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
