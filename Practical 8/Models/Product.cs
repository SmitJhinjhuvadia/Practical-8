using System.ComponentModel.DataAnnotations;

namespace Practical_8.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0", "999999999", ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }
    }
}