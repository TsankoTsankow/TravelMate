using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public string AuthorId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
