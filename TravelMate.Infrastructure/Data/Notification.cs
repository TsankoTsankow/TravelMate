using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Infrastructure.Data
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NotificationTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(NotificationTypeId))]
        public NotificationType NotificationType { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public bool IsRead { get; set; } = false;
    }
}
