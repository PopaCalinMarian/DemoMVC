using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string? LoginName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        //public string ?UserType { get; set; } //making it nullable
        // Navigation property
        public UserProfile UserProfile { get; set; }
        //public ArtistProfile ArtistProfile { get; set; }
        //public CustomerProfile CustomerProfile { get; set; }
    }
}
