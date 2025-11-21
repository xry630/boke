using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Video.Application.Contract.Base;
using Video.Application.Contract.Videos;
using Video.Application.Contract.Videos.Dtos;

namespace Video.HttpApi.Host.Controllers;

/// <summary>
/// 视频模块
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;

    public VideoController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    /// <summary>
    /// 发布视频
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task CreateAsync(CreateVideoInput input)
    {
        await _videoService.CreateAsync(input);
    }

    /// <summary>
    /// 删除视频
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _videoService.DeleteAsync(id);
    }

    /// <summary>
    /// 获取视频列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("list")]
    [Authorize(Roles = "admin")]
    public async Task<PageResultDto<GetVideoListDto>> GetListAsync([FromQuery] GetVideoListInput input)
    {
        return await _videoService.GetListAsync(input);
    }

    /// <summary>
    /// 管理员删除接口
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("admin/{id}")]
    [Authorize(Roles = "admin")]
    public async Task AdminDeleteAsync(Guid id)
    {
        await _videoService.AdminDeleteAsync(id);
    }

    /// <summary>
    /// 创建视频分类
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPost("classify")]
    [Authorize(Roles = "admin")]
    public async Task CreateClassifyAsync(string name)
    {
        await _videoService.CreateClassifyAsync(name);
    }

    /// <summary>
    /// 删除视频分类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("classify/{id}")]
    [Authorize(Roles = "admin")]
    public async Task DeleteClassifyAsync(Guid id)
    {
        await _videoService.DeleteClassifyAsync(id);
    }

    /// <summary>
    /// 编辑分类
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("classify")]
    [Authorize(Roles = "admin")]
    public async Task UpdateClassifyAsync(ClassifyDto dto)
    {
        await _videoService.UpdateClassifyAsync(dto);
    }

    /// <summary>
    /// 获取分类列表
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("classify/list")]
    [AllowAnonymous]
    public async Task<List<ClassifyDto>> GetListClassifyDtoAsync(string? name)
    {
        return await _videoService.GetListClassifyDtoAsync(name);
    }
}
