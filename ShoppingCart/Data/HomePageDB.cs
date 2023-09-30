using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public class HomePageDB : DbContext
    {
        public HomePageDB(DbContextOptions<HomePageDB> options) : base(options)
        {
            
        }
        public DbSet<HomePageModel> homePage { get; set; }
    }
}