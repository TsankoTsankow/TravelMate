﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [MaxLength(PostContentMaxLength)]
        public string? Content { get; set; }

        [Required]
        public string AuthorId { get; set; } = null!;

        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;

        public List<PostPhoto> Photos { get; set; } = new List<PostPhoto>();

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category PostCategory { get; set; } = null!;

        [Required]
        public int CountryId { get; set; }

        [Required]
        public Country Country { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Like> Likes { get; set; } = new List<Like>();


    }
}
