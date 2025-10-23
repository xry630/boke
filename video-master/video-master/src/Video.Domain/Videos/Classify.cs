using Simple.EntityFrameworkCore.Core.Base;

namespace Video.Domain.Videos
{
    public class Classify : Entity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string? Name { get; set; }
    }
}
