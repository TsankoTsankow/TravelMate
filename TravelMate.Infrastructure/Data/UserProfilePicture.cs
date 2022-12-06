using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Infrastructure.Data
{
    public class UserProfilePicture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
                
        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public string PhotoUrl { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

    }
}
