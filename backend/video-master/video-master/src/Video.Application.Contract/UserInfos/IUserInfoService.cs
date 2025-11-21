using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Video.Application.Contract.Base;
using Video.Application.Contract.UserInfos.Dtos;

namespace Video.Application.Contract.UserInfos
{
    public interface IUserInfoService
    {
        /// <summary>
        /// 登录账号获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserInfoRoleDto> LoginAsync(LoginInput input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserInfoRoleDto> GetAsync();

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <returns></returns>
        Task UpdateAsync(UpdateUserInfoInput input);

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserInfoRoleDto> RegisterAsync(RegisterInput input);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<UserInfoDto>> GetListAsync(GetListInput input);

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
