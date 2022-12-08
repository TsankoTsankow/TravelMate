using TravelMate.Core.Models.Photo;
using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Contracts
{
    public interface IPostService
    {
        Task CreatePost(CreatePostViewModel post, string userId);
        Task<IEnumerable<PostViewModel>> GetAllPostsById(string userId);
        Task<IEnumerable<DisplayPhotoViewModel>> DisplayUserGallery(string userId);
    }
}
