using Video.Application.Contract.Base;

namespace Video.Application.Contract.Videos.Dtos
{
    /// <summary>
    /// 分类名称
    /// </summary>
    public class ClassifyDto : EntityDto
    {
        public string? Name { get; set; }
    }
}
