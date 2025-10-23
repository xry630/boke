using Simple.EntityFrameworkCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Video.Domain.Videos.Views;

namespace Video.Domain.Videos
{
    public interface IBeanVermicelliRepository : IRepository<BeanVermicelli>
    {
        /// <summary>
        /// 获取粉丝 | 关注 列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keywords"></param>
        /// <param name="concern"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<ConcernUserListView>> GetConcernUserLists(Guid userId, string? keywords, bool concern, int page = 1, int pageSize = 20);

        /// <summary>
        /// 获取粉丝 | 关注 数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keywords"></param>
        /// <param name="concern"></param>
        /// <returns></returns>
        Task<int> GetConcernUserCount(Guid userId, string? keywords, bool concern);
    }
}
