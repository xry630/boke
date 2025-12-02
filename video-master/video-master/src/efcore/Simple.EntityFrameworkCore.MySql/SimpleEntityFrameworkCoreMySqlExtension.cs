using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.EntityFrameworkCore.MySql
{
    public static class SimpleEntityFrameworkCoreMySqlExtension
    {
        public static IServiceCollection AddMySqlEntityFrameworkCore<TDbContext>(this IServiceCollection services,string connect)
            where TDbContext : MasterDbContext<TDbContext>
        {

            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddEntityFrameworkCore<TDbContext>(x =>
            {
                x.UseMySql(configuration.GetConnectionString(connect),new MySqlServerVersion(new Version(8,0,10)));
            },ServiceLifetime.Scoped);

            return services;
        }
    }
}