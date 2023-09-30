using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository repository;

        public OrderController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await repository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
        {
            var order = await repository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID = {id} not found");
            }
            return Ok(order);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> CreateOrderAsync(OrderDTO order)
        {
           var newOrder = await repository.CreateOrderAsync(order);
           return Ok(newOrder);
        }

        [HttpGet]
        [Route("ByUser/{userId}")]
        public async Task<IActionResult> GetOrdersByUserIdAsync([FromRoute] int userId)
        {
            var orders = await repository.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPut]
        [Route("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateOrderStatusAsync([FromRoute] int id, [FromQuery] string status)
        {
            var updatedOrder = await repository.UpdateOrderStatusAsync(id, status);

            if (updatedOrder != null)
            {
                return Ok(updatedOrder);
            }
            return NotFound($"Order with ID = {id} not found");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int id)
        {
            var order = await repository.DeleteOrderAsync(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order with ID = {id} not found");
        }
    }
}
