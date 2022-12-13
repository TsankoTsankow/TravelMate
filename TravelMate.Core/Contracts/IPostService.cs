using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Contracts
{
    public interface IPostService
    {
        Task CreatePost(CreatePostViewModel post, string userId, string? url);
        Task<IEnumerable<PostViewModel>> GetAllPostsByUserId(string userId);
        Task Edit(EditPostViewModel model, int postId, string? url);
        Task<EditPostViewModel> GetPostById(int postId);
        Task Delete(int postId);
        Task<PostViewModel> GetPostInfoByPostId(int postId);
        Task<IEnumerable<PostViewModel>> GetAllPostsByCategoryId(int categoryId);
        Task<AllPostsQueryModel> GetAllPostsQuery(string? category = null, string? country = null);
        Task<IEnumerable<PostViewModel>> GetAllPostsOfUserFriends(string id);
    }
}
