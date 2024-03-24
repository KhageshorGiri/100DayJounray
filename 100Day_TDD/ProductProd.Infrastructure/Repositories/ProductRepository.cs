using Microsoft.EntityFrameworkCore;
using ProductPro.Domain.IRepositories;
using ProductPro.Domain.Models;
using ProductProd.Infrastructure.Data;

namespace ProductProd.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductProDbContext _dbContext;
        public ProductRepository(ProductProDbContext context)
        {
           _dbContext = context;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                 _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Product product)
        {
            try
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Products
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> GetAsync(int id)
        {
            try
            {
                var product =  await _dbContext.Products.Where(x => x.Id == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                if (product is null)
                {
                    return null;
                }
                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            try
            {
                var existingProduct = await _dbContext.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.ProductDescription = product.ProductDescription;
                    existingProduct.Price = product.Price;
                }

                return existingProduct;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
    }
}
