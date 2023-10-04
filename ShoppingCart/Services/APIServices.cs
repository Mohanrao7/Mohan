using ShoppingCart.Models;
using System.Text;

namespace ShoppingCart.Services
{
    public class APIServices : IAPIService
    {
        private readonly IHttpClientFactory httpClientFactory;



        public APIServices(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<List<Product>> GetAll()
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync("api/Product");
            if (response.IsSuccessStatusCode)
            {
                var productList = await response.Content.ReadFromJsonAsync<List<Product>>();
                return productList;
            }
            else
                return null;
        }

        public async Task<List<Cart>> GetAllCart()
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync("api/Cart");
            if (response.IsSuccessStatusCode)
            {
                var cartList = await response.Content.ReadFromJsonAsync<List<Cart>>();
                return cartList;
            }
            else
                return null;
        }

    }
}
