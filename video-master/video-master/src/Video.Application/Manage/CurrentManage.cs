using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Video.Domain.Shared;

namespace Video.Application.Manage;

public class CurrentManage
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentManage(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public Guid GetId()
    {
        var value = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constant.Id)?.Value;

        if (string.IsNullOrEmpty(value))
        {
            throw new BusinessException("账号未登录", 401);
        }

        return Guid.Parse(value);
    }

    /// <summary>
    /// 获取角色编码
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetRoles()
    {
        var value = _httpContext.HttpContext.User.Claims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Select(x=>x.Value);

        return value;
    }
}