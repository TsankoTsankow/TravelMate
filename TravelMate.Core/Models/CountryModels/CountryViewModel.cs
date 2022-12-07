using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMate.Infrastructure.Data;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Core.Models.CountryModels
{
    public class CountryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(CountryDescriptionMaxLength, MinimumLength = CountryDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public int RegionId { get; set; }

        [Required]
        public Region Region { get; set; } = null!;

    }
}

