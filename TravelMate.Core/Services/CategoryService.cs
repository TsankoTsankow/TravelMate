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

        /// <summary>
        /// Adds a new category to the database
        /// </summary>
        /// <param name="model">Gets the name and the description of the category</param>
        /// <returns></returns>
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

        /// <summary>
        /// Edits the information for an already created category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Gets a list with basic data about all the categories in the DB
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the names of all the categories in the DB
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetAllCategoriesNames()
        {
            return await context.Categories
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Returns the category with all its information based on its Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
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
