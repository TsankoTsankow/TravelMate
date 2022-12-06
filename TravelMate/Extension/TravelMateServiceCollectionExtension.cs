using TravelMate.Core.Contracts;
using TravelMate.Core.Services;

namespace TravelMate.Extension
{
    public static class TravelMateServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();

            return services;
        }
    }
}
