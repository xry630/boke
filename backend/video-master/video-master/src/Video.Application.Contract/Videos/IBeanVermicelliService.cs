using System;
using System.Threading.Tasks;
using Video.Application.Contract.Base;
using Video.Application.Contract.Videos.Dtos;

namespace Video.Application.Contract.Videos
{
    /// <summary>
    /// 关注模块
    /// </summary>
    public interface IBeanVermicelliService
    {
        /// <summary>
        /// 关注用户 | 取消关注
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ConcernAsync(Guid userId);

        /// <summary>
        /// 获取粉丝 | 关注 列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<GetConcernListDto>> GetListAsync(GetListInput input);
    }
}
