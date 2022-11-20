using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
