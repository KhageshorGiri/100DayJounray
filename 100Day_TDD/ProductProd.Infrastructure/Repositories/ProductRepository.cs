using ProductPro.Domain.IRepositories;
using ProductPro.Domain.Models;

namespace ProductProd.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        List<Product> products;
        public ProductRepository()
        {
            products = new List<Product>()
            {
                new Product() {Id=1, ProductName = "Item 1", ProductDescription = "First Item", Price=200},
                new Product() {Id=3, ProductName = "Item 2", ProductDescription = "Second Item", Price=400},
                new Product() {Id=4, ProductName = "Item 4", ProductDescription = "third Item", Price=600}
            };
        }

        public async Task AddAsync(Product product)
        {
            products.Add(product);
        }

        public async Task DeleteAsync(Product product)
        {
            products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return products;
        }

        public async Task<Product> GetAsync(int id)
        {
            return products.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = products.Where(x=>x.Id == product.Id).FirstOrDefault();
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.Price = product.Price;
            }

            return existingProduct;
        }
    }
}
