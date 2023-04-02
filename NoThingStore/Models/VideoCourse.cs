using Slugify;
using System.ComponentModel.DataAnnotations;

namespace NoThingStore.Models
{
    public class VideoCourse : Product
    {
        [Required(ErrorMessage = "The CountOfVideos field is required.")]
        [Range(1, uint.MaxValue, ErrorMessage = "The CountOfVideos field must be a positive integer.")]
        public uint CountOfVideos { get; set; }
        
        [Required(ErrorMessage = "The Format field is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The Format field must be between 3 and 20 characters.")]
        public string Format { get; set; }

        [Required(ErrorMessage = "The Duration field is required.")]
        [Range(1, uint.MaxValue, ErrorMessage = "The Duration field must be a positive integer.")]
        public uint Duration { get; set; }

        [Required(ErrorMessage = "The Language field is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The Language field must be between 3 and 20 characters.")]
        public string Language { get; set; }

        [Url(ErrorMessage = "The DownloadUrl field must be a valid URL.")]
        public string DownloadUrl { get; set; }

        public override VideoCourse GenerateSlug()
        {
            SlugHelper helper = new SlugHelper();
            Slug = helper.GenerateSlug(Name.Trim().ToLower());
            return this;
        }
    }

}
