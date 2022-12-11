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

        public async Task Add(EditCategoryViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name,
                Description = model.Description
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task Edit(EditCategoryViewModel model)
        {
            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (category == null)
            {
                throw new Exception("No such category");
            }

            category.Name = model.Name;
            category.Description = model.Description;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = await context.Categories
                .OrderByDescending(c => c.Name)
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

        public async Task<EditCategoryViewModel> GetCategoryById(int categoryId)
        {
            return await context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new EditCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstAsync();
        }
    }
}
