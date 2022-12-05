using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(CountryDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public int RegionId { get; set; }

        [Required]
        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; } = null!;
    }
}
