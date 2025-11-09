using Simple.EntityFrameworkCore.Core.Base;

namespace Video.Domain
{
    public class Role : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string? Code { get; set; }
    }
}
