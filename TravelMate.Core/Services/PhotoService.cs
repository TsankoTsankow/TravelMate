using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TravelMate.Core.Contracts;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IWebHostEnvironment hostingEnv;

        public PhotoService(IWebHostEnvironment _hostingEnv)
        {
            this.hostingEnv = _hostingEnv;
        }

        public async Task<string> UploadPhoto(IFormFile? photo)
        {
            string photoUrl = "";

            if (photo != null)
            {
                var FileDir = "Images";

                string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDir);

                var photoName = Path.GetFileNameWithoutExtension(photo.FileName);

                var photoExtension = Path.GetExtension(photo.FileName);

                var fileName = photoName + DateTime.Now.ToString("MMddyyyyHHmmss") + photoExtension;

                var filePath = Path.Combine(FilePath, fileName);

                photoUrl = Path.Combine(hostingEnv.WebRootPath, "\\Images\\", fileName);

                if (photo.Length <= 3145728)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                }
                else
                {
                    throw new ArgumentException("Picture is too big");
                }
            }

            return photoUrl;
        }
    }
}
