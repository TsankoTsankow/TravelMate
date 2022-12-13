using Microsoft.AspNetCore.Http;

namespace TravelMate.Core.Contracts
{
    public interface IPhotoService
    {
        Task<string> UploadPhoto(IFormFile? photo);
    }
}
