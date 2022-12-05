using System.ComponentModel.DataAnnotations;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(CategoryDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public IEnumerable<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
