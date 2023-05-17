using Slugify;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoThingStore.Models
{
    public abstract class Product
    {
        private readonly ISlugHelper _slugHelper;

        public Product()
        {
            _slugHelper = new SlugHelper();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name field must be between 3 and 100 characters.")]
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Slug = _slugHelper.GenerateSlug(value);
            }
        }

        [Required(ErrorMessage = "The Slug field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Slug field must be between 3 and 100 characters.")]
        public string Slug { get; private set; }

        [Required(ErrorMessage = "The Short Description field is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The Short Description field must be between 5 and 100 characters.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "The Long Description field is required.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "The Long Description field must be between 5 and 500 characters.")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, 1000000, ErrorMessage = "The Price field must be between 0.01 and 1 000 000.")]
        public decimal Price { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }
    }
}
