using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFriendship>(b =>
            {
                b.HasKey(k => new {k.UserId, k.UserFriendId});

                b.HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.UserFriend)
                .WithMany()
                .HasForeignKey(x => x.UserFriendId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserProfilePicture> UserPhotos { get; set; }
    }
}