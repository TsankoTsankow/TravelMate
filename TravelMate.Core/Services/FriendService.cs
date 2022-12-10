using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.ApplicationUser;
using TravelMate.Core.Models.Post;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext context;

        public FriendService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<UserPostsViewModel>> GetAllFriends(string userId)
        {
            
            var friends = await context.UserFriendships
                .Where(u => u.UserId == userId)
                .Where(u => u.UserFriend.IsDeleted == false)
                .Select(f => new UserPostsViewModel()
                {
                    Id = f.UserFriendId,
                    Username = f.UserFriend.UserName,
                    Email = f.UserFriend.Email,
                    Posts = f.UserFriend.Posts
                    .Where(p => p.IsDeleted == false)
                    .Select(p => new PostViewModel()
                    {
                        Id = p.Id,
                        AuthorName = p.Author.UserName,
                        AuthorId = p.AuthorId,
                        PostTime = p.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                        Content = p.Content,
                        Likes = p.Likes.Count(),
                        Comments = p.Comments.Count(),
                        Category = p.PostCategory.Name,
                        PhotoUrl = p.PhotoUrl,
                        Country = p.Country.Name
                    })
                        .ToList()
                })
                .ToListAsync();

            return friends; 
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
