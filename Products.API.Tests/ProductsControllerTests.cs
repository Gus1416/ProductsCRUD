using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.API.Controllers;
using Products.API.Models;

namespace Products.API.Tests
{
    public class ProductsControllerTests
    {

        private ProductsContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(databaseName: "ProductsDb")
                .Options;
            return new ProductsContext(options);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenPriceIsNegative()
        {
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);
            var product = new Product
            {
                Name = "Test Product",
                Quantity = 10,
                Price = -50000
            };

            var result = await controller.CreateProduct(product);

            Assert.IsType<BadRequestObjectResult>(result); // Expecting a Bad Request Response
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenQuantityIsNegative()
        {
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);
            var product = new Product
            {
                Name = "Test Product",
                Quantity = -1,
                Price = 20000
            };

            var result = await controller.CreateProduct(product);

            Assert.IsType<BadRequestObjectResult>(result); // Expecting a Bad Request response
        }
    }
}