using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Infrastructure.Data
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

    }
}
