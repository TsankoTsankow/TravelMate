using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMate.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(SeeUserRoles());
        }

        private List<IdentityUserRole<string>> SeeUserRoles()
        {
            var roles = new List<IdentityUserRole<string>>();

            var role = new IdentityUserRole<string> ()
            {
               RoleId = "1bd9abc0-c646-4848-b216-3685875a303b",
               UserId = "9717154f-b1cc-44e4-a23b-7dcfdea11eb1"
            };

            roles.Add(role);

            role = new IdentityUserRole<string>()
            {
                RoleId = "e571299a-b482-4d73-becc-33e93c51ad05",
                UserId = "0dfd8f1e-fba5-4db7-82a7-0ba2e5cc6a09"
            };

            roles.Add(role);


            return roles;
        }
    }
}
