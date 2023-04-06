using Slugify;
using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public abstract class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name field must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Slug field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Slug field must be between 3 and 100 characters.")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "The Description field must be between 10 and 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, 1000000, ErrorMessage = "The Price field must be between 0.01 and 1 000 000.")]
        public decimal Price { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        public abstract Product GenerateSlug();
    }

}
