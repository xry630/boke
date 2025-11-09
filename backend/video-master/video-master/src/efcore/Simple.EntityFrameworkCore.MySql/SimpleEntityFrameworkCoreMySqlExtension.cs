using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.EntityFrameworkCore.MySql
{
    public static class SimpleEntityFrameworkCoreMySqlExtension
    {
        public static IServiceCollection AddMySqlEntityFrameworkCore<IDbContext>(this IServiceCollection services,string connect)
            where IDbContext : MasterDbContext<IDbContext>
        {

            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddEntityFrameworkCore<IDbContext>(x =>
            {
                x.UseMySql(configuration.GetConnectionString(connect),new MySqlServerVersion(new Version(8,0,10)));
            },ServiceLifetime.Scoped);

            return services;
        }
    }
}