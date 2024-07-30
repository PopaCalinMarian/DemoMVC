namespace DemoMVC.Models
{
    public class ArtistProfile
    {
        public int ArtistProfileID { get; set; }
        public int UserID { get; set; }
        public string Bio { get; set; }
        public string PortfolioUrl { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
