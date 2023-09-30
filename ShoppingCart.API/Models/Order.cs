using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;
    }
}
