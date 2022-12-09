using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMate.Core.Models.ApplicationUser;

namespace TravelMate.Core.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();
        Task<IEnumerable<UserViewModel>> GetAllFriends(string userId);

        Task AddFriend(string userId, string friendId);
        Task SendFriendRequest(string userId, string friendId);

        Task<bool> UsersAreFriends(string userId, string friendId);
    }
}
