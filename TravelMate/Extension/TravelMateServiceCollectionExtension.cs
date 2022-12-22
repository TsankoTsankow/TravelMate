using TravelMate.Core.Contracts;
using TravelMate.Core.Services;

namespace TravelMate.Extension
{
    public static class TravelMateServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}
