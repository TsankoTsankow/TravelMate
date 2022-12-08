using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelMate.Infrastructure.Data;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Core.Models.Post
{
    public class CreatePostViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(PostContentMaxLength)]
        public string? Content { get; set; }

        public List<PostPhoto> Photos { get; set; } = new List<PostPhoto>();

        [Required]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public int CountryId { get; set; }

        [Required]
        public List<Country> Countries { get; set; } = new List<Country>();

    }
}

