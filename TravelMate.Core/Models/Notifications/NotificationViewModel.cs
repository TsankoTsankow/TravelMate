using System.ComponentModel.DataAnnotations;
using TravelMate.Infrastructure.Data.Enums;

namespace TravelMate.Core.Models.Notifications
{
    public class NotificationViewModel
    {
        [Key]
        public int Id { get; set; }

        public NotificationType NotificationType { get; set; }

        public string Description { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string SenderId { get; set; } = null!;

    }
}
