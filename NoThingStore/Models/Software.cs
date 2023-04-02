using Slugify;
using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public class Software : Product
    {
        [Required(ErrorMessage = "The MegabyteSize field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The MegabyteSize field must be a positive integer.")]
        public uint MegabyteSize { get; set; }

        [Required(ErrorMessage = "The HowToInstall field is required.")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "The HowToInstall field must be between 3 and 500 characters.")]
        public string HowToInstall { get; set; }

        [Required(ErrorMessage = "The SystemRequirement field is required.")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "The SystemRequirement field must be between 3 and 500 characters.")]
        public string SystemRequirement { get; set; }

        [Required(ErrorMessage = "The Authorship field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Authorship field must be between 3 and 100 characters.")]
        public string Authorship { get; set; }

        [Url(ErrorMessage = "The DownloadUrl field must be a valid URL.")]
        public string DownloadUrl { get; set; }

        public override Software GenerateSlug()
        {
            SlugHelper helper = new SlugHelper();
            Slug = helper.GenerateSlug(Name.Trim().ToLower());
            return this;
        }
    }

}
