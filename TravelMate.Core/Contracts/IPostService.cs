using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Contracts
{
    public interface IPostService
    {
        Task CreatePost(CreatePostViewModel post, string userId);
        Task<IEnumerable<PostViewModel>> GetAllPostsByUserId(string userId);

        Task Edit(EditPostViewModel model, int postId);

        Task<EditPostViewModel> GetPostById(int postId);

        Task Delete(int postId);

        Task<PostViewModel> GetPostInfoByPostId(int postId);

        Task<IEnumerable<PostViewModel>> GetAllPostsByCategoryId(int categoryId);

        Task<PostsByCategoryViewModel> GetAllPostsByCategory(string? category = null);
    }
}
