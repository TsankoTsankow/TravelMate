using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        //public ICollection<UserFriendship> FriendOf { get; set; } = new List<UserFriendship>();
        public ICollection<UserFriendship> Friends { get; set; } = new List<UserFriendship>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
