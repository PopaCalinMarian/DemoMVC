using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login Name")]
        public string? LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
