using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Core.Contracts
{
    public interface IPhotoService
    {
        Task<string> UploadPhoto(IFormFile? photo);

    }
}
