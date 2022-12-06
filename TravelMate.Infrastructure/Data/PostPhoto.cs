using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Infrastructure.Data
{
    public class PostPhoto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public string PhotoUrl { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

    }
}
