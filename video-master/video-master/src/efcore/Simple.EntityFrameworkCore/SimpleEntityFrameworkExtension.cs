﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Simple.EntityFrameworkCore.Core;
using Simple.EntityFrameworkCore.Core.Base;

namespace Simple.EntityFrameworkCore
{
    public static class SimpleEntityFrameworkExtension
    {
        /// <summary>
        /// efcore核心扩展
        /// </summary>
        /// <typeparam name="IDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFrameworkCore<IDbContext>(
            this IServiceCollection services, 
            Action<DbContextOptionsBuilder>? options = null, 
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where IDbContext : MasterDbContext<IDbContext>
        {
            services.AddDbContext<IDbContext>(options,lifetime);

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork<IDbContext>));

            return services;
        }
    }
}
