using Simple.EntityFrameworkCore.Core.Base;
using System;

namespace Video.Domain.Videos
{
    /// <summary>
    /// 关注表
    /// </summary>
    public class BeanVermicelli : Entity
    {
        /// <summary>
        /// 被关注者Id
        /// </summary>
        public Guid BeFocusedId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

    }
}
