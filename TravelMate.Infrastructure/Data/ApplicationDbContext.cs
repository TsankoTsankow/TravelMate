﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelMate.Infrastructure.Data.Configuration;

namespace TravelMate.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private bool seedDb;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool seed = true)
            : base(options)
        {
            if (this.Database.IsRelational())
            {
                seed = true;
            }
            else
            {
                seed = false;
            }

            this.seedDb = seed;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new UserFriendshipConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());

            if (this.seedDb)
            {
                builder.ApplyConfiguration(new UserConfiguration());
                builder.ApplyConfiguration(new RoleConfiguration());
                builder.ApplyConfiguration(new UserRoleConfiguration());
                builder.ApplyConfiguration(new RegionConfiguration());
                builder.ApplyConfiguration(new CountryConfiguration());
                builder.ApplyConfiguration(new CategoryConfiguration());
            }

            base.OnModelCreating(builder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<UserFriendship> UserFriendships { get; set; }


    }
}