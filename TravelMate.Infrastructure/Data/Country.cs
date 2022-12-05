using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Infrastructure.Data
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(15000)]
        public string Description { get; set; } = null!;

        [Required]
        public int RegionId { get; set; }

        [Required]
        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; } = null!;
    }
}
