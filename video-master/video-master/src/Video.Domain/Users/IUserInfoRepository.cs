using System;
using System.Collections.Generic;
using Simple.EntityFrameworkCore.Core.Base;
using System.Threading.Tasks;

namespace Video.Domain.Users
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        /// <summary>
        /// 获取用户详细信息
        /// 包括角色
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserInfoRoleView?> GetUserInfoRoleAsync(string username,string password);

        /// <summary>
        /// 获取用户详细信息
        /// 包括角色
        /// </summary>
        /// <returns></returns>
        Task<UserInfoRoleView?> GetAsync(Guid id);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<List<UserInfo>> GetListAsync(string? keywords, DateTime? startTime, DateTime? endTime, int skipCount,
            int maxResultCount);

        /// <summary>
        /// 获取用户总数
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(string? keywords, DateTime? startTime, DateTime? endTime);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        Task EnableAsync(IEnumerable<Guid> ids, bool enable = true);
    }
}
