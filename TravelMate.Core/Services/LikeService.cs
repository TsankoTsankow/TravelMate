using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext context;

        public LikeService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddLike(int postId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .FirstAsync();
            
            var like = new Like()
            {
                PostId = postId,
                UserId = userId
            };

            user.Likes.Add(like);
            await context.Likes.AddAsync(like);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UserLikedPost(int postId, string userId)
        {
            return await context.Likes
                .AnyAsync(l => l.UserId == userId && l.PostId == postId);
                        
        }
    }
}
