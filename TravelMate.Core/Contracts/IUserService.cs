using TravelMate.Core.Models.ApplicationUser;

namespace TravelMate.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserPostsViewModel>> GetAllUsers();
        Task<IEnumerable<UserPostsViewModel>> GetAllFriends(string userId);

        Task AddFriend(string userId, string friendId);
        Task SendRequest(string userId, string friendId);
    }
}
