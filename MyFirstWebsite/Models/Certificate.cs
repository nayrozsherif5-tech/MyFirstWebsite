using System.ComponentModel.DataAnnotations;

namespace MyFirstWebsite.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Organization { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime Date { get; set; }

        public string? ImagePath { get; set; }

        public string? CertificateLink { get; set; }
    }
}