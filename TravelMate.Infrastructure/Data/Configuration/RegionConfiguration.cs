using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMate.Infrastructure.Data.Configuration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(SeedRegions());
        }

        private List<Region> SeedRegions()
        {
            var regions = new List<Region>();

            var region = new Region()
            {
                Id = 1,
                Name = "Europe",
                Description = "The old continent - the place for culture, history, cuisine and so much more.",
            };

            regions.Add(region);

            region = new Region()
            {
                Id = 2,
                Name = "North America",
                Description = "The land of diversity - nature, technology and so much more",
            };

            regions.Add(region);

            return regions;
        }
    }
}
