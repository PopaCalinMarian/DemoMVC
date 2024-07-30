namespace DemoMVC.Models
{
    public class UserProfile
    {
        public int UserProfileID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
    }
}
