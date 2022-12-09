using Microsoft.EntityFrameworkCore;
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

            if (user.Friends != null && user.Friends.Any(uf => uf.UserFriendId == friendId))
            {
                return;
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

        public async Task<IEnumerable<UserViewModel>> GetAllFriends(string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Friends)
                .ThenInclude(uf => uf.UserFriend)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Ivalid User ID");
            }

            return user.Friends.Select(u => new UserViewModel()
            {
                Id = u.UserFriendId,
                Username = u.UserFriend.UserName,
                Email = u.UserFriend.Email
            });

        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = await context.Users.Select(u => new UserViewModel()
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
