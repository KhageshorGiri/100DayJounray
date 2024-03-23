using ProductPro.Domain.Models;

namespace ProductPro.Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
