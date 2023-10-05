using ShoppingCart.API.Models;
using ShoppingCart.Models;
using Product = ShoppingCart.Models.Product;

namespace ShoppingCart.Services
{
    public interface IAPIService
    {
        Task<List<Product>> GetAll();
        Task<List<Models.Cart>> GetAllCart();
    }
}
