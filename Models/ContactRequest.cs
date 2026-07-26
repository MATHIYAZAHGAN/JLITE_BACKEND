using System.ComponentModel.DataAnnotations;

namespace JLITE.API.Models
{
    public class ContactRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;
    }
}
