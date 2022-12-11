using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CategoryModels;
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
                .Select(c => new CountryUserViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return countries;
        }

        public async Task<IEnumerable<string>> GetAllCountiresNames()
        {
            return await context.Countries
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task Add(EditCountryViewModel model)
        {
            var country = new Country()
            {
                Name = model.Name,
                Description = model.Description, 
                RegionId = 1
            };

            await context.Countries.AddAsync(country);
            await context.SaveChangesAsync();
        }

        public async Task Edit(EditCountryViewModel model)
        {
            var country = await context.Countries
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (country == null)
            {
                throw new Exception("No such country");
            }

            country.Name = model.Name;
            country.Description = model.Description;
            country.RegionId = 1;

            await context.SaveChangesAsync();
        }

        public async Task<EditCountryViewModel> GetCountryById(int countryId)
        {
            return await context.Countries
                .Where(c => c.Id == countryId)
                .Select(c => new EditCountryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description, 
                    RegionId = 1
                })
                .FirstAsync();
        }
    }
}
