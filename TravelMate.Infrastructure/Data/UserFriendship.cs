using System.ComponentModel.DataAnnotations;

namespace TravelMate.Infrastructure.Data
{
    public class UserFriendship
    {
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public string UserFriendId { get; set; } = null!;
        [Required]
        public ApplicationUser UserFriend { get; set; } = null!;
    }
}
