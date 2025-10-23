﻿using System.Collections.Generic;
using Video.Application.Contract.Base;

namespace Video.Application.Contract.UserInfos.Dtos
{
    public class UserInfoRoleDto : EntityDto
    {
        public UserInfoRoleDto()
        {
            Role = new List<RoleDto>();
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } = true;

        public List<RoleDto> Role { get; set; }
    }
}
