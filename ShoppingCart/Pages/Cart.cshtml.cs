using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using ShoppingCart.Services;

namespace ShoppingCart.Pages
{
    public class CartModel : PageModel
    {
        private readonly IAPIService apiService;
        public List<Cart> CartItems { get; set; }
        
        public CartModel(IAPIService apiService)
        {
            this.apiService = apiService;
        }
        public async Task OnGet()
        {
            CartItems = await apiService.GetAllCart();
        }
 
    }
}
