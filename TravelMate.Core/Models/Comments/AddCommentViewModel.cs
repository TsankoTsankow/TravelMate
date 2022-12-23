using System.ComponentModel.DataAnnotations;
using static TravelMate.Infrastructure.Data.Constants.EntityConstants;

namespace TravelMate.Core.Models.Comments
{
    public class AddCommentViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; } = null!;
        
    }
}
