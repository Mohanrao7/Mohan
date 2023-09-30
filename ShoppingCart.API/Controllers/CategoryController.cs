using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository repository;

        public CategoryController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await repository.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryByID([FromRoute] int id)
        {
            var category = await repository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID = {id} not found");
            }
            return Ok(category);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCategory(CategoryDTO category)
        {
            var newCategory = await repository.AddCategory(category);
            return Ok(newCategory);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await repository.DeleteCategoryAsync(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound($"Category with ID = {id} not found");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryDTO category)
        {
            var updatedCategory = await repository.UpdateCategoryAsync(id, category);

            if (updatedCategory != null)
            {
                return Ok(updatedCategory);
            }
            return NotFound($"Category with ID = {id} not found");
        }
    }
}
