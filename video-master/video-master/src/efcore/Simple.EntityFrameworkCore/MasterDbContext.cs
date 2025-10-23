using Microsoft.EntityFrameworkCore;

namespace Simple.EntityFrameworkCore
{
    public class MasterDbContext<TDbContext> : DbContext where TDbContext : DbContext
    {
        protected MasterDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 全局禁用跟踪查询
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

#if DEBUG
            // 显示更详细的异常日志
            optionsBuilder.EnableDetailedErrors();
#endif
        }

    }
}