using Video.Domain.Shared;

namespace Video.Application.Contract.Code.Dtos
{
    public class CodeInput
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 验证码类型
        /// </summary>
        public CodeType Type { get; set; }
    }
}