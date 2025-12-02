using Microsoft.EntityFrameworkCore;
using Simple.EntityFrameworkCore;
using Video.Domain;
using Video.Domain.Users;
using Video.Domain.Videos;

namespace Video.Domain
{
    public class VideoDbContext : MasterDbContext<VideoDbContext>
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Domain.Videos.Video> Video { get; set; }

        public DbSet<Like> Like { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Classify> Classify { get; set; }

        public DbSet<BeanVermicelli> BeanVermicelli { get; set; }

        public VideoDbContext(DbContextOptions<VideoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInfo>(u =>
            {
                u.ToTable("UserInfos");

                u.HasKey(x => x.Id);
                u.HasIndex(x => x.Id);

                u.Property(x => x.Username);
                u.HasIndex(x => x.Username).IsUnique();

            });

            builder.Entity<Role>(x =>
            {
                x.ToTable("Roles");

                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);

            });

            builder.Entity<UserRole>(x =>
            {
                x.ToTable("UserRoles");

                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);
                x.HasIndex(x => x.UserId);
                x.HasIndex(x => x.RoleId);
            });

            builder.Entity<Domain.Videos.Video>(x =>
            {

                x.ToTable("Videos");

                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);

                x.HasIndex(x => x.ClassifyId);

                x.HasIndex(x => x.UserId);

            });

            builder.Entity<Like>(x =>
            {
                x.ToTable("Likes");

                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);
                x.HasIndex(x => x.UserId);
                x.HasIndex(x => x.VideoId);
            });

            builder.Entity<Comment>(x =>
            {
                x.ToTable("Comments");

                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);
                x.HasIndex(x => x.ParentId);
                x.HasIndex(x => x.UserId);
            });

            builder.Entity<Classify>(x =>
            {
                x.ToTable("Classifys");

                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);
                
            });

            builder.Entity<BeanVermicelli>(x =>
            {
                x.ToTable("BeanVermicellis");
                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Id);
                x.HasIndex(x => x.UserId);
                x.HasIndex(x => x.BeFocusedId);
            });

            #region 初始化数据
            var userInfo = new UserInfo()
            {
                Id = Guid.NewGuid(),
                Avatar = "",
                CreatedTime=DateTime.Now,
                Enable = true,
                Name = "admin",
                Username= "admin",
                Password = "admin",
            };

            var role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                Code = "admin"
            };

            var user = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Code = "user"
            };

            var userRole = new UserRole()
            {
                Id = Guid.NewGuid(),
                RoleId = role.Id,
                UserId = userInfo.Id,
            };
            
            builder.Entity<UserInfo>().HasData(userInfo);
            builder.Entity<Role>().HasData(role, user);
            builder.Entity<UserRole>().HasData(userRole);
            #endregion
        }
    }
}