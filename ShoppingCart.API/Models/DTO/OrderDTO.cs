using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models.DTO
{
    public class OrderDTO
    { 
        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;
    }
}
