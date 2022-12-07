using System.ComponentModel.DataAnnotations;

namespace TravelMate.Core.Models.ApplicationUser
{
    public class UserViewModel
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
