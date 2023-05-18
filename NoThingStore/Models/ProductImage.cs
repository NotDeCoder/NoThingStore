using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please choose an image file")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
