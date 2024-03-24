using Microsoft.AspNetCore.Mvc;
using ProductPro.Domain.Models;
using ProjectPro.Application.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;  
            _logger = logger;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var allProducts = await _productService.GetAllAsync();
            return allProducts.ToList();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var item =  await _productService.GetAsync(id);
            if(item is null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product newProduct)
        {
            await _productService.AddAsync(newProduct);
            return Ok();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product updatedPoduct)
        {
            var existingProduct = await _productService.GetAsync(id);
            if (existingProduct != null)
            {
                await _productService.UpdateAsync(id, updatedPoduct);
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var existingProduct =  await _productService.GetAsync(id);
            if (existingProduct != null)
            {
                await _productService.DeleteAsync(existingProduct);
                return Ok();
            }
            return NotFound();
        }

    }
}
