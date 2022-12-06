using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Profile;
using TravelMate.Data;

namespace TravelMate.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;

        public ProfileService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public async Task<ProfileViewModel> DisplayProfileById(string Id)
        {
            var profile = await context.Users
                .Where(u => u.IsDeleted == false)
                .Where(u => u.Id == Id)
                .Select(u => new ProfileViewModel()
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BirthDate = u.BirthDate.ToString(),
                    Information = u.Information,
                    ProfilePictureUrl = u.ProfilePictureUrl
                })
                .FirstAsync();

            return profile;
        }

        public async Task Edit(string userId, ProfileViewModel model)
        {
            var user = await context.Users.FirstAsync(u => u.Id == userId);

            user.Information = model.Information;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.ProfilePictureUrl = model.ProfilePictureUrl;
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
            string format = "d";

            result = DateTime.ParseExact(date, format, provider);

            return result;
        }
    }
}
