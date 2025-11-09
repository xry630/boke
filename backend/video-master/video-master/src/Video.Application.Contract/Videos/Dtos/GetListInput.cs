using System;
using Video.Application.Contract.Base;

namespace Video.Application.Contract.Videos.Dtos
{
    public class GetListInput : VideoInput
    {
        /// <summary>
        /// 是否关注
        /// 如果为true 查询当前用户关注列表 |false 则查询被关注用户列表
        /// </summary>
        public bool Concern { get; set; }

        /// <summary>
        /// 查询用户id 为空获取当前用户
        /// </summary>
        public Guid? UserId{ get; set; }
    }
}
