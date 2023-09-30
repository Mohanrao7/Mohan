using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository repository;

        public ProductController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var Products = await repository.GetProductsAsync();
            return Ok(Products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductByID([FromRoute] int id)
        {
            var product = await repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID = {id} not found");
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddProduct(ProductDTO Product)
        {
            var NewProduct = await repository.AddProduct(Product);
            return Ok(NewProduct);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var Product = await repository.DeleteProductAsync(id);
            if (Product != null)
            {
                return Ok(Product);
            }
            return NotFound($"Customer with ID = {id} not found");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductDTO Product)
        {
            var product = await repository.UpdateProductAsync(id, Product);

            if (product != null)
            {
                return Ok(product);
            }
            return NotFound($"Customer with ID = {id} not found");
        }
    }
}
