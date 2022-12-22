using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Models.Comments
{
    public class PostCommentsViewModel
    {
        public PostViewModel Post { get; set; } = null!;

        public IEnumerable<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
