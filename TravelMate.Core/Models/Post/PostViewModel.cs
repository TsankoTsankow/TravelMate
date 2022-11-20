using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Models.Photo;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Models.Post
{
    public class PostViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PostTime { get; set; }


        public string? Content { get; set; }


        public List<PhotoViewModel>? Photos { get; set; } = new List<PhotoViewModel>();

        [FromForm]

        public IFormFileCollection? Files { get; set; }
    }
}
