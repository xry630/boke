using Simple.EntityFrameworkCore.Core.Base;
using System;

namespace Video.Domain
{
    public class UserRole : Entity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
