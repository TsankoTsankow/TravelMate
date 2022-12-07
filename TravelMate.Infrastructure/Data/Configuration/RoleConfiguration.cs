using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMate.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(SeedRoles());
        }

        private List<IdentityRole> SeedRoles()
        {
            var roles = new List<IdentityRole>();

            var role = new IdentityRole()
            {
                Id = "1bd9abc0-c646-4848-b216-3685875a303b",
                Name = "Admin", 
                NormalizedName = "admin",
                ConcurrencyStamp = "300b2afd-b7ea-413d-aee4-20f444159418"
            };

            roles.Add(role);

            role = new IdentityRole()
            {
                Id = "e571299a-b482-4d73-becc-33e93c51ad05",
                Name = "TravelGuru",
                NormalizedName = "travelguru",
                ConcurrencyStamp = "96026e09-f269-4065-8f54-136785dab507"
            };

            roles.Add(role);


            return roles;
        }
    }
}
