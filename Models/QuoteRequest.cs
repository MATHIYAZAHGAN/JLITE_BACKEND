using System.ComponentModel.DataAnnotations;

namespace JLITE.API.Models
{
    public class QuoteRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Company { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectType { get; set; } = string.Empty;

        [StringLength(50)]
        public string? SiteType { get; set; }

        [StringLength(50)]
        public string? Quantity { get; set; }

        [StringLength(50)]
        public string? Budget { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(100)]
        public string? Timeline { get; set; }

        [StringLength(50)]
        public string? Urgency { get; set; }

        [StringLength(2000)]
        public string? Details { get; set; }
    }
}
