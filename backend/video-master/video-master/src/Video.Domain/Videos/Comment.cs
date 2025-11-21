using Simple.EntityFrameworkCore.Core.Base;
using System;
using Video.Domain.Users;

namespace Video.Domain.Videos
{
    /// <summary>
    /// 评论表
    /// </summary>
    public class Comment : Entity
    {
        /// <summary>
        /// 上级评论Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 视频Id
        /// </summary>
        public Guid VideoId { get; set; }

        public virtual UserInfo? User{ get; set; }

        public virtual Video? Video { get; set; }
    }
}
