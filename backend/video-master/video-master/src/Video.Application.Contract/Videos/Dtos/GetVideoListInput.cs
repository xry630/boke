using System;
using Video.Application.Contract.Base;

namespace Video.Application.Contract.Videos.Dtos
{
    public class GetVideoListInput : VideoInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public Guid? ClassifyId { get; set; }
    }
}
