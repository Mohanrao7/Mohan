using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepository repository;

        public CartController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cartItems = await repository.GetCartsAsync();
            return Ok(cartItems);
        }

        [HttpGet]
        [Route("UserID/{id}")]
        public async Task<IActionResult> GetCartItemByUserID([FromRoute] int id)
        {
            var cartItem = await repository.GetCartByUserIdAsync(id);
            if (cartItem == null)
            {
                return NotFound($"Cart item with ID = {id} not found");
            }
            return Ok(cartItem);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCartItem(CartDTO cartItem)
        {
            var newCartItem = await repository.AddCart(cartItem);
            return Ok(newCartItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await repository.DeleteCartByIdAsync(id);
            if (cartItem != null)
            {
                return Ok(cartItem);
            }
            return NotFound($"Cart item with ID = {id} not found");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] int id, [FromQuery] int Quantity)
        {
            var updatedCartItem = await repository.UpdateCartItemQuantityByIdAsync(id, Quantity);

            if (updatedCartItem != null)
            {
                return Ok(updatedCartItem);
            }
            return NotFound($"Cart item with ID = {id} not found");
        }
    }
}
