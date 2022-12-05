using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Infrastructure.Data.Enums;

namespace TravelMate.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Information { get; set; }

        public UserProfilePicture ProfilePicture { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();

        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

        public ICollection<Post> Posts { get; set; } = new List<Post>();


        public ICollection<UserFriendship> Friends { get; set; } = new List<UserFriendship>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
