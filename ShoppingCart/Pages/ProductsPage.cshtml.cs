using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;


namespace ShoppingCart.Pages
{
    public class ProductsPageModel : PageModel
    {
        private readonly IAPIService apiService;
        public List<Product> ProductItems { get; set; }

        public ProductsPageModel(IAPIService apiService)
        {
            this.apiService = apiService;
        }
        public async Task OnGet()
        {
            ProductItems = await apiService.GetAll();
        }
    }
}
