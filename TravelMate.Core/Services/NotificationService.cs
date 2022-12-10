using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.ApplicationUser;
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

        public async Task AddFriend(string userId, string friendId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Friends)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var friend = await context.Users
                .Where(u => u.Id == friendId)
                .Include(u => u.Friends)
                .FirstOrDefaultAsync();

            if (friend == null)
            {
                throw new ArgumentException("Invalid friend Id");
            }

            
            var userFriend = new UserFriendship()
            {
                UserId = userId,
                User = user,
                UserFriendId = friendId,
                UserFriend = friend
            };

            var friendUser = new UserFriendship()
            {
                UserId = friendId,
                User = friend,
                UserFriendId = userId,
                UserFriend = user
            };

            user.Friends.Add(userFriend);
            friend.Friends.Add(friendUser);
            await context.SaveChangesAsync();
        }


        public async Task<IEnumerable<UserPostsViewModel>> GetAllUsers()
        {
            var users = await context.Users.Select(u => new UserPostsViewModel()
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email
            }).ToListAsync();

            return users;
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
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var friend = await context.Users
                .FirstOrDefaultAsync(u => u.Id == friendId);

            if (friend == null)
            {
                throw new ArgumentException("Invalid friend Id");
            }

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
                .Where(p => p.Id == postId)
                .Where(p => p.IsDeleted == false)
                .FirstAsync();

            var sender = await context.Users
                .Where(p => p.Id == senderId)
                .Where(p => p.IsDeleted == false)
                .FirstAsync();

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
        public async Task<bool> UsersAreFriends(string userId, string friendId)
        {
            var userFriendship = await context.UserFriendships
                .Where(uf => uf.UserId == userId && uf.UserFriendId == friendId)
                .FirstOrDefaultAsync();

            if (userFriendship is null)
            {
                return false;
            }

            return true;
        }
    }
}
