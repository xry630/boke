using System.Threading.Tasks;
using Video.Application.Contract.Code.Dtos;

namespace Video.Application.Contract.Code
{
    public interface ICodeService
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GetAsync(CodeInput input);
    }
}