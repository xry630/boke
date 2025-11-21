using System;

namespace Video.Application.Contract.Videos.Dtos
{
    /// <summary>
    /// 发布视频Input
    /// </summary>
    public class CreateVideoInput 
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
    }
}
