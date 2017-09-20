using System.ComponentModel.DataAnnotations;

namespace Blank.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 10)]
        public string Message { get; set; }
    }
}
