using TravelMate.Core.Models.ApplicationUser;
using TravelMate.Core.Models.Notifications;

namespace TravelMate.Core.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<UserPostsViewModel>> GetAllUsers();
        Task AddFriend(string userId, string friendId);
        Task SendFriendRequest(string userId, string friendId);
        Task<bool> UsersAreFriends(string userId, string friendId);
        Task<IEnumerable<NotificationViewModel>> GetNotificationsByUserId(string id);
    }
}
