using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoThingStore.Models
{
    public class ActivationKey : Product
    {
        [Required(ErrorMessage = "The Key field is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The Key field must be between 4 and 100 characters.")]
        public string Key { get; set; }

        [Required(ErrorMessage = "The TargetProgramName field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The TargetProgramName field must be between 3 and 50 characters.")]
        public string TargetProgramName { get; set; }

        [Required(ErrorMessage = "The ProgramVersion field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The ProgramVersion field must be between 3 and 20 characters.")]
        public string ProgramVersion { get; set; }

        [Required(ErrorMessage = "The ExpirationDate field is required.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The HowToActivate field is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "The HowToActivate field must be between 10 and 500 characters.")]
        public string HowToActivate { get; set; }
    }
}