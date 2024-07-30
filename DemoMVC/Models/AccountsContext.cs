using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Models
{
    public class AccountsContext(DbContextOptions<AccountsContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ArtistProfile> ArtistProfiles { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserID);

            /*
            modelBuilder.Entity<User>()
                .HasOne(u => u.ArtistProfile)
                .WithOne(ap => ap.User)
                .HasForeignKey<ArtistProfile>(ap => ap.UserID);

            modelBuilder.Entity<User>()
                .HasOne(u => u.CustomerProfile)
                .WithOne(cp => cp.User)
                .HasForeignKey<CustomerProfile>(cp => cp.UserID);
            */
        }
    }
}
