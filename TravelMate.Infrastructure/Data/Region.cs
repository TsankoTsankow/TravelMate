using System.ComponentModel.DataAnnotations;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Region
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(RegionNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(RegionDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public IEnumerable<Country> Counries { get; set; } = new HashSet<Country>();
    }
}
