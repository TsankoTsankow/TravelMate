using TravelMate.Core.Models.Photo;
using TravelMate.Core.Models.Post;

namespace TravelMate.Core.Contracts
{
    public interface IPostService
    {
        Task CreatePost(PostViewModel post, string userId);
        Task<IEnumerable<PostViewModel>> GetAllPosts(string userId);
        Task AddPhotoToFolder(AddPhotoViewModel photo, string userId);
        Task<IEnumerable<DisplayPhotoViewModel>> DisplayUserGallery(string userId);
    }
}
