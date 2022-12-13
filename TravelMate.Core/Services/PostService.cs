using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Post;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IProfileService profileService;
        private readonly IFriendService friendService;

        public PostService(
            ApplicationDbContext _context,
            IProfileService _profileService,
            IFriendService _friendService)
        {
            this.context = _context;
            this.profileService = _profileService;
            this.friendService = _friendService;
        }
                

        public async Task CreatePost(CreatePostViewModel model, string userId, string? url)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new ArgumentException("No such user");
            }

            var post = new Post()
            {
                CreatedOn = DateTime.Now,
                Content = model.Content,
                AuthorId = userId,
                Author = user,
                CategoryId = model.CategoryId,
                CountryId = model.CountryId,
            };

            if (!string.IsNullOrEmpty(url))
            {
                post.PhotoUrl = url;
            }

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int postId)
        {
            var post = await context.Posts
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == postId);

            post.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task Edit(EditPostViewModel model, int postId, string? url)
        {
            var post = await context.Posts
                .FindAsync(postId);

            post.CategoryId = model.CategoryId;
            post.Content = model.Content;
            post.CountryId = model.CountryId;
            if (!string.IsNullOrEmpty(url))
            {
                post.PhotoUrl = url;
            }


            await context.SaveChangesAsync();
        }

        public async Task<AllPostsQueryModel> GetAllPostsQuery(string? category = null, string? country = null)
        {
            var postQuery = context.Posts
                .Where(p => p.IsDeleted == false)
                .AsQueryable();

            var result = new AllPostsQueryModel();

            if (string.IsNullOrEmpty(category) == false)
            {
                postQuery = postQuery
                    .Where(p => p.PostCategory.Name == category);
            }

            if (string.IsNullOrEmpty(country) == false)
            {
                postQuery = postQuery
                    .Where(p => p.Country.Name == country);
            }

            result.Posts = await postQuery
                .OrderByDescending(p => p.CreatedOn)
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
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsByCategoryId(int categoryId)
        {
            var posts = await context.Posts
                .Where(p => p.CategoryId == categoryId)
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
                .ToListAsync();

            return posts;
        }


        public async Task<IEnumerable<PostViewModel>> GetAllPostsByUserId(string userId)
        {
            var posts = await context.Posts
                .Where(p => p.IsDeleted == false)
                .Where(p => p.AuthorId == userId)
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
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPostsOfUserFriends(string id)
        {
            var userFriends = await friendService.GetAllFriends(id);

            var result = new List<PostViewModel>();

            foreach (var user in userFriends)
            {
                foreach (var post in user.Posts)
                {
                    result.Add(post);
                }
            }

            result = result.OrderByDescending(p => p.PostTime).ToList();

            return result;
        }

        public async Task<EditPostViewModel> GetPostById(int postId)
        {
            return await context.Posts
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == postId)
                .Select(p => new EditPostViewModel()
                {
                    Id=p.Id,
                    CategoryId = p.CategoryId,
                    CountryId = p.CountryId,
                    Content = p.Content,
                    AuthorId = p.AuthorId,
                })
                .FirstAsync();
        }

        public async Task<PostViewModel> GetPostInfoByPostId(int postId)
        {
            return await context.Posts
               .Where(p => p.Id == postId)
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
               .FirstAsync();

            
        }
    }
}

