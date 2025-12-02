using Microsoft.Extensions.DependencyInjection;
using Simple.EntityFrameworkCore.Sqlite;
using Video.Domain.Users;
using Video.Domain.Videos;
using Video.Domain.Users;
using Video.Domain.Videos;

namespace Video.Domain
{
    public static class VideoEntityFrameworkCoreExtension
    {
        public static IServiceCollection AddVideoEntityFrameworkCore(this IServiceCollection services)
        {
            // 注入efcore
            services.AddSqliteEntityFrameworkCore<VideoDbContext>("Default");

            services.AddTransient<IUserInfoRepository, UserInfoRepository>();

            services.AddTransient<IVideoRepository, VideoRepository>();

            services.AddTransient<IBeanVermicelliRepository, BeanVermicelliRepository>();
            return services;
        }

    }
}
