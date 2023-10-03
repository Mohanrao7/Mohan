using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public interface IAPIService
    {
        Task<List<Product>> GetAll();
    }
}
