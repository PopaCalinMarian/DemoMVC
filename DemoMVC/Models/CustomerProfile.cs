namespace DemoMVC.Models
{
    public class CustomerProfile
    {
        public int CustomerProfileID { get; set; }
        public int UserID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
