using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMate.Infrastructure.Data.Enums;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public NotificationType NotificationType { get; set; }

        [Required]
        [MaxLength(NotificationDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public bool IsRead { get; set; } = false;
    }
}
