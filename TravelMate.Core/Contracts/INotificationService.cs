using TravelMate.Core.Models.ApplicationUser;
using TravelMate.Core.Models.Notifications;

namespace TravelMate.Core.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<UserPostsViewModel>> GetAllUsers();
        Task SendFriendRequest(string userId, string friendId);
        Task<IEnumerable<NotificationViewModel>> GetNotificationsByUserId(string id);
        Task SendLikeNotification(int postId, string senderId);
    }
}
