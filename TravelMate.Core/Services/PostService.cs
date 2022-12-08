using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Photo;
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

        public async Task Edit(CreatePostViewModel model, int postId)
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

        public async Task<IEnumerable<PostViewModel>> GetAllPostsByUserId(string userId)
        {
            var posts = await context.Posts
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

        public async Task<CreatePostViewModel> GetPostById(int postId)
        {
            return await context.Posts
                .Where(p => p.Id == postId)
                .Select(p => new CreatePostViewModel()
                {
                    Id=p.Id,
                    CategoryId = p.CategoryId,
                    CountryId = p.CountryId,
                    Content = p.Content,
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



    //public async Task<IEnumerable<DisplayPhotoViewModel>> DisplayUserGallery(string userId)
    //{
    //    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    //    var gallery = await context.PostPhotos.Include(p => p.Post)
    //        .Where(u => u.Post.AuthorId == userId)
    //        .Select(up => new DisplayPhotoViewModel()
    //        {
    //            Id = up.Id,
    //            DateAdded = up.DateAdded.ToString("dd/MM/yyyy"),
    //            ImageUrl = up.PhotoUrl,
    //        })
    //        .ToListAsync();

    //    return gallery;
    //}

    //public async Task<IEnumerable<PostViewModel>> GetAllPosts(string UserId)
    //{
    //    var posts = await context.Posts
    //        .Where(posts => posts.AuthorId == UserId)
    //        .OrderByDescending(p => p.CreatedOn)
    //        .Select(post => new PostViewModel()
    //        {
    //            Id = post.Id,
    //            PostTime = post.CreatedOn,
    //            Content = post.Content,
    //            Photos = post.Photos.Select(p => new PhotoViewModel()
    //            {
    //                Id = p.Id,

    //            })
    //            .ToList()
    //        })
    //        .ToListAsync();

    //    return posts;
    //}
}

