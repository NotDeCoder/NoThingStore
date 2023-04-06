using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public class CartItem
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be at least 0.01")]

        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        [Required]
        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get { return Price * Quantity; }
        }
    }
}
