using System.ComponentModel.DataAnnotations;

namespace TravelMate.Infrastructure.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public IEnumerable<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
