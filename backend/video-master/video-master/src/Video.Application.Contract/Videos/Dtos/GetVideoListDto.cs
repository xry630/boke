using System;
using Video.Application.Contract.Base;

namespace Video.Application.Contract.Videos.Dtos
{
    public class GetVideoListDto : EntityDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string? VideoUrl { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public Guid ClassifyId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
