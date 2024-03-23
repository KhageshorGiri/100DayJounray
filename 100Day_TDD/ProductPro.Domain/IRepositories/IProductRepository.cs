using ProductPro.Domain.Models;

namespace ProductPro.Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetAsync(int id);

        Task AddAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}
