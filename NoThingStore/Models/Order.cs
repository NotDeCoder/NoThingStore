using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Total is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than 0.")]
        public decimal Total => OrderItems.Sum(oi => oi.Price * oi.Quantity);

        [Required(ErrorMessage = "Order items are required.")]
        public List<OrderItem> OrderItems { get; set; }
    }
}
