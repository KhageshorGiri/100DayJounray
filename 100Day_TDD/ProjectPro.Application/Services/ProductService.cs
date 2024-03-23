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

        public async Task AddAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _productRepository.GetAsync(id);
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            product.Id =  id;
            return await _productRepository.UpdateAsync(product);
        }
    }
}
