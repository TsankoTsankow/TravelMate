using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Photo;
using TravelMate.Core.Models.Post;
using TravelMate.Data;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnv;

        public PostService(ApplicationDbContext _context, IWebHostEnvironment _hostingEnv)
        {
            context = _context;
            hostingEnv = _hostingEnv;
        }

        public async Task AddPhotoToFolder(AddPhotoViewModel photo, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new Exception("no such user");
            }

            var FileDir = "Images";
            string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDir);

            var photoName = Path.GetFileNameWithoutExtension(photo.Photo.FileName);
            var photoExtension = Path.GetExtension(photo.Photo.FileName);
            var type = photo.Photo.ContentType;
            var fileName = photoName + DateTime.Now.ToString("MMddyyyyHHmmss") + photoExtension;

            var filePath = Path.Combine(FilePath, fileName);
            var photoUrl = Path.Combine(FileDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.Photo.CopyToAsync(stream);
            }

            var userPhoto = new UserPhoto()
            {
                Name = photoName,
                Type = type,
                Extension = photoExtension,
                UserId = userId,    
                User = user,
                PhotoUrl = photoUrl,
                DateAdded = DateTime.Now
            };

            await context.UserPhotos.AddAsync(userPhoto);
            await context.SaveChangesAsync();
        }

        public async Task CreatePost(PostViewModel model, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {

                var post = new Post()
                {
                    PostTime = DateTime.Now,
                    Content = model.Content,
                    UserId = userId,
                    User = user
                };

                List<Photo> photos = new List<Photo>();

                if (model.Files != null && model.Files.Count > 0)
                {
                    foreach (var formFile in model.Files)
                    {
                        if (formFile.Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await formFile.CopyToAsync(memoryStream);

                                var fileExtension = Path.GetExtension(formFile.FileName);
                                

                                var fileFormat = formFile.ContentType;

                                var fileName = Path.GetFileNameWithoutExtension(formFile.FileName) + DateTime.Now.ToString("MMddyyyyHHmm") + fileExtension;

                                var filePath = $"{hostingEnv.WebRootPath}\\Images\\{fileName}";

                                var path = Path.Combine(hostingEnv.WebRootPath, "\\Images\\", fileName);

                                if (fileExtension != ".img" && fileExtension != ".png" && fileExtension != ".jpg")
                                {
                                    throw new FormatException("Not a picture");
                                }

                                //Check this!
                                //using (var stream = new FileStream(path, FileMode.Create))
                                //{
                                //    await formFile.CopyToAsync(stream);
                                //}

                                if (formFile.Length <= 2097152)
                                {
                                    var newPhoto = new Photo()
                                    {
                                        Bytes = memoryStream.ToArray(),
                                        FileExtension = fileExtension
                                    };

                                    photos.Add(newPhoto);
                                }
                                else
                                {
                                    throw new ArgumentException("Picture is too big");
                                }
                            }
                        }
                    }
                }

                post.Photos = photos;

                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DisplayPhotoViewModel>> DisplayUserGallery(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var gallery = await context.UserPhotos
                .Where(u => u.User == user)
                .Select(up => new DisplayPhotoViewModel()
                {
                    Id = up.Id,
                    DateAdded = up.DateAdded.ToString("dd/MM/yyyy"),
                    ImageUrl = up.PhotoUrl,
                })
                .ToListAsync();

            return gallery;
        }

        public async Task<IEnumerable<PostViewModel>> GetAllPosts(string UserId)
        {
            var posts = await context.Posts
                .Where(posts => posts.UserId == UserId)
                .OrderByDescending(p => p.PostTime)
                .Select(post => new PostViewModel()
                {
                    Id = post.Id,
                    PostTime = post.PostTime,
                    Content = post.Content,
                    Photos = post.Photos.Select(p => new PhotoViewModel()
                    {
                        Id = p.Id,
                        Bytes = p.Bytes,
                        FileExtension = p.FileExtension
                    }).ToList()
                })
                .ToListAsync();

            return posts;
        }
    }
}
