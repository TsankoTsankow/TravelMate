using Microsoft.EntityFrameworkCore;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CategoryModels;
using TravelMate.Infrastructure.Data;

namespace TravelMate.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = await context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryViewModel()
                {
                    Name = c.Name,
                    Id = c.Id
                })
                .ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<string>> GetAllCategoriesNames()
        {
            return await context.Categories
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }
    }
}
