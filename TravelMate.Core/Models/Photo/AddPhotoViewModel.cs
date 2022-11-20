using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Core.Models.Photo
{
    public class AddPhotoViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        //[Required]
        //public string Type{ get; set; } = null!;

        //[Required]
        //public string  Extension { get; set; } = null!;

        //[Required]
        //public string UserId { get; set; } = null!;

        [Required]
        public IFormFile Photo { get; set; } = null!;
    }
}
