using ProductPro.Domain.Models;

namespace ProjectPro.Application.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
