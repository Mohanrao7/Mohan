using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models.DTO
{
    public class CartDTO
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
