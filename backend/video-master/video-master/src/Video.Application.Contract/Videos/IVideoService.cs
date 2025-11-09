using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Video.Application.Contract.Base;
using Video.Application.Contract.Videos.Dtos;
using Video.Domain.Videos.Views;

namespace Video.Application.Contract.Videos
{
    public interface IVideoService
    {
        /// <summary>
        /// 发布视频
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateVideoInput input);

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task AdminDeleteAsync(Guid id);

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResultDto<GetVideoListDto>> GetListAsync(GetVideoListInput input);

        /// <summary>
        /// 创建视频分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task CreateClassifyAsync(string name);

        /// <summary>
        /// 删除视频分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteClassifyAsync(Guid id);

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task UpdateClassifyAsync(ClassifyDto dto);

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<ClassifyDto>> GetListClassifyDtoAsync(string? name);

        /// <summary>
        /// 是否能删除当前视频文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> WhetheritCanBeDeleted(string path);


        /// <summary>
        /// 统计时间段关注数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> StatisticalConcernAsync(StatisticalConcernInput input);

        /// <summary>
        /// 统计发布数量
        /// </summary>
        /// <returns></returns>
        Task<int> ReleaseCountasync(VideoInput input);
    }
}