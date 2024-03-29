﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Infrastructure.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public int PostId { get; set; }

        [Required]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
