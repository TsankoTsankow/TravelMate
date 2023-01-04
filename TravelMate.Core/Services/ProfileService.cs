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

        public ProfileService(ApplicationDbContext _context)
        {
            this.context = _context;
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
                    BirthDate = u.BirthDate.HasValue ? u.BirthDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    Information = u.Information,
                    CountryId = u.CountryId,
                    Country = u.Country != null ? u.Country.Name : string.Empty,
                    ProfilePictureUrl = u.ProfilePictureUrl
                })
                .FirstAsync();


            return profile;
        }

        public async Task Edit(string userId, EditProfileViewModel model, string? url)
        {
            var user = await context.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("This user Id does not exist");
            }

            user.Information = model.Information;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.CountryId = model.CountryId;

            if (!string.IsNullOrEmpty(url))
            {
                user.ProfilePictureUrl = url;
            }

            if (model.BirthDate != null)
            {
                user.BirthDate = dateConvert(model.BirthDate);
            }

            await context.SaveChangesAsync();
        }

        private DateTime dateConvert(string date)
        {
            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format = "dd/MM/yyyy";
            try
            {
                result = DateTime.ParseExact(date, format, provider);
            }
            catch (Exception)
            {

                throw new FormatException("The date is not in the correct format");
            }

            return result;
        }
    }
}
