using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository repository;

        public CustomerController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await repository.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerByID([FromRoute] int id)
        {
            var customer = await repository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID = {id} not found");
            }
            return Ok(customer);    
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCustomer(CustomerDTO Customer)
        {
            var NewCustomer = await repository.AddCustomer(Customer);
            return Ok(NewCustomer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var Customer = await repository.DeleteCustomerAsync(id);
            if (Customer != null)
            {
                return Ok(Customer);
            }
            return NotFound($"Customer with ID = {id} not found");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerDTO Customer)
        {
            var Cust = await repository.UpdateCustomerAsync(id, Customer);

            if (Cust != null)
            {
                return Ok(Cust);
            }
            return NotFound($"Customer with ID = {id} not found");
        }
    }
}
