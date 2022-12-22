using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Comments;
using TravelMate.Core.Models.Post;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext context;

        public CommentService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public async Task<PostCommentsViewModel> GetPostComments(PostViewModel post)
        {
            var model = await context.Posts
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == post.Id)
                .Select(p => new PostCommentsViewModel()
                {
                    Post = post, 
                    Comments = p.Comments
                    .Where(c => c.IsDeleted == false)
                    .Select(c => new CommentViewModel()
                    {
                        Id = c.Id,
                        Content = c.Content,
                        PostId = post.Id,
                        User = c.User,
                        CreatedOn = c.CreatedOn,
                        IsDeleted = c.IsDeleted
                    })
                    .ToList()
                })
                .FirstAsync();

            return model;
        }
    }
}
