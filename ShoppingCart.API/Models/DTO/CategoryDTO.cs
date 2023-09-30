using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models.DTO
{
    public class CategoryDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
