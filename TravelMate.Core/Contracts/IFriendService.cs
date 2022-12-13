using TravelMate.Core.Models.ApplicationUser;

namespace TravelMate.Core.Contracts
{
    public interface IFriendService
    {
        Task<IEnumerable<UserPostsViewModel>> GetAllFriends(string userId);
        Task AddFriend(string userId, string friendId);
        Task<bool> UsersAreFriends(string userId, string friendId);
    }
}
