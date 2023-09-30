using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
