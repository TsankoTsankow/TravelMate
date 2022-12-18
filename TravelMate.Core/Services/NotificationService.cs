using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Notifications;
using TravelMate.Infrastructure.Data;
using TravelMate.Infrastructure.Data.Enums;

namespace TravelMate.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext context;

        public NotificationService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        
        public async Task<IEnumerable<NotificationViewModel>> GetNotificationsByUserId(string id)
        {
            var result = await context.Notifications
                .Where(n => n.UserId == id)
                .OrderByDescending(n => n.Id)
                .Select(n => new NotificationViewModel()
                {
                    Id = n.Id,
                    Description = n.Description,
                    UserId = n.UserId,
                    NotificationType = n.NotificationType,
                    SenderId = n.SenderId,
                })
                .ToListAsync();

            return result;
        }

        public async Task SendFriendRequest(string userId, string friendId)
        {
            var user = await GetUserById(userId);

            var friend = await GetUserById(friendId);

            var notification = new Notification()
            {
                NotificationType = NotificationType.FriendRequest,
                Description = $"{user.UserName} wants to add you as a friend",
                UserId = friendId,
                User = friend,
                SenderId = userId,
                IsRead = false
            };

            friend.Notifications.Add(notification);
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();

        }

        public async Task SendLikeNotification(int postId, string senderId)
        {
            var post = await context.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                throw new ArgumentException("Invalid post Id");
            }

            var sender = await GetUserById(senderId);

            var notification = new Notification()
            {
                NotificationType = NotificationType.PostLike,
                Description = $"{sender.UserName} liked your post from {post.CreatedOn.ToString("dd/MM/yyyy HH:mm")}",
                UserId = post.AuthorId,
                User = post.Author,
                SenderId = senderId,
                IsRead = false
            };

            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        private async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await context.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            return user;
        }
        
    }
}
