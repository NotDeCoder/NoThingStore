using Slugify;
using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public class EBook : Product
    {
        [Required(ErrorMessage = "The MegabyteSize field is required.")]
        [Range(1, uint.MaxValue, ErrorMessage = "The MegabyteSize field must be a positive integer.")]
        public uint MegabyteSize { get; set; }

        [Required(ErrorMessage = "The Authorship field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Authorship field must be between 3 and 100 characters.")]
        public string Authorship { get; set; }

        [Required(ErrorMessage = "The Format field is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The Format field must be between 3 and 10 characters.")]
        public string Format { get; set; }

        [Required(ErrorMessage = "The CountOfPages field is required.")]
        [Range(1, uint.MaxValue, ErrorMessage = "The CountOfPages field must be a positive integer.")]
        public uint CountOfPages { get; set; }

        [Url(ErrorMessage = "The DownloadUrl field must be a valid URL.")]
        public string DownloadUrl { get; set; }

        public override EBook GenerateSlug()
        {
            SlugHelper helper = new SlugHelper();
            Slug = helper.GenerateSlug(Name.Trim().ToLower());
            return this;
        }
    }
}
