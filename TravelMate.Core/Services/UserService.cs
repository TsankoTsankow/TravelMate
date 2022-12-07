using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.ApplicationUser;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext _context)
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

        public async Task SendRequest(string userId, string friendId)
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
                
                //NotificationType = "Friend request",
                Description = $"{user.UserName} wants to add you as a friend",
                UserId = friendId,
                User = friend,
                SenderId = userId,
                IsRead = false
            };

            
        }
    }
}

//[Key]
//public int Id { get; set; }

//[Required]
//public int NotificationTypeId { get; set; }

//[Required]
//[ForeignKey(nameof(NotificationTypeId))]
//public NotificationType NotificationType { get; set; } = null!;

//[Required]
//public string Description { get; set; } = null!;

//public string UserId { get; set; } = null!;

//[ForeignKey(nameof(UserId))]
//public ApplicationUser User { get; set; } = null!;

//[Required]
//public string SenderId { get; set; } = null!;

//[Required]
//public bool IsRead { get; set; } = false;