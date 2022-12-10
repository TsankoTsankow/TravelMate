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
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Friends)
                .ThenInclude(uf => uf.UserFriend)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Ivalid User ID");
            }

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
    }
}
