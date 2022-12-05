using System.ComponentModel.DataAnnotations;

namespace TravelMate.Infrastructure.Data
{
    public class Region
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; } = null!;

        public IEnumerable<Country> Counries { get; set; } = new HashSet<Country>();
    }
}
