using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string? LoginName { get; set; }

        [Required]
        [StringLength(200)]
        public string? Password { get; set; }

        /*
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? UserType { get; set; }

        public string? Bio { get; set; }
        public string? PortfolioUrl { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        */
    }
}
