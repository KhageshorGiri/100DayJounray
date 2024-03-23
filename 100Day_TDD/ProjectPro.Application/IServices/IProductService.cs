using ProductPro.Domain.Models;

namespace ProjectPro.Application.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetAsync(int id);

        Task AddAsync(Product product);

        Task<Product> UpdateAsync(int id, Product product);

        Task DeleteAsync(Product product);
    }
}
