using Microsoft.EntityFrameworkCore;
using Simple.EntityFrameworkCore.Core;
using Simple.EntityFrameworkCore.Extensions;
using System.Linq;
using Video.Domain.Users;

namespace Video.Domain.Users
{
    public class UserInfoRepository : Repository<VideoDbContext, UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(VideoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserInfoRoleView?> GetUserInfoRoleAsync(string username, string password)
        {
            var userInfo = await _dbContext.UserInfo.Where(x => x.Username == username && x.Password == password)
                .Select(x => new UserInfoRoleView()
                {
                    Avatar = x.Avatar,
                    CreatedTime = x.CreatedTime,
                    Enable = x.Enable,
                    Id = x.Id,
                    Name = x.Name,
                    Password = x.Password,
                    Username = x.Username
                })
                .FirstOrDefaultAsync();

            if (userInfo == null)
            {
                return null;
            }

            // 获取当用户的所有角色
            var query =
                from role in _dbContext.Role
                join userRole in _dbContext.UserRole on role.Id equals userRole.RoleId
                where userRole.UserId == userInfo.Id
                select role;

            userInfo.Role = await query.ToListAsync();

            return userInfo;
        }

        public async Task<UserInfoRoleView?> GetAsync(Guid id)
        {

            var userInfo = await _dbContext.UserInfo.Where(x => x.Id == id)
                .Select(x => new UserInfoRoleView()
                {
                    Avatar = x.Avatar,
                    CreatedTime = x.CreatedTime,
                    Enable = x.Enable,
                    Id = x.Id,
                    Name = x.Name,
                    Password = x.Password,
                    Username = x.Username
                })
                .FirstOrDefaultAsync();

            if (userInfo == null)
            {
                return null;
            }

            // 获取当用户的所有角色
            var query =
                from role in _dbContext.Role
                join userRole in _dbContext.UserRole on role.Id equals userRole.RoleId
                where userRole.UserId == userInfo.Id
                select role;

            userInfo.Role = await query.ToListAsync();

            return userInfo;
        }

        public async Task<List<UserInfo>> GetListAsync(string? keywords, DateTime? startTime, DateTime? endTime,
            int skipCount, int maxResultCount)
        {
            var query = CreateQueryable(keywords, startTime, endTime);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync();
        }

        public async Task<int> GetCountAsync(string? keywords, DateTime? startTime, DateTime? endTime)
        {
            var query = CreateQueryable(keywords, startTime, endTime);

            return await query.CountAsync();
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids)
        {
            var idsList = ids.ToList();
            var entities = await _dbContext.UserInfo.Where(x => idsList.Contains(x.Id)).ToListAsync();
            _dbContext.UserInfo.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EnableAsync(IEnumerable<Guid> ids, bool enable = true)
        {
            var idsList = ids.ToList();
            var entities = await _dbContext.UserInfo.Where(x => idsList.Contains(x.Id)).ToListAsync();
            foreach (var entity in entities)
            {
                entity.Enable = enable;
            }
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<UserInfo> CreateQueryable(string? keywords, DateTime? startTime, DateTime? endTime)
        {
            var query =
                _dbContext.UserInfo.WhereIf(!string.IsNullOrEmpty(keywords),
                    x => EF.Functions.Like(x.Name, $"%{keywords}%"!) && EF.Functions.Like(x.Username, $"%{keywords}%"))
                    .WhereIf(startTime.HasValue, x => x.CreatedTime >= startTime)
                    .WhereIf(endTime.HasValue, x => x.CreatedTime <= endTime);

            return query;
        }
    }
}
