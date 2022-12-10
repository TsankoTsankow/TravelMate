using Microsoft.AspNetCore.Http;
using TravelMate.Core.Models.Profile;

namespace TravelMate.Core.Contracts
{
    public interface IProfileService
    {
        Task<PersonalProfileViewModel> DisplayProfileById(string Id);

        Task Edit(string Id, EditProfileViewModel model);

        Task<string> UploadPhoto(IFormFile? photo);
    }
}
