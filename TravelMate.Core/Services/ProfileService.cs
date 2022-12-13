﻿using Microsoft.AspNetCore.Hosting;
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
            var user = await context.Users.FirstAsync(u => u.Id == userId);

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
            string format = "d";

            result = DateTime.ParseExact(date, format, provider);

            return result;
        }
    }
}
