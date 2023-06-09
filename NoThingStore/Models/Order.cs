using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public decimal Total => OrderItems != null && OrderItems.Count > 0 ? OrderItems.Sum(oi => oi.Price * oi.Quantity) : 0;

        [Required(ErrorMessage = "Order items are required.")]
        public List<OrderItem> OrderItems { get; set; }
    }
}
