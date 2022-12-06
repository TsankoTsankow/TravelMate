using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Models.Profile;

namespace TravelMate.Core.Contracts
{
    public interface IProfileService
    {
        Task<ProfileViewModel> DisplayProfileById(string Id);
    }
}
