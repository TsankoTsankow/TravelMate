using TravelMate.Core.Models.CountryModels;

namespace TravelMate.Core.Contracts
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryUserViewModel>> GetAllCountries();
        Task<IEnumerable<string>> GetAllCountiresNames();
    }
}
