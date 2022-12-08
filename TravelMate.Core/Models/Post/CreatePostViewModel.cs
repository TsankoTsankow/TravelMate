using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TravelMate.Core.Models.CategoryModels;
using TravelMate.Core.Models.CountryModels;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Core.Models.Post
{
    public class CreatePostViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(PostContentMaxLength)]
        public string? Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        [Required]
        public int CountryId { get; set; }

        [Required]
        public IEnumerable<CountryUserViewModel> Countries { get; set; } = new List<CountryUserViewModel>();

        public IFormFile? File { get; set; }

    }
}

