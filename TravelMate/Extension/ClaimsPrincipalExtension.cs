using System.Security.Claims;

namespace TravelMate.Extensions
{
    public static class ClaimsPrincipalExtension 
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
