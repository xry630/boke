﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Video.Application.Contract.Base;
using Video.Application.Contract.UserInfos;
using Video.Application.Contract.UserInfos.Dtos;

namespace Video.HttpApi.Host.Controllers;

/// <summary>
/// 用户
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserInfoController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public UserInfoController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<UserInfoRoleDto> GetAsync()
    {
        return await _userInfoService.GetAsync();
    }

    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task UpdateAsync(UpdateUserInfoInput input)
    {
        await _userInfoService.UpdateAsync(input);
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("list")]
    [Authorize(Roles = "admin")]
    public async Task<PageResultDto<UserInfoDto>> GetListAsync([FromQuery]GetListInput input)
    {
        return await _userInfoService.GetListAsync(input);
    }


    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpDelete("list")]
    public async Task DeleteAsync(IEnumerable<Guid> ids)
    {
        await _userInfoService.DeleteAsync(ids);
    }

    /// <summary>
    /// 禁用用户
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="enable">是否启用</param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
    [HttpPut("enable")]
    public async Task EnableAsync(IEnumerable<Guid> ids, bool enable = true)
    {
        await _userInfoService.EnableAsync(ids, enable);
    }
}
