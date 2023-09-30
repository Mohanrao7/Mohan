using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;


namespace ShoppingCartApplication.Pages
{
    public class itemModel : PageModel
    {
        public List<HomePageModel> cartDB = new List<HomePageModel>()
        {
            new HomePageModel(){ImageTitle =  "OnePlus", Price = 44999 , Name = "One Plus"},
            new HomePageModel(){ImageTitle =  "Iphone", Price = 85999, Name = "Iphone"},
            new HomePageModel(){ImageTitle =  "IQOO", Price = 21999, Name = "Iqoo"},
        };

        public void OnGet()
        {
        }
    }
}
