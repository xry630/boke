using Microsoft.AspNetCore.Mvc;
using Video.Application.Contract.Code;
using Video.Application.Contract.Code.Dtos;
using Video.Domain.Shared;

namespace Video.HttpApi.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CodeController : ControllerBase
{
    private readonly ICodeService _codeService;

    public CodeController(ICodeService codeService)
    {
        _codeService = codeService;
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<string> GetAsync([FromQuery] CodeInput input)
    {
        try
        {
            return await _codeService.GetAsync(input);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"获取验证码失败: {ex.Message}");
        }
    }
}
