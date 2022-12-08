using TravelMate.Core.Models.Photo;
using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Contracts
{
    public interface IPostService
    {
        Task CreatePost(CreatePostViewModel post, string userId);
        Task<IEnumerable<PostViewModel>> GetAllPostsByUserId(string userId);

        Task Edit(CreatePostViewModel model, int postId);

        Task<CreatePostViewModel> GetPostById(int postId);
    }
}
