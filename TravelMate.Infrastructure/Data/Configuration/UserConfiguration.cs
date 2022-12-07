using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMate.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<ApplicationUser> SeedUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Stamat",
                NormalizedUserName = "stamat",
                Email = "stamat@mail.com",
                NormalizedEmail = "stamat@mail.com",
                FirstName = "Stamat",
                LastName = "Petrov",
                CountryId = 1,
                Information = "I love to travel in the mountains of Europe", 
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://i.pinimg.com/originals/d9/56/9b/d9569bbed4393e2ceb1af7ba64fdf86a.jpg"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "stamat123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "Gosho",
                NormalizedUserName = "gosho",
                Email = "gosho@mail.com",
                NormalizedEmail = "gosho@mail.com",
                FirstName = "Gosho",
                LastName = "Ivanov",
                CountryId = 2,
                Information = "I am looking forward to visiting all the oceans of the world!",
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://thumbs.dreamstime.com/b/profile-picture-vector-perfect-social-media-other-web-use-125320944.jpg"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "gosho123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "0dfd8f1e-fba5-4db7-82a7-0ba2e5cc6a09",
                UserName = "Stanka",
                NormalizedUserName = "stanka",
                Email = "stanka@mail.com",
                NormalizedEmail = "stanka@mail.com",
                FirstName = "Stanka",
                LastName = "Petrova",
                CountryId = 3,
                Information = "I love to travel to all the beautiful cities in the world and meet all the great people there!",
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://pub-static.fotor.com/assets/projects/pages/d5bdd0513a0740a8a38752dbc32586d0/fotor-03d1a91a0cec4542927f53c87e0599f6.jpg"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "stanka123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "9717154f-b1cc-44e4-a23b-7dcfdea11eb1",
                UserName = "Admin",
                NormalizedUserName = "admin",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
                FirstName = "Ivan",
                LastName = "Ivanov",
                CountryId = 1,
                Information = "I want to create the best plarform for travellers in the world!",
                BirthDate = DateTime.Now,
                ProfilePictureUrl = "https://thumbs.dreamstime.com/b/profile-picture-vector-perfect-social-media-other-web-use-125320944.jpg"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "admin123");

            users.Add(user);

            return users;
        }
    }
}
