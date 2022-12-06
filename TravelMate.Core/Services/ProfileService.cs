using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
