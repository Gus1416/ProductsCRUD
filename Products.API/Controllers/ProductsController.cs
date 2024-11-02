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

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product object to create.</param>
        /// <returns>Returns the created product if successful.</returns>
        /// <response code="200">Product created successfully.</response>
        /// <response code="400">Invalid product data (e.g., negative price or quantity).</response>
        /// <response code="500">Internal server error when creating the product.</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (product.Price < 0 || product.Quantity < 0)
            {
                return BadRequest("Price and Quantity must be non-negative values.");
            }

            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating product: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products in the database.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="500">Internal server error when retrieving products.</response>
        [HttpGet]
        [Route("get_all_products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The requested product if found.</returns>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">Product not found.</response>
        /// <response code="500">Internal server error when retrieving the product.</response>
        [HttpGet]
        [Route("get_product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>GetProduct(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Updates an existing product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product object.</param>
        /// <returns>Returns the updated product if successful.</returns>
        /// <response code="200">Product updated successfully.</response>
        /// <response code="404">Product not found.</response>
        /// <response code="400">Invalid product data.</response>
        /// <response code="500">Internal server error when updating the product.</response>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>UpdateProduct(int id, Product product)
        {
            try
            {
                var currentProduct = await _context.Products.FindAsync(id);

                if (currentProduct == null)
                {
                    return NotFound();
                }

                currentProduct!.Name = product.Name;
                currentProduct.Quantity = product.Quantity;
                currentProduct.Price = product.Price;

                await _context.SaveChangesAsync();

                return Ok(currentProduct);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating product: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>Status 200 OK if the product is deleted successfully.</returns>
        /// <response code="200">Product deleted successfully.</response>
        /// <response code="404">Product not found.</response>
        /// <response code="500">Internal server error when deleting the product.</response>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product!);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting product: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }

        }

    }
}
