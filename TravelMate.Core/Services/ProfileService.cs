using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Profile;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnv;

        public ProfileService(ApplicationDbContext _context,
            IWebHostEnvironment _hostingEnv)
        {
            this.context = _context;
            this.hostingEnv = _hostingEnv;
        }
        public async Task<PersonalProfileViewModel> DisplayProfileById(string Id)
        {
            var profile = await context.Users
                .Where(u => u.IsDeleted == false)
                .Where(u => u.Id == Id)
                .Select(u => new PersonalProfileViewModel()
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BirthDate = u.BirthDate.HasValue ? u.BirthDate.Value.ToString("dd/MM/yyyy") : String.Empty,
                    Information = u.Information,
                    CountryId = u.CountryId,
                    Country = u.Country,
                    ProfilePictureUrl = u.ProfilePictureUrl
                })
                .FirstAsync();


            return profile;
        }

        public async Task Edit(string userId, EditProfileViewModel model)
        {
            var user = await context.Users.FirstAsync(u => u.Id == userId);

            user.Information = model.Information;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.CountryId = model.CountryId;
            if (model.ProfilePicture != null)
            {
                user.ProfilePictureUrl = uploadPhoto(model.ProfilePicture);
            }

            if (model.BirthDate != null)
            {
                user.BirthDate = dateConvert(model.BirthDate);
            }

            await context.SaveChangesAsync();
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

        private DateTime dateConvert(string date)
        {
            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "d";

            result = DateTime.ParseExact(date, format, provider);

            return result;
        }
    }
}
