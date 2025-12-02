using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.EntityFrameworkCore.Sqlite
{
    public static class SimpleEntityFrameworkCoreSqliteExtension
    {
        public static IServiceCollection AddSqliteEntityFrameworkCore<TDbContext>(this IServiceCollection services,string connect)
            where TDbContext : MasterDbContext<TDbContext>
        {

            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddEntityFrameworkCore<TDbContext>(x =>
            {
                x.UseSqlite(configuration.GetConnectionString(connect));
            },ServiceLifetime.Scoped);

            return services;
        }
    }
}