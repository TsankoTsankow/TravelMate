using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Post;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnv;

        public PostService(ApplicationDbContext _context,
            IWebHostEnvironment _hostingEnv)
        {
            this.context = _context;
            this.hostingEnv = _hostingEnv;
        }
                

        public async Task CreatePost(CreatePostViewModel model, string userId)
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
                PhotoUrl = uploadPhoto(model.File)
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(u => u.Id == postId);

            post.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task Edit(EditPostViewModel model, int postId)
        {
            var post = await context.Posts
                .FindAsync(postId);

            post.CategoryId = model.CategoryId;
            post.Content = model.Content;
            post.CountryId = model.CountryId;
            if (model.File != null)
            {
                post.PhotoUrl = uploadPhoto(model.File);
            }


            await context.SaveChangesAsync();
        }

        public async Task<PostsByCategoryViewModel> GetAllPostsByCategory(string? category = null)
        {
            var postQuery = context.Posts
                .Where(p => p.IsDeleted == false)
                .AsQueryable();

            var result = new PostsByCategoryViewModel();

            if (string.IsNullOrEmpty(category) == false)
            {
                postQuery = postQuery
                    .Where(p => p.PostCategory.Name == category);
            }

            result.Posts = await postQuery
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

        public async Task<PostsByCountryViewModel> GetAllPostsByCountry(string? country = null)
        {
            var postQuery = context.Posts
                .Where(p => p.IsDeleted == false)
                .AsQueryable();

            var result = new PostsByCountryViewModel();

            if (string.IsNullOrEmpty(country) == false)
            {
                postQuery = postQuery
                    .Where(p => p.Country.Name == country);
            }

            result.Posts = await postQuery
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
               //.Where(p => p.IsDeleted == false)
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

        private string? uploadPhoto(IFormFile? photo)
        {
            var FileDir = "Images";

            string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDir);

            var photoName = Path.GetFileNameWithoutExtension(photo.FileName);

            var photoExtension = Path.GetExtension(photo.FileName);

            var fileName = photoName + DateTime.Now.ToString("MMddyyyyHHmmss") + photoExtension;

            var filePath = Path.Combine(FilePath, fileName);

            var photoUrl = Path.Combine(hostingEnv.WebRootPath, "\\Images\\", fileName);

            if (photo.Length <= 3145728)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
            }
            else
            {
                throw new ArgumentException("Picture is too big");
            }

            return photoUrl;
        }
    }
        
}

