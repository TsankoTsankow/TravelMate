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

        public async Task Add(AddCommentViewModel model, string userId)
        {
            var author = await context.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id ==userId);

            if (author == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var post = await context.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (post == null)
            {
                throw new ArgumentException("Invalid post Id");
            }

            var comment = new Comment()
            {
                Content = model.Content,
                PostId = model.Id,
                Post = post,
                UserId = userId,
                User = author,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };

            post.Comments.Add(comment);
            author.Comments.Add(comment);
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
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
