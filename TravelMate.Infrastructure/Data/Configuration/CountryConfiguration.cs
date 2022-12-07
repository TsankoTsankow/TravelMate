using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMate.Infrastructure.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<Country> SeedUsers()
        {
            var countries = new List<Country>();

            var country = new Country()
            {
                Id = 1,
                Name = "Bulgaria",
                Description = "The best place to live in. Lots of mountains, beautiful seaside, lovely food and amazing people.",
                RegionId = 1
            };

            countries.Add(country);

            country = new Country()
            {
                Id = 2,
                Name = "Greece",
                Description = "History, culture, fine dining, crystal-clear water, endless beaches - you can find all that here.",
                RegionId = 1
            };

            countries.Add(country);

            country = new Country()
            {
                Id = 3,
                Name = "USA",
                Description = "The country of endless possibilities, what you can think of - you can find it here",
                RegionId = 2
            };

            countries.Add(country);

            return countries;
        }
    }
}
