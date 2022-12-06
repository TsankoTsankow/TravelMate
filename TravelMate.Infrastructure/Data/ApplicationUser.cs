using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(UserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(UserLastNameMaxLength)]
        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(UserInformationMaxLength)]
        public string? Information { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<UserFriendship> Friends { get; set; } = new List<UserFriendship>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
