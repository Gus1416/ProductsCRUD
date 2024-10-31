using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.API.Models;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult>CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("get_all_products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("get_product")]
        public async Task<IActionResult>GetProduct(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult>UpdateProduct(int id, Product product)
        {
            var currentProduct = await _context.Products.FindAsync(id);

            currentProduct!.Name = product.Name;
            currentProduct.Quantity = product.Quantity;
            currentProduct.Price = product.Price;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product!);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
