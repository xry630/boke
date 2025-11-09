using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Video.Application.Contract.Videos;
using Video.Domain.Shared;
using Video.HttpApi.Host.Options;

namespace Video.HttpApi.Host.Controllers;

/// <summary>
/// 文件管理
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FileController : ControllerBase
{
    private readonly VideoFileOptions _videoFileOptions;
    private readonly IVideoService _videoService;

    public FileController(IVideoService videoService, VideoFileOptions videoFileOptions)
    {
        _videoService = videoService;
        _videoFileOptions = videoFileOptions;
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> Upload(IFormFile file, FileType type)
    {
        // 文件存放目录
        var path = Path.Combine(AppContext.BaseDirectory, "wwwroot", type.ToString().ToLower());

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if(file.Length > _videoFileOptions.MaxFileSize * 1024)
        {
            throw new BusinessException("上传文件大小超过设置的限制");
        }

        var fileName = Guid.NewGuid().ToString("N") + file.FileName;

        using var fileStream = System.IO.File.Create(Path.Combine(path, fileName));

        await file.OpenReadStream().CopyToAsync(fileStream);
        await fileStream.FlushAsync();
        fileStream.Close();

        return type.ToString().ToLower() + "/" + fileName;
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync(string path)
    {
        path = Path.Combine(AppContext.BaseDirectory, "wwwroot", path);

        if (System.IO.File.Exists(path))
        {
            if(await _videoService.WhetheritCanBeDeleted(path))
            {
                System.IO.File.Delete(path);
            }
            else
            {
                throw new BusinessException("您没有权限删除");
            }
        }
    }
}
