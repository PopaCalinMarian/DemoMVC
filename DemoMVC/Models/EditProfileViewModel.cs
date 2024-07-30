namespace DemoMVC.Models
{
    public class EditProfileViewModel
    {
        public int UserID { get; set; }

        public string Name { get; set; }
        public string UserType { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // For file upload
        public IFormFile ProfileImage { get; set; }
    }
}
