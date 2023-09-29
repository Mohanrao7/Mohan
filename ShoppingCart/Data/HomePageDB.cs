using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Data
{
    public class HomePageDB : DbContext
    {
        public HomePageDB(DbContextOptions<HomePageDB> options) : base(options)
        {
            
        }

    }
}