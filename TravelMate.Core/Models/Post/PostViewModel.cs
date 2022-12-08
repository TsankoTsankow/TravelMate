using System.ComponentModel.DataAnnotations;

namespace TravelMate.Core.Models.Post
{
    public class PostViewModel
    {
        [Key]
        public int Id { get; set; }

        public string AuthorName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;

        public string PostTime { get; set; } = null!;

        public string? Content { get; set; }

        public string Category { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string? PhotoUrl { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }
    }
}
