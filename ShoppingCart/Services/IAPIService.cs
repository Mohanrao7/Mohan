using ShoppingCart.Models;
using ShoppingCart.Pages;

namespace ShoppingCart.Services
{
    public interface IAPIService
    {
        Task<List<Product>> GetAll();
        Task<List<Cart>> GetAllCart();
    }
}
