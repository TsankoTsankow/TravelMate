using System.ComponentModel.DataAnnotations;
using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Models.ApplicationUser
{
    public class UserPostsViewModel
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public ICollection<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
