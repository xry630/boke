using System.Collections.Generic;

namespace Video.Application.Contract.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultDto<T>
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        public IReadOnlyList<T> Items { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="items"></param>
        public PageResultDto(int count, IReadOnlyList<T> items)
        {
            Items = items;
            Count = count;
        }
    }
}