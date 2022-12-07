﻿using System.Security.Claims;

namespace TravelMate.Extension
{
    public static class ClaimsPrincipalExtension 
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
