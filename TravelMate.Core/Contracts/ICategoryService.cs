using TravelMate.Core.Models.CategoryModels;

namespace TravelMate.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
        Task<IEnumerable<string>> GetAllCategoriesNames();
        Task Edit(EditCategoryViewModel model);
        Task Add(EditCategoryViewModel model);
        Task<EditCategoryViewModel> GetCategoryById(int categoryId);
    }
}
