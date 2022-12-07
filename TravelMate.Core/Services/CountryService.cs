using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CountryModels;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext context;

        public CountryService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<CountryUserViewModel>> GetAllCountries()
        {
            var countries = await context.Countries
                .OrderByDescending(c => c.Name)
                .Select(c => new CountryUserViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return countries;
        }
    }
}
