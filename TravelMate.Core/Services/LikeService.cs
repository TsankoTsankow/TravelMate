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


        /// <summary>
        /// Creates a new Like entity and adds it to the collection of the user who liked the post 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if the user, who is trying to add a like to a certain post, has already liked it
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns>True or false</returns>
        public async Task<bool> UserLikedPost(int postId, string userId)
        {
            return await context.Likes
                .AnyAsync(l => l.UserId == userId && l.PostId == postId);
                        
        }
    }
}
