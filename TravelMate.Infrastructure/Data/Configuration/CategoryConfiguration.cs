using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelMate.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private List<Category> SeedCategories()
        {
            var categories = new List<Category>();

            var category = new Category()
            {
                Id = 1,
                Name = "Nature",
                Description = "Here you can share all your experiences in the nature"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Culture",
                Description = "A place for all the cultural experiences"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Cuisine",
                Description = "For the lovers of fine dining"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Urban",
                Description = "The most beautiful and fascinating cities of the world",
            };

            categories.Add(category);

            return categories;
        }
    }
}
