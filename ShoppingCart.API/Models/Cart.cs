using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
