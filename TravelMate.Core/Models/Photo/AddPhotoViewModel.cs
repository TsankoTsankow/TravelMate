using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TravelMate.Core.Models.Photo
{
    public class AddPhotoViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        
        [Required]
        public IFormFile Photo { get; set; } = null!;
    }
}
