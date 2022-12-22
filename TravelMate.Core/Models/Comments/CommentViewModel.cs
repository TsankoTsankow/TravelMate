using System.ComponentModel.DataAnnotations;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;


namespace TravelMate.Core.Models.Comments
{
    public class CommentViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        public int PostId { get; set; }

        [Required]
        public Infrastructure.Data.ApplicationUser User { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
