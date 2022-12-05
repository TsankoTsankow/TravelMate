﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Infrastructure.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
                
        public string? Content { get; set; }

        [Required]
        public string AuthorId { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;

        public List<Photo>? Photos { get; set; } = new List<Photo>();

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category PostCategory { get; set; } = null!;

        [Required]
        public int CountryId { get; set; }

        [Required]
        public Country Country { get; set; } = null!;

        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

        public ICollection<Like>? Likes { get; set; } = new HashSet<Like>();


    }
}
