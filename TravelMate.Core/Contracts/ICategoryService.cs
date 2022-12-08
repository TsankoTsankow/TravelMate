using TravelMate.Core.Models.CategoryModels;

namespace TravelMate.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
    }
}
