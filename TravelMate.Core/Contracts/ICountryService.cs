using TravelMate.Core.Models.CountryModels;

namespace TravelMate.Core.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryUserViewModel>> GetAllCountries();
        Task<IEnumerable<string>> GetAllCountiresNames();
        Task Edit(EditCountryViewModel model);
        Task Add(EditCountryViewModel model);
        Task<EditCountryViewModel> GetCountryById(int countryId);
    }
}
