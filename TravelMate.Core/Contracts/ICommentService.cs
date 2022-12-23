using TravelMate.Core.Models.Comments;
using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Contracts
{
    public interface ICommentService
    {
        Task<PostCommentsViewModel> GetPostComments(PostViewModel post);

        Task Add(AddCommentViewModel model, string userId);
    }
}
